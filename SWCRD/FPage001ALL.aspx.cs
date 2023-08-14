using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SWCRD_FPage001ALL : System.Web.UI.Page
{
    string pubDDLUID = "";
    string pubDDLUNAME = "";
    string optojs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        //if (ssUserName == "" || ssUserType != "03") { Response.Write("<script>alert('很抱歉，您尚未開通此功能權限，如有疑問請洽承辦單位或系統管理員聯絡洽詢。');location.href='../SWCDOC/SWC001.aspx';</script>"); }

        if (!IsPostBack)
        {
            getGV2Date();
            Label2.Text = "";
        }
		TextUserName.Text = "您好";
        #region 全區供用
        SBApp.ViewRecord("設定檢查公會技師", "update", "");
        #endregion 全區供用
    }

    private void GenerateDropDownList()
    {
        #region DropDownList value
        GetEtuserList();
        string[] aID = pubDDLUID.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] aName = pubDDLUNAME.Split(new string[] { ";;" }, StringSplitOptions.None);
        DropDownList[] aDropDownList = new DropDownList[] { DDL301, DDL302, DDL303, DDL304, DDL305, DDL306, DDL307, DDL308, DDL309 };
        for (int dd = 0; dd < aDropDownList.Length; dd++)
        {
            aDropDownList[dd].Items.Clear();
            aDropDownList[dd].Items.Add(new ListItem("請選擇委員名單", ""));
            for (int te = 0; te < aID.Length; te++)
            {
                if (aID[te].Trim() != "")
                    aDropDownList[dd].Items.Add(new ListItem(aName[te].ToString(), aID[te].ToString()));
            }
        }
        #endregion
    }

    private void GetEtuserList()
    {
        pubDDLUID = "";
        pubDDLUNAME = "";
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string serviceGuild1 = ssUserGuild == "ge-50702" ? "OR ISNULL(ServiceSubstitute,'')= 'Y'" : "";
        string tmpSql = " select * from TCGESWC.dbo.ETUsers Where((ISNULL(GuildSubstitute,'') = '" + ssUserGuild + "' and GuildTcgeChk = '1')" + serviceGuild1 + " ) AND STATUS = '已開通'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = tmpSql;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tETID = readerTslm["ETID"] + "";
                            string tETName = readerTslm["ETName"] + "";

                            pubDDLUID += tETID + ";;";
                            pubDDLUNAME += tETName + ";;";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
    }

    private void getGV2Date()
    {
		optojs = "";
		Session["optojs"] = "";
        GenerateDropDownList();
        RemoveSelSession();
        string pageQ001 = TBQ001.Text;
        string pageQ002 = TBQ002.Text;
        string pageQ003a = TBQ003a.Text;
        string pageQ003b = TBQ003b.Text;
        Session["PageSearch001"] = pageQ001;
        Session["PageSearch002"] = pageQ002;
        Session["PageSearch003a"] = pageQ003a;
        Session["PageSearch003b"] = pageQ003b;

        string pageSearchSql = "";
        if (pageQ001.Trim() != "") pageSearchSql += " AND SWC02 like N'%" + pageQ001 + "%' ";
        if (pageQ002.Trim() != "") pageSearchSql += " AND SWC05 like N'%" + pageQ002 + "%' ";
        if (pageQ003a.Trim() != "") pageSearchSql += " AND SWC113 >= '" + pageQ003a + "' ";
        if (pageQ003b.Trim() != "") pageSearchSql += " AND SWC113 <= '" + pageQ003b + "' ";

        GBClass001 CLS = new GBClass001();
        string ssUserGuild = Session["ETU_Guild01"] + "";
        DataTable OBJ_GV02 = null;
        DataTable DTGV02 = new DataTable();
        DTGV02.Columns.Add(new DataColumn("SWC000", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC002", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC005", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC012", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC013TEL", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC021ID", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC022", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC022ID", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC025", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC088", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC108", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC113", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC113H", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC113M", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC113_2", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC113_2H", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC113_2M", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("CHKUS", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("CHKUSID", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("CHKSET", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC120", typeof(string)));
        ViewState["S7GV2"] = DTGV02;
        OBJ_GV02 = DTGV02;

        string myGV2Data = " select A.*,E.LAND001 from swcswc A left join (select SWC000,MIN(LAND000) as LAND000 from TCGESWC.dbo.SWCLAND group by SWC000) D ON A.SWC00 = D.SWC000 left join (select SWC000,LAND000,LAND001 as LAND001 from TCGESWC.dbo.SWCLAND) E ON A.SWC00 = E.SWC000 and D.LAND000 = E.LAND000 where SWC04='審查中' " + pageSearchSql + " order by SWC00 desc; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            SqlDataReader readerItem02;
            SqlCommand objCmdItem02 = new SqlCommand(myGV2Data, TslmConn);
            readerItem02 = objCmdItem02.ExecuteReader();

			int i = 0;

            //if (readerItem01.HasRows) { } else { Gridpanel.Visible = false; }
            while (readerItem02.Read())
            {
                string tmpSWC000 = readerItem02["SWC00"] + "";
                DataRow dr01 = OBJ_GV02.NewRow();
                dr01["SWC000"] = tmpSWC000;
                dr01["SWC002"] = readerItem02["SWC02"] + "";
                dr01["SWC005"] = readerItem02["SWC05"] + "";
                dr01["SWC012"] = readerItem02["SWC12"] + "";
                dr01["SWC021ID"] = readerItem02["SWC021ID"] + "";
                dr01["SWC022"] = readerItem02["SWC22"] + "";
                dr01["SWC022ID"] = readerItem02["SWC022ID"] + "";
                dr01["SWC025"] = readerItem02["SWC25"] + "";
                dr01["SWC088"] = CLS.DateView(readerItem02["SWC88"] + "", "00");
                dr01["SWC108"] = readerItem02["SWC108"] + "";
                dr01["SWC113"] = CLS.DateView(readerItem02["SWC113"] + "", "00");
                dr01["SWC113H"] = CLS.DateView(readerItem02["SWC113"] + "", "06");
                dr01["SWC113M"] = CLS.DateView(readerItem02["SWC113"] + "", "07");
                dr01["SWC113_2"] = CLS.DateView(readerItem02["SWC113_2"] + "", "00");
                dr01["SWC113_2H"] = CLS.DateView(readerItem02["SWC113_2"] + "", "06");
                dr01["SWC113_2M"] = CLS.DateView(readerItem02["SWC113_2"] + "", "07");
                dr01["SWC120"] = readerItem02["SWC120"] + "";

                //書件;起;迄;義務人;案件編號;地點;地區;承辦技師;審查公會
				if(CLS.DateView(readerItem02["SWC113"] + "","00") !="" && CLS.DateView(readerItem02["SWC113_2"] + "","00") != ""){
					optojs += readerItem02["SWC05"].ToString().Trim() + "|" + CLS.DateView(readerItem02["SWC113"] + "","00") + " " + CLS.DateView(readerItem02["SWC113"] + "","06") + ":" + CLS.DateView(readerItem02["SWC113"] + "", "07") + "|" + CLS.DateView(readerItem02["SWC113_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC113_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC113_2"] + "", "07") + "|" + readerItem02["SWC13"] + "|" + tmpSWC000 + "|" + readerItem02["SWC120"] + "|" + readerItem02["LAND001"] + "|" + readerItem02["SWC21"] + "|" + readerItem02["SWC22"] + "/";
				}
				
                string tmpCHKUSID = "", tmpCHKUS = "", tmpCHKSET = "";
                string tmpSqlx = " select * from tcgeswc.dbo.GuildGroup where RGType = 'S3' and SWC000='" + tmpSWC000 + "' order by convert(float,RGSID) ;";
                using (SqlConnection TslmConn2 = new SqlConnection(connectionString.ConnectionString))
                {
                    TslmConn2.Open();
                    using (var cmd = TslmConn2.CreateCommand())
                    {
                        cmd.CommandText = tmpSqlx;
                        cmd.ExecuteNonQuery();

                        using (SqlDataReader readerTslm = cmd.ExecuteReader())
                        {
                            if (readerTslm.HasRows)
                                while (readerTslm.Read())
                                {
                                    string tmpCHGType = readerTslm["CHGType"] + "";
                                    string tmpETID = readerTslm["ETID"] + "";

                                    if (tmpCHGType == "1") { tmpCHKUS = CLS.GetETUser(tmpETID, "Name"); tmpCHKUSID = tmpETID; } else { tmpCHKSET += tmpCHKSET.Trim() == "" ? tmpETID : ";;" + tmpETID; }
                                }
                            readerTslm.Close();
                        }
                        cmd.Cancel();
                    }

                }
                dr01["CHKUSID"] = tmpCHKUSID;
                dr01["CHKUS"] = tmpCHKUS;
                dr01["CHKSET"] = tmpCHKSET;

                OBJ_GV02.Rows.Add(dr01);
                ViewState["S7GV2"] = OBJ_GV02;
            }
        }
		Session["optojs"] = optojs.Replace(System.Environment.NewLine,"");
		ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "calendar_test1", "<script>c('" + Session["optojs"] + "');</script>");
        GridView2.DataSource = OBJ_GV02;
        GridView2.DataBind();
    }

    private void RemoveSelSession()
    {
        Session["PageSearch001"] = "";
        Session["PageSearch002"] = "";
        Session["PageSearch003a"] = "";
        Session["PageSearch003b"] = "";
    }

    public SqlDataReader DLfrom1()  //這裡是用「public SqlDataReader」
    {
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string serviceGuild1 = ssUserGuild == "ge-50702" ? "OR ISNULL(ServiceSubstitute,'')= 'Y'" : "";

        string ds = WebConfigurationManager.ConnectionStrings["SWCConnStr"].ConnectionString;
        SqlConnection conn = new SqlConnection(ds);
        SqlDataReader dr = null;
        string dc = " select* from ETUsers Where((ISNULL(GuildSubstitute,'') = '" + ssUserGuild + "' and GuildTcgeChk = '1')" + serviceGuild1 + " ) AND STATUS = '已開通'; ";
        SqlCommand cmd = new SqlCommand(dc, conn);
        conn.Open();
        dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);//加上CommandBehavior.CloseConnection自動關閉連線，就能安心return
        dr.Read();
        return dr;
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                break;
            case DataControlRowType.DataRow:
                HiddenField HDFD = (HiddenField)e.Row.Cells[0].FindControl("DDLValue");
                DropDownList DDLList = (DropDownList)e.Row.Cells[0].FindControl("DropDownList1");
                string[] aID = pubDDLUID.Split(new string[] { ";;" }, StringSplitOptions.None);
                string[] aName = pubDDLUNAME.Split(new string[] { ";;" }, StringSplitOptions.None);

                DDLList.Items.Clear();
                DDLList.Items.Add("");
                for (int te = 0; te < aID.Length; te++)
                {
                    if (aID[te].Trim() != "")
                        DDLList.Items.Add(new ListItem(aName[te].ToString(), aID[te].ToString()));
                }
                DDLList.Text = HDFD.Value;


				HiddenField HFHour = (HiddenField)e.Row.Cells[0].FindControl("HFHour");
				HiddenField HFHour_2 = (HiddenField)e.Row.Cells[0].FindControl("HFHour_2");
				
				HiddenField HFMinute = (HiddenField)e.Row.Cells[0].FindControl("HFMinute");
				HiddenField HFMinute_2 = (HiddenField)e.Row.Cells[0].FindControl("HFMinute_2");
				
				TextBox SWC113_NEW = (TextBox)e.Row.Cells[0].FindControl("SWC113_NEW");
				TextBox SWC113_2NEW = (TextBox)e.Row.Cells[0].FindControl("SWC113_2NEW");
				HiddenField HFDate = (HiddenField)e.Row.Cells[0].FindControl("HFDate");
				HiddenField HFDate_2 = (HiddenField)e.Row.Cells[0].FindControl("HFDate_2");
				SWC113_NEW.Text = HFDate.Value + "T" + HFHour.Value + ":" + HFMinute.Value;
				SWC113_2NEW.Text = HFDate_2.Value + "T" + HFHour_2.Value + ":" + HFMinute_2.Value;
                break;
        }
    }
    protected void RowInfo_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView2.Rows[aaa].Cells[6].FindControl("RSWC000");
        string tmpLink = "../SWCDOC/SWC003.aspx?SWCNO=" + hiddenDate01.Value;
        Response.Write("<script>window.open('" + tmpLink + "','_blank')</script>");
    }

    protected void ShowPP_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView2.Rows[aaa].Cells[5].FindControl("HFPP");
        HiddenField hiddenDate02 = (HiddenField)GridView2.Rows[aaa].Cells[6].FindControl("RSWC000");
        string uuu = hiddenDate01.Value;
        string[] auuu = uuu.Split(new string[] { ";;" }, StringSplitOptions.None);
        DropDownList[] aDropDownList = new DropDownList[] { DDL301, DDL302, DDL303, DDL304, DDL305, DDL306, DDL307, DDL308, DDL309 };
        for (int a = 0; a < aDropDownList.Length; a++)
        {
            aDropDownList[a].Text = "";
        }
        for (int a = 0; a < aDropDownList.Length; a++)
        {
            try { aDropDownList[a].Text = auuu[a]; } catch { }
        }
        LBSWC000.Text = hiddenDate02.Value;
        LBRowIndex.Text = aaa.ToString();
        Label2.Text = "55688";
    }

    protected void SaveRow_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView2.Rows[aaa].Cells[6].FindControl("RSWC000");
        string tmpSWC000 = hiddenDate01.Value;
        DropDownList DDL01 = (DropDownList)GridView2.Rows[aaa].Cells[5].FindControl("DropDownList1");
        HiddenField hiddenDate02 = (HiddenField)GridView2.Rows[aaa].Cells[4].FindControl("HDSWC021ID");
        HiddenField hiddenDate03 = (HiddenField)GridView2.Rows[aaa].Cells[4].FindControl("HDSWC022ID");
        HiddenField hiddenDate04 = (HiddenField)GridView2.Rows[aaa].Cells[4].FindControl("HDSWC025");
        HiddenField hiddenDate05 = (HiddenField)GridView2.Rows[aaa].Cells[4].FindControl("HDSWC108");
        HiddenField hiddenDate06 = (HiddenField)GridView2.Rows[aaa].Cells[4].FindControl("HDSWC013TEL");
        HiddenField hiddenDate07 = (HiddenField)GridView2.Rows[aaa].Cells[4].FindControl("HDSWC012");
        TextBox TB01 = (TextBox)GridView2.Rows[aaa].Cells[6].FindControl("SWC113_NEW");
        TextBox TB01_2 = (TextBox)GridView2.Rows[aaa].Cells[6].FindControl("SWC113_2NEW");
        CheckBox CBOX01 = (CheckBox)GridView2.Rows[aaa].Cells[6].FindControl("CheckBox1");
        TextBox TBSWC120 = (TextBox)GridView2.Rows[aaa].Cells[3].FindControl("TBSWC120");
		
        string tmpSWC002 = GridView2.Rows[aaa].Cells[0].Text;
        string tmpSWC005 = GridView2.Rows[aaa].Cells[1].Text;
        string tmpSWC012 = hiddenDate07.Value;
        string tmpSWC013TEL = hiddenDate06.Value;
        string tmpSWC021ID = hiddenDate02.Value;
        string tmpSWC022ID = hiddenDate03.Value;
        string tmpSWC025 = hiddenDate04.Value;
        string tmpSWC113 = TB01.Text.Replace("T"," ");
		string tmpSWC113_2 = TB01_2.Text.Replace("T"," ");
        string tmpSWC108 = hiddenDate05.Value;
        string tmpEtU01 = DDL01.SelectedItem.Value;
		string tmpSWC120 = TBSWC120.Text;

        string updSql = " update tslm2.dbo.swcswc set SWC113=@SWC113,SWC113_2=@SWC113_2,SWC120=@SWC120 where SWC00=@SWC000; ";
        updSql += " update tcgeswc.dbo.swccase set  SWC113=@SWC113,SWC113_2=@SWC113_2,SWC120=@SWC120 where SWC000=@SWC000; ";
        updSql += " Delete tcgeswc.dbo.GuildGroup Where SWC000=@SWC000 and RGType='S3' and RGSID='1'; ";
        if (tmpEtU01.Trim() != "") updSql += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES (@SWC000,@SWC002,'1',@ETUSER1,'S3','1','" + ssUserID + "',getdate());";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = updSql;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC002", tmpSWC002));
                cmd.Parameters.Add(new SqlParameter("@SWC113", tmpSWC113));
                cmd.Parameters.Add(new SqlParameter("@SWC113_2", tmpSWC113_2));
                cmd.Parameters.Add(new SqlParameter("@ETUSER1", tmpEtU01));
                cmd.Parameters.Add(new SqlParameter("@SWC120", tmpSWC120));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        if (CBOX01.Checked)
        {
            string tMailSub = "", tMailText = "", SentMailGroup = "";
            string tGuildName = ssUserName;
            //若狀態為「審查中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「審查會議時間」	
            //股長、管理者、承辦人員(SWC25)、承辦技師、審查委員(召集人、委員)、公會、義務人 
            tMailSub = "提醒您，【" + tGuildName + "】已訂於 " + tmpSWC113 + " ~ " + tmpSWC113_2 + " 舉辦【" + tmpSWC005 + "】審查會議。";
            tMailText = "提醒您，【" + tGuildName + "】已訂於 " + tmpSWC113 + " ~ " + tmpSWC113_2 + " 舉辦【" + tmpSWC005 + "】審查會議。";
            tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
            tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

            string exeSQLSTR2 = " select ETEmail as EMAIL from GuildGroup G Left Join ETUsers E On G.ETID = E.ETID where G.swc000 = '" + tmpSWC000 + "' and((ETEmail is not null and RGType = 'S3') or e.etid = '" + tmpSWC021ID + "') ";
            exeSQLSTR2 += "UNION ALL select email as EMAIL from tslm2.dbo.geouser g where ((department = '審查管理科' and ((jobtitle = '股長' and Tcgearea01 like '%" + tmpSWC012 + "%') or mbgroup02 = '系統管理員' or[name] = '" + tmpSWC025 + "')) or(userid = '" + tmpSWC022ID + "')) and status <> '停用' ";

            #region.名單
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                SqlDataReader readerItemS;
                SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR2, SwcConn);
                readerItemS = objCmdItemS.ExecuteReader();

                while (readerItemS.Read())
                {
                    string tEMail = readerItemS["EMAIL"] + "";
                    SentMailGroup += ";;" + tEMail;
                }
                readerItemS.Close();
                objCmdItemS.Dispose();
            }
            SentMailGroup += ";;" + tmpSWC108;
            string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
            #endregion

            bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub, tMailText);
            SBApp.SendSMS(tmpSWC013TEL, tMailSub);
        }
		Response.Redirect("FPage001.aspx");
    }

    protected void SaveGT_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        string tmpSWC000 = LBSWC000.Text + "";
        string tmpSWC002 = LBSWC002.Text + "";
        string tmpRowIndex = LBRowIndex.Text + "";
        string tmpCHKSET = "";
        if (tmpSWC000.Trim() != "")
        {
            DropDownList[] aDropDownList = new DropDownList[] { DDL301, DDL302, DDL303, DDL304, DDL305, DDL306, DDL307, DDL308, DDL309 };
            string exeSqlStr = " Delete GuildGroup Where SWC000 = '" + tmpSWC000 + "' AND RGSID<>'1' and RGSID in ('2','3','4','5','6','7','8','9','10'); ";
            for (int i = 0; i < aDropDownList.Length; i++)
            {

                DropDownList DDLTMP = aDropDownList[i];
                string selETID = DDLTMP.SelectedItem.Value;
                string tmpType = "S3";
                string tmpType2 = "0";
                exeSqlStr += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES ('" + tmpSWC000 + "','" + tmpSWC002 + "','" + (i + 2).ToString() + "','" + selETID + "','" + tmpType + "','" + tmpType2 + "','" + ssUserID + "',getdate());";
                tmpCHKSET += selETID + ";;";
            }
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();
                SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
            HiddenField HDFD1 = (HiddenField)GridView2.Rows[Convert.ToInt32(tmpRowIndex)].Cells[6].FindControl("HFPP");
            Label Label1 = (Label)GridView2.Rows[Convert.ToInt32(tmpRowIndex)].Cells[6].FindControl("Label1");
            HDFD1.Value = tmpCHKSET;
        }
        Label2.Text = "";
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        getGV2Date();
    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        TBQ001.Text = "";
        TBQ002.Text = "";
        TBQ003a.Text = "";
        TBQ003b.Text = "";
        RemoveSelSession();
        getGV2Date();
    }
    #region Sort
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        GetEtuserList();
        string sPage = e.SortExpression;
        string tmpSort = ViewState["SortFP001" + sPage] + "";
        string thisSort = "";
        switch (tmpSort)
        {
            case "":
            case "Asc":
                thisSort = "Desc"; ViewState["SortFP001" + sPage] = thisSort;
                break;
            case "Desc":
                thisSort = "Asc"; ViewState["SortFP001" + sPage] = thisSort;
                break;
        }
        DataTable OJB_GV = (DataTable)ViewState["S7GV2"];
        OJB_GV.DefaultView.Sort = sPage + " " + thisSort;
        GridView2.DataSource = OJB_GV;
        GridView2.DataBind();
    }
    #endregion
}