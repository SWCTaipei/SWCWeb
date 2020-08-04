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
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
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

        string sSWC002 = TXTSWC002.Text;
        string sONA01001 = LBONA001.Text;
        string sONA01002 = TXTONA002.Text;
        string sONA01003 = TXTONA003.Text;
        string sONA01004 = TXTONA004.Text;
        string sONA01005 = TXTONA005.Text;
        string sONA01006 = TXTONA006.Text;
        string sONA01007 = TXTONA007.Text;
        string sONA01025 = TXTONA025.Text;

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
            sEXESQLUPD = sEXESQLUPD + " ONA01025 = '" + sONA01025 + "', ";
            if (ssUserID == "") { sEXESQLUPD = sEXESQLUPD + " DATALOCK = 'Y', "; }
            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA01001 = '" + sONA01001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string sSWC000 = LBSWC000.Text + "";

            if (ssUserID == "")
            {
                SendMailNotice(sSWC000);
                Response.Write("<script>alert('資料已存檔');location.href='OnlineApply001v.aspx?SWCNO=" + sSWC000 + "&OACode="+ sONA01001 + "&UA=over';</script>");
            }
            else
            {
                string thisPageAct = ((Button)sender).ID + "";

                switch (thisPageAct)
                {
                    case "SaveCase":
                        Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                        break;
                }
            }
        }
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text;
        string sONA01001 = LBONA001.Text + "";

        string sEXESQLSTR = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply01 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA01001 = '" + sONA01001 + "' ";

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
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply001v.aspx?SWCNO=" + sSWC000 + "&OACode=" + sONA01001 + "'; </script>");
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
                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserName.Trim() == (Session["TempMailSWC107"]+"").Trim())
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