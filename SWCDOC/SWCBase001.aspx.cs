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
    string UserUpLoadFilePath = "..\\UpLoadFiles\\SwcUser\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rETID = Request.QueryString["ETID"] + "";

        string SessETID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();

        if (!IsPostBack)
        {
            if (rETID == "" && SessETID =="")
            {
                Response.Redirect("SWC001.aspx");
            }
            if (rETID == "NEW")
            {
                JSID.Text = rETID;
                Image1.ImageUrl = "../images/title/title-apply.png";
                GetUserData("NEW");
                TitleLink01.Visible = false;
            }
            else
            {
                if (SessETID == "")
                {
                    Response.Redirect("SWC001.aspx");
                }
                else
                {
                    Image1.ImageUrl = "../images/title/title-account.png";
                    JSID.Text = SessETID;
                    TXTACTION.Text = SessETID;
                    GetUserData(SessETID);
                    TitleLink01.Visible = true;
                    CHGPW.Visible=true;
                }
            }
        }
        GoTslm.Visible = false;
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
    private void GetUserData(string v)
    {
        GBClass001 SBApp = new GBClass001();

        AddNewAcc.Visible = false;
        SaveAccount.Visible = false;
        TextUserName.Visible = false; ;
        OLDPWAREA.Visible = false;

        switch (v)
        {
            case "NEW":
                TXTSYSID.Text = GetSYSID();
                AddNewAcc.Visible = true;
                LBPWNew1.Text = "密碼";
                LBPWNew2.Text = "密碼確認";
                break;

            default:
                TextUserName.Visible = true;
                OLDPWAREA.Visible = true;
                SaveAccount.Visible = true;
                break;
        }

        if (v == "NEW")
        {
        }
        else
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();

                string strSQLRV = " select * from ETUsers ";
                strSQLRV = strSQLRV + " where ETID = '" + v + "' ";

                SqlDataReader readeUser;
                SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
                readeUser = objCmdUser.ExecuteReader();

                while (readeUser.Read())
                {
                    string tSYSID = readeUser["SYSID"] + "";
                    string tETID = readeUser["ETID"] + "";
                    string tETPW = readeUser["ETPW"] + "";
                    string tETName = readeUser["ETName"] + "";
                    string tETTCNo01 = readeUser["ETTCNo01"] + "";
                    string tETTCNo02 = readeUser["ETTCNo02"] + "";
                    string tETTCNo03 = readeUser["ETTCNo03"] + "";
                    string tETTCNo04 = readeUser["ETTCNo04"] + "";
                    string tETTel = readeUser["ETTel"] + "";
                    string tETEmail = readeUser["ETEmail"] + "";
                    string tETOrgName = readeUser["ETOrgName"] + "";
                    string tETOrgGUINo = readeUser["ETOrgGUINo"] + "";
                    string tETOrgIssNo = readeUser["ETOrgIssNo"] + "";
                    string tETOrgAddr = readeUser["ETOrgAddr"] + "";
                    string tETOrgTel = readeUser["ETOrgTel"] + "";
                    string tETCOPC = readeUser["ETCOPC"] + "";
                    string tTCNo01ED = readeUser["TCNo01ED"] + "";
                    string tTCNo02ED = readeUser["TCNo02ED"] + "";
                    string tTCNo03ED = readeUser["TCNo03ED"] + "";
                    string tTCNo04ED = readeUser["TCNo04ED"] + "";
                    string tETCOPCExp = readeUser["ETCOPCExp"] + "";

                    TXTSYSID.Text = tSYSID;
                    TXTETIDNo.Text = tETID;
                    LBETIDNo.Text = tETID;
                    TXTETName.Text = tETName;
                    TXTETTCNo01.Text = tETTCNo01;
                    TXTETTCNo02.Text = tETTCNo02;
                    TXTETTCNo03.Text = tETTCNo03;
                    TXTETTCNo04.Text = tETTCNo04;
                    TXTETTel.Text = tETTel;
                    TXTETEmail.Text = tETEmail;
                    TXTETOrgName.Text = tETOrgName;
                    TXTETOrgGUINo.Text = tETOrgGUINo;
                    TXTETOrgIssNo.Text = tETOrgIssNo;
                    TXTETOrgAddr.Text = tETOrgAddr;
                    TXTETOrgTel.Text = tETOrgTel;
                    TXTETCOPC.Text = tETCOPC;

                    TXTTCNo01ED.Text = SBApp.DateView(tTCNo01ED, "00");
                    TXTTCNo02ED.Text = SBApp.DateView(tTCNo02ED, "00");
                    TXTTCNo03ED.Text = SBApp.DateView(tTCNo03ED, "00");
                    TXTTCNo04ED.Text = SBApp.DateView(tTCNo04ED, "00");
                    TXTETCOPCExp.Text = SBApp.DateView(tETCOPCExp, "00");

                    TXTETIDNo.Visible = false;
                    LBETIDNo.Visible = true;

                    //string tETIDNo = readeUser["ETIDNo"] + ";









                    //string tETStatus = readeUser["ETStatus"] + ";

                    //string tApproved = readeUser["Approved"] + ";

                    //string tApprovedDate = readeUser["ApprovedDate"] + ";

                    //string tSaveuser = readeUser["Saveuser"] + ";

                    //string tsavedate = readeUser["savedate"] + ";


                    //string tstatus = readeUser["status"] + ";



                    //圖片類處理
                    string[] arrayFileName = new string[] { tETTCNo01, tETTCNo02, tETTCNo03, tETTCNo04 };
                    System.Web.UI.WebControls.Image[] arrayImgAppobj = new System.Web.UI.WebControls.Image[] { TXTETTCNo01_img, TXTETTCNo02_img, TXTETTCNo03_img, TXTETTCNo04_img };

                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        string strFileName = arrayFileName[i];
                        System.Web.UI.WebControls.Image ImgFileObj = arrayImgAppobj[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = UserUpLoadFilePath + v + "/" + strFileName;
                            ImgFileObj.Attributes.Add("src", tempImgPateh + "?ts=" + DateTime.Now.Millisecond);
                        }
                    }
                }

                readeUser.Close();
                objCmdUser.Dispose();

            }
        }
    }

    private string GetSYSID()
    {
        DateTime oTime = DateTime.Now;
        string strTime = string.Format("{0:yyyyMMddHHmmss}{1:0000}", oTime, oTime.Millisecond);

        return strTime;
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWC000.aspx");
    }

    protected void SaveAccount_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";

        string SSPW = Session["PW"]+"";

        string SaveDate = "Y";
        string NewAccount = TXTETIDNo.Text + "";
        string gETID1 = TXTETPW.Text + "";
        string gETID2 = TXTETPWChk.Text + "";
        string gETName = TXTETName.Text + "";
        string gETTel = TXTETTel.Text + "";
        string gETEmail = TXTETEmail.Text + "";
        string gETOrgName = TXTETOrgName.Text + "";
        string gETOrgGUINo = TXTETOrgGUINo.Text + "";
        string gETOrgAddr = TXTETOrgAddr.Text + "";
        string gETOrgTel = TXTETOrgTel.Text + "";
        string gETCOPC = TXTETCOPC.Text + "";
        string gETTCNo01 = TXTETTCNo01.Text + "";
        string gETTCNo02 = TXTETTCNo02.Text + "";
        string gETTCNo03 = TXTETTCNo03.Text + "";
        string gETTCNo04 = TXTETTCNo04.Text + "";
        string gETOrgIssNo = TXTETOrgIssNo.Text + "";
        string gSYSID = TXTSYSID.Text + "";
        string gOldPw = TXTETPWOLD.Text + "";
        string gTCNo01ED = TXTTCNo01ED.Text + "";
        string gTCNo02ED = TXTTCNo02ED.Text + "";
        string gTCNo03ED = TXTTCNo03ED.Text + "";
        string gTCNo04ED = TXTTCNo04ED.Text + "";
        string gETCOPCExp = TXTETCOPCExp.Text + "";

        GBClass001 SBApp = new GBClass001();

        if (gOldPw == "" && (gETID1 != "" || gETID2 !=""))
        {
            SaveDate = "N";
            error_msg.Text = SBApp.AlertMsg("請輸入舊密碼，謝謝!!");
            TXTETIDNo.Focus();
            return;

        }
            if (gOldPw != "" && (SSPW != gOldPw))
            {
                SaveDate = "N";
                error_msg.Text = SBApp.AlertMsg("密碼不正確請重新輸入，謝謝!!");
                TXTETIDNo.Focus();
                return;
            }

        if (gETID1 != gETID2)
        {
            SaveDate = "N";
            error_msg.Text = SBApp.AlertMsg("密碼與確認密碼不符");
            TXTETPW.Focus();
            return;
        }

        if(SaveDate == "Y")
        {
            string UserSqlStr = "";

            UserSqlStr = UserSqlStr + " Update ETUsers Set ";
            if (gETID1 !="")
            {
                UserSqlStr = UserSqlStr + " ETPW ='" + gETID1 + "', ";
                Session["PW"] = gETID1;
            }
            UserSqlStr = UserSqlStr + " ETName =N'" + gETName + "', ";
            UserSqlStr = UserSqlStr + " ETTel =N'" + gETTel + "', ";
            UserSqlStr = UserSqlStr + " ETEmail =N'" + gETEmail + "', ";
            UserSqlStr = UserSqlStr + " ETOrgName =N'" + gETOrgName + "', ";
            UserSqlStr = UserSqlStr + " ETOrgGUINo =N'" + gETOrgGUINo + "', ";
            UserSqlStr = UserSqlStr + " ETOrgAddr =N'" + gETOrgAddr + "', ";
            UserSqlStr = UserSqlStr + " ETOrgTel =N'" + gETOrgTel + "', ";
            UserSqlStr = UserSqlStr + " ETCOPC =N'" + gETCOPC + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo01 =N'" + gETTCNo01 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo02 =N'" + gETTCNo02 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo03 =N'" + gETTCNo03 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo04 =N'" + gETTCNo04 + "', ";
            UserSqlStr = UserSqlStr + " ETOrgIssNo =N'" + gETOrgIssNo + "', ";
            UserSqlStr = UserSqlStr + " TCNo01ED =N'" + gTCNo01ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo02ED =N'" + gTCNo02ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo03ED =N'" + gTCNo03ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo04ED =N'" + gTCNo04ED + "', ";
            UserSqlStr = UserSqlStr + " ETCOPCExp =N'" + gETCOPCExp + "', ";
            UserSqlStr = UserSqlStr + " SYSID =N'" + gSYSID + "', ";
            UserSqlStr = UserSqlStr + " saveuser = N'" + NewAccount + "', ";
            UserSqlStr = UserSqlStr + " savedate = getdate() ";
            UserSqlStr = UserSqlStr + " Where ETIDNo = '" + NewAccount + "'";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                SqlCommand objCmdUser = new SqlCommand(UserSqlStr, SWCConn);
                objCmdUser.ExecuteNonQuery();
                objCmdUser.Dispose();

            }

            //上傳檔案…
            UpLoadTempFileMoveChk(NewAccount);

            GetUserData(NewAccount);
            
            Response.Write("<script>alert('資料已存檔'); location.href='SWC001.aspx'; </script>");
            
        }
    }

    protected void AddNewAcc_Click(object sender, EventArgs e)
    {
        string SaveDate = "Y";
        string NewAccount = TXTETIDNo.Text + "";
        string gETID1 = TXTETPW.Text + "";
        string gETID2 = TXTETPWChk.Text + "";
        string gETName = TXTETName.Text + "";
        string gETTel = TXTETTel.Text + "";
        string gETEmail = TXTETEmail.Text + "";
        string gETOrgName = TXTETOrgName.Text + "";
        string gETOrgGUINo = TXTETOrgGUINo.Text = "";
        string gETOrgAddr = TXTETOrgAddr.Text + "";
        string gETOrgTel = TXTETOrgTel.Text + "";
        string gETCOPC = TXTETCOPC.Text + "";
        string gETTCNo01 = TXTETTCNo01.Text + "";
        string gETTCNo02 = TXTETTCNo02.Text + "";
        string gETTCNo03 = TXTETTCNo03.Text + "";
        string gETTCNo04 = TXTETTCNo04.Text + "";
        string gETOrgIssNo = TXTETOrgIssNo.Text + "";
        string gSYSID = TXTSYSID.Text + "";
        string gTCNo01ED = TXTTCNo01ED.Text + "";
        string gTCNo02ED = TXTTCNo02ED.Text + "";
        string gTCNo03ED = TXTTCNo03ED.Text + "";
        string gTCNo04ED = TXTTCNo04ED.Text + "";
        string gETCOPCExp = TXTETCOPCExp.Text + "";
        

        GBClass001 SBApp = new GBClass001();

        NewAccount = NewAccount.ToUpper();

        if (NewAccount == "")
        {
            error_msg.Text = SBApp.AlertMsg("身分證字號務必填登，謝謝!!");
            TXTETIDNo.Focus();
            return;
        }
        if (gETID1 != gETID2)
        {
            error_msg.Text = SBApp.AlertMsg("密碼與確認密碼不符");
            TXTETPW.Focus();
            return;
        }

        //帳號重覆檢查
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            string strSQLUS = " select ETIDNo from ETUsers ";
            strSQLUS = strSQLUS + " where ETIDNo ='" + NewAccount + "' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLUS, SWCConn);
            readerUser = objCmdUser.ExecuteReader();

            if (readerUser.HasRows)
            {
                Response.Write("<script>alert('您好，此帳號已重複申請，請再次確認密碼，或與大地工程處聯繫，謝謝。'); location.href='SWC000.aspx'; </script>");
                TXTETIDNo.Focus();
                SaveDate = "N";
                return;
            }
        }

        string UserSqlStr = "";
        if (SaveDate == "Y") {
            UserSqlStr = UserSqlStr + " INSERT INTO ETUsers (ETID,ETIDNo,ETStatus,status) VALUES ('" + NewAccount + "','"+ NewAccount + "','0','申請中') ;";

            UserSqlStr = UserSqlStr + " Update ETUsers Set ";

            UserSqlStr = UserSqlStr + " ETPW =N'" + gETID1 + "', ";
            UserSqlStr = UserSqlStr + " ETName =N'" + gETName + "', ";
            UserSqlStr = UserSqlStr + " ETTel =N'" + gETTel + "', ";
            UserSqlStr = UserSqlStr + " ETEmail =N'" + gETEmail + "', ";
            UserSqlStr = UserSqlStr + " ETOrgName =N'" + gETOrgName + "', ";
            UserSqlStr = UserSqlStr + " ETOrgGUINo =N'" + gETOrgGUINo + "', ";
            UserSqlStr = UserSqlStr + " ETOrgAddr =N'" + gETOrgAddr + "', ";
            UserSqlStr = UserSqlStr + " ETOrgTel =N'" + gETOrgTel + "', ";
            UserSqlStr = UserSqlStr + " ETCOPC =N'" + gETCOPC + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo01 =N'" + gETTCNo01 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo02 =N'" + gETTCNo02 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo03 =N'" + gETTCNo03 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo04 =N'" + gETTCNo04 + "', "; 
            UserSqlStr = UserSqlStr + " ETOrgIssNo =N'" + gETOrgIssNo + "', ";
            UserSqlStr = UserSqlStr + " TCNo01ED =N'" + gTCNo01ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo02ED =N'" + gTCNo02ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo03ED =N'" + gTCNo03ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo04ED =N'" + gTCNo04ED + "', ";
            UserSqlStr = UserSqlStr + " ETCOPCExp =N'" + gETCOPCExp + "', ";            
            UserSqlStr = UserSqlStr + " SYSID ='" + gSYSID + "', ";
            UserSqlStr = UserSqlStr + " saveuser = '" + NewAccount + "', ";
            UserSqlStr = UserSqlStr + " savedate = getdate() ";
            UserSqlStr = UserSqlStr + " Where ETIDNo = '" + NewAccount + "'";

            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                SqlCommand objCmdUser = new SqlCommand(UserSqlStr, SWCConn);
                objCmdUser.ExecuteNonQuery();
                objCmdUser.Dispose();
                
                GBClass001 CL01 = new GBClass001();

                CL01.Mail_Send(GetMailTo(), MailSub(), MailBody());

            }

            //上傳檔案…
            UpLoadTempFileMoveChk(NewAccount);
        }
        GetUserData(NewAccount);
        
        Response.Write("<script>alert('已送出帳號申請，請等待審核通知，申請結果將以E-mail通知。'); location.href='SWC000.aspx'; </script>");

    }
    private string MailSub()
    {
        string rValue = "「臺北市水土保持書件管理平台」有新帳號申請";
        return rValue;
    }
    private string MailBody()
    {
        string mName = TXTETName.Text;
        string mName2 = TXTETOrgName.Text;
        string mTel = TXTETTel.Text;

        string rValue = "您好，「臺北市水土保持書件管理平台」有"+ mName + "的新帳號申請單，請系統管理員前往「坡地管理資料庫 - 帳號管理」進行審核。<br><br>";
        rValue = rValue + "申請人："+ mName+ "<br>";
        rValue = rValue + "執業機構名稱：" + mName2 + "<br>";
        rValue = rValue + "手機：" + mTel + "<br><br>";
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
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTETTCNo01", "TXTETTCNo02", "TXTETTCNo03", "TXTETTCNo04" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTETTCNo01, TXTETTCNo02, TXTETTCNo03, TXTETTCNo04 };
        string csUpLoadField = "TXTETTCNo01";
        TextBox csUpLoadAppoj = TXTETTCNo01;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string UserCaseFolderPath = ConfigurationManager.AppSettings["UserFilePath"];

        folderExists = Directory.Exists(UserCaseFolderPath);
        if (folderExists == false)
        {
            Directory.CreateDirectory(UserCaseFolderPath);
        }

        folderExists = Directory.Exists(UserCaseFolderPath + CaseId);
        if (folderExists == false)
        {
            Directory.CreateDirectory(UserCaseFolderPath + CaseId);
        }


        for (int i = 0; i < arryUpLoadField.Length; i++)
        {
            csUpLoadField = arryUpLoadField[i];
            csUpLoadAppoj = arryUpLoadAppoj[i];

            if (Session[csUpLoadField] + "" == "有檔案")
            {
                Boolean fileExists;
                string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;
                string SwcCaseFilePath = UserCaseFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;

                fileExists = File.Exists(TempFilePath);
                if (fileExists)
                {
                    if (File.Exists(SwcCaseFilePath))
                    {
                        File.Delete(SwcCaseFilePath);
                    }
                    File.Move(TempFilePath, SwcCaseFilePath);
                }

            }
        }

    }

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = TXTETIDNo.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名

            switch (ChkType)
            {
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".jpg", ".png", "doc", "pdf", "dwg", "dxf" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG DOC PDF DWG DXF 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 10MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 10000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10Mb 以下檔案上傳，謝謝!!");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            UpLoadText.Text = SwcFileName;

            // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
            // 使用 Path.Combine 來集合路徑的優點
            //  以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
            //  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
            //  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
            //  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合

            string serverFilePath = Path.Combine(serverDir, SwcFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(SwcFileName);
            int fileCount = 1;

            //while (File.Exists(serverFilePath))
            //{
            //    // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
            //    filename = string.Concat(fileNameOnly, "_", fileCount, extension);
            //    serverFilePath = Path.Combine(serverDir, filename);
            //    fileCount++;
            //}

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);
                //error_msg.Text = "檔案上傳成功";

                switch (ChkType)
                {
                    case "PIC":
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;

                }

            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }


        }
        else
        {
            Session[UpLoadStr] = "";
        }

    }
    protected void imagestitch(System.Web.UI.WebControls.Image UpLoadView, string sourcePath, int ShowWidth, int ShowHeight)
    {  //影像調整，處理照片顯示

        if (File.Exists(sourcePath))
        {
            System.Drawing.Image image = new Bitmap(sourcePath);

            int width = image.Width;
            int height = image.Height;

            int ShowUpPicWidth = 0;
            int ShowUpPicHeight = 0;

            if (width < height)
            {
                ShowUpPicWidth = Convert.ToInt32(width * ShowHeight / height);
                ShowUpPicHeight = ShowHeight;
            }
            else
            {
                ShowUpPicWidth = ShowWidth;
                ShowUpPicHeight = Convert.ToInt32(height * ShowWidth / width);
            }
            UpLoadView.Width = ShowUpPicWidth;
            UpLoadView.Height = ShowUpPicHeight;

            image.Dispose();
        }
    }
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = TXTSYSID.Text + "";
        string csCaseID2 = TXTETIDNo.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update ETUsers set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SYSID = '" + csCaseID + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

        string DelFileName = ImgText.Text;
        string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        string FileFullPath = SwcCaseFolderPath + csCaseID2 + "\\" + ImgText.Text;

        try
        {
            if (File.Exists(TempFileFullPath))
            {
                File.Delete(TempFileFullPath);
            }
        }
        catch
        {
        }
        try
        {
            if (File.Exists(FileFullPath))
            {
                File.Delete(FileFullPath);
            }
        }
        catch
        {
        }

        switch (DelType)
        {
            case "PIC":
                ImgView.Attributes.Clear();
                ImgView.ImageUrl = "";
                ImgView.Width = NoneWidth;
                ImgView.Height = NoneHeight;
                break;
            case "DOC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                FileLink.Visible = false;
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }
    protected void TXTETTCNo01_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC", TXTETTCNo01_FileUpload, TXTETTCNo01, "TXTETTCNo01", "_" + rID + "_photo1", TXTETTCNo01_img, null);
    }

    protected void TXTETTCNo01_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo01, TXTETTCNo01_img, null, "TCNo01ED", "TXTETTCNo01", 0, 0);
    }

    protected void TXTETTCNo02_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTETTCNo02_FileUpload, TXTETTCNo02, "TXTETTCNo02", "_" + rID + "_photo2", TXTETTCNo02_img, null);
    }
    protected void TXTETTCNo02_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo02, TXTETTCNo02_img, null, "TCNo02ED", "TXTETTCNo02", 0, 0);

    }

    protected void TXTETTCNo03_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTETTCNo03_FileUpload, TXTETTCNo03, "TXTETTCNo03", "_" + rID + "_photo3", TXTETTCNo03_img, null);

    }
    protected void TXTETTCNo03_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo03, TXTETTCNo03_img, null, "TCNo03ED", "TXTETTCNo03", 0, 0);
    }
    protected void TXTETTCNo04_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC", TXTETTCNo04_FileUpload, TXTETTCNo04, "TXTETTCNo04", "_" + rID + "_photo4", TXTETTCNo04_img, null);

    }

    protected void TXTETTCNo04_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo04, TXTETTCNo04_img, null, "TCNo04ED", "TXTETTCNo04", 0, 0);

    }


}