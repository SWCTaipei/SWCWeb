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
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWC002 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["CaseId"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();

        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId == "" || ssUserID == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        if (!IsPostBack) {
            Session["CaseStatus"] = "";

            GenerateDropDownList();
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
                    TXTSWC013ID.Enabled = false;
                    TXTSWC013ID.Text = ssUserID;
                    TXTSWC013TEL.Enabled = false;
                    TXTSWC013TEL.Text = ssUserPW;
                    DDLSWC007.Enabled = false;
                    break;

                case "02":
                    TitleLink00.Visible = true;

                    if (rCaseId == "ADDNEW")
                    {
                        tOUT = "N";
                        Area01Open();
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

                        if (tCaseStatus == "退補件" || tCaseStatus == "受理中")
                        {
                            Area01Open();
                        }
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
                    //審查公會
                    if (tSWC022ID == ssUserID) 
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
                    }
                    //檢查公會
                    if (tSWC024ID == ssUserID) 
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
                    break;
            }
        }











            if (ssUserID == "gv-admin")
        {
            Area03Close();
            Area04Close();
            Area05Close();
        } else
        {
            switch (ssUserType)
            {
                case "01":

                    if (tSWC013ID == ssUserID)
                    {
                        if (tCaseType == "簡易水保")
                        {
                            if (tCaseStatus == "退補件" || tCaseStatus == "受理中")
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
                    break;

                case "02":
                    
                        
                    if (tSWC021ID == ssUserID)  //承辦技師
                    {
                        tOUT = "N";

                        Area02.Visible = true;

                        Area03Close();

                        Area05Close();

                        if (tCaseStatus == "退補件" || tCaseStatus == "受理中")
                        {
                            Area01Open();
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

        //以下全區公用

        SBApp.ViewRecord("水保申請案件", "view", rCaseId);

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "") 
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }
    private void GetSwcData(string vSwcID)
    {
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();
        //Area01.Visible = true;
        //Area02.Visible = true;
        //Area03.Visible = true;
        //Area04.Visible = true;
        //Area05.Visible = true;

        if (vSwcID.Trim() == "ADDNEW")
        {
            //新建
            string pSwcId=GetNewId();
            string tSwc004 = "受理中";
            string LBSWC000ID = "SWC" + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            Session["CaseStatus"] = tSwc004;

            switch (ssUserType)
            {
                case "01":
                    DDLSWC007.SelectedValue = "簡易水保";
                    DDLSWC007.Enabled = false;
                    break;
            }

            TempSwc000.Text = LBSWC000ID;
            TXTSWC108_chk.Text = SBApp.GetETUser(ssUserID, "Email"); ;

            LBSWC000.Text = vSwcID;
            LBSWC002.Text = pSwcId;
            LBSWC004.Text = tSwc004;
            DDLSWC007.Visible = true;
            LBSWC007.Visible = false;
            LBSWC007.Text = "";
            LBSWC021.Text = ssUserName;

            Area02.Visible = false;
            Area03.Visible = false;
            Area04.Visible = false;
            Area05.Visible = false;
        }
        else
        {
            TempSwc000.Text = vSwcID;
            SetSwcCase(vSwcID);
        }
        LBSWC000.Visible = false;
        TempSwc000.Visible = false;
    }
    private void SetSwcCase(string rSWCNO)
    {
        string ssUserType = Session["UserType"] + "";

        string qSWC000 = "", qSWC001 = "", qSWC002 = "", qSWC004 = "", qSWC005 = "", qSWC007 = "";
        string qSWC013 = "", qSWC014 = "", qSWC015 = "", qSWC016 = "", qSWC017 = "", qSWC018 = "";//, qSWC004 = "", qSWC005 = "", qSWC007 = "";


        string qSWC031 = "", qSWC032 = "", qSWC033 = "", qSWC034 = "", qSWC035 = "", qSWC036 = "", qSWC037 = "", qSWC038 = "", qSWC039 = "", qSWC040 = "";
        string qSWC021 = "", qSWC022 = "", qSWC023 = "", qSWC024 = "", qSWC025 = "", qSWC026 = "", qSWC027 = "", qSWC028 = "", qSWC029 = "", qSWC030 = "";
        string qSWC041 = "", qSWC042 = "", qSWC043 = "", qSWC044 = "", qSWC045 = "", qSWC046 = "", qSWC047 = "", qSWC048 = "", qSWC049 = "", qSWC050 = "";
        string qSWC051 = "", qSWC052 = "", qSWC053 = "", qSWC054 = "", qSWC055 = "", qSWC056 = "", qSWC057 = "", qSWC058 = "", qSWC059 = "", qSWC060 = "";
        string qSWC061 = "", qSWC062 = "", qSWC063 = "", qSWC064 = "", qSWC065 = "", qSWC066 = "", qSWC067 = "", qSWC068 = "", qSWC069 = "", qSWC070 = "";
        string qSWC071 = "", qSWC072 = "", qSWC073 = "", qSWC074 = "", qSWC075 = "", qSWC076 = "", qSWC077 = "", qSWC078 = "", qSWC079 = "", qSWC080 = "";
        string qSWC081 = "", qSWC082 = "", qSWC083 = "", qSWC084 = "", qSWC085 = "", qSWC086 = "", qSWC087 = "", qSWC088 = "", qSWC089 = "", qSWC090 = "";
        string qSWC091 = "", qSWC092 = "", qSWC093 = "", qSWC094 = "", qSWC095 = "", qSWC096 = "", qSWC097 = "", qSWC098 = "", qSWC099 = "", qSWC100 = "";
        string qSWC101 = "", qSWC103 = "", qSWC104 = "", qSWC105 = "", qSWC106 = "", qSWC108 = "", qSWC109 = "", qSWC110 = "";// qSWC096 = "", qSWC097 = "";

        string qSWC013ID = "", qSWC021ID = "", qSWC045ID = "", qSWC022ID = "", qSWC024ID = "", qSWC107ID = "";
        string qSWC029CAD = "", qSWC013TEL="", qSWC101CAD="";

        GBClass001 SBApp = new GBClass001();

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
                qSWC000 = readeSwc["SWC000"] + "";
                qSWC002 = readeSwc["SWC002"] + "";
                qSWC004 = readeSwc["SWC004"] + "";
                qSWC005 = readeSwc["SWC005"] + "";
                qSWC007 = readeSwc["SWC007"] + "";
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

                qSWC013ID = readeSwc["SWC013ID"] + "";
                qSWC021ID = readeSwc["SWC021ID"] + "";
                qSWC045ID = readeSwc["SWC045ID"] + "";
                qSWC022ID = readeSwc["SWC022ID"] + "";
                qSWC024ID = readeSwc["SWC024ID"] + "";
                qSWC107ID = readeSwc["SWC107ID"] + "";

                qSWC013TEL = readeSwc["SWC013TEL"] + "";

                Session["CaseStatus"] = qSWC004;
            }
        }

        //丟資料
        CHKSWC061.Checked = false;
        CHKSWC063.Checked = false;
        CHKSWC065.Checked = false;
        
        LBSWC000.Text = qSWC000;
        LBSWC002.Text = qSWC002;
        LBSWC004.Text = qSWC004;
        TXTSWC005.Text = qSWC005;
        DDLSWC007.SelectedValue = qSWC007;
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
        TXTSWC110.Text = qSWC110;
        TXTSWC101CAD.Text = qSWC101CAD;

        if (qSWC061 == "1") { CHKSWC061.Checked = true; }
        if (qSWC063 == "1") { CHKSWC063.Checked = true; }
        if (qSWC065 == "1") { CHKSWC065.Checked = true; }
        if (qSWC067 == "1") { CHKSWC067.Checked = true; }
        if (qSWC071 == "1") { CHKSWC071.Checked = true; }
        if (qSWC073 == "1") { CHKSWC073.Checked = true; }
        if (qSWC075 == "1") { CHKSWC075.Checked = true; }

        //檔案類處理
        string[] arrayFileNameLink = new string[] { qSWC029, qSWC029CAD, qSWC030, qSWC080, qSWC101, qSWC106,qSWC110, qSWC101CAD };
        System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link029, Link029CAD, Link030, Link080, Link101, Link106,Link110, Link101CAD };

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
                string tempLinkPateh = SwcUpLoadFilePath + qSWC000 + "/" + strFileName;
                FileLinkObj.Text = strFileName;
                FileLinkObj.NavigateUrl = tempLinkPateh;
                FileLinkObj.Visible = true;
            }

        }

        //01.審查，審查公會，可編，ssUserType=04;SWC022ID=ssUserID
        //03.施工，檢查公會，可編，ssUserType=04;SWC024ID=ssUserID
        //04.施工，監造技師，可編，ssUserType=02;SWC045ID=ssUserID
        //05.施工，監造技師，可編，ssUserType=02;SWC045ID=ssUserID
        //06.施工，檢查公會，可編，ssUserType=04;SWC024ID=ssUserID
        //07.完工，完工公會，可編，ssUserType=04;Session["Edit4"] + "" == "Y"

        string ssUserID = Session["ID"] + "";

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

            Sql04Str = Sql04Str + " Select [DTLD001] ,CONVERT(varchar(100), [DTLD002], 23) AS DTLD002,DTLD003,DTLD004,DATALOCK From SWCDTL04 ";
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

        //地籍...
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

                DataTable tbCadastral = (DataTable)ViewState["SwcCadastral"];

                if (tbCadastral == null)
                {
                    DataTable GVTBCD = new DataTable();

                    GVTBCD.Columns.Add(new DataColumn("序號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("小段", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地號", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地使用分區", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("土地可利用限度", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("林地類別", typeof(string)));
                    GVTBCD.Columns.Add(new DataColumn("地質敏感區", typeof(string)));

                    ViewState["SwcCadastral"] = GVTBCD;
                    tbCadastral = (DataTable)ViewState["SwcCadastral"];
                }

                DataRow GVTBCDRow = tbCadastral.NewRow();

                GVTBCDRow["序號"] = ++nj;
                GVTBCDRow["區"] = dLAND001;
                GVTBCDRow["段"] = dLAND002;
                GVTBCDRow["小段"] = dLAND003;
                GVTBCDRow["地號"] = dLAND004;
                GVTBCDRow["土地使用分區"] = dLAND005;
                GVTBCDRow["土地可利用限度"] = dLAND006;
                GVTBCDRow["林地類別"] = dLAND007;
                GVTBCDRow["地質敏感區"] = dLAND008;

                tbCadastral.Rows.Add(GVTBCDRow);

                //Store the DataTable in ViewState
                ViewState["SwcCadastral"] = tbCadastral;

                GVCadastral.DataSource = tbCadastral;
                GVCadastral.DataBind();

                CDNO.Text = nj.ToString();

            }
            readerItem.Close();
        }

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

                if (qSWC004=="受理中" || qSWC004 == "退補件")
                {
                    Area01Open();
                }

                DDLSWC007.Enabled = false;
                break;
            case "04":
                Area01Close();      //基本資料

                break;
        }
        SBApp.ViewRecord("水保申請案件", "view", rSWCNO);

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
        ADDLIST01.Enabled = true;
        DDLDistrict.Enabled = true;
        DDLSection.Enabled = true;
        DDLSection2.Enabled = true;
        TXTNumber.Enabled = true;
        DDLCA01.Enabled = true;
        DDLCA02.Enabled = true;
        DDLCA03.Enabled = true;
        DDLCA04.Enabled = true;
        TXTSWC027.Enabled = true;
        TXTSWC028.Enabled = true;
        TXTFILES001_fileupload.Enabled = true;
        TXTFILES001_fileuploadok.Enabled = true;
        BTNFILES001.Enabled = true;
        AddFile001.Enabled = true;
        GVCadastral.Columns[9].Visible = true;
        SWCFILES001.Columns[2].Visible = true;
        SWCFILES001.CssClass = "detailsGrid_skyblue_innerTable detailsGrid_skyblue_innerTable_lastCenter";
        TXTSWC108.Enabled = true;
    }

    private void Area01Close()
    {
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
        ADDLIST01.Enabled = false;
        DDLDistrict.Enabled = false;
        DDLSection.Enabled = false;
        DDLSection2.Enabled = false;
        TXTNumber.Enabled = false;
        DDLCA01.Enabled = false;
        DDLCA02.Enabled = false;
        DDLCA03.Enabled = false;
        DDLCA04.Enabled = false;
        TXTSWC027.Enabled = false;
        TXTSWC028.Enabled = false;
        TXTFILES001_fileupload.Enabled = false;
        TXTFILES001_fileuploadok.Enabled = false;
        BTNFILES001.Enabled = false;
        AddFile001.Enabled = false;
        GVCadastral.Columns[9].Visible = false;
        SWCFILES001.Columns[2].Visible = false;
        SWCFILES001.CssClass = "detailsGrid_skyblue_innerTable";
        TXTSWC108.Enabled = false;
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
            {
                if (Convert.ToInt32(aSwc002.Substring(7, 5)) > Convert.ToInt32(tSWC002.Substring(7, 5)))
                {
                    tSWC002 = aSwc002;
                }

            }
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
    protected void GenerateDropDownList() {
        //string[] array_CaseStatus = new string[] { "", "退補件", "不予受理", "受理中", "審查中", "暫停審查", "撤銷", "不予核定", "已核定", "施工中", "停工中", "已完工", "廢止", "失效", "已變更" };

        string[] array_SWC007 = new string[] { "", "水土保持計畫", "簡易水保" };
        DDLSWC007.DataSource = array_SWC007;
        DDLSWC007.DataBind();
        
        string[,] array_District = new string[,] { { "0", "" }, { "16", "北投" }, { "15", "士林" }, { "14", "內湖" }, { "10", "中山" }, { "03", "中正" }, { "17", "信義" }, { "02", "大安" }, { "13", "南港" }, { "11", "文山" } };
        List<ListItem> ListZip = new List<ListItem>();
        for (int te = 0; te <= array_District.GetUpperBound(0); te++)
        {
            ListItem li = new ListItem(array_District[te, 1], array_District[te, 0]);
            ListZip.Add(li);
        }
        DDLDistrict.Items.AddRange(ListZip.ToArray());

        string[] array_DDLCA01 = new string[] { "", "保護區", "住宅區", "陽明山國家公園", "保變住", "風景區", "商業區", "工業區", "行政區", "文教區", "倉庫區", "農業區", "行水區", "保存區", "特定專用區", "公共設施用地" };
        DDLCA01.DataSource = array_DDLCA01;
        DDLCA01.DataBind();

        string[] array_DDLCA02 = new string[] { "", "尚未公告", "宜農牧地", "宜林地", "加強保育地", "不屬查定" };
        DDLCA02.DataSource = array_DDLCA02;
        DDLCA02.DataBind();

        string[] array_DDLCA03 = new string[] { "", "本市林地", "保安林" };
        DDLCA03.DataSource = array_DDLCA03;
        DDLCA03.DataBind();

        string[] array_DDLCA04 = new string[] { "", "山崩與地滑", "地質遺跡", "地下水補注", "活動斷層" };
        DDLCA04.DataSource = array_DDLCA04;
        DDLCA04.DataBind();
        
        string[] array_SWC017 = new string[] { "", "都市發展局", "陽明山國家公園管理處", "新建工程處", "大地工程處", "殯葬管理處", "經濟部", "產業發展局", "公園路燈工程管理處", "臺北自來水事業處", "環境保護局" };
        DDLSWC017.DataSource = array_SWC017;
        DDLSWC017.DataBind();
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWC001.aspx");
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + ""; 
        string ssUserPW = Session["PW"] + ""; 
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";

        string qTslmLand = "";

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
        string gSWC051 = TXTSWC051.Text + "";
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

        if (gSWC005.Length > 512) { gSWC005 = gSWC005.Substring(0, 512); }
        if (gSWC013.Length > 100) { gSWC013 = gSWC013.Substring(0, 100); }
        if (gSWC014.Length > 255) {
            gSWC014 = gSWC014.Substring(0, 255);
        }
        if (gSWC048.Length > 255) { gSWC048 = gSWC048.Substring(0, 255); }
        if (gSWC087.Length > 255) { gSWC087 = gSWC087.Substring(0, 255); }

        GBClass001 SBApp = new GBClass001();

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

        if (gSWC000.Trim()== "ADDNEW") {
            gSWC000 = "SWC" + DateTime.Now.Date.ToString("yyyyMMdd") + DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            LBSWC000.Text = gSWC000;

            gSWC000 = TempSwc000.Text;

            ExeSQLStr = ExeSQLStr + " INSERT INTO SWCCASE (SWC000,SWC004,SWC021ID,SWC021) VALUES ('" + gSWC000 + "','受理中','" + gSWC021ID + "','" + ssUserName + "');";
            RsvSQLStr = RsvSQLStr + " INSERT INTO SWCSWC  ( SWC00, SWC04,SWC021ID, SWC21,SWC02,SWC05) VALUES ('" + gSWC000 + "','受理中','" + gSWC021ID + "','" + ssUserName + "','" + gSWC002 + "','');";
        }

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
        ExeSQLStr = ExeSQLStr + " saveuser = '" + ssUserID + "', ";
        ExeSQLStr = ExeSQLStr + " savedate = getdate() ";
        ExeSQLStr = ExeSQLStr + " Where SWC000 = '" + gSWC000 + "';";
        
        int FileYear = Convert.ToInt16(gSWC002.Substring(4, 3));
        string FileYearS = FileYear.ToString();
        if ((FileYear > 93))
        {
            FileYearS = FileYearS + "年掃描圖檔";
        }
        else
        {
            FileYearS = "93年度暨以前掃描圖檔";
        }

        string temp55Path29="",temp55Path30 = "",temp55Path101 = "";
        if (gSWC029 != "") { temp55Path29  = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + gSWC007 + "\\" + gSWC002 + "\\審查\\" + "6-1" + "\\" + gSWC029;}
        if (gSWC030 != "") { temp55Path30  = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + gSWC007 + "\\" + gSWC002 + "\\審查\\" + "7-1" + "\\" + gSWC030; }
        if (gSWC101 != "") { temp55Path101 = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + gSWC007 + "\\" + gSWC002 + "\\竣工圖說\\" + "竣工圖說" + "\\" + gSWC101; }

        RsvSQLStr = RsvSQLStr + " Update SWCSWC Set ";
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
        RsvSQLStr = RsvSQLStr + " SWC110 =N'" + gSWC110 + "' ";
        RsvSQLStr = RsvSQLStr + " Where SWC00 = '" + gSWC000 + "'";

        string RecField = " SWC000,SWC001,SWC002,SWC003,SWC004,SWC005,SWC006,SWC007,SWC008,SWC009,SWC010,SWC011,SWC012,SWC013,SWC014,SWC015,SWC016,SWC017,SWC018,SWC019,SWC020,SWC021,SWC022,SWC023,SWC024,SWC025,SWC026,SWC027,SWC028,SWC029,SWC030,SWC031,SWC032,SWC033,SWC034,SWC035,SWC036,SWC037,SWC038,SWC039,SWC040,SWC041,SWC042,SWC043,SWC044,SWC045,SWC046,SWC047,SWC048,SWC049,SWC050,SWC051,SWC052,SWC053,SWC054,SWC055,SWC056,SWC057,SWC058,SWC059,SWC060,SWC061,SWC062,SWC063,SWC064,SWC065,SWC066,SWC067,SWC068,SWC069,SWC070,SWC071,SWC072,SWC073,SWC074,SWC075,SWC076,SWC077,SWC078,SWC079,SWC080,SWC081,SWC082,SWC083,SWC084,SWC085,SWC086,SWC087,SWC088,SWC089,SWC090,SWC091,SWC092,SWC093,SWC094,SWC095,SWC096,SWC097,SWC098,SWC099,SWC100,SWC021ID,SaveuSer,Savedate,SWC013ID,SWC013TEL,SWC022ID,SWC024ID,SWC045ID,SWC101,SWC029CAD,SWC107ID,SWC107,SWC103,SWC104,SWC105,SWC106,SWC108,SWC109,SWC110 ";
        string RecSqlStr = " INSERT INTO SWCCASE_record ("+ RecField + ") select "+ RecField + " from SWCCASE WHERE SWC000 = '" + gSWC000 + "';";

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
                    string tLAND005 = dtCD.Rows[i]["土地使用分區"].ToString().Trim();
                    string tLAND006 = dtCD.Rows[i]["土地可利用限度"].ToString().Trim();
                    string tLAND007 = dtCD.Rows[i]["林地類別"].ToString().Trim();
                    string tLAND008 = dtCD.Rows[i]["地質敏感區"].ToString().Trim();
                    string tKCNT = tLAND002 + "段" + tLAND003 + "小段";
                    
                    strSQLRV = strSQLRV + " insert into SWCLAND (SWC000,LAND000,LAND001,LAND002,LAND003,LAND004,LAND005,LAND006,LAND007,LAND008) VALUES ";
                    strSQLRV = strSQLRV + " ('" + gSWC000 + "','" + tLAND000 + "','" + tLAND001 + "','" + tLAND002 + "','" + tLAND003 + "','" + tLAND004 + "','" + tLAND005 + "','" + tLAND006 + "','" + tLAND007 + "','" + tLAND008 + "');  ";

                    strSqlTslm = strSqlTslm + " INSERT INTO [relationLand] ([區] , [段], [小段], [KCNT], [地號], [水保案件編號], [行政管理案件編號], [違規案件編號], [備註], [土地權屬], [地目], [土地使用分區], [土地可利用限度], [林地類別], [地質敏感區] ) VALUES ";
                    strSqlTslm = strSqlTslm + " ('"+ tLAND001 + "' ,'"+ tLAND002 + "' ,'"+ tLAND003 + "' ,'"+ tKCNT + "' ,'"+ tLAND004 + "' ,'' ,'"+ gSWC002 + "' ,'' ,'' ,'' ,'' ,'"+ tLAND005 + "' ,'"+ tLAND006 + "' ,'"+ tLAND007 + "' ,'"+ tLAND008 + "'); ";

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

            //計畫申請書存檔
            DataTable dt = new DataTable();

            string tFILE001 = "001";

            strSQLRV = "delete SWCFILES";
            strSQLRV = strSQLRV + " Where SWC000 = '" + gSWC000 + "' ";
            strSQLRV = strSQLRV + "   and FILE001 = '" + tFILE001 + "' ";

            dt = (DataTable)ViewState["File001C"];

            if (dt != null)
            {
                int i = 0;

                for (i = 0; i <= (Convert.ToInt32(dt.Rows.Count) - 1); i++)
                {
                    string tFILE000 = (i + 1).ToString();
                    string tFILE003 = dt.Rows[i]["File001003"].ToString().Trim();

                    strSQLRV = strSQLRV + " insert into SWCFILES ";
                    strSQLRV = strSQLRV + " (SWC000,FILE000,FILE001,FILE003,Saveuser,savedate) VALUES ";
                    strSQLRV = strSQLRV + " ('" + gSWC000 + "','"+ tFILE000 + "','" + tFILE001 + "','" + tFILE003 + "','" + ssUserID + "',getdate());  ";
                }

                SqlCommand objCmdItem2 = new SqlCommand(strSQLRV, SWCConn);
                objCmdItem2.ExecuteNonQuery();

                objCmdItem2.Cancel();
                objCmdItem2.Dispose();

                Files001No.Text = i.ToString();
            }
            //更新空間資料
            SBApp.UpdateShape(gSWC002, gSWC027, gSWC028, "0");

            MoveSwcFiles(gSWC000);

            Response.Write("<script>alert('資料已存檔'); location.href='SWC003.aspx?SWCNO="+ gSWC000 + "'; </script>");            
        }

        //Synchronize
        string synfile = " update UploadFileSyn set HAVEPROCESS=0  where SOURCEPC = 'swcdoc' and SOURCEURL like '%"+ gSWC000 + "%' and REQUESTTIME > dateadd(hour,-1,getdate());";
        string allExcSqlStr = RsvSQLStr + qTslmLand + synfile + "";

        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();

            allExcSqlStr = allExcSqlStr + "";

            SqlCommand objCmdTsl = new SqlCommand(allExcSqlStr, TslmConn);
            objCmdTsl.ExecuteNonQuery();
            objCmdTsl.Dispose();            
        }


        SBApp.ViewRecord("水保申請案件", "update", gSWC000);
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
            
            string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
            string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

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
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTSWC029", "TXTSWC029CAD", "TXTSWC030", "TXTSWC080", "TXTSWC101", "TXTSWC110", "TXTSWC101CAD" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTSWC029, TXTSWC029CAD,TXTSWC030, TXTSWC080, TXTSWC101, TXTSWC110, TXTSWC101CAD };
        string csUpLoadField = "TXTSWC029";
        TextBox csUpLoadAppoj = TXTSWC029;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

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

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink,float _FileMaxSize)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string SwcFileName = "";
        string CaseId = TempSwc000.Text + "";
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
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

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
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
        {
            view = "v";
        }

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
        {
            view = "v";
        }

        Button LButton = (Button)sender;
        string LINK = "SWCDT005"+ view + ".aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;

        if (ssDTL05VIEW == "Y") //檢查公會
        {
            LINK = "SWCDT005v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        }
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
        FileUpLoadApp("PDF", TXTFILES001_fileupload, TXTFILES001, "TXTFILES001","", null, Link001, 150); //150MB
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

        TXTFILES001.Text = "";
        Link001.Text = "";
        Link001.NavigateUrl = "";
        //ACItem2c.Text = "";
        //TxtItemNi2.Text = nItem2.ToString();

    }
    protected void ADDLIST01_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";

        string addData01 = DDLDistrict.Text + "";
        string addData02 = DDLSection.Text + "";
        string addData03 = DDLSection2.Text + "";
        string addData04 = TXTNumber.Text + "";

        GBClass001 SBApp = new GBClass001();

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
            GVTBCD.Columns.Add(new DataColumn("土地使用分區", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("土地可利用限度", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("林地類別", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("地質敏感區", typeof(string)));

            ViewState["SwcCadastral"] = GVTBCD;
            tbCadastral = (DataTable)ViewState["SwcCadastral"];            
        }

        DataRow GVTBCDRow = tbCadastral.NewRow();

        GVTBCDRow["序號"] = CDNO.Text;
        GVTBCDRow["區"] = DDLDistrict.SelectedItem.Text;
        GVTBCDRow["段"] = DDLSection.SelectedItem.Text;
        GVTBCDRow["小段"] = DDLSection2.SelectedItem.Text;
        GVTBCDRow["地號"] = TXTNumber.Text;
        GVTBCDRow["土地使用分區"] = DDLCA01.SelectedItem.Text;
        GVTBCDRow["土地可利用限度"] = DDLCA02.SelectedItem.Text;
        GVTBCDRow["林地類別"] = DDLCA03.SelectedItem.Text;
        GVTBCDRow["地質敏感區"] = DDLCA04.SelectedItem.Text;

        tbCadastral.Rows.Add(GVTBCDRow);

        //Store the DataTable in ViewState
        ViewState["SwcCadastral"] = tbCadastral;

        GVCadastral.DataSource = tbCadastral;
        GVCadastral.DataBind();

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

        string exeSqlStr = " delete SWCDTL03 where swc000='" + tSwc000 + "' and dtlC000 ='" + tDtl000 + "' ";

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
                targetPATH = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\審查\\" + doctype + "\\" + sourceFILENAME;

                switch (doctype)
                {
                    case "竣工圖說CAD":
                    case "竣工圖說":
                        targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/竣工圖說/" + doctype + "/" + sourceFILENAME;
                        targetPATH = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\竣工圖說\\" + doctype + "\\" + sourceFILENAME;
                        break;
                    case "掃描檔":
                        targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/掃描檔/掃描檔/" + sourceFILENAME;
                        targetPATH = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\掃描檔\\掃描檔\\" + sourceFILENAME;
                        break;
                    case "審查單位查核表":
                        targetURL = ConfigurationManager.AppSettings["Swcip"] + FileYearS + "/水保申請案件/" + swctype + "/" + caseid + "/審查單位查核表/審查單位查核表/" + sourceFILENAME;
                        targetPATH = "D:\\公用區\\唯讀區\\" + FileYearS + "\\水保申請案件\\" + swctype + "\\" + caseid + "\\審查單位查核表\\審查單位查核表\\" + sourceFILENAME;
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
}