using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply001 : System.Web.UI.Page
{
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
        string ssJobTitle = Session["JobTitle"] + "";
		string ssUserType = Session["UserType"] + "";
        string rUA = Request.QueryString["UA"] + "";

        GBClass001 SBApp = new GBClass001();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rONA000 = Request.QueryString["OACode"] + "";
        string rBTNCTL = Request.QueryString["PRS"] + "";

        //測試用變數設定
        //rONA000 = "TEST10704001";

        if (!IsPostBack)
        {
            Data2Page(rCaseId, rONA000);
        }
		/*
        if (ssUserName == "")
        {
        }
        else
        {
            LBSWC000.Text = rCaseId;
            DataLock.Visible = true;
            SaveCase.Text = "暫時儲存";
            GoHomePage.Visible = true;
        }
		*/
		if (ssUserType == "")
        {
			LBSWC000.Text = rCaseId;
			DataLock.Visible = true;
			SaveCase.Visible = false;
            GoHomePage.Visible = false;
        }
        else
        {
            LBSWC000.Text = rCaseId;
            DataLock.Visible = true;
            SaveCase.Text = "暫時儲存";
            GoHomePage.Visible = true;
        }


        //全區供用
        SBApp.ViewRecord("臺北市山坡地水土保持設施安全自主檢查表", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        //全區供用
    }

    private void Data2Page(string rCaseId, string rONA000)
    {
        string rUA = Request.QueryString["UA"] + "";

        GBClass001 SBApp = new GBClass001();
        
        if (rONA000 == "" || rONA000 == "ADDNEW")
        {
            rONA000 = GetONAID();
            LBONA001.Text = rONA000;

            if (rUA == "") { } else
            {
                TXTSWC002.Text = rUA;
                LBSWC002.Text = rUA;
                TXTSWC002.Visible = false;
                LBSWC002.Visible = true;
            }
        }
        else
        {
            LBONA001.Text = rONA000;

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from OnlineApply01 ";
                strSQLRV = strSQLRV + "   where ONA01001 = '" + rONA000 + "' ";

                SqlDataReader readerOA1;
                SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
                readerOA1 = objCmdSWC.ExecuteReader();

                while (readerOA1.Read())
                {
                    string rSWC002 = readerOA1["SWC002"] + "";
                    string rONA01001 = readerOA1["ONA01001"] + ""; 
                    string rONA01002 = readerOA1["ONA01002"] + "";
                    string rONA01003 = readerOA1["ONA01003"] + "";
                    string rONA01004 = readerOA1["ONA01004"] + "";
                    string rONA01005 = readerOA1["ONA01005"] + "";
                    string rONA01006 = readerOA1["ONA01006"] + "";
                    string rONA01007 = readerOA1["ONA01007"] + "";
                    string rONA01008 = readerOA1["ONA01008"] + "";
                    string rONA01009 = readerOA1["ONA01009"] + "";
                    string rONA01010 = readerOA1["ONA01010"] + "";
                    string rONA01011 = readerOA1["ONA01011"] + "";
                    string rONA01012 = readerOA1["ONA01012"] + "";
                    string rONA01013 = readerOA1["ONA01013"] + "";
                    string rONA01014 = readerOA1["ONA01014"] + "";
                    string rONA01015 = readerOA1["ONA01015"] + "";
                    string rONA01016 = readerOA1["ONA01016"] + "";
                    string rONA01017 = readerOA1["ONA01017"] + "";
                    string rONA01018 = readerOA1["ONA01018"] + "";
                    string rONA01019 = readerOA1["ONA01019"] + "";
                    string rONA01020 = readerOA1["ONA01020"] + "";
                    string rONA01021 = readerOA1["ONA01021"] + "";
                    string rONA01022 = readerOA1["ONA01022"] + "";
                    string rONA01023 = readerOA1["ONA01023"] + "";
                    string rONA01024 = readerOA1["ONA01024"] + "";
                    string rONA01025 = readerOA1["ONA01025"] + "";
                    string rONA01026 = readerOA1["ONA01026"] + "";
                    string rONA01027 = readerOA1["ONA01027"] + "";
                    string rONA01028 = readerOA1["ONA01028"] + "";
					string rDATALOCK = readerOA1["DATALOCK"] + "";
					if(rDATALOCK=="Y")
						Response.Write("<script>alert('目前僅供瀏覽。');location.href='OnlineApply001v.aspx?OACode=" + rONA000 + "';</script>");

                    TXTSWC002.Text = rSWC002;
                    LBONA001.Text  = rONA01001;
                    TXTONA002.Text = SBApp.DateView(rONA01002, "00");
                    TXTONA003.Text = rONA01003;
                    TXTONA004.Text = rONA01004;
                    TXTONA005.Text = rONA01005;
                    TXTONA006.Text = rONA01006;
                    TXTONA007.Text = rONA01007;
                    TXTONA022.Text = rONA01022;
                    TXTONA024.Text = SBApp.DateView(rONA01024, "00");
                    TXTONA025.Text = rONA01025;
                    TXTONA026.Text = rONA01026;
                    TXTONA027.Text = rONA01027;
                    TXTONA028.Text = rONA01028;
                    TXTONA009aD.Text = readerOA1["ONA01009aD"] + "";
                    TXTONA009bD.Text = readerOA1["ONA01009bD"] + "";
                    TXTONA010aD.Text = readerOA1["ONA01010aD"] + "";
                    TXTONA010bD.Text = readerOA1["ONA01010bD"] + "";
                    TXTONA012aD.Text = readerOA1["ONA01012aD"] + "";
                    TXTONA012bD.Text = readerOA1["ONA01012bD"] + "";
                    TXTONA013aD.Text = readerOA1["ONA01013aD"] + "";
                    TXTONA013bD.Text = readerOA1["ONA01013bD"] + "";
                    TXTONA015aD.Text = readerOA1["ONA01015aD"] + "";
                    TXTONA015bD.Text = readerOA1["ONA01015bD"] + "";
                    TXTONA016aD.Text = readerOA1["ONA01016aD"] + "";
                    TXTONA016bD.Text = readerOA1["ONA01016bD"] + "";
                    TXTONA017aD.Text = readerOA1["ONA01017aD"] + "";
                    TXTONA017bD.Text = readerOA1["ONA01017bD"] + "";
                    TXTONA019aD.Text = readerOA1["ONA01019aD"] + "";
                    TXTONA019bD.Text = readerOA1["ONA01019bD"] + "";
                    TXTONA020aD.Text = readerOA1["ONA01020aD"] + "";
                    TXTONA020bD.Text = readerOA1["ONA01020bD"] + "";
                    TXTONA023bD.Text = readerOA1["ONA01023bD"] + "";

                    //點選處理
                    string[] arrayRadioValue = new string[] { rONA01008, rONA01009, rONA01010, rONA01011, rONA01012, rONA01013, rONA01014, rONA01015, rONA01016, rONA01017, rONA01018, rONA01019, rONA01020, rONA01021, rONA01023 };
                    System.Web.UI.WebControls.RadioButton[] arrayRadioA = new System.Web.UI.WebControls.RadioButton[] { RaONA008a, RaONA009a, RaONA010a, RaONA011a, RaONA012a, RaONA013a, RaONA014a, RaONA015a, RaONA016a, RaONA017a, RaONA018a, RaONA019a, RaONA020a, RaONA021a, RaONA023a };
                    System.Web.UI.WebControls.RadioButton[] arrayRadioB = new System.Web.UI.WebControls.RadioButton[] { RaONA008b, RaONA009b, RaONA010b, RaONA011b, RaONA012b, RaONA013b, RaONA014b, RaONA015b, RaONA016b, RaONA017b, RaONA018b, RaONA019b, RaONA020b, RaONA021b, RaONA023b };

                    for (int i = 0; i < arrayRadioValue.Length; i++)
                    {
                        string aValue = arrayRadioValue[i];
                        System.Web.UI.WebControls.RadioButton aRadioA = arrayRadioA[i];
                        System.Web.UI.WebControls.RadioButton aRadioB = arrayRadioB[i];

                        switch (aValue)
                        {
                            case "1":
                                aRadioA.Checked = true;
                                break;
                            case "0":
                                aRadioB.Checked = true;
                                break;
                        }
                    }
                }
            }
        }
    }

    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA01" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA01" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA01001) AS MAXID from OnlineApply01 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA01001,9) = '" + tempVal + "' ";

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

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
		string ssUserType = Session["UserType"] + "";
        if (((Button)sender).ID + "" == "SaveCase")
			SaveThisCase();
        string sSWC002 = TXTSWC002.Text;
        string sONA01001 = LBONA001.Text;
        //string sONA01002 = TXTONA002.Text;
        string sONA01002 = Request.Form["TXTONA002"].Trim();
		string sONA01003 = TXTONA003.Text;
        string sONA01004 = TXTONA004.Text;
        string sONA01005 = TXTONA005.Text;
        string sONA01006 = TXTONA006.Text;
        string sONA01007 = TXTONA007.Text;
        string sONA01024 = TXTONA024.Text;
        string sONA01025 = TXTONA025.Text;
        string sONA01026 = TXTONA026.Text;

        if (sONA01026.Length > 250) sONA01026 = sONA01026.Substring(250);

        //點選處理
        string sONA01008 = "", sONA01009 = "", sONA01010 = "", sONA01011 = "", sONA01012 = "", sONA01013 = "", sONA01014 = "", sONA01015 = "", sONA01016 = "", sONA01017 = "", sONA01018 = "", sONA01019 = "", sONA01020 = "", sONA01021 = "", sONA0123 = "";
        string[] arrayRadioField = new string[] { "ONA01008", "ONA01009", "ONA01010", "ONA01011", "ONA01012", "ONA01013", "ONA01014", "ONA01015", "ONA01016", "ONA01017", "ONA01018", "ONA01019", "ONA01020", "ONA01021", "ONA01023" };
        string[] arrayRadioValue = new string[] { sONA01008, sONA01009, sONA01010, sONA01011, sONA01012, sONA01013, sONA01014, sONA01015, sONA01016, sONA01017, sONA01018, sONA01019, sONA01020, sONA01021, sONA0123 };
        System.Web.UI.WebControls.RadioButton[] arrayRadioA = new System.Web.UI.WebControls.RadioButton[] { RaONA008a, RaONA009a, RaONA010a, RaONA011a, RaONA012a, RaONA013a, RaONA014a, RaONA015a, RaONA016a, RaONA017a, RaONA018a, RaONA019a, RaONA020a, RaONA021a, RaONA023a };
        System.Web.UI.WebControls.RadioButton[] arrayRadioB = new System.Web.UI.WebControls.RadioButton[] { RaONA008b, RaONA009b, RaONA010b, RaONA011b, RaONA012b, RaONA013b, RaONA014b, RaONA015b, RaONA016b, RaONA017b, RaONA018b, RaONA019b, RaONA020b, RaONA021b, RaONA023b };

        for (int i = 0; i < arrayRadioValue.Length; i++)
        {
            string tRadioValue = "";
            string tRadioA = (arrayRadioA[i].Checked.CompareTo(true) + 1).ToString();
            string tRadioB = (arrayRadioB[i].Checked.CompareTo(true) + 1).ToString();
            if (tRadioA == "1") { tRadioValue = "1"; }
            if (tRadioB == "1") { tRadioValue = "0"; }

            //sONA01017，i=9 
            if (i==9)
            {
                string tRadioC = (RaONA017c.Checked.CompareTo(true) + 1).ToString();
                if (tRadioC == "1") { tRadioValue = "2"; }
            }
            
            arrayRadioValue[i] = tRadioValue;
        }
        

        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply01 ";  
            strSQLRV = strSQLRV + " where ONA01001 = '" + sONA01001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply01 (ONA01001) VALUES ('" + sONA01001 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply01 Set ";
            sEXESQLUPD = sEXESQLUPD + " SWC002 = '" + sSWC002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01002 = '" + sONA01002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01003 = '" + sONA01003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01004 = '" + sONA01004 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01005 = '" + sONA01005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01006 = '" + sONA01006 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01007 = '" + sONA01007 + "', ";
            for (int i = 0; i < arrayRadioValue.Length; i++)
            {
                string aField = arrayRadioField[i];
                string aValue = arrayRadioValue[i];
                sEXESQLUPD = sEXESQLUPD + aField + " = '" + aValue + "', ";
            }
            sEXESQLUPD = sEXESQLUPD + " ONA01024 = '" + sONA01024 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01025 = '" + sONA01025 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA01026 = '" + sONA01026 + "', ";
            //if (ssUserID == "") { sEXESQLUPD = sEXESQLUPD + " DATALOCK = 'Y', "; }
            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA01001 = '" + sONA01001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //string sSWC000 = LBSWC000.Text;//Requset.Form["LBSWC000"].Trim();
			/*
            if (ssUserID == "")
            {
                //SendMailNotice(sSWC000);
                //Response.Write("<script>alert('資料已存檔');location.href='OnlineApply001v.aspx?SWCNO=" + sSWC000 + "&OACode="+ sONA01001 + "&UA=over';</script>");
				Response.Write("<script>alert('資料已存檔');</script>");
			}
            else
            {
                string thisPageAct = ((Button)sender).ID + "";

                switch (thisPageAct)
                {
                    case "SaveCase":
                        //Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
						Response.Write("<script>alert('資料已存檔');</script>");
                        break;
                }
            }
			*/
			string thisPageAct = ((Button)sender).ID + "";
			switch (thisPageAct)
            {
                case "SaveCase":
					if(ssUserType == "")
						Response.Write("<script>alert('您的資料已經暫存成功，如需繼續編輯，請回到案件「基本資料」>「設施安全自主檢查表」點擊詳情編輯。');location.href='../Default.aspx';</script>");
					else
						Response.Write("<script>alert('您的資料已經暫存成功，如需繼續編輯，請回到案件「基本資料」>「設施安全自主檢查表」點擊詳情編輯。');location.href='SWC001.aspx';</script>");
                    break;
            }
        }
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
		string vCaseID = Request.QueryString["SWCNO"] + "";
		if(vCaseID != "")
			Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
		else
			Response.Redirect("HaloPage001.aspx");
    }

    private bool SaveThisCase() {
        Class1 C1 = new Class1();
        string sSWC002 = TXTSWC002.Text;
        string sONA01007 = TXTONA007.Text;
        //string sONA01002 = TXTONA002.Text;
		string sONA01002 = Request.Form["TXTONA002"].Trim();
        string sONA01024 = TXTONA024.Text;
        string sONA01027 = TXTONA027.Text;
        string sONA01028 = TXTONA028.Text;
        string sONA01009aD = TXTONA009aD.Text;
        string sONA01009bD = TXTONA009bD.Text;
        string sONA01010aD = TXTONA010aD.Text;
        string sONA01010bD = TXTONA010bD.Text;
        string sONA01012aD = TXTONA012aD.Text;
        string sONA01012bD = TXTONA012bD.Text;
        string sONA01013aD = TXTONA013aD.Text;
        string sONA01013bD = TXTONA013bD.Text;
        string sONA01015aD = TXTONA015aD.Text;
        string sONA01015bD = TXTONA015bD.Text;
        string sONA01016aD = TXTONA016aD.Text;
        string sONA01016bD = TXTONA016bD.Text;
        string sONA01017aD = TXTONA017aD.Text;
        string sONA01017bD = TXTONA017bD.Text;
        string sONA01019aD = TXTONA019aD.Text;
        string sONA01019bD = TXTONA019bD.Text;
        string sONA01020aD = TXTONA020aD.Text;
        string sONA01020bD = TXTONA020bD.Text;
        string sONA01023bD = TXTONA023bD.Text;
        if (!C1.chkDateFormat(sONA01002)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');</script>"); TXTONA002.Focus(); return false; }
        if (sONA01007.Trim() == "") { Response.Write("<script>alert('請輸入行動電話。');</script>"); TXTONA007.Focus(); return false; }
        if (!C1.chkDateFormat(sONA01024)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');</script>"); TXTONA024.Focus(); return false; }

        AddNewCase();
		//抓OA編號
        string sONA01001 = LBONA001.Text;
        //SaveCase_Click(null, null);
        string exeSQLstr = " Update OnlineApply01 Set SWC002=@SWC002,ONA01002=@ONA01002,ONA01027=@ONA01027,ONA01028=@ONA01028,ONA01009aD=@ONA01009aD,ONA01009bD=@ONA01009bD,ONA01010aD=@ONA01010aD,ONA01010bD=@ONA01010bD,ONA01012aD=@ONA01012aD,ONA01012bD=@ONA01012bD,ONA01013aD=@ONA01013aD,ONA01013bD=@ONA01013bD,ONA01015aD=@ONA01015aD,ONA01015bD=@ONA01015bD,ONA01016aD=@ONA01016aD,ONA01016bD=@ONA01016bD,ONA01017aD=@ONA01017aD,ONA01017bD=@ONA01017bD,ONA01019aD=@ONA01019aD,ONA01019bD=@ONA01019bD,ONA01020aD=@ONA01020aD,ONA01020bD=@ONA01020bD,ONA01023bD=@ONA01023bD Where ONA01001=@ONA01001 ;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = exeSQLstr;
                cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                cmd.Parameters.Add(new SqlParameter("@ONA01001", sONA01001));
                cmd.Parameters.Add(new SqlParameter("@ONA01002", sONA01002));
                cmd.Parameters.Add(new SqlParameter("@ONA01027", sONA01027));
                cmd.Parameters.Add(new SqlParameter("@ONA01028", sONA01028));
                cmd.Parameters.Add(new SqlParameter("@ONA01009aD", sONA01009aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01009bD", sONA01009bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01010aD", sONA01010aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01010bD", sONA01010bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01012aD", sONA01012aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01012bD", sONA01012bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01013aD", sONA01013aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01013bD", sONA01013bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01015aD", sONA01015aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01015bD", sONA01015bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01016aD", sONA01016aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01016bD", sONA01016bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01017aD", sONA01017aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01017bD", sONA01017bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01019aD", sONA01019aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01019bD", sONA01019bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01020aD", sONA01020aD));
                cmd.Parameters.Add(new SqlParameter("@ONA01020bD", sONA01020bD));
                cmd.Parameters.Add(new SqlParameter("@ONA01023bD", sONA01023bD));
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        return true;
    }

    private void AddNewCase()
    {
		LBONA001.Text = GetONAID();
        string vCaseId= LBONA001.Text;
		string vSWC002 = TXTSWC002.Text;
        string sEXESQLUPD = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            string strSQLRV = " select * from OnlineApply01 where ONA01001 = '" + vCaseId + "'; ";
            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();
			
            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply01 (ONA01001) VALUES ('" + vCaseId + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            if (sEXESQLUPD.Trim()!="") { 
            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();}
        }
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
		string ssUserType = Session["UserType"] + "";
        if (SaveThisCase()) { }


        //string sSWC000 = LBSWC000.Text;//Requset.Form["LBSWC000"].Trim();
        string sONA01001 = LBONA001.Text + "";

        string sEXESQLSTR = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply01 ";
            //strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            //strSQLRV = strSQLRV + "   and ONA01001 = '" + sONA01001 + "' ";
			strSQLRV = strSQLRV + "  Where ONA01001 = '" + sONA01001 + "' ";

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
                        //Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply001v.aspx?SWCNO=" + sSWC000 + "&OACode=" + sONA01001 + "'; </script>");
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');</script>");
						return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply01 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKUSER = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKDATE = getdate() ";
            //sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + " Where ONA01001 = '" + sONA01001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //SendMailNotice(sSWC000);

            //Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
			if(ssUserType == "")
				Response.Write("<script>alert('您的「設施安全自主檢查表」已經填寫完成，如需查看表單內容，可於登入書件管理平台後，到案件「基本資料」>「設施安全自主檢查表」點擊詳情查看。');location.href='../Default.aspx'</script>");
			else
				Response.Write("<script>alert('您的「設施安全自主檢查表」已經填寫完成，如需查看表單內容，可於登入書件管理平台後，到案件「基本資料」>「設施安全自主檢查表」點擊詳情查看。');location.href='SWC001.aspx'</script>");
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

        //送出提醒名單：承辦人、主管（科長，正工，股長，系統管理員）、已完工公會

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

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserName.Trim() == (Session["TempMailSWC107"]+"").Trim() || aUserId.Trim() == "ge-10706")
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = aUserName + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增已完工水土保持設施自主檢查表";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增已完工水土保持設施自主檢查表，請上管理平台查看" + "<br><br>";
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
}