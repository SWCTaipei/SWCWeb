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

public partial class SWCDOC_SWCDT007 : System.Web.UI.Page
{
	
	string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/";
    //string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/";
    //string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
	protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId.Trim() == "" && rDTLId.Trim() == "")
            Response.Redirect("SWC001.aspx");

        if (!IsPostBack)
        {
            C20.swcLogRC("SWCDT007", "設施維護檢查表", "詳情", "瀏覽", rCaseId + "," + rDTLId);
            GenerateDropDownList();
            Data2Page(rCaseId, rDTLId);
			
			if(ssUserType == "09") DataLock.Visible = false;
        }
        #region 全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        #endregion
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
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";

                LBSWC000.Text = v;
                LBSWC005.Text = tSWC005;
                LBSWC013.Text = tSWC013;
                LBSWC013TEL.Text = tSWC013TEL;
                LBSWC014.Text = tSWC014;
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
                string strSQLRV2 = " select * from SWCDTL07 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   AND DTLG000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLG001 = readeDTL["DTLG001"] + "";
                    string tDTLG002 = readeDTL["DTLG002"] + "";
                    string tDTLG003 = readeDTL["DTLG003"] + "";
                    string tDTLG004 = readeDTL["DTLG004"] + "";
                    string tDTLG005 = readeDTL["DTLG005"] + "";
                    string tDTLG006 = readeDTL["DTLG006"] + "";
                    string tDTLG007 = readeDTL["DTLG007"] + "";
                    string tDTLG008 = readeDTL["DTLG008"] + "";
                    string tDTLG009 = readeDTL["DTLG009"] + "";
                    string tDTLG010 = readeDTL["DTLG010"] + "";
                    string tDTLG011 = readeDTL["DTLG011"] + "";
                    string tDTLG012 = readeDTL["DTLG012"] + "";
                    string tDTLG013 = readeDTL["DTLG013"] + "";
                    string tDTLG014 = readeDTL["DTLG014"] + "";
                    string tDTLG015 = readeDTL["DTLG015"] + "";
                    string tDTLG016 = readeDTL["DTLG016"] + "";
                    string tDTLG017 = readeDTL["DTLG017"] + "";
                    string tDTLG018 = readeDTL["DTLG018"] + "";
                    string tDTLG019 = readeDTL["DTLG019"] + "";
                    string tDTLG020 = readeDTL["DTLG020"] + "";
                    string tDTLG021 = readeDTL["DTLG021"] + "";
                    string tDTLG022 = readeDTL["DTLG022"] + "";
                    string tDTLG023 = readeDTL["DTLG023"] + "";
                    string tDTLG024 = readeDTL["DTLG024"] + "";
                    string tDTLG025 = readeDTL["DTLG025"] + "";
                    string tDTLG026 = readeDTL["DTLG026"] + "";
                    string tDTLG027 = readeDTL["DTLG027"] + "";
                    string tDTLG028 = readeDTL["DTLG028"] + "";
                    string tDTLG029 = readeDTL["DTLG029"] + "";
                    string tDTLG030 = readeDTL["DTLG030"] + "";
                    string tDTLG031 = readeDTL["DTLG031"] + "";
                    string tDTLG032 = readeDTL["DTLG032"] + "";
                    string tDTLG033 = readeDTL["DTLG033"] + "";
                    string tDTLG034 = readeDTL["DTLG034"] + "";
                    string tDTLG035 = readeDTL["DTLG035"] + "";
                    string tDTLG036 = readeDTL["DTLG036"] + "";
                    string tDTLG037 = readeDTL["DTLG037"] + "";
                    string tDTLG038 = readeDTL["DTLG038"] + "";
                    string tDTLG039 = readeDTL["DTLG039"] + "";
                    string tDTLG040 = readeDTL["DTLG040"] + "";
                    string tDTLG041 = readeDTL["DTLG041"] + "";
                    string tDTLG042 = readeDTL["DTLG042"] + "";
                    string tDTLG043 = readeDTL["DTLG043"] + "";
                    string tDTLG044 = readeDTL["DTLG044"] + "";
                    string tDTLG045 = readeDTL["DTLG045"] + "";
                    string tDTLG046 = readeDTL["DTLG046"] + "";
                    string tDTLG047 = readeDTL["DTLG047"] + "";
                    string tDTLG048 = readeDTL["DTLG048"] + "";
                    string tDTLG049 = readeDTL["DTLG049"] + "";
                    string tDTLG050 = readeDTL["DTLG050"] + "";
                    string tDTLG051 = readeDTL["DTLG051"] + "";
                    string tDTLG052 = readeDTL["DTLG052"] + "";
                    string tDTLG053 = readeDTL["DTLG053"] + "";
                    string tDTLG054 = readeDTL["DTLG054"] + "";
                    string tDTLG055 = readeDTL["DTLG055"] + "";
                    string tDTLG056 = readeDTL["DTLG056"] + "";
                    string tDTLG057 = readeDTL["DTLG057"] + "";
                    string tDTLG058 = readeDTL["DTLG058"] + "";
                    string tDTLG059 = readeDTL["DTLG059"] + "";
                    string tDTLG060 = readeDTL["DTLG060"] + "";
                    string tDTLG061 = readeDTL["DTLG061"] + "";
                    string tDTLG062 = readeDTL["DTLG062"] + "";
                    string tDTLG063 = readeDTL["DTLG063"] + "";
                    string tDTLG064 = readeDTL["DTLG064"] + "";
                    string tDTLG065 = readeDTL["DTLG065"] + "";
                    string tDTLG066 = readeDTL["DTLG066"] + "";
                    string tDTLG067 = readeDTL["DTLG067"] + "";
                    string tDTLG068 = readeDTL["DTLG068"] + "";
                    string tDTLG069 = readeDTL["DTLG069"] + "";
                    string tDTLG070 = readeDTL["DTLG070"] + "";
                    string tDTLG071 = readeDTL["DTLG071"] + "";
                    string tDTLG072 = readeDTL["DTLG072"] + "";
                    string tDTLG073 = readeDTL["DTLG073"] + "";
                    string tDTLG074 = readeDTL["DTLG074"] + "";
                    string tDTLG075 = readeDTL["DTLG075"] + "";
                    string tDTLG076 = readeDTL["DTLG076"] + "";
                    string tDTLG077 = readeDTL["DTLG077"] + "";
                    string tDTLG078 = readeDTL["DTLG078"] + "";
                    string tDTLG079 = readeDTL["DTLG079"] + "";
                    string tDTLG080 = readeDTL["DTLG080"] + "";
                    string tDTLG081 = readeDTL["DTLG081"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

                    CHKDTL011.Checked = false;
                    CHKDTL013.Checked = false;
                    CHKDTL015.Checked = false;
                    CHKDTL017.Checked = false;
                    CHKDTL019.Checked = false;
                    CHKDTL021.Checked = false;

                    LBDTL001.Text = tDTLG001;
                    TXTDTL002.Text = SBApp.DateView(tDTLG002, "00");
                    
                    TXTDTL004.Text = tDTLG004;
                    TXTDTL005.Text = tDTLG005;
                    TXTDTL006.Text = tDTLG006;
                    TXTDTL007.Text = tDTLG007;
                    TXTDTL008.Text = tDTLG008;
                    DDLDTL009.SelectedValue = tDTLG009;
                    TXTDTL010.Text = tDTLG010;
                    TXTDTL012.Text = tDTLG012;
                    TXTDTL014.Text = tDTLG014;
                    TXTDTL016.Text = tDTLG016;
                    TXTDTL018.Text = tDTLG018;
                    TXTDTL020.Text = tDTLG020;
                    TXTDTL022.Text = tDTLG022;
                    TXTDTL023.Text = tDTLG023;
                    TXTDTL024.Text = tDTLG024;
                    TXTDTL026.Text = tDTLG026;
                    DDLDTL027.SelectedValue = tDTLG027;
                    DDLDTL028.SelectedValue = tDTLG028;
                    DDLDTL030.SelectedValue = tDTLG030;
                    TXTDTL031.Text = tDTLG031;
                    DDLDTL032.SelectedValue = tDTLG032;
                    DDLDTL033.SelectedValue = tDTLG033;
                    DDLDTL035.Text = tDTLG035;
                    DDLDTL037.Text = tDTLG037;
                    DDLDTL038.Text = tDTLG038;
                    TXTDTL039.Text = tDTLG039;
                    DDLDTL040.SelectedValue = tDTLG040;
                    DDLDTL042.SelectedValue = tDTLG042;
                    DDLDTL044.SelectedValue = tDTLG044;
                    TXTDTL045.Text = tDTLG045;
                    TXTDTL047.Text = tDTLG047;
                    TXTDTL049.Text = tDTLG049;
                    TXTDTL051.Text = tDTLG051;
                    TXTDTL054.Text = SBApp.DateView(tDTLG054, "00");
                    TXTDTL056.Text = tDTLG056;
                    DDLDTL058.SelectedValue = tDTLG058;
                    TXTDTL059.Text = tDTLG059;
                    TXTDTL060.Text = tDTLG060;
                    DDLDTL061.SelectedValue = tDTLG061;
                    TXTDTL062.Text = tDTLG062;
                    TXTDTL063.Text = tDTLG063;
                    DDLDTL064.SelectedValue = tDTLG064;
                    TXTDTL065.Text = tDTLG065;
                    TXTDTL066.Text = tDTLG066;
                    DDLDTL067.SelectedValue = tDTLG067;
                    TXTDTL068.Text = tDTLG068;
                    TXTDTL069.Text = tDTLG069;
                    DDLDTL070.SelectedValue = tDTLG070;
                    TXTDTL071.Text = tDTLG071;
                    TXTDTL072.Text = tDTLG072;
                    DDLDTL073.SelectedValue = tDTLG073;
                    TXTDTL074.Text = tDTLG074;
                    TXTDTL075.Text = tDTLG075;
                    DDLDTL076.SelectedValue = tDTLG076;
                    TXTDTL077.Text = tDTLG077;
                    TXTDTL078.Text = tDTLG078;
                    TXTDTL079.Text = tDTLG079;
                    TXTDTL080.Text = tDTLG080;
                    TXTDTL081.Text = tDTLG081;

                    if (tDTLG011 == "1") { CHKDTL011.Checked = true; }
                    if (tDTLG013 == "1") { CHKDTL013.Checked = true; }
                    if (tDTLG015 == "1") { CHKDTL015.Checked = true; }
                    if (tDTLG017 == "1") { CHKDTL017.Checked = true; }
                    if (tDTLG019 == "1") { CHKDTL019.Checked = true; }
                    if (tDTLG021 == "1") { CHKDTL021.Checked = true; }
                    if (tDTLG025 == "1") { CHKDTL025.Checked = true; }
                    if (tDTLG046 == "1") { CHKDTL046.Checked = true; }
                    if (tDTLG048 == "1") { CHKDTL048.Checked = true; }
                    if (tDTLG050 == "1") { CHKDTL050.Checked = true; }
                    if (tDTLG052 == "1") { CHKDTL052.Checked = true; }
                    if (tDTLG053 == "1") { CHKDTL053.Checked = true; }
                    if (tDTLG055 == "1") { CHKDTL055.Checked = true; }
                    if (tDTLG057 == "1") { CHKDTL057.Checked = true; }

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLG047, tDTLG079, tDTLG080,tDTLG081 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link047,Link079, Link080,Link081 };

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
                            string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            FileLinkObj.Text=strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    //點擊放大圖片類處理
                    string[] arrayFileName0 = new string[] { tDTLG060, tDTLG062,tDTLG065, tDTLG068, tDTLG071, tDTLG074, tDTLG077 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj0 = new System.Web.UI.WebControls.HyperLink[] { HyperLink060, HyperLink062, HyperLink065, HyperLink068, HyperLink071, HyperLink074, HyperLink077 };

                    for (int i = 0; i < arrayFileName0.Length; i++)
                    {
                        string strFileName = arrayFileName0[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj0[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            Class1 C1 = new Class1();
                            C1.FilesSortOut(strFileName, v, "");
							
                            string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                            ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                        }
                    }

                    //圖片類處理
                    string[] arrayFileName = new string[] { tDTLG060, tDTLG062, tDTLG065, tDTLG068, tDTLG071, tDTLG074, tDTLG077 };
                    System.Web.UI.WebControls.Image[] arrayImgAppobj = new System.Web.UI.WebControls.Image[] { TXTDTL060_img, TXTDTL062_img, TXTDTL065_img, TXTDTL068_img, TXTDTL071_img, TXTDTL074_img, TXTDTL077_img };

                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        string strFileName = arrayFileName[i];
                        System.Web.UI.WebControls.Image ImgFileObj = arrayImgAppobj[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                            ImgFileObj.Attributes.Add("src", tempImgPateh + "?ts=" + DateTime.Now.Millisecond);
                        }
                    }

                    //按鈕處理
                    if (tDATALOCK=="Y")
                    {
                        DataLock.Visible = false;
                        SaveCase.Visible = false;

                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='SWCDT007v.aspx?SWCNO=" + v + "&DTLNO=" + v2 + "'; </script>");
                        //error_msg.Text = SBApp.AlertMsg("資料已送出，目前僅供瀏覽。");
                    }
                }
            }

        }
    }

    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RG" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RG" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLG000) AS MAXID from SWCDTL07 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLG000,7) = '" + tempVal + "' ";

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

    protected void GenerateDropDownList()
    {
        string[] array_DTL009 = new string[] { "", "水土保持計畫", "水保竣工圖說", "建築執照圖說", "其他" };
        DDLDTL009.DataSource = array_DTL009;
        DDLDTL009.DataBind();
        DDLDTL009.SelectedValue = "水土保持計畫";

        string[] array_DTL027 = new string[] { "", "有滯洪沉砂設施", "無滯洪沉砂設施" };
        DDLDTL027.DataSource = array_DTL027;
        DDLDTL027.DataBind();
        DDLDTL027.SelectedValue = "有滯洪沉砂設施";

        string[] array_DTL028 = new string[] { "", "功能正常", "未清疏" };
        DDLDTL028.DataSource = array_DTL028;
        DDLDTL028.DataBind();
        DDLDTL028.SelectedValue = "功能正常";

        string[] array_DTL030 = new string[] { "", "功能正常", "完全無法排放" };
        DDLDTL030.DataSource = array_DTL030;
        DDLDTL030.DataBind();
        DDLDTL030.SelectedValue = "功能正常";

        string[] array_DTL032 = new string[] { "", "有排水設施", "無排水設施" };
        DDLDTL032.DataSource = array_DTL032;
        DDLDTL032.DataBind();
        DDLDTL032.SelectedValue = "有排水設施";

        string[] array_DTL033 = new string[] { "", "功能正常", "有淤積或堵塞" };
        DDLDTL033.DataSource = array_DTL033;
        DDLDTL033.DataBind();
        DDLDTL033.SelectedValue = "功能正常";

        string[] array_DTL035 = new string[] { "", "功能正常", "排水設施有損壞" };
        DDLDTL035.DataSource = array_DTL035;
        DDLDTL035.DataBind();
        DDLDTL035.SelectedValue = "功能正常";

        string[] array_DTL037 = new string[] { "", "有邊坡保護設施", "無邊坡保護設施" };
        DDLDTL037.DataSource = array_DTL037;
        DDLDTL037.DataBind();
        DDLDTL037.SelectedValue = "有邊坡保護設施";

        string[] array_DTL038 = new string[] { "", "排水孔無異常", "排水孔堵塞", "排水孔不正常出水","其他" };
        DDLDTL038.DataSource = array_DTL038;
        DDLDTL038.DataBind();
        DDLDTL038.SelectedValue = "排水孔無異常";

        string[] array_DTL040 = new string[] { "", "無明顯外凸變形", "有外凸變形" };
        DDLDTL040.DataSource = array_DTL040;
        DDLDTL040.DataBind();
        DDLDTL040.SelectedValue = "無明顯外凸變形";

        string[] array_DTL042 = new string[] { "", "無明顯龜裂", "有明顯龜裂" };
        DDLDTL042.DataSource = array_DTL042;
        DDLDTL042.DataBind();
        DDLDTL042.SelectedValue = "無明顯龜裂";

        string[] array_DTL044 = new string[] { "", "無諮詢事項", "有諮詢事項" };
        DDLDTL044.DataSource = array_DTL044;
        DDLDTL044.DataBind();
        DDLDTL044.SelectedValue = "無諮詢事項";

        string[] array_DTL058 = new string[] { "", "已改善", "未改善" };
        DDLDTL058.DataSource = array_DTL058;
        DDLDTL058.DataBind();
        DDLDTL058.SelectedValue = "已改善";

        string[] array_DTLP01 = new string[] { "請選擇單位", "水土保持義務人", "台北市土木技師公會", "社團法人臺北市水利技師公會", "臺北市政府工務局大地工程處", "臺北市政府工務局水利工程處", "臺北市政府工務局新建工程處", "臺北市停車管理處", "臺北市政府產業發展局", "臺北市政府都市發展局", "臺北市政府建築管理工程處", "臺北市都市更新處", "陽明山國家公園管理處", "中華民國大地工程技師公會", "社團法人臺灣省水土保持技師公會", "其他" };
        DropDownList_01.DataSource = array_DTLP01;
        DropDownList_01.DataBind();
        DropDownList_01.SelectedValue = "水土保持義務人";

        string[] array_DTL061 = new string[] { "請選擇檢查項目", "滯洪沉砂設施", "排水設施", "邊坡保護設施", "其他" };
        DDLDTL061.DataSource = array_DTL061;
        DDLDTL061.DataBind();
        DDLDTL061.SelectedValue = "滯洪沉砂設施";
        //DDLDTL061.Enabled = false;

        string[] array_DTL064 = new string[] { "請選擇檢查項目", "滯洪沉砂設施", "排水設施", "邊坡保護設施", "其他" };
        DDLDTL064.DataSource = array_DTL064;
        DDLDTL064.DataBind();
        DDLDTL064.SelectedValue = "滯洪沉砂設施";
        //DDLDTL064.Enabled = false;

        string[] array_DTL067 = new string[] { "請選擇檢查項目", "滯洪沉砂設施", "排水設施", "邊坡保護設施", "其他" };
        DDLDTL067.DataSource = array_DTL067;
        DDLDTL067.DataBind();
        DDLDTL067.SelectedValue = "排水設施";
        //DDLDTL067.Enabled = false;

        string[] array_DTL070 = new string[] { "請選擇檢查項目", "滯洪沉砂設施", "排水設施", "邊坡保護設施", "其他" };
        DDLDTL070.DataSource = array_DTL070;
        DDLDTL070.DataBind();
        DDLDTL070.SelectedValue = "排水設施";
        //DDLDTL070.Enabled = false;

        string[] array_DTL073 = new string[] { "請選擇檢查項目", "滯洪沉砂設施", "排水設施", "邊坡保護設施", "其他" };
        DDLDTL073.DataSource = array_DTL073;
        DDLDTL073.DataBind();
        DDLDTL073.SelectedValue = "邊坡保護設施";
        //DDLDTL073.Enabled = false;

        string[] array_DTL076 = new string[] { "請選擇檢查項目", "滯洪沉砂設施", "排水設施", "邊坡保護設施", "其他" };
        DDLDTL076.DataSource = array_DTL076;
        DDLDTL076.DataBind();
        DDLDTL076.SelectedValue = "邊坡保護設施";
        //DDLDTL076.Enabled = false;

        //TXTDTL017.Text = "(一)檢查單位及人員：" + System.Environment.NewLine + "(二)承辦監造技師：" + System.Environment.NewLine + "(三)水土保持義務人：";
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        string sDTLG000 = LBDTL001.Text + "";
        string sDTLG002 = TXTDTL002.Text + "";
        string sDTLG003 = "";
        string sDTLG004 = TXTDTL004.Text + "";
        string sDTLG005 = TXTDTL005.Text + "";
        string sDTLG006 = TXTDTL006.Text + "";
        string sDTLG007 = TXTDTL007.Text + "";
        string sDTLG008 = TXTDTL008.Text + "";
        string sDTLG009 = DDLDTL009.SelectedValue + "";
        string sDTLG010 = TXTDTL010.Text + "";
        string sDTLG011 = CHKDTL011.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG012 = TXTDTL012.Text + "";
        string sDTLG013 = CHKDTL013.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG014 = TXTDTL014.Text + "";
        string sDTLG015 = CHKDTL015.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG016 = TXTDTL016.Text + "";
        string sDTLG017 = CHKDTL017.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG018 = TXTDTL018.Text + "";
        string sDTLG019 = CHKDTL019.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG020 = TXTDTL020.Text + "";
        string sDTLG021 = CHKDTL021.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG022 = TXTDTL022.Text + "";
        string sDTLG023 = TXTDTL023.Text + "";
        string sDTLG024 = TXTDTL024.Text + "";
        string sDTLG025 = CHKDTL025.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG026 = TXTDTL026.Text + "";
        string sDTLG027 = DDLDTL027.SelectedValue + "";
        string sDTLG028 = DDLDTL028.SelectedValue + "";
        //string sDTLG029 = DDLDTL029.SelectedValue + "";
        string sDTLG030 = DDLDTL030.SelectedValue + "";
        string sDTLG031 = TXTDTL031.Text + "";
        string sDTLG032 = DDLDTL032.SelectedValue + "";
        string sDTLG033 = DDLDTL033.SelectedValue + "";
        //string sDTLG034 = TXTDTL034.Text + "";
        string sDTLG035 = DDLDTL035.SelectedValue + "";
        //string sDTLG036 = TXTDTL036.Text + "";
        string sDTLG037 = DDLDTL037.SelectedValue + "";
        string sDTLG038 = DDLDTL038.SelectedValue + "";
        string sDTLG039 = TXTDTL039.Text + "";
        string sDTLG040 = DDLDTL040.SelectedValue + "";
        //string sDTLG041 = TXTDTL041.Text + "";
        string sDTLG042 = DDLDTL042.SelectedValue + "";
        //string sDTLG043 = TXTDTL043.Text + "";
        string sDTLG044 = DDLDTL044.SelectedValue + "";
        string sDTLG045 = TXTDTL045.Text + "";
        string sDTLG046 = CHKDTL046.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG047 = TXTDTL047.Text + "";
        string sDTLG048 = CHKDTL048.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG049 = TXTDTL049.Text + "";
        string sDTLG050 = CHKDTL050.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG051 = TXTDTL051.Text + "";
        string sDTLG052 = CHKDTL052.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG053 = CHKDTL053.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG054 = TXTDTL054.Text + "";
        string sDTLG055 = CHKDTL055.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG056 = TXTDTL056.Text + "";
        string sDTLG057 = CHKDTL057.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLG058 = DDLDTL058.SelectedValue + "";
        string sDTLG059 = TXTDTL059.Text + "";
        string sDTLG060 = TXTDTL060.Text + "";
        string sDTLG061 = DDLDTL061.SelectedValue + "";
        string sDTLG062 = TXTDTL062.Text + "";
        string sDTLG063 = TXTDTL063.Text + "";
        string sDTLG064 = DDLDTL064.SelectedValue + "";
        string sDTLG065 = TXTDTL065.Text + "";
        string sDTLG066 = TXTDTL066.Text + "";
        string sDTLG067 = DDLDTL067.SelectedValue + "";
        string sDTLG068 = TXTDTL068.Text + "";
        string sDTLG069 = TXTDTL069.Text + "";
        string sDTLG070 = DDLDTL070.SelectedValue + "";
        string sDTLG071 = TXTDTL071.Text + "";
        string sDTLG072 = TXTDTL072.Text + "";
        string sDTLG073 = DDLDTL073.SelectedValue + "";
        string sDTLG074 = TXTDTL074.Text + "";
        string sDTLG075 = TXTDTL075.Text + "";
        string sDTLG076 = DDLDTL076.SelectedValue + "";
        string sDTLG077 = TXTDTL077.Text + "";
        string sDTLG078 = TXTDTL078.Text + "";
        string sDTLG079 = TXTDTL079.Text + "";
        string sDTLG080 = TXTDTL080.Text + "";
        string sDTLG081 = TXTDTL081.Text + "";

        if (CHKDTL052.Checked) { sDTLG003 = sDTLG003 + "水保設施維護均正常，無應改正事項 "; }
        if (CHKDTL053.Checked) { sDTLG003 = sDTLG003 + "須辦理複檢，義務人同意於" + sDTLG054 + "日前完成改善 "; }
        if (CHKDTL055.Checked) { sDTLG003 = sDTLG003 + "有變更使用，影響安全情事" + sDTLG056 + " "; }
        if (CHKDTL057.Checked) { sDTLG003 = sDTLG003 + "前次檢查之改正事項辦理情形" + sDTLG058 + " "; }

        if (sDTLG003.Length > 300) { sDTLG003 = sDTLG003.Substring(0, 300); }
        if (sDTLG045.Length > 500) { sDTLG045 = sDTLG045.Substring(0, 500); }
        if (sDTLG049.Length > 300) { sDTLG049 = sDTLG049.Substring(0, 300); }
        if (sDTLG051.Length > 300) { sDTLG051 = sDTLG051.Substring(0, 300); }
        if (sDTLG056.Length > 300) { sDTLG056 = sDTLG056.Substring(0, 300); }
        if (sDTLG059.Length > 300) { sDTLG059 = sDTLG059.Substring(0, 300); }
        

        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL07 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLG000 = '" + sDTLG000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL07 (SWC000,DTLG000) VALUES ('" + sSWC000 + "','" + sDTLG000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL07 Set ";

            sEXESQLSTR = sEXESQLSTR + " DTLG001 = DTLG000 , ";
            sEXESQLSTR = sEXESQLSTR + " DTLG002 ='" + sDTLG002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLG003 = N'" + sDTLG003.Replace("'", "''") + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG004 = N'" + sDTLG004.Replace("'", "''") + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG005 = N'" + sDTLG005.Replace("'", "''") + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG006	 ='" + sDTLG006 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG007	 ='" + sDTLG007 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG008	 ='" + sDTLG008 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG009	 ='" + sDTLG009 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG010	 ='" + sDTLG010 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG011	 ='" + sDTLG011 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG012	 ='" + sDTLG012 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG013	 ='" + sDTLG013 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG014	 ='" + sDTLG014 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG015	 ='" + sDTLG015 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG016	 ='" + sDTLG016 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG017	 ='" + sDTLG017 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG018	 ='" + sDTLG018 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG019	 ='" + sDTLG019 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG020	 ='" + sDTLG020 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG021	 ='" + sDTLG021 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG022	 ='" + sDTLG022 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG023	 ='" + sDTLG023 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG024	 ='" + sDTLG024 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG025	 ='" + sDTLG025 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG026	 ='" + sDTLG026 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG027	 ='" + sDTLG027 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG028	 ='" + sDTLG028 + "', ";
            //sEXESQLSTR = sEXESQLSTR + "	DTLG029	 ='" + sDTLG029 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG030	 ='" + sDTLG030 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG031	 ='" + sDTLG031 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG032	 ='" + sDTLG032 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG033	 ='" + sDTLG033 + "', ";
            //sEXESQLSTR = sEXESQLSTR + "	DTLG034	 ='" + sDTLG034 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG035	 ='" + sDTLG035 + "', ";
            //sEXESQLSTR = sEXESQLSTR + "	DTLG036	 ='" + sDTLG036 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG037	 ='" + sDTLG037 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG038	 ='" + sDTLG038 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG039	 ='" + sDTLG039 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG040	 ='" + sDTLG040 + "', ";
            //sEXESQLSTR = sEXESQLSTR + "	DTLG041	 ='" + sDTLG041 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG042	 ='" + sDTLG042 + "', ";
            //sEXESQLSTR = sEXESQLSTR + "	DTLG043	 ='" + sDTLG043 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG044	 ='" + sDTLG044 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG045	 ='" + sDTLG045 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG046	 ='" + sDTLG046 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG047	 ='" + sDTLG047 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG048	 ='" + sDTLG048 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG049	 ='" + sDTLG049 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG050	 ='" + sDTLG050 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG051	 ='" + sDTLG051 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG052	 ='" + sDTLG052 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG053	 ='" + sDTLG053 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG054	 ='" + sDTLG054 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG055	 ='" + sDTLG055 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG056	 ='" + sDTLG056 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG057	 ='" + sDTLG057 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG058	 ='" + sDTLG058 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG059	 ='" + sDTLG059 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG060	 ='" + sDTLG060 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG061	 ='" + sDTLG061 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG062	 ='" + sDTLG062 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG063	 ='" + sDTLG063 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG064	 ='" + sDTLG064 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG065	 ='" + sDTLG065 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG066	 ='" + sDTLG066 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG067	 ='" + sDTLG067 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG068	 ='" + sDTLG068 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG069	 ='" + sDTLG069 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG070	 ='" + sDTLG070 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG071	 ='" + sDTLG071 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG072	 ='" + sDTLG072 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG073	 ='" + sDTLG073 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG074	 ='" + sDTLG074 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG075	 ='" + sDTLG075 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG076	 ='" + sDTLG076 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG077	 ='" + sDTLG077 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG078	 ='" + sDTLG078 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG079	 ='" + sDTLG079 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG080	 ='" + sDTLG080 + "', ";
            sEXESQLSTR = sEXESQLSTR + "	DTLG081	 ='" + sDTLG081 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLG000 = '" + sDTLG000 + "'";

            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";
            C20.swcLogRC("SWCDT007", "設施維護檢查表", "詳情", "更新", sSWC000 + "," + sDTLG000);
            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
            
            string vCaseID = Request.QueryString["SWCNO"] + "";
            Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
        }
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL047", "TXTDTL060", "TXTDTL062", "TXTDTL065", "TXTDTL068", "TXTDTL071", "TXTDTL074", "TXTDTL077", "TXTDTL079", "TXTDTL080", "TXTDTL081" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL047, TXTDTL060, TXTDTL062, TXTDTL065, TXTDTL068, TXTDTL071, TXTDTL074, TXTDTL077, TXTDTL079, TXTDTL080, TXTDTL081 };
        string csUpLoadField = "TXTDTL047";
        TextBox csUpLoadAppoj = TXTDTL047;

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
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
    }
    private void FileUpLoadApp(string ChkType,FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
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
                        List<string> allowedExtextsion01 = new List<string> { ".jpg",".png" };

                        if (allowedExtextsion01.IndexOf(extension) == -1)
                        {
                            error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                            return;
                        }
                    break;
                case "DOC":
                        List<string> allowedExtextsion02 = new List<string> { ".jpg", ".png" ,".doc", ".pdf", ".dwg", ".dxf" };

                        if (allowedExtextsion02.IndexOf(extension) == -1)
                        {
                            error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG DOC PDF DWG DXF 檔案格式上傳，謝謝!!");
                            return;
                        }
                    break;

            }

            // 限制檔案大小，限制為 10MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 10000000)
            {
                error_msg.Text = "請選擇 10Mb 以下檔案上傳，謝謝!!";
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
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
							UpLoadView.Attributes.Add("src", GlobalUpLoadTempFilePath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                            //UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                            //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;
                
                            imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "PIC2":
						UpLoadLink.ImageUrl = GlobalUpLoadTempFilePath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.NavigateUrl = GlobalUpLoadTempFilePath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        //UpLoadLink.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        //UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        break;

                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = GlobalUpLoadTempFilePath + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        //UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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



    private void DeleteUpLoadFile(string DelType,TextBox ImgText, System.Web.UI.WebControls.Image ImgView,HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csDTLID = LBDTL001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCDTL07 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLG001 = '" + csDTLID + "' ";

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
                ImgView.Attributes.Clear();
                ImgView.ImageUrl = "";
                ImgView.Width = NoneWidth;
                ImgView.Height = NoneHeight;
                break;
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

    protected void TXTDTL081_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL081, null, Link081, "DTLG081", "TXTDTL081", 0, 0);
    }
    protected void TXTDTL080_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL080, null, Link080, "DTLG080", "TXTDTL080", 0, 0);
    }
    protected void TXTDTL079_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL079, null, Link079, "DTLG079", "TXTDTL079", 0, 0);
    }

    protected void TXTDTL077_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL077, TXTDTL077_img, null, "DTLG077", "TXTDTL077", 0, 0);
    }

    protected void TXTDTL074_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL074, TXTDTL074_img, null, "DTLG074", "TXTDTL074", 0, 0);
    }

    protected void TXTDTL071_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL071, TXTDTL071_img, null, "DTLG071", "TXTDTL071", 0, 0);
    }

    protected void TXTDTL068_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL068, TXTDTL068_img, null, "DTLG068", "TXTDTL068", 0, 0);
    }

    protected void TXTDTL065_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL065, TXTDTL065_img, null, "DTLG065", "TXTDTL065", 0, 0);
    }

    protected void TXTDTL062_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL062, TXTDTL062_img, null, "DTLG062", "TXTDTL062", 0, 0);
    }

    protected void TXTDTL060_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("PIC", TXTDTL060, TXTDTL060_img, null, "DTLG060", "TXTDTL060", 0, 0);
    }

    protected void TXTDTL047_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL047, null, Link047, "DTLG047", "TXTDTL047", 0, 0);
    }

    protected void TXTDTL081_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL081_fileupload, TXTDTL081, "TXTDTL081", "_" + rDTLNO + "_07_doc4", null, Link081);
    }
    protected void TXTDTL080_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL080_fileupload, TXTDTL080, "TXTDTL080", "_" + rDTLNO + "_07_doc3", null, Link080);

    }
    protected void TXTDTL079_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL079_fileupload, TXTDTL079, "TXTDTL079", "_" + rDTLNO + "_07_doc2", null, Link079);
    }

    protected void TXTDTL047_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL047_fileupload, TXTDTL047, "TXTDTL047", "_" + rDTLNO + "_07_doc1", null, Link047);
    }

    protected void TXTDTL060_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL060_fileupload, TXTDTL060, "TXTDTL060", "_" + rDTLNO + "_07_Sign", null, HyperLink060);

    }
    protected void TXTDTL062_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL062_fileupload, TXTDTL062, "TXTDTL062", "_" + rDTLNO + "_07_photo2", null, HyperLink062);
    }
    protected void TXTDTL065_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL065_fileupload, TXTDTL065, "TXTDTL065", "_" + rDTLNO + "_07_photo3", null, HyperLink065);
    }
    protected void TXTDTL068_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL068_fileupload, TXTDTL068, "TXTDTL068", "_" + rDTLNO + "_07_photo4", null, HyperLink068);
    }
    protected void TXTDTL071_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL071_fileupload, TXTDTL071, "TXTDTL071", "_" + rDTLNO + "_07_photo5", null, HyperLink071);
    }
    protected void TXTDTL074_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL074_fileupload, TXTDTL074, "TXTDTL074", "_" + rDTLNO + "_07_photo6", null, HyperLink074);

    }
    protected void TXTDTL077_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL077_fileupload, TXTDTL077, "TXTDTL077", "_" + rDTLNO + "_07_photo7", null, HyperLink077);
    }


    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLG000 = LBDTL001.Text + "";

        string sEXESQLSTR = "";
		
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL07 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLG000 = '" + sDTLG000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL07 (SWC000,DTLG000) VALUES ('" + sSWC000 + "','" + sDTLG000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL07 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLG000 = '" + sDTLG000 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }

        SendMailNotice(sSWC000);
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
		
        //送出提醒名單：股長、管理者、承辦人員、施柏宇(ge-10754)、章姿隆(ge-10706)
		
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC025, SWC.SWC024ID,SWC.SWC108 from SWCCASE SWC ";
            //strSQLRV = strSQLRV + " LEFT JOIN ETUsers U on SWC.SWC045ID = U.ETID ";
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
                string tSWC024ID = readeSwc["SWC024ID"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";
				
                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706" || aUserId == "ge-10754")
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;
                    }
                }

                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增設施維護檢查表";
                string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增設施維護檢查表，請上管理平台查看" + "<br><br>";
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                string[] arraySentMail02 = new string[] {tSWC108 };
                string ssMailSub02 = "您好，水土保持計畫【" + tSWC002 + "】已新增設施維護檢查表";
                string ssMailBody02 = "您好，【" + tSWC005 + "】已新增設施維護檢查表，請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                
                bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);

                string ssMailBody03 = "您好，【" + tSWC005 + "】已新增設施維護檢查表，請至臺北市水土保持申請書件管理平台上瀏覽。";
                
                string[] arraySWC013TEL = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
				SBApp.SendSMS_Arr(arraySWC013TEL, ssMailBody03);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
}