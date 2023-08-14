using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Linq;

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
        string SQL_PW = "", SQL_Unit = "", SQL_NAME = "", SQL_department = "", SQL_AuthMwall = "", SQL_Status = "", SQL_DataView = "", SQL_DateEdit = "", SQL_jobtitle = "", SQL_jobTitle="", SQL_EDIT="", SQL_GuildSubstitute="", SQL_GuildSubstitute2 = "", SQL_GuildTcgeChk="", SQL_GuildTcgeChk2 = "", SQL_Guild01="", SQL_Guild02="", SQL_ServiceSubstitute="";

        Session["UserType"] = "";
        Session["ID"] = "";
        Session["PW"] = "";
        Session["NAME"] = "";
        Session["Unit"] = "";
        Session["JobTitle"] = "";
        Session["Edit4"] = "";
        Session["WMGuild"] = "";
        Session["Guild01"] = "";
        Session["Guild02"] = "";
        Session["ETU_Guild01"] = "";
        Session["ETU_Guild02"] = "";
        Session["ETU_Guild03"] = "";

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
                        SQL_GuildSubstitute = readerUser["GuildSubstitute"] + "";
                        SQL_GuildSubstitute2 = readerUser["GuildSubstitute2"] + "";
                        SQL_GuildTcgeChk = readerUser["GuildTcgeChk"] + "";
                        SQL_GuildTcgeChk2 = readerUser["GuildTcgeChk2"] + "";
                        SQL_jobTitle = readerUser["ETIdentity"] + "";
                        SQL_ServiceSubstitute= readerUser["ServiceSubstitute"] + "";
                    }
                    if (lPw == SQL_PW)
                    {
                        Session["UserType"] = lType;
                        Session["ID"] = lID;
                        Session["PW"] = SQL_PW;
                        Session["NAME"] = SQL_NAME;
                        Session["Unit"] = SQL_Unit;
                        Session["JobTitle"] = SQL_jobTitle;

                        //ge-50702	水土保持服務團
                        if (SQL_GuildTcgeChk  == "1") Session["ETU_Guild01"] = SQL_GuildSubstitute;
                        if (SQL_GuildTcgeChk2 == "1") Session["ETU_Guild02"] = SQL_GuildSubstitute2;
                        if (SQL_ServiceSubstitute == "Y") Session["ETU_Guild03"] = "ge-50702";

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
                        //strSQLRV = strSQLRV + "   and unit ='工務局大地工程處' ";
                        strSQLRV = strSQLRV + "   and (tcgedataview like '%書件水土保持案件;%' or TcgeDataedit like '%書件水土保持案件;%') ";
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
                        SQL_Guild01 = readerUser["Guild01"] + "";
                        SQL_Guild02 = readerUser["Guild02"] + "";
                        //完工設施維護檢查表登錄
                    }

                    if (lPw == SQL_PW)
                    {
                        Session["UserType"] = lType;
                        Session["ID"] = lID;
                        Session["PW"] = SQL_PW;
                        Session["NAME"] = SQL_NAME;
                        Session["Unit"] = SQL_Unit;
                        Session["Guild01"] = SQL_Guild01;
                        Session["Guild02"] = SQL_Guild02;
                        if (SQL_EDIT.IndexOf("完工設施維護檢查表登錄;") >= 0) {
                            Session["Edit4"] = "Y";
                        }
                        if (SQL_EDIT.IndexOf("書件水土保持案件;") >= 0 || lID == "gv-admin" )
                        {
                            Session["ONLINEAPPLY"] = "Y";
                        }
                        Session["ETU_Guild01"] = lID;
                        Session["ETU_Guild02"] = lID;
                        Session["ETU_Guild03"] = lID;
                        _ReturnValue = true;

                    } else
                    {
                        Session["UserType"] = "";
                        Session["ID"] = "";
                        Session["PW"] = "";
                        Session["NAME"] = "";
                        Session["Unit"] = "";
                        Session["Edit4"] = "";
                        Session["Guild01"] = "";
                        Session["Guild02"] = "";
                        Session["ETU_Guild01"] = "";
                        Session["ETU_Guild02"] = "";
                        Session["ETU_Guild03"] = "";
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
        //05：106-12-12
		//06：HH(24)
		//07：mm
		//08：2017-12-12 HH:mm

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
                case "05":
                    System.Globalization.TaiwanCalendar tc5 = new System.Globalization.TaiwanCalendar();
                    DateTime d5;
                    if (DateTime.TryParse(gValue, out d5))
                    {
                        d5 = Convert.ToDateTime(gValue);
                        rValue = String.Format("{0}年{1:00}月{2:00}日", tc5.GetYear(d5), tc5.GetMonth(d5), tc5.GetDayOfMonth(d5));
                    }
                    break;
				case "06":
                    rValue = Convert.ToDateTime(gValue).ToString("HH");
                    break;
				case "07":
                    rValue = Convert.ToDateTime(gValue).ToString("mm");
                    break;
				case "08":
                    rValue = Convert.ToDateTime(gValue).ToString("yyyy-MM-dd HH:mm");
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
                string tETTel = readerUser["ETTel"] + "";
                string tETIdentity = readerUser["ETIdentity"] + "";
                

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
                    case "ETTel":
                        rValue = tETTel;
                        break;
                    case "ETIdentity":
                        rValue = tETIdentity;
                        break;


                }


            }

        }
        return rValue;
    }
	public string getFilePath(string tmpSwc000, string tmpSwc002, string tmpSwc007)
    {
        string rValue = ConfigurationManager.AppSettings["swcpspath"];
        string FileYearS = tmpSwc002.Substring(4, 3);
        rValue += Convert.ToInt32(FileYearS) > 93 ? Convert.ToInt32(FileYearS).ToString() + "年掃描圖檔" : "93年度暨以前掃描圖檔";
        rValue += tmpSwc007 == "簡易水保" ? "\\水保申請案件\\簡易水保\\" : "\\水保申請案件\\水保計畫\\";
        return rValue;
    }
	public string getFileUrl(string tmpSwc000, string tmpSwc002, string tmpSwc007, string fileType)
    {
        string rValue = ConfigurationManager.AppSettings["SwcFileUrl"];
        string FileYearS = tmpSwc002.Substring(4, 3);
        rValue += Convert.ToInt32(FileYearS) > 93 ? FileYearS + "年掃描圖檔" : "93年度暨以前掃描圖檔";
        rValue += tmpSwc007 == "簡易水保" ? "/水保申請案件/簡易水保/" : "/水保申請案件/水保計畫/";
        switch (fileType)
        {
            case "TXTSWC080":
                rValue += tmpSwc002 + "/掃描檔/掃描檔/";
                break;
            case "TXTSWC029":
                rValue += tmpSwc002 + "/審查/6-1/";
                break;
            case "TXTSWC029CAD":
                rValue += tmpSwc002 + "/審查/6-1-CAD/";
                break;
            case "TXTSWC030":
                rValue += tmpSwc002 + "/審查/7-1/";
                break;
            case "TXTSWC030CAD":
                rValue += tmpSwc002 + "/審查/7-1-CAD/";
                break;
            case "TXTSWC101":
                rValue += tmpSwc002 + "/竣工圖說/竣工圖說/";
                break;
            case "TXTSWC101CAD":
                rValue += tmpSwc002 + "/竣工圖說/竣工圖說CAD/";
                break;
            case "TXTSWC110":
                rValue += tmpSwc002 + "/審查單位查核表/審查單位查核表/";
                break;
            case "TXTSWC118":
                rValue += tmpSwc002 + "/公開核定本/公開核定本/";
                break;
            case "TXTSWC138":
                rValue += tmpSwc002 + "/環評報告書or免環評證明文件/環評報告書or免環評證明文件/";
                break;
            case "TXTSWC137":
                rValue += tmpSwc002 + "/掃描檔/核定不予核定函/";
                break;
        }
        return rValue;
    }
    #region.PDF浮水印
    public bool DLFileReMark(string Swc000, string FileName, string FileAttributes,string tmpSwc002,string tmpSwc007,string fileType)
    {
        GBClass001 SBApp = new GBClass001();

        bool rValue = false;
        string ssUserName = Session["NAME"] + "";
        string extension = Path.GetExtension(FileName).ToLowerInvariant();
        string wReMark01 = "臺北市政府工務局大地工程處";
        string wReMark02 = "本文件由" + ssUserName + "於" + SBApp.DateView(DateTime.Now.ToString(), "05") + "下載使用";
		string wReMark03 = "核定日期：" + "";
        string pathValue01 = "";

        switch (extension)
        {
            case ".pdf":
                #region.PDF浮水印
				
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
                    default:
                        break;
                }

                try
                {
                    //string ReadPath = Server.MapPath(@"~\UpLoadFiles\SwcCaseFile\" + Swc000 + "\\" + FileName);
                    string ReadPath = getFilePath(Swc000, tmpSwc002, tmpSwc007)+ tmpSwc002 + pathValue01 + FileName;
                    string NewUpath = Server.MapPath(@"~\OutputFile\" + FileName);

                    PdfReader Pdfreader = new PdfReader(ReadPath);
                    PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(NewUpath, FileMode.Create));

                    int PageCount = Pdfreader.NumberOfPages;

                    PdfGState gstate = new PdfGState()
                    {
                        FillOpacity = 0.6f,
                        StrokeOpacity = 0.6f
                    };

                    for (int i = 1; i <= PageCount; i++)
                    {
                        PdfContentByte pdfPageContents = Pdfstamper.GetOverContent(i);
                        pdfPageContents.SetGState(gstate);

                        iTextSharp.text.Rectangle pagesize = Pdfreader.GetPageSizeWithRotation(i); //每頁的Size
                        float x = pagesize.Width;
                        float y = pagesize.Height;
                        float xx = 333;
                        float yy = 333;

                        BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        float fontSize = 36;
                        float adjustTxtLineHeight = fontSize - 4;
                        pdfPageContents.BeginText();
                        pdfPageContents.SetFontAndSize(bfChinese, fontSize);
                        pdfPageContents.SetRGBColorFill(210, 210, 210);
                        pdfPageContents.SetGState(gstate);
                        pdfPageContents.ShowTextAligned(PdfContentByte.ALIGN_LEFT, wReMark01, (x - xx) / 2, (y - yy) / 2, 45);
                        pdfPageContents.EndText();

                        PdfContentByte pdfPageContents2 = Pdfstamper.GetOverContent(i);
                        pdfPageContents2.SetGState(gstate);

                        BaseFont bfChinese2 = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                        float fontSize2 = 16;
                        float adjustTxtLineHeight2 = fontSize2 - 4;
                        pdfPageContents2.BeginText();
                        pdfPageContents2.SetFontAndSize(bfChinese2, fontSize2);
                        pdfPageContents2.SetRGBColorFill(210, 210, 210);
                        pdfPageContents2.SetGState(gstate);
                        pdfPageContents2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, wReMark02, 10, y - 20, 0);
						pdfPageContents2.ShowTextAligned(PdfContentByte.ALIGN_LEFT, wReMark03, (x - xx) / 2, 20, 0);

                        pdfPageContents2.EndText();
                    }
                    Pdfstamper.Close();
                    Pdfreader.Close();

                    rValue = true;
                }
                catch (Exception ex)
                {
                }
                #endregion
                break;
        }
        return rValue;
    }
    
    public string getFilePath55(string tmpSwc000, string tmpSwc002, string tmpSwc007, string fileType)
    {
        bool folderExists;
        string rValue = ConfigurationManager.AppSettings["swcpspath"];
        string FileYearS = tmpSwc002.Substring(4, 3);
        rValue += Convert.ToInt32(FileYearS) > 93 ? Convert.ToInt32(FileYearS).ToString() + "年掃描圖檔" : "93年度暨以前掃描圖檔";
        rValue += tmpSwc007 == "簡易水保" ? "\\水保申請案件\\簡易水保\\" : "\\水保申請案件\\水保計畫\\";
        switch (fileType)
        {
            case "TXTSWC080":
                rValue += tmpSwc002 + "\\掃描檔\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "掃描檔\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC029":
                rValue += tmpSwc002 + "\\審查\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "6-1\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC029CAD":
                rValue += tmpSwc002 + "\\審查\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "6-1-CAD\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC030":
                rValue += tmpSwc002 + "\\審查\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "7-1\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC030CAD":
                rValue += tmpSwc002 + "\\審查\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "7-1-CAD\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC101":
                rValue += tmpSwc002 + "\\竣工圖說\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "竣工圖說\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC101CAD":
                rValue += tmpSwc002 + "\\竣工圖說\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "竣工圖說CAD\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
            case "TXTSWC110":
                rValue += tmpSwc002 + "\\審查單位查核表\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                rValue += "審查單位查核表\\";
                folderExists = Directory.Exists(rValue);
                if (folderExists == false)
                    Directory.CreateDirectory(rValue);
                break;
        }
        return rValue;
    }
    #endregion

    public string GetGuildSWC000()
    {
        string ssUserID = Session["ID"] + "";
        string ssUserGuild01 = Session["ETU_Guild01"] + "";
        string ssUserGuild02 = Session["ETU_Guild02"] + "";
        string ssUserGuild03 = Session["ETU_Guild03"] + "";
        #region-召集人/委員案件
        string sqlCaseID = "''";
        if (ssUserGuild01 != ssUserID)
        {
            string searchCaseId = " select SWC000 from GuildGroup where ETID='" + ssUserID + "'; ";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                SqlDataReader readerData;
                SqlCommand objCmdRV = new SqlCommand(searchCaseId, SWCConn);
                readerData = objCmdRV.ExecuteReader();

                while (readerData.Read())
                {
                    string tSWC000 = readerData["SWC000"] + "";
                    sqlCaseID += ",'" + tSWC000 + "'";
                }
                readerData.Close();
                objCmdRV.Dispose();
            }
        }
        return sqlCaseID;
        #endregion
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
    public bool IsDate(string strDate)
    {
        try
        {
            DateTime.Parse(strDate);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public string GetVisitorsCount()
    {
        string VCount = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string VisitorsCmd = " select max(id)/4 as Visitor from loginnotes;";
			
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

    public void PDFWaterMark(string vFilePath, string vNewFilePath,string vGuildImg)
    {
        iTextSharp.text.Image pdfimageobj;
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

            //頁尾的文本                                                                    
            //Chunk ctitle = new Chunk("Page-" + i.ToString().Trim(), FontFactory.GetFont("Futura", 12f, new BaseColor(0, 0, 0)));
            //Phrase ptitle = new Phrase(ctitle);

            //浮水印
            //string imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/" + ssGuildImg + ".png"); //Logo
            string imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/" + vGuildImg + ".png"); //Logo
            //imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/dPeitou13.jpg"); //Logo

            pdfimageobj = iTextSharp.text.Image.GetInstance(imageUrl);

            float xx = pdfimageobj.ScaledHeight;
            float yy = pdfimageobj.ScaledWidth;

            xx = xx * 0.3f;
            yy = yy * 0.3f;

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageUrl);
            img.ScalePercent(30);  //縮放比例
            //img.SetAbsolutePosition(y - yy, 0); //設定圖片每頁的絕對位置
            img.SetAbsolutePosition(y - yy - 20, x - xx - 20); //設定圖片每頁的絕對位置
            PdfContentByte waterMark = Pdfstamper.GetOverContent(i);
            waterMark.AddImage(img);    //把圖片印上去 

        }
        Pdfstamper.Close();
        Pdfreader.Close();


    }
	
	//讀印章資料
    public string[] PDFS(string text)
    {
        string[] d = new string[6];//存列印印章的資料 0=職字編號 1=水保證明時間 2=土木證明時間  3=水利證明時間  4=大地證明時間  使用ㄓㄜ
        for (int i = 0; i <= 5; i++)
        {

            d[i] = "";

        }
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        try
        {

            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {

                UserConn.Open();

                string strSQLRV = "SELECT    s.SWC21,e.ETOrgIssNo,e.TCNo01ED,e.TCNo02ED,e.TCNo03ED,e.TCNo04ED ";
                strSQLRV += "FROM     SWCSWC AS s INNER JOIN    TCGESWC.dbo.ETUsers AS e ON s.SWC21 = e.ETName";
                strSQLRV += " WHERE(s.SWC00 = '" + text + "')";
                //strSQLRV = strSQLRV + " where ETID = '" + v + "' ";

                SqlDataReader readeUser;
                SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
                readeUser = objCmdUser.ExecuteReader();
                while (readeUser.Read())
                {
                    // string ETOrgIssNo = "" + readeUser["ETOrgIssNo"];
                    d[0] = "" + readeUser["ETOrgIssNo"];
                    //     if (asy=0) { }

                    string TCNo01ED = "" + readeUser["TCNo01ED"];
                    string TCNo02ED = "" + readeUser["TCNo02ED"];
                    string TCNo03ED = "" + readeUser["TCNo03ED"];
                    string TCNo04ED = "" + readeUser["TCNo04ED"];
                    d[5] = "" + readeUser["SWC21"];
                    DateTime sDate = Convert.ToDateTime(TCNo01ED);
                    DateTime sDate1 = Convert.ToDateTime(TCNo02ED);
                    DateTime sDate2 = Convert.ToDateTime(TCNo03ED);
                    DateTime sDate3 = Convert.ToDateTime(TCNo04ED);
                    DateTime sDatea = DateTime.Now;
                    if (sDatea > sDate)
                    {
                        d[1] = "" + 1;
                    }
                    else { d[1] = "" + 0; }
                    if (sDatea > sDate1)
                    {
                        d[2] = "" + 1;
                    }
                    else { d[2] = "" + 0; }
                    if (sDatea > sDate2)
                    {
                        d[3] = "" + 1;
                    }
                    else { d[3] = "" + 0; }
                    if (sDatea > sDate3)
                    {
                        d[4] = "" + 1;
                    }
                    else { d[4] = "" + 0; }





                }
                UserConn.Close();
            }
        }
        catch (Exception eer)
        {

            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Close();
            }
            //   return "" + eer;
        }

        return d;

    }


    //印章浮水印 vFilePath PDF位置,  vNewFilePath  新PDF位置, 新的名稱 vGuildImg, 水保局編號 text 
    public string PDFWaterMark2(string vFilePath, string vNewFilePath, string vGuildImg, string text)
    {
       
        string wr = "";
        string a1 = "";
        try
        {
            iTextSharp.text.Image pdfimageobj;
            PdfReader Pdfreader = new PdfReader(vFilePath);
            PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(vNewFilePath, FileMode.Create));
            string[] sd = PDFS(text);//讀印章資料
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

                //頁尾的文本                                                                    
                //Chunk ctitle = new Chunk("Page-" + i.ToString().Trim(), FontFactory.GetFont("Futura", 12f, new BaseColor(0, 0, 0)));
                //Phrase ptitle = new Phrase(ctitle);

                //浮水印
                //string imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/" + ssGuildImg + ".png"); //Logo
                string imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/" + vGuildImg + ".png"); //Logo
				
				
				
                                                                                                                   //imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/dPeitou13.jpg"); //Logo
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(Server.MapPath("../images/Watermark/signcircle.png"));//印章框的位置
                                                                                                                                    //   string imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/" + vGuildImg + ".png"); //Logo
                pdfimageobj = iTextSharp.text.Image.GetInstance(imageUrl);

                float xx = pdfimageobj.ScaledHeight;
                float yy = pdfimageobj.ScaledWidth;
                img.ScalePercent(23f);  //縮放比例
                                        //旋轉角度
                img.SetAbsolutePosition(30, 30);
                 wr=  pdfimageobj.ScaledHeight + "  "+ pagesize.Height+" "  +pagesize.Width+" "+ pdfimageobj.ScaledWidth;

                PdfContentByte waterMark = Pdfstamper.GetOverContent(i);
                float fontSize = 10;
                float adjustTxtLineHeight = fontSize - 4;
                float txtMarginTop = 0;
                //  BaseFont fontChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\msjh.ttc", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);//字型輸入
                BaseFont fontChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);                        //FontFactory.Register(fontPath);

                pdfPageContents.BeginText();
                pdfPageContents.SetFontAndSize(fontChinese, fontSize);
                pdfPageContents.SetRGBColorFill(27, 63, 239);
                pdfPageContents.SetGState(gstate);
                string a = DateTime.Now.ToString("yyyy-MM-dd");//現在日期

                DateTime dt = DateTime.Parse(a);
                CultureInfo culture = new CultureInfo("zh-TW");
                culture.DateTimeFormat.Calendar = new TaiwanCalendar();
                dt.ToString("yyy-MM-dd", culture);
				
                var color = new PdfSpotColor("spotColorName", new BaseColor(27, 63, 239));//字形顏色
                string[] idn = { "水保", "土木", "水利", "大地" };
                string identity = "";//判斷職稱
                string identity4 = "";
                int sw = 0;
                int[] sdr = { 0,0,0,0};
                if (sd[1] == "" + 0)
                {


                    identity4 += "水保";
                    sdr[0] = 1;
                    sw++;
                }
                if (sd[2] == "" + 0)
                {
                    if (identity4 != "")
                    {
                        identity4 += "、土木";
                    }
                    else { identity4 += "土木"; }
                    sw++;
                    sdr[1] = 1;
                }
                if (sd[3] == "" + 0)
                {
                    if (identity4 != "")
                    {
                        identity4 += "、水利";
                    }
                    else
                    {
                        identity4 += "水利";
                    }
                    sw++;
                    sdr[2] = 1;
                }
                if (sd[4] == "" + 0)
                {
                    if (identity4 != "")
                    {
                        identity4 += "、大地";
                    }
                    else
                    {
                        identity4 += "大地";
                    }
                    sw++;
                    sdr[3] = 1;
                }
                // identity = "水保，土木";
                string identity1 = "技師";
                int re1 = 14;
                Phrase phrase = new Phrase();//技師職稱
                Font font = new Font(fontChinese, re1);

                float[] Cs1 = new float[2];
                float[] Cs2 = new float[2];
                float[] Cs3 = new float[2];
                float[] Cs4 = new float[2];
                float[] Cs5 = new float[2];
              //  sw =3;
              // int[] sdr1 ={ 0,1,1,1 };
             //   sdr = sdr1;
                if (sw == 4)
                {
                  identity = idn[0] + "、" + idn[1];
                    identity4 = idn[2] + "、" + idn[3];
                    Cs1[0] = 95f; Cs1[1] = 136;
                    Cs2[0] = 95f; Cs2[1] = 123;
                }

                if (sw == 3)
                {
                    identity = "";
                    if (sdr[0] == 0)
                    {
                        identity = idn[1];
                        identity4 = idn[2] + "、" + idn[3];
                    }

                    if (sdr[1] == 0)
                    {
                        identity = idn[0];
                        identity4 = idn[2] + "、" + idn[3];
                    }
                    if (sdr[2] == 0)
                    {
                        identity = idn[0];
                        identity4 = idn[1] + "、" + idn[3];
                    }
                    if (sdr[3] == 0)
                    {
                        identity = idn[0];
                        identity4 = idn[1] + "、" + idn[2];
                    }


                    // identity4 = idn[1]+"、"+idn[2] + "、" + idn[3];


                    Cs1[0] = 95f; Cs1[1] = 136;
                    Cs2[0] = 95f; Cs2[1] = 123;
                }
                if (sw == 2)
                {
                    //  identity = "";
                    //   identity4 =  idn[2] + "、" + idn[3]+"技師";
                    identity4 +=  "技師";
                    identity1 = "";
                    Cs1[0] = 95f; Cs1[1] = 136;
                    Cs2[0] = 95f; Cs2[1] = 123;
                }
                if (sw == 1)
                {
                    identity = "";
                    identity1 = "" ;
                   // identity4 =  idn[3]+ "技師";
                    identity4 +=  "技師";
                    Cs1[0] = 95f; Cs1[1] = 136;
                    Cs2[0] = 95f; Cs2[1] = 123;
                }
                if (sw == 0)
                {
                    identity = "";
                    identity1 = "";
                    // identity4 =  idn[3]+ "技師";
                    identity4 += "技師";
                    Cs1[0] = 95f; Cs1[1] = 136;
                    Cs2[0] = 95f; Cs2[1] = 123;
                }
                //  identity = "水保，土木";
                //   identity4 = idn[2] + "，" + idn[3];
                Cs3[0] = 95f; Cs3[1] = 110f;
                Cs4[0] = 95.0f; Cs4[1] = 89.6f;
                Cs5[0] = 94.2f; Cs5[1] = 55;
                Chunk chunk = new Chunk(identity + "\n", font);

                //you may change the 2nd parameter to adjust the weight of boldness
                chunk.SetTextRenderMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE, 0.35f, new BaseColor(27, 63, 239));
                chunk.SetCharacterSpacing(-0.4f);
                phrase.Add(chunk);

                ColumnText.ShowTextAligned(pdfPageContents, PdfContentByte.ALIGN_CENTER, phrase, Cs1[0], Cs1[1], 0);
                // string s = "王大明";




                Phrase phrase8 = new Phrase();//技師職稱
                Font font8 = new Font(fontChinese, re1);

                Chunk chunk8 = new Chunk(identity4 + "\n", font8);
                chunk8.SetCharacterSpacing(-0.4f);
                //you may change the 2nd parameter to adjust the weight of boldness
                chunk8.SetTextRenderMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE, 0.35f, new BaseColor(27, 63, 239));

                phrase8.Add(chunk8);

                ColumnText.ShowTextAligned(pdfPageContents, PdfContentByte.ALIGN_CENTER, phrase8, Cs2[0], Cs2[1], 0);



                Phrase phrase1 = new Phrase();//技師職稱
                Font font1 = new Font(fontChinese, re1);

                Chunk chunk1 = new Chunk(identity1 + "\n", font1);
                chunk1.SetCharacterSpacing(-0.7f);
                //you may change the 2nd parameter to adjust the weight of boldness
                chunk1.SetTextRenderMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE, 0.35f, new BaseColor(27, 63, 239));

                phrase1.Add(chunk1);

                ColumnText.ShowTextAligned(pdfPageContents, PdfContentByte.ALIGN_CENTER, phrase1, Cs3[0], Cs3[1], 0);
                Phrase phrase2 = new Phrase();//技師編號


     //   sd[0] = "台工登字第001012號";

                Font font2 = new Font(fontChinese, 14);
                Chunk chunk2 = new Chunk(sd[0] + "\n", font2);
                chunk2.SetCharacterSpacing(-0.8f);
                //you may change the 2nd parameter to adjust the weight of boldness
                chunk2.SetTextRenderMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE, 0.35f, new BaseColor(27, 63, 239));

                phrase2.Add(chunk2);

                ColumnText.ShowTextAligned(pdfPageContents, PdfContentByte.ALIGN_CENTER, phrase2, Cs4[0], Cs4[1], 0);

                Phrase phrase3 = new Phrase();//技師名子

                Font font3 = new Font(fontChinese, 22);
                Chunk chunk3 = new Chunk(sd[5] + "\n", font3);
                // Chunk chunk3 = new Chunk(s + "\n", font3);
                //you may change the 2nd parameter to adjust the weight of boldness
                chunk3.SetTextRenderMode(PdfContentByte.TEXT_RENDER_MODE_FILL_STROKE, 0.35f, new BaseColor(27, 63, 239));

                phrase3.Add(chunk3);

                ColumnText.ShowTextAligned(pdfPageContents, PdfContentByte.ALIGN_CENTER, phrase3, Cs5[0], Cs5[1], 0);
                //pdfPageContents.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, identity + "\n", 94, 115, 0);//技師職稱

                //  pdfPageContents.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, identity1 + "\n", 94, 105, 0);//技師職稱
                //  pdfPageContents.BeginText();
                //    pdfPageContents.SetFontAndSize(fontChinese, 12);
                //  pdfPageContents.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, sd[0] + "\n", 96, 87, 0);//技師編號
                //   pdfPageContents.SetFontAndSize(fontChinese, 20);
                //   pdfPageContents.ShowTextAligned(iTextSharp.text.Element.ALIGN_CENTER, sd[5] + "\n",96,53, 0);//技師名子

                // iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageUrl);
                //img.ScalePercent(35f);  //縮放比例
                //   img.SetAbsolutePosition(y - yy, 0); //設定圖片每頁的絕對位置
                //   waterMark.ShowTextAligned(canvas, Element.ALIGN_LEFT, new Phrase("Left aligned"), 300, 500, 0);
                waterMark.AddImage(img);    //把圖片印上去 


        }

                Pdfstamper.Close();
                Pdfreader.Close();//編輯結束

                //印字

                //   SetWatermark(vNewFilePath, "你好");
             
		}
        catch (Exception er)
        {
            a1 += er;//返回錯誤訊息
        }
		return wr+" ";
	}

    public bool Mail_Sendxx(string[] MailTos, string MailSub, string MailBody)
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
            mms.From = new System.Net.Mail.MailAddress(MailFrom);
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
                        mms.To.Add(new System.Net.Mail.MailAddress(MailTos[i].Trim()));
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

            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(smtpServer, smtpPort))//或公司、客戶的smtp_server
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

    public string[] GetGeoUserBaseData(string v1, string v2, string v3)
    {
        string[] arrV1 = v1.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        string[] arrV2 = v2.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        string[] arrV3 = v3.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        string[] arrayValue = new string[] { "", "", "", "" };
        string geoUserSql = " select userid,name,jobtitle,email from geouser where department = '審查管理科' AND status<>'停用' ";
        string geoUserStr = "";
        if (arrV1.Length > 0)
            for (int p = 0; p < arrV1.Length; p++)
                geoUserStr += arrV1[p].Trim() != "" ? " or jobtitle ='" + arrV1[p].Trim() + "' " : "";
        if (arrV2.Length > 0)
            for (int p = 0; p < arrV2.Length; p++)
                geoUserStr += arrV2[p].Trim() != "" ? " or mbgroup02 ='" + arrV2[p].Trim() + "' " : "";
        if (arrV3.Length > 0)
            for (int p = 0; p < arrV3.Length; p++)
                geoUserStr += arrV3[p].Trim() != "" ? " or name ='" + arrV3[p].Trim() + "' " : "";
        geoUserSql += geoUserStr == "" ? "" : " AND (" + geoUserStr.Substring(3, geoUserStr.Length - 3) + ")";

        ConnectionStringSettings connectionStringGeo = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection GeoConn = new SqlConnection(connectionStringGeo.ConnectionString))
        {
            GeoConn.Open();

            SqlDataReader readerGeo;
            SqlCommand objCmdGeo = new SqlCommand(geoUserSql, GeoConn);
            readerGeo = objCmdGeo.ExecuteReader();

            while (readerGeo.Read())
            {
                arrayValue[0] += readerGeo["userid"] + ";";
                arrayValue[1] += readerGeo["name"] + ";";
                arrayValue[2] += readerGeo["email"] + ";";
                arrayValue[3] += readerGeo["jobtitle"] + ";";
            }
        }
        return arrayValue;
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
    public void SingRecord(string vSWC000, string vSWC002, string vCaseID,string vCaseType,string vSCType,string vSCDESC,string vSCDeadline)
    {
        Random R = new Random();

        string ssUserId = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string gg = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        string tSCID = "SC" + gg.Substring(2) + R.Next(0, 10).ToString().PadLeft(1, '0');

        string wSingRecord = " INSERT INTO SignCourse (SWC000,SWC002,FormNo,FormType,SCID,SCTtimes,SCType,SCDesc,SCDeadline,SaveUser,SaveDate) VALUES ";
        wSingRecord += " ('" + vSWC000 + "','" + vSWC002 + "','"+ vCaseID + "','"+ vCaseType + "','"+ tSCID + "',getdate(),'"+ vSCType + "','"+ vSCDESC + "','"+ vSCDeadline + "','" + ssUserId + "',getdate()) ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlCommand objCmdUpd = new SqlCommand(wSingRecord, SwcConn);
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
    public bool Mail_Send(string[] MailTos, string MailSub, string MailBody)
    {
        #region setMail
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
        #endregion
		return MailSend(MailTos, MailSub, MailBody, mailAccount, mailPwd);
		//return false;
    }
	public bool Mail_Send_dp(string[] MailTos, string MailSub, string MailBody)
    {
        #region setMail
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
        #endregion
		return MailSend_dp(MailTos, MailSub, MailBody, mailAccount, mailPwd);
		//return false;
    }
    
    public bool MailSend(string[] MailTos, string MailSub, string MailBody,string mailACC,string mailPW)
    {
		mailACC = "SWC.Taipei";
		mailPW = "";
		bool IsMailToNotEmpty = true;
		
        try
        {
			System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
			
			for (int i=0;i< MailTos.Length;i++) { 
				if (MailTos[i].Trim()!="")msg.To.Add(MailTos[i]);
			}

			msg.To.Add("geocheck@geovector.com.tw");
			//msg.To.Add("b@b.com");可以發送給多人
			//msg.CC.Add("c@c.com");
			
			//msg.CC.Add("c@c.com");可以抄送副本給多人 
			//msg.Bcc.Add("geocheck@geovector.com.tw");
			//這裡可以隨便填，不是很重要
			//msg.From = new System.Net.Mail.MailAddress("SWC.taipei@gmail.com", "臺北市政府工務局大地工程處", System.Text.Encoding.UTF8);
			msg.From = new System.Net.Mail.MailAddress("tslm.swc.taipei@gmail.com", "臺北市政府工務局大地工程處", System.Text.Encoding.UTF8);
			/* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
			msg.Subject = MailSub.Replace("\r\n", "").Replace("\n\r", "");//郵件標題
			msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼
			msg.Body = MailBody; //郵件內容
			msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
														 //  msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
			msg.IsBodyHtml = true;//是否是HTML郵件 
								  //msg.Priority = MailPriority.High;//郵件優先級 

			SmtpClient client = new SmtpClient();
			client.Credentials = new System.Net.NetworkCredential(mailACC, mailPW); //這裡要填正確的帳號跟密碼
			client.Host = "smtp.gmail.com"; //設定smtp Server
			client.Port = 587; //設定Port
			client.EnableSsl = true; //gmail預設開啟驗證
			client.Send(msg); //寄出信件
			client.Dispose();
			msg.Dispose();
			if(IsMailToNotEmpty)
            {
                AddMailLog(MailTos, MailSub, MailBody);     //20220411 新增MailLog
            }
			return true;
        }
        catch (Exception ep)
        {
			string ppo = "";
			if (MailTos != null)//防呆
			{
				for (int i = 0; i < MailTos.Length; i++)
				{
					//加入信件的收信人(們)address
					if (!string.IsNullOrEmpty(MailTos[i].Trim()))
					{
						ppo += ppo == "" ? "" : ",";
						ppo += MailTos[i].Trim();
					}
				}
			}
			Class20 C20 = new Class20();
			string errmsg = ppo + "||" + MailSub + "||" + MailBody;
			//C20.recerr("GCClass001","寄信", errmsg);
			return false;
        }
    }
	public bool MailSend_dp(string[] MailTos, string MailSub, string MailBody,string mailACC,string mailPW)
    {
		mailACC = "geo.taipei.dp";
		mailPW = "";
		bool rValue = false;
		
        try
        {
				System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
				msg.To.Add("geocheck@geovector.com.tw");
				
				for (int i=0;i< MailTos.Length;i++) { 
					if (MailTos[i].Trim()!="")msg.To.Add(MailTos[i]); 
				}
					
				msg.From = new System.Net.Mail.MailAddress("geo.taipei.dp@gmail.com", "臺北市政府工務局大地工程處", System.Text.Encoding.UTF8);
				/* 上面3個參數分別是發件人地址（可以隨便寫），發件人姓名，編碼*/
				msg.Subject = MailSub.Replace("\r\n", "").Replace("\n\r", "");//郵件標題
				msg.SubjectEncoding = System.Text.Encoding.UTF8;//郵件標題編碼
				msg.Body = MailBody; //郵件內容
				msg.BodyEncoding = System.Text.Encoding.UTF8;//郵件內容編碼 
															 //  msg.Attachments.Add(new Attachment(@"D:\test2.docx"));  //附件
				msg.IsBodyHtml = true;//是否是HTML郵件 
									  //msg.Priority = MailPriority.High;//郵件優先級 

				SmtpClient client = new SmtpClient();
				client.Credentials = new System.Net.NetworkCredential(mailACC, mailPW); //這裡要填正確的帳號跟密碼
				client.Host = "smtp.gmail.com"; //設定smtp Server
				client.Port = 587; //設定Port
				client.EnableSsl = true; //gmail預設開啟驗證
				client.Send(msg); //寄出信件
				client.Dispose();
				msg.Dispose();
				rValue = true;
        }
        catch (Exception ep)
        {
			string ppo = "";
			if (MailTos != null)//防呆
			{
				for (int i = 0; i < MailTos.Length; i++)
				{
					//加入信件的收信人(們)address
					if (!string.IsNullOrEmpty(MailTos[i].Trim()))
					{
						ppo += ppo == "" ? "" : ",";
						ppo += MailTos[i].Trim();
					}
				}
			}
			Class20 C20 = new Class20();
			string errmsg = ppo + "||" + MailSub + "||" + MailBody;
			//C20.recerr("GCClass001","寄信", errmsg);
        }
        return rValue;
    }
    public string[] str2array(string myString, string splitStr)
    {
        string[] rArray = myString.Split(new string[] { splitStr }, StringSplitOptions.None);
        return rArray;
    }	
	
    public string[] GetUserMailData()
    {
        string ChkUserId = "";
        string ChkUserName = "";
        string ChkUserJobTitle = "";
        string ChkMail = "";
        string ChkMBGROUP = "";
        string ChkMailText = "";
		
        string ChkMailGroup1 = "";
        string ChkMailGroup2 = "";
        string ChkMailGroup3 = "";

        ConnectionStringSettings connectionStringGeo = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection GeoConn = new SqlConnection(connectionStringGeo.ConnectionString))
        {
            GeoConn.Open();

            string strSQLUser = " select * from geouser ";
            strSQLUser = strSQLUser + " where (department = '審查管理科' and [status] ='正常') ";
            //strSQLUser = strSQLUser + "    or userid = 'gv-hsun' ";
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
                
				string tUserMailGroup1 = readerGeo["MailGroup1"] + "";
                string tUserMailGroup2 = readerGeo["MailGroup2"] + "";
                string tUserMailGroup3 = readerGeo["MailGroup3"] + "";

                ChkUserId = ChkUserId + ";;" + tUserId;
                ChkUserName = ChkUserName + ";;" + tUserName;
                ChkUserJobTitle = ChkUserJobTitle + ";;" + tUserJobTitle;
                ChkMail = ChkMail + ";;" + tUserMail;
                ChkMBGROUP = ChkMBGROUP + ";;" + tUserMBGROUP;
                ChkMailText = ChkMailText + ";;";
				
                ChkMailGroup1 = ChkMailGroup1 + ";;" + tUserMailGroup1;
                ChkMailGroup2 = ChkMailGroup2 + ";;" + tUserMailGroup2;
                ChkMailGroup3 = ChkMailGroup3 + ";;" + tUserMailGroup3;

                if (tUserTcgeDataedit.IndexOf("完工設施維護檢查表登錄;") >= 0)
                {
                    Session["TempMailSWC107"] = tUserName;
                }
            }
        }
        string[] arrayReturnValue = new string[] { ChkUserId, ChkUserName, ChkUserJobTitle, ChkMail, ChkMBGROUP, ChkMailText, ChkMailGroup1, ChkMailGroup2, ChkMailGroup3 };
        return arrayReturnValue;
    }
	public string[] GetUserMailDataNoGuild()
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
            //strSQLUser = strSQLUser + "    or userid = 'gv-hsun' ";
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
            }
        }
        string[] arrayReturnValue = new string[] { ChkUserId, ChkUserName, ChkUserJobTitle, ChkMail, ChkMBGROUP, ChkMailText };
        return arrayReturnValue;
    }
    public void SendSMS(string vbPhoneNo, string vbText)
    {
        var CellNo = vbPhoneNo;

        string url = "";
        //系統商的設定到這邊
        //發簡訊時間預約到早上的8點
        string[] sMailh = new string[] { "geocheck@geovector.com.tw" };
		bool reserve = false;
		string timestring = "";
		string args = vbText; // + "&dlvtime=" + timestring;
		args = System.Web.HttpUtility.UrlEncode(args, System.Text.Encoding.GetEncoding("big5"));
		if(DateTime.Now.Hour >= 18){
			timestring = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "080000";
			args += "&dlvtime=" + timestring;
			reserve = true;
		}
		else if(DateTime.Now.Hour < 8){
			timestring = DateTime.Now.ToString("yyyyMMdd") + "080000";
			args += "&dlvtime=" + timestring;
			reserve = true;
		}
		//string args = vbText + "&dlvtime=" + timestring;

        try
        {
            System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url + args);
            myRequest.Method = "GET";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            System.Net.WebResponse myResponse = myRequest.GetResponse();
            System.IO.StreamReader RecStream = new System.IO.StreamReader(myResponse.GetResponseStream());
            string ResponseHTML = RecStream.ReadToEnd();
			//20220408寄信留存
			if(reserve)
				Mail_Send(sMailh, "預約" + timestring + vbText + CellNo, "預約" + timestring + vbText + CellNo);
			else
				Mail_Send(sMailh, vbText + CellNo, vbText + CellNo);
            //return true;//成功
        }
        catch (Exception ex)
        {
            //return false;//寄失敗
            string aa = vbPhoneNo;
            string bb = vbText;
        }

    }
	//複數義務人改陣列
	public void SendSMS_Arr(string[] vbPhoneNoArr, string vbText)
    {
		for (int i = 0; i < vbPhoneNoArr.Length; i++)
		{
			var CellNo = vbPhoneNoArr[i];
	
			string url = "";
			
			//系統商的設定到這邊
			//發簡訊時間預約到早上的8點
			string[] sMailh = new string[] { "geocheck@geovector.com.tw" };
			bool reserve = false;
			string timestring = "";
			string args = vbText; // + "&dlvtime=" + timestring;
			args = System.Web.HttpUtility.UrlEncode(args, System.Text.Encoding.GetEncoding("big5"));
			if(DateTime.Now.Hour >= 18){
				timestring = DateTime.Now.AddDays(1).ToString("yyyyMMdd") + "080000";
				args += "&dlvtime=" + timestring;
				reserve = true;
			}
			else if(DateTime.Now.Hour < 8){
				timestring = DateTime.Now.ToString("yyyyMMdd") + "080000";
				args += "&dlvtime=" + timestring;
				reserve = true;
			}
			//string args = vbText + "&dlvtime=" + timestring;
			
			try
			{
				System.Net.WebRequest myRequest = System.Net.WebRequest.Create(url + args);
				myRequest.Method = "GET";
				myRequest.ContentType = "application/x-www-form-urlencoded";
				System.Net.WebResponse myResponse = myRequest.GetResponse();
				System.IO.StreamReader RecStream = new System.IO.StreamReader(myResponse.GetResponseStream());
				string ResponseHTML = RecStream.ReadToEnd();
				//20220408寄信留存
				if(reserve)
					Mail_Send(sMailh, "預約" + timestring + vbText + CellNo, "預約" + timestring + vbText + CellNo);
				else
					Mail_Send(sMailh, vbText + CellNo, vbText + CellNo);
				//return true;//成功
			}
			catch (Exception ex)
			{
				//return false;//寄失敗
				string aa = vbPhoneNoArr[i];
				string bb = vbText;
			}
		}

    }
    public string Decrypt(string Input)
    {
        byte[] key = { };
        byte[] IV = { };

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
    public void RecordTrunHistory(string v, string v2, string v3, string d, string d2, string d3, string d4)
    {
        string exeSQLstr = " INSERT INTO [dbo].[TrunHistory] ([ID001],[ID002],[ID003],[TH001],[TH002],[TH003],[TH004],[TH005]) values (@ID001,@ID002,@ID003,getdate(),@TH002,@TH003,@TH004,@TH005)";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection HeoConn = new SqlConnection(connectionString.ConnectionString))
        {
            HeoConn.Open();
            using (var cmd = HeoConn.CreateCommand())
            {
                cmd.CommandText = exeSQLstr;
                cmd.Parameters.Add(new SqlParameter("@ID001", v));
                cmd.Parameters.Add(new SqlParameter("@ID002", v2));
                cmd.Parameters.Add(new SqlParameter("@ID003", v3));
                cmd.Parameters.Add(new SqlParameter("@TH002", d));
                cmd.Parameters.Add(new SqlParameter("@TH003", d2));
                cmd.Parameters.Add(new SqlParameter("@TH004", d3));
                cmd.Parameters.Add(new SqlParameter("@TH005", d4));
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
    }
	
	public bool isHoliday(DateTime dt)
    {
		bool rValue = false;
		string date = dt.ToString("yyyyMMdd");
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();
			
			//2是假日 0是上班日
            string strSQLRV = "select * from Holiday ";
            strSQLRV = strSQLRV + " where Date='" + date + "' and Holiday='2' ;";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
            readerUser = objCmdUser.ExecuteReader();

            while (readerUser.Read())
            {
                rValue = true;
            }

        }
		return rValue;
        
    }
	public string GetArchitectUser(string tUserId, string tType)
    {
        string rValue = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            
            string strSQL = " select * from Architect ";
            strSQL = strSQL + " where 帳號 ='" + tUserId + "' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQL, TslmConn);
            readerUser = objCmdUser.ExecuteReader();

            while (readerUser.Read())
            {
                string tEmail = readerUser["信箱"] + "";
                string tName = readerUser["姓名"] + "";
                

                switch (tType)
                {
                    case "Email":
                        rValue = tEmail;
                        break;
					case "Name":
                        rValue = tName;
                        break;
                }
            }
        }
        return rValue;
    }
	
	public string GetArchitectUserByID(string tUserId, string tType)
    {
        string rValue = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            
            string strSQL = " select * from Architect ";
            strSQL = strSQL + " where 帳號 ='" + tUserId + "' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQL, TslmConn);
            readerUser = objCmdUser.ExecuteReader();

            while (readerUser.Read())
            {
                string tEmail = readerUser["信箱"] + "";
                string tName = readerUser["姓名"] + "";
                string tAcctNo = readerUser["帳號"] + "";
                

                switch (tType)
                {
                    case "Email":
                        rValue = tEmail;
                        break;
					case "Name":
                        rValue = tName;
                        break;
					case "AccountNo":
                        rValue = tAcctNo;
                        break;
                }
            }
        }
        return rValue;
    }
	//XY97轉經緯
    public string XY97toll(string orgx, string orgy)
    {
		if(orgx == "")
		{
			return "";
		}
		else
		{
			string newx = "";
			string newy = "";
			var x97 = Convert.ToDouble(orgx);
			var y97 = Convert.ToDouble(orgy);
			var a1 = 6378137.0;
			var b1 = 6356752.314245;
			var lon0 = 121 * Math.PI / 180;
			var k0 = 0.9999;
			var dx = 250000;
			//---------------------------------------------------------    
			var dy = 0;
			var e9 = Math.Pow((1 - Math.Pow(b1, 2) / Math.Pow(a1, 2)), 0.5);
			var x = x97 - dx;
			var y = y97 - dy;
			// Calculate the Meridional Arc 
			var M = y / k0;
			// Calculate Footprint Latitude  
			var mu = M / (a1 * (1.0 - Math.Pow(e9, 2) / 4.0 - 3 * Math.Pow(e9, 4) / 64.0 - 5 * Math.Pow(e9, 6) / 256.0));
			var e1 = (1.0 - Math.Pow(1.0 - Math.Pow(e9, 2), 0.5)) / (1.0 + Math.Pow((1.0 - Math.Pow(e9, 2)), 0.5));
			var J1 = 3 * e1 / 2 - 27 * Math.Pow(e1, 3) / 32.0;
			var J2 = 21 * Math.Pow(e1, 2) / 16 - 55 * Math.Pow(e1, 4) / 32.0;
			var J3 = 151 * Math.Pow(e1, 3) / 96.0;
			var J4 = 1097 * Math.Pow(e1, 4) / 512.0;
			var fp = mu + J1 * Math.Sin(2 * mu) + J2 * Math.Sin(4 * mu) + J3 * Math.Sin(6 * mu) + J4 * Math.Sin(8 * mu);
			// Calculate Latitude and Longitude   
			var e2 = Math.Pow(e9 * a1 / b1, 2);
			var C1 = Math.Pow(e2 * Math.Cos(fp), 2);
			var T1 = Math.Pow(Math.Tan(fp), 2);
			var R1 = a1 * (1 - Math.Pow(e9, 2)) / Math.Pow((1 - Math.Pow(e9, 2) * Math.Pow(Math.Sin(fp), 2)), (3.0 / 2.0));
			var N1 = a1 / Math.Pow(1 - Math.Pow(e9, 2) * Math.Pow(Math.Sin(fp), 2), 0.5);
			var D = x / (N1 * k0);
			// 計算緯度   
			var Q1 = N1 * Math.Tan(fp) / R1;
			var Q2 = Math.Pow(D, 2) / 2.0;
			var Q3 = (5 + 3 * T1 + 10 * C1 - 4 * Math.Pow(C1, 2) - 9 * e2) * Math.Pow(D, 4) / 24.0;
			var Q4 = (61 + 90 * T1 + 298 * C1 + 45 * Math.Pow(T1, 2) - 3 * Math.Pow(C1, 2) - 252 * e2) * Math.Pow(D, 6) / 720.0;
			var lat = fp - Q1 * (Q2 - Q3 + Q4);
			// 計算經度    
			var Q5 = D;
			var Q6 = (1 + 2 * T1 + C1) * Math.Pow(D, 3) / 6;
			var Q7 = (5 - 2 * C1 + 28 * T1 - 3 * Math.Pow(C1, 2) + 8 * e2 + 24 * Math.Pow(T1, 2)) * Math.Pow(D, 5) / 120.0;
			var lon = lon0 + (Q5 - Q6 + Q7) / Math.Cos(fp);
			//緯
			lat = lat * 180 / Math.PI;
			//geohs034.Text = Left(lat.ToString, 9);
			//經
			lon = lon * 180 / Math.PI;
			newx = lon.ToString("0.0000000").Trim();
			newy = lat.ToString("0.0000000").Trim();
			return newx + "," + newy;
		}
    }
	
	#region.紀錄寄信(簡訊)LOG
    protected void AddMailLog(string[] MailTos, string MailSub, string MailBody)    //20220411 新增MailLog mailtext 裡面包含簡訊的號碼
    {
        string mailTosStr = string.Join(";", MailTos);
        string strSQL = string.Empty;
        strSQL = " INSERT INTO MailLog (Email, MailSub, MailText, MailDate) VALUES (@Email, @mailsub, @mailtext, @MailDate); ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = strSQL;
                cmd.Parameters.Add(new SqlParameter("@Email", mailTosStr));
                cmd.Parameters.Add(new SqlParameter("@mailsub", MailSub));
                cmd.Parameters.Add(new SqlParameter("@mailtext", MailBody));
                cmd.Parameters.Add(new SqlParameter("@MailDate", DateTime.Now));
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
    }
    #endregion
}