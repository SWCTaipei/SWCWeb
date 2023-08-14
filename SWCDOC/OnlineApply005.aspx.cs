using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply005 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();
        LoadSwcClass01 LoadApp = new LoadSwcClass01();
        Class20 C20 = new Class20();

        Page.MaintainScrollPositionOnPostBack = true;

        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        if(ssUserName.Trim()=="")
            Response.Redirect("SWC000.aspx");

        if (!IsPostBack)
        {
            C20.swcLogRC("OnlineApply005", "設施報備調整", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
            if (rRRPG == "55")
            {
                string LINK = "";
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR) {LINK = "OnlineApply005v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO; }
                else { LINK = "SWC001.aspx"; }
                Response.Redirect(LINK);
            }
            else
            {
                switch (ssUserType)
                {
                    case "01":
                    case "02":
                    case "03":
                    case "04":
                    case "08":
                    case "09":
                        if (rOLANO == "") { Response.Redirect("SWC001.aspx"); }
                        else
                            GetOLA02Data(rSWCNO, rOLANO);
                        break;
                    default:
                        Response.Redirect("SWC000.aspx");
                        break;
                }
				if(ssUserType == "08") DataLock.Visible = false;
            }
        }



        //全區供用
        SBApp.ViewRecord("設施調整報備", "update", rOLANO);

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用

    }

    private void GetOLA02Data(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";

        GenerateDropDownList();

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
                string tSWC025 = readeSwc["SWC025"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
                LBSWC025.Text = tSWC025;

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
                    tDATALOCK = readeONA["DATALOCK"] + "";

                    TXTONA001.Text = v2;
                    DDLONA002.SelectedValue = tONA002;
                    if (tONA003 == "1") { CHKBOXONA003.Checked = true; }
                    if (tONA004 == "1") { CHKBOXONA004.Checked = true; }
                    if (tONA005 == "1") { CHKBOXONA005.Checked = true; }
                    if (tONA006 == "1") { CHKBOXONA006.Checked = true; }
                    if (tONA007 == "1") { CHKBOXONA007.Checked = true; }
                    if (tONA008 == "1") { CHKBOXONA008.Checked = true; }
                    if (tONA009 == "1") { CHKBOXONA009.Checked = true; }
                    if (tONA010 == "1") { CHKBOXONA010.Checked = true; }
                    if (tONA012 == "1") { CHKBOXONA012.Checked = true; }

                    TXTONA011.Text = tONA011;
                    TXTONA013.Text = tONA013;
                    TXTONA014.Text = tONA014;
                    TXTONA015.Text = tONA015;
                    TXTONA016.Text = tONA016;

                    TXTONA017.Text = tONA017;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tONA011, tONA013, tONA014, tONA015, tONA016 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link011, Link013, Link014, Link015, Link016 };

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
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v+"' and ID003='"+v2+"' order by h.id; ";
    }

    protected void GenerateDropDownList()
    {
        string[] array_DTL002 = new string[] { "是", "否" };
        DDLONA002.DataSource = array_DTL002;
        DDLONA002.DataBind();

        DDLONA002.SelectedValue = "否";
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
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".pdf", ".odt", ".doc", ".docx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇pdf、odt或doc檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                

                case "CAD":
                    List<string> allowedExtextsion03 = new List<string> { ".dwg", ".DWG" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇DWG檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "excel":
                    List<string> allowedExtextsion04 = new List<string> { ".xls", ".xlsx", ".doc", ".docx", ".png", ".jpg", ".pdf" };

                    if (allowedExtextsion04.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 .xls .xlsx .doc .docx .png .jpg .pdf 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
            }
            int filesize2 = UpLoadBar.PostedFile.ContentLength;
            // 限制檔案大小，限制為 50MB
            if (filesize2 > 50000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50 Mb 以下檔案上傳，謝謝!!");
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
                    case "excel":
                    case "CAD":
                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
            case "CAD":
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
        string sONA05002 = DDLONA002.SelectedValue + "";
        string sONA05003 = CHKBOXONA003.Checked + "";
        string sONA05004 = CHKBOXONA004.Checked + "";
        string sONA05005 = CHKBOXONA005.Checked + "";
        string sONA05006 = CHKBOXONA006.Checked + "";
        string sONA05007 = CHKBOXONA007.Checked + "";
        string sONA05008 = CHKBOXONA008.Checked + "";
        string sONA05009 = CHKBOXONA009.Checked + "";
        string sONA05010 = CHKBOXONA010.Checked + "";
        string sONA05012 = CHKBOXONA012.Checked + "";
        string sONA05011 = TXTONA011.Text + "";
        string sONA05013 = TXTONA013.Text + "";
        string sONA05014 = TXTONA014.Text + "";
        string sONA05015 = TXTONA015.Text + "";
        string sONA05016 = TXTONA016.Text + "";
        string sONA05017 = TXTONA017.Text + "";

        string sEXESQLUPD = "";

        if (sONA05003 == "True") { sONA05003 = "1"; } else { sONA05003 = "0"; }
        if (sONA05004 == "True") { sONA05004 = "1"; } else { sONA05004 = "0"; }
        if (sONA05005 == "True") { sONA05005 = "1"; } else { sONA05005 = "0"; }
        if (sONA05006 == "True") { sONA05006 = "1"; } else { sONA05006 = "0"; }
        if (sONA05007 == "True") { sONA05007 = "1"; } else { sONA05007 = "0"; }
        if (sONA05008 == "True") { sONA05008 = "1"; } else { sONA05008 = "0"; }
        if (sONA05009 == "True") { sONA05009 = "1"; } else { sONA05009 = "0"; }
        if (sONA05010 == "True") { sONA05010 = "1"; } else { sONA05010 = "0"; }
        if (sONA05012 == "True") { sONA05012 = "1"; } else { sONA05012 = "0"; }

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply05 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + " and ONA05001 = '" + sONA05001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply05 (SWC000,SWC002,ONA05001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA05001 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply05 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA05002 = '" + sONA05002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05003 = '" + sONA05003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05004 = '" + sONA05004 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05005 = '" + sONA05005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05006 = '" + sONA05006 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05007 = '" + sONA05007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05008 = '" + sONA05008 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05009 = '" + sONA05009 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05010 = '" + sONA05010 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05011 = '" + sONA05011 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05012 = '" + sONA05012 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05013 = '" + sONA05013 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05014 = '" + sONA05014 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05015 = '" + sONA05015 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05016 = '" + sONA05016 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA05017 = '" + sONA05017 + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where SWC000 = '"+ sSWC000 + "' AND ONA05001 = '" + sONA05001 + "'";

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

        string[] arryUpLoadField = new string[] { "TXTONA011", "TXTONA013", "TXTONA014", "TXTONA015", "TXTONA016" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTONA011, TXTONA013, TXTONA014, TXTONA015, TXTONA016 };
        string csUpLoadField = "TXTONA011";
        TextBox csUpLoadAppoj = TXTONA011;

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
        GBClass001 GBC = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC005 = LBSWC005.Text;
        string sSWC025 = LBSWC025.Text;
        string sONA05001 = TXTONA001.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply05 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA05001 = '" + sONA05001 + "' ";
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
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply005v.aspx?SWCNO=" + sSWC000 + "&OLANO=" + sONA05001 + "'; </script>");
                        return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            //LOCKUSER2,ReviewResults,ReviewDoc,ResultsExplain,ReviewDirections,ReSendDeadline
            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply05 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewResults = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDoc = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ResultsExplain = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDirections = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReSendDeadline = null, ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y', ";
            sEXESQLSTR += " SWC005 = N'" + sSWC005 + "', ";
            sEXESQLSTR += " SWC025 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING002 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING004 = N'" + sSWC025 + ";', ";
            sEXESQLSTR += " SING006 = N'" + ssUserName + "', ";
            sEXESQLSTR += " SING007 = N'送出',ONAHEAD=N'水土保持計畫義務人及技師變更報備',SING008 = N'待簽辦', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKUSER = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKDATE = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA05001 = '" + sONA05001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@ONA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
            ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = strSQL3;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sSWC005));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sSWC025));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA05001));
                    cmd.Parameters.Add(new SqlParameter("@R001", ""));
                    cmd.Parameters.Add(new SqlParameter("@R002", ""));
                    cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                    //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                    cmd.Parameters.Add(new SqlParameter("@R005", ""));
                    cmd.Parameters.Add(new SqlParameter("@R006", ""));
                    cmd.Parameters.Add(new SqlParameter("@R007", GBC.GetETUser(ssUserID, "OrgName")));
                    cmd.Parameters.Add(new SqlParameter("@R008", GBC.GetETUser(ssUserID, "ETIdentity")));
                    cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/hhmm")));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            GBC.RecordTrunHistory(sSWC000,sSWC002, sONA05001, "申請中", ssUserID,"","");
            SendMailNotice(sSWC000);            

            Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }
    }

    private void SendMailNotice(string gSWC000)
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

        //送出提醒名單：股長、管理者、承辦人員、章姿隆(ge-10706)

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
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
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706")
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = aUserName + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增設施調整報備申請";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增設施調整報備申請，請上臺北市坡地管理資料庫查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增暫停審查申請";
                //string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增暫停審查申請，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    protected void UpLoadFileOk_Click(object sender, EventArgs e)
    {
        string thisPageAct = ((Button)sender).ID + "";
        string rONANO = TXTONA001.Text + "";

        error_msg.Text = "";

        switch (thisPageAct)
        {
            case "TXTONA011_fileuploadok":
                FileUpLoadApp("excel", TXTONA011_fileupload, TXTONA011, "TXTONA011", "_" + rONANO + "_ONA11_DOC1", null, Link011);
                break;
            case "TXTONA013_fileuploadok":
                FileUpLoadApp("CAD", TXTONA013_fileupload, TXTONA013, "TXTONA013", "_" + rONANO + "_ONA13_CAD1", null, Link013);
                break;
            case "TXTONA014_fileuploadok":
                FileUpLoadApp("DOC", TXTONA014_fileupload, TXTONA014, "TXTONA014", "_" + rONANO + "_ONA14_DOC1", null, Link014);
                break;
            case "TXTONA015_fileuploadok":
                FileUpLoadApp("CAD", TXTONA015_fileupload, TXTONA015, "TXTONA015", "_" + rONANO + "_ONA15_CAD1", null, Link015);
                break;
            case "TXTONA016_fileuploadok":
                FileUpLoadApp("DOC", TXTONA016_fileupload, TXTONA016, "TXTONA016", "_" + rONANO + "_ONA16_DOC1", null, Link016);
                break;
        }
    }
    protected void UpLoadFileDel_Click(object sender, EventArgs e)
    {
        string thisPageAct = ((Button)sender).ID + "";
        string rONANO = TXTONA001.Text + "";

        error_msg.Text = "";

        switch (thisPageAct)
        {
            case "TXTONA011_fileuploaddel":
                DeleteUpLoadFile("DOC", TXTONA011, null, Link011, "ONA05011", "TXTONA011", 320, 180);
                break;
            case "TXTONA013_fileuploaddel":
                DeleteUpLoadFile("CAD", TXTONA013, null, Link013, "ONA05013", "TXTONA013", 320, 180);
                break;
            case "TXTONA014_fileuploaddel":
                DeleteUpLoadFile("DOC", TXTONA014, null, Link014, "ONA05014", "TXTONA014", 320, 180);
                break;
            case "TXTONA015_fileuploaddel":
                DeleteUpLoadFile("CAD", TXTONA015, null, Link015, "ONA05015", "TXTONA015", 320, 180);
                break;
            case "TXTONA016_fileuploaddel":
                DeleteUpLoadFile("DOC", TXTONA016, null, Link016, "ONA05016", "TXTONA016", 320, 180);
                break;
        }
    }    

    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }
}