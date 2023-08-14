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

public partial class SWCDOC_SWC003aa : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    DateTime sqlin;
    DateTime sqlout;
    TimeSpan sqlts;
    string sqleventname;
    int sqlsec;
    //string sqlSWCNO = "SWC20190906095300";
    //string sqlSWCNO = "SWC20190827141649";
	string sqlSWCNO = "SWC20191003105432";
    protected void Page_Load(object sender, EventArgs e)
    {
        sqlin = DateTime.Now;

        //測試用
        Session["NAME"] = "系統管理員";
        Session["UserType"] = "系統管理員";
        Session["JobTitle"] = "";

        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

        GBClass001 SBApp = new GBClass001();

        if (rSWCNO == "" || ssUserType == "")
            Response.Redirect("SWC001.aspx");

        if (!IsPostBack) {
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text=sqllog.Text + "\n\r" + "進入表單到準備寫入USERLOG" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;
            SBApp.ViewRecord("水保申請案件", "view", "");
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "寫入USERLOG的時間" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;
            SetSwcCase(rSWCNO);
        }
        LockArea(ssUserType);

        //全區供用
        sqlin = DateTime.Now;
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();
        sqlout = DateTime.Now;
        sqlts = sqlout - sqlin;
        sqllog.Text = sqllog.Text + "\n\r" + "抓使用者數量的時間" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
        sqlin = DateTime.Now;

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        //全區供用
    }

    private void LockArea(string v)
    {
        string ssUserID = Session["ID"] + "";
        string ssUserGuild = Session["WMGuild"] + "";

        string tCaseStatus = LBSWC004.Text + "";
        string tCaseType = LBSWC007.Text+"";
        string tSWC021ID = LBSWC021ID.Text + "";
        string tSWC045ID = LBSWC045ID.Text + "";

        string tSWC022ID = LBSWC022ID.Text + "";
        string tSWC024ID = LBSWC024ID.Text + "";
        string tSWC107ID = LBSWC107ID.Text + "";
        
        EditCase.Visible = false;
        GoTslm.Visible = false;
        TitleLink00.Visible = false;

        //申請表新增按鈕
        DTL_02_01_Link.Visible = false;
        SWCOLA201.Columns[5].Visible = false;
        DTL_02_02_Link.Visible = false;
        SWCOLA202.Columns[5].Visible = false;
        DTL_02_03_Link.Visible = false;
        SWCOLA203.Columns[6].Visible = false;
        DTL_02_04_Link.Visible = false;
        SWCOLA204.Columns[5].Visible = false;
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

        if (ssUserID == "gv-admin")
        { }
        else
        {
            Area02.Visible = false;

            switch (v) {
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
                            DTL_02_03_Link.Visible = true;
                            DTL_02_04_Link.Visible = true;
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
                    SWCOLA203.Columns[6].Visible = true;
                    SWCOLA204.Columns[5].Visible = true;
                    SWCOLA205.Columns[4].Visible = true;
                    SWCOLA206.Columns[4].Visible = true;
                    SWCOLA207.Columns[6].Visible = true;
                    SWCOLA208.Columns[6].Visible = true;
                    SWCOLA209.Columns[5].Visible = true;

                    break;
                case "02":
                    //承辦技師
                    if (tSWC021ID == ssUserID)
                    {
                        Area02.Visible = true;
                    }

                    //監告技師
                    if (tSWC045ID == ssUserID)
                    {

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
                            break;
                        case "已完工":
                            DTL_02_01_Link.Visible = true;
                            break;
                    }
                    DTL_02_06_Link.Visible = true;
                    SWCOLA201.Columns[5].Visible = true;
                    SWCOLA202.Columns[5].Visible = true;
                    SWCOLA203.Columns[6].Visible = true;
                    SWCOLA204.Columns[5].Visible = true;
                    SWCOLA205.Columns[4].Visible = true;
                    SWCOLA206.Columns[4].Visible = true;
                    SWCOLA207.Columns[6].Visible = true;
                    SWCOLA208.Columns[6].Visible = true;
                    SWCOLA209.Columns[5].Visible = true;

                    break;
                case "03":
                    //大地只能看…
                    Area02.Visible = true;

                    break;
                case "04":
                    Area03.Visible = false;
                    Area04.Visible = false;
                    Area05.Visible = false;

                    string tmpUserID = ssUserID == ssUserGuild ? ssUserID : ssUserGuild;
                    //審查公會
                    if (tSWC022ID == tmpUserID)
                    {
                        Area02.Visible = true;
                    }
                    if (Session["Edit4"] + "" == "Y")  //完工檢查公會
                    {
                        if (tCaseStatus == "已完工" || tCaseStatus == "撤銷" || tCaseStatus == "已變更")
                        {
                            EditCase.Visible = true;
                            Area03.Visible = true;
                            Area04.Visible = true;
                            Area05.Visible = true;
                        }
                    }
                    break;
            }
        }











            switch (v)
        {
            case "01":  //水保義務人
                if (tCaseType == "簡易水保")
                {
                    if (tCaseStatus == "退補件" || tCaseStatus == "受理中" || tCaseStatus == "申請中")
                    {
                        EditCase.Visible = true;
                    }
                }                
                break;

            case "02":  //技師
                TitleLink00.Visible = true;
                SWCDTL01.Visible = false;
                Area04.Visible = false;
                Area05.Visible = false;

                if (tSWC021ID == ssUserID)  //承辦技師
                {
                    SWCDTL01.Visible = true;
                    if (tCaseStatus == "退補件" || tCaseStatus == "受理中" || tCaseStatus == "申請中" || tCaseStatus == "審查中")
                    {
                        EditCase.Visible = true;
                    }

                }
                if (tSWC045ID == ssUserID)  //監告技師
                {
                    Area04.Visible = true;

                    if (tCaseStatus == "施工中")
                    {
                        EditCase.Visible = true;
                    }
                }

                break;

            case "03":  //大地只能看…
                GoTslm.Visible = true;
                EditCase.Visible = false;
                break;

            case "04":  //公會
                string tmpRGType = "";
                string tmpUserID = ssUserID == ssUserGuild ? ssUserID : ssUserGuild;

                if (tSWC022ID == tmpUserID) //審查公會
                {
                    tmpRGType = "S3";
                    Area03.Visible = true;
                    if (tCaseStatus == "審查中" || tCaseStatus == "暫停審查")
                    {
                        EditCase.Visible = true;
                    }
                }
                if (tSWC024ID == tmpUserID) //檢查公會
                {
                    tmpRGType = "S4";
                    Area03.Visible = true;
                    Area04.Visible = true;
                    if (tCaseStatus == "施工中")
                    {
                        SWCDTL01.Visible = false;

                        EditCase.Visible = true;
                        Area04.Visible = true;
                    }
                }
                //召集人才能改
                if (ssUserID == ssUserGuild) { } else {
                    EditCase.Visible = false;
                    string chkGuildType = " select * from GuildGroup Where ETID='"+ ssUserID + "' And RGType='"+ tmpRGType + "' and CHGType='1'; ";

                    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                    using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SWCConn.Open();

                        SqlDataReader readerData;
                        SqlCommand objCmdRV = new SqlCommand(chkGuildType, SWCConn);
                        readerData = objCmdRV.ExecuteReader();

                        while (readerData.Read())
                        {
                            EditCase.Visible = true;
                        }
                        readerData.Close();
                        objCmdRV.Dispose();
                    }
                }
                break;
        }

        if (ssUserID == "gv-admin")
        {
            EditCase.Visible = true;
            Area04.Visible = true;
            Area05.Visible = true;
        }
    }

    private void SetSwcCase(string v)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserType = Session["UserType"] + "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            sqlin = DateTime.Now;
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = '"+v+"' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓案件主表" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;
            SBApp.SWCRecord("SWC003",v, strSQLRV);
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓案件主表" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

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
                string qSWC112 = readeSwc["SWC112"] + "";
                string qSWC113 = readeSwc["SWC113"] + "";
                string qSWC114 = readeSwc["SWC114"] + "";
                string qSWC115 = readeSwc["SWC115"] + "";

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
                #endregion

                //丟資料
                LBSWC000.Text = qSWC000;
                LBSWC002.Text = qSWC002;
                LBSWC004.Text = qSWC004;
                LBSWC005.Text = qSWC005;
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
                LBSWC103.Text = qSWC103;
                LBSWC105.Text = qSWC105;
                LBSWC107.Text = qSWC107;
                LBSWC107ID.Text = qSWC107ID;
                LBSWC108.Text = qSWC108;

                OutLink1.NavigateUrl = "https://tcgegis.geonet.tw/?rid="+qSWC002;
                #region-
                string[] arrayDateValue = new string[] { qSWC104,qSWC109, qSWC112, qSWC113, qSWC114, qSWC115};
                Label[] arrayLBDate = new Label[] { LBSWC104,LBSWC109, LBSWC112, LBSWC113, LBSWC114, LBSWC115 };
                for (int i = 0; i < arrayLBDate.Length; i++) arrayLBDate[i].Text = SBApp.DateView(arrayDateValue[i], "00");
                #endregion

                #region-委員名單
                string tLBSAOID = "";
                int ii = 0;
                Label[] arryLB01 = new Label[] { LBSA01, LBSA02, LBSA03, LBSA04, LBSA05, LBSA06, LBSA07, LBSA08, LBSA09, LBSA10, LBSB01, LBSB02 };
                string exeSqlStr = " select E.ETName,E.ETID from GuildGroup G Left Join ETUsers E on G.ETID=E.ETID where G.swc000='" + v + "' order by convert(float,RGSID) ";
                sqlin = DateTime.Now;
                using (SqlConnection DDL01Conn = new SqlConnection(connectionString.ConnectionString))
                {
                    DDL01Conn.Open();
                    SqlDataReader readerItemGG;
                    SqlCommand objCmdItemGG = new SqlCommand(exeSqlStr, DDL01Conn);
                    readerItemGG = objCmdItemGG.ExecuteReader();
                    sqlout = DateTime.Now;
                    sqlts = sqlout - sqlin;
                    sqllog.Text = sqllog.Text + "\n\r" + "抓委員名單" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
                    sqlin = DateTime.Now;
                    while (readerItemGG.Read())
                    {
                        string tmpUserName = readerItemGG["ETName"] + "";
                        string tmpUserID = readerItemGG["ETID"] + "";
                        arryLB01[ii++].Text = tmpUserName.Trim();
                        tLBSAOID += tmpUserID + ";;";
                    }
                }
                LBSAOID.Text = tLBSAOID;
                #endregion

                if (qSWC061 == "1")
                {
                    if (qSWC062.Trim()=="") { qSWC062 = "0"; }
                    LBSWC061062.Text= "建物"+ qSWC062 + "戶 ";
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
                    if (qSWC069.Trim() != "") {
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
                    } else
                    {
                        LBSWC073074.Text = "，擋土設施 " + qSWC074 + " 道";
                    }
                    
                }
                if (qSWC075 == "1")
                {
                    LBSWC075.Text = "植生工程";
                }

                //檔案類處理
                string[] arrayFileNameLink = new string[] { qSWC029, qSWC029CAD, qSWC030, qSWC080, qSWC101, qSWC106, qSWC110, qSWC101CAD };
                System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link029, Link029CAD, Link030, Link080, Link101, Link106, Link110,Link101CAD };

                for (int i = 0; i < arrayFileNameLink.Length; i++)
                {
                    sqlin = DateTime.Now;
                    string strFileName = arrayFileNameLink[i];
                    System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];

                    FileLinkObj.Visible = false;
                    if (strFileName == "")
                    {
                    }
                    else
                    {
                        string extension = Path.GetExtension(strFileName).ToLowerInvariant();

                        string NewUpath = @"~\OutputFile\" + strFileName;
                        string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                        if (extension == ".pdf") if (SBApp.DLFileReMark(v, strFileName, "")) tempLinkPateh = NewUpath;

                        FileLinkObj.Text = strFileName;
                        FileLinkObj.NavigateUrl = tempLinkPateh;
                        FileLinkObj.Visible = true;
                    }
                    sqlout = DateTime.Now;
                    sqlts = sqlout - sqlin;
                    sqllog.Text = sqllog.Text + "\n\r" + "抓浮水印" + strFileName + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
                    sqlin = DateTime.Now;
                }

                switch (ssUserType)
                {
                    case "01":

                        break;
                }

            }
        }

        //表1
        sqlin = DateTime.Now;
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql01Str = "";

            Sql01Str = Sql01Str + " select SWC000,DTLA000,DTLA001,DTLA002,DTLA003,DTLA004 from SWCDTL01 ";
            Sql01Str = Sql01Str + "  Where SWC000 = '" + v + "' and isnull(DATALOCK,'')='Y' ";
            Sql01Str = Sql01Str + "  order by DTLA002 ";

            SqlDataReader readerItem01;
            SqlCommand objCmdItem01 = new SqlCommand(Sql01Str, DTLConn);
            readerItem01 = objCmdItem01.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓表1" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;
            while (readerItem01.Read())
            {
                string dDTLA01 = readerItem01["DTLA001"] + "";
                string dDTLA02 = readerItem01["DTLA002"] + "";
                string dDTLA03 = readerItem01["DTLA003"] + "";
                string dDTLA04 = readerItem01["DTLA004"] + "";

                DataTable OBJ_GV01 = (DataTable)ViewState["GV01"];
                DataTable DTGV01 = new DataTable();

                if (OBJ_GV01 == null)
                {
                    DTGV01.Columns.Add(new DataColumn("DTLA001", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA002", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA003", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA004", typeof(string)));

                    ViewState["GV01"] = DTGV01;
                    OBJ_GV01 = DTGV01;
                }
                DataRow dr01 = OBJ_GV01.NewRow();

                dr01["DTLA001"] = dDTLA01;
                dr01["DTLA002"] = dDTLA02;
                dr01["DTLA003"] = dDTLA03;
                dr01["DTLA004"] = dDTLA04;

                OBJ_GV01.Rows.Add(dr01);

                ViewState["GV01"] = OBJ_GV01;

                SWCDTL01.DataSource = OBJ_GV01;
                SWCDTL01.DataBind();
            }
        }

        //表3+2
        sqlin = DateTime.Now;
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql0302Str = "";

            Sql0302Str = Sql0302Str + " Select SWC000,DTLC000,[DTLC001] ,CONVERT(varchar(100), [DTLC002], 23) AS DTLC002,DTLC003,DTLC004  From SWCDTL03 Where SWC000 = '" + v + "' and isnull(DATALOCK,'')= 'Y' UNION ";
            Sql0302Str = Sql0302Str + " select swc000,DTLB000 AS DTLC000,DTLB001 AS [DTLC001] ,CONVERT(varchar(100), [DTLB002], 23) AS DTLC002,DTLB003 AS DTLC003,DTLB004 AS DTLC004 from SWCDTL02 ";
            Sql0302Str = Sql0302Str + "  Where SWC000 = '" + v + "'  and isnull(DATALOCK,'')='Y' ";
            Sql0302Str = Sql0302Str + "  order by DTLC002 ";

            SqlDataReader readerItem0302;
            SqlCommand objCmdItem0302 = new SqlCommand(Sql0302Str, DTLConn);
            readerItem0302 = objCmdItem0302.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓表3+2" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

            while (readerItem0302.Read())
            {
                string dDTLC01 = readerItem0302["DTLC001"] + "";
                string dDTLC02 = readerItem0302["DTLC002"] + "";
                string dDTLC03 = readerItem0302["DTLC003"] + "";
                string dDTLC04 = readerItem0302["DTLC004"] + "";

                DataTable OBJ_GV0302 = (DataTable)ViewState["GV0302"];
                DataTable DTGV0302 = new DataTable();

                if (OBJ_GV0302 == null)
                {
                    DTGV0302.Columns.Add(new DataColumn("DTLC001", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC002", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC003", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC004", typeof(string)));

                    ViewState["GV03"] = DTGV0302;
                    OBJ_GV0302 = DTGV0302;
                }
                DataRow dr0302 = OBJ_GV0302.NewRow();

                dr0302["DTLC001"] = dDTLC01;
                dr0302["DTLC002"] = dDTLC02;
                dr0302["DTLC003"] = dDTLC03;
                dr0302["DTLC004"] = dDTLC04;

                OBJ_GV0302.Rows.Add(dr0302);

                ViewState["GV0302"] = OBJ_GV0302;

                SWCDTL0302.DataSource = OBJ_GV0302;
                SWCDTL0302.DataBind();
            }
        }

        //表4
        sqlin = DateTime.Now;
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql04Str = "";

            Sql04Str = Sql04Str + " Select [DTLD001] ,CONVERT(varchar(100), [DTLD002], 23) AS DTLD002,ISNULL(D4.DTLD003,'')+ISNULL(ISNULL(DE.DENAME,DE2.DENAME),'') AS DTLD003,DTLD004,DATALOCK From SWCDTL04 D4 ";
            Sql04Str += " LEFT JOIN DisasterEvent DE ON D4.DTLD085=DE.DENo ";
            Sql04Str += " LEFT JOIN DisasterEvent DE2 ON D4.DENo=DE2.DENo ";
            Sql04Str = Sql04Str + "  Where SWC000 = '" + v + "' and isnull(DATALOCK,'')='Y' ";
            Sql04Str = Sql04Str + "  order by DTLD001 ";

            SqlDataReader readerItem04;
            SqlCommand objCmdItem04 = new SqlCommand(Sql04Str, DTLConn);
            readerItem04 = objCmdItem04.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓表4" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

            while (readerItem04.Read())
            {
                string dDTLD01 = readerItem04["DTLD001"] + "";
                string dDTLD02 = readerItem04["DTLD002"] + "";
                string dDTLD03 = readerItem04["DTLD003"] + "";
                string dDTLD04 = readerItem04["DTLD004"] + "";

                DataTable OBJ_GV04 = (DataTable)ViewState["GV04"];
                DataTable DTGV04 = new DataTable();

                if (OBJ_GV04 == null)
                {
                    DTGV04.Columns.Add(new DataColumn("DTLD001", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD002", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD003", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD004", typeof(string)));

                    ViewState["GV04"] = DTGV04;
                    OBJ_GV04 = DTGV04;
                }
                DataRow dr04 = OBJ_GV04.NewRow();

                dr04["DTLD001"] = dDTLD01;
                dr04["DTLD002"] = dDTLD02;
                dr04["DTLD003"] = dDTLD03;
                dr04["DTLD004"] = dDTLD04;

                OBJ_GV04.Rows.Add(dr04);

                ViewState["GV04"] = OBJ_GV04;

                SWCDTL04.DataSource = OBJ_GV04;
                SWCDTL04.DataBind();
            }
        }

        //表5
        sqlin = DateTime.Now;
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql05Str = "";

            Sql05Str = Sql05Str + " Select [DTLE001] ,CONVERT(varchar(100), [DTLE002], 23) AS DTLE002,DTLE082 as DTLE003,DTLE004  From SWCDTL05 ";
            Sql05Str = Sql05Str + "  Where SWC000 = '" + v + "'  and isnull(DATALOCK,'')='Y' ";
            Sql05Str = Sql05Str + "  order by DTLE002 ";

            SqlDataReader readerItem05;
            SqlCommand objCmdItem05 = new SqlCommand(Sql05Str, DTLConn);
            readerItem05 = objCmdItem05.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓表5" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

            while (readerItem05.Read())
            {
                string dDTLE01 = readerItem05["DTLE001"] + "";
                string dDTLE02 = readerItem05["DTLE002"] + "";
                string dDTLE03 = readerItem05["DTLE003"] + "";
                string dDTLE04 = readerItem05["DTLE004"] + "";

                DataTable OBJ_GV05 = (DataTable)ViewState["GV05"];
                DataTable DTGV05 = new DataTable();

                if (OBJ_GV05 == null)
                {
                    DTGV05.Columns.Add(new DataColumn("DTLE001", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE002", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE003", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE004", typeof(string)));

                    ViewState["GV05"] = DTGV05;
                    OBJ_GV05 = DTGV05;
                }
                DataRow dr05 = OBJ_GV05.NewRow();

                dr05["DTLE001"] = dDTLE01;
                dr05["DTLE002"] = dDTLE02;
                dr05["DTLE003"] = dDTLE03;
                dr05["DTLE004"] = dDTLE04;

                OBJ_GV05.Rows.Add(dr05);

                ViewState["GV05"] = OBJ_GV05;

                SWCDTL05.DataSource = OBJ_GV05;
                SWCDTL05.DataBind();
            }
        }

        //表6
        sqlin = DateTime.Now;
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql06Str = "";

            Sql06Str = Sql06Str + " Select [DTLF001] ,CONVERT(varchar(100), [DTLF002], 23) AS DTLF002,DTLF003,DTLF004  From SWCDTL06 ";
            Sql06Str = Sql06Str + "  Where SWC000 = '" + v + "' and isnull(DATALOCK,'')='Y' ";
            Sql06Str = Sql06Str + "  order by DTLF001 ";

            SqlDataReader readerItem06;
            SqlCommand objCmdItem06 = new SqlCommand(Sql06Str, DTLConn);
            readerItem06 = objCmdItem06.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓表6" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

            while (readerItem06.Read())
            {
                string dDTLF01 = readerItem06["DTLF001"] + "";
                string dDTLF02 = readerItem06["DTLF002"] + "";
                string dDTLF03 = readerItem06["DTLF003"] + "";
                string dDTLF04 = readerItem06["DTLF004"] + "";

                DataTable OBJ_GV06 = (DataTable)ViewState["GV06"];
                DataTable DTGV06 = new DataTable();

                if (OBJ_GV06 == null)
                {
                    DTGV06.Columns.Add(new DataColumn("DTLF001", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF002", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF003", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF004", typeof(string)));

                    ViewState["GV06"] = DTGV06;
                    OBJ_GV06 = DTGV06;
                }
                DataRow dr06 = OBJ_GV06.NewRow();

                dr06["DTLF001"] = dDTLF01;
                dr06["DTLF002"] = dDTLF02;
                dr06["DTLF003"] = dDTLF03;
                dr06["DTLF004"] = dDTLF04;

                OBJ_GV06.Rows.Add(dr06);

                ViewState["GV06"] = OBJ_GV06;

                SWCDTL06.DataSource = OBJ_GV06;
                SWCDTL06.DataBind();
            }
        }

        //表7
        sqlin = DateTime.Now;
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql07Str = "";

            Sql07Str = Sql07Str + " Select [DTLG001] ,CONVERT(varchar(100), [DTLG002], 23) AS DTLG002,DTLG003,DTLG004  From SWCDTL07 ";
            Sql07Str = Sql07Str + "  Where SWC000 = '" + v + "' and isnull(DATALOCK,'')='Y' ";
            Sql07Str = Sql07Str + "  order by DTLG002 ";

            SqlDataReader readerItem07;
            SqlCommand objCmdItem07 = new SqlCommand(Sql07Str, DTLConn);
            readerItem07 = objCmdItem07.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓表7" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

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
        sqlin = DateTime.Now;
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
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓GVSWCCHG" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

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
        sqlin = DateTime.Now;
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
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓地籍" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

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

                DataTable tbCadastral = (DataTable)ViewState["SwcCadastral"];

                if (tbCadastral == null)
                {
                    DataTable GVTBCD = new DataTable();

                    GVTBCD.Columns.Add(new DataColumn("序號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("小段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地使用分區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地可利用限度", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("林地類別", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地質敏感區", typeof(string)));

                    ViewState["SwcCadastral"] = GVTBCD;
                    tbCadastral = (DataTable)ViewState["SwcCadastral"];
                }

                DataRow GVTBCDRow = tbCadastral.NewRow();

                GVTBCDRow["序號"] = ++nj;
                GVTBCDRow["區"] = dLAND001;
                GVTBCDRow["段"] = dLAND002;
                GVTBCDRow["小段"] = dLAND003;
                GVTBCDRow["地號"] = dLAND004;
                GVTBCDRow["土地使用分區"] = dLAND005;
                GVTBCDRow["土地可利用限度"] = dLAND006;
                GVTBCDRow["林地類別"] = dLAND007;
                GVTBCDRow["地質敏感區"] = dLAND008;

                tbCadastral.Rows.Add(GVTBCDRow);

                //Store the DataTable in ViewState
                ViewState["SwcCadastral"] = tbCadastral;

                GVCadastral.DataSource = tbCadastral;
                GVCadastral.DataBind();
                
            }
            readerItem.Close();
        }

        //計畫申請書...
        sqlin = DateTime.Now;
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV = "select * from SWCFILES";
            strSQLRV = strSQLRV + " Where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + " order by convert(int,FILE001)  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓計畫申請書" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

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
                GVFILE001Row["File001004"] = SwcUpLoadFilePath + v + "\\" + dFILE003;

                File001C.Rows.Add(GVFILE001Row);

                //Store the DataTable in ViewState
                ViewState["File001C"] = File001C;

                SWCFILES001.DataSource = File001C;
                SWCFILES001.DataBind();
                
            }
            readerItem.Close();
        }

        //2018-新增子表單
        GenOnlineApply(v);
        SetDtlData(v);
        SetPayData(v);
    }

    private void SetPayData(string vSwc000)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;
        string ssUserName = Session["NAME"] + "";
        string payData01 = " select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CONVERT(varchar(100), CPI004, 23) as CPI004 from tslm2.dbo.CasePaymentInfo where CaseID = '" + vSwc000 + "' and CaseType = '審查費' and CPI006='已列印' order by id; ";
        SqlDataSource01.SelectCommand = payData01;
        string payData02 = " select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CONVERT(varchar(100), CPI004, 23) as CPI004 from tslm2.dbo.CasePaymentInfo where CaseID = '" + vSwc000 + "' and CaseType = '保證金' and CPI006='已列印' order by id; ";
        SqlDataSource02.SelectCommand = payData02;
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
            Sql201Str = Sql201Str + " select ONA01001 as DATA01 ,left(convert(char, ONA01002, 120),10) as DATA02,left(convert(char, ONA01003, 120),10) as DATA03,ONA01004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply01 ";
            Sql201Str = Sql201Str + "  Where SWC002 = '" + v2 + "' ";
            Sql201Str = Sql201Str + "  order by ONA01001 ";

            string Sql202Str = "";
            Sql202Str = Sql202Str + " select ONA02001 as DATA01 ,left(convert(char, ONA02002, 120),10) as DATA02,left(convert(char, ONA02003, 120),10) as DATA03,ONA02004 as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply02 ";
            Sql202Str = Sql202Str + "  Where SWC000 = '" + v + "' ";
            Sql202Str = Sql202Str + "  order by ONA02001 ";

            string Sql203Str = "";
            Sql203Str = Sql203Str + " select ONA03001 as DATA01 ,left(convert(char, ONA03002, 120),10) as DATA02,left(convert(char, ONA03003, 120),10) as DATA03,'第'+ONA03004+'次展延' as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply03 ";
            Sql203Str = Sql203Str + "  Where SWC000 = '" + v + "' ";
            Sql203Str = Sql203Str + "  order by ONA03001 ";

            string Sql204Str = "";
            Sql204Str = Sql204Str + " select ONA04001 as DATA01 ,left(convert(char, ONA04003, 120),10) as DATA02,left(convert(char, ONA04004, 120),10) as DATA03,'' as DATA04,DATALOCK as DATA05,Case when ISNULL(DATALOCK2,'')<>'Y' then '' else  replace(replace(replace(isnull(ReviewResults,''),1,'核准'),'0','駁回'),'2','退補正') End AS DATA06 from OnlineApply04 ";
            Sql204Str = Sql204Str + "  Where SWC000 = '" + v + "' ";
            Sql204Str = Sql204Str + "  order by ONA04001 ";

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

            string[] arraySqlExeStr = new string[] { Sql201Str,Sql202Str, Sql203Str, Sql204Str, Sql205Str, Sql206Str, Sql207Str, Sql208Str, Sql209Str };
            GridView[] arrayONLAGV = new GridView[] { SWCOLA201,SWCOLA202, SWCOLA203, SWCOLA204, SWCOLA205, SWCOLA206, SWCOLA207, SWCOLA208, SWCOLA209 };

            for (int i = 0; i < arraySqlExeStr.Length; i++)
            {
                sqlin = DateTime.Now;
                string aSqlStr = arraySqlExeStr[i]+"";
                GridView SWCONLA = arrayONLAGV[i];

                if (aSqlStr.Trim() == "") { } else
                {
                    SqlDataReader readerItem;
                    SqlCommand objCmdItem = new SqlCommand(aSqlStr, OLAConn);
                    readerItem = objCmdItem.ExecuteReader();
                    sqlout = DateTime.Now;
                    sqlts = sqlout - sqlin;
                    sqllog.Text = sqllog.Text + "\n\r" + "抓子表單" + aSqlStr + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
                    sqlin = DateTime.Now;
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
        //string rCaseId = Request.QueryString["SWCNO"] + "";
        string rCaseId = sqlSWCNO;

        Button LButton = (Button)sender;
        string LINK = "SWCDT001v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);
    }
    protected void ButtonDTL0302_Click(object sender, EventArgs e)
    {
        //string rCaseId = Request.QueryString["SWCNO"] + "";
        string rCaseId = sqlSWCNO;

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
        //string rCaseId = Request.QueryString["SWCNO"] + "";
        string rCaseId = sqlSWCNO;

        Button LButton = (Button)sender;
        string LINK = "SWCDT004v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL05_Click(object sender, EventArgs e)
    {
        //string rCaseId = Request.QueryString["SWCNO"] + "";
        string rCaseId = sqlSWCNO;

        Button LButton = (Button)sender;
        string LINK = "SWCDT005v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL06_Click(object sender, EventArgs e)
    {
        //string rCaseId = Request.QueryString["SWCNO"] + "";
        string rCaseId = sqlSWCNO;

        Button LButton = (Button)sender;
        string LINK = "SWCDT006v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL07_Click(object sender, EventArgs e)
    {
        //string rCaseId = Request.QueryString["SWCNO"] + "";
        string rCaseId = sqlSWCNO;

        Button LButton = (Button)sender;
        string LINK = "SWCDT007v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

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

    protected void DTL_02_01_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;
        string rSWCUA = LBSWC002.Text + "";

        Response.Redirect("OnlineApply001.aspx?SWCNO=" + rSWCNO + "&OACode=ADDNEW&UA=" + rSWCUA);
    }
    protected void DTL_02_09_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

        Response.Redirect("OnlineApply009.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void DTL_02_02_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

        Response.Redirect("OnlineApply002.aspx?SWCNO="+ rSWCNO+ "&OLANO=AddNew");
    }

    protected void ButtonOLA202_Click(object sender, EventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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

        Response.Redirect("OnlineApply002"+ rPageView + ".aspx?SWCNO=" + rSWCNO + "&OLANO="+ rONLANO);

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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
    
    protected void ButtonOLA203_Click(object sender, EventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;
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

    protected void DTL_02_04_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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

    protected void DTL_02_05_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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

                if (tempLock == "Y" || tmpStatus== "退補正")
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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

        Response.Redirect("OnlineApply006.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }

    protected void DTL_02_07_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

        Response.Redirect("OnlineApply007.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }


    protected void DTL_02_08_Link_Click(object sender, ImageClickEventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

        Response.Redirect("OnlineApply008.aspx?SWCNO=" + rSWCNO + "&OLANO=AddNew");
    }




    protected void ButtonOLA201_Click(object sender, EventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
    protected void ButtonOLA205_Click(object sender, EventArgs e)
    {
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
        //string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rSWCNO = sqlSWCNO;

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
        sqlin = DateTime.Now;
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
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓分段驗收" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;
            while (readerItem.Read())
            {
                #region
                string sSDI001 = readerItem["SDI001"] + "";
                string sSDI002 = readerItem["SDI002"] + "";
                string sSDI003 = readerItem["SDI003"] + "";
                string sSDI004 = readerItem["SDI004"] + "";
                string sSDI005 = readerItem["SDI005"] + "";
                string sSDI006 = readerItem["SDI006"] + "";
                string sSDI006D = readerItem["SDI006D"] + "";
                string sSDI007 = readerItem["SDI007"] + "";
                string sSDI008 = readerItem["SDI008"] + "";
                string sSDI009 = readerItem["SDI009"] + "";
                string sSDI010 = readerItem["SDI010"] + "";
                string sSDI011 = readerItem["SDI011"] + "";
                string sSDI012 = readerItem["SDI012"] + "";
                string sSDI012D = readerItem["SDI012D"] + "";
                string sSDI013 = readerItem["SDI013"] + "";
                string sSDI014 = readerItem["SDI014"] + "";
                string sSDI015 = readerItem["SDI015"] + "";
                string sSDI016 = readerItem["SDI016"] + "";

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
                    SDITB.Columns.Add(new DataColumn("SDIFD006D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD009", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD010", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD011", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));

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
                SDITBRow["SDIFD006D"] = sSDI006D;
                SDITBRow["SDIFD007"] = sSDI007;
                SDITBRow["SDIFD008"] = sSDI008;
                SDITBRow["SDIFD009"] = sSDI009;
                SDITBRow["SDIFD010"] = sSDI010;
                SDITBRow["SDIFD011"] = sSDI011;
                SDITBRow["SDIFD012"] = sSDI012;
                SDITBRow["SDIFD012D"] = sSDI012D;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;

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
        sqlin = DateTime.Now;
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(getSFileSqlStr, ItemConn);
            readerItem = objCmdItem.ExecuteReader();
            sqlout = DateTime.Now;
            sqlts = sqlout - sqlin;
            sqllog.Text = sqllog.Text + "\n\r" + "抓檔案交換區" + ";" + sqlin.ToString("HH:mm:ss.fff") + "-" + sqlout.ToString("HH:mm:ss.fff") + ";共計 " + sqlts.TotalMilliseconds.ToString() + "毫秒";
            sqlin = DateTime.Now;

            while (readerItem.Read())
            {
                #region.設定資料
                string sSFtype = readerItem["SFType"] + "";
                string sFileName = readerItem["SFName"] + "";
                string sSaveUser = readerItem["SaveUser"] + "";
                string sSaveDate = readerItem["SaveDate"] + "";
                string sFileLink = SwcUpLoadFilePath + rSWCNO + "/" + sFileName;

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
                    case "006":
                        fileName = TXTSFile06; fileLink = SFLINK06; uploadUser = LBUPLOADU06; uploadDate = LBUPLOADD06;
                        break;
                    case "007":
                        fileName = TXTSFile07; fileLink = SFLINK07; uploadUser = LBUPLOADU07; uploadDate = LBUPLOADD07;
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
                rUP = FileUpLoadApp2("PDFJPGPNG", SFFile04_FileUpload, TXTSFile04, "TXTSFile04", tSFName, null, SFLINK04, 50, ""); //50MB
                break;
            case "Btn_UPSFile05":
                tSFType = "005"; tSFName = "UPSFE_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU05; pgTBDate = LBUPLOADD05;
                filename = SFFile05_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDFJPGPNG", SFFile05_FileUpload, TXTSFile05, "TXTSFile05", tSFName, null, SFLINK05, 50, ""); //50MB
                break;
            case "Btn_UPSFile06":
                tSFType = "006"; tSFName = "UPSFF_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU06; pgTBDate = LBUPLOADD06;
                filename = SFFile06_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile06_FileUpload, TXTSFile06, "TXTSFile06", tSFName, null, SFLINK06, 150, ""); //50MB
                break;
            case "Btn_UPSFile07":
                tSFType = "007"; tSFName = "UPSFG_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU07; pgTBDate = LBUPLOADD07;
                filename = SFFile07_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile07_FileUpload, TXTSFile07, "TXTSFile07", tSFName, null, SFLINK07, 150, ""); //50MB
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
                    exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE) VALUES ('" + rDTLNO + "','" + tSFType + "');";
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

        #region.寄信
        //審查公會、公會審查召集人、審查委員
        string tToDay = DateTime.Now.ToString("yyyy-MM-dd");
        string tSWC005 = LBSWC005.Text;
        string tSWC022ID = LBSWC022ID.Text; //審查公會
        string tLBSAOID = ";;"+LBSAOID.Text.Replace(";;;;","");
        string tMailSub  = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        string tMailText = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

        switch (btnType)
        {
            case "Btn_UPSFile01":
            case "Btn_UPSFile02":
            case "Btn_UPSFile03":
                string[] arraySentMail = new string[] {""};
                string[] arrayMailToP = tLBSAOID.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

                arraySentMail[0] = SBApp.GetGeoUser(tSWC022ID, "Email");
                bool MailTo01 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);

                for (int i = 0; i < arrayMailToP.Length; i++) {
                    arraySentMail[0] = SBApp.GetETUser(arrayMailToP[i], "Email");
                    bool MailTo02 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
                }
                break;
        }

        #endregion
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
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath"] + CaseId;
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
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\SwcCaseFile\\" + CaseId + "\\" + SubFolder + "\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
                case "Btn_DelSFile06":
                    pgLink = SFLINK06; pgTB = TXTSFile06; tSFType = "006"; pgTBName = LBUPLOADU06; pgTBDate = LBUPLOADD06;
                    break;
                case "Btn_DelSFile07":
                    pgLink = SFLINK07; pgTB = TXTSFile07; tSFType = "007"; pgTBName = LBUPLOADU07; pgTBDate = LBUPLOADD07;
                    break;
            }

            string DelDBDA = " Delete ShareFiles Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

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
    }
    #region.刪檔
    private bool DelPageFile(string delFileName, string delFileType)
    {
        bool rValue = false;
        string csCaseID = LBSWC000.Text + "";

        //刪實體檔
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];
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
        string rSwcNo = LBSWC002.Text+"";
        if (rSwcNo.Length > 12)
            if (rSwcNo.Substring(13, 1) == "9") {
                Response.Write("<script>alert('變更次數已達系統設計極限，請洽大地工程處，謝謝!!');</script>"); return;}

        Session["CopyCaseIdD1"] = rCaseId;
        Session["CopyCaseIdD2"] = rSwcNo;
        Session["TempSwcCadastral"] = ViewState["SwcCadastral"];
        Session["TempSwcDocItem"] = ViewState["SwcDocItem"];

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
}