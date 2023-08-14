using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply008 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        
        GBClass001 SBApp = new GBClass001();
        LoadSwcClass01 LoadApp = new LoadSwcClass01();
        Class20 C20 = new Class20();

        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        if (!IsPostBack)
        {
            C20.swcLogRC("OnlineApply008", "停工申請", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
                string LINK = "OnlineApply008v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
                Response.Redirect(LINK);

            }
            else
			{
                switch (ssUserType)
                {
                    case "01":
                    case "02":
                    case "03":
                    case "04":
                    case "08":
                    case "09":
                        if (rOLANO == "") { Response.Redirect("SWC001.aspx"); }
                        else
                            GetOLA02Data(rSWCNO, rOLANO);
                        break;
                    default:
                        Response.Redirect("SWC000.aspx");
                        break;
                }
				if(ssUserType == "08") DataLock.Visible = false;
			}
        }
        else { if (ssUserName == "") { Response.Redirect("SWC001.aspx"); } }

        //全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用

    }

    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }
    private void GetOLA02Data(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            string tSWC051 = "";
            while (readeSwc.Read())
            {
                string tSWC000 = readeSwc["SWC000"] + "";
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                tSWC051 = readeSwc["SWC051"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
                LBSWC025.Text = tSWC025;

            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string rONA000 = GetONAID();
                TXTONA001.Text = rONA000;
                DDLONA010.SelectedValue = "是";
                TXTONA002.Text = SBApp.DateView(tSWC051, "00");
                TXTONA003.Text = SBApp.DateView(DateTime.Now.ToString(),"00");
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply08 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA08001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA08002"] + "";
                    string tONA003 = readeONA["ONA08003"] + "";
                    string tONA004 = readeONA["ONA08004"] + "";
                    string tONA005 = readeONA["ONA08005"] + "";
                    string tONA006 = readeONA["ONA08006"] + "";
                    string tONA007 = readeONA["ONA08007"] + "";
                    string tONA008 = readeONA["ONA08008"] + "";
                    string tONA009 = readeONA["ONA08009"] + "";
                    string tONA010 = readeONA["ONA08010"] + "";
                    tDATALOCK = readeONA["DATALOCK"] + "";

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = SBApp.DateView(tONA003, "00");
                    TXTONA004.Text = SBApp.DateView(tONA004, "00");
                    if (tONA005 == "1") { CHKONA005.Checked = true; }
                    if (tONA006 == "1") { CHKONA006.Checked = true; }
                    if (tONA007 == "1") { CHKONA007.Checked = true; }
                    if (tONA008 == "1") { CHKONA008.Checked = true; }
                    TXTONA009.Text = tONA009;
                    DDLONA010.SelectedValue = tONA010;
                }
            }
        }
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA08" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA08" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA08001) AS MAXID from OnlineApply08 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA08001,9) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 6, 6)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(6, '0');
                }
            }

        }
        return _ReturnVal;
    }
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA08001 = TXTONA001.Text + "";
        string sONA08002 = TXTONA002.Text + "";
        string sONA08003 = TXTONA003.Text + "";
        string sONA08004 = TXTONA004.Text + "";
        string sONA08005 = CHKONA005.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA08006 = CHKONA006.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA08007 = CHKONA007.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA08008 = CHKONA008.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA08009 = TXTONA009.Text + "";
        string sONA08010 = DDLONA010.SelectedValue + "";

        string sEXESQLUPD = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply08 ";
            strSQLRV = strSQLRV + " where ONA08001 = '" + sONA08001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
                sEXESQLUPD = " INSERT INTO OnlineApply08 (SWC000,SWC002,ONA08001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA08001 + "');";
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply08 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA08002 = '" + sONA08002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08003 = '" + sONA08003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08004 = '" + sONA08004 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08005 = '" + sONA08005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08006 = '" + sONA08006 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08007 = '" + sONA08007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08008 = '" + sONA08008 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08009 = '" + sONA08009 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA08010 = '" + sONA08010 + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA08001 = '" + sONA08001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
            
            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        }
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC005 = LBSWC005.Text;
        string sSWC025 = LBSWC025.Text;
        string sONA08001 = TXTONA001.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply08 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA08001 = '" + sONA08001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (readeSwc.HasRows)
            {
                while (readeSwc.Read())
                {
                    string sDATALOCK = readeSwc["DATALOCK"] + "";
                    if (sDATALOCK == "Y")
                    {
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply008v.aspx?SWCNO=" + sSWC000 + "&OLANO=" + sONA08001 + "'; </script>");
                        return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply08 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewResults = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDoc = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ResultsExplain = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDirections = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReSendDeadline = null, ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y', ";
            sEXESQLSTR += " SWC005 = N'" + sSWC005 + "', ";
            sEXESQLSTR += " SWC025 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING002 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING004 = N'" + sSWC025 + ";', ";
            sEXESQLSTR += " SING006 = N'" + ssUserName + "', ";
            sEXESQLSTR += " SING007 = N'送出',ONAHEAD=N'水土保持計畫義務人及技師變更報備',SING008 = N'待簽辦', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKUSER = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKDATE = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA08001 = '" + sONA08001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@ONA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
            ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = strSQL3;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sSWC005));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sSWC025));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA08001));
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
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA08001, "申請中", ssUserID, "", "");
            SendMailNotice(sSWC000);

            Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }
    }
    private void SendMailNotice(string gSWC000)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();

        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";

        //TextBox1.Text = strUserName;
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);

        //送出提醒名單：承辦人、主管（科長，正工，股長，系統管理員）、檢查公會

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC024, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV = strSQLRV + " LEFT JOIN ETUsers U on SWC.SWC045ID = U.ETID ";
            strSQLRV = strSQLRV + " where SWC.SWC000 = '" + gSWC000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC012 = readeSwc["SWC012"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC024 = readeSwc["SWC024"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄件名單
                //寫死名單：章姿隆  ge-10706

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706")
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = aUserName + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增停工申請";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增停工申請，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
					else if (aUserName.Trim() == tSWC024.Trim())
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail02 = new string[] { aUserMail };
                        string ssMailSub02 = "(" + aUserName + ")" + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增停工申請";
                        string ssMailBody02 = tSWC012 + "轄區【" + tSWC005 + "】已新增停工申請，請上管理平台查看" + "<br><br>";
                        ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);
                    }
                }
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
}