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
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// GBClass001 的摘要描述
/// </summary>
public class GBClass001 : System.Web.UI.Page
{
    public GBClass001()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public Boolean GetLoginStatus(string lID, string lPw, string lType)
    {
        Boolean _ReturnValue = false;
        string SQL_PW = "", SQL_Unit = "", SQL_NAME = "", SQL_department = "", SQL_AuthMwall = "", SQL_Status = "", SQL_DataView = "", SQL_DateEdit = "", SQL_jobtitle = "", SQL_jobTitle="", SQL_EDIT="";

        Session["UserType"] = "";
        Session["ID"] = "";
        Session["PW"] = "";
        Session["NAME"] = "";
        Session["Unit"] = "";
        Session["JobTitle"] = "";
        Session["Edit4"] = "";

        switch (lType)
        {
            case "01":
                Session["UserType"] = lType;
                Session["ID"] = lID;
                Session["PW"] = lPw;
                _ReturnValue = true;
                break;

            case "02":
                ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection UserConn = new SqlConnection(connectionStringSwc.ConnectionString))
                {
                    UserConn.Open();

                    string strSQLRV = "select * from ETUsers ";
                    strSQLRV = strSQLRV + " where ETID COLLATE Latin1_General_CS_AI ='" + lID + "' ";
                    //strSQLRV = strSQLRV + "   and status = '正常' ";
                    strSQLRV = strSQLRV + "   and ETStatus = '1' ";

                    SqlDataReader readerUser;
                    SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
                    readerUser = objCmdUSER.ExecuteReader();

                    while (readerUser.Read())
                    {
                        SQL_PW = readerUser["ETPW"] + "";
                        SQL_NAME = readerUser["ETName"] + "";
                        SQL_jobTitle = "技師";
                    }
                    if (lPw == SQL_PW)
                    {
                        Session["UserType"] = lType;
                        Session["ID"] = lID;
                        Session["PW"] = SQL_PW;
                        Session["NAME"] = SQL_NAME;
                        Session["Unit"] = SQL_Unit;
                        Session["JobTitle"] = SQL_jobTitle;

                        _ReturnValue = true;
                    }
                }
                break;

            case "03":
            case "04":                
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
                using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
                {
                    UserConn.Open();

                    string strSQLRV = "select * from geouser ";
                    strSQLRV = strSQLRV + " where userid='" + lID + "' ";
                    strSQLRV = strSQLRV + "   and status = '正常' ";
                    if (lType == "03" && lID != "gv-admin")
                    {
                        strSQLRV = strSQLRV + "   and unit ='工務局大地工程處' ";
                    }
                    if (lType == "04" && lID != "gv-admin")
                    {
                        strSQLRV = strSQLRV + "   and unit ='技師公會' ";
                    }

                    SqlDataReader readerUser;
                    SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
                    readerUser = objCmdUSER.ExecuteReader();

                    while (readerUser.Read())
                    {
                        SQL_PW = readerUser["passwd"] + "";
                        SQL_NAME = readerUser["name"] + "";
                        SQL_Unit = readerUser["unit"] + "";
                        SQL_EDIT = readerUser["TcgeDataedit"] + "";
                        //完工設施維護檢查表登錄

                    }

                    if (lPw == SQL_PW)
                    {
                        Session["UserType"] = lType;
                        Session["ID"] = lID;
                        Session["PW"] = SQL_PW;
                        Session["NAME"] = SQL_NAME;
                        Session["Unit"] = SQL_Unit;
                        if (SQL_EDIT.IndexOf("完工設施維護檢查表登錄;") >= 0) {

                            Session["Edit4"] = "Y";

                        }

                        _ReturnValue = true;

                    } else
                    {
                        Session["UserType"] = "";
                        Session["ID"] = "";
                        Session["PW"] = "";
                        Session["NAME"] = "";
                        Session["Unit"] = "";
                        Session["Edit4"] = "";
                    }

                }
                break;

        }

        string tempLS = "0";
        if (_ReturnValue) { tempLS = "1"; } 
        LoginNotes(lID, lType, tempLS);

        return _ReturnValue;
    }
    public void LoginNotes(string UserID,string UserType, string LoginStatus)
    {
        string clientIP = GetClientIP();
        string strSQLErr = "";

        LoginStatus = LoginStatus + "";
        if (LoginStatus != "0") { LoginStatus = "1"; }

        //從資料庫取得資料
        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        //strSQLErr = " update geouser set loginstaus = '" + LoginStatus + "' where userid='" + UserID + "';";
        strSQLErr = strSQLErr + " insert into loginnotes (userid,usertype,success,logtime,loginip) values ";
        strSQLErr = strSQLErr + " ('" + UserID + "','"+ UserType + "','" + LoginStatus + "',getdate(),'" + clientIP + "');";

        SqlCommand objCmdRV = new SqlCommand(strSQLErr, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();
    }

    public string DateView(string gValue, string oType)
    {
        //00:2017-12-12
        //01：
        //02：106.12.12
        //03：2016年04月13日
        //04：106-12-12

        string rValue = "";

        try
        {
            switch (oType)
            {
                case "00":
                    rValue = Convert.ToDateTime(gValue).ToString("yyyy-MM-dd");
                    break;
                case "01":
                    System.Globalization.TaiwanCalendar tc = new System.Globalization.TaiwanCalendar();
                    DateTime d;
                    if (DateTime.TryParse(gValue, out d))
                    {
                        d = Convert.ToDateTime(gValue);
                        rValue = String.Format("{0}年{1}月{2}日", tc.GetYear(d), tc.GetMonth(d), tc.GetDayOfMonth(d));
                    }
                    break;
                case "02":
                    System.Globalization.TaiwanCalendar tc2 = new System.Globalization.TaiwanCalendar();
                    DateTime d2;
                    if (DateTime.TryParse(gValue, out d2))
                    {
                        d2 = Convert.ToDateTime(gValue);
                        rValue = String.Format("{0}.{1:00}.{2:00}", tc2.GetYear(d2), tc2.GetMonth(d2), tc2.GetDayOfMonth(d2));
                    }
                    break;
                case "03":
                    rValue = Convert.ToDateTime(gValue).ToString("yyyy年MM月dd日");
                    break;
                case "04":
                    System.Globalization.TaiwanCalendar tc4 = new System.Globalization.TaiwanCalendar();
                    DateTime d4;
                    if (DateTime.TryParse(gValue, out d2))
                    {
                        d2 = Convert.ToDateTime(gValue);
                        rValue = String.Format("{0}-{1:00}-{2:00}", tc4.GetYear(d2), tc4.GetMonth(d2), tc4.GetDayOfMonth(d2));
                    }
                    break;
            }
        }
        catch
        {
            rValue = gValue;
        }

        if (gValue != "")
        {
            if (gValue.Substring(0, 4) == "1900")
            {
                rValue = "";
            }
        }
        return rValue;
    }
    public string AlertMsg(string ShowMsg)
    {
        string MsgStr = "";

        MsgStr = "<script>alert('" + ShowMsg + "');</script>";

        return MsgStr;

        //Response.Write("<script> alert('" + ShowMsg + "') </script>");
    }
    public string SDQQSTR(string _ORGSTR) {
        string ReturnValue = "";
        ReturnValue = _ORGSTR.Replace("'","''");
        return ReturnValue;
    }
    public string GetETUser(string tUserId, string tType)
    {
        string rValue = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            
            string strSQLUS = " select * from ETUsers ";
            strSQLUS = strSQLUS + " where ETIDNo ='" + tUserId + "' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLUS, SwcConn);
            readerUser = objCmdUser.ExecuteReader();

            while (readerUser.Read())
            {
                string tName = readerUser["ETName"] + "";
                string tETOrgName = readerUser["ETOrgName"] + "";
                string tETOrgIssNo = readerUser["ETOrgIssNo"] + "";
                string tETOrgGUINo = readerUser["ETOrgGUINo"] + "";
                string tETOrgTel = readerUser["ETOrgTel"] + "";
                string tETOrgAddr = readerUser["ETOrgAddr"] + "";
                string tETEmail = readerUser["ETEmail"] + ""; 

                switch (tType)
                {
                    case "Name":
                        rValue = tName;
                        break;
                    case "Email":
                        rValue = tETEmail;
                        break;
                    case "OrgName":
                        rValue = tETOrgName;
                        break;
                    case "OrgIssNo":
                        rValue = tETOrgIssNo;
                        break;
                    case "OrgGUINo":
                        rValue = tETOrgGUINo;
                        break;
                    case "OrgTel":
                        rValue = tETOrgTel;
                        break;
                    case "OrgAddr":
                        rValue = tETOrgAddr;
                        break;



                }


            }

        }
        return rValue;
    }
    public string GetGeoUser(string tUserId, string tType)
    {
        string rValue = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            string strSQLRV = "select * from geouser ";
            strSQLRV = strSQLRV + " where userid='" + tUserId + "' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
            readerUser = objCmdUser.ExecuteReader();

            while (readerUser.Read())
            {
                string tUserName = readerUser["Name"] + "";
                string tUserEmail = readerUser["email"] + "";

                switch (tType)
                {
                    case "Email":
                        rValue = tUserEmail;
                        break;
                    case "Name":
                        rValue = tUserName;
                        break;
                }
            }

        }
        return rValue;
    }
    public string GetVisitorsCount()
    {
        string VCount = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string VisitorsCmd = " SELECT isnull(Max(id),0) as Visitor FROM ACTLOG ";
            
            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(VisitorsCmd, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                VCount = readeSwc["Visitor"] + "";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
        }

        return VCount;
    }

    public bool Mail_Send(string[] MailTos, string MailSub, string MailBody)
    {
        // 標題 
        // 收件人 - MailTos
        // 信件內容

        // 寄信人Email
        string sendMail = ConfigurationManager.AppSettings["sendMail"].Trim();
        // 寄信smtp server
        string smtpServer = ConfigurationManager.AppSettings["smtpServer"].Trim();
        // 寄信smtp server的Port，預設25
        int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"].Trim());
        // 寄信帳號
        string mailAccount = ConfigurationManager.AppSettings["mailAccount"].Trim();
        // 寄信密碼
        string mailPwd = ConfigurationManager.AppSettings["mailPwd"].Trim();

        string MailFrom = sendMail;

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConnM = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConnM.Open();

            string strSQLUser = " select * from SysValue where GVType = 'EMail' ";

            SqlDataReader readerSwcM;
            SqlCommand objCmdSwcM = new SqlCommand(strSQLUser, SwcConnM);
            readerSwcM = objCmdSwcM.ExecuteReader();

            while (readerSwcM.Read())
            {
                string tmpType  = readerSwcM["GVTypeDesc"] +"";
                string tmpValue = readerSwcM["GVValue"] + "";

                switch (tmpType)
                {
                    case "sendMail":
                        sendMail = tmpValue;
                        break;
                    case "smtpServer":
                        smtpServer = tmpValue;
                        break;
                    case "smtpPort":
                        smtpPort = Convert.ToInt32(tmpValue);
                        break;
                    case "mailAccount":
                        mailAccount = tmpValue;
                        break;
                    case "mailPwd":
                        mailPwd = tmpValue;
                        break;
                }
            }
            readerSwcM.Close();
            objCmdSwcM.ExecuteNonQuery();
            objCmdSwcM.Dispose();
        }

        string[] Ccs;
        /// <summary>
        /// 完整的寄信功能
        /// </summary>
        /// <param name="MailFrom">寄信人E-mail Address</param>
        /// <param name="MailTos">收信人E-mail Address</param>
        /// <param name="Ccs">副本E-mail Address</param>
        /// <param name="MailSub">主旨</param>
        /// <param name="MailBody">信件內容</param>
        /// <param name="isBodyHtml">是否採用HTML格式</param>
        /// <param name="filePaths">附檔在WebServer檔案總管路徑</param>
        /// <param name="deleteFileAttachment">是否刪除在WebServer上的附件</param>
        /// <returns>是否成功</returns>

        try
        {
            //建立MailMessage物件
            System.Net.Mail.MailMessage mms = new System.Net.Mail.MailMessage();
            //指定一位寄信人MailAddress
            mms.From = new MailAddress(MailFrom);
            //信件主旨
            mms.Subject = MailSub;
            //信件內容
            mms.Body = MailBody;
            //信件內容 是否採用Html格式
            mms.IsBodyHtml = true;

            if (MailTos != null)//防呆
            {
                for (int i = 0; i < MailTos.Length; i++)
                {
                    //加入信件的收信人(們)address
                    if (!string.IsNullOrEmpty(MailTos[i].Trim()))
                    {
                        mms.To.Add(new MailAddress(MailTos[i].Trim()));
                    }

                }
            }
            mms.Bcc.Add("geocheck@geovector.com.tw"); //這是密件副本收件者

            //if (Ccs != null) //防呆
            //{
            //    for (int i = 0; i < Ccs.Length; i++)
            //    {
            //        if (!string.IsNullOrEmpty(Ccs[i].Trim()))
            //        {
            //            //加入信件的副本(們)address
            //            mms.CC.Add(new MailAddress(Ccs[i].Trim()));
            //        }

            //    }
            //}//End if (Ccs!=null) //防呆

            using (SmtpClient client = new SmtpClient(smtpServer, smtpPort))//或公司、客戶的smtp_server
            {
                if (!string.IsNullOrEmpty(mailAccount) && !string.IsNullOrEmpty(mailPwd))//.config有帳密的話
                {
                    client.Credentials = new NetworkCredential(mailAccount, mailPwd);//寄信帳密
                }
                client.Send(mms);//寄出一封信
            }//end using 
             //釋放每個附件，才不會Lock住
            if (mms.Attachments != null && mms.Attachments.Count > 0)
            {
                for (int i = 0; i < mms.Attachments.Count; i++)
                {
                    mms.Attachments[i].Dispose();

                }
            }

            return true;//成功
        }
        catch (Exception ex)
        {
            return false;//寄失敗
        }
    }
    public void ViewRecord(string vPage,string vAct,string vCaseID)
    {
        string ssUserId = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";

        string wViewRecord = " INSERT INTO ACTLOG (ViewPage,ActPage,ActUser,ActTime,ActCase,UserType) VALUES ";
        wViewRecord = wViewRecord + " ('"+ vPage + "','"+ vAct + "','"+ ssUserId + "',getdate(),'"+ vCaseID + "','"+ ssUserType + "') ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlCommand objCmdUpd = new SqlCommand(wViewRecord, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }
    }
    public void SWCRecord(string iPage,string iCase,string sv)
    {
        //string ssUserId = Session["ID"] + "";
        //string gIp = GetClientIP();

        //sv = sv.Replace("'", "’");

        //string wRecord = " INSERT INTO SwcRecord (ActPage,ActIp,ActUser,ActTime,ActCase,ActSql) VALUES ";
        //wRecord = wRecord + " ('" + iPage + "','" + gIp + "','" + ssUserId + "',getdate(),'" + iCase + "','"+sv+"') ";

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        //{
        //    SwcConn.Open();

        //    SqlCommand objCmdUpd = new SqlCommand(wRecord, SwcConn);
        //    objCmdUpd.ExecuteNonQuery();
        //    objCmdUpd.Dispose();
        //}

    }
    public string GetClientIP()
    {
        string ip = "";

        if (Context.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
        {
            ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP. 得到真实的客户端地址
        }
        else    //如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
        {
            ip = Context.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP. 得到服务端的地址
        }
        return ip;
    }
    public bool CheckPageView() {
        bool bReturnValue = false;

        return bReturnValue;
    }
    public void UpdateShape(string casenoin, string Xstrin, string Ystrin, string EPSGstrin)
    {
        //casenoin 是案件的編號，才知道應更新哪一筆, Xstrin 是二度分帶97的X座標6位精確度是公尺
        //Ystrin 是二度分帶97的Y座標6位精確度是公尺, EPSGstrin 是地理座標的編碼，台灣二度分帶97是3826
        //[SWCSWC] 是水保案件資料表的名稱，違規、輔導、災害、兵棋推演等不同案件請自行替換
        //[Shape] 是SQL裡面我設定儲存空間資料的欄位名稱，以後會盡量統一存這一個，如有發現不同的請通知簡大哥討論修正的可行性
        //[SWC02] 是案件編號的欄位，才能確保是要更新哪一筆，違規、輔導、災害、兵棋推演等不同案件請自行替換
        //Ex: geometry::STGeomFromText ('POINT( 123456 1234567 )', 3826)

        try
        {
            if (Xstrin != "" && Ystrin != "")
            {
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
                {
                    string usSQLSTR = "UPDATE [SWCSWC] SET [Shape]=geometry::STGeomFromText('Point(" + Xstrin + " " + Ystrin + ")' ," + EPSGstrin + ") WHERE [SWC02]='" + casenoin + "'";

                    TslmConn.Open();
                    SqlCommand objCmdTslm = new SqlCommand(usSQLSTR, TslmConn);
                    objCmdTslm.ExecuteNonQuery();
                    objCmdTslm.Dispose();
                }
            }
        } catch { }
    }

    public string[] GetUserMailData()
    {
        string ChkUserId = "";
        string ChkUserName = "";
        string ChkUserJobTitle = "";
        string ChkMail = "";
        string ChkMBGROUP = "";
        string ChkMailText = "";

        ConnectionStringSettings connectionStringGeo = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection GeoConn = new SqlConnection(connectionStringGeo.ConnectionString))
        {
            GeoConn.Open();

            string strSQLUser = " select * from geouser ";
            strSQLUser = strSQLUser + " where (department = '審查管理科' and [status] ='正常') ";
            strSQLUser = strSQLUser + "    or userid = 'gv-hsun' ";
            strSQLUser = strSQLUser + "    or unit = '技師公會' ";
            strSQLUser = strSQLUser + " order by userid  ";

            SqlDataReader readerGeo;
            SqlCommand objCmdGeo = new SqlCommand(strSQLUser, GeoConn);
            readerGeo = objCmdGeo.ExecuteReader();

            while (readerGeo.Read())
            {
                string tUserId = readerGeo["userid"] + "";
                string tUserName = readerGeo["name"] + "";
                string tUserMail = readerGeo["email"] + "";
                string tUserJobTitle = readerGeo["jobtitle"] + "";
                string tUserMBGROUP = readerGeo["mbgroup02"] + "";
                string tUserTcgeDataedit = readerGeo["TcgeDataedit"] + "";

                ChkUserId = ChkUserId + ";;" + tUserId;
                ChkUserName = ChkUserName + ";;" + tUserName;
                ChkUserJobTitle = ChkUserJobTitle + ";;" + tUserJobTitle;
                ChkMail = ChkMail + ";;" + tUserMail;
                ChkMBGROUP = ChkMBGROUP + ";;" + tUserMBGROUP;
                ChkMailText = ChkMailText + ";;";

                if (tUserTcgeDataedit.IndexOf("完工設施維護檢查表登錄;") >= 0)
                {
                    Session["TempMailSWC107"] = tUserName;
                }
            }
        }
        string[] arrayReturnValue = new string[] { ChkUserId, ChkUserName, ChkUserJobTitle, ChkMail, ChkMBGROUP, ChkMailText };
        return arrayReturnValue;
    }
    public void SendSMS(string vbPhoneNo, string vbText)
    {
        var CellNo = vbPhoneNo;

        //以下設定要使用的平台商，請記得到上面的CELLNO改正確的分隔符號
        //下面這一行是原本的帳號與ID，但是沒錢了
        //string url = @"http://api.message.net.tw/send.php?longsms=1&id=lin2317&password=lwk762317&tel=" + CellNo + "&msg=";
        //下面這一行是新的多維備援用的的帳號與ID，有一些些錢
        //string url = @"http://api.message.net.tw/send.php?id=TCGE3001&password=GeoVector04&tel=" + CellNo + "&msg=";
        //下面這一行是市府簡訊平台的帳號與ID
        string url = @"http://sms.taipei.gov.tw:8000/SmSendGet.asp?username=tcgesms&password=tcge3001&dstaddr=" + CellNo + "&smbody=";
        //系統商的設定到這邊
        //發簡訊時間預約到早上的8點
        string timestring = DateTime.Now.ToString("yyyyMMdd") + "080000";
        string args = vbText + "&dlvtime=" + timestring;
        args = System.Web.HttpUtility.UrlEncode(args, System.Text.Encoding.GetEncoding("big5"));

        try
        {
            System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url + args);
            myRequest.Method = "GET";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            System.Net.WebResponse myResponse = myRequest.GetResponse();
            System.IO.StreamReader RecStream = new System.IO.StreamReader(myResponse.GetResponseStream());
            string ResponseHTML = RecStream.ReadToEnd();

            //return true;//成功
        }
        catch (Exception ex)
        {
            //return false;//寄失敗
            string aa = vbPhoneNo;
            string bb = vbText;
        }

    }
    public string Decrypt(string Input)
    {
        byte[] key = { 0x21, 0x43, 0x65, 0x87, 0x09, 0xAB, 0xDC, 0xFE };
        byte[] IV = { 0x34, 0x56, 0x78, 0x90, 0xBA, 0xCD, 0x12, 0xEF };

        if (!string.IsNullOrEmpty(Input))
        {
            Input = Input.Replace(" ", "+");
            Byte[] inputByteArray = new Byte[Input.Length];
            try
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(Input);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        else
        {
            return "";
        }
    }
}