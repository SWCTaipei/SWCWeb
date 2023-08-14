using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply011 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    
	protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "tcge7@geovector.com.tw" };
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
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        GBClass001 SBApp = new GBClass001();

        if (!IsPostBack)
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
                    {
                        GetOLA11Data(rSWCNO, rOLANO);
                    }
                    break;
                default:
                    Response.Redirect("SWC000.aspx");
                    break;
            }
			if(ssUserType == "08") DataLock.Visible = false;
        }

        //全區供用
        SBApp.ViewRecord("失效重核", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用
    }

    private void GetOLA11Data(string v, string v2)
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

            while (readeSwc.Read())
            {
                string sSWC000 = readeSwc["SWC000"] + "";
                string sSWC002 = readeSwc["SWC002"] + "";
                string sSWC005 = readeSwc["SWC005"] + "";
                string sSWC025 = readeSwc["SWC025"] + "";
                string sSWC038 = readeSwc["SWC038"] + "";
                string sSWC039 = readeSwc["SWC039"] + "";
                sSWC038 = SBApp.DateView(sSWC038, "00");

                #region Label
                string[] aLBValue = new string[] { sSWC000, sSWC002, sSWC005, sSWC025, sSWC038 + sSWC039 };
                Label[] aLabel = new Label[] { LBSWC000, LBSWC002, LBONA11004, LBONA11003, LBONA11005 };
                for (int i = 0; i < aLBValue.Length; i++)
                {
                    string strLBValue = aLBValue[i];
                    System.Web.UI.WebControls.Label LabelObj = aLabel[i];
                    LabelObj.Text = strLBValue;
                }
                #endregion

            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string rONA000 = GetONAID();
                LBONA11001.Text = rONA000;
                LBONA11002.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply11 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA11001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA11001 = readeONA["ONA11001"] + "";
                    string tONA11002 = readeONA["ONA11002"] + "";
                    string tONA11006 = readeONA["ONA11006"] + "";
                    string tONA11007 = readeONA["ONA11007"] + "";
                    string tONA11008 = readeONA["ONA11008"] + "";
                    string tONA11009 = readeONA["ONA11009"] + "";
                    string tONA11010 = readeONA["ONA11010"] + "";
                    string tONA11011 = readeONA["ONA11011"] + "";
                    string tONA11012 = readeONA["ONA11012"] + "";
                    string tONA11013 = readeONA["ONA11013"] + "";
                    string tONA11014 = readeONA["ONA11014"] + "";
                    string tONA11015 = readeONA["ONA11015"] + "";
                    string tONA11016 = readeONA["ONA11016"] + "";
                    string tSING007 = readeONA["SING007"] + "";
                    string tTMPSN01 = readeONA["TMPSN01"] + "";
                    tDATALOCK = readeONA["DATALOCK"] + "";
                    LBONA11001.Text = tONA11001;
                    if (tDATALOCK == "Y") { LBONA11002.Text = SBApp.DateView(tONA11002, "00"); LockArea(false); Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); </script>"); }
                    else { LBONA11002.Text = DateTime.Now.ToString("yyyy-MM-dd"); LockArea(true); }

                    if (tONA11006 == "True") { CHKONA11006_Y.Checked = true; }
                    if (tONA11006 == "False") { CHKONA11006_N.Checked = true; }
                    DDLONA11007.SelectedValue = tONA11007;

                    if (tONA11008 == "True") { CHKONA11008_Y.Checked = true; }
                    if (tONA11008 == "False") { CHKONA11008_N.Checked = true; }
                    TXTONA11009.Text = tONA11009;

                    if (tONA11010 == "True") { CHKONA11010_Y.Checked = true; }
                    if (tONA11010 == "False") { CHKONA11010_N.Checked = true; }
                    TXTONA11011.Text = tONA11011;

                    if (tONA11012 == "True") { CHKONA11012_Y.Checked = true; }
                    if (tONA11012 == "False") { CHKONA11012_N.Checked = true; }
                    TXTONA11013.Text = tONA11013;

                    if (tONA11014 == "True") { CHKONA11014_Y.Checked = true; }
                    if (tONA11014 == "False") { CHKONA11014_N.Checked = true; }
                    TXTONA11015.Text = tONA11015;

                    if (tONA11016 == "True") { CHKONA11016.Checked = true; }
                    if (tONA11016 == "False") { CHKONA11016.Checked = false; }

                    if (tSING007 == "決行") { YorN.Text = tTMPSN01; }
                }
            }
        }
    }
    //可否編輯
    private void LockArea(bool s)
    {
        CHKONA11006_Y.Enabled = s;
        CHKONA11006_N.Enabled = s;
        DDLONA11007.Enabled = s;
        CHKONA11008_Y.Enabled = s;
        CHKONA11008_N.Enabled = s;
        TXTONA11009.Enabled = s;
        CHKONA11010_Y.Enabled = s;
        CHKONA11010_N.Enabled = s;
        TXTONA11011.Enabled = s;
        CHKONA11012_Y.Enabled = s;
        CHKONA11012_N.Enabled = s;
        TXTONA11013.Enabled = s;
        CHKONA11014_Y.Enabled = s;
        CHKONA11014_N.Enabled = s;
        TXTONA11015.Enabled = s;
        CHKONA11016.Enabled = s;
        DataLock.Visible = s;
        SaveCase.Visible = s;
    }

    //新單取單號
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA11" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA11" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA11001) AS MAXID from OnlineApply11 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA11001,9) = '" + tempVal + "' ";

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
    //暫存
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        //SWC000
        string sSWC000 = LBSWC000.Text + "";
        //SWC002
        string sSWC002 = LBSWC002.Text + "";
        //ONA11001單號
        string sONA11001 = LBONA11001.Text + "";
        //ONA11002申報日期
        string sONA11002 = LBONA11002.Text + "";
        //ONA11003承辦人員
        string sONA11003 = LBONA11003.Text + "";
        //ONA11004計畫名稱
        string sONA11004 = LBONA11004.Text + "";
        //ONA11005原核定日期
        string sONA11005 = LBONA11005.Text + "";

        //ONA11006
        string sONA11006 = "";
        if (CHKONA11006_Y.Checked == true) { sONA11006 = "True"; }
        if (CHKONA11006_N.Checked == true) { sONA11006 = "False"; }
        //ONA11007
        string sONA11007 = DDLONA11007.SelectedValue + "";

        //ONA11008
        string sONA11008 = "";
        if (CHKONA11008_Y.Checked == true) { sONA11008 = "True"; }
        if (CHKONA11008_N.Checked == true) { sONA11008 = "False"; }
        //ONA11009
        string sONA11009 = TXTONA11009.Text + "";

        //ONA11010
        string sONA11010 = "";
        if (CHKONA11010_Y.Checked == true) { sONA11010 = "True"; }
        if (CHKONA11010_N.Checked == true) { sONA11010 = "False"; }
        //ONA11011
        string sONA11011 = TXTONA11011.Text + "";

        //ONA11012
        string sONA11012 = "";
        if (CHKONA11012_Y.Checked == true) { sONA11012 = "True"; }
        if (CHKONA11012_N.Checked == true) { sONA11012 = "False"; }
        //ONA11013
        string sONA11013 = TXTONA11013.Text + "";

        //ONA11014
        string sONA11014 = "";
        if (CHKONA11014_Y.Checked == true) { sONA11014 = "True"; }
        if (CHKONA11014_N.Checked == true) { sONA11014 = "False"; }
        //ONA11015
        string sONA11015 = TXTONA11015.Text + "";

        //ONA11016
        string sONA11016 = "";
        if (CHKONA11016.Checked == true) { sONA11016 = "True"; }
        if (CHKONA11016.Checked == false) { sONA11016 = "False"; }

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply11 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + " and ONA11001 = '" + sONA11001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                strSQLRV = " INSERT INTO OnlineApply11 (SWC000,SWC002,ONA11001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA11001 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            strSQLRV = strSQLRV + " Update OnlineApply11 Set ";

            strSQLRV = strSQLRV + " ONA11002 = '" + sONA11002 + "', ";
            strSQLRV = strSQLRV + " ONA11003 = '" + sONA11003 + "', ";
            strSQLRV = strSQLRV + " ONA11004 = '" + sONA11004 + "', ";
            strSQLRV = strSQLRV + " ONA11005 = '" + sONA11005 + "', ";
            strSQLRV = strSQLRV + " ONA11006 = '" + sONA11006 + "', ";
            strSQLRV = strSQLRV + " ONA11006_1 = '" + sONA11006 + "', ";
            strSQLRV = strSQLRV + " ONA11007 = '" + sONA11007 + "', ";
            strSQLRV = strSQLRV + " ONA11007_1 = '" + sONA11007 + "', ";
            strSQLRV = strSQLRV + " ONA11008 = '" + sONA11008 + "', ";
            strSQLRV = strSQLRV + " ONA11008_1 = '" + sONA11008 + "', ";
            strSQLRV = strSQLRV + " ONA11009 = '" + sONA11009 + "', ";
            strSQLRV = strSQLRV + " ONA11009_1 = '" + sONA11009 + "', ";
            strSQLRV = strSQLRV + " ONA11010 = '" + sONA11010 + "', ";
            strSQLRV = strSQLRV + " ONA11010_1 = '" + sONA11010 + "', ";
            strSQLRV = strSQLRV + " ONA11011 = '" + sONA11011 + "', ";
            strSQLRV = strSQLRV + " ONA11011_1 = '" + sONA11011 + "', ";
            strSQLRV = strSQLRV + " ONA11012 = '" + sONA11012 + "', ";
            strSQLRV = strSQLRV + " ONA11012_1 = '" + sONA11012 + "', ";
            strSQLRV = strSQLRV + " ONA11013 = '" + sONA11013 + "', ";
            strSQLRV = strSQLRV + " ONA11013_1 = '" + sONA11013 + "', ";
            strSQLRV = strSQLRV + " ONA11014 = '" + sONA11014 + "', ";
            strSQLRV = strSQLRV + " ONA11014_1 = '" + sONA11014 + "', ";
            strSQLRV = strSQLRV + " ONA11015 = '" + sONA11015 + "', ";
            strSQLRV = strSQLRV + " ONA11015_1 = '" + sONA11015 + "', ";
            strSQLRV = strSQLRV + " ONA11016 = '" + sONA11016 + "', ";

            strSQLRV = strSQLRV + " saveuser = '" + ssUserID + "', ";
            strSQLRV = strSQLRV + " savedate = getdate() ";

            strSQLRV = strSQLRV + " Where ONA11001 = '" + sONA11001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(strSQLRV, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();


            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔'); location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        }
    }
    //送出
    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 SwcApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";

        string SaveUserName = SwcApp.GetETUser(ssUserID, "Name");


        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA11001 = LBONA11001.Text + "";

        string sONA11003 = LBONA11003.Text + "";
        string sONA11004 = LBONA11004.Text + "";

        string sEXESQLSTR = "Update OnlineApply11 Set DATALOCK = 'Y',SWC005=@SWC005,SWC025=@SWC025,SING002=@SING002,";
        sEXESQLSTR += "SING004=@SING004,SING006=@SING006,SING007=N'送出',SING008=N'待簽辦',ONAHEAD=N'水土保持計畫失效重新核定申請表',";
        sEXESQLSTR += "TB05=@TB05,TB06=@TB06,TB07=@TB07,TB08=N'建案適用;;',TB09=N'上述審查僅就目的事業開發範圍之水土保持處理，有關建築物配置位置、結構、基礎、地下室開挖、相關開發利用規定及土地權屬，請秉權卓處，並於核發開發利用許可後知會本處，俾據以核處山坡地開發利用回饋金。如水土保持計畫與其他法規牴觸致須修改，請即知會本處辦理。',";
        sEXESQLSTR += "TB12=N'水土保持計畫',TB13=N'依據「水土保持計畫審核監督辦法」第22條及第31條之1規定，水土保持計畫應於核定後3年內申報開工，無法於3年內開工（得申請展延2次，每次不得超過6個月），水土保持計畫失其效力。',TB16 = N'屬建案',TB17=N'為強化坡地管理，(同函副請本府都市發展局(目的事業非建管處者適用))請將水土保持計畫之水土保持設施配置圖及臨時防災配置圖納入建築執照核准圖，並於建築執照竣工後，將竣工水土保持設施納入建築竣工圖繪製。',";
        sEXESQLSTR += "LOCKUSER=@LOCKUSER,LOCKDATE=getdate() Where ONA11001=@ONA11001;";

        sEXESQLSTR += "Update SWCCASE set SWC131='Y' where SWC000=@SWC000 ;";
        sEXESQLSTR += "Update tslm2.dbo.SWCSWC set SWC131='Y' where SWC00=@SWC000 ;";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE where SWC000='" + sSWC000 + "' ;";
            string sSWC022 = "";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (readeSwc.HasRows)
            {
                while (readeSwc.Read())
                {
                    sSWC022 = readeSwc["SWC022"] + "";
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            ConnectionStringSettings connectionString1 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn1 = new SqlConnection(connectionString1.ConnectionString))
            {
                SwcConn1.Open();
                using (var cmd = SwcConn1.CreateCommand())
                {
                    cmd.CommandText = sEXESQLSTR;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sONA11004));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sONA11003));
                    cmd.Parameters.Add(new SqlParameter("@SING002", sONA11003));
                    cmd.Parameters.Add(new SqlParameter("@SING004", sONA11003 + ";"));
                    cmd.Parameters.Add(new SqlParameter("@SING006", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@TB05", SaveUserName));
                    cmd.Parameters.Add(new SqlParameter("@TB06", sONA11001));
                    cmd.Parameters.Add(new SqlParameter("@TB07", sSWC022));
                    cmd.Parameters.Add(new SqlParameter("@LOCKUSER", ssUserID));
                    cmd.Parameters.Add(new SqlParameter("@ONA11001", sONA11001));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }

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
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sONA11004));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sONA11003));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA11001));
                    cmd.Parameters.Add(new SqlParameter("@R001", ""));
                    cmd.Parameters.Add(new SqlParameter("@R002", ""));
                    cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                    //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                    cmd.Parameters.Add(new SqlParameter("@R005", ""));
                    cmd.Parameters.Add(new SqlParameter("@R006", ""));
                    cmd.Parameters.Add(new SqlParameter("@R007", SwcApp.GetETUser(ssUserID, "OrgName")));
                    cmd.Parameters.Add(new SqlParameter("@R008", SwcApp.GetETUser(ssUserID, "ETIdentity")));
                    cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/hhmm")));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            SwcApp.RecordTrunHistory(sSWC000, sSWC002, sONA11001, "申請中", ssUserID, "", "");
            SendMailNotice(sSWC000);

            Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }
    }
    private void SendMailNotice(string gSWC000)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailDataNoGuild();

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

        //送出提醒名單：承辦人、主管（科長，正工程司，股長，系統管理員）

		string tSWC005 = "";
		string tSWC012 = "";
		string tSWC013TEL = "";
		string tSWC022 = "";
        string tSWC108 = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = '" + gSWC000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                tSWC005 = readeSwc["SWC005"] + "";
                tSWC012 = readeSwc["SWC012"] + "";
                tSWC013TEL = readeSwc["SWC013TEL"] + "";
                tSWC022 = readeSwc["SWC022"] + "";
                tSWC108 = readeSwc["SWC108"] + "";
			}
            readeSwc.Close();
            objCmdSwc.Dispose();
		}
		//寄件名單
		//寫死名單：章姿隆  ge-10706
		string ssMailSub01 = "您好，【" + tSWC012 + "】轄區【" + tSWC005 + "】已新增失效重核申請，請至系統查看。";
		string ssMailBody01 = "您好，【" + tSWC012 + "】轄區【" + tSWC005 + "】已新增失效重核申請，請至系統查看" + "<br><br>";
		ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

		string ssMailSub02 = "您好，【" + tSWC005 + "】已新增失效重核申請，請至臺北市水土保持申請書件管理平台上瀏覽。";
		string ssMailBody02 = "您好，【" + tSWC005 + "】已新增失效重核申請，請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
		ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";


		string SentMailGroup = "";
		for (int i = 1; i < arrayUserId.Length; i++)
		{
			string aUserId = arrayUserId[i];
			string aUserName = arrayUserName[i];
			string aJobTitle = arrayJobTitle[i];
			string aUserMail = arrayUserMail[i];
			string aMBGROUP = arrayMBGROUP[i];
			
			if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工程司" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserId.Trim() == "ge-10706")
			{
				SentMailGroup = SentMailGroup + ";;" + aUserMail;
			}
		}
		string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);

		bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

		string[] arraySWC013TEL = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
		SBApp.SendSMS_Arr(arraySWC013TEL, ssMailBody02);

        string ssUserName = Session["NAME"] + "";
        string exeSQLSTR2 = " select ETEmail as EMAIL from ETUsers where etname = '" + ssUserName + "' ;";
        string SentMailGroup1 = "";
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(exeSQLSTR2, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tEMail = readeSwc["EMAIL"] + "";
                SentMailGroup1 += ";;" + tEMail;
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
        }
        SentMailGroup1 += ";;" + tSWC108;
        string[] arraySentMail02 = SentMailGroup1.Split(new string[] { ";;" }, StringSplitOptions.None);
        bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);
    }
}