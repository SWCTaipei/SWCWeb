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

public partial class SWCDOC_OnlineApply002 : System.Web.UI.Page
{
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
			SBApp.ViewRecord("水土保持計畫暫停審查", "update", "");
            C20.swcLogRC("OnlineApply002", "暫停審查", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
            if (rRRPG=="55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
                string LINK = "OnlineApply002v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
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
                        {
                            GetOLA02Data(rSWCNO, rOLANO);
                        }
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
        if (ssUserType=="02") { TitleLink00.Visible = true; }
        //全區供用

    }

    private void GetOLA02Data(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";
		string tSWC088 = "";

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
                string tSWC000 = readeSwc["SWC000"] + "";
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
				tSWC088 = readeSwc["SWC088"] + "";
                tSWC088 = SBApp.DateView(tSWC088, "00");

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
                #region Label                           
                string[] aLBValue = new string[] { tSWC025 };
                Label[] aLabel = new Label[] { LBSWC025 };
                for (int i = 0; i < aLBValue.Length; i++)
                {
                    string strTBValue = aLBValue[i];
                    System.Web.UI.WebControls.Label LabelObj = aLabel[i];
                    LabelObj.Text = strTBValue;
                }
                #endregion

            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
				//審查期限判斷
                if (tSWC088 == "") 
                {
                    Response.Write("<script>alert('審查期限無值，請確認');location.href='SWC003.aspx?SWCNO=" + v + "';</script>");
                }
                else
                {
                    if (DateTime.Parse(tSWC088) < DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                    {
                        Response.Write("<script>alert('已超過審查期限，故無法申請');location.href='SWC003.aspx?SWCNO=" + v + "';</script>");
                    }
                }
                //判斷是否已經有暫停審查表單
                if (chkOA002(v))
                {
                    Response.Write("<script>alert('此案件已申請過暫停審查，故無法申請');location.href='SWC003.aspx?SWCNO=" + v + "';</script>");
                }
                string rONA000 = GetONAID();
                TXTONA001.Text = rONA000;
				
				TXTONA002.Text = DateTime.Now.ToString("yyyy-MM-dd");
				string rONA003 = Convert.ToDateTime(DateTime.Now).AddMonths(3).ToString("yyyy-MM-dd");
				//***新增假日判斷
				bool qholiday = true;
				DateTime dt = Convert.ToDateTime(rONA003);
				dt = dt.AddDays(-1);
				while (qholiday == true){
					dt = dt.AddDays(1);
					qholiday = SBApp.isHoliday(dt);
				}
				rONA003 = dt.ToString("yyyy-MM-dd");
				//***新增假日判斷
				TXTONA003.Text = rONA003;
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply02 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA02001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA02002"] + "";
                    string tONA003 = readeONA["ONA02003"] + "";
                    string tONA004 = readeONA["ONA02004"] + "";
                    string tONA005 = readeONA["ONA02005"] + "";
                    string tONA006 = readeONA["ONA02006"] + "";
                    string tONA007 = readeONA["ONA02007"] + "";
                    string tONA008 = readeONA["ONA02008"] + "";
                    tDATALOCK = readeONA["DATALOCK"] + "";

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = SBApp.DateView(tONA003, "00");
                    TXTONA004.Text = tONA004;

                    if (tONA005 == "1") { CHKONA005.Checked = true; }
                    if (tONA006 == "1") { CHKONA006.Checked = true; }
                    if (tONA007 == "1") { CHKONA007.Checked = true; }
                    if (tONA008 == "1") { CHKONA008.Checked = true; }
                }
            }
        }

        if (tDATALOCK == "Y")
            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply002v.aspx?SWCNO=" + v + "&OLANO=" + v2 + "'; </script>");
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA02" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA02" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA02001) AS MAXID from OnlineApply02 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA02001,9) = '" + tempVal + "' ";

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
        string sONA02001 = TXTONA001.Text + "";
        string sONA02002 = TXTONA002.Text + "";
        string sONA02003 = TXTONA003.Text + "";
        string sONA02004 = TXTONA004.Text + "";
        string sONA02005 = CHKONA005.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA02006 = CHKONA006.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA02007 = CHKONA007.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA02008 = CHKONA008.Checked.ToString().Replace("False", "").Replace("True", "1");

        if (sONA02004.Length > 500) { sONA02004 = sONA02004.Substring(0, 500); }

        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply02 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + " and ONA02001 = '" + sONA02001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply02 (SWC000,SWC002,ONA02001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA02001 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply02 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA02002 = '" + sONA02002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA02003 = '" + sONA02003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA02004 = '" + sONA02004 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA02005 = '" + sONA02005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA02006 = '" + sONA02006 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA02007 = '" + sONA02007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA02008 = '" + sONA02008 + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA02001 = '" + sONA02001 + "'";

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
        string sONA02001 = TXTONA001.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply02 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA02001 = '" + sONA02001 + "' ";

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
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply002v.aspx?SWCNO=" + sSWC000 + "&OLANO=" + sONA02001 + "'; </script>");
                        return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply02 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewResults = '1', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDoc = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ResultsExplain = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDirections = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReSendDeadline = null, ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y', ";
            sEXESQLSTR += " SWC005 = N'" + sSWC005 + "', ";
            sEXESQLSTR += " SWC025 = N'" + sSWC025 + "', ";
            //sEXESQLSTR += " SING002 = N'" + sSWC025 + "', ";
            //sEXESQLSTR += " SING004 = N'" + sSWC025 + ";', ";
            //sEXESQLSTR += " SING006 = N'" + ssUserName + "', ";
            //sEXESQLSTR += " SING007 = N'送出', ";
			sEXESQLSTR += " SING007 = N'結案', ";
            sEXESQLSTR += " SING008 = N'結案', ";
            sEXESQLSTR += " TMPSN01 = N'准', ";
			sEXESQLSTR += " ONAHEAD=N'水土保持計畫暫停審查', ";
			//sEXESQLSTR += " SING008 = N'待簽辦', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKUSER = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKDATE = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA02001 = '" + sONA02001 + "'";

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
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA02001));
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
			//改狀態"暫停審查"，帶"暫停期限"到主表
			string strSQL4 = " update swcswc set SWC04=@SWC004,SWC89=@SWC089 Where SWC00=@SWC000; update tcgeswc.dbo.swccase set SWC004=@SWC004,SWC089=@SWC089 Where SWC000=@SWC000; ";
            ConnectionStringSettings connectionString4 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = strSQL4;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
					cmd.Parameters.Add(new SqlParameter("@SWC004", "暫停審查"));
                    cmd.Parameters.Add(new SqlParameter("@SWC089", TXTONA003.Text));
                    
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            //GBC.RecordTrunHistory(sSWC000, sSWC002, sONA02001, "申請中", ssUserID, "", "");
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA02001, "核准", ssUserID, "", "");
			SendMailNotice(sSWC000);

            Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
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
		
        string ChkMailGroup1 = arrayChkUserMsg[6] + "";
        string ChkMailGroup2 = arrayChkUserMsg[7] + "";
        string ChkMailGroup3 = arrayChkUserMsg[8] + "";

        //TextBox1.Text = strUserName;
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);
		
        string[] arrayMailGroup1 = ChkMailGroup1.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMailGroup2 = ChkMailGroup2.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMailGroup3 = ChkMailGroup3.Split(new string[] { ";;" }, StringSplitOptions.None);

        //送出提醒名單：承辦人、主管（科長，正工，股長，系統管理員）、審查公會
        //2022-05-06 送出提醒名單：股長、承辦人、審查階段通知收信窗口群組、審查公會

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC022, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
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
                string tSWC022 = readeSwc["SWC022"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];
					string aMailGroup2 = arrayMailGroup2[i];

                    if (aJobTitle.Trim() == "股長" || aUserName.Trim() == tSWC025.Trim() || aMailGroup2 == "Y" || aUserName.Trim() == tSWC022.Trim())
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = aUserName + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增暫停審查申請";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增暫停審查申請，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增暫停審查申請";
                //string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增暫停審查申請，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
	protected bool chkOA002(string v)
    {
        bool re = false;
        string strSQL = " select * from OnlineApply02 where SWC000 = @SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = strSQL;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                #endregion
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        re = true;
                }
                cmd.Cancel();
            }
        }
        return re;
    }
}