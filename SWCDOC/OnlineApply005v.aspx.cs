using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;

public partial class SWCDOC_OnlineApply005 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        Class20 C20 = new Class20();
        GBClass001 SBApp = new GBClass001();
        LoadSwcClass01 LoadApp = new LoadSwcClass01();

        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";


        if (!IsPostBack) {
            C20.swcLogRC("OnlineApply005v", "設施調整報備", "詳情", "瀏覽", rSWCNO + "-" + rOLANO);
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
                string LINK = "OnlineApply009v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
                Response.Redirect(LINK);
            }
            else
                if (rOLANO == "")
                    Response.Redirect("SWC001.aspx");
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

    private void GetOLA02Data(string v, string v2)
    {
        string ssONLINEAPPLY = Session["ONLINEAPPLY"] + "";

        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";
        string DataLock2 = "";

        System.Configuration.ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
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
                string strSQLRV2 = " select * from OnlineApply05 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA05001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA05002"] + "";
                    string tONA003 = readeONA["ONA05003"] + "";
                    string tONA004 = readeONA["ONA05004"] + "";
                    string tONA005 = readeONA["ONA05005"] + "";
                    string tONA006 = readeONA["ONA05006"] + "";
                    string tONA007 = readeONA["ONA05007"] + "";
                    string tONA008 = readeONA["ONA05008"] + "";
                    string tONA009 = readeONA["ONA05009"] + "";
                    string tONA010 = readeONA["ONA05010"] + "";
                    string tONA011 = readeONA["ONA05011"] + "";
                    string tONA012 = readeONA["ONA05012"] + "";
                    string tONA013 = readeONA["ONA05013"] + "";
                    string tONA014 = readeONA["ONA05014"] + "";
                    string tONA015 = readeONA["ONA05015"] + "";
                    string tONA016 = readeONA["ONA05016"] + "";
                    string tONA017 = readeONA["ONA05017"] + "";

                    string tReviewResults = readeONA["ReviewResults"] + "";
                    string tResultsExplain = readeONA["ResultsExplain"] + "";
                    string tReviewDirections = readeONA["ReviewDirections"] + "";
                    string tReSendDeadline =readeONA["ReSendDeadline"] +"";
                    string tReviewDoc = readeONA["ReviewDoc"] + "";
                    string tLOCKUSER2 = readeONA["LOCKUSER2"] + "";

                    tDATALOCK = readeONA["DATALOCK"] + "";
                    DataLock2 = readeONA["DATALOCK2"] + "";

                    TXTONA001.Text = v2;
                    DDLONA002.Text = tONA002;
                    if (tONA003 == "1") { CHKBOXONA003.Checked = true; }
                    if (tONA004 == "1") { CHKBOXONA004.Checked = true; }
                    if (tONA005 == "1") { CHKBOXONA005.Checked = true; }
                    if (tONA006 == "1") { CHKBOXONA006.Checked = true; }
                    if (tONA007 == "1") { CHKBOXONA007.Checked = true; }
                    if (tONA008 == "1") { CHKBOXONA008.Checked = true; }
                    if (tONA009 == "1") { CHKBOXONA009.Checked = true; }
                    if (tONA010 == "1") { CHKBOXONA010.Checked = true; }
                    TXTONA011.Text = tONA011;
                    if (tONA012 == "1") { CHKBOXONA012.Checked = true; }
                    TXTONA017.Text = tONA017;

                    if (tReviewResults == "1") { CHKRRa.Checked = true; LBRR.Text = "准予通過"; LBResultsExplain.Text = tResultsExplain; }
                    if (tReviewResults == "0") { CHKRRb.Checked = true; LBRR.Text = "駁回"; LBResultsExplain.Text = tResultsExplain; }
                    if (tReviewResults == "2") { CHKRRc.Checked = true; LBRR.Text = "退補正"; LBResultsExplain.Text = tReviewDirections+"<br>補正期限："+SBApp.DateView(tReSendDeadline,"00"); }
                    if (tReviewResults == "1" && DataLock2 == "Y") { OutPdf.Visible = true; }

                    ResultsExplain.Text = tResultsExplain;
                    TXTReviewDoc.Text = tReviewDoc;
                    ReviewDirections.Text = tReviewDirections;
                    TXTDeadline.Text = SBApp.DateView(tReSendDeadline,"00");
                    ReviewID.Text = SBApp.GetGeoUser(tLOCKUSER2, "Name");

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tONA011, tONA013, tONA014, tONA015, tONA016, tReviewDoc };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link011, Link013, Link014, Link015, Link016, LinkReviewDoc };
                    string[] arrayFileNameButton = new string[] { tONA011, tONA014, tONA016 };
                    System.Web.UI.WebControls.Button[] arrayButtonAppobj = new System.Web.UI.WebControls.Button[] { BTN011, BTN014, BTN016 };

                    //HyperLink
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
                            if(i == 0 || i == 2 || i == 4) { FileLinkObj.Visible = false; }
                        }
                    }
					
                    //Button
                    for (int i = 0; i < arrayFileNameButton.Length; i++)
                    {
                        string strFileName = arrayFileNameButton[i];
                        System.Web.UI.WebControls.Button FileLinkObj = arrayButtonAppobj[i];

                        FileLinkObj.Visible = false;
                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            FileLinkObj.Text = strFileName;
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
            CHKRRc.Visible = false;
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
    
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA05" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA05" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA05001) AS MAXID from OnlineApply05 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA05001,9) = '" + tempVal + "' ";

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
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
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
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

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

        strSQLClearFieldValue = " update OnlineApply05 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and ONA05001 = '" + csCaseID2 + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

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

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA05001 = TXTONA001.Text + "";
        string sReviewDoc = TXTReviewDoc.Text + "";
        string sResultsExplain = ResultsExplain.Text + "";
        string sReviewResults = CHKRRa.Checked ? "1": CHKRRb.Checked ? "0": CHKRRc.Checked ? "2" : "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text;

        string sEXESQLUPD = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply05 Set ";
            sEXESQLUPD = sEXESQLUPD + " ReviewResults = '" + sReviewResults + "', ";
            sEXESQLUPD = sEXESQLUPD + " ResultsExplain = '" + sResultsExplain + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDoc = '" + sReviewDoc + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDirections = '" + sReviewDirections + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReSendDeadline = '" + sReSendDeadline + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA05001 = '" + sONA05001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        }
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTReviewDoc" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTReviewDoc };
        string csUpLoadField = "TXTReviewDoc";
        TextBox csUpLoadAppoj = TXTReviewDoc;

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

    protected void DataLock_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        GBClass001 GBC = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sONA05001 = TXTONA001.Text + "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply05 Set ";
            if (CHKRRc.Checked) { sEXESQLSTR += " DATALOCK='',LOCKUSER='',LOCKDATE=null, "; }
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '" + ssUserID + "' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA05001 = '" + sONA05001 + "';";
            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string sReviewResults = CHKRRa.Checked ? "核准" : CHKRRb.Checked ? "駁回" : CHKRRc.Checked ? "退補正":"";
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA05001, sReviewResults, ssUserID, sReviewDirections, sReSendDeadline);
            SendMailNotice(sSWC000, sReviewResults);

            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
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

        //送出提醒名單：義務人、監造技師、檢查公會

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC024, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV = strSQLRV + " LEFT JOIN ETUsers U on SWC.SWC045ID = U.ETID ";
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
                string tSWC024 = readeSwc["SWC024"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄件名單
                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aUserName.Trim() == tSWC024.Trim())
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = "您好，" + "水土保持計畫【" + tSWC002 + "】申請設施調整報備已" + gReview;
                        string ssMailBody01 = "您好，【" + tSWC005 + "】申請設施調整報備已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = "您好，" + "水土保持計畫【" + tSWC002 + "】申請設施調整報備已" + gReview;
                //string ssMailBody01 = "您好，【" + tSWC005 + "】申請設施調整報備已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                string[] arraySentMail02a = new string[] { tETEmail };
                string[] arraySentMail02b = new string[] { tSWC108 };
                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】申請設施調整報備已" + gReview;
                string ssMailBody02 = "您好，【" + tSWC005 + "】申請設施調整報備已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo02a = SBApp.Mail_Send(arraySentMail02a, ssMailSub02, ssMailBody02);
                bool MailTo02b = SBApp.Mail_Send(arraySentMail02b, ssMailSub02, ssMailBody02);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    protected void TXTReviewDoc_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTReviewDoc_fileupload, TXTReviewDoc, "TXTReviewDoc", "_" + rONANO + "_05_ReviewDoc", null, LinkReviewDoc, 60);
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

        Response.Redirect("../SwcReport/PdfSwcOLA05.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO);
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }

    //6-1、7-1PDF須加上大地處浮水印
    protected void Watermark_Click(object sender, EventArgs e)
    {
        string btnType = ((Button)(sender)).ID;
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];
        string SwcCaseFilePath = "";
        //string CaseId = LBSWC000.Text;
		//TEST
		string CaseId2 = Request.QueryString["SWCNO"] + "";
		
        switch (btnType)
        {
            case "BTN014":
                SwcCaseFilePath = SwcCaseFolderPath + CaseId2 + "\\" + BTN014.Text;
                break;
            case "BTN016":
                SwcCaseFilePath = SwcCaseFolderPath + CaseId2 + "\\" + BTN016.Text;
                break;
            case "BTN011":
                SwcCaseFilePath = SwcCaseFolderPath + CaseId2 + "\\" + BTN011.Text;
                break;
        }
		string na = Path.GetExtension(SwcCaseFilePath);
		if(na==".pdf"){
			//PDF套表開始
			PdfReader Pdfreader = new PdfReader(SwcCaseFilePath);
			string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
			PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));
	
			PdfGState gstate = new PdfGState()
			{
				FillOpacity = 0.5f,
				StrokeOpacity = 0.5f
			};
	
			for (int page = 1; page <= Pdfreader.NumberOfPages; page++)
			{
				PdfContentByte pdfPageContents = Pdfstamper.GetOverContent(page);
				if (OutPdf.Visible == true){
					iTextSharp.text.Rectangle pagesize = Pdfreader.GetPageSizeWithRotation(page); //每頁的Size
					float x = pagesize.Height;
					//float y = pagesize.Width;
					//float xx = (pagesize.Right + pagesize.Left) / 2;
					//float yy = (pagesize.Bottom + pagesize.Top) / 2;
					pdfPageContents.SetGState(gstate); //塞入我們設定的透明度
				
					string imageUrl = HttpContext.Current.Server.MapPath("../images/Watermark/大地浮水印.jpg");
					if(x <= 842) { imageUrl = HttpContext.Current.Server.MapPath("../images/Watermark/大地浮水印_A4.jpg"); }
					else { imageUrl = HttpContext.Current.Server.MapPath("../images/Watermark/大地浮水印_A3.jpg"); }
					iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageUrl);
					//Response.Write("<script>alert('x = " + x  + ",y = " + y + "');</script>");
					//img.ScalePercent(100f);  //縮放比例
					img.RotationDegrees = 0; //旋轉角度
					img.SetAbsolutePosition(0, 0); //設定圖片每頁的絕對位置
					PdfContentByte waterMark = Pdfstamper.GetOverContent(page);
					waterMark.AddImage(img); //把圖片印上去 
				}
			}
	
			Pdfstamper.Close();
			Pdfreader.Close();
	
			//把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
			System.IO.Stream iStream;
	
			// 以10K為單位暫存:
			Byte[] buffer = new byte[10000];
			int length = 0;
			long dataToRead = 0;
	
			// 制定文件路徑
			string filepath = Server.MapPath("~\\OutputFile\\" + pdfnewname);
			//string filepathm = Server.MapPath("~\\OutputFile\\m" + pdfnewname);
	
			// 得到文件名
			string filename = System.IO.Path.GetFileName(filepath);
	
			//Try
			// 打開文件
			iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
			// 得到文件大小
			dataToRead = iStream.Length;
			Response.ContentType = "application/x-rar-compressed";
			Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));
	
			while (dataToRead > 0)
			{
				if (Response.IsClientConnected)
				{
					length = iStream.Read(buffer, 0, 10000);
					Response.OutputStream.Write(buffer, 0, length);
					Response.Flush();
					dataToRead = dataToRead - length;
				}
				else
				{
					dataToRead = -1;
				}
			}
			if (iStream.Length != 0)
			{
				//關閉文件
				iStream.Close();
				File.Delete(filepath);
			}

		}
		else{	
			
		}
    }

}