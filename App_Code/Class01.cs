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
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Class01 的摘要描述
/// </summary>
public class Class01: System.Web.UI.Page
{
    public Class01()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public string SQLstrValue(string cSQLstr)
    {
        //變數，過濾單引

        string okSQLstr = cSQLstr.Replace("'", "''");

        return okSQLstr;
    }
    public string AlertMsg(string ShowMsg)
    {
        string MsgStr = "";

        MsgStr = "<script>alert('" + ShowMsg + "');</script>";

        return MsgStr;

        //Response.Write("<script> alert('" + ShowMsg + "') </script>");
    }
    public Boolean ChkUserAuth(string cPage)
    {
        string SS_ID = Session["ID"] + "";
        string SS_PW = Session["PW"] + "";
        
        string NormalAuth = "Y";

        if (SS_ID == "")
        {
            NormalAuth = "N";
            //Response.Redirect("../Login.aspx");
        }
        

        
        //'Session("name") = "系統管理員"
        //'Session("grade") = ""
        //'Session("uid") = "gv-admin"
        //'Session("right") = "系統管理員"

        

        if (NormalAuth == "Y") { return true; } else { return false; } 









        


        //if (NormalAuth == "Y")
        //{
        //    if (!CheckPageAuth(cPage, SS_ID, SS_PW))
        //    {
        //        NormalAuth = "N";
        //    }
        //}

        

    }
    private Boolean CheckPageAuth(string cPage, string UserID, string UserPW)
    {
        Boolean UserAuth = false;
        string UserMbGroup = "", UserDataView = "", UserDataEdit = "";
        string strSQLRV = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            strSQLRV = "select * from geouser where userid='" + UserID + "' and passwd = '" + UserPW + "' and status <>'停用' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
            readerUser = objCmdUSER.ExecuteReader();

            while (readerUser.Read())
            {
                UserMbGroup = readerUser["mbgroup"] + "";
                UserDataView = ";" + readerUser["tcgedataview"] + "";
                UserDataEdit = ";" + readerUser["tcgedataedit"] + "";

            }
            readerUser.Close();
            objCmdUSER.Dispose();

            switch (cPage)
            {
                case "UserEditAuth":
                    if (UserMbGroup == "系統管理員")
                    {
                        UserAuth = true;
                    }
                    break;

                default:
                    if (UserDataView.IndexOf(";" + cPage + ";", StringComparison.OrdinalIgnoreCase) >= 0) { UserAuth = true; }
                    if (UserDataEdit.IndexOf(";" + cPage + ";", StringComparison.OrdinalIgnoreCase) >= 0) { UserAuth = true; }
                    break;
            }





        }

















        return UserAuth;
    }

    public string DateView(string gValue, string oType)
    {
        //00:2017-12-12

        string rValue = "";

        try
        {
            switch (oType)
            {
                case "00":
                    rValue = Convert.ToDateTime(gValue).ToString("yyyy-MM-dd");
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
    public void LoginNotes(string UserID, string LoginStatus)
    {
        string clientIP = GetClientIP();
        string strSQLErr = "";

        LoginStatus = LoginStatus + "";
        if (LoginStatus != "0") { LoginStatus = "1"; }

        //從資料庫取得資料
        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        //strSQLErr = " update geouser set loginstaus = '" + LoginStatus + "' where userid='" + UserID + "';";
        strSQLErr = strSQLErr + " insert into loginnotes (userid,success,logtime,loginip) values ";
        strSQLErr = strSQLErr + " ('" + UserID + "','" + LoginStatus + "',getdate(),'" + clientIP + "');";

        SqlCommand objCmdRV = new SqlCommand(strSQLErr, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

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
                string tmpType = readerSwcM["GVTypeDesc"] + "";
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
}