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
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCDT005 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rRRPage = Request.QueryString["RRPG"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        if (rCaseId == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        switch (ssUserType)
        {
            case "02":
                TitleLink00.Visible = true;
                break;
            case "03":
                GoTslm.Visible = true;
                break;
        }

        if (!IsPostBack)
        {
            Data2Page(rCaseId, rDTLId);
        }

        if (rRRPage == "55")
        {
            GoHomePage.Visible = false;
        }

        //全區供用

        SBApp.ViewRecord("臺北市水土保持計畫監造紀錄", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
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
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC048 = readeSwc["SWC048"] + "";

                LBSWC000.Text = v;
                LBSWC005.Text = tSWC005;
                LBSWC005_01.Text = tSWC005;
                LBSWC013.Text = tSWC013;
                LBSWC043.Text = SBApp.DateView(tSWC043, "00");
                LBSWC044.Text = tSWC044;
                LBSWC045.Text = tSWC045;
                LBSWC048.Text = tSWC048;
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
                string strSQLRV2 = " select * from SWCDTL05 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   and DTLE000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLE001 = readeDTL["DTLE001"] + "";
                    string tDTLE002 = readeDTL["DTLE002"] + "";
                    string tDTLE003 = readeDTL["DTLE003"] + "";
                    string tDTLE004 = readeDTL["DTLE004"] + "";
                    string tDTLE005 = readeDTL["DTLE005"] + "";
                    string tDTLE006 = readeDTL["DTLE006"] + "";
                    string tDTLE007 = readeDTL["DTLE007"] + "";

                    string tDTLE016 = readeDTL["DTLE016"] + "";
                    string tDTLE017 = readeDTL["DTLE017"] + "";
                    string tDTLE018 = readeDTL["DTLE018"] + "";
                    string tDTLE019 = readeDTL["DTLE019"] + "";
                    string tDTLE020 = readeDTL["DTLE020"] + "";
                    string tDTLE021 = readeDTL["DTLE021"] + "";
                    string tDTLE022 = readeDTL["DTLE022"] + "";
                    string tDTLE023 = readeDTL["DTLE023"] + "";
                    string tDTLE024 = readeDTL["DTLE024"] + "";
                    string tDTLE025 = readeDTL["DTLE025"] + "";
                    string tDTLE026 = readeDTL["DTLE026"] + "";
                    string tDTLE027 = readeDTL["DTLE027"] + "";
                    string tDTLE028 = readeDTL["DTLE028"] + "";
                    string tDTLE029 = readeDTL["DTLE029"] + "";
                    string tDTLE030 = readeDTL["DTLE030"] + "";
                    string tDTLE031 = readeDTL["DTLE031"] + "";
                    string tDTLE032 = readeDTL["DTLE032"] + "";
                    string tDTLE033 = readeDTL["DTLE033"] + "";
                    string tDTLE034 = readeDTL["DTLE034"] + "";
                    string tDTLE035 = readeDTL["DTLE035"] + "";
                    string tDTLE036 = readeDTL["DTLE036"] + "";
                    string tDTLE037 = readeDTL["DTLE037"] + "";
                    string tDTLE038 = readeDTL["DTLE038"] + "";
                    string tDTLE039 = readeDTL["DTLE039"] + "";
                    string tDTLE040 = readeDTL["DTLE040"] + "";
                    string tDTLE041 = readeDTL["DTLE041"] + "";
                    string tDTLE042 = readeDTL["DTLE042"] + "";
                    string tDTLE043 = readeDTL["DTLE043"] + "";
                    string tDTLE044 = readeDTL["DTLE044"] + "";
                    string tDTLE045 = readeDTL["DTLE045"] + "";
                    string tDTLE046 = readeDTL["DTLE046"] + "";
                    string tDTLE047 = readeDTL["DTLE047"] + "";
                    string tDTLE048 = readeDTL["DTLE048"] + "";
                    string tDTLE049 = readeDTL["DTLE049"] + "";
                    string tDTLE050 = readeDTL["DTLE050"] + "";
                    string tDTLE051 = readeDTL["DTLE051"] + "";
                    string tDTLE052 = readeDTL["DTLE052"] + "";
                    string tDTLE053 = readeDTL["DTLE053"] + "";
                    string tDTLE054 = readeDTL["DTLE054"] + "";
                    string tDTLE055 = readeDTL["DTLE055"] + "";
                    string tDTLE056 = readeDTL["DTLE056"] + "";
                    string tDTLE057 = readeDTL["DTLE057"] + "";
                    string tDTLE058 = readeDTL["DTLE058"] + "";
                    string tDTLE059 = readeDTL["DTLE059"] + "";
                    string tDTLE060 = readeDTL["DTLE060"] + "";
                    string tDTLE061 = readeDTL["DTLE061"] + "";
                    string tDTLE062 = readeDTL["DTLE062"] + "";
                    string tDTLE063 = readeDTL["DTLE063"] + "";
                    string tDTLE064 = readeDTL["DTLE064"] + "";
                    string tDTLE065 = readeDTL["DTLE065"] + "";
                    string tDTLE066 = readeDTL["DTLE066"] + "";
                    string tDTLE067 = readeDTL["DTLE067"] + "";
                    string tDTLE068 = readeDTL["DTLE068"] + "";
                    string tDTLE069 = readeDTL["DTLE069"] + "";
                    string tDTLE070 = readeDTL["DTLE070"] + "";
                    string tDTLE071 = readeDTL["DTLE071"] + "";
                    string tDTLE072 = readeDTL["DTLE072"] + "";
                    string tDTLE073 = readeDTL["DTLE073"] + "";
                    string tDTLE074 = readeDTL["DTLE074"] + "";
                    string tDTLE075 = readeDTL["DTLE075"] + "";
                    string tDTLE076 = readeDTL["DTLE076"] + "";
                    string tDTLE077 = readeDTL["DTLE077"] + "";
                    string tDTLE078 = readeDTL["DTLE078"] + "";
                    string tDTLE079 = readeDTL["DTLE079"] + "";
                    string tDTLE080 = readeDTL["DTLE080"] + "";
                    string tDTLE081 = readeDTL["DTLE081"] + "";
                    string tDTLE082 = readeDTL["DTLE082"] + "";
                    string tDTLE083 = readeDTL["DTLE083"] + "";
                    string tDTLE084 = readeDTL["DTLE084"] + "";
                    string tDTLE085 = readeDTL["DTLE085"] + "";
                    string tDTLE086 = readeDTL["DTLE086"] + "";
                    string tDTLE087 = readeDTL["DTLE087"] + "";
                    
                    //string tDTLE006 = readeDTL["DTLE006"] + "";
                    //string tDTLE007 = readeDTL["DTLE007"] + "";
                    //string tDTLE008 = readeDTL["DTLE008"] + "";
                    //string tDTLE009 = readeDTL["DTLE009"] + "";
                    //string tDTLE010 = readeDTL["DTLE010"] + "";
                    //string tDTLE011 = readeDTL["DTLE011"] + "";
                    //string tDTLE012 = readeDTL["DTLE012"] + "";
                    //string tDTLE013 = readeDTL["DTLE013"] + "";
                    //string tDTLE014 = readeDTL["DTLE014"] + "";
                    //string tDTLE015 = readeDTL["DTLE015"] + "";

                    LBDTL001.Text = tDTLE001;
                    TXTDTL002.Text = SBApp.DateView(tDTLE002, "00");
                    TXTDTL003.Text = tDTLE003;
                    TXTDTL004.Text = tDTLE004;
                    TXTDTL005.Text = SBApp.DateView(tDTLE005, "00");
                    TXTDTL006.Text = SBApp.DateView(tDTLE006, "00");
                    TXTDTL007.Text = SBApp.DateView(tDTLE007, "00");

                    //問卷選項
                    string[] arrayItem01 = new string[] { tDTLE016,tDTLE034 };
                    string[] arrayItem02 = new string[] { tDTLE017,tDTLE035 };
                    string[] arrayItem03 = new string[] { tDTLE018,tDTLE036 };

                    Label[] arrayLB01 = new Label[] { DDLDTL016,DDLDTL034 };
                    Label[] arrayLB02 = new Label[] { TXTDTL017,TXTDTL035 };
                    Label[] arrayLB03 = new Label[] { TXTDTL018,TXTDTL036 };

                    for (int i = 0; i < arrayItem01.Length; i++)
                    {
                        string aItem01 = arrayItem01[i] + "";
                        string aItem02 = arrayItem02[i] + "";
                        string aItem03 = arrayItem03[i] + "";

                        Label LB01 = arrayLB01[i];
                        Label LB02 = arrayLB02[i];
                        Label LB03 = arrayLB03[i];

                        if (aItem01 == "是")
                        {
                            LB01.Text = "符合";
                        }
                        if (aItem01 == "否")
                        {
                            LB01.Text = "不符，";
                            LB02.Text = "不符部分：" + aItem02;
                        }
                        LB03.Text = aItem03;

                    }

                    
                    
                    if (tDTLE019 == "是")
                    {
                        DDLDTL019.Text = "符合";
                    }
                    if (tDTLE019 == "否")
                    {
                        DDLDTL019.Text = "不符";
                        TXTDTL020.Text = "不符部分：" + tDTLE020;
                    }
                    TXTDTL021.Text = tDTLE021;

                    if (tDTLE022 == "是")
                    {
                        DDLDTL022.Text = "符合";
                    }
                    if (tDTLE022 == "否")
                    {
                        DDLDTL022.Text = "不符";
                        TXTDTL023.Text = "不符部分：" + tDTLE023;
                    }
                    TXTDTL024.Text = tDTLE024;

                    if (tDTLE025 == "是")
                    {
                        DDLDTL025.Text = "符合";
                    }
                    if (tDTLE025 == "否")
                    {
                        DDLDTL025.Text = "不符";
                        TXTDTL026.Text = "不符部分：" + tDTLE026;
                    }
                    TXTDTL027.Text = tDTLE027;

                    if (tDTLE028 == "是")
                    {
                        DDLDTL028.Text = "符合";
                    }
                    if (tDTLE028 == "否")
                    {
                        DDLDTL028.Text = "不符";
                        TXTDTL029.Text = "不符部分：" + tDTLE029;
                    }
                    TXTDTL030.Text = tDTLE030;

                    if (tDTLE031 == "是")
                    {
                        DDLDTL031.Text = "符合";
                    }
                    if (tDTLE031 == "否")
                    {
                        DDLDTL031.Text = "不符";
                        TXTDTL032.Text = "不符部分：" + tDTLE032;
                    }
                    TXTDTL033.Text = tDTLE033;

                    if (tDTLE037 == "是")
                    {
                        DDLDTL037.Text = "符合";
                    }
                    if (tDTLE037 == "否")
                    {
                        DDLDTL037.Text = "不符";
                        TXTDTL038.Text = "不符部分：" + tDTLE038;
                    }
                    TXTDTL039.Text = tDTLE039;

                    if (tDTLE040 == "是")
                    {
                        DDLDTL040.Text = "符合";
                    }
                    if (tDTLE040 == "否")
                    {
                        DDLDTL040.Text = "不符";
                        TXTDTL041.Text = "不符部分：" + tDTLE041;
                    }
                    TXTDTL042.Text = tDTLE042;

                    if (tDTLE043 == "是")
                    {
                        DDLDTL043.Text = "符合";
                    }
                    if (tDTLE043 == "否")
                    {
                        DDLDTL043.Text = "不符";
                        TXTDTL044.Text = "不符部分：" + tDTLE044;
                    }
                    TXTDTL045.Text = tDTLE045;

                    if (tDTLE046 == "是")
                    {
                        DDLDTL046.Text = "符合";
                    }
                    if (tDTLE046 == "否")
                    {
                        DDLDTL046.Text = "不符";
                        TXTDTL047.Text = "不符部分：" + tDTLE047;
                    }
                    TXTDTL048.Text = tDTLE048;

                    if (tDTLE049 == "是")
                    {
                        DDLDTL049.Text = "符合";
                    }
                    if (tDTLE049 == "否")
                    {
                        DDLDTL049.Text = "不符";
                        TXTDTL050.Text = "不符部分：" + tDTLE050;
                    }
                    TXTDTL051.Text = tDTLE051;

                    if (tDTLE052 == "是")
                    {
                        DDLDTL052.Text = "符合";
                    }
                    if (tDTLE052 == "否")
                    {
                        DDLDTL052.Text = "不符";
                        TXTDTL053.Text = "不符部分：" + tDTLE053;
                    }
                    TXTDTL054.Text = tDTLE054;

                    if (tDTLE055 == "是")
                    {
                        DDLDTL055.Text = "符合";
                    }
                    if (tDTLE055 == "否")
                    {
                        DDLDTL055.Text = "不符";
                        TXTDTL056.Text = "不符部分：" + tDTLE056;
                    }
                    TXTDTL057.Text = tDTLE057;

                    if (tDTLE058 == "是")
                    {
                        DDLDTL058.Text = "符合";
                    }
                    if (tDTLE058 == "否")
                    {
                        DDLDTL058.Text = "不符";
                        TXTDTL059.Text = "不符部分：" + tDTLE059;
                    }
                    TXTDTL060.Text = tDTLE060;

                    if (tDTLE061 == "是")
                    {
                        DDLDTL061.Text = "符合";
                    }
                    if (tDTLE061 == "否")
                    {
                        DDLDTL061.Text = "不符";
                        TXTDTL062.Text = "不符部分：" + tDTLE062;
                    }
                    TXTDTL063.Text = tDTLE063;

                    if (tDTLE064 == "是")
                    {
                        DDLDTL064.Text = "符合";
                    }
                    if (tDTLE064 == "否")
                    {
                        DDLDTL064.Text = "不符";
                        TXTDTL065.Text = "不符部分：" + tDTLE065;
                    }
                    TXTDTL066.Text = tDTLE066;

                    if (tDTLE067 == "是")
                    {
                        DDLDTL067.Text = "符合";
                    }
                    if (tDTLE067 == "否")
                    {
                        DDLDTL067.Text = "不符";
                        TXTDTL068.Text = "不符部分：" + tDTLE068;
                    }
                    TXTDTL069.Text = tDTLE069;

                    if (tDTLE070 == "是")
                    {
                        DDLDTL070.Text = "符合";
                    }
                    if (tDTLE070 == "否")
                    {
                        DDLDTL070.Text = "不符";
                        TXTDTL071.Text = "不符部分：" + tDTLE071;
                    }
                    TXTDTL072.Text = tDTLE072;

                    

                    if (tDTLE073 == "是")
                    {
                        DDLDTL073.Text = "符合";
                    }
                    if (tDTLE073 == "否")
                    {
                        DDLDTL073.Text = "不符";
                        TXTDTL074.Text = "不符部分：" + tDTLE074;
                    }
                    TXTDTL075.Text = tDTLE075;

                    if (tDTLE076 == "是")
                    {
                        DDLDTL076.Text = "符合";
                    }
                    if (tDTLE076 == "否")
                    {
                        DDLDTL076.Text = "不符";
                        TXTDTL077.Text = "不符部分：" + tDTLE077;
                    }
                    TXTDTL078.Text = tDTLE078;
                    
                    TXTDTL079.Text = tDTLE079;
                    TXTDTL080.Text = tDTLE080;
                    TXTDTL081.Text = SBApp.DateView(tDTLE081, "00");
                    TXTDTL082.Text = tDTLE082;
                    TXTDTL083.Text = tDTLE083;
                    TXTDTL084.Text = tDTLE084;
                    TXTDTL085.Text = tDTLE085;
                    TXTDTL086.Text = tDTLE086;
                    TXTDTL087.Text = tDTLE087;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLE083, tDTLE084 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link083, Link084 };

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
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                    
                    //點擊放大圖片類處理
                    string[] arrayFileName = new string[] { tDTLE085, tDTLE086, tDTLE087 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj = new System.Web.UI.WebControls.HyperLink[] { HyperLink085, HyperLink086, HyperLink087 };

                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        string strFileName = arrayFileName[i];
                        System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj[i];

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
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RE" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RE" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLE000) AS MAXID from SWCDTL05 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLE000,7) = '" + tempVal + "' ";

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
    
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL083", "TXTDTL084", "TXTDTL085", "TXTDTL086", "TXTDTL087" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL083, TXTDTL084, TXTDTL085, TXTDTL086, TXTDTL087 };
        string csUpLoadField = "TXTDTL083";
        TextBox csUpLoadAppoj = TXTDTL083;

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
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 excel 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            
            int filesize = UpLoadBar.PostedFile.ContentLength;

            switch (ChkType)
            {
                case "PIC":
                    // 限制檔案大小，限制為 5MB
                    if (filesize > 5000000)
                    {
                        error_msg.Text = "請選擇 5Mb 以下檔案上傳，謝謝!!";
                        return;
                    }
                    break;
                case "DOC":
                    // 限制檔案大小，限制為 50MB
                    if (filesize > 50000000)
                    {
                        error_msg.Text = "請選擇 50Mb 以下檔案上傳，謝謝!!";
                        return;
                    }
                    break;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

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
                    case "PIC":
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

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
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
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

        strSQLClearFieldValue = " update SWCDTL05 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLE001 = '" + csDTLID + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

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

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }
    
    
}