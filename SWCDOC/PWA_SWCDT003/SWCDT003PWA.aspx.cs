using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PWA_SWCDT003_SWCDT003PWA : System.Web.UI.Page
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "tim@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";

        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：" + errUrl + "<br/>";
        mailText += "錯誤訊息：" + errMsg + "<br/>";

        C1.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
        //Response.Redirect("~/errPage/500.htm");
		Response.Write(errMsg);
        Server.ClearError();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (!IsPostBack) {
            C20.swcLogRC("SWCDT003", "施工檢查紀錄表", "詳情", "瀏覽", rCaseId+","+ rDTLId);
            if (rCaseId == "" && rDTLId != "AddNew")
                rCaseId = Get03SWC000(rDTLId);
            LBSWC000.Text = rCaseId.Trim() == "" ? rDTLId : rCaseId;
            GenerateDropDownList();
            Data2Page(rCaseId, rDTLId);
        }
        #region 全區供用
        SBApp.ViewRecord("水土保持施工監督檢查紀錄", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        #endregion
    }
    private string Get03SWC000(string v2)
    {
        string _returnVall = "";
        string strSQLRV = " select SWC000 from SWCDTL03 ";
        strSQLRV = strSQLRV + " where DTLC000 = '" + v2 + "' ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                _returnVall = readerSWC["SWC000"] + "";
            }
        }
        return _returnVall;
    }
    private void Data2Page(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE where SWC000 = '" + v + "' ";
            #region SWCCASE
            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC012 = readeSwc["SWC012"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC023 = readeSwc["SWC023"] + "";
                string tSWC024 = readeSwc["SWC024"] + "";
                string tSWC024ID = readeSwc["SWC024ID"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";

                LBSWC000.Text = v;
                LBSWC002.Text = tSWC002;
				TXTDTL004.Text = tSWC024;
                LBSWC005.Text = tSWC005;
                LBSWC005a.Text = tSWC005;
                LBSWC007.Text = tSWC007;
                LBSWC012.Text = tSWC012;
                LBSWC013ID.Text = tSWC013ID;
                LBSWC013.Text = tSWC013;
                LBSWC013TEL.Text = tSWC013TEL;
                LBSWC014.Text = tSWC014;
                LBSWC021.Text = tSWC045;
                LBSWC021Name.Text = SBApp.GetETUser(tSWC045ID, "OrgName");
                LBSWC021OrgIssNo.Text = SBApp.GetETUser(tSWC045ID, "OrgIssNo");
                LBSWC021OrgGUINo.Text = SBApp.GetETUser(tSWC045ID, "OrgGUINo");
                LBSWC021OrgTel.Text = SBApp.GetETUser(tSWC045ID, "OrgTel");
                LBSWC021OrgAddr.Text = SBApp.GetETUser(tSWC045ID, "OrgAddr");
                LBSWC023.Text = tSWC023;
                LBSWC025.Text = tSWC025;
                LBSWC038.Text = SBApp.DateView(tSWC038, "00");
                LBSWC039.Text = tSWC039;
                LBSWC043.Text = SBApp.DateView(tSWC043, "00");
                LBSWC044.Text = tSWC044;
                LBSWC051.Text = SBApp.DateView(tSWC051, "00");
                LBSWC052.Text = SBApp.DateView(tSWC052, "00");
                TXTDTL004.Text = SBApp.GetGeoUser(tSWC024ID, "Name");
                LBSWCO01.Text = getProgress(v);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
            #endregion

            string tmpDTL000 = getDTL000(v, v2);
            if (v2 == "AddNew")
                LBDTL001.Text=GetDTLAID(v);



            //if (v2 == "AddNew")
            //{
            //    string nIDA = GetDTLAID(v);
            //    string ssGuildId = Session["ETU_Guild02"] + "";

                //    LBDTL001.Text = nIDA;

                //    //if (TXTDTL004.Text.Trim() == "")
                //    //    TXTDTL004.Text = SBApp.GetGeoUser(ssGuildId, "Name");

                //    //設定預測值
                //    TXTDTL051.Text = GetLastDtl48N50(v);


                //}
                //else
            //{
                //string ssUserName = Session["NAME"] + "";
                //TXTDTL004.Text = ssUserName;

                string strSQLRV2 = " select * from SWCDTL03 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and DTLC001 = '" + tmpDTL000 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLC001 = v2 == "AddNew" ? GetDTLAID(v) : readeDTL["DTLC001"] + "";
                    //string tDTLC001 = readeDTL["DTLC001"] + "";
                    string tDTLC002 = readeDTL["DTLC002"] + "";
                    string tDTLC003 = readeDTL["DTLC003"] + "";
                    //string tDTLC004 = readeDTL["DTLC004"] + "";
                    string tDTLC005 = readeDTL["DTLC005"] + "";
                    string tDTLC006 = readeDTL["DTLC006"] + "";
                    string tDTLC007 = readeDTL["DTLC007"] + "";
                    string tDTLC008 = readeDTL["DTLC008"] + "";
                    string tDTLC009 = readeDTL["DTLC009"] + "";
                    string tDTLC010 = readeDTL["DTLC010"] + "";
                    string tDTLC011 = readeDTL["DTLC011"] + "";
                    string tDTLC012 = readeDTL["DTLC012"] + "";
                    string tDTLC013 = readeDTL["DTLC013"] + "";
                    string tDTLC014 = readeDTL["DTLC014"] + "";
                    string tDTLC015 = readeDTL["DTLC015"] + "";
                    string tDTLC016 = readeDTL["DTLC016"] + "";
                    string tDTLC017 = readeDTL["DTLC017"] + "";
                    string tDTLC018 = readeDTL["DTLC018"] + "";
                    string tDTLC019 = readeDTL["DTLC019"] + "";
                    string tDTLC020 = readeDTL["DTLC020"] + "";
                    string tDTLC021 = readeDTL["DTLC021"] + "";
                    string tDTLC022 = readeDTL["DTLC022"] + "";
                    string tDTLC023 = readeDTL["DTLC023"] + "";
                    string tDTLC024 = readeDTL["DTLC024"] + "";
                    string tDTLC025 = readeDTL["DTLC025"] + "";
                    string tDTLC026 = readeDTL["DTLC026"] + "";
                    string tDTLC027 = readeDTL["DTLC027"] + "";
                    string tDTLC028 = readeDTL["DTLC028"] + "";
                    string tDTLC029 = readeDTL["DTLC029"] + "";
                    string tDTLC030 = readeDTL["DTLC030"] + "";
                    string tDTLC031 = readeDTL["DTLC031"] + "";
                    string tDTLC032 = readeDTL["DTLC032"] + "";
                    string tDTLC033 = readeDTL["DTLC033"] + "";
                    string tDTLC034 = readeDTL["DTLC034"] + "";
                    string tDTLC035 = readeDTL["DTLC035"] + "";
                    string tDTLC036 = readeDTL["DTLC036"] + "";
                    string tDTLC037 = readeDTL["DTLC037"] + "";
                    string tDTLC038 = readeDTL["DTLC038"] + "";
                    string tDTLC039 = readeDTL["DTLC039"] + "";
                    string tDTLC040 = readeDTL["DTLC040"] + "";
                    string tDTLC041 = readeDTL["DTLC041"] + "";
                    string tDTLC042 = readeDTL["DTLC042"] + "";
                    string tDTLC043 = readeDTL["DTLC043"] + "";
                    string tDTLC044 = readeDTL["DTLC044"] + "";
                    string tDTLC045 = readeDTL["DTLC045"] + "";
                    string tDTLC046 = readeDTL["DTLC046"] + "";
                    string tDTLC047 = readeDTL["DTLC047"] + "";
                    string tDTLC048 = readeDTL["DTLC048"] + "";
                    string tDTLC049 = readeDTL["DTLC049"] + "";
                    string tDTLC050 = readeDTL["DTLC050"] + "";
                    string tDTLC051 = readeDTL["DTLC051"] + "";
                    string tDTLC052 = readeDTL["DTLC052"] + "";
                    string tDTLC053 = readeDTL["DTLC053"] + "";
                    string tDTLC054 = readeDTL["DTLC054"] + "";
                    string tDTLC055 = readeDTL["DTLC055"] + "";
                    string tDTLC056 = readeDTL["DTLC056"] + "";
                    string tDTLC057 = readeDTL["DTLC057"] + "";
                    string tDTLC058 = readeDTL["DTLC058"] + "";
                    string tDTLC059 = readeDTL["DTLC059"] + "";
                    string tDTLC060 = readeDTL["DTLC060"] + "";
                    string tDTLC061 = readeDTL["DTLC061"] + "";
                    string tDTLC062 = readeDTL["DTLC062"] + "";
                    string tDTLC063 = readeDTL["DTLC063"] + "";
                    string tDTLC064 = readeDTL["DTLC064"] + "";
                    string tDTLC065 = readeDTL["DTLC065"] + "";
                    string tDTLC066 = readeDTL["DTLC066"] + "";
                    string tDTLC067 = readeDTL["DTLC067"] + "";
                    string tDTLC068 = readeDTL["DTLC068"] + "";
                    string tDTLC070 = readeDTL["DTLC070"] + "";
                    string tDTLC071 = readeDTL["DTLC071"] + "";
                    string tDTLC072 = readeDTL["DTLC072"] + "";
                    string tDTLC073 = readeDTL["DTLC073"] + "";
                    string tDTLC074 = readeDTL["DTLC074"] + "";
                    string tDTLC075 = readeDTL["DTLC075"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";
                    
                    LBDTL001.Text = tDTLC001;
                    TXTDTL002.Text = SBApp.DateView(tDTLC002, "00");
                    //TXTDTL003.Text = tDTLC003;
                    //TXTDTL004.Text = tDTLC004;
                    TXTDTL005.Text = tDTLC005;
                    DDLDTL006.SelectedValue = tDTLC006;
                    TXTDTL007.Text = tDTLC007;
                    DDLDTL008.SelectedValue = tDTLC008;
                    TXTDTL009.Text = tDTLC009;
                    DDLDTL010.SelectedValue = tDTLC010;
                    TXTDTL011.Text = tDTLC011;
                    DDLDTL012.SelectedValue = tDTLC012;
                    TXTDTL013.Text = tDTLC013;
                    DDLDTL014.SelectedValue = tDTLC014;
                    TXTDTL015.Text = tDTLC015;
                    DDLDTL016.SelectedValue = tDTLC016;
                    TXTDTL017.Text = tDTLC017;
                    DDLDTL018.SelectedValue = tDTLC018;
                    TXTDTL019.Text = tDTLC019;
                    DDLDTL020.SelectedValue = tDTLC020;
                    TXTDTL021.Text = tDTLC021;
                    DDLDTL022.SelectedValue = tDTLC022;
                    TXTDTL023.Text = tDTLC023;
                    DDLDTL024.SelectedValue = tDTLC024;
                    TXTDTL025.Text = tDTLC025;
                    DDLDTL026.SelectedValue = tDTLC026;
                    TXTDTL027.Text = tDTLC027;
                    DDLDTL028.SelectedValue = tDTLC028;
                    TXTDTL029.Text = tDTLC029;
                    DDLDTL030.SelectedValue = tDTLC030;
                    TXTDTL031.Text = tDTLC031;
                    DDLDTL032.SelectedValue = tDTLC032;
                    TXTDTL033.Text = tDTLC033;
                    DDLDTL034.SelectedValue = tDTLC034;
                    TXTDTL035.Text = tDTLC035;
                    DDLDTL036.SelectedValue = tDTLC036;
                    TXTDTL037.Text = tDTLC037;
                    DDLDTL038.SelectedValue = tDTLC038;
                    TXTDTL039.Text = tDTLC039;
                    DDLDTL040.SelectedValue = tDTLC040;
                    TXTDTL041.Text = tDTLC041;
                    DDLDTL042.SelectedValue = tDTLC042;
                    TXTDTL043.Text = tDTLC043;
                    DDLDTL044.SelectedValue = tDTLC044;
                    TXTDTL045.Text = tDTLC045;
                    DDLDTL046.SelectedValue = tDTLC046;
                    TXTDTL047.Text = tDTLC047;
                    TXTDTL048.Text = tDTLC048;
                    TXTDTL049.Text = SBApp.DateView(tDTLC049, "00");
                    TXTDTL050.Text = tDTLC050;
                    TXTDTL051.Text = tDTLC051;
                    TXTDTL052.Text = tDTLC052;
                    DDLDTL053.SelectedValue = tDTLC053;
                    TXTDTL054.Text = tDTLC054;
                    TXTDTL055.Text = tDTLC055;
                    TXTDTL056.Text = tDTLC056;
                    TXTDTL057.Text = tDTLC057;
                    TXTDTL058.Text = tDTLC058;
                    TXTDTL059.Text = tDTLC059;
                    TXTDTL060.Text = tDTLC060;
                    TXTDTL061.Text = tDTLC061;
                    TXTDTL062.Text = tDTLC062;
                    TXTDTL063.Text = tDTLC063;
                    TXTDTL064.Text = tDTLC064;
                    TXTDTL065.Text = tDTLC065;
                    TXTDTL066.Text = tDTLC066;
                    TXTDTL067.Text = tDTLC067;
                    TXTDTL068.Text = tDTLC068;
                    TXTDTL070.Text = tDTLC070;
                    DDLDTL071.Text = tDTLC071;
                    DDLDTL072.Text = tDTLC072;
                    TXTDTL073.Text = tDTLC073;
                    LBSWC071.Text = tDTLC071;
                    LBSWCO01.Text = tDTLC075;

                    #region 日期
                    TextBox[] aTBD = new TextBox[] { TXTDTL074 };
                    string[] aDate = new string[] { tDTLC074 };
                    for (int i = 0; i < aTBD.Length; i++) {
                        aTBD[i].Text = SBApp.DateView(aDate[i], "00");
                    }
                    #endregion
                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLC070 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link070 };

                    for (int i = 0; i < arrayFileNameLink.Length; i++)
                    {
                        string strFileName = arrayFileNameLink[i];
                        System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];

                        FileLinkObj.Visible = false;
                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    //點擊放大圖片類處理
                    string[] arrayFileName2 = new string[] { tDTLC056, tDTLC057, tDTLC059, tDTLC061, tDTLC063, tDTLC065, tDTLC067 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj2 = new System.Web.UI.WebControls.HyperLink[] { HyperLink056, HyperLink057, HyperLink059, HyperLink061, HyperLink063, HyperLink065, HyperLink067 };

                    for (int i = 0; i < arrayFileName2.Length; i++)
                    {
                        string strFileName = arrayFileName2[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj2[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                            ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                        }
                    }

                    //按鈕處理
                    if (tDATALOCK == "Y" && v2 != "AddNew")
                    {
                        SaveCase.Visible = false;

                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='SWCDT003v2.aspx?SWCNO=" + v + "&DTLNO=" + v2 + "'; </script>");
                        //error_msg.Text = SBApp.AlertMsg("資料已送出，目前僅供瀏覽。");
                    }

                    string tmpDTL048 = "", tmpDTL051 = "";
                    #region 二、通知水土保持義務人，三、未依計畫施作事項
                    string[] arrayItmeStr = new string[] { 			"水土保持施工告示牌", 	"開發範圍界樁", 	"開挖整地範圍界樁", "臨時防災設施-排水設施", 	"臨時防災設施-沉砂設施", "臨時防災設施-滯洪設施", "臨時防災設施-土方暫置", "臨時防災設施-邊坡保護措施", "臨時防災設施-施工便道", "臨時防災設施-臨時攔砂設施(如砂包、防溢座等)", 	"臨時防災設施-其他",  	"永久性防災措施-排水設施", 		"永久性防災措施-沉砂設施", "永久性防災措施-滯洪設施", "永久性防災措施-聯外排水", "永久性防災措施-擋土設施", "永久性防災措施-植生工程", "永久性防災措施-邊坡穩定措施", "永久性防災措施-其他", "承辦監造技師是否在場", "是否備妥監造紀錄" };
					DropDownList[] arrayDDL = new DropDownList[] { DDLDTL006 , DDLDTL008, DDLDTL010, DDLDTL012, DDLDTL014, DDLDTL016, DDLDTL018, DDLDTL020, DDLDTL022, DDLDTL072, DDLDTL024, DDLDTL026, DDLDTL028, DDLDTL030, DDLDTL032, DDLDTL034, DDLDTL036, DDLDTL038 };
                    TextBox[] arrayTB = new TextBox[] { TXTDTL007, TXTDTL009, TXTDTL011, TXTDTL013, TXTDTL015, TXTDTL017, TXTDTL019, TXTDTL021, TXTDTL023, TXTDTL073, TXTDTL025, TXTDTL027, TXTDTL029, TXTDTL031, TXTDTL033, TXTDTL035, TXTDTL037, TXTDTL039 };
                    for (int i = 0; i < arrayDDL.Length; i++) {
                        string qq = DDLDTL012.Text;
                        string qq2 = tDTLC012;
                        switch (arrayDDL[i].SelectedValue) {
                            case "有缺失應改善":
                                tmpDTL048 += arrayItmeStr[i] + "：" + arrayTB[i].Text + ",,,";
                                break;
                            case "缺失應改未改或未依計畫施作，通知大地處查處":
                                tmpDTL051 += arrayItmeStr[i] + "：" + arrayTB[i].Text + ",,,";
                                break;
                        }
                    }
                    TXTDTL048.Text = tmpDTL048.Trim()==""?"": "【"+(tmpDTL048.Substring(0, tmpDTL048.Length-3)).Replace(",,,", "】<br/>【") + "】";
                    LBDTL051.Text = tmpDTL051.Trim() == "" ? "" : "【"+(tmpDTL051.Substring(0, tmpDTL051.Length - 3)).Replace(",,,", "】<br/>【") + "】";
                    #endregion
                }
            //}

        }
        SetDtlData(v, v2);
    }


    private string getDTL000(string v, string v2)
    {
        string rValue = v2;
        if (v2 == "AddNew")
        {
            string sqlStr = " select top 1 * from SWCDTL03 where SWC000=@SWC000 and DATALOCK='Y' order by id desc; ";
            #region
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();
                using (var cmd = SWCConn.CreateCommand())
                {
                    cmd.CommandText = sqlStr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                    #endregion
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerTslm = cmd.ExecuteReader())
                    {
                        while (readerTslm.Read())
                        {
                            rValue = readerTslm["DTLC000"] + "";
                        }
                        readerTslm.Close();
                    }
                    cmd.Cancel();
                }
            }
            #endregion
        }
        return rValue;
    }

    private string getProgress(string v)
    {
        GBClass001 GB1 = new GBClass001();
        string rValue = "";
        string sqlStr = " select top 1 * from SWCDTL05 where SWC000=@SWC000 and isnull(DATALOCK,'')='Y' order by id desc; ";
        #region
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                    {
                        while (readerSWC.Read())
                        {
                            string qComplete = readerSWC["DTLE003"] + "";
                            string qSentDate = readerSWC["savedate"] + "";
                            rValue = "至" + GB1.DateView(qSentDate, "05") + "工程進度" + qComplete + "%";
                        }
                    }
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }

    private string GetLastDtl48N50(string v)
    {
        string _ReturnVal = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select top 1 DTLC048,DTLC050 from SWCDTL03 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   order by id desc ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string tDTLC048 = readerSWC["DTLC048"] + "";
                string tDTLC050 = readerSWC["DTLC050"] + "";

                _ReturnVal = tDTLC048 + "\r\n" + tDTLC050;
            }
        }
        return _ReturnVal;
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
		//處理IMAGE
		DeleteIMAGE();
		string[] arrayIMAGE = TXTIMAGE.Text.Split(new string[] { "/////" }, StringSplitOptions.None);	
		
		for(int i = 0; i < arrayIMAGE.Count(); i++)
		{
			string IMAGE = arrayIMAGE[i];
			if(IMAGE != "")
			{
				string IMAGENO = (IMAGE.Split(new string[] { ";;;;;" }, StringSplitOptions.None))[0];
				string IDENTITY = (IMAGE.Split(new string[] { ";;;;;" }, StringSplitOptions.None))[1];
				string NAME = (IMAGE.Split(new string[] { ";;;;;" }, StringSplitOptions.None))[2];
				string IMAGE64 = (IMAGE.Split(new string[] { ";;;;;" }, StringSplitOptions.None))[3];
				//列表轉IMAGE & 寫進SQL Server
				UploadAndInsert(IMAGENO,IMAGE64,IDENTITY,NAME);
			}
		}
		
        Class20 C20 = new Class20();

        string sqlStr = "", Q = "";
        string sSWC000 = Request.QueryString["SWCNO"] + "";
        string sDTLC000 = LBDTL001.Text + "";
        #region pageValue
        #endregion

        if (chkPageValue()) {
            if (chkNewCase()) {
                sqlStr = "INSERT INTO SWCDTL03 (SWC000,DTLC000) VALUES (@SWC000,@DTL000);";
            }
        }
        sqlStr += " Update SWCDTL03 Set DTLC071=@DTLC071,DTLC072=@DTLC072,DTLC073=@DTLC073,DTLC074=@DTLC074,DTLC075=@DTLC075 where SWC000=@SWC000 and DTLC000=@DTL000; ";
        #region DBWriter
        C20.swcLogRC("SWCDT003", "施工檢查紀錄表", "詳情", "修改", sSWC000 + "," + sDTLC000);
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                cmd.Parameters.Add(new SqlParameter("@DTL000", sDTLC000));
                cmd.Parameters.Add(new SqlParameter("@DTLC071", DDLDTL071.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@DTLC072", DDLDTL072.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@DTLC073", TXTDTL073.Text));
                cmd.Parameters.Add(new SqlParameter("@DTLC074", TXTDTL074.Text));
                cmd.Parameters.Add(new SqlParameter("@DTLC075", LBSWCO01.Text));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion



        string ssUserID = Session["ID"] + "";
        if(ssUserID == "") ssUserID = "PWA";
        Class1 C1 = new Class1();

        #region.PageValue
        string sDTLC002 = TXTDTL002.Text + "";
        string sDTLC003 = TXTDTL003.Text + "";
        string sDTLC004 = TXTDTL004.Text + "";
        string sDTLC005 = TXTDTL005.Text + "";
        string sDTLC006 = DDLDTL006.SelectedValue + "";
        string sDTLC007 = TXTDTL007.Text + "";
        string sDTLC008 = DDLDTL008.SelectedValue + "";
        string sDTLC009 = TXTDTL009.Text + "";
        string sDTLC010 = DDLDTL010.SelectedValue + "";
        string sDTLC011 = TXTDTL011.Text + "";
        string sDTLC012 = DDLDTL012.SelectedValue + "";
        string sDTLC013 = TXTDTL013.Text + "";
        string sDTLC014 = DDLDTL014.SelectedValue + "";
        string sDTLC015 = TXTDTL015.Text + "";
        string sDTLC016 = DDLDTL016.SelectedValue + "";
        string sDTLC017 = TXTDTL017.Text + "";
        string sDTLC018 = DDLDTL018.SelectedValue + "";
        string sDTLC019 = TXTDTL019.Text + "";
        string sDTLC020 = DDLDTL020.SelectedValue + "";
        string sDTLC021 = TXTDTL021.Text + "";
        string sDTLC022 = DDLDTL022.SelectedValue + "";
        string sDTLC023 = TXTDTL023.Text + "";
        string sDTLC024 = DDLDTL024.SelectedValue + "";
        string sDTLC025 = TXTDTL025.Text + "";
        string sDTLC026 = DDLDTL026.SelectedValue + "";
        string sDTLC027 = TXTDTL027.Text + "";
        string sDTLC028 = DDLDTL028.SelectedValue + "";
        string sDTLC029 = TXTDTL029.Text + "";
        string sDTLC030 = DDLDTL030.SelectedValue + "";
        string sDTLC031 = TXTDTL031.Text + "";
        string sDTLC032 = DDLDTL032.SelectedValue + "";
        string sDTLC033 = TXTDTL033.Text + "";
        string sDTLC034 = DDLDTL034.SelectedValue + "";
        string sDTLC035 = TXTDTL035.Text + "";
        string sDTLC036 = DDLDTL036.SelectedValue + "";
        string sDTLC037 = TXTDTL037.Text + "";
        string sDTLC038 = DDLDTL038.SelectedValue + "";
        string sDTLC039 = TXTDTL039.Text + "";
        string sDTLC040 = DDLDTL040.SelectedValue + "";
        string sDTLC041 = TXTDTL041.Text + "";
        string sDTLC042 = DDLDTL042.SelectedValue + "";
        string sDTLC043 = TXTDTL043.Text + "";
        string sDTLC044 = DDLDTL044.SelectedValue + "";
        string sDTLC045 = TXTDTL045.Text + "";
        string sDTLC046 = DDLDTL046.SelectedValue + "";
        string sDTLC047 = TXTDTL047.Text + "";
        string sDTLC049 = TXTDTL049.Text + "";
        string sDTLC050 = TXTDTL050.Text + "";
        string sDTLC052 = TXTDTL052.Text + "";
        string sDTLC053 = DDLDTL053.SelectedValue + "";
        string sDTLC054 = TXTDTL054.Text + "";
        string sDTLC055 = TXTDTL055.Text + "";
        string sDTLC056 = TXTDTL056.Text + "";
        string sDTLC057 = TXTDTL057.Text + "";
        string sDTLC058 = TXTDTL058.Text + "";
        string sDTLC059 = TXTDTL059.Text + "";
        string sDTLC060 = TXTDTL060.Text + "";
        string sDTLC061 = TXTDTL061.Text + "";
        string sDTLC062 = TXTDTL062.Text + "";
        string sDTLC063 = TXTDTL063.Text + "";
        string sDTLC064 = TXTDTL064.Text + "";
        string sDTLC065 = TXTDTL065.Text + "";
        string sDTLC066 = TXTDTL066.Text + "";
        string sDTLC067 = TXTDTL067.Text + "";
        string sDTLC068 = TXTDTL068.Text + "";
        string sDTLC070 = TXTDTL070.Text + "";

        if (!C1.chkDateFormat(sDTLC002)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');</script>"); TXTDTL002.Focus(); return; }
        if (!C1.chkDateFormat(sDTLC049)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');</script>"); TXTDTL049.Focus(); return; }
        if (sDTLC050.Length > 500) { sDTLC050 = sDTLC050.Substring(0, 500); }
        if (sDTLC052.Length > 500) { sDTLC052 = sDTLC052.Substring(0, 500); }
        if (sDTLC055.Length > 500) { sDTLC055 = sDTLC055.Substring(0, 500); }
        if (sDTLC058.Length > 255) { sDTLC058 = sDTLC058.Substring(0, 255); }
        if (sDTLC060.Length > 255) { sDTLC060 = sDTLC060.Substring(0, 255); }
        if (sDTLC062.Length > 255) { sDTLC062 = sDTLC062.Substring(0, 255); }
        if (sDTLC064.Length > 255) { sDTLC064 = sDTLC064.Substring(0, 255); }
        if (sDTLC066.Length > 255) { sDTLC066 = sDTLC066.Substring(0, 255); }
        if (sDTLC068.Length > 255) { sDTLC068 = sDTLC068.Substring(0, 255); }
		
        //2021-03-22調整備妥監造紀錄文字
        sDTLC045 = FindDTEL088(sSWC000);
        #endregion

        string tmpDTL048 = "", tmpDTL051 = "";
        #region 二、通知水土保持義務人，三、未依計畫施作事項
        string[] arrayItmeStr = new string[] { 			"水土保持施工告示牌", 	"開發範圍界樁", 	"開挖整地範圍界樁", "臨時防災設施-排水設施", 	"臨時防災設施-沉砂設施", "臨時防災設施-滯洪設施", "臨時防災設施-土方暫置", "臨時防災設施-邊坡保護措施", "臨時防災設施-施工便道", "臨時防災設施-臨時攔砂設施(如砂包、防溢座等)", 	"臨時防災設施-其他",  	"永久性防災措施-排水設施", 		"永久性防災措施-沉砂設施", "永久性防災措施-滯洪設施", "永久性防災措施-聯外排水", "永久性防災措施-擋土設施", "永久性防災措施-植生工程", "永久性防災措施-邊坡穩定措施", "永久性防災措施-其他", "承辦監造技師是否在場", "是否備妥監造紀錄" };
		DropDownList[] arrayDDL = new DropDownList[] { DDLDTL006, DDLDTL008, DDLDTL010, DDLDTL012, DDLDTL014, DDLDTL016, DDLDTL018, DDLDTL020, DDLDTL022, DDLDTL072, DDLDTL024, DDLDTL026, DDLDTL028, DDLDTL030, DDLDTL032, DDLDTL034, DDLDTL036, DDLDTL038 };
        TextBox[] arrayTB = new TextBox[] { TXTDTL007, TXTDTL009, TXTDTL011, TXTDTL013, TXTDTL015, TXTDTL017, TXTDTL019, TXTDTL021, TXTDTL023, TXTDTL073, TXTDTL025, TXTDTL027, TXTDTL029, TXTDTL031, TXTDTL033, TXTDTL035, TXTDTL037, TXTDTL039 };
        for (int i = 0; i < arrayDDL.Length; i++)
        {
            switch (arrayDDL[i].Text)
            {
                case "有缺失應改善":
                    tmpDTL048 += arrayItmeStr[i] + "：" + arrayTB[i].Text + ",";
                    break;
                case "缺失應改未改或未依計畫施作，通知大地處查處":
                    tmpDTL051 += arrayItmeStr[i] + "：" + arrayTB[i].Text + ",";
                    break;
            }
        }
        TXTDTL048.Text = tmpDTL048.Trim() == "" ? "" : tmpDTL048.Substring(0, tmpDTL048.Length - 1);
        LBDTL051.Text = tmpDTL051.Trim() == "" ? "" : tmpDTL051.Substring(0, tmpDTL051.Length - 1);
        string sDTLC048 = TXTDTL048.Text + "";
        string sDTLC051 = LBDTL051.Text + "";
        if (sDTLC048.Length > 500) { sDTLC048 = sDTLC048.Substring(0, 500); }
        if (sDTLC051.Length > 1000) { sDTLC051 = sDTLC051.Substring(0, 1000); }
        #endregion

        string sDTLC069 = "依計畫施作";
        string[] arrayDTLC069 = new string[] { sDTLC006, sDTLC008, sDTLC010, sDTLC012, sDTLC014, sDTLC016, sDTLC018, sDTLC020, sDTLC022, sDTLC024, sDTLC026, sDTLC028, sDTLC030, sDTLC032, sDTLC034, sDTLC036, sDTLC038, sDTLC040 };

        for (int i = 0; i < arrayDTLC069.Length; i++)
        {
            string aSWC069 = arrayDTLC069[i];

            if (aSWC069 == "未依計畫施作") {
                sDTLC069 = "未依計畫施作";
            }
        }

        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL03 Set ";
            #region.欄位←值
            sEXESQLSTR = sEXESQLSTR + " DTLC001 = DTLC000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLC002 ='" + sDTLC002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC003 ='" + sDTLC003 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC004 ='" + sDTLC004 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC005 ='" + sDTLC005 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC006 ='" + sDTLC006 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC007 ='" + sDTLC007 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC008 ='" + sDTLC008 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC009 ='" + sDTLC009 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC010 ='" + sDTLC010 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC011 ='" + sDTLC011 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC012 ='" + sDTLC012 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC013 ='" + sDTLC013 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC014 ='" + sDTLC014 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC015 ='" + sDTLC015 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC016 ='" + sDTLC016 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC017 ='" + sDTLC017 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC018 ='" + sDTLC018 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC019 ='" + sDTLC019 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC020 ='" + sDTLC020 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC021 ='" + sDTLC021 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC022 ='" + sDTLC022 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC023 ='" + sDTLC023 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC024 ='" + sDTLC024 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC025 ='" + sDTLC025 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC026 ='" + sDTLC026 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC027 ='" + sDTLC027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC028 ='" + sDTLC028 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC029 ='" + sDTLC029 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC030 ='" + sDTLC030 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC031 ='" + sDTLC031 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC032 ='" + sDTLC032 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC033 ='" + sDTLC033 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC034 ='" + sDTLC034 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC035 ='" + sDTLC035 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC036 ='" + sDTLC036 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC037 ='" + sDTLC037 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC038 ='" + sDTLC038 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC039 ='" + sDTLC039 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC040 ='" + sDTLC040 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC041 ='" + sDTLC041 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC042 ='" + sDTLC042 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC043 ='" + sDTLC043 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC044 ='" + sDTLC044 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC045 ='" + sDTLC045 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC046 ='" + sDTLC046 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC047 ='" + sDTLC047 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC048 ='" + sDTLC048 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC049 ='" + sDTLC049 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC050 ='" + sDTLC050 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC051 ='" + sDTLC051 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC052 ='" + sDTLC052 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC053 ='" + sDTLC053 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC054 ='" + sDTLC054 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC055 ='" + sDTLC055 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC056 ='" + sDTLC056 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC057 ='" + sDTLC057 + "', ";            
            sEXESQLSTR = sEXESQLSTR + " DTLC058 ='" + sDTLC058 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC059 ='" + sDTLC059 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC060 ='" + sDTLC060 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC061 ='" + sDTLC061 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC062 ='" + sDTLC062 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC063 ='" + sDTLC063 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC064 ='" + sDTLC064 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC065 ='" + sDTLC065 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC066 ='" + sDTLC066 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC067 ='" + sDTLC067 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC068 ='" + sDTLC068 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC069 ='" + sDTLC069 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLC070 ='" + sDTLC070 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLC000 = '" + sDTLC000 + "'";

            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";
            #endregion
            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
            #region.
            if (Q == "")
            {
                string strSQLText = " select * from SwcItemChk where SWC000 = '" + sSWC000 + "' and DTLRPNO = '" + sDTLC000 + "' ";

                SqlDataReader readerTest;
                SqlCommand objCmdText = new SqlCommand(strSQLText, SwcConn);
                readerTest = objCmdText.ExecuteReader();

                if (!readerTest.HasRows) Q = "A";
            }
            #endregion
            SavChkSwcItem(Q);

			ClientScript.RegisterStartupScript(ClientScript.GetType(), "delValue", "<script>delValue();</script>");
			//Response.Write("<script>alert('資料已存檔');window.open('', '_self', ''); window.close(); </script>");
            
            Session["SaveOk"] = "Y";
        }
    }

    private bool chkNewCase()
    {
        bool rValue = true;
        string sSWC000 = Request.QueryString["SWCNO"] + "";
        string sDTL000 = LBDTL001.Text + "";
        string sqlStr = " select * from SWCDTL03 Where SWC000=@SWC000 and DTLC000=@DTL000; ";
        #region
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                cmd.Parameters.Add(new SqlParameter("@DTL000", sDTL000));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        rValue = false;
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }

    private bool chkPageValue()
    {
        bool rValue = true;
        return rValue;
    }

    private void SavChkSwcItem(string v)
    {
        int gLine = 0;
        string exeSqlStr = "";
        string nMSG01 = "";
        string ssUserID = Session["ID"] + "";

        DataTable dtSDI = new DataTable();
        dtSDI = (DataTable)ViewState["SwcDocItem"];

        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            if (++gLine % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[gLine - 2].Cells[3].Text;

                HiddenField HDF001 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI001");
                HiddenField HDF008 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI008");
                HiddenField HDF011 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI011");
                Label LBSDI006 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("SDILB006");
                Label LBSDI003 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE03");
                Label LBSDI004 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE04");
                Label LBSDI005 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE05");
                Label LBSDI019 = (Label)SDIList.Rows[gLine - 1].Cells[3].FindControl("LB019");
                TextBox CHK01 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001");
                TextBox CHK01_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001_1");
                TextBox CHK01D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001D");
                TextBox CHK04 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK006_1");
                TextBox RCH01 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001");
                TextBox RCH01_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001_1");
                TextBox RCH01D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001D");
                TextBox RCH04 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004");
                TextBox RCH04_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004_1");
                TextBox RCH04D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004D");
                TextBox RCH05 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH005");
                TextBox RCH05_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH005_1");
                TextBox RCH06 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH006");
                TextBox RCH06_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH006_1");
                TextBox RCH10 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH10");
                TextBox TSIC2 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK002");
                TextBox TSIC7 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK007");
                DropDownList DDLPASS = (DropDownList)SDIList.Rows[gLine - 1].Cells[3].FindControl("DDLPASS");

                string mSWC000 = LBSWC000.Text;
                string mDTLE001 = LBDTL001.Text;

                string mSDI001 = HDF001.Value;
                string mSIC01a = LBSDI006.Text; if (mSIC01a.Trim() == "") mSIC01a = "0";
                //string CHK01temp = CHK01.Text == "" ? "0" : CHK01.Text;
                //string CHK01_1temp = CHK01_1.Text == "" ? "0" : CHK01_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK01temp) > Convert.ToDouble(CHK01_1temp)) { CHK01.Text= CHK01_1temp; CHK01_1.Text = CHK01temp; }
                string mSIC01b = CHK01.Text; if (mSIC01b.Trim() == "") mSIC01b = "0";
                string mSIC01c = CHK01_1.Text; if (mSIC01c.Trim() == "") mSIC01c = "0";
                string mSIC01r = RCH01.Text; if (mSIC01r.Trim() == "") mSIC01r = "0";
                //string mSIC01r2 = RCH01_1.Text; if (mSIC01r2.Trim() == "") mSIC01r2 = "0";
                string mSIC01Da = CHK01D.Text;
                string mSIC01Dr = RCH01D.Text;
                string mSIC04a = LBSDI003.Text; if (mSIC04a.Trim() == "") mSIC04a = "0";
                string CHK04temp = CHK04.Text == "" ? "0" : CHK04.Text;
                string CHK04_1temp = CHK04.Text == "" ? "0" : CHK04_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK04temp) > Convert.ToDouble(CHK04_1temp)) { CHK04.Text = CHK04_1temp; CHK04_1.Text = CHK04temp; }
                string mSIC04b = CHK04.Text; if (mSIC04b.Trim() == "") mSIC04b = "0";
                string mSIC04c = CHK04_1.Text; if (mSIC04c.Trim() == "") mSIC04c = "0";
                string mSIC04r = RCH04.Text; if (mSIC04r.Trim() == "") mSIC04r = "0";
                string mSIC04r2 = RCH04_1.Text; if (mSIC04r2.Trim() == "") mSIC04r2 = "0";
                string mSIC04Da = CHK04D.Text;
                string mSIC04Dr = RCH04D.Text;
                string mSIC05a = LBSDI004.Text; if (mSIC05a.Trim() == "") mSIC05a = "0";
                string CHK05temp = CHK05.Text == "" ? "0" : CHK05.Text;
                string CHK05_1temp = CHK05.Text == "" ? "0" : CHK05_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK05temp) > Convert.ToDouble(CHK05_1temp)) { CHK05.Text = CHK05_1temp; CHK05_1.Text = CHK05temp; }
                string mSIC05b = CHK05.Text; if (mSIC05b.Trim() == "") mSIC05b = "0";
                string mSIC05c = CHK05_1.Text; if (mSIC05c.Trim() == "") mSIC05c = "0";
                string mSIC05r = RCH05.Text; if (mSIC05r.Trim() == "") mSIC05r = "0";
                string mSIC05r2 = RCH05_1.Text; if (mSIC05r2.Trim() == "") mSIC05r2 = "0";
                string mSIC06a = LBSDI005.Text; if (mSIC06a.Trim() == "") mSIC06a = "0";
                string CHK06temp = CHK06.Text == "" ? "0" : CHK06.Text;
                string CHK06_1temp = CHK06.Text == "" ? "0" : CHK06_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK06temp) > Convert.ToDouble(CHK06_1temp)) { CHK06.Text = CHK06_1temp; CHK06_1.Text = CHK06temp; }
                string mSIC06b = CHK06.Text; if (mSIC06b.Trim() == "") mSIC06b = "0";
                string mSIC06c = CHK06_1.Text; if (mSIC06c.Trim() == "") mSIC06c = "0";
                string mSIC06r = RCH06.Text; if (mSIC06r.Trim() == "") mSIC06r = "0";
                string mSIC06r2 = RCH06_1.Text; if (mSIC06r2.Trim() == "") mSIC06r2 = "0";
                string mSIC08 = "";
                string mSIC08b = HDF008.Value;
                string mSIC10a = RCH10.Text;
                string mSIC10b = DDLPASS.SelectedItem.Text;
                bool mSIC10c = DDLPASS.Enabled;
                string mSIC19 = LBSDI019.Text;

                if (mSIC10b != "") mSIC08 = TXTDTL002.Text;

                //數量差異百分比：SIC02=(D2-D1)/D1
                double mSIC02 = 0; double mSIC02_1 = 0;
                /*
				if (mSIC19 == "是")
                {
                    string[] sArray = mSIC01a.Split('~');
                    string Arr_a = sArray[0];
                    string Arr_b = sArray[1];
                    if (Convert.ToDouble(Arr_a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(Arr_a)) / Convert.ToDouble(Arr_a) * 100, 2);
                    if (Convert.ToDouble(Arr_b) != 0) mSIC02_1 = Math.Round((double)(Convert.ToDouble(mSIC01c) - Convert.ToDouble(Arr_b)) / Convert.ToDouble(Arr_b) * 100, 2);
                }
                else
                {
                    if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
                }
				*/
				if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
				
				
                //尺寸差異百分比：1:(A2-A1)/A1，2:((A2*B2)-(A1*B1))/(A1*B1)，3:((A2*B2*C2)-(A1*B1*C1))/(A1*B1*C1)
                string mSIC03 = HDF011.Value;
                double mSIC07 = 0; double mSIC07_1 = 0;
                if (mSIC19 == "是")
                {
                    double cA1 = Convert.ToDouble(mSIC04a.Split('~')[0]), cB1 = Convert.ToDouble(mSIC05a.Split('~')[0]), cC1 = Convert.ToDouble(mSIC06a.Split('~')[0]);
                    double cA2 = Convert.ToDouble(mSIC04b), cB2 = Convert.ToDouble(mSIC05b), cC2 = Convert.ToDouble(mSIC06b);
                    if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                    if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                    if (cA1 * cB1 * cC1 != 0) mSIC07 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);

                    cA1 = Convert.ToDouble(mSIC04a.Split('~')[1]); cB1 = Convert.ToDouble(mSIC05a.Split('~')[1]); cC1 = Convert.ToDouble(mSIC06a.Split('~')[1]);
                    cA2 = Convert.ToDouble(mSIC04c); cB2 = Convert.ToDouble(mSIC05c); cC2 = Convert.ToDouble(mSIC06c);
                    if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                    if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                    if (cA1 * cB1 * cC1 != 0) mSIC07_1 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);
                }
                else
                {
                    if (tSDIFD004 == "其他")
                    {
                        if (TSIC2.Text + "" != "") { mSIC02 = Convert.ToDouble(TSIC2.Text + ""); } else { mSIC02 = 0; }
                        if (TSIC7.Text + "" != "") { mSIC07 = Convert.ToDouble(TSIC7.Text + ""); } else { mSIC07 = 0; }
                    }
                    else
                    {
                        double cA1 = Convert.ToDouble(mSIC04a), cB1 = Convert.ToDouble(mSIC05a), cC1 = Convert.ToDouble(mSIC06a);
                        double cA2 = Convert.ToDouble(mSIC04b), cB2 = Convert.ToDouble(mSIC05b), cC2 = Convert.ToDouble(mSIC06b);
                        if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                        if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                        if (cA1 * cB1 * cC1 != 0) mSIC07 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);
                    }
                }

                if (mSIC02 > 20 || mSIC02 < -20) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、構造物斷面及通水斷面之面積增加不超過20%或減少不超過10%，且不影響原構造物正常功能，否則應辦理變更設計。";
                if (mSIC07 > 20 || mSIC02 < -10) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、構造物斷面及通水斷面之面積增加不超過20%或減少不超過10%，且不影響原構造物正常功能，否則應辦理變更設計。";
                //if (mSIC02 > 20 || mSIC02 < -20) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、尺寸差異百分比不得增加不得超過20%，減少不得超過10%，否則應辦理變更設計。";
                //if (mSIC07 > 20 || mSIC02 < -10) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、尺寸差異百分比不得增加不得超過20%，減少不得超過10%，否則應辦理變更設計。";

                if (tSDIFD004 == "其他" || mSIC01r != mSIC01b || /*mSIC01r2 != mSIC01c ||*/ mSIC04r != mSIC04b || mSIC04r2 != mSIC04c || mSIC05r != mSIC05b || mSIC05r2 != mSIC05c || mSIC06r != mSIC06b || mSIC06r2 != mSIC06c || mSIC10a != mSIC10b || v == "A" || mSIC01Da != mSIC01Dr || mSIC04Da != mSIC04Dr)
                {
                    if (v == "A") 
					{ 
						exeSqlStr = " insert into SwcItemChk (SWC000,DTLRPNO,SDI001,DTLTYPE,SIC01,SIC01_1,SIC01D,SIC02,SIC02_1,SIC03,SIC04,SIC04_1,SIC04D,SIC05,SIC05_1,SIC06,SIC06_1,SIC07,SIC07_1,SIC08,SIC10,SaveUser,SaveDate) values (@SWC000,@DTLRPNO,@SDI001,'D3',@SIC01,@SIC01_1,@SIC01D,@SIC02,@SIC02_1,@SIC03,@SIC04,@SIC04_1,@SIC04D,@SIC05,@SIC05_1,@SIC06,@SIC06_1,@SIC07,@SIC07_1,@SIC08,@SIC10,@SaveUser,getdate());";
					}
					else 
					{ 
						if(mSIC10c)
						{
							exeSqlStr = " update SwcItemChk set SIC01=@SIC01,SIC01_1=@SIC01_1,SIC01D=@SIC01D,SIC02=@SIC02,SIC02_1=@SIC02_1,SIC03=@SIC03,SIC04=@SIC04,SIC04_1=@SIC04_1,SIC04D=@SIC04D,SIC05=@SIC05,SIC05_1=@SIC05_1,SIC06=@SIC06,SIC06_1=@SIC06_1,SIC07=@SIC07,SIC07_1=@SIC07_1,SIC08=@SIC08,SIC10=@SIC10,SaveUser=@SaveUser,SaveDate=getdate() where SWC000=@SWC000 and DTLRPNO=@DTLRPNO and SDI001=@SDI001; "; 
						}
					}
					if(exeSqlStr != "")
					{
						ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
						using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
						{
							SWCConn.Open();
	
							using (var cmd = SWCConn.CreateCommand())
							{
								cmd.CommandText = exeSqlStr;
								//設定值
								#region
								if (mSIC10b=="通過" && v == "A" && mSIC01r == mSIC01b && /*mSIC01r2 == mSIC01c &&*/ mSIC04r == mSIC04b && mSIC04r2 == mSIC04c && mSIC05r == mSIC05b && mSIC05r2 == mSIC05c && mSIC06r == mSIC06b && mSIC06r2 == mSIC06c && mSIC10a == mSIC10b && mSIC01Da == mSIC01Dr && mSIC04Da == mSIC04Dr)
									mSIC08 = ToSimpleUSDate(mSIC08b);
	
								cmd.Parameters.Add(new SqlParameter("@SWC000", mSWC000));
								cmd.Parameters.Add(new SqlParameter("@DTLRPNO", mDTLE001));
								cmd.Parameters.Add(new SqlParameter("@SDI001", mSDI001));
								cmd.Parameters.Add(new SqlParameter("@SIC01", mSIC01b));
								cmd.Parameters.Add(new SqlParameter("@SIC01_1", mSIC01c));
								cmd.Parameters.Add(new SqlParameter("@SIC01D", mSIC01Da));
								cmd.Parameters.Add(new SqlParameter("@SIC02", mSIC02));
								cmd.Parameters.Add(new SqlParameter("@SIC02_1", mSIC02_1));
								cmd.Parameters.Add(new SqlParameter("@SIC03", mSIC03));
								cmd.Parameters.Add(new SqlParameter("@SIC04", mSIC04b));
								cmd.Parameters.Add(new SqlParameter("@SIC04_1", mSIC04c));
								cmd.Parameters.Add(new SqlParameter("@SIC04D", mSIC04Da));
								cmd.Parameters.Add(new SqlParameter("@SIC05", mSIC05b));
								cmd.Parameters.Add(new SqlParameter("@SIC05_1", mSIC05c));
								cmd.Parameters.Add(new SqlParameter("@SIC06", mSIC06b));
								cmd.Parameters.Add(new SqlParameter("@SIC06_1", mSIC06c));
								cmd.Parameters.Add(new SqlParameter("@SIC07", mSIC07));
								cmd.Parameters.Add(new SqlParameter("@SIC07_1", mSIC07_1));
								cmd.Parameters.Add(new SqlParameter("@SIC08", mSIC08));
								cmd.Parameters.Add(new SqlParameter("@SIC10", mSIC10b));
								cmd.Parameters.Add(new SqlParameter("@SaveUser", ssUserID));
								#endregion
								cmd.ExecuteNonQuery();
								cmd.Cancel();
							}
						}
					}
                }

            }

        }
        if (nMSG01 != "") Response.Write("<script>alert('" + nMSG01 + "');</script>");
    }
    private string ToSimpleUSDate(string tDate)
    {
        string rValue = tDate;
        if(rValue !="")
        rValue = tDate.Replace(tDate.Substring(0, 3), (Int32.Parse(tDate.Substring(0, 3)) +1911).ToString());
        return rValue;
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL056", "TXTDTL057", "TXTDTL059", "TXTDTL061", "TXTDTL063", "TXTDTL065", "TXTDTL067", "TXTDTL070" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL056, TXTDTL057, TXTDTL059, TXTDTL061, TXTDTL063, TXTDTL065, TXTDTL067, TXTDTL070 };
        string csUpLoadField = "TXTDTL056";
        TextBox csUpLoadAppoj = TXTDTL056;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

        folderExists = Directory.Exists(SwcCaseFolderPath);
        if (folderExists == false)
        {
            Directory.CreateDirectory(SwcCaseFolderPath);
        }

        folderExists = Directory.Exists(SwcCaseFolderPath + CaseId);
        if (folderExists == false)
        {
            Directory.CreateDirectory(SwcCaseFolderPath + CaseId);
        }


        for (int i = 0; i < arryUpLoadField.Length; i++)
        {
            csUpLoadField = arryUpLoadField[i];
            csUpLoadAppoj = arryUpLoadAppoj[i];

            if (Session[csUpLoadField] + "" == "有檔案")
            {
                Boolean fileExists;
                string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;
                string SwcCaseFilePath = SwcCaseFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;

                fileExists = File.Exists(TempFilePath);
                if (fileExists)
                {
                    if (File.Exists(SwcCaseFilePath))
                    {
                        File.Delete(SwcCaseFilePath);
                    }
                    File.Move(TempFilePath, SwcCaseFilePath);
                }

            }
        }


    }
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
    }
    protected void GenerateDropDownList()
    {
        //2018-05-07，第五次工作會議加預設值...

        /*
        string[] array_DTL006 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作","無此項" };
        DDLDTL006.DataSource = array_DTL006;
        DDLDTL006.DataBind();
        DDLDTL006.SelectedValue = "依計畫施作";

        string[] array_DTL008 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL008.DataSource = array_DTL008;
        DDLDTL008.DataBind();
        DDLDTL008.SelectedValue = "依計畫施作";

        string[] array_DTL010 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL010.DataSource = array_DTL010;
        DDLDTL010.DataBind();
        DDLDTL010.SelectedValue = "依計畫施作";

        string[] array_DTL012 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL012.DataSource = array_DTL012;
        DDLDTL012.DataBind();
        DDLDTL012.SelectedValue = "依計畫施作";

        string[] array_DTL014 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL014.DataSource = array_DTL014;
        DDLDTL014.DataBind();
        DDLDTL014.SelectedValue = "依計畫施作";

        string[] array_DTL016 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL016.DataSource = array_DTL016;
        DDLDTL016.DataBind();
        DDLDTL016.SelectedValue = "依計畫施作";

        string[] array_DTL018 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL018.DataSource = array_DTL018;
        DDLDTL018.DataBind();
        DDLDTL018.SelectedValue = "依計畫施作";

        string[] array_DTL020 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL020.DataSource = array_DTL020;
        DDLDTL020.DataBind();
        DDLDTL020.SelectedValue = "依計畫施作";

        string[] array_DTL022 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL022.DataSource = array_DTL022;
        DDLDTL022.DataBind();
        DDLDTL022.SelectedValue = "依計畫施作";

        string[] array_DTL024 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL024.DataSource = array_DTL024;
        DDLDTL024.DataBind();
        DDLDTL024.SelectedValue = "依計畫施作";

        string[] array_DTL026 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL026.DataSource = array_DTL026;
        DDLDTL026.DataBind();
        DDLDTL026.SelectedValue = "依計畫施作";

        string[] array_DTL028 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL028.DataSource = array_DTL028;
        DDLDTL028.DataBind();
        DDLDTL028.SelectedValue = "依計畫施作";

        string[] array_DTL030 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL030.DataSource = array_DTL030;
        DDLDTL030.DataBind();
        DDLDTL030.SelectedValue = "依計畫施作";

        string[] array_DTL032 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL032.DataSource = array_DTL032;
        DDLDTL032.DataBind();
        DDLDTL032.SelectedValue = "依計畫施作";

        string[] array_DTL034 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL034.DataSource = array_DTL034;
        DDLDTL034.DataBind();
        DDLDTL034.SelectedValue = "依計畫施作";

        string[] array_DTL036 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL036.DataSource = array_DTL036;
        DDLDTL036.DataBind();
        DDLDTL036.SelectedValue = "依計畫施作";

        string[] array_DTL038 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL038.DataSource = array_DTL038;
        DDLDTL038.DataBind();
        DDLDTL038.SelectedValue = "依計畫施作";

        string[] array_DTL040 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        DDLDTL040.DataSource = array_DTL040;
        DDLDTL040.DataBind();
        DDLDTL040.SelectedValue = "依計畫施作";

        string[] array_DTL042 = new string[] { "", "是", "否" };
        DDLDTL042.DataSource = array_DTL042;
        DDLDTL042.DataBind();
        DDLDTL042.SelectedValue = "是";

        string[] array_DTL044 = new string[] { "", "是", "否" };
        DDLDTL044.DataSource = array_DTL044;
        DDLDTL044.DataBind();
        DDLDTL044.SelectedValue = "是";

        string[] array_DTL046 = new string[] { "", "是", "否" };
        DDLDTL046.DataSource = array_DTL046;
        DDLDTL046.DataBind();
        DDLDTL046.SelectedValue = "是";

        string[] array_DTL053 = new string[] { "", "是", "否", "其他" };
        DDLDTL053.DataSource = array_DTL053;
        DDLDTL053.DataBind();
        DDLDTL053.SelectedValue = "是";
        */
        TXTDTL055.Text = "(一) 檢查單位及人員：" + System.Environment.NewLine + "(二) 會同檢查單位及人員：" + System.Environment.NewLine + "(三) 承辦監造技師：" + System.Environment.NewLine + "(四) 水土保持義務人：";
    }
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RC" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RC" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLC000) AS MAXID from SWCDTL03 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLC000,7) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 3, 3)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(3, '0');
                }
            }
        }
        return _ReturnVal;
    }

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = LBSWC000.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名

            switch (ChkType)
            {
                case "PIC2":
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".odt", ".pdf", ".doc", ".docx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF, ODT, WORD 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 10 MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            switch (ChkType)
            {
                case "DOC":
                    if (filesize > 50000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10 Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
                default:
                    if (filesize > 10000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10 Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            
            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            //SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);            
            UpLoadText.Text = SwcFileName;

            // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
            // 使用 Path.Combine 來集合路徑的優點
            //  以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
            //  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
            //  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
            //  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合

            string serverFilePath = Path.Combine(serverDir, SwcFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(SwcFileName);
            int fileCount = 1;

            //while (File.Exists(serverFilePath))
            //{
            //    // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
            //    filename = string.Concat(fileNameOnly, "_", fileCount, extension);
            //    serverFilePath = Path.Combine(serverDir, filename);
            //    fileCount++;
            //}

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);
                string filestempPath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/";

                switch (ChkType)
                {
                    case "PIC2":
                        UpLoadLink.ImageUrl = filestempPath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.NavigateUrl = filestempPath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        break;

                    case "PIC":
                        UpLoadView.Attributes.Add("src", filestempPath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "DOC":
                        UpLoadText.Text = SwcFileName;
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = filestempPath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;

                }

            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }


        }
        else
        {
            Session[UpLoadStr] = "";
        }

    }
    protected void imagestitch(System.Web.UI.WebControls.Image UpLoadView, string sourcePath, int ShowWidth, int ShowHeight)
    {  //影像調整，處理照片顯示

        if (File.Exists(sourcePath))
        {
            System.Drawing.Image image = new Bitmap(sourcePath);

            int width = image.Width;
            int height = image.Height;

            int ShowUpPicWidth = 0;
            int ShowUpPicHeight = 0;

            if (width < height)
            {
                ShowUpPicWidth = Convert.ToInt32(width * ShowHeight / height);
                ShowUpPicHeight = ShowHeight;
            }
            else
            {
                ShowUpPicWidth = ShowWidth;
                ShowUpPicHeight = Convert.ToInt32(height * ShowWidth / width);
            }
            UpLoadView.Width = ShowUpPicWidth;
            UpLoadView.Height = ShowUpPicHeight;

            image.Dispose();
        }
    }
    protected void TXTDTL056_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL056_fileupload, TXTDTL056, "TXTDTL056", "_" + rDTLNO + "_03_sign", null, HyperLink056);
    }

    protected void TXTDTL057_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL057_fileupload, TXTDTL057, "TXTDTL057", "_" + rDTLNO + "_03_photo1", null, HyperLink057);

    }

    protected void TXTDTL059_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC2", TXTDTL059_fileupload, TXTDTL059, "TXTDTL059", "_" + rDTLNO + "_03_photo2", null, HyperLink059);

    }

    protected void TXTDTL061_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC2", TXTDTL061_fileupload, TXTDTL061, "TXTDTL061", "_" + rDTLNO + "_03_photo3", null, HyperLink061);

    }

    protected void TXTDTL063_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC2", TXTDTL063_fileupload, TXTDTL063, "TXTDTL063", "_" + rDTLNO + "_03_photo4", null, HyperLink063);

    }

    protected void TXTDTL065_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC2", TXTDTL065_fileupload, TXTDTL065, "TXTDTL065", "_" + rDTLNO + "_03_photo5", null, HyperLink065);

    }

    protected void TXTDTL067_fileuploadok_Click(object sender, EventArgs e)
    {

        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC2", TXTDTL067_fileupload, TXTDTL067, "TXTDTL067", "_" + rDTLNO + "_03_photo6", null, HyperLink067);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        Session["SaveOk"] = "N";
        SaveCase_Click(sender, e);
        string ssSaveOk = Session["SaveOk"]+"";
        if (ssSaveOk == "Y")
        {
            string sSWC000 = LBSWC000.Text;
            string sDTLC000 = LBDTL001.Text + "";

            string sEXESQLSTR = "";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from SWCDTL03 ";
                strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
                strSQLRV = strSQLRV + "   and DTLC000 = '" + sDTLC000 + "' ";

                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                if (readeSwc.HasRows)
                {
                    while (readeSwc.Read())
                    {
                        string sDATALOCK = readeSwc["DATALOCK"] + "";
                        if (sDATALOCK == "Y")
                        {
                            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='SWCDT003v2.aspx?SWCNO=" + sSWC000 + "&DTLNO=" + sDTLC000 + "'; </script>");
                            return;
                        }
                    }
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                sEXESQLSTR = sEXESQLSTR + " Update SWCDTL03 Set ";
                sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
                sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
                sEXESQLSTR = sEXESQLSTR + "   and DTLC000 = '" + sDTLC000 + "'";

                SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
            SendMailNotice(sSWC000);
            string vCaseID = Request.QueryString["SWCNO"] + "";
            Response.Write("<script>location.href='SWC003.aspx?SWCNO=" + vCaseID + "';</script>");
        }
    }
    private void SendMailNotice(string gSWC000)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();
        string pgSWC002 = LBSWC002.Text;
        string pgSWC005 = LBSWC005.Text;
        string pgSWC012 = LBSWC012.Text;
        string pgSWC013TEL = LBSWC013TEL.Text; 
        string pgSWC025 = LBSWC025.Text;
        string pgDTL048 = TXTDTL048.Text;
        string pgDTL049 = TXTDTL049.Text;
        string pgDTL051 = TXTDTL051.Text;
        string pgDTL074 = TXTDTL074.Text; 

        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";

        //TextBox1.Text = strUserName;
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);

        string pgDTL071 = DDLDTL071.Text;   //檢查結果

        string ssMailSub01 = "", ssMailBody01 = "";
        string SentMailGroup = "";
        switch (pgDTL071) {
            case "尚未施工":
            case "依計畫施作":
            case "完工":
                //通知人：許巽舜(gv-hsun)
                //內容：轄區【水土保持計畫】已新增施工監造紀錄，監造結果為【尚未施工/依計畫施作/完工】請上管理平台查看
                SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aUserId.Trim() == "gv-hsun")
                        SentMailGroup += ";;" + aUserMail;
                }
                ssMailSub01 = pgSWC012 + "水土保持計畫【" + pgSWC002 + "】已新增施工檢查紀錄";
                ssMailBody01 = pgSWC012 + "【" + pgSWC005 + "】已新增施工檢查紀錄，檢查結果為【" + pgDTL071 + "】請上管理平台查看。" + "<br><br>";
                ssMailBody01 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                string[] arraySentMail01a = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                bool MailTo01a = SBApp.Mail_Send(arraySentMail01a, ssMailSub01, ssMailBody01);
                break;
            case "有施工缺失改善中":
                //通知人：科長、正工程司、股長、管理者、承辦人員、沈漢國(ge-10755)
                //內容：轄區【水土保持計畫】已新增監造紀錄，監造結果為【有施工缺失改善中】改正期限為【改正期限(二)】，缺失項目如下：【二、通知水土保持義務人及營造單位施工缺失改正事項欄位資料】
                SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工程司" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == pgSWC025.Trim() || aUserId.Trim() == "ge-10755")
                        SentMailGroup += ";;" + aUserMail;
                }
                string[] arraySentMail01b = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                ssMailSub01 = pgSWC012 + "水土保持計畫【" + pgSWC002 + "】已新增施工檢查紀錄";
                ssMailBody01 = pgSWC012 + "【" + pgSWC005 + "】已新增施工檢查紀錄，檢查結果為【" + pgDTL071 + "】改正期限為【" + pgDTL049 + "】，缺失項目如下：【" + pgDTL048 + "】" + "<br><br>";
                ssMailBody01 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                bool MailTo01b = SBApp.Mail_Send(arraySentMail01b, ssMailSub01, ssMailBody01);
                break;
            case "缺失應改未改或未依計畫施工，通知大地處查處":
                //通知人：科長、正工程司、股長、管理者、承辦人員、沈漢國(ge-10755)
                //內容：轄區【水土保持計畫】已新增監造紀錄，監造結果為【缺失應改未改或未依計畫施工，通知大地處查處】改正期限為【改正期限(八)】，未依計畫施作項目如下：【八、未依計畫施作事項及改正期限欄位
                SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工程司" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == pgSWC025.Trim() || aUserId.Trim() == "ge-10755")
                        SentMailGroup += ";;" + aUserMail;
                }
                string[] arraySentMail01c = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                ssMailSub01 = pgSWC012 + "水土保持計畫【" + pgSWC002 + "】已新增施工檢查紀錄";
                ssMailBody01 = pgSWC012 + "【" + pgSWC005 + "】已新增施工檢查紀錄，檢查結果為【" + pgDTL071 + "】改正期限為【" + pgDTL074 + "】，未依計畫施作項目如下：【" + pgDTL051 + "】" + "<br><br>";
                ssMailBody01 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                bool MailTo01c = SBApp.Mail_Send(arraySentMail01c, ssMailSub01, ssMailBody01);
                //通知人：監造技師email、義務人(簡訊)、聯絡人(email)、檢查公會(email)
                //內容：您好，【水土保持計畫】已新增監造紀錄，監造結果為【缺失應改未改或未依計畫施工，通知大地處查處】請於【改正期限(八)】前改正，未依計畫施作項目如下：【八、未依計畫施作事項及改正期限欄位】
                string ssMailBody03 = "您好，【" + pgSWC005 + "】已新增監造紀錄，監造結果為【缺失應改未改或未依計畫施工，通知大地處查處】請於【"+ pgDTL074 + "】前改正，未依計畫施作項目如下：【"+ pgDTL051 + "】。";
                
				string[] arraySWC013TEL = pgSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
				SBApp.SendSMS_Arr(arraySWC013TEL, ssMailBody03);
                break;
        }
    }

    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csCaseID2 = LBDTL001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCDTL03 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLC000 = '" + csCaseID2 + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

        string DelFileName = ImgText.Text;
        string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + ImgText.Text;

        try
        {
            if (File.Exists(TempFileFullPath))
            {
                File.Delete(TempFileFullPath);
            }
        }
        catch
        {
        }
        try
        {
            if (File.Exists(FileFullPath))
            {
                File.Delete(FileFullPath);
            }
        }
        catch
        {
        }

        switch (DelType)
        {
            case "PIC":
                //ImgView.Attributes.Clear();
                FileLink.ImageUrl = null;
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                ImgView.Width = NoneWidth;
                ImgView.Height = NoneHeight;
                break;
            case "CAD":
            case "PDF":
            case "DOC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                FileLink.Visible = false;
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }
    protected void TXTDTL056_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL056, TXTDTL056_img, HyperLink056, "DTLC056", "TXTDTL056", 320, 180);
    }
    protected void TXTDTL057_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL057, TXTDTL057_img, HyperLink057, "DTLC057", "TXTDTL057", 320, 180);
    }
    protected void TXTDTL059_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL059, TXTDTL059_img, HyperLink059, "DTLC059", "TXTDTL059", 320, 180);
    }

    protected void TXTDTL061_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL061, TXTDTL061_img, HyperLink061, "DTLC061", "TXTDTL061", 320, 180);
    }

    protected void TXTDTL063_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL063, TXTDTL063_img, HyperLink063, "DTLC063", "TXTDTL063", 320, 180);
    }

    protected void TXTDTL065_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL065, TXTDTL065_img, HyperLink065, "DTLC065", "TXTDTL065", 320, 180);
    }

    protected void TXTDTL067_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL067, TXTDTL067_img, HyperLink067, "DTLC067", "TXTDTL067", 320, 180);
    }

    protected void TXTDTL070_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTDTL070_fileupload, TXTDTL070, "TXTDTL070", "_" + rDTLNO + "_03_doc1", null, Link070);
    }

    protected void TXTDTL070_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTDTL070, null, Link070, "DTLC070", "TXTDTL070", 320, 180);

    }

    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        Response.Redirect("../SwcReport/PdfSwcDtl03.aspx?SWCNO=" + rCaseId + "&DTLNO="+ rDTLId);
    }

    private void SetDtlData(string rSWCNO, string v2)
    {
        GBClass001 SBApp = new GBClass001();
        bool bb = true;

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //分段驗收核定項目
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
                bb = false;
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
                string sSDI017 = readerItem["SDI017"] + "";
                string sSDI018 = readerItem["SDI018"] + "";
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
                    SDITB.Columns.Add(new DataColumn("SDIFD017", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD018", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDILB019", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK009", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK010", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }
                if (sSDI017 != "完成") sSDI017 = "未完成";
                if (sSDI019 == "是" && sSDI005 != "其他")
                {
                    //sSDI006 = sSDI006 + "~" + sSDI006_1;
					sSDI006 = sSDI006;
                    sSDI012 = sSDI012 + "~" + sSDI012_1;
                    sSDI013 = sSDI013 + "~" + sSDI013_1;
                    sSDI014 = sSDI014 + "~" + sSDI014_1;
                }

                string sSIC002 = "-";
                string sSIC007 = "-";

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
                SDITBRow["SDIFD012_1"] = sSDI012_1;
                SDITBRow["SDIFD012D"] = sSDI012D;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD013_1"] = sSDI013_1;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD014_1"] = sSDI014_1;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDIFD017"] = sSDI017;
                SDITBRow["SDIFD018"] = sSDI018;
                SDITBRow["SDICHK001"] = "";
                SDITBRow["SDICHK002"] = "-";
                SDITBRow["SDICHK004"] = "";
                SDITBRow["SDICHK004D"] = sSDI012D;
                SDITBRow["SDICHK005"] = "";
                SDITBRow["SDICHK006"] = "";
                SDITBRow["SDICHK007"] = "-";
                SDITBRow["SDICHK008"] = "";// "-";
                SDITBRow["SDICHK009"] = sSDI017;
                SDITBRow["SDICHK010"] = sSDI018;

                tbSDIVS.Rows.Add(SDITBRow);

                //DB
                if (v2 == "AddNew")
                {
                    using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                    {
                        ItemConnS.Open();

                        string strSQLRVS = "select MAX(DTLC001) as MAXKEY from SWCDTL03";
                        strSQLRVS += " Where SWC000 = '" + rSWCNO + "' ";
                        strSQLRVS += "   AND DATALOCK='Y' ";

                        SqlDataReader readerItemS;
                        SqlCommand objCmdItemS = new SqlCommand(strSQLRVS, ItemConnS);
                        readerItemS = objCmdItemS.ExecuteReader();

                        while (readerItemS.Read()) { v2 = readerItemS["MAXKEY"] + ""; }
                    }

                }

                using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                {
                    ItemConnS.Open();

                    string tSIC01 = "";
                    string tSIC01_1 = "";
                    string tSIC01D = "";
                    string tSIC02 = "";
                    string tSIC02_1 = "";
                    string tSIC03 = "";
                    string tSIC04 = "";
                    string tSIC04_1 = "";
                    string tSIC04D = "";
                    string tSIC05 = "";
                    string tSIC05_1 = "";
                    string tSIC06 = "";
                    string tSIC06_1 = "";
                    string tSIC07 = "";
                    string tSIC07_1 = "";
                    string tSIC08 = "";
                    string tSIC09 = "未完成";
                    string tSIC10 = "";

                    string strSQLRVS = "select * from SwcItemChk";
                    strSQLRVS += " Where SWC000 = '" + rSWCNO + "' ";
                    strSQLRVS += "   and DTLRPNO = '" + v2 + "' ";
                    strSQLRVS += "   and SDI001 = '" + sSDI001 + "' ";

                    SqlDataReader readerItemS;
                    SqlCommand objCmdItemS = new SqlCommand(strSQLRVS, ItemConnS);
                    readerItemS = objCmdItemS.ExecuteReader();

                    while (readerItemS.Read())
                    {
                        tSIC01 = readerItemS["SIC01"] + "";
                        tSIC01_1 = readerItemS["SIC01_1"] + "";
                        tSIC01D = readerItemS["SIC01D"] + "";
                        tSIC02 = readerItemS["SIC02"] + "";
                        tSIC02_1 = readerItemS["SIC02_1"] + "";
                        tSIC03 = readerItemS["SIC03"] + "";
                        tSIC04 = readerItemS["SIC04"] + "";
                        tSIC04_1 = readerItemS["SIC04_1"] + "";
                        tSIC04D = readerItemS["SIC04D"] + "";
                        tSIC05 = readerItemS["SIC05"] + "";
                        tSIC05_1 = readerItemS["SIC05_1"] + "";
                        tSIC06 = readerItemS["SIC06"] + "";
                        tSIC06_1 = readerItemS["SIC06_1"] + "";
                        tSIC07 = readerItemS["SIC07"] + "";
                        tSIC07_1 = readerItemS["SIC07_1"] + "";
                        tSIC08 = readerItemS["SIC08"] + "";
                        tSIC09 = readerItemS["SIC09"] + "";
                        tSIC10 = readerItemS["SIC10"] + "";

                        tSIC02 = Math.Round(Convert.ToDecimal(tSIC02), 2, MidpointRounding.AwayFromZero).ToString();
                        tSIC07 = Math.Round(Convert.ToDecimal(tSIC07), 2, MidpointRounding.AwayFromZero).ToString();
                    }
                    DataRow SDITBRow2 = tbSDIVS.NewRow();

                    SDITBRow2["SDIFDNI"] = ni.ToString();
                    SDITBRow2["SDIFD001"] = sSDI001;
                    SDITBRow2["SDIFD002"] = sSDI002;
                    SDITBRow2["SDIFD003"] = "";
                    SDITBRow2["SDIFD004"] = "";
                    SDITBRow2["SDIFD005"] = "";
                    SDITBRow2["SDIFD006"] = sSDI006;
                    SDITBRow2["SDIFD006D"] = sSDI006D;
                    SDITBRow2["SDIFD007"] = sSDI007;
                    SDITBRow2["SDIFD008"] = sSDI008;
                    SDITBRow2["SDIFD009"] = sSDI009;
                    SDITBRow2["SDIFD010"] = sSDI010;
                    SDITBRow2["SDIFD011"] = sSDI011;
                    SDITBRow2["SDIFD012"] = sSDI012;
                    SDITBRow2["SDIFD012D"] = sSDI012D;
                    SDITBRow2["SDIFD013"] = sSDI013;
                    SDITBRow2["SDIFD014"] = sSDI014;
                    SDITBRow2["SDIFD015"] = sSDI015;
                    SDITBRow2["SDIFD016"] = sSDI016;
                    SDITBRow2["SDIFD017"] = "";
                    SDITBRow2["SDIFD018"] = tSIC10;
                    SDITBRow2["SDILB019"] = sSDI019;
                    SDITBRow2["SDICHK001"] = tSIC01;
                    SDITBRow2["SDICHK001_1"] = tSIC01_1;
                    SDITBRow2["SDICHK001D"] = tSIC01D;
                    SDITBRow2["SDICHK002"] = tSIC02;
                    SDITBRow2["SDICHK002_1"] = tSIC02_1;
                    SDITBRow2["SDICHK004"] = tSIC04;
                    SDITBRow2["SDICHK004_1"] = tSIC04_1;
                    SDITBRow2["SDICHK004D"] = tSIC04D;
                    SDITBRow2["SDICHK005"] = tSIC05;
                    SDITBRow2["SDICHK005_1"] = tSIC05_1;
                    SDITBRow2["SDICHK006"] = tSIC06;
                    SDITBRow2["SDICHK006_1"] = tSIC06_1;
                    SDITBRow2["SDICHK007"] = tSIC07;
                    SDITBRow2["SDICHK007_1"] = tSIC07_1;
                    SDITBRow2["SDICHK008"] = SBApp.DateView(tSIC08, "04");
                    SDITBRow2["SDICHK009"] = tSIC09.Trim();
                    SDITBRow2["SDICHK010"] = tSIC10.Trim();

                    tbSDIVS.Rows.Add(SDITBRow2);
                }

                ViewState["SwcDocItem"] = tbSDIVS;

                SDIList.DataSource = tbSDIVS;
                SDIList.DataBind();

                //TXTSDINI.Text = ni.ToString();
            }
            readerItem.Close();
        }
        GVMSG.Visible = bb;
    }
    protected void SDIList_DataBound(object sender, EventArgs e)
    {
        int aaaaaa = 0;

        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            Label LBIT06 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("SDILB006");
            Label LBIT06D = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("SDILB006D");
            TextBox CHK01 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001");
            Label A1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A1");
            TextBox CHK01_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001_1");
            TextBox CHK01D = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001D");
            TextBox CHK04 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004");
            Label A2 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A2");
            TextBox CHK04_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004_1");
            TextBox CHK04D = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004D");
            TextBox CHK05 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK005");
            Label A3 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A3");
            TextBox CHK05_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK005_1");
            TextBox CHK06 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK006");
            Label A4 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A4");
            TextBox CHK06_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK006_1");
            Label LabelX1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LabelX1");
            Label LabelX2 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LabelX2");
            TextBox TXTCHK002 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK002");
            TextBox TXTCHK002_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK002_1");
            TextBox TXTCHK007 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK007");
            TextBox TXTCHK007_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK007_1");
            Label LBCHK002 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002");
            Label LBCHK002_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002_1");
            Label LBCHK002pers = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002pers");
            Label LBCHK002pers_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002pers_1");
            Label ITNONE03 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE03");
            Label ITNONE04 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE04");
            Label LB004D = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB004D");
            Label ITNONE05 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE05");
            Label LBCHK007 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007");
            Label LBCHK007_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007_1");
            Label LBCHK007pers = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007pers");
            Label LBCHK007pers_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007pers_1");
            Label LB019 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB019");

            if (++aaaaaa % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 2].Cells[3].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");
                DropDownList DDLPASS = (DropDownList)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("DDLPASS");
                TextBox RCH010 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("RCH10");
                Button MODIFYDATA = (Button)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("MODIFYDATA");

                Label A1_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A1");
                Label A2_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A2");
                Label A3_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A3");
                Label A4_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A4");
                TextBox CHK01_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK001_1");
                TextBox CHK04_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK004_1");
                TextBox CHK05_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK005_1");
                TextBox CHK06_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK006_1");

                LBIT06.Visible = false; LBIT06D.Visible = false;
                ITNONE03.Visible = false;
                ITNONE04.Visible = false;
                ITNONE05.Visible = false;
                LB004D.Visible = false;
                DDLPASS.Visible = true;
				
				//水土保持設施類別非臨時開頭移除"已由永久設施取代"
                if (SDIList.Rows[aaaaaa - 2].Cells[1].Text.Substring(0, 2) != "臨時")
                {
                    DDLPASS.Items.Remove("已由永久設施取代");
                }

                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; CHK06.Enabled = true; CHK06_1_onoff.Enabled = true; break; }
                }
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = false; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; CHK06.Enabled = true; CHK06_1_onoff.Enabled = false; break; }
                }
				
                if (LB019.Text == "是" && tSDIFD004 != "其他") { A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; }
                else { A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; A2_onoff.Visible = false; CHK04_1_onoff.Visible = false; A3_onoff.Visible = false; CHK05_1_onoff.Visible = false; A4_onoff.Visible = false; CHK06_1_onoff.Visible = false; }

                string testvalue = RCH010.Text;
                if (RCH010.Text == "通過" || RCH010.Text == "已由永久設施取代") { CHK01D.Enabled = false; CHK04D.Enabled = false; MODIFYDATA.Visible = true; CHK01.Enabled = false; CHK01_1_onoff.Enabled = false; CHK04.Enabled = false; CHK04_1_onoff.Enabled = false; CHK05.Enabled = false; CHK05_1_onoff.Enabled = false; CHK06.Enabled = false; CHK06_1_onoff.Enabled = false; DDLPASS.Enabled = false; TXTCHK002.Enabled = false; TXTCHK002_1.Enabled = false; TXTCHK007.Enabled = false; TXTCHK007_1.Enabled = false; } else { CHK01D.Enabled = true; CHK04D.Enabled = true; }
				
				
                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = true; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; CHK06.Visible = true; CHK06_1_onoff.Visible = true; break; }
                    /*LBCHK002_1.Visible = true; LBCHK002pers_1.Visible = true;*/
					LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false; 
					LBCHK007_1.Visible = true; LBCHK007pers_1.Visible = true;
                }
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = false; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; CHK06.Visible = true; CHK06_1_onoff.Visible = false; break; }
                    LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false; LBCHK007_1.Visible = false; LBCHK007pers_1.Visible = false;
                }
				
				if (tSDIFD004 == "其他") { CHK01.Visible = false; CHK01D.Visible = true; CHK04D.Visible = true; CHK04.Visible = false; CHK05.Visible = false; CHK06.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; CHK04D.Visible = true; LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = true; TXTCHK007_1.Visible = false; LBCHK002.Visible = false; LBCHK002_1.Visible = false; TXTCHK002.Visible = true; TXTCHK002_1.Visible = false; }
                else { CHK01.Visible = true; CHK01D.Visible = false; CHK04.Visible = true; CHK05.Visible = true; CHK06.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; CHK04D.Visible = false; LBCHK007.Visible = true; LBCHK007_1.Visible = false; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK002.Visible = true; LBCHK002_1.Visible = false; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; }
				
            }
            else
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 1].Cells[3].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");

                CHK01.Visible = false; A1.Visible = false; CHK01_1.Visible = false; CHK01D.Visible = false;
                CHK04.Visible = false; A2.Visible = false; CHK04_1.Visible = false; CHK04D.Visible = false;
                CHK05.Visible = false; A3.Visible = false; CHK05_1.Visible = false;
                CHK06.Visible = false; A4.Visible = false; CHK06_1.Visible = false;

                switch (HDF011.Value) { case "1": ITNONE04.Text = "-"; ITNONE05.Text = "-"; break; case "2": ITNONE05.Text = "-"; break; }
                LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK007pers.Visible = false; LBCHK007pers_1.Visible = false; LBCHK002.Visible = false; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; LBCHK002pers.Visible = false; LBCHK002pers_1.Visible = false;
                if (tSDIFD004 == "其他") { LBIT06.Visible = false; LBIT06D.Visible = true; LB004D.Visible = true; ITNONE03.Visible = false; ITNONE04.Visible = false; ITNONE05.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; }
                else { LBIT06.Visible = true; LBIT06D.Visible = false; LB004D.Visible = false; ITNONE03.Visible = true; ITNONE04.Visible = true; ITNONE05.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; }
            }
            ITNONE03.Text = Server.HtmlDecode(ITNONE03.Text);
            ITNONE04.Text = Server.HtmlDecode(ITNONE04.Text);
            ITNONE05.Text = Server.HtmlDecode(ITNONE05.Text);
            LBIT06.Text = Server.HtmlDecode(LBIT06.Text);
        }

    }

    protected void SDIList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "Modify":
                int aa = Convert.ToInt32(e.CommandArgument);

                HiddenField HDF011 = (HiddenField)SDIList.Rows[aa].Cells[0].FindControl("HDSDI011");
                TextBox CHK01 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001");
                TextBox CHK01_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001_1");
                TextBox CHK01D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001D");
                TextBox CHK04 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006_1");
                DropDownList DDLPASS = (DropDownList)SDIList.Rows[aa].Cells[3].FindControl("DDLPASS");
                TextBox CHK02 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK002");
                TextBox CHK07 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK007");

                CHK01.Enabled = true; CHK01_1.Enabled = true; CHK02.Enabled = true; CHK07.Enabled = true;
                switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; CHK06.Enabled = true; CHK06_1.Enabled = true; break; }
                DDLPASS.Enabled = true;
                CHK01D.Enabled = true; CHK04D.Enabled = true;
                break;
        }

    }
    private bool canDoModify(string pSWC000, string pDTLF000)
    {
        Class1 C1 = new Class1();
        bool rValue = true;
        string ssUserID = Session["ID"] + "";

        string sqlStr = " select * from SWCDTL03 Where SWC000=@SWC000 and DTLC000=@DTLC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                cmd.Parameters.Add(new SqlParameter("@DTLC000", pDTLF000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        string tmpDataLock = readerTslm["DATALOCK"] + "";
                        //string tmpDataLock2 = readerTslm["DATALOCK2"] + "";
                        string tmpSaveuser = readerTslm["Saveuser"] + "";

                        if (tmpDataLock == "Y")
                            rValue = false;
                        //else if (tmpDataLock2 != "Y" && tmpSaveuser != ssUserID)
                        //    rValue = false;
                        else if (tmpSaveuser != ssUserID && C1.getSWCSWCData(pSWC000,"SWC024ID") !=ssUserID)
                            rValue = false;

                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    //網頁簽名
    protected void TXTDTL05602_Signsture_Click(object sender, EventArgs e)
    {
        string base64 = hfImageData.Value.ToString().Split(',')[1];
        byte[] bytes = Convert.FromBase64String(base64);
        string CaseId = LBSWC000.Text + "";
        string rDTLNO = LBDTL001.Text + "";
        string QQ = CaseId + "_" + rDTLNO + "_03_sign.jpg";
        string filestempPath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/";
        string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId + "/";

        if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

        string filePath = serverDir + QQ;

        File.WriteAllBytes(filePath, bytes);

        TXTDTL056.Text = QQ;
        HyperLink056.ImageUrl = filestempPath + CaseId + "/" + QQ + "?ts=" + System.DateTime.Now.Millisecond;
        HyperLink056.NavigateUrl = filestempPath + CaseId + "/" + QQ + "?ts=" + System.DateTime.Now.Millisecond;
        HyperLink056.Visible = true;
    }

    //找尋最新監造計畫(監造日期 & 工程進度)
    public string FindDTEL088(string qSWC000)
    {
        string Q = "";
        string QQ = "";
        string rValue = "";
        string sqlStr = " select TOP(1)DTLE088, DTLE003 FROM  SWCDTL05 WHERE (SWC000 = '" + qSWC000 + "') ORDER BY id DESC; ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection HSIConn = new SqlConnection(connectionString.ConnectionString))
        {
            HSIConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(sqlStr, HSIConn);
            readerData = objCmdRV.ExecuteReader();

            if (!readerData.HasRows)
            {
                rValue = "123";
            }

            while (readerData.Read())
            {
                Q = readerData["DTLE088"] + "";
                QQ = readerData["DTLE003"] + "";
            }
            readerData.Close();
            objCmdRV.Dispose();
        }

        if (Q != "")
        {
            rValue = Q.Replace("~", "至") + "施工進度" + QQ + "％";
        }

        return rValue;
    }
	
	protected void UploadAndInsert(string NO, string IMAGE, string IDENTITY, string NAME)
	{
		string rCaseId = Request.QueryString["SWCNO"] + "";
		string rDTLNO = LBDTL001.Text + "";
		NO = ("0" + NO).Substring(("0" + NO).Length-2,2);
		
		string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + rCaseId;
		
		if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
		
		string SwcFileName = rCaseId + "_" + rDTLNO + "_03"+NO+"_sign.png";
		
		string serverFilePath = Path.Combine(serverDir, SwcFileName);
		
		
		//---------------------------------------
		string dir = serverDir;
		bool dirExists = Directory.Exists(dir);
		if (!dirExists)
			Directory.CreateDirectory(dir);
		string SavePath = serverFilePath;
		var bytes = Convert.FromBase64String(IMAGE.Replace("data:image/png;base64,",""));
		using (var imageFile = new FileStream(SavePath, FileMode.Create))
		{
			imageFile.Write(bytes, 0, bytes.Length);
			imageFile.Flush();
		}
		//---------------------------------------
		
		string sqlStr = " insert into SWCDTL03_IMAGE(SWC000,DTLC000,NO,[IDENTITY],NAME,IMAGENAME) values (@SWC000,@DTLC000,@NO,@IDENTITY,@NAME,@IMAGENAME); ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", rCaseId));
                cmd.Parameters.Add(new SqlParameter("@DTLC000", rDTLNO));
                cmd.Parameters.Add(new SqlParameter("@NO", NO));
                cmd.Parameters.Add(new SqlParameter("@IDENTITY", IDENTITY));
                cmd.Parameters.Add(new SqlParameter("@NAME", NAME));
                cmd.Parameters.Add(new SqlParameter("@IMAGENAME", SwcFileName));
				
				
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
	}
	
	protected void DeleteIMAGE()
	{
		string rCaseId = Request.QueryString["SWCNO"] + "";
		string rDTLNO = LBDTL001.Text + "";
		
		string sqlStr = " delete SWCDTL03_IMAGE where SWC000=@SWC000 and DTLC000=@DTLC000; ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", rCaseId));
                cmd.Parameters.Add(new SqlParameter("@DTLC000", rDTLNO));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
	}
}