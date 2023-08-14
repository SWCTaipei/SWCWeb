using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply003 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";
        string rPrevious = Request.QueryString["PV"] + "";

        GBClass001 SBApp = new GBClass001();

        if (!IsPostBack)
        {
            SBApp.ViewRecord("開工期限或復工期限展延", "view", "");
            if (rOLANO == "") { Response.Redirect("SWC001.aspx"); }
            else
                GetOLA02Data(rSWCNO, rOLANO);
        }

        //全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用

    }
	
	protected void Page_LoadComplete(object sender, EventArgs e) 
    {
        //判斷顯示開工或復工
        LBSWC004_1.Text = LBSWC004.Text;
        LBSWC004_2.Text = LBSWC004.Text;
        LBSWC004_3.Text = LBSWC004.Text;
    }

    private void GetOLA02Data(string v, string v2)
    {
        string ssONLINEAPPLY = Session["ONLINEAPPLY"] + "";

        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";
        string DataLock2 = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC000 = readeSwc["SWC000"] + "";
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;

            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string rONA000 = GetONAID();
                TXTONA001.Text = rONA000;
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply03 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA03001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA03002"] + "";
                    string tONA003 = readeONA["ONA03003"] + "";
                    string tONA004 = readeONA["ONA03004"] + "";
                    string tONA005 = readeONA["ONA03005"] + "";
                    string tONA006 = readeONA["ONA03006"] + "";
                    string tONA007 = readeONA["ONA03007"] + "";
                    string tONA008 = readeONA["ONA03008"] + "";
                    string tONA009 = readeONA["ONA03009"] + "";
                    string tONA010 = readeONA["ONA03010"] + "";
                    string tONA011 = readeONA["ONA03011"] + "";

                    string tReviewResults = readeONA["ReviewResults"] + "";
                    string tResultsExplain = readeONA["ResultsExplain"] + "";
                    string tReviewDoc = readeONA["ReviewDoc"] + "";
                    string tLOCKUSER2 = readeONA["LOCKUSER2"] + "";
                    string tReviewDirections = readeONA["ReviewDirections"] + "";
                    string tReSendDeadline = readeONA["ReSendDeadline"] + "";

                    tDATALOCK = readeONA["DATALOCK"] + "";
                    DataLock2 = readeONA["DATALOCK2"] + "";

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = SBApp.DateView(tONA003, "00");
                    DDLONA004.Text = tONA004;
                    TXTONA005.Text = tONA005;
                    if (tONA007 == "1") { LBONA007.Visible = true; }
                    if (tONA008 == "1") { LBONA008.Visible = true; }
                    if (tONA009 == "1") { LBONA009.Visible = true; }
                    if (tONA010 == "1") { LBONA010.Visible = true; }
					LBSWC004.Text = tONA011 == "" ? "開工/復工" : tONA011;

                    if (tReviewResults == "1") { CHKRRa.Checked = true; LBRR.Text = "審查結果：准予通過"; LBResultsExplain.Text = tResultsExplain; }
                    if (tReviewResults == "0") { CHKRRb.Checked = true; LBRR.Text = "審查結果：駁回"; LBResultsExplain.Text = tResultsExplain; }
                    if (tReviewResults == "2") { CHKRRc.Checked = true; LBRR.Text = "審查結果：退補正"; LBResultsExplain.Text = tReviewDirections + "<br>補正期限：" + SBApp.DateView(tReSendDeadline, "00"); }
                    if (tReviewResults == "1" && DataLock2 == "Y") { OutPdf.Visible = true; }

                    ResultsExplain.Text = tResultsExplain;
                    TXTReviewDoc.Text = tReviewDoc;
                    ReviewDirections.Text = tReviewDirections;
                    TXTDeadline.Text = SBApp.DateView(tReSendDeadline, "00");
                    ReviewID.Text = SBApp.GetGeoUser(tLOCKUSER2, "Name");

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tReviewDoc };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { LinkReviewDoc };

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
                            Class1 C1 = new Class1();
                            C1.FilesSortOut(strFileName, v, "");
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                }
            }
        }
        string ssUserType = Session["UserType"] + "";
        if (ssUserType == "03" && DataLock2 != "Y")
        {
            LBResultsExplain.Visible = false;
            ReviewResults.Visible = true;
            DataLock.Visible = true;
            SaveCase.Visible = true;
        }
        if (DataLock2 == "Y")
        {
            ReViewUL.Visible = false;
            ResultsExplain.Visible = false;
            ReviewResults.Visible = true;
            LBRR.Visible = true;
            CHKRRa.Visible = false;
            CHKRRb.Visible = false;
            DataLock.Visible = false;
            SaveCase.Visible = false;
            Panel1.Visible = false;
        }
        if (ssONLINEAPPLY != "Y" && DataLock2 != "Y")
            ReviewResults.Visible = false;
		
		//為了不讓簽核內容及按鈕出現
		ResultsExplain.Visible = false;
		DataLock.Visible = false;
		SaveCase.Visible = false;
		
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        string rPrevious = Request.QueryString["PV"] + "";

        if (rPrevious == "4") { Response.Redirect("SWC004.aspx?SWCNO=" + vCaseID); } else { Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID); }
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string rPrevious = Request.QueryString["PV"] + "";

        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA03001 = TXTONA001.Text + "";
        string sReviewDoc = TXTReviewDoc.Text + "";
        string sResultsExplain = ResultsExplain.Text + "";
        string sReviewResults = CHKRRa.Checked ? "1" : CHKRRb.Checked ? "0" : CHKRRc.Checked ? "2" : "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text;
        
        string sEXESQLUPD = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply03 ";
            strSQLRV = strSQLRV + " where ONA03001 = '" + sONA03001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
                sEXESQLUPD = " INSERT INTO OnlineApply03 (SWC000,SWC002,ONA03001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA03001 + "');";
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply03 Set ";
            sEXESQLUPD = sEXESQLUPD + " ReviewResults = '" + sReviewResults + "', ";
            sEXESQLUPD = sEXESQLUPD + " ResultsExplain = '" + sResultsExplain + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDoc = '" + sReviewDoc + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDirections = '" + sReviewDirections + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReSendDeadline = '" + sReSendDeadline + "', ";

            sEXESQLUPD = sEXESQLUPD + " LOCKUSER2 = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " LOCKDATE = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA03001 = '" + sONA03001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    if (rPrevious == "4") { Response.Write("<script>alert('資料已存檔');location.href='SWC004.aspx?SWCNO=" + sSWC000 + "';</script>"); } else { Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>"); }
                    break;
            }
        }

    }
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA02" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA02" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA03001) AS MAXID from OnlineApply03 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA03001,9) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 6, 6)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(6, '0');
                }
            }

        }
        return _ReturnVal;
    }

    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTReviewDoc" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTReviewDoc };
        string csUpLoadField = "TXTReviewDoc";
        TextBox csUpLoadAppoj = TXTReviewDoc;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

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
    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
        SaveCase_Click(sender, e);
        string rPrevious = Request.QueryString["PV"] + "";
        string ssUserID = Session["ID"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sONA03001 = TXTONA001.Text + "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply03 Set ";
            if (CHKRRc.Checked) { sEXESQLSTR += " DATALOCK='',LOCKUSER='',LOCKDATE=null, "; }
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '" + ssUserID + "' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA03001 = '" + sONA03001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();


            string sReviewResults = CHKRRa.Checked ? "核准" : CHKRRb.Checked ? "駁回" : CHKRRc.Checked ? "退補正" : "";
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA03001, sReviewResults, ssUserID, sReviewDirections, sReSendDeadline);
            switch (sReviewResults)
            {
                case "核准":
                    //2019-03-28 核准後，直接將【預定展延期限】代入 水保申請案的【開工期限 SWC82】
                    string sONA003 = TXTONA003.Text + "";

                    string exeSQL = " Update SWCCASE Set SWC082=N'" + sONA003 + "' Where SWC000 = '" + sSWC000 + "' ";
                    string exeSQL55 = " Update SWCSWC Set SWC82=N'" + sONA003 + "' Where SWC00 = '" + sSWC000 + "' ";

                    using (SqlConnection SWCUpdConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SWCUpdConn.Open();

                        SqlCommand objCmdUpdSwc = new SqlCommand(exeSQL, SWCUpdConn);
                        objCmdUpdSwc.ExecuteNonQuery();
                        objCmdUpdSwc.Dispose();
                    }

                    ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                    using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
                    {
                        TslmConn.Open();

                        SqlCommand objCmdTsl = new SqlCommand(exeSQL55, TslmConn);
                        objCmdTsl.ExecuteNonQuery();
                        objCmdTsl.Dispose();
                    }
                    break;
            }

            SendMailNotice(sSWC000, sReviewResults);

            if (rPrevious == "4") { Response.Write("<script>alert('資料已送出。');location.href='SWC004.aspx?SWCNO=" + sSWC000 + "';</script>"); } else { Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>"); }
            
        }

    }
    private void SendMailNotice(string gSWC000, string gReview)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();

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

        //送出提醒名單：系統管理者、監造技師、義務人

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC022, SWC.SWC021ID,SWC.SWC045ID,Uu.ETName as SWC045Name,Uu.ETEmail as SWC045Email,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV = strSQLRV + " LEFT JOIN ETUsers U on SWC.SWC021ID = U.ETID ";
            strSQLRV = strSQLRV + " LEFT JOIN ETUsers Uu on SWC.SWC045ID = Uu.ETID ";
            strSQLRV = strSQLRV + " where SWC.SWC000 = '" + gSWC000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC012 = readeSwc["SWC012"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC022 = readeSwc["SWC022"] + "";
                string tSWC021ID = readeSwc["SWC021ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tETName45 = readeSwc["SWC045Name"] + "";
                string tETEmail45 = readeSwc["SWC045Email"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aMBGROUP.Trim() == "系統管理員")
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = "您好，" + "水土保持計畫【" + tSWC002 + "】申請開工/復工展延已" + gReview;
                        string ssMailBody01 = "您好，【" + tSWC005 + "】申請開工/復工展延已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                        ssMailBody01 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }

                string[] arraySentMail02 = new string[] { tSWC108, tETEmail45 };
                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】申請開工/復工展延已" + gReview;
                string ssMailBody02 = "您好，【" + tSWC005 + "】申請開工/復工展延已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize)
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
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".pdf", ".PDF", ".odt", ".ODT", ".doc", ".DOC", ".docx", ".DOCX" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF、ODT、DOC 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
            }

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > _FileMaxSize * 1000000)
            //if (filesize > 5000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
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
                //error_msg.Text = "檔案上傳成功";

                switch (ChkType)
                {
                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csCaseID2 = TXTONA001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update OnlineApply03 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and ONA03001 = '" + csCaseID2 + "' ";

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
                ImgView.Attributes.Clear();
                ImgView.ImageUrl = "";
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
    protected void TXTReviewDoc_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTReviewDoc_fileupload, TXTReviewDoc, "TXTReviewDoc", "_" + rONANO + "_03_ReviewDoc", null, LinkReviewDoc, 60);

    }

    protected void TXTReviewDoc_fileclean_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTReviewDoc, null, LinkReviewDoc, "ReviewDoc", "TXTReviewDoc", 320, 180);

    }
    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        Response.Redirect("../SwcReport/PdfSwcOLA03.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO);
    }
}