using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCAPPLY_SwcApply2001 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "tim@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";

        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：" + errUrl + "<br/>";
        mailText += "錯誤訊息：" + errMsg + "<br/>";

        C1.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
        Response.Redirect("~/errPage/500.htm");
        Server.ClearError();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();

        string ssUserName = Session["NAME"] + "";
        string pSwcId = Request.QueryString["SWC000"] + "";
        LBSWC000.Text = pSwcId;
        if (pSwcId.Trim() == "" || ssUserName.Trim() == "") Response.Redirect("~/SWCDOC/SWC001.aspx");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
		Class1 C1 = new Class1();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string pSwcId = Request.QueryString["SWC000"] + "";
        string tmpSWC002 = "", tmpSWC005 = "", tmpSWC025 = "";
        string qId1 = "", qId2="";
        string sqlStr = " select top 1 * from SwcApply2002 where SWC000=@SWC000 order by SAB001 desc ; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSwcId));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        string qMaxCode = readerTslm["SAB001"] + "";
                        string qMaxNo = qMaxCode.Substring(qMaxCode.Length-1,1);

                        tmpSWC002 = readerTslm["SWC002"] + "";
                        tmpSWC005 = readerTslm["SWC005"] + "";
						//不要抓前一張的承辦人員
                        //tmpSWC025 = readerTslm["SWC025"] + "";
                        qId1 = qMaxCode;
                        qId2 = "SA2002" + DateTime.Now.ToString("yyyyMMdd") + "0" + (Convert.ToInt32(qMaxNo) + 1);
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
		//抓現在案件的承辦人員
		tmpSWC025 = C1.getSWCSWCData(pSwcId,"SWC25");
        string RecField = " SWC000,SWC002,SWC005,SWC025,DATALOCK,LOCKUSER,LOCKDATE,savedate,saveuser,SABC001,SABC001T,SABC002,SABC002T,SABC003,SABC003T,SABC004,SABC004T,SABC005,SABC005T,SABC006,SABC006T,SABC007,SABC008,SABC008T,SABC009,SABC009T,SABC010,SABC010T,SABC011,SABC011T,SABC012,SABC012T,SABC013,SABC014,SABC014D1,SABC014D2a,SABC014D2b,SABC014D3,SABC014D4,SABC014D5,SABC015,SABC015T,SABC016,SABC016T,SABC017,SABC017T,SABC018,SABC018T,SABC019,SABC019T,SABC020,SABC020T ";
        string RecSqlStr = " INSERT INTO SwcApply2002 (SAB001,SING006," + RecField + ") select '"+ qId2 + "',SWC025," + RecField + " from SwcApply2002 WHERE SWC000=@SWC000 AND SAB001 = '" + qId1 + "';";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = RecSqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSwcId));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        string sqlLock = " Update SwcApply2002 Set DATALOCK = 'Y',DATALOCK2 = '',LOCKUSER2 = '',ReviewResults = '',ReviewDoc = '',ResultsExplain = '',ReviewDirections = '',ReSendDeadline = null,SING002=@SWC025,SING004=@SING004,SING006=@SING006,SING007=N'送出',ONAHEAD=N'水土保持計畫受理查核表',SING008 = N'待簽辦',LOCKUSER=@USERID,LOCKDATE = getdate() Where SWC000=@SWC000 and SAB001=@SAB001; ";
        sqlLock += " Update SWCSWC set SWC04='受理中' Where SWC00=@SWC000; Update TCGESWC.dbo.SWCCASE set SWC004='受理中' Where SWC000=@SWC000; ";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlLock;
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSwcId));
                cmd.Parameters.Add(new SqlParameter("@SWC025", tmpSWC025));
                cmd.Parameters.Add(new SqlParameter("@SAB001", qId2));
                cmd.Parameters.Add(new SqlParameter("@SING004", tmpSWC025 + ";"));
                cmd.Parameters.Add(new SqlParameter("@SING006", ssUserName));
                cmd.Parameters.Add(new SqlParameter("@USERID", ssUserID));
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@SAA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
        ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = strSQL3;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSwcId));
                cmd.Parameters.Add(new SqlParameter("@SWC002", tmpSWC002));
                cmd.Parameters.Add(new SqlParameter("@SWC005", tmpSWC005));
                cmd.Parameters.Add(new SqlParameter("@SWC025", tmpSWC025));
                cmd.Parameters.Add(new SqlParameter("@SAA001", qId2));
                cmd.Parameters.Add(new SqlParameter("@R001", ""));
                cmd.Parameters.Add(new SqlParameter("@R002", ""));
                cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                cmd.Parameters.Add(new SqlParameter("@R005", ""));
                cmd.Parameters.Add(new SqlParameter("@R006", ""));
                cmd.Parameters.Add(new SqlParameter("@R007", GBC.GetETUser(ssUserID, "OrgName")));
                cmd.Parameters.Add(new SqlParameter("@R008", GBC.GetETUser(ssUserID, "ETIdentity")));
                cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/HHmm")));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        GBC.RecordTrunHistory(pSwcId, tmpSWC002, qId2, "申請中", ssUserID, "", "");
        SendMailNotice(pSwcId);
        Response.Write("<script>alert('資料已送出。');location.href='../SWCDOC/SWC003.aspx?SWCNO=" + pSwcId + "';</script>");
    }

    private void SendMailNotice(object pSWC000)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();

        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);

        //送出提醒名單：股長、系統管理員、承辦人員、章姿隆
        string caseStr = " select * from swcswc where SWC00=@SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString)) {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = caseStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        string tSWC005 = readerTslm["SWC05"] + "";
                        string tSWC012 = readerTslm["SWC12"] + "";
                        string tSWC025 = readerTslm["SWC25"] + "";
                        //寄件名單
                        string SentMailGroup = "";
                        for (int i = 1; i < arrayUserId.Length; i++)
                        {
                            string aUserId = arrayUserId[i];
                            string aUserName = arrayUserName[i];
                            string aJobTitle = arrayJobTitle[i];
                            string aUserMail = arrayUserMail[i];
                            string aMBGROUP = arrayMBGROUP[i];

                            if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706")
                                SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        }
                        //轄區【水土保持計畫】，技師已於(退補正補件送出日期)補件，請至系統查看。
                        string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                        string ssMailSub01 = tSWC012 + "【" + tSWC005 + "】，技師已於" + DateTime.Now.ToString("yyyy-MM-dd")+ "補件";
                        string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】，技師已於" + DateTime.Now.ToString("yyyy-MM-dd") + "補件，請至系統查看。" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
    }
}