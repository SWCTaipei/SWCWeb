using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

        switch (ssUserType)
        {
            case "01":
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            case "02":
                TitleLink00.Visible = true;
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            case "03":  //大地人員
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                if (!IsPostBack) { /*GenerateDropDownList();*/ }
                break;
            case "04":
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            default:
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
        }



        //以下全區公用
        SBApp.ViewRecord("防災事件通知", "view", "");

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
					
                    string sDEDISTRICT = readerDN["DEDISTRICT"] + "";
                    string sSWCSTATUS = readerDN["SWCSTATUS"] + "";
                    string sSENDMBR = readerDN["SENDMBR"] + "";
                    string sSENDFUN = readerDN["SENDFUN"] + "";
                    string sSWCSTATUS_ILG = readerDN["ILGSTATUS"] + "";
                    string sSENDMBR_ILG = readerDN["ILGSENDMBR"] + "";
                    string sSENDFUN_ILG = readerDN["ILGSENDFUN"] + "";
					
                    string sDESENDCOUNT = readerDN["DESENDCOUNT"] + "";
					St05.Text = sDESENDCOUNT;
					
                    string tDETYPEDESC = "";
                    switch (sDETYPE) {
                        case "0104":
                            tDETYPEDESC = "颱風豪雨通知回傳自主檢查表";
                            break;
                        case "0203":
                            tDETYPEDESC = "違規案件防災整備通知";
                            break;
                    }

                    TXTDENO.Text = sDENo;
                    TXTDENAME.Text = sDENAME;
                    TXTDENTTEXT.Text = sDENTTEXT;
                    DDLDETYPE.Text = tDETYPEDESC;
                    CHKSWCSTATUS.Text = sSWCSTATUS.Replace(","," ");
                    CHKSENDMBR.Text = sSENDMBR.Replace(",", " ");
                    CHKSENDFUN.Text = sSENDFUN.Replace(",", " ");
					
					LBDISTRICT.Text = sDEDISTRICT.Replace(",", " ");
					LBSWCSTATUS.Text = sSWCSTATUS.Replace(",", " ");
					LBSENDMBR.Text = sSENDMBR.Replace(",", " ");
					LBSENDFUN.Text = sSENDFUN.Replace(",", " ");
					LBSWCSTATUS_ILG.Text = sSWCSTATUS_ILG.Replace(",", " ");
					LBSENDMBR_ILG.Text = sSENDMBR_ILG.Replace(",", " ");
					LBSENDFUN_ILG.Text = sSENDFUN_ILG.Replace(",", " ");
                }
                readerDN.Close();
                objCmdDN.Dispose();
            }

            //統計資料
            string tmpSQL01 = " select count(*) as dC from SWCDTL04 where DTLD085 = '" + rCaseID+ "' and DTLD004='整備完成' and DATALOCK = 'Y'; ";
            string tmpSQL02 = " select count(*) as dC from SWCDTL04 where DTLD085 = '" + rCaseID + "' and isnull(DTLD004,'待改善')='待改善' and DATALOCK = 'Y'; ";
            string tmpSQL03 = " select count(*) as dC from SWCDTL04 where DTLD085 = '" + rCaseID + "' and DATALOCK = 'Y'; ";
            string tmpSQL04 = " select count(*) as dC from SWCDTL04 where DTLD085 = '" + rCaseID + "' and isnull(DATALOCK,'')!='Y'; ";
            string tmpSQL05 = " select count(*) as dC from SWCDTL04 where DTLD085 = '" + rCaseID + "' ; ";
            string tmpSQL06 = " select count(*) as dC from SWCDTL04 where DTLD085 = '" + rCaseID + "' ; ";


            using (SqlConnection DTLConn = new SqlConnection(connectionStringSwc.ConnectionString))
            {
                DTLConn.Open();

                //整備完成
                SqlDataReader readerDN;
                SqlCommand objCmdDN = new SqlCommand(tmpSQL01, DTLConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                {
                    string tCount = readerDN["dC"] + "";

                    St01.Text = tCount;
                }
                readerDN.Close();
                objCmdDN.Dispose();

                //未整備完成
                objCmdDN = new SqlCommand(tmpSQL02, DTLConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                {
                    string tCount = readerDN["dC"] + "";

                    St02.Text = tCount;
                }
                readerDN.Close();
                objCmdDN.Dispose();

                //已送出
                objCmdDN = new SqlCommand(tmpSQL03, DTLConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                {
                    string tCount = readerDN["dC"] + "";

                    St03.Text = tCount;
                }
                readerDN.Close();
                objCmdDN.Dispose();

                //未送出
                objCmdDN = new SqlCommand(tmpSQL04, DTLConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                {
                    string tCount = readerDN["dC"] + "";

                    St04.Text = tCount;
                }
                readerDN.Close();
                objCmdDN.Dispose();

                //發送筆數
                objCmdDN = new SqlCommand(tmpSQL05, DTLConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                {
                    string tCount = readerDN["dC"] + "";

                    //St05.Text = tCount;
                }
                readerDN.Close();
                objCmdDN.Dispose();

                //事件筆數
                objCmdDN = new SqlCommand(tmpSQL06, DTLConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                {
                    string tCount = readerDN["dC"] + "";

                    St06.Text = tCount;
                }
                readerDN.Close();
                objCmdDN.Dispose();
            }


            //表4
            using (SqlConnection DTLConn = new SqlConnection(connectionStringSwc.ConnectionString))
            {
                DTLConn.Open();

                string Sql04Str = "";

                Sql04Str = Sql04Str + " Select C.SWC002 AS DTLD001,C.SWC005 AS DTLD002,C.SWC013 AS DTLD003,U.ETNAME AS DTLD004 ,ISNULL(D4.DATALOCK,'RED') AS DTLD005,C.SWC000 AS DTLD006, DTLD001 as DTLD007  From SWCDTL04 D4 ";
                Sql04Str += " LEFT JOIN SWCCASE C ON D4.SWC000=C.SWC000 ";
                Sql04Str += " LEFT JOIN ETUsers U ON C.SWC045ID=U.ETID ";
                Sql04Str = Sql04Str + "  Where DTLD085 = '" + rCaseID + "' ";
                Sql04Str = Sql04Str + "  order by C.SWC002 ";

                SqlDataReader readerItem04;
                SqlCommand objCmdItem04 = new SqlCommand(Sql04Str, DTLConn);
                readerItem04 = objCmdItem04.ExecuteReader();

                while (readerItem04.Read())
                {
                    string dDTLD01 = readerItem04["DTLD001"] + "";
                    string dDTLD02 = readerItem04["DTLD002"] + "";
                    string dDTLD03 = readerItem04["DTLD003"] + "";
                    string dDTLD04 = readerItem04["DTLD004"] + "";
                    string dDTLD05 = readerItem04["DTLD005"] + "";
                    string dDTLD06 = readerItem04["DTLD006"] + "";
                    string dDTLD07 = readerItem04["DTLD007"] + "";
                    string dUrl = "";

                    if (dDTLD05 == "RED") { dUrl = "../images/icon/red.png"; }

                    DataTable OBJ_GV04 = (DataTable)ViewState["GV04"];
                    DataTable DTGV04 = new DataTable();

                    if (OBJ_GV04 == null)
                    {
                        DTGV04.Columns.Add(new DataColumn("DTLD001", typeof(string)));
                        DTGV04.Columns.Add(new DataColumn("DTLD002", typeof(string)));
                        DTGV04.Columns.Add(new DataColumn("DTLD003", typeof(string)));
                        DTGV04.Columns.Add(new DataColumn("DTLD004", typeof(string)));
                        DTGV04.Columns.Add(new DataColumn("DTLD005", typeof(string)));
                        DTGV04.Columns.Add(new DataColumn("DTLD006", typeof(string)));
                        DTGV04.Columns.Add(new DataColumn("DTLD007", typeof(string)));

                        ViewState["GV04"] = DTGV04;
                        OBJ_GV04 = DTGV04;
                    }
                    DataRow dr04 = OBJ_GV04.NewRow();

                    dr04["DTLD001"] = dDTLD01;
                    dr04["DTLD002"] = dDTLD02;
                    dr04["DTLD003"] = dDTLD03;
                    dr04["DTLD004"] = dDTLD04;
                    dr04["DTLD005"] = dUrl;
                    dr04["DTLD006"] = dDTLD06;
                    dr04["DTLD007"] = dDTLD07;

                    OBJ_GV04.Rows.Add(dr04);

                    ViewState["GV04"] = OBJ_GV04;

                    SWCDTL04.DataSource = OBJ_GV04;
                    SWCDTL04.DataBind();
                }
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
        string MailStr = "geocheck@geovector.com.tw;;";

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


    protected void ButtonDTL04_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        GridViewRow gvr = (GridViewRow)LButton.NamingContainer;
        int rowindex = gvr.RowIndex;
        string view = "v";

        HiddenField hiddenDate01 = (HiddenField)SWCDTL04.Rows[rowindex].Cells[5].FindControl("HiddenField1");
        HiddenField hiddenDate02 = (HiddenField)SWCDTL04.Rows[rowindex].Cells[5].FindControl("HiddenField2");
        HiddenField hiddenDate03 = (HiddenField)SWCDTL04.Rows[rowindex].Cells[5].FindControl("HiddenField3");

        string tmpValue1 = hiddenDate01.Value;
        string tmpValue2 = hiddenDate02.Value;
        string tmpValue3 = hiddenDate03.Value;
        
        string gogoPage = "../SWCDOC/SWCDT004"+ view + ".aspx?SWCNO="+ tmpValue1 + "&DTLNO="+ tmpValue2+"&"+ tmpValue3;

        Response.Write("<script>window.open('" + gogoPage + "','_blank')</script>");
    }

    protected void ShowRed_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        string rCaseID = Request.QueryString["DisEventId"] + "";

        //表4
        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection DTLConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            DTLConn.Open();

            string Sql04Str = "";

            Sql04Str = Sql04Str + " Select C.SWC002 AS DTLD001,C.SWC005 AS DTLD002,C.SWC013 AS DTLD003,U.ETNAME AS DTLD004 ,ISNULL(D4.DATALOCK,'RED') AS DTLD005,C.SWC000 AS DTLD006, DTLD001 as DTLD007  From SWCDTL04 D4 ";
            Sql04Str += " LEFT JOIN SWCCASE C ON D4.SWC000=C.SWC000 ";
            Sql04Str += " LEFT JOIN ETUsers U ON C.SWC045ID=U.ETID ";
            Sql04Str = Sql04Str + "  Where DTLD085 = '" + rCaseID + "' ";
            Sql04Str += " and ISNULL(D4.DATALOCK,'')<>'Y'";
            Sql04Str = Sql04Str + "  order by C.SWC002 ";

            SqlDataReader readerItem04;
            SqlCommand objCmdItem04 = new SqlCommand(Sql04Str, DTLConn);
            readerItem04 = objCmdItem04.ExecuteReader();

            ViewState["GV04"] = null;

            while (readerItem04.Read())
            {
                string dDTLD01 = readerItem04["DTLD001"] + "";
                string dDTLD02 = readerItem04["DTLD002"] + "";
                string dDTLD03 = readerItem04["DTLD003"] + "";
                string dDTLD04 = readerItem04["DTLD004"] + "";
                string dDTLD05 = readerItem04["DTLD005"] + "";
                string dDTLD06 = readerItem04["DTLD006"] + "";
                string dDTLD07 = readerItem04["DTLD007"] + "";
                string dUrl = "";

                if (dDTLD05 == "RED") { dUrl = "../images/icon/red.png"; }

                DataTable OBJ_GV04 = (DataTable)ViewState["GV04"];
                DataTable DTGV04 = new DataTable();

                if (OBJ_GV04 == null)
                {
                    DTGV04.Columns.Add(new DataColumn("DTLD001", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD002", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD003", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD004", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD005", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD006", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD007", typeof(string)));

                    ViewState["GV04"] = DTGV04;
                    OBJ_GV04 = DTGV04;
                }
                DataRow dr04 = OBJ_GV04.NewRow();

                dr04["DTLD001"] = dDTLD01;
                dr04["DTLD002"] = dDTLD02;
                dr04["DTLD003"] = dDTLD03;
                dr04["DTLD004"] = dDTLD04;
                dr04["DTLD005"] = dUrl;
                dr04["DTLD006"] = dDTLD06;
                dr04["DTLD007"] = dDTLD07;

                OBJ_GV04.Rows.Add(dr04);

                ViewState["GV04"] = OBJ_GV04;

                SWCDTL04.DataSource = OBJ_GV04;
                SWCDTL04.DataBind();
            }
        }
    }


    protected void SWCDTL04_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SWCDTL04.PageIndex = e.NewPageIndex;
    }

    protected void SWCDTL04_PageIndexChanged(object sender, EventArgs e)
    {
        SWCDTL04.DataSource = ViewState["GV04"];
        SWCDTL04.DataBind();
    }
}