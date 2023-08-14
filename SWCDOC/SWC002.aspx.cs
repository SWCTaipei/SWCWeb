using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Web;
using System.Security.Cryptography;
using System.Text;

public partial class SWCDOC_SWC002 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"];
    string GlobalUpLoadTempFilePath = ConfigurationManager.AppSettings["SwcFileTemp20"];
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
        Class20 C20 = new Class20();
        GBClass001 SBApp = new GBClass001();
        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserGuild = Session["ETU_Guild01"] + "";

        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId == "" || ssUserID == "") Response.Redirect("SWC001.aspx");

        if (!IsPostBack)
        {
            C20.swcLogRC("SWC002","水保申請案","詳情","瀏覽", rCaseId);

            //將按鈕Disable，並修改顯示文字
            SaveCase.Attributes["onclick"] = "this.disabled = true;this.value = 'Please wait..';" + Page.ClientScript.GetPostBackEventReference(SaveCase, "");
            Session["CaseStatus"] = "";
            GenerateDropDownList();
            if (rCaseId == "COPY")
                if (ChkHavCase())
                    Response.Write("<script>alert('本案已有變更設計紀錄，請確認案件歷程。'); location.href='SWC001.aspx'; </script>");
                else
                    CopySwcCase();
            else
                GetSwcData(rCaseId);

            string tCaseStatus = LBSWC004.Text + "";
            string tCaseType = DDLSWC007.SelectedValue + "";
            string tSWC013ID = TXTSWC013ID.Text + "";
            string tSWC021ID = LBSWC021ID.Text + "";
            string tSWC045ID = LBSWC045ID.Text + "";
            string tSWC022ID = LBSWC022ID.Text + "";
            string tSWC024ID = LBSWC024ID.Text + "";
            string tSWC107ID = LBSWC107ID.Text + "";

            //預設無法
            string tOUT = "Y";

            Area02.Visible = false;
            Area03.Visible = false;
            Area04.Visible = false;
            Area05.Visible = false;

            Area01Close();
            Area01Close_2();
            //Area02Close();
            Area03Close();
            Area04Close();
            Area05Close();

            SWCDTL01.Columns[5].Visible = false;
            SWCDTL0302.Columns[5].Visible = false;
            SWCDTL04.Columns[5].Visible = false;
            SWCDTL05.Columns[5].Visible = false;
            SWCDTL06.Columns[5].Visible = false;
            SWCDTL07.Columns[4].Visible = false;

            SWCDTL01.Visible = false;
            //SWCDTL0302.Visible = false;
            //SWCDTL04.Visible = false;
            //SWCDTL05.Visible = false;
            //SWCDTL06.Visible = false;
            //SWCDTL07.Visible = false;

            DT001.Visible = false;
            DT003.Visible = false;
            DT004.Visible = false;
            DT005.Visible = false;
            DT006.Visible = false;
            DT007.Visible = false;

            Session["DTL02VIEW"] = "Y";
            Session["DTL03VIEW"] = "Y";
            Session["DTL04VIEW"] = "Y";
            Session["DTL05VIEW"] = "Y";

            if (ssUserID == "gv-admin")
            {
            }
            else
            {
                switch (ssUserType)
                {
                    case "01":
                        //義務人
                        if (tSWC013ID == ssUserID)
                        {
                            SWCDTL01.Visible = true;
                        }
                        else
                        {
                            Area01Open();
                            if (rCaseId == "ADDNEW")
                            {
                                tOUT = "N";
                                SWCDTL01.Visible = false;
                            }
                        }
						TXTSWC013.Text = Session["NAME"] + "";
                        TXTSWC013ID.Enabled = false;
                        TXTSWC013ID.Text = ssUserID;
                        TXTSWC013TEL.Enabled = false;
                        TXTSWC013TEL.Text = ssUserPW;
                        DDLSWC007.Enabled = false;
                        break;

                    case "02":
                        TitleLink00.Visible = true;

                        if (rCaseId == "ADDNEW" || rCaseId == "COPY")
                        {
                            tCaseStatus = "申請中";
                            tOUT = "N";
                            Area01Open();
                            Area01Open_2();

                        }
                        //承辦技師
                        if (tSWC021ID == ssUserID)
                        {
                            tOUT = "N";
                            Area02.Visible = true;
                            Area03.Visible = true;

                            SWCDTL01.Visible = true;

                            Area03Close();
                            Area04Close();
                            Area05Close();
                        }
                        //監造技師
                        if (tSWC045ID == ssUserID)
                        {
                            tOUT = "N";
                            Area03.Visible = true;
                            Area04.Visible = true;

                            if (tCaseStatus == "施工中")
                            {
                                Area04Open();

                                DT004.Visible = true;
                                DT005.Visible = true;
                                SWCDTL04.Columns[5].Visible = true;
                                SWCDTL05.Columns[5].Visible = true;
                                Session["DTL04VIEW"] = "N";
                                Session["DTL05VIEW"] = "N";
                            }

                        }
                        break;
                    case "03":
                        SWCDTL01.Visible = true;
                        break;
                    case "04":
                        string tmpRGType = "";
                        string tmpUserID = ssUserID == ssUserGuild ? ssUserID : ssUserGuild;

                        if (ssUserID == ssUserGuild)
                        {
                            //審查公會
                            if (tSWC022ID == tmpUserID)
                            {
                                tmpRGType = "S3";
                                tOUT = "N";
                                Area02.Visible = true;
                                Area03.Visible = true;

                                SWCDTL01.Visible = true;

                                if (tCaseStatus == "審查中" || tCaseStatus == "暫停審查")
                                {
                                    Area03Open();
                                    DT001.Visible = true;
                                    SWCDTL01.Columns[5].Visible = true;
                                }
                            }
                            //檢查公會
                            if (tSWC024ID == tmpUserID)
                            {
                                tmpRGType = "S4";
                                tOUT = "N";
                                Area03.Visible = true;
                                Area04.Visible = true;

                                if (tCaseStatus == "施工中")
                                {
                                    Area04Open();
                                    DT003.Visible = true;
                                    DT006.Visible = true;
                                    SWCDTL0302.Columns[5].Visible = true;
                                    SWCDTL06.Columns[5].Visible = true;
                                    Session["DTL03VIEW"] = "N";
                                }
                            }
                            //完工檢查公會
                            if (Session["Edit4"] + "" == "Y")
                            {
                                tOUT = "N";
                                Area03.Visible = true;
                                Area04.Visible = true;
                                Area05.Visible = true;

                                if (tCaseStatus == "已完工" || tCaseStatus == "撤銷" || tCaseStatus == "已變更")
                                {
                                    Area05Open();
                                    DT007.Visible = true;
                                    SWCDTL07.Columns[4].Visible = true;
                                }
                            }
                        }
                        else
                        {
                            string tmpUserGuild1 = Session["ETU_Guild01"] + "";
                            string tmpUserGuild2 = Session["ETU_Guild02"] + "";
                            string tmpUserGuild3 = Session["ETU_Guild03"] + "";
                            #region 代審查
                            if (tSWC022ID == tmpUserGuild1 || tSWC022ID == tmpUserGuild3)
                            {
                                tOUT = "N";
                                Area02.Visible = true;
                                Area03.Visible = true;
                                SWCDTL01.Visible = true;
                                if (tCaseStatus == "審查中" || tCaseStatus == "暫停審查")
                                {
                                    Area03Open();
                                    DT001.Visible = true;
                                    SWCDTL01.Columns[5].Visible = true;
                                }
                                tOUT = chkUpdateStatus(tOUT, "S3");
                                DropDownList[] arryDDLA = new DropDownList[] { DDLSA01, DDLSA02, DDLSA03, DDLSA04, DDLSA05, DDLSA06, DDLSA07, DDLSA08, DDLSA09, DDLSA10 };
                                for (int i = 0; i < arryDDLA.Length; i++) arryDDLA[i].Enabled = false;
                            }
                            #endregion
                            #region 代檢查
                            if (tSWC024ID == tmpUserGuild2 || tSWC022ID == tmpUserGuild3)
                            {
                                tOUT = "N";
                                Area03.Visible = true;
                                Area04.Visible = true;
                                if (tCaseStatus == "施工中")
                                {
                                    Area04Open();
                                    DT003.Visible = true;
                                    DT006.Visible = true;
                                    SWCDTL0302.Columns[5].Visible = true;
                                    SWCDTL06.Columns[5].Visible = true;
                                    Session["DTL03VIEW"] = "N";
                                }
                                tOUT = chkUpdateStatus(tOUT, "S4");
                                DropDownList[] arryDDLB = new DropDownList[] { DDLSB01, DDLSB02 };
                                for (int i = 0; i < arryDDLB.Length; i++) arryDDLB[i].Enabled = false;
                            }
                            #endregion
                        }
                        break;
                }





                if (ssUserID == "gv-admin")
                {
                    Area03Close();
                    Area04Close();
                    Area05Close();
                }
                else
                {
                    switch (ssUserType)
                    {
                        case "01":

                            if (tSWC013ID == ssUserID)
                            {
                                if (tCaseType == "簡易水保")
                                {
                                    if (tCaseStatus == "退補件" || tCaseStatus == "受理中" || tCaseStatus == "申請中")
                                    {
                                        Area01Open();
                                    }
                                    else
                                    {
                                        Area01Close();
                                    }
                                }
                                else
                                {
                                    Response.Redirect("SWC001.aspx");
                                }
                            }
							Area01Open_2();
                            break;

                        case "02":


                            if (tSWC021ID == ssUserID)  //承辦技師
                            {
                                tOUT = "N";

                                Area02.Visible = true;

                                Area03Close();

                                Area05Close();

                                switch (tCaseStatus)
                                {
                                    case "申請中":
                                    case "受理中":
                                    case "退補件":
                                        Area01Open();
                                        Area01Open_2();
                                        break;
                                    case "審查中":
                                        Area01Open_2();
                                        break;
                                }
                            }

                            if (tOUT == "Y" && rCaseId != "ADDNEW")
                            {
                                Response.Redirect("SWC001.aspx");
                            }
                            break;

                        case "03":
                            Response.Redirect("SWC001.aspx");
                            break;

                        case "04":

                            if (tOUT == "Y")
                            {
                                Response.Redirect("SWC001.aspx");
                            }
                            break;

                    }
                }
            }
			if(GVCadastral.Rows.Count > 0) P_Message.Visible = true;
			else P_Message.Visible = false;
            
            //以下全區公用


            ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
            Visitor.Text = SBApp.GetVisitorsCount();

            TextUserName.Text = "";
            if (ssUserName != "")
            {
                TextUserName.Text = ssUserName + ssJobTitle + "，您好";
            }
        }
    }
    private string chkUpdateStatus(string vO,string tmpRGType)
    {
        string rValue = vO;
        string ssUserID = Session["ID"] + "";
        //召集人才能改
        string chkGuildType = " select * from GuildGroup Where ETID='" + ssUserID + "' And RGType='" + tmpRGType + "' and CHGType='1'; ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(chkGuildType, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
                rValue = "N";
            readerData.Close();
            objCmdRV.Dispose();
        }
        return rValue;
    }

    protected void SqlDataSource01_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int aa = e.AffectedRows;
        if (aa == 0) LBSWC035.Visible = true;
    }

    protected void SqlDataSource02_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        int aa = e.AffectedRows;
        if (aa == 0) LBSWC040.Visible = true;

    }
    private void SetPayData(string vSwc000)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string ssUserName = Session["NAME"] + "";
        string payData01 = " select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CONVERT(varchar(100), CPI004, 23) as CPI004 from tslm2.dbo.CasePaymentInfo where CaseID = '" + vSwc000 + "' and CaseType = '審查費' and CPI006='已列印' order by id; ";
        SqlDataSource01.SelectCommand = payData01;
        string payData02 = " select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CONVERT(varchar(100), CPI004, 23) as CPI004 from tslm2.dbo.CasePaymentInfo where CaseID = '" + vSwc000 + "' and CaseType = '保證金' and CPI006='已列印' order by id; ";
        SqlDataSource02.SelectCommand = payData02;
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aa = Convert.ToInt32(LButton.CommandArgument);
        string PBCode = GVPay02.Rows[aa].Cells[0].Text.ToString().Replace("&nbsp;", "");
        Response.Write("<script>window.open('../SwcReport/PPayBillSM02.aspx?pno=" + PBCode + "');</script>");
    }
    private bool ChkHavCase()
    {
        bool rValue = false;
        string ssPreviousSwcNo = Session["CopyCaseIdD2"] + "";
        string pCkkSwcNo = (ssPreviousSwcNo.Length > 12) ? ssPreviousSwcNo.Substring(0, 12) + "-" + (int.Parse(ssPreviousSwcNo.Substring(13, 1)) + 1).ToString() : ssPreviousSwcNo + "-1";

        string strSQLRV = " select * from SWCCASE where SWC002 = '" + pCkkSwcNo + "' and SWC004<>'不予受理' and SWC004<>'不予核定' and SWC004<>'廢止' and SWC004<>'失效' and SWC004<>'撤銷'; ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
                rValue = true;
        }
        return rValue;
    }

    private void CopySwcCase()
    {
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";

        string ssPreviousCaseId = Session["CopyCaseIdD1"]+"";
        string ssPreviousSwcNo = Session["CopyCaseIdD2"] + "";
        
        if (ssPreviousCaseId=="" || ssPreviousSwcNo=="")
            Response.Redirect("SWC001.aspx");

        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();
        LandDetailClass LDC = new LandDetailClass();

        #region 變更設計
        string pCaseId = "SWC" + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
        //string pSwcNo = (ssPreviousSwcNo.Length > 12) ? ssPreviousSwcNo.Substring(0, 12) + "-"+ (int.Parse(ssPreviousSwcNo.Substring(13, 1))+1).ToString(): ssPreviousSwcNo + "-1";
        string pSwcNo = getCopyCaseId(ssPreviousSwcNo.Substring(0, 12));

        LBSWC000.Text = pCaseId;
        LBSWC002.Text = pSwcNo;

        #region 原案load
        string strSQLRV = " select * from SWCCASE where SWC000 = '" + ssPreviousCaseId + "' ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            
            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tmpCase004 = "申請中";
                string tmpCase005 = readeSwc["SWC005"] + "";
                #region
                string tmpCase007 = readeSwc["SWC007"] + "";
                string tmpCase013 = readeSwc["SWC013"] + "";
                string tmpCase013ID = readeSwc["SWC013ID"] + "";
                string tmpCase013TEL = readeSwc["SWC013TEL"] + "";
                string tmpCase014 = readeSwc["SWC014"] + "";
                string tmpCase015 = readeSwc["SWC015"] + "";
                string tmpCase016 = readeSwc["SWC016"] + "";
                string tmpCase017 = readeSwc["SWC017"] + "";
                string tmpCase018 = readeSwc["SWC018"] + "";
                string tmpCase023 = readeSwc["SWC023"] + "";
                string tmpCase024 = readeSwc["SWC024"] + "";
                string tmpCase024ID = readeSwc["SWC024ID"] + "";
                string tmpCase025 = readeSwc["SWC025"] + "";
                string tmpCase027 = readeSwc["SWC027"] + "";
                string tmpCase028 = readeSwc["SWC028"] + "";
                string tmpCase041 = readeSwc["SWC041"] + "";
                string tmpCase043 = SBApp.DateView(readeSwc["SWC043"] + "", "00");
                string tmpCase044 = readeSwc["SWC044"] + "";
                string tmpCase045 = readeSwc["SWC045"] + "";
                string tmpCase045ID = readeSwc["SWC045ID"] + "";
                string tmpCase047 = readeSwc["SWC047"] + "";
                string tmpCase048 = readeSwc["SWC048"] + "";
                string tmpCase049 = readeSwc["SWC049"] + "";
                string tmpCase050 = readeSwc["SWC050"] + "";
                string tmpCase051 = SBApp.DateView(readeSwc["SWC051"] + "", "00");
                string tmpCase052 = SBApp.DateView(readeSwc["SWC052"] + "", "00");
                string tmpCase082 = SBApp.DateView(readeSwc["SWC082"] + "", "00");
                string tmpCase083 = readeSwc["SWC083"] + "";
                string tmpCase108 = readeSwc["SWC108"] + "";
                string tmpCase112 = SBApp.DateView(readeSwc["SWC112"] + "", "00");
                #endregion

                #region Label                           
                string[] aLBValue = new string[] { tmpCase004, ssUserName, ssUserID, tmpCase024, tmpCase024ID, tmpCase025, tmpCase041, tmpCase043, tmpCase044, tmpCase045, tmpCase045ID, tmpCase047, tmpCase052, tmpCase082, tmpCase083, tmpCase112 };
                Label[] aLabel = new Label[] { LBSWC004, LBSWC021, LBSWC021ID, LBSWC024, LBSWC024ID, LBSWC025, LBSWC041, LBSWC043, LBSWC044, LBSWC045, LBSWC045ID, LBSWC047, LBSWC052, LBSWC082, LBSWC083, LBSWC112 };
                for (int i = 0; i < aLBValue.Length; i++)
                {
                    string strTBValue = aLBValue[i];
                    System.Web.UI.WebControls.Label LabelObj = aLabel[i];
                    LabelObj.Text = strTBValue;
                }
                #endregion
                #region textbox                            
                string[] aTBValue = new string[] { tmpCase005, tmpCase013, tmpCase013ID, tmpCase013TEL, tmpCase014, tmpCase015, tmpCase016, tmpCase018, tmpCase023, tmpCase027, tmpCase028, tmpCase048, tmpCase049, tmpCase050, tmpCase051, tmpCase108 };
                TextBox[] aTextBox = new TextBox[] { TXTSWC005, TXTSWC013, TXTSWC013ID, TXTSWC013TEL, TXTSWC014, TXTSWC015, TXTSWC016, TXTSWC018, TXTSWC023, TXTSWC027, TXTSWC028, TXTSWC048, TXTSWC049, TXTSWC050, TXTSWC051, TXTSWC108 };
                for (int i = 0; i < aTBValue.Length; i++)
                {
                    string strTBValue = aTBValue[i];
                    System.Web.UI.WebControls.TextBox TextBoxObj = aTextBox[i];
                    TextBoxObj.Text = strTBValue;
                }
                #endregion
                #region DropDownList
                string[] aDDLValue = new string[] { tmpCase007, tmpCase017 };
                DropDownList[] aDropDownList = new DropDownList[] { DDLSWC007, DDLSWC017 };
                for (int i = 0; i < aDDLValue.Length; i++)
                {
                    string strDDLValue = aDDLValue[i];
                    System.Web.UI.WebControls.DropDownList DropDownListObj = aDropDownList[i];
                    DropDownListObj.Text = strDDLValue;
                }
                #endregion
                #region 地籍
                ViewState["SwcCadastral"] = Session["TempSwcCadastral"];
                //update 地籍
				DataTable updateLAND = (DataTable)ViewState["SwcCadastral"];
				for (int i=0; i<updateLAND.Rows.Count; i++)
                {
                    string adArea = updateLAND.Rows[i]["區"].ToString();
                    string adSection = updateLAND.Rows[i]["段"].ToString() + "段" + updateLAND.Rows[i]["小段"].ToString() + "小段";
                    string adNum = updateLAND.Rows[i]["地號"].ToString();
                    string[] arrayCD = C20.CadastralInfo(adArea, adSection, adNum);
                    string newA = "", newB = "", newC = "", newD = "", newE = "", newF = "";

                    //newA = arrayCD[1] == "是" ? "宜林地" : arrayCD[1] == "否" ? "否" : "";
                    //newA = arrayCD[2] == "是" ? "宜農牧地" : arrayCD[2] == "否" ? newA == "" ? "否" : newA : "";
                    //newA = arrayCD[1] == "否" && arrayCD[2] == "否" ? "非屬查定範圍" : newA;
                    if (arrayCD[1] == "是" && arrayCD[2] == "否") newA = "宜林地";
                    if (arrayCD[1] == "否" && arrayCD[2] == "是") newA = "宜農牧地";
                    if (arrayCD[1] == "否" && arrayCD[2] == "否") newA = "非屬範圍內";
                    if (arrayCD[1] == "是" && arrayCD[2] == "是") newA = "宜林地";

                    //newB = arrayCD[6] == "是" ? "本市林地" : "非屬公告範圍";//arrayCD[2] == "宜農牧地" ? "" : "";
                    //newB = arrayCD[2] == "是" ? "宜農牧地" : arrayCD[2] == "否" ? "否" : "";
                    if (arrayCD[4] == "是" && arrayCD[6] == "否") newB = "屬保安林";
                    if (arrayCD[4] == "否" && arrayCD[6] == "是") newB = "屬臺北市林地";
                    if (arrayCD[4] == "否" && arrayCD[6] == "否") newB = "非屬範圍內";
                    if (arrayCD[4] == "是" && arrayCD[6] == "是") newB = "屬保安林";

                    //newC = arrayCD[5] == "是" ? "山崩與地滑" : arrayCD[5] == "否" ? "否" : "";
                    if (arrayCD[5] == "是") newC = "屬地質敏感區";
                    if (arrayCD[5] == "否") newC = "非屬範圍內";

                    newD = arrayCD[7] == "" ? "" : newD = arrayCD[7];

                    if (arrayCD[0] == "是") newE = "屬山坡地";
                    if (arrayCD[0] == "否") newE = "非屬範圍內";

                    if (arrayCD[3] == "是") newF = "屬國家公園";
                    if (arrayCD[3] == "否") newF = "非屬範圍內";

                    updateLAND.Rows[i]["山坡地範圍"] = newE;
                    updateLAND.Rows[i]["土地使用分區"] = newD;
                    updateLAND.Rows[i]["土地可利用限度"] = newA;
                    updateLAND.Rows[i]["陽明山國家公園範圍"] = newF;
                    updateLAND.Rows[i]["林地類別"] = newB;
                    updateLAND.Rows[i]["地質敏感區"] = newC; 
                    
                    string getswc = (LDC.getSWC(updateLAND.Rows[i]["區"].ToString(), updateLAND.Rows[i]["段"].ToString(), updateLAND.Rows[i]["小段"].ToString(), updateLAND.Rows[i]["地號"].ToString()).ToString() == "True") ? "有" : "無";
                    string getilg = (LDC.getILG(updateLAND.Rows[i]["區"].ToString(), updateLAND.Rows[i]["段"].ToString(), updateLAND.Rows[i]["小段"].ToString(), updateLAND.Rows[i]["地號"].ToString()).ToString() == "True") ? "有" : "無";

                    updateLAND.Rows[i]["水保計畫申請紀錄"] = getswc;
                    updateLAND.Rows[i]["水土保持法違規紀錄"] = getilg;

                }
                ViewState["SwcCadastral"] = updateLAND;
				//update 地籍
                GVCadastral.DataSource = (DataTable)ViewState["SwcCadastral"];
                GVCadastral.DataBind();
                int nj = GVCadastral.Rows.Count;
                CDNO.Text = nj.ToString();
                #endregion
                #region 水保設施核定項目
                ViewState["SwcDocItem"] = Session["TempSwcDocItem"];
                SDIList.DataSource = (DataTable)ViewState["SwcDocItem"];
                SDIList.DataBind();
                int njj = SDIList.Rows.Count;
                TXTSDINI.Text = njj.ToString();
                #endregion
				
				#region 義務人資訊
                ViewState["SwcPeople"] = Session["TempSwcPeople"];
                GVPEOPLE.DataSource = (DataTable)ViewState["SwcPeople"];
                GVPEOPLE.DataBind();
				int njjj = GVPEOPLE.Rows.Count;
				if (njjj > 0)
					AddNO.Text = GVPEOPLE.Rows[njjj-1].Cells[0].Text.ToString();
                #endregion
                
            }
        }
        Session["CopyCaseIdD1"] = "";
        Session["CopyCaseIdD2"] = "";
        Session["TempSwcCadastral"] = null;
        Session["TempSwcDocItem"] = null;
		Session["TempSwcPeople"] = null;
		
        #endregion
        #endregion
    }

    private string getCopyCaseId(string vCopyFromID12L)
    {
        string rValue = vCopyFromID12L + "-1";
        string sqlStr = " select top 1 * from SWCCASE where left(SWC002,12)=@SWC002 order by SWC000 Desc;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC002", vCopyFromID12L.Substring(0,12)));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    if (readerSwc.HasRows)
                        while (readerSwc.Read()) {
                            string tmpSwc002 = readerSwc["SWC002"] + "";
                            rValue = tmpSwc002.Length > 12 ? tmpSwc002.Substring(0, 12) + "-" + (int.Parse(tmpSwc002.Substring(13, 1)) + 1).ToString() : tmpSwc002 + "-1";
                        }
                    readerSwc.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    private void GetSwcData(string vSwcID)
    {
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        GBClass001 SBApp = new GBClass001();

        if (vSwcID.Trim() == "ADDNEW")
        {
            //新建
            string pSwcId=GetNewId();
            string tSwc004 = "申請中";
            string LBSWC000ID = "SWC" + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");

            Session["CaseStatus"] = tSwc004;

            switch (ssUserType)
            {
                case "01":
                    DDLSWC007.SelectedValue = "簡易水保";
                    DDLSWC007.Enabled = false;
                    break;
				default:
					LBSWC021.Text = ssUserName;
					break;
            }
            
            TXTSWC108_chk.Text = SBApp.GetETUser(ssUserID, "Email"); ;

            LBSWC000.Text = LBSWC000ID;
            LBSWC002.Text = pSwcId;
            LBSWC004.Text = tSwc004;
            DDLSWC007.Visible = true;
            LBSWC007.Visible = false;
            LBSWC007.Text = "";
            //LBSWC021.Text = ssUserName;

            Area02.Visible = false;
            Area03.Visible = false;
            Area04.Visible = false;
            Area05.Visible = false;
        }
        else
        {
            SetSwcCase(vSwcID);
        }
    }
    private void SetSwcCase(string rSWCNO)
    {
        GBClass001 SBApp = new GBClass001();
        Class1 C1 = new Class1();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string qSWC120 = "";

        #region swcArea1
        string sqlStr1 = " SELECT * FROM SWCSWCA where SWC000=@SWC000 ";
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
                cmd.Parameters.Add(new SqlParameter("@SWC000", rSWCNO));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            qSWC120 = readerTslm["SWC120"] + "";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion

        #region textbox
        string[] arrayValue = new string[] { qSWC120 };
        TextBox[] arrayOBJTB = new TextBox[] { TBSWC120 };
        for (int i=0;i<arrayValue.Length;i++) {
            arrayOBJTB[i].Text = arrayValue[i];
        }
        #endregion

        #region-預設值
        string qSWC000 = "", qSWC001 = "", qSWC002 = "", qSWC004 = "", qSWC005 = "", qSWC007 = "";
        string qSWC012 = "", qSWC013 = "", qSWC014 = "", qSWC015 = "", qSWC016 = "", qSWC017 = "", qSWC018 = "";//, qSWC004 = "", qSWC005 = "", qSWC007 = "";
        string qSWC031 = "", qSWC032 = "", qSWC033 = "", qSWC034 = "", qSWC035 = "", qSWC036 = "", qSWC037 = "", qSWC038 = "", qSWC039 = "", qSWC040 = "";
        string qSWC021 = "", qSWC022 = "", qSWC023 = "", qSWC024 = "", qSWC025 = "", qSWC026 = "", qSWC027 = "", qSWC028 = "", qSWC029 = "", qSWC030 = "";
        string qSWC041 = "", qSWC042 = "", qSWC043 = "", qSWC044 = "", qSWC045 = "", qSWC046 = "", qSWC047 = "", qSWC048 = "", qSWC049 = "", qSWC050 = "";
        string qSWC051 = "", qSWC052 = "", qSWC053 = "", qSWC054 = "", qSWC055 = "", qSWC056 = "", qSWC057 = "", qSWC058 = "", qSWC059 = "", qSWC060 = "";
        string qSWC061 = "", qSWC062 = "", qSWC063 = "", qSWC064 = "", qSWC065 = "", qSWC066 = "", qSWC067 = "", qSWC068 = "", qSWC069 = "", qSWC070 = "";
        string qSWC071 = "", qSWC072 = "", qSWC073 = "", qSWC074 = "", qSWC075 = "", qSWC076 = "", qSWC077 = "", qSWC078 = "", qSWC079 = "", qSWC080 = "";
        string qSWC081 = "", qSWC082 = "", qSWC083 = "", qSWC084 = "", qSWC085 = "", qSWC086 = "", qSWC087 = "", qSWC088 = "", qSWC089 = "", qSWC090 = "";
        string qSWC091 = "", qSWC092 = "", qSWC093 = "", qSWC094 = "", qSWC095 = "", qSWC096 = "", qSWC097 = "", qSWC098 = "", qSWC099 = "", qSWC100 = "";
        string qSWC101 = "", qSWC103 = "", qSWC104 = "", qSWC105 = "", qSWC106 = "", qSWC108 = "", qSWC109 = "", qSWC110 = "";// qSWC096 = "", qSWC097 = "";
        string qSWC112 = "", qSWC113 = "", qSWC114 = "", qSWC115 = "";// qSWC096 = "", qSWC097 = "";
        string qSWC013ID = "", qSWC021ID = "", qSWC045ID = "", qSWC022ID = "", qSWC024ID = "", qSWC107ID = "";
        string qSWC029CAD = "", qSWC013TEL="", qSWC101CAD="";
		string qSWC134 = "", qSWC138 = "";
        #endregion

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + " where SWC000 = '" + rSWCNO + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                #region-取得db資料
                qSWC000 = readeSwc["SWC000"] + "";
                qSWC002 = readeSwc["SWC002"] + "";
                qSWC004 = readeSwc["SWC004"] + "";
                qSWC005 = readeSwc["SWC005"] + "";
                qSWC007 = readeSwc["SWC007"] + "";
                qSWC012 = readeSwc["SWC012"] + "";
                qSWC013 = readeSwc["SWC013"] + "";
                qSWC014 = readeSwc["SWC014"] + "";
                qSWC015 = readeSwc["SWC015"] + "";
                qSWC016 = readeSwc["SWC016"] + "";
                qSWC017 = readeSwc["SWC017"] + "";
                qSWC018 = readeSwc["SWC018"] + "";
                qSWC021 = readeSwc["SWC021"] + "";
                qSWC022 = readeSwc["SWC022"] + "";
                qSWC023 = readeSwc["SWC023"] + "";
                qSWC024 = readeSwc["SWC024"] + "";
                qSWC025 = readeSwc["SWC025"] + "";
                qSWC027 = readeSwc["SWC027"] + "";
                qSWC028 = readeSwc["SWC028"] + "";
                qSWC029 = readeSwc["SWC029"] + "";
                qSWC029CAD = readeSwc["SWC029CAD"] + "";
                qSWC030 = readeSwc["SWC030"] + "";
                qSWC031 = readeSwc["SWC031"] + "";
                qSWC032 = readeSwc["SWC032"] + "";
                qSWC033 = readeSwc["SWC033"] + "";
                qSWC034 = readeSwc["SWC034"] + "";
                qSWC035 = readeSwc["SWC035"] + "";
                qSWC036 = readeSwc["SWC036"] + "";
                qSWC038 = readeSwc["SWC038"] + "";
                qSWC039 = readeSwc["SWC039"] + "";
                qSWC040 = readeSwc["SWC040"] + "";
                qSWC041 = readeSwc["SWC041"] + "";
                qSWC043 = readeSwc["SWC043"] + "";
                qSWC044 = readeSwc["SWC044"] + "";
                qSWC045 = readeSwc["SWC045"] + "";
                qSWC048 = readeSwc["SWC048"] + "";
                qSWC049 = readeSwc["SWC049"] + "";
                qSWC050 = readeSwc["SWC050"] + "";
                qSWC051 = readeSwc["SWC051"] + "";
                qSWC052 = readeSwc["SWC052"] + "";
                qSWC053 = readeSwc["SWC053"] + "";
                qSWC056 = readeSwc["SWC056"] + "";
                qSWC058 = readeSwc["SWC058"] + "";
                qSWC059 = readeSwc["SWC059"] + "";
                qSWC061 = readeSwc["SWC061"] + "";
                qSWC062 = readeSwc["SWC062"] + "";
                qSWC063 = readeSwc["SWC063"] + "";
                qSWC064 = readeSwc["SWC064"] + "";
                qSWC065 = readeSwc["SWC065"] + "";
                qSWC066 = readeSwc["SWC066"] + "";
                qSWC067 = readeSwc["SWC067"] + "";
                qSWC068 = readeSwc["SWC068"] + "";
                qSWC069 = readeSwc["SWC069"] + "";
                qSWC070 = readeSwc["SWC070"] + "";
                qSWC071 = readeSwc["SWC071"] + "";
                qSWC072 = readeSwc["SWC072"] + "";
                qSWC073 = readeSwc["SWC073"] + "";
                qSWC074 = readeSwc["SWC074"] + "";
                qSWC075 = readeSwc["SWC075"] + "";
                qSWC080 = readeSwc["SWC080"] + "";
                qSWC082 = readeSwc["SWC082"] + "";
                qSWC083 = readeSwc["SWC083"] + "";
                qSWC084 = readeSwc["SWC084"] + "";
                qSWC085 = readeSwc["SWC085"] + "";
                qSWC087 = readeSwc["SWC087"] + "";
                qSWC088 = readeSwc["SWC088"] + "";
                qSWC089 = readeSwc["SWC089"] + "";
                qSWC092 = readeSwc["SWC092"] + "";
                qSWC093 = readeSwc["SWC093"] + "";
                qSWC094 = readeSwc["SWC094"] + "";
                qSWC095 = readeSwc["SWC095"] + "";
                qSWC101 = readeSwc["SWC101"] + "";
                qSWC101CAD = readeSwc["SWC101CAD"] + "";
                qSWC103 = readeSwc["SWC103"] + "";
                qSWC104 = readeSwc["SWC104"] + "";
                qSWC105 = readeSwc["SWC105"] + "";
                qSWC106 = readeSwc["SWC106"] + "";
                qSWC108 = readeSwc["SWC108"] + "";
                qSWC109 = readeSwc["SWC109"] + "";
                qSWC110 = readeSwc["SWC110"] + "";
                qSWC113 = readeSwc["SWC113"] + "";
                qSWC114 = readeSwc["SWC114"] + "";
                qSWC115 = readeSwc["SWC115"] + "";
                qSWC134 = readeSwc["SWC134"] + "";
                qSWC138 = readeSwc["SWC138"] + "";

                qSWC013ID = readeSwc["SWC013ID"] + "";
                qSWC021ID = readeSwc["SWC021ID"] + "";
                qSWC045ID = readeSwc["SWC045ID"] + "";
                qSWC022ID = readeSwc["SWC022ID"] + "";
                qSWC024ID = readeSwc["SWC024ID"] + "";
                qSWC107ID = readeSwc["SWC107ID"] + "";

                qSWC013TEL = readeSwc["SWC013TEL"] + "";

                #region-委員下拉
                DropDownList[] arryDDLA = new DropDownList[] { DDLSA01, DDLSA02, DDLSA03, DDLSA04, DDLSA05, DDLSA06, DDLSA07, DDLSA08, DDLSA09, DDLSA10 };
                DropDownList[] arryDDLB = new DropDownList[] { DDLSB01, DDLSB02 };
                for (int i = 0; i < arryDDLA.Length; i++)
                {
                    DropDownList DDLTMP = arryDDLA[i];
                    DDLTMP.Items.Clear();
                    DDLTMP.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));
                }
                for (int i = 0; i < arryDDLB.Length; i++)
                {
                    DropDownList DDLTMP = arryDDLB[i];
                    DDLTMP.Items.Clear();
                    DDLTMP.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));
                }
                string serviceGuild1 = qSWC022ID== "ge-50702" ? "OR ISNULL(ServiceSubstitute,'')= 'Y'" : "";
                string strSQLCaseA = " select* from ETUsers Where((ISNULL(GuildSubstitute,'') = '" + qSWC022ID + "' and GuildTcgeChk = '1')"+ serviceGuild1 + " ) AND STATUS = '已開通';";
                using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                {
                    SWCConn.Open();

                    SqlDataReader readerData;
                    SqlCommand objCmdRV = new SqlCommand(strSQLCaseA, SWCConn);
                    readerData = objCmdRV.ExecuteReader();

                    while (readerData.Read())
                    {
                        string tETID = readerData["ETID"] + "";
                        string tETName = readerData["ETName"] + "";

                        for (int i = 0; i < arryDDLA.Length; i++)
                        {
                            DropDownList DDLTMP = arryDDLA[i];
                            DDLTMP.Items.Add(new System.Web.UI.WebControls.ListItem(tETName, tETID));
                        }
                    }
                    readerData.Close();
                    objCmdRV.Dispose();
                }
                string serviceGuild2 = qSWC024ID == "ge-50702" ? "OR ISNULL(ServiceSubstitute,'')= 'Y'" : "";
                string strSQLCaseB = " select* from ETUsers Where((ISNULL(GuildSubstitute2,'') = '" + qSWC024ID + "' and GuildTcgeChk2 = '1') "+ serviceGuild2 + ") AND STATUS = '已開通'; ";
                using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                {
                    SWCConn.Open();

                    SqlDataReader readerData;
                    SqlCommand objCmdRV = new SqlCommand(strSQLCaseB, SWCConn);
                    readerData = objCmdRV.ExecuteReader();

                    while (readerData.Read())
                    {
                        string tETID = readerData["ETID"] + "";
                        string tETName = readerData["ETName"] + "";

                        for (int i = 0; i < arryDDLB.Length; i++)
                        {
                            DropDownList DDLTMP = arryDDLB[i];
                            DDLTMP.Items.Add(new System.Web.UI.WebControls.ListItem(tETName, tETID));
                        }
                    }
                    readerData.Close();
                    objCmdRV.Dispose();
                }
                #endregion
                #endregion

                Session["CaseStatus"] = qSWC004;
				
				//*******************
				//TOKEN
				//string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
				//string token = ssUserID + "|" + ssUserType + "|" + ssUserName + "|" + dt;
				//byte[] b = Encoding.UTF8.GetBytes(token);
				//DESCryptoServiceProvider des = new DESCryptoServiceProvider();
				//var Key = new Byte[] { };
				//var IV = new Byte[] { };
				//ICryptoTransform ict = des.CreateEncryptor(Key,IV);
				//byte[] outData = ict.TransformFinalBlock(b, 0, b.Length);
				//var op = Convert.ToBase64String(outData);
				//Response.Write("https://tcgegis.geonet.tw/ID_ortented.php?UTYPE="+ ssUserType + "&UNAME="+ ssUserName+ "&rid=" + qSWC002 + "&token=" + op);
				//Response.Write("<br/>");
				//b = Convert.FromBase64String(op);
				//des = new DESCryptoServiceProvider();
				//ict = des.CreateDecryptor(ASCIIEncoding.ASCII.GetBytes(Key), ASCIIEncoding.ASCII.GetBytes(IV));
				//outData = ict.TransformFinalBlock(b, 0, b.Length);
				//op = Encoding.UTF8.GetString(outData);
				//Response.Write("https://tcgegis.geonet.tw/ID_ortented.php?UTYPE="+ ssUserType + "&UNAME="+ ssUserName+ "&rid=" + qSWC002 + "&token=" + op);
				//*******************

				//OutLink1.NavigateUrl = "https://tcgegis.geonet.tw/ID_ortented.php?UTYPE="+ ssUserType + "&UNAME="+ ssUserName+ "&rid=" + qSWC002 + "&Token=" + op;
                //OutLink1.NavigateUrl = "https://tcgegis.geonet.tw/?rid=" + qSWC002;
            }
        }

        #region tslm
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            string strQtslm = " select * from SWCSWC where SWC00 = '" + rSWCNO + "' ";

            SqlDataReader readeTslm;
            SqlCommand objCmdTslm = new SqlCommand(strQtslm, TslmConn);
            readeTslm = objCmdTslm.ExecuteReader();

            while (readeTslm.Read())
            {
                qSWC031 = readeTslm["SWC31"] + "";   //2
                qSWC031 = SBApp.DateView(qSWC031, "00");
                qSWC033 = readeTslm["SWC33"] + "";   //2
                qSWC033 = SBApp.DateView(qSWC033, "00");
            }
        }
        #endregion

        #region-資料2畫面
        CHKSWC061.Checked = false;
        CHKSWC063.Checked = false;
        CHKSWC065.Checked = false;
        
        LBSWC000.Text = qSWC000;
        LBSWC002.Text = qSWC002;
        LBSWC004.Text = qSWC004;
        TXTSWC005.Text = qSWC005;
        DDLSWC007.SelectedValue = qSWC007;
        LBSWC012.Text = qSWC012;
        TXTSWC013.Text = qSWC013;
        TXTSWC013ID.Text = qSWC013ID;
        TXTSWC013TEL.Text = qSWC013TEL;
        TXTSWC014.Text = qSWC014;
        TXTSWC015.Text = qSWC015;
        TXTSWC016.Text = qSWC016;
        DDLSWC017.SelectedValue = qSWC017;
        TXTSWC018.Text = qSWC018;
        //LBSWC007.Text = qSWC007;
        LBSWC021.Text = SBApp.GetETUser(qSWC021ID, "Name");
        LBSWC021ID.Text = qSWC021ID;
        LBSWC022.Text = qSWC022;
        LBSWC022ID.Text = qSWC022ID;
        TXTSWC023.Text = qSWC023;
        LBSWC024.Text = qSWC024;
        LBSWC024ID.Text = qSWC024ID;
        LBSWC025.Text = qSWC025;
        TXTSWC027.Text = qSWC027;
        TXTSWC028.Text = qSWC028;
        TXTSWC029.Text = qSWC029;
        TXTSWC029CAD.Text = qSWC029CAD;
        TXTSWC030.Text = qSWC030;
        LBSWC031.Text = SBApp.DateView(qSWC031, "00");
        LBSWC032.Text = SBApp.DateView(qSWC032, "00"); 
        LBSWC033.Text = SBApp.DateView(qSWC033, "00"); 
        LBSWC034.Text = SBApp.DateView(qSWC034, "00"); 
        LBSWC035.Text = qSWC035;
        LBSWC036.Text = qSWC036;
        LBSWC038.Text = SBApp.DateView(qSWC038, "00");
        LBSWC039.Text = qSWC039;
        LBSWC040.Text = qSWC040;
        LBSWC041.Text = qSWC041;
        LBSWC043.Text = SBApp.DateView(qSWC043, "00");
        LBSWC044.Text = qSWC044;
        LBSWC045.Text = qSWC045;
        LBSWC045ID.Text = qSWC045ID;
        TXTSWC048.Text = qSWC048;
        TXTSWC049.Text = qSWC049;
        TXTSWC050.Text = qSWC050;
        TXTSWC051.Text= SBApp.DateView(qSWC051, "00");
        LBSWC052.Text = SBApp.DateView(qSWC052, "00");
        LBSWC053.Text = SBApp.DateView(qSWC053, "00");
        LBSWC056.Text = qSWC056;
        TXTSWC058.Text = SBApp.DateView(qSWC058, "00");
        LBSWC059.Text = SBApp.DateView(qSWC059,"00");
        TXTSWC062.Text = qSWC062;
        TXTSWC064.Text = qSWC064;
        TXTSWC066.Text = qSWC066;
        TXTSWC068.Text = qSWC068;
        TXTSWC069.Text = qSWC069;
        TXTSWC070.Text = qSWC070;
        TXTSWC072.Text = qSWC072;
        TXTSWC074.Text = qSWC074;
        TXTSWC080.Text = qSWC080;
        LBSWC082.Text = SBApp.DateView(qSWC082, "00");
        LBSWC083.Text = qSWC083;
        LBSWC084.Text = SBApp.DateView(qSWC084, "00");
        TXTSWC087.Text = qSWC087;
        LBSWC088.Text = SBApp.DateView(qSWC088, "00");
        LBSWC089.Text = SBApp.DateView(qSWC089, "00");
        LBSWC092.Text = qSWC092;
        TXTSWC093.Text = qSWC093;
        TXTSWC094.Text = qSWC094;
        TXTSWC095.Text = qSWC095;
        TXTSWC101.Text = qSWC101;
        LBSWC103.Text = qSWC103;
        LBSWC104.Text = SBApp.DateView(qSWC104, "00");
        LBSWC105.Text = qSWC105;
        LBSWC107ID.Text = qSWC107ID;
        TXTSWC108.Text = qSWC108;
        TXTSWC108_chk.Text = SBApp.GetETUser(qSWC021ID, "Email");
        TXTSWC109.Text = SBApp.DateView(qSWC109, "00");
        TBSWC109o.Text = TXTSWC109.Text;
        TXTSWC110.Text = qSWC110;
        TXTSWC101CAD.Text = qSWC101CAD;
        DDLSWC134.SelectedValue = qSWC134;
		if(qSWC138 != "")
		{
			TXTSWC138.Text = qSWC138;
			Link138.Text = qSWC138;
			Link138.NavigateUrl = SBApp.getFileUrl(LBSWC000.Text, LBSWC002.Text, DDLSWC007.Text, "TXTSWC138") + qSWC138;
		}

        if (qSWC061 == "1") { CHKSWC061.Checked = true; }
        if (qSWC063 == "1") { CHKSWC063.Checked = true; }
        if (qSWC065 == "1") { CHKSWC065.Checked = true; }
        if (qSWC067 == "1") { CHKSWC067.Checked = true; }
        if (qSWC071 == "1") { CHKSWC071.Checked = true; }
        if (qSWC073 == "1") { CHKSWC073.Checked = true; }
        if (qSWC075 == "1") { CHKSWC075.Checked = true; }

        string[] arrayDATEValue = new string[] { qSWC113, qSWC114, qSWC115 };
        TextBox[] arrayTXTDATE = new TextBox[] { TXTSWC113, TXTSWC114, TXTSWC115 };

        for (int i = 0; i < arrayDATEValue.Length; i++) arrayTXTDATE[i].Text = SBApp.DateView(arrayDATEValue[i], "00");

        #endregion

        #region-委員名單
        string tLBSAOID = "";
        int ii = 0;
        DropDownList[] arryDDL01 = new DropDownList[] { DDLSA01, DDLSA02, DDLSA03, DDLSA04, DDLSA05, DDLSA06, DDLSA07, DDLSA08, DDLSA09, DDLSA10, DDLSB01, DDLSB02 };
        string exeSqlStr = " select * from GuildGroup where swc000='"+ rSWCNO + "' order by convert(float,RGSID) ";
        using (SqlConnection DDL01Conn = new SqlConnection(connectionString.ConnectionString))
        {
            DDL01Conn.Open();
            SqlDataReader readerItemGG;
            SqlCommand objCmdItemGG = new SqlCommand(exeSqlStr, DDL01Conn);
            readerItemGG = objCmdItemGG.ExecuteReader();

            while (readerItemGG.Read()) {
                string tmpUserID = readerItemGG["ETID"] + "";
                arryDDL01[ii++].Text = tmpUserID.Trim();
                tLBSAOID += tmpUserID + ";;";
            }
        }
        LBSAOID.Text = tLBSAOID;
        #endregion

        //檔案類處理
        if (1==2)
        {
            string[] arrayFileNameLink = new string[] { qSWC029, qSWC029CAD, qSWC030, qSWC080, qSWC101, qSWC106, qSWC110, qSWC101CAD };
            System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link029, Link029CAD, Link030, Link080, Link101, Link106, Link110, Link101CAD };
            string[] arrayFileType = new string[] { "6-1", "", "7-1", "核定本", qSWC101, qSWC106, qSWC110, qSWC101CAD };

            for (int i = 0; i < arrayFileNameLink.Length; i++)
            {
                string strFileName = arrayFileNameLink[i];
                string fileType = arrayFileType[i];
                System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];


                FileLinkObj.Visible = false;
                if (strFileName == "")
                {
                }
                else
                {
                    string extension = Path.GetExtension(strFileName).ToLowerInvariant();

                    string NewUpath = @"~\OutputFile\" + strFileName;
                    string tempLinkPateh = SwcUpLoadFilePath + rSWCNO + "/" + strFileName;
                    if (extension == ".pdf") if (C1.DLFileReMark(rSWCNO, strFileName, "", qSWC002, qSWC007, fileType)) tempLinkPateh = NewUpath;

                    FileLinkObj.Text = strFileName;
                    FileLinkObj.NavigateUrl = tempLinkPateh;
                    FileLinkObj.Visible = true;
                }

            }
        }

        //01.審查，審查公會，可編，ssUserType=04;SWC022ID=ssUserID
        //03.施工，檢查公會，可編，ssUserType=04;SWC024ID=ssUserID
        //04.施工，監造技師，可編，ssUserType=02;SWC045ID=ssUserID
        //05.施工，監造技師，可編，ssUserType=02;SWC045ID=ssUserID
        //06.施工，檢查公會，可編，ssUserType=04;SWC024ID=ssUserID
        //07.完工，完工公會，可編，ssUserType=04;Session["Edit4"] + "" == "Y"

        //string ssUserID = Session["ID"] + "";

        //表1
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql01Str = "";

            Sql01Str = Sql01Str + " select SWC000,DTLA000,DTLA001,DTLA002,DTLA003,DTLA004,DATALOCK from SWCDTL01 ";
            Sql01Str = Sql01Str + "  Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "04" && ssUserID == qSWC022ID) { } else { Sql01Str = Sql01Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql01Str = Sql01Str + "  order by DTLA002 ";

            SqlDataReader readerItem01;
            SqlCommand objCmdItem01 = new SqlCommand(Sql01Str, DTLConn);
            readerItem01 = objCmdItem01.ExecuteReader();

            while (readerItem01.Read())
            {
                string dDTLA01 = readerItem01["DTLA001"] + "";
                string dDTLA02 = readerItem01["DTLA002"] + "";
                string dDTLA03 = readerItem01["DTLA003"] + "";
                string dDTLA04 = readerItem01["DTLA004"] + "";
                string dDTLA05 = readerItem01["DATALOCK"] + "";

                DataTable OBJ_GV01 = (DataTable)ViewState["GV01"];
                DataTable DTGV01 = new DataTable();

                if (OBJ_GV01 == null)
                {
                    DTGV01.Columns.Add(new DataColumn("DTLA001", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA002", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA003", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA004", typeof(string)));
                    DTGV01.Columns.Add(new DataColumn("DTLA005", typeof(string)));

                    ViewState["GV01"] = DTGV01;
                    OBJ_GV01 = DTGV01;
                }
                DataRow dr01 = OBJ_GV01.NewRow();

                dr01["DTLA001"] = dDTLA01;
                dr01["DTLA002"] = dDTLA02;
                dr01["DTLA003"] = dDTLA03;
                dr01["DTLA004"] = dDTLA04;
                dr01["DTLA005"] = dDTLA05;

                OBJ_GV01.Rows.Add(dr01);

                ViewState["GV01"] = OBJ_GV01;

                SWCDTL01.DataSource = OBJ_GV01;
                SWCDTL01.DataBind();
                
            }
        }

        //表3+2
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql0302Str = "";

            Sql0302Str = Sql0302Str + " Select SWC000,DTLC000,[DTLC001] ,CONVERT(varchar(100), [DTLC002], 23) AS DTLC002,DTLC003,DTLC004,DATALOCK  From SWCDTL03 Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "04" && ssUserID == qSWC024ID) { } else { Sql0302Str = Sql0302Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql0302Str = Sql0302Str + " UNION ";
            Sql0302Str = Sql0302Str + " select swc000,DTLB000 AS DTLC000,DTLB001 AS [DTLC001] ,CONVERT(varchar(100), [DTLB002], 23) AS DTLC002,DTLB003 AS DTLC003,DTLB004 AS DTLC004,DATALOCK from SWCDTL02 ";
            Sql0302Str = Sql0302Str + "  Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "04" && ssUserID == qSWC024ID) { } else { Sql0302Str = Sql0302Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql0302Str = Sql0302Str + "  order by DTLC002 ";
            
            SqlDataReader readerItem0302;
            SqlCommand objCmdItem0302 = new SqlCommand(Sql0302Str, DTLConn);
            readerItem0302 = objCmdItem0302.ExecuteReader();

            while (readerItem0302.Read())
            {
                string dDTLC01 = readerItem0302["DTLC001"] + "";
                string dDTLC02 = readerItem0302["DTLC002"] + "";
                string dDTLC03 = readerItem0302["DTLC003"] + "";
                string dDTLC04 = readerItem0302["DTLC004"] + "";
                string dDTLC05 = readerItem0302["DATALOCK"] + "";

                DataTable OBJ_GV0302 = (DataTable)ViewState["GV0302"];
                DataTable DTGV0302 = new DataTable();

                if (OBJ_GV0302 == null)
                {
                    DTGV0302.Columns.Add(new DataColumn("DTLC001", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC002", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC003", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC004", typeof(string)));
                    DTGV0302.Columns.Add(new DataColumn("DTLC005", typeof(string)));

                    ViewState["GV03"] = DTGV0302;
                    OBJ_GV0302 = DTGV0302;
                }
                DataRow dr0302 = OBJ_GV0302.NewRow();

                dr0302["DTLC001"] = dDTLC01;
                dr0302["DTLC002"] = dDTLC02;
                dr0302["DTLC003"] = dDTLC03;
                dr0302["DTLC004"] = dDTLC04;
                dr0302["DTLC005"] = dDTLC05;

                OBJ_GV0302.Rows.Add(dr0302);

                ViewState["GV0302"] = OBJ_GV0302;

                SWCDTL0302.DataSource = OBJ_GV0302;
                SWCDTL0302.DataBind();
            }
        }

        //表4
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql04Str = "";

            Sql04Str = Sql04Str + " Select [DTLD001] ,CONVERT(varchar(100), [DTLD002], 23) AS DTLD002,ISNULL(D4.DTLD003,'')+ISNULL(ISNULL(DE.DENAME,DE2.DENAME),'') AS DTLD003,DTLD004,DATALOCK From SWCDTL04 D4 ";
            Sql04Str += " LEFT JOIN DisasterEvent DE ON D4.DTLD085=DE.DENo ";
            Sql04Str += " LEFT JOIN DisasterEvent DE2 ON D4.DENo=DE2.DENo ";
            Sql04Str = Sql04Str + "  Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "02" && ssUserID == qSWC045ID) { } else { Sql04Str = Sql04Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql04Str = Sql04Str + "  order by DTLD002 ";

            SqlDataReader readerItem04;
            SqlCommand objCmdItem04 = new SqlCommand(Sql04Str, DTLConn);
            readerItem04 = objCmdItem04.ExecuteReader();

            while (readerItem04.Read())
            {
                string dDTLD01 = readerItem04["DTLD001"] + "";
                string dDTLD02 = readerItem04["DTLD002"] + "";
                string dDTLD03 = readerItem04["DTLD003"] + "";
                string dDTLD04 = readerItem04["DTLD004"] + "";
                string dDTLD05 = readerItem04["DATALOCK"] + "";

                DataTable OBJ_GV04 = (DataTable)ViewState["GV04"];
                DataTable DTGV04 = new DataTable();

                if (OBJ_GV04 == null)
                {
                    DTGV04.Columns.Add(new DataColumn("DTLD001", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD002", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD003", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD004", typeof(string)));
                    DTGV04.Columns.Add(new DataColumn("DTLD005", typeof(string)));

                    ViewState["GV04"] = DTGV04;
                    OBJ_GV04 = DTGV04;
                }
                DataRow dr04 = OBJ_GV04.NewRow();

                dr04["DTLD001"] = dDTLD01;
                dr04["DTLD002"] = dDTLD02;
                dr04["DTLD003"] = dDTLD03;
                dr04["DTLD004"] = dDTLD04;
                dr04["DTLD005"] = dDTLD05;

                OBJ_GV04.Rows.Add(dr04);

                ViewState["GV04"] = OBJ_GV04;

                SWCDTL04.DataSource = OBJ_GV04;
                SWCDTL04.DataBind();
            }
        }

        //表5
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql05Str = "";

            Sql05Str = Sql05Str + " Select [DTLE001] ,CONVERT(varchar(100), [DTLE002], 23) AS DTLE002,DTLE082 as DTLE003, DTLE004,DATALOCK From SWCDTL05 ";
            Sql05Str = Sql05Str + "  Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "02" && ssUserID == qSWC045ID) { } else { Sql05Str = Sql05Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql05Str = Sql05Str + "  order by DTLE002 ";

            SqlDataReader readerItem05;
            SqlCommand objCmdItem05 = new SqlCommand(Sql05Str, DTLConn);
            readerItem05 = objCmdItem05.ExecuteReader();

            while (readerItem05.Read())
            {
                string dDTLE01 = readerItem05["DTLE001"] + "";
                string dDTLE02 = readerItem05["DTLE002"] + "";
                string dDTLE03 = readerItem05["DTLE003"] + "";
                string dDTLE04 = readerItem05["DTLE004"] + "";
                string dDTLE05 = readerItem05["DATALOCK"] + "";

                DataTable OBJ_GV05 = (DataTable)ViewState["GV05"];
                DataTable DTGV05 = new DataTable();

                if (OBJ_GV05 == null)
                {
                    DTGV05.Columns.Add(new DataColumn("DTLE001", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE002", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE003", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE004", typeof(string)));
                    DTGV05.Columns.Add(new DataColumn("DTLE005", typeof(string)));

                    ViewState["GV05"] = DTGV05;
                    OBJ_GV05 = DTGV05;
                }
                DataRow dr05 = OBJ_GV05.NewRow();

                dr05["DTLE001"] = dDTLE01;
                dr05["DTLE002"] = dDTLE02;
                dr05["DTLE003"] = dDTLE03;
                dr05["DTLE004"] = dDTLE04;
                dr05["DTLE005"] = dDTLE05;

                OBJ_GV05.Rows.Add(dr05);

                ViewState["GV05"] = OBJ_GV05;

                SWCDTL05.DataSource = OBJ_GV05;
                SWCDTL05.DataBind();
            }
        }

        //表6
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql06Str = "";

            Sql06Str = Sql06Str + " Select [DTLF001] ,CONVERT(varchar(100), [DTLF002], 23) AS DTLF002,DTLF003,DTLF004,DATALOCK From SWCDTL06 ";
            Sql06Str = Sql06Str + "  Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "04" && ssUserID == qSWC024ID) { } else { Sql06Str = Sql06Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql06Str = Sql06Str + "  order by DTLF002 ";

            SqlDataReader readerItem06;
            SqlCommand objCmdItem06 = new SqlCommand(Sql06Str, DTLConn);
            readerItem06 = objCmdItem06.ExecuteReader();

            while (readerItem06.Read())
            {
                string dDTLF01 = readerItem06["DTLF001"] + "";
                string dDTLF02 = readerItem06["DTLF002"] + "";
                string dDTLF03 = readerItem06["DTLF003"] + "";
                string dDTLF04 = readerItem06["DTLF004"] + "";
                string dDTLF05 = readerItem06["DATALOCK"] + "";

                DataTable OBJ_GV06 = (DataTable)ViewState["GV06"];
                DataTable DTGV06 = new DataTable();

                if (OBJ_GV06 == null)
                {
                    DTGV06.Columns.Add(new DataColumn("DTLF001", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF002", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF003", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF004", typeof(string)));
                    DTGV06.Columns.Add(new DataColumn("DTLF005", typeof(string)));

                    ViewState["GV06"] = DTGV06;
                    OBJ_GV06 = DTGV06;
                }
                DataRow dr06 = OBJ_GV06.NewRow();

                dr06["DTLF001"] = dDTLF01;
                dr06["DTLF002"] = dDTLF02;
                dr06["DTLF003"] = dDTLF03;
                dr06["DTLF004"] = dDTLF04;
                dr06["DTLF005"] = dDTLF05;

                OBJ_GV06.Rows.Add(dr06);

                ViewState["GV06"] = OBJ_GV06;

                SWCDTL06.DataSource = OBJ_GV06;
                SWCDTL06.DataBind();                
            }
        }

        //表7
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            string Sql07Str = "";

            Sql07Str = Sql07Str + " Select [DTLG001] ,CONVERT(varchar(100), [DTLG002], 23) AS DTLG002,DTLG003,DTLG004,DATALOCK  From SWCDTL07 ";
            Sql07Str = Sql07Str + "  Where SWC000 = '" + rSWCNO + "' ";
            if (ssUserType == "04" && Session["Edit4"] + "" == "Y") { } else { Sql07Str = Sql07Str + "    and isnull(DATALOCK,'')= 'Y' "; }
            Sql07Str = Sql07Str + "  order by DTLG007 ";

            SqlDataReader readerItem07;
            SqlCommand objCmdItem07 = new SqlCommand(Sql07Str, DTLConn);
            readerItem07 = objCmdItem07.ExecuteReader();

            while (readerItem07.Read())
            {
                string dDTLG01 = readerItem07["DTLG001"] + "";
                string dDTLG02 = readerItem07["DTLG002"] + "";
                string dDTLG03 = readerItem07["DTLG003"] + "";
                string dDTLG05 = readerItem07["DATALOCK"] + "";

                DataTable OBJ_GV07 = (DataTable)ViewState["GV07"];
                DataTable DTGV07 = new DataTable();

                if (OBJ_GV07 == null)
                {
                    DTGV07.Columns.Add(new DataColumn("DTLG001", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG002", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG003", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG005", typeof(string)));

                    ViewState["GV07"] = DTGV07;
                    OBJ_GV07 = DTGV07;
                }
                DataRow dr07 = OBJ_GV07.NewRow();

                dr07["DTLG001"] = dDTLG01;
                dr07["DTLG002"] = dDTLG02;
                dr07["DTLG003"] = dDTLG03;
                dr07["DTLG005"] = dDTLG05;

                OBJ_GV07.Rows.Add(dr07);

                ViewState["GV07"] = OBJ_GV07;

                SWCDTL07.DataSource = OBJ_GV07;
                SWCDTL07.DataBind();
            }
        }

        //GVSWCCHG
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();
            
            string strSQLRV = "select * from SWCCASE";
            strSQLRV = strSQLRV + " Where LEFT(SWC002,12) = '" + qSWC002 + "' ";
            strSQLRV = strSQLRV + "   and SWC002 <> '" + qSWC002 + "' ";
            strSQLRV = strSQLRV + " order by SWC002  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dSWC000 = readerItem["SWC000"] + "";
                string dSWC002 = readerItem["SWC002"] + "";

                DataTable tbSWCCHK = (DataTable)ViewState["SWCCHK"];

                if (tbSWCCHK == null)
                {
                    DataTable GVTBSWCDHK = new DataTable();

                    GVTBSWCDHK.Columns.Add(new DataColumn("SWC002", typeof(string)));
                    GVTBSWCDHK.Columns.Add(new DataColumn("SWC002Link", typeof(string)));

                    ViewState["SWCCHK"] = GVTBSWCDHK;
                    tbSWCCHK = (DataTable)ViewState["SWCCHK"];
                }

                DataRow GVTBSWCDHKRow = tbSWCCHK.NewRow();

                GVTBSWCDHKRow["SWC002"] = dSWC002;
                GVTBSWCDHKRow["SWC002Link"] = "SWC003.aspx?SWCNO="+ dSWC000;

                tbSWCCHK.Rows.Add(GVTBSWCDHKRow);

                //Store the DataTable in ViewState
                ViewState["SWCCHK"] = tbSWCCHK;

                GVSWCCHG.DataSource = tbSWCCHK;
                GVSWCCHG.DataBind();
                
            }
            readerItem.Close();
        }

        #region 地籍
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV = "select * from SWCLAND";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rSWCNO + "' ";
            strSQLRV = strSQLRV + " order by convert(int,LAND000)  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dLAND000 = readerItem["LAND000"] + "";
                string dLAND001 = readerItem["LAND001"] + "";
                string dLAND002 = readerItem["LAND002"] + "";
                string dLAND003 = readerItem["LAND003"] + "";
                string dLAND004 = readerItem["LAND004"] + "";
                string dLAND005 = readerItem["LAND005"] + "";
                string dLAND006 = readerItem["LAND006"] + "";
                string dLAND007 = readerItem["LAND007"] + "";
                string dLAND008 = readerItem["LAND008"] + "";
                string dLAND009 = readerItem["LAND009"] + "";
                string dLAND010 = readerItem["LAND010"] + "";
                string dLAND011 = readerItem["LAND011"] + "";
                string dLAND012 = readerItem["LAND012"] + "";

                DataTable tbCadastral = (DataTable)ViewState["SwcCadastral"];

                if (tbCadastral == null)
                {
                    DataTable GVTBCD = new DataTable();

                    GVTBCD.Columns.Add(new DataColumn("序號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("小段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("山坡地範圍", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地使用分區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地可利用限度", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("陽明山國家公園範圍", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("林地類別", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地質敏感區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("水保計畫申請紀錄", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("水土保持法違規紀錄", typeof(string)));

                    ViewState["SwcCadastral"] = GVTBCD;
                    tbCadastral = (DataTable)ViewState["SwcCadastral"];
                }

                DataRow GVTBCDRow = tbCadastral.NewRow();

                GVTBCDRow["序號"] = ++nj;
                GVTBCDRow["區"] = dLAND001;
                GVTBCDRow["段"] = dLAND002;
                GVTBCDRow["小段"] = dLAND003;
                GVTBCDRow["地號"] = dLAND004;
                GVTBCDRow["山坡地範圍"] = dLAND009;
                GVTBCDRow["土地使用分區"] = dLAND005;
                GVTBCDRow["土地可利用限度"] = dLAND006;
                GVTBCDRow["陽明山國家公園範圍"] = dLAND010;
                GVTBCDRow["林地類別"] = dLAND007;
                GVTBCDRow["地質敏感區"] = dLAND008;
                GVTBCDRow["水保計畫申請紀錄"] = dLAND011;
                GVTBCDRow["水土保持法違規紀錄"] = dLAND012;

                tbCadastral.Rows.Add(GVTBCDRow);

                //Store the DataTable in ViewState
                ViewState["SwcCadastral"] = tbCadastral;

                GVCadastral.DataSource = tbCadastral;
                GVCadastral.DataBind();

                CDNO.Text = nj.ToString();

            }
            readerItem.Close();
        }
        #endregion

        //計畫申請書...
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV = "select * from SWCFILES";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rSWCNO + "' ";
            strSQLRV = strSQLRV + " order by convert(int,FILE001)  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                string dFILE000 = readerItem["FILE000"] + "";
                string dFILE001 = readerItem["FILE001"] + "";
                string dFILE002 = readerItem["FILE002"] + "";
                string dFILE003 = readerItem["FILE003"] + "";

                DataTable File001C = (DataTable)ViewState["File001C"];
                
                if (File001C == null)
                {
                    DataTable GVFILE001 = new DataTable();

                    GVFILE001.Columns.Add(new DataColumn("File001000", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001001", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001002", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001003", typeof(string)));
                    GVFILE001.Columns.Add(new DataColumn("File001004", typeof(string)));

                    ViewState["File001C"] = GVFILE001;
                    File001C = (DataTable)ViewState["File001C"];
                }

                DataRow GVFILE001Row = File001C.NewRow();

                GVFILE001Row["File001000"] = ++nj;
                GVFILE001Row["File001001"] = "001";
                GVFILE001Row["File001002"] = "";
                GVFILE001Row["File001003"] = dFILE003;
                GVFILE001Row["File001004"] = SwcUpLoadFilePath + rSWCNO + "\\" + dFILE003;

                File001C.Rows.Add(GVFILE001Row);

                //Store the DataTable in ViewState
                ViewState["File001C"] = File001C;

                SWCFILES001.DataSource = File001C;
                SWCFILES001.DataBind();

                Files001No.Text = nj.ToString();

            }
                readerItem.Close();
        }
        SetPayData(rSWCNO);
        SetDtlData(rSWCNO); //分段驗收

        //DT001.NavigateUrl = "SWCDT001.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";
        DT002.NavigateUrl = "SWCDT002.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";
        //DT003.NavigateUrl = "SWCDT003.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";
        //DT004.NavigateUrl = "SWCDT004.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";
        //DT005.NavigateUrl = "SWCDT005.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";
        //DT006.NavigateUrl = "SWCDT006.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";
        //DT007.NavigateUrl = "SWCDT007.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";

        //DT007.Visible = false;

        //顯示？修改？
        switch (ssUserType)
        {
            case "01":
                Area01Close();      //基本資料
                //Area02Close();    //受理
                Area03Close();      //審查
                Area04Close();      //施工
                Area05Close();      //完工後水土保持設施檢查

                if (qSWC004== "受理中" || qSWC004 == "申請中" || qSWC004 == "退補件")
                {
                    Area01Open();
                }

                DDLSWC007.Enabled = false;
                break;
            case "04":
                Area01Close();      //基本資料

                break;
        }
		#region-義務人資訊
        using (SqlConnection ObliConn = new SqlConnection(connectionString.ConnectionString))
        {
            ObliConn.Open();

            string SqlStr = "";

            SqlStr = SqlStr + " select * from SWCObligor ";
            SqlStr = SqlStr + "  Where SWC000 = '" + rSWCNO + "' ";
            SqlStr = SqlStr + "  order by 序號 ";

            SqlDataReader readerObli;
            SqlCommand objCmdObli = new SqlCommand(SqlStr, ObliConn);
            readerObli = objCmdObli.ExecuteReader();

            while (readerObli.Read())
            {
                string dNO = readerObli["序號"] + "";
				//順便更新預設序號
				AddNO.Text = readerObli["序號"] + "";
                string dSWC013 = readerObli["SWC013"] + "";
                string dSWC013ID = readerObli["SWC013ID"] + "";
                string dSWC013TEL = readerObli["SWC013TEL"] + "";
                string dSWC014Zip = readerObli["SWC014Zip"] + "";
                string dSWC014City = readerObli["SWC014City"] + "";
                string dSWC014District = readerObli["SWC014District"] + "";
                string dSWC014Address = readerObli["SWC014Address"] + "";

                DataTable OBJ_GVSWCP = (DataTable)ViewState["SwcPeople"];
                DataTable dtSWCP = new DataTable();

                if (OBJ_GVSWCP == null)
                {
                    dtSWCP.Columns.Add(new DataColumn("序號", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("姓名", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("身分證字號", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("手機", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址ZipCode", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址City", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址District", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址Address", typeof(string)));
                    dtSWCP.Columns.Add(new DataColumn("地址", typeof(string)));

                    ViewState["SwcPeople"] = dtSWCP;
                    OBJ_GVSWCP = dtSWCP;
                }
                DataRow drSWCP = OBJ_GVSWCP.NewRow();

                drSWCP["序號"] = dNO;
                drSWCP["姓名"] = dSWC013;
                drSWCP["身分證字號"] = dSWC013ID;
                drSWCP["手機"] = dSWC013TEL;
                drSWCP["地址ZipCode"] = dSWC014Zip;
                drSWCP["地址City"] = dSWC014City;
                drSWCP["地址District"] = dSWC014District;
                drSWCP["地址Address"] = dSWC014Address;
                drSWCP["地址"] = dSWC014Zip + dSWC014City + dSWC014District + dSWC014Address;

                OBJ_GVSWCP.Rows.Add(drSWCP);

                ViewState["SwcPeople"] = OBJ_GVSWCP;

                GVPEOPLE.DataSource = OBJ_GVSWCP;
                GVPEOPLE.DataBind();

            }
        }
        #endregion
        SBApp.ViewRecord("水保申請案件", "view", rSWCNO);

    }

    private void SetDtlData(string rSWCNO)
    {
        GBClass001 SBApp = new GBClass001();

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        #region.分段驗收核定項目
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int ni = 0;

            string strSQLRV = "select * from SwcDocItem";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rSWCNO + "' ";
            strSQLRV = strSQLRV + " order by SDI001 ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                #region
                string sSDI001 = readerItem["SDI001"] + "";
                string sSDI002 = readerItem["SDI002"] + "";
                string sSDI003 = readerItem["SDI003"] + "";
                string sSDI004 = readerItem["SDI004"] + "";
                string sSDI005 = readerItem["SDI005"] + "";
                string sSDI006 = readerItem["SDI006"] + "";
                string sSDI006_1 = readerItem["SDI006_1"] + "";
                string sSDI006D = readerItem["SDI006D"] + "";
                string sSDI007 = readerItem["SDI007"] + "";
                string sSDI008 = readerItem["SDI008"] + "";
                string sSDI009 = readerItem["SDI009"] + "";
                string sSDI010 = readerItem["SDI010"] + "";
                string sSDI011 = readerItem["SDI011"] + "";
                string sSDI012 = readerItem["SDI012"] + "";
                string sSDI012_1 = readerItem["SDI012_1"] + "";
                string sSDI012D = readerItem["SDI012D"] + "";
                string sSDI013 = readerItem["SDI013"] + "";
                string sSDI013_1 = readerItem["SDI013_1"] + "";
                string sSDI014 = readerItem["SDI014"] + "";
                string sSDI014_1 = readerItem["SDI014_1"] + "";
                string sSDI015 = readerItem["SDI015"] + "";
                string sSDI016 = readerItem["SDI016"] + "";
				string sSDI019 = readerItem["SDI019"] + "";

                DataTable tbSDIVS = (DataTable)ViewState["SwcDocItem"];

                if (tbSDIVS == null)
                {
                    DataTable SDITB = new DataTable();

                    SDITB.Columns.Add(new DataColumn("SDIFDNI", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD003", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD009", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD010", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD011", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD019", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }

                DataRow SDITBRow = tbSDIVS.NewRow();

                SDITBRow["SDIFDNI"] = ni.ToString();
                SDITBRow["SDIFD001"] = sSDI001;
                SDITBRow["SDIFD002"] = sSDI002;
                SDITBRow["SDIFD003"] = sSDI003;
                SDITBRow["SDIFD004"] = sSDI004;
                SDITBRow["SDIFD005"] = sSDI005;
                SDITBRow["SDIFD006"] = sSDI006;
                SDITBRow["SDIFD006_1"] = sSDI006_1;
                SDITBRow["SDIFD006D"] = sSDI006D;
                SDITBRow["SDIFD007"] = sSDI007;
                SDITBRow["SDIFD008"] = sSDI008;
                SDITBRow["SDIFD009"] = sSDI009;
                SDITBRow["SDIFD010"] = sSDI010;
                SDITBRow["SDIFD011"] = sSDI011;
                SDITBRow["SDIFD012"] = sSDI012;
                SDITBRow["SDIFD012_1"] = sSDI012_1;
                SDITBRow["SDIFD012D"] = sSDI012D;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD013_1"] = sSDI013_1;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD014_1"] = sSDI014_1;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDIFD019"] = sSDI019;

                tbSDIVS.Rows.Add(SDITBRow);
                #endregion
                ViewState["SwcDocItem"] = tbSDIVS;

                SDIList.DataSource = tbSDIVS;
                SDIList.DataBind();

                TXTSDINI.Text = ni.ToString();
            }
            readerItem.Close();
        }
        #endregion

        #region.檔案交換區
        string getSFileSqlStr = " select * from [ShareFiles] Where SWC000='"+ rSWCNO + "';";
        HyperLink fileLink = new HyperLink();
        TextBox fileName = new TextBox();
        Label uploadUser = new Label();
        Label uploadDate = new Label();

        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();
            
            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(getSFileSqlStr, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                #region.設定資料
                string sSFtype = readerItem["SFType"] + "";
                string sFileName = readerItem["SFName"] + "";
                string sSaveUser = readerItem["SaveUser"] + "";
                string sSaveDate = readerItem["SaveDate"] + "";
                string sFileLink = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + rSWCNO + "/" + sFileName + "?ts=" + System.DateTime.Now.Millisecond;

                switch (sSFtype)
                {
                    case "001":
                        fileName = TXTSFile01; fileLink = SFLINK01; uploadUser = LBUPLOADU01; uploadDate = LBUPLOADD01;
                        break;
                    case "002":
                        fileName = TXTSFile02; fileLink = SFLINK02; uploadUser = LBUPLOADU02; uploadDate = LBUPLOADD02;
                        break;
                    case "003":
                        fileName = TXTSFile03; fileLink = SFLINK03; uploadUser = LBUPLOADU03; uploadDate = LBUPLOADD03;
                        break;
                    case "004":
                        fileName = TXTSFile04; fileLink = SFLINK04; uploadUser = LBUPLOADU04; uploadDate = LBUPLOADD04;
                        break;
                    case "005":
                        fileName = TXTSFile05; fileLink = SFLINK05; uploadUser = LBUPLOADU05; uploadDate = LBUPLOADD05;
                        break;
                    case "006":
                        fileName = TXTSFile06; fileLink = SFLINK06; uploadUser = LBUPLOADU06; uploadDate = LBUPLOADD06;
                        break;
                    case "007":
                        fileName = TXTSFile07; fileLink = SFLINK07; uploadUser = LBUPLOADU07; uploadDate = LBUPLOADD07;
                        break;
                }
                #endregion

                fileLink.Text = sFileName;
                fileLink.NavigateUrl = sFileLink;
                fileLink.Visible = true;
                fileName.Text = sFileName;
                uploadUser.Text = sSaveUser;
                uploadDate.Text = SBApp.DateView(sSaveDate,"00");
            }
            readerItem.Close();
        }
        #endregion
    }

    protected void GVPay02_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                Button btnPrint = (Button)e.Row.Cells[3].FindControl("btnPrint");
                Label Lock01 = (Label)e.Row.Cells[3].FindControl("LBCSMSG");
                string tempLock = Lock01.Text;

                if (tempLock == "已繳納")
                {
                    Lock01.Visible = true;
                    btnPrint.Visible = false;
                }
                break;
        }
    }

    protected void GVPay01_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？
                break;
            case DataControlRowType.DataRow:
                Button btnPrint = (Button)e.Row.Cells[3].FindControl("btnPrint");
                Label Lock01 = (Label)e.Row.Cells[3].FindControl("LBCSMSG");
                string tempLock = Lock01.Text;

                if (tempLock == "已繳納")
                {
                    Lock01.Visible = true;
                    btnPrint.Visible = false;
                }
                break;
        }

    }
    private void Area01Open()
    {
        TXTSWC005.Enabled = true;
        DDLSWC007.Enabled = true;
        TXTSWC013.Enabled = true;
        TXTSWC013ID.Enabled = true;
        TXTSWC013TEL.Enabled = true;
        TXTSWC014.Enabled = true;
        TXTSWC015.Enabled = true;
        TXTSWC016.Enabled = true;
        DDLSWC017.Enabled = true;
        TXTSWC018.Enabled = true;
        TXTSWC023.Enabled = true;
        TBSWC120.Enabled = true;
        ADDLIST01.Enabled = true;
        DDLDistrict.Enabled = true;
        DDLSection.Enabled = true;
        DDLSection2.Enabled = true;
        TXTNumber.Enabled = true;
        DDLCA01.Enabled = true;
        /*
        DDLCA02.Enabled = true;
        DDLCA03.Enabled = true;
        DDLCA04.Enabled = true;
        */
        TXTSWC027.Enabled = true;
        TXTSWC028.Enabled = true;
        TXTFILES001_fileupload.Enabled = true;
        TXTFILES001_fileuploadok.Enabled = true;
        TXTSWC138_fileupload.Enabled = true;
        TXTSWC138_fileuploadok.Enabled = true;
        BTNFILES001.Enabled = true;
        TXTSWC138_fileclean.Enabled = true;
        AddFile001.Enabled = true;
        SDIList.Columns[6].Visible = true;
        //GVCadastral.Columns[9].Visible = true;
		//義務人刪除顯示
		GVPEOPLE.Columns[9].Visible = true;
        SWCFILES001.Columns[2].Visible = true;
        SWCFILES001.CssClass = "detailsGrid_skyblue_innerTable detailsGrid_skyblue_innerTable_lastCenter";
        TXTSWC108.Enabled = true;
		
		TXTSWCPNAME.Enabled = true;
		TXTSWCPID.Enabled = true;
		TXTSWCPPHONE.Enabled = true;
		DDLSWCPCITY.Enabled = true;
		DDLSWCPAREA.Enabled = true;
		TXTSWCPADDRESS.Enabled = true;
		AddAddress.Enabled = true;
		
		DDLSWC134.Enabled = true;
    }
    private void Area01Open_2()
    {
        #region 水保設施核定項目
        SDIAREA.Visible = true;
        SDIList.Columns[6].Visible = true;
        #endregion
		
		//有人打來反應審查中的案件沒辦法編輯此欄位,但都被擋住無法送出,故先讓此欄位文件可上傳
        #region 環評報告書免環評證明文件
		TXTSWC138_fileupload.Enabled = true;
        TXTSWC138_fileuploadok.Enabled = true;
        TXTSWC138_fileclean.Enabled = true;
        #endregion
    }

    protected void btnPrint01_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aa = Convert.ToInt32(LButton.CommandArgument);
        string PBCode = GVPay01.Rows[aa].Cells[0].Text.ToString().Replace("&nbsp;", "");
        Response.Write("<script>window.open('../SwcReport/PPayBillSM01.aspx?pno=" + PBCode + "');</script>");
    }
    private void Area01Close()
    {
        #region 基本資料
        TXTSWC005.Enabled = false;
        DDLSWC007.Enabled = false;
        TXTSWC013.Enabled = false;
        TXTSWC013ID.Enabled = false;
        TXTSWC013TEL.Enabled = false;
        TXTSWC014.Enabled = false;
        TXTSWC015.Enabled = false;
        TXTSWC016.Enabled = false;
        DDLSWC017.Enabled = false;
        TXTSWC018.Enabled = false;
        TXTSWC023.Enabled = false;
        TBSWC120.Enabled = false;
        ADDLIST01.Enabled = false;
        DDLDistrict.Enabled = false;
        DDLSection.Enabled = false;
        DDLSection2.Enabled = false;
        TXTNumber.Enabled = false;
        DDLCA01.Enabled = false;
        /*
        DDLCA02.Enabled = false;
        DDLCA03.Enabled = false;
        DDLCA04.Enabled = false;
        */
        TXTSWC027.Enabled = false;
        TXTSWC028.Enabled = false;
        TXTFILES001_fileupload.Enabled = false;
        TXTFILES001_fileuploadok.Enabled = false;
        TXTSWC138_fileupload.Enabled = false;
        TXTSWC138_fileuploadok.Enabled = false;
        BTNFILES001.Enabled = false;
        TXTSWC138_fileclean.Enabled = false;
        AddFile001.Enabled = false;
        //GVCadastral.Columns[13].Visible = false;
        //義務人刪除隱藏
		GVPEOPLE.Columns[9].Visible = false;
        //義務人填的欄位及按鈕
		TXTSWCPNAME.Enabled = false;
		TXTSWCPID.Enabled = false;
		TXTSWCPPHONE.Enabled = false;
		DDLSWCPCITY.Enabled = false;
		DDLSWCPAREA.Enabled = false;
		TXTSWCPADDRESS.Enabled = false;
		AddAddress.Enabled = false;
		DDLSWC134.Enabled = false;
		
		SWCFILES001.Columns[2].Visible = false;
        SDIList.Columns[6].Visible = false;
        SWCFILES001.CssClass = "detailsGrid_skyblue_innerTable";
        TXTSWC108.Enabled = false;
        #endregion

    }
    private void Area01Close_2()
    {
        #region 水保設施核定項目
        SDIAREA.Visible = false;
        SDIList.Columns[6].Visible = false;
        #endregion
    }

        private void Area03Open()
    {
        TXTSWC087.Enabled = true;
        TXTSWC080_fileupload.Enabled = true;
        TXTSWC080_fileuploadok.Enabled = true;
        TXTSWC080_fileclean.Enabled = true;
        TXTSWC029CAD_fileupload.Enabled = true;
        TXTSWC029CAD_fileuploadok.Enabled = true;
        TXTSWC029CAD_fileclean.Enabled = true;
        TXTSWC029_fileupload.Enabled = true;
        TXTSWC029_fileuploadok.Enabled = true;
        TXTSWC029_fileclean.Enabled = true;
        TXTSWC030_fileupload.Enabled = true;
        TXTSWC030_fileuploadok.Enabled = true;
        TXTSWC030_fileclean.Enabled = true;
        TXTSWC109.Enabled = true;
        TXTSWC110_fileclean.Enabled = true;
        TXTSWC110_fileuploadok.Enabled = true;
        TXTSWC110_fileupload.Enabled = true;

        #region-

        DropDownList[] arryDDL01 = new DropDownList[] { DDLSA01, DDLSA02, DDLSA03, DDLSA04, DDLSA05, DDLSA06, DDLSA07, DDLSA08, DDLSA09, DDLSA10 };
        for (int i = 0; i < arryDDL01.Length; i++) arryDDL01[i].Enabled = true;
        TextBox[] arryTXTB01 = new TextBox[] { TXTSWC113 };
        for (int i = 0; i < arryTXTB01.Length; i++) arryTXTB01[i].Enabled = true;
        CheckBox[] arryCHKB01 = new CheckBox[] { CHKSendMail01 };
        for (int i = 0; i < arryCHKB01.Length; i++) arryCHKB01[i].Enabled = true;

        #endregion

        SWCDTL01.Columns[5].Visible = true;
    }
    private void Area03Close()
    {
        SWCDTL01.Columns[5].Visible = false;

        TXTSWC087.Enabled = false;
        TXTSWC080_fileupload.Enabled = false;
        TXTSWC080_fileuploadok.Enabled = false;
        TXTSWC080_fileclean.Enabled=false;
        TXTSWC029CAD_fileupload.Enabled = false;
        TXTSWC029CAD_fileuploadok.Enabled = false;
        TXTSWC029CAD_fileclean.Enabled = false;
        TXTSWC029_fileupload.Enabled = false;
        TXTSWC029_fileuploadok.Enabled = false;
        TXTSWC029_fileclean.Enabled = false;
        TXTSWC030_fileupload.Enabled = false;
        TXTSWC030_fileuploadok.Enabled = false;
        TXTSWC030_fileclean.Enabled = false;
        TXTSWC109.Enabled = false;
        TXTSWC110_fileupload.Enabled = false;
        TXTSWC110_fileclean.Enabled = false;
        TXTSWC110_fileuploadok.Enabled = false;

        #region-
        DropDownList[] arryDDL01 = new DropDownList[] { DDLSA01, DDLSA02, DDLSA03, DDLSA04, DDLSA05, DDLSA06, DDLSA07, DDLSA08, DDLSA09, DDLSA10 };
        for (int i = 0; i < arryDDL01.Length; i++) arryDDL01[i].Enabled = false;
        TextBox[] arryTXTB01 = new TextBox[] { TXTSWC113 };
        for (int i = 0; i < arryTXTB01.Length; i++) arryTXTB01[i].Enabled = false;
        CheckBox[] arryCHKB01 = new CheckBox[] { CHKSendMail01 };
        for (int i = 0; i < arryCHKB01.Length; i++) arryCHKB01[i].Enabled = false;
        #endregion

    }
    private void Area04Open()
    {
        TXTSWC051.Enabled = true;
        TXTSWC048.Enabled = true;
        TXTSWC049.Enabled = true;
        TXTSWC050.Enabled = true;
        TXTSWC058.Enabled = true;
        TXTSWC101_fileupload.Enabled = true;
        BTNSWC101_fileuploadok.Enabled = true;
        BTNSWC101_fileuploaddel.Enabled = true;
        TXTSWC101CAD_fileupload.Enabled = true;
        BTNSWC101CAD_fileuploadok.Enabled = true;
        BTNSWC101CAD_fileuploaddel.Enabled = true;
        CHKSWC067.Enabled = true;
        TXTSWC068.Enabled = true;
        TXTSWC069.Enabled = true;
        TXTSWC070.Enabled = true;
        CHKSWC071.Enabled = true;
        TXTSWC072.Enabled = true;
        CHKSWC073.Enabled = true;
        TXTSWC074.Enabled = true;
        CHKSWC075.Enabled = true;

        #region-
        DropDownList[] arryDDL01 = new DropDownList[] { DDLSB01, DDLSB02 };
        for (int i = 0; i < arryDDL01.Length; i++) arryDDL01[i].Enabled = true;
        TextBox[] arryTXTB01 = new TextBox[] { TXTSWC114,TXTSWC115 };
        for (int i = 0; i < arryTXTB01.Length; i++) arryTXTB01[i].Enabled = true;
        CheckBox[] arryCHKB01 = new CheckBox[] { CHKSendMail02, CHKSendMail03 };
        for (int i = 0; i < arryCHKB01.Length; i++) arryCHKB01[i].Enabled = true;
        #endregion

    }
    private void Area04Close()
    {
        TXTSWC051.Enabled = false;
        TXTSWC048.Enabled = false;
        TXTSWC049.Enabled = false;
        TXTSWC050.Enabled = false;
        TXTSWC058.Enabled = false;
        TXTSWC101_fileupload.Enabled = false;
        BTNSWC101_fileuploadok.Enabled = false;
        BTNSWC101_fileuploaddel.Enabled = false;
        TXTSWC101CAD_fileupload.Enabled = false;
        BTNSWC101CAD_fileuploadok.Enabled = false;
        BTNSWC101CAD_fileuploaddel.Enabled = false;
        CHKSWC067.Enabled = false;
        TXTSWC068.Enabled = false;
        TXTSWC069.Enabled = false;
        TXTSWC070.Enabled = false;
        CHKSWC071.Enabled = false;
        TXTSWC072.Enabled = false;
        CHKSWC073.Enabled = false;
        TXTSWC074.Enabled = false;
        CHKSWC075.Enabled = false;

        #region-
        DropDownList[] arryDDL01 = new DropDownList[] { DDLSB01, DDLSB02 };
        for (int i = 0; i < arryDDL01.Length; i++) arryDDL01[i].Enabled = false;
        TextBox[] arryTXTB01 = new TextBox[] { TXTSWC114, TXTSWC115 };
        for (int i = 0; i < arryTXTB01.Length; i++) arryTXTB01[i].Enabled = false;
        CheckBox[] arryCHKB01 = new CheckBox[] { CHKSendMail02, CHKSendMail03 };
        for (int i = 0; i < arryCHKB01.Length; i++) arryCHKB01[i].Enabled = false;
        #endregion
    }
    private void Area05Open()
    {
        TXTSWC093.Enabled = true;
        TXTSWC095.Enabled = true;
        TXTSWC094.Enabled = true;
        CHKSWC061.Enabled = true;
        TXTSWC062.Enabled = true;
        CHKSWC063.Enabled = true;
        TXTSWC064.Enabled = true;
        CHKSWC065.Enabled = true;
        TXTSWC066.Enabled = true;
        DT007.Visible = true;
    }
    private void Area05Close()
    {
        TXTSWC093.Enabled = false;
        TXTSWC095.Enabled = false;
        TXTSWC094.Enabled = false;
        CHKSWC061.Enabled = false;
        TXTSWC062.Enabled = false;
        CHKSWC063.Enabled = false;
        TXTSWC064.Enabled = false;
        CHKSWC065.Enabled = false;
        TXTSWC066.Enabled = false;
        DT007.Visible = false;
    }
    

    private string GetNewId()
    {
        string SearchValA = "TT99"+ (Convert.ToInt32(DateTime.Now.ToString("yyyy"))-1911).ToString() + DateTime.Now.ToString("MM");
        string ValB = "001";
        string tSWC02 = "";
        string tSWC002a = "", tSWC002b = "", tSWC002c = "";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRVa = " select MAX(SWCKEY) as MAXID from SWCGENKEY ";
            strSQLRVa = strSQLRVa + " where TABLEID ='SWCCASE' ";
            strSQLRVa = strSQLRVa + "   AND LEFT(SWCKEY,9) ='" + SearchValA + "' ";

            SqlDataReader readerSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRVa, SwcConn);
            readerSwc = objCmdSwc.ExecuteReader();

            if (readerSwc.HasRows)
            {
                while (readerSwc.Read())
                {
                    tSWC002a = readerSwc["MAXID"] + "";
                }
            }
            objCmdSwc.Dispose();
            readerSwc.Close();
            SwcConn.Close();

            SwcConn.Open();

            string strSQLRVb = " select MAX(SWC002) as MAXID from SWCCASE ";
            strSQLRVb = strSQLRVb + " where LEFT(SWC002,9) ='" + SearchValA + "' ";
            
            objCmdSwc = new SqlCommand(strSQLRVb, SwcConn);
            readerSwc = objCmdSwc.ExecuteReader();

            if (readerSwc.HasRows)
            {
                while (readerSwc.Read())
                {
                    tSWC002b = readerSwc["MAXID"] + "";
                }
            }
            SwcConn.Close();
        }

        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();

            string strSQLRVc = " select MAX(SWC02) as MAXID from SWCSWC ";
            strSQLRVc = strSQLRVc + " where LEFT(SWC02,9) ='" + SearchValA + "' ";

            SqlDataReader readerTslm;
            SqlCommand objCmdTslm = new SqlCommand(strSQLRVc, TslmConn);
            readerTslm = objCmdTslm.ExecuteReader();

            if (readerTslm.HasRows)
            {
                while (readerTslm.Read())
                {
                    tSWC002c = readerTslm["MAXID"] + "";
                }
            }
        }
        if (tSWC002a.Trim() == "") {
            tSWC002a = SearchValA + "000";
        }
        string[] array_Swc02 = new string[] { tSWC002a, tSWC002b, tSWC002c };
        string tSWC002 = tSWC002a;

        for (int i = 0; i < array_Swc02.Length; i++)
        {
            string aSwc002 = array_Swc02[i].Trim();

            if (aSwc002 != "")
                if (Convert.ToInt32(aSwc002.Substring(7, 5)) > Convert.ToInt32(tSWC002.Substring(7, 5)))
                    tSWC002 = aSwc002;
        }
        if (tSWC002 != "")
        {
            string tempValue = (Convert.ToInt32(tSWC002.Substring(tSWC002.Length - 3, 3)) + 1).ToString();
            ValB = tempValue.PadLeft(3, '0');
        }

        string strSQLIns = " INSERT INTO SWCGENKEY (TABLEID,SWCKEY) values ('SWCCASE','"+ SearchValA + ValB + "'); ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlCommand objCmdUser = new SqlCommand(strSQLIns, SWCConn);
            objCmdUser.ExecuteNonQuery();
            objCmdUser.Dispose();
        }
        return SearchValA + ValB;
    }

    protected void SwcList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SWC001.aspx?SR=Y");
    }
    protected void GenerateDropDownList()
    {
        AddressApiClass AAC = new AddressApiClass();
        string ssUserGuild = Session["WMGuild"] + "";

        //string[] array_CaseStatus = new string[] { "", "退補件", "不予受理", "受理中", "審查中", "暫停審查", "撤銷", "不予核定", "已核定", "施工中", "停工中", "已完工", "廢止", "失效", "已變更" };

        string[] array_SWC007 = new string[] { "", "水土保持計畫", "簡易水保" };
        DDLSWC007.DataSource = array_SWC007;
        DDLSWC007.DataBind();
        
        string[,] array_District = new string[,] { { "0", "" }, { "16", "北投" }, { "15", "士林" }, { "14", "內湖" }, { "10", "中山" }, { "03", "中正" }, { "17", "信義" }, { "02", "大安" }, { "13", "南港" }, { "11", "文山" } };
        List<System.Web.UI.WebControls.ListItem> ListZip = new List<System.Web.UI.WebControls.ListItem>();
        for (int te = 0; te <= array_District.GetUpperBound(0); te++)
        {
            System.Web.UI.WebControls.ListItem li = new System.Web.UI.WebControls.ListItem(array_District[te, 1], array_District[te, 0]);
            ListZip.Add(li);
        }
        DDLDistrict.Items.AddRange(ListZip.ToArray());

        string[] array_DDLCA01 = new string[] { "", "保護區", "住宅區", "陽明山國家公園", "保變住", "風景區", "商業區", "工業區", "行政區", "文教區", "倉庫區", "農業區", "行水區", "保存區", "特定專用區", "公共設施用地" };
        DDLCA01.DataSource = array_DDLCA01;
        DDLCA01.DataBind();

        /*
        string[] array_DDLCA02 = new string[] { "", "尚未公告", "宜農牧地", "宜林地", "加強保育地", "不屬查定" };
        DDLCA02.DataSource = array_DDLCA02;
        DDLCA02.DataBind();

        string[] array_DDLCA03 = new string[] { "", "本市林地", "保安林" };
        DDLCA03.DataSource = array_DDLCA03;
        DDLCA03.DataBind();

        string[] array_DDLCA04 = new string[] { "", "山崩與地滑", "地質遺跡", "地下水補注", "活動斷層" };
        DDLCA04.DataSource = array_DDLCA04;
        DDLCA04.DataBind();
        */
        
        string[] array_SWC017 = new string[] { "", "都市發展局", "陽明山國家公園管理處", "新建工程處", "大地工程處", "殯葬管理處", "經濟部", "產業發展局", "公園路燈工程管理處", "臺北自來水事業處", "環境保護局" };
        DDLSWC017.DataSource = array_SWC017;
        DDLSWC017.DataBind();
        
        #region-分段驗收下拉
        string strSQLCase = " select * from [SwcItemChkRule] where [level] = 1 order by SICRSORT ";

        DDLSIC01.Items.Clear();
        DDLSIC01.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLCase, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string tSICID = readerData["SICR001"] + "";
                string tSICDESC = readerData["SICR002"] + "";

                DDLSIC01.Items.Add(new System.Web.UI.WebControls.ListItem(tSICDESC, tSICID));
            }
            readerData.Close();
            objCmdRV.Dispose();

        }
        #endregion
		
		#region-義務人資訊
        string[] arryCity = new string[] { };
        arryCity = AAC.GetCity();
        DDLSWCPCITY.Items.Clear();
        for (int i = 0; i < arryCity.Length; i++)
        {
            DDLSWCPCITY.Items.Add(new System.Web.UI.WebControls.ListItem(arryCity[i], arryCity[i]));
        }
		
        //List<AddressApiClass.Area_detail> arryArea = new List<AddressApiClass.Area_detail> { };
        string[] arryArea = new string[] { };
        arryArea = AAC.GetArea(DDLSWCPCITY.SelectedItem.Text);
        DDLSWCPAREA.Items.Clear();
        foreach (var item in arryArea)
        {
            //DDLSWCPAREA.Items.Add(new System.Web.UI.WebControls.ListItem(item.district, item.district));
			DDLSWCPAREA.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
        }
		
        TXTSWCPCODE.Text = AAC.GetZip(DDLSWCPCITY.SelectedItem.Text, DDLSWCPAREA.SelectedItem.Text);
        #endregion
		
		#region-承辦建築師
        string strSQLArch = " select * from Architect order by 姓名 ";

        DDLSWC134.Items.Clear();
        DDLSWC134.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));
        connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLArch, TslmConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string name = readerData["姓名"] + "";
                string id = readerData["帳號"] + "";

                DDLSWC134.Items.Add(new System.Web.UI.WebControls.ListItem(name, id));
            }
            readerData.Close();
            objCmdRV.Dispose();

        }
        #endregion
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWC001.aspx");
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
		if(Link138.Text == "")
		{
			Response.Write("<script>alert('請上傳環評報告書/免環評證明文件');</script>");
			return;
		}
		DataTable dtSWCP = new DataTable();
        dtSWCP = (DataTable)ViewState["SwcPeople"];
		
		TXTSWC013.Text = "";
		TXTSWC013ID.Text = "";
		TXTSWC013TEL.Text = "";
		TXTSWC014.Text = "";
		if(dtSWCP.Rows.Count > 0)
		{
			for (int i = 0; i <= (Convert.ToInt32(dtSWCP.Rows.Count) - 1); i++)
			{
				string rNO = dtSWCP.Rows[i]["序號"].ToString().Trim();
				string rSWC013 = dtSWCP.Rows[i]["姓名"].ToString().Trim();
				string rSWC013ID = dtSWCP.Rows[i]["身分證字號"].ToString().Trim();
				string rSWC013TEL = dtSWCP.Rows[i]["手機"].ToString().Trim();
				string rSWC014Zip = dtSWCP.Rows[i]["地址ZipCode"].ToString().Trim();
				string rSWC014City = dtSWCP.Rows[i]["地址City"].ToString().Trim();
				string rSWC014District = dtSWCP.Rows[i]["地址District"].ToString().Trim();
				string rSWC014Address = dtSWCP.Rows[i]["地址Address"].ToString().Trim();
				
				TXTSWC013.Text += rSWC013 + ";";
				TXTSWC013ID.Text += rSWC013ID + ";";
				TXTSWC013TEL.Text += rSWC013TEL + ";";
				TXTSWC014.Text += rSWC014Zip + rSWC014City + rSWC014District + rSWC014Address + ";";
			}
			TXTSWC013.Text = TXTSWC013.Text.ToString().Substring(0,TXTSWC013.Text.ToString().Length-1);
			TXTSWC013ID.Text = TXTSWC013ID.Text.ToString().Substring(0,TXTSWC013ID.Text.ToString().Length-1);
			TXTSWC013TEL.Text = TXTSWC013TEL.Text.ToString().Substring(0,TXTSWC013TEL.Text.ToString().Length-1);
			TXTSWC014.Text = TXTSWC014.Text.ToString().Substring(0,TXTSWC014.Text.ToString().Length-1);
		}
		
        SaveCase.Enabled = true;
        string ssUserID = Session["ID"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC120 = TBSWC120.Text;

        string ssUserPW = Session["PW"] + ""; 
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string pageAction = Request.QueryString["CaseId"] + "";
        string qTslmLand = "",tmpAddCase="N";

        #region-取得畫面資料
        int tmpV01 = SWCFILES001.PageCount;
        //if(tmpV01<1) { Response.Write("<script>alert('提醒您，尚未上傳計畫申請書！');</script>"); TXTFILES001_fileupload.Focus(); return; }

        string gSWC000 = LBSWC000.Text + "";
        string gSWC002 = LBSWC002.Text + "";
        string gSWC005 = TXTSWC005.Text + "";
        string gSWC007 = DDLSWC007.SelectedValue + "";
        string gSWC013 = TXTSWC013.Text + "";
        string gSWC013ID = TXTSWC013ID.Text + "";
        string gSWC013TEL = TXTSWC013TEL.Text + "";
        string gSWC014 = TXTSWC014.Text + "";
        string gSWC015 = TXTSWC015.Text + "";
        string gSWC016 = TXTSWC016.Text + "";
        string gSWC017 = DDLSWC017.SelectedValue + "";
        string gSWC018 = TXTSWC018.Text + "";
        string gSWC023 = TXTSWC023.Text + "";
        string gSWC027 = TXTSWC027.Text + "";
        string gSWC028 = TXTSWC028.Text + "";
        string gSWC029 = TXTSWC029.Text + "";
        string gSWC029CAD = TXTSWC029CAD.Text + "";
        string gSWC030 = TXTSWC030.Text + "";
        string gSWC048 = TXTSWC048.Text + "";
        string gSWC049 = TXTSWC049.Text + "";
        string gSWC050 = TXTSWC050.Text + "";
        string gSWC058 = TXTSWC058.Text + "";
        string gSWC061 = CHKSWC061.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC062 = TXTSWC062.Text + "";
        string gSWC063 = CHKSWC063.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC064 = TXTSWC064.Text + "";
        string gSWC065 = CHKSWC065.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC066 = TXTSWC066.Text + "";
        string gSWC067 = CHKSWC067.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC068 = TXTSWC068.Text + "";
        string gSWC069 = TXTSWC069.Text + "";
        string gSWC070 = TXTSWC070.Text + "";
        string gSWC071 = CHKSWC071.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC072 = TXTSWC072.Text + "";
        string gSWC073 = CHKSWC073.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC074 = TXTSWC074.Text + "";
        string gSWC075 = CHKSWC075.Checked.ToString().Replace("False", "").Replace("True", "1");
        string gSWC080 = TXTSWC080.Text + "";
        string gSWC087 = TXTSWC087.Text + "";
        string gSWC093 = TXTSWC093.Text + "";
        string gSWC094 = TXTSWC094.Text + "";
        string gSWC095 = TXTSWC095.Text + "";
        string gSWC101 = TXTSWC101.Text + "";
        string gSWC108 = TXTSWC108.Text + "";
        string gSWC109 = TXTSWC109.Text + "";
        string gSWC110 = TXTSWC110.Text + "";
        string gSWC101CAD = TXTSWC101CAD.Text + "";
        string gSWC113 = TXTSWC113.Text + "";
        string gSWC114 = TXTSWC114.Text + "";
        string gSWC115 = TXTSWC115.Text + "";
		string gSWC134 = DDLSWC134.SelectedValue + "";
		string gSWC138 = TXTSWC138.Text + "";

        if (gSWC005.Length > 512) { gSWC005 = gSWC005.Substring(0, 512); }
        if (gSWC013.Length > 100) { gSWC013 = gSWC013.Substring(0, 100); }
        if (gSWC014.Length > 255) {
            gSWC014 = gSWC014.Substring(0, 255);
        }
        if (gSWC048.Length > 255) { gSWC048 = gSWC048.Substring(0, 255); }
        if (gSWC087.Length > 255) { gSWC087 = gSWC087.Substring(0, 255); }
        #endregion

        GBClass001 SBApp = new GBClass001();
		string gSWC139 = SBApp.XY97toll(gSWC027,gSWC028);

        string ExeSQLStr = "";
        string RsvSQLStr = "";

        string gSWC021ID = "";
        if (ssUserType == "01")
        {
            gSWC013ID = ssUserID;
            gSWC013TEL = ssUserPW;
        }

        if (ssUserType=="02")
        {
            gSWC021ID = ssUserID;
        }

        if (pageAction.Trim()== "ADDNEW" || pageAction.Trim() == "COPY") {
            //gSWC000 = "SWC" + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            tmpAddCase = "Y";
            
            if(!ChkHaveSWC(gSWC000)){
				ExeSQLStr = ExeSQLStr + " INSERT INTO SWCCASE (SWC000,SWC004,SWC021ID,SWC021,CaseDate01) VALUES ('" + gSWC000 + "','申請中','" + gSWC021ID + "',N'" + ssUserName + "',GETDATE());";
				RsvSQLStr = RsvSQLStr + " INSERT INTO tslm2.dbo.SWCSWC  ( SWC00, SWC04,SWC021ID, SWC21,SWC02,SWC05,CaseDate01) VALUES ('" + gSWC000 + "','申請中','" + gSWC021ID + "',N'" + ssUserName + "','" + gSWC002 + "','',GETDATE());";
			}
		}
        #region.update
        ExeSQLStr = ExeSQLStr + " Update SWCCASE Set ";
        ExeSQLStr = ExeSQLStr + " SWC002 =N'" + gSWC002 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC005 =N'" + gSWC005 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC007 =N'" + gSWC007 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC013 =N'" + gSWC013 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC013ID =N'" + gSWC013ID + "', ";
        ExeSQLStr = ExeSQLStr + " SWC013TEL =N'" + gSWC013TEL + "', ";
        ExeSQLStr = ExeSQLStr + " SWC014 =N'" + gSWC014 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC015 =N'" + gSWC015 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC016 =N'" + gSWC016 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC017 =N'" + gSWC017 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC018 =N'" + gSWC018 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC023 =N'" + gSWC023 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC027 =N'" + gSWC027 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC028 =N'" + gSWC028 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC029 =N'" + gSWC029 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC029CAD =N'" + gSWC029CAD + "', ";
        ExeSQLStr = ExeSQLStr + " SWC030 =N'" + gSWC030 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC048 =N'" + gSWC048 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC049 =N'" + gSWC049 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC050 =N'" + gSWC050 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC058 =N'" + gSWC058 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC061 =N'" + gSWC061 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC062 =N'" + gSWC062 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC063 =N'" + gSWC063 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC064 =N'" + gSWC064 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC065 =N'" + gSWC065 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC066 =N'" + gSWC066 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC067 =N'" + gSWC067 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC068 =N'" + gSWC068 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC069 =N'" + gSWC069 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC070 =N'" + gSWC070 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC071 =N'" + gSWC071 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC072 =N'" + gSWC072 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC073 =N'" + gSWC073 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC074 =N'" + gSWC074 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC075 =N'" + gSWC075 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC080 =N'" + gSWC080 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC087 =N'" + gSWC087 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC093 =N'" + gSWC093 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC094 =N'" + gSWC094 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC095 =N'" + gSWC095 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC101 =N'" + gSWC101 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC101CAD =N'" + gSWC101CAD + "', ";
        ExeSQLStr = ExeSQLStr + " SWC108 =N'" + gSWC108 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC109 =N'" + gSWC109 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC110 =N'" + gSWC110 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC113 =N'" + gSWC113 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC114 =N'" + gSWC114 + "', ";
        ExeSQLStr = ExeSQLStr + " SWC115 =N'" + gSWC115 + "', ";
		ExeSQLStr = ExeSQLStr + " SWC134 =N'" + gSWC134 + "', ";
		ExeSQLStr = ExeSQLStr + " SWC138 =N'" + gSWC138 + "', ";
		ExeSQLStr = ExeSQLStr + " SWC139 =N'" + gSWC139 + "', ";
        ExeSQLStr = ExeSQLStr + " saveuser = '" + ssUserID + "', ";
        ExeSQLStr = ExeSQLStr + " savedate = getdate() ";
        ExeSQLStr = ExeSQLStr + " Where SWC000 = '" + gSWC000 + "';";

        int FileYear = Convert.ToInt16(gSWC002.Substring(4, 3));
        string FileYearS = FileYear.ToString();
        FileYearS = FileYear > 93 ? FileYearS+"年掃描圖檔":"93年度暨以前掃描圖檔";
        string temp55Path29="",temp55Path30 = "",temp55Path101 = "";
        if (gSWC029 != "") { temp55Path29  = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + gSWC007 + "\\" + gSWC002 + "\\審查\\" + "6-1" + "\\" + gSWC029;}
        if (gSWC030 != "") { temp55Path30  = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + gSWC007 + "\\" + gSWC002 + "\\審查\\" + "7-1" + "\\" + gSWC030; }
        if (gSWC101 != "") { temp55Path101 = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + gSWC007 + "\\" + gSWC002 + "\\竣工圖說\\" + "竣工圖說" + "\\" + gSWC101; }

        RsvSQLStr = RsvSQLStr + " Update tslm2.dbo.SWCSWC Set ";
        RsvSQLStr = RsvSQLStr + " SWC02 =N'" + gSWC002 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC05 =N'" + gSWC005 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC07 =N'" + gSWC007 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC13 =N'" + gSWC013 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC013ID =N'" + gSWC013ID + "', ";
        RsvSQLStr = RsvSQLStr + " SWC013TEL =N'" + gSWC013TEL + "', ";
        RsvSQLStr = RsvSQLStr + " SWC14 =N'" + gSWC014 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC15 =N'" + gSWC015 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC16 =N'" + gSWC016 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC17 =N'" + gSWC017 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC18 =N'" + gSWC018 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC23 =N'" + gSWC023 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC27 =N'" + gSWC027 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC28 =N'" + gSWC028 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC29 =N'" + temp55Path29 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC029CAD =N'" + gSWC029CAD + "', ";
        RsvSQLStr = RsvSQLStr + " SWC30 =N'" + temp55Path30 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC48 =N'" + gSWC048 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC49 =N'" + gSWC049 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC50 =N'" + gSWC050 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC58 =N'" + gSWC058 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC61 =N'" + gSWC061 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC62 =N'" + gSWC062 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC63 =N'" + gSWC063 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC64 =N'" + gSWC064 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC65 =N'" + gSWC065 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC66 =N'" + gSWC066 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC67 =N'" + gSWC067 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC68 =N'" + gSWC068 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC69 =N'" + gSWC069 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC70 =N'" + gSWC070 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC71 =N'" + gSWC071 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC72 =N'" + gSWC072 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC73 =N'" + gSWC073 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC74 =N'" + gSWC074 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC75 =N'" + gSWC075 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC80 =N'" + gSWC080 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC87 =N'" + gSWC087 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC93 =N'" + gSWC093 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC94 =N'" + gSWC094 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC95 =N'" + gSWC095 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC101 =N'" + temp55Path101 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC101CAD =N'" + gSWC101CAD + "', ";
        RsvSQLStr = RsvSQLStr + " SWC108 =N'" + gSWC108 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC109 =N'" + gSWC109 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC110 =N'" + gSWC110 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC113 =N'" + gSWC113 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC114 =N'" + gSWC114 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC115 =N'" + gSWC115 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC134 =N'" + gSWC134 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC138 =N'" + gSWC138 + "', ";
        RsvSQLStr = RsvSQLStr + " SWC139 =N'" + gSWC139 + "' ";
        RsvSQLStr = RsvSQLStr + " Where SWC00 = '" + gSWC000 + "';";
        #endregion
        string RecField = " SWC000,SWC001,SWC002,SWC003,SWC004,SWC005,SWC006,SWC007,SWC008,SWC009,SWC010,SWC011,SWC012,SWC013,SWC014,SWC015,SWC016,SWC017,SWC018,SWC019,SWC020,SWC021,SWC022,SWC023,SWC024,SWC025,SWC026,SWC027,SWC028,SWC029,SWC030,SWC031,SWC032,SWC033,SWC034,SWC035,SWC036,SWC037,SWC038,SWC039,SWC040,SWC041,SWC042,SWC043,SWC044,SWC045,SWC046,SWC047,SWC048,SWC049,SWC050,SWC051,SWC052,SWC053,SWC054,SWC055,SWC056,SWC057,SWC058,SWC059,SWC060,SWC061,SWC062,SWC063,SWC064,SWC065,SWC066,SWC067,SWC068,SWC069,SWC070,SWC071,SWC072,SWC073,SWC074,SWC075,SWC076,SWC077,SWC078,SWC079,SWC080,SWC081,SWC082,SWC083,SWC084,SWC085,SWC086,SWC087,SWC088,SWC089,SWC090,SWC091,SWC092,SWC093,SWC094,SWC095,SWC096,SWC097,SWC098,SWC099,SWC100,SWC021ID,SaveuSer,Savedate,SWC013ID,SWC013TEL,SWC022ID,SWC024ID,SWC045ID,SWC101,SWC029CAD,SWC107ID,SWC107,SWC103,SWC104,SWC105,SWC106,SWC108,SWC109,SWC110,SWC113,SWC114,SWC115,[CaseDate01],[CaseDate02] ";
        string RecSqlStr = " INSERT INTO SWCCASE_record ("+ RecField + ") select "+ RecField + " from SWCCASE WHERE SWC000 = '" + gSWC000 + "';";

        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];

        #region saveArea1
        string sqlStr1 = "";
        if (!ChkHavCaseA(sSWC000))
        {
            sqlStr1 = " INSERT INTO SWCSWCA (SWC000) VALUES (@SWC000);";
        }
        sqlStr1 += " Update SWCSWCA Set SWC120=@SWC120,saveuser=@saveuser,savedate = getdate() Where SWC000=@SWC000;";
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", gSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC120", sSWC120));
                cmd.Parameters.Add(new SqlParameter("@saveuser", ssUserID));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
		//確認地籍欄位陽明山國家公園範圍，若為是所屬權責單位欄位國家公園要打勾
		if(CheckLand())
		{
			string sqlStr2 = "update SWCSWCA set SWC121=ISNULL(SWC121,'')+'國家公園;;',savedate=getdate() where SWC000=@SWC000 and ISNULL(SWC121,'') not like '%國家公園%' ;";
			using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
			{
				TslmConn.Open();
				using (var cmd = TslmConn.CreateCommand())
				{
					cmd.CommandText = sqlStr2;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("@SWC000", gSWC000));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
        #endregion

        #region 變更設計   
        if (pageAction == "COPY")
        {
            RsvSQLStr += " Update SWCSWC Set SWC51 = '" + TXTSWC051.Text + "', ";
            ExeSQLStr += " Update SWCCASE Set SWC051 = '" + TXTSWC051.Text + "', ";
            string[] aFeildSWC = new string[] { "SWC04", "SWC21", "SWC021ID", "SWC24", "SWC024ID", "SWC25", "SWC41", "SWC43", "SWC44", "SWC45", "SWC045ID", "SWC47", "SWC52", "SWC82", "SWC83", "SWC112" };
            string[] aFeildDOC = new string[] { "SWC004", "SWC021", "SWC021ID", "SWC024", "SWC024ID", "SWC025", "SWC041", "SWC043", "SWC044", "SWC045", "SWC045ID", "SWC047", "SWC052", "SWC082", "SWC083", "SWC112" };
            Label[] aLabel = new Label[] { LBSWC004, LBSWC021, LBSWC021ID, LBSWC024, LBSWC024ID, LBSWC025, LBSWC041, LBSWC043, LBSWC044, LBSWC045, LBSWC045ID, LBSWC047, LBSWC052, LBSWC082, LBSWC083, LBSWC112 };
            for (int i = 0; i < aLabel.Length; i++)
            {
                string strFeildSWC = aFeildSWC[i], strFeildDOC = aFeildDOC[i];
                System.Web.UI.WebControls.Label LabelObj = aLabel[i];

                if (i == 0) {
                    RsvSQLStr += strFeildSWC + " =N'" + LabelObj.Text + "' ";
                    ExeSQLStr += strFeildDOC + " =N'" + LabelObj.Text + "' "; }
                else {
                    RsvSQLStr += "," + strFeildSWC + " =N'" + LabelObj.Text + "' ";
                    ExeSQLStr += "," + strFeildDOC + " =N'" + LabelObj.Text + "' "; }
            }
            RsvSQLStr += " Where SWC00 = '" + gSWC000 + "';";
            ExeSQLStr += " Where SWC000 = '" + gSWC000 + "';";
        }
        #endregion

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlCommand objCmdUser = new SqlCommand(ExeSQLStr+ RecSqlStr, SWCConn);
            objCmdUser.ExecuteNonQuery();
            objCmdUser.Dispose();
            
            //上傳檔案…
            UpLoadTempFileMoveChk(gSWC000);

            //地籍存檔...
            DataTable dtCD = new DataTable();
            
            string strSQLRV = "";
            string strSqlTslm = "";
            string DATA01 = "",DATA02="",DATA03="",DATA04="";
            
            strSQLRV = "delete SWCLAND Where SWC000 = '" + gSWC000 + "';";
            strSqlTslm = " delete relationLand Where 行政管理案件編號 = '" + gSWC002 + "'; ";

            dtCD = (DataTable)ViewState["SwcCadastral"];

            if (dtCD != null)
            {
                int i = 0;
                DATA01 = ";"; DATA02 = ";";DATA03 = ";"; DATA04 = ";";
                for (i = 0; i <= (Convert.ToInt32(dtCD.Rows.Count) - 1); i++)
                {
                    string tLAND000 = (i + 1).ToString();
                    string tLAND001 = dtCD.Rows[i]["區"].ToString().Trim();
                    string tLAND002 = dtCD.Rows[i]["段"].ToString().Trim();
                    string tLAND003 = dtCD.Rows[i]["小段"].ToString().Trim();
                    string tLAND004 = dtCD.Rows[i]["地號"].ToString().Trim();
                    string tLAND009 = dtCD.Rows[i]["山坡地範圍"].ToString().Trim();
                    string tLAND005 = dtCD.Rows[i]["土地使用分區"].ToString().Trim();
                    string tLAND006 = dtCD.Rows[i]["土地可利用限度"].ToString().Trim();
                    string tLAND010 = dtCD.Rows[i]["陽明山國家公園範圍"].ToString().Trim();
                    string tLAND007 = dtCD.Rows[i]["林地類別"].ToString().Trim();
                    string tLAND008 = dtCD.Rows[i]["地質敏感區"].ToString().Trim();
                    string tLAND011 = dtCD.Rows[i]["水保計畫申請紀錄"].ToString().Trim();
                    string tLAND012 = dtCD.Rows[i]["水土保持法違規紀錄"].ToString().Trim();
                    string tKCNT = tLAND002 + "段" + tLAND003 + "小段";
                    
                    strSQLRV = strSQLRV + " insert into SWCLAND (SWC000,LAND000,LAND001,LAND002,LAND003,LAND004,LAND005,LAND006,LAND007,LAND008,LAND009,LAND010,LAND011,LAND012) VALUES ";
                    strSQLRV = strSQLRV + " ('" + gSWC000 + "','" + tLAND000 + "','" + tLAND001 + "','" + tLAND002 + "','" + tLAND003 + "','" + tLAND004 + "','" + tLAND005 + "','" + tLAND006 + "','" + tLAND007 + "','" + tLAND008 + "','" + tLAND009 + "','" + tLAND010 + "','" + tLAND011 + "','" + tLAND012 + "');  ";

                    strSqlTslm = strSqlTslm + " INSERT INTO [relationLand] ([區] , [段], [小段], [KCNT], [地號], [水保案件編號], [行政管理案件編號], [違規案件編號], [備註], [土地權屬], [地目], [土地使用分區], [土地可利用限度], [林地類別], [地質敏感區], [山坡地範圍], [陽明山國家公園範圍], [水保計畫申請紀錄], [水土保持法違規紀錄] ) VALUES ";
                    strSqlTslm = strSqlTslm + " ('" + tLAND001 + "' ,'" + tLAND002 + "' ,'" + tLAND003 + "' ,'" + tKCNT + "' ,'" + tLAND004 + "' ,'' ,'" + gSWC002 + "' ,'' ,'' ,'' ,'' ,'" + tLAND005 + "' ,'" + tLAND006 + "' ,'" + tLAND007 + "' ,'" + tLAND008 + "' ,'" + tLAND009 + "' ,'" + tLAND010 + "','" + tLAND011 + "','" + tLAND012 + "'); ";

                    DATA01 = DATA01 + tLAND001 + ";";
                    DATA02 = DATA02 + tLAND002 + ";";
                    DATA03 = DATA03 + tLAND003 + ";";
                    DATA04 = DATA04 + tLAND004 + ";";
                }

                strSQLRV = strSQLRV + " Update SWCCASE Set ";
                strSQLRV = strSQLRV + " SWC008 =N'" + DATA01 + "', ";
                strSQLRV = strSQLRV + " SWC009 =N'" + DATA02 + "', ";
                strSQLRV = strSQLRV + " SWC010 =N'" + DATA03 + "', ";
                strSQLRV = strSQLRV + " SWC011 =N'" + DATA04 + "' ";
                strSQLRV = strSQLRV + " Where SWC000 = '" + gSWC000 + "';";

                strSqlTslm = strSqlTslm + " Update SWCSWC Set ";
                strSqlTslm = strSqlTslm + " SWC08 =N'" + DATA01 + "', ";
                strSqlTslm = strSqlTslm + " SWC09 =N'" + DATA02 + "', ";
                strSqlTslm = strSqlTslm + " SWC10 =N'" + DATA03 + "', ";
                strSqlTslm = strSqlTslm + " SWC11 =N'" + DATA04 + "' ";
                strSqlTslm = strSqlTslm + " Where SWC00 = '" + gSWC000 + "';";

                SqlCommand objCmdItem2 = new SqlCommand(strSQLRV, SWCConn);
                objCmdItem2.ExecuteNonQuery();

                objCmdItem2.Cancel();
                objCmdItem2.Dispose();

                CDNO.Text = i.ToString();
            }
            qTslmLand = strSqlTslm;

            if (pageAction.Trim() == "ADDNEW" || pageAction.Trim() == "COPY") { } else {
            #region-委員存檔
            //string exeSqlStr = " Delete GuildGroup Where SWC000 = '"+ gSWC000 + "'; ";
            //DropDownList[] arryDDL01 = new DropDownList[] { DDLSA01, DDLSA02, DDLSA03, DDLSA04, DDLSA05, DDLSA06, DDLSA07, DDLSA08, DDLSA09, DDLSA10, DDLSB01, DDLSB02 };
			//
            //for (int i = 0; i < arryDDL01.Length; i++)
            //{
            //    DropDownList DDLTMP = arryDDL01[i];
            //    string selETID = DDLTMP.SelectedItem.Value;
            //    string tmpType = i < 10 ? "S3" : "S4";
            //    string tmpType2 = i == 0 || i > 9 ? "1" : "0";
            //    exeSqlStr += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES ('" + gSWC000 + "','" + gSWC002 + "','" + (i + 1).ToString() + "','" + selETID + "','" + tmpType + "','" + tmpType2 + "','"+ ssUserID + "',getdate());";
            //}
            //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            //{
            //    SwcConn.Open();
            //    SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
            //    objCmdUpd.ExecuteNonQuery();
            //    objCmdUpd.Dispose();
            //}
            #region.寄信
            string tSWC013TEL = TXTSWC013TEL.Text;  //義務人手機
            string tSendMail = TXTSWC108.Text;      //聯絡人e-mail
            string tSWC012 = LBSWC012.Text;         //轄區
            string tSWC021ID = LBSWC021ID.Text;     //承辦技師
            string tSWC022ID = LBSWC022ID.Text;     //審查公會
            string tSWC024ID = LBSWC024ID.Text;     //檢查公會            
            string tSWC025 = LBSWC025.Text;         //承辦人員
            string tSWC045ID = LBSWC045ID.Text;     //監造技師
            string tCaseStatus = LBSWC004.Text;
            string tGuildName = Session["NAME"] +"";
            string tMailSub  = "",tMailText = "",SentMailGroup = "";
			string[] arraySWC013TEL = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);

            if (CHKSendMail01.Checked)
            {
                //23.若狀態為「審查中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「審查會議時間」	
                //股長、管理者、承辦人員、承辦技師、審查委員(召集人、委員)、審查公會、義務人 
                if (tCaseStatus == "審查中")
                {
                    tMailSub  = "提醒您，【" + tGuildName + "】已訂於 " + gSWC113 + " 舉辦【"+ gSWC005 + "】審查會議。";
                    tMailText = "提醒您，【" + tGuildName + "】已訂於 " + gSWC113 + " 舉辦【" + gSWC005 + "】審查會議。";
                    tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                    tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                    string exeSQLSTR = " select ETEmail as EMAIL from GuildGroup G Left Join ETUsers E On G.ETID = E.ETID where G.swc000 = '"+ gSWC000 + "' and((ETEmail is not null and RGSID = 'S3') or e.etid = '"+ tSWC021ID + "') ";
                    exeSQLSTR += "UNION ALL select email as EMAIL from tslm2.dbo.geouser g where ((department = '審查管理科' and ((jobtitle = '股長' and Tcgearea01 like '%"+ tSWC012 + "%') or mbgroup02 = '系統管理員' or[name] = '"+ tSWC025 + "')) or(userid = '"+ tSWC022ID + "')) and status <> '停用' ";

                    #region.名單
                    using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SwcConn.Open();

                        SqlDataReader readerItemS;
                        SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR, SwcConn);
                        readerItemS = objCmdItemS.ExecuteReader();

                        while (readerItemS.Read())
                        {
                            string tEMail = readerItemS["EMAIL"] + "";
                            SentMailGroup += ";;" + tEMail;
                        }
                        readerItemS.Close();
                        objCmdItemS.Dispose();
                    }
                    SentMailGroup += ";;" + tSendMail;
                    string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                    #endregion

                    bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub, tMailText);
                    
					SBApp.SendSMS_Arr(arraySWC013TEL, tMailSub);
                }
            }
            if (CHKSendMail02.Checked)
            {
                //24.若狀態為「施工中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「施工檢查時間」
                //股長、管理者、承辦人員、監造技師、檢查委員(2位)、檢查公會、義務人


                if (tCaseStatus == "施工中")
                {
                    tMailSub = "提醒您，【" + tGuildName + "】已訂於 " + gSWC114 + " 進行【" + gSWC005 + "】施工監督檢查。";
                    tMailText = "提醒您，【" + tGuildName + "】已訂於 " + gSWC114 + " 進行【" + gSWC005 + "】施工監督檢查。";
                    tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                    tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                    string exeSQLSTR = " select ETEmail as EMAIL from GuildGroup G Left Join ETUsers E On G.ETID = E.ETID where G.swc000 = '" + gSWC000 + "' and((ETEmail is not null and RGSID = 'S4') or e.etid = '" + tSWC045ID + "') ";
                    exeSQLSTR += "UNION ALL select email as EMAIL from tslm2.dbo.geouser g where ((department = '審查管理科' and ((jobtitle = '股長' and Tcgearea01 like '%" + tSWC012 + "%') or mbgroup02 = '系統管理員' or[name] = '" + tSWC025 + "')) or(userid = '" + tSWC024ID + "')) and status <> '停用' ";

                    #region.名單
                    using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SwcConn.Open();

                        SqlDataReader readerItemS;
                        SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR, SwcConn);
                        readerItemS = objCmdItemS.ExecuteReader();

                        while (readerItemS.Read())
                        {
                            string tEMail = readerItemS["EMAIL"] + "";
                            SentMailGroup += ";;" + tEMail;
                        }
                        readerItemS.Close();
                        objCmdItemS.Dispose();
                    }
                    SentMailGroup += ";;" + tSendMail;
                    string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                    #endregion

                    bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub, tMailText);
                    
					SBApp.SendSMS_Arr(arraySWC013TEL, tMailSub);
                }
            }
            if (CHKSendMail03.Checked)
            {
                tMailSub  = "提醒您，【" + tGuildName + "】已訂於 " + gSWC115 + " 舉辦【" + gSWC005 + "】完工檢查。";
                tMailText = "提醒您，【" + tGuildName + "】已訂於 " + gSWC115 + " 舉辦【" + gSWC005 + "】完工檢查。";
                tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                //若狀態為「施工中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「完工檢查時間」
                //股長、管理者、承辦人員、監造技師、檢查委員、公會、義務人

                string exeSQLSTR = " select ETEmail as EMAIL from GuildGroup G Left Join ETUsers E On G.ETID = E.ETID where G.swc000 = '" + gSWC000 + "' and((ETEmail is not null and RGSID = 'S4') or e.etid = '" + tSWC045ID + "') ";
                exeSQLSTR += "UNION ALL select email as EMAIL from tslm2.dbo.geouser g where ((department = '審查管理科' and ((jobtitle = '股長' and Tcgearea01 like '%" + tSWC012 + "%') or mbgroup02 = '系統管理員' or[name] = '" + tSWC025 + "')) or(userid = '" + tSWC024ID + "')) and status <> '停用' ";

                #region.名單
                using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                {
                    SwcConn.Open();

                    SqlDataReader readerItemS;
                    SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR, SwcConn);
                    readerItemS = objCmdItemS.ExecuteReader();

                    while (readerItemS.Read())
                    {
                        string tEMail = readerItemS["EMAIL"] + "";
                        SentMailGroup += ";;" + tEMail;
                    }
                    readerItemS.Close();
                    objCmdItemS.Dispose();
                }
                SentMailGroup += ";;" + tSendMail;
                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                #endregion

                if (tCaseStatus == "施工中")
                {
                    bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub, tMailText);
                    
					SBApp.SendSMS_Arr(arraySWC013TEL, tMailSub);
                }
            }
                #endregion
                #endregion
            }
            //更新空間資料
            SBApp.UpdateShape(gSWC002, gSWC027, gSWC028, "0"); 
        }

        //Synchronize
        string synfile = " update UploadFileSyn set HAVEPROCESS=0  where SOURCEPC = 'swcdoc' and SOURCEURL like '%"+ gSWC000 + "%' and REQUESTTIME > dateadd(hour,-1,getdate());";
        string allExcSqlStr = RsvSQLStr + qTslmLand + synfile + "";

        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();

            allExcSqlStr = allExcSqlStr + "";

            SqlCommand objCmdTsl = new SqlCommand(allExcSqlStr, TslmConn);
            objCmdTsl.ExecuteNonQuery();
            objCmdTsl.Dispose();            
        }

        SaveGridViewData(gSWC000); MoveSwcFiles(gSWC000);
        if (tmpAddCase == "Y")
            SendMailAddNew(gSWC000);
        SBApp.ViewRecord("水保申請案件", "update", gSWC000);
        swcSendMail();

        Response.Write("<script>alert('資料已存檔'); location.href='SWC003.aspx?SWCNO=" + gSWC000 + "'; </script>");
    }

    private bool ChkHavCaseA(string sSWC000)
    {
        #region ChkHavCaseA
        bool rValue = false;
        string sqlstr = " SELECT * FROM SWCSWCA where SWC000=@SWC000 ";
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            rValue = true;
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
        #endregion
    }
	
	private bool ChkHaveSWC(string sSWC000)
    {
        #region ChkHaveSWC
        bool rValue = false;
        string sqlstr = " SELECT * FROM SWCSWC where SWC00=@SWC00 ";
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC00", sSWC000));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            rValue = true;
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
        #endregion
    }

    private void swcSendMail()
    {
        GBClass001 GCA = new GBClass001();
        string cSWC004 = LBSWC004.Text;
        string cSWC005 = TXTSWC005.Text;
        string cSWC012 = LBSWC012.Text;
        string cSWC025 = LBSWC025.Text; //承辦人員
        string MailSub = "";
        string MailBody = "";

        #region 39.使用者上傳檔案至「計畫申請書」欄位
        //系統管理員、承辦人員、章姿隆
        //承辦技師已於(上傳日期)上傳【水土保持計畫】計畫申請書，請至書件管理平台查看
        string tv = TBuploadNtc.Text;
        string[] userData39 = GCA.GetGeoUserBaseData("", "系統管理員;", cSWC025+";章姿隆;");
        string[] GetMailTo39 = userData39[2].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

        MailSub = "承辦技師已於("+DateTime.Now.ToString("yyyy-MM-dd")+ ")上傳【" + cSWC005 + "】計畫申請書，請至書件管理平台查看。";
        MailBody = "您好，" + cSWC012 + "承辦技師已於(" + DateTime.Now.ToString("yyyy-MM-dd") + ")上傳【" + cSWC005 + "】計畫申請書，請至書件管理平台查看。";
        if(tv=="1")
            GCA.Mail_Send(GetMailTo39, MailSub, MailBody);
        #endregion

        #region 40.若狀態為「審查中」，「公會建議核定日期」從「null變成有值」時寄信。
        //20210917暫停寄信
		//string cSWC109 = TXTSWC109.Text;
        //string cSWC109o = TBSWC109o.Text;
        //string[] userData40 = GCA.GetGeoUserBaseData("科長;正工程司;股長;", "系統管理員;", cSWC025 + ";章姿隆;");
        //string[] GetMailTo40 = userData40[2].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
		//
        //switch (cSWC004) {
        //    case "審查中":
        //        MailSub = cSWC012 + "【" + cSWC005 + "】，審查單位已於(" + cSWC109 + ")建議核定，請至系統查看。";
        //        MailBody = "您好，" + cSWC012 + "【" + cSWC005 + "】，審查單位已於(" + cSWC109 + ")建議核定，請至系統查看。";
        //        if (cSWC109o == "" && cSWC109 != "")
        //            GCA.Mail_Send(GetMailTo40, MailSub, MailBody);
        //        break;
        //}
        #endregion
    }
    private void SendMailAddNew(string gSWC000)
    {
        string ssUserName = Session["NAME"] + "";

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

                //37.使用者於書件管理平台申請水土保持案件
                //送出提醒名單：股長、系統管理員、承辦人員、章姿隆(ge-10706)

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706")
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;
                    }
                }
                //一人一封，mail server會壞掉，還是大家一封!!，但民眾還是一人一封…
                string sToday = DateTime.Now.ToString("yyyy-MM-dd");
                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                string ssMailSub01 = ssUserName + "已於 "+ sToday + " 申請【"+ tSWC005 + "】，請至書件管理平台查看";
                string ssMailBody01 = ssUserName + "已於 " + sToday + " 申請【"+ tSWC005 + "】，請至書件管理平台查看" + "<br><br>";
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }

    }

    private void MoveSwcFiles(string CaseId)
    {
        Boolean folderExists;

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            string strSQLRV = "";
            string tFILE001 = "001";

            strSQLRV = " Select * From SWCFILES ";
            strSQLRV = strSQLRV + " Where SWC000 = '" + CaseId + "' ";
            strSQLRV = strSQLRV + "   and FILE001 = '" + tFILE001 + "' ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, SWCConn);
            readerItem = objCmdItem.ExecuteReader();

            string FileSTr = "";
            while (readerItem.Read())
            {
                FileSTr = FileSTr + "," + readerItem["FILE003"] +"";
            }
            
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

            string[] arrayFile = FileSTr.Split(new string[] { "," }, StringSplitOptions.None);
            
            for (int i = 0; i < arrayFile.Length; i++)
            {
                string csUpLoadField = arrayFile[i];

                Boolean fileExists;

                string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadField;
                string SwcCaseFilePath = SwcCaseFolderPath + CaseId + "\\" + csUpLoadField;

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
    private void UpLoadTempFileMoveChk(string CaseId)
    {
		GBClass001 MyBassAppPj = new GBClass001();
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTSWC029", "TXTSWC029CAD", "TXTSWC030", "TXTSWC080", "TXTSWC101", "TXTSWC110", "TXTSWC101CAD", "TXTSWC138" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTSWC029, TXTSWC029CAD,TXTSWC030, TXTSWC080, TXTSWC101, TXTSWC110, TXTSWC101CAD, TXTSWC138 };
        string csUpLoadField = "TXTSWC029";
        TextBox csUpLoadAppoj = TXTSWC029;

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
                string SwcCaseFolderPath1 = MyBassAppPj.getFilePath(LBSWC000.Text, LBSWC002.Text, DDLSWC007.Text);
                string SwcCaseFilePath1 = MyBassAppPj.getFilePath(LBSWC000.Text, LBSWC002.Text, DDLSWC007.Text) + LBSWC002.Text + "\\環評報告書or免環評證明文件\\環評報告書or免環評證明文件\\" + csUpLoadAppoj.Text;

                fileExists = File.Exists(TempFilePath);
                if (fileExists)
                {
					if(csUpLoadField == "TXTSWC138")
					{
						SwcCaseFolderPath1 += LBSWC002.Text;
						folderExists = Directory.Exists(SwcCaseFolderPath1);
						if (folderExists == false)
						{
							Directory.CreateDirectory(SwcCaseFolderPath1);
						}
						SwcCaseFolderPath1 += "\\環評報告書or免環評證明文件";
						folderExists = Directory.Exists(SwcCaseFolderPath1);
						if (folderExists == false)
						{
							Directory.CreateDirectory(SwcCaseFolderPath1);
						}
						SwcCaseFolderPath1 += "\\環評報告書or免環評證明文件";
						if(Directory.Exists(SwcCaseFolderPath1))
						{
							DirectoryInfo di = new DirectoryInfo(SwcCaseFolderPath1);
							foreach (FileInfo file in di.GetFiles())
							{
								file.Delete(); 
							}
						}
						else
						{
							Directory.CreateDirectory(SwcCaseFolderPath1);
						}
						
						//if (File.Exists(SwcCaseFilePath1))
						//{
						//	File.Delete(SwcCaseFilePath1);
						//}
						File.Move(TempFilePath, SwcCaseFilePath1);
					}
					else
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
              
    }

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink,float _FileMaxSize)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string SwcFileName = "";
        string CaseId = LBSWC000.Text + "";
        string FileId = LBSWC002.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名

            switch (ChkType)
            {
                case "竣工圖說CAD":
                case "CAD6-1":
                    List<string> allowedExtextsion04 = new List<string> { ".dwg", ".DWG" };

                    if (allowedExtextsion04.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 CAD 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "PDF6-1":
                case "PDF7-1":
                case "PDF":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".PDF" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
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
                    break;
				case "PDFJPG138":
                    List<string> allowedExtextsion05 = new List<string> { ".pdf", ".PDF", ".jpg", ".JPG",  };

                    if (allowedExtextsion05.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF JPG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;
            
            if (filesize > _FileMaxSize* 1000000)
            //if (filesize > 5000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);

            switch (ChkType)
            {
                case "CAD6-1":
                    SwcFileName = FileId + "_6-1.dwg";
                    break;
                case "PDF6-1":
                    SwcFileName = FileId + "_6-1.pdf";
                    break;
                case "PDF7-1":
                    SwcFileName = FileId + "_7-1.pdf";
                    break;
            }

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
            string pdfWMGimg = LBSWC022ID.Text;
            try 
            {
                UpLoadBar.SaveAs(serverFilePath);

                string newFileName = CaseId.Substring(CaseId.Length-5, 5);

                switch (UpLoadStr)
                {
                    case "TXTSWC029":
                        newFileName = "PDF6-1_" + newFileName + ".pdf";
                        SwcFileName = newFileName;

                        //浮水印處理
                        MyBassAppPj.PDFWaterMark(serverFilePath, Path.Combine(serverDir, newFileName), pdfWMGimg);
                        break;
                    case "TXTSWC030":
                        newFileName = "PDF7-1_" + newFileName + ".pdf";
                        SwcFileName = newFileName;

                        //浮水印處理
                        MyBassAppPj.PDFWaterMark(serverFilePath, Path.Combine(serverDir, newFileName), pdfWMGimg);
                        break;
                    case "TXTSWC080":
                        newFileName = "核定本_"+ newFileName+".pdf";
                        SwcFileName = newFileName;

                        //浮水印處理
                        MyBassAppPj.PDFWaterMark(serverFilePath, Path.Combine(serverDir, newFileName), pdfWMGimg);
                        break;
                }
                //UpLoadStr

                //error_msg.Text = "檔案上傳成功";

                switch (ChkType)
                {
                    case "PIC":
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "竣工圖說CAD":
                    case "CAD6-1":
                    case "PDF6-1":
                    case "PDF7-1":
                    case "PDF":
                    case "DOC":
                    case "PDFJPG138":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;

                }
                UpLoadText.Text = SwcFileName;
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
    
    protected void ButtonDTL01_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssCaseStatus = Session["CaseStatus"] + "";
        string view = "";

        if (ssCaseStatus != "審查中" && ssCaseStatus != "暫停審查")
        {
            view = "v";
        }

        Button LButton = (Button)sender;
        string LINK = "SWCDT001"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);
    }
    protected void ButtonDTL03_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender,e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        Button LButton = (Button)sender;

        string GetDTLType = LButton.CommandArgument.Substring(0, 2);
        string GetDTLType2 = LButton.CommandArgument.Substring(0, 6);
        if (GetDTLType2 == "SWCCHG") { GetDTLType = "RC"; }
        string ssCaseStatus = Session["CaseStatus"]+"";
        string view = "";
        string LINK = "SWC001.aspx";

        if (ssCaseStatus !="施工中" || Session["DTL03VIEW"]+"" == "Y")
        {
            view = "v";
        }
        
        switch (GetDTLType)
        {
            case "RC":
                LINK = "SWCDT003"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;
            case "RB":
                LINK = "SWCDT002v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
                break;
        }
        Response.Redirect(LINK);

    }
    protected void ButtonDTL04_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssDTL04VIEW = Session["DTL04VIEW"] + "";
        string ssCaseStatus = Session["CaseStatus"] + "";
        string view = "";

        if (ssCaseStatus != "施工中")
            view = "v";

        Button LButton = (Button)sender;
        string LINK = "SWCDT004"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        if (ssDTL04VIEW == "Y") //檢查公會
        {
            LINK = "SWCDT004v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        } 
        Response.Redirect(LINK);
    }
    protected void ButtonDTL05_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssDTL05VIEW = Session["DTL05VIEW"] + "";
        string ssCaseStatus = Session["CaseStatus"] + "";
        string view = "";

        if (ssCaseStatus != "施工中")
            view = "v";

        Button LButton = (Button)sender;
        string LINK = "SWCDT005"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        if (ssDTL05VIEW == "Y") //檢查公會
            LINK = "SWCDT005v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void ButtonDTL06_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssCaseStatus = Session["CaseStatus"] + "";
        string view = "";

        if (ssCaseStatus != "施工中")
        {
            view = "v";
        }

        Button LButton = (Button)sender;
        string LINK = "SWCDT006"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);
    }

    protected void ButtonDTL07_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssCaseStatus = Session["CaseStatus"] + "";
        string view = "";

        if (ssCaseStatus != "已完工" && ssCaseStatus != "已變更" && ssCaseStatus != "撤銷")
        {
            view = "v";
        }

        Button LButton = (Button)sender;
        string LINK = "SWCDT007"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        Response.Redirect(LINK);

    }
    protected void TXTSWC080_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC080.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC080_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDF", TXTSWC080_fileupload, TXTSWC080, "TXTSWC080", "_" + rDTLNO + "_pdf02", null, Link080, 150); //150MB

        string sourceFILENAME = TXTSWC080.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "/UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "掃描檔";

        if (sourceFILENAME !="")
        {
            SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        }
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected void TXTSWC029CAD_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC029CAD.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC029CAD_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("CAD6-1", TXTSWC029CAD_fileupload, TXTSWC029CAD, "TXTSWC029CAD", "_" + rDTLNO + "_CAD", null, Link029CAD, 50); //50MB

        string sourceFILENAME = TXTSWC029CAD.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "6-1-CAD";

        if (sourceFILENAME != "") {
            SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        }
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }
    protected void TXTSWC029_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC029.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC029_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDF6-1", TXTSWC029_fileupload, TXTSWC029, "TXTSWC029", "_" + rDTLNO + "", null, Link029, 50); //50MB

        string sourceFILENAME = TXTSWC029.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "6-1";

        if (sourceFILENAME !="") { SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);}
        
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }
    protected void TXTSWC030_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC030.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC030_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDF7-1", TXTSWC030_fileupload, TXTSWC030, "TXTSWC030", "_" + rDTLNO + "", null, Link030, 50); //50MB

        string sourceFILENAME = TXTSWC030.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "7-1";

        if (sourceFILENAME !="") { SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype); }
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }

    protected void BTNSWC101_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC101.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC101_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDF", TXTSWC101_fileupload, TXTSWC101, "TXTSWC101", "_" + rDTLNO + "_pdf05", null, Link101, 50); //50MB

        string sourceFILENAME = TXTSWC101.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "竣工圖說";

        if (sourceFILENAME !="") { SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype); }
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }


    protected void TXTFILES001_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDF", TXTFILES001_fileupload, TXTFILES001, "TXTFILES001","", null, Link001, 500); //150MB
        Response.Write("<script>alert('檔案上已上傳，請按下方「加入清單」按鈕，方為上傳成功。');</script>");

    }   

    protected void AddFile001_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBSWC000.Text + "";
        string tFiles001No = Files001No.Text;
        Files001No.Text = (Convert.ToInt32(tFiles001No) + 1).ToString();

        DataTable File001C = (DataTable)ViewState["File001C"];

        if (File001C == null)
        {
            DataTable GVFILE001 = new DataTable();

            GVFILE001.Columns.Add(new DataColumn("File001000", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001001", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001002", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001003", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001004", typeof(string)));

            ViewState["File001C"] = GVFILE001;
            File001C = (DataTable)ViewState["File001C"];

        }
        DataRow GVFILE001Row = File001C.NewRow();

        GVFILE001Row["File001000"] = Files001No.Text;
        GVFILE001Row["File001001"] = "001";
        GVFILE001Row["File001002"] = "";
        GVFILE001Row["File001003"] = TXTFILES001.Text;
        GVFILE001Row["File001004"] = GlobalUpLoadTempFilePath+ rDTLNO+"\\" + TXTFILES001.Text;

        File001C.Rows.Add(GVFILE001Row);

        //Store the DataTable in ViewState
        ViewState["File001C"] = File001C;

        SWCFILES001.DataSource = File001C;
        SWCFILES001.DataBind();

        TBuploadNtc.Text = "1";
        TXTFILES001.Text = "";
        Link001.Text = "";
        Link001.NavigateUrl = "";
        //ACItem2c.Text = "";
        //TxtItemNi2.Text = nItem2.ToString();

    }
    protected void ADDLIST01_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();
        LandDetailClass LDC = new LandDetailClass();
        string addData01 = DDLDistrict.Text + "";
        string addData02 = DDLSection.Text + "";
        string addData03 = DDLSection2.Text + "";
        string addData04 = TXTNumber.Text + "";
        error_msg.Text = "";

        if (addData01 == "" || addData02 == "" || addData03 == "" || addData04 == "")
        {
            error_msg.Text = SBApp.AlertMsg("請輸入完整地段號");
            return;
        }

        CDNO.Text = (Convert.ToInt32(CDNO.Text) + 1).ToString();

        DataTable tbCadastral = (DataTable)ViewState["SwcCadastral"];

        if (tbCadastral == null)
        {
            DataTable GVTBCD = new DataTable();

            GVTBCD.Columns.Add(new DataColumn("序號", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("區", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("段", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("小段", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("地號", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("山坡地範圍", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("土地使用分區", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("土地可利用限度", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("陽明山國家公園範圍", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("林地類別", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("地質敏感區", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("水保計畫申請紀錄", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("水土保持法違規紀錄", typeof(string)));

            ViewState["SwcCadastral"] = GVTBCD;
            tbCadastral = (DataTable)ViewState["SwcCadastral"];            
        }
        string adArea = DDLDistrict.SelectedItem.Text;
        string adSection = DDLSection.SelectedItem.Text+"段"+ DDLSection2.SelectedItem.Text+"小段";
        string adNum = TXTNumber.Text;
		
		//避免成大地籍壞掉，如果壞掉全部都先帶空，之後再由資料庫存檔重新跑（資料為空且核定前）
        string[] arrayCD = new string[11];
		try{
			arrayCD = C20.CadastralInfo(adArea, adSection, adNum);
		}
		catch(Exception ex) {
			arrayCD[0] = "";
			arrayCD[1] = "";
			arrayCD[2] = "";
			arrayCD[3] = "";
			arrayCD[4] = "";
			arrayCD[5] = "";
			arrayCD[6] = "";
			arrayCD[7] = "";
			arrayCD[8] = "";
			arrayCD[9] = "";
			arrayCD[10] = "";
		}
		
        string newA = "", newB = "", newC = "", newD = "", newE = "", newF = "";
        
		//newA = arrayCD[1] == "是" ? "宜林地" : arrayCD[1] == "否" ? "否" : "";
        //newA = arrayCD[2] == "是" ? "宜農牧地" : arrayCD[2] == "否" ? newA == "" ? "否" : newA : "";
        //newA = arrayCD[1] == "否" && arrayCD[2] == "否" ? "非屬查定範圍" : newA;
        if (arrayCD[1] == "是" && arrayCD[2] == "否") newA = "宜林地";
        if (arrayCD[1] == "否" && arrayCD[2] == "是") newA = "宜農牧地";
        if (arrayCD[1] == "否" && arrayCD[2] == "否") newA = "非屬範圍內";
        if (arrayCD[1] == "是" && arrayCD[2] == "是") newA = "宜林地";

        //newB = arrayCD[6] == "是" ? "本市林地" : "非屬公告範圍";//arrayCD[2] == "宜農牧地" ? "" : "";
        //newB = arrayCD[2] == "是" ? "宜農牧地" : arrayCD[2] == "否" ? "否" : "";
        if (arrayCD[4] == "是" && arrayCD[6] == "否") newB = "屬保安林";
        if (arrayCD[4] == "否" && arrayCD[6] == "是") newB = "屬臺北市林地";
        if (arrayCD[4] == "否" && arrayCD[6] == "否") newB = "非屬範圍內";
        if (arrayCD[4] == "是" && arrayCD[6] == "是") newB = "屬保安林";

        //newC = arrayCD[5] == "是" ? "山崩與地滑" : arrayCD[5] == "否" ? "否" : "";
        if (arrayCD[5] == "是") newC = "屬地質敏感區";
        if (arrayCD[5] == "否") newC = "非屬範圍內";

        newD = arrayCD[7] == "" ? "" : newD = arrayCD[7];

        if (arrayCD[0] == "是") newE = "屬山坡地";
        if (arrayCD[0] == "否") newE = "非屬範圍內";

        if (arrayCD[3] == "是") newF = "屬國家公園";
        if (arrayCD[3] == "否") newF = "非屬範圍內";

        DataRow GVTBCDRow = tbCadastral.NewRow();

        GVTBCDRow["序號"] = CDNO.Text;
        GVTBCDRow["區"] = adArea;
        GVTBCDRow["段"] = DDLSection.SelectedItem.Text;
        GVTBCDRow["小段"] = DDLSection2.SelectedItem.Text;
        GVTBCDRow["地號"] = TXTNumber.Text;
        GVTBCDRow["山坡地範圍"] = newE;
        //GVTBCDRow["土地使用分區"] = DDLCA01.SelectedItem.Text;
        GVTBCDRow["土地使用分區"] = newD;
        GVTBCDRow["土地可利用限度"] = newA;
        GVTBCDRow["陽明山國家公園範圍"] = newF;
        GVTBCDRow["林地類別"] = newB;
        GVTBCDRow["地質敏感區"] = newC;

		string getswc = (LDC.getSWC(adArea, DDLSection.SelectedItem.Text, DDLSection2.SelectedItem.Text, TXTNumber.Text).ToString() == "True") ? "有" : "無";
        string getilg = (LDC.getILG(adArea, DDLSection.SelectedItem.Text, DDLSection2.SelectedItem.Text, TXTNumber.Text).ToString() == "True") ? "有" : "無";

		GVTBCDRow["水保計畫申請紀錄"] = getswc;
        GVTBCDRow["水土保持法違規紀錄"] = getilg;

        tbCadastral.Rows.Add(GVTBCDRow);

        //Store the DataTable in ViewState
        ViewState["SwcCadastral"] = tbCadastral;

        GVCadastral.DataSource = tbCadastral;
        GVCadastral.DataBind();
		
		if(GVCadastral.Rows.Count > 0) P_Message.Visible = true;
		else P_Message.Visible = false;
		
        if ((arrayCD[0]+"").Trim() == "") {
            Response.Write("<script>alert('很抱歉，因內政部地政司系統維護中，請您稍後再試');</script>");
        }

        //以下為清空已新增資料…但是業主想要保留，所以先mark
        //DDLDistrict.SelectedValue = "0";
        //DDLCadastralChange("01", DDLDistrict, DDLSection, DDLSection2, DDLNumber);

        //DDLCA01.SelectedValue = "";
        //DDLCA02.SelectedValue = "";
        //DDLCA03.SelectedValue = "";
        //DDLCA04.SelectedValue = "";
        

    }

    private void DDLCadastralChange(string dTYPE,DropDownList vDP01, DropDownList vDP02, DropDownList vDP03)
    {
        //區、段、小段、地號
        string strDP01 = vDP01.Text;
        string strDP02 = vDP02.Text;
        string strDP03 = vDP03.Text;

        if (strDP01 == "0")
        {
            vDP03.Items.Clear();
            vDP02.Items.Clear();

        }
        else
        {
            string strSQLCAADDR = " ";

            switch (dTYPE)
            {
                case "01":
                    strSQLCAADDR = strSQLCAADDR + " SELECT DISTINCT LEFT(KCNT,2) AS KCNT FROM LAND ";
                    strSQLCAADDR = strSQLCAADDR + " WHERE AA46='" + strDP01 + "' ";
                    break;

                case "02":
                    strSQLCAADDR = strSQLCAADDR + " SELECT DISTINCT SUBSTRING(KCNT, LEN(KCNT) - 2, 1) AS KCNT2 FROM LAND ";
                    strSQLCAADDR = strSQLCAADDR + " WHERE AA46='" + strDP01 + "' ";
                    strSQLCAADDR = strSQLCAADDR + "   AND substring(KCNT, 1, 2)='" + strDP02 + "' ";

                    break;

                case "03":
                    string csKCNT = strDP02 + "段" + strDP03 + "小段";

                    strSQLCAADDR = strSQLCAADDR + " SELECT DISTINCT CADA_TEXT FROM LAND ";
                    strSQLCAADDR = strSQLCAADDR + " WHERE AA46='" + strDP01 + "' ";
                    strSQLCAADDR = strSQLCAADDR + "   AND KCNT='" + csKCNT + "' ";
                    strSQLCAADDR = strSQLCAADDR + " ORDER BY CADA_TEXT ";
                    break;

            }

            //連DB
            ConnectionStringSettings settingCAADDR = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            SqlConnection ConnCAADDR = new SqlConnection();
            ConnCAADDR.ConnectionString = settingCAADDR.ConnectionString;
            ConnCAADDR.Open();

            SqlDataReader readerCAADDR;
            SqlCommand objCmdGETCAADDR = new SqlCommand(strSQLCAADDR, ConnCAADDR);
            readerCAADDR = objCmdGETCAADDR.ExecuteReader();

            if (readerCAADDR.HasRows)
            {
                switch (dTYPE)
                {
                    case "01":
                        vDP03.Items.Clear();
                        vDP02.Items.Clear();
                        vDP02.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            vDP02.Items.Add(readerCAADDR["KCNT"].ToString());
                        }
                        break;
                    case "02":
                        vDP03.Items.Clear();
                        vDP03.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            vDP03.Items.Add(readerCAADDR["KCNT2"].ToString());
                        }
                        break;
                    case "03":
                        while (readerCAADDR.Read())
                        {
                            //if (readerCAADDR["CADA_TEXT"].ToString() != "") { vDP04.Items.Add(readerCAADDR["CADA_TEXT"].ToString()); }
                        }
                        break;
                }
            }

            objCmdGETCAADDR.Dispose();
            readerCAADDR.Close();

            ConnCAADDR.Close();
            ConnCAADDR.Dispose();

        }

    }
    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DDLCadastralChange("01", DDLDistrict, DDLSection, DDLSection2);
    }
    protected void DDLSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DDLCadastralChange("02", DDLDistrict, DDLSection, DDLSection2);
    }
    protected void DDLSection2_SelectedIndexChanged(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DDLCadastralChange("03", DDLDistrict, DDLSection, DDLSection2);
    }


    protected void GVCadastral_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "delfile001":
                int aa = Convert.ToInt32(e.CommandArgument);
                int number = Convert.ToInt32(GVCadastral.Rows[aa].Cells[0].Text);

                DataTable VS_GV1 = (DataTable)ViewState["SwcCadastral"];

                int i = 0;

                VS_GV1.Rows.RemoveAt(aa);

                ViewState["SwcCadastral"] = VS_GV1;
                GVCadastral.DataSource = VS_GV1;
                GVCadastral.DataBind();
				if(GVCadastral.Rows.Count > 0) P_Message.Visible = true;
				else P_Message.Visible = false;
                break;
        }
    }
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csCaseID2 = LBSWC002.Text + "";
        string strSQLClearFieldValue = "", strSQLClearFieldValue55="";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCCASE set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";

        string DelFieldValue55 = DelFieldValue;
        if (DelFieldValue55 == "SWC029CAD") { } else {
            DelFieldValue55 = DelFieldValue.Replace("SWC0","SWC");
        }

        strSQLClearFieldValue55 = " update SWCSWC set ";
        strSQLClearFieldValue55 = strSQLClearFieldValue55 + DelFieldValue55 + "='' ";
        strSQLClearFieldValue55 = strSQLClearFieldValue55 + " where SWC00 = '" + csCaseID + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();

            SqlCommand objCmdTsl = new SqlCommand(strSQLClearFieldValue55, TslmConn);
            objCmdTsl.ExecuteNonQuery();
            objCmdTsl.Dispose();
        }


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
            case "CAD":
            case "PDF":
            case "DOC":
			case "PDFJPG138":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                FileLink.Visible = false;
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }

    protected void BTNSWC101_fileuploaddel_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC101.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTSWC101, null, Link101, "SWC101", "TXTSWC101", 0, 0);

        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "竣工圖說";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected void BTNFILES001_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        TXTFILES001.Text = "";
        Link001.NavigateUrl = "";
        Link001.Visible = false;
    }


    protected void SWCFILES001_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "delfile002":
                int aa = Convert.ToInt32(e.CommandArgument);
                int number = Convert.ToInt32(SWCFILES001.Rows[aa].Cells[0].Text);

                DataTable VS_GV1 = (DataTable)ViewState["File001C"];

                int i = 0;

                VS_GV1.Rows.RemoveAt(aa);

                ViewState["File001C"] = VS_GV1;
                SWCFILES001.DataSource = VS_GV1;
                SWCFILES001.DataBind();

                break;
        }

    }

    protected void GVCadastral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        GVCadastral.PageIndex = e.NewPageIndex;
    }

    protected void GVCadastral_PageIndexChanged(object sender, EventArgs e)
    {
        GVCadastral.DataSource = ViewState["SwcCadastral"]; 
        GVCadastral.DataBind();
    }
	
	protected void GVCadastral_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:
                if (e.Row.Cells[6].Text == "有")
                {
                    HyperLink Hyper = new HyperLink();
                    Hyper.Text = e.Row.Cells[6].Text;
                    Hyper.Target = "_blank";
                    Hyper.NavigateUrl = "../../open/openswc.aspx?qarea=" + e.Row.Cells[1].Text + "&qsection=" + e.Row.Cells[2].Text + "&qsubsection=" + e.Row.Cells[3].Text + "&qlandno=" + e.Row.Cells[4].Text;
                    e.Row.Cells[6].Controls.Add(Hyper);
                }
                if (e.Row.Cells[7].Text == "有")
                {
                    HyperLink Hyper = new HyperLink();
                    Hyper.Text = e.Row.Cells[7].Text;
                    Hyper.Target = "_blank";
                    Hyper.NavigateUrl = "../../open/openilg.aspx?qarea=" + e.Row.Cells[1].Text + "&qsection=" + e.Row.Cells[2].Text + "&qsubsection=" + e.Row.Cells[3].Text + "&qlandno=" + e.Row.Cells[4].Text;
                    e.Row.Cells[7].Controls.Add(Hyper);
                }
                break;
        }

    }
	
    protected void ButtonDEL01_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";

        string exeSqlStr = " delete SWCDTL01 where swc000='" + tSwc000 + "' and dtla000 ='" + tDtl000 + "' ";
        
        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?CaseId=" + tSwc000 + "'; </script>");
    }

    protected void TXTSWC029CAD_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC029CAD.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("CAD", TXTSWC029CAD, null, Link029CAD, "SWC029CAD", "TXTSWC029CAD", 0, 0);

        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "6-1-CAD";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }

    protected void SWCDTL07_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock07 = (HiddenField)e.Row.Cells[2].FindControl("Lock07");
                string tempLock = Lock07.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL07");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }

    }

    protected void ButtonDEL07_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";

        string exeSqlStr = " delete SWCDTL07 where swc000='" + tSwc000 + "' and dtlG000 ='" + tDtl000 + "' ";

        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?SWCNO=" + tSwc000 + "'; </script>");

    }

    protected void TXTSWC080_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC080.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PDF", TXTSWC080, null, Link080, "SWC080", "TXTSWC080", 0, 0);
        
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "掃描檔";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected void TXTSWC029_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC029.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PDF", TXTSWC029, null, Link029, "SWC029", "TXTSWC029", 0, 0);

        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "6-1";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])

    }

    protected void TXTSWC030_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC030.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PDF", TXTSWC030, null, Link030, "SWC030", "TXTSWC030", 0, 0);

        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        //Response.Redirect(ConfigurationManager.AppSettings["thisip"] + "tslm/WEBGIS1.aspx");
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "7-1";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected string UpLoadFiles55(string tFile,string tFiletype)
    {
        string rFile = "";

        switch (tFiletype)
        {
            case "6-1":
                string FileYear = Convert.ToInt16(tFile.Substring(4, 3)).ToString();

                string targetDirectory = ConfigurationManager.AppSettings["swcpspath"]; // + FileYearS + "\水保申請案件\水保計畫";
                break;
            default:
                rFile = tFile;
                break;
        }
        return rFile;
    }

    protected void ButtonDEL06_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";

        string exeSqlStr = " delete SWCDTL06 where swc000='" + tSwc000 + "' and dtlF000 ='" + tDtl000 + "' ";

        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?CaseId=" + tSwc000 + "'; </script>");

    }

    protected void SWCDTL06_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock06 = (HiddenField)e.Row.Cells[2].FindControl("Lock06");
                string tempLock = Lock06.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL06");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }

    protected void ButtonDEL03_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";

        string exeSqlStr = " delete SWCDTL03 where swc000='" + tSwc000 + "' and dtlC000 ='" + tDtl000 + "'; delete SwcItemChk where swc000='" + tSwc000 + "' and DTLRPNO ='" + tDtl000 + "';  ";

        ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        {
            SwcConn03.Open();

            SqlDataReader readeSwc03;
            SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
            readeSwc03 = objCmdSwc03.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?CaseId=" + tSwc000 + "'; </script>");


    }

    protected void SWCDTL0302_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock03 = (HiddenField)e.Row.Cells[2].FindControl("Lock03");
                string tempLock = Lock03.Value;
                
                string GetDTLType = e.Row.Cells[0].Text.Substring(0, 2);

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL03");

                    btn.Text = "搞啥"+ GetDTLType;
                    btn.Visible = false;
                }
                if (GetDTLType == "RB")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL03");

                    btn.Text = "搞啥" + GetDTLType;
                    btn.Visible = false;
                }
                break;
        }

    }
    public void SWCDOCFilesyn(string sourcePC, string sourceURL, string sourcePATH, string targetPC, string sourceFILENAME, string caseid, string swctype, string doctype)
    {
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        //tcge 55 的範例      "D:\公用區\唯讀區\101年掃描圖檔\水保申請案件\簡易水保\UA1610102002-1\審查\6-1\UA1610102002-1_6-1.pdf"
        //tcge 55 的範例      "http://172.28.100.55/ILGFILE/101年掃描圖檔/水保申請案件/簡易水保/UA1610102002-1/審查/6-1/UA1610102002-1_6-1.pdf"
        //swcdoc 8 的範例     "http://tcgeswc.taipei.gov.tw/SWCDOC/UpLoadFiles/SwcCaseFile/SWC199901010345/UA1610102002-1_6-1.pdf"
        //swcdoc 8 的範例     "D:\Web\SWCWeb\UpLoadFiles\SwcCaseFile\SWC199901010345\UA1610102002-1_6-1.pdf"  (程式在"http://tcgeswc.taipei.gov.tw/SWCDOC/SWCDOC/SWC002.aspx")
        string targetURL = "";
        string targetPATH = "";
        DateTime requestTIME = DateTime.Now;
        string swcdos8caseid = "";

        swctype = swctype.Replace("水土保持計畫", "水保計畫");

        //先判斷目的地的URL跟PATH
        switch (targetPC)
        {
            case "tcge":
                int FileYear = Convert.ToInt16(caseid.Substring(4, 3));
                string FileYearS = FileYear.ToString();
                if ((FileYear > 93))
                {
                    FileYearS = FileYearS + "年掃描圖檔";
                }
                else
                {
                    FileYearS = "93年度暨以前掃描圖檔";
                }
                targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/審查/" + doctype + "/" + sourceFILENAME;
                targetPATH = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\審查\\" + doctype + "\\" + sourceFILENAME;

                switch (doctype)
                {
                    case "竣工圖說CAD":
                    case "竣工圖說":
                        targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/竣工圖說/" + doctype + "/" + sourceFILENAME;
                        targetPATH = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\竣工圖說\\" + doctype + "\\" + sourceFILENAME;
                        break;
                    case "掃描檔":
                        targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/掃描檔/掃描檔/" + sourceFILENAME;
                        targetPATH = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\掃描檔\\掃描檔\\" + sourceFILENAME;
                        break;
                    case "審查單位查核表":
                        targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/審查單位查核表/審查單位查核表/" + sourceFILENAME;
                        targetPATH = "E:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\審查單位查核表\\審查單位查核表\\" + sourceFILENAME;
                        break;
                }

                break;
            case "swcdoc":
                //取得caseid的流水號
                SqlConnection conns = new SqlConnection();
                SqlCommand sqlcoms = new SqlCommand();
                SqlDataReader dr = default(SqlDataReader);
                conns.ConnectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"].ConnectionString;
                conns.Open();
                sqlcoms.Connection = conns;
                sqlcoms.CommandText = "SELECT SWC00 FROM SWCSWC WHERE SWC02 ='" + caseid + "'";
                dr = sqlcoms.ExecuteReader();
                while ((dr.Read()))
                {
                    swcdos8caseid = dr["SWC00"].ToString().Trim(); 
                }
                dr.Close();
                dr.Dispose();
                sqlcoms.Dispose();
                conns.Close();
                if (string.IsNullOrEmpty(swcdos8caseid))
                {
                    //MB("請確認" + caseid + "的流水號，上傳檔案並未同步，建議先存檔以後再重新上傳一次");
                    return;
                }
                targetURL = ConfigurationManager.AppSettings["thisDM"] + "UpLoadFiles/SwcCaseFile/" + swcdos8caseid + "/" + sourceFILENAME;
                targetPATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + swcdos8caseid + "\\" + sourceFILENAME;
                break;
        }

        //開始存吧
        //啟用ssl，必需存domain name, IP會壞掉
        sourceURL = sourceURL.Replace(ConfigurationManager.AppSettings["thisip"], ConfigurationManager.AppSettings["thisDM"]);

        SqlConnection conn = new SqlConnection();
        SqlCommand sqlcom = new SqlCommand();
        conn.ConnectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"].ConnectionString;
        conn.Open();
        sqlcom.Connection = conn;
        sqlcom.CommandText = "";
        sqlcom.CommandText = sqlcom.CommandText + "INSERT INTO [UploadFileSyn] ";
        sqlcom.CommandText = sqlcom.CommandText + "([sourcePC],[SOURCEURL],[SOURCEPATH],[TARGETPC],[TARGETURL],[TARGETPATH],[FILENAME],[REQUESTTIME],[PROCESSTIME],[HAVEPROCESS]) ";
        sqlcom.CommandText = sqlcom.CommandText + "VALUES ";
        sqlcom.CommandText = sqlcom.CommandText + "('" + sourcePC + "','" + sourceURL + "','" + sourcePATH + "','" + targetPC + "','" + targetURL + "','" + targetPATH + "','" + sourceFILENAME + "','" + requestTIME.ToString("yyyy-MM-dd HH:mm:ss.000") + "','1911-01-01 00:00:00.000',0)";
        sqlcom.ExecuteNonQuery();
        sqlcom.Dispose();
        conn.Close();
    }
    protected void SWCDTL01_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock01 = (HiddenField)e.Row.Cells[2].FindControl("Lock01");
                string tempLock = Lock01.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL01");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }
    protected void SWCDTL04_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock04 = (HiddenField)e.Row.Cells[2].FindControl("Lock04");
                string tempLock = Lock04.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL04");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }
    }    
    protected void ButtonDEL04_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";

        string exeSqlStr = " delete SWCDTL04 where swc000='" + tSwc000 + "' and dtlD000 ='" + tDtl000 + "' ";

        ConnectionStringSettings connectionString04 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn04 = new SqlConnection(connectionString04.ConnectionString))
        {
            SwcConn04.Open();

            SqlDataReader readeSwc04;
            SqlCommand objCmdSwc04 = new SqlCommand(exeSqlStr, SwcConn04);
            readeSwc04 = objCmdSwc04.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?CaseId=" + tSwc000 + "'; </script>");

    }

    protected void SWCDTL05_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                HiddenField Lock05 = (HiddenField)e.Row.Cells[2].FindControl("Lock05");
                string tempLock = Lock05.Value;

                if (tempLock == "Y")
                {
                    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL05");

                    btn.Text = "搞啥";
                    btn.Visible = false;
                }
                break;
        }

    }

    protected void ButtonDEL05_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        Button LButton = (Button)sender;

        string tSwc000 = LBSWC000.Text + "";
        string tDtl000 = LButton.CommandArgument + "";

        string exeSqlStr = " delete SWCDTL05 where swc000='" + tSwc000 + "' and dtlE000 ='" + tDtl000 + "' ";
        exeSqlStr += " delete SwcItemChk where swc000='" + tSwc000 + "' and DTLRPNO ='" + tDtl000 + "' ";

        ConnectionStringSettings connectionString05 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn05 = new SqlConnection(connectionString05.ConnectionString))
        {
            SwcConn05.Open();

            SqlDataReader readeSwc05;
            SqlCommand objCmdSwc05 = new SqlCommand(exeSqlStr, SwcConn05);
            readeSwc05 = objCmdSwc05.ExecuteReader();
        }
        Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?CaseId=" + tSwc000 + "'; </script>");

    }

    protected void DT003_Click(object sender, EventArgs e)
    {
        //DT003.NavigateUrl = "SWCDT003.aspx?SWCNO=" + rSWCNO + "&DTLNO=AddNew";

        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        string LINK = "SWCDT003.aspx?SWCNO=" + rCaseId + "&DTLNO=AddNew";
        Response.Redirect(LINK);
    }
    protected void DT004_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        string LINK = "SWCDT004.aspx?SWCNO=" + rCaseId + "&DTLNO=AddNew";
        Response.Redirect(LINK);

    }

    protected void DT005_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        string LINK = "SWCDT005.aspx?SWCNO=" + rCaseId + "&DTLNO=AddNew";
        Response.Redirect(LINK);

    }

    protected void DT006_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        string LINK = "SWCDT006.aspx?SWCNO=" + rCaseId + "&DTLNO=AddNew";
        Response.Redirect(LINK);
    }

    protected void DT007_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        string LINK = "SWCDT007.aspx?SWCNO=" + rCaseId + "&DTLNO=AddNew";
        Response.Redirect(LINK);
    }

    protected void DT001_Click(object sender, EventArgs e)
    {
        SaveCase_Click(sender, e);

        string rCaseId = Request.QueryString["CaseId"] + "";

        string LINK = "SWCDT001.aspx?SWCNO=" + rCaseId + "&DTLNO=AddNew";
        Response.Redirect(LINK);

    }

    protected void TXTSWC110_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC110.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC110_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDF", TXTSWC110_fileupload, TXTSWC110, "TXTSWC110", "_" + rDTLNO + "_pdf110", null, Link110, 50); //50MB

        string sourceFILENAME = TXTSWC110.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "審查單位查核表";

        if (sourceFILENAME !="") {
        SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype); }
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected void TXTSWC110_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC110.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PDF", TXTSWC110, null, Link110, "SWC110", "TXTSWC110", 0, 0);

        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\","\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "審查單位查核表";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected void TXTSWC101CAD_fileupload_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC101CAD.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC101CAD_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("竣工圖說CAD", TXTSWC101CAD_fileupload, TXTSWC101CAD, "TXTSWC101CAD", "_" + rDTLNO + "_CAD", null, Link101CAD, 50); //50MB

        string sourceFILENAME = TXTSWC101CAD.Text;
        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "竣工圖說CAD";
        
        if (sourceFILENAME != "") {
            SWCDOCFilesyn("swcdoc", sourceURL, sourcePATH, "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        }
        
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])
        
    }

    protected void BTNSWC101CAD_fileuploaddel_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC101CAD.Text;
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("CAD", TXTSWC101CAD, null, Link101CAD, "SWC101CAD", "TXTSWC101CAD", 0, 0);

        string sourceURL = ConfigurationManager.AppSettings["thisip"] + "UpLoadFiles/SwcCaseFile/" + rDTLNO + "/" + sourceFILENAME;
        string sourcePATH = ConfigurationManager.AppSettings["SwcFilePath"].Replace("\\", "\\\\") + rDTLNO + "\\" + sourceFILENAME;
        string swctype = DDLSWC007.Text;
        string doctype = "竣工圖說CAD";

        SWCDOCFilesyn("swcdoc", "", "", "tcge", sourceFILENAME, LBSWC002.Text, swctype, doctype);
        //SWCDOCFilesyn([來源網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1完整URL], [6-1，7-1完整實體路徑], [目的地網站的URL的最後一個資夾名子(全小寫)], [6-1，7-1的純檔名與副檔名部分],[案件編號UA],[書件類型(簡易水保，水土保持計畫)],[上傳的是哪一個(6-1，7-1，......)])


    }

    protected void DDLSIC01_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSIC01 = DDLSIC01.SelectedValue;
		bool cSDI019 = CHKSDI019.Checked;

        if (strSIC01 == "") { strSIC01 = "NaN"; }

        DDLSIC02.Items.Clear();
        DDLSIC02.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));
        DDLSIC05.Items.Clear();
        DDLSIC05.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));
        //TXTSDI006.Visible = true;
        //TXTSDI006.Enabled = false;
        //TXTSDI006D.Visible = false;
		if(cSDI019){
			TXTSDI006.Visible = true;
        	TXTSDI006.Enabled = false;
			//TEST
			//AA.Visible = true;
			//TXTSDI006_1.Visible = true;
        	//TXTSDI006_1.Enabled = false;
			AA.Visible = false;
			TXTSDI006_1.Visible = false;
        	TXTSDI006_1.Enabled = false;
			
			TXTSDI006D.Visible = false;
			TXTSDI006D.Enabled = false;
		}else{
			TXTSDI006.Visible = true;
        	TXTSDI006.Enabled = false;
			AA.Visible = false;
			TXTSDI006_1.Visible = false;
        	TXTSDI006_1.Enabled = false;
			TXTSDI006D.Visible = false;
			TXTSDI006D.Enabled = false;
		}
        LBSDI007.Visible = true;
        LBSDI007.Text = "";

        TXTSDI002.Enabled = false;
        TXTSDI012.Enabled = false;
        TXTSDI012_1.Enabled = false;
        TXTSDI013.Enabled = false;
        TXTSDI013_1.Enabled = false;
        TXTSDI014.Enabled = false;
        TXTSDI014_1.Enabled = false;
        LBSDI010.Text = "";
        LBSDI015.Text = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            //檢查項目
            string strSQLCase = " select * from [SwcItemChkRule] where [level] = 2 AND [SICR001] LIKE '" + strSIC01 + "%' order by SICRSORT ";

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLCase, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string tSICID = readerData["SICR001"] + "";
                string tSICDESC = readerData["SICR003"] + "";

                DDLSIC02.Items.Add(new System.Web.UI.WebControls.ListItem(tSICDESC, tSICID));
            }
            readerData.Close();
            objCmdRV.Dispose();

        }

    }

    protected void DDLSIC02_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSIC01 = DDLSIC01.SelectedValue;
        string strSIC02 = DDLSIC02.SelectedValue;
        string strSIC02Text = DDLSIC02.SelectedItem.Text;
		bool cSDI019 = CHKSDI019.Checked;

        DDLSIC05.Items.Clear();
        DDLSIC05.Items.Add(new System.Web.UI.WebControls.ListItem("", ""));

        TXTSDI002.Enabled = false;
        TXTSDI012.Enabled = false;
        TXTSDI012_1.Enabled = false;
        TXTSDI013.Enabled = false;
        TXTSDI013_1.Enabled = false;
        TXTSDI014.Enabled = false;
        TXTSDI014_1.Enabled = false;
		LBSDI007.Text = "";
        LBSDI010.Text = "";
        LBSDI015.Text = "";

        if (strSIC02Text == "其他")
        {
			CHKSDI019.Enabled = false;
			CHKSDI019.Checked = false;
            TXTSIC05.Visible = true;
            DDLSIC05.Visible = false;
            TXTSDI006.Enabled = false;
            TXTSDI006.Visible = false;
			AA.Visible = false;
			TXTSDI006_1.Enabled = false;
            TXTSDI006_1.Visible = false;
            LBSDI007.Visible = false;
			TXTSDI006D.Enabled = true;
            TXTSDI006D.Visible = true;
            TXTSDI012D.Visible = true; ThreeArea.Visible = false;
        }
		else if(strSIC02Text != "")
        {
			CHKSDI019.Enabled = true;
            TXTSIC05.Visible = false;
            DDLSIC05.Visible = true;
			
            //TXTSDI006.Visible = true;
            //TXTSDI006.Enabled = true;
            //TXTSDI006D.Visible = false;
            if(cSDI019){
				TXTSDI006.Visible = true;
				TXTSDI006.Enabled = true;
				//TEST
				//AA.Visible = true;
				//TXTSDI006_1.Visible = true;
				//TXTSDI006_1.Enabled = true;
				
				AA.Visible = false;
				TXTSDI006_1.Visible = false;
				TXTSDI006_1.Enabled = false;
				
				TXTSDI006D.Visible = false;
				TXTSDI006D.Enabled = false;
			}else{
				TXTSDI006.Visible = true;
				TXTSDI006.Enabled = true;
				AA.Visible = false;
				TXTSDI006_1.Visible = false;
				TXTSDI006_1.Enabled = false;
				TXTSDI006D.Visible = false;
				TXTSDI006D.Enabled = false;
			}
            LBSDI007.Visible = true;
            TXTSDI012D.Visible = false; ThreeArea.Visible = true;
		}else 
        {
			CHKSDI019.Enabled = true;
			TXTSIC05.Visible = false;
            DDLSIC05.Visible = true;
        }

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            //檢查項目
            string strSQLCase = " select * from [SwcItemChkRule] where [level] = 3 AND [SICR001] LIKE '" + strSIC02 + "%' order by SICRSORT ";

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLCase, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string tSICID = readerData["SICR001"] + "";
                string tSICDESC = readerData["SICR005"] + "";
                string tUNIT = readerData["SICR004"] + "";

                DDLSIC05.Items.Add(new System.Web.UI.WebControls.ListItem(tSICDESC, tSICID));
                LBSDI007.Text = tUNIT;
            }
            readerData.Close();
            objCmdRV.Dispose();
        }
    }

    protected void DDLSIC05_SelectedIndexChanged(object sender, EventArgs e)
    {
        string strSIC05 = DDLSIC05.SelectedValue;
		LBSDI010.Text = "";
		TXTSDI012.Enabled = false;
		TXTSDI012_1.Enabled = false;
		TXTSDI013.Enabled = false;
		TXTSDI013_1.Enabled = false;
		TXTSDI014.Enabled = false;
		TXTSDI014_1.Enabled = false;
        TXTSDI012.Text = "";
        TXTSDI012_1.Text = "";
        TXTSDI013.Text = "";
        TXTSDI013_1.Text = "";
        TXTSDI014.Text = "";
        TXTSDI014_1.Text = "";

        bool cSDI019 = CHKSDI019.Checked;
		
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            //檢查項目
            string strSQLCase = " select * from [SwcItemChkRule] where [SICR001] = '" + strSIC05 + "' ";

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLCase, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string tSICID = readerData["SICR001"] + "";
                string tSICDESC = readerData["SICR005"] + "";
                string tSICR007 = readerData["SICR007"] + "";

                string tSDI009 = readerData["SICR009"] + "";
                string tSDI010 = readerData["SICR006"] + "";
                string tSDI011 = readerData["SICR007"] + "";
                string tSDI015 = readerData["SICR008"] + "";
                string tSDI016 = readerData["SICR010"] + "";

                TXTSDI002.Text = strSIC05;
                LBSDI010.Text = tSDI010;
                LBSDI015.Text = tSDI015;

                TXTSDI009.Text = tSDI009;
                TXTSDI016.Text = tSDI016;
                TXTSDI011.Text = tSDI011;

                switch (tSICR007)
                {
                    case "1":
                        TXTSDI012.Enabled = true;
                        TXTSDI013.Enabled = false;
                        TXTSDI014.Enabled = false;
                        if (cSDI019) TXTSDI012_1.Enabled = true;
                        else TXTSDI012_1.Enabled = false;
                        TXTSDI013_1.Enabled = false;
                        TXTSDI014_1.Enabled = false;
                        break;
                    case "2":
                        TXTSDI012.Enabled = true;
                        TXTSDI013.Enabled = true;
                        TXTSDI014.Enabled = false;
                        if (cSDI019) { TXTSDI012_1.Enabled = true; TXTSDI013_1.Enabled = true; }
                        else { TXTSDI012_1.Enabled = false; TXTSDI013_1.Enabled = false; }
                        TXTSDI014_1.Enabled = false;
                        break;
                    case "3":
                        TXTSDI012.Enabled = true;
                        TXTSDI013.Enabled = true;
                        TXTSDI014.Enabled = true;
                        if (cSDI019) { TXTSDI012_1.Enabled = true; TXTSDI013_1.Enabled = true; TXTSDI014_1.Enabled = true; }
                        else { TXTSDI012_1.Enabled = false; TXTSDI013_1.Enabled = false; TXTSDI014_1.Enabled = false; }
                        break;
                }
            }
            readerData.Close();
            objCmdRV.Dispose();
        }

    }

    protected void BTNADDSDI_Click(object sender, EventArgs e)
    {
		try
        {
			string vSDI002 = DDLSIC01.SelectedItem.Value;
			string vSDI003 = DDLSIC01.SelectedItem.Text;
			string vSDI004 = TXTSDI004.Text;
			string vSDI005 = DDLSIC02.SelectedItem.Text;
			string vSDI006 = TXTSDI006.Text;
			string vSDI006_1 = TXTSDI006_1.Text;
			string vSDI006D = TXTSDI006D.Text;
			string vSDI007 = LBSDI007.Text;
			string vSDI008 = DDLSIC05.SelectedItem.Text;
			string vSDI009 = TXTSDI009.Text;
			string vSDI010 = LBSDI010.Text;
			string vSDI011 = TXTSDI011.Text; if (vSDI011.Trim() == "") vSDI011 = "0";
			string vSDI012 = TXTSDI012.Text;//if (vSDI012.Trim() == "") vSDI012 = "0";
			string vSDI012D = TXTSDI012D.Text;
			string vSDI012_1 = TXTSDI012_1.Text; //if (vSDI012_1.Trim() == "") vSDI012_1 = "0";
			string vSDI013 = TXTSDI013.Text; //if (vSDI013.Trim() == "") vSDI013 = "0";
			string vSDI013_1 = TXTSDI013_1.Text; //if (vSDI013_1.Trim() == "") vSDI013_1 = "0";
			string vSDI014 = TXTSDI014.Text; //if (vSDI014.Trim() == "") vSDI014 = "0";
			string vSDI014_1 = TXTSDI014_1.Text; //if (vSDI014_1.Trim() == "") vSDI014_1 = "0";
			string vSDI015 = LBSDI015.Text;
			string vSDI016 = TXTSDI016.Text;
			string vSDI019; if (CHKSDI019.Checked) vSDI019 = "是"; else vSDI019 = "否";
			
			//if(vSDI019 == "是"){
			//	vSDI012D = vSDI012 + "~" + vSDI012_1;
			//}
	
			if(vSDI005.Trim() != "其他"){
				if (vSDI012 != "") vSDI012 = Convert.ToDouble(vSDI012).ToString("#0.00");
				if (vSDI012_1 != "") vSDI012_1 = Convert.ToDouble(vSDI012_1).ToString("#0.00");
				if (vSDI013 != "") vSDI013 = Convert.ToDouble(vSDI013).ToString("#0.00");
				if (vSDI013_1 != "") vSDI013_1 = Convert.ToDouble(vSDI013_1).ToString("#0.00");
				if (vSDI014 != "") vSDI014 = Convert.ToDouble(vSDI014).ToString("#0.00");
				if (vSDI014_1 != "") vSDI014_1 = Convert.ToDouble(vSDI014_1).ToString("#0.00");
			}
	
			if (vSDI003.Trim() == "") { Response.Write("<script>alert('請選擇水土保持設施類別');</script>"); return; }
			if (vSDI004.Trim() == "") { Response.Write("<script>alert('請輸入設施名稱（位置或編號）');</script>"); return; }
			if (vSDI005.Trim() == "") { Response.Write("<script>alert('請選擇設施型式');</script>"); return; }
			//if (vSDI005.Trim() == "其他") { if (vSDI006D == "") { Response.Write("<script>alert('請輸入原核定計畫之數量');</script>"); return; } } else { if (vSDI006 == "") { Response.Write("<script>alert('請輸入原核定計畫之數量');</script>"); return; } else { vSDI006D = vSDI006 + vSDI007; } }
			if (vSDI019 == "是"){
				if (vSDI006 == "" && vSDI006_1 == "") { 
					Response.Write("<script>alert('請輸入原核定計畫之數量');</script>"); 
					return; 
				}
				else{
					//TEST
					if(vSDI006 == ""){
						Response.Write("<script>alert('請輸入原核定計畫之數量');</script>"); 
						return;
					}
					//if(Convert.ToDouble(vSDI006)>Convert.ToDouble(vSDI006_1)){
						//TXTSDI006.Text = vSDI006_1;
						//TXTSDI006_1.Text = vSDI006;
					vSDI006=TXTSDI006.Text;
						//vSDI006_1=TXTSDI006_1.Text;
					//}
					vSDI006D = vSDI006 + vSDI007;
				}
			}
			else if (vSDI005.Trim() == "其他") { 
				if (vSDI006D == "") { 
					Response.Write("<script>alert('請輸入原核定計畫之數量');</script>"); 
					return; 
				}
				else{
					vSDI006D = vSDI006D;
				}
			} 
			else { 
				if (vSDI006 == "") { 
					Response.Write("<script>alert('請輸入原核定計畫之數量');</script>");
					return; 
				} 
				else { 
					vSDI006D = vSDI006 + vSDI007; 
				} 
			}
			//if (vSDI005.Trim() == "其他") { vSDI008=TXTSIC05.Text; if(vSDI008=="") { Response.Write("<script>alert('請選擇檢核項目');</script>"); return; } } else { if (vSDI008.Trim() == "") { Response.Write("<script>alert('請選擇檢核項目');</script>"); return; } }
			if (vSDI005.Trim() == "其他" || vSDI019 == "是") {
				if(vSDI005.Trim() == "其他"){
					vSDI008=TXTSIC05.Text; 
					if(vSDI008=="") {
						Response.Write("<script>alert('請選擇檢核項目');</script>"); 
						return; 
					} 
				}
				else{
					//vSDI008=vSDI008.Text; 
					if(vSDI008=="") {
						Response.Write("<script>alert('請選擇檢核項目');</script>"); 
						return; 
					} 
				}
			} else { 
				if (vSDI008.Trim() == "") {
					Response.Write("<script>alert('請選擇檢核項目');</script>");
					return; 
				}
			}
			string vSDI012_t = vSDI012;
			string vSDI012_t1 = vSDI012_1;
			string vSDI013_t = vSDI013;
			string vSDI013_t1 = vSDI013_1;
			string vSDI014_t = vSDI014;
			string vSDI014_t1 = vSDI014_1;
			//if (vSDI005.Trim() == "其他") { if (vSDI012D == "") { Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); return; } } else { if (vSDI012 == "" && Convert.ToInt32(vSDI011) > 0) { Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); return; } else { if(vSDI012.Trim() == "") { } else{ vSDI012D = vSDI010 + vSDI012; } } if (vSDI013.Trim() == "" && Convert.ToInt32(vSDI011) > 1) { Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); return; } else { if (vSDI013.Trim() == "") { } else { vSDI012D += "×" + vSDI013; } } if (vSDI014.Trim() == "" && Convert.ToInt32(vSDI011) > 2) { Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); return; } { if (vSDI014.Trim() == "") { }else { vSDI012D += "×" + vSDI014; } } vSDI012D += " " + vSDI015; }
			if (vSDI005.Trim() == "其他") { 
				if (vSDI012D == "") {
					Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); 
					return; 
				} 
			} 
			else { 
				if (vSDI012 == "" && Convert.ToInt32(vSDI011) > 0) { 
					Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); 
					return; 
				} 
				else { 
					if(vSDI012.Trim() != "") {
						if (vSDI019 == "是")
							if(Convert.ToDouble(vSDI012_1)>=Convert.ToDouble(vSDI012))
								vSDI012D = vSDI010 + vSDI012 + "~" + vSDI012_1;
							else{
								vSDI012D = vSDI010 + vSDI012_1 + "~" + vSDI012;
								vSDI012_1 = vSDI012_t;
								vSDI012 = vSDI012_t1;
							}
						else
							vSDI012D = vSDI010 + vSDI012; 
					} 
				} 
				if (vSDI013.Trim() == "" && Convert.ToInt32(vSDI011) > 1) { 
					Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); 
					return; 
				} 
				else {
					if (vSDI013.Trim() != "") {
						if (vSDI019 == "是")
							if(Convert.ToDouble(vSDI013_1)>=Convert.ToDouble(vSDI013))
								vSDI012D += "×" + vSDI013 + "~" + vSDI013_1;
							else{
								vSDI012D += "×" + vSDI013_1 + "~" + vSDI013;
								vSDI013_1 = vSDI013_t;
								vSDI013 = vSDI013_t1;
							}
						else
							vSDI012D += "×" + vSDI013;
					} 
				} 
				if (vSDI014.Trim() == "" && Convert.ToInt32(vSDI011) > 2) {
					Response.Write("<script>alert('請輸入原核定計畫之尺寸');</script>"); 
					return; 
				}
				else{ 
					if (vSDI014.Trim() != "") {
						if (vSDI019 == "是")
							if(Convert.ToDouble(vSDI014_1)>=Convert.ToDouble(vSDI014))
								vSDI012D += "×" + vSDI014 + "~" + vSDI014_1;
							else{
								vSDI012D += "×" + vSDI014_1 + "~" + vSDI014;
								vSDI014_1 = vSDI014_t;
								vSDI014 = vSDI014_t1;
							}
						else
							vSDI012D += "×" + vSDI014;
					} 
				} 
				vSDI012D += " " + vSDI015; 
			}
			
			TXTSDINI.Text = (Convert.ToInt32(TXTSDINI.Text) + 1).ToString();
	
			DataTable tbSDIVS = (DataTable)ViewState["SwcDocItem"];
	
			if (tbSDIVS == null)
			{
				DataTable SDITB = new DataTable();
	
				SDITB.Columns.Add(new DataColumn("SDIFDNI", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD001", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD002", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD003", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD004", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD005", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD006", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD006_1", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD006D", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD007", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD008", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD009", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD010", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD011", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD012", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD012_1", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD012D", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD013_1", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD014_1", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));
				SDITB.Columns.Add(new DataColumn("SDIFD019", typeof(string)));
	
				ViewState["SwcDocItem"] = SDITB;
				tbSDIVS = (DataTable)ViewState["SwcDocItem"];
			}
	
			DataRow SDITBRow = tbSDIVS.NewRow();
	
			SDITBRow["SDIFDNI"] = TXTSDINI.Text;
			SDITBRow["SDIFD001"] = "NEW";
			SDITBRow["SDIFD002"] = vSDI002;
			SDITBRow["SDIFD003"] = vSDI003;
			SDITBRow["SDIFD004"] = vSDI004;
			SDITBRow["SDIFD005"] = vSDI005;
			SDITBRow["SDIFD006"] = vSDI006;
			SDITBRow["SDIFD006_1"] = vSDI006_1;
			SDITBRow["SDIFD006D"] = vSDI006D;
			SDITBRow["SDIFD007"] = vSDI007;
			SDITBRow["SDIFD008"] = vSDI008;
			SDITBRow["SDIFD009"] = vSDI009;
			SDITBRow["SDIFD010"] = vSDI010;
			SDITBRow["SDIFD011"] = vSDI011;
			SDITBRow["SDIFD012"] = vSDI012;
			SDITBRow["SDIFD012_1"] = vSDI012_1;
			SDITBRow["SDIFD012D"] = vSDI012D;
			SDITBRow["SDIFD013"] = vSDI013;
			SDITBRow["SDIFD013_1"] = vSDI013_1;
			SDITBRow["SDIFD014"] = vSDI014;
			SDITBRow["SDIFD014_1"] = vSDI014_1;
			SDITBRow["SDIFD015"] = vSDI015;
			SDITBRow["SDIFD016"] = vSDI016;
			SDITBRow["SDIFD019"] = vSDI019;
	
			tbSDIVS.Rows.Add(SDITBRow);
	
			ViewState["SwcDocItem"] = tbSDIVS;
	
			SDIList.DataSource = tbSDIVS;
			SDIList.DataBind();
	
			//清除部份資料
			DDLSIC05.Text = "";
			TXTSDI012.Text = "";
			TXTSDI012_1.Text = "";
			TXTSDI013.Text = "";
			TXTSDI013_1.Text = "";
			TXTSDI014.Text = "";
			TXTSDI014_1.Text = "";
			LBSDI015.Text = "";
			TXTSDI012.Enabled = false;
			TXTSDI012_1.Enabled = false;
			TXTSDI013.Enabled = false;
			TXTSDI013_1.Enabled = false;
			TXTSDI014.Enabled = false;
			TXTSDI014_1.Enabled = false;
		}
        catch (Exception ex)
        {
            Response.Write("<script>alert('資料不全');</script>");
        }
    }

    protected void SDIList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ssDELGVSDI = Session["DELGVSDI"] + ";;";
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "SDIDEL":
                int aa = Convert.ToInt32(e.CommandArgument);

                HiddenField hiddenDate01 = (HiddenField)SDIList.Rows[aa].Cells[6].FindControl("HDSDI001");
                string sHDSDI001 = hiddenDate01.Value;
                
                DataTable DELGVTB = (DataTable)ViewState["SwcDocItem"];

                DELGVTB.Rows.RemoveAt(aa);

                ViewState["SwcDocItem"] = DELGVTB;
                SDIList.DataSource = DELGVTB;
                SDIList.DataBind();
                
                if (sHDSDI001 == "NEW") { } else { ssDELGVSDI += sHDSDI001; }

                Session["DELGVSDI"] = ssDELGVSDI;
                break;
        }

    }
    private void SaveGridViewData(string v)
    {
        string pageAction = Request.QueryString["CaseId"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = v;
        string sSWC002 = LBSWC002.Text+"";
        string exeSQLStr = "";
        LBSWC000.Text = sSWC000;

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];

        #region-計畫申請書存檔
        
        DataTable dtSWCFile = new DataTable();

        string tFILE001 = "001";    //計畫申請書

        exeSQLStr = " delete SWCFILES Where SWC000 = '" + v + "' and FILE001 = '" + tFILE001 + "'; ";

        dtSWCFile = (DataTable)ViewState["File001C"];

        if (dtSWCFile != null)
        {
            int i = 0;

            for (i = 0; i <= (Convert.ToInt32(dtSWCFile.Rows.Count) - 1); i++)
            {
                string tFILE000 = (i + 1).ToString();
                string tFILE003 = dtSWCFile.Rows[i]["File001003"].ToString().Trim();

                exeSQLStr += " insert into SWCFILES (SWC000,FILE000,FILE001,FILE003,Saveuser,savedate) VALUES ";
                exeSQLStr += " ('" + v + "','" + tFILE000 + "','" + tFILE001 + "',N'" + tFILE003 + "','" + ssUserID + "',getdate()); ";
            }

            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();
                SqlCommand objCmdItem2 = new SqlCommand(exeSQLStr, SWCConn);
                objCmdItem2.ExecuteNonQuery();

                objCmdItem2.Cancel();
                objCmdItem2.Dispose();
            }
            Files001No.Text = i.ToString();
        }
        #endregion

        #region-分段驗收
        DataTable dtSDI = new DataTable();
        dtSDI = (DataTable)ViewState["SwcDocItem"];

        if (dtSDI == null) { } else {
            int i = 0;

            for (i = 0; i <= (Convert.ToInt32(dtSDI.Rows.Count) - 1); i++)
            {
                #region
                string tSDI001 = dtSDI.Rows[i]["SDIFD001"].ToString().Trim();
                string sSDI002 = dtSDI.Rows[i]["SDIFD002"].ToString().Trim();
                string sSDI003 = dtSDI.Rows[i]["SDIFD003"].ToString().Trim();
                string sSDI004 = dtSDI.Rows[i]["SDIFD004"].ToString().Trim();
                string sSDI005 = dtSDI.Rows[i]["SDIFD005"].ToString().Trim();
                string sSDI006 = dtSDI.Rows[i]["SDIFD006"].ToString().Trim(); 
                string sSDI006_1 = dtSDI.Rows[i]["SDIFD006_1"].ToString().Trim(); 
                string sSDI006D = dtSDI.Rows[i]["SDIFD006D"].ToString().Trim(); 
                string sSDI007 = dtSDI.Rows[i]["SDIFD007"].ToString().Trim();
                string sSDI008 = dtSDI.Rows[i]["SDIFD008"].ToString().Trim();
                string sSDI009 = dtSDI.Rows[i]["SDIFD009"].ToString().Trim();
                string sSDI010 = dtSDI.Rows[i]["SDIFD010"].ToString().Trim();
                string sSDI011 = dtSDI.Rows[i]["SDIFD011"].ToString().Trim();
                string sSDI012 = dtSDI.Rows[i]["SDIFD012"].ToString().Trim();
                string sSDI012_1 = dtSDI.Rows[i]["SDIFD012_1"].ToString().Trim();
                string sSDI012D = dtSDI.Rows[i]["SDIFD012D"].ToString().Trim();
                string sSDI013 = dtSDI.Rows[i]["SDIFD013"].ToString().Trim();
                string sSDI013_1 = dtSDI.Rows[i]["SDIFD013_1"].ToString().Trim();
                string sSDI014 = dtSDI.Rows[i]["SDIFD014"].ToString().Trim();
                string sSDI014_1 = dtSDI.Rows[i]["SDIFD014_1"].ToString().Trim();
                string sSDI015 = dtSDI.Rows[i]["SDIFD015"].ToString().Trim();
                string sSDI016 = dtSDI.Rows[i]["SDIFD016"].ToString().Trim();
                string sSDI019 = dtSDI.Rows[i]["SDIFD019"].ToString().Trim();
                #endregion

                if (tSDI001=="NEW" || pageAction=="COPY") {
                    exeSQLStr = " insert into SwcDocItem (SWC000,SWC002,SDI001,SDI002,SDI003,SDI004,SDI005,SDI006,SDI006_1,SDI006D,SDI007,SDI008,SDI009,SDI010,SDI011,SDI012,SDI012_1,SDI012D,SDI013,SDI013_1,SDI014,SDI014_1,SDI015,SDI016,SDI019,SaveUser,SaveDate) VALUES (@SWC000,@SWC002,@SDI001,@SDI002,@SDI003,@SDI004,@SDI005,@SDI006,@SDI006_1,@SDI006D,@SDI007,@SDI008,@SDI009,@SDI010,@SDI011,@SDI012,@SDI012_1,@SDI012D,@SDI013,@SDI013_1,@SDI014,@SDI014_1,@SDI015,@SDI016,@SDI019,@SaveUser,getdate()); ";

                    using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SWCConn.Open();

                        using (var cmd = SWCConn.CreateCommand())
                        {
                            cmd.CommandText = exeSQLStr;
                            #region-設定值
                            cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                            cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                            cmd.Parameters.Add(new SqlParameter("@SDI001", GenSwcDocItemID()));
                            cmd.Parameters.Add(new SqlParameter("@SDI002", sSDI002));
                            cmd.Parameters.Add(new SqlParameter("@SDI003", sSDI003));
                            cmd.Parameters.Add(new SqlParameter("@SDI004", sSDI004));
                            cmd.Parameters.Add(new SqlParameter("@SDI005", sSDI005));
                            cmd.Parameters.Add(new SqlParameter("@SDI006", sSDI006));
                            cmd.Parameters.Add(new SqlParameter("@SDI006_1", sSDI006_1));
                            cmd.Parameters.Add(new SqlParameter("@SDI006D", sSDI006D));
                            cmd.Parameters.Add(new SqlParameter("@SDI007", sSDI007));
                            cmd.Parameters.Add(new SqlParameter("@SDI008", sSDI008));
                            cmd.Parameters.Add(new SqlParameter("@SDI009", sSDI009));
                            cmd.Parameters.Add(new SqlParameter("@SDI010", sSDI010));
                            cmd.Parameters.Add(new SqlParameter("@SDI011", sSDI011));
                            cmd.Parameters.Add(new SqlParameter("@SDI012", sSDI012));
                            cmd.Parameters.Add(new SqlParameter("@SDI012_1", sSDI012_1));
                            cmd.Parameters.Add(new SqlParameter("@SDI012D", sSDI012D));
                            cmd.Parameters.Add(new SqlParameter("@SDI013", sSDI013));
                            cmd.Parameters.Add(new SqlParameter("@SDI013_1", sSDI013_1));
                            cmd.Parameters.Add(new SqlParameter("@SDI014", sSDI014));
                            cmd.Parameters.Add(new SqlParameter("@SDI014_1", sSDI014_1));
                            cmd.Parameters.Add(new SqlParameter("@SDI015", sSDI015));
                            cmd.Parameters.Add(new SqlParameter("@SDI016", sSDI016));
                            cmd.Parameters.Add(new SqlParameter("@SDI019", sSDI019));
                            cmd.Parameters.Add(new SqlParameter("@SaveUser", ssUserID));
                            #endregion
                            cmd.ExecuteNonQuery();
                            cmd.Cancel();
                        }
                    }

                }
            }
        }

        string exeDelSqlStr = "";
        string ssDelSwcDocItem = Session["DELGVSDI"] + "";

        string[] arrayDelId = ssDelSwcDocItem.Split(new string[] { ";;" }, StringSplitOptions.None);

        for (int i = 0; i < arrayDelId.Length; i++)
        {
            string tempID = arrayDelId[i] + "";

            if (tempID.Trim() == "") { } else {
                exeDelSqlStr += " DELETE SwcDocItem WHERE SWC000='"+ sSWC000 + "' AND SDI001='"+ tempID + "' ";
            }
        }
        if (exeDelSqlStr!="")
        {
            Session["DELGVSDI"] = "";

            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                SqlCommand objCmdDel = new SqlCommand(exeDelSqlStr, SwcConn);
                objCmdDel.ExecuteNonQuery();
                objCmdDel.Dispose();
            }
        }
        #endregion
		
		#region-義務人資訊
        DataTable dtSWCP = new DataTable();
        dtSWCP = (DataTable)ViewState["SwcPeople"];

		exeSQLStr = " delete SWCObligor where SWC000=@SWC000; ";
		if(dtSWCP.Rows.Count > 0)
		{
			for (int i = 0; i <= (Convert.ToInt32(dtSWCP.Rows.Count) - 1); i++)
			{
				string rNO = (i+1).ToString().Trim();
				string rSWC013 = dtSWCP.Rows[i]["姓名"].ToString().Trim();
				string rSWC013ID = dtSWCP.Rows[i]["身分證字號"].ToString().Trim();
				string rSWC013TEL = dtSWCP.Rows[i]["手機"].ToString().Trim();
				string rSWC014Zip = dtSWCP.Rows[i]["地址ZipCode"].ToString().Trim();
				string rSWC014City = dtSWCP.Rows[i]["地址City"].ToString().Trim();
				string rSWC014District = dtSWCP.Rows[i]["地址District"].ToString().Trim();
				string rSWC014Address = dtSWCP.Rows[i]["地址Address"].ToString().Trim();
				
				if (i == 0)
					exeSQLStr += " insert into SWCObligor (SWC000,序號,SWC013,SWC013ID,SWC013TEL,SWC014Zip,SWC014City,SWC014District,SWC014Address) VALUES (@SWC000,@序號,@SWC013,@SWC013ID,@SWC013TEL,@SWC014Zip,@SWC014City,@SWC014District,@SWC014Address); ";
				else
					exeSQLStr = " insert into SWCObligor (SWC000,序號,SWC013,SWC013ID,SWC013TEL,SWC014Zip,SWC014City,SWC014District,SWC014Address) VALUES (@SWC000,@序號,@SWC013,@SWC013ID,@SWC013TEL,@SWC014Zip,@SWC014City,@SWC014District,@SWC014Address); ";
				using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
				{
					SWCConn.Open();
	
					using (var cmd = SWCConn.CreateCommand())
					{
						cmd.CommandText = exeSQLStr;
						#region-設定值
						cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
						cmd.Parameters.Add(new SqlParameter("@序號", rNO));
						cmd.Parameters.Add(new SqlParameter("@SWC013", rSWC013));
						cmd.Parameters.Add(new SqlParameter("@SWC013ID", rSWC013ID));
						cmd.Parameters.Add(new SqlParameter("@SWC013TEL", rSWC013TEL));
						cmd.Parameters.Add(new SqlParameter("@SWC014Zip", rSWC014Zip));
						cmd.Parameters.Add(new SqlParameter("@SWC014City", rSWC014City));
						cmd.Parameters.Add(new SqlParameter("@SWC014District", rSWC014District));
						cmd.Parameters.Add(new SqlParameter("@SWC014Address", rSWC014Address));
						#endregion
						cmd.ExecuteNonQuery();
						cmd.Cancel();
					}
				}
			}
		}
		else
		{
			using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
			{
				SWCConn.Open();
	
				using (var cmd = SWCConn.CreateCommand())
				{
					cmd.CommandText = exeSQLStr;
					#region-設定值
					cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
        #endregion
    }

    private void PDFWaterMark99(string vFilePath,string vNewFilePath)
    {
        string ssGuildImg = Session["WMGuild"] + "";

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
            string imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/"+ ssGuildImg + ".png"); //Logo
            //imageUrl = HttpContext.Current.Server.MapPath(@"~/images/Watermark/dPeitou13.jpg"); //Logo

            pdfimageobj = iTextSharp.text.Image.GetInstance(imageUrl);

            float xx = pdfimageobj.ScaledHeight;
            float yy = pdfimageobj.ScaledWidth;

            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imageUrl);
            //img.ScalePercent(35f);  //縮放比例
            img.SetAbsolutePosition(y-yy, 0); //設定圖片每頁的絕對位置
            PdfContentByte waterMark = Pdfstamper.GetOverContent(i);
            waterMark.AddImage(img);    //把圖片印上去 
            
        }
        Pdfstamper.Close();
        Pdfreader.Close();


    }

    private string GenSwcDocItemID() {

        string sSWC000 = LBSWC000.Text;
        string tmpV = "SDI" + sSWC000.Substring(sSWC000.Length-5);
        string rValue = tmpV + "1".PadLeft(4,'0');

        string exeSQLMaxId = " select MAX(SDI001) AS MAXID from SwcDocItem ";
        exeSQLMaxId += " Where LEFT(SDI001,"+ tmpV.Length+ ")='" + tmpV + "' ";
        exeSQLMaxId += "   and SWC000 = '"+ sSWC000 + "' ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            
            SqlDataReader readerSWC;
            SqlCommand objCmdRV = new SqlCommand(exeSQLMaxId, SWCConn);
            readerSWC = objCmdRV.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 4, 4)) + 1).ToString();
                    rValue = tmpV + tempvalue.PadLeft(4, '0');
                }

            }
            readerSWC.Close();
        }
        return rValue;
    }

    protected void Btn_UPSFile_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string btnType = ((Button)(sender)).ID;
        string rDTLNO = LBSWC000.Text + "";
        bool rUP = false;
        string tSFType = "",tSFName = "", filename = "", extension="";
        string ssUserNAME = Session["NAME"] + "";
        string exeSqlStr = "";
        error_msg.Text = "";
        Label pgTBName = new Label();
        Label pgTBDate = new Label();

        #region.值設定
        switch (btnType) {
            case "Btn_UPSFile01":
                tSFType = "001"; tSFName = "UPSFA_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU01; pgTBDate = LBUPLOADD01;
                filename = SFFile01_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile01_FileUpload, TXTSFile01, "TXTSFile01", tSFName, null, SFLINK01, 150, ""); //150MB
                break;
            case "Btn_UPSFile02":
                tSFType = "002"; tSFName = "UPSFB_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU02; pgTBDate = LBUPLOADD02;
                filename = SFFile02_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile02_FileUpload, TXTSFile02, "TXTSFile02", tSFName, null, SFLINK02, 150, ""); //150MB
                break;
            case "Btn_UPSFile03":
                tSFType = "003"; tSFName = "UPSFC_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU03; pgTBDate = LBUPLOADD03;
                filename = SFFile03_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile03_FileUpload, TXTSFile03, "TXTSFile03", tSFName, null, SFLINK03, 150, ""); //150MB
                break;
            case "Btn_UPSFile04":
                tSFType = "004"; tSFName = "UPSFD_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU04; pgTBDate = LBUPLOADD04;
                filename = SFFile04_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDFJPGPNG", SFFile04_FileUpload, TXTSFile04, "TXTSFile04", tSFName, null, SFLINK04, 50, ""); //50MB
                break;
            case "Btn_UPSFile05":
                tSFType = "005"; tSFName = "UPSFE_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU05; pgTBDate = LBUPLOADD05;
                filename = SFFile05_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDFJPGPNG", SFFile05_FileUpload, TXTSFile05, "TXTSFile05", tSFName, null, SFLINK05, 50, ""); //50MB
                break;
            case "Btn_UPSFile06":
                tSFType = "006"; tSFName = "UPSFF_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU06; pgTBDate = LBUPLOADD06;
                filename = SFFile06_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile06_FileUpload, TXTSFile06, "TXTSFile06", tSFName, null, SFLINK06, 150, ""); //50MB
                break;
            case "Btn_UPSFile07":
                tSFType = "007"; tSFName = "UPSFG_" + rDTLNO.Substring(rDTLNO.Length - 3); pgTBName = LBUPLOADU07; pgTBDate = LBUPLOADD07;
                filename = SFFile07_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile07_FileUpload, TXTSFile07, "TXTSFile07", tSFName, null, SFLINK07, 150, ""); //50MB
                break;
        }
        #endregion

        #region.存入db
        if (rUP)
        {
            tSFName += extension;
            pgTBName.Text = ssUserNAME;
            pgTBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string updDB = " Update ShareFiles Set [SFName]='"+ tSFName + "',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();
                
                string strSQLRV = " select * from ShareFiles  Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                if (!readeSwc.HasRows)
                {
                    exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE) VALUES ('" + rDTLNO + "','" + tSFType + "');";
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                exeSqlStr += updDB;
                SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
        }
        #endregion

        #region.寄信
        //審查公會、公會審查召集人、審查委員
        string tToDay = DateTime.Now.ToString("yyyy-MM-dd");
        string tSWC005 = TXTSWC005.Text;
        string tSWC022ID = LBSWC022ID.Text; //審查公會
        string tLBSAOID = ";;" + LBSAOID.Text.Replace(";;;;", "");
        string tMailSub = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        string tMailText = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

        switch (btnType)
        {
            case "Btn_UPSFile01":
            case "Btn_UPSFile02":
            case "Btn_UPSFile03":
                string[] arraySentMail = new string[] { "" };
                string[] arrayMailToP = tLBSAOID.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);

                arraySentMail[0] = SBApp.GetGeoUser(tSWC022ID, "Email");
                bool MailTo01 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);

                for (int i = 0; i < arrayMailToP.Length; i++)
                {
                    arraySentMail[0] = SBApp.GetETUser(arrayMailToP[i], "Email");
                    bool MailTo02 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
                }
                break;
        }

        #endregion
    }

    #region.檔案交換區
    private bool FileUpLoadApp2(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadReName, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize, string SubFolder)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string CaseId = LBSWC000.Text + "";
        string UpLoadFileName = UpLoadReName;
        bool rValue = false;

        #region.基本檢查
        string vTempValue = UpLoadText.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return rValue;
        }
        string tUpLoadFile = UpLoadBar.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return rValue;
        }
        #endregion

        #region.檔案上傳
        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名
            switch (ChkType)
            {
                case "PDF":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".PDF" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF 檔案格式上傳，謝謝!!");
                        return rValue;
                    }
                    break;
                case "PDFJPGPNG":
                    List<string> allowedExtextsion01 = new List<string> { ".pdf", ".PDF", ".JPG", ".jpg", ".PNG", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF、JPG、PNG 檔案格式上傳，謝謝!!");
                        return rValue;
                    }
                    break;
            }

            //檔案大小限制
            int filesize = UpLoadBar.PostedFile.ContentLength;
            if (filesize > _FileMaxSize * 1000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!");
                return rValue;
            }
            UpLoadFileName += extension;

            #region.上傳設定
            //檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath"] + CaseId;
            if (SubFolder.Trim() != "") serverDir += "\\" + SubFolder;
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            Session[UpLoadStr] = "有檔案";

            string serverFilePath = Path.Combine(serverDir, UpLoadFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(UpLoadFileName);

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);
                
                switch (ChkType)
                {
                    case "PDF":
                    case "PDFJPGPNG":
                        UpLoadLink.Text = UpLoadFileName;
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\SwcCaseFile\\" + CaseId + "\\"+ SubFolder+"\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;
                }
                UpLoadText.Text = UpLoadFileName;

                #region.上傳成功存db
                rValue = true;
                #endregion
            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }
            #endregion
        }
        else
        {
            Session[UpLoadStr] = "";
        }
        return rValue;
        #endregion
    }
    #endregion

    protected void Btn_DelSFile_Click(object sender, EventArgs e)
    {
        string btnType = ((Button)(sender)).ID;

        string sourceFILENAME = TXTSWC110.Text;
        string rDTLNO = LBSWC000.Text + "";
        string delFileName = "", tSFType="";
        error_msg.Text = "";
        bool tdel = false;
        HyperLink pgLink = new HyperLink();
        TextBox pgTB = new TextBox();
        Label pgTBName = new Label();
        Label pgTBDate = new Label();

        tdel = DelPageFile(delFileName,"");

        #region.存db
        if (tdel)
        {
            switch (btnType)
            {
                case "Btn_DelSFile01":
                    pgLink = SFLINK01; pgTB = TXTSFile01; tSFType = "001"; pgTBName = LBUPLOADU01; pgTBDate = LBUPLOADD01;
                    break;
                case "Btn_DelSFile02":
                    pgLink = SFLINK02; pgTB = TXTSFile02; tSFType = "002"; pgTBName = LBUPLOADU02; pgTBDate = LBUPLOADD02;
                    break;
                case "Btn_DelSFile03":
                    pgLink = SFLINK03; pgTB = TXTSFile03; tSFType = "003"; pgTBName = LBUPLOADU03; pgTBDate = LBUPLOADD03;
                    break;
                case "Btn_DelSFile04":
                    pgLink = SFLINK04; pgTB = TXTSFile04; tSFType = "004"; pgTBName = LBUPLOADU04; pgTBDate = LBUPLOADD04;
                    break;
                case "Btn_DelSFile05":
                    pgLink = SFLINK05; pgTB = TXTSFile05; tSFType = "005"; pgTBName = LBUPLOADU05; pgTBDate = LBUPLOADD05;
                    break;
                case "Btn_DelSFile06":
                    pgLink = SFLINK06; pgTB = TXTSFile06; tSFType = "006"; pgTBName = LBUPLOADU06; pgTBDate = LBUPLOADD06;
                    break;
                case "Btn_DelSFile07":
                    pgLink = SFLINK07; pgTB = TXTSFile07; tSFType = "007"; pgTBName = LBUPLOADU07; pgTBDate = LBUPLOADD07;
                    break;
            }

            string DelDBDA = " Delete ShareFiles Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
            {
                SwcConn.Open();

                SqlCommand objCmdSWC = new SqlCommand(DelDBDA, SwcConn);
                objCmdSWC.ExecuteNonQuery();
                objCmdSWC.Dispose();
            }
            
            pgLink.Text = "";
            pgLink.NavigateUrl = "";
            pgLink.Visible = false;
            pgTB.Text = "";
            pgTBName.Text = "";
            pgTBDate.Text = "";
        }
        #endregion
    }
    #region.刪檔
    private bool DelPageFile(string delFileName,string delFileType)
    {
        bool rValue = false;
        string csCaseID = LBSWC000.Text + "";

        //刪實體檔
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];        
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + delFileName;

        try {
            if (File.Exists(FileFullPath))File.Delete(FileFullPath);
            rValue = true;  } catch{ }
        
        return rValue;
    }
    #endregion


    protected void Button1_Click(object sender, EventArgs e)
    {
        GenPdf();
    }
    private void GenPdf()
    {
        string rCaseId = Request.QueryString["CaseId"] + "";
        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath"].Trim();
        GBClass001 SBApp = new GBClass001();
        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/swcchg.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        string ExeSqlStr = " select * from SWCCASE where SWC000 = '" + rCaseId + "' ";
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //資料開始
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(ExeSqlStr, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";
                
                tSWC038 = SBApp.DateView(tSWC038, "00");
                tSWC043 = SBApp.DateView(tSWC043, "00");
                tSWC051 = SBApp.DateView(tSWC051, "00");
                tSWC052 = SBApp.DateView(tSWC052, "00");
                
                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //0.檢查日期
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg001");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg001");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg002");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg002");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg003");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg003");

                //1.列印案件編號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg004");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg004");

                //2.計畫名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg005");

                //3.水保與簡易水保
                string dwSwc007a = "□";
                string dwSwc007b = "□";
                if (tSWC007 == "水土保持計畫") { dwSwc007a = "■"; } else { dwSwc007b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg006");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg006");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg007");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg007");

                //4.核定日期文號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg008");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg008");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg009");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC039, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg009");

                //5.施工許可證日期文號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg010");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg010");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg011");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC044, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg011");

                //6.開工日期
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg012");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg012");

                //7.預定完工日期
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg013");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg013");

                //8.水保義務人姓名或名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg014");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg014");

                //8.水保義務人身分證或統編
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg015");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013ID, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg015");

                //9.水保義務人地址
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg016");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg016");

                //10.承辦監造計師姓名
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg017");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC045, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg017");

                //11.承辦監造計師機構名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg018");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgName"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg018");

                //12.承辦監造計師執業執照字號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg019");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgIssNo"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg019");

                //13.承辦監造計師統編
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg020");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgGUINo"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg020");

                //14.承辦監造計師電話
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg021");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgTel"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg021");

                //15.實施地點土地標是
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg022");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg022");

                //16.水保告示牌....(下拉+說明)
                string[] arrayCheckBoxStr = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
                string[] arrayRemarkTxStr = new string[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };

                for (int i = 0; i < arrayCheckBoxStr.Length; i++)
                {
                    string strCheckBox = arrayCheckBoxStr[i];
                    string strRemarkTx = arrayRemarkTxStr[i];

                    string dwDTLCa = "□";
                    string dwDTLCb = "□";
                    string dwDTLCc = "□";

                    switch (strCheckBox)
                    {
                        case "依計畫施作":
                            dwDTLCa = "■";
                            break;
                        case "未依計畫施作":
                            dwDTLCb = "■";
                            break;
                        case "尚未施作":
                            dwDTLCc = "■";
                            break;
                        case "無此項":
                            strRemarkTx = "無此項 \n" + strRemarkTx;
                            break;
                    }

                    //選項用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 1).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCa, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 1).ToString());

                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 2).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCb, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 2).ToString());

                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 3).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCc, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 3).ToString());

                    //說明用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 4).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(strRemarkTx, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 4).ToString());

                }


                string[] arrayCheckBox02 = new string[] { "", "", "" };
                string[] arrayRemarkTx02 = new string[] { "", "", "" };
                //監造計師是否在場 ， "swcchg095","swcchg096","swcchg097"

                for (int i = 0; i < arrayCheckBox02.Length; i++)
                {
                    string strCheckBox02 = arrayCheckBox02[i];
                    string strRemarkTx02 = arrayRemarkTx02[i];

                    string dwDTLC2a = "□";
                    string dwDTLC2b = "□";

                    switch (strCheckBox02)
                    {
                        case "是":
                            dwDTLC2a = "■";
                            break;
                        case "否":
                            dwDTLC2b = "■";
                            break;

                    }
                    //選項用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 1).ToString()).PadLeft(3, '0'));
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLC2a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 1).ToString()).PadLeft(3, '0'));

                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 2).ToString()).PadLeft(3, '0'));
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLC2b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 2).ToString()).PadLeft(3, '0'));

                    //說明用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 3).ToString()).PadLeft(3, '0'));
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(strRemarkTx02, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 3).ToString()).PadLeft(3, '0'));
                }



                //37.實施與計畫或規定不符事項及改正奇現
                string t4849 = "";
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg104");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(t4849, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg104");

                //38.其他注意事項
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg105");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg105");

                //39.前次改正事項
                string t5152 = "";
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg106");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(t5152, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg106");

                //40.簽名
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg107");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg107");

                //41.相片標題(水保計畫名)
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg108");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg108");

                //42~47.相片說明文字
                string[] arrayPicRemark = new string[] { "", "", "", "", "", "" };
                string[] arrayPDFView05 = new string[] { "swcchg109", "swcchg110", "swcchg111", "swcchg112", "swcchg113", "swcchg114" };

                for (int i = 0; i < arrayPicRemark.Length; i++)
                {
                    string aPicRemark = arrayPicRemark[i] + "";
                    string aPdfView05 = arrayPDFView05[i] + "";

                    ps = Pdfstamper.AcroFields.GetFieldPositions(aPdfView05);
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(aPicRemark, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField(aPdfView05);

                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();


        }


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        Pdfstamper.Close();
        Pdfreader.Close();

        //把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        System.IO.Stream iStream;

        // 以10K為單位暫存:
        Byte[] buffer = new byte[10000];
        int length = 0;
        long dataToRead = 0;

        // 制定文件路徑
        string filepath = Server.MapPath("~\\OutputFile\\" + pdfnewname);
        string filepathm = Server.MapPath("~\\OutputFile\\m" + pdfnewname);

        // 得到文件名
        string filename = System.IO.Path.GetFileName(filepath);

        //Try
        // 打開文件
        iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        // 得到文件大小
        dataToRead = iStream.Length;
        Response.ContentType = "application/x-rar-compressed";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));

        while (dataToRead > 0)
        {
            if (Response.IsClientConnected)
            {
                length = iStream.Read(buffer, 0, 10000);
                Response.OutputStream.Write(buffer, 0, length);
                Response.Flush();
                dataToRead = dataToRead - length;
            }
            else
            {
                dataToRead = -1;
            }
        }
        if (iStream.Length != 0)
        {
            //關閉文件
            iStream.Close();
            File.Delete(filepath);
        }
    }
	protected void LB1_Click(object sender, EventArgs e){
		//*******************
		//TOKEN
		string ssUserID = Session["ID"] + "";
		string ssUserType = Session["UserType"] + "";
		string ssUserName = Session["NAME"] + "";
		string token_temp = Session["PW"]+"|"+Session["Unit"]+"|"+Session["JobTitle"]+"|"+Session["Edit4"]+"|"+Session["WMGuild"]+"|"+Session["Guild01"]+"|"+Session["Guild02"]+"|"+Session["ETU_Guild01"]+"|"+Session["ETU_Guild02"]+"|"+Session["ETU_Guild03"]+"|"+Session["ONLINEAPPLY"]+"|"+Session["NUIDNO"]+"|"+Session["NUNAME"]+"|"+Session["NUCELL"]+"|"+Session["NUMAIL"]+"|"+Session["Department"]+"|"+Session["uid"]+"|"+Session["right"]+"|"+Session["grade"]+"|"+Session["TcgeDataedit"]+"|"+Session["TcgeDataview"]+"|"+Session["SuperUser"]+"|"+Session["presented"]+"";

		string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
		string source = "書件";
		// 0 Session["ID"]
		// 1 Session["UserType"]
		// 2 Session["NAME"]
		// 3 時間
		// 4 來源
		// 5 Session["PW"]
		// 6 Session["Unit"]
		// 7 Session["JobTitle"]
		// 8 Session["Edit4"]
		// 9 Session["WMGuild"]
		//10 Session["Guild01"]
		//11 Session["Guild02"]
		//12 Session["ETU_Guild01"]
		//13 Session["ETU_Guild02"]
		//14 Session["ETU_Guild03"]
		//15 Session["ONLINEAPPLY"]
		//16 Session["NUIDNO"]
		//17 Session["NUNAME"]
		//18 Session["NUCELL"]
		//19 Session["NUMAIL"]
		//20 Session["Department"]
		//21 Session["uid"]
		//22 Session["right"]
		//23 Session["grade"]
		//24 Session["TcgeDataedit"]
		//25 Session["TcgeDataview"]
		//26 Session["SuperUser"]
		//27 Session["presented"]
		string token = ssUserID + "|" + ssUserType + "|" + ssUserName + "|" + dt + "|" + source + "|" + token_temp;
		byte[] b = Encoding.UTF8.GetBytes(token);
		DESCryptoServiceProvider des = new DESCryptoServiceProvider();
		var Key = new Byte[] { };
		var IV = new Byte[] { };
		ICryptoTransform ict = des.CreateEncryptor(Key, IV);
		byte[] outData = ict.TransformFinalBlock(b, 0, b.Length);
		var op = Convert.ToBase64String(outData);
		//*******************second part
		//0 dt
		//1 Session["IDNO"]
        string token2 = dt + "|" + Session["IDNO"];
		byte[] b2 = Encoding.UTF8.GetBytes(token2);
		DESCryptoServiceProvider des2 = new DESCryptoServiceProvider();
		var Key2 = "TGEOTGEO";
		var IV2 = "TGEOTGEO";
		ICryptoTransform ict2 = des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes(Key2), ASCIIEncoding.ASCII.GetBytes(IV2));
		byte[] outData2 = ict2.TransformFinalBlock(b2, 0, b2.Length);
		var op2 = Convert.ToBase64String(outData2);
		//*******************
		var qSWC002 = LBSWC002.Text;
		Session["NCKU"] = "T";
		
		string url="https://tgeo.swc.taipei?UTYPE="+ ssUserType + "&UNAME="+ ssUserName+ "&rid=" + qSWC002 + "&Token=" + op + "&Source=" + source + "&Token2=" + op2;
		Response.Write("<script>window.open('" + url + "','_blank'); </script>");
		//Response.Redirect(url);
	}
	protected void CHKSDI019_CheckedChanged(object sender, EventArgs e)
    {
		TXTSDI006.Text = "";
		TXTSDI006_1.Text = "";
		TXTSDI006D.Text = "";
		//TEST
		if (TXTSDI006.Enabled || /*TXTSDI006_1.Enabled ||*/ TXTSDI006D.Enabled){
			TXTSDI006.Enabled = true;
			//TEST
			//TXTSDI006_1.Enabled = true;
			TXTSDI006_1.Enabled = false;
			
			TXTSDI006D.Enabled = true;
		}
		
		if (TXTSDI012.Enabled) TXTSDI012_1.Enabled = true;
		if (TXTSDI013.Enabled) TXTSDI013_1.Enabled = true;
		if (TXTSDI014.Enabled) TXTSDI014_1.Enabled = true;
		
        if (CHKSDI019.Checked)
        {	
			if (TXTSDI012.Enabled) TXTSDI012_1.Enabled = true;
			if (TXTSDI013.Enabled) TXTSDI013_1.Enabled = true;
			if (TXTSDI014.Enabled) TXTSDI014_1.Enabled = true;
            TXTSDI006.Visible = true;
			//TEST
			//AA.Visible = true;
			//TXTSDI006_1.Visible = true;
			AA.Visible = false;
			TXTSDI006_1.Visible = false;	
			
			TXTSDI006D.Visible = false;
        }
		//else if (DDLSIC02.SelectedItem.Value=="其他"){
		//	TXTSDI006.Visible = false;
		//	AA.Visible = false;
		//	TXTSDI006_1.Visible = false;
		//	TXTSDI006D.Visible = true;
		//}
        else
        {
			TXTSDI012_1.Enabled = false;
			TXTSDI013_1.Enabled = false;
			TXTSDI014_1.Enabled = false;
			TXTSDI012_1.Text = "";
			TXTSDI013_1.Text = "";
			TXTSDI014_1.Text = "";
			TXTSDI006.Visible = true;
			AA.Visible = false;
			TXTSDI006_1.Visible = false;
			TXTSDI006D.Visible = false;
        }
    }
	
	protected void DDLSWCPCITY_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region-義務人資訊
        AddressApiClass AAC = new AddressApiClass();
        //List<AddressApiClass.Area_detail> arryArea = new List<AddressApiClass.Area_detail> { };
        string[] arryArea = new string[] { };
        arryArea = AAC.GetArea(DDLSWCPCITY.SelectedItem.Text);
        DDLSWCPAREA.Items.Clear();
        foreach (var item in arryArea)
        {
            //DDLSWCPAREA.Items.Add(new System.Web.UI.WebControls.ListItem(item.district, item.district));
			DDLSWCPAREA.Items.Add(new System.Web.UI.WebControls.ListItem(item, item));
        }
		
		TXTSWCPCODE.Text = AAC.GetZip(DDLSWCPCITY.SelectedItem.Text, DDLSWCPAREA.SelectedItem.Text);
        #endregion
    }
	protected void DDLSWCPAREA_SelectedIndexChanged(object sender, EventArgs e)
    {
        #region-義務人資訊
        AddressApiClass AAC = new AddressApiClass();
		TXTSWCPCODE.Text = AAC.GetZip(DDLSWCPCITY.SelectedItem.Text, DDLSWCPAREA.SelectedItem.Text);
        #endregion
    }
	
	protected void AddAddress_Click(object sender, EventArgs e)
    {
		#region-義務人資訊
        GBClass001 SBApp = new GBClass001();
		
		string name = TXTSWCPNAME.Text;
		string id_no = TXTSWCPID.Text;
		string phone_no = TXTSWCPPHONE.Text;
		string zip_code = TXTSWCPCODE.Text;
		string city = DDLSWCPCITY.SelectedItem.Text;
		string area = DDLSWCPAREA.SelectedItem.Text;
		string address = TXTSWCPADDRESS.Text;
		
		if (name == "" || id_no == "" || phone_no == "" || address == "")
        {
            Response.Write("<script>alert('請輸入完整義務人資訊');</script>");
            return;
        }
		
		AddNO.Text = (Convert.ToInt32(AddNO.Text) + 1).ToString();
		
		DataTable tbPeople = (DataTable)ViewState["SwcPeople"];
		if (tbPeople == null)
        {
            DataTable GVPL = new DataTable();

            GVPL.Columns.Add(new DataColumn("序號", typeof(string)));
            GVPL.Columns.Add(new DataColumn("姓名", typeof(string)));
            GVPL.Columns.Add(new DataColumn("身分證字號", typeof(string)));
            GVPL.Columns.Add(new DataColumn("手機", typeof(string)));
            GVPL.Columns.Add(new DataColumn("地址ZipCode", typeof(string)));
            GVPL.Columns.Add(new DataColumn("地址City", typeof(string)));
            GVPL.Columns.Add(new DataColumn("地址District", typeof(string)));
            GVPL.Columns.Add(new DataColumn("地址Address", typeof(string)));
            GVPL.Columns.Add(new DataColumn("地址", typeof(string)));

            ViewState["SwcPeople"] = GVPL;
            tbPeople = (DataTable)ViewState["SwcPeople"];
        }
		
		DataRow GVPLRow = tbPeople.NewRow();

        GVPLRow["序號"] = AddNO.Text;
        GVPLRow["姓名"] = name; //SWC013
        GVPLRow["身分證字號"] = id_no; //SWC013ID
        GVPLRow["手機"] = phone_no; //SWC013TEL
        GVPLRow["地址ZipCode"] = zip_code; //SWC014
        GVPLRow["地址City"] = city; //SWC014
        GVPLRow["地址District"] = area; //SWC014
        GVPLRow["地址Address"] = address; //SWC014
        GVPLRow["地址"] = zip_code + city + area + address; //SWC014

        tbPeople.Rows.Add(GVPLRow);

        //Store the DataTable in ViewState
        ViewState["SwcPeople"] = tbPeople;

        GVPEOPLE.DataSource = tbPeople;
        GVPEOPLE.DataBind();
		#endregion
    }
	protected void GVPEOPLE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
		#region-義務人資訊
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "delfile001":
                int aa = Convert.ToInt32(e.CommandArgument);

                DataTable VS_GV1 = (DataTable)ViewState["SwcPeople"];

                VS_GV1.Rows.RemoveAt(aa);

                ViewState["SwcPeople"] = VS_GV1;
                GVPEOPLE.DataSource = VS_GV1;
                GVPEOPLE.DataBind();

                break;
        }
		#endregion
    }
	
	protected void TXTSWC138_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC138.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC138_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
		string tSWC007 = DDLSWC007.Text + "";
        if (tSWC007 == "")
        {
            Response.Write("<script>alert('請先選擇書件類別');</script>");
            return;
        }
		

        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PDFJPG138", TXTSWC138_fileupload, TXTSWC138, "TXTSWC138", "", null, Link138, 500); //50MB
    }
	
	protected void TXTSWC138_fileclean_Click(object sender, EventArgs e)
    {
		GBClass001 MyBassAppPj = new GBClass001();
        string rDTLNO = LBSWC000.Text + "";
        error_msg.Text = "";
		string SwcCaseFolderPath1 = MyBassAppPj.getFilePath(LBSWC000.Text, LBSWC002.Text, DDLSWC007.Text) + LBSWC002.Text + "\\環評報告書or免環評證明文件\\環評報告書or免環評證明文件";
		if(Directory.Exists(SwcCaseFolderPath1))
		{
			DirectoryInfo di = new DirectoryInfo(SwcCaseFolderPath1);
			foreach (FileInfo file in di.GetFiles())
			{
				file.Delete(); 
			}
		}
        DeleteUpLoadFile("PDFJPG138", TXTSWC138, null, Link138, "SWC138", "TXTSWC138", 0, 0);
    }
	protected bool CheckLand()
    {
		bool re = false;
		DataTable updateLAND = (DataTable)ViewState["SwcCadastral"];
		for (int i=0; i<updateLAND.Rows.Count; i++)
        {
			string aa = updateLAND.Rows[i]["陽明山國家公園範圍"] + "";
			//Response.Write("<script>alert('"+aa+"');</script>");
            if(aa == "屬國家公園") re = true;
        }
		return re;
    }
}