using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_UserBoard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rqSearch = Request["SR"] + "";

        GBClass001 SBApp = new GBClass001();

        switch (ssUserType)
        {
            case "01":
                UserBoard00.Visible = true;
                break;
            case "02":
                TitleLink00.Visible = true;
                UserBoard00.Visible = true;
                break;
            case "03":
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                break;
            case "04":
                UserBoard00.Visible = true;
                break;
            default:
                Response.Redirect("SWC000.aspx");
                break;
        }

        //全區供用

        SBApp.ViewRecord("留言版-留言", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        textadd();

        //NewCase
        LBTB001.Text = NewCase();
    }

    //限字數
    public void textadd()
    {
        TXTTB003.Attributes.Add("maxlength", "50");
        //TXTTB003.Attributes.Add("onkeyup", "return ismaxlength(this)");
        TXTTB004.Attributes.Add("maxlength", "255");
        //TXTTB004.Attributes.Add("onkeyup", "return ismaxlength(this)");
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSaveSQL = "";
        string sEXESQLUPD = "";
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";

        #region set
        string pgTB01 = "";
        string pgTB02 = "";
        string pgTB03 = "";
        string pgTB04 = "";
        string pgTB05 = "";
        string pgTB06 = "";

        //string savedate = DateTime.Now.ToString("yyyy-MM-dd");
        #endregion

        #region 值
        pgTB01 = LBTB001.Text;
        pgTB02 = ssUserName;
        pgTB03 = TXTTB003.Text;
        pgTB04 = TXTTB004.Text;
        pgTB05 = HLTBPT05.Text;
        #endregion

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection HSIConn = new SqlConnection(connectionString.ConnectionString))
        {
            HSIConn.Open();

            string strSQLRV = " select * from [UserBoard] ";
            strSQLRV = strSQLRV + " where UB001 = '" + pgTB01 + "' ";
            //strSQLRV = strSQLRV + "   and saveuser = '" + ssUserID + "' ";

            SqlDataReader readeHSI;
            SqlCommand objCmdHSI = new SqlCommand(strSQLRV, HSIConn);
            readeHSI = objCmdHSI.ExecuteReader();

            if (!readeHSI.HasRows)
            {
                //pgTB01 = BC.GetMaxRF("Rfom_Facility", "RF001", "RF", pgTB07.Substring(3));
                sEXESQLUPD = " INSERT INTO UserBoard (UB001) VALUES ('" + pgTB01 + "');";
            }
            readeHSI.Close();
            objCmdHSI.Dispose();

            sEXESQLUPD += " Update UserBoard Set ";
            sEXESQLUPD += " UB002=@UB002, UB003=@UB003, UB004=@UB004, UB005=@UB005, UB006=@UB006, ";

            sEXESQLUPD += " saveuser='" + ssUserID + "', savedate=GETDATE() ";
            sEXESQLUPD += " where UB001='" + pgTB01 + "';";

            //NC("", "", "存檔", "", pgTB01, get_client_ip(0), sEXESQLUPD);
            //NC("", "", "存檔", "", pgTB01, get_client_ip(0), sSaveSQL);

            using (var cmd = HSIConn.CreateCommand())
            {
                cmd.CommandText = sEXESQLUPD;
                //設定值
                #region
                //基本資料
                //cmd.Parameters.Add(new SqlParameter("@FD001", pgTB01));
                cmd.Parameters.Add(new SqlParameter("@UB002", pgTB02));
                cmd.Parameters.Add(new SqlParameter("@UB003", pgTB03));
                cmd.Parameters.Add(new SqlParameter("@UB004", pgTB04));
                cmd.Parameters.Add(new SqlParameter("@UB005", pgTB05));
                cmd.Parameters.Add(new SqlParameter("@UB006", pgTB06));
                //cmd.Parameters.Add(new SqlParameter("@savedate", savedate));
                #endregion

                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }

        }

        Response.Write("<script>alert('資料已存檔');location.href='../SWCDOC/UserBoardList.aspx';</script>");
    }

    //上傳檔案
    protected void BtnFile_upload_Click(object sender, EventArgs e)
    {
        string qCaseidC = LBTB001.Text;
        string btnType = ((Button)(sender)).ID;
        string tSFName, filename, extension;
        bool rUP = false;

        switch (btnType)
        {
            case "BtnFilePT05_upload":
                tSFName = qCaseidC + "_01";
                filename = FULFilePT05.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp("PDFJPG", FULFilePT05, TBUpLoadFilePT05, "TBUpLoadFilePT05", tSFName, null, HLTBPT05, 10, qCaseidC, IMTBPT05);
                break;
        }
    }

    //上傳檔案fun
    private bool FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadReName, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize, string SubFolder, Image UpLoadImg)
    {
        string CaseId = LBTB001.Text + "";
        string UpLoadFileName = UpLoadReName;
        string UpLoadFileNameA = UpLoadReName + "A";
        bool rValue = false;

        #region.基本檢查
        string vTempValue = UpLoadText.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要更新，請先刪除原有檔案');</script>");
            return rValue;
        }
        string tUpLoadFile = UpLoadBar.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return rValue;
        }
        #endregion

        #region.檔案上傳
        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名
            switch (ChkType)
            {
                case "PDFJPG":
                    List<string> allowedExtextsion01 = new List<string> { ".JPG", ".jpg", ".PNG", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 JPG、PNG 檔案格式上傳，謝謝!!');</script>");
                        return rValue;
                    }
                    break;
            }

            //檔案大小限制
            int filesize = UpLoadBar.PostedFile.ContentLength;
            if (filesize > _FileMaxSize * 1000000)
            {
                Response.Write("<script>alert('請選擇  " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!');</script>");
                return rValue;
            }
            UpLoadFileName += extension;
            UpLoadFileNameA += extension;

            #region.上傳設定
            //檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath"] + "UserBoard\\" + CaseId;
            //if (SubFolder.Trim() != "") serverDir += "\\" + SubFolder;
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            Session[UpLoadStr] = "有檔案";

            string serverFilePath = Path.Combine(serverDir, UpLoadFileName);
            string serverFilePathA = Path.Combine(serverDir, UpLoadFileNameA);
            string fileNameOnly = Path.GetFileNameWithoutExtension(UpLoadFileName);
            //string serverUploadPath = ConfigurationManager.AppSettings["SwcFileUrl"];
            string serverUploadPath = "http:\\\\211.22.61.183\\swc6\\UpLoadFiles\\SwcCaseFile\\";

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePathA);

                //壓縮圖片
                if (UpLoadBar.HasFile)
                {
                    int maxWidth = 2048;   //圖片寬度最大限制
                    int maxHeight = 1536;  //圖片高度最大限制
                    System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(serverFilePathA);
                    int imgWidth = imgPhoto.Width;
                    int imgHeight = imgPhoto.Height;
                    if (imgWidth > imgHeight)  //如果寬度超過高度以寬度為準來壓縮
                    {
                        if (imgWidth > maxWidth)  //如果圖片寬度超過限制
                        {
                            float toImgWidth = maxWidth;   //圖片壓縮後的寬度
                            float toImgHeight = imgHeight / (float)(imgWidth / toImgWidth); //圖片壓縮後的高度

                            System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto, int.Parse(toImgWidth.ToString()), int.Parse(toImgHeight.ToString()));
                            img.Save(serverFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);  //儲存壓縮後的圖片
                        }
                        else
                        {
                            UpLoadBar.SaveAs(serverFilePath);
                        }
                    }
                    else
                    {
                        if (imgHeight > maxHeight)
                        {
                            float toImgHeight1 = maxHeight;
                            float toImgWidth1 = imgWidth / (float)(imgHeight / toImgHeight1);

                            System.Drawing.Bitmap img = new System.Drawing.Bitmap(imgPhoto, int.Parse(toImgWidth1.ToString()), int.Parse(toImgHeight1.ToString()));
                            img.Save(serverFilePath, System.Drawing.Imaging.ImageFormat.Jpeg);  //儲存壓縮後的圖片
                        }
                        else
                        {
                            UpLoadBar.SaveAs(serverFilePath);
                        }
                    }
                }

                switch (ChkType)
                {
                    case "PDFJPG":
                        UpLoadLink.Text = UpLoadFileName;
                        UpLoadLink.NavigateUrl = serverUploadPath + "UserBoard\\" + CaseId + "\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        UpLoadImg.ImageUrl = serverUploadPath + "UserBoard\\" + CaseId + "\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadImg.Visible = true;
                        break;
                }
                UpLoadText.Text = UpLoadFileName;

                #region.上傳成功存db
                rValue = true;
                #endregion
            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }
            #endregion
        }
        else
        {
            Session[UpLoadStr] = "";
        }

        return rValue;
        #endregion
    }

    //刪除上傳
    protected void BtnFile_del_Click(object sender, EventArgs e)
    {
        string btnType = ((Button)(sender)).ID;

        switch (btnType)
        {
            case "BtnFilePT05_del":
                HLTBPT05.Text = "";
                TBUpLoadFilePT05.Text = "";
                IMTBPT05.Visible = false;
                BtnFilePT05_del.Focus();
                break;
        }
    }

    public string NewCase()
    {
        Random R = new Random();
        string gg = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        string _ReturnVal = "UB" + gg.Substring(2) + R.Next(0, 10).ToString().PadLeft(1, '0');
        return _ReturnVal;
    }


}