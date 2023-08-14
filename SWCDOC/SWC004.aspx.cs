using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWC003 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
	
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

        GBClass001 SBApp = new GBClass001();

        if (rSWCNO == "" || ssUserType == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        if (!IsPostBack) {
            SetSwcCase(rSWCNO);
        }
        LockArea(ssUserType);

        //全區供用

        SBApp.ViewRecord("完工後水土保持設施檢查", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
    }

    private void LockArea(string v)
    {
        string ssUserID = Session["ID"] + "";

        string tCaseStatus = LBSWC004.Text + "";
        
        GoTslm.Visible = false;
        TitleLink00.Visible = false;

        //申請表新增按鈕
        DTL_02_01_Link.Visible = false;
        SWCOLA201.Columns[5].Visible = false;
        DTL_02_02_Link.Visible = false;
        SWCOLA202.Columns[5].Visible = false;
        DTL_02_03_Link.Visible = false;
        DTL_02_03_1_Link.Visible = false;
        SWCOLA203.Columns[5].Visible = false;
        SWCOLA203_1.Columns[5].Visible = false;
        DTL_02_04_Link.Visible = false;
        DTL_02_04_1_Link.Visible = false;
        SWCOLA204.Columns[4].Visible = false;
        SWCOLA204_1.Columns[4].Visible = false;
        DTL_02_05_Link.Visible = false;
        SWCOLA205.Columns[4].Visible = false;
        DTL_02_06_Link.Visible = false;
        SWCOLA206.Columns[3].Visible = false;
        DTL_02_07_Link.Visible = false;
        SWCOLA207.Columns[5].Visible = false;
        DTL_02_08_Link.Visible = false;
        SWCOLA208.Columns[6].Visible = false;
        DTL_02_09_Link.Visible = false;
        SWCOLA209.Columns[5].Visible = false;

        if (ssUserID == "gv-admin")
        { }
        else
        {
            switch (v) {
                //case "01":    //水保義務人
                //    switch (tCaseStatus)
                //    {
                //        case "審查中":
                //            DTL_02_02_Link.Visible = true;
                //            break;
                //        case "已核定":
                //            DTL_02_04_Link.Visible = true;
                //            DTL_02_07_Link.Visible = true;
                //            break;
                //        case "施工中":
                //            DTL_02_03_Link.Visible = true;
                //            DTL_02_05_Link.Visible = true;
                //            DTL_02_08_Link.Visible = true;
                //            DTL_02_09_Link.Visible = true;
                //            break;
                //        case "已完工":
                //            DTL_02_01_Link.Visible = true;
                //            break;
                //    }
                //    DTL_02_06_Link.Visible = true;
                //    SWCOLA201.Columns[5].Visible = true;
                //    SWCOLA202.Columns[5].Visible = true;
                //    SWCOLA203.Columns[5].Visible = true;
                //    SWCOLA204.Columns[4].Visible = true;
                //    SWCOLA205.Columns[4].Visible = true;
                //    SWCOLA206.Columns[3].Visible = true;
                //    SWCOLA207.Columns[5].Visible = true;
                //    SWCOLA208.Columns[6].Visible = true;
                //    SWCOLA209.Columns[5].Visible = true;

                //    break;
                case "02":      //技師

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
                            DTL_02_03_1_Link.Visible = true;
                            DTL_02_04_1_Link.Visible = true;
                            DTL_02_05_Link.Visible = true;
							CopyCase.Visible = true;
                            break;
                        case "已完工":
                            DTL_02_01_Link.Visible = true;
                            break;
                    }
                    DTL_02_06_Link.Visible = true;
                    SWCOLA201.Columns[5].Visible = true;
                    SWCOLA202.Columns[5].Visible = true;
                    SWCOLA203.Columns[5].Visible = true;
                    SWCOLA203_1.Columns[5].Visible = true;
                    SWCOLA204.Columns[4].Visible = true;
                    SWCOLA204_1.Columns[4].Visible = true;
                    SWCOLA205.Columns[4].Visible = true;
                    SWCOLA206.Columns[3].Visible = true;
                    SWCOLA207.Columns[5].Visible = true;
                    SWCOLA208.Columns[6].Visible = true;
                    SWCOLA209.Columns[5].Visible = true;

                    break;
                case "03":  //大地只能看…
                    GoTslm.Visible = true;
                    break;
                    //case "04":    //公會
                    //    if (Session["Edit4"] + "" == "Y")  //完工檢查公會
                    //    {
                    //    }
                    //    break;
            }
        }
    }

    private void SetSwcCase(string v)
    {
        string ssUserType = Session["UserType"] + "";

        string SWC002 = "";

        GBClass001 SBApp = new GBClass001();

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = '"+v+"' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();
            SBApp.SWCRecord("SWC003",v, strSQLRV);

            while (readeSwc.Read())
            {
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
                string qSWC031 = readeSwc["SWC031"] + "";
                string qSWC032 = readeSwc["SWC032"] + "";
                string qSWC033 = readeSwc["SWC033"] + "";
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

                //丟資料
                LBSWC000.Text = qSWC000;
                LBSWC002.Text = qSWC002;
                LBSWC004.Text = qSWC004;
                LBSWC005.Text = qSWC005;
                

            }
        }
        
        
        
        //GVSWCCHG
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            string strSQLRV = "select * from SWCCASE";
            strSQLRV = strSQLRV + " Where LEFT(SWC002,12) = '" + LBSWC002.Text.Substring(0,12) + "' ";
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
                GVTBSWCDHKRow["SWC002Link"] = "SWC004.aspx?SWCNO=" + dSWC000;

                tbSWCCHK.Rows.Add(GVTBSWCDHKRow);

                //Store the DataTable in ViewState
                ViewState["SWCCHK"] = tbSWCCHK;

                GVSWCCHG.DataSource = tbSWCCHK;
                GVSWCCHG.DataBind();

            }
            readerItem.Close();
        }

        //2018-新增子表單
        GenOnlineApply(v);

    }

    private void GenOnlineApply(string v)
    {
        GBClass001 SBApp = new GBClass001();

        string ssUserType = Session["UserType"] + "";
        string v2 = LBSWC002.Text + "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];

        //表2
        using (SqlConnection OLAConn = new SqlConnection(connectionString.ConnectionString))
        {
            OLAConn.Open();

            string Sql201Str = "";
            Sql201Str = Sql201Str + " select ONA01001 as DATA01 ,left(convert(char, ONA01002, 120),10) as DATA02,left(convert(char, ONA01003, 120),10) as DATA03,ONA01004 as DATA04,DATALOCK as DATA05,replace(replace(isnull(DATALOCK,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply01 ";
            Sql201Str = Sql201Str + "  Where SWC002 = '" + v2 + "' ";
            Sql201Str = Sql201Str + "  order by ONA01001 ";

            string Sql202Str = "";
            Sql202Str = Sql202Str + " select ONA02001 as DATA01 ,left(convert(char, ONA02002, 120),10) as DATA02,left(convert(char, ONA02003, 120),10) as DATA03,ONA02004 as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply02 ";
            Sql202Str = Sql202Str + "  Where SWC000 = '" + v + "' ";
            Sql202Str = Sql202Str + "  order by ONA02001 ";

            string Sql203Str = "";
            Sql203Str = Sql203Str + " select ONA03001 as DATA01 ,left(convert(char, ONA03002, 120),10) as DATA02,left(convert(char, ONA03003, 120),10) as DATA03,'第'+ONA03004+'次展延' as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply03 ";
            Sql203Str = Sql203Str + "  Where SWC000 = '" + v + "' and ISNULL(ONA03011,'') != '復工' ";
            Sql203Str = Sql203Str + "  order by ONA03001 ";
			
			string Sql203Str_1 = "";
            Sql203Str_1 = Sql203Str_1 + " select ONA03001 as DATA01 ,left(convert(char, ONA03002, 120),10) as DATA02,left(convert(char, ONA03003, 120),10) as DATA03,'第'+ONA03004+'次展延' as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply03 ";
            Sql203Str_1 = Sql203Str_1 + "  Where SWC000 = '" + v + "' and ONA03011 = '復工' ";
            Sql203Str_1 = Sql203Str_1 + "  order by ONA03001 ";

            string Sql204Str = "";
            Sql204Str = Sql204Str + " select ONA04001 as DATA01 ,left(convert(char, ONA04003, 120),10) as DATA02,left(convert(char, ONA04004, 120),10) as DATA03,'' as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply04 ";
            Sql204Str = Sql204Str + "  Where SWC000 = '" + v + "' and ISNULL(ONA04023,'') != '復工' ";
            Sql204Str = Sql204Str + "  order by ONA04001 ";
			
			string Sql204Str_1 = "";
            Sql204Str_1 = Sql204Str_1 + " select ONA04001 as DATA01 ,left(convert(char, ONA04003, 120),10) as DATA02,left(convert(char, ONA04004, 120),10) as DATA03,'' as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply04 ";
            Sql204Str_1 = Sql204Str_1 + "  Where SWC000 = '" + v + "' and ONA04023 = '復工' ";
            Sql204Str_1 = Sql204Str_1 + "  order by ONA04001 ";

            string Sql205Str = "";
            Sql205Str = Sql205Str + " select ONA05001 as DATA01 ,ONA05002 as DATA02,ONA05003 as DATA03,ONA05004 as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply05 ";
            Sql205Str = Sql205Str + "  Where SWC000 = '" + v + "' ";
            Sql205Str = Sql205Str + "  order by ONA05001 ";

            string Sql206Str = "";
            Sql206Str = Sql206Str + " select ONA06001 as DATA01 ,left(convert(char, ONA06002, 120),10) as DATA02,ET.ETName as DATA03,ONA06004 as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply06 ONA6 ";
            Sql206Str = Sql206Str + "   LEFT JOIN ETUsers ET ON ONA6.ONA06015=et.etid ";
            Sql206Str = Sql206Str + "  Where SWC000 = '" + v + "' ";
            Sql206Str = Sql206Str + "  order by ONA06001 ";

            string Sql207Str = "";
            Sql207Str = Sql207Str + " select ONA07001 as DATA01 ,left(convert(char, ONA07002, 120),10) as DATA02,left(convert(char, ONA07003, 120),10) as DATA03,left(convert(char, ONA07004, 120),10) as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply07 ";
            Sql207Str = Sql207Str + "  Where SWC000 = '" + v + "' ";
            Sql207Str = Sql207Str + "  order by ONA07001 ";

            string Sql208Str = "";
            Sql208Str = Sql208Str + " select ONA08001 as DATA01 ,left(convert(char, ONA08002, 120),10) as DATA02,left(convert(char, ONA08003, 120),10) as DATA03,left(convert(char, ONA08004, 120),10) as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply08 ";
            Sql208Str = Sql208Str + "  Where SWC000 = '" + v + "' ";
            Sql208Str = Sql208Str + "  order by ONA08001 ";

            string Sql209Str = "";
            Sql209Str = Sql209Str + " select ONA09001 as DATA01 ,left(convert(char, ONA09002, 120),10) as DATA02,ET.ETName as DATA03,ONA09004 as DATA04,DATALOCK as DATA05,replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回') AS DATA06 from OnlineApply09 ONA9 ";
            Sql209Str = Sql209Str + "   LEFT JOIN ETUsers ET ON ONA9.saveuser=et.etid ";
            Sql209Str = Sql209Str + "  Where SWC000 = '" + v + "' ";
            Sql209Str = Sql209Str + "  order by ONA09001 ";

            string[] arraySqlExeStr = new string[] { Sql201Str, Sql202Str, Sql203Str, Sql203Str_1, Sql204Str, Sql204Str_1, Sql205Str, Sql206Str, Sql207Str, Sql208Str, Sql209Str };
            GridView[] arrayONLAGV = new GridView[] { SWCOLA201, SWCOLA202, SWCOLA203, SWCOLA203_1, SWCOLA204, SWCOLA204_1, SWCOLA205, SWCOLA206, SWCOLA207, SWCOLA208, SWCOLA209 };

            for (int i = 0; i < arraySqlExeStr.Length; i++)
            {
                string aSqlStr = arraySqlExeStr[i]+"";
                GridView SWCONLA = arrayONLAGV[i];

                if (aSqlStr.Trim() == "") { } else
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
            
        }
    }

    protected void GoListPage_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SWC001.aspx?SR=Y");
    }

    protected void EditCase_Click(object sender, ImageClickEventArgs e)
    {
        string sSWCNO = LBSWC000.Text+"";
        Response.Redirect("SWC002.aspx?CaseId=" + sSWCNO);
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
        string LINK = "SWCDT001v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);
    }
    protected void ButtonDTL0302_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;

        string GetDTLType = LButton.CommandArgument.Substring(0, 2);
        string GetDTLType2 = LButton.CommandArgument.Substring(0, 6);
        if (GetDTLType2== "SWCCHG") { GetDTLType = "RC"; }

        string LINK = "SWC001.aspx";
        switch (GetDTLType)
        {
            case "RC":
                LINK = "SWCDT003v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;
            case "RB":
                LINK = "SWCDT002v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;

        }
        Response.Redirect(LINK);

    }
    protected void ButtonDTL04_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;
        string LINK = "SWCDT004v.aspx?PV=4&SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL05_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;
        string LINK = "SWCDT005v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL06_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;
        string LINK = "SWCDT006v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL07_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Button LButton = (Button)sender;
        string LINK = "SWCDT007v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);
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

        Response.Redirect("OnlineApply002.aspx?SWCNO="+ rSWCNO+ "&OLANO=AddNew");
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

        Response.Redirect("OnlineApply002"+ rPageView + ".aspx?PV=4&SWCNO=" + rSWCNO + "&OLANO="+ rONLANO);

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

                if (tempLock == "Y")
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

                if (tempLock == "Y")
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

                if (tempLock == "Y")
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

        Response.Redirect("OnlineApply003" + rPageView + ".aspx?PV=4&SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

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

        HiddenField Lock01 = (HiddenField)SWCOLA203.Rows[rowindex].Cells[5].FindControl("Lock203_1");

        string rDataLock2 = Lock01.Value;
        if (rDataLock2 == "Y") { rPageView = "v"; }

        Response.Redirect("OnlineApply003" + rPageView + ".aspx?PV=4&SWCNO=" + rSWCNO + "&OLANO=" + rONLANO);

    }

    protected void DTL_02_04_Link_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";

        Response.Redirect("OnlineApply004.aspx?PV=4&SWCNO=" + rSWCNO + "&OLANO=AddNew");
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

                if (tempLock == "Y")
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

        Response.Redirect("OnlineApply004.aspx?PV=4&SWCNO=" + rSWCNO + "&OLANO=AddNew");
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

                if (tempLock == "Y")
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

    protected void SWCOLA205_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[4].FindControl("Lock205");
                string tempLock = Lock01.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL205");

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

                if (tempLock == "Y")
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

                if (tempLock == "Y")
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

                if (tempLock == "Y")
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

                if (tempLock == "Y")
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

        Response.Redirect("OnlineApply001" + rPageView + ".aspx?SWCNO=" + rSWCNO + "&OACode=" + rONLANO+"&UA="+ rSWCUA);

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

        HiddenField Lock01 = (HiddenField)SWCOLA205.Rows[rowindex].Cells[4].FindControl("Lock205");

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



    protected void ButtonDEL201_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tONA01001 = LButton.CommandArgument + "";

        string exeSqlStr = " delete OnlineApply01 where ONA09001 ='" + tONA01001 + "' ";

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
	protected void CopyCase_Click(object sender, EventArgs e)
    {
        string rCaseId = LBSWC000.Text + "";
        string rSwcNo = LBSWC002.Text + "";
        if (rSwcNo.Length > 12){
            if (rSwcNo.Substring(13, 1) == "9")
            {
                Response.Write("<script>alert('變更次數已達系統設計極限，請洽大地工程處，謝謝!!');</script>"); return;
            }
		}
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV = "select * from SWCLAND";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rCaseId + "' ";
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
            }
            readerItem.Close();
        }
		using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int ni = 0;

            string strSQLRV = "select * from SwcDocItem";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rCaseId + "' ";
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

            }
            readerItem.Close();
        }
		
        Session["CopyCaseIdD1"] = rCaseId;
        Session["CopyCaseIdD2"] = rSwcNo;
        Session["TempSwcCadastral"] = ViewState["SwcCadastral"];
        Session["TempSwcDocItem"] = ViewState["SwcDocItem"];

        string LINK = "SWC002.aspx?CaseId=COPY";
        Response.Redirect(LINK);
    }



}