using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply003 : System.Web.UI.Page
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
        string rPrevious = Request.QueryString["PV"] + "";

        if (!IsPostBack)
        {
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
                string LINK = "OnlineApply003v.aspx?PV="+ rPrevious + "&SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
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
                            GetOLA03Data(rSWCNO, rOLANO);
                        }
                        break;
                    default:
                        Response.Redirect("SWC000.aspx");
                        break;
                }
				if(ssUserType == "08") DataLock.Visible = false;
            }
            C20.swcLogRC("OnlineApply003", "開工/復工展延申請", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
        }
        else { if (ssUserName == "") { Response.Redirect("SWC001.aspx"); } }
        

        //全區供用
        SBApp.ViewRecord("開工/復工展延", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用
    }
	
	protected void Page_LoadComplete(object sender, EventArgs e) 
    {
        //判斷顯示開工或復工
        LBSWC004_1.Text = LBSWC004.Text;
        LBSWC004_2.Text = LBSWC004.Text;
        LBSWC004_3.Text = LBSWC004.Text;
    }

    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }
    private void GetOLA03Data(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();
        //GenerateDropDownList();

        string tDATALOCK = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = @SWC000 ";
			
            string tSWC004 = "";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            objCmdSwc.Parameters.Add(new SqlParameter("SWC000", v));
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC000 = readeSwc["SWC000"] + "";
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC082 = readeSwc["SWC082"] + "";
                string tSWC084 = readeSwc["SWC084"] + "";
                tSWC004 = readeSwc["SWC004"] + "";
                
                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
                if (tSWC004 == "已核定") 
                {
                    LBSWC082_LBSWC084.Text = SBApp.DateView(tSWC082, "00");
                    TXTONA002.Text = SBApp.DateView(tSWC082, "00");
                    TXTONA003.Text = SBApp.DateView(Convert.ToDateTime(tSWC082).AddMonths(6).ToString(), "00");
                }
                if (tSWC004 == "停工中")
                {
                    LBSWC082_LBSWC084.Text = SBApp.DateView(tSWC084, "00");
                    TXTONA002.Text = SBApp.DateView(tSWC084, "00");
                    TXTONA003.Text = SBApp.DateView(Convert.ToDateTime(tSWC084).AddMonths(6).ToString(), "00");
                }
				
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
                string rONA000 = GetONAID();
                TXTONA001.Text = rONA000;
				if (tSWC004 == "已核定")
				{
					LBSWC004.Text = "開工";
					TXTONA004.Text = GetExtendNo(v,"1");
					if(Convert.ToInt32(TXTONA004.Text) > 2)
					{
						Response.Write("<script>alert('您好，"+LBSWC004.Text+"展延以二次為限，本案已達次數上限，無法辦理展延。'); location.href='SWC003.aspx?SWCNO="+v+"'; </script>");
					}
				}
                if (tSWC004 == "停工中")
				{
					LBSWC004.Text = "復工";
					TXTONA004.Text = GetExtendNo(v,"2");
					if(Convert.ToInt32(TXTONA004.Text) > 2)
					{
						Response.Write("<script>alert('您好，"+LBSWC004.Text+"展延以二次為限，本案已達次數上限，無法辦理展延。'); location.href='SWC003.aspx?SWCNO="+v+"'; </script>");
					}
				}
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply03 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' and ONA03001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA03002"] + "";
                    string tONA003 = readeONA["ONA03003"] + "";
                    string tONA004 = readeONA["ONA03004"] + "";
                    string tONA005 = readeONA["ONA03005"] + "";
                    string tONA006 = readeONA["ONA03006"] + "";
                    string tONA007 = readeONA["ONA03007"] + "";
                    string tONA008 = readeONA["ONA03008"] + "";
                    string tONA009 = readeONA["ONA03009"] + "";
                    string tONA010 = readeONA["ONA03010"] + "";
                    string tONA011 = readeONA["ONA03011"] + "";
                    tDATALOCK = readeONA["DATALOCK"] + "";

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
					LBSWC082_LBSWC084.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = SBApp.DateView(tONA003, "00");
                    //DDLONA004.SelectedValue = tONA004;
                    TXTONA005.Text = tONA005;
                    if (tONA007 == "1") { CHKONA007.Checked = true; }
                    if (tONA008 == "1") { CHKONA008.Checked = true; }
                    if (tONA009 == "1") { CHKONA009.Checked = true; }
                    if (tONA010 == "1") { CHKONA010.Checked = true; }
                    LBSWC004.Text = tONA011 == "" ? "開工/復工" : tONA011;
                }
            }
        }
        if (tDATALOCK == "Y")
            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply003v.aspx?SWCNO=" + v + "&OLANO=" + v2 + "'; </script>");
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    protected void GenerateDropDownList()
    {
        //string[] array_DTL006 = new string[] { "1", "2" };
        //DDLONA004.DataSource = array_DTL006;
        //DDLONA004.DataBind();
        
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        string rPrevious = Request.QueryString["PV"] + "";

        if (rPrevious=="4") { Response.Redirect("SWC004.aspx?SWCNO=" + vCaseID); } else { Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID); }
        
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string rPrevious = Request.QueryString["PV"] + "";

        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA03001 = TXTONA001.Text + "";
        string sONA03002 = TXTONA002.Text + "";
        string sONA03003 = TXTONA003.Text + "";
        //string sONA03004 = DDLONA004.SelectedValue;
		string sONA03004 = TXTONA004.Text + "";
        string sONA03005 = TXTONA005.Text + "";
        string sONA03007 = CHKONA007.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA03008 = CHKONA008.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA03009 = CHKONA009.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sONA03010 = CHKONA010.Checked.ToString().Replace("False", "").Replace("True", "1");
		string sONA03011 = LBSWC004.Text + "";

        string sEXESQLUPD = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply03 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + " and ONA03001 = '" + sONA03001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply03 (SWC000,SWC002,ONA03001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA03001 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply03 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA03002 = '" + sONA03002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03003 = '" + sONA03003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03004 = '" + sONA03004 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03005 = '" + sONA03005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03007 = '" + sONA03007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03008 = '" + sONA03008 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03009 = '" + sONA03009 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03010 = '" + sONA03010 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA03011 = '" + sONA03011 + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA03001 = '" + sONA03001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    if (rPrevious == "4") { Response.Write("<script>alert('資料已存檔');location.href='SWC004.aspx?SWCNO=" + sSWC000 + "';</script>"); } else { Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>"); }
                    break;
            }
        }
    }
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA03" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA03" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA03001) AS MAXID from OnlineApply03 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA03001,9) = '" + tempVal + "' ";

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

    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string rPrevious = Request.QueryString["PV"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC005 = LBSWC005.Text;
        string sSWC025 = LBSWC025.Text;
        string sONA03001 = TXTONA001.Text + "";
        string sONA03011 = LBSWC004.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply03 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA03001 = '" + sONA03001 + "' ";

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
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply003v.aspx?PV="+ rPrevious + "SWCNO=" + sSWC000 + "&OLANO=" + sONA03001 + "'; </script>");
                        return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            sEXESQLSTR += " Update OnlineApply03 Set ";
            sEXESQLSTR += "  DATALOCK2 = 'Y', ";
            sEXESQLSTR += "  LOCKUSER2 = '', ";
            sEXESQLSTR += "  ReviewResults = '1', ";
            sEXESQLSTR += "  ReviewDoc = '', ";
            sEXESQLSTR += "  ResultsExplain = '', ";
            sEXESQLSTR += "  ReviewDirections = '', ";
            sEXESQLSTR += "  ReSendDeadline = null, ";
            sEXESQLSTR += "  DATALOCK = 'Y', ";
            sEXESQLSTR += " SWC005 = @SWC005, ";
            sEXESQLSTR += " SWC025 = @SWC025, ";
            //sEXESQLSTR += " SING002 = @SING002, ";
            //sEXESQLSTR += " SING004 = @SING004, ";
            //sEXESQLSTR += " SING006 = @SING006, ";
            //sEXESQLSTR += " SING007 = N'送出', ";
            sEXESQLSTR += " SING007 = N'結案', ";
            sEXESQLSTR += " SING008 = N'結案', ";
            sEXESQLSTR += " TMPSN01 = N'准', ";
            sEXESQLSTR += " ONAHEAD=@ONAHEAD, ";
            //sEXESQLSTR += " SING008 = N'待簽辦', ";
            sEXESQLSTR += " LOCKUSER = @LOCKUSER, ";
            sEXESQLSTR += " LOCKDATE = getdate() ";
            sEXESQLSTR += " Where SWC000 = @SWC000 ";
            sEXESQLSTR += " and ONA03001 = @ONA03001";

            SqlCommand objCmdSwc1 = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdSwc1.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
            objCmdSwc1.Parameters.Add(new SqlParameter("@ONA03001", sONA03001));
			
            objCmdSwc1.Parameters.Add(new SqlParameter("@SWC005", sSWC005));
            objCmdSwc1.Parameters.Add(new SqlParameter("@SWC025", sSWC025));
            //objCmdSwc1.Parameters.Add(new SqlParameter("@SING002", sSWC025));
            //objCmdSwc1.Parameters.Add(new SqlParameter("@SING004", sSWC025+";"));
            //objCmdSwc1.Parameters.Add(new SqlParameter("@SING006", ssUserName));
            objCmdSwc1.Parameters.Add(new SqlParameter("@ONAHEAD", "水土保持計畫" + sONA03011 + "期限展延"));
            objCmdSwc1.Parameters.Add(new SqlParameter("@LOCKUSER", ssUserID));
            objCmdSwc1.ExecuteNonQuery();
            objCmdSwc1.Dispose();

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
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA03001));
                    cmd.Parameters.Add(new SqlParameter("@R001", ""));
                    cmd.Parameters.Add(new SqlParameter("@R002", ""));
                    cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                    //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                    cmd.Parameters.Add(new SqlParameter("@R005", ""));
                    cmd.Parameters.Add(new SqlParameter("@R006", ""));
                    cmd.Parameters.Add(new SqlParameter("@R007", SBApp.GetETUser(ssUserID, "OrgName")));
                    cmd.Parameters.Add(new SqlParameter("@R008", SBApp.GetETUser(ssUserID, "ETIdentity")));
                    cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/HHmm")));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            string sqlstr = "";
			if(LBSWC004.Text == "開工")
				sqlstr = "update swcswc set SWC82=@SWC082SWC084,SWC83=@SWC083SWC092 Where SWC00=@SWC000; update tcgeswc.dbo.swccase set SWC082=@SWC082SWC084,SWC083=@SWC083SWC092 Where SWC000=@SWC000; ";
			if(LBSWC004.Text == "復工")
				sqlstr = "update swcswc set SWC84=@SWC082SWC084,SWC92=@SWC083SWC092 Where SWC00=@SWC000; update tcgeswc.dbo.swccase set SWC084=@SWC082SWC084,SWC092=@SWC083SWC092 Where SWC000=@SWC000; ";
			
			ConnectionStringSettings connectionString3 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString3.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = sqlstr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC082SWC084", TXTONA003.Text));
                    cmd.Parameters.Add(new SqlParameter("@SWC083SWC092", Convert.ToInt32(TXTONA004.Text)));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
			
            //SBApp.RecordTrunHistory(sSWC000, sSWC002, sONA03001, "申請中", ssUserID, "", "");
			SBApp.RecordTrunHistory(sSWC000, sSWC002, sONA03001, "申請且核准", ssUserID, "", "");
            //SendMailNotice(sSWC000);

            if (rPrevious == "4") { Response.Write("<script>alert('資料已送出。');location.href='SWC004.aspx?SWCNO=" + sSWC000 + "';</script>"); } else { Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>"); }
            
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

        //送出提醒名單：承辦人、主管（科長，正工，股長，系統管理員）、審查公會

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
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
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄件名單
                //寫死名單：章姿隆  ge-10706@mail.taipei.gov.tw

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
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;

                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增" + LBSWC004.Text + "展延申請";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增" + LBSWC004.Text + "展延申請，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增開工/復工展延申請";
                //string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增開工/復工展延申請，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
	private string GetExtendNo(string v,string type)
    {
		string _ReturnVal = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
		
            string strSQLRV = " select ISNULL(SWC083,0)+1 SWC083ADD1,ISNULL(SWC092,0)+1 SWC092ADD1 from SWCCASE ";
            strSQLRV = strSQLRV + "   where SWC000 = '" + v + "' ";
		
            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();
		
            while (readerSWC.Read())
            {
                if(type == "1") _ReturnVal = readerSWC["SWC083ADD1"].ToString()+"";
                if(type == "2") _ReturnVal = readerSWC["SWC092ADD1"].ToString()+"";
            }
		
        }
        return _ReturnVal;
    }
}