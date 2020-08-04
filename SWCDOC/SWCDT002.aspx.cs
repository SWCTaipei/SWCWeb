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

public partial class SWCDOC_SWCDT002 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";

        Class01 SBApp = new Class01();

        if (rCaseId == "")
        {
            Response.Redirect("SWC000.aspx");
        }

        if (!IsPostBack)
        {
            GenerateDropDownList();
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
        Class01 SBApp = new Class01();

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
                    TXTDTL020.Text = tDTLB001;
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
                            string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    //圖片類處理
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
                        DataLock.Visible = false;
                        SaveCase.Visible = false;

                        error_msg.Text = SBApp.AlertMsg("資料已送出，目前僅供瀏覽。");
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

        Class01 SBApp = new Class01();

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

            string vCaseID = Request.QueryString["SWCNO"] + "";

            Response.Write("<script>alert('資料已存檔'); location.href='SWCDT002.aspx?SWCNO=" + vCaseID + "&DTLNO=" + sDTL000 + "'; </script>");

        }

    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL022", "TXTDTL023" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL022, TXTDTL023 };
        string csUpLoadField = "TXTDTL022";
        TextBox csUpLoadAppoj = TXTDTL022;

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
    protected void GenerateDropDownList()
    {
        //string[] array_DTL006 = new string[] { "1", "2", "3" };
        //DDLDTL006.DataSource = array_DTL006;
        //DDLDTL006.DataBind();

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
        Class01 MyBassAppPj = new Class01();
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

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 10000000)
            {
                error_msg.Text = "請選擇 10 Mb 以下檔案上傳，謝謝!!";
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
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        //Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);

        Response.Write("<script language='javascript'>window.close();</" + "script>");
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

        SaveCase_Click(sender, e);

    }

    protected void TXTDTL022_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL022_fileupload, TXTDTL022, "TXTDTL022", "_" + rDTLNO + "_02_sign", TXTDTL022_img, null);

        string sourceFILENAME = TXTDTL022.Text;
        string sourceURL = "http://211.22.61.183/tcge/UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = "D:\\miro55\\tslm\\web\\UpLoadFiles\\SwcCaseFile\\" + rDTLNO + "\\" + sourceFILENAME;
        string swctype = "DTL02";
        string doctype = "抽查紀錄";

        SWCDOCFilesyn("tcge", sourceURL, sourcePATH, "swcdoc", sourceFILENAME, LBSWC000.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }
    protected void TXTDTL023_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTDTL023_fileupload, TXTDTL023, "TXTDTL023", "_" + rDTLNO + "_02_doc", null, Link023);

        string sourceFILENAME = TXTDTL023.Text;
        string sourceURL = "http://211.22.61.183/tcge/UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = "D:\\miro55\\tslm\\web\\UpLoadFiles\\SwcCaseFile\\" + rDTLNO + "\\" + sourceFILENAME;
        string swctype = "DTL02";
        string doctype = "抽查紀錄";
        
        SWCDOCFilesyn("tcge", sourceURL, sourcePATH, "swcdoc", sourceFILENAME, LBSWC000.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }
    private void SWCDOCFilesyn(string sourcePC, string sourceURL, string sourcePATH, string targetPC, string sourceFILENAME, string caseid, string swctype, string doctype)
    {
        string targetURL = "";
        string targetPATH = "";
        DateTime requestTIME = DateTime.Now;
        string tSWC00 = LBSWC000.Text;

        switch (targetPC) {
            case "tcge":
                targetURL = "http://211.22.61.186/tcge/UpLoadFiles/SwcCaseFile/" + tSWC00 + "/" + sourceFILENAME;
                targetPATH = "D:\\miro55\\tslm\\web\\UpLoadFiles\\SwcCaseFile\\" + tSWC00 + "\\" + sourceFILENAME;                
                break;

            case "swcdoc":
                targetURL = "http://211.22.61.186/tcge/UpLoadFiles/SwcCaseFile/" + tSWC00 + "/" + sourceFILENAME;
                targetPATH = "E:\\Web\\SWCWeb\\UpLoadFiles\\SwcCaseFile\\" + tSWC00 + "\\" + sourceFILENAME;
                break;
        }

        //開始存吧
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["tslmConnectionString2"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            string SQLCOM = " INSERT INTO[UploadFileSyn] ";
            SQLCOM = SQLCOM + " ([sourcePC],[SOURCEURL],[SOURCEPATH],[TARGETPC],[TARGETURL],[TARGETPATH],[FILENAME],[REQUESTTIME],[PROCESSTIME],[HAVEPROCESS]) ";
            SQLCOM = SQLCOM + " VALUES ";
            SQLCOM = SQLCOM + " ('" + sourcePC + "','" + sourceURL + "','" + sourcePATH + "','" + targetPC + "','" + targetURL + "','" + targetPATH + "','" + sourceFILENAME + "','" + requestTIME.ToString("yyyy-MM-dd HH:mm:ss.000") + "','1911-01-01 00:00:00.000',0) ";

            SwcConn.Open();
            SqlCommand objCmdUpd = new SqlCommand(SQLCOM, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }

    }
}