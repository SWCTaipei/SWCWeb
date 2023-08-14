using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SwcSchedule_SWCSCHED01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        setPageValue();
    }

    private void setPageValue()
    {
		GBClass001 SBApp = new GBClass001();
        string sqlStr = " select A.SWC000,convert(varchar(10), dateadd(day,datediff(day,LOCKDATE,A.SWC088),getdate()), 126) as 'output' from SWCCASE A ";
		sqlStr += " left join (select SWC000,min(ONA02003) as 'ONA02003',min(LOCKDATE) as 'LOCKDATE' from OnlineApply02 group by SWC000) B ON A.SWC000 = B.SWC000 ";
		sqlStr += " where SWC004 = '暫停審查' and convert(varchar, getdate(), 111)>convert(varchar, ONA02003, 111) ;";
        
		string tempSWC000 = "";
		string tempDate = "";
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    if (readerSwc.HasRows)
                    {
                        while (readerSwc.Read())
                        {
                            string qSWC000 = readerSwc["SWC000"]+"";
                            string qDate = readerSwc["output"]+"";
							
							//***新增假日判斷
							bool qholiday = true;
							DateTime dt = Convert.ToDateTime(qDate);
							dt = dt.AddDays(-1);
							while (qholiday == true){
								dt = dt.AddDays(1);
								qholiday = SBApp.isHoliday(dt);
							}
							qDate = dt.ToString("yyyy-MM-dd");
							//***新增假日判斷
							tempSWC000 = tempSWC000 + qSWC000 + ";;";
							tempDate = tempDate + qDate + ";;";
							LB01.Text += qSWC000 + "------" + qDate + "<br>";
                        }
                    }
                    readerSwc.Close();
                }
                cmd.Cancel();
            }
        }
		string[] arraySWC000 = tempSWC000.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
		string[] arrayDate = tempDate.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
		string sqlstr1 = "";
		for(int i=0; i<arraySWC000.Length; i++){
			using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
			{
				SwcConn.Open();
				using (var cmd = SwcConn.CreateCommand())
				{
					sqlstr1 = " update SWCCASE set SWC004 = '審查中', SWC088 = '" + arrayDate[i] + "' , SWC136 = @SWC136 where SWC000 = '" + arraySWC000[i] + "';";
					sqlstr1 += " update tslm2.dbo.SWCSWC set SWC04 = '審查中', SWC88 = '" + arrayDate[i] + "', SWC136 = @SWC136 where SWC00 = '" + arraySWC000[i] + "';";
					cmd.CommandText = sqlstr1;
					cmd.Parameters.Add(new SqlParameter("@SWC136", DateTime.Now));
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
			SendMailNotice(arraySWC000[i],arrayDate[i]);
		}
		
		
    }
	
	protected void BTN01_Click(object sender, EventArgs e)
    {
		//setPageValue();
	}
	
	private void SendMailNotice(string v,string v2)
    {
		GBClass001 SBApp = new GBClass001();
		string tSWC005 = "";				//水土保持計畫名稱
		string tDeadline = v2;				//審查期限
		
		string tSWC013TEL = "";				//義務人手機
        string tSendMail = "";				//聯絡人e-mail
        string tSWC025 = "";				//承辦人員姓名
        string tSWC022ID = "";				//審查公會ID
		
        string tSWC021 = "";				//承辦技師姓名
        string tSWC021ID = "";				//承辦技師ID
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + "   where SWC000 = '" + v + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                tSWC005 = readerSWC["SWC005"]+"";
				
                tSWC013TEL = readerSWC["SWC013TEL"]+"";
				tSendMail = readerSWC["SWC108"]+"";
				tSWC025 = readerSWC["SWC025"]+"";
				tSWC022ID = readerSWC["SWC022ID"]+"";
				
				tSWC021 = readerSWC["SWC021"]+"";
				tSWC021ID = readerSWC["SWC021ID"]+"";
            }
        }
		
        string tMailSub1  = "",tMailText1 = "",SentMailGroup1 = ""; 
        string tMailSub2  = "",tMailText2 = "",SentMailGroup2 = ""; 


		tMailSub1  = "您好，【" + tSWC005 + "】已恢復審查，審查期限至【"+ tDeadline + "】。";
		tMailText1 = "您好，【" + tSWC005 + "】已恢復審查，審查期限至【" + tDeadline + "】。<br><br>";
		tMailText1 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		tMailText1 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
		
		tMailSub2  = "【" + tSWC021 + "】您好，【" + tSWC005 + "】已恢復審查，審查期限至【"+ tDeadline + "】。";
		tMailText2 = "【" + tSWC021 + "】您好，【" + tSWC005 + "】已恢復審查，審查期限至【"+ tDeadline + "】，提醒您記得於【" + DateTime.Now.AddDays(7).ToString("yyyy-MM-dd") + "】內上傳修正本以利續審，否則審查單位將依相關規定建議不予核定。<br><br>";
		tMailText2 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		tMailText2 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

		string exeSQLSTR1 = " select email as EMAIL from tslm2.dbo.geouser g where ([name] = '"+ tSWC025 + "' or userid = '"+ tSWC022ID + "') and status <> '停用' ;";
		string exeSQLSTR2 = " select ETEmail as EMAIL from ETUsers where etid = '"+ tSWC021ID + "' ;";
		
		#region.名單
		using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
		{
			SwcConn.Open();
	
			SqlDataReader readerItemS;
			SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR1, SwcConn);
			readerItemS = objCmdItemS.ExecuteReader();
	
			while (readerItemS.Read())
			{
				string tEMail = readerItemS["EMAIL"] + "";
				SentMailGroup1 += ";;" + tEMail;
			}
			readerItemS.Close();
			objCmdItemS.Dispose();
		}
		SentMailGroup1 += ";;" + tSendMail;
		string[] arraySentMail01 = SentMailGroup1.Split(new string[] { ";;" }, StringSplitOptions.None);
		bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub1, tMailText1);
		
		
		using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
		{
			SwcConn.Open();
	
			SqlDataReader readerItemS;
			SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR2, SwcConn);
			readerItemS = objCmdItemS.ExecuteReader();
	
			while (readerItemS.Read())
			{
				string tEMail = readerItemS["EMAIL"] + "";
				SentMailGroup2 += ";;" + tEMail;
			}
			readerItemS.Close();
			objCmdItemS.Dispose();
		}
		string[] arraySentMail02 = SentMailGroup2.Split(new string[] { ";;" }, StringSplitOptions.None);
		bool MailTo02 = SBApp.Mail_Send(arraySentMail02, tMailSub2, tMailText2);

		#endregion
		//SBApp.SendSMS(tSWC013TEL, tMailSub1);
		string[] arrayPhoneNo = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
		SBApp.SendSMS_Arr(arrayPhoneNo, tMailSub1);
    }	
}