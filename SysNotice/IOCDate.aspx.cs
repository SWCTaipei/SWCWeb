using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

public partial class SysNotice_IOCDate : System.Web.UI.Page
{
    string ChkUserId = "";
    string ChkUserName = "";
    string ChkJobTitle = "";
    string ChkMbGroup = "";
    string ChkMail = "";
    string ChkMailText = "";

    string ChkGuildId = "";
    string ChkGuildName = "";
    string ChkGuildMail = "";
    string ChkGuildMailText = "";

    string ChkETUserId = "";
    string ChkETUserName = "";
    string ChkETUserMail = "";
    string ChkETUserMailText = "";

    int ChkOverDay = 6;

    //索引用：strSQLRV15 - strSQLRV16

    protected void Page_Load(object sender, EventArgs e)
    {
        GetUserMailData();

        string[] arrayChkUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayChkUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMbGroup = ChkMbGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayChkMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayChkUserMailTxt = ChkMailText.Split(new string[] { ";;" }, StringSplitOptions.None);
        
        GetGuildMailData();

        string[] arrayChkGuildId = ChkGuildId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayChkGuildName = ChkGuildName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayChkGuildMail = ChkGuildMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayChkGuildMailText = ChkGuildMailText.Split(new string[] { ";;" }, StringSplitOptions.None);

        GetETUserMailData();

        string[] arrayETUserId = ChkETUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayETUserName = ChkETUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayETUserMail = ChkETUserMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayETUserMailText = ChkETUserMailText.Split(new string[] { ";;" }, StringSplitOptions.None);
        
        string strObligors = "";
        string strObligorsMail = "";
        string strObligorsText = "";

        //string strPhoneNO = "";
        //string strPhoneMsg = "";

        Class01 SBApp = new Class01();
        GBClass001 GBSBApp = new GBClass001();
        
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            SwcConn.Open();

            //15.水保案件：狀態為「施工中」，「施工檢查表」-03
            //1. 1號寄信「是否辦理檢查」(主表的施工檢查)
            //2.「檢查書件繳送是否逾期」：檢查日期與送出日期不得超過固定天數(6工作天)(行政院人事行政總處是否有api可介接)
			//3. 25號寄信「是否辦理檢查」(SWCDTL03的檢查日期)

            int tThisMonth = DateTime.Now.Month;
            int tThisDay = DateTime.Now.Day;

            if (tThisDay == 1)
            {
                string strSQLRV15a = " select * from SWCCASE ";
                strSQLRV15a = strSQLRV15a + " WHERE SWC004 = '施工中' ORDER BY SWC000 ";

                SqlDataReader readeSwcA;
                SqlCommand objCmdSwcA = new SqlCommand(strSQLRV15a, SwcConn);
                readeSwcA = objCmdSwcA.ExecuteReader();

                while (readeSwcA.Read())
                {
                    bool CheckHadDate = false;

                    double dSwc023 = Convert.ToDouble(readeSwcA["SWC023"] + "");
                    DateTime dSwc114 = Convert.ToDateTime(readeSwcA["SWC114"] + "");

                    string tSWC002 = readeSwcA["SWC002"] + "";  //水保局編號
                    string tSWC005 = readeSwcA["SWC005"] + "";  //書件名稱
                    string tSWC012 = readeSwcA["SWC012"] + "";  //轄區：北一區
                    string tSWC024ID = readeSwcA["SWC024ID"] + "";  //檢查公會
                    string tSWC025 = readeSwcA["SWC025"] + "";  //承辦人
                    if (dSwc023 < 1)
                    {
						CheckHadDate = true;
                    }
                    DateTime tChkDate_S = DateTime.Now;
                    DateTime tChkDate_E = DateTime.Now;
					//未滿一公頃 每個月檢查
                    if (CheckHadDate)
                    {
						tChkDate_S = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
						tChkDate_E = new DateTime(DateTime.Now.AddMonths(1).Year,DateTime.Now.AddMonths(1).Month,1).AddDays(-1);
                    }
					//一公頃以上
                    else
                    {
						if(tThisMonth == 12 || tThisMonth == 1 || tThisMonth == 2 || tThisMonth == 3 || tThisMonth == 4)
						{
							tChkDate_S = new DateTime(DateTime.Now.AddMonths(-1).Year,DateTime.Now.AddMonths(-1).Month,1);
							tChkDate_E = new DateTime(DateTime.Now.AddMonths(1).Year,DateTime.Now.AddMonths(1).Month,1).AddDays(-1);
						}
						else
						{
							tChkDate_S = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
							tChkDate_E = new DateTime(DateTime.Now.AddMonths(1).Year,DateTime.Now.AddMonths(1).Month,1).AddDays(-1);
						}
                    }
					
                    if(!(dSwc114 >= tChkDate_S && dSwc114 <= tChkDate_E))
                    {
                        string tMailText02 = "提醒您，【" + tSWC005 + "】本月預計檢查日期尚未送出，請確認並盡速登記。";
                        for (int i = 0; i < arrayChkGuildId.Length-1; i++)
                        {
                            string tGuildID = arrayChkGuildId[i];

                            if (tGuildID == tSWC024ID)
                            {
                                arrayChkGuildMailText[i] = arrayChkGuildMailText[i] + tMailText02 + "<br>";
                            }
                        }
                    }
                }
                readeSwcA.Close();
                objCmdSwcA.Dispose();
            }

            string strSQLRV15b = " select SWC.SWC000,SWC.SWC005,SWC.SWC012,SWC.SWC013,SWC.SWC013TEL,SWC.SWC024ID,SWC.SWC025,SWC.SWC045ID,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV15b = strSQLRV15b + "  LEFT JOIN SWCDTL03 D3 ON SWC.SWC000 = D3.SWC000 ";
            strSQLRV15b = strSQLRV15b + " WHERE SWC.SWC004 = '施工中' ";
            strSQLRV15b = strSQLRV15b + "   AND D3.DTLC002 < GETDATE()-6 AND D3.DATALOCK <>'Y' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV15b, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC012 = readeSwc["SWC012"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC024ID = readeSwc["SWC024ID"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                string tMailText01 = tSWC012 + "【" + tSWC005 + "】本月檢查紀錄尚未送出，請確認檢查單位是否依合約辦理。";
                for (int i = 0; i < arrayChkUserId.Length-1; i++)
                {
                    string tG1UserName = arrayChkUserName[i];
                    string tG1JObTitle = arrayJobTitle[i];
                    string tG1MbGroup = arrayMbGroup[i];

                    if (tG1JObTitle == "科長" || tG1JObTitle == "正工" || tG1JObTitle == "股長" || tG1MbGroup == "系統管理員" || tG1UserName.Trim() == tSWC025.Trim())
                    {
                        arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText01 + "<br>";
                    }
                }

                string tMailText02 = "提醒您，【" + tSWC005 + "】本月檢查紀錄尚未送出，請確認並盡速辦理。";
                for (int i = 0; i < arrayChkGuildId.Length-1; i++)
                {
                    string tGuildID = arrayChkGuildId[i];

                    if (tGuildID == tSWC024ID)
                    {
                        arrayChkGuildMailText[i] = arrayChkGuildMailText[i] + tMailText02 + "<br>";
                    }
                }

            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
			
			if (tThisDay == 25)
            {
                string strSQLRV15c = " select C.SWC002,C.SWC005,C.SWC012,C.SWC024ID,C.SWC025,ISNULL(C.SWC023,0) AS SWC023,ISNULL(D3.DTLC002,GETDATE()-1000) AS DTLC002,D3.DATALOCK from SWCCASE C ";
                strSQLRV15c = strSQLRV15c + " LEFT JOIN SWCDTL03 D3 ON C.SWC000 = D3.SWC000 AND D3.DTLC000 = (SELECT MAX(DTLC000) FROM SWCDTL03 D3m WHERE C.SWC000 = D3m.SWC000 ) ";
                strSQLRV15c = strSQLRV15c + " WHERE C.SWC004 = '施工中' ORDER BY C.SWC000 ";

                SqlDataReader readeSwcA;
                SqlCommand objCmdSwcA = new SqlCommand(strSQLRV15c, SwcConn);
                readeSwcA = objCmdSwcA.ExecuteReader();

                while (readeSwcA.Read())
                {
                    bool CheckHadDate = false;

                    double dSwc023 = Convert.ToDouble(readeSwcA["SWC023"] + "");
                    DateTime dDTLC002 = Convert.ToDateTime(readeSwcA["DTLC002"] + "");

                    string tSWC002 = readeSwcA["SWC002"] + "";  //水保局編號
                    string tSWC005 = readeSwcA["SWC005"] + "";  //書件名稱
                    string tSWC012 = readeSwcA["SWC012"] + "";  //轄區：北一區
                    string tSWC024ID = readeSwcA["SWC024ID"] + "";  //檢查公會
                    string tSWC025 = readeSwcA["SWC025"] + "";  //承辦人

                    string tDTLC002 = readeSwcA["DTLC002"] + "";    //檢查日期
                    string tDATALOCK = readeSwcA["DATALOCK"] + "";  //資料送出
                    if (dSwc023 < 1)
                    {
						CheckHadDate = true;
                    }
					
                    DateTime tChkDate_S = DateTime.Now;
                    DateTime tChkDate_E = DateTime.Now;
					//未滿一公頃 每個月檢查
                    if (CheckHadDate)
                    {
						tChkDate_S = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
						tChkDate_E = new DateTime(DateTime.Now.AddMonths(1).Year,DateTime.Now.AddMonths(1).Month,1).AddDays(-1);
                    }
					//一公頃以上
                    else
                    {
						if(tThisMonth == 12 || tThisMonth == 1 || tThisMonth == 2 || tThisMonth == 3 || tThisMonth == 4)
						{
							tChkDate_S = new DateTime(DateTime.Now.AddMonths(-1).Year,DateTime.Now.AddMonths(-1).Month,1);
							tChkDate_E = new DateTime(DateTime.Now.AddMonths(1).Year,DateTime.Now.AddMonths(1).Month,1).AddDays(-1);
						}
						else
						{
							tChkDate_S = new DateTime(DateTime.Now.Year,DateTime.Now.Month,1);
							tChkDate_E = new DateTime(DateTime.Now.AddMonths(1).Year,DateTime.Now.AddMonths(1).Month,1).AddDays(-1);
						}
                    }
					
                    if(!(dDTLC002 >= tChkDate_S && dDTLC002 <= tChkDate_E && tDATALOCK == "Y") || tDATALOCK != "Y")
                    {
                        string tMailText01 = "提醒您，" + tSWC012 + "【" + tSWC005 + "】本月檢查表單尚未送出，請確認檢查單位是否依合約辦理。";
                        for (int i = 1; i < arrayChkUserId.Length; i++)
                        {
                            string tG1UserName = arrayChkUserName[i];
                            string tG1JObTitle = arrayJobTitle[i];
                            string tG1MbGroup = arrayMbGroup[i];

                            if (tG1JObTitle == "科長" || tG1JObTitle == "正工" || tG1JObTitle == "股長" || tG1MbGroup == "系統管理員" || tG1UserName.Trim() == tSWC025.Trim())
                            {
                                arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText01 + "<br>";
                            }
                        }

                        string tMailText02 = "提醒您，【" + tSWC005 + "】本月檢查表單尚未送出，請確認是否依合約辦理檢查。";
                        for (int i = 0; i < arrayChkGuildId.Length-1; i++)
                        {
                            string tGuildID = arrayChkGuildId[i];

                            if (tGuildID == tSWC024ID)
                            {
                                arrayChkGuildMailText[i] = arrayChkGuildMailText[i] + tMailText02 + "<br>";
                            }
                        }
                    }
                }
                readeSwcA.Close();
                objCmdSwcA.Dispose();
            }

            #region 16-1.若狀態為「施工中」且「申報完工日期」欄位沒有值，「監造紀錄表」，每週三檢核是否當週(前週三至當週二)有值，無值則發信通知
            if (Week()=="三") {
                string sqlStr = " select * from SWCCASE where SWC004 = '施工中' AND ISNULL(SWC058,'1900-01-01') < '1980-01-01' ; ";
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection swcConn = new SqlConnection(connectionString.ConnectionString))
                {
                    swcConn.Open();
                    using (var cmd = swcConn.CreateCommand())
                    {
                        cmd.CommandText = sqlStr;
                        cmd.ExecuteNonQuery();

                        using (SqlDataReader readerSwc= cmd.ExecuteReader())
                        {
                            while (readerSwc.Read())
                            {
                                string tmpSWC000 = readerSwc["SWC000"].ToString();
                                string tmpSWC005 = readerSwc["SWC005"].ToString();
                                string tmpSWC012 = readerSwc["SWC012"].ToString();
                                string tmpSWC013 = readerSwc["SWC013"].ToString();
                                string tSWC013TEL = readerSwc["SWC013TEL"].ToString();
                                string tmpSWC025 = readerSwc["SWC025"].ToString();
                                string tSWC024ID = readerSwc["SWC024ID"].ToString();
                                string tSWC045ID = readerSwc["SWC045ID"].ToString();
                                string tmpSWC108 = readerSwc["SWC108"].ToString();

                                string tMailText01 = tmpSWC012 + "【" + tmpSWC005 + "】「上週三至本週二」區間之監造紀錄表，請確認。";
                                if (!hasD5Data(tmpSWC000))
                                {
                                    //(轄區)承辦人員
                                    //轄區【水土保持計畫】尚未上傳「上週三至本週二」區間之監造紀錄表，請確認
                                    for (int i = 1; i < arrayChkUserId.Length; i++)
                                    {
                                        string tG1UserName = arrayChkUserName[i];
                                        string tG1JObTitle = arrayJobTitle[i];
                                        string tG1MbGroup = arrayMbGroup[i];

                                        if (tG1UserName .Trim() == tmpSWC025.Trim())
                                            arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText01 + "<br>";
                                    }

                                    //監造技師寄email、義務人傳簡訊、聯絡人傳email
                                    //提醒您，【水土保持計畫】尚未上傳「上週三至本週二」區間之監造紀錄表，請確認
                                    string tMailText02 = "提醒您，【" + tmpSWC005 + "】尚未上傳「上週三至本週二」區間之監造紀錄表，請確認。";
                                    for (int i = 1; i < arrayETUserId.Length; i++)
                                    {
                                        string tETUserID = arrayETUserId[i];
                                        if (tETUserID == tSWC045ID)
                                            arrayETUserMailText[i] = arrayETUserMailText[i] + tMailText02 + "<br>";
                                    }
                                    if (tmpSWC108.Trim() != "")
                                    {
                                        strObligors = strObligors + ";;" + tmpSWC013;
                                        strObligorsMail = strObligorsMail + ";;" + tmpSWC108;
                                        strObligorsText = strObligorsText + ";;" + tMailText02;
                                    }
                                    //strPhoneNO = strPhoneNO + ";;" + tSWC013TEL;
                                    //strPhoneMsg = strPhoneMsg + ";;" + tMailText02;
									
									string[] arrayPhoneNo = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
									GBSBApp.SendSMS_Arr(arrayPhoneNo, tMailText02.Replace("<br>",""));
									
                                }
                            }
                            readerSwc.Close();
                        }
                        cmd.Cancel();
                    }
                }
            }
            #endregion

            #region 16-2.若狀態為「施工中」且「申報完工日期」欄位沒有值，「監造紀錄表」，每週五檢核是否當週(前週三至當週二)有值，無值則發信通知
            if (Week() == "五")
            {
                string sqlStr = " select * from SWCCASE where SWC004 = '施工中' AND ISNULL(SWC058,'1900-01-01') < '1980-01-01' ; ";
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection swcConn = new SqlConnection(connectionString.ConnectionString))
                {
                    swcConn.Open();
                    using (var cmd = swcConn.CreateCommand())
                    {
                        cmd.CommandText = sqlStr;
                        cmd.ExecuteNonQuery();

                        using (SqlDataReader readerSwc = cmd.ExecuteReader())
                        {
                            while (readerSwc.Read())
                            {
                                string tmpSWC000 = readerSwc["SWC000"].ToString();
                                string tmpSWC005 = readerSwc["SWC005"].ToString();
                                string tmpSWC012 = readerSwc["SWC012"].ToString();
                                string tmpSWC013 = readerSwc["SWC013"].ToString();
                                string tSWC013TEL = readerSwc["SWC013TEL"].ToString();
                                string tmpSWC025 = readerSwc["SWC025"].ToString();
                                string tSWC024ID = readerSwc["SWC024ID"].ToString();
                                string tSWC045ID = readerSwc["SWC045ID"].ToString();
                                string tmpSWC108 = readerSwc["SWC108"].ToString();

                                string tMailText01 = tmpSWC012 + "【" + tmpSWC005 + "】「上週三至本週二」區間之監造紀錄表，請確認。";
                                if (!hasD5Data(tmpSWC000))
                                {
                                    //(轄區)承辦人員
                                    //轄區【水土保持計畫】尚未上傳「上週三至本週二」區間之監造紀錄表，請確認
                                    for (int i = 1; i < arrayChkUserId.Length; i++)
                                    {
                                        string tG1UserName = arrayChkUserName[i];
                                        string tG1JObTitle = arrayJobTitle[i];
                                        string tG1MbGroup = arrayMbGroup[i];

                                        if (tG1UserName.Trim() == tmpSWC025.Trim())
                                            arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText01 + "<br>";
                                    }

                                    //監造技師寄email、義務人傳簡訊、聯絡人傳email
                                    //提醒您，【水土保持計畫】尚未上傳「上週三至本週二」區間之監造紀錄表，請於本日前盡速補正
                                    string tMailText02 = "提醒您，【" + tmpSWC005 + "】尚未上傳「上週三至本週二」區間之監造紀錄表，請於本日前盡速補正。";
                                    for (int i = 1; i < arrayETUserId.Length; i++)
                                    {
                                        string tETUserID = arrayETUserId[i];
                                        if (tETUserID == tSWC045ID)
                                            arrayETUserMailText[i] = arrayETUserMailText[i] + tMailText02 + "<br>";
                                    }
                                    if (tmpSWC108.Trim() != "")
                                    {
                                        strObligors = strObligors + ";;" + tmpSWC013;
                                        strObligorsMail = strObligorsMail + ";;" + tmpSWC108;
                                        strObligorsText = strObligorsText + ";;" + tMailText02;
                                    }
                                    //strPhoneNO = strPhoneNO + ";;" + tSWC013TEL;
                                    //strPhoneMsg = strPhoneMsg + ";;" + tMailText02;
									
									string[] arrayPhoneNo = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
									GBSBApp.SendSMS_Arr(arrayPhoneNo, tMailText02.Replace("<br>",""));
                                }
                            }
                            readerSwc.Close();
                        }
                        cmd.Cancel();
                    }
                }
            }
            #endregion

            /* 2021-02-03
            //16.水保案件：狀態為「施工中」，「監造紀錄表」-05
            //1.檢查過去7天內有無檔案，若無則須通知。 (無檔案：子表單，送出日期)
            //2.開工日期<過去7天

            string strSQLRV16 = " select SWC.SWC000,SWC.SWC005,SWC.SWC012,SWC.SWC013,SWC.SWC013TEL,SWC.SWC024ID,SWC.SWC025,SWC.SWC045ID,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV16 = strSQLRV16 + "  LEFT JOIN SWCDTL05 D5 ON SWC.SWC000 = D5.SWC000 ";
            strSQLRV16 = strSQLRV16 + " WHERE SWC.SWC004 = '施工中' AND ISNULL(SWC.SWC058,'1900-01-01') < '1980-01-01' ";
            strSQLRV16 = strSQLRV16 + "   AND SWC.SWC051 < GETDATE() - 7 ";
            strSQLRV16 = strSQLRV16 + "   AND SWC.SWC000 NOT IN(SELECT SWC000 FROM SWCDTL05 WHERE savedate > GETDATE() - 7) ";
            strSQLRV16 = strSQLRV16 + " GROUP BY SWC.SWC000,SWC.SWC005,SWC.SWC012,SWC.SWC013,SWC.SWC013TEL,SWC.SWC024ID,SWC.SWC025,SWC.SWC045ID,SWC.SWC108 ";
            strSQLRV16 = strSQLRV16 + " ORDER BY SWC.SWC000 ";
            
            objCmdSwc = new SqlCommand(strSQLRV16, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC012 = readeSwc["SWC012"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC024ID = readeSwc["SWC024ID"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";


                string tMailText01 = tSWC012 + "【" + tSWC005 + "】尚未上傳監造紀錄表，請確認。";
                for (int i = 1; i < arrayChkUserId.Length; i++)
                {
                    string tG1UserName = arrayChkUserName[i];
                    string tG1JObTitle = arrayJobTitle[i];
                    string tG1MbGroup = arrayMbGroup[i];

                    if (tG1JObTitle == "科長" || tG1JObTitle == "正工" || tG1JObTitle == "股長" || tG1MbGroup == "系統管理員" || tG1UserName.Trim() == tSWC025.Trim())
                    {
                        arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText01 + "<br>";
                    }
                }

                string tMailText02 = "提醒您，【" + tSWC005 + "】尚未上傳本週監造紀錄表，請盡速補正。";
                for (int i = 1; i < arrayChkGuildId.Length; i++)
                {
                    string tGuildID = arrayChkGuildId[i];

                    if (tGuildID == tSWC024ID)
                    {
                        //2020-04-01：巽舜與凱暉討論先不寄公會
                        //arrayChkGuildMailText[i] = arrayChkGuildMailText[i] + tMailText02 + "<br>";
                    }
                }

                for (int i = 1; i < arrayETUserId.Length; i++)
                {
                    string tETUserID = arrayETUserId[i];

                    if (tETUserID == tSWC045ID)
                    {
                        arrayETUserMailText[i] = arrayETUserMailText[i] + tMailText02 + "<br>";
                    }
                }

                if (tSWC108.Trim() != "")
                {
                    strObligors = strObligors + ";;" + tSWC013;
                    strObligorsMail = strObligorsMail + ";;" + tSWC108;
                    strObligorsText = strObligorsText + ";;" + tMailText02;
                }


                strPhoneNO = strPhoneNO + ";;" + tSWC013TEL;
                strPhoneMsg = strPhoneMsg + ";;" + tMailText02;
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
            */

            //26.「義務人/技師變更報備申請」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV26 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A6.LOCKDATE, 23),'') as LOCKDATE from OnlineApply06 A6 LEFT JOIN SWCCASE SWC ON A6.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV26a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A6.LOCKDATE, 23),'') as LOCKDATE from OnlineApply06 A6 LEFT JOIN SWCCASE SWC ON A6.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //27.「工期展延申請」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV27 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A7.LOCKDATE, 23),'') as LOCKDATE from OnlineApply07 A7 LEFT JOIN SWCCASE SWC ON A7.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV27a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A7.LOCKDATE, 23),'') as LOCKDATE from OnlineApply07 A7 LEFT JOIN SWCCASE SWC ON A7.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //28.「停工申請」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV28 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A8.LOCKDATE, 23),'') as LOCKDATE from OnlineApply08 A8 LEFT JOIN SWCCASE SWC ON A8.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV28a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A8.LOCKDATE, 23),'') as LOCKDATE from OnlineApply08 A8 LEFT JOIN SWCCASE SWC ON A8.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //29.「設施調整報備申請」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV29 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A5.LOCKDATE, 23),'') as LOCKDATE from OnlineApply05 A5 LEFT JOIN SWCCASE SWC ON A5.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV29a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A5.LOCKDATE, 23),'') as LOCKDATE from OnlineApply05 A5 LEFT JOIN SWCCASE SWC ON A5.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //30.「開工/復工展延申請」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV30 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A3.LOCKDATE, 23),'') as LOCKDATE from OnlineApply03 A3 LEFT JOIN SWCCASE SWC ON A3.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV30a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A3.LOCKDATE, 23),'') as LOCKDATE from OnlineApply03 A3 LEFT JOIN SWCCASE SWC ON A3.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //31.「開工申報」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV31 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A4.LOCKDATE, 23),'') as LOCKDATE from OnlineApply04 A4 LEFT JOIN SWCCASE SWC ON A4.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV31a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A4.LOCKDATE, 23),'') as LOCKDATE from OnlineApply04 A4 LEFT JOIN SWCCASE SWC ON A4.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //32.「暫停審查申請」申請單，送出3.6天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV32 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A2.LOCKDATE, 23),'') as LOCKDATE from OnlineApply02 A2 LEFT JOIN SWCCASE SWC ON A2.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-6; ";
            string strSQLRV32a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A2.LOCKDATE, 23),'') as LOCKDATE from OnlineApply02 A2 LEFT JOIN SWCCASE SWC ON A2.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-3; ";

            //33.「完工申報」申請單,送出27.30天後檢核是否有「完工檢查表單」，若無則發信通知。
            string strSQLRV33 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A9.LOCKDATE, 23),'') as LOCKDATE from OnlineApply09 A9 LEFT JOIN SWCCASE SWC ON A9.SWC000=SWC.SWC000 left join SWCDTL06 D6 on A9.SWC000 = D6.SWC000 where isnull(A9.DATALOCK,'') = 'Y' and isnull(D6.DTLF000, '') = '' and A9.LOCKDATE < getdate() - 30; ";
            string strSQLRV33a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A9.LOCKDATE, 23),'') as LOCKDATE from OnlineApply09 A9 LEFT JOIN SWCCASE SWC ON A9.SWC000=SWC.SWC000 left join SWCDTL06 D6 on A9.SWC000 = D6.SWC000 where isnull(A9.DATALOCK,'') = 'Y' and isnull(D6.DTLF000, '') = '' and A9.LOCKDATE < getdate() - 27; ";

            //34.「完工申報」申請單 送出33.36天後檢核是否已進行准駁動作，若無則發信通知。
            string strSQLRV34 = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A9.LOCKDATE, 23),'') as LOCKDATE from OnlineApply09 A9 LEFT JOIN SWCCASE SWC ON A9.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-36; ";
            string strSQLRV34a = " select SWC.SWC005,SWC.SWC012,SWC.SWC025,isnull(CONVERT(varchar(100), A9.LOCKDATE, 23),'') as LOCKDATE from OnlineApply09 A9 LEFT JOIN SWCCASE SWC ON A9.SWC000=SWC.SWC000 where isnull(DATALOCK,'')='Y' and isnull(DATALOCK2,'')<>'Y' and LOCKDATE<getdate()-33; ";

            string[] arraySqlStr = new string[] { strSQLRV26, strSQLRV27, strSQLRV28, strSQLRV29, strSQLRV30, strSQLRV31, strSQLRV32, strSQLRV33, strSQLRV34, strSQLRV26a, strSQLRV27a, strSQLRV28a, strSQLRV29a, strSQLRV30a, strSQLRV31a, strSQLRV32a, strSQLRV33a, strSQLRV34a };

            for (int i = 0; i < arraySqlStr.Length-1; i++)
            {
                string tSQLSTR = arraySqlStr[i];

                if (tSQLSTR != "")
                {
                    objCmdSwc = new SqlCommand(tSQLSTR, SwcConn);
                    readeSwc = objCmdSwc.ExecuteReader();
                    
                    while (readeSwc.Read())
                    {
                        string tMailText = "";
                        string tSWC005 = readeSwc["SWC005"] + "";
                        string tSWC012 = readeSwc["SWC012"] + "";
                        string tSWC025 = readeSwc["SWC025"] + "";
                        string tLOCKDATE = readeSwc["LOCKDATE"] + "";

                        switch (i)
                        {
                            case 0:
                            case 9:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 "+ tLOCKDATE + " 申請「義務人/技師變更報備」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 1:
                            case 10:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「工期展延」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 2:
                            case 11:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「停工申請」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 3:
                            case 12:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「設施調整報備」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 4:
                            case 13:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「開工/復工展延申請」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 5:
                            case 14:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「開工申報」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 6:
                            case 15:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「暫停審查」，尚未進行准駁，請確認辦理情形。";
                                break;
                            case 7:
                            case 16:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「完工申報」，尚未進行完工檢查，請確認辦理情形。";
                                break;
                            case 8:
                            case 17:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「完工申報」，尚未進行准駁，請確認辦理情形。";
                                break;
                        }

                        for (int ii = 1; ii < arrayChkUserId.Length; ii++)
                        {
                            string tG1UserName = arrayChkUserName[i];
                            string tG1JObTitle = arrayJobTitle[i];
                            string tG1MbGroup = arrayMbGroup[i];

                            if (tG1JObTitle == "科長" || tG1JObTitle == "正工" || tG1JObTitle == "股長" || tG1MbGroup == "系統管理員" || tG1UserName.Trim() == tSWC025.Trim())
                            {
                                arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText + "<br>";
                            }
                        }
                    }
                    readeSwc.Close();
                    objCmdSwc.Dispose();
                }
            }
			
			//「建議核定申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「建議核定申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「建議核定申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVSA2001_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from tslm2.dbo.SwcApply2001 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVSA2001_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from tslm2.dbo.SwcApply2001 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVSA2001_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from tslm2.dbo.SwcApply2001 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「開工/復工申報」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「開工/復工申報」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「開工/復工申報」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA004_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply04 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA004_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply04 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA004_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply04 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「設施調整報備申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「設施調整報備申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「設施調整報備申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA005_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply05 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA005_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply05 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA005_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply05 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「義務人/技師變更報備申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「義務人/技師變更報備申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「義務人/技師變更報備申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA006_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply06 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA006_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply06 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA006_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply06 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「工期展延申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「工期展延申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「工期展延申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA007_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply07 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA007_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply07 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA007_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply07 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「停工申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「停工申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「停工申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA008_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply08 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA008_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply08 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA008_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply08 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「完工申報」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「完工申報」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「完工申報」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA009_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply09 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA009_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply09 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA009_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply09 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「失效重核申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「失效重核申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「失效重核申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVOA011_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply11 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,3,LOCKDATE) else Dateadd(DAY,1,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) ";
            string strSQLRVOA011_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply11 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,4,LOCKDATE) else Dateadd(DAY,2,LOCKDATE) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
            string strSQLRVOA011_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, LOCKDATE, 23) LOCKDATE from OnlineApply11 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and LOCKDATE >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, LOCKDATE, 23)) in (5,6) then Dateadd(DAY,5,LOCKDATE) else Dateadd(DAY,3,LOCKDATE) end) ";
			
			//「完工檢查紀錄申請」申請單，送出第2天檢核是否已進行簽核動作，若無則發信通知。(承辦)
			//「完工檢查紀錄申請」申請單，送出第3天檢核是否已進行簽核動作，若無則發信通知。(股長 tcge7)
			//「完工檢查紀錄申請」申請單，送出第4天檢核是否已進行簽核動作，若無則發信通知。(第4天以後每天都發)(科長)
			string strSQLRVSWCDT006_1 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, savedate, 23) LOCKDATE from SWCDTL06 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and savedate >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, savedate, 23)) in (5,6) then Dateadd(DAY,3,savedate) else Dateadd(DAY,1,savedate) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, savedate, 23)) in (5,6) then Dateadd(DAY,4,savedate) else Dateadd(DAY,2,savedate) end) ";
            string strSQLRVSWCDT006_2 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, savedate, 23) LOCKDATE from SWCDTL06 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and savedate >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, savedate, 23)) in (5,6) then Dateadd(DAY,4,savedate) else Dateadd(DAY,2,savedate) end) and getdate() <= (case when datepart(DW, CONVERT(varchar, savedate, 23)) in (5,6) then Dateadd(DAY,5,savedate) else Dateadd(DAY,3,savedate) end) ";
            string strSQLRVSWCDT006_3 = " select SWC.SWC005, SWC.SWC012, SWC.SWC025, CONVERT(varchar, savedate, 23) LOCKDATE from SWCDTL06 left join (select SWC005,SWC012,SWC025 from SWCCASE) SWC on SWC000 = SWC.SWC005 where DATALOCK = 'Y' and SING007 = '送出' and SING008 = '待簽辦' and savedate >= '2022-10-01' and getdate() > (case when datepart(DW, CONVERT(varchar, savedate, 23)) in (5,6) then Dateadd(DAY,5,savedate) else Dateadd(DAY,3,savedate) end) ";
			
            
            arraySqlStr = new string[] { 
				strSQLRVSA2001_1, strSQLRVSA2001_2, strSQLRVSA2001_3,
				strSQLRVOA004_1, strSQLRVOA004_2, strSQLRVOA004_3,
				strSQLRVOA005_1, strSQLRVOA005_2, strSQLRVOA005_3,
				strSQLRVOA006_1, strSQLRVOA006_2, strSQLRVOA006_3,
				strSQLRVOA007_1, strSQLRVOA007_2, strSQLRVOA007_3,
				strSQLRVOA008_1, strSQLRVOA008_2, strSQLRVOA008_3,
				strSQLRVOA009_1, strSQLRVOA009_2, strSQLRVOA009_3,
				strSQLRVOA011_1, strSQLRVOA011_2, strSQLRVOA011_3,
				strSQLRVSWCDT006_1, strSQLRVSWCDT006_2, strSQLRVSWCDT006_3
			};
			
            for (int i = 0; i < arraySqlStr.Length-1; i++)
            {
                string tSQLSTR = arraySqlStr[i];

                if (tSQLSTR != "")
                {
                    objCmdSwc = new SqlCommand(tSQLSTR, SwcConn);
                    readeSwc = objCmdSwc.ExecuteReader();
                    
                    while (readeSwc.Read())
                    {
                        string tMailText = "";
                        string tSWC005 = readeSwc["SWC005"] + "";
                        string tSWC012 = readeSwc["SWC012"] + "";
                        string tSWC025 = readeSwc["SWC025"] + "";
                        string tLOCKDATE = readeSwc["LOCKDATE"] + "";

                        switch (i)
                        {
                            case 0:
                            case 1:
                            case 2:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 "+ tLOCKDATE + " 申請「建議核定」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 3:
                            case 4:
                            case 5:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「開工/復工申報」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 6:
                            case 7:
                            case 8:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「設施調整報備」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 9:
                            case 10:
                            case 11:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「義務人/技師變更報備」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 12:
                            case 13:
                            case 14:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「工期展延」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 15:
                            case 16:
                            case 17:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「停工申請」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 18:
                            case 19:
                            case 20:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「完工申報」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 21:
                            case 22:
                            case 23:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「失效重核」，尚未進行簽辦，請確認審核情形。";
                                break;
                            case 24:
                            case 25:
                            case 26:
                                tMailText = tSWC012 + "【" + tSWC005 + "】於 " + tLOCKDATE + " 申請「完工檢查紀錄」，尚未進行簽辦，請確認審核情形。";
                                break;
                        }

                        for (int ii = 1; ii < arrayChkUserId.Length; ii++)
                        {
                            string tG1UserName = arrayChkUserName[i];
                            string tG1JObTitle = arrayJobTitle[i];
                            string tG1MbGroup = arrayMbGroup[i];

                            if (tG1UserName.Trim() == tSWC025.Trim() && (i == 0 || i == 3 || i == 6 || i == 9 || i == 12 || i == 15 || i == 18 || i == 21 || i == 24))
                            {
                                arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText + "<br>";
                            }
							else if (tG1JObTitle == "股長" && (i == 1 || i == 4 || i == 7 || i == 10 || i == 13 || i == 16 || i == 19 || i == 22 || i == 25))
                            {
                                arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText + "<br>";
								string[] _sMail = new string[] { "tcge7@geovector.com.tw" };
								bool _MailTo = GBSBApp.Mail_Send(_sMail, tMailText, tMailText);
                            }
							else if (tG1JObTitle == "科長" && (i == 2 || i == 5 || i == 8 || i == 11 || i == 14 || i == 17 || i == 20 || i == 23 || i == 26))
                            {
                                arrayChkUserMailTxt[i] = arrayChkUserMailTxt[i] + tMailText + "<br>";
                            }
                        }
                    }
                    readeSwc.Close();
                    objCmdSwc.Dispose();
                }
            }

            //41.若狀態為「審查中」，A「受理日期」17天後發信提醒；B「修正本上傳日期」17天後發信提醒。
            //提醒您，【水土保持計畫】記得召開審查會議並於平台上傳審查意見。"
            //公會寄email 召集人寄email"
            #region 41...
            string StrMail41 = " select G.ETID,SC.* from SWCCASE SC left join ShareFiles SF on SC.SWC000 = SF.SWC000 left join GuildGroup G ON SC.SWC000=G.SWC000 AND RGSID=1  left join ETUsers EU ON EU.ETID=G.ETID  left join tslm2.dbo.geouser GU ON SC.SWC022ID=GU.userid where SWC004 = '審查中' and (CONVERT(varchar(100), DATEADD(DAY, 17, SC.SWC034), 23) = CONVERT(varchar(100), GETDATE(), 23) or  CONVERT(varchar(100), DATEADD(DAY, 17, SF.savedate), 23) = CONVERT(varchar(100), GETDATE(), 23)); ";

            objCmdSwc = new SqlCommand(StrMail41, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC022ID = readeSwc["SWC022ID"] + "";
                string tETID = readeSwc["ETID"] + "";

                string tMailText02 = "提醒您，【" + tSWC005 + "】記得召開審查會議並於平台上傳審查意見。(此為系統自動發信，若已召開審查會及上傳會議記錄可忽略此郵件)";
                for (int i = 1; i < arrayChkGuildId.Length; i++)
                {
                    string tGuildID = arrayChkGuildId[i];

                    if (tGuildID == tSWC022ID)
                    {
                        arrayChkGuildMailText[i] = arrayChkGuildMailText[i] + tMailText02 + "<br>";
                    }
                }

                for (int i = 1; i < arrayETUserId.Length; i++)
                {
                    string tETUserID = arrayETUserId[i];

                    if (tETUserID == tETID && tETID!="")
                    {
                        arrayETUserMailText[i] = arrayETUserMailText[i] + tMailText02 + "<br>";
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
            #endregion

        }




        string ChkMailSub = GBSBApp.DateView(DateTime.Now.ToString(), "00") + " 水土保持書件管理平台子表檢核通知信";

        string ssMailFFF = "<br>「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        ssMailFFF = ssMailFFF + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

        //寄信
        string[] sMailh = new string[] { "geocheck@geovector.com.tw", "hhsu@geovector.com.tw" };
        for (int i = 1; i < arrayChkUserId.Length; i++)
        {
            //string[] sMail = new string[] { arrayChkMail[i] };
            string[] sMail = new string[] { "geocheck@geovector.com.tw", "hhsu@geovector.com.tw" };
            string sUserName = arrayChkUserName[i];
            string sUserJob = arrayJobTitle[i];
            string sMailText = arrayChkUserMailTxt[i];

            if (sMailText != "" )
            {
                //bool MailTo01 = GBSBApp.Mail_Send(sMail, ChkMailSub, sUserName+ sUserJob+"，您好"+sMailText + ssMailFFF);
            }
        }
        for (int i = 0; i < arrayChkGuildId.Length-1; i++)
        {
            string[] sMail = new string[] { arrayChkGuildMail[i] };
            //string[] sMail = new string[] { "tim@geovector.com.tw", "hhsu@geovector.com.tw" };
            string sUserName = arrayChkGuildName[i];
            string sMailText = arrayChkGuildMailText[i];

            if (sMailText != "")
            {
                bool MailTo02 = GBSBApp.Mail_Send(sMail, ChkMailSub, sUserName + "，您好<br><br>" + sMailText + ssMailFFF);
                bool MailTo02h = GBSBApp.Mail_Send(sMailh, ChkMailSub, sUserName + "，您好<br><br>" + sMailText + ssMailFFF);
            }
        }
        for (int i = 0; i < arrayETUserId.Length-1; i++)
        {
            string[] sMail = new string[] { arrayETUserMail[i] };
            //string[] sMail = new string[] { "tim@geovector.com.tw", "hhsu@geovector.com.tw" };
            string sETuserName = arrayETUserName[i];
            string sMailText = arrayETUserMailText[i];

            if (sMailText != "")
            {
                bool MailTo03 = GBSBApp.Mail_Send(sMail, ChkMailSub, sETuserName + "，您好<br><br>" + sMailText + ssMailFFF);
                bool MailTo03h = GBSBApp.Mail_Send(sMailh, ChkMailSub, sETuserName + "，您好<br><br>" + sMailText + ssMailFFF);
            }
        }
		
		//因為要處理複數義務人 改在每一段簡訊處理時傳
        //傳簡訊
        //string[] arrayPhoneNo = strPhoneNO.Split(new string[] { ";;" }, StringSplitOptions.None);
        //string[] arrayPhoneMsg = strPhoneMsg.Split(new string[] { ";;" }, StringSplitOptions.None);
		//
        //for (int i = 0; i < arrayPhoneNo.Length-1; i++)
        //{
        //    string sPhoneNo = arrayPhoneNo[i];
        //    string sPhoneMsg = arrayPhoneMsg[i];
		//
        //    GBSBApp.SendSMS(sPhoneNo, sPhoneMsg.Replace("<br>",""));
        //}
    }

    private bool hasD5Data(string tmpSWC000)
    {
        bool rValue = false;
        string sqlStr = " select * from SWCDTL05 Where SWC000='"+ tmpSWC000 + "' and DATALOCK='Y' and DTLE088a = convert(nvarchar(10),GETDATE()-7,121) and DTLE088b = convert(nvarchar(10),GETDATE()-1,121); ";
        if (Week().Trim()=="五") { sqlStr = " select * from SWCDTL05 Where SWC000='" + tmpSWC000 + "' and DATALOCK='Y' and DTLE088a = convert(nvarchar(10),GETDATE()-9,121) and DTLE088b = convert(nvarchar(10),GETDATE()-3,121); "; }
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection swcConn = new SqlConnection(connectionString.ConnectionString))
        {
            swcConn.Open();
            using (var cmd = swcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    while (readerSwc.Read())
                        rValue = true;
                    readerSwc.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    protected string Week()
    {
        string[] weekdays = { "日", "一", "二", "三", "四", "五", "六" };
        string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
        return week;
    }
    private void GetETUserMailData()
    {
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            string strSQLRV = " select * from ETUsers ";
            strSQLRV = strSQLRV + " where status = '已開通' ";

            SqlDataReader readeUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
            readeUser = objCmdUser.ExecuteReader();

            while (readeUser.Read())
            {
                string tUserId = readeUser["ETID"] + "";
                string tUserName = readeUser["ETName"] + "";
                string tUserMail = readeUser["ETEmail"] + "";

                ChkETUserId = ChkETUserId + ";;" + tUserId;
                ChkETUserName = ChkETUserName + ";;" + tUserName;
                ChkETUserMail = ChkETUserMail + ";;" + tUserMail;
                ChkETUserMailText = ChkETUserMailText + ";;";
                
            }
        }
    }

    private void GetGuildMailData()
    {
        ConnectionStringSettings connectionStringGeo = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection GeoConn = new SqlConnection(connectionStringGeo.ConnectionString))
        {
            GeoConn.Open();

            string strSQLUser = " select * from geouser ";
            strSQLUser = strSQLUser + " where status = '正常' ";
            strSQLUser = strSQLUser + "   and unit = '技師公會' ";
            strSQLUser = strSQLUser + " order by userid  ";

            SqlDataReader readerGeo;
            SqlCommand objCmdGeo = new SqlCommand(strSQLUser, GeoConn);
            readerGeo = objCmdGeo.ExecuteReader();

            while (readerGeo.Read())
            {
                string tUserId = readerGeo["userid"] + "";
                string tUserName = readerGeo["name"] + "";
                string tUserMail = readerGeo["email"] + "";

                ChkGuildId = ChkGuildId + ";;" + tUserId;
                ChkGuildName = ChkGuildName + ";;" + tUserName;
                ChkGuildMail = ChkGuildMail + ";;" + tUserMail;
                ChkGuildMailText = ChkGuildMailText + ";;";

            }

        }
    }

    private void GetUserMailData()
    {
        ConnectionStringSettings connectionStringGeo = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection GeoConn = new SqlConnection(connectionStringGeo.ConnectionString))
        {
            GeoConn.Open();

            string strSQLUser = " select * from geouser ";
            strSQLUser = strSQLUser + " where department = '審查管理科' ";
            strSQLUser = strSQLUser + "   and status = '正常' ";
            strSQLUser = strSQLUser + "   and unit = '工務局大地工程處' ";
            strSQLUser = strSQLUser + " order by userid  ";

            SqlDataReader readerGeo;
            SqlCommand objCmdGeo = new SqlCommand(strSQLUser, GeoConn);
            readerGeo = objCmdGeo.ExecuteReader();

            while (readerGeo.Read())
            {
                string tUserId = readerGeo["userid"] + "";
                string tUserName = readerGeo["name"] + "";
                string tUserMail = readerGeo["email"] + "";
                string tUserJBTitle = readerGeo["JobTitle"] + "";
                string tmbgroup = readerGeo["mbgroup02"] + "";

                ChkUserId = ChkUserId + ";;" + tUserId;
                ChkUserName = ChkUserName + ";;" + tUserName;
                ChkJobTitle = ChkJobTitle + ";;" + tUserJBTitle;
                ChkMbGroup = ChkMbGroup + ";;" + tmbgroup;                
                ChkMail = ChkMail + ";;" + tUserMail;
                ChkMailText = ChkMailText + ";;";

            }

        }
    }


    

    

    
}