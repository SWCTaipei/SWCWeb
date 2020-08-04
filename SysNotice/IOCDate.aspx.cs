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

        string strPhoneNO = "";
        string strPhoneMsg = "";

        Class01 SBApp = new Class01();
        GBClass001 GBSBApp = new GBClass001();
        
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            SwcConn.Open();

            //水保案件：狀態為「施工中」，「施工檢查表」-03
            //1.「是否辦理檢查」：每月1號檢核，5 - 11月每月皆應檢查、12 - 4月1公頃以下案件2個月至少應檢查1次，1公頃以上案件每月至少應檢查1次。
            //2.「檢查書件繳送是否預期」：檢查日期與送出日期不得超過固定天數(6工作天)(行政院人事行政總處是否有api可介接)

            int tThisMonth = DateTime.Now.Month;
            int tThisDay = DateTime.Now.Day;

            if (tThisDay == 1)
            {

                string strSQLRV15a = " select C.SWC002,C.SWC005,C.SWC012,C.SWC024ID,C.SWC025,ISNULL(C.SWC023,0) AS SWC023,ISNULL(D3.DTLC002,GETDATE()-1000) AS DTLC002,D3.DATALOCK from SWCCASE C ";
                strSQLRV15a = strSQLRV15a + " LEFT JOIN SWCDTL03 D3 ON C.SWC000 = D3.SWC000 AND D3.DTLC000 = (SELECT MAX(DTLC000) FROM SWCDTL03 D3m WHERE C.SWC000 = D3m.SWC000 ) ";
                strSQLRV15a = strSQLRV15a + " WHERE C.SWC004 = '施工中' ORDER BY C.SWC000 ";

                SqlDataReader readeSwcA;
                SqlCommand objCmdSwcA = new SqlCommand(strSQLRV15a, SwcConn);
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

                    //tThisMonth：本月份，未填檢查日期,每月1日檢查
                    if (tThisDay == 1)
                    {
                        if (dSwc023 < 2)
                        {
                            //1公頃以下，5.6.7.8.9.10.11，每月檢查1次，12.1.2.3.4，每2月檢查1次。(檢查月：2.4.5.6.7.8.9.10.11.12)
                            if (tThisMonth != 1 && tThisMonth != 3)
                            {
                                CheckHadDate = true;
                            }
                        }
                        else
                        {
                            //1公頃以上，每月檢查1次。
                            CheckHadDate = true;
                        }
                    }
                    if (CheckHadDate)
                    {
                        DateTime tChkDate = DateTime.Now;

                        if (dDTLC002 < tChkDate)
                        {
                            //尚未填寫檢查日期請盡速填寫。
                            string tMailText01 = tSWC012 + "【" + tSWC005 + "】本月預計檢查日期尚未送出，請確認公會是否依合約辦理。";
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

                            string tMailText02 = "提醒您，【" + tSWC005 + "】本月預計檢查日期尚未送出，請確認並盡速辦理。";
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

                string tMailText01 = tSWC012 + "【" + tSWC005 + "】本月檢查紀錄尚未送出，請確認公會是否依合約辦理。";
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

            //水保案件：狀態為「施工中」，「監造紀錄表」-05
            //1.檢查過去7天內有無檔案，若無則須通知。 (無檔案：子表單，送出日期)
            //2.開工日期<過去7天

            string strSQLRV16 = " select SWC.SWC000,SWC.SWC005,SWC.SWC012,SWC.SWC013,SWC.SWC013TEL,SWC.SWC024ID,SWC.SWC025,SWC.SWC045ID,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV16 = strSQLRV16 + "  LEFT JOIN SWCDTL05 D5 ON SWC.SWC000 = D5.SWC000 ";
            strSQLRV16 = strSQLRV16 + " WHERE SWC.SWC004 = '施工中' ";
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
                        arrayChkGuildMailText[i] = arrayChkGuildMailText[i] + tMailText02 + "<br>";
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
        }




        string ChkMailSub = SBApp.DateView(DateTime.Now.ToString(), "00") + " 水土保持書件管理平台子表檢核通知信";

        string ssMailFFF = "<br>「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        ssMailFFF = ssMailFFF + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

        //寄信

        for (int i = 1; i < arrayChkUserId.Length; i++)
        {
            string[] sMail = new string[] { arrayChkMail[i] };
            string sUserName = arrayChkUserName[i];
            string sUserJob = arrayJobTitle[i];
            string sMailText = arrayChkUserMailTxt[i];

            if (sMailText != "" )
            {
                bool MailTo01 = SBApp.Mail_Send(sMail, ChkMailSub, sUserName+ sUserJob+"，您好"+sMailText + ssMailFFF);
            }
        }
        for (int i = 0; i < arrayChkGuildId.Length-1; i++)
        {
            string[] sMail = new string[] { arrayChkGuildMail[i] };
            string sUserName = arrayChkUserName[i];
            string sMailText = arrayChkGuildMailText[i];

            if (sMailText != "")
            {
                bool MailTo02 = SBApp.Mail_Send(sMail, ChkMailSub, sUserName + "，您好" + sMailText + ssMailFFF);
            }
        }
        for (int i = 0; i < arrayETUserId.Length-1; i++)
        {
            string[] sMail = new string[] { arrayETUserMail[i] };
            string sETuserName = arrayETUserName[i];
            string sMailText = arrayETUserMailText[i];

            if (sMailText != "")
            {
                bool MailTo03 = SBApp.Mail_Send(sMail, ChkMailSub, sETuserName + "，您好" + sMailText + ssMailFFF);
            }
        }

        //傳簡訊

        string[] arrayPhoneNo = strPhoneNO.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayPhoneMsg = strPhoneMsg.Split(new string[] { ";;" }, StringSplitOptions.None);

        for (int i = 0; i < arrayPhoneNo.Length-1; i++)
        {
            string sPhoneNo = arrayPhoneNo[i];
            string sPhoneMsg = arrayPhoneMsg[i];

            GBSBApp.SendSMS(sPhoneNo, sPhoneMsg.Replace("<br>",""));
        }




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