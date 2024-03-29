﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCDT003 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        string ssJobTitle = Session["JobTitle"] + "";
        GBClass001 SBApp = new GBClass001();

        if (rCaseId == "" || ssUserName =="")
        {
            //Response.Redirect("SWC001.aspx");
        }

        if (!IsPostBack)
        {
            if (rCaseId == "" && rDTLId != "AddNew")
            {
                rCaseId = Get03SWC000(rDTLId);
            }
            LBSWC000.Text = rCaseId.Trim() == "" ? rDTLId : rCaseId;

            GenerateDropDownList();
            Data2Page(rCaseId, rDTLId);
        }
        
        //全區供用

        //SBApp.ViewRecord("水土保持施工監督檢查紀錄", "update", "");

        //ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        //Visitor.Text = SBApp.GetVisitorsCount();

        //TextUserName.Text = "";
        if (ssUserName != "")
        {
            //TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用

    }
    private string Get03SWC000(string v2)
    {
        string _returnVall = "";
        string strSQLRV = " select SWC000 from SWCDTL03 ";
        strSQLRV = strSQLRV + " where DTLC000 = '" + v2 + "' ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                _returnVall = readerSWC["SWC000"] + "";
            }
        }
        return _returnVall;
    }
    private void Data2Page(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

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
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC021ID = readeSwc["SWC021ID"] + "";
                string tSWC021 = readeSwc["SWC021"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";

                LBSWC000.Text = v;
                LBSWC005.Text = tSWC005;
                LBSWC007.Text = tSWC007;
                //LBSWC013ID.Text = tSWC013ID;
                //LBSWC013.Text = tSWC013;
                //LBSWC014.Text = tSWC014;
                //LBSWC021.Text = tSWC021;
                //LBSWC021Name.Text = SBApp.GetETUser(tSWC021ID, "OrgName");
                //LBSWC021OrgIssNo.Text = SBApp.GetETUser(tSWC021ID, "OrgIssNo");
                //LBSWC021OrgGUINo.Text = SBApp.GetETUser(tSWC021ID, "OrgGUINo");
                //LBSWC021OrgTel.Text = SBApp.GetETUser(tSWC021ID, "OrgTel");
                //LBSWC038.Text = SBApp.DateView(tSWC038, "00");
                //LBSWC039.Text = tSWC039;
                //LBSWC043.Text = SBApp.DateView(tSWC043, "00");
                //LBSWC044.Text = tSWC044;
                //LBSWC051.Text = SBApp.DateView(tSWC051, "00");
                //LBSWC052.Text = SBApp.DateView(tSWC052, "00");
            }

            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string nIDA = GetDTLAID(v);
                string ssUserName = Session["NAME"] + "";

                LBDTL001.Text = nIDA;
                //TXTDTL004.Text = ssUserName;

            }
            else
            {
                string strSQLRV2 = " select * from SWCDTL03 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and DTLC001 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLC001 = readeDTL["DTLC001"] + "";
                    string tDTLC002 = readeDTL["DTLC002"] + "";
                    string tDTLC003 = readeDTL["DTLC003"] + "";
                    string tDTLC004 = readeDTL["DTLC004"] + "";
                    string tDTLC005 = readeDTL["DTLC005"] + "";
                    string tDTLC006 = readeDTL["DTLC006"] + "";
                    string tDTLC007 = readeDTL["DTLC007"] + "";
                    string tDTLC008 = readeDTL["DTLC008"] + "";
                    string tDTLC009 = readeDTL["DTLC009"] + "";
                    string tDTLC010 = readeDTL["DTLC010"] + "";
                    string tDTLC011 = readeDTL["DTLC011"] + "";
                    string tDTLC012 = readeDTL["DTLC012"] + "";
                    string tDTLC013 = readeDTL["DTLC013"] + "";
                    string tDTLC014 = readeDTL["DTLC014"] + "";
                    string tDTLC015 = readeDTL["DTLC015"] + "";
                    string tDTLC016 = readeDTL["DTLC016"] + "";
                    string tDTLC017 = readeDTL["DTLC017"] + "";
                    string tDTLC018 = readeDTL["DTLC018"] + "";
                    string tDTLC019 = readeDTL["DTLC019"] + "";
                    string tDTLC020 = readeDTL["DTLC020"] + "";
                    string tDTLC021 = readeDTL["DTLC021"] + "";
                    string tDTLC022 = readeDTL["DTLC022"] + "";
                    string tDTLC023 = readeDTL["DTLC023"] + "";
                    string tDTLC024 = readeDTL["DTLC024"] + "";
                    string tDTLC025 = readeDTL["DTLC025"] + "";
                    string tDTLC026 = readeDTL["DTLC026"] + "";
                    string tDTLC027 = readeDTL["DTLC027"] + "";
                    string tDTLC028 = readeDTL["DTLC028"] + "";
                    string tDTLC029 = readeDTL["DTLC029"] + "";
                    string tDTLC030 = readeDTL["DTLC030"] + "";
                    string tDTLC031 = readeDTL["DTLC031"] + "";
                    string tDTLC032 = readeDTL["DTLC032"] + "";
                    string tDTLC033 = readeDTL["DTLC033"] + "";
                    string tDTLC034 = readeDTL["DTLC034"] + "";
                    string tDTLC035 = readeDTL["DTLC035"] + "";
                    string tDTLC036 = readeDTL["DTLC036"] + "";
                    string tDTLC037 = readeDTL["DTLC037"] + "";
                    string tDTLC038 = readeDTL["DTLC038"] + "";
                    string tDTLC039 = readeDTL["DTLC039"] + "";
                    string tDTLC040 = readeDTL["DTLC040"] + "";
                    string tDTLC041 = readeDTL["DTLC041"] + "";
                    string tDTLC042 = readeDTL["DTLC042"] + "";
                    string tDTLC043 = readeDTL["DTLC043"] + "";
                    string tDTLC044 = readeDTL["DTLC044"] + "";
                    string tDTLC045 = readeDTL["DTLC045"] + "";
                    string tDTLC046 = readeDTL["DTLC046"] + "";
                    string tDTLC047 = readeDTL["DTLC047"] + "";
                    string tDTLC048 = readeDTL["DTLC048"] + "";
                    string tDTLC049 = readeDTL["DTLC049"] + "";
                    string tDTLC050 = readeDTL["DTLC050"] + "";
                    string tDTLC051 = readeDTL["DTLC051"] + "";
                    string tDTLC052 = readeDTL["DTLC052"] + "";
                    string tDTLC053 = readeDTL["DTLC053"] + "";
                    string tDTLC054 = readeDTL["DTLC054"] + "";
                    string tDTLC055 = readeDTL["DTLC055"] + "";
                    string tDTLC056 = readeDTL["DTLC056"] + "";
                    string tDTLC057 = readeDTL["DTLC057"] + "";
                    string tDTLC058 = readeDTL["DTLC058"] + "";
                    string tDTLC059 = readeDTL["DTLC059"] + "";
                    string tDTLC060 = readeDTL["DTLC060"] + "";
                    string tDTLC061 = readeDTL["DTLC061"] + "";
                    string tDTLC062 = readeDTL["DTLC062"] + "";
                    string tDTLC063 = readeDTL["DTLC063"] + "";
                    string tDTLC064 = readeDTL["DTLC064"] + "";
                    string tDTLC065 = readeDTL["DTLC065"] + "";
                    string tDTLC066 = readeDTL["DTLC066"] + "";
                    string tDTLC067 = readeDTL["DTLC067"] + "";
                    string tDTLC068 = readeDTL["DTLC068"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";
                    
                    LBDTL001.Text = tDTLC001;
                    TXTDTL002.Text = SBApp.DateView(tDTLC002, "00");
                    //TXTDTL003.Text = tDTLC003;
                    //TXTDTL004.Text = tDTLC004;
                    TXTDTL005.Text = tDTLC005;
                    DDLDTL006.Text = tDTLC006;
                    TXTDTL007.Text = tDTLC007;
                    DDLDTL008.Text = tDTLC008;
                    TXTDTL009.Text = tDTLC009;
                    DDLDTL010.Text = tDTLC010;
                    TXTDTL011.Text = tDTLC011;
                    DDLDTL012.Text = tDTLC012;
                    TXTDTL013.Text = tDTLC013;
                    DDLDTL014.Text = tDTLC014;
                    TXTDTL015.Text = tDTLC015;
                    DDLDTL016.Text = tDTLC016;
                    TXTDTL017.Text = tDTLC017;
                    DDLDTL018.Text = tDTLC018;
                    TXTDTL019.Text = tDTLC019;
                    DDLDTL020.Text = tDTLC020;
                    TXTDTL021.Text = tDTLC021;
                    DDLDTL022.Text = tDTLC022;
                    TXTDTL023.Text = tDTLC023;
                    DDLDTL024.Text = tDTLC024;
                    TXTDTL025.Text = tDTLC025;
                    DDLDTL026.Text = tDTLC026;
                    TXTDTL027.Text = tDTLC027;
                    DDLDTL028.Text = tDTLC028;
                    TXTDTL029.Text = tDTLC029;
                    DDLDTL030.Text = tDTLC030;
                    TXTDTL031.Text = tDTLC031;
                    DDLDTL032.Text = tDTLC032;
                    TXTDTL033.Text = tDTLC033;
                    DDLDTL034.Text = tDTLC034;
                    TXTDTL035.Text = tDTLC035;
                    DDLDTL036.Text = tDTLC036;
                    TXTDTL037.Text = tDTLC037;
                    DDLDTL038.Text = tDTLC038;
                    TXTDTL039.Text = tDTLC039;
                    DDLDTL040.Text = tDTLC040;
                    TXTDTL041.Text = tDTLC041;
                    DDLDTL042.Text = tDTLC042;
                    TXTDTL043.Text = tDTLC043;
                    DDLDTL044.Text = tDTLC044;
                    TXTDTL045.Text = tDTLC045;
                    DDLDTL046.Text = tDTLC046;
                    TXTDTL047.Text = tDTLC047;
                    TXTDTL048.Text = tDTLC048.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL049.Text = SBApp.DateView(tDTLC049, "00");
                    TXTDTL050.Text = tDTLC050.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL051.Text = tDTLC051.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL052.Text = tDTLC052.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    if (tDTLC053 != "")
                    {
                        DDLDTL053.Text = "前次監督檢查缺失之複查&nbsp&nbsp&nbsp(&nbsp&nbsp&nbsp是否已改正：" + tDTLC053;
                        TXTDTL054.Text = tDTLC054 + "&nbsp&nbsp&nbsp)";
                    }
                    TXTDTL055.Text = tDTLC055.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    //TXTDTL056.Text = tDTLC056;
                    //TXTDTL057.Text = tDTLC057;
                    //TXTDTL058.Text = tDTLC058;
                    //TXTDTL059.Text = tDTLC059;
                    //TXTDTL060.Text = tDTLC060;
                    //TXTDTL061.Text = tDTLC061;
                    //TXTDTL062.Text = tDTLC062;
                    //TXTDTL063.Text = tDTLC063;
                    //TXTDTL064.Text = tDTLC064;
                    //TXTDTL065.Text = tDTLC065;
                    //TXTDTL066.Text = tDTLC066;
                    //TXTDTL067.Text = tDTLC067;
                    //TXTDTL068.Text = tDTLC068;


                    //點擊放大圖片類處理
                    string[] arrayFileName2 = new string[] { tDTLC056 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj2 = new System.Web.UI.WebControls.HyperLink[] { HyperLink056 };

                    for (int i = 0; i < arrayFileName2.Length; i++)
                    {
                        string strFileName = arrayFileName2[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj2[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                            ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                        }
                    }
                }
            }

        }
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        //string sDTLC000 = LBDTL001.Text + "";
        //string sDTLC002 = TXTDTL002.Text + "";
        //string sDTLC003 = TXTDTL003.Text + "";
        //string sDTLC004 = TXTDTL004.Text + "";
        //string sDTLC005 = TXTDTL005.Text + "";
        //string sDTLC006 = DDLDTL006.SelectedValue + "";
        //string sDTLC007 = TXTDTL007.Text + "";
        //string sDTLC008 = DDLDTL008.SelectedValue + "";
        //string sDTLC009 = TXTDTL009.Text + "";
        //string sDTLC010 = DDLDTL010.SelectedValue + "";
        //string sDTLC011 = TXTDTL011.Text + "";
        //string sDTLC012 = DDLDTL012.SelectedValue + "";
        //string sDTLC013 = TXTDTL013.Text + "";
        //string sDTLC014 = DDLDTL014.SelectedValue + "";
        //string sDTLC015 = TXTDTL015.Text + "";
        //string sDTLC016 = DDLDTL016.SelectedValue + "";
        //string sDTLC017 = TXTDTL017.Text + "";
        //string sDTLC018 = DDLDTL018.SelectedValue + "";
        //string sDTLC019 = TXTDTL019.Text + "";
        //string sDTLC020 = DDLDTL020.SelectedValue + "";
        //string sDTLC021 = TXTDTL021.Text + "";
        //string sDTLC022 = DDLDTL022.SelectedValue + "";
        //string sDTLC023 = TXTDTL023.Text + "";
        //string sDTLC024 = DDLDTL024.SelectedValue + "";
        //string sDTLC025 = TXTDTL025.Text + "";
        //string sDTLC026 = DDLDTL026.SelectedValue + "";
        //string sDTLC027 = TXTDTL027.Text + "";
        //string sDTLC028 = DDLDTL028.SelectedValue + "";
        //string sDTLC029 = TXTDTL029.Text + "";
        //string sDTLC030 = DDLDTL030.SelectedValue + "";
        //string sDTLC031 = TXTDTL031.Text + "";
        //string sDTLC032 = DDLDTL032.SelectedValue + "";
        //string sDTLC033 = TXTDTL033.Text + "";
        //string sDTLC034 = DDLDTL034.SelectedValue + "";
        //string sDTLC035 = TXTDTL035.Text + "";
        //string sDTLC036 = DDLDTL036.SelectedValue + "";
        //string sDTLC037 = TXTDTL037.Text + "";
        //string sDTLC038 = DDLDTL038.SelectedValue + "";
        //string sDTLC039 = TXTDTL039.Text + "";
        //string sDTLC040 = DDLDTL040.SelectedValue + "";
        //string sDTLC041 = TXTDTL041.Text + "";
        //string sDTLC042 = DDLDTL042.SelectedValue + "";
        //string sDTLC043 = TXTDTL043.Text + "";
        //string sDTLC044 = DDLDTL044.SelectedValue + "";
        //string sDTLC045 = TXTDTL045.Text + "";
        //string sDTLC046 = DDLDTL046.SelectedValue + "";
        //string sDTLC047 = TXTDTL047.Text + "";
        //string sDTLC048 = TXTDTL048.Text + "";
        //string sDTLC049 = TXTDTL049.Text + "";
        //string sDTLC050 = TXTDTL050.Text + "";
        //string sDTLC051 = TXTDTL051.Text + "";
        //string sDTLC052 = TXTDTL052.Text + "";
        //string sDTLC053 = DDLDTL053.SelectedValue + "";
        //string sDTLC054 = TXTDTL054.Text + "";
        //string sDTLC055 = TXTDTL055.Text + "";
        //string sDTLC056 = TXTDTL056.Text + "";
        //string sDTLC057 = TXTDTL057.Text + "";
        //string sDTLC058 = TXTDTL058.Text + "";
        //string sDTLC059 = TXTDTL059.Text + "";
        //string sDTLC060 = TXTDTL060.Text + "";
        //string sDTLC061 = TXTDTL061.Text + "";
        //string sDTLC062 = TXTDTL062.Text + "";
        //string sDTLC063 = TXTDTL063.Text + "";
        //string sDTLC064 = TXTDTL064.Text + "";
        //string sDTLC065 = TXTDTL065.Text + "";
        //string sDTLC066 = TXTDTL066.Text + "";
        //string sDTLC067 = TXTDTL067.Text + "";
        //string sDTLC068 = TXTDTL068.Text + "";

        //if (sDTLC048.Length > 500) { sDTLC048 = sDTLC048.Substring(0, 500); }
        //if (sDTLC050.Length > 500) { sDTLC050 = sDTLC050.Substring(0, 500); }
        //if (sDTLC051.Length > 500) { sDTLC051 = sDTLC051.Substring(0, 500); }
        //if (sDTLC052.Length > 500) { sDTLC052 = sDTLC052.Substring(0, 500); }
        //if (sDTLC055.Length > 500) { sDTLC055 = sDTLC055.Substring(0, 500); }
        //if (sDTLC058.Length > 255) { sDTLC058 = sDTLC058.Substring(0, 255); }
        //if (sDTLC060.Length > 255) { sDTLC060 = sDTLC060.Substring(0, 255); }
        //if (sDTLC062.Length > 255) { sDTLC062 = sDTLC062.Substring(0, 255); }
        //if (sDTLC064.Length > 255) { sDTLC064 = sDTLC064.Substring(0, 255); }
        //if (sDTLC066.Length > 255) { sDTLC066 = sDTLC066.Substring(0, 255); }
        //if (sDTLC068.Length > 255) { sDTLC068 = sDTLC068.Substring(0, 255); }

        //GBClass001 SBApp = new GBClass001();

        //string sEXESQLSTR = "";
        //string sEXESQLUPD = "";

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        //{
        //    SwcConn.Open();

        //    string strSQLRV = " select * from SWCDTL03 ";
        //    strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
        //    strSQLRV = strSQLRV + "   and DTLC000 = '" + sDTLC000 + "' ";

        //    SqlDataReader readeSwc;
        //    SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
        //    readeSwc = objCmdSwc.ExecuteReader();

        //    if (!readeSwc.HasRows)
        //    {
        //        sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL03 (SWC000,DTLC000) VALUES ('" + sSWC000 + "','" + sDTLC000 + "');";
        //    }
        //    readeSwc.Close();
        //    objCmdSwc.Dispose();

        //    sEXESQLSTR = sEXESQLSTR + " Update SWCDTL03 Set ";

        //    sEXESQLSTR = sEXESQLSTR + " DTLC001 = DTLC000, ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC002 ='" + sDTLC002 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC003 ='" + sDTLC003 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC004 ='" + sDTLC004 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC005 ='" + sDTLC005 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC006 ='" + sDTLC006 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC007 ='" + sDTLC007 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC008 ='" + sDTLC008 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC009 ='" + sDTLC009 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC010 ='" + sDTLC010 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC011 ='" + sDTLC011 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC012 ='" + sDTLC012 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC013 ='" + sDTLC013 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC014 ='" + sDTLC014 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC015 ='" + sDTLC015 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC016 ='" + sDTLC016 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC017 ='" + sDTLC017 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC018 ='" + sDTLC018 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC019 ='" + sDTLC019 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC020 ='" + sDTLC020 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC021 ='" + sDTLC021 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC022 ='" + sDTLC022 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC023 ='" + sDTLC023 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC024 ='" + sDTLC024 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC025 ='" + sDTLC025 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC026 ='" + sDTLC026 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC027 ='" + sDTLC027 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC028 ='" + sDTLC028 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC029 ='" + sDTLC029 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC030 ='" + sDTLC030 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC031 ='" + sDTLC031 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC032 ='" + sDTLC032 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC033 ='" + sDTLC033 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC034 ='" + sDTLC034 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC035 ='" + sDTLC035 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC036 ='" + sDTLC036 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC037 ='" + sDTLC037 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC038 ='" + sDTLC038 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC039 ='" + sDTLC039 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC040 ='" + sDTLC040 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC041 ='" + sDTLC041 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC042 ='" + sDTLC042 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC043 ='" + sDTLC043 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC044 ='" + sDTLC044 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC045 ='" + sDTLC045 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC046 ='" + sDTLC046 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC047 ='" + sDTLC047 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC048 ='" + sDTLC048 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC049 ='" + sDTLC049 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC050 ='" + sDTLC050 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC051 ='" + sDTLC051 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC052 ='" + sDTLC052 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC053 ='" + sDTLC053 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC054 ='" + sDTLC054 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC055 ='" + sDTLC055 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC056 ='" + sDTLC056 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC057 ='" + sDTLC057 + "', ";            
        //    sEXESQLSTR = sEXESQLSTR + " DTLC058 ='" + sDTLC058 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC059 ='" + sDTLC059 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC060 ='" + sDTLC060 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC061 ='" + sDTLC061 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC062 ='" + sDTLC062 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC063 ='" + sDTLC063 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC064 ='" + sDTLC064 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC065 ='" + sDTLC065 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC066 ='" + sDTLC066 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC067 ='" + sDTLC067 + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " DTLC068 ='" + sDTLC068 + "', ";
            
        //    sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
        //    sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
        //    sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
        //    sEXESQLSTR = sEXESQLSTR + "   and DTLC000 = '" + sDTLC000 + "'";

        //    sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
        //    sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
        //    sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
        //    sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";

        //    SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
        //    objCmdUpd.ExecuteNonQuery();
        //    objCmdUpd.Dispose();

        //    //上傳檔案…
        //    UpLoadTempFileMoveChk(sSWC000);

        //    string vCaseID = Request.QueryString["SWCNO"] + "";
        //    Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);

        //}
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        //string[] arryUpLoadField = new string[] { "TXTDTL056", "TXTDTL057", "TXTDTL059", "TXTDTL061", "TXTDTL063", "TXTDTL065", "TXTDTL067" };
        //TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL056, TXTDTL057, TXTDTL059, TXTDTL061, TXTDTL063, TXTDTL065, TXTDTL067 };
        //string csUpLoadField = "TXTDTL056";
        //TextBox csUpLoadAppoj = TXTDTL056;

        //string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        //string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

        //folderExists = Directory.Exists(SwcCaseFolderPath);
        //if (folderExists == false)
        //{
        //    Directory.CreateDirectory(SwcCaseFolderPath);
        //}

        //folderExists = Directory.Exists(SwcCaseFolderPath + CaseId);
        //if (folderExists == false)
        //{
        //    Directory.CreateDirectory(SwcCaseFolderPath + CaseId);
        //}


        //for (int i = 0; i < arryUpLoadField.Length; i++)
        //{
        //    csUpLoadField = arryUpLoadField[i];
        //    csUpLoadAppoj = arryUpLoadAppoj[i];

        //    if (Session[csUpLoadField] + "" == "有檔案")
        //    {
        //        Boolean fileExists;
        //        string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;
        //        string SwcCaseFilePath = SwcCaseFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;

        //        fileExists = File.Exists(TempFilePath);
        //        if (fileExists)
        //        {
        //            if (File.Exists(SwcCaseFilePath))
        //            {
        //                File.Delete(SwcCaseFilePath);
        //            }
        //            File.Move(TempFilePath, SwcCaseFilePath);
        //        }

        //    }
        //}


    }
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
    }
    protected void GenerateDropDownList()
    {
        //string[] array_DTL006 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作","無此項" };
        //DDLDTL006.DataSource = array_DTL006;
        //DDLDTL006.DataBind();

        //string[] array_DTL008 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL008.DataSource = array_DTL008;
        //DDLDTL008.DataBind();

        //string[] array_DTL010 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL010.DataSource = array_DTL010;
        //DDLDTL010.DataBind();

        //string[] array_DTL012 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL012.DataSource = array_DTL012;
        //DDLDTL012.DataBind();

        //string[] array_DTL014 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL014.DataSource = array_DTL014;
        //DDLDTL014.DataBind();

        //string[] array_DTL016 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL016.DataSource = array_DTL016;
        //DDLDTL016.DataBind();

        //string[] array_DTL018 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL018.DataSource = array_DTL018;
        //DDLDTL018.DataBind();

        //string[] array_DTL020 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL020.DataSource = array_DTL020;
        //DDLDTL020.DataBind();

        //string[] array_DTL022 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL022.DataSource = array_DTL022;
        //DDLDTL022.DataBind();

        //string[] array_DTL024 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL024.DataSource = array_DTL024;
        //DDLDTL024.DataBind();

        //string[] array_DTL026 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL026.DataSource = array_DTL026;
        //DDLDTL026.DataBind();

        //string[] array_DTL028 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL028.DataSource = array_DTL028;
        //DDLDTL028.DataBind();

        //string[] array_DTL030 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL030.DataSource = array_DTL030;
        //DDLDTL030.DataBind();

        //string[] array_DTL032 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL032.DataSource = array_DTL032;
        //DDLDTL032.DataBind();

        //string[] array_DTL034 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL034.DataSource = array_DTL034;
        //DDLDTL034.DataBind();

        //string[] array_DTL036 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL036.DataSource = array_DTL036;
        //DDLDTL036.DataBind();

        //string[] array_DTL038 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL038.DataSource = array_DTL038;
        //DDLDTL038.DataBind();

        //string[] array_DTL040 = new string[] { "", "依計畫施作", "未依計畫施作", "尚未施作", "無此項" };
        //DDLDTL040.DataSource = array_DTL040;
        //DDLDTL040.DataBind();

        //string[] array_DTL042 = new string[] { "", "是", "否" };
        //DDLDTL042.DataSource = array_DTL042;
        //DDLDTL042.DataBind();

        //string[] array_DTL044 = new string[] { "", "是", "否" };
        //DDLDTL044.DataSource = array_DTL044;
        //DDLDTL044.DataBind();

        //string[] array_DTL046 = new string[] { "", "是", "否" };
        //DDLDTL046.DataSource = array_DTL046;
        //DDLDTL046.DataBind();

        //string[] array_DTL053 = new string[] { "", "是", "否", "其他" };
        //DDLDTL053.DataSource = array_DTL053;
        //DDLDTL053.DataBind();
        
        //TXTDTL055.Text = "(一) 檢查單位及人員：" + System.Environment.NewLine + "(二) 會同檢查單位及人員：" + System.Environment.NewLine + "(三) 承辦監造技師：" + System.Environment.NewLine + "(四) 水土保持義務人：";
    }
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RC" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RC" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLC000) AS MAXID from SWCDTL03 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLC000,7) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 3, 3)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(3, '0');
                }
            }
        }
        return _ReturnVal;
    }

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
    {
        //GBClass001 MyBassAppPj = new GBClass001();
        //string SwcFileName = "";
        //string CaseId = LBSWC000.Text + "";

        //if (UpLoadBar.HasFile)
        //{
        //    string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

        //    string extension = Path.GetExtension(filename).ToLowerInvariant();

        //    // 判斷是否為允許上傳的檔案附檔名

        //    switch (ChkType)
        //    {
        //        case "PIC2":
        //        case "PIC":
        //            List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

        //            if (allowedExtextsion01.IndexOf(extension) == -1)
        //            {
        //                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
        //                return;
        //            }
        //            break;
        //        case "DOC":
        //            List<string> allowedExtextsion02 = new List<string> { ".jpg", ".png", "doc", "pdf", "dwg", "dxf" };

        //            if (allowedExtextsion02.IndexOf(extension) == -1)
        //            {
        //                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG DOC PDF DWG DXF 檔案格式上傳，謝謝!!");
        //                return;
        //            }
        //            break;

        //    }

        //    // 限制檔案大小，限制為 10 MB
        //    int filesize = UpLoadBar.PostedFile.ContentLength;

        //    if (filesize > 10000000)
        //    {
        //        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10 Mb 以下檔案上傳，謝謝!!");
        //        return;
        //    }

        //    // 檢查 Server 上該資料夾是否存在，不存在就自動建立
        //    string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

        //    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

        //    Session[UpLoadStr] = "有檔案";
        //    //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
        //    SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
        //    UpLoadText.Text = SwcFileName;

        //    // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
        //    // 使用 Path.Combine 來集合路徑的優點
        //    //  以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
        //    //  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
        //    //  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
        //    //  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合

        //    string serverFilePath = Path.Combine(serverDir, SwcFileName);
        //    string fileNameOnly = Path.GetFileNameWithoutExtension(SwcFileName);
        //    int fileCount = 1;

        //    //while (File.Exists(serverFilePath))
        //    //{
        //    //    // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
        //    //    filename = string.Concat(fileNameOnly, "_", fileCount, extension);
        //    //    serverFilePath = Path.Combine(serverDir, filename);
        //    //    fileCount++;
        //    //}

        //    // 把檔案傳入指定的 Server 內路徑
        //    try
        //    {
        //        UpLoadBar.SaveAs(serverFilePath);
        //        //error_msg.Text = "檔案上傳成功";

        //        switch (ChkType)
        //        {
        //            case "PIC2":
        //                UpLoadLink.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
        //                UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
        //                break;

        //            case "PIC":
        //                UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
        //                //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

        //                imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
        //                break;

        //            case "DOC":
        //                UpLoadLink.Text = SwcFileName;
        //                UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
        //                UpLoadLink.Visible = true;
        //                break;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //error_msg.Text = "檔案上傳失敗";
        //    }


        //}
        //else
        //{
        //    Session[UpLoadStr] = "";
        //}

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
    protected void TXTDTL056_fileuploadok_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";
        //error_msg.Text = "";
        //FileUpLoadApp("PIC2", TXTDTL056_fileupload, TXTDTL056, "TXTDTL056", "_" + rDTLNO + "_03_sign", null, HyperLink056);
    }

    protected void TXTDTL057_fileuploadok_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";
        //error_msg.Text = "";
        //FileUpLoadApp("PIC", TXTDTL057_fileupload, TXTDTL057, "TXTDTL057", "_" + rDTLNO + "_03_photo1", TXTDTL057_img, null);

    }

    protected void TXTDTL059_fileuploadok_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";

        //error_msg.Text = "";

        //FileUpLoadApp("PIC", TXTDTL059_fileupload, TXTDTL059, "TXTDTL059", "_" + rDTLNO + "_03_photo2", TXTDTL059_img, null);

    }

    protected void TXTDTL061_fileuploadok_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";

        //error_msg.Text = "";

        //FileUpLoadApp("PIC", TXTDTL061_fileupload, TXTDTL061, "TXTDTL061", "_" + rDTLNO + "_03_photo3", TXTDTL061_img, null);

    }

    protected void TXTDTL063_fileuploadok_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";

        //error_msg.Text = "";

        //FileUpLoadApp("PIC", TXTDTL063_fileupload, TXTDTL063, "TXTDTL063", "_" + rDTLNO + "_03_photo4", TXTDTL063_img, null);

    }

    protected void TXTDTL065_fileuploadok_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";

        //error_msg.Text = "";

        //FileUpLoadApp("PIC", TXTDTL065_fileupload, TXTDTL065, "TXTDTL065", "_" + rDTLNO + "_03_photo5", TXTDTL065_img, null);

    }

    protected void TXTDTL067_fileuploadok_Click(object sender, EventArgs e)
    {

        //string rDTLNO = LBDTL001.Text + "";

        //error_msg.Text = "";

        //FileUpLoadApp("PIC", TXTDTL067_fileupload, TXTDTL067, "TXTDTL067", "_" + rDTLNO + "_03_photo6", TXTDTL067_img, null);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        //string sSWC000 = LBSWC000.Text;
        //string sDTLG000 = LBDTL001.Text + "";

        //string sEXESQLSTR = "";

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        //{
        //    SwcConn.Open();

        //    sEXESQLSTR = sEXESQLSTR + " Update SWCDTL03 Set ";
        //    sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
        //    sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
        //    sEXESQLSTR = sEXESQLSTR + "   and DTLC000 = '" + sDTLG000 + "'";

        //    SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
        //    objCmdUpd.ExecuteNonQuery();
        //    objCmdUpd.Dispose();
        //}

        //SaveCase_Click(sender, e);

    }
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        //string csCaseID = LBSWC000.Text + "";
        //string csCaseID2 = LBDTL001.Text + "";
        //string strSQLClearFieldValue = "";

        ////從資料庫取得資料

        ////ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        ////using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        //ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //SqlConnection ConnERR = new SqlConnection();
        //ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        //ConnERR.Open();

        //strSQLClearFieldValue = " update SWCDTL03 set ";
        //strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        //strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        //strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLC000 = '" + csCaseID2 + "' ";

        //SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        //objCmdRV.ExecuteNonQuery();
        //objCmdRV.Dispose();

        //ConnERR.Close();

        ////刪實體檔
        //string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        //string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

        //string DelFileName = ImgText.Text;
        //string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        //string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + ImgText.Text;

        //try
        //{
        //    if (File.Exists(TempFileFullPath))
        //    {
        //        File.Delete(TempFileFullPath);
        //    }
        //}
        //catch
        //{
        //}
        //try
        //{
        //    if (File.Exists(FileFullPath))
        //    {
        //        File.Delete(FileFullPath);
        //    }
        //}
        //catch
        //{
        //}

        //switch (DelType)
        //{
        //    case "PIC":
        //        ImgView.Attributes.Clear();
        //        ImgView.ImageUrl = "";
        //        ImgView.Width = NoneWidth;
        //        ImgView.Height = NoneHeight;
        //        break;
        //    case "CAD":
        //    case "PDF":
        //    case "DOC":
        //        FileLink.Text = "";
        //        FileLink.NavigateUrl = "";
        //        FileLink.Visible = false;
        //        break;
        //}
        ////畫面顯示、值的處理
        //ImgText.Text = "";
        //Session[AspxFeildId] = "";

    }
    protected void TXTDTL056_fileuploaddel_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";
        //error_msg.Text = "";
        //DeleteUpLoadFile("PIC", TXTDTL056, TXTDTL056_img, null, "DTLC056", "TXTDTL056", 320, 180);
    }
    protected void TXTDTL057_fileuploaddel_Click(object sender, EventArgs e)
    {
        //string rDTLNO = LBDTL001.Text + "";
        //error_msg.Text = "";
        //DeleteUpLoadFile("PIC", TXTDTL057, TXTDTL057_img, null, "DTLC057", "TXTDTL057", 320, 180);
    }
    protected void TXTDTL059_fileuploaddel_Click(object sender, EventArgs e)
    {
    //    string rDTLNO = LBDTL001.Text + "";
    //    error_msg.Text = "";
    //    DeleteUpLoadFile("PIC", TXTDTL059, TXTDTL059_img, null, "DTLC059", "TXTDTL059", 320, 180);
    }

    protected void TXTDTL061_fileuploaddel_Click(object sender, EventArgs e)
    {
    //    string rDTLNO = LBDTL001.Text + "";
    //    error_msg.Text = "";
    //    DeleteUpLoadFile("PIC", TXTDTL061, TXTDTL061_img, null, "DTLC061", "TXTDTL061", 320, 180);
    }

    protected void TXTDTL063_fileuploaddel_Click(object sender, EventArgs e)
    {
    //    string rDTLNO = LBDTL001.Text + "";
    //    error_msg.Text = "";
    //    DeleteUpLoadFile("PIC", TXTDTL063, TXTDTL063_img, null, "DTLC063", "TXTDTL063", 320, 180);
    }

    protected void TXTDTL065_fileuploaddel_Click(object sender, EventArgs e)
    {
    //    string rDTLNO = LBDTL001.Text + "";
    //    error_msg.Text = "";
    //    DeleteUpLoadFile("PIC", TXTDTL065, TXTDTL065_img, null, "DTLC065", "TXTDTL065", 320, 180);
    }

    protected void TXTDTL067_fileuploaddel_Click(object sender, EventArgs e)
    {
    //    string rDTLNO = LBDTL001.Text + "";
    //    error_msg.Text = "";
    //    DeleteUpLoadFile("PIC", TXTDTL067, TXTDTL067_img, null, "DTLC067", "TXTDTL067", 320, 180);
    }
}