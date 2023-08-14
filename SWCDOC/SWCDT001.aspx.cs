using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCDT001 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
	string GetMax = "";
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
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";
		
        GBClass001 SBApp = new GBClass001();
		
        if (!canDoModify(rCaseId, rDTLId))
            Response.Redirect("SWCDT001v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + rDTLId);
		
        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId == "")
        {
            Response.Redirect("SWC001.aspx");
        }
		if (rDTLId == "AddNew"){
			ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
			using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
			{
				SwcConn.Open();
				
				string strSQLRV = " select * from SWCDTL01 ";
				strSQLRV = strSQLRV + " where SWC000 = '" + rCaseId + "' and ISNULL(DataLock,'') != 'Y' ;";
				
				SqlDataReader readeSwc;
				SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
				readeSwc = objCmdSwc.ExecuteReader();
				
				while (readeSwc.Read())
				{
					if(readeSwc.HasRows){
						Response.Write("<script>alert('已經有審查表單於暫存區。'); location.href='SWC003.aspx?SWCNO=" + rCaseId + "' ; </script>");
					}
				}
				
				readeSwc.Close();
				objCmdSwc.Dispose();
			}
		}
		
        if (!IsPostBack)
        {
            GenerateDropDownList(rCaseId);
            Data2Page(rCaseId, rDTLId);
            SBApp.ViewRecord("審查表單", "view", rCaseId+"-"+ rDTLId);
			
			if(ssUserType == "09") DataLock.Visible = false;
        }

        //全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
			TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        //全區供用
    }
    private void Data2Page(string v,string v2)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();
        C20.swcLogRC("SWCDT001", "審查紀錄", "詳情", "瀏覽", v + ";" + v2);

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
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";

                LBSWC000.Text = v;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
                LBSWC005a.Text = tSWC005;
                LBSWC005_2.Text = tSWC005;
            }

            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string nIDA = GetDTLAID(v);

                LBDTL001.Text = nIDA;
            }
            else
            {
                string strSQLRV2 = " select * from SWCDTL01 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   AND DTLA000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLA001 = readeDTL["DTLA001"] + "";
                    string tDTLA002 = readeDTL["DTLA002"] + "";
                    string tDTLA003 = readeDTL["DTLA003"] + "";
                    string tDTLA004 = readeDTL["DTLA004"] + "";
                    string tDTLA005 = readeDTL["DTLA005"] + "";
                    string tDTLA006 = readeDTL["DTLA006"] + "";
                    string tDTLA007 = readeDTL["DTLA007"] + "";
                    string tDTLA008 = readeDTL["DTLA008"] + "";
                    string tDTLA009 = readeDTL["DTLA009"] + "";
                    string tDTLA010 = readeDTL["DTLA010"] + "";
                    string tDTLA011 = readeDTL["DTLA011"] + "";
                    string tDTLA012 = readeDTL["DTLA012"] + "";
                    string tDTLA013 = readeDTL["DTLA013"] + "";
                    string tDTLA014 = readeDTL["DTLA014"] + "";
                    string tDTLA015 = readeDTL["DTLA015"] + "";
                    string tDTLA016 = readeDTL["DTLA016"] + "";
                    string tDTLA017 = readeDTL["DTLA017"] + "";
                    string tDTLA018 = readeDTL["DTLA018"] + "";
                    string tDTLA019 = readeDTL["DTLA019"] + "";
                    string tDTLA020 = readeDTL["DTLA020"] + "";
                    string tDTLA021 = readeDTL["DTLA021"] + "";
                    string tDTLA022 = readeDTL["DTLA022"] + "";
                    string tDTLA023 = readeDTL["DTLA023"] + "";
                    string tDTLA024 = readeDTL["DTLA024"] + "";
                    string tDTLA025 = readeDTL["DTLA025"] + "";
                    string tDTLA026 = readeDTL["DTLA026"] + "";
                    string tDTLA027 = readeDTL["DTLA027"] + "";
                    string tDTLA028 = readeDTL["DTLA028"] + "";
                    string tDTLA029 = readeDTL["DTLA029"] + "";
                    string tDTLA030 = readeDTL["DTLA030"] + "";
                    string tDTLA031 = readeDTL["DTLA031"] + "";
                    string tDTLA032 = readeDTL["DTLA032"] + "";
                    string tDTLA034 = readeDTL["DTLA034"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

                    LBDTL001.Text = tDTLA001;
                    TXTDTL003.Text = SBApp.DateView(tDTLA003, "00"); 
                    TXTDTL004.Text = tDTLA004;
                    DDLDTL006.SelectedValue = tDTLA006;
					if(DDLDTL006.SelectedIndex == 1){
						TXTDTL034.Enabled = false;
					}					
                    TXTDTL007.Text = SBApp.DateView(tDTLA007, "00");
                    TXTDTL008.Text = tDTLA008;
                    TXTDTL009.Text = tDTLA009;
                    TXTDTL010.Text = tDTLA010;
                    TXTDTL011.Text = tDTLA011;
                    TXTDTL012.Text = tDTLA012;
                    TXTDTL013.Text = tDTLA013;
                    TXTDTL017.Text = tDTLA017;
                    TXTDTL018.Text = tDTLA018;
                    TXTDTL020.Text = tDTLA020;
                    TXTDTL021.Text = tDTLA021;
                    TXTDTL022.Text = tDTLA022;
                    TXTDTL023.Text = tDTLA023;
                    TXTDTL024.Text = tDTLA024;
                    TXTDTL025.Text = tDTLA025;
                    TXTDTL026.Text = tDTLA026;
                    TXTDTL027.Text = tDTLA027;
                    TXTDTL028.Text = tDTLA028;
                    TXTDTL029.Text = tDTLA029;
                    TXTDTL030.Text = tDTLA030;
                    TXTDTL031.Text = tDTLA031;
                    TXTDTL032.Text = tDTLA032;
                    TXTDTL034.Text = tDTLA034;

                    //點擊放大圖片類處理
                    string[] arrayFileName2 = new string[] { tDTLA018, tDTLA021, tDTLA023, tDTLA025, tDTLA027, tDTLA029, tDTLA031 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj2 = new System.Web.UI.WebControls.HyperLink[] { HyperLink018, HyperLink021, HyperLink023, HyperLink025, HyperLink027, HyperLink029, HyperLink031 };

                    for (int i = 0; i < arrayFileName2.Length; i++)
                    {
                        string strFileName = arrayFileName2[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj2[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = "../tcgefile/SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                            ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                        }
                    }
                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLA020 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link020 };

                    for (int i = 0; i < arrayFileNameLink.Length; i++)
                    {
                        string strFileName = arrayFileNameLink[i];
                        System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];

                        FileLinkObj.Visible = false;
                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempLinkPateh = "../tcgefile/SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    //審查意見-多筆
                    int nj = 0;
                    DataTable tbRVList = null;
                    string GetListSQLstr = " Select * From SWCDTL01R ";
                    GetListSQLstr += " Where SWC000 = '"+v+"' and DTLA001 = '"+v2+"' ";
                    GetListSQLstr += " Order by DTLR01 ";

                    ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                    using (SqlConnection SwcConn2 = new SqlConnection(connectionString.ConnectionString))
                    {
                        SwcConn2.Open();

                        SqlDataReader readeDTLR;
                        SqlCommand objCmdDTLR = new SqlCommand(GetListSQLstr, SwcConn2);
                        readeDTLR = objCmdDTLR.ExecuteReader();

                        while (readeDTLR.Read())
                        {
                            nj++;
                            string tDTLR01 = readeDTLR["DTLR01"] + "";
                            string tDTLR02 = readeDTLR["DTLR02"] + "";
                            string tDTLR03 = readeDTLR["DTLR03"] + "";

                            tbRVList = (DataTable)ViewState["ReviewListVS"];

                            if (tbRVList == null)
                            {
                                DataTable ListTable = new DataTable();

                                ListTable.Columns.Add(new DataColumn("DataId", typeof(string)));
                                ListTable.Columns.Add(new DataColumn("ShortText", typeof(string)));
                                ListTable.Columns.Add(new DataColumn("FullText", typeof(string)));
                                ListTable.Columns.Add(new DataColumn("SaveTime", typeof(string)));

                                ViewState["ReviewListVS"] = ListTable;
                                tbRVList = (DataTable)ViewState["ReviewListVS"];
                            }

                            //設定資料
                            string tShortText = tDTLR02;
                            if (tShortText.Length > 20) { tShortText = tShortText.Substring(0, 20) + "…"; };

                            DataRow ListNewRow = tbRVList.NewRow();

                            ListNewRow["DataId"] = tDTLR01;
                            ListNewRow["ShortText"] = tShortText;
                            ListNewRow["FullText"] = tDTLR02;
                            ListNewRow["SaveTime"] = tDTLR03;

                            tbRVList.Rows.Add(ListNewRow);

                            ViewState["ReviewListVS"] = tbRVList;

                            GVReViewList.DataSource = tbRVList;
                            GVReViewList.DataBind();

                            ListNo.Text = nj.ToString();
                        }
                    }

                    //按鈕處理
                    if (tDATALOCK == "Y")
                    {
                        DataLock.Visible = false;
                        SaveCase.Visible = false;
                        
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='SWCDT001v.aspx?SWCNO=" + v + "&DTLNO="+v2 +"'; </script>");                        
                    }
                }
            }
        }
    }

    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RA" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RA" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLA000) AS MAXID from SWCDTL01 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLA000,7) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 3, 3)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(3,'0');
                }
            }
        }
        return _ReturnVal;
    }

    protected void GenerateDropDownList(string v)
    {
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select ISNULL(MAX(DTLA006),0) as MAX from SWCDTL01 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' and DATALOCK = 'Y';";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();
			
            while (readerSWC.Read())
            {
                GetMax = readerSWC["MAX"] + "";
            }
        }
		string[] array_DTL006 = new string[] {};
		if(GetMax=="0"){
			array_DTL006 = new string[] { "1" };
			TXTDTL034.Enabled = false;
		}else{
			array_DTL006 = new string[] { (Convert.ToInt32(GetMax)).ToString(),(Convert.ToInt32(GetMax)+1).ToString() }; //(Convert.ToInt32(GetMax)).ToString(),(Convert.ToInt32(GetMax)+1).ToString()
		}
        DDLDTL006.DataSource = array_DTL006;	
		DDLDTL006.DataBind();

        TXTDTL017.Text = "(一)審查委員：" + System.Environment.NewLine + "(二)主管機關：" + System.Environment.NewLine + "(三)承辦技師：" + System.Environment.NewLine + "(四)水保義務人：" + System.Environment.NewLine + "(五)其他業務主管機關：";
    }
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId="+ vCaseID);
    }


    protected void SaveCase_Click(object sender, EventArgs e)
    {
        Class1 C1 = new Class1();
        Class20 C20 = new Class20();
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        string sDTLA000 = LBDTL001.Text + "";
        string sDTLA002 = "";
        string sDTLA003 = TXTDTL003.Text + "";
        string sDTLA004 = TXTDTL004.Text + "";
        string sDTLA005 = LBSWC005.Text + "";
        string sDTLA006 = DDLDTL006.SelectedValue + "";
        string sDTLA007 = TXTDTL007.Text + "";
        string sDTLA008 = TXTDTL008.Text + "";
        string sDTLA009 = TXTDTL009.Text + "";
        string sDTLA010 = TXTDTL010.Text + "";
        string sDTLA011 = TXTDTL011.Text + "";
        string sDTLA012 = TXTDTL012.Text + "";
        string sDTLA013 = TXTDTL013.Text + "";
        string sDTLA017 = TXTDTL017.Text + "";
        string sDTLA018 = TXTDTL018.Text + "";
        string sDTLA020 = TXTDTL020.Text + "";
        string sDTLA021 = TXTDTL021.Text + "";
        string sDTLA022 = TXTDTL022.Text + "";
        string sDTLA023 = TXTDTL023.Text + "";
        string sDTLA024 = TXTDTL024.Text + "";
        string sDTLA025 = TXTDTL025.Text + "";
        string sDTLA026 = TXTDTL026.Text + "";
        string sDTLA027 = TXTDTL027.Text + "";
        string sDTLA028 = TXTDTL028.Text + "";
        string sDTLA029 = TXTDTL029.Text + "";
        string sDTLA030 = TXTDTL030.Text + "";
        string sDTLA031 = TXTDTL031.Text + "";
        string sDTLA032 = TXTDTL032.Text + "";
        string sDTLA034 = TXTDTL034.Text + "";

        if (!C1.chkDateFormat(sDTLA003)) { Response.Write("<script>alert('提醒您，您輸入的「補正期限」日期格式不正確，請重新輸入。');</script>"); TXTDTL003.Focus(); return; }
        if (DDLDTL006.Items.Count!=1 && DDLDTL006.SelectedIndex==0 && TXTDTL034.Text=="") {
			Response.Write("<script>alert('請填寫重新上傳原因');</script>");
			return;
		}
		if (!C1.chkDateFormat(sDTLA007)) { Response.Write("<script>alert('提醒您，您輸入的「會議時間」日期格式不正確，請重新輸入。');</script>"); TXTDTL007.Focus(); return; }

        if (sDTLA013.Length > 255) { sDTLA013 = sDTLA013.Substring(0, 255); }
        if (sDTLA017.Length > 500) { sDTLA017 = sDTLA017.Substring(0, 500); }
        if (sDTLA022.Length > 255) { sDTLA022 = sDTLA022.Substring(0, 255); }
        if (sDTLA024.Length > 255) { sDTLA024 = sDTLA024.Substring(0, 255); }
        if (sDTLA026.Length > 255) { sDTLA026 = sDTLA026.Substring(0, 255); }
        if (sDTLA028.Length > 255) { sDTLA028 = sDTLA028.Substring(0, 255); }
        if (sDTLA030.Length > 255) { sDTLA030 = sDTLA030.Substring(0, 255); }
        if (sDTLA032.Length > 255) { sDTLA032 = sDTLA032.Substring(0, 255); }
        
        sDTLA002 = sDTLA007;

        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL01 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLA000 = '" + sDTLA000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL01 (SWC000,DTLA000,DTLA033) VALUES ('" + sSWC000 + "','"+ sDTLA000 + "',getdate());";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL01 Set ";
            sEXESQLSTR = sEXESQLSTR + " DTLA001 = DTLA000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLA002 = N'" + sDTLA002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA003 ='" + sDTLA003 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA004 ='" + sDTLA004 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA005 ='" + sDTLA005 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA006 ='" + sDTLA006 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA007 ='" + sDTLA007 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA008 ='" + sDTLA008 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA009 = N'" + sDTLA009 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA010 = N'" + sDTLA010 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA011 ='" + sDTLA011 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA012 ='" + sDTLA012 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA013 ='" + sDTLA013 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA017 ='" + sDTLA017 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA018 ='" + sDTLA018 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA020 ='" + sDTLA020 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA021 ='" + sDTLA021 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA022 ='" + sDTLA022 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA023 ='" + sDTLA023 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA024 ='" + sDTLA024 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA025 ='" + sDTLA025 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA026 ='" + sDTLA026 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA027 ='" + sDTLA027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA028 ='" + sDTLA028 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA029 ='" + sDTLA029 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA030 ='" + sDTLA030 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA031 ='" + sDTLA031 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA032 ='" + sDTLA032 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLA034 ='" + sDTLA034 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLA000 = '" + sDTLA000 + "'";

            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";
            C20.swcLogRC("SWCDT001", "審查紀錄", "詳情", "修改", sSWC000 + ";" + sDTLA000);

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
            //儲存審查意見
            SaveReviewList();

            SBApp.ViewRecord("審查表單", "update", rCaseId + "-" + sDTLA000);
            
        }
        string btnType = ((Button)(sender)).ID;
		if(btnType == "DataLock")
			SendMailNotice(sSWC000);
		
		Response.Write("<script>alert('資料已存檔'); location.href='SWC003.aspx?SWCNO=" + sSWC000 + "'; </script>");
    }
    private void SaveReviewList()
    {
        string vSWC000 = LBSWC000.Text;
        string vSWC002 = LBSWC002.Text;
        string vDTLA001 = LBDTL001.Text;
        string ssUserID = Session["ID"] + "";
        DataTable RList = (DataTable)ViewState["ReviewListVS"];
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        string qq111 = " delete SWCDTL01R Where SWC000=@SWC000 and DTLA001=@DTLA001; ";

        if (RList != null)
        {
            int i = 0;
            for (i = 0; i <= (Convert.ToInt32(RList.Rows.Count) - 1); i++)
            {
                string tDTLR01 = RList.Rows[i]["DataId"].ToString().Trim();
                string tDTLR02 = RList.Rows[i]["FullText"].ToString().Trim();
                string tDTLR03 = RList.Rows[i]["SaveTime"].ToString().Trim();

                string qq222 = " INSERT INTO SWCDTL01R (SWC000,SWC002,DTLA001,DTLR01,DTLR02,DTLR03,Saveuser,savedate) VALUES (@SWC000,@SWC002,@DTLA001,@DTLR01,@DTLR02,@DTLR03,@Saveuser,getdate()) ";
                using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
                {
                    dbConn.Open();
                    using (var cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = qq111+ qq222;
                        cmd.Parameters.Add(new SqlParameter("@SWC000", vSWC000));
                        #region.設定值
                        cmd.Parameters.Add(new SqlParameter("@SWC002", vSWC002));
                        cmd.Parameters.Add(new SqlParameter("@DTLA001", vDTLA001));
                        cmd.Parameters.Add(new SqlParameter("@DTLR01", tDTLR01));
                        cmd.Parameters.Add(new SqlParameter("@DTLR02", tDTLR02));
                        cmd.Parameters.Add(new SqlParameter("@DTLR03", tDTLR03));
                        cmd.Parameters.Add(new SqlParameter("@Saveuser", ssUserID));
                        #endregion
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                qq111 = "";
            }
        }
    }

    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL018", "TXTDTL020", "TXTDTL021", "TXTDTL023", "TXTDTL025", "TXTDTL027", "TXTDTL029", "TXTDTL031" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL018, TXTDTL020, TXTDTL021, TXTDTL023, TXTDTL025, TXTDTL027, TXTDTL029, TXTDTL031 };
        string csUpLoadField = "TXTDTL018";
        TextBox csUpLoadAppoj = TXTDTL018;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

        folderExists = Directory.Exists(SwcCaseFolderPath);
        if (folderExists == false)
        {
            Directory.CreateDirectory(SwcCaseFolderPath);
        }

        folderExists = Directory.Exists(SwcCaseFolderPath + CaseId);
        if (folderExists == false)
        {
            Directory.CreateDirectory(SwcCaseFolderPath + CaseId);
        }


        for (int i = 0; i < arryUpLoadField.Length; i++)
        {
            csUpLoadField = arryUpLoadField[i];
            csUpLoadAppoj = arryUpLoadAppoj[i];

            if (Session[csUpLoadField] + "" == "有檔案")
            {
                Boolean fileExists;
                string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;
                string SwcCaseFilePath = SwcCaseFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;

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
    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = LBSWC000.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名

            switch (ChkType)
            {
                case "PIC2":
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    // 限制檔案大小，限制為 10 MB
                    int filesize = UpLoadBar.PostedFile.ContentLength;

                    if (filesize > 10000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10 Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".xls", ".xlsx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 Excel 檔案格式上傳，謝謝!!");
                        return;
                    }
                    // 限制檔案大小，限制為 50MB
                    int filesize2 = UpLoadBar.PostedFile.ContentLength;

                    if (filesize2 > 50000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50 Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
            }


            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

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
                    case "PIC2":
                        string tmpLink = "../tcgefile/SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.ImageUrl = tmpLink;
                        UpLoadLink.NavigateUrl = tmpLink;
                        break;

                    case "PIC":
                        UpLoadView.Attributes.Add("src","../tcgefile/SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = "../tcgefile/SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
    protected void TXTDTL018_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL018_fileupload, TXTDTL018, "TXTDTL018", "_" + rDTLNO + "_01_photo1", null, HyperLink018);
    }
    protected void TXTDTL020_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTDTL020_fileupload, TXTDTL020, "TXTDTL020", "_" + rDTLNO + "_01_doc1", null, Link020);

    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLA000 = LBDTL001.Text + "";
		
		//20220523若上一次修正本尚未上傳，無法送出只能暫存
		string sfno = "";
		bool Can_Send = false;
		if (DDLDTL006.SelectedValue != "" && DDLDTL006.SelectedValue != "1") {
			sfno = "00"+(Convert.ToInt16(DDLDTL006.SelectedValue)-1).ToString();
			Can_Send = CanSend(sSWC000,sfno);
			if(!Can_Send){
				Response.Write("<script>alert('您好，前次審查會議紀錄尚未上傳修正本，請確認承辦技師是否已上傳修正本。')</script>");
				return;
			}
		}
        string sEXESQLSTR = "";

		
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL01 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLA000 = '" + sDTLA000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL01 (SWC000,DTLA000) VALUES ('" + sSWC000 + "','"+ sDTLA000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL01 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLA000 = '" + sDTLA000 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }
        SaveCase_Click(sender, e);
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

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC025, SWC.SWC021ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
            strSQLRV = strSQLRV + " LEFT JOIN ETUsers U on SWC.SWC021ID = U.ETID ";
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
                string tSWC021ID = readeSwc["SWC021ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄信名單：股長、管理者、承辦人員、章姿隆
                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserName.Trim() == "章姿隆")
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;

                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增審查紀錄";
                        string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增審查紀錄，請上臺北市坡地管理資料庫查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
                
                string[] arraySentMail02a = new string[] { tETEmail };
                string[] arraySentMail02b = new string[] { tSWC108 };

                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】已新增審查紀錄";
                string ssMailBody02 = "您好，" + "【" + tSWC005 + "】已新增審查紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                
                bool MailTo02a = SBApp.Mail_Send(arraySentMail02a, ssMailSub02, ssMailBody02);
                bool MailTo02b = SBApp.Mail_Send(arraySentMail02b, ssMailSub02, ssMailBody02);

                string ssMailBody03 = "您好，【" + tSWC005 + "】已新增審查紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。";
                
                SBApp.SendSMS(tSWC013TEL, ssMailBody03);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csCaseID2 = LBDTL001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCDTL01 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLA000 = '" + csCaseID2 + "' ";
        
        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();
        
        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

        string DelFileName = ImgText.Text;
        string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + ImgText.Text;

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
                FileLink.Text = "";
                FileLink.ImageUrl = "";
                FileLink.NavigateUrl = "";
                break;
            case "CAD":
            case "PDF":
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
    protected void TXTDTL021_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL021_fileupload, TXTDTL021, "TXTDTL021", "_" + rDTLNO + "_01_photo1", null, HyperLink021);
    }
    protected void TXTDTL021_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL021, null, HyperLink021, "DTLA021", "TXTDTL021", 320, 180);
    }
    protected void TXTDTL023_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL023_fileupload, TXTDTL023, "TXTDTL023", "_" + rDTLNO + "_01_photo2", null, HyperLink023);
    }
    protected void TXTDTL023_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL023, null, HyperLink023, "DTLA023", "TXTDTL023", 320, 180);
    }
    protected void TXTDTL025_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL025_fileupload, TXTDTL025, "TXTDTL025", "_" + rDTLNO + "_01_photo3", null, HyperLink025);
    }
    protected void TXTDTL025_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL025, null, HyperLink025, "DTLA025", "TXTDTL025", 320, 180);
    }
    protected void TXTDTL027_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL027_fileupload, TXTDTL027, "TXTDTL027", "_" + rDTLNO + "_01_photo4", null, HyperLink027);
    }
    protected void TXTDTL027_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL027, null, HyperLink027, "DTLA027", "TXTDTL027", 320, 180);
    }
    protected void TXTDTL029_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL029_fileupload, TXTDTL029, "TXTDTL029", "_" + rDTLNO + "_01_photo5", null, HyperLink029);
    }
    protected void TXTDTL029_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL029, null, HyperLink029, "DTLA029", "TXTDTL029", 320, 180);
    }
    protected void TXTDTL031_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL031_fileupload, TXTDTL031, "TXTDTL031", "_" + rDTLNO + "_01_photo6", null, HyperLink031);
    }
    protected void TXTDTL031_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL031, null, HyperLink031, "DTLA031", "TXTDTL031", 320, 180);
    }

    protected void TXTDTL020_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTDTL020, null, Link020, "DTLA020", "TXTDTL020", 0, 0);
    }

    protected void TXTDTL018_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL018, null, HyperLink018 , "DTLA018", "TXTDTL018", 320, 180);
    }

    protected void AddList_Click(object sender, EventArgs e)
    {
        string sAction = AddList.Text + "";

        string DataTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        string tFullText = ReViewText.Text;
        string tShortText = ReViewText.Text;
        if (tShortText.Length > 20) { tShortText = tShortText.Substring(0, 20) + "…"; };

        DataTable ReviewListVS = (DataTable)ViewState["ReviewListVS"];

        switch (sAction)
        {
            case "確定修改":
                int aa = Convert.ToInt16(Session["ReViewListIndex"].ToString());

                ReviewListVS.Rows[aa]["ShortText"] = tShortText;
                ReviewListVS.Rows[aa]["FullText"] = tFullText;
                ReviewListVS.Rows[aa]["SaveTime"] = DataTime;

                ViewState["ReviewListVS"] = ReviewListVS;

                GVReViewList.DataSource = ReviewListVS;
                GVReViewList.DataBind();

                AddList.Text = "加入清單";
                QuitAddListTxt.Visible = false;
                ReViewText.Text = "";

                break;

            default:
                ListNo.Text = (Convert.ToInt16(ListNo.Text) + 1).ToString();

                //設定資料頭
                if (ReviewListVS == null)
                {
                    DataTable ListTable = new DataTable();

                    ListTable.Columns.Add(new DataColumn("DataId", typeof(string)));
                    ListTable.Columns.Add(new DataColumn("ShortText", typeof(string)));
                    ListTable.Columns.Add(new DataColumn("FullText", typeof(string)));
                    ListTable.Columns.Add(new DataColumn("SaveTime", typeof(string)));

                    ViewState["ReviewListVS"] = ListTable;
                    ReviewListVS = ListTable;
                }

                //設定資料

                DataRow ListNewRow = ReviewListVS.NewRow();

                ListNewRow["DataId"] = ListNo.Text;
                ListNewRow["ShortText"] = tShortText;
                ListNewRow["FullText"] = tFullText;
                ListNewRow["SaveTime"] = DataTime;

                ReviewListVS.Rows.Add(ListNewRow);

                ViewState["ReviewListVS"] = ReviewListVS;

                GVReViewList.DataSource = ReviewListVS;
                GVReViewList.DataBind();

                ReViewText.Text = "";

                break;
        }
    }


    protected void GVReViewList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;
        int aa = Convert.ToInt32(e.CommandArgument);

        switch (ExcAction)
        {
            case "DelReView":
                int number = Convert.ToInt32(GVReViewList.Rows[aa].Cells[0].Text);

                DataTable VS_GV1 = (DataTable)ViewState["ReviewListVS"];
                
                VS_GV1.Rows.RemoveAt(aa);

                ViewState["ReviewListVS"] = VS_GV1;
                GVReViewList.DataSource = VS_GV1;
                GVReViewList.DataBind();

                break;

            case "EditReView":
                Session["ReViewListIndex"] = aa.ToString();

                DataTable GetSTReviewList = (DataTable)ViewState["ReviewListVS"];

                string DataId = GVReViewList.Rows[aa].Cells[0].Text;
                string DataText = GetSTReviewList.Rows[aa][2].ToString();

                ReViewText.Text = DataText;
                AddList.Text = "確定修改";
                QuitAddListTxt.Visible = true;
                break;
        }

    }
    private bool canDoModify(string pSWC000, string pDTLF000)
    {
        Class1 C1 = new Class1();
        bool rValue = true;
        string ssUserID = Session["ID"] + "";

        string sqlStr = " select * from SWCDTL01 Where SWC000=@SWC000 and DTLA000=@DTLA000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                cmd.Parameters.Add(new SqlParameter("@DTLA000", pDTLF000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        string tmpDataLock = readerTslm["DATALOCK"] + "";
                        //string tmpDataLock2 = readerTslm["DATALOCK2"] + "";
                        string tmpSaveuser = readerTslm["Saveuser"] + "";

                        if (tmpDataLock == "Y")
                            rValue = false;
                        //else if (tmpDataLock2 != "Y" && tmpSaveuser != ssUserID)
                        else if (tmpSaveuser != ssUserID && C1.getSWCSWCData(pSWC000, "SWC022ID") != ssUserID)
                                    rValue = false;

                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    protected void QuitAddListTxt_Click(object sender, EventArgs e)
    {
        AddList.Text = "加入清單";
        QuitAddListTxt.Visible = false;
        ReViewText.Text = "";
    }
	protected void DDLDTL006_SelectedIndexChanged(object sender, EventArgs e)
	{
		if(DDLDTL006.SelectedIndex==1){
			TXTDTL034.Text="";
			TXTDTL034.Enabled=false;
		}
		if(DDLDTL006.SelectedIndex==0){
			TXTDTL034.Enabled=true;
		}
	}
	
	protected bool CanSend(string SWC000,string no)
    {
        bool rValue = false;

        string sqlStr = " select * from ShareFiles Where SWC000=@SWC000 and SFTYPE=@SFTYPE; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", SWC000));
                cmd.Parameters.Add(new SqlParameter("@SFTYPE", no));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readeSwc = cmd.ExecuteReader())
                {
                    while (readeSwc.Read())
                    {
                        if(readeSwc.HasRows)
							rValue = true;
                    }
                    readeSwc.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
	}
}