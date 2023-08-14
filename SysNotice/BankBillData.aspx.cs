using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using Tamir.SharpSsh;
using Renci.SshNet;

public partial class SysNotice_BankData : System.Web.UI.Page
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "tcge7@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";

        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：" + errUrl + "<br/>";
        mailText += "錯誤訊息：" + errMsg + "<br/>";
		
        Response.Write(mailText);

        C1.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
        //Response.Redirect("~/errPage/500.htm");
        Server.ClearError();
    }
	
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        #region SFTP DL
        Class01 CL = new Class01();
        string host = "";
        string user = "";
        string password = "";

        string remoteDirectory = "";
        string localDirectory = "";
        string chkFiles = "";
		
        using (var sftp = new SftpClient(host, user, password))
        {
            sftp.Connect();
            var files = sftp.ListDirectory(remoteDirectory);

            foreach (var file in files)
            {
                string remoteFileName = file.Name;
                if ((!file.Name.StartsWith(".")) && ((file.LastWriteTime.Date == DateTime.Today)))
                    using (Stream file1 = File.OpenWrite(localDirectory + remoteFileName))
                    {
                        sftp.DownloadFile(remoteDirectory + remoteFileName, file1);
						Response.Write(remoteDirectory + remoteFileName+"<br>");
						chkFiles += remoteFileName + ";;";
                    }
            }
        }
        Response.Write("下載完成？？？<br/>");
        //Response.Write(chkFiles + "<br/>");
        #endregion

        #region
        int xxx = 0;
        string mailtext = "";
        string tmpToday = DateTime.Now.AddDays(-5).ToString("yyyyMMdd");
        //string tmpToday = DateTime.Now.ToShortDateString().Replace("/", "");
        string tmpPartName = tmpToday + "_" + tmpToday + "_D.txt";
        //tmpPartName = "10882_032091_20210407_20210407_D.txt"; //測試用
        string testmailstr = "<br>"; //檢查下載檔案內容

        Response.Write(tmpPartName + "<br/>");

        FileInfo info;
        string[] tempFile;
        string tFileName = "";
        string tRMAReportPath = localDirectory; //System.Web.HttpContext.Current.Server.MapPath("..\\TslmReport\\MMM");

        if (Directory.Exists(tRMAReportPath))
        {
            tempFile = Directory.GetFiles(tRMAReportPath);//取得資料夾下所有檔案
            foreach (string item in tempFile)
            {
                info = new FileInfo(item);
                tFileName = info.Name.ToString().Trim();//取得檔名
				string tmpFileLWT=info.LastWriteTime.Date.ToString("yyyyMMdd");
				bool test = Convert.ToInt32(tmpFileLWT) > Convert.ToInt32(tmpToday) && tFileName.IndexOf("_D.txt") > 0;
				if(test){
				Response.Write(test + "<br/>");
				Response.Write(info.LastWriteTime.Date.ToString("yyyyMMdd") + "<br/>");
				Response.Write(tFileName + "<br/>");
				Response.Write(tmpToday + "<br/><br/>");}
				
				if(test)
                //if (tFileName.IndexOf(tmpPartName) > 0)
                {
					
					Response.Write(tFileName + "<br/>");
                    testmailstr += tFileName + "：<br>";
                    // 建立檔案串流（@ 可取消跳脫字元 escape sequence）
                    StreamReader sr = new StreamReader(@"D:\web\TSLM3\TslmReport\MMM\" + tFileName);
                    while (!sr.EndOfStream)
                    {               // 每次讀取一行，直到檔尾
                        string line = sr.ReadLine();            // 讀取文字到 line 變數
                        testmailstr += line +"<br>";

                        string D1 = line.Substring(0, 1);   //明細資料列區別碼為1
                        if (D1 == "1")
                        {
							string D2 = line.Substring(1, 5);   //委託單位代號五碼
							string D3 = line.Substring(6, 1);   //代收通路 D：台北富邦銀行（臨櫃）
							string D4 = line.Substring(7, 10);   //代收分行/超商門市
							string D5 = line.Substring(17, 14);   //交易日期/交易時間 YYYYMMDD（西元）HHMMSS（時分秒）
                            D5 = D5.Substring(0, 4) + "-" + D5.Substring(4, 2) + "-" + D5.Substring(6, 2)+" "+ D5.Substring(8, 2)+":"+ D5.Substring(10, 2)+":"+ D5.Substring(12, 2);
							string D6 = line.Substring(31, 1);   //存提款別
							string D7 = line.Substring(58, 11);   //代收金額
							string D11 = line.Substring(72, 8);   //交易序號
							string D12 = line.Substring(80, 3);   //代理行
							string D16 = line.Substring(114, 8);   //帳務日
                            D16 = D16.Substring(0, 4) + "-" + D16.Substring(4, 2) + "-" + D16.Substring(6, 2);
							string D17 = line.Substring(122, 24);   //繳款人識別碼
							
                            string D17ss = Convert.ToInt32(D7).ToString();

							mailtext = mailtext + D17 + " " + D5 + " " + D7 + "<br/>";
							
                            xxx++;
                            string abc = " update SwcBill set BD003=@BD003,SB009=@SB009,SB010=@SB010,SB011=@SB011,SB012=@SB012,SB013=@SB013,SB014=@SB014,SB015=@SB015,SB016=@SB016,Csdate=getdate() where SB002=@SB002;update CasePaymentInfo set CPI004=@SB013,CPI006='已繳納' where REPLACE(BILLID,CPI001+'00',CPI001)=@SB002;";
                            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                            using (SqlConnection HeoConn = new SqlConnection(connectionString.ConnectionString))
                            {
                                HeoConn.Open();
                                using (var cmd = HeoConn.CreateCommand())
                                {
                                    cmd.CommandText = abc;
                                    cmd.Parameters.Add(new SqlParameter("@SB002", D17.Substring(2, 14)));
                                    #region.設定值
                                    cmd.Parameters.Add(new SqlParameter("@SB009", D2));
                                    cmd.Parameters.Add(new SqlParameter("@SB010", D11));
                                    cmd.Parameters.Add(new SqlParameter("@SB011", D17ss));
                                    cmd.Parameters.Add(new SqlParameter("@SB012", D5));
                                    cmd.Parameters.Add(new SqlParameter("@SB013", D16));
                                    cmd.Parameters.Add(new SqlParameter("@SB014", D3));
                                    cmd.Parameters.Add(new SqlParameter("@SB015", D4));
                                    cmd.Parameters.Add(new SqlParameter("@SB016", D12));
                                    cmd.Parameters.Add(new SqlParameter("@BD003", "已繳納"));
                                    #endregion
                                    cmd.ExecuteNonQuery();
                                    cmd.Cancel();
                                }
                            }
                            CL.UpdatePayMent(D17.Substring(2, 14));
                        }

                    }
                    sr.Close();						// 關閉串流
                }
            }
        }
        #region sendmail
        //if (xxx > 0) {
            string mailsub = "富邦對帳_" + DateTime.Now.ToString("yyyy-MM-dd hhmmss");
            mailtext = "共"+xxx +"筆資料：<br/>" + mailtext + testmailstr;
            string[] uuu = new string[] { "geocheck@geovector.com.tw" };
            SBApp.Mail_Send(uuu, mailsub, mailtext);
        //}
        #endregion
		
        Response.Write(mailsub+"<br/>"+mailtext+"<br/>");
        Response.Write("資料已更新");
        #endregion
		
    }
}