using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using Ionic.Zip;

using System.Data;
using System.Xml;
using System.Globalization;
using System.Net;

public partial class SWCDOC_SWC001 : System.Web.UI.Page
{
    int ChkOverDay = 6;
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
        string ssUserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssONLINEAPPLY = Session["ONLINEAPPLY"] + "";
        string rqSearch = Request["SR"] + "";
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string ssGuild01 = Session["Guild01"] + "";
        string ssGuild02 = Session["Guild02"] + "";

        GBClass001 SBApp = new GBClass001();

        if (!IsPostBack)
        {
            switch (ssUserType) {
            case "01":
                NewSwc.Visible = true;
				ToCalendar.Visible = false;
                //CHKSHTYPE.Visible = true;
				BTNSHTYPE.Visible = true;
                UserBoard00.Visible = true;
                break;
            case "02":
			case "08":
                if(CheckExpireOrNot()==true){Response.Write("<script>alert('提醒您，您所填登之執業執照到期日已逾期，無法使用水保計畫案件申請相關權限，若需恢復權限，請至「帳號管理」中更新「執業執照及到期日」欄位資訊。'); location.href='HaloPage001.aspx';</script>");}
                if(ssUserType == "02") NewSwc.Visible = true; else NewSwc.Visible = false;
				ToCalendar.Visible = true;
                if(ssUserType == "02") TitleLink00.Visible = true; else TitleLink00.Visible = false;
                UserBoard00.Visible = true;
                //CHKSHTYPE.Visible = true;
				BTNSHTYPE.Visible = true;
                ChgUserType.Visible = true;
                DDLChange.Text = "技師";
                break;
            case "03":
                NewSwc.Visible = false;
				ToCalendar.Visible = false;
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                break;
            case "04":
			case "09":
                NewSwc.Visible = false;
				ToCalendar.Visible = false;
                UserBoard00.Visible = true;
                if (ssONLINEAPPLY != "Y") GOVMG.Visible = false;
                if (ssUserID != ssUserGuild) { ChgUserType.Visible = true; DDLChange.Text = "公會"; ToCalendar.Visible = true;}
                if (ssGuild01 == "1" || ssGuild02 == "1") TitleLink01.Visible = true;
                break;
			case "05":
                NewSwc.Visible = false;
				ToCalendar.Visible = false;
                break;
			case "06":
                NewSwc.Visible = false;
				ToCalendar.Visible = false;
                break;
			case "07":
                NewSwc.Visible = false;
				ToCalendar.Visible = false;
                break;
            default:
                Response.Redirect("SWC000.aspx");
                break;
        }

            GenerateDropDownList();
            GetGVSWCList();

            if (rqSearch == "Y")
            {
                SessionSel2Page();
                ExeQSel_Click(sender, e);
            }
            else
            {
                RemoveSel_Click(sender, e);
            }

            SBApp.ViewRecord("書件目錄查詢", "view", "");
        }

        #region 您好...
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "") {
            TextUserName.Text =ssUserName+ ssJobTitle+"，您好";
        }
        #endregion
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

    private void RemoveSelSession()
    {
        Session["PageSearch001"] = "";
        Session["PageSearch002a"] = "";
        Session["PageSearch002b"] = "";
        Session["CHKSHTYPE"] = "";
        Session["PageSearch003"] = "";
        Session["PageSearch004"] = "";
        Session["PageSearchQ01"] = "";
        Session["PageSearchD01"] = "";
        Session["PageSearch004a"] = "";
        Session["PageSearch004b"] = "";
        Session["PageSearch004c"] = "";
        Session["PageSearch004d"] = "";
        Session["PageSearch004e"] = "";
        Session["PageSearch004f"] = "";
        Session["PageSearch004g"] = "";
        Session["PageSearch004h"] = "";
        Session["PageSearch004i"] = "";
        Session["PageSearch004j"] = "";
        Session["PageSearch004k"] = "";
        Session["PageSearch004l"] = "";
        Session["PageSearch004m"] = "";
        Session["PageSearch004n"] = "";
        Session["PageSearch004o"] = "";
        Session["PageSearchQQ5a"] = "";
        Session["PageSearchQQ5b"] = "";
        Session["PageSearchQQ5c"] = "";
        Session["PageSearchQQ5d"] = "";
        Session["PageSearchSWC038a"] = "";
        Session["PageSearchSWC038b"] = "";
        Session["PageSearchSWC058a"] = "";
        Session["PageSearchSWC058b"] = "";
    }

    private void SessionSel2Page()
    {
        GBClass001 SBApp = new GBClass001();

        TXTS001.Text = Session["PageSearch001"] + "";
        CHKS002a.Checked = (Session["PageSearch002a"] + "" == "True");
        CHKS002b.Checked = (Session["PageSearch002b"] + "" == "True");
        CHKSHTYPE.Checked = (Session["CHKSHTYPE"] + "" == "True");
        TXTS003.Text = Session["PageSearch003"]+ "";
        TXTS004.Text = Session["PageSearch004"] + "";
        TXTQQ01.Text = Session["PageSearchQ01"] + "";
        DDLQQ01.Text=Session["PageSearchD01"] + "";
        CHKS003a.Checked = (Session["PageSearch004a"] + "" == "True");
        CHKS003b.Checked = (Session["PageSearch004b"] + "" == "True");
        CHKS003c.Checked = (Session["PageSearch004c"] + "" == "True");
        CHKS003d.Checked = (Session["PageSearch004d"] + "" == "True");
        CHKS003e.Checked = (Session["PageSearch004e"] + "" == "True");
        CHKS003f.Checked = (Session["PageSearch004f"] + "" == "True");
        CHKS003g.Checked = (Session["PageSearch004g"] + "" == "True");
        CHKS003h.Checked = (Session["PageSearch004h"] + "" == "True");
        CHKS003i.Checked = (Session["PageSearch004i"] + "" == "True");
        CHKS003j.Checked = (Session["PageSearch004j"] + "" == "True");
        CHKS003k.Checked = (Session["PageSearch004k"] + "" == "True");
        CHKS003l.Checked = (Session["PageSearch004l"] + "" == "True");
        CHKS003m.Checked = (Session["PageSearch004m"] + "" == "True");
        CHKS003n.Checked = (Session["PageSearch004n"] + "" == "True");
        CheckBox1.Checked = (Session["PageSearch004o"] + "" == "True");

        DDLDistrict.Text = Session["PageSearchQQ5a"] + "";
        DDLSection.Text = Session["PageSearchQQ5b"] + "";
        DDLSection2.Text = Session["PageSearchQQ5c"] + "";
        TXTNumber.Text = Session["PageSearchQQ5d"] + "";
        
        TXTS005.Text = Session["PageSearchSWC038a"] + "";
        TXTS006.Text = Session["PageSearchSWC038b"] + "";
        TXTS007.Text = Session["PageSearchSWC058a"] + "";
        TXTS008.Text = Session["PageSearchSWC058b"] + "";
    }

    protected void GenerateDropDownList()
    {
        string[,] array_District = new string[,] { { "0", "" }, { "16", "北投" }, { "15", "士林" }, { "14", "內湖" }, { "10", "中山" }, { "03", "中正" }, { "17", "信義" }, { "02", "大安" }, { "13", "南港" }, { "11", "文山" } };
        List<ListItem> ListZip = new List<ListItem>();
        for (int te = 0; te <= array_District.GetUpperBound(0); te++)
        {
            ListItem li = new ListItem(array_District[te, 1], array_District[te, 0]);
            ListZip.Add(li);
        }
        DDLDistrict.Items.AddRange(ListZip.ToArray());
        
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            string sqlSelectStr = " SELECT * FROM [geouser]  where unit = '技師公會' order by id ";

            SqlDataReader readerUsers;
            SqlCommand objCmdUser = new SqlCommand(sqlSelectStr, UserConn);
            readerUsers = objCmdUser.ExecuteReader();

            DDLQQ01.Items.Clear();
            DDLQQ01.Items.Add("");

            while (readerUsers.Read())
            {
                DDLQQ01.Items.Add(new ListItem(readerUsers["name"].ToString(), readerUsers["name"].ToString()));
            }

            objCmdUser.Dispose();
            readerUsers.Close();
            UserConn.Close();
            UserConn.Dispose();
        }
    }

    protected void NewSwc_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SWC002.aspx?CaseId=ADDNEW");
    }
    protected void GetGVSWCList()
    {
        Class20 C20 = new Class20();
        Class1 C1 = new Class1();
        GBClass001 GBApp = new GBClass001();
        string UserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssUserGuild1 = Session["ETU_Guild01"] + "";
        string ssUserGuild2 = Session["ETU_Guild02"] + "";
        string ssUserGuild3 = Session["ETU_Guild03"] + "";
        string sqlCaseID = GBApp.GetGuildSWC000();

        string sqlSelectStr = " SELECT * FROM [SWCCASE] ";
        sqlSelectStr = sqlSelectStr + " WHERE 1=1 ";
        sqlSelectStr += limitListRange();

        //sqlSelectStr = sqlSelectStr + "  AND SWC005 NOT LIKE '%測試%' ";
        sqlSelectStr = sqlSelectStr + " ORDER BY [SWC000] DESC";
        C1.RvSysRecord("水保書件", "水保案件列表", "瀏覽", "SWC001", ssUserID, sqlSelectStr);
        SqlDataSource.SelectCommand = sqlSelectStr;
        SqlDataSourceReq.SelectCommand = sqlSelectStr;

        if (CHKSHTYPE.Checked)
        {
            List01.Visible = false;
            List02.Visible = true;
        }
        else
        {
            List01.Visible = true;
            List02.Visible = false;
        }

        Session["PrintExcelOdtStr"] = sqlSelectStr;
    }
    protected string limitListRange()
    {
        GBClass001 GBApp = new GBClass001();
        Class20 C20 = new Class20();
        string rValue = "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssUserGuild1 = Session["ETU_Guild01"] + "";
        string ssUserGuild2 = Session["ETU_Guild02"] + "";
        string ssUserGuild3 = Session["ETU_Guild03"] + "";
        string ssUserType = Session["UserType"] + "";
        string tUserType = ssUserType;
		string idno = Session["IDNO"] + "";
		string ssUserName = Session["NAME"] + "";

        if (ssUserID == "gv-admin") { } else {
            if (ssUserType == "04" && ssUserID != ssUserGuild1) tUserType = "87";
            switch (tUserType)
            {
                case "01":
                    rValue = rValue + " AND ((SWC013ID like '%" + ssUserID + "%' ";
                    rValue = rValue + " AND (SWC013TEL like '%" + ssUserPW + "%' and ISNULL(SWC013TEL,'')!='')) ";
                    rValue = rValue + " OR (SWC016 = '" + ssUserPW + "' and ISNULL(SWC016,'')!='')) ";
                    break;
                case "02":
                    rValue = rValue + " AND (SWC021ID = '" + ssUserID + "' ";
                    rValue = rValue + "  OR (SWC045ID = '" + ssUserID + "' AND SWC004 IN ('施工中','停工中','已完工','廢止','失效','已變更'))) ";
                    break;
                case "04":
                    rValue = rValue + " AND ((SWC022ID = '" + ssUserID + "' AND SWC004 IN ('審查中','暫停審查','撤銷','不予核定','已核定','施工中','停工中','已完工','廢止','失效','已變更')) ";
                    if (Session["Edit4"] + "" == "Y") rValue = rValue + "   OR SWC004 IN ('撤銷','已完工','廢止','失效','已變更')  ";
                    rValue = rValue + "   OR (SWC024ID = '" + ssUserID + "' AND SWC004 IN ('施工中','停工中','已完工','廢止','失效','已變更'))) ";
                    break;
				case "05":
					if(C20.GetOrganData(idno,"UnitName")+""=="陽明山國家公園")
					{
						rValue = rValue + " AND SWC000 in (select SWC000 from tslm2.dbo.SWCSWCA where SWC121 like '%國家公園%') ";
					}
					else
					{
						rValue = rValue + " AND 1=2 ";
					}
					break;
				case "06":
					rValue = rValue + " AND (SWC134 = '" + C20.GetArchitectData(idno,"帳號") + "' or SWC135 = '" + C20.GetArchitectData(idno,"帳號") + "') ";
					break;
				case "07":
					rValue = rValue + " AND (SWC116 = '" + ssUserID + "' or SWC049 = '" + ssUserName + "' or SWC050 = '" + ssUserPW + "') ";
					break;
                case "87":
                    rValue += " AND ((SWC022ID = '" + ssUserGuild1 + "' AND SWC004 IN ('審查中','暫停審查','撤銷','不予核定','已核定','施工中','停工中','已完工','廢止','失效','已變更')) OR (SWC022ID = '" + ssUserGuild3 + "' AND SWC004 IN ('審查中','暫停審查','撤銷','不予核定','已核定','施工中','停工中','已完工','廢止','失效','已變更')) OR (SWC024ID = '" + ssUserGuild2 + "' AND SWC004 IN ('施工中','停工中','已完工','廢止','失效','已變更')) OR (SWC024ID = '" + ssUserGuild3 + "' AND SWC004 IN ('施工中','停工中','已完工','廢止','失效','已變更'))      )";
                    rValue += " AND SWC000 IN ("+ GBApp.GetGuildSWC000() + ")";
                    break;
            }
        }
        return rValue;
    }

    protected void GVSWCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GBClass001 SBApp = new GBClass001();

        string ShowEditBtn = "N";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssUserType = Session["UserType"] + "";

        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                string tempCaseSwc000 = Convert.ToString(e.Row.Cells[1].Text);
                string tempCaseSwc004 = Convert.ToString(e.Row.Cells[3].Text);
                string tempCaseSwc024ID = "";

                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                {
                    string strSQLRV = " select * from SWCCASE where SWC000='" + tempCaseSwc000 + "' ";

                    SwcConn.Open();

                    SqlDataReader readerSWC;
                    SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
                    readerSWC = objCmdSWC.ExecuteReader();

                    while (readerSWC.Read())
                    {
                        ShowEditBtn = "N";
                        string tempSWC004 = readerSWC["SWC004"] + "";
                        string tempSWC007 = readerSWC["SWC007"] + "";
                        string tempSWC021ID = readerSWC["SWC021ID"] + "";
                        string tempSWC022ID = readerSWC["SWC022ID"] + "";
                        string tempSWC024ID = readerSWC["SWC024ID"] + "";
                        string tempSWC045ID = readerSWC["SWC045ID"] + "";

                        tempCaseSwc024ID = tempSWC024ID;

                        switch (ssUserType.ToString())
                        {
                            case "01":
                                if (tempSWC007 == "簡易水保" && (tempSWC004 == "退補件" || tempSWC004 == "申請中"))
                                    ShowEditBtn = "Y";
                                break;
                            case "02":
                                /* 2020版 - 編輯只剩基本資料可以修改、原本受理中可修改，改為不可修改
                                //承辦技師
                                if (tempSWC021ID == ssUserID && (tempSWC004 == "退補件" || tempSWC004 == "受理中" || tempSWC004 == "申請中" || tempSWC004 == "審查中"))
                                    ShowEditBtn = "Y";
                                //監造技師
                                if (tempSWC045ID == ssUserID && tempSWC004 == "施工中")
                                    ShowEditBtn = "Y";
                                */
                                //承辦技師
                                if (tempSWC021ID == ssUserID && (tempSWC004 == "退補件" || tempSWC004 == "申請中" || tempSWC004 == "受理中" || tempSWC004 == "審查中"))
                                    ShowEditBtn = "Y";
                                break;
                                break;
                            case "03":
                                OdsPointDtl3Date.Visible = true;
                                break;
                            case "04":
                                /* 2020版 - 編輯只剩基本資料可以修改
                                //審查公會
                                if (tempSWC022ID == ssUserID && (tempSWC004 == "審查中" || tempSWC004 == "暫停審查"))
                                {
                                    ShowEditBtn = "Y";
                                }
                                //檢查公會
                                if (tempSWC024ID == ssUserID && tempSWC004 == "施工中")
                                {
                                    ShowEditBtn = "Y";
                                }
                                //完工檢查公會
                                if (Session["Edit4"] + "" == "Y" && tempSWC004 == "已完工" || tempSWC004 == "撤銷" || tempSWC004 == "已變更")
                                {
                                    ShowEditBtn = "Y";
                                }
                                //召集人
                                string chkEidtStr= " select * from GuildGroup where ETID='"+ ssUserID + "' and SWC000 ='"+ tempCaseSwc000 + "' and CHGType='1' ";
                                using (SqlConnection GGConn = new SqlConnection(connectionString.ConnectionString))
                                {
                                    GGConn.Open();

                                    SqlDataReader readerGG;
                                    SqlCommand objCmdGG = new SqlCommand(chkEidtStr, GGConn);
                                    readerGG = objCmdGG.ExecuteReader();

                                    while (readerGG.Read())
                                    {
                                        string tmpRGtype = readerGG["RGtype"] +"";
                                        ShowEditBtn = (tmpRGtype == "S3" && (tempSWC004 == "審查中" || tempSWC004 == "暫停審查")) ? "Y": ShowEditBtn;
                                        ShowEditBtn = (tmpRGtype == "S4" && tempSWC004 == "施工中") ? "Y" : ShowEditBtn;
                                    }
                                }
                                */
                                break;
                        }
                    }
                }

                if (ShowEditBtn == "Y")
                {
                    Button btn = (Button)e.Row.Cells[0].FindControl("BtnEdit");
                    btn.Visible = true;
                }

                //施工檢查檢核(警示燈)
                if (tempCaseSwc024ID == ssUserID && tempCaseSwc004 =="施工中")
                {
                    using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        string strSQLRV = " select MAX(DTLC002) AS DTLC002  from SWCDTL03 where SWC000='" + tempCaseSwc000 + "' ";

                        SwcConn.Open();

                        SqlDataReader readerSWC;
                        SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
                        readerSWC = objCmdSWC.ExecuteReader();
                    
                        while (readerSWC.Read())
                        {
                            if ((readerSWC["DTLC002"] + "").Trim() != "")
                            {
                                DateTime tDTLC002 = Convert.ToDateTime(readerSWC["DTLC002"] +"");
                                DateTime tTodey = DateTime.Now;

                                TimeSpan ts = tTodey - tDTLC002;
                                double days = ts.TotalDays;

                                if (days > ChkOverDay)
                                {
                                    e.Row.Cells[0].Attributes.Add("style", "text-align:center");
                                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                                    e.Row.Cells[0].Text = "●";
                                }
                            }
                        }
                    }
                }

                //審查期限

                String tDATE5 = e.Row.Cells[5].Text + "";

                switch (tempCaseSwc004)
                {
                    case "退補件":
                    case "不予受理":
                    case "受理中":
                    case "申請中":
                    case "審查中":
                    case "暫停審查":
                        if (tDATE5.Trim() != "")
                        {
                            tDATE5 = SBApp.DateView(tDATE5, "02");
                        }
                        if (tDATE5.Trim().PadLeft(4) == "1900")
                        {
                            tDATE5 = "";
                        }
                        e.Row.Cells[5].Text = tDATE5;
                        break;

                    case "撤銷":
                    case "不予核定":
                    case "已核定":
                    case "施工中":
                    case "停工中":
                    case "已完工":
                    case "廢止":
                    case "失效":
                    case "已變更":
                        e.Row.Cells[5].Text = "審查完成";
                        break;
                    default:
                        e.Row.Cells[5].Text = "";
                        break;
                }


                if (tDATE5.Trim().PadLeft(4) == "1900")
                {
                    tDATE5 = "";
                    e.Row.Cells[5].Text = tDATE5;
                }
                break;
        }
    }

    private void DDLCadastralChange(string dTYPE, DropDownList vDP01, DropDownList vDP02, DropDownList vDP03, DropDownList vDP04)
    {
        //區、段、小段、地號
        string strDP01 = vDP01.Text;
        string strDP02 = vDP02.Text;
        string strDP03 = vDP03.Text;

        if (strDP01 == "0")
        {
            vDP03.Items.Clear();
            vDP02.Items.Clear();

        }
        else
        {
            string strSQLCAADDR = " ";

            switch (dTYPE)
            {
                case "01":
                    strSQLCAADDR = strSQLCAADDR + " SELECT DISTINCT LEFT(KCNT,2) AS KCNT FROM LAND ";
                    strSQLCAADDR = strSQLCAADDR + " WHERE AA46='" + strDP01 + "' ";
                    break;

                case "02":
                    strSQLCAADDR = strSQLCAADDR + " SELECT DISTINCT SUBSTRING(KCNT, LEN(KCNT) - 2, 1) AS KCNT2 FROM LAND ";
                    strSQLCAADDR = strSQLCAADDR + " WHERE AA46='" + strDP01 + "' ";
                    strSQLCAADDR = strSQLCAADDR + "   AND substring(KCNT, 1, 2)='" + strDP02 + "' ";

                    break;

                case "03":
                    string csKCNT = strDP02 + "段" + strDP03 + "小段";

                    strSQLCAADDR = strSQLCAADDR + " SELECT DISTINCT CADA_TEXT FROM LAND ";
                    strSQLCAADDR = strSQLCAADDR + " WHERE AA46='" + strDP01 + "' ";
                    strSQLCAADDR = strSQLCAADDR + "   AND KCNT='" + csKCNT + "' ";
                    strSQLCAADDR = strSQLCAADDR + " ORDER BY CADA_TEXT  ";
                    break;

            }

            //連DB
            ConnectionStringSettings settingCAADDR = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
            SqlConnection ConnCAADDR = new SqlConnection();
            ConnCAADDR.ConnectionString = settingCAADDR.ConnectionString;
            ConnCAADDR.Open();

            SqlDataReader readerCAADDR;
            SqlCommand objCmdGETCAADDR = new SqlCommand(strSQLCAADDR, ConnCAADDR);
            readerCAADDR = objCmdGETCAADDR.ExecuteReader();

            if (readerCAADDR.HasRows)
            {
                switch (dTYPE)
                {
                    case "01":
                        vDP03.Items.Clear();
                        vDP02.Items.Clear();                        
                        vDP02.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            vDP02.Items.Add(readerCAADDR["KCNT"].ToString());
                        }
                        break;
                    case "02":
                        vDP03.Items.Clear();
                        vDP03.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            vDP03.Items.Add(readerCAADDR["KCNT2"].ToString());
                        }
                        break;
                    case "03":
                        vDP04.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            if (readerCAADDR["CADA_TEXT"].ToString() != "") { vDP04.Items.Add(readerCAADDR["CADA_TEXT"].ToString()); }
                        }
                        break;
                }
            }

            objCmdGETCAADDR.Dispose();
            readerCAADDR.Close();

            ConnCAADDR.Close();
            ConnCAADDR.Dispose();

        }

    }
    protected void SqlDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        CaseCount.Text = e.AffectedRows.ToString();
    }
    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DDLCadastralChange("01", DDLDistrict, DDLSection, DDLSection2, null);
    }
    protected void DDLSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DDLCadastralChange("02", DDLDistrict, DDLSection, DDLSection2, null);
    }
    protected void DDLSection2_SelectedIndexChanged(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DDLCadastralChange("03", DDLDistrict, DDLSection, DDLSection2, null);
    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        TXTS001.Text = "";
        CHKS002a.Checked = false;
        CHKS002b.Checked = false;
        CHKSHTYPE.Checked = false;
        TXTS003.Text = "";
        TXTS004.Text = "";
        CheckBox1.Checked = false;
        CHKS003a.Checked = false;
        CHKS003b.Checked = false;
        CHKS003c.Checked = false;
        CHKS003d.Checked = false;
        CHKS003e.Checked = false;
        CHKS003f.Checked = false;
        CHKS003g.Checked = false;
        CHKS003h.Checked = false;
        CHKS003i.Checked = false;
        CHKS003j.Checked = false;
        CHKS003k.Checked = false;
        CHKS003l.Checked = false;
        CHKS003m.Checked = false;
        CHKS003n.Checked = false;
        TXTQQ01.Text = "";
        DDLQQ01.SelectedValue = "";
        TXTS005.Text = "";
        TXTS006.Text = "";
        TXTS007.Text = "";
        TXTS008.Text = "";

        RemoveSelSession();

        DDLDistrict.SelectedValue = "0";
        DDLCadastralChange("01", DDLDistrict, DDLSection, DDLSection2, null);
        TXTNumber.Text = "";

        GetGVSWCList();
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        SelectSearchCaseList("");
    }
    private void SelectSearchCaseList(string cPageMode)
    {
        GBClass001 GBApp = new GBClass001();

        RemoveSelSession();

        string qSearch001 = TXTS001.Text;
        bool qSearch002a = CHKS002a.Checked;
        bool qSearch002b = CHKS002b.Checked;
        bool qCHKSHTYPE = CHKSHTYPE.Checked;
        string qSearch003 = TXTS003.Text;

        string qSearch004 = TXTS004.Text;
        string qSearchQ01 = TXTQQ01.Text;
        string qSearchD01 = DDLQQ01.SelectedValue;

        bool qSearch004a = CHKS003a.Checked;
        bool qSearch004b = CHKS003b.Checked;
        bool qSearch004c = CHKS003c.Checked;
        bool qSearch004d = CHKS003d.Checked;
        bool qSearch004e = CHKS003e.Checked;
        bool qSearch004f = CHKS003f.Checked;
        bool qSearch004g = CHKS003g.Checked;
        bool qSearch004h = CHKS003h.Checked;
        bool qSearch004i = CHKS003i.Checked;
        bool qSearch004j = CHKS003j.Checked;
        bool qSearch004k = CHKS003k.Checked;
        bool qSearch004l = CHKS003l.Checked;
        bool qSearch004m = CHKS003m.Checked;
        bool qSearch004n = CHKS003n.Checked;
        bool qSearch004o = CheckBox1.Checked;

        string qSWC038a = TXTS005.Text + "";
        string qSWC038b = TXTS006.Text + "";
        string qSWC058a = TXTS007.Text + "";
        string qSWC058b = TXTS008.Text + "";

        string UserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssUserGuild = Session["WMGuild"] + "";

        bool[] arrySearch004 = new bool[] { qSearch004a, qSearch004b, qSearch004c, qSearch004d, qSearch004e, qSearch004f, qSearch004g, qSearch004h, qSearch004i, qSearch004j, qSearch004k, qSearch004l, qSearch004m, qSearch004n, qSearch004o };
        string[] arraySearch004Str = new string[] { "受理中","審查中","已核定","施工中","停工中","已完工","廢止","撤銷","失效","不予受理","不予核定","已變更", "退補件", "暫停審查","申請中" };

        string Search004STR = "";
        for (int i = 0; i < arrySearch004.Length; i++)
        {
            bool S04 = arrySearch004[i];
            string Str04 = arraySearch004Str[i];

            if (S04)
            {
                Search004STR = Search004STR + " SWC004='"+ Str04 + "'"; 
            }

        }

        string qqSearch05a = DDLDistrict.SelectedItem.Text;
        string qqSearch05b = DDLSection.SelectedValue;
        string qqSearch05c = DDLSection2.SelectedValue;
        string qqSearch05d = TXTNumber.Text;

        string qSearch06STR = "''";

        string tempSTR = "";
        if (qqSearch05a != "" || qqSearch05b != "" || qqSearch05c != "" || qqSearch05d != "" )
        {
            tempSTR = " SELECT SWC000 FROM SWCLAND ";
            tempSTR = tempSTR + "  WHERE 1=1 ";
            if (qqSearch05a != "")
            {
                tempSTR = tempSTR + "  AND LAND001 = '"+ qqSearch05a + "' ";
            }
            if (qqSearch05b != "")
            {
                tempSTR = tempSTR + "  AND LAND002 = '"+ qqSearch05b + "' ";
            }
            if (qqSearch05c != "")
            {
                tempSTR = tempSTR + "  AND LAND003 = '"+ qqSearch05c + "' ";
            }
            if (qqSearch05d != "")
            {
                tempSTR = tempSTR + "  AND LAND004 like '%"+ qqSearch05d + "%' ";
            }

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();
                
                SqlDataReader readerLAND;
                SqlCommand objCmdLAND = new SqlCommand(tempSTR, SwcConn);
                readerLAND = objCmdLAND.ExecuteReader();

                while (readerLAND.Read())
                {
                    string tSWC000 = readerLAND["SWC000"] + "";

                    if (qSearch06STR.IndexOf(tSWC000) < 0)
                    {
                        qSearch06STR = qSearch06STR + ",'" + tSWC000 + "'";
                    }

                }
            }
        }

        //查詢…
        string sqlSelectStr = " SELECT * FROM [SWCCASE] ";
        sqlSelectStr = sqlSelectStr + " WHERE 1=1 ";
        if (qSearch001 != "")
        {
            if (qCHKSHTYPE) { sqlSelectStr = sqlSelectStr + "  AND SWC002  = '" + qSearch001 + "' "; } else { sqlSelectStr = sqlSelectStr + "  AND SWC002 like '%" + qSearch001 + "%' "; }
        }
        if (qSearch002a != qSearch002b)
        {
            if (qSearch002a)
            {
                sqlSelectStr = sqlSelectStr + "  AND SWC007 = '水土保持計畫' ";
            }
            if (qSearch002b)
            {
                sqlSelectStr = sqlSelectStr + "  AND SWC007 = '簡易水保' ";
            }
        }
        if (qSearchQ01 != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC021 like N'%" + qSearchQ01 + "%' ";
        }
        if (qSearch003 != "")
        {
            if (qCHKSHTYPE) { sqlSelectStr = sqlSelectStr + "  AND SWC005  = '" + qSearch003 + "' "; } else { sqlSelectStr = sqlSelectStr + "  AND SWC005 like '%" + qSearch003 + "%' "; }
        }
        if (qSearch004 != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC013 like N'%" + qSearch004 + "%' ";
        }

        if (qSWC038a != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC038 >= '" + qSWC038a + "' ";
        }
        if (qSWC038b != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC038 <= '" + qSWC038b + "' ";
        }
        if (qSWC058a != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC058 >= '" + qSWC058a + "' ";
        }
        if (qSWC058b != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC058 <= '" + qSWC058b + "' ";
        }



        if (qSearchD01 != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC022 = '" + qSearchD01 + "' ";
        }
        if (Search004STR.Trim() !="")
        {
            sqlSelectStr = sqlSelectStr + "  AND (" + Search004STR.Trim().Replace(" "," OR ") + ")";
        }
        if (tempSTR != "" )
        {
            sqlSelectStr = sqlSelectStr + "  AND SWC000 IN ("+ qSearch06STR + ") ";
        }
        

        if (ssUserID == "gv-admin" || CHKSHTYPE.Checked)
        { }
        else
        {
            sqlSelectStr += limitListRange();
        }
        //sqlSelectStr = sqlSelectStr + "  AND SWC005 NOT LIKE '%測試%' ";
        sqlSelectStr = sqlSelectStr + " ORDER BY [SWC000] DESC ";

        //回上一頁用
        Session["PageSearch001"] = qSearch001;
        Session["PageSearch002a"] = qSearch002a;
        Session["PageSearch002b"] = qSearch002b;
        Session["CHKSHTYPE"] = qCHKSHTYPE;
        Session["PageSearch003"] = qSearch003;
        Session["PageSearch004"] = qSearch004;
        Session["PageSearchQ01"] = qSearchQ01;
        Session["PageSearchD01"] = qSearchD01;
        Session["PageSearch004a"] = qSearch004a;
        Session["PageSearch004b"] = qSearch004b;
        Session["PageSearch004c"] = qSearch004c;
        Session["PageSearch004d"] = qSearch004d;
        Session["PageSearch004e"] = qSearch004e;
        Session["PageSearch004f"] = qSearch004f;
        Session["PageSearch004g"] = qSearch004g;
        Session["PageSearch004h"] = qSearch004h;
        Session["PageSearch004i"] = qSearch004i;
        Session["PageSearch004j"] = qSearch004j;
        Session["PageSearch004k"] = qSearch004k;
        Session["PageSearch004l"] = qSearch004l;
        Session["PageSearch004m"] = qSearch004m;
        Session["PageSearch004n"] = qSearch004n;
        Session["PageSearch004o"] = qSearch004o;
        Session["PageSearchQQ5a"] = qqSearch05a;
        Session["PageSearchQQ5b"] = qqSearch05b;
        Session["PageSearchQQ5c"] = qqSearch05c;
        Session["PageSearchQQ5d"] = qqSearch05d;
        Session["PageSearchSWC038a"] = qSWC038a;
        Session["PageSearchSWC038b"] = qSWC038b;
        Session["PageSearchSWC058a"] = qSWC058a;
        Session["PageSearchSWC058b"] = qSWC058b;

        SqlDataSource.SelectCommand = sqlSelectStr;
        SqlDataSourceReq.SelectCommand = sqlSelectStr;

        if (CHKSHTYPE.Checked)
        {
            List01.Visible = false;
            List02.Visible = true;
        }
        else
        {
            List01.Visible = true;
            List02.Visible = false;
        }

        Session["PrintExcelOdtStr"] = sqlSelectStr;
    }
    protected void GVSWCList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ExeQSel_Click(sender, e);
    }

    protected void GVSWCList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["mySorting"] == null)
        {
            e.SortDirection = SortDirection.Ascending;
            ViewState["mySorting"] = "Ascending";
        }
        else
        {
            if (ViewState["mySorting"] + "" == "Ascending")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["mySorting"] = "Descending";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["mySorting"] = "Ascending";
            }
        }
        ExeQSel_Click(sender, e);
    }
    protected void WriteExcel_Click(object sender, ImageClickEventArgs e)
    {
        string ExcelFileName = AppDomain.CurrentDomain.BaseDirectory + "/OutputFile/" + DateTime.Now.ToString("yyyy-MM-dd_hhmmss") + "水土保持申請書件.xls";

        //Response.Write(ExcelFileName);

        Response.Clear();
        Response.Buffer = true;
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");

        FileStream Excelfilestream = new FileStream(ExcelFileName, FileMode.Create, FileAccess.Write);
        StreamWriter Excelstreamwrite = new StreamWriter(Excelfilestream, System.Text.Encoding.GetEncoding("unicode"));
        String Excelcol = "";

        //建立標題行各欄位
        Excelcol = Excelcol + "流水號" + "\t";
        Excelcol = Excelcol + "核定編號" + "\t";
        Excelcol = Excelcol + "水保局編號" + "\t";
        Excelcol = Excelcol + "監督管理案件編號" + "\t";
        Excelcol = Excelcol + "案件狀態" + "\t";
        Excelcol = Excelcol + "書件名稱" + "\t";
        Excelcol = Excelcol + "變更設計次數" + "\t";
        Excelcol = Excelcol + "書件類別" + "\t";
        //Excelcol = Excelcol + "區" + "\t";
        //Excelcol = Excelcol + "段" + "\t";
        //Excelcol = Excelcol + "小段" + "\t";
        //Excelcol = Excelcol + "地號" + "\t";
        Excelcol = Excelcol + "轄區" + "\t";
        Excelcol = Excelcol + "義務人(ID)" + "\t";
        Excelcol = Excelcol + "義務人(電話)" + "\t";
        Excelcol = Excelcol + "義務人" + "\t";
        Excelcol = Excelcol + "義務人地址" + "\t";
        Excelcol = Excelcol + "聯絡人" + "\t";
        Excelcol = Excelcol + "聯絡人手機" + "\t";
        Excelcol = Excelcol + "目的事業主管機關" + "\t";
        Excelcol = Excelcol + "上述的其他" + "\t";
        Excelcol = Excelcol + "工程類型" + "\t";
        Excelcol = Excelcol + "工程類型_其他" + "\t";
        Excelcol = Excelcol + "承辦技師(ID)" + "\t";
        Excelcol = Excelcol + "承辦技師(人名)" + "\t";
        Excelcol = Excelcol + "審查單位(帳號)" + "\t";
        Excelcol = Excelcol + "審查單位" + "\t";
        Excelcol = Excelcol + "核定面積_公頃" + "\t";
        Excelcol = Excelcol + "檢查公會(ID)" + "\t";
        Excelcol = Excelcol + "檢查公會" + "\t";
        Excelcol = Excelcol + "承辦人員" + "\t";
        Excelcol = Excelcol + "檔號date" + "\t";
        Excelcol = Excelcol + "座標X" + "\t";
        Excelcol = Excelcol + "座標Y" + "\t";
        Excelcol = Excelcol + "圖6 - 1(上傳)" + "\t";
        Excelcol = Excelcol + "圖6 - 1(上傳CAD檔)" + "\t";
        Excelcol = Excelcol + "圖7 - 1(上傳)" + "\t";
        Excelcol = Excelcol + "審查費繳納期限" + "\t";
        Excelcol = Excelcol + "補正期限" + "\t";
        Excelcol = Excelcol + "審查費繳納日期" + "\t";
        Excelcol = Excelcol + "受理日期" + "\t";
        Excelcol = Excelcol + "審查費金額" + "\t";
        Excelcol = Excelcol + "審查費核銷" + "\t";
        Excelcol = Excelcol + "審查費核銷日期" + "\t";
        Excelcol = Excelcol + "核定日期" + "\t";
        Excelcol = Excelcol + "核定文號" + "\t";
        Excelcol = Excelcol + "保證金金額" + "\t";
        Excelcol = Excelcol + "保證金繳納" + "\t";
        Excelcol = Excelcol + "保證金繳交日期" + "\t";
        Excelcol = Excelcol + "施工許可證核發日期" + "\t";
        Excelcol = Excelcol + "施工許可證核發文號" + "\t";
        Excelcol = Excelcol + " 監造技師(ID)" + "\t";
        Excelcol = Excelcol + "監造技師" + "\t";
        Excelcol = Excelcol + "監造技師地址" + "\t";
        Excelcol = Excelcol + "監造技師手機" + "\t";
        Excelcol = Excelcol + "施工廠商" + "\t";
        Excelcol = Excelcol + "工地負責人" + "\t";
        Excelcol = Excelcol + "工地負責人手機" + "\t";
        Excelcol = Excelcol + "開工日期" + "\t";
        Excelcol = Excelcol + "預定完工日期" + "\t";
        Excelcol = Excelcol + "停工日期" + "\t";
        Excelcol = Excelcol + "施工中監督檢查紀錄表" + "\t";
        Excelcol = Excelcol + "完工申報日期" + "\t";
        Excelcol = Excelcol + "保證金退還" + "\t";
        Excelcol = Excelcol + "保證金退還日期" + "\t";
        Excelcol = Excelcol + "完工日期" + "\t";
        Excelcol = Excelcol + "完工證明書核發日期" + "\t";
        Excelcol = Excelcol + "完工證明書核發文號" + "\t";
        Excelcol = Excelcol + "基地概況_建物" + "\t";
        Excelcol = Excelcol + "基地概況_建物數量" + "\t";
        Excelcol = Excelcol + "基地概況_道路" + "\t";
        Excelcol = Excelcol + "基地概況_道路數量" + "\t";
        Excelcol = Excelcol + "基地概況其他" + "\t";
        Excelcol = Excelcol + "基地概況_其他說明" + "\t";
        Excelcol = Excelcol + "滯洪沉砂設施" + "\t";
        Excelcol = Excelcol + "滯洪沉砂設施數量" + "\t";
        Excelcol = Excelcol + "滯洪量" + "\t";
        Excelcol = Excelcol + "沉砂量" + "\t";
        Excelcol = Excelcol + "排水設施" + "\t";
        Excelcol = Excelcol + "排水設施數量" + "\t";
        Excelcol = Excelcol + "擋土設施" + "\t";
        Excelcol = Excelcol + "擋土設施數量" + "\t";
        Excelcol = Excelcol + "設施維護檢查紀錄表" + "\t";
        Excelcol = Excelcol + "已廢止已撤銷已失效不予受理不予核定" + "\t";
        Excelcol = Excelcol + "已廢止已撤銷已失效不予受理不予核定日期" + "\t";
        Excelcol = Excelcol + "備註" + "\t";
        Excelcol = Excelcol + "核定本(上傳)" + "\t";
        Excelcol = Excelcol + "違規編號" + "\t";
        Excelcol = Excelcol + "開工期限" + "\t";
        Excelcol = Excelcol + "開工展延次數" + "\t";
        Excelcol = Excelcol + "停工期限" + "\t";
        Excelcol = Excelcol + "承辦技師(e - mail)" + "\t";
        Excelcol = Excelcol + "審查單位其他" + "\t";
        Excelcol = Excelcol + "審查委員" + "\t";
        Excelcol = Excelcol + "審查期限" + "\t";
        Excelcol = Excelcol + "暫停審查期限" + "\t";
        Excelcol = Excelcol + "監造技師Email" + "\t";
        Excelcol = Excelcol + "工程進度" + "\t";
        Excelcol = Excelcol + "停工展延次數" + "\t";
        Excelcol = Excelcol + "維護管理人" + "\t";
        Excelcol = Excelcol + "維護管理人地址" + "\t";
        Excelcol = Excelcol + "維護管理人手機" + "\t";
        Excelcol = Excelcol + "其他公開資訊說明1" + "\t";
        Excelcol = Excelcol + "其他公開資訊說明2" + "\t";
        Excelcol = Excelcol + "其他公開資訊說明3" + "\t";
        Excelcol = Excelcol + "檢查單位其他" + "\t";
        Excelcol = Excelcol + "竣工圖說" + "\t";
        
        //把第一行的標題行寫入串流
        Excelstreamwrite.WriteLine(Excelcol.ToString());

        //開始填入裡面的值

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string PrintSQLStr = Session["PrintExcelOdtStr"] + "";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(PrintSQLStr, SwcConn);
            readerItem = objCmdItem.ExecuteReader();
            
            while (readerItem.Read())
            {
                string dSWC000 = readerItem["SWC000"] + "";
                string dSWC001 = readerItem["SWC001"] + "";
                string dSWC002 = readerItem["SWC002"] + "";
                string dSWC003 = readerItem["SWC003"] + "";
                string dSWC004 = readerItem["SWC004"] + "";
                string dSWC005 = readerItem["SWC005"] + "";
                string dSWC006 = readerItem["SWC006"] + "";
                string dSWC007 = readerItem["SWC007"] + "";
                string dSWC008 = readerItem["SWC008"] + "";
                string dSWC009 = readerItem["SWC009"] + "";
                string dSWC010 = readerItem["SWC010"] + "";
                string dSWC011 = readerItem["SWC011"] + "";
                string dSWC012 = readerItem["SWC012"] + "";
                string dSWC013ID = readerItem["SWC013ID"] + "";
                string dSWC013TEL = readerItem["SWC013TEL"] + "";
                string dSWC013 = readerItem["SWC013"] + "";
                string dSWC014 = readerItem["SWC014"] + "";
                string dSWC015 = readerItem["SWC015"] + "";
                string dSWC016 = readerItem["SWC016"] + "";
                string dSWC017 = readerItem["SWC017"] + "";
                string dSWC018 = readerItem["SWC018"] + "";
                string dSWC019 = readerItem["SWC019"] + "";
                string dSWC020 = readerItem["SWC020"] + "";
                string dSWC021ID = readerItem["SWC021ID"] + "";
                string dSWC021 = readerItem["SWC021"] + "";
                string dSWC022ID = readerItem["SWC022ID"] + "";
                string dSWC022 = readerItem["SWC022"] + "";
                string dSWC023 = readerItem["SWC023"] + "";
                string dSWC024ID = readerItem["SWC024ID"] + "";
                string dSWC024 = readerItem["SWC024"] + "";
                string dSWC025 = readerItem["SWC025"] + "";
                string dSWC026 = readerItem["SWC026"] + "";
                string dSWC027 = readerItem["SWC027"] + "";
                string dSWC028 = readerItem["SWC028"] + "";
                string dSWC029 = readerItem["SWC029"] + "";
                string dSWC029CAD = readerItem["SWC029CAD"] + "";
                string dSWC030 = readerItem["SWC030"] + "";
                string dSWC031 = readerItem["SWC031"] + "";
                string dSWC032 = readerItem["SWC032"] + "";
                string dSWC033 = readerItem["SWC033"] + "";
                string dSWC034 = readerItem["SWC034"] + "";
                string dSWC035 = readerItem["SWC035"] + "";
                string dSWC036 = readerItem["SWC036"] + "";
                string dSWC037 = readerItem["SWC037"] + "";
                string dSWC038 = readerItem["SWC038"] + "";
                string dSWC039 = readerItem["SWC039"] + "";
                string dSWC040 = readerItem["SWC040"] + "";
                string dSWC041 = readerItem["SWC041"] + "";
                string dSWC042 = readerItem["SWC042"] + "";
                string dSWC043 = readerItem["SWC043"] + "";
                string dSWC044 = readerItem["SWC044"] + "";
                string dSWC045ID = readerItem["SWC045ID"] + "";
                string dSWC045 = readerItem["SWC045"] + "";
                string dSWC046 = readerItem["SWC046"] + "";
                string dSWC047 = readerItem["SWC047"] + "";
                string dSWC048 = readerItem["SWC048"] + "";
                string dSWC049 = readerItem["SWC049"] + "";
                string dSWC050 = readerItem["SWC050"] + "";
                string dSWC051 = readerItem["SWC051"] + "";
                string dSWC052 = readerItem["SWC052"] + "";
                string dSWC053 = readerItem["SWC053"] + "";
                string dSWC054 = readerItem["SWC054"] + "";
                string dSWC055 = readerItem["SWC055"] + "";
                string dSWC056 = readerItem["SWC056"] + "";
                string dSWC057 = readerItem["SWC057"] + "";
                string dSWC058 = readerItem["SWC058"] + "";
                string dSWC059 = readerItem["SWC059"] + "";
                string dSWC060 = readerItem["SWC060"] + "";
                string dSWC061 = readerItem["SWC061"] + "";
                string dSWC062 = readerItem["SWC062"] + "";
                string dSWC063 = readerItem["SWC063"] + "";
                string dSWC064 = readerItem["SWC064"] + "";
                string dSWC065 = readerItem["SWC065"] + "";
                string dSWC066 = readerItem["SWC066"] + "";
                string dSWC067 = readerItem["SWC067"] + "";
                string dSWC068 = readerItem["SWC068"] + "";
                string dSWC069 = readerItem["SWC069"] + "";
                string dSWC070 = readerItem["SWC070"] + "";
                string dSWC071 = readerItem["SWC071"] + "";
                string dSWC072 = readerItem["SWC072"] + "";
                string dSWC073 = readerItem["SWC073"] + "";
                string dSWC074 = readerItem["SWC074"] + "";
                string dSWC075 = readerItem["SWC075"] + "";
                string dSWC076 = readerItem["SWC076"] + "";
                string dSWC077 = readerItem["SWC077"] + "";
                string dSWC078 = readerItem["SWC078"] + "";
                string dSWC079 = readerItem["SWC079"] + "";
                string dSWC080 = readerItem["SWC080"] + "";
                string dSWC081 = readerItem["SWC081"] + "";
                string dSWC082 = readerItem["SWC082"] + "";
                string dSWC083 = readerItem["SWC083"] + "";
                string dSWC084 = readerItem["SWC084"] + "";
                string dSWC085 = readerItem["SWC085"] + "";
                string dSWC086 = readerItem["SWC086"] + "";
                string dSWC087 = readerItem["SWC087"] + "";
                string dSWC088 = readerItem["SWC088"] + "";
                string dSWC089 = readerItem["SWC089"] + "";
                string dSWC090 = readerItem["SWC090"] + "";
                string dSWC091 = readerItem["SWC091"] + "";
                string dSWC092 = readerItem["SWC092"] + "";
                string dSWC093 = readerItem["SWC093"] + "";
                string dSWC094 = readerItem["SWC094"] + "";
                string dSWC095 = readerItem["SWC095"] + "";
                string dSWC096 = readerItem["SWC096"] + "";
                string dSWC097 = readerItem["SWC097"] + "";
                string dSWC098 = readerItem["SWC098"] + "";
                string dSWC099 = readerItem["SWC099"] + "";
                string dSWC101 = readerItem["SWC101"] + "";
                //string dSWC102 = readerItem["SWC102"] + "";
                //string dSWC103 = readerItem["SWC103"] + "";
                //string dSWC104 = readerItem["SWC104"] + "";
                //string dSWC105 = readerItem["SWC105"] + "";
                //string dSWC106 = readerItem["SWC106"] + "";
                //string dSWC107ID = readerItem["SWC107ID"] + "";
                //string dSWC107 = readerItem["SWC107"] + "";

                Excelcol = dSWC000 + "\t";
                Excelcol = Excelcol + dSWC001 + "\t";
                Excelcol = Excelcol + dSWC002 + "\t";
                Excelcol = Excelcol + dSWC003 + "\t";
                Excelcol = Excelcol + dSWC004 + "\t";
                Excelcol = Excelcol + dSWC005 + "\t";
                Excelcol = Excelcol + dSWC006 + "\t";
                Excelcol = Excelcol + dSWC007 + "\t";
                Excelcol = Excelcol + dSWC008 + "\t";
                //Excelcol = Excelcol + dSWC009 + "\t";
                //Excelcol = Excelcol + dSWC010 + "\t";
                //Excelcol = Excelcol + dSWC011 + "\t";
                //Excelcol = Excelcol + dSWC012 + "\t";
                Excelcol = Excelcol + dSWC013ID + "\t";
                Excelcol = Excelcol + dSWC013TEL + "\t";
                Excelcol = Excelcol + dSWC013 + "\t";
                Excelcol = Excelcol + dSWC014 + "\t";
                Excelcol = Excelcol + dSWC015 + "\t";
                Excelcol = Excelcol + dSWC016 + "\t";
                Excelcol = Excelcol + dSWC017 + "\t";
                Excelcol = Excelcol + dSWC018 + "\t";
                Excelcol = Excelcol + dSWC019 + "\t";
                Excelcol = Excelcol + dSWC020 + "\t";
                Excelcol = Excelcol + dSWC021ID + "\t";
                Excelcol = Excelcol + dSWC021.Replace("\n", "").Replace("\r", "") + "\t";
                Excelcol = Excelcol + dSWC022ID + "\t";
                Excelcol = Excelcol + dSWC022 + "\t";
                Excelcol = Excelcol + dSWC023 + "\t";
                Excelcol = Excelcol + dSWC024ID + "\t";
                Excelcol = Excelcol + dSWC024 + "\t";
                Excelcol = Excelcol + dSWC025.Replace("\n", "").Replace("\r", "") + "\t";
                Excelcol = Excelcol + dSWC026 + "\t";
                Excelcol = Excelcol + dSWC027 + "\t";
                Excelcol = Excelcol + dSWC028 + "\t";
                Excelcol = Excelcol + dSWC029 + "\t";
                Excelcol = Excelcol + dSWC029CAD + "\t";
                Excelcol = Excelcol + dSWC030 + "\t";
                Excelcol = Excelcol + dSWC031 + "\t";
                Excelcol = Excelcol + dSWC032 + "\t";
                Excelcol = Excelcol + dSWC033 + "\t";
                Excelcol = Excelcol + dSWC034 + "\t";
                Excelcol = Excelcol + dSWC035 + "\t";
                Excelcol = Excelcol + dSWC036 + "\t";
                Excelcol = Excelcol + dSWC037 + "\t";
                Excelcol = Excelcol + dSWC038 + "\t";
                Excelcol = Excelcol + dSWC039 + "\t";
                Excelcol = Excelcol + dSWC040 + "\t";
                Excelcol = Excelcol + dSWC041 + "\t";
                Excelcol = Excelcol + dSWC042 + "\t";
                Excelcol = Excelcol + dSWC043 + "\t";
                Excelcol = Excelcol + dSWC044 + "\t";
                Excelcol = Excelcol + dSWC045ID + "\t";
                Excelcol = Excelcol + dSWC045 + "\t";
                Excelcol = Excelcol + dSWC046 + "\t";
                Excelcol = Excelcol + dSWC047 + "\t";
                Excelcol = Excelcol + dSWC048 + "\t";
                Excelcol = Excelcol + dSWC049 + "\t";
                Excelcol = Excelcol + dSWC050 + "\t";
                Excelcol = Excelcol + dSWC051 + "\t";
                Excelcol = Excelcol + dSWC052 + "\t";
                Excelcol = Excelcol + dSWC053 + "\t";
                Excelcol = Excelcol + dSWC054 + "\t";
                Excelcol = Excelcol + dSWC055 + "\t";
                Excelcol = Excelcol + dSWC056 + "\t";
                Excelcol = Excelcol + dSWC057 + "\t";
                Excelcol = Excelcol + dSWC058 + "\t";
                Excelcol = Excelcol + dSWC059 + "\t";
                Excelcol = Excelcol + dSWC060 + "\t";
                Excelcol = Excelcol + dSWC061 + "\t";
                Excelcol = Excelcol + dSWC062 + "\t";
                Excelcol = Excelcol + dSWC063 + "\t";
                Excelcol = Excelcol + dSWC064 + "\t";
                Excelcol = Excelcol + dSWC065 + "\t";
                Excelcol = Excelcol + dSWC066 + "\t";
                Excelcol = Excelcol + dSWC067 + "\t";
                Excelcol = Excelcol + dSWC068 + "\t";
                Excelcol = Excelcol + dSWC069 + "\t";
                Excelcol = Excelcol + dSWC070 + "\t";
                Excelcol = Excelcol + dSWC071 + "\t";
                Excelcol = Excelcol + dSWC072 + "\t";
                Excelcol = Excelcol + dSWC073 + "\t";
                Excelcol = Excelcol + dSWC074 + "\t";
                Excelcol = Excelcol + dSWC075 + "\t";
                Excelcol = Excelcol + dSWC076 + "\t";
                Excelcol = Excelcol + dSWC077 + "\t";
                Excelcol = Excelcol + dSWC078 + "\t";
                Excelcol = Excelcol + dSWC079 + "\t";
                Excelcol = Excelcol + dSWC080 + "\t";
                Excelcol = Excelcol + dSWC081 + "\t";
                Excelcol = Excelcol + dSWC082 + "\t";
                Excelcol = Excelcol + dSWC083 + "\t";
                Excelcol = Excelcol + dSWC084 + "\t";
                Excelcol = Excelcol + dSWC085 + "\t";
                Excelcol = Excelcol + dSWC086 + "\t";
                Excelcol = Excelcol + dSWC087 + "\t";
                Excelcol = Excelcol + dSWC088 + "\t";
                Excelcol = Excelcol + dSWC089 + "\t";
                Excelcol = Excelcol + dSWC090 + "\t";
                Excelcol = Excelcol + dSWC091 + "\t";
                Excelcol = Excelcol + dSWC092 + "\t";
                Excelcol = Excelcol + dSWC093 + "\t";
                Excelcol = Excelcol + dSWC094 + "\t";
                Excelcol = Excelcol + dSWC095 + "\t";
                Excelcol = Excelcol + dSWC096 + "\t";
                Excelcol = Excelcol + dSWC097 + "\t";
                Excelcol = Excelcol + dSWC098 + "\t";
                Excelcol = Excelcol + dSWC099 + "\t";
                Excelcol = Excelcol + dSWC101 + "\t";
                //Excelcol = Excelcol + dSWC102 + "\t";
                //Excelcol = Excelcol + dSWC103 + "\t";
                //Excelcol = Excelcol + dSWC104 + "\t";
                //Excelcol = Excelcol + dSWC105 + "\t";
                //Excelcol = Excelcol + dSWC106 + "\t";
                //Excelcol = Excelcol + dSWC107 + "\t";

                Excelstreamwrite.WriteLine(Excelcol.ToString());

            }

            

        }

        
        
        Excelstreamwrite.Close();

        Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(DateTime.Now.ToString("yyyy-MM-dd_hhmmss") + "水土保持申請書件.xls"));
        Response.ContentType = "application/ms-excel";

        //指定返回的是一個不能被用戶端讀取的流，必須被下載
        Response.WriteFile(ExcelFileName);

        //把檔流發送到用戶端
        Response.End();

    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;        
        string tDtl000 = LButton.CommandArgument + "";

        Response.Redirect("SWC002.aspx?CaseId=" + tDtl000);        
    }
    protected void WriteOds_Click(object sender, ImageClickEventArgs e)
    {
        string tempFilseName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".ods";
        string outputFilePath = ConfigurationManager.AppSettings["SwcFileTemp"] + tempFilseName;

        string PrintSQLStr = "";
        
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            PrintSQLStr = Session["PrintExcelOdtStr"] + "";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(PrintSQLStr, SwcConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dSWC000 = readerItem["SWC000"] + "";
                string dSWC001 = readerItem["SWC001"] + "";
                string dSWC002 = readerItem["SWC002"] + "";
                string dSWC003 = readerItem["SWC003"] + "";
                string dSWC004 = readerItem["SWC004"] + "";
                string dSWC005 = readerItem["SWC005"] + "";
                string dSWC006 = readerItem["SWC006"] + "";
                string dSWC007 = readerItem["SWC007"] + "";
                string dSWC008 = readerItem["SWC008"] + "";
                string dSWC009 = readerItem["SWC009"] + "";
                string dSWC010 = readerItem["SWC010"] + "";
                string dSWC011 = readerItem["SWC011"] + "";
                string dSWC012 = readerItem["SWC012"] + "";
                string dSWC013ID = readerItem["SWC013ID"] + "";
                string dSWC013TEL = readerItem["SWC013TEL"] + "";
                string dSWC013 = readerItem["SWC013"] + "";
                string dSWC014 = readerItem["SWC014"] + "";
                string dSWC015 = readerItem["SWC015"] + "";
                string dSWC016 = readerItem["SWC016"] + "";
                string dSWC017 = readerItem["SWC017"] + "";
                string dSWC018 = readerItem["SWC018"] + "";
                string dSWC019 = readerItem["SWC019"] + "";
                string dSWC020 = readerItem["SWC020"] + "";
                string dSWC021ID = readerItem["SWC021ID"] + "";
                string dSWC021 = readerItem["SWC021"] + "";
                string dSWC022ID = readerItem["SWC022ID"] + "";
                string dSWC022 = readerItem["SWC022"] + "";
                string dSWC023 = readerItem["SWC023"] + "";
                string dSWC024ID = readerItem["SWC024ID"] + "";
                string dSWC024 = readerItem["SWC024"] + "";
                string dSWC025 = readerItem["SWC025"] + "";
                string dSWC026 = readerItem["SWC026"] + "";
                string dSWC027 = readerItem["SWC027"] + "";
                string dSWC028 = readerItem["SWC028"] + "";
                string dSWC029 = readerItem["SWC029"] + "";
                string dSWC029CAD = readerItem["SWC029CAD"] + "";
                string dSWC030 = readerItem["SWC030"] + "";
                string dSWC031 = readerItem["SWC031"] + "";
                string dSWC032 = readerItem["SWC032"] + "";
                string dSWC033 = readerItem["SWC033"] + "";
                string dSWC034 = readerItem["SWC034"] + "";
                string dSWC035 = readerItem["SWC035"] + "";
                string dSWC036 = readerItem["SWC036"] + "";
                string dSWC037 = readerItem["SWC037"] + "";
                string dSWC038 = readerItem["SWC038"] + "";
                string dSWC039 = readerItem["SWC039"] + "";
                string dSWC040 = readerItem["SWC040"] + "";
                string dSWC041 = readerItem["SWC041"] + "";
                string dSWC042 = readerItem["SWC042"] + "";
                string dSWC043 = readerItem["SWC043"] + "";
                string dSWC044 = readerItem["SWC044"] + "";
                string dSWC045ID = readerItem["SWC045ID"] + "";
                string dSWC045 = readerItem["SWC045"] + "";
                string dSWC046 = readerItem["SWC046"] + "";
                string dSWC047 = readerItem["SWC047"] + "";
                string dSWC048 = readerItem["SWC048"] + "";
                string dSWC049 = readerItem["SWC049"] + "";
                string dSWC050 = readerItem["SWC050"] + "";
                string dSWC051 = readerItem["SWC051"] + "";
                string dSWC052 = readerItem["SWC052"] + "";
                string dSWC053 = readerItem["SWC053"] + "";
                string dSWC054 = readerItem["SWC054"] + "";
                string dSWC055 = readerItem["SWC055"] + "";
                string dSWC056 = readerItem["SWC056"] + "";
                string dSWC057 = readerItem["SWC057"] + "";
                string dSWC058 = readerItem["SWC058"] + "";
                string dSWC059 = readerItem["SWC059"] + "";
                string dSWC060 = readerItem["SWC060"] + "";
                string dSWC061 = readerItem["SWC061"] + "";
                string dSWC062 = readerItem["SWC062"] + "";
                string dSWC063 = readerItem["SWC063"] + "";
                string dSWC064 = readerItem["SWC064"] + "";
                string dSWC065 = readerItem["SWC065"] + "";
                string dSWC066 = readerItem["SWC066"] + "";
                string dSWC067 = readerItem["SWC067"] + "";
                string dSWC068 = readerItem["SWC068"] + "";
                string dSWC069 = readerItem["SWC069"] + "";
                string dSWC070 = readerItem["SWC070"] + "";
                string dSWC071 = readerItem["SWC071"] + "";
                string dSWC072 = readerItem["SWC072"] + "";
                string dSWC073 = readerItem["SWC073"] + "";
                string dSWC074 = readerItem["SWC074"] + "";
                string dSWC075 = readerItem["SWC075"] + "";
                string dSWC076 = readerItem["SWC076"] + "";
                string dSWC077 = readerItem["SWC077"] + "";
                string dSWC078 = readerItem["SWC078"] + "";
                string dSWC079 = readerItem["SWC079"] + "";
                string dSWC080 = readerItem["SWC080"] + "";
                string dSWC081 = readerItem["SWC081"] + "";
                string dSWC082 = readerItem["SWC082"] + "";
                string dSWC083 = readerItem["SWC083"] + "";
                string dSWC084 = readerItem["SWC084"] + "";
                string dSWC085 = readerItem["SWC085"] + "";
                string dSWC086 = readerItem["SWC086"] + "";
                string dSWC087 = readerItem["SWC087"] + "";
                string dSWC088 = readerItem["SWC088"] + "";
                string dSWC089 = readerItem["SWC089"] + "";
                string dSWC090 = readerItem["SWC090"] + "";
                string dSWC091 = readerItem["SWC091"] + "";
                string dSWC092 = readerItem["SWC092"] + "";
                string dSWC093 = readerItem["SWC093"] + "";
                string dSWC094 = readerItem["SWC094"] + "";
                string dSWC095 = readerItem["SWC095"] + "";
                string dSWC096 = readerItem["SWC096"] + "";
                string dSWC097 = readerItem["SWC097"] + "";
                string dSWC098 = readerItem["SWC098"] + "";
                string dSWC099 = readerItem["SWC099"] + "";
                string dSaveuser = readerItem["Saveuser"] + "";
                string dsavedate = readerItem["savedate"] + "";
                string dSWC101 = readerItem["SWC101"] + "";
                //string dSWC102 = readerItem["SWC102"] + "";
                //string dSWC103 = readerItem["SWC103"] + "";
                //string dSWC104 = readerItem["SWC104"] + "";
                //string dSWC105 = readerItem["SWC105"] + "";
                //string dSWC106 = readerItem["SWC106"] + "";
                //string dSWC107ID = readerItem["SWC107ID"] + "";
                //string dSWC107 = readerItem["SWC107"] + "";
                
                DataTable OBJ_GV = (DataTable)ViewState["GV"];
                DataTable DTGV = new DataTable();

                if (OBJ_GV == null)
                {
                    DTGV.Columns.Add(new DataColumn("DTSWC000", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC001", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC002", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC003", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC004", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC005", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC006", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC007", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC008", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC009", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC010", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC011", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC012", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC013ID", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC013TEL", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC013", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC014", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC015", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC016", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC017", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC018", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC019", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC020", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC021ID", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC021", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC022ID", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC022", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC023", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC024ID", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC024", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC025", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC026", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC027", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC028", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC029", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC029CAD", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC030", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC031", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC032", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC033", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC034", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC035", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC036", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC037", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC038", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC039", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC040", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC041", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC042", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC043", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC044", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC045ID", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC045", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC046", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC047", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC048", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC049", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC050", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC051", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC052", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC053", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC054", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC055", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC056", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC057", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC058", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC059", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC060", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC061", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC062", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC063", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC064", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC065", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC066", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC067", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC068", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC069", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC070", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC071", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC072", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC073", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC074", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC075", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC076", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC077", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC078", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC079", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC080", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC081", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC082", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC083", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC084", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC085", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC086", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC087", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC088", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC089", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC090", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC091", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC092", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC093", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC094", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC095", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC096", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC097", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC098", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC099", typeof(string)));
                    DTGV.Columns.Add(new DataColumn("DTSWC101", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC102", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC103", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC104", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC105", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC106", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC107ID", typeof(string)));
                    //DTGV.Columns.Add(new DataColumn("DTSWC107", typeof(string)));
                    
                    ViewState["GV"] = DTGV;
                    OBJ_GV = DTGV;

                    DataRow drTitle = OBJ_GV.NewRow();

                    drTitle["DTSWC000"] = "流水號	";
                    drTitle["DTSWC001"] = "核定編號	";
                    drTitle["DTSWC002"] = "水保局編號	";
                    drTitle["DTSWC003"] = "監督管理案件編號	";
                    drTitle["DTSWC004"] = "案件狀態	";
                    drTitle["DTSWC005"] = "書件名稱	";
                    drTitle["DTSWC006"] = "變更設計次數	";
                    drTitle["DTSWC007"] = "書件類別	";
                    //drTitle["DTSWC008"] = "地籍_區	";
                    //drTitle["DTSWC009"] = "地籍_段	";
                    //drTitle["DTSWC010"] = "地籍_小段	";
                    //drTitle["DTSWC011"] = "地籍_號	";
                    drTitle["DTSWC012"] = "轄區	";
                    drTitle["DTSWC013ID"] = "義務人(ID)	";
                    drTitle["DTSWC013TEL"] = "義務人(電話)	";
                    drTitle["DTSWC013"] = "義務人	";
                    drTitle["DTSWC014"] = "義務人地址	";
                    drTitle["DTSWC015"] = "聯絡人	";
                    drTitle["DTSWC016"] = "聯絡人手機	";
                    drTitle["DTSWC017"] = "目的事業主管機關	";
                    drTitle["DTSWC018"] = "上述的其他	";
                    drTitle["DTSWC019"] = "工程類型	";
                    drTitle["DTSWC020"] = "工程類型_其他	";
                    drTitle["DTSWC021ID"] = "承辦技師(ID)	";
                    drTitle["DTSWC021"] = "承辦技師(人名)	";
                    drTitle["DTSWC022ID"] = "審查單位(帳號)	";
                    drTitle["DTSWC022"] = "審查單位	";
                    drTitle["DTSWC023"] = "核定面積_公頃	";
                    drTitle["DTSWC024ID"] = "檢查公會(ID)	";
                    drTitle["DTSWC024"] = "檢查公會	";
                    drTitle["DTSWC025"] = "承辦人員	";
                    drTitle["DTSWC026"] = "檔號date	";
                    drTitle["DTSWC027"] = "座標X";
                    drTitle["DTSWC028"] = "座標Y	";
                    drTitle["DTSWC029"] = "圖6-1(上傳)	";
                    drTitle["DTSWC029CAD"] = "圖6-1(上傳CAD檔)	";
                    drTitle["DTSWC030"] = "圖7-1(上傳)	";
                    drTitle["DTSWC031"] = "審查費繳納期限	";
                    drTitle["DTSWC032"] = "補正期限	";
                    drTitle["DTSWC033"] = "審查費繳納日期	";
                    drTitle["DTSWC034"] = "受理日期	";
                    drTitle["DTSWC035"] = "審查費金額	";
                    drTitle["DTSWC036"] = "審查費核銷	";
                    drTitle["DTSWC037"] = "審查費核銷日期	";
                    drTitle["DTSWC038"] = "核定日期	";
                    drTitle["DTSWC039"] = "核定文號	";
                    drTitle["DTSWC040"] = "保證金金額	";
                    drTitle["DTSWC041"] = "保證金繳納	";
                    drTitle["DTSWC042"] = "保證金繳交日期	";
                    drTitle["DTSWC043"] = "施工許可證核發日期	";
                    drTitle["DTSWC044"] = "施工許可證核發文號	";
                    drTitle["DTSWC045ID"] = "監造技師(ID)	";
                    drTitle["DTSWC045"] = "監造技師	";
                    drTitle["DTSWC046"] = "監造技師地址	";
                    drTitle["DTSWC047"] = "監造技師手機	";
                    drTitle["DTSWC048"] = "施工廠商	";
                    drTitle["DTSWC049"] = "工地負責人	";
                    drTitle["DTSWC050"] = "工地負責人手機	";
                    drTitle["DTSWC051"] = "開工日期	";
                    drTitle["DTSWC052"] = "預定完工日期	";
                    drTitle["DTSWC053"] = "停工日期	";
                    drTitle["DTSWC054"] = "施工中監督檢查紀錄表	";
                    drTitle["DTSWC055"] = "完工申報日期	";
                    drTitle["DTSWC056"] = "保證金退還	";
                    drTitle["DTSWC057"] = "保證金退還日期	";
                    drTitle["DTSWC058"] = "完工日期	";
                    drTitle["DTSWC059"] = "完工證明書核發日期	";
                    drTitle["DTSWC060"] = "完工證明書核發文號	";
                    drTitle["DTSWC061"] = "基地概況_建物	";
                    drTitle["DTSWC062"] = "基地概況_建物數量	";
                    drTitle["DTSWC063"] = "基地概況_道路	";
                    drTitle["DTSWC064"] = "基地概況_道路數量	";
                    drTitle["DTSWC065"] = "基地概況其他	";
                    drTitle["DTSWC066"] = "基地概況_其他說明	";
                    drTitle["DTSWC067"] = "滯洪沉砂設施	";
                    drTitle["DTSWC068"] = "滯洪沉砂設施數量	";
                    drTitle["DTSWC069"] = "滯洪量	";
                    drTitle["DTSWC070"] = "沉砂量	";
                    drTitle["DTSWC071"] = "排水設施	";
                    drTitle["DTSWC072"] = "排水設施數量	";
                    drTitle["DTSWC073"] = "擋土設施	";
                    drTitle["DTSWC074"] = "擋土設施數量	";
                    drTitle["DTSWC075"] = "植生工程	";
                    drTitle["DTSWC076"] = "設施維護檢查紀錄表	";
                    drTitle["DTSWC077"] = "已廢止已撤銷已失效不予受理不予核定	";
                    drTitle["DTSWC078"] = "已廢止已撤銷已失效不予受理不予核定日期	";
                    drTitle["DTSWC079"] = "備註	";
                    drTitle["DTSWC080"] = "核定本(上傳)	";
                    drTitle["DTSWC081"] = "違規編號	";
                    drTitle["DTSWC082"] = "開工期限	";
                    drTitle["DTSWC083"] = "開工展延次數	";
                    drTitle["DTSWC084"] = "停工期限	";
                    drTitle["DTSWC085"] = "承辦技師(e-mail)	";
                    drTitle["DTSWC086"] = "審查單位其他	";
                    drTitle["DTSWC087"] = "審查委員	";
                    drTitle["DTSWC088"] = "審查期限	";
                    drTitle["DTSWC089"] = "暫停審查期限	";
                    drTitle["DTSWC090"] = "監造技師Email	";
                    drTitle["DTSWC091"] = "工程進度	";
                    drTitle["DTSWC092"] = "停工展延次數	";
                    drTitle["DTSWC093"] = "維護管理人	";
                    drTitle["DTSWC094"] = "維護管理人地址	";
                    drTitle["DTSWC095"] = "維護管理人手機	";
                    drTitle["DTSWC096"] = "其他公開資訊說明1	";
                    drTitle["DTSWC097"] = "其他公開資訊說明2	";
                    drTitle["DTSWC098"] = "其他公開資訊說明3	";
                    drTitle["DTSWC099"] = "檢查單位其他	";
                    drTitle["DTSWC101"] = "竣工圖說	";
                    //drTitle["DTSWC102"] = "計畫申請書	";
                    //drTitle["DTSWC103"] = "退補件說明	";
                    //drTitle["DTSWC104"] = "第二次補正期限	";
                    //drTitle["DTSWC105"] = "第二次退補件說明	";
                    //drTitle["DTSWC106"] = "核備圖說變更	";
                    //drTitle["DTSWC107ID"] = "完工檢查公會(ID)	";
                    //drTitle["DTSWC107"] = "完工檢查公會	";


                    OBJ_GV.Rows.Add(drTitle);

                    ViewState["GV"] = OBJ_GV;
                }
                DataRow drDATA = OBJ_GV.NewRow();
                
                drDATA["DTSWC000"] = dSWC000;
                drDATA["DTSWC001"] = dSWC001;
                drDATA["DTSWC002"] = dSWC002;
                drDATA["DTSWC003"] = dSWC003;
                drDATA["DTSWC004"] = dSWC004;
                drDATA["DTSWC005"] = dSWC005;
                drDATA["DTSWC006"] = dSWC006;
                drDATA["DTSWC007"] = dSWC007;
                //drDATA["DTSWC008"] = dSWC008;
                //drDATA["DTSWC009"] = dSWC009;
                //drDATA["DTSWC010"] = dSWC010;
                //drDATA["DTSWC011"] = dSWC011;
                drDATA["DTSWC012"] = dSWC012;
                drDATA["DTSWC013ID"] = dSWC013ID;
                drDATA["DTSWC013TEL"] = dSWC013TEL;
                drDATA["DTSWC013"] = dSWC013;
                drDATA["DTSWC014"] = dSWC014;
                drDATA["DTSWC015"] = dSWC015;
                drDATA["DTSWC016"] = dSWC016;
                drDATA["DTSWC017"] = dSWC017;
                drDATA["DTSWC018"] = dSWC018;
                drDATA["DTSWC019"] = dSWC019;
                drDATA["DTSWC020"] = dSWC020;
                drDATA["DTSWC021ID"] = dSWC021ID;
                drDATA["DTSWC021"] = dSWC021;
                drDATA["DTSWC022ID"] = dSWC022ID;
                drDATA["DTSWC022"] = dSWC022;
                drDATA["DTSWC023"] = dSWC023;
                drDATA["DTSWC024ID"] = dSWC024ID;
                drDATA["DTSWC024"] = dSWC024;
                drDATA["DTSWC025"] = dSWC025;
                drDATA["DTSWC026"] = dSWC026;
                drDATA["DTSWC027"] = dSWC027; ;
                drDATA["DTSWC028"] = dSWC028;
                drDATA["DTSWC029"] = dSWC029;
                drDATA["DTSWC029CAD"] = dSWC029CAD;
                drDATA["DTSWC030"] = dSWC030;
                drDATA["DTSWC031"] = dSWC031;
                drDATA["DTSWC032"] = dSWC032;
                drDATA["DTSWC033"] = dSWC033;
                drDATA["DTSWC034"] = dSWC034;
                drDATA["DTSWC035"] = dSWC035;
                drDATA["DTSWC036"] = dSWC036;
                drDATA["DTSWC037"] = dSWC037;
                drDATA["DTSWC038"] = dSWC038;
                drDATA["DTSWC039"] = dSWC039;
                drDATA["DTSWC040"] = dSWC040;
                drDATA["DTSWC041"] = dSWC041;
                drDATA["DTSWC042"] = dSWC042;
                drDATA["DTSWC043"] = dSWC043;
                drDATA["DTSWC044"] = dSWC044;
                drDATA["DTSWC045ID"] = dSWC045ID;
                drDATA["DTSWC045"] = dSWC045;
                drDATA["DTSWC046"] = dSWC046;
                drDATA["DTSWC047"] = dSWC047;
                drDATA["DTSWC048"] = dSWC048;
                drDATA["DTSWC049"] = dSWC049;
                drDATA["DTSWC050"] = dSWC050;
                drDATA["DTSWC051"] = dSWC051;
                drDATA["DTSWC052"] = dSWC052;
                drDATA["DTSWC053"] = dSWC053;
                drDATA["DTSWC054"] = dSWC052;
                drDATA["DTSWC055"] = dSWC052;
                drDATA["DTSWC056"] = dSWC052;
                drDATA["DTSWC057"] = dSWC052;
                drDATA["DTSWC058"] = dSWC052;
                drDATA["DTSWC059"] = dSWC052;
                drDATA["DTSWC060"] = dSWC060;
                drDATA["DTSWC061"] = dSWC061;
                drDATA["DTSWC062"] = dSWC062;
                drDATA["DTSWC063"] = dSWC063;
                drDATA["DTSWC064"] = dSWC064;
                drDATA["DTSWC065"] = dSWC065;
                drDATA["DTSWC066"] = dSWC066;
                drDATA["DTSWC067"] = dSWC067;
                drDATA["DTSWC068"] = dSWC068;
                drDATA["DTSWC069"] = dSWC069;
                drDATA["DTSWC070"] = dSWC070;
                drDATA["DTSWC071"] = dSWC071;
                drDATA["DTSWC072"] = dSWC072;
                drDATA["DTSWC073"] = dSWC073;
                drDATA["DTSWC074"] = dSWC074;
                drDATA["DTSWC075"] = dSWC075;
                drDATA["DTSWC076"] = dSWC076;
                drDATA["DTSWC077"] = dSWC077;
                drDATA["DTSWC078"] = dSWC078;
                drDATA["DTSWC079"] = dSWC078;
                drDATA["DTSWC080"] = dSWC080;
                drDATA["DTSWC081"] = dSWC081;
                drDATA["DTSWC082"] = dSWC082;
                drDATA["DTSWC083"] = dSWC083;
                drDATA["DTSWC084"] = dSWC084;
                drDATA["DTSWC085"] = dSWC085;
                drDATA["DTSWC086"] = dSWC086;
                drDATA["DTSWC087"] = dSWC087;
                drDATA["DTSWC088"] = dSWC088;
                drDATA["DTSWC089"] = dSWC089;
                drDATA["DTSWC090"] = dSWC090;
                drDATA["DTSWC091"] = dSWC091;
                drDATA["DTSWC092"] = dSWC092;
                drDATA["DTSWC093"] = dSWC092;
                drDATA["DTSWC094"] = dSWC094;
                drDATA["DTSWC095"] = dSWC095;
                drDATA["DTSWC096"] = dSWC096;
                drDATA["DTSWC097"] = dSWC097;
                drDATA["DTSWC098"] = dSWC098;
                drDATA["DTSWC099"] = dSWC099;
                drDATA["DTSWC101"] = dSWC101;
                //drDATA["DTSWC102"] = dSWC102;
                //drDATA["DTSWC103"] = dSWC013;
                //drDATA["DTSWC104"] = dSWC104;
                //drDATA["DTSWC105"] = dSWC105;
                //drDATA["DTSWC106"] = dSWC106;
                //drDATA["DTSWC107ID"] = dSWC107ID;
                //drDATA["DTSWC107"] = dSWC107;

                OBJ_GV.Rows.Add(drDATA);

                ViewState["GV"] = OBJ_GV;
                
            }
            
            DataTable OBJ_GVSWC = (DataTable)ViewState["GV"];

            DataSet ds = new DataSet();

            if (OBJ_GVSWC != null)
            {
                //DataTable 加入 dataSet裡面
                ds.Tables.Add(OBJ_GVSWC);

                WriteOdsFile(ds, outputFilePath);
                Response.Redirect("..\\UpLoadFiles\\temp\\" + tempFilseName);
            }
            else
            {
                tempFilseName = "水保申請案件.ods";
                Response.Redirect("..\\UpLoadFiles\\temp\\" + tempFilseName);
            }
        }

    }
    public void WriteOdsFile(DataSet odsFile, string outputFilePath)
    {
        string tFilePath = ConfigurationManager.AppSettings["SwcSysFilePath"] + "template.ods";

        ZipFile templateFile = this.GetZipFile(tFilePath);

        XmlDocument contentXml = this.GetContentXmlFile(templateFile);

        XmlNamespaceManager nmsManager = this.InitializeXmlNamespaceManager(contentXml);

        XmlNode sheetsRootNode = this.GetSheetsRootNodeAndRemoveChildrens(contentXml, nmsManager);


        foreach (DataTable sheet in odsFile.Tables)
            this.SaveSheet(sheet, sheetsRootNode);

        this.SaveContentXml(templateFile, contentXml);

        templateFile.Save(outputFilePath);
    }
    private void SaveSheet(DataTable sheet, XmlNode sheetsRootNode)
    {
        XmlDocument ownerDocument = sheetsRootNode.OwnerDocument;

        XmlNode sheetNode = ownerDocument.CreateElement("table:table", this.GetNamespaceUri("table"));

        XmlAttribute sheetName = ownerDocument.CreateAttribute("table:name", this.GetNamespaceUri("table"));
        sheetName.Value = sheet.TableName;
        sheetNode.Attributes.Append(sheetName);

        this.SaveColumnDefinition(sheet, sheetNode, ownerDocument);

        this.SaveRows(sheet, sheetNode, ownerDocument);

        sheetsRootNode.AppendChild(sheetNode);
    }
    private void SaveRows(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
    {
        DataRowCollection rows = sheet.Rows;
        for (int i = 0; i < rows.Count; i++)
        {
            XmlNode rowNode = ownerDocument.CreateElement("table:table-row", this.GetNamespaceUri("table"));

            this.SaveCell(rows[i], rowNode, ownerDocument);

            sheetNode.AppendChild(rowNode);
        }
    }

    private void SaveCell(DataRow row, XmlNode rowNode, XmlDocument ownerDocument)
    {
        object[] cells = row.ItemArray;

        for (int i = 0; i < cells.Length; i++)
        {
            XmlElement cellNode = ownerDocument.CreateElement("table:table-cell", this.GetNamespaceUri("table"));

            if (row[i] != DBNull.Value)
            {
                // We save values as text (string)
                XmlAttribute valueType = ownerDocument.CreateAttribute("office:value-type", this.GetNamespaceUri("office"));
                valueType.Value = "string";
                cellNode.Attributes.Append(valueType);

                XmlElement cellValue = ownerDocument.CreateElement("text:p", this.GetNamespaceUri("text"));
                cellValue.InnerText = row[i].ToString();
                cellNode.AppendChild(cellValue);
            }

            rowNode.AppendChild(cellNode);
        }
    }
    private void SaveColumnDefinition(DataTable sheet, XmlNode sheetNode, XmlDocument ownerDocument)
    {
        XmlNode columnDefinition = ownerDocument.CreateElement("table:table-column", this.GetNamespaceUri("table"));

        XmlAttribute columnsCount = ownerDocument.CreateAttribute("table:number-columns-repeated", this.GetNamespaceUri("table"));
        columnsCount.Value = sheet.Columns.Count.ToString(CultureInfo.InvariantCulture);
        columnDefinition.Attributes.Append(columnsCount);

        sheetNode.AppendChild(columnDefinition);
    }
    private void SaveContentXml(ZipFile templateFile, XmlDocument contentXml)
    {
        templateFile.RemoveEntry("content.xml");

        MemoryStream memStream = new MemoryStream();
        contentXml.Save(memStream);
        memStream.Seek(0, SeekOrigin.Begin);

        templateFile.AddEntry("content.xml", memStream);
    }

    private string GetNamespaceUri(string prefix)
    {
        for (int i = 0; i < namespaces.GetLength(0); i++)
        {
            if (namespaces[i, 0] == prefix)
                return namespaces[i, 1];
        }

        throw new InvalidOperationException("Can't find that namespace URI");
    }
    // In ODF sheet is stored in table:table node
    private XmlNodeList GetTableNodes(XmlDocument contentXmlDocument, XmlNamespaceManager nmsManager)
    {
        return contentXmlDocument.SelectNodes("/office:document-content/office:body/office:spreadsheet/table:table", nmsManager);
    }
    private XmlNode GetSheetsRootNodeAndRemoveChildrens(XmlDocument contentXml, XmlNamespaceManager nmsManager)
    {
        XmlNodeList tableNodes = this.GetTableNodes(contentXml, nmsManager);

        XmlNode sheetsRootNode = tableNodes.Item(0).ParentNode;
        // remove sheets from template file
        foreach (XmlNode tableNode in tableNodes)
            sheetsRootNode.RemoveChild(tableNode);

        return sheetsRootNode;
    }
    // Read zip file (.ods file is zip file).
    private ZipFile GetZipFile(string inputFilePath)
    {
        return ZipFile.Read(inputFilePath);
    }
    private XmlDocument GetContentXmlFile(ZipFile zipFile)
    {
        // Get file(in zip archive) that contains data ("content.xml").
        ZipEntry contentZipEntry = zipFile["content.xml"];

        // Extract that file to MemoryStream.
        Stream contentStream = new MemoryStream();
        contentZipEntry.Extract(contentStream);
        contentStream.Seek(0, SeekOrigin.Begin);

        // Create XmlDocument from MemoryStream (MemoryStream contains content.xml).
        XmlDocument contentXml = new XmlDocument();
        contentXml.Load(contentStream);

        return contentXml;
    }
    private XmlNamespaceManager InitializeXmlNamespaceManager(XmlDocument xmlDocument)
    {
        XmlNamespaceManager nmsManager = new XmlNamespaceManager(xmlDocument.NameTable);

        for (int i = 0; i < namespaces.GetLength(0); i++)
            nmsManager.AddNamespace(namespaces[i, 0], namespaces[i, 1]);

        return nmsManager;
    }
    private static string[,] namespaces = new string[,]
     {
            {"table", "urn:oasis:names:tc:opendocument:xmlns:table:1.0"},
            {"office", "urn:oasis:names:tc:opendocument:xmlns:office:1.0"},
            {"style", "urn:oasis:names:tc:opendocument:xmlns:style:1.0"},
            {"text", "urn:oasis:names:tc:opendocument:xmlns:text:1.0"},
            {"draw", "urn:oasis:names:tc:opendocument:xmlns:drawing:1.0"},
            {"fo", "urn:oasis:names:tc:opendocument:xmlns:xsl-fo-compatible:1.0"},
            {"dc", "http://purl.org/dc/elements/1.1/"},
            {"meta", "urn:oasis:names:tc:opendocument:xmlns:meta:1.0"},
            {"number", "urn:oasis:names:tc:opendocument:xmlns:datastyle:1.0"},
            {"presentation", "urn:oasis:names:tc:opendocument:xmlns:presentation:1.0"},
            {"svg", "urn:oasis:names:tc:opendocument:xmlns:svg-compatible:1.0"},
            {"chart", "urn:oasis:names:tc:opendocument:xmlns:chart:1.0"},
            {"dr3d", "urn:oasis:names:tc:opendocument:xmlns:dr3d:1.0"},
            {"math", "http://www.w3.org/1998/Math/MathML"},
            {"form", "urn:oasis:names:tc:opendocument:xmlns:form:1.0"},
            {"script", "urn:oasis:names:tc:opendocument:xmlns:script:1.0"},
            {"ooo", "http://openoffice.org/2004/office"},
            {"ooow", "http://openoffice.org/2004/writer"},
            {"oooc", "http://openoffice.org/2004/calc"},
            {"dom", "http://www.w3.org/2001/xml-events"},
            {"xforms", "http://www.w3.org/2002/xforms"},
            {"xsd", "http://www.w3.org/2001/XMLSchema"},
            {"xsi", "http://www.w3.org/2001/XMLSchema-instance"},
            {"rpt", "http://openoffice.org/2005/report"},
            {"of", "urn:oasis:names:tc:opendocument:xmlns:of:1.2"},
            {"rdfa", "http://docs.oasis-open.org/opendocument/meta/rdfa#"},
            {"config", "urn:oasis:names:tc:opendocument:xmlns:config:1.0"}
     };




    

    protected void SqlDataSourceReq_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }
    protected void GVSWCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string jAct = e.CommandName;

        switch (jAct)
        {
            case "detail":
                int aa = GVSWCList.Rows[Convert.ToInt32(e.CommandArgument)].RowIndex;
                string jKeyValue = GVSWCList.Rows[aa].Cells[1].Text;
                Response.Redirect("SWC003.aspx?SWCNO=" + jKeyValue);
                break;


        }
    }
    protected void GVReqList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string jAct = e.CommandName;

        switch (jAct)
        {
            case "detail":
                int aa = GVReqList.Rows[Convert.ToInt32(e.CommandArgument)].RowIndex;
                string jKeyValue = GVReqList.Rows[aa].Cells[1].Text;
                Response.Redirect("SWC004.aspx?SWCNO=" + jKeyValue);
                break;
        }
    }
    protected void DDLChange_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["UserType"] = DDLChange.SelectedItem.Text=="技師"?"02":"04";
        Response.Redirect("SWC001.aspx");
    }
	
	protected void BTNSHTYPE_Click(object sender, EventArgs e)
    {
        Response.Redirect("LIST024.aspx");
    }
}