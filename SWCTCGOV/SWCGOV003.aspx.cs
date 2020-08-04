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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCBase001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rDETID = Request.QueryString["DisEventId"] + "";

        string SessETID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        if (ssUserType == "03")
        {
            if (!IsPostBack) {  GetDisasterEventData(rDETID); }
        } else
        {
            Response.Redirect("../SWCDOC/SWC001.aspx");
        }




        //以下全區公用
        SBApp.ViewRecord("帳號管理", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }

    private void GetDisasterEventData(string rCaseID)
    {
        if (rCaseID == "ADDNEW")
        {
            string sDEID = GetNewId();
            TXTDENO.Text = sDEID;
        } else
        {
            ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
            {
                SwcConn.Open();

                string gDNSQLStr = " select * from DisasterEvent ";
                gDNSQLStr = gDNSQLStr + " where DENo ='" + rCaseID + "' ";

                SqlDataReader readerDN;
                SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                { 
                    string sDENo = readerDN["DENo"] + "";
                    string sDENAME = readerDN["DENAME"] + "";
                    string sDEDATE = readerDN["DEDATE"] + "";
                    string sDENTTEXT = readerDN["DENTTEXT"] + "";
                    string sDETYPE = readerDN["DETYPE"] + "";
                    string sSWCSTATUS = readerDN["SWCSTATUS"] + "";
                    string sSENDMBR = readerDN["SENDMBR"] + "";
                    string sSENDFUN = readerDN["SENDFUN"] + "";

                    string tDETYPEDESC = "";
                    switch (sDETYPE) {
                        case "0104":
                            tDETYPEDESC = "颱風豪雨通知回傳自主檢查表";
                            break;
                    }

                    TXTDENO.Text = sDENo;
                    TXTDENAME.Text = sDENAME;
                    TXTDENTTEXT.Text = sDENTTEXT;
                    DDLDETYPE.Text = tDETYPEDESC;
                    CHKSWCSTATUS.Text = sSWCSTATUS.Replace(","," ");
                    CHKSENDMBR.Text = sSENDMBR.Replace(",", " ");
                    CHKSENDFUN.Text = sSENDFUN.Replace(",", " ");
                }
                readerDN.Close();
                objCmdDN.Dispose();

            }
        }
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string gDENO = TXTDENO.Text + "";
        string gDENAME = TXTDENAME.Text + "";
        //string gDEDATE = TXTDEDATE.Text + "";
        string gDENTTEXT = TXTDENTTEXT.Text + "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select DENO from DisasterEvent ";
            gDNSQLStr = gDNSQLStr + " where DENo ='" + gDENO + "' ";

            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();

            if (!readerDN.HasRows)
            {
                sEXESQLUPD = " INSERT INTO DisasterEvent (DENo) VALUES ('" + gDENO + "');";
            }
            readerDN.Close();
            objCmdDN.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update DisasterEvent Set ";
            sEXESQLUPD = sEXESQLUPD + " DENAME = '" + gDENAME + "', ";
            //sEXESQLUPD = sEXESQLUPD + " DEDATE = '" + gDEDATE + "', ";
            sEXESQLUPD = sEXESQLUPD + " DENTTEXT = '" + gDENTTEXT + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " where DENo = '" + gDENO + "' ";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWCGOV002.aspx?DisEventId=" + gDENO + "';</script>");
                    break;
            }

        }
    }

    private string GetNewId()
    {
        string SearchValA = "DE" + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1911).ToString() + DateTime.Now.ToString("MM");
        string MaxKeyVal = "0001";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRVa = " select MAX(DENo) as MAXID from DisasterEvent ";
            strSQLRVa = strSQLRVa + " where LEFT(DENo,7) ='" + SearchValA + "' ";

            SqlDataReader readerSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRVa, SwcConn);
            readerSwc = objCmdSwc.ExecuteReader();

            if (readerSwc.HasRows)
            {
                while (readerSwc.Read())
                {
                    string tempMaxKeyVal = readerSwc["MAXID"] + "";

                    if (tempMaxKeyVal != "") {
                        string tempValue = (Convert.ToInt32(tempMaxKeyVal.Substring(tempMaxKeyVal.Length - 4, 4)) + 1).ToString();
                        MaxKeyVal = tempValue.PadLeft(4, '0');
                    }
                }
            }
            objCmdSwc.Dispose();
            readerSwc.Close();
            SwcConn.Close();
        }

        return SearchValA+ MaxKeyVal;
    }














    

    private string GetSYSID()
    {
        DateTime oTime = DateTime.Now;
        string strTime = string.Format("{0:yyyyMMddHHmmss}{1:0000}", oTime, oTime.Millisecond);

        return strTime;
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWCGOV001.aspx");
    }

    //protected void SaveAccount_Click(object sender, EventArgs e)
    //{
    //    error_msg.Text = "";
        
    //    string SaveDate = "Y";
    //    string gETID1 = TXTETPW.Text + "";
    //    string gETID2 = TXTETPWChk.Text + "";
    //    string gETTel = TXTETTel.Text + "";
    //    string gETEmail = TXTDENTTEXT.Text + "";

    //    GBClass001 SBApp = new GBClass001();
        

    //    if (gETID1 != gETID2)
    //    {
    //        SaveDate = "N";
    //        error_msg.Text = SBApp.AlertMsg("密碼與確認密碼不符");
    //        TXTETPW.Focus();
    //        return;
    //    }

    //    if(SaveDate == "Y")
    //    {
    //        string UserSqlStr = "";

    //        UserSqlStr = UserSqlStr + " Update ETUsers Set ";
    //        if (gETID1 !="")
    //        {
    //            UserSqlStr = UserSqlStr + " ETPW ='" + gETID1 + "', ";
    //            Session["PW"] = gETID1;
    //        }
    //        UserSqlStr = UserSqlStr + " ETTel =N'" + gETTel + "', ";
    //        UserSqlStr = UserSqlStr + " ETEmail =N'" + gETEmail + "', ";
    //        UserSqlStr = UserSqlStr + " saveuser = N'" + NewAccount + "', ";
    //        UserSqlStr = UserSqlStr + " savedate = getdate() ";
    //        UserSqlStr = UserSqlStr + " Where ETIDNo = '" + NewAccount + "'";

    //        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
    //        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
    //        {
    //            SWCConn.Open();

    //            SqlCommand objCmdUser = new SqlCommand(UserSqlStr, SWCConn);
    //            objCmdUser.ExecuteNonQuery();
    //            objCmdUser.Dispose();

    //        }
            
    //        GetUserData(NewAccount);
            
    //        Response.Write("<script>alert('資料已存檔'); location.href='SWC001.aspx'; </script>");
            
    //    }
    //}

    //protected void AddNewAcc_Click(object sender, EventArgs e)
    //{
    //    string SaveDate = "Y";
    //    string NewAccount = TXTETIDNo.Text + "";
    //    string gETID1 = TXTETPW.Text + "";
    //    string gETID2 = TXTETPWChk.Text + "";
    //    string gETTel = TXTETTel.Text + "";
    //    string gETEmail = TXTDENTTEXT.Text + "";
        

    //    GBClass001 SBApp = new GBClass001();

    //    NewAccount = NewAccount.ToUpper();

    //    if (NewAccount == "")
    //    {
    //        error_msg.Text = SBApp.AlertMsg("身分證字號務必填登，謝謝!!");
    //        TXTETIDNo.Focus();
    //        return;
    //    }
    //    if (gETID1 != gETID2)
    //    {
    //        error_msg.Text = SBApp.AlertMsg("密碼與確認密碼不符");
    //        TXTETPW.Focus();
    //        return;
    //    }

    //    //帳號重覆檢查
    //    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
    //    using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
    //    {
    //        SWCConn.Open();

    //        string strSQLUS = " select ETIDNo from ETUsers ";
    //        strSQLUS = strSQLUS + " where ETIDNo ='" + NewAccount + "' ";

    //        SqlDataReader readerUser;
    //        SqlCommand objCmdUser = new SqlCommand(strSQLUS, SWCConn);
    //        readerUser = objCmdUser.ExecuteReader();

    //        if (readerUser.HasRows)
    //        {
    //            Response.Write("<script>alert('您好，此帳號已重複申請，請再次確認密碼，或與大地工程處聯繫，謝謝。'); location.href='SWC000.aspx'; </script>");
    //            TXTETIDNo.Focus();
    //            SaveDate = "N";
    //            return;
    //        }
    //    }

    //    string UserSqlStr = "";
    //    if (SaveDate == "Y") {
    //        UserSqlStr = UserSqlStr + " INSERT INTO ETUsers (ETID,ETIDNo,ETStatus,status) VALUES ('" + NewAccount + "','"+ NewAccount + "','0','申請中') ;";

    //        UserSqlStr = UserSqlStr + " Update ETUsers Set ";

    //        UserSqlStr = UserSqlStr + " ETPW =N'" + gETID1 + "', ";
    //        UserSqlStr = UserSqlStr + " ETTel =N'" + gETTel + "', ";
    //        UserSqlStr = UserSqlStr + " ETEmail =N'" + gETEmail + "', ";
    //        UserSqlStr = UserSqlStr + " saveuser = '" + NewAccount + "', ";
    //        UserSqlStr = UserSqlStr + " savedate = getdate() ";
    //        UserSqlStr = UserSqlStr + " Where ETIDNo = '" + NewAccount + "'";

    //        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
    //        {
    //            SWCConn.Open();

    //            SqlCommand objCmdUser = new SqlCommand(UserSqlStr, SWCConn);
    //            objCmdUser.ExecuteNonQuery();
    //            objCmdUser.Dispose();
                
    //            GBClass001 CL01 = new GBClass001();

    //            CL01.Mail_Send(GetMailTo(), MailSub(), MailBody());

    //        }
            
    //    }
    //    GetUserData(NewAccount);
        
    //    Response.Write("<script>alert('已送出帳號申請，請等待審核通知，申請結果將以E-mail通知。'); location.href='SWC000.aspx'; </script>");

    //}
    private string MailSub()
    {
        string rValue = "「臺北市水土保持書件管理平台」請填登颱風豪雨設施自主檢查表";
        return rValue;
    }
    private string MailBody()
    {
        string mMailText = TXTDENTTEXT.Text;

        string rValue = "您好，請您至「臺北市水土保持書件管理平台」系統填登「颱風豪雨設施自主檢查表」。<br><br>";
        rValue = rValue + mMailText + "<br><br>";

        rValue = rValue + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        rValue = rValue + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
        
        return rValue;
    }
    private string[] GetMailTo()
    {
        string MailStr = "claire@geovector.com.tw;;";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            string strSQLRV = " select * from geouser ";
            strSQLRV = strSQLRV + " where (department = '審查管理科' and mbgroup02 = '系統管理員') ";
            strSQLRV = strSQLRV + "    or (department = '審查管理科' and jobtitle = '股長') ";

            SqlDataReader readeUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
            readeUser = objCmdUser.ExecuteReader();

            string mbMail = "";
            while (readeUser.Read())
            {
                mbMail = readeUser["email"] + "";
                MailStr = MailStr + mbMail + ";;";
            }
        }

        string[] arrayMailMb = MailStr.Split(new string[] { ";;" }, StringSplitOptions.None);
        return arrayMailMb;
    }

}