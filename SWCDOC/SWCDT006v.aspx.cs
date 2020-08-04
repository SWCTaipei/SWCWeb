/*  Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCDT006 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
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
            Response.Redirect("SWC000.aspx");
        }

        if (!IsPostBack)
        {
            Data2Page(rCaseId, rDTLId);
        }
        
        if (rRRPage == "55")
        {
            GoHomePage.Visible = false;
        }
        //全區供用

        SBApp.ViewRecord("水土保持完工檢查紀錄表", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
    }
    private void Data2Page(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

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
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC021 = readeSwc["SWC021"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";
                string tSWC058 = readeSwc["SWC058"] + "";

                LBSWC000.Text = v;
                LBSWC005.Text = tSWC005;
                LBSWC005a.Text = tSWC005;
                LBSWC007.Text = tSWC007;
                LBSWC013ID.Text = tSWC013ID;
                LBSWC013.Text = tSWC013;
                LBSWC014.Text = tSWC014;
                LBSWC021.Text = tSWC045;
                LBSWC021Name.Text = SBApp.GetETUser(tSWC045ID, "OrgName");
                LBSWC021OrgIssNo.Text = SBApp.GetETUser(tSWC045ID, "OrgIssNo");
                LBSWC021OrgGUINo.Text = SBApp.GetETUser(tSWC045ID, "OrgGUINo");
                LBSWC021OrgTel.Text = SBApp.GetETUser(tSWC045ID, "OrgTel");
                LBSWC038.Text = SBApp.DateView(tSWC038, "04");
                LBSWC039.Text = tSWC039;
                LBSWC043.Text = SBApp.DateView(tSWC043, "04");
                LBSWC044.Text = tSWC044;
                LBSWC051.Text = SBApp.DateView(tSWC051, "04");
                LBSWC052.Text = SBApp.DateView(tSWC052, "04");
                LBSWC058.Text = SBApp.DateView(tSWC058, "04");
            }

            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string nIDF = GetDTLFID(v);

                LBDTL001.Text = nIDF;
            }
            else
            {
                string strSQLRV2 = " select * from SWCDTL06 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   and DTLF000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLF001 = readeDTL["DTLF001"] + "";
                    string tDTLF002 = readeDTL["DTLF002"] + "";
                    string tDTLF003 = readeDTL["DTLF003"] + "";
                    string tDTLF004 = readeDTL["DTLF004"] + "";
                    string tDTLF023 = readeDTL["DTLF023"] + "";
                    string tDTLF024 = readeDTL["DTLF024"] + "";
                    string tDTLF025 = readeDTL["DTLF025"] + "";
                    string tDTLF026 = readeDTL["DTLF026"] + "";
                    string tDTLF027 = readeDTL["DTLF027"] + "";
                    string tDTLF028 = readeDTL["DTLF028"] + "";
                    string tDTLF029 = readeDTL["DTLF029"] + "";
                    string tDTLF030 = readeDTL["DTLF030"] + "";
                    string tDTLF031 = readeDTL["DTLF031"] + "";
                    string tDTLF032 = readeDTL["DTLF032"] + "";
                    string tDTLF033 = readeDTL["DTLF033"] + "";
                    string tDTLF034 = readeDTL["DTLF034"] + "";
                    string tDTLF035 = readeDTL["DTLF035"] + "";
                    string tDTLF036 = readeDTL["DTLF036"] + "";
                    string tDTLF037 = readeDTL["DTLF037"] + "";
                    string tDTLF038 = readeDTL["DTLF038"] + "";
                    string tDTLF039 = readeDTL["DTLF039"] + "";
                    string tDTLF040 = readeDTL["DTLF040"] + "";
                    string tDTLF041 = readeDTL["DTLF041"] + "";
                    string tDTLF042 = readeDTL["DTLF042"] + "";

                    string tLOCKDATE = readeDTL["savedate"] + "";

                    LBDTL001.Text = tDTLF001;
                    TXTDTL002.Text = SBApp.DateView(tDTLF002, "04"); ;
                    TXTSENDDATE.Text = SBApp.DateView(tLOCKDATE, "04");
                    DDLDTL003.Text = tDTLF003;
                    TXTDTL004.Text = tDTLF004;                    
                    TXTDTL023.Text = tDTLF023;
                    TXTDTL024.Text = tDTLF024;
                    TXTDTL025.Text = tDTLF025.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL026.Text = tDTLF026.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    DDLDTL027.Text = tDTLF027;
                    TXTDTL028.Text = tDTLF028.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL029.Text = tDTLF029;
                    TXTDTL030.Text = tDTLF030;
                    TXTDTL031.Text = tDTLF031;
                    TXTDTL032.Text = tDTLF032;
                    TXTDTL033.Text = tDTLF033;
                    TXTDTL034.Text = tDTLF034;
                    TXTDTL035.Text = tDTLF035;
                    TXTDTL036.Text = tDTLF036;
                    TXTDTL037.Text = tDTLF037;
                    TXTDTL038.Text = tDTLF038;
                    TXTDTL039.Text = tDTLF039;
                    TXTDTL040.Text = tDTLF040;
                    TXTDTL041.Text = tDTLF041;
                    TXTDTL042.Text = tDTLF042;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLF024, tDTLF042 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link024, Link042 };

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
                            string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    //點擊放大圖片類處理
                    string[] arrayFileName = new string[] { tDTLF029, tDTLF030, tDTLF032, tDTLF034, tDTLF036, tDTLF038, tDTLF040 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj = new System.Web.UI.WebControls.HyperLink[] { HyperLink029, HyperLink030, HyperLink032, HyperLink034, HyperLink036, HyperLink038, HyperLink040 };

                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        string strFileName = arrayFileName[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                            ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                        }
                    }
                }
            }

        }
    }
    
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL024", "TXTDTL029" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL024, TXTDTL029 };
        string csUpLoadField = "TXTDTL024";
        TextBox csUpLoadAppoj = TXTDTL024;

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

    private string GetDTLFID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RF" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RF" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLF000) AS MAXID from SWCDTL06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLF000,7) = '" + tempVal + "' ";

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

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
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
                    List<string> allowedExtextsion02 = new List<string> { ".xls",".xlsx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 excel 檔案格式上傳，謝謝!!");
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
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csDTLID = LBDTL001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCDTL06 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLF001 = '" + csDTLID + "' ";

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
    
    protected void TXTDTL024_fileclean_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL024, null, Link024, "DTLF024", "TXTDTL024", 0, 0);
    }
    protected void TXTDTL029_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL029, TXTDTL029_img, null, "DTLF029", "TXTDTL029", 320, 180);
    }
    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        Response.Redirect("../SwcReport/PdfSwcDtl06.aspx?SWCNO=" + rCaseId + "&DTLNO=" + rDTLId);
    }

}