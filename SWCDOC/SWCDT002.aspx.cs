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

public partial class SWCDOC_SWCDT002 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";

        GBClass001 SBApp = new GBClass001();

        if (rCaseId == "")
        {
            Response.Redirect("SWC000.aspx");
        }

        if (!IsPostBack)
        {
            GenerateDropDownList();
            GV2Page(rCaseId, rDTLId);
            Data2Page(rCaseId, rDTLId);
        }

        //以下全區公用

        SBApp.ViewRecord("水土保持施工抽查紀錄", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + "，您好";
        }
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
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";

                LBSWC000.Text = v;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
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
                string strSQLRV2 = " select * from SWCDTL02 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   AND DTLB000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLB001 = readeDTL["DTLB001"] + "";
                    string tDTLB002 = readeDTL["DTLB002"] + "";
                    string tDTLB003 = readeDTL["DTLB003"] + "";
                    string tDTLB004 = readeDTL["DTLB004"] + "";
                    string tDTLB006 = readeDTL["DTLB006"] + "";
                    string tDTLB007 = readeDTL["DTLB007"] + "";
                    string tDTLB008 = readeDTL["DTLB008"] + "";
                    string tDTLB009 = readeDTL["DTLB009"] + "";
                    string tDTLB010 = readeDTL["DTLB010"] + "";
                    string tDTLB011 = readeDTL["DTLB011"] + "";
                    string tDTLB012 = readeDTL["DTLB012"] + "";
                    string tDTLB013 = readeDTL["DTLB013"] + "";
                    string tDTLB014 = readeDTL["DTLB014"] + "";
                    string tDTLB015 = readeDTL["DTLB015"] + "";
                    string tDTLB016 = readeDTL["DTLB016"] + "";
                    string tDTLB017 = readeDTL["DTLB017"] + "";
                    string tDTLB018 = readeDTL["DTLB018"] + "";
                    string tDTLB019 = readeDTL["DTLB019"] + "";
                    string tDTLB020 = readeDTL["DTLB020"] + "";
                    string tDTLB021 = readeDTL["DTLB021"] + "";
                    string tDTLB022 = readeDTL["DTLB022"] + "";
                    string tDTLB023 = readeDTL["DTLB023"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

                    LBDTL001.Text = tDTLB001;
                    TXTDTL002.Text = tDTLB002;
                    TXTDTL003.Text = tDTLB003;
                    TXTDTL004.Text = tDTLB004;
                    //TXTDTL006.Text = SBApp.DateView(tDTLB006, "00");
                    if (tDTLB007 == "1") { CHKDTL007.Checked = true; }
                    if (tDTLB008 == "1") { CHKDTL008.Checked = true; }
                    if (tDTLB009 == "1") { CHKDTL009.Checked = true; }
                    TXTDTL010.Text = tDTLB010;
                    DropList011.SelectedValue = tDTLB011;
                    DropList012.SelectedValue = tDTLB012;
                    DropList013.SelectedValue = tDTLB013;
                    TXTDTL014.Text = tDTLB014;
                    if (tDTLB015 == "1") { CHKBOX015.Checked = true; }
                    if (tDTLB016 == "1") { CHKBOX016.Checked = true; }
                    if (tDTLB017 == "1") { CHKBOX017.Checked = true; }
                    if (tDTLB018 == "1") { CHKBOX018.Checked = true; }
                    TXTDTL019.Text = tDTLB019;
                    TXTDTL020.Text = tDTLB020;
                    TXTDTL021.Text = tDTLB021;
                    TXTDTL022.Text = tDTLB022;
                    TXTDTL023.Text = tDTLB023;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLB023 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link023 };

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
                            string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName + "?ts=" + System.DateTime.Now.Millisecond;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    //點擊放大圖片類處理
                    string[] arrayFileName = new string[] { tDTLB022 };
                    System.Web.UI.WebControls.Image[] arrayImgAppobj = new System.Web.UI.WebControls.Image[] { TXTDTL022_img };

                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        string strFileName = arrayFileName[i];
                        System.Web.UI.WebControls.Image ImgFileObj = arrayImgAppobj[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            ImgFileObj.Attributes.Add("src", tempImgPateh + "?ts=" + DateTime.Now.Millisecond);
                        }
                    }
                    
                    //按鈕處理
                    if (tDATALOCK == "Y")
                    {
                        error_msg.Text = SBApp.AlertMsg("資料已送出，目前僅供瀏覽。");
                        DataLock.Visible = false;
                        SaveCase.Visible = false;
						
						TXTDTL002.Enabled = false;
                        CHKDTL007.Enabled = false;
                        CHKDTL008.Enabled = false;
                        CHKDTL009.Enabled = false;
                        TXTDTL010.Enabled = false;
                        DropList011.Enabled = false;
                        DropList012.Enabled = false;
                        DropList013.Enabled = false;
                        TXTDTL014.Enabled = false;
                        CHKBOX015.Enabled = false;
                        CHKBOX016.Enabled = false;
                        CHKBOX017.Enabled = false;
                        CHKBOX018.Enabled = false;
                        TXTDTL019.Enabled = false;
                        TXTDTL020.Enabled = false;
                        TXTDTL021.Enabled = false;
                        TXTDTL022_fileupload.Enabled = false;
                        TXTDTL022_fileuploadok.Enabled = false;
                        TXTDTL022_fileuploaddel.Enabled = false;
                        TXTDTL023_fileupload.Enabled = false;
                        TXTDTL023_fileuploadok.Enabled = false;
                        TXTDTL023_fileuploaddel.Enabled = false;
						
						DDL_Sign.Enabled = false;
						TB_Sign.Enabled = false;
						btn1.Enabled = false;
						btn2.Enabled = false;
						
						GVIMAGE.Columns[4].Visible = false;
                    }
                }
            }
        }
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        string sDTL000 = LBDTL001.Text + "";
        string sSWC002 = LBSWC002.Text+"";
        string sDTL002 = TXTDTL002.Text + "";
        string sDTL003 = TXTDTL003.Text + "";
        string sDTL004 = TXTDTL004.Text + "";
        string sDTL005 = LBSWC005.Text + "";
        string sDTL007 = "";
        string sDTL008 = "";
        string sDTL009 = "";
        string sDTL010 = TXTDTL010.Text + "";
        string sDTL011 = DropList011.SelectedValue + "";
        string sDTL012 = DropList012.SelectedValue + "";
        string sDTL013 = DropList013.SelectedValue + "";
        string sDTL014 = TXTDTL014.Text + "";
        string sDTL015 = "";
        string sDTL016 = "";
        string sDTL017 = "";
        string sDTL018 = "";
        string sDTL019 = TXTDTL019.Text + "";
        string sDTL020 = TXTDTL020.Text + "";
        string sDTL021 = TXTDTL021.Text + "";
        string sDTL022 = TXTDTL022.Text + "";
        string sDTL023 = TXTDTL023.Text + "";

        if (CHKDTL007.Checked) { sDTL007 = "1"; }
        if (CHKDTL008.Checked) { sDTL008 = "1"; }
        if (CHKDTL009.Checked) { sDTL009 = "1"; }
        if (CHKBOX015.Checked) { sDTL015 = "1"; }
        if (CHKBOX016.Checked) { sDTL016 = "1"; }
        if (CHKBOX017.Checked) { sDTL017 = "1"; }
        if (CHKBOX018.Checked) { sDTL018 = "1"; }

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL02 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   AND DTLB000 = '" + sDTL000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL02 (SWC000,DTLB000) VALUES ('" + sSWC000 + "','" + sDTL000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL02 Set ";
            sEXESQLSTR = sEXESQLSTR + " DTLB001 = DTLB000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLB002 ='" + sDTL002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB003 ='" + sDTL003 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB004 ='" + sDTL004 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB005 ='" + sDTL005 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLB006 ='" + sDTL006 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB007 ='" + sDTL007 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB008 ='" + sDTL008 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB009 ='" + sDTL009 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB010 ='" + sDTL010 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB011 ='" + sDTL011 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB012 ='" + sDTL012 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB013 ='" + sDTL013 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB014 ='" + sDTL014 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB015 ='" + sDTL015 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB016 ='" + sDTL016 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB017 ='" + sDTL017 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB018 ='" + sDTL018 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB019 ='" + sDTL019 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB020 ='" + sDTL020 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB021 ='" + sDTL021 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB022 ='" + sDTL022 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLB023 ='" + sDTL023 + "', ";

            sEXESQLSTR = sEXESQLSTR + " SWC002 ='" + sSWC002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLB000 = '" + sDTL000 + "'";

            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
			//存image列表
			SAVE_IMAGE_LIST(sSWC000,sDTL000);
            string vCaseID = Request.QueryString["SWCNO"] + "";

            Response.Write("<script>alert('資料已存檔'); location.href='SWC003.aspx?SWCNO=" + vCaseID + "'; </script>");
        }

    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL022", "TXTDTL023" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL022, TXTDTL023 };
        string csUpLoadField = "TXTDTL022";
        TextBox csUpLoadAppoj = TXTDTL022;

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
    protected void GenerateDropDownList()
    {
        string[] array_YesNo = new string[] { "", "是", "否" };
        DropList011.DataSource = array_YesNo;
        DropList011.DataBind();
        DropList011.SelectedValue = "是";

        DropList012.DataSource = array_YesNo;
        DropList012.DataBind();
        DropList012.SelectedValue = "是";

        DropList013.DataSource = array_YesNo;
        DropList013.DataBind();
        DropList013.SelectedValue = "是";
        
        TXTDTL021.Text = "(一)檢查單位及人員：" + System.Environment.NewLine + "(二)承辦監造技師：" + System.Environment.NewLine + "(三)水土保持義務人：";
    }
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RB" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RB" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLB000) AS MAXID from SWCDTL02 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLB000,7) = '" + tempVal + "' ";

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
                    List<string> allowedExtextsion02 = new List<string> { ".pdf" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 10MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 10000000)
            {
                error_msg.Text = "請選擇 10 Mb 以下檔案上傳，謝謝!!";
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            //SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
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

                string thUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                switch (ChkType)
                {
                    case "PIC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = thUrl;
						IMAGE_TO_LIST("2", SwcFileName);
                        break;

                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = thUrl;
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
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLB000 = LBDTL001.Text + "";

        string sEXESQLSTR = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            
            string strSQLRV = " select * from SWCDTL02 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   AND DTLB000 = '" + sDTLB000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL02 (SWC000,DTLB000) VALUES ('" + sSWC000 + "','" + sDTLB000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL02 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLB000 = '" + sDTLB000 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }
        SendMailNotice(sSWC000);
        SaveCase_Click(sender, e);
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

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL,SWC.SWC024ID, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
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
                string tSWC024ID = readeSwc["SWC024ID"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄件名單, 注意：ge-10754	施柏宇，寫死，必需寄！

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10754")
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;

                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增施工抽查紀錄";
                        string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增施工抽查紀錄，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }

                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01  = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增施工檢查紀錄";
                //string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增施工檢查紀錄，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                string[] arraySentMail02a = new string[] { tETEmail };
                string[] arraySentMail02b = new string[] { tSWC108 };
                string[] arraySentMail02c = new string[] { SBApp.GetGeoUser(tSWC024ID, "Email") };
                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】已新增施工抽查紀錄";
                string ssMailBody02 = "您好，" + "【" + tSWC005 + "】已新增施工抽查紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo02a = SBApp.Mail_Send(arraySentMail02a, ssMailSub02, ssMailBody02);
                bool MailTo02b = SBApp.Mail_Send(arraySentMail02b, ssMailSub02, ssMailBody02);
                bool MailTo02c = SBApp.Mail_Send(arraySentMail02c, ssMailSub02, ssMailBody02);

                string ssMailBody03 = "您好，【" + tSWC005 + "】已新增施工抽查紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。";

                SBApp.SendSMS(tSWC013TEL, ssMailBody03);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }

    protected void TXTDTL022_fileuploadok_Click(object sender, EventArgs e)
    {
        //目前最大NO
		string no_now = "";
		string no_new = "01";
		if(GVIMAGE.Rows.Count > 0)
		{
			
			no_now = GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text;
			//目前最大NO+1(最新的)
			no_new = "0" + (Convert.ToInt32(GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text) + 1).ToString();
			no_new = no_new.Substring(no_new.Length-2,2);
		}
		
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL022_fileupload, TXTDTL022, "TXTDTL022", "_" + rDTLNO + "_02" + no_new + "_sign", null, Link022);
    }
    protected void TXTDTL023_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTDTL023_fileupload, TXTDTL023, "TXTDTL023", "_" + rDTLNO + "_02_doc", null, Link023);

        //string sourceFILENAME = TXTDTL023.Text;
        //string sourceURL = ConfigurationManager.AppSettings["thisip"] + "tslmwork/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + sourceFILENAME;
        //string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        //string swctype = "DTL02";
        //string doctype = "抽查紀錄";
        //
        //if (sourceFILENAME.Trim() != "") { SWCDOCFilesyn("tcge", sourceURL, sourcePATH, "swcdoc", sourceFILENAME, LBSWC000.Text, swctype, doctype);}
        //SWCDOCFilesyn("tcge", sourceURL, sourcePATH, "swcdoc", sourceFILENAME, LBSWC000.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }
    private void SWCDOCFilesyn(string sourcePC, string sourceURL, string sourcePATH, string targetPC, string sourceFILENAME, string caseid, string swctype, string doctype)
    {
        //string targetURL = "";
        //string targetPATH = "";
        //DateTime requestTIME = DateTime.Now;
        //string tSWC00 = LBSWC000.Text;
		//
        //switch (targetPC) {
        //    case "tcge":
        //        targetURL = ConfigurationManager.AppSettings["thisip"] + "tcge/UpLoadFiles/SwcCaseFile/" + tSWC00 + "/" + sourceFILENAME;
        //        targetPATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + tSWC00 + "\\" + sourceFILENAME;                
        //        break;
		//
        //    case "swcdoc":
        //        targetURL = ConfigurationManager.AppSettings["thisip"] + "tcge/UpLoadFiles/SwcCaseFile/" + tSWC00 + "/" + sourceFILENAME;
        //        targetPATH = ConfigurationManager.AppSettings["SwcDocFilePath"].Replace("\\", "\\\\") + tSWC00 + "\\" + sourceFILENAME;
        //        break;
        //}
		//
        ////開始存吧
        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TslmConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        //{
        //    string SQLCOM = " INSERT INTO[UploadFileSyn] ";
        //    SQLCOM = SQLCOM + " ([sourcePC],[SOURCEURL],[SOURCEPATH],[TARGETPC],[TARGETURL],[TARGETPATH],[FILENAME],[REQUESTTIME],[PROCESSTIME],[HAVEPROCESS]) ";
        //    SQLCOM = SQLCOM + " VALUES ";
        //    SQLCOM = SQLCOM + " ('" + sourcePC + "','" + sourceURL + "','" + sourcePATH + "','" + targetPC + "','" + targetURL + "','" + targetPATH + "','" + sourceFILENAME + "','" + requestTIME.ToString("yyyy-MM-dd HH:mm:ss.000") + "','1911-01-01 00:00:00.000',0) ";
		//
        //    SwcConn.Open();
        //    SqlCommand objCmdUpd = new SqlCommand(SQLCOM, SwcConn);
        //    objCmdUpd.ExecuteNonQuery();
        //    objCmdUpd.Dispose();
        //}
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

        strSQLClearFieldValue = " update SWCDTL02 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLB001 = '" + csDTLID + "' ";

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
            case "PIC":
                ImgView.Attributes.Remove("src");
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
    protected void TXTDTL022_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL022, null, Link022, "DTLB022", "TXTDTL022", 320, 180);
    }
    protected void TXTDTL023_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTDTL023, null, Link023, "DTLB023", "TXTDTL023", 320, 180);

    }
	private void GV2Page(string v, string v2)
    {
		string sqlStr = " select * from SWCDTL02_IMAGE where SWC000=@SWC000 and DTLB000=@DTLB000 order by NO; ";
		
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];

		if (tbIMAGE == null)
		{
			DataTable GVTBIMAGE = new DataTable();
		
			GVTBIMAGE.Columns.Add(new DataColumn("NO", typeof(string)));
			GVTBIMAGE.Columns.Add(new DataColumn("IDENTITY", typeof(string)));
			GVTBIMAGE.Columns.Add(new DataColumn("NAME", typeof(string)));
			GVTBIMAGE.Columns.Add(new DataColumn("IMAGENAME", typeof(string)));
		
			ViewState["SwcIMAGE"] = GVTBIMAGE;
			tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		}
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.Parameters.Add(new SqlParameter("@DTLB000", v2));
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                        while (readerSWC.Read())
                        {
							DataRow GVTBIMAGERow = tbIMAGE.NewRow();
			
							GVTBIMAGERow["NO"] = readerSWC["NO"] + "";
							GVTBIMAGERow["IDENTITY"] = readerSWC["IDENTITY"] + "";
							GVTBIMAGERow["NAME"] = readerSWC["NAME"] + "";
							GVTBIMAGERow["IMAGENAME"] = readerSWC["IMAGENAME"] + "";
			
							tbIMAGE.Rows.Add(GVTBIMAGERow);
			
							//Store the DataTable in ViewState
							ViewState["SwcIMAGE"] = tbIMAGE;
			
							GVIMAGE.DataSource = tbIMAGE;
							GVIMAGE.DataBind();
                        }
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
	}
	protected void GVIMAGE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                break;
            case DataControlRowType.DataRow:
                //HyperLink Hyper = new HyperLink();
                //Hyper.Text = e.Row.Cells[3].Text;
				//Hyper.Target = "_blank";
                //Hyper.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + e.Row.Cells[3].Text;
                //e.Row.Cells[3].Controls.Add(Hyper);
				
				HyperLink hl = (HyperLink)e.Row.FindControl("link"); 
				if (hl != null) 
				{
					hl.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + hl.Text;
				}
                break;
        }
    }
	//加入清單 p1=>簽名(1)上傳(2) p2=>上傳檔名
	private void IMAGE_TO_LIST(string p1, string p2)
    {
		string rCaseId = Request.QueryString["SWCNO"] + "";
		string rDTLNO = LBDTL001.Text + "";
		
		//目前最大NO
		string no_now = "";
		//目前最大NO+1(最新的)
		string no_new = "01";
		if(GVIMAGE.Rows.Count > 0)
		{
			no_now = GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text;
			no_new = "0" + (Convert.ToInt32(GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text) + 1).ToString();
			no_new = no_new.Substring(no_new.Length-2,2);
		}
		
		string SwcFileName = "";
		
		//手寫簽名
		if(p1 == "1")
		{
			//上傳檔案
			string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + rCaseId;
			
			if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
			
			SwcFileName = rCaseId + "_" + rDTLNO + "_02"+no_new+"_sign.png";
			
			string serverFilePath = Path.Combine(serverDir, SwcFileName);
			
			
			//---------------------------------------
			string dir = serverDir;
			bool dirExists = Directory.Exists(dir);
			if (!dirExists)
			    Directory.CreateDirectory(dir);
			string SavePath = serverFilePath;
			var bytes = Convert.FromBase64String(hfImageData.Text.Replace("data:image/png;base64,",""));
			using (var imageFile = new FileStream(SavePath, FileMode.Create))
			{
			    imageFile.Write(bytes, 0, bytes.Length);
			    imageFile.Flush();
			}
			//---------------------------------------
			
			string thUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + SwcFileName;
			
			
		}
		//上傳檔案
		else
		{
			SwcFileName = p2;
		}
		//加入清單
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		DataRow GVTBIMAGERow = tbIMAGE.NewRow();
		
		GVTBIMAGERow["NO"] = no_new;
		GVTBIMAGERow["IDENTITY"] = DDL_Sign.SelectedValue;
		GVTBIMAGERow["NAME"] = TB_Sign.Text;
		GVTBIMAGERow["IMAGENAME"] = SwcFileName;
		
		tbIMAGE.Rows.Add(GVTBIMAGERow);
		
		//Store the DataTable in ViewState
		ViewState["SwcIMAGE"] = tbIMAGE;
		
		GVIMAGE.DataSource = tbIMAGE;
		GVIMAGE.DataBind();
	}
	protected void btn2_Click(object sender, EventArgs e)
    {
        IMAGE_TO_LIST("1","");
    }
	protected void GVIMAGE_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		int index = Convert.ToInt32(e.RowIndex);
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		tbIMAGE.Rows[index].Delete();
		ViewState["SwcIMAGE"] = tbIMAGE;
		GVIMAGE.DataSource = tbIMAGE;
		GVIMAGE.DataBind();
	}
	protected void SAVE_IMAGE_LIST(string sSWC000, string ssDTL000)
	{
		string sqlstr = " delete SWCDTL02_IMAGE where SWC000=@SWC000 and DTLB000=@DTLB000;";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = sqlstr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
				cmd.Parameters.Add(new SqlParameter("@DTLB000", ssDTL000));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
		
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		for(int i = 0; i < tbIMAGE.Rows.Count; i++)
		{
			sqlstr = " insert into SWCDTL02_IMAGE(SWC000,DTLB000,NO,[IDENTITY],NAME,IMAGENAME) values (@SWC000,@DTLB000,@NO,@IDENTITY,@NAME,@IMAGENAME); ";
			
			using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
			{
				SwcConn.Open();
				
				using (var cmd = SwcConn.CreateCommand())
				{
					cmd.CommandText = sqlstr;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
					cmd.Parameters.Add(new SqlParameter("@DTLB000", ssDTL000));
					cmd.Parameters.Add(new SqlParameter("@NO", tbIMAGE.Rows[i].ItemArray[0]));
					cmd.Parameters.Add(new SqlParameter("@IDENTITY", tbIMAGE.Rows[i].ItemArray[1]));
					cmd.Parameters.Add(new SqlParameter("@NAME", tbIMAGE.Rows[i].ItemArray[2]));
					cmd.Parameters.Add(new SqlParameter("@IMAGENAME", tbIMAGE.Rows[i].ItemArray[3]));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
	}
}