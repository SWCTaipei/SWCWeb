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

public partial class SWCDOC_SWCBase001 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rBBTID = Request.QueryString["BillBID"] + "";

        string SessETID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        if (ssUserType == "03")
        {
            if (!IsPostBack) { GenerateDropDownList(); GetBillBoard(rBBTID); if(SessETID=="gv-admin") P_GV.Visible=true;}
        } else
        {
            Response.Redirect("../SWCDOC/SWC001.aspx");
        }

        switch (ssUserType)
        {
            case "01":
                break;
            case "02":
                TitleLink00.Visible = true;
                break;
            case "03":  //大地人員
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                break;
            case "04":
                break;
            default:
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
        }



        //以下全區公用
        SBApp.ViewRecord("公佈欄", "Update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }
    private void GenerateDropDownList()
    {
        //是否顯示
        string[] array_DropDownList1 = new string[] { "是", "否" };
        RABBShow.DataSource = array_DropDownList1;
        RABBShow.DataBind();
        RABBMain.DataSource = array_DropDownList1;
        RABBMain.DataBind();
        RABBMainGV.DataSource = array_DropDownList1;
        RABBMainGV.DataBind();

        //公佈單位
        string[] array_DropDownList2 = new string[] { "大地工程處審查科", "多維空間資訊" };
        DDLBBUnit.DataSource = array_DropDownList2;
        DDLBBUnit.DataBind();
    }

    private void GetBillBoard(string rCaseID)
    {
        if (rCaseID == "ADDNEW")
        {
            string sDEID = GetNewId();
            TXTBBNO.Text = sDEID;
        } else
        {
            ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
            {
                SwcConn.Open();

                string gDNSQLStr = " select * from BillBoard ";
                gDNSQLStr = gDNSQLStr + " where BBNo ='" + rCaseID + "' ";

                SqlDataReader readerDN;
                SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                { 
                    string sBBNo = readerDN["BBNo"] + "";
                    string sBBDateStart = readerDN["BBDateStart"] + "";
                    string sBBDateEnd = readerDN["BBDateEnd"] + "";
                    string sBBTitle = readerDN["BBTitle"] + "";
                    string sBBText = readerDN["BBText"] + "";
                    string sBBShow = readerDN["BBShow"] + "";
                    string sBBUnit = readerDN["BBUnit"] + "";
                    string sBBFile = readerDN["BBFile"] + "";
                    string sBBMain = readerDN["BBMain"] + "";
                    string sBBMainGV = readerDN["BBMainGV"] + "";

                    TXTBBNO.Text = sBBNo;
                    TXTBBDateStart.Text= sBBDateStart;
                    TXTBBDateEnd.Text= sBBDateEnd;
                    TXTBBTitle.Text= sBBTitle;
                    TXTBBText.Text= sBBText;
                    RABBShow.SelectedValue= sBBShow;
                    RABBMain.SelectedValue = sBBMain;
                    RABBMainGV.SelectedValue = sBBMainGV;
                    DDLBBUnit.SelectedValue= sBBUnit;
                    TXTBBFile.Text = sBBFile;

                    #region 檔案類處理
                    string[] arrayFileNameLink = new string[] { sBBFile };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { LinkFile };

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
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/"+ sBBNo+"/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }
                    }
                    #endregion
                }
                readerDN.Close();
                objCmdDN.Dispose();

            }
        }
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string gBBNO = TXTBBNO.Text + "";
        string gBBDate1 = TXTBBDateStart.Text + "";
        string gBBDate2 = TXTBBDateEnd.Text + "";
        string gBBTitle = TXTBBTitle.Text+"";
        string gBBText = TXTBBText.Text + "";
        string gBBShow = RABBShow.SelectedValue;
        string gBBMain = RABBMain.SelectedValue;
        string gBBMainGV = RABBMainGV.SelectedValue;
        string gBBUnit = DDLBBUnit.SelectedValue;
        string gBBFile = TXTBBFile.Text + "";

        #region chkeck input values
        if (gBBMain.Trim()=="") { Response.Write("<script>alert('請選擇是否為重要公告');</script>"); return; }
        #endregion

        string sEXESQLUPD = "";

        if (gBBText.Length>300) { gBBText = gBBText.Substring(0, 300); }

        //RABBShow
        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select BBNo from BillBoard ";
            gDNSQLStr = gDNSQLStr + " where BBNo ='" + gBBNO + "' ";

            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();

            if (!readerDN.HasRows)
            {
                sEXESQLUPD = " INSERT INTO BillBoard (BBNo) VALUES ('" + gBBNO + "');";
            }
            readerDN.Close();
            objCmdDN.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update BillBoard Set ";
            sEXESQLUPD = sEXESQLUPD + " BBDateStart = '" + gBBDate1.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBDateEnd = '" + gBBDate2.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBTitle = '" + gBBTitle.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBText = '" + gBBText.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBShow = '" + gBBShow.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBMain = '" + gBBMain.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBMainGV = '" + gBBMainGV.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBUnit = '" + gBBUnit.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBFile = '" + gBBFile.Replace("'", "''") + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " where BBNo = '" + gBBNO + "' ";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWCGOV012.aspx?BillBID=" + gBBNO + "';</script>");
                    break;
            }
            UpLoadTempFileMoveChk(gBBNO);
        }
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTBBFile" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTBBFile };
        string csUpLoadField = "TXTBBFile";
        TextBox csUpLoadAppoj = TXTBBFile;

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

    private string GetNewId()
    {
        string SearchValA = "BB" + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1911).ToString() + DateTime.Now.ToString("MM");
        string MaxKeyVal = "001";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRVa = " select MAX(BBNo) as MAXID from BillBoard ";
            strSQLRVa = strSQLRVa + " where LEFT(BBNo,7) ='" + SearchValA + "' ";

            SqlDataReader readerSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRVa, SwcConn);
            readerSwc = objCmdSwc.ExecuteReader();

            if (readerSwc.HasRows)
            {
                while (readerSwc.Read())
                {
                    string tempMaxKeyVal = readerSwc["MAXID"] + "";

                    if (tempMaxKeyVal != "") {
                        string tempValue = (Convert.ToInt32(tempMaxKeyVal.Substring(tempMaxKeyVal.Length - 3, 3)) + 1).ToString();
                        MaxKeyVal = tempValue.PadLeft(3, '0');
                    }
                }
            }
            objCmdSwc.Dispose();
            readerSwc.Close();
            SwcConn.Close();
        }
        return SearchValA+ MaxKeyVal;
    }
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWCGOV011.aspx");
    }
    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = TXTBBNO.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名
            switch (ChkType)
            {
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".pdf", ".odt", ".xls", ".xlsx", ".doc", ".docx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇pdf、odt或doc檔案格式上傳，謝謝!!");
                        return;
                    }
                    // 限制檔案大小，限制為 50MB
                    int filesize2 = UpLoadBar.PostedFile.ContentLength;

                    if (filesize2 > 50000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50 Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
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
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/"+ CaseId+"/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
        string csCaseID = TXTBBNO.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update BillBoard set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where BBNO = '" + csCaseID + "' ";

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
    protected void TXTBBFile_fileuploadok_Click(object sender, EventArgs e)
    {
        FileUpLoadApp("DOC", TXTBBFile_fileupload, TXTBBFile, "TXTBBFile", "_" + DateTime.Now.ToString("yyyyMMdd"), null, LinkFile);
    }

    protected void TXTBBFile_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTBBFile, null, LinkFile, "BBFile", "TXTBBFile", 320, 280);
    }
}