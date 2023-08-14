using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply001 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rONA000 = Request.QueryString["OACode"] + "";
        string rBTNCTL = Request.QueryString["PRS"] + "";
        string rUA = Request.QueryString["UA"] + "";

        //測試用變數設定
        //rONA000 = "TEST10704001";
        LBSWC000.Text = rCaseId;

        if (!IsPostBack)
        {
            if (rONA000 == "")
            {
                Response.Redirect("OnlineApply001L.aspx");
            }
            else
            {
                Data2Page(rCaseId, rONA000);
            }
        }
        if (rUA == "over") { DataLock.Visible = false; SaveCase.Visible = false; GoHomePage.Visible = false; }
        else if (rUA.Trim() != "over") { GoHomePage.Visible = true; }

        //全區供用
        SBApp.ViewRecord("臺北市山坡地水土保持設施安全自主檢查表", "view", rONA000);

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        //全區供用
    }

    private void Data2Page(string rCaseId, string rONA000)
    {
        string ssONLINEAPPLY = Session["ONLINEAPPLY"] + "";

        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";
        string DataLock2 = "";

        if (rONA000 == "" || rONA000 == "ADDNEW")
        {
            rONA000 = GetONAID();
            LBONA001.Text = rONA000;
        }
        else
        {
            LBONA001.Text = rONA000;

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from OnlineApply01 ";
                strSQLRV = strSQLRV + "   where ONA01001 = '" + rONA000 + "' ";

                SqlDataReader readerOA1;
                SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
                readerOA1 = objCmdSWC.ExecuteReader();

                while (readerOA1.Read())
                {
                    string rSWC002 = readerOA1["SWC002"] + "";
                    string rONA01001 = readerOA1["ONA01001"] + ""; 
                    string rONA01002 = readerOA1["ONA01002"] + "";
                    string rONA01003 = readerOA1["ONA01003"] + "";
                    string rONA01004 = readerOA1["ONA01004"] + "";
                    string rONA01005 = readerOA1["ONA01005"] + "";
                    string rONA01006 = readerOA1["ONA01006"] + "";
                    string rONA01007 = readerOA1["ONA01007"] + "";
                    string rONA01008 = readerOA1["ONA01008"] + "";
                    string rONA01009 = readerOA1["ONA01009"] + "";
                    string rONA01010 = readerOA1["ONA01010"] + "";
                    string rONA01011 = readerOA1["ONA01011"] + "";
                    string rONA01012 = readerOA1["ONA01012"] + "";
                    string rONA01013 = readerOA1["ONA01013"] + "";
                    string rONA01014 = readerOA1["ONA01014"] + "";
                    string rONA01015 = readerOA1["ONA01015"] + "";
                    string rONA01016 = readerOA1["ONA01016"] + "";
                    string rONA01017 = readerOA1["ONA01017"] + "";
                    string rONA01018 = readerOA1["ONA01018"] + "";
                    string rONA01019 = readerOA1["ONA01019"] + "";
                    string rONA01020 = readerOA1["ONA01020"] + "";
                    string rONA01021 = readerOA1["ONA01021"] + "";
                    string rONA01022 = readerOA1["ONA01022"] + "";
                    string rONA01023 = readerOA1["ONA01023"] + "";
                    string rONA01024 = readerOA1["ONA01024"] + "";
                    if (rONA01024 != "")
                        rONA01024 = Convert.ToDateTime(readerOA1["ONA01024"]).ToString("MM-dd") + "";
                    string rONA01025 = readerOA1["ONA01025"] + "";
                    string rONA01026 = readerOA1["ONA01026"] + "";
                    string rONA01027 = readerOA1["ONA01027"] + "";
                    string rONA01028 = readerOA1["ONA01028"] + "";

                    string tReviewResults = readerOA1["ReviewResults"] + "";
                    string tResultsExplain = readerOA1["ResultsExplain"] + "";
                    string tReviewDoc = readerOA1["ReviewDoc"] + "";
                    string tLOCKUSER2 = readerOA1["LOCKUSER2"] + "";

                    tDATALOCK = readerOA1["DATALOCK"] + "";
                    DataLock2 = readerOA1["DATALOCK2"] + "";
                    
                    rONA01008 = rONA01008.Replace("1", "有此設施").Replace("0", "無此設施");
                    rONA01009 = rONA01009.Replace("1", "是").Replace("0", "否");
                    rONA01010 = rONA01010.Replace("1", "是").Replace("0", "否");
                    rONA01011 = rONA01011.Replace("1", "有此設施").Replace("0", "無此設施");
                    rONA01012 = rONA01012.Replace("1", "是").Replace("0", "否");
                    rONA01013 = rONA01013.Replace("1", "是").Replace("0", "否");
                    rONA01014 = rONA01014.Replace("1", "有此設施").Replace("0", "無此設施");
                    rONA01015 = rONA01015.Replace("1", "是").Replace("0", "否");
                    rONA01016 = rONA01016.Replace("1", "是").Replace("0", "否");
                    rONA01017 = rONA01017.Replace("1", "是").Replace("0", "否").Replace("2", "無排水孔");
                    rONA01018 = rONA01018.Replace("1", "有此設施").Replace("0", "無此設施");
                    rONA01019 = rONA01019.Replace("1", "是").Replace("0", "否");
                    rONA01020 = rONA01020.Replace("1", "是").Replace("0", "否");
                    rONA01021 = rONA01021.Replace("1", "是").Replace("0", "否");
                    rONA01023 = rONA01023.Replace("1", "是").Replace("0", "否");

                    TXTSWC002.Text = rSWC002;
                    LBONA001.Text  = rONA01001;
                    TXTONA002.Text = SBApp.DateView(rONA01002, "00");
                    TXTONA003.Text = rONA01003;
                    TXTONA004.Text = rONA01004;
                    TXTONA005.Text = rONA01005;
                    TXTONA006.Text = rONA01006;
                    TXTONA007.Text = rONA01007;
                    LBONA008.Text = rONA01008;
                    LBONA009.Text = rONA01009 == "是" ? rONA01009 +"，"+ readerOA1["ONA01009aD"] + "":rONA01009 + "，" + readerOA1["ONA01009bD"] + "";
                    LBONA010.Text = rONA01010 == "是" ? rONA01010 + "，" + readerOA1["ONA01010aD"] + "" : rONA01010 + "，" + readerOA1["ONA01010bD"] + "";
                    LBONA011.Text = rONA01011;
                    LBONA012.Text = rONA01012 == "是" ? rONA01012 + "，" + readerOA1["ONA01012aD"] + "" : rONA01012 + "，" + readerOA1["ONA01012bD"] + "";
                    LBONA013.Text = rONA01013 == "是" ? rONA01013 + "，" + readerOA1["ONA01013aD"] + "" : rONA01013 + "，" + readerOA1["ONA01013bD"] + "";
                    LBONA014.Text = rONA01014;
                    LBONA015.Text = rONA01015 == "是" ? rONA01015 + "，" + readerOA1["ONA01015aD"] + "" : rONA01015 + "，" + readerOA1["ONA01015bD"] + "";
                    LBONA016.Text = rONA01016 == "是" ? rONA01016 + "，" + readerOA1["ONA01016aD"] + "" : rONA01016 + "，" + readerOA1["ONA01016bD"] + "";
                    LBONA017.Text = rONA01017 == "是" ? rONA01017 + "，" + readerOA1["ONA01017aD"] + "" : rONA01017 == "否" ? rONA01017 + "，" + readerOA1["ONA01017bD"] + "": rONA01017;
                    LBONA018.Text = rONA01018;
                    LBONA019.Text = rONA01019 == "是" ? rONA01019 + "，" + readerOA1["ONA01019aD"] + "" : rONA01019 + "，" + readerOA1["ONA01019bD"] + "";
                    LBONA020.Text = rONA01020 == "是" ? rONA01020 + "，" + readerOA1["ONA01020aD"] + "" : rONA01020 + "，" + readerOA1["ONA01020bD"] + "";
                    LBONA021.Text = rONA01021;
                    LBONA023.Text = rONA01023 == "否" ? rONA01023 + "，" + readerOA1["ONA01023bD"] + "" : rONA01023;
                    if (rONA01021 == "是" && rONA01022.Trim() != "") { TXTONA022.Text = "，說明：" + rONA01022; }
                    if (rONA01023 == "是" && rONA01024.Trim() != "") { TXTONA024.Text += "， 規劃於" + rONA01024 + "改善完成"; }
                    //TXTONA024.Text = SBApp.DateView(rONA01024, "00");
                    TXTONA025.Text = rONA01025;
                    TXTONA026.Text = rONA01026;
                    TXTONA027.Text = rONA01027;
                    TXTONA028.Text = rONA01028;

                    if (tReviewResults == "1") { CHKRRa.Checked = true; LBRR.Text = "審查結果：准予通過"; }
                    if (tReviewResults == "0") { CHKRRb.Checked = true; LBRR.Text = "審查結果：駁回"; }
                    if (tResultsExplain.Trim() != "") { LBResultsExplain.Text = "：" + tResultsExplain; }
                    if (tReviewResults == "1" && DataLock2 == "Y") { OutPdf.Visible = true; }

                    ResultsExplain.Text = tResultsExplain;
                    TXTReviewDoc.Text = tReviewDoc;
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
                            string v = Request.QueryString["SWCNO"] + "";

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
            //ReviewResults.Visible = true;
            ReviewResults.Visible = false;
            //DataLock.Visible = true;
            DataLock.Visible = false;
            //SaveCase.Visible = true;
            SaveCase.Visible = false;
        }
        if (DataLock2 == "Y")
        {
            ReViewUL.Visible = false;
            ResultsExplain.Visible = false;
            //ReviewResults.Visible = true;
            ReviewResults.Visible = false;
            LBRR.Visible = true;
            CHKRRa.Visible = false;
            CHKRRb.Visible = false;
            DataLock.Visible = false;
            SaveCase.Visible = false;
        }
        if (ssONLINEAPPLY != "Y" && DataLock2 != "Y")
        {
            ReviewResults.Visible = false;
        }
    }

    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA01" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA01" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA01001) AS MAXID from OnlineApply01 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA01001,9) = '" + tempVal + "' ";

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

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string sSWC000 = Request.QueryString["SWCNO"] + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA02001 = LBONA001.Text + "";
        string sReviewDoc = TXTReviewDoc.Text + "";
        string sResultsExplain = ResultsExplain.Text + "";

        string sReviewResults = "";
        if (CHKRRa.Checked) { sReviewResults = "1"; }
        if (CHKRRb.Checked) { sReviewResults = "0"; }

        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply01 Set ";
            sEXESQLUPD = sEXESQLUPD + " ReviewResults = '" + sReviewResults + "', ";
            sEXESQLUPD = sEXESQLUPD + " ResultsExplain = '" + sResultsExplain + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDoc = '" + sReviewDoc + "', ";

            sEXESQLUPD = sEXESQLUPD + " LOCKUSER2 = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " LOCKDATE = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA01001 = '" + sONA02001 + "'";

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

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }
    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string SwcFileName = "";
        string CaseId = Request.QueryString["SWCNO"] + "";

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
        string csCaseID2 = LBONA001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update OnlineApply01 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where ONA01001 = '" + csCaseID2 + "' ";

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
        string rONANO = LBONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTReviewDoc_fileupload, TXTReviewDoc, "TXTReviewDoc", "_" + rONANO + "_01_ReviewDoc", null, LinkReviewDoc, 60);

    }

    protected void TXTReviewDoc_fileclean_Click(object sender, EventArgs e)
    {
        string rONANO = LBONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTReviewDoc, null, LinkReviewDoc, "ReviewDoc", "TXTReviewDoc", 320, 180);

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

        //送出提醒名單：義務人、承辦技師、審查公會

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC022, SWC.SWC021ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV = strSQLRV + " LEFT JOIN ETUsers U on SWC.SWC021ID = U.ETID ";
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

                    if (aUserName.Trim() == tSWC022.Trim())
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;
                    }
                }

                //一人一封不打結…
                //審查公會
                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                string ssMailSub01 = tSWC022 + "您好，" + "水土保持計畫【" + tSWC002 + "】申請暫停審查已" + gReview;
                string ssMailBody01 = "您好，【" + tSWC005 + "】申請暫停審查已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                //承辦技師
                string[] arraySentMail02 = new string[] { tETEmail };
                string ssMailSub02 = tETName + "技師您好，" + "水土保持計畫【" + tSWC002 + "】申請暫停審查已" + gReview;
                string ssMailBody02 = "您好，【" + tSWC005 + "】申請暫停審查已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);

                //義務人
                string[] arraySentMail03 = new string[] { tSWC108 };
                string ssMailSub03 = tSWC013 + "您好，" + "水土保持計畫【" + tSWC002 + "】申請暫停審查已" + gReview;
                string ssMailBody03 = "您好，【" + tSWC005 + "】申請暫停審查已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody03 = ssMailBody03 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody03 = ssMailBody03 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo03 = SBApp.Mail_Send(arraySentMail03, ssMailSub03, ssMailBody03);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text;
        string sONA01001 = LBONA001.Text + "";

        string sEXESQLSTR = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply01 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '" + ssUserID + "' ";
            sEXESQLSTR = sEXESQLSTR + " Where ONA01001 = '" + sONA01001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string sReviewResults = "";
            if (CHKRRa.Checked) { sReviewResults = "核准"; }
            if (CHKRRb.Checked) { sReviewResults = "駁回"; }

            //SendMailNotice(sSWC000, sReviewResults);

            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");

        }

    }
    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OACode"] + "";
        string rSWCNO02 = TXTSWC002.Text + "";

        Response.Redirect("../SwcReport/PdfSwcOLA01.aspx?SWCNO02=" + rSWCNO02 + "&OLANO=" + rOLANO);
    }
}