using EASendMail;
using iTextSharp.text.pdf;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;

/// <summary>
/// Class1 的摘要描述
/// </summary>
public class Class1 : System.Web.UI.Page
{
    public Class1()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public string GetClientIP()
    {
        string ip = "";
        try
        {
            if (Context.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
            {
                ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 得到真实的客户端地址
            }
            else    //如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
            {
                ip = Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 得到服务端的地址
            }
        } catch { }
        return ip;
    }
	public string getFilePath(string fileType, string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05)
    {
        string rValue = ConfigurationManager.AppSettings["SwcFilePath20"];
        if (tmpPath01.Trim() != "") { rValue += tmpPath01 + "\\"; }
        if (tmpPath02.Trim() != "") { rValue += tmpPath02 + "\\"; }
        if (tmpPath03.Trim() != "") { rValue += tmpPath03 + "\\"; }
        if (tmpPath04.Trim() != "") { rValue += tmpPath04 + "\\"; }
        if (tmpPath05.Trim() != "") { rValue += tmpPath05 + "\\"; }
        return rValue;
    }
	public string getTmpFilePath(string fileType, string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05)
    {
        string rValue = ConfigurationManager.AppSettings["SwcFileTemp20"];
        if (tmpPath01.Trim() != "") { rValue += tmpPath01 + "\\"; }
        if (tmpPath02.Trim() != "") { rValue += tmpPath02 + "\\"; }
        if (tmpPath03.Trim() != "") { rValue += tmpPath03 + "\\"; }
        if (tmpPath04.Trim() != "") { rValue += tmpPath04 + "\\"; }
        if (tmpPath05.Trim() != "") { rValue += tmpPath05 + "\\"; }
        return rValue;
    }
	public string getSwcDocRFileUrl(string fileType, string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05)
    {
        string rValue = "";
        switch (fileType) {
            case "BillBoard":
                rValue = ConfigurationManager.AppSettings["SwcDoc8Url"] + "UpLoadFiles/SwcCaseFile/";
                break;
            default:
                rValue = ConfigurationManager.AppSettings["SwcDoc8Url"] + "UpLoadFiles/SwcCaseFile/";
                break;
        }
        if (tmpPath01.Trim() != "") { rValue += tmpPath01 + "/"; }
        if (tmpPath02.Trim() != "") { rValue += tmpPath02 + "/"; }
        if (tmpPath03.Trim() != "") { rValue += tmpPath03 + "/"; }
        if (tmpPath04.Trim() != "") { rValue += tmpPath04 + "/"; }
        if (tmpPath05.Trim() != "") { rValue += tmpPath05 + "/"; }
        return rValue;
    }
    public string getFileUrl(string fileType,string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05)
    {
        string rValue = ConfigurationManager.AppSettings["SwcFileUrl20"]+ "SWCDOC/UpLoadFiles/SwcCaseFile/";
        if (tmpPath01.Trim() != "") { rValue += tmpPath01 + "/"; }
        if (tmpPath02.Trim() != "") { rValue += tmpPath02 + "/"; }
        if (tmpPath03.Trim() != "") { rValue += tmpPath03 + "/"; }
        if (tmpPath04.Trim() != "") { rValue += tmpPath04 + "/"; }
        if (tmpPath05.Trim() != "") { rValue += tmpPath05 + "/"; }
        return rValue;
    }
    
    
    
    public string maxStr(string myString,int maxLen)
    {   try { 
        string rStr = myString.Length > maxLen ? myString.Substring(0, maxLen) : myString; return rStr;
        } catch { return myString; } }
    public bool Mail_Send(string[] MailTos, string MailSub, string MailBody)
    {
		GBClass001 GBA = new GBClass001();
        #region setMail
        //// 寄信人Email
        //string sendMail = ConfigurationManager.AppSettings["sendMail"].Trim();
        //// 寄信smtp server
        //string smtpServer = ConfigurationManager.AppSettings["smtpServer"].Trim();
        //// 寄信smtp server的Port，預設25
        //int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].Trim());
        //// 寄信帳號
        //string mailAccount = ConfigurationManager.AppSettings["mailAccount"].Trim();
        //// 寄信密碼
        //string mailPwd = ConfigurationManager.AppSettings["mailPwd"].Trim();
		//
        //string MailFrom = sendMail;
        //try
        //{
        //    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //    using (SqlConnection SwcConnM = new SqlConnection(connectionString.ConnectionString))
        //    {
        //        SwcConnM.Open();
		//
        //        string strSQLUser = " select * from SysValue where GVType = 'EMail' ";
		//
        //        SqlDataReader readerSwcM;
        //        SqlCommand objCmdSwcM = new SqlCommand(strSQLUser, SwcConnM);
        //        readerSwcM = objCmdSwcM.ExecuteReader();
		//
        //        while (readerSwcM.Read())
        //        {
        //            string tmpType = readerSwcM["GVTypeDesc"] + "";
        //            string tmpValue = readerSwcM["GVValue"] + "";
		//
        //            switch (tmpType)
        //            {
        //                case "sendMail":
        //                    sendMail = tmpValue;
        //                    break;
        //                case "smtpServer":
        //                    smtpServer = tmpValue;
        //                    break;
        //                case "smtpPort":
        //                    smtpPort = Convert.ToInt32(tmpValue);
        //                    break;
        //                case "mailAccount":
        //                    mailAccount = tmpValue;
        //                    break;
        //                case "mailPwd":
        //                    mailPwd = tmpValue;
        //                    break;
        //            }
        //        }
        //        readerSwcM.Close();
        //        objCmdSwcM.ExecuteNonQuery();
        //        objCmdSwcM.Dispose();
        //    }
        //} catch { }
		//
        //string ppo = "";
        //if (MailTos != null)//防呆
        //{
        //    for (int i = 0; i < MailTos.Length; i++)
        //    {
        //        //加入信件的收信人(們)address
        //        if (!string.IsNullOrEmpty(MailTos[i].Trim()))
        //        {
        //            ppo += ppo == "" ? "" : ",";
        //            ppo += MailTos[i].Trim();
        //        }
        //    }
        //}
        #endregion
        //string MailSub = "test"; string MailBody = "gggg";

        //try
        //{
        //    SmtpMail oMail = new SmtpMail("TryIt");
		//
        //    oMail.From = MailFrom;
        //    oMail.To = ppo;
        //    oMail.Subject = MailSub;
        //    oMail.HtmlBody = MailBody;
		//
        //    SmtpServer oServer = new SmtpServer(smtpServer);
        //    oServer.User = mailAccount;
        //    oServer.Password = mailPwd;
        //    oServer.Port = 465;
        //    oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        //    EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();
        //    oSmtp.SendMail(oServer, oMail);
        //    return true;
        //}
        //catch (Exception ep)
        //{
        //    return false;
        //}
		return GBA.MailSend(MailTos,MailSub,MailBody,"","");
    }

    public void RvSysRecord(string vPageMain, string vPage, string vCRUD, string vDBID, string vKeyID, string SQL)
    {
        #region syslog v2
        string ssUserID = "";
        string tClientIP = GetClientIP();
        try
        {
            ssUserID = HttpContext.Current.Session["UID"].ToString();
        }
        catch
        {
            ssUserID = "";
        }
        if (ssUserID == "") return;

        string iRecord = " INSERT INTO Swclog ([AddTTL], [SysPageMain], [SysPage], [SysCRUD], [KeyID01], [KeyID02], [RcUser], [RcTime], [RcIP], [RcSQL]) VALUES ";
        iRecord += "(0, @SysPageMain, @SysPage, @SysCRUD, @KeyID01, @KeyID02, @RcUser, getdate(), @RcIP, @RcSQL)";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = iRecord;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SysPageMain", vPageMain));
                cmd.Parameters.Add(new SqlParameter("@SysPage", vPage));
                cmd.Parameters.Add(new SqlParameter("@SysCRUD", vCRUD));
                cmd.Parameters.Add(new SqlParameter("@KeyID01", vDBID));
                cmd.Parameters.Add(new SqlParameter("@KeyID02", vKeyID));
                cmd.Parameters.Add(new SqlParameter("@RcUser", ssUserID));
                cmd.Parameters.Add(new SqlParameter("@RcIP", tClientIP));
                cmd.Parameters.Add(new SqlParameter("@RcSQL", SQL));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
    }
    public void GetFileFromSwcDoc(string tmpFilePath, string tmpFileUrl)
    {
        if (!System.IO.File.Exists(tmpFilePath))
        {
            
        }
    }
	public void FilesSortOut(string fName, string fid, string fid2)
    {
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];
        string chkFileName = "";
        string tmpFileName = "";
        if (fName.Trim() != "" && fid.Trim() != "")
        {
            chkFileName = fid2.Trim() == "" ? SwcCaseFolderPath + fid + "\\" + fName : SwcCaseFolderPath + fid + "\\" + fid2 + "\\" + fName;
            tmpFileName = fid2.Trim() == "" ? TempFolderPath + fid + "\\" + fName : SwcCaseFolderPath + fid + "\\" + fid2 + "\\" + fName;
            //檔案不存在，從temp COPY
            if (!System.IO.File.Exists(chkFileName))
            {
                if (System.IO.File.Exists(tmpFileName))
                {
                    File.Copy(tmpFileName, chkFileName);
                }
                else
                {
                    string tMailSub = "書件平台檔案同步通知信-"+DateTime.Now.ToString("yyyyMMddhhmmss");
                    string tMailText = "書件平台檔案："+ fName+"，尚未同步";
                    tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                    tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                    string[] arraySentMail01 = new string[] { "ge-tslm@mail.taipei.gov.tw", "tcge7@geovector.com.tw", "hhsu@geovector.com.tw" };
                    //Mail_Send(arraySentMail01, tMailSub, tMailText);
                }
            }
            if (!System.IO.File.Exists(chkFileName))
            {
                string tMailSub = "書件平台檔案同步通知信-" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string tMailText = "書件平台檔案：" + chkFileName + "，尚未同步";
                tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                string[] arraySentMail01 = new string[] { "ge-tslm@mail.taipei.gov.tw", "tcge7@geovector.com.tw", "hhsu@geovector.com.tw" };
                //Mail_Send(arraySentMail01, tMailSub, tMailText);
            }
        }
    }
	public void FilesSortOut20(string tmpFileName, string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05, string tmpFileType) 
    {
        bool haveFile = false;
        string chkPahtAddFile = getFilePath("", tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05) + tmpFileName;
        #region temp
        haveFile = (System.IO.File.Exists(chkPahtAddFile)) ? true : MoveFileFun01(tmpFileName, tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05);
        haveFile = haveFile ? true : MoveFileFun02(tmpFileName, tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05, tmpFileType);
        #endregion
    }
	private bool MoveFileFun02(string tmpFileName, string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05,string tmpFileType)
    {
        bool rValue = false;
        string webUrl = getSwcDocRFileUrl(tmpFileType, tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05) + tmpFileName;
        string myComputerFile = getFilePath("",tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05) + tmpFileName;
        WebClient wc = new WebClient();
        wc.DownloadFile(webUrl, myComputerFile);
        return System.IO.File.Exists(myComputerFile);
    }

    private bool MoveFileFun01(string tmpFileName, string tmpPath01, string tmpPath02, string tmpPath03, string tmpPath04, string tmpPath05)
    {
        bool rValue = false;
        string pTmpFile = getTmpFilePath("", tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05) + tmpFileName;
        string pWebFile = getFilePath("", tmpPath01, tmpPath02, tmpPath03, tmpPath04, tmpPath05) + tmpFileName;

        if (System.IO.File.Exists(pTmpFile))
        {
            try
            {
                File.Copy(pTmpFile, pWebFile);
                rValue = true;
            } catch { }
        }
        return rValue;
    }       

    public bool chkDateFormat(string thisDate)
    {
        bool rValue = true;
        DateTime Test;
        if (DateTime.TryParseExact(thisDate, "yyyy-MM-dd", null, DateTimeStyles.None, out Test) == true) { } else { rValue = false; }
        if (thisDate.Trim() == "") { rValue = true; }
        return rValue;
    }
    public string getSWC000(string caseSWC002) {
        string rValue = caseSWC002;
        string sqlSWCSWC = " select SWC00 from SWCSWC where SWC02=@SWC002;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlSWCSWC;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC002", caseSWC002));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        rValue = readerTslm["SWC00"] + "";
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
	public string getSWCSWCData(string vSWC000,string vFiled)
    {
        string rValue = vSWC000;
        string sqlSWCSWC = " select * from SWCSWC where SWC00=@SWC000;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlSWCSWC;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", vSWC000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                        rValue = readerTslm[vFiled] + "";
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
	public string[] GenDLFilePNG(string Swc000)
    {
        GBClass001 GBA = new GBClass001();
        string ssUserName = Session["NAME"] + "";
        string tmpTime = DateTime.Now.ToString("HHmmss");
        string wReMark01 = "臺北市政府工務局大地工程處";
        string wReMark02 = "本文件由" + ssUserName + "於" + GBA.DateView(DateTime.Now.ToString(), "05") + "下載使用";
		//string wReMark03 = "核定日期：" + GBA.DateView(getSWCSWCData(Swc000, "SWC38"), "05");
        string wReMark03 = " ";
        string tmpPicPath = HttpContext.Current.Server.MapPath(@"~/images/Watermark/tempWM/");

        string[] arrayFName = new string[] { tmpTime + "01.png", tmpTime + "02.png", tmpTime + "03.png" }; //檔案名稱
		string[] arrayStr = new string[] { wReMark01, wReMark02, wReMark03 }; //圖片文字
        int[] arrayFontSize = new int[] { 36, 16, 16 }; //文字大小

        for (int i = 0; i < arrayStr.Length; i++)
        {
            //圖片輸出MemoryStream
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Image_String(arrayStr[i], arrayFontSize[i], false, Color.FromArgb(237, 237, 237), Color.FromArgb(237, 237, 237)).Save(ms, ImageFormat.Png);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            image.Save(tmpPicPath + arrayFName[i]);
        }
        return arrayFName;
    }
    public bool DLFileReMark(string Swc000, string FileName, string FileAttributes, string tmpSwc002, string tmpSwc007, string fileType)
    {
        GBClass001 GBA = new GBClass001();
        bool rValue = false;
        string extension = Path.GetExtension(FileName).ToLowerInvariant();
        string[] pngReMark = GenDLFilePNG(Swc000);
        string pathValue01 = "\\";

        if (extension == ".pdf") {
            #region 來源檔路徑
            switch (fileType)
            {
                case "6-1":
                    pathValue01 = "\\審查\\6-1\\";
                    break;
                case "7-1":
                    pathValue01 = "\\審查\\7-1\\";
                    break;
                case "核定本":
                    pathValue01 = "\\掃描檔\\掃描檔\\";
                    break;
                case "竣工圖說":
                    pathValue01 = "\\竣工圖說\\竣工圖說\\";
                    break;
                case "竣工圖說CAD":
                    pathValue01 = "\\竣工圖說\\竣工圖說CAD\\";
                    break;
                case "核備圖說變更":
                    pathValue01 = "\\核備圖說變更\\核備圖說變更\\";
                    break;
                case "審查單位查核表":
                    pathValue01 = "\\審查單位查核表\\審查單位查核表\\";
                    break;
                case "核定不予核定函":
                    pathValue01 = "\\掃描檔\\核定不予核定函\\";
                    break;
                default:
                    break;
            }
            #endregion

            string ReadPath = GBA.getFilePath(Swc000, tmpSwc002, tmpSwc007) + tmpSwc002 + pathValue01 + FileName;
            string NewUpath = Server.MapPath(@"~\OutputFile\" + FileName);

            PDFWaterMark(ReadPath, NewUpath, pngReMark); 
            rValue = true;
        }
        return rValue;
    }
    public void PDFWaterMark(string vFilePath, string vNewFilePath, string[] pngReMark)
    {
        iTextSharp.text.Image pdfimageobj00, pdfimageobj01, pdfimageobj02;
        PdfReader Pdfreader = new PdfReader(vFilePath);
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(vNewFilePath, FileMode.Create));

        int PageCount = Pdfreader.NumberOfPages;

        PdfGState gstate = new PdfGState()
        {
            FillOpacity = 1f,
            StrokeOpacity = 1f
        };

        for (int i = 1; i <= PageCount; i++)
        {
            PdfContentByte pdfPageContents = Pdfstamper.GetOverContent(i);
            pdfPageContents.SetGState(gstate);
            iTextSharp.text.Rectangle pagesize = Pdfreader.GetPageSizeWithRotation(i); //每頁的Size

            float x = pagesize.Height;
            float y = pagesize.Width;
            string tmpPicPath = HttpContext.Current.Server.MapPath(@"~/images/Watermark/tempWM/");

            string imageUrl00 = tmpPicPath + pngReMark[0]; //Logo
            string imageUrl01 = tmpPicPath + pngReMark[1]; //Logo
            string imageUrl02 = tmpPicPath + pngReMark[2]; //Logo

            pdfimageobj00 = iTextSharp.text.Image.GetInstance(imageUrl00);
            pdfimageobj01 = iTextSharp.text.Image.GetInstance(imageUrl01);
            pdfimageobj02 = iTextSharp.text.Image.GetInstance(imageUrl02);

            float xx0 = pdfimageobj00.ScaledHeight;
            float yy0 = pdfimageobj00.ScaledWidth;

            float xx1 = pdfimageobj01.ScaledHeight;
            float yy1 = pdfimageobj01.ScaledWidth;

            float xx2 = pdfimageobj02.ScaledHeight;
            float yy2 = pdfimageobj02.ScaledWidth;

            iTextSharp.text.Image img0 = iTextSharp.text.Image.GetInstance(imageUrl00);
            iTextSharp.text.Image img1 = iTextSharp.text.Image.GetInstance(imageUrl01);
            iTextSharp.text.Image img2 = iTextSharp.text.Image.GetInstance(imageUrl02);

            img0.SetAbsolutePosition((y - 554) / 2, (x - 554) / 2); //設定圖片每頁的絕對位置
            PdfContentByte waterMark0 = Pdfstamper.GetOverContent(i);
            img0.RotationDegrees = 45;
            waterMark0.AddImage(img0);    //把圖片印上去 

            img1.SetAbsolutePosition(10, x - xx1 - 10); //設定圖片每頁的絕對位置
            PdfContentByte waterMark1 = Pdfstamper.GetOverContent(i);
            waterMark1.AddImage(img1);    //把圖片印上去 

            img2.SetAbsolutePosition((y - yy2) / 2, 20); //設定圖片每頁的絕對位置
            PdfContentByte waterMark2 = Pdfstamper.GetOverContent(i);
            waterMark2.AddImage(img2);    //把圖片印上去 
        }
        Pdfstamper.Close();
        Pdfreader.Close();
    }
    
    
    
    protected Bitmap Image_String(string font, int font_size, bool font_bold, Color bgcolor, Color color)
    {
		
        //Font f = new System.Drawing.Font(@"C:\WINDOWS\Fonts\kaiu.ttf", font_size, font_bold ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular); //文字字型
        Font f = new System.Drawing.Font(@"C:\WINDOWS\Fonts\kaiu.ttf", font_size); //文字字型
        Brush b = new System.Drawing.SolidBrush(color); //文字顏色

        //計算文字長寬
        int img_width = 0, img_height = 0;
        using (Graphics gr = Graphics.FromImage(new Bitmap(1, 1)))
        {
            SizeF size = gr.MeasureString(font, f);
            img_width = Convert.ToInt32(size.Width);
            img_height = Convert.ToInt32(size.Height);
            gr.Dispose();
        }
		
        //圖片產生
        Bitmap image = new Bitmap(img_width, img_height);

        //填滿顏色並透明
        using (Graphics g = Graphics.FromImage(image))
        {
            //g.Clear(bgcolor);
            image = Image_ChangeOpacity(image, 0.2f);
            g.Dispose();
        }
        //文字寫入
        using (Graphics g = Graphics.FromImage(image))
        {
            g.DrawString(font, f, b, 0, 0);
            image = Image_ChangeOpacity(image, 0.2f);
            g.Dispose();
        }
        return image;
    }
    

    protected Bitmap Image_ChangeOpacity(System.Drawing.Image img, float opacityvalue)
    {
        Bitmap bmp = new Bitmap(img.Width, img.Height);
        Graphics graphics = Graphics.FromImage(bmp);
        ColorMatrix colormatrix = new ColorMatrix();
        colormatrix.Matrix33 = opacityvalue;
        ImageAttributes imgAttribute = new ImageAttributes();
        imgAttribute.SetColorMatrix(colormatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
        graphics.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgAttribute);
        graphics.Dispose();
        return bmp;
    }
    
}