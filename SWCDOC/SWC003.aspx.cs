using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Collections;
//using System.Web.Http;

public partial class SWCDOC_SWC003 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
	string max = "";
    string tNo;
	protected void Page_Init(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        //動態生成審查歷程
        GenerateApprove(rSWCNO);
    }
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "tcge7@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";
		
        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：" + errUrl + "<br/>";
        mailText += "錯誤訊息：" + errMsg + "<br/>";

        C1.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
        Response.Redirect("~/errPage/500.htm");
        Server.ClearError();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string op = (Request.QueryString["Token"] + "").Replace(' ', '+');
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();
        Page.MaintainScrollPositionOnPostBack = true;
        if (!IsPostBack)
        {
            
            if (op != "")
            {
                try
                {
                    //解密
                    //*******************
                    byte[] b = Convert.FromBase64String(op);
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    var Key = new Byte[] {  };
                    var IV = new Byte[] {  };
                    ICryptoTransform ict = des.CreateDecryptor(Key, IV);
                    byte[] outData = ict.TransformFinalBlock(b, 0, b.Length);
                    op = Encoding.UTF8.GetString(outData);
                    string[] item = op.Split('|');
                    TimeSpan ts1 = DateTime.Now - DateTime.Parse(item[3]);
                    if (item[4] == "書件")
                    {
                        if ((long)ts1.TotalSeconds > 3600)
                            Response.Redirect("SWC000.aspx?ACT=LogOut");
                        else
                        {
                            Session["ID"] = item[0];
                            Session["UserType"] = item[1];
                            Session["NAME"] = item[2];
                            Session["PW"] = item[5];
                            Session["Unit"] = item[6];
                            Session["JobTitle"] = item[7];
                            Session["Edit4"] = item[8];
                            Session["WMGuild"] = item[9];
                            Session["Guild01"] = item[10];
                            Session["Guild02"] = item[11];
                            Session["ETU_Guild01"] = item[12];
                            Session["ETU_Guild02"] = item[13];
                            Session["ETU_Guild03"] = item[14];
                            Session["ONLINEAPPLY"] = item[15];
                            Session["NUIDNO"] = item[16];
                            Session["NUNAME"] = item[17];
                            Session["NUCELL"] = item[18];
                            Session["NUMAIL"] = item[19];
                            Session["Department"] = item[20];
                            Session["uid"] = item[21];
                            Session["right"] = item[22];
                            Session["grade"] = item[23];
                            Session["TcgeDataedit"] = item[24];
                            Session["TcgeDataview"] = item[25];
                            Session["SuperUser"] = item[26];
                            Session["presented"] = item[27];
                        }
                    }
                    else
                    {
                        Response.Redirect("SWC000.aspx?ACT=LogOut");
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("SWC000.aspx?ACT=LogOut");
                }


                //*******************
            }
            ssUserName = Session["NAME"] + "";
            ssUserType = Session["UserType"] + "";
            ssJobTitle = Session["JobTitle"] + "";
            C20.swcLogRC("SWC003", "水保申請案", "詳情", "瀏覽", rSWCNO);
            SetSwcCase(rSWCNO);
        }
        if (rSWCNO == "" || ssUserType == "")
            Response.Redirect("SWC001.aspx");

		if(ssUserType == "02" && CheckExpireOrNot()==true){Response.Write("<script>alert('提醒您，您所填登之執業執照到期日已逾期，無法使用水保計畫案件申請相關權限，若需恢復權限，請至「帳號管理」中更新「執業執照及到期日」欄位資訊。'); location.href='HaloPage001.aspx';</script>");}
		
        LockArea(ssUserType, rSWCNO);

		if(GVCadastral.Rows.Count > 0) P_Message.Visible = true;
		else P_Message.Visible = false;

        //全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        //全區供用
    }
	private bool CheckExpireOrNot()
	{
        string ssUserID = Session["ID"] + "";
		bool YN = false;
		string gDNSQLStr =  "select * from ETUsers where ((TCNo01ED < getdate() and TCNo01ED != '1900-01-01 00:00:00.000') ";
		gDNSQLStr += " or (TCNo02ED < getdate()  and TCNo02ED != '1900-01-01 00:00:00.000') ";
		gDNSQLStr += " or (TCNo03ED < getdate()  and TCNo03ED != '1900-01-01 00:00:00.000') ";
		gDNSQLStr += " or (TCNo04ED < getdate()  and TCNo04ED != '1900-01-01 00:00:00.000')) and ETID = @ETID ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = gDNSQLStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@ETID", ssUserID));
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                        YN = true;
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
		return YN;
	}
    private void GenerateApprove(string s)
    {
        GBClass001 SBApp = new GBClass001();
        pdata.Controls.Clear();
        var span = new HtmlGenericControl("span");
        var a = new HtmlGenericControl("a");
        var button = new HtmlGenericControl("Button");
        string tsql = " select ISNULL(max(DTLA006),'0') AS DTLA006 from SWCDTL01 Where SWC000='" + s + "';";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader reader;
            SqlCommand sc = new SqlCommand(tsql, SWCConn);
            reader = sc.ExecuteReader();

            while (reader.Read())
                max = reader["DTLA006"].ToString();
            reader.Close();
            sc.Dispose();
        }
        //if (max == "0") pdata2.Visible = false;
        for (int i = 1; i <= Convert.ToInt32(max); i++)
        {
            //span 第幾次審查
            span = new HtmlGenericControl("span");
            span.InnerText = "第" + i.ToString() + "次審查";
            span.Attributes["class"] = "R_title RT1";
            pdata.Controls.Add(span);
            //span hr
            span = new HtmlGenericControl("span");
            span.Attributes["class"] = "hr";
            pdata.Controls.Add(span);

            DataTable OBJ_GV01 = (DataTable)ViewState["GV"];
            DataTable DTGV01 = new DataTable();
            if (OBJ_GV01 == null)
            {
                DTGV01.Columns.Add(new DataColumn("GV_1", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_2", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_3", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_4", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_5", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_6", typeof(string)));
                //DTGV01.Columns.Add(new DataColumn("GV_7", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_8", typeof(string)));
                DTGV01.Columns.Add(new DataColumn("GV_9", typeof(string)));

                ViewState["GV"] = DTGV01;
                OBJ_GV01 = DTGV01;
            }

            tsql = " select * from SWCDTL01 Where SWC000='" + s + "' and DTLA006='" + i.ToString() + "' order by DTLA001;";
            GridView gv = new GridView();
            //gv.ID = OBJ_GV01.TableName;
            gv.ID = "GV" + i.ToString();
            gv.AllowPaging = false;
            gv.AllowSorting = true;
            gv.AutoGenerateColumns = false;
            gv.CssClass = "R_TB1";

            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                SqlDataReader reader = null;
                SqlCommand sc = new SqlCommand(tsql, SWCConn);
                reader = sc.ExecuteReader();



                while (reader.Read())
                {
                    DataRow dr01 = OBJ_GV01.NewRow();
                    dr01["GV_1"] = reader["DTLA001"];
                    dr01["GV_2"] = SBApp.DateView(reader["savedate"].ToString(), "00");
                    dr01["GV_3"] = SBApp.DateView(reader["DTLA003"].ToString(), "00");
                    dr01["GV_4"] = reader["DTLA004"];
                    dr01["GV_5"] = SBApp.DateView(reader["DTLA007"].ToString(), "00");
                    dr01["GV_6"] = reader["DTLA034"];
                    //dr01["GV_7"] = i.ToString();
                    dr01["GV_8"] = reader["DTLA001"];
                    dr01["GV_9"] = reader["DATALOCK"];
					if(reader["DATALOCK"].ToString()!="Y")
					{
						if(reader["Saveuser"].ToString()==Session["ID"].ToString()) OBJ_GV01.Rows.Add(dr01);
					}
					else
					{
						OBJ_GV01.Rows.Add(dr01);
					}
                }

                reader.Close();
                sc.Dispose();
            }


            foreach (DataColumn c in OBJ_GV01.Columns)
            {
                if (c.ColumnName == "GV_1") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "審查表單編號"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_2") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "函送日期"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_3") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "補正期限"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_4") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "主旨"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_5") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "開會日期"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_6") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "重新上傳原因"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_7") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "TEST"; gv.Columns.Add(bf); }
                if (c.ColumnName == "GV_8")
                {
                    //HyperLinkField hlf = new HyperLinkField();
                    //hlf.Text = "詳情";
                    //hlf.NavigateUrl = "SWCDT001.aspx?SWCNO=" + s + "&DTLNO=";
                    //hlf.Visible = false;
                    //gv.Columns.Add(hlf);
                    BoundField bf = new BoundField();
                    gv.Columns.Add(bf);
                }
                if (c.ColumnName == "GV_9") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; gv.Columns.Add(bf); }
            }
            gv.DataSource = OBJ_GV01;
            gv.DataBind();
            pdata.Controls.Add(gv);
            OBJ_GV01.Clear();

            foreach (GridViewRow rw in gv.Rows)
            {
                if (rw.RowType == DataControlRowType.DataRow)
                {
                    //HyperLinkField hl = ((HyperLinkField)rw.Cells[6].FindControl("fd"));
                    //rw.Cells[0].Text = "";
                    //hl.NavigateUrl = "";
                    //Button btn1 = new Button();
                    //btn1.Text = "詳情";
                    //btn1.ID = gv.ID;
                    //btn1.Click += new EventHandler(this.GoCase_Click);
                    //rw.Cells[6].Controls.Add(btn1);

                    HyperLink hlf = new HyperLink();
					if(rw.Cells[7].Text == "Y")
					{
						hlf.Text = "詳情";
						hlf.NavigateUrl = "SWCDT001.aspx?SWCNO=" + s + "&DTLNO=" + rw.Cells[0].Text;
						rw.Cells[6].Controls.Add(hlf);
					}
					else
					{
						hlf.Text = "編輯";
						hlf.NavigateUrl = "SWCDT001.aspx?SWCNO=" + s + "&DTLNO=" + rw.Cells[0].Text;
						rw.Cells[6].Controls.Add(hlf);
						Button btn = new Button();
						rw.Cells[6].Controls.Add(btn);
						btn.Click += new EventHandler(Btn_Click);
						btn.Text = "刪除";
						btn.CommandArgument = rw.Cells[0].Text;
                    }
                }
            }
			//隱藏最後一欄
			gv.Columns[7].Visible = false;

            //span 修正本
            span = new HtmlGenericControl("span");
            span.Attributes["class"] = "R_upload";
            span.Attributes["style"] = "width:420px;";
            span.InnerHtml = "<b>修正本：</b>";


            string SFName = ""; string createdate = ""; string savedate = ""; string saveuser = "";
            string sqlStr1 = " SELECT * FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE";
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();
                using (var cmd = SWCConn.CreateCommand())
                {
                    cmd.CommandText = sqlStr1;
                    cmd.Parameters.Add(new SqlParameter("@SWC000", s));
                    tNo = "00" + i.ToString();
                    cmd.Parameters.Add(new SqlParameter("@SFTYPE", tNo.Substring(tNo.Length - 3, 3)));
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerSWC = cmd.ExecuteReader())
                    {
                        if (readerSWC.HasRows)
                            while (readerSWC.Read())
                            {
                                SFName = readerSWC["SFName"] + "";
                                createdate = readerSWC["createdate"] + "";
                                savedate = readerSWC["savedate"] + "";
                                saveuser = readerSWC["saveuser"] + "";
                            }
                        readerSWC.Close();
                    }
                    cmd.Cancel();
                }
            }

            //修正本
            a = new HtmlGenericControl("a");
            a.InnerText = SFName;
            a.Attributes["href"] = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + s + "//" + SFName;
            span.Controls.Add(a);



            //span.Controls.Add(new LiteralControl("<input type='file' id='GVF_FileUpload' style='height:25px;'>"));
            //span.Controls.Add(new LiteralControl("<input type='submit' id='Btn_UPSFile05'  value='上傳檔案' style='height:25px;' OnClick='Btn_UPSFile_Click'>"));
            //span.Controls.Add(new LiteralControl("&nbsp;"));
            //span.Controls.Add(new LiteralControl("<input type='submit' id='Btn_DelSFile05' value='x' onclick='return confirm('刪除後無法復原，請再次確認是否要刪除!!!');'  style='height:25px;'>"));

            pdata.Controls.Add(span);


            //span 第一次上傳日期
            span = new HtmlGenericControl("span");
            span.Attributes["class"] = "R_upload";
            span.InnerHtml = "<b>第一次上傳日期：</b>" + SBApp.DateView(createdate, "04");
            pdata.Controls.Add(span);

            //span 最後上傳日期
            span = new HtmlGenericControl("span");
            span.Attributes["class"] = "R_upload";
            span.InnerHtml = "<b>最後上傳日期：</b>" + SBApp.DateView(savedate, "04");
            pdata.Controls.Add(span);

            //span 最後更新人員
            span = new HtmlGenericControl("span");
            span.Attributes["class"] = "R_upload";
            span.InnerHtml = "<b>最後更新人員：</b>" + saveuser;
            pdata.Controls.Add(span);





            //最後一次審查
            if (i != Convert.ToInt32(max))
            {
                pdata.Controls.Add(new LiteralControl("<br />"));
                pdata.Controls.Add(new LiteralControl("<br />"));
            }
            else
            {
				pdata.Controls.Add(new LiteralControl("<br />"));
                pdata.Controls.Add(new LiteralControl("<br />"));
            
                //pdata.Controls.Add(new LiteralControl("<span class='red' style='font-weight:bold; display:block; margin:10px 0;'>※ 上傳格式限定為PDF，檔案大小請於150mb以內</span>"));
            }
        }
    }

	private void Btn_Click(object sender, EventArgs e)
	{
		Response.Write("<script>alert('是否確定刪除');</script>");
		Button btn = (Button)sender;
		string s = Request.QueryString["SWCNO"] + "";
		string tsql = " delete SWCDTL01 Where SWC000=@SWC000 and DTLA000=@DTLA000;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
			using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = tsql;
                cmd.Parameters.Add(new SqlParameter("@SWC000", s));
                cmd.Parameters.Add(new SqlParameter("@DTLA000", btn.CommandArgument.ToString()));
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
		Response.Write("<script type='text/javascript'> location.href = 'SWC003.aspx?SWCNO="+Request.QueryString["SWCNO"]+"';</script>");
	}
	
    private void LockArea(string v, string v2)
    {
        string ssUserID = Session["ID"] + "";
        string ssUserGuild1 = Session["ETU_Guild01"] + "";
        string ssUserGuild2 = Session["ETU_Guild02"] + "";
        string ssUserGuild3 = Session["ETU_Guild03"] + "";

        string tCaseStatus = LBSWC004.Text + "";
        string tCaseType = LBSWC007.Text + "";
        string tSWC021ID = LBSWC021ID.Text + "";
        string tSWC045ID = LBSWC045ID.Text + "";

        string tSWC022ID = LBSWC022ID.Text + "";
        string tSWC024ID = LBSWC024ID.Text + "";
        string tSWC107ID = LBSWC107ID.Text + "";
		
		string tSWC134ID = LBSWC134ID.Text + "";
		string tSWC135ID = LBSWC135ID.Text + "";

        EditCase.Visible = false;
        GoTslm.Visible = false;
        TitleLink00.Visible = false;

        //申請表新增按鈕
        SWCDTL0302.Columns[5].Visible = false;
        SWCDTL06.Columns[6].Visible = false;
        DTL_02_01_Link.Visible = false;
        SWCOLA201.Columns[5].Visible = false;
        DTL_02_02_Link.Visible = false;
        SWCOLA202.Columns[5].Visible = false;
        DTL_02_03_Link.Visible = false;
        DTL_02_03_1_Link.Visible = false;
        SWCOLA203.Columns[6].Visible = false;
        SWCOLA203_1.Columns[6].Visible = false;
        DTL_02_04_Link.Visible = false;
        DTL_02_04_1_Link.Visible = false;
        SWCOLA204.Columns[5].Visible = false;
        SWCOLA204_1.Columns[5].Visible = false;
        DTL_02_05_Link.Visible = false;
        SWCOLA205.Columns[4].Visible = false;
        DTL_02_06_Link.Visible = false;
        SWCOLA206.Columns[4].Visible = false;
        DTL_02_07_Link.Visible = false;
        SWCOLA207.Columns[6].Visible = false;
        DTL_02_08_Link.Visible = false;
        SWCOLA208.Columns[6].Visible = false;
        DTL_02_09_Link.Visible = false;
        SWCOLA209.Columns[5].Visible = false;
		
		//SWC131 失效重核(Y或空)
        //SWC059 完工證明書核發日期
        //SWC051 開工日期 SWC038 核定日期
        string chkGuildType = " select * from SWCCASE Where SWC000='" + v2 + "' and ISNULL(SWC131,'') <> 'Y' and ((SWC059>1900-01-01) or (SWC051>1900-01-01 and DateAdd(YEAR,6,SWC038)>=getdate()) or (SWC038>1900-01-01 and DateAdd(YEAR,4,SWC038)>=getdate())); ";
		bool reapprove = false;
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(chkGuildType, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
                reapprove = true;
            readerData.Close();
            objCmdRV.Dispose();
        }
		
		string chkHasData = " select ISNULL(max(DTLA006),'0') AS DTLA006 from SWCDTL01 Where SWC000='" + v2 + "'; ";
		bool upload = false;
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(chkHasData, SWCConn);
            readerData = objCmdRV.ExecuteReader();
			int i = 0;
            while (readerData.Read()){
				i = Convert.ToInt32(readerData["DTLA006"]);
				if(i>0){
					upload = true;
				}
			}
            readerData.Close();
            objCmdRV.Dispose();
        }

        if (ssUserID == "gv-admin")
        { }
        else
        {
            BTNTimes1.NavigateUrl = "../SWCDOC/SWCTimes.aspx?SWCNO=" + v2;
            BTNTimes2.NavigateUrl = "../SWCDOC/SWCTimes.aspx?SWCNO=" + v2;
            P1906.Visible = true; HL1906.NavigateUrl = "../SWCDOC/OnlineApply006.aspx?SWCNO=" + v2 + "&OLANO=AddNew";

            Area02.Visible = false;

            if (ssUserID != ssUserGuild1 && v == "04") v = "87";
            switch (v)
            {
                case "01":
                    //水保義務人
                    Area02.Visible = true;

                    switch (tCaseStatus)
                    {
                        case "審查中":
                            DTL_02_02_Link.Visible = true;
                            break;
                        case "已核定":
                            DTL_02_03_Link.Visible = true;
                            DTL_02_05_Link.Visible = true;
                            CopyCase.Visible = true;
                            break;
                        case "施工中":
                            DTL_02_05_Link.Visible = true;
                            DTL_02_07_Link.Visible = true;
                            DTL_02_08_Link.Visible = true;
                            DTL_02_09_Link.Visible = true;
                            CopyCase.Visible = true;
                            break;
                        case "停工中":
                            DTL_02_03_1_Link.Visible = true;
                            DTL_02_05_Link.Visible = true;
                            CopyCase.Visible = true;
                            break;
                        case "已完工":
                            DTL_02_01_Link.Visible = true;
                            break;
                    }
                    //DTL_02_06_Link.Visible = true;
                    SWCOLA201.Columns[5].Visible = true;
                    SWCOLA202.Columns[5].Visible = true;
                    SWCOLA203.Columns[6].Visible = true;
                    SWCOLA203_1.Columns[6].Visible = true;
                    SWCOLA204.Columns[5].Visible = true;
                    SWCOLA204_1.Columns[5].Visible = true;
                    SWCOLA205.Columns[4].Visible = true;
                    SWCOLA206.Columns[4].Visible = true;
                    SWCOLA207.Columns[6].Visible = true;
                    SWCOLA208.Columns[6].Visible = true;
                    SWCOLA209.Columns[5].Visible = true;

                    break;
                case "02":
                case "08":
                    imgU2.Visible = true;
                    //技師(承辦、監造)
                    if (tCaseStatus == "施工中") { P1908.Visible = true; HL1908.NavigateUrl = "../SWCDOC/OnlineApply008.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                    //承辦技師
                    if (tSWC021ID == ssUserID)
                    {
                        Area02.Visible = true;
                        if (tCaseStatus == "退補件" && v == "02") { P2002.Visible = true; HL2002.NavigateUrl = "../SWCAPPLY/SwcApply2002.aspx?SWC000=" + v2 + "&SA20ID=addnewapply"; }
                        if (tCaseStatus == "審查中") { P1902.Visible = true; HL1902.NavigateUrl = "../SWCDOC/OnlineApply002.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "已核定") { P1903.Visible = true; HL1903.NavigateUrl = "../SWCDOC/OnlineApply003.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "已核定") { P1904.Visible = true; HL1904.NavigateUrl = "../SWCDOC/OnlineApply004.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "已核定") { P1905.Visible = true; HL1905.NavigateUrl = "../SWCDOC/OnlineApply005.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
						if (tCaseStatus == "暫停審查") { P1910.Visible = true; HL1910.NavigateUrl = "../SWCAPPLY/OnlineApply010.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "失效" && reapprove) { P1911.Visible = true; HL1911.NavigateUrl = "../SWCDOC/OnlineApply011.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
						if (tCaseStatus == "審查中" && upload && v == "02"){
							PFileUpload.Visible = true;
							HLFileUpload.NavigateUrl = "../SWCDOC/SWCUpload.aspx?SWCNO=" + v2 ;
						}
					}

                    //監造技師
                    if (tSWC045ID == ssUserID)
                    {
                        if (tCaseStatus == "停工中") { P1904_1.Visible = true; HL1904_1.NavigateUrl = "../SWCDOC/OnlineApply004.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "停工中") { P1903_1.Visible = true; HL1903_1.NavigateUrl = "../SWCDOC/OnlineApply003.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "施工中") { P1907.Visible = true; HL1907.NavigateUrl = "../SWCDOC/OnlineApply007.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "施工中") { P1909.Visible = true; HL1909.NavigateUrl = "../SWCDOC/OnlineApply009.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { P1905.Visible = true; HL1905.NavigateUrl = "../SWCDOC/OnlineApply005.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { PDTL4.Visible = true; HLDTL4.NavigateUrl = "../SWCDOC/SWCDT004.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { PDTL5.Visible = true; HLDTL5.NavigateUrl = "../SWCDOC/SWCDT005.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }
						if (tCaseStatus == "失效" && reapprove) { P1911.Visible = true; HL1911.NavigateUrl = "../SWCDOC/OnlineApply011.aspx?SWCNO=" + v2 + "&OLANO=AddNew"; }
                    }

                    switch (tCaseStatus)
                    {
                        case "審查中":
                            DTL_02_02_Link.Visible = true;
                            break;
                        case "已核定":
                            DTL_02_03_Link.Visible = true;
                            DTL_02_04_Link.Visible = true;
                            DTL_02_05_Link.Visible = true;
                            CopyCase.Visible = true;
                            break;
                        case "施工中":
                            DTL_02_05_Link.Visible = true;
                            DTL_02_07_Link.Visible = true;
                            DTL_02_08_Link.Visible = true;
                            DTL_02_09_Link.Visible = true;
                            CopyCase.Visible = true;
                            break;
                        case "停工中":
                            CopyCase.Visible = true;
                            DTL_02_03_1_Link.Visible = true;
                            DTL_02_04_1_Link.Visible = true;
                            break;
                        case "已完工":
                            DTL_02_01_Link.Visible = true;
                            break;
                    }
                    //DTL_02_06_Link.Visible = true;
                    SWCOLA201.Columns[5].Visible = true;
                    SWCOLA202.Columns[5].Visible = true;
                    SWCOLA203.Columns[6].Visible = true;
                    SWCOLA203_1.Columns[6].Visible = true;
                    SWCOLA204.Columns[5].Visible = true;
                    SWCOLA204_1.Columns[5].Visible = true;
                    SWCOLA205.Columns[4].Visible = true;
                    SWCOLA206.Columns[4].Visible = true;
                    SWCOLA207.Columns[6].Visible = true;
                    SWCOLA208.Columns[6].Visible = true;
                    SWCOLA209.Columns[5].Visible = true;

                    break;
                case "03":
                    //大地只能看… 20220526多加上抽查的表(從資料庫搬過來書件)
                    Area02.Visible = true;
					imgU3.Visible = true;
					PDTL2.Visible = true;
					HLDTL2.NavigateUrl = "../SWCDOC/SWCDT002.aspx?SWCNO=" + v2 + "&DTLNO=AddNew";
                    break;
                case "04":
                case "09":
                    Area03.Visible = false;
                    Area04.Visible = false;
                    Area05.Visible = false;
                    imgU4.Visible = true;

                    //審查公會
                    if (tSWC022ID == ssUserID)
                    {
                        Area02.Visible = true;
                        Area03.Visible = true;
                        //if (tCaseStatus == "審查中" || tCaseStatus == "暫停審查") { EditCase.Visible = true; }
                        if (tCaseStatus == "審查中" && v == "04") { P2001.Visible = true; HL2001.NavigateUrl = "../SWCAPPLY/SwcApply2001.aspx?SWC000=" + v2 + "&SA20ID=addnewapply"; }
                        if (tCaseStatus == "審查中") { PDTL1.Visible = true; HLDTL1.NavigateUrl = "../SWCDOC/SWCDT001.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }

                    }
                    if (tSWC024ID == ssUserID) //檢查公會
                    {
                        Area03.Visible = true;
                        Area04.Visible = true;
                        if (tCaseStatus == "施工中")
                        {
                            SWCDTL01.Visible = false;
                            //EditCase.Visible = true;
                            Area04.Visible = true;
                        }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { PDTL3.Visible = true; HLDTL3.NavigateUrl = "../SWCDOC/SWCDT003.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { PDTL6.Visible = true; HLDTL6.NavigateUrl = "../SWCDOC/SWCDT006.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }

                        SWCDTL0302.Columns[5].Visible = true;
                        SWCDTL06.Columns[6].Visible = true;
                    }
                    if (Session["Edit4"] + "" == "Y")  //完工檢查公會
                    {
                        if (tCaseStatus == "已完工") { PDTL7.Visible = true; HLDTL7.NavigateUrl = "../SWCDOC/SWCDT007.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }
                        if (tCaseStatus == "已完工" || tCaseStatus == "撤銷" || tCaseStatus == "已變更")
                        {
                            //EditCase.Visible = true;
                            Area03.Visible = true;
                            Area04.Visible = true;
                            Area05.Visible = true;
                        }
                    }
                    break;
				//建築師瀏覽權限同技師 但不能出現按鈕
				case "06":
                    imgU2.Visible = false;
					
                    SWCOLA201.Columns[5].Visible = true;
                    SWCOLA202.Columns[5].Visible = true;
                    SWCOLA203.Columns[6].Visible = true;
                    SWCOLA203_1.Columns[6].Visible = true;
                    SWCOLA204.Columns[5].Visible = true;
                    SWCOLA204_1.Columns[5].Visible = true;
                    SWCOLA205.Columns[4].Visible = true;
                    SWCOLA206.Columns[4].Visible = true;
                    SWCOLA207.Columns[6].Visible = true;
                    SWCOLA208.Columns[6].Visible = true;
                    SWCOLA209.Columns[5].Visible = true;

                    break;
                case "87":
                    string tUpdate = "N";
                    string tShowArea = "N";
                    imgU4.Visible = true;
                    //代審查
                    if (tSWC022ID == ssUserGuild1 || tSWC022ID == ssUserGuild3)
                    {
                        tShowArea = chkUpdateStatus(tShowArea, "S3");
                        if (tShowArea == "Y")
                        {
                            Area02.Visible = true;
                            Area03.Visible = true;
                            //if (tCaseStatus == "審查中" || tCaseStatus == "暫停審查")
                            //    EditCase.Visible = true;
                            //tUpdate = "Y";
                        }
                        if (tCaseStatus == "審查中") { P2001.Visible = true; HL2001.NavigateUrl = "../SWCAPPLY/SwcApply2001.aspx?SWC000=" + v2 + "&SA20ID=addnewapply"; }
                        if (tCaseStatus == "審查中") { PDTL1.Visible = true; HLDTL1.NavigateUrl = "../SWCDOC/SWCDT001.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }
                    }
                    //代檢查
                    if (tSWC024ID == ssUserGuild2 || tSWC024ID == ssUserGuild3)
                    {
                        tShowArea = "N";
                        tShowArea = chkUpdateStatus(tShowArea, "S4");
                        if (tShowArea == "Y")
                        {
                            Area03.Visible = true;
                            Area04.Visible = true;
                            if (tCaseStatus == "施工中")
                            {
                                SWCDTL01.Visible = false;
                                //EditCase.Visible = true;
                                Area04.Visible = true;
                            }
                            //tUpdate = "Y";
                        }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { PDTL3.Visible = true; HLDTL3.NavigateUrl = "../SWCDOC/SWCDT003.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }
                        if (tCaseStatus == "施工中" || tCaseStatus == "停工中") { PDTL6.Visible = true; HLDTL6.NavigateUrl = "../SWCDOC/SWCDT006.aspx?SWCNO=" + v2 + "&DTLNO=AddNew"; }

                        SWCDTL0302.Columns[5].Visible = true;
                        SWCDTL06.Columns[6].Visible = true;
                    }
                    //if (tUpdate == "Y") { EditCase.Visible = true; }
                    break;
            }
        }











        switch (v)
        {
            case "01":  //水保義務人
                if (tCaseType == "簡易水保")
                    if (tCaseStatus == "退補件" || tCaseStatus == "申請中")
                        EditCase.Visible = true;
                break;
            case "02":  //技師
                TitleLink00.Visible = true;
                SWCDTL01.Visible = false;
                Area04.Visible = false;
                Area05.Visible = false;

                if (tSWC021ID == ssUserID)  //承辦技師
                    SWCDTL01.Visible = true;
                if (tCaseStatus == "退補件" || tCaseStatus == "受理中" || tCaseStatus == "申請中" || tCaseStatus == "審查中")
                    EditCase.Visible = true;
                if (tSWC045ID == ssUserID)  //監告技師
                {
                    Area04.Visible = true;
                    //if (tCaseStatus == "施工中")
                    //    EditCase.Visible = true;
                }
                break;

            case "03":  //大地只能看…
                GoTslm.Visible = true;
                EditCase.Visible = false;
                break;

        }
        if (ssUserID == "gv-admin")
        {
            EditCase.Visible = true;
            Area04.Visible = true;
            Area05.Visible = true;
        }
    }

    private string chkUpdateStatus(string vO, string tmpRGType)
    {
        string rValue = vO;
        string ssUserID = Session["ID"] + "";
        string pgSWC000 = LBSWC000.Text;
        //召集人才能改
        string chkGuildType = " select * from GuildGroup Where ETID='" + ssUserID + "' And RGType='" + tmpRGType + "' and CHGType='1' and swc000='" + pgSWC000 + "'; ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(chkGuildType, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
                rValue = "Y";
            readerData.Close();
            objCmdRV.Dispose();
        }
        return rValue;
    }
    private void SetSwcCase(string v)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string qSWC120 = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE where SWC000 = '" + v + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();
            SBApp.SWCRecord("SWC003", v, strSQLRV);

            while (readeSwc.Read())
            {
                #region-
                string qSWC000 = readeSwc["SWC000"] + "";
                string qSWC002 = readeSwc["SWC002"] + "";
                string qSWC004 = readeSwc["SWC004"] + "";
                string qSWC005 = readeSwc["SWC005"] + "";
                string qSWC007 = readeSwc["SWC007"] + "";
                string qSWC013 = readeSwc["SWC013"] + "";
                string qSWC013ID = readeSwc["SWC013ID"] + "";
                string qSWC013TEL = readeSwc["SWC013TEL"] + "";
                string qSWC014 = readeSwc["SWC014"] + "";
                string qSWC015 = readeSwc["SWC015"] + "";
                string qSWC016 = readeSwc["SWC016"] + "";
                string qSWC017 = readeSwc["SWC017"] + "";
                string qSWC018 = readeSwc["SWC018"] + "";
                string qSWC021 = readeSwc["SWC021"] + "";
                string qSWC021ID = readeSwc["SWC021ID"] + "";
                string qSWC022 = readeSwc["SWC022"] + "";
                string qSWC022ID = readeSwc["SWC022ID"] + "";
                string qSWC023 = readeSwc["SWC023"] + "";
                string qSWC024 = readeSwc["SWC024"] + "";
                string qSWC024ID = readeSwc["SWC024ID"] + "";
                string qSWC025 = readeSwc["SWC025"] + "";
                string qSWC027 = readeSwc["SWC027"] + "";
                string qSWC028 = readeSwc["SWC028"] + "";
                string qSWC029 = readeSwc["SWC029"] + "";
                string qSWC029CAD = readeSwc["SWC029CAD"] + "";
                string qSWC030 = readeSwc["SWC030"] + "";
                string qSWC030CAD = readeSwc["SWC030CAD"] + "";
                string qSWC031 = readeSwc["SWC031"] + "";
                string qSWC032 = readeSwc["SWC032"] + "";
                string qSWC033 = readeSwc["SWC033"] + "";  //22
                string qSWC034 = readeSwc["SWC034"] + "";
                string qSWC035 = readeSwc["SWC035"] + "";
                string qSWC036 = readeSwc["SWC036"] + "";
                string qSWC038 = readeSwc["SWC038"] + "";
                string qSWC039 = readeSwc["SWC039"] + "";
                string qSWC040 = readeSwc["SWC040"] + "";
                string qSWC041 = readeSwc["SWC041"] + "";
                string qSWC043 = readeSwc["SWC043"] + "";
                string qSWC044 = readeSwc["SWC044"] + "";
                string qSWC045 = readeSwc["SWC045"] + "";
                string qSWC045ID = readeSwc["SWC045ID"] + "";
                string qSWC047 = readeSwc["SWC047"] + "";
                string qSWC048 = readeSwc["SWC048"] + "";
                string qSWC049 = readeSwc["SWC049"] + "";
                string qSWC050 = readeSwc["SWC050"] + "";
                string qSWC051 = readeSwc["SWC051"] + "";
                string qSWC052 = readeSwc["SWC052"] + "";
                string qSWC053 = readeSwc["SWC053"] + "";
                string qSWC056 = readeSwc["SWC056"] + "";
                string qSWC058 = readeSwc["SWC058"] + "";
                string qSWC059 = readeSwc["SWC059"] + "";
                string qSWC061 = readeSwc["SWC061"] + "";
                string qSWC062 = readeSwc["SWC062"] + "";
                string qSWC063 = readeSwc["SWC063"] + "";
                string qSWC064 = readeSwc["SWC064"] + "";
                string qSWC065 = readeSwc["SWC065"] + "";
                string qSWC066 = readeSwc["SWC066"] + "";
                string qSWC067 = readeSwc["SWC067"] + "";
                string qSWC068 = readeSwc["SWC068"] + "";
                string qSWC069 = readeSwc["SWC069"] + "";
                string qSWC070 = readeSwc["SWC070"] + "";
                string qSWC071 = readeSwc["SWC071"] + "";
                string qSWC072 = readeSwc["SWC072"] + "";
                string qSWC073 = readeSwc["SWC073"] + "";
                string qSWC074 = readeSwc["SWC074"] + "";
                string qSWC075 = readeSwc["SWC075"] + "";
                string qSWC080 = readeSwc["SWC080"] + "";
                string qSWC082 = readeSwc["SWC082"] + "";
                string qSWC083 = readeSwc["SWC083"] + "";
                string qSWC084 = readeSwc["SWC084"] + "";
                string qSWC087 = readeSwc["SWC087"] + "";
                string qSWC088 = readeSwc["SWC088"] + "";
                string qSWC089 = readeSwc["SWC089"] + "";
                string qSWC092 = readeSwc["SWC092"] + "";
                string qSWC093 = readeSwc["SWC093"] + "";
                string qSWC094 = readeSwc["SWC094"] + "";
                string qSWC095 = readeSwc["SWC095"] + "";
                string qSWC101 = readeSwc["SWC101"] + "";
                string qSWC101CAD = readeSwc["SWC101CAD"] + "";
                string qSWC103 = readeSwc["SWC103"] + "";
                string qSWC104 = readeSwc["SWC104"] + "";
                string qSWC105 = readeSwc["SWC105"] + "";
                string qSWC106 = readeSwc["SWC106"] + "";
                string qSWC107 = readeSwc["SWC107"] + "";
                string qSWC107ID = readeSwc["SWC107ID"] + "";
                string qSWC108 = readeSwc["SWC108"] + "";
                string qSWC109 = readeSwc["SWC109"] + "";
                string qSWC110 = readeSwc["SWC110"] + "";
                string qSWC112 = readeSwc["SWC112"] + "";
                string qSWC113 = readeSwc["SWC113"] + "";
                string qSWC114 = readeSwc["SWC114"] + "";
                string qSWC114_2 = readeSwc["SWC114_2"] + "";
                string qSWC115 = readeSwc["SWC115"] + "";
                string qSWC116 = readeSwc["SWC116"] + "";
                string qSWC118 = readeSwc["SWC118"] + "";
                string qSWC119 = readeSwc["SWC119"] + "";
                string qSWC125 = readeSwc["SWC125"] + "";
				string qSWC131 = readeSwc["SWC131"] + "";
                string qSWC132 = readeSwc["SWC132"] + "";
                string qSWC133 = readeSwc["SWC133"] + "";
                string qSWC134 = SBApp.GetArchitectUser(readeSwc["SWC134"] + "","Name") + "";
				string qSWC134ID = readeSwc["SWC134"] + "";
                string qSWC135 = SBApp.GetArchitectUser(readeSwc["SWC135"] + "","Name") + "";
				string qSWC135ID = readeSwc["SWC135"] + "";
				string qSWC136 = readeSwc["SWC136"] + "";
				string qSWC137 = readeSwc["SWC137"] + "";
				string qSWC138 = readeSwc["SWC138"] + "";

                qSWC031 = SBApp.DateView(qSWC031, "00");
                qSWC032 = SBApp.DateView(qSWC032, "00");
                qSWC033 = SBApp.DateView(qSWC033, "00");

                qSWC034 = SBApp.DateView(qSWC034, "00");
                qSWC038 = SBApp.DateView(qSWC038, "00");
                qSWC043 = SBApp.DateView(qSWC043, "00");
                qSWC051 = SBApp.DateView(qSWC051, "00");
                qSWC052 = SBApp.DateView(qSWC052, "00");
                qSWC082 = SBApp.DateView(qSWC082, "00");
                qSWC110 = SBApp.DateView(qSWC110, "00");
				
                qSWC136 = SBApp.DateView(qSWC136, "00");

                #region tslm
                using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
                {
                    TslmConn.Open();
                    string strQtslm = " select * from SWCSWC where SWC00 = '" + v + "' ";

                    SqlDataReader readeTslm;
                    SqlCommand objCmdTslm = new SqlCommand(strQtslm, TslmConn);
                    readeTslm = objCmdTslm.ExecuteReader();

                    while (readeTslm.Read())
                    {
                        qSWC031 = readeTslm["SWC31"] + "";   //2
                        qSWC031 = SBApp.DateView(qSWC031, "00");
                        qSWC033 = readeTslm["SWC33"] + "";   //2
                        qSWC033 = SBApp.DateView(qSWC033, "00");
                        qSWC041 = readeTslm["SWC41"] + "";   //2
                    }
                }
                #endregion
                #endregion


                //丟資料
                LBSWC000.Text = qSWC000;
                LBSWC000s.Text = qSWC000;
                LBSWC002.Text = qSWC002;
                LBSWC002s.Text = qSWC002;
                LBSWC004.Text = qSWC004;
                LBSWC004s.Text = qSWC004;
                LBSWC005.Text = qSWC005;
                LBSWC005s.Text = qSWC005;
                LBSWC007.Text = qSWC007;
                LBSWC013.Text = qSWC013;
                TXTSWC013ID.Text = qSWC013ID;
                TXTSWC013TEL.Text = qSWC013TEL;
                LBSWC014.Text = qSWC014;
                LBSWC015.Text = qSWC015;
                LBSWC016.Text = qSWC016;
                LBSWC017.Text = qSWC017;
                LBSWC018.Text = qSWC018;
                LBSWC021.Text = qSWC021;
                LBSWC021ID.Text = qSWC021ID;
                LBSWC022.Text = qSWC022;
                LBSWC022ID.Text = qSWC022ID;
                LBSWC023.Text = qSWC023;
                LBSWC024.Text = qSWC024;
                LBSWC024ID.Text = qSWC024ID;
                LBSWC025.Text = qSWC025;
                LBSWC027.Text = qSWC027;
                LBSWC028.Text = qSWC028;
                LBSWC031.Text = qSWC031;
                LBSWC032.Text = qSWC032;
                LBSWC033.Text = qSWC033;
                LBSWC034.Text = qSWC034;
                LBSWC035.Text = qSWC035;
                LBSWC036.Text = qSWC036;
                LBSWC038.Text = qSWC038;
                LBSWC039.Text = qSWC039;
                LBSWC040.Text = qSWC040;
                LBSWC041.Text = qSWC041;
                LBSWC043.Text = qSWC043;
                LBSWC044.Text = qSWC044;
                LBSWC045ID.Text = qSWC045ID;
                LBSWC045.Text = qSWC045;
                LBSWC047.Text = qSWC047;
                LBSWC048.Text = qSWC048;
                LBSWC049.Text = qSWC049;
                LBSWC050.Text = qSWC050;
                LBSWC051.Text = qSWC051;
                LBSWC052.Text = qSWC052;
                LBSWC053.Text = SBApp.DateView(qSWC053, "00");
                LBSWC056.Text = qSWC056;
                TXTSWC058.Text = SBApp.DateView(qSWC058, "00");
                LBSWC059.Text = SBApp.DateView(qSWC059, "00");
                LBSWC082.Text = qSWC082;
                LBSWC083.Text = qSWC083;
                LBSWC084.Text = SBApp.DateView(qSWC084, "00");
                LBSWC087.Text = qSWC087;
                LBSWC088.Text = SBApp.DateView(qSWC088, "00");
                LBSWC089.Text = SBApp.DateView(qSWC089, "00");
                LBSWC092.Text = qSWC092;
                LBSWC093.Text = qSWC093;
                LBSWC094.Text = qSWC094;
                LBSWC095.Text = qSWC095;
                LBSWC103.Text = qSWC103.Replace(";;", "<br/>").Replace("  ","<br/>");
                LBSWC105.Text = qSWC105.Replace(";;", "<br/>").Replace("  ","<br/>");
                LBSWC107.Text = qSWC107;
                LBSWC107ID.Text = qSWC107ID;
                LBSWC108.Text = qSWC108;
                LBSWC116.Text = qSWC116;
                LBSWC134.Text = qSWC134;
				LBSWC134ID.Text = qSWC134ID;
				LBSWC135.Text = qSWC135;
				LBSWC135ID.Text = qSWC135ID;
                LBSWC136.Text = qSWC136;


                if (qSWC004 == "已完工" && LBSWC059.Text.Trim() != "")
                {
                    HyperLink1.Visible = true; HyperLink1.NavigateUrl = "../SwcReport/pdfSwc01.aspx?CaseId=" + qSWC000;
                }

                #region-
                string[] arrayDateValue = new string[] { qSWC104, qSWC109, qSWC112, qSWC113, qSWC115, qSWC125 };
                Label[] arrayLBDate = new Label[] { LBSWC104, LBSWC109, LBSWC112, LBSWC113, LBSWC115, LBSWC125 };
                for (int i = 0; i < arrayLBDate.Length; i++) arrayLBDate[i].Text = SBApp.DateView(arrayDateValue[i], "00");
				
                arrayDateValue = new string[] { qSWC114, qSWC114_2 };
                arrayLBDate = new Label[] { LBSWC114, LBSWC114_2 };
                for (int i = 0; i < arrayLBDate.Length; i++) arrayLBDate[i].Text = SBApp.DateView(arrayDateValue[i], "08");
				if(LBSWC114_2.Text != "") LBSWC114_2.Text = "~" + LBSWC114_2.Text;
                #endregion

                #region-委員名單
                string tLBSAOID = "";
                int ii = 0;
                Label[] arryLB01 = new Label[] { LBSA01, LBSA02, LBSA03, LBSA04, LBSA05, LBSA06, LBSA07, LBSA08, LBSA09, LBSA10, LBSB01, LBSB02 };
                string exeSqlStr = " select E.ETName,E.ETID,ISNULL(RGSID,'0') AS RGSID from GuildGroup G Left Join ETUsers E on G.ETID=E.ETID where G.swc000='" + v + "' order by convert(float,RGSID) ";
                using (SqlConnection DDL01Conn = new SqlConnection(connectionString.ConnectionString))
                {
                    DDL01Conn.Open();
                    SqlDataReader readerItemGG;
                    SqlCommand objCmdItemGG = new SqlCommand(exeSqlStr, DDL01Conn);
                    readerItemGG = objCmdItemGG.ExecuteReader();

                    while (readerItemGG.Read())
                    {
                        string tmpUserName = readerItemGG["ETName"] + "";
                        string tmpUserID = readerItemGG["ETID"] + "";
                        string tmpRGSID = readerItemGG["RGSID"] + "";
                        int aaa = Convert.ToInt32(tmpRGSID);
                        if (aaa > 0)
                        {
                            arryLB01[aaa - 1].Text = tmpUserName.Trim();
                            tLBSAOID += tmpUserID + ";;";
                            if (aaa == 1)
                                LBSA01ID.Text = tmpUserID;
                        }
                    }
                }
                LBSAOID.Text = tLBSAOID;
                #endregion

                if (qSWC061 == "1")
                {
                    if (qSWC062.Trim() == "") { qSWC062 = "0"; }
                    LBSWC061062.Text = "建物" + qSWC062 + "戶 ";
                }
                if (qSWC063 == "1")
                {
                    if (qSWC064.Trim() == "") { qSWC064 = "0"; }
                    LBSWC063064.Text = "道路" + qSWC064 + "條";
                }
                if (qSWC065 == "1")
                {
                    LBSWC065066.Text = qSWC066;
                }
                if (qSWC067 == "1")
                {
                    LBSWC067068.Text = "滯洪沉砂設施 " + qSWC068 + " 座";
                    if (qSWC069.Trim() != "")
                    {
                        LBSWC069.Text = "，滯洪量 " + qSWC069 + " m³";
                    }
                    if (qSWC070.Trim() != "")
                    {
                        LBSWC070.Text = "，沉砂量 " + qSWC070 + " m³";
                    }
                }
                if (qSWC071 == "1")
                {
                    LBSWC071072.Text = "排水設施 " + qSWC072 + " 條";
                }
                if (qSWC073 == "1")
                {
                    if (LBSWC071072.Text.Trim() == "")
                    {
                        LBSWC073074.Text = "擋土設施 " + qSWC074 + " 道";
                    }
                    else
                    {
                        LBSWC073074.Text = "，擋土設施 " + qSWC074 + " 道";
                    }

                }
                if (qSWC075 == "1")
                    LBSWC075.Text = "植生工程";
                if (qSWC119 == "1")
                    SWC119.Checked = true;

                //檔案類處理

                //string[] arrayFileNameLink = new string[] { qSWC029, qSWC029CAD, qSWC030, qSWC080, qSWC101, qSWC106, qSWC110, qSWC101CAD };
                //System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link029, Link029CAD, Link030, Link080, Link101, Link106, Link110,Link101CAD };
                string[] arrayFileNameLink = new string[] { qSWC029, qSWC029CAD, qSWC030, qSWC030CAD, qSWC080, qSWC101, qSWC106, qSWC110, qSWC101CAD, qSWC118, qSWC138 };
                string[] arrayFileType = new string[] { qSWC029, "TXTSWC029CAD", qSWC030, "TXTSWC030CAD", qSWC080, qSWC101, qSWC106, "TXTSWC101", "TXTSWC101CAD", "TXTSWC118", "TXTSWC138" };
                System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link029, Link029CAD, Link030, Link030CAD, Link080, Link101, Link106, Link110, Link101CAD, Link118, Link138 };

                for (int i = 0; i < arrayFileNameLink.Length; i++)
                {
                    string strFileName = Path.GetFileName(arrayFileNameLink[i]);
                    System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];

                    FileLinkObj.Visible = false;
                    if (strFileName == "")
                    {
                    }
                    else
                    {
                        string extension = Path.GetExtension(strFileName).ToLowerInvariant();

                        string NewUpath = @"~\OutputFile\" + strFileName;
                        string tempLinkPateh = SBApp.getFileUrl(qSWC000, qSWC002, qSWC007, arrayFileType[i]) + strFileName;
                        //if (extension == ".pdf") if (SBApp.DLFileReMark(v, strFileName, ""))
                        //tempLinkPateh = NewUpath;

                        Class1 C1 = new Class1();
                        C1.FilesSortOut(strFileName, v, "");

                        FileLinkObj.Text = strFileName;
                        FileLinkObj.NavigateUrl = tempLinkPateh;
                        FileLinkObj.Visible = true;
                    }

                }
                #region filesWaterMark
                if (qSWC029.Trim() != "")
                {
                    BTNLINK029.Text = qSWC029;
                    BTNLINK029.Visible = true;
                    Link029.Visible = false;
                }
                if (qSWC030.Trim() != "")
                {
                    BTNLINK030.Text = qSWC030;
                    BTNLINK030.Visible = true;
                    Link030.Visible = false;
                }
                if (qSWC080.Trim() != "")
                {
                    if(qSWC004.Trim() != "不予核定")
					{
						BTNLINK080.Text = qSWC080;
						BTNLINK080.Visible = true;
						Link080.Visible = false;
					}
					else
					{
						BTNLINK080.Text = "";
						BTNLINK080.Visible = false;
						Link080.Visible = false;
					}
                }
                if (qSWC101.Trim() != "")
                {
                    BTNLINK101.Text = Path.GetFileName(qSWC101);
                    BTNLINK101.Visible = true;
                    Link101.Visible = false;
                }
                if (qSWC106.Trim() != "")
                {
                    BTNLINK106.Text = qSWC106;
                    BTNLINK106.Visible = true;
                    Link106.Visible = false;
                }
                if (qSWC110.Trim() != "")
                {
                    string tSWC110 = Path.GetFileName(qSWC110);
                    BTNLINK110.Text = tSWC110;
                    BTNLINK110.Visible = true;
                    Link110.Visible = false;
                }
				if (qSWC131 == "Y")
                {
					RASWC131a.Checked = true;
                }else{
					RASWC131b.Checked = true;
				}
                if (qSWC132 != "")
                {
                    LBSWCXXX.Visible = true;
                    LBSWCXXX.Text = "原案：";
                    HLXXX.Visible = true;
                    HLXXX.Text = qSWC132;
                    HLXXX.NavigateUrl = "SWC003.aspx?SWCNO="+qSWC132;
                }
                if (qSWC133 != "")
                {
                    LBSWCXXX.Visible = true;
                    LBSWCXXX.Text = "新案：";
                    HLXXX.Visible = true;
                    HLXXX.Text = qSWC133;
                    HLXXX.NavigateUrl = "SWC003.aspx?SWCNO=" + qSWC133;
                }
				if (qSWC137.Trim() != "")
                {
					BTNLINK137.Text = qSWC137;
					BTNLINK137.Visible = true;
					Link137.Visible = false;
                }
                #endregion

                switch (ssUserType)
                {
                    case "01":

                        break;
                }

            }
        }

        #region swcArea1
        string sqlStr1 = " SELECT * FROM SWCSWCA where SWC000=@SWC000 ";
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            qSWC120 = readerTslm["SWC120"] + "";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion

        #region label
        string[] arrayValue = new string[] { qSWC120 };
        Label[] arrayOBJLB = new Label[] { LBSWC120 };
        for (int i = 0; i < arrayValue.Length; i++)
        {
            arrayOBJLB[i].Text = arrayValue[i];
        }
        #endregion

        //表1
        using (SqlConnection DTLConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            DTLConn.Open();

            string Sql01Str = "";

            Sql01Str = Sql01Str + " SELECT * FROM SWCCheckreport where [SWCCheckreport05]='" + LBSWC002.Text + "' order by SWCCheckreport02;";

            SqlDataReader readerItem01;
            SqlCommand objCmdItem01 = new SqlCommand(Sql01Str, DTLConn);
            readerItem01 = objCmdItem01.ExecuteReader();
			int i = 1;
            while (readerItem01.Read())
            {
                string dDTLA01 = i.ToString();
                string dDTLA02 = readerItem01["SWCCheckreport02"] + "";
                string dDTLA03 = readerItem01["SWCCheckreport03"] + "";
                string dDTLA04 = readerItem01["SWCCheckreport04"] + "";
                string dDTLA05 = readerItem01["SWCCheckreport08"] + "";

                DataTable OBJ_GV01 = (DataTable)ViewState["GV01"];
                DataTable DTGV01 = new DataTable();

                if (OBJ_GV01 == null)
                {
                    DTGV01.Columns.Add(new DataColumn("DTLA001", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA002", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA003", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA004", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA005", typeof(string)));

                    ViewState["GV01"] = DTGV01;
                    OBJ_GV01 = DTGV01;
                }
                DataRow dr01 = OBJ_GV01.NewRow();

                dr01["DTLA001"] = dDTLA01;
                dr01["DTLA002"] = SBApp.DateView(dDTLA02, "00");
                dr01["DTLA003"] = SBApp.DateView(dDTLA03, "00");
                dr01["DTLA004"] = dDTLA04;
				dr01["DTLA005"] = dDTLA05.Replace("http://172.28.100.55/ILGFILE/", ConfigurationManager.AppSettings["ilgfileurl"]).Replace("https://tslm.swc.taipei/tcgefile/", ConfigurationManager.AppSettings["ilgfileurl"]);

                OBJ_GV01.Rows.Add(dr01);

                ViewState["GV01"] = OBJ_GV01;

                SWCDTL01.DataSource = OBJ_GV01;
                SWCDTL01.DataBind();
				i++;
            }
        }
		
		//表2
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql02Str = "";

            Sql02Str = Sql02Str + " select swc000,DTLB001,DATALOCK from SWCDTL02 ";
            Sql02Str = Sql02Str + "  Where SWC000 = '" + v + "' ";
            Sql02Str = Sql02Str + "  order by DTLB001 ";

            SqlDataReader readerItem02;
            SqlCommand objCmdItem02 = new SqlCommand(Sql02Str, DTLConn);
            readerItem02 = objCmdItem02.ExecuteReader();

            while (readerItem02.Read())
            {
                string dDTLB01 = readerItem02["DTLB001"] + "";
                string dDTLB02 = "";//readerItem02["DTLB002"] + "";
                string dDTLB03 = "";//readerItem02["DTLB003"] + "";
                string dDTLB04 = "";//readerItem02["DTLB004"] + "";
                string dDTLB05 = readerItem02["DATALOCK"] + "";

                DataTable OBJ_GV02 = (DataTable)ViewState["GV02"];
                DataTable DTGV02 = new DataTable();

                if (OBJ_GV02 == null)
                {
                    DTGV02.Columns.Add(new DataColumn("DTLB001", typeof(string)));
                    DTGV02.Columns.Add(new DataColumn("DTLB002", typeof(string)));
                    DTGV02.Columns.Add(new DataColumn("DTLB003", typeof(string)));
                    DTGV02.Columns.Add(new DataColumn("DTLB004", typeof(string)));
                    DTGV02.Columns.Add(new DataColumn("DTLB005", typeof(string)));

                    ViewState["GV02"] = DTGV02;
                    OBJ_GV02 = DTGV02;
                }
                DataRow dr02 = OBJ_GV02.NewRow();

                dr02["DTLB001"] = dDTLB01;
                dr02["DTLB002"] = dDTLB02;
                dr02["DTLB003"] = dDTLB03;
                dr02["DTLB004"] = dDTLB04;
                dr02["DTLB005"] = dDTLB05;

                OBJ_GV02.Rows.Add(dr02);

                ViewState["GV02"] = OBJ_GV02;

                SWCDTL02.DataSource = OBJ_GV02;
                SWCDTL02.DataBind();
            }
        }
		
        //表3
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql0302Str = "";
            Sql0302Str = Sql0302Str + " Select  D3.SWC000,DTLC000,[DTLC001] ,CONVERT(varchar(100), [DTLC002], 23) AS DTLC002,DTLC003,DTLC004,DATALOCK  From SWCDTL03  D3 left join SWCCASE swc on swc.swc000=D3.SWC000 Where D3.SWC000 = '" + v + "' and (isnull(DATALOCK,'')='Y' or D3.Saveuser='" + ssUserID + "' or SWC024ID='" + ssUserID + "' or D3.Saveuser='PWA') ";//UNION ";
            //Sql0302Str = Sql0302Str + " select swc000,DTLB000 AS DTLC000,DTLB001 AS [DTLC001] ,CONVERT(varchar(100), [DTLB002], 23) AS DTLC002,DTLB003 AS DTLC003,DTLB004 AS DTLC004,DATALOCK from SWCDTL02 ";
            //Sql0302Str = Sql0302Str + "  Where SWC000 = '" + v + "'  and isnull(DATALOCK,'')='Y' ";
            Sql0302Str = Sql0302Str + "  order by DTLC000 desc ";

            SqlDataReader readerItem0302;
            SqlCommand objCmdItem0302 = new SqlCommand(Sql0302Str, DTLConn);
            readerItem0302 = objCmdItem0302.ExecuteReader();

            while (readerItem0302.Read())
            {
                string dDTLC01 = readerItem0302["DTLC001"] + "";
                string dDTLC02 = readerItem0302["DTLC002"] + "";
                string dDTLC03 = readerItem0302["DTLC003"] + "";
                string dDTLC04 = readerItem0302["DTLC004"] + "";
                string dDTLC05 = readerItem0302["DATALOCK"] + "";

                DataTable OBJ_GV0302 = (DataTable)ViewState["GV0302"];
                DataTable DTGV0302 = new DataTable();

                if (OBJ_GV0302 == null)
                {
                    DTGV0302.Columns.Add(new DataColumn("DTLC001", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC002", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC003", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC004", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC005", typeof(string)));

                    ViewState["GV03"] = DTGV0302;
                    OBJ_GV0302 = DTGV0302;
                }
                DataRow dr0302 = OBJ_GV0302.NewRow();

                dr0302["DTLC001"] = dDTLC01;
                dr0302["DTLC002"] = dDTLC02;
                dr0302["DTLC003"] = dDTLC03;
                dr0302["DTLC004"] = dDTLC04;
                dr0302["DTLC005"] = dDTLC05;

                OBJ_GV0302.Rows.Add(dr0302);

                ViewState["GV0302"] = OBJ_GV0302;

                SWCDTL0302.DataSource = OBJ_GV0302;
                SWCDTL0302.DataBind();
            }
        }

        #region 颱風豪雨設施自主檢查表(SWCDT004)
        string Sql04Str = " Select [DTLD001] ,CONVERT(varchar(100), [DTLD002], 23) AS DTLD002,ISNULL(D4.DTLD003,'')+ISNULL(ISNULL(DE.DENAME,DE2.DENAME),'') AS DTLD003,DTLD004,D4.DATALOCK,D4.SAVEUSER From SWCDTL04 D4 ";
        Sql04Str += " left join SWCCASE S on S.SWC000=D4.SWC000 LEFT JOIN DisasterEvent DE ON D4.DTLD085=DE.DENo LEFT JOIN DisasterEvent DE2 ON D4.DENo=DE2.DENo ";
        Sql04Str += "  Where D4.SWC000 = '" + v + "' and (isnull(DATALOCK,'')='Y' or isnull(D4.Saveuser,'')='" + ssUserID + "' or S.SWC045ID='" + ssUserID + "' or isnull(D4.Saveuser,'')='PWA') ";
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();
            Sql04Str = Sql04Str + "  order by DTLD001 ";

            SqlDataReader readerItem04;
            SqlCommand objCmdItem04 = new SqlCommand(Sql04Str, DTLConn);
            readerItem04 = objCmdItem04.ExecuteReader();

            while (readerItem04.Read())
            {
                string dDTLD01 = readerItem04["DTLD001"] + "";
                string dDTLD02 = readerItem04["DTLD002"] + "";
                string dDTLD03 = readerItem04["DTLD003"] + "";
                string dDTLD04 = readerItem04["DTLD004"] + "";
                string DATALOCK = readerItem04["DATALOCK"] + "";
                string SAVEUSER = readerItem04["SAVEUSER"] + "";

                DataTable OBJ_GV04 = (DataTable)ViewState["GV04"];
                DataTable DTGV04 = new DataTable();

                if (OBJ_GV04 == null)
                {
                    DTGV04.Columns.Add(new DataColumn("DTLD001", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD002", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD003", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD004", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DATALOCK", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("SAVEUSER", typeof(string)));

                    ViewState["GV04"] = DTGV04;
                    OBJ_GV04 = DTGV04;
                }
                DataRow dr04 = OBJ_GV04.NewRow();

                dr04["DTLD001"] = dDTLD01;
                dr04["DTLD002"] = dDTLD02;
                dr04["DTLD003"] = dDTLD03;
                dr04["DTLD004"] = dDTLD04;
                dr04["DATALOCK"] = DATALOCK;
                dr04["SAVEUSER"] = SAVEUSER;

                OBJ_GV04.Rows.Add(dr04);

                ViewState["GV04"] = OBJ_GV04;

                SWCDTL04.DataSource = OBJ_GV04;
                SWCDTL04.DataBind();
            }
        }
        #endregion
        #region 監造紀錄表(SWCDT005)
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql05Str = "";

            Sql05Str += " Select [DTLE001] ,CONVERT(varchar(100), [DTLE002], 23) AS DTLE002,CONVERT(varchar(100), [DTLE088], 23) AS DTLE002o,DTLE003 as DTLE003,DTLE082 as DTLE003o,DTLE004,DATALOCK,Saveuser  From SWCDTL05 ";
            Sql05Str += "  Where SWC000 = '" + v + "'  and ((isnull(DATALOCK,'')='Y') OR (isnull(DATALOCK,'')<>'Y' AND Saveuser=N'" + ssUserID + "')) ";
            Sql05Str += "  order by DTLE001 desc; ";

            SqlDataReader readerItem05;
            SqlCommand objCmdItem05 = new SqlCommand(Sql05Str, DTLConn);
            readerItem05 = objCmdItem05.ExecuteReader();

            while (readerItem05.Read())
            {
                string dDTLE01 = readerItem05["DTLE001"] + "";
                string dDTLE02 = readerItem05["DTLE002o"] + "";
                string dDTLE03 = readerItem05["DTLE003"] + "";
                string dDTLE04 = readerItem05["DTLE004"] + "";
                string dDATALOCK = readerItem05["DATALOCK"] + "";
                string dSaveuser = readerItem05["Saveuser"] + "";

                dDTLE02 = dDTLE02.Trim() == "" ? readerItem05["DTLE002"] + "" : dDTLE02;
                try { dDTLE03 = Convert.ToInt32(dDTLE03).ToString(); }
                catch { dDTLE03 = readerItem05["DTLE003o"] + ""; }

                DataTable OBJ_GV05 = (DataTable)ViewState["GV05"];
                DataTable DTGV05 = new DataTable();

                if (OBJ_GV05 == null)
                {
                    DTGV05.Columns.Add(new DataColumn("DTLE001", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE002", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE003", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE004", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DATALOCK", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("Saveuser", typeof(string)));

                    ViewState["GV05"] = DTGV05;
                    OBJ_GV05 = DTGV05;
                }
                DataRow dr05 = OBJ_GV05.NewRow();

                dr05["DTLE001"] = dDTLE01;
                dr05["DTLE002"] = dDTLE02;
                dr05["DTLE003"] = dDTLE03;
                dr05["DTLE004"] = dDTLE04;
                dr05["DATALOCK"] = dDATALOCK;
                dr05["Saveuser"] = dSaveuser;

                OBJ_GV05.Rows.Add(dr05);

                ViewState["GV05"] = OBJ_GV05;

                SWCDTL05.DataSource = OBJ_GV05;
                SWCDTL05.DataBind();
            }
        }
        #endregion

        //表6
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql06Str = "";

            //Sql06Str = Sql06Str + " Select [DTLF001] ,CONVERT(varchar(100), [DTLF002], 23) AS DTLF002,DTLF003,DTLF004,DATALOCK,DATALOCK2,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End as SIGN From SWCDTL06 D6 ";
            //Sql06Str += " left join SWCCASE SWC on SWC.SWC000=D6.SWC000 ";
            //Sql06Str = Sql06Str + "  Where D6.SWC000 = '" + v + "' and (isnull(DATALOCK,'')='Y' or D6.Saveuser='" + ssUserID + "' or SWC024ID='" + ssUserID + "' or DATALOCK2='Y') ";
            //Sql06Str = Sql06Str + "  order by DTLF001 ";

            //Modified by Tim 20210615
            //1.退補正要判斷該公會及檢查委員可看到 2.未送出自己可看到
            Sql06Str = Sql06Str + " Select [DTLF001] ,CONVERT(varchar(100), [DTLF002], 23) AS DTLF002,DTLF003,DTLF004,DATALOCK,DATALOCK2,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End as SIGN From SWCDTL06 D6 ";
            Sql06Str += " left join SWCCASE SWC on SWC.SWC000=D6.SWC000 ";
            Sql06Str = Sql06Str + "  Where D6.SWC000 = '" + v + "' and (isnull(DATALOCK,'')='Y' or D6.Saveuser='" + ssUserID + "' or SWC024ID='" + ssUserID + "' or D6.Saveuser='PWA' or (DATALOCK2='Y' and D6.ReviewResults = '2' and D6.Saveuser='" + ssUserID + "' or SWC024ID='" + ssUserID + "')) ";
            Sql06Str = Sql06Str + "  order by DTLF001 ";

            SqlDataReader readerItem06;
            SqlCommand objCmdItem06 = new SqlCommand(Sql06Str, DTLConn);
            readerItem06 = objCmdItem06.ExecuteReader();

            while (readerItem06.Read())
            {
                string dDTLF01 = readerItem06["DTLF001"] + "";
                string dDTLF02 = readerItem06["DTLF002"] + "";
                string dDTLF03 = readerItem06["DTLF003"] + "";
                string dDTLF04 = readerItem06["DTLF004"] + "";
                string dDTLF05 = readerItem06["DATALOCK"] + "";
                string dDTLF06 = readerItem06["DATALOCK2"] + "";
                string dDTLF07 = readerItem06["SIGN"] + "";

                DataTable OBJ_GV06 = (DataTable)ViewState["GV06"];
                DataTable DTGV06 = new DataTable();

                if (OBJ_GV06 == null)
                {
                    DTGV06.Columns.Add(new DataColumn("DTLF001", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF002", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF003", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF004", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF005", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF006", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF007", typeof(string)));

                    ViewState["GV06"] = DTGV06;
                    OBJ_GV06 = DTGV06;
                }
                DataRow dr06 = OBJ_GV06.NewRow();

                dr06["DTLF001"] = dDTLF01;
                dr06["DTLF002"] = dDTLF02;
                dr06["DTLF003"] = dDTLF03;
                dr06["DTLF004"] = dDTLF04;
                dr06["DTLF005"] = dDTLF05;
                dr06["DTLF006"] = dDTLF06;
                dr06["DTLF007"] = dDTLF07;

                OBJ_GV06.Rows.Add(dr06);

                ViewState["GV06"] = OBJ_GV06;

                SWCDTL06.DataSource = OBJ_GV06;
                SWCDTL06.DataBind();
            }
        }

        //表7
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql07Str = "";

            Sql07Str = Sql07Str + " Select [DTLG001] ,CONVERT(varchar(100), [DTLG002], 23) AS DTLG002,DTLG003,DTLG004  From SWCDTL07 ";
            Sql07Str = Sql07Str + "  Where SWC000 = '" + v + "' and (isnull(DATALOCK,'')='Y' or Saveuser='" + ssUserID + "') ";
            Sql07Str = Sql07Str + "  order by DTLG002 ";

            SqlDataReader readerItem07;
            SqlCommand objCmdItem07 = new SqlCommand(Sql07Str, DTLConn);
            readerItem07 = objCmdItem07.ExecuteReader();

            while (readerItem07.Read())
            {
                string dDTLG01 = readerItem07["DTLG001"] + "";
                string dDTLG02 = readerItem07["DTLG002"] + "";
                string dDTLG03 = readerItem07["DTLG003"] + "";

                DataTable OBJ_GV07 = (DataTable)ViewState["GV07"];
                DataTable DTGV07 = new DataTable();

                if (OBJ_GV07 == null)
                {
                    DTGV07.Columns.Add(new DataColumn("DTLG001", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG002", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG003", typeof(string)));

                    ViewState["GV07"] = DTGV07;
                    OBJ_GV07 = DTGV07;
                }
                DataRow dr07 = OBJ_GV07.NewRow();

                dr07["DTLG001"] = dDTLG01;
                dr07["DTLG002"] = dDTLG02;
                dr07["DTLG003"] = dDTLG03;

                OBJ_GV07.Rows.Add(dr07);

                ViewState["GV07"] = OBJ_GV07;

                SWCDTL07.DataSource = OBJ_GV07;
                SWCDTL07.DataBind();
            }
        }

        //GVSWCCHG
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            string strSQLRV = "select * from SWCCASE";
            strSQLRV = strSQLRV + " Where LEFT(SWC002,12) = '" + LBSWC002.Text.Substring(0, 12) + "' ";
            strSQLRV = strSQLRV + "   and SWC002 <> '" + LBSWC002.Text + "' ";
            strSQLRV = strSQLRV + " order by SWC002  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dSWC000 = readerItem["SWC000"] + "";
                string dSWC002 = readerItem["SWC002"] + "";

                DataTable tbSWCCHK = (DataTable)ViewState["SWCCHK"];

                if (tbSWCCHK == null)
                {
                    DataTable GVTBSWCDHK = new DataTable();

                    GVTBSWCDHK.Columns.Add(new DataColumn("SWC002", typeof(string)));
                    GVTBSWCDHK.Columns.Add(new DataColumn("SWC002Link", typeof(string)));

                    ViewState["SWCCHK"] = GVTBSWCDHK;
                    tbSWCCHK = (DataTable)ViewState["SWCCHK"];
                }

                DataRow GVTBSWCDHKRow = tbSWCCHK.NewRow();

                GVTBSWCDHKRow["SWC002"] = dSWC002;
                GVTBSWCDHKRow["SWC002Link"] = "SWC003.aspx?SWCNO=" + dSWC000;

                tbSWCCHK.Rows.Add(GVTBSWCDHKRow);

                //Store the DataTable in ViewState
                ViewState["SWCCHK"] = tbSWCCHK;

                GVSWCCHG.DataSource = tbSWCCHK;
                GVSWCCHG.DataBind();

            }
            readerItem.Close();
        }
        //地籍...
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV = "select * from SWCLAND";
            strSQLRV = strSQLRV + " Where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + " order by convert(int,LAND000)  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dLAND000 = readerItem["LAND000"] + "";
                string dLAND001 = readerItem["LAND001"] + "";
                string dLAND002 = readerItem["LAND002"] + "";
                string dLAND003 = readerItem["LAND003"] + "";
                string dLAND004 = readerItem["LAND004"] + "";
                string dLAND005 = readerItem["LAND005"] + "";
                string dLAND006 = readerItem["LAND006"] + "";
                string dLAND007 = readerItem["LAND007"] + "";
                string dLAND008 = readerItem["LAND008"] + "";
                string dLAND009 = readerItem["LAND009"] + "";
                string dLAND010 = readerItem["LAND010"] + "";
                string dLAND011 = readerItem["LAND011"] + "";
                string dLAND012 = readerItem["LAND012"] + "";

                DataTable tbCadastral = (DataTable)ViewState["SwcCadastral"];

                if (tbCadastral == null)
                {
                    DataTable GVTBCD = new DataTable();

                    GVTBCD.Columns.Add(new DataColumn("序號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("小段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地號", typeof(string)));
					GVTBCD.Columns.Add(new DataColumn("山坡地範圍", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地使用分區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地可利用限度", typeof(string)));
					GVTBCD.Columns.Add(new DataColumn("陽明山國家公園範圍", typeof(string)));		
                    GVTBCD.Columns.Add(new DataColumn("林地類別", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地質敏感區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("水保計畫申請紀錄", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("水土保持法違規紀錄", typeof(string)));

                    ViewState["SwcCadastral"] = GVTBCD;
                    tbCadastral = (DataTable)ViewState["SwcCadastral"];
                }

                DataRow GVTBCDRow = tbCadastral.NewRow();

                GVTBCDRow["序號"] = ++nj;
                GVTBCDRow["區"] = dLAND001;
                GVTBCDRow["段"] = dLAND002;
                GVTBCDRow["小段"] = dLAND003;
                GVTBCDRow["地號"] = dLAND004;
                GVTBCDRow["山坡地範圍"] = dLAND009;
                GVTBCDRow["土地使用分區"] = dLAND005;
                GVTBCDRow["土地可利用限度"] = dLAND006;
                GVTBCDRow["陽明山國家公園範圍"] = dLAND010;
                GVTBCDRow["林地類別"] = dLAND007;
                GVTBCDRow["地質敏感區"] = dLAND008;
                GVTBCDRow["水保計畫申請紀錄"] = dLAND011;
                GVTBCDRow["水土保持法違規紀錄"] = dLAND012;

                tbCadastral.Rows.Add(GVTBCDRow);

                //Store the DataTable in ViewState
                ViewState["SwcCadastral"] = tbCadastral;

                GVCadastral.DataSource = tbCadastral;
                GVCadastral.DataBind();

            }
            readerItem.Close();
        }

        //計畫申請書...
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV = "select * from SWCFILES";
            strSQLRV = strSQLRV + " Where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + " order by convert(int,FILE000)  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dFILE000 = readerItem["FILE000"] + "";
                string dFILE001 = readerItem["FILE001"] + "";
                string dFILE002 = readerItem["FILE002"] + "";
                string dFILE003 = readerItem["FILE003"] + "";

                DataTable File001C = (DataTable)ViewState["File001C"];

                if (File001C == null)
                {
                    DataTable GVFILE001 = new DataTable();

                    GVFILE001.Columns.Add(new DataColumn("File001000", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001001", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001002", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001003", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001004", typeof(string)));

                    ViewState["File001C"] = GVFILE001;
                    File001C = (DataTable)ViewState["File001C"];
                }

                DataRow GVFILE001Row = File001C.NewRow();

                GVFILE001Row["File001000"] = ++nj;
                GVFILE001Row["File001001"] = "001";
                GVFILE001Row["File001002"] = "";
                GVFILE001Row["File001003"] = dFILE003;
                GVFILE001Row["File001004"] = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + dFILE003;

                File001C.Rows.Add(GVFILE001Row);
                Class1 C1 = new Class1();
                C1.FilesSortOut(dFILE003, v, "");

                //Store the DataTable in ViewState
                ViewState["File001C"] = File001C;

                SWCFILES001.DataSource = File001C;
                SWCFILES001.DataBind();

            }
            readerItem.Close();
        }
		#region 失效重核

        //connectionStringTslm
        using (SqlConnection ONA11Conn = new SqlConnection(connectionString.ConnectionString))
        {
            ONA11Conn.Open();

            string Sql01Str = "";

            Sql01Str = Sql01Str + " select SWC000,SWC002,ONA11001,isnull(ReviewResults,'') As ONA11002,DATALOCK,saveuser from OnlineApply11 ";
            Sql01Str = Sql01Str + "  Where SWC000 = '" + v + "' and (isnull(DATALOCK,'')='Y' or Saveuser='" + ssUserID + "') ";
            Sql01Str = Sql01Str + "  order by ONA11001 ";

            SqlDataReader readerItem01;
            SqlCommand objCmdItem01 = new SqlCommand(Sql01Str, ONA11Conn);
            readerItem01 = objCmdItem01.ExecuteReader();

            while (readerItem01.Read())
            {
                string ONA11001 = readerItem01["ONA11001"] + "";
                string ONA11002 = readerItem01["ONA11002"] + "";
                string DATALOCK = readerItem01["DATALOCK"] + "";
                string saveuser = readerItem01["saveuser"] + "";

                DataTable OBJ_GV123 = (DataTable)ViewState["GV123"];
                DataTable DTGV123 = new DataTable();

                if (OBJ_GV123 == null)
                {
                    DTGV123.Columns.Add(new DataColumn("DTL11001", typeof(string)));
                    DTGV123.Columns.Add(new DataColumn("DTL11002", typeof(string)));
                    DTGV123.Columns.Add(new DataColumn("DATALOCK", typeof(string)));
                    DTGV123.Columns.Add(new DataColumn("Saveuser", typeof(string)));

                    ViewState["GV123"] = DTGV123;
                    OBJ_GV123 = DTGV123;
                }
                DataRow dr01 = OBJ_GV123.NewRow();

                dr01["DTL11001"] = ONA11001;
                dr01["DTL11002"] = ONA11002;
                dr01["DATALOCK"] = DATALOCK;
                dr01["Saveuser"] = saveuser;

                OBJ_GV123.Rows.Add(dr01);

                ViewState["GV123"] = OBJ_GV123;

                GV123.DataSource = OBJ_GV123;
                GV123.DataBind();
            }
        }
        #endregion
		
		#region-義務人資訊
        using (SqlConnection ObliConn = new SqlConnection(connectionString.ConnectionString))
        {
            ObliConn.Open();

            string SqlStr = " select * from SWCObligor ";
            SqlStr = SqlStr + "  Where SWC000 = '" + v + "' ";
            SqlStr = SqlStr + "  order by 序號 ;";

            SqlDataReader readerObli;
            SqlCommand objCmdObli = new SqlCommand(SqlStr, ObliConn);
            readerObli = objCmdObli.ExecuteReader();
			

            while (readerObli.Read())
            {
                string dNO = readerObli["序號"] + "";
                string dSWC013 = readerObli["SWC013"] + "";
                string dSWC013ID = readerObli["SWC013ID"] + "";
                string dSWC013TEL = readerObli["SWC013TEL"] + "";
                string dSWC014Zip = readerObli["SWC014Zip"] + "";
                string dSWC014City = readerObli["SWC014City"] + "";
                string dSWC014District = readerObli["SWC014District"] + "";
                string dSWC014Address = readerObli["SWC014Address"] + "";

                DataTable OBJ_GVSWCP = (DataTable)ViewState["SwcPeople"];
                DataTable dtSWCP = new DataTable();

                if (OBJ_GVSWCP == null)
                {

                    dtSWCP.Columns.Add(new DataColumn("序號", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("姓名", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("身分證字號", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("手機", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址ZipCode", typeof(string)));
					dtSWCP.Columns.Add(new DataColumn("地址City", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址District", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址Address", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址", typeof(string)));

                    ViewState["SwcPeople"] = dtSWCP;
                    OBJ_GVSWCP = dtSWCP;
                }
                DataRow drSWCP = OBJ_GVSWCP.NewRow();

                drSWCP["序號"] = dNO;
                drSWCP["姓名"] = dSWC013;
                drSWCP["身分證字號"] = dSWC013ID;
                drSWCP["手機"] = dSWC013TEL;
                drSWCP["地址ZipCode"] = dSWC014Zip;
                drSWCP["地址City"] = dSWC014City;
                drSWCP["地址District"] = dSWC014District;
                drSWCP["地址Address"] = dSWC014Address;
                drSWCP["地址"] = dSWC014Zip + dSWC014City + dSWC014District + dSWC014Address;

                OBJ_GVSWCP.Rows.Add(drSWCP);

                //Store the DataTable in ViewState
                ViewState["SwcPeople"] = OBJ_GVSWCP;

                GVPEOPLE.DataSource = OBJ_GVSWCP;
                GVPEOPLE.DataBind();

            }
            readerObli.Close();
        }
        #endregion
		
        //2018-新增子表單
        GenOnlineApply(v);
        SetDtlData(v);
        SetPayData(v);
    }

    private void SetPayData(string vSwc000)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string ssUserName = Session["NAME"] + "";
        string payData01 = " select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CONVERT(varchar(100), CPI004, 23) as CPI004 from tslm2.dbo.CasePaymentInfo where CaseID = '" + vSwc000 + "' and CaseType = '審查費' and (CPI006='已列印' or CPI006='已繳納') order by id; ";
        SqlDataSource01.SelectCommand = payData01;
        string payData02 = " select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CONVERT(varchar(100), CPI004, 23) as CPI004 from tslm2.dbo.CasePaymentInfo where CaseID = '" + vSwc000 + "' and CaseType = '保證金' and (CPI006='已列印' or CPI006='已繳納') order by id; ";
        SqlDataSource02.SelectCommand = payData02;
    }

    private void GenOnlineApply(string v)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssUserGuild1 = Session["ETU_Guild01"] + "";
        string pgCase022ID = LBSWC022ID.Text + "";
        string pgCaseSub = LBSAOID.Text + "";
        string v2 = LBSWC002.Text + "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];

        //表2
        using (SqlConnection OLAConn = new SqlConnection(connectionString.ConnectionString))
        {
            OLAConn.Open();

            string Sql201Str = "";
            Sql201Str = Sql201Str + " select ONA01001 as DATA01 ,left(convert(char, ONA01002, 120),10) as DATA02,left(convert(char, ONA01003, 120),10) as DATA03,ONA01004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply01 ";
            Sql201Str = Sql201Str + "  Where SWC002 = '" + v2 + "' ";
            Sql201Str = Sql201Str + "  order by ONA01001 ";

            string Sql202Str = "";
            Sql202Str = Sql202Str + " select ONA02001 as DATA01 ,left(convert(char, ONA02002, 120),10) as DATA02,left(convert(char, ONA02003, 120),10) as DATA03,ONA02004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply02 ";
            Sql202Str = Sql202Str + "  Where SWC000 = '" + v + "' ";
            Sql202Str = Sql202Str + "  order by ONA02001 ";

            string Sql203Str = "";
            Sql203Str = Sql203Str + " select ONA03001 as DATA01 ,left(convert(char, ONA03002, 120),10) as DATA02,left(convert(char, ONA03003, 120),10) as DATA03,'第'+ONA03004+'次展延' as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply03 ";
            Sql203Str = Sql203Str + "  Where SWC000 = '" + v + "' and ISNULL(ONA03011,'') != '復工' ";
            Sql203Str = Sql203Str + "  order by ONA03001 ";
			
			string Sql203Str_1 = "";
            Sql203Str_1 = Sql203Str_1 + " select ONA03001 as DATA01 ,left(convert(char, ONA03002, 120),10) as DATA02,left(convert(char, ONA03003, 120),10) as DATA03,'第'+ONA03004+'次展延' as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply03 ";
            Sql203Str_1 = Sql203Str_1 + "  Where SWC000 = '" + v + "' and ONA03011 = '復工' ";
            Sql203Str_1 = Sql203Str_1 + "  order by ONA03001 ";

            string Sql204Str = "";
            Sql204Str = Sql204Str + " select ONA04001 as DATA01 ,left(convert(char, ONA04003, 120),10) as DATA02,left(convert(char, ONA04004, 120),10) as DATA03,'' as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply04 ";
            Sql204Str = Sql204Str + "  Where SWC000 = '" + v + "' and ISNULL(ONA04023,'') != '復工' ";
            Sql204Str = Sql204Str + "  order by ONA04001 ";
			
			string Sql204Str_1 = "";
            Sql204Str_1 = Sql204Str_1 + " select ONA04001 as DATA01 ,left(convert(char, ONA04003, 120),10) as DATA02,left(convert(char, ONA04004, 120),10) as DATA03,'' as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply04 ";
            Sql204Str_1 = Sql204Str_1 + "  Where SWC000 = '" + v + "' and ONA04023 = '復工' ";
            Sql204Str_1 = Sql204Str_1 + "  order by ONA04001 ";

            string Sql205Str = "";
            Sql205Str = Sql205Str + " select ONA05001 as DATA01 ,ONA05002 as DATA02,left(convert(char, LOCKDATE, 120),10) as DATA03,ONA05004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply05 ";
            Sql205Str = Sql205Str + "  Where SWC000 = '" + v + "' ";
            Sql205Str = Sql205Str + "  order by ONA05001 ";

            string Sql206Str = "";
            Sql206Str = Sql206Str + " select ONA06001 as DATA01 ,left(convert(char, ONA06002, 120),10) as DATA02,ET.ETName as DATA03,ONA06004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply06 ONA6 ";
            Sql206Str = Sql206Str + "   LEFT JOIN ETUsers ET ON ONA6.ONA06015=et.etid ";
            Sql206Str = Sql206Str + "  Where SWC000 = '" + v + "' ";
            Sql206Str = Sql206Str + "  order by ONA06001 ";

            string Sql207Str = "";
            Sql207Str = Sql207Str + " select ONA07001 as DATA01 ,left(convert(char, ONA07002, 120),10) as DATA02,left(convert(char, ONA07003, 120),10) as DATA03,left(convert(char, ONA07004, 120),10) as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply07 ";
            Sql207Str = Sql207Str + "  Where SWC000 = '" + v + "' ";
            Sql207Str = Sql207Str + "  order by ONA07001 ";

            string Sql208Str = "";
            Sql208Str = Sql208Str + " select ONA08001 as DATA01 ,left(convert(char, ONA08002, 120),10) as DATA02,left(convert(char, ONA08003, 120),10) as DATA03,left(convert(char, ONA08004, 120),10) as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply08 ";
            Sql208Str = Sql208Str + "  Where SWC000 = '" + v + "' ";
            Sql208Str = Sql208Str + "  order by ONA08001 ";

            string Sql209Str = "";
            Sql209Str = Sql209Str + " select ONA09001 as DATA01 ,left(convert(char, ONA09002, 120),10) as DATA02,ET.ETName as DATA03,ONA09004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply09 ONA9 ";
            Sql209Str = Sql209Str + "   LEFT JOIN ETUsers ET ON ONA9.saveuser=et.etid ";
            Sql209Str = Sql209Str + "  Where SWC000 = '" + v + "' ";
            Sql209Str = Sql209Str + "  order by ONA09001 ";

			string Sql210Str = "";
            Sql210Str = Sql210Str + " select ONA10001 as DATA01 ,left(convert(char, LOCKDTAE, 120),10) as DATA02,left(convert(char, ONA10002, 120),10) as DATA03,left(convert(char, ONA10004, 120),10) as DATA04,DATALOCK as DATA05,'OnlineApply10' AS DATA06 from OnlineApply10 ";
            Sql210Str = Sql210Str + "  Where SWC000 = '" + v + "' ";
            Sql210Str = Sql210Str + "  order by ONA10001 ";
			
            /*
			string Sql211Str = "";
			Sql211Str = Sql211Str + " select ONA11001 as DATA01 ,left(convert(char, savedate, 120),10) as DATA02,ET.ETName as DATA03,"" as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply11 ONA11 ";
            Sql211Str = Sql211Str + "   LEFT JOIN ETUsers ET ON ONA11.saveuser=et.etid ";
            Sql211Str = Sql211Str + "  Where SWC000 = '" + v + "' ";
            Sql211Str = Sql211Str + "  order by ONA11001 ";
			*/
            string[] arraySqlExeStr = new string[] { Sql201Str, Sql202Str, Sql203Str, Sql203Str_1, Sql204Str, Sql204Str_1, Sql205Str, Sql206Str, Sql207Str, Sql208Str, Sql209Str , Sql210Str/*, Sql211Str */};
            GridView[] arrayONLAGV = new GridView[] { SWCOLA201, SWCOLA202, SWCOLA203, SWCOLA203_1, SWCOLA204, SWCOLA204_1, SWCOLA205, SWCOLA206, SWCOLA207, SWCOLA208, SWCOLA209, SWCOLA210/*, SWCOLA211 */};

            for (int i = 0; i < arraySqlExeStr.Length; i++)
            {
                string aSqlStr = arraySqlExeStr[i] + "";
                GridView SWCONLA = arrayONLAGV[i];

                if (aSqlStr.Trim() == "") { }
                else
                {
                    SqlDataReader readerItem;
                    SqlCommand objCmdItem = new SqlCommand(aSqlStr, OLAConn);
                    readerItem = objCmdItem.ExecuteReader();

                    while (readerItem.Read())
                    {
                        string dONA001 = readerItem["DATA01"] + "";
                        string dONA002 = readerItem["DATA02"] + "";
                        string dONA003 = readerItem["DATA03"] + "";
                        string dONA004 = readerItem["DATA04"] + "";
                        string dONA005 = readerItem["DATA05"] + "";
                        string dONA006 = readerItem["DATA06"] + "";

                        if (ssUserType == "01" || ssUserType == "02" || dONA005 == "Y")
                        {
                            DataTable OBJ_OLAGV = (DataTable)ViewState["OLAGV" + i.ToString().PadLeft(2, '0')];

                            if (OBJ_OLAGV == null)
                            {
                                DataTable OLAGV = new DataTable();

                                OLAGV.Columns.Add(new DataColumn("OLA001", typeof(string)));
                                OLAGV.Columns.Add(new DataColumn("OLA002", typeof(string)));
                                OLAGV.Columns.Add(new DataColumn("OLA003", typeof(string)));
                                OLAGV.Columns.Add(new DataColumn("OLA004", typeof(string)));
                                OLAGV.Columns.Add(new DataColumn("OLA005", typeof(string)));
                                OLAGV.Columns.Add(new DataColumn("OLA006", typeof(string)));

                                ViewState["OLAGV" + i.ToString().PadLeft(2, '0')] = OLAGV;
                                OBJ_OLAGV = OLAGV;

                            }
                            DataRow dr = OBJ_OLAGV.NewRow();

                            dr["OLA001"] = dONA001;
                            dr["OLA002"] = dONA002;
                            dr["OLA003"] = dONA003;
                            dr["OLA004"] = dONA004;
                            dr["OLA005"] = dONA005;
                            dr["OLA006"] = dONA006;

                            OBJ_OLAGV.Rows.Add(dr);

                            ViewState["OLAGV" + i.ToString().PadLeft(2, '0')] = OBJ_OLAGV;

                            SWCONLA.DataSource = OBJ_OLAGV;
                            SWCONLA.DataBind();
                        }

                    }
                    readerItem.Close();
                    objCmdItem.Dispose();
                }
            }
            string tmpChgEtUserId = LBSA01ID.Text;
            string gv2001 = " select SAA001 as DATA01,left(convert(char, SAA002, 120), 10) as DATA02,replace(replace(isnull(SAA003, 0), 0, '不予核定'), 1, '核定') as DATA03,'' as DATA04,'' as DATA05,'' as DATA06,'' as DATA07,DATALOCK as DATA08,Case when ISNULL(DATALOCK2, '') <> 'Y' then '' else replace(replace(replace(isnull(ReviewResults, ''), 1, '核准'), '0', '駁回'), '2', '退補正') End AS DATA09 from SwcApply2001 where SWC000='" + v + "' order by id; ";
            #region setGVdata
            string[] arraySqlStr = new string[] { gv2001 };
            GridView[] arrayGVs = new GridView[] { GridView2001 };
            ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
            {
                TslmConn.Open();
                for (int i = 0; i < arraySqlStr.Length; i++)
                {
                    string aSqlStr = arraySqlStr[i] + "";
                    GridView SWCGVS = arrayGVs[i];

                    if (aSqlStr.Trim() == "") { }
                    else
                    {
                        SqlDataReader readerItem;
                        SqlCommand objCmdItem = new SqlCommand(aSqlStr, TslmConn);
                        readerItem = objCmdItem.ExecuteReader();

                        while (readerItem.Read())
                        {
                            string dONA001 = readerItem["DATA01"] + "";
                            string dONA002 = readerItem["DATA02"] + "";
                            string dONA003 = readerItem["DATA03"] + "";
                            string dONA004 = readerItem["DATA04"] + "";
                            string dONA005 = readerItem["DATA05"] + "";
                            string dONA006 = readerItem["DATA06"] + "";
                            string dONA007 = readerItem["DATA07"] + "";
                            string dONA008 = readerItem["DATA08"] + "";
                            string dONA009 = readerItem["DATA09"] + "";

                            if (dONA008 == "Y" || ssUserID == pgCase022ID || (ssUserID == tmpChgEtUserId && pgCase022ID == ssUserGuild1 && ssUserType == "04"))
                            {
                                DataTable OBJ_GVS = (DataTable)ViewState["GVS" + i.ToString().PadLeft(2, '0')];

                                if (OBJ_GVS == null)
                                {
                                    DataTable GVs = new DataTable();

                                    GVs.Columns.Add(new DataColumn("DATA01", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA02", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA03", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA04", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA05", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA06", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA07", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA08", typeof(string)));
                                    GVs.Columns.Add(new DataColumn("DATA09", typeof(string)));

                                    ViewState["GVS" + i.ToString().PadLeft(2, '0')] = GVs;
                                    OBJ_GVS = GVs;

                                }
                                DataRow dr = OBJ_GVS.NewRow();

                                dr["DATA01"] = dONA001;
                                dr["DATA02"] = dONA002;
                                dr["DATA03"] = dONA003;
                                dr["DATA04"] = dONA004;
                                dr["DATA05"] = dONA005;
                                dr["DATA06"] = dONA006;
                                dr["DATA07"] = dONA007;
                                dr["DATA08"] = dONA008;
                                dr["DATA09"] = dONA009;

                                OBJ_GVS.Rows.Add(dr);

                                ViewState["GVS" + i.ToString().PadLeft(2, '0')] = OBJ_GVS;

                                SWCGVS.DataSource = OBJ_GVS;
                                SWCGVS.DataBind();
                            }

                        }
                        readerItem.Close();
                        objCmdItem.Dispose();
                    }
                }
            }
            #endregion
        }
    }

    protected void GoListPage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SWC001.aspx?SR=Y");
    }

    protected void SWCFILES_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            foreach (Control c in e.Row.Cells[0].Controls)
            {
                if (c.GetType().Equals(typeof(HyperLink)))
                {
                    HyperLink hl = (HyperLink)c;
                    hl.Attributes.Add("onclick", "window.open('WebForm1.aspx');");
                    hl.NavigateUrl = "#";
                }
            }
        }
    }

    protected void ButtonDTL01_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        Button LButton = (Button)sender;
        string LINK = "SWCDT001.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        Response.Redirect(LINK);
    }
    protected void ButtonDTL0302_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;

        string GetDTLType = LButton.CommandArgument.Substring(0, 2);
        string GetDTLType2 = LButton.CommandArgument.Substring(0, 6);
        if (GetDTLType2 == "SWCCHG") { GetDTLType = "RC"; }

        string LINK = "SWC001.aspx";
        switch (GetDTLType)
        {
            case "RC":
                LINK = "SWCDT003.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;
            case "RB":
                LINK = "SWCDT002.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;

        }
        //Response.Redirect(LINK);
        Response.Write("<script>window.open('"+LINK+"','_blank')</script>");
    }
    protected void ButtonDTL04_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;
        string LINK = "SWCDT004v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        if (!CheckDTL04Datalock(rCaseId, LButton.CommandArgument))
        {
            LINK = "SWCDT004.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        }

        Response.Redirect(LINK);

    }

    public bool CheckDTL04Datalock(string rCaseId, string DTLNO)
    {
        bool QQ = false;
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection HSIConn = new SqlConnection(connectionString.ConnectionString))
        {
            HSIConn.Open();

            string strSQLCase = " select DATALOCK from [SWCDTL04] where [SWC000] = '" + rCaseId + "' and DTLD000 = '" + DTLNO + "' ";

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLCase, HSIConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                if (readerData["DATALOCK"].ToString() == "Y") { QQ = true; }

            }
            readerData.Close();
            objCmdRV.Dispose();
        }

        return QQ;
    }

    protected void SWCDTL04_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                Button btn = (Button)e.Row.Cells[4].FindControl("ButtonDTL04");
                HiddenField hf01 = (HiddenField)e.Row.Cells[4].FindControl("HDD04DataLock");
                HiddenField hf02 = (HiddenField)e.Row.Cells[4].FindControl("HDD04SaveUser");
                string tValue01 = hf01.Value;
                string tValue02 = hf02.Value;

                if (tValue01 != "Y" && tValue02 == ssUserID)
                    btn.Text = "編輯";
                else
                    btn.Text = "詳情";
                break;
        }

    }

    protected void ButtonDTL06_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        Button LButton = (Button)sender;
        string LINK = "SWCDT006.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        Response.Redirect(LINK);
    }
    protected void ButtonDTL07_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;
        string LINK = "SWCDT007.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);
    }

    protected void GVCadastral_PageIndexChanged(object sender, EventArgs e)
    {

        GVCadastral.DataSource = ViewState["SwcCadastral"]; //GV分頁2 ViewState暫存網頁 關掉就消失

        //GridView1.DataSource = Session["Date"]; //GV分頁2 暫存網頁

        GVCadastral.DataBind();
    }

    protected void GVCadastral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string rCaseId = Request.QueryString["CaseId"] + "";
        GVCadastral.PageIndex = e.NewPageIndex; //抓取GV分頁頁數
    }
	
	protected void GVCadastral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:
                if (e.Row.Cells[6].Text == "有")
                {
                    HyperLink Hyper = new HyperLink();
                    Hyper.Text = e.Row.Cells[6].Text;
					Hyper.Target = "_blank";
                    Hyper.NavigateUrl = "../../open/openswc.aspx?qarea=" + e.Row.Cells[1].Text + "&qsection=" + e.Row.Cells[2].Text + "&qsubsection=" + e.Row.Cells[3].Text + "&qlandno=" + e.Row.Cells[4].Text;
                    e.Row.Cells[6].Controls.Add(Hyper);
                }
                if (e.Row.Cells[7].Text == "有")
                {
                    HyperLink Hyper = new HyperLink();
                    Hyper.Text = e.Row.Cells[7].Text;
					Hyper.Target = "_blank";
                    Hyper.NavigateUrl = "../../open/openilg.aspx?qarea=" + e.Row.Cells[1].Text + "&qsection=" + e.Row.Cells[2].Text + "&qsubsection=" + e.Row.Cells[3].Text + "&qlandno=" + e.Row.Cells[4].Text;
                    e.Row.Cells[7].Controls.Add(Hyper);
                }
                break;
        }

    }

    protected void DTL_02_01_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCUA = LBSWC002.Text + "";

        Response.Redirect("OnlineApply001.aspx?SWCNO=" + rSWCNO + "&OACode=ADDNEW&UA=" + rSWCUA);
    }
    protected void DTL_02_09_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply009.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void DTL_02_02_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply002.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void ButtonOLA202_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA202.Rows[rowindex].Cells[5].FindControl("Lock202");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply002" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }

    protected void SWCOLA201_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[2].FindControl("Lock201");
                string tempLock = Lock01.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL201");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }
    protected void SWCOLA202_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[2].FindControl("Lock202");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[3].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL202");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }

    protected void DTL_02_03_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply003.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void SWCOLA203_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:
                HiddenField Lock01 = (HiddenField)e.Row.Cells[2].FindControl("Lock203");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[4].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL203");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }

    }
	
	protected void DTL_02_03_1_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply003.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }
	protected void SWCOLA203_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:
                HiddenField Lock01 = (HiddenField)e.Row.Cells[2].FindControl("Lock203_1");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[4].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL203_1");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }

    }

    protected void ButtonOLA203_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA203.Rows[rowindex].Cells[5].FindControl("Lock203");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply003" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }
	
	protected void ButtonOLA203_1_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA203_1.Rows[rowindex].Cells[5].FindControl("Lock203_1");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply003" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }

    protected void DTL_02_04_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply004.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void SWCOLA204_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[3].FindControl("Lock204");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[3].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL204");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }
	protected void DTL_02_04_1_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply004.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }
	protected void SWCOLA204_1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[3].FindControl("Lock204_1");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[3].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL204_1");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }

    protected void DTL_02_05_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply005.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void SWCOLA2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField Lock01 = (HiddenField)e.Row.Cells[4].FindControl("HDLock2");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[2].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("BtnDel2");
                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }

    protected void SWCOLA206_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;

            case DataControlRowType.DataRow:
                HiddenField Lock01 = (HiddenField)e.Row.Cells[3].FindControl("Lock206");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[2].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL206");
                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }

    }
    protected void SWCOLA207_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[3].FindControl("Lock207");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[4].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL207");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }
    protected void SWCOLA208_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField Lock01 = (HiddenField)e.Row.Cells[3].FindControl("Lock208");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[4].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL208");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }
    protected void SWCOLA209_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[3].FindControl("Lock209");
                string tempLock = Lock01.Value;
                String tmpStatus = e.Row.Cells[3].Text;

                if (tempLock == "Y" || tmpStatus == "退補正")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL209");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }

    protected void DTL_02_06_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply006.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void DTL_02_07_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply007.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }


    protected void DTL_02_08_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply008.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }




    protected void ButtonOLA201_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCUA = LBSWC002.Text + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA201.Rows[rowindex].Cells[3].FindControl("Lock201");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply001" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OACode=" + rONLANO + "&UA=" + rSWCUA);

    }
    protected void ButtonOLA204_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA204.Rows[rowindex].Cells[2].FindControl("Lock204");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply004" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }
	protected void ButtonOLA204_1_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA204_1.Rows[rowindex].Cells[2].FindControl("Lock204_1");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply004" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }
    protected void ButtonOLA205_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA205.Rows[rowindex].Cells[4].FindControl("HDLock2");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply005" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }
    protected void ButtonOLA206_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA206.Rows[rowindex].Cells[3].FindControl("Lock206");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply006" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }
    protected void ButtonOLA207_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA207.Rows[rowindex].Cells[3].FindControl("Lock207");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply007" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);


    }
    protected void ButtonOLA208_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA208.Rows[rowindex].Cells[6].FindControl("Lock208");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply008" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }
    protected void ButtonOLA209_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rONLANO = ((Button)(sender)).CommandArgument;
        string rPageView = "";

        //Get the button that raised the event
        Button btn = (Button)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        int rowindex = gvr.RowIndex;

        HiddenField Lock01 = (HiddenField)SWCOLA209.Rows[rowindex].Cells[3].FindControl("Lock209");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply009" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);
    }


    private void SetDtlData(string rSWCNO)
    {
        GBClass001 SBApp = new GBClass001();

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        #region.分段驗收核定項目
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int ni = 0;

            string strSQLRV = "select * from SwcDocItem";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rSWCNO + "' ";
            strSQLRV = strSQLRV + " order by SDI001 ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                #region
                string sSDI001 = readerItem["SDI001"] + "";
                string sSDI002 = readerItem["SDI002"] + "";
                string sSDI003 = readerItem["SDI003"] + "";
                string sSDI004 = readerItem["SDI004"] + "";
                string sSDI005 = readerItem["SDI005"] + "";
                string sSDI006 = readerItem["SDI006"] + "";
                string sSDI006_1 = readerItem["SDI006_1"] + "";
                string sSDI006D = readerItem["SDI006D"] + "";
                string sSDI007 = readerItem["SDI007"] + "";
                string sSDI008 = readerItem["SDI008"] + "";
                string sSDI009 = readerItem["SDI009"] + "";
                string sSDI010 = readerItem["SDI010"] + "";
                string sSDI011 = readerItem["SDI011"] + "";
                string sSDI012 = readerItem["SDI012"] + "";
                string sSDI012_1 = readerItem["SDI012_1"] + "";
                string sSDI012D = readerItem["SDI012D"] + "";
                string sSDI013 = readerItem["SDI013"] + "";
                string sSDI013_1 = readerItem["SDI013_1"] + "";
                string sSDI014 = readerItem["SDI014"] + "";
                string sSDI014_1 = readerItem["SDI014_1"] + "";
                string sSDI015 = readerItem["SDI015"] + "";
                string sSDI016 = readerItem["SDI016"] + "";
                string sSDI019 = readerItem["SDI019"] + "";

                DataTable tbSDIVS = (DataTable)ViewState["SwcDocItem"];

                if (tbSDIVS == null)
                {
                    DataTable SDITB = new DataTable();

                    SDITB.Columns.Add(new DataColumn("SDIFDNI", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD003", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD009", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD010", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD011", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD019", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }

                DataRow SDITBRow = tbSDIVS.NewRow();

                SDITBRow["SDIFDNI"] = ni.ToString();
                SDITBRow["SDIFD001"] = sSDI001;
                SDITBRow["SDIFD002"] = sSDI002;
                SDITBRow["SDIFD003"] = sSDI003;
                SDITBRow["SDIFD004"] = sSDI004;
                SDITBRow["SDIFD005"] = sSDI005;
                SDITBRow["SDIFD006"] = sSDI006;
                SDITBRow["SDIFD006_1"] = sSDI006_1;
                SDITBRow["SDIFD006D"] = sSDI006D;
                SDITBRow["SDIFD007"] = sSDI007;
                SDITBRow["SDIFD008"] = sSDI008;
                SDITBRow["SDIFD009"] = sSDI009;
                SDITBRow["SDIFD010"] = sSDI010;
                SDITBRow["SDIFD011"] = sSDI011;
                SDITBRow["SDIFD012"] = sSDI012;
                SDITBRow["SDIFD012_1"] = sSDI012_1;
                SDITBRow["SDIFD012D"] = sSDI012D;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD013_1"] = sSDI013_1;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD014_1"] = sSDI014_1;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDIFD019"] = sSDI019;

                tbSDIVS.Rows.Add(SDITBRow);
                #endregion
                ViewState["SwcDocItem"] = tbSDIVS;

                SDIList.DataSource = tbSDIVS;
                SDIList.DataBind();
            }
            readerItem.Close();
        }
        #endregion

        #region.檔案交換區
        string getSFileSqlStr = " select * from [ShareFiles] Where SWC000='" + rSWCNO + "';";
        HyperLink fileLink = new HyperLink();
        TextBox fileName = new TextBox();
        Label uploadUser = new Label();
        Label uploadDate = new Label();

        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(getSFileSqlStr, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                #region.設定資料
                string sSFtype = readerItem["SFType"] + "";
                string sFileName = readerItem["SFName"] + "";
                string sSaveUser = readerItem["SaveUser"] + "";
                string sSaveDate = readerItem["SaveDate"] + "";
                string sFileLink = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rSWCNO + "/" + sFileName;
                Class1 C1 = new Class1();
                C1.FilesSortOut(sFileName, rSWCNO, "");

                switch (sSFtype)
                {
                    case "001":
                        fileName = TXTSFile01; fileLink = SFLINK01; uploadUser = LBUPLOADU01; uploadDate = LBUPLOADD01;
                        break;
                    case "002":
                        fileName = TXTSFile02; fileLink = SFLINK02; uploadUser = LBUPLOADU02; uploadDate = LBUPLOADD02;
                        break;
                    case "003":
                        fileName = TXTSFile03; fileLink = SFLINK03; uploadUser = LBUPLOADU03; uploadDate = LBUPLOADD03;
                        break;
                    case "004":
                        fileName = TXTSFile04; fileLink = SFLINK04; uploadUser = LBUPLOADU04; uploadDate = LBUPLOADD04;
                        break;
                    case "005":
                        fileName = TXTSFile05; fileLink = SFLINK05; uploadUser = LBUPLOADU05; uploadDate = LBUPLOADD05;
                        break;
                    case "099":
                        fileName = TXTSFile99; fileLink = SFLINK99;
                        break;
                    case "00A":
                        fileName = TXTSFile0A; fileLink = SFLINK0A; uploadUser = LBUPLOADU0A; uploadDate = LBUPLOADD0A;
                        break;
                    case "00B":
                        fileName = TXTSFile0B; fileLink = SFLINK0B; uploadUser = LBUPLOADU0B; uploadDate = LBUPLOADD0B;
                        break;
                }
                #endregion

                fileLink.Text = sFileName;
                fileLink.NavigateUrl = sFileLink;
                fileLink.Visible = true;
                fileName.Text = sFileName;
                uploadUser.Text = sSaveUser;
                uploadDate.Text = SBApp.DateView(sSaveDate, "00");
            }
            readerItem.Close();
        }
        #endregion
    }
    protected void ButtonDEL201_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA01001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply01 where ONA01001 ='" + tONA01001 + "' ";

        ConnectionStringSettings connectionString01 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn01 = new SqlConnection(connectionString01.ConnectionString))
        {
            SwcConn01.Open();

            SqlDataReader readeSwc01;
            SqlCommand objCmdSwc01 = new SqlCommand(exeSqlStr, SwcConn01);
            readeSwc01 = objCmdSwc01.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }
    protected void ButtonDEL206_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA06001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply06 where swc000='" + tSwc000 + "' and ONA06001 ='" + tONA06001 + "' ";

        ConnectionStringSettings connectionString06 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn06 = new SqlConnection(connectionString06.ConnectionString))
        {
            SwcConn06.Open();

            SqlDataReader readeSwc06;
            SqlCommand objCmdSwc06 = new SqlCommand(exeSqlStr, SwcConn06);
            readeSwc06 = objCmdSwc06.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }
    protected void ButtonDEL207_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA07001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply07 where swc000='" + tSwc000 + "' and ONA07001 ='" + tONA07001 + "' ";

        ConnectionStringSettings connectionString07 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn07 = new SqlConnection(connectionString07.ConnectionString))
        {
            SwcConn07.Open();

            SqlDataReader readeSwc07;
            SqlCommand objCmdSwc07 = new SqlCommand(exeSqlStr, SwcConn07);
            readeSwc07 = objCmdSwc07.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");

    }
    protected void ButtonDEL209_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA09001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply09 where swc000='" + tSwc000 + "' and ONA09001 ='" + tONA09001 + "' ";

        ConnectionStringSettings connectionString09 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn09 = new SqlConnection(connectionString09.ConnectionString))
        {
            SwcConn09.Open();

            SqlDataReader readeSwc09;
            SqlCommand objCmdSwc09 = new SqlCommand(exeSqlStr, SwcConn09);
            readeSwc09 = objCmdSwc09.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }



    protected void ButtonDEL203_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA03001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply03 where swc000='" + tSwc000 + "' and ONA03001 ='" + tONA03001 + "' ";

        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }
	protected void ButtonDEL203_1_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA03001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply03 where swc000='" + tSwc000 + "' and ONA03001 ='" + tONA03001 + "' ";

        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }

    protected void Btn_UPSFile_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string btnType = ((Button)(sender)).ID;
        string rDTLNO = LBSWC000.Text + "";
        bool rUP = false;
        string tSFType = "", tSFName = "", filename = "", extension = "";
        string ssUserNAME = Session["NAME"] + "";
        string exeSqlStr = "";
        Label pgTBName = new Label();
        Label pgTBDate = new Label();

        #region.值設定
        switch (btnType)
        {
            case "Btn_UPSFile01":
                tSFType = "001"; tSFName = "UPSFA_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU01; pgTBDate = LBUPLOADD01;
                filename = SFFile01_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile01_FileUpload, TXTSFile01, "TXTSFile01", tSFName, null, SFLINK01, 150, ""); //150MB
                break;
            case "Btn_UPSFile02":
                tSFType = "002"; tSFName = "UPSFB_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU02; pgTBDate = LBUPLOADD02;
                filename = SFFile02_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile02_FileUpload, TXTSFile02, "TXTSFile02", tSFName, null, SFLINK02, 150, ""); //150MB
                break;
            case "Btn_UPSFile03":
                tSFType = "003"; tSFName = "UPSFC_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU03; pgTBDate = LBUPLOADD03;
                filename = SFFile03_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile03_FileUpload, TXTSFile03, "TXTSFile03", tSFName, null, SFLINK03, 150, ""); //150MB
                break;
            case "Btn_UPSFile04":
                tSFType = "004"; tSFName = "UPSFD_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU04; pgTBDate = LBUPLOADD04;
                filename = SFFile04_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile04_FileUpload, TXTSFile04, "TXTSFile04", tSFName, null, SFLINK04, 150, ""); //150MB
                break;
            case "Btn_UPSFile05":
                tSFType = "005"; tSFName = "UPSFE_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU05; pgTBDate = LBUPLOADD05;
                filename = SFFile05_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile05_FileUpload, TXTSFile05, "TXTSFile05", tSFName, null, SFLINK05, 150, ""); //150MB
                break;
            case "Btn_UPSFileXX":
                tSFType = tNo.Substring(tNo.Length - 3, 3);
                if (tSFType == "001") tSFName = "UPSFA_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "002") tSFName = "UPSFB_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "003") tSFName = "UPSFC_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "004") tSFName = "UPSFD_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "005") tSFName = "UPSFE_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "006") tSFName = "UPSFF_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "007") tSFName = "UPSFG_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "008") tSFName = "UPSFH_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "009") tSFName = "UPSFI_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "010") tSFName = "UPSFJ_" + rDTLNO.Substring(rDTLNO.Length - 3);
                filename = SFFileXX_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFileXX_FileUpload, TXTSFileXX, "TXTSFileXX", tSFName, null, SFLINKXX, 150, ""); //150MB
                break;
            case "Btn_UPSFile99":
                tSFType = "099"; tSFName = "UPSFZ_" + rDTLNO.Substring(rDTLNO.Length - 3);
                filename = SFFile99_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile99_FileUpload, TXTSFile99, "TXTSFile99", tSFName, null, SFLINK99, 150, ""); //150MB
                break;
            case "Btn_UPSFile0A":
                tSFType = "00A"; tSFName = "UPSF1_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU0A; pgTBDate = LBUPLOADD0A;
                filename = SFFile0A_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDFJPGPNG", SFFile0A_FileUpload, TXTSFile0A, "TXTSFile0A", tSFName, null, SFLINK0A, 50, ""); //50MB
                break;
            case "Btn_UPSFile0B":
                tSFType = "00B"; tSFName = "UPSF2_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU0B; pgTBDate = LBUPLOADD0B;
                filename = SFFile0B_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDFJPGPNG", SFFile0B_FileUpload, TXTSFile0B, "TXTSFile0B", tSFName, null, SFLINK0B, 50, ""); //50MB
                break;
        }
        #endregion

        #region.存入db
        if (rUP)
        {
            tSFName += extension;
            pgTBName.Text = ssUserNAME;
            pgTBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string updDB = " Update ShareFiles Set [SFName]='" + tSFName + "',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from ShareFiles  Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                if (!readeSwc.HasRows)
                {
                    exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE,createdate) VALUES ('" + rDTLNO + "','" + tSFType + "',GETDATE());";
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                exeSqlStr += updDB;
                SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
        }
        #endregion
		
		//動態生成審查歷程
        GenerateApprove(rDTLNO);
        SFLINKXX.Visible = false;

        #region.寄信
        //審查公會、公會審查召集人、審查委員
        string tToDay = DateTime.Now.ToString("yyyy-MM-dd");
        string tSWC005 = LBSWC005.Text;
        string tSWC022ID = LBSWC022ID.Text; //審查公會
        string tLBSAOID = ";;" + LBSAOID.Text.Replace(";;;;", "");
        string tMailSub = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        string tMailText = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

        switch (btnType)
        {
            case "Btn_UPSFile01":
            case "Btn_UPSFile02":
            case "Btn_UPSFile03":
                string[] arraySentMail = new string[] { "" };
                string[] arrayMailToP = tLBSAOID.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

                arraySentMail[0] = SBApp.GetGeoUser(tSWC022ID, "Email");
                bool MailTo01 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);

                for (int i = 0; i < arrayMailToP.Length; i++)
                {
                    arraySentMail[0] = SBApp.GetETUser(arrayMailToP[i], "Email");
                    bool MailTo02 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
                }
                break;
        }

        #endregion

        lbarea.Text = "檔案交換區";
    }

    #region.檔案交換區
    private bool FileUpLoadApp2(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadReName, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize, string SubFolder)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string CaseId = LBSWC000.Text + "";
        string UpLoadFileName = UpLoadReName;
        bool rValue = false;

        #region.基本檢查
        string vTempValue = UpLoadText.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return rValue;
        }
        string tUpLoadFile = UpLoadBar.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return rValue;
        }
        #endregion

        #region.檔案上傳
        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名
            switch (ChkType)
            {
                case "PDF":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".PDF" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 PDF 檔案格式上傳，謝謝!!');</script>");
                        return rValue;
                    }
                    break;
                case "PDFJPGPNG":
                    List<string> allowedExtextsion01 = new List<string> { ".pdf", ".PDF", ".JPG", ".jpg", ".PNG", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 PDF、JPG、PNG 檔案格式上傳，謝謝!!');</script>");
                        return rValue;
                    }
                    break;
            }

            //檔案大小限制
            int filesize = UpLoadBar.PostedFile.ContentLength;
            if (filesize > _FileMaxSize * 1000000)
            {
                Response.Write("<script>alert('請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!');</script>");
                return rValue;
            }
            UpLoadFileName += extension;

            #region.上傳設定
            //檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + CaseId;
            if (SubFolder.Trim() != "") serverDir += "\\" + SubFolder;
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            Session[UpLoadStr] = "有檔案";

            string serverFilePath = Path.Combine(serverDir, UpLoadFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(UpLoadFileName);

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);

                switch (ChkType)
                {
                    case "PDF":
                    case "PDFJPGPNG":
                        UpLoadLink.Text = UpLoadFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC\\UpLoadFiles\\SwcCaseFile\\" + CaseId + "\\" + SubFolder + "\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;
                }
                UpLoadText.Text = UpLoadFileName;

                #region.上傳成功存db
                rValue = true;
                #endregion
            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }
            #endregion
        }
        else
        {
            Session[UpLoadStr] = "";
        }
        return rValue;
        #endregion
    }
    #endregion

    protected void Btn_DelSFile_Click(object sender, EventArgs e)
    {
        string btnType = ((Button)(sender)).ID;

        string rDTLNO = LBSWC000.Text + "";
        string delFileName = "", tSFType = "";
        bool tdel = false;
        HyperLink pgLink = new HyperLink();
        TextBox pgTB = new TextBox();
        Label pgTBName = new Label();
        Label pgTBDate = new Label();

        tdel = DelPageFile(delFileName, "");

        #region.存db
        if (tdel)
        {
            switch (btnType)
            {
                case "Btn_DelSFile01":
                    pgLink = SFLINK01; pgTB = TXTSFile01; tSFType = "001"; pgTBName = LBUPLOADU01; pgTBDate = LBUPLOADD01;
                    break;
                case "Btn_DelSFile02":
                    pgLink = SFLINK02; pgTB = TXTSFile02; tSFType = "002"; pgTBName = LBUPLOADU02; pgTBDate = LBUPLOADD02;
                    break;
                case "Btn_DelSFile03":
                    pgLink = SFLINK03; pgTB = TXTSFile03; tSFType = "003"; pgTBName = LBUPLOADU03; pgTBDate = LBUPLOADD03;
                    break;
                case "Btn_DelSFile04":
                    pgLink = SFLINK04; pgTB = TXTSFile04; tSFType = "004"; pgTBName = LBUPLOADU04; pgTBDate = LBUPLOADD04;
                    break;
                case "Btn_DelSFile05":
                    pgLink = SFLINK05; pgTB = TXTSFile05; tSFType = "005"; pgTBName = LBUPLOADU05; pgTBDate = LBUPLOADD05;
                    break;
                case "Btn_DelSFile99":
                    pgLink = SFLINK99; pgTB = TXTSFile99; tSFType = "099";
                    break;
                case "Btn_DelSFile0A":
                    pgLink = SFLINK0A; pgTB = TXTSFile0A; tSFType = "00A"; pgTBName = LBUPLOADU0A; pgTBDate = LBUPLOADD0A;
                    break;
                case "Btn_DelSFile0B":
                    pgLink = SFLINK0B; pgTB = TXTSFile0B; tSFType = "00B"; pgTBName = LBUPLOADU0B; pgTBDate = LBUPLOADD0B;
                    break;
            }

            string DelDBDA = " Update ShareFiles set SFName='' Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
            {
                SwcConn.Open();

                SqlCommand objCmdSWC = new SqlCommand(DelDBDA, SwcConn);
                objCmdSWC.ExecuteNonQuery();
                objCmdSWC.Dispose();
            }

            pgLink.Text = "";
            pgLink.NavigateUrl = "";
            pgLink.Visible = false;
            pgTB.Text = "";
            pgTBName.Text = "";
            pgTBDate.Text = "";
        }
        #endregion
        lbarea.Text = "檔案交換區";
    }
    #region.刪檔
    private bool DelPageFile(string delFileName, string delFileType)
    {
        bool rValue = false;
        string csCaseID = LBSWC000.Text + "";

        //刪實體檔
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + delFileName;

        try
        {
            if (File.Exists(FileFullPath)) File.Delete(FileFullPath);
            rValue = true;
        }
        catch { }

        return rValue;
    }
    #endregion



    protected void CopyCase_Click(object sender, EventArgs e)
    {
        string rCaseId = LBSWC000.Text + "";
        string rSwcNo = LBSWC002.Text + "";
        if (rSwcNo.Length > 12)
            if (rSwcNo.Substring(13, 1) == "9")
            {
                Response.Write("<script>alert('變更次數已達系統設計極限，請洽大地工程處，謝謝!!');</script>"); return;
            }

        Session["CopyCaseIdD1"] = rCaseId;
        Session["CopyCaseIdD2"] = rSwcNo;
        Session["TempSwcCadastral"] = ViewState["SwcCadastral"];
        Session["TempSwcDocItem"] = ViewState["SwcDocItem"];
        Session["TempSwcPeople"] = ViewState["SwcPeople"];

        string LINK = "SWC002.aspx?CaseId=COPY";
        Response.Redirect(LINK);
    }
    protected void btnPrint01_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aa = Convert.ToInt32(LButton.CommandArgument);
        string PBCode = GVPay01.Rows[aa].Cells[0].Text.ToString().Replace("&nbsp;", "");
        Response.Write("<script>window.open('../SwcReport/PPayBillSM01.aspx?pno=" + PBCode + "');</script>");
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aa = Convert.ToInt32(LButton.CommandArgument);
        string PBCode = GVPay02.Rows[aa].Cells[0].Text.ToString().Replace("&nbsp;", "");
        Response.Write("<script>window.open('../SwcReport/PPayBillSM02.aspx?pno=" + PBCode + "');</script>");
    }

    protected void BTNLINK_Click(object sender, EventArgs e)
    {
        Class1 C1 = new Class1();
        string strFileName = "";
        string btnType = ((Button)(sender)).ID;
        string fileType = "";

        switch (btnType)
        {
            case "BTNLINK029":
                strFileName = Link029.Text;
                fileType = "6-1";
                break;
            case "BTNLINK030":
                strFileName = Link030.Text;
                fileType = "7-1";
                break;
            case "BTNLINK080":
                strFileName = Link080.Text;
                fileType = "核定本";
                break;
            case "BTNLINK101":
                strFileName = Link101.Text;
                fileType = "竣工圖說";
                break;
            case "BTNLINK106":
                strFileName = Link106.Text;
                fileType = "核備圖說變更";
                break;
            case "BTNLINK110":
                strFileName = Link110.Text;
                fileType = "審查單位查核表";
                break;
            case "BTNLINK137":
                strFileName = BTNLINK137.Text;
                fileType = "核定不予核定函";
                break;
        }
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC007 = LBSWC007.Text;
        lbarea.Text = "審查";
        string extension = Path.GetExtension(strFileName).ToLowerInvariant();
        string NewUpath = @"..\\OutputFile\\" + strFileName;
        string tempLinkPateh = ConfigurationManager.AppSettings["SwcFilePath20"] + sSWC000 + "/" + strFileName;

        //if (extension == ".pdf") if (C1.DLFileReMark(sSWC000, strFileName, "", sSWC002, sSWC007,fileType)) tempLinkPateh = NewUpath;
        //Response.Write("<script>window.open('"+ tempLinkPateh + "');</script>");
        if (extension == ".pdf")
        {
            if (C1.DLFileReMark(sSWC000, strFileName, "", sSWC002, sSWC007, fileType))
            {
                tempLinkPateh = NewUpath;
            }
        }
        Response.Write("<script>window.open('" + tempLinkPateh + "');</script>");


    }

    protected void SqlDataSource01_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int aa = e.AffectedRows;
        if (aa == 0) LBSWC035.Visible = true;
    }

    protected void SqlDataSource02_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int aa = e.AffectedRows;
        if (aa == 0) LBSWC040.Visible = true;

    }

    protected void GVPay02_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                Button btnPrint = (Button)e.Row.Cells[3].FindControl("btnPrint");
                Label Lock01 = (Label)e.Row.Cells[3].FindControl("LBCSMSG");
                string tempLock = Lock01.Text;

                if (tempLock == "已繳納")
                {
                    Lock01.Visible = true;
                    btnPrint.Visible = false;
                }
                break;
        }
    }

    protected void GVPay01_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                Button btnPrint = (Button)e.Row.Cells[3].FindControl("btnPrint");
                Label Lock01 = (Label)e.Row.Cells[3].FindControl("LBCSMSG");
                string tempLock = Lock01.Text;

                if (tempLock == "已繳納")
                {
                    Lock01.Visible = true;
                    btnPrint.Visible = false;
                }
                break;
        }

    }

    protected void gvBtnLink_Click(object sender, EventArgs e)
    {
        string ohMyGoose = ((Button)(sender)).ID;
        string goYourLink = "SWC001.aspx";
        string thisSWC000 = LBSWC000.Text + "";
        Button oButton = (Button)sender;

        switch (ohMyGoose)
        {
            case "btn2001d":
                goYourLink = "../SWCAPPLY/SwcApply2001.aspx?SWC000=" + thisSWC000 + "&SA20ID=" + oButton.CommandArgument + "&M=EE";
                break;
            case "btn2001u":
                goYourLink = "../SWCAPPLY/SwcApply2001.aspx?SWC000=" + thisSWC000 + "&SA20ID=" + oButton.CommandArgument + "&M=UU";
                break;
        }
        Response.Redirect(goYourLink);
    }

    #region GridView_RowDataBound
    #region 20
    protected void GridView2001_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                break;
            case DataControlRowType.DataRow:
                Button btnUpd = (Button)e.Row.Cells[4].FindControl("btn2001u");
                HiddenField HFLock = (HiddenField)e.Row.Cells[4].FindControl("HFLock");
                string tempLock = HFLock.Value + "";
                if (LBSWC004s.Text == "審查中")
                {
                    if (tempLock != "Y")
                    {
                        btnUpd.Visible = true;
                    }
                }
                break;
        }
    }
    #endregion
    #endregion

    protected void EditCase_Click(object sender, EventArgs e)
    {
        string sSWCNO = LBSWC000.Text + "";
        Response.Redirect("../SWCDOC/SWC002.aspx?CaseId=" + sSWCNO);
    }
	
	protected void GV123_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField hf01 = (HiddenField)e.Row.Cells[3].FindControl("GV123DataLock");
                HiddenField hf02 = (HiddenField)e.Row.Cells[3].FindControl("GV123SaveUser");
                Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDTLGV123");
                string tValue01 = hf01.Value;
                string tValue02 = hf02.Value;
                if (tValue01 != "Y")
                    btn.Text = "編輯";
                break;
        }

    }

    protected void ButtonDTLGV123_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        Button LButton = (Button)sender;
        string LINK = "OnlineApply011.aspx?SWCNO=" + rCaseId + "&OLANO=" + LButton.CommandArgument;
        Response.Redirect(LINK);
    }

    protected void SWCDTL05_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                Button btn = (Button)e.Row.Cells[5].FindControl("ButtonDEL05");
                HiddenField hf01 = (HiddenField)e.Row.Cells[5].FindControl("HDD05DataLock");
                HiddenField hf02 = (HiddenField)e.Row.Cells[5].FindControl("HDD05SaveUser");
                string tValue01 = hf01.Value;
                string tValue02 = hf02.Value;

                if (!(tValue01 != "Y" && tValue02 == ssUserID))
                    btn.Visible = false;
                break;
        }

    }

    protected void ButtonDTL05_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        Button LButton = (Button)sender;
        string LINK = "SWCDT005.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        Response.Write("<script>window.open('"+LINK+"','_blank')</script>");
		//Response.Redirect(LINK);
    }
    protected void ButtonDEL05_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";
        string exeSqlStr = " delete SWCDTL05 where swc000='" + tSwc000 + "' and dtlE000 ='" + tDtl000 + "' ";
        exeSqlStr += " delete SwcItemChk where swc000='" + tSwc000 + "' and DTLRPNO ='" + tDtl000 + "' ";
        ConnectionStringSettings connectionString05 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn05 = new SqlConnection(connectionString05.ConnectionString))
        {
            SwcConn05.Open();

            SqlDataReader readeSwc05;
            SqlCommand objCmdSwc05 = new SqlCommand(exeSqlStr, SwcConn05);
            readeSwc05 = objCmdSwc05.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");

    }

    protected void SWCDTL06_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField Lock06 = (HiddenField)e.Row.Cells[2].FindControl("Lock06");
                HiddenField Lock06b = (HiddenField)e.Row.Cells[2].FindControl("Lock06b");
                string tempLock = Lock06.Value;
                string tempLockb = Lock06b.Value;
                if (tempLock == "Y" || tempLockb == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL06");
                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }

    protected void SWCDTL0302_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField Lock03 = (HiddenField)e.Row.Cells[2].FindControl("Lock03");
                string tempLock = Lock03.Value;
                string GetDTLType = e.Row.Cells[0].Text.Substring(0, 2);
                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL03");
                    btn.Text = "搞啥" + GetDTLType;
                    btn.Visible = false;
                }
                if (GetDTLType == "RB")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL03");
                    btn.Text = "搞啥" + GetDTLType;
                    btn.Visible = false;
                }
                break;
        }

    }

    protected void ButtonDEL03_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";
        string exeSqlStr = " delete SWCDTL03 where swc000='" + tSwc000 + "' and dtlC000 ='" + tDtl000 + "'; delete SwcItemChk where swc000='" + tSwc000 + "' and DTLRPNO ='" + tDtl000 + "';  ";
        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }

    protected void ButtonDEL06_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";
        string exeSqlStr = " delete SWCDTL06 where swc000='" + tSwc000 + "' and dtlf000 ='" + tDtl000 + "' ";
		exeSqlStr += " delete SwcItemChk where swc000='" + tSwc000 + "' and DTLRPNO ='" + tDtl000 + "' ";
        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }

    protected void ButtonDEL01_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";
        string exeSqlStr = " delete SWCDTL01 where swc000='" + tSwc000 + "' and dtla000 ='" + tDtl000 + "' ";

        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }

    protected void SWCDTL01_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField Lock01 = (HiddenField)e.Row.Cells[2].FindControl("Lock01");
                string tempLock = Lock01.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL01");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }

    }

    protected void LB1_Click(object sender, EventArgs e)
    {
        //*******************
        //TOKEN
        string ssUserID = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string token_temp = Session["PW"] + "|" + Session["Unit"] + "|" + Session["JobTitle"] + "|" + Session["Edit4"] + "|" + Session["WMGuild"] + "|" + Session["Guild01"] + "|" + Session["Guild02"] + "|" + Session["ETU_Guild01"] + "|" + Session["ETU_Guild02"] + "|" + Session["ETU_Guild03"] + "|" + Session["ONLINEAPPLY"] + "|" + Session["NUIDNO"] + "|" + Session["NUNAME"] + "|" + Session["NUCELL"] + "|" + Session["NUMAIL"] + "|" + Session["Department"] + "|" + Session["uid"] + "|" + Session["right"] + "|" + Session["grade"] + "|" + Session["TcgeDataedit"] + "|" + Session["TcgeDataview"] + "|" + Session["SuperUser"] + "|" + Session["presented"] + "";

        string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string source = "書件";
        // 0 Session["ID"]
        // 1 Session["UserType"]
        // 2 Session["NAME"]
        // 3 時間
        // 4 來源
        // 5 Session["PW"]
        // 6 Session["Unit"]
        // 7 Session["JobTitle"]
        // 8 Session["Edit4"]
        // 9 Session["WMGuild"]
        //10 Session["Guild01"]
        //11 Session["Guild02"]
        //12 Session["ETU_Guild01"]
        //13 Session["ETU_Guild02"]
        //14 Session["ETU_Guild03"]
        //15 Session["ONLINEAPPLY"]
        //16 Session["NUIDNO"]
        //17 Session["NUNAME"]
        //18 Session["NUCELL"]
        //19 Session["NUMAIL"]
        //20 Session["Department"]
        //21 Session["uid"]
        //22 Session["right"]
        //23 Session["grade"]
        //24 Session["TcgeDataedit"]
        //25 Session["TcgeDataview"]
        //26 Session["SuperUser"]
        //27 Session["presented"]
        string token = ssUserID + "|" + ssUserType + "|" + ssUserName + "|" + dt + "|" + source + "|" + token_temp;
        //Response.Write(token);
        byte[] b = Encoding.UTF8.GetBytes(token);
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        var Key = new Byte[] {  };
        var IV = new Byte[] {  };
        ICryptoTransform ict = des.CreateEncryptor(Key, IV);
        byte[] outData = ict.TransformFinalBlock(b, 0, b.Length);
        var op = Convert.ToBase64String(outData);
        //*******************second part
        //0 dt
		//1 Session["IDNO"]
        string token2 = dt + "|" + Session["IDNO"];
        byte[] b2 = Encoding.UTF8.GetBytes(token2);
        DESCryptoServiceProvider des2 = new DESCryptoServiceProvider();
        var Key2 = "TGEOTGEO";
        var IV2 = "TGEOTGEO";
        ICryptoTransform ict2 = des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes(Key2), ASCIIEncoding.ASCII.GetBytes(IV2));
        byte[] outData2 = ict2.TransformFinalBlock(b2, 0, b2.Length);
        var op2 = Convert.ToBase64String(outData2);
        //*******************
        var qSWC002 = LBSWC002.Text;
        Session["NCKU"] = "T";

        string url = "https://tgeo.swc.taipei?UTYPE=" + ssUserType + "&UNAME=" + ssUserName + "&rid=" + qSWC002 + "&Token=" + op + "&Source=" + source + "&Token2=" + op2;

        Response.Write("<script>window.open('" + url + "','_blank');</script>");

        //Response.Redirect(url);
    }

	protected void SWCDTL05_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SWCDTL05.PageIndex = e.NewPageIndex;
        SWCDTL05.DataSource = (System.Data.DataTable)ViewState["GV05"];
        SWCDTL05.DataBind();
    }
	
	protected void SWCDTL02_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                HiddenField Lock02 = (HiddenField)e.Row.Cells[2].FindControl("Lock02");
                string tempLock = Lock02.Value;
                string GetDTLType = e.Row.Cells[0].Text.Substring(0, 2);
                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL02");
                    btn.Text = "搞啥" + GetDTLType;
                    btn.Visible = false;
                }
                break;
        }

    }
	protected void ButtonDTL02_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;

        string GetDTLType = LButton.CommandArgument.Substring(0, 2);
        string GetDTLType2 = LButton.CommandArgument.Substring(0, 6);
        if (GetDTLType2 == "SWCCHG") { GetDTLType = "RC"; }

        string LINK = "SWC001.aspx";
        switch (GetDTLType)
        {
            case "RC":
                LINK = "SWCDT003.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;
            case "RB":
                LINK = "SWCDT002.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;

        }
        Response.Redirect(LINK);

    }
	
	protected void ButtonDEL02_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";
        string exeSqlStr = " delete SWCDTL02 where swc000='" + tSwc000 + "' and dtlb000 ='" + tDtl000 + "';  ";
        ConnectionStringSettings connectionString02 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn02 = new SqlConnection(connectionString02.ConnectionString))
        {
            SwcConn02.Open();

            SqlDataReader readeSwc02;
            SqlCommand objCmdSwc02 = new SqlCommand(exeSqlStr, SwcConn02);
            readeSwc02 = objCmdSwc02.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC003.aspx?SWCNO=" + tSwc000 + "'; </script>");
    }

}