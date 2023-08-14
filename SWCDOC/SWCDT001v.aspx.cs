using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCDT001 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFileUrl20"]+ "SWCDOC\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC\\UpLoadFiles\\temp\\";
    //string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    //string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rRRPage = Request.QueryString["RRPG"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";

        string ssJobTitle = Session["JobTitle"] + "";
        GBClass001 SBApp = new GBClass001();

        if (rCaseId == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        if (!IsPostBack)
        {
            Data2Page(rCaseId, rDTLId);
        }

        if (rRRPage=="55")
        {
            GoHomePage.Visible = false;
        }
        //全區供用

        SBApp.ViewRecord("審查表單", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
    }
    private void Data2Page(string v,string v2)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

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
                string tSWC005 = readeSwc["SWC005"] + "";

                LBSWC000.Text = v;
                LBSWC005.Text = tSWC005;
                LBSWC005_2.Text = tSWC005;
                LBSWC005a.Text = tSWC005;
            }

            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string nIDA = GetDTLAID(v);

                LBDTL001.Text = nIDA;
            }
            else
            {
                string strSQLRV2 = " select * from SWCDTL01 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   AND DTLA000 = '" + v2 + "' ";
                C20.swcLogRC("SWCDT001v", "審查紀錄", "詳情", "瀏覽", v + ";" + v2);

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLA001 = readeDTL["DTLA001"] + "";
                    string tDTLA002 = readeDTL["DTLA002"] + "";
                    string tDTLA003 = readeDTL["DTLA003"] + "";
                    string tDTLA004 = readeDTL["DTLA004"] + "";
                    string tDTLA005 = readeDTL["DTLA005"] + "";
                    string tDTLA006 = readeDTL["DTLA006"] + "";
                    string tDTLA007 = readeDTL["DTLA007"] + "";
                    string tDTLA008 = readeDTL["DTLA008"] + "";
                    string tDTLA009 = readeDTL["DTLA009"] + "";
                    string tDTLA010 = readeDTL["DTLA010"] + "";
                    string tDTLA011 = readeDTL["DTLA011"] + "";
                    string tDTLA012 = readeDTL["DTLA012"] + "";
                    string tDTLA013 = readeDTL["DTLA013"] + "";
                    string tDTLA014 = readeDTL["DTLA014"] + "";
                    string tDTLA015 = readeDTL["DTLA015"] + "";
                    string tDTLA016 = readeDTL["DTLA016"] + "";
                    string tDTLA017 = readeDTL["DTLA017"] + "";
                    string tDTLA018 = readeDTL["DTLA018"] + "";
                    string tDTLA019 = readeDTL["DTLA019"] + "";
                    string tDTLA020 = readeDTL["DTLA020"] + "";
                    string tDTLA021 = readeDTL["DTLA021"] + "";
                    string tDTLA022 = readeDTL["DTLA022"] + "";
                    string tDTLA023 = readeDTL["DTLA023"] + "";
                    string tDTLA024 = readeDTL["DTLA024"] + "";
                    string tDTLA025 = readeDTL["DTLA025"] + "";
                    string tDTLA026 = readeDTL["DTLA026"] + "";
                    string tDTLA027 = readeDTL["DTLA027"] + "";
                    string tDTLA028 = readeDTL["DTLA028"] + "";
                    string tDTLA029 = readeDTL["DTLA029"] + "";
                    string tDTLA030 = readeDTL["DTLA030"] + "";
                    string tDTLA031 = readeDTL["DTLA031"] + "";
                    string tDTLA032 = readeDTL["DTLA032"] + "";
                    string tDTLA034 = readeDTL["DTLA034"] + "";

                    string tLOCKDATE = readeDTL["savedate"] + "";

                    LBDTL001.Text = tDTLA001;
                    TXTDTL003.Text = SBApp.DateView(tDTLA003, "04");
                    TXTSENDDATE.Text = SBApp.DateView(tLOCKDATE, "04");
                    TXTDTL004.Text = tDTLA004;
                    DDLDTL006.Text = tDTLA006;                    
                    TXTDTL007.Text = SBApp.DateView(tDTLA007, "04");
                    TXTDTL008.Text = tDTLA008;
                    TXTDTL009.Text = tDTLA009;
                    TXTDTL010.Text = tDTLA010;
                    TXTDTL011.Text = tDTLA011;
                    TXTDTL012.Text = tDTLA012;
                    TXTDTL013.Text = tDTLA013.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL017.Text = tDTLA017.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL018.Text = tDTLA018.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL020.Text = tDTLA020;
                    TXTDTL021.Text = tDTLA021;
                    TXTDTL022.Text = tDTLA022;
                    TXTDTL023.Text = tDTLA023;
                    TXTDTL024.Text = tDTLA024;
                    TXTDTL025.Text = tDTLA025;
                    TXTDTL026.Text = tDTLA026;
                    TXTDTL027.Text = tDTLA027;
                    TXTDTL028.Text = tDTLA028;
                    TXTDTL029.Text = tDTLA029;
                    TXTDTL030.Text = tDTLA030;
                    TXTDTL031.Text = tDTLA031;
                    TXTDTL032.Text = tDTLA032;
                    TXTDTL034.Text = tDTLA034;

                    //點擊放大圖片類處理
                    string[] arrayFileName2 = new string[] { tDTLA018, tDTLA021, tDTLA023, tDTLA025, tDTLA027, tDTLA029, tDTLA031 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj2 = new System.Web.UI.WebControls.HyperLink[] { HyperLink018, HyperLink021, HyperLink023, HyperLink025, HyperLink027, HyperLink029, HyperLink031 };

                    for (int i = 0; i < arrayFileName2.Length; i++)
                    {
                        string strFileName = arrayFileName2[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj2[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = "../tcgefile/SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                            ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                        }
                    }

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLA020 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link020 };

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
                            string tempLinkPateh = "../tcgefile/SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                    //審查意見-多筆
                    int nj = 0;
                    DataTable tbRVList = null;
                    string GetListSQLstr = " Select * From SWCDTL01R ";
                    GetListSQLstr += " Where SWC000 = '" + v + "' and DTLA001 = '" + v2 + "' ";
                    GetListSQLstr += " Order by convert(int,DTLR01) ";

                    string ReViewStr = "";
                    ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                    using (SqlConnection SwcConn2 = new SqlConnection(connectionString.ConnectionString))
                    {
                        SwcConn2.Open();

                        SqlDataReader readeDTLR;
                        SqlCommand objCmdDTLR = new SqlCommand(GetListSQLstr, SwcConn2);
                        readeDTLR = objCmdDTLR.ExecuteReader();

                        while (readeDTLR.Read())
                        {
                            nj++;
                            string tDTLR01 = readeDTLR["DTLR01"] + "";
                            string tDTLR02 = readeDTLR["DTLR02"] + "";
                            string tDTLR03 = readeDTLR["DTLR03"] + "";

                            ReViewStr += tDTLR02.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>") + "<br/><br/>";
                        }
                    }
                    LBReView.Text = ReViewStr;
                }
            }

        }
    }

    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RA" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RA" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLA000) AS MAXID from SWCDTL01 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLA000,7) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 3, 3)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(3,'0');
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

    
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL018", "TXTDTL020" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL018, TXTDTL020 };
        string csUpLoadField = "TXTDTL018";
        TextBox csUpLoadAppoj = TXTDTL018;

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
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".xls", ".xlsx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 Excel 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 5000000)
            {
                error_msg.Text = "請選擇 5Mb 以下檔案上傳，謝謝!!";
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
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
                    case "PIC":
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

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

    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        //OutPdf.Attributes.Add("onclick", "this.form.target='_blank'");
        Response.Redirect("../SwcReport/PdfSwcDtl01.aspx?SWCNO=" + rCaseId + "&DTLNO=" + rDTLId);
    }
}