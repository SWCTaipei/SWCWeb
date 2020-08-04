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
            GenerateDropDownList();
            Data2Page(rCaseId, rDTLId);
        }

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + "，您好";
        }

        //全區供用

        SBApp.ViewRecord("臺北市水土保持計畫監造紀錄", "update", "");

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

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

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

                    DDLDTL016.SelectedValue = tDTLE016;
                    TXTDTL017.Text = tDTLE017;
                    TXTDTL018.Text = tDTLE018;
                    DDLDTL019.SelectedValue = tDTLE019;
                    TXTDTL020.Text = tDTLE020;
                    TXTDTL021.Text = tDTLE021;
                    DDLDTL022.SelectedValue = tDTLE022;
                    TXTDTL023.Text = tDTLE023;
                    TXTDTL024.Text = tDTLE024;
                    DDLDTL025.SelectedValue = tDTLE025;
                    TXTDTL026.Text = tDTLE026;
                    TXTDTL027.Text = tDTLE027;
                    DDLDTL028.SelectedValue = tDTLE028;
                    TXTDTL029.Text = tDTLE029;
                    TXTDTL030.Text = tDTLE030;
                    DDLDTL031.SelectedValue = tDTLE031;
                    TXTDTL032.Text = tDTLE032;
                    TXTDTL033.Text = tDTLE033;
                    DDLDTL034.SelectedValue = tDTLE034;
                    TXTDTL035.Text = tDTLE035;
                    TXTDTL036.Text = tDTLE036;
                    DDLDTL037.SelectedValue = tDTLE037;
                    TXTDTL038.Text = tDTLE038;
                    TXTDTL039.Text = tDTLE039;
                    DDLDTL040.SelectedValue = tDTLE040;
                    TXTDTL041.Text = tDTLE041;
                    TXTDTL042.Text = tDTLE042;
                    DDLDTL043.SelectedValue = tDTLE043;
                    TXTDTL044.Text = tDTLE044;
                    TXTDTL045.Text = tDTLE045;
                    DDLDTL046.SelectedValue = tDTLE046;
                    TXTDTL047.Text = tDTLE047;
                    TXTDTL048.Text = tDTLE048;
                    DDLDTL049.SelectedValue = tDTLE049;
                    TXTDTL050.Text = tDTLE050;
                    TXTDTL051.Text = tDTLE051;
                    DDLDTL052.SelectedValue = tDTLE052;
                    TXTDTL053.Text = tDTLE053;
                    TXTDTL054.Text = tDTLE054;
                    DDLDTL055.SelectedValue = tDTLE055;
                    TXTDTL056.Text = tDTLE056;
                    TXTDTL057.Text = tDTLE057;
                    DDLDTL058.SelectedValue = tDTLE058;
                    TXTDTL059.Text = tDTLE059;
                    TXTDTL060.Text = tDTLE060;
                    DDLDTL061.SelectedValue = tDTLE061;
                    TXTDTL062.Text = tDTLE062;
                    TXTDTL063.Text = tDTLE063;
                    DDLDTL064.SelectedValue = tDTLE064;
                    TXTDTL065.Text = tDTLE065;
                    TXTDTL066.Text = tDTLE066;
                    DDLDTL067.SelectedValue = tDTLE067;
                    TXTDTL068.Text = tDTLE068;
                    TXTDTL069.Text = tDTLE069;
                    DDLDTL070.SelectedValue = tDTLE070;
                    TXTDTL071.Text = tDTLE071;
                    TXTDTL072.Text = tDTLE072;
                    DDLDTL073.SelectedValue = tDTLE073;
                    TXTDTL074.Text = tDTLE074;
                    TXTDTL075.Text = tDTLE075;
                    DDLDTL076.SelectedValue = tDTLE076;
                    TXTDTL077.Text = tDTLE077;
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

                    //按鈕處理
                    if (tDATALOCK == "Y")
                    {
                        DataLock.Visible = false;
                        SaveCase.Visible = false;
                        
                        Response.Write("<script>alert('資料已提交大地處，目前僅供瀏覽。'); location.href='SWCDT005v.aspx?SWCNO=" + v + "&DTLNO=" + v2 + "'; </script>");
                    }
                }
            }

        }
    }
    protected void GenerateDropDownList()
    {
        //2018-05-07，第五次工作會議加預設值...

        string[] array_DTL016 = new string[] { "", "是", "否" };
        DDLDTL016.DataSource = array_DTL016;
        DDLDTL016.DataBind();
        DDLDTL016.SelectedValue = "是";

        string[] array_DTL019 = new string[] { "", "是", "否" };
        DDLDTL019.DataSource = array_DTL019;
        DDLDTL019.DataBind();
        DDLDTL019.SelectedValue = "是";

        string[] array_DTL022 = new string[] { "", "是", "否" };
        DDLDTL022.DataSource = array_DTL022;
        DDLDTL022.DataBind();
        DDLDTL022.SelectedValue = "是";

        string[] array_DTL025 = new string[] { "", "是", "否" };
        DDLDTL025.DataSource = array_DTL025;
        DDLDTL025.DataBind();
        DDLDTL025.SelectedValue = "是";

        string[] array_DTL028 = new string[] { "", "是", "否" };
        DDLDTL028.DataSource = array_DTL028;
        DDLDTL028.DataBind();
        DDLDTL028.SelectedValue = "是";

        string[] array_DTL031 = new string[] { "", "是", "否" };
        DDLDTL031.DataSource = array_DTL031;
        DDLDTL031.DataBind();
        DDLDTL031.SelectedValue = "是";

        string[] array_DTL034 = new string[] { "", "是", "否" };
        DDLDTL034.DataSource = array_DTL034;
        DDLDTL034.DataBind();
        DDLDTL034.SelectedValue = "是";

        string[] array_DTL037 = new string[] { "", "是", "否" };
        DDLDTL037.DataSource = array_DTL037;
        DDLDTL037.DataBind();
        DDLDTL037.SelectedValue = "是";

        string[] array_DTL040 = new string[] { "", "是", "否" };
        DDLDTL040.DataSource = array_DTL040;
        DDLDTL040.DataBind();
        DDLDTL040.SelectedValue = "是";

        string[] array_DTL043 = new string[] { "", "是", "否" };
        DDLDTL043.DataSource = array_DTL043;
        DDLDTL043.DataBind();
        DDLDTL043.SelectedValue = "是";

        string[] array_DTL046 = new string[] { "", "是", "否" };
        DDLDTL046.DataSource = array_DTL046;
        DDLDTL046.DataBind();
        DDLDTL046.SelectedValue = "是";

        string[] array_DTL049 = new string[] { "", "是", "否" };
        DDLDTL049.DataSource = array_DTL049;
        DDLDTL049.DataBind();
        DDLDTL049.SelectedValue = "是";

        string[] array_DTL052 = new string[] { "", "是", "否" };
        DDLDTL052.DataSource = array_DTL052;
        DDLDTL052.DataBind();
        DDLDTL052.SelectedValue = "是";

        string[] array_DTL055 = new string[] { "", "是", "否" };
        DDLDTL055.DataSource = array_DTL055;
        DDLDTL055.DataBind();
        DDLDTL055.SelectedValue = "是";

        string[] array_DTL058 = new string[] { "", "是", "否" };
        DDLDTL058.DataSource = array_DTL058;
        DDLDTL058.DataBind();
        DDLDTL058.SelectedValue = "是";

        string[] array_DTL061 = new string[] { "", "是", "否" };
        DDLDTL061.DataSource = array_DTL061;
        DDLDTL061.DataBind();
        DDLDTL061.SelectedValue = "是";

        string[] array_DTL064 = new string[] { "", "是", "否" };
        DDLDTL064.DataSource = array_DTL064;
        DDLDTL064.DataBind();
        DDLDTL064.SelectedValue = "是";

        string[] array_DTL067 = new string[] { "", "是", "否" };
        DDLDTL067.DataSource = array_DTL067;
        DDLDTL067.DataBind();
        DDLDTL067.SelectedValue = "是";

        string[] array_DTL070 = new string[] { "", "是", "否" };
        DDLDTL070.DataSource = array_DTL070;
        DDLDTL070.DataBind();
        DDLDTL070.SelectedValue = "是";

        string[] array_DTL073 = new string[] { "", "是", "否" };
        DDLDTL073.DataSource = array_DTL073;
        DDLDTL073.DataBind();
        DDLDTL073.SelectedValue = "是";

        string[] array_DTL076 = new string[] { "", "是", "否" };
        DDLDTL076.DataSource = array_DTL076;
        DDLDTL076.DataBind();
        DDLDTL076.SelectedValue = "是";

        //TXTDTL021.Text = "(一)檢查單位及人員：" + System.Environment.NewLine + "(二)承辦監造技師：" + System.Environment.NewLine + "(三)水土保持義務人：";
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

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        Class01 BASEAPP = new Class01();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        string sDTLE000 = LBDTL001.Text + "";
        string sDTLE002 = TXTDTL002.Text + "";
        string sDTLE003 = BASEAPP.SQLstrValue(TXTDTL003.Text + "");
        string sDTLE004 = BASEAPP.SQLstrValue(TXTDTL004.Text + "");
        string sDTLE005 = BASEAPP.SQLstrValue(TXTDTL005.Text + "");
        string sDTLE006 = BASEAPP.SQLstrValue(TXTDTL006.Text + "");
        string sDTLE007 = BASEAPP.SQLstrValue(TXTDTL007.Text + "");
        string sDTLE016 = DDLDTL016.SelectedValue + "";
        string sDTLE017 = BASEAPP.SQLstrValue(TXTDTL017.Text + "");
        string sDTLE018 = BASEAPP.SQLstrValue(TXTDTL018.Text + "");
        string sDTLE019 = DDLDTL019.SelectedValue + "";
        string sDTLE020 = BASEAPP.SQLstrValue(TXTDTL020.Text + "");
        string sDTLE021 = BASEAPP.SQLstrValue(TXTDTL021.Text + "");
        string sDTLE022 = DDLDTL022.SelectedValue + "";
        string sDTLE023 = BASEAPP.SQLstrValue(TXTDTL023.Text + "");
        string sDTLE024 = BASEAPP.SQLstrValue(TXTDTL024.Text + "");
        string sDTLE025 = DDLDTL025.SelectedValue + "";
        string sDTLE026 = BASEAPP.SQLstrValue(TXTDTL026.Text + "");
        string sDTLE027 = BASEAPP.SQLstrValue(TXTDTL027.Text + "");
        string sDTLE028 = DDLDTL028.SelectedValue + "";
        string sDTLE029 = BASEAPP.SQLstrValue(TXTDTL029.Text + "");
        string sDTLE030 = BASEAPP.SQLstrValue(TXTDTL030.Text + "");
        string sDTLE031 = DDLDTL031.SelectedValue + "";
        string sDTLE032 = BASEAPP.SQLstrValue(TXTDTL032.Text + "");
        string sDTLE033 = BASEAPP.SQLstrValue(TXTDTL033.Text + "");
        string sDTLE034 = DDLDTL034.SelectedValue + "";
        string sDTLE035 = BASEAPP.SQLstrValue(TXTDTL035.Text + "");
        string sDTLE036 = BASEAPP.SQLstrValue(TXTDTL036.Text + "");
        string sDTLE037 = DDLDTL037.SelectedValue + "";
        string sDTLE038 = BASEAPP.SQLstrValue(TXTDTL038.Text + "");
        string sDTLE039 = BASEAPP.SQLstrValue(TXTDTL039.Text + "");
        string sDTLE040 = DDLDTL040.SelectedValue + "";
        string sDTLE041 = BASEAPP.SQLstrValue(TXTDTL041.Text + "");
        string sDTLE042 = BASEAPP.SQLstrValue(TXTDTL042.Text + "");
        string sDTLE043 = DDLDTL043.SelectedValue + "";
        string sDTLE044 = BASEAPP.SQLstrValue(TXTDTL044.Text + "");
        string sDTLE045 = BASEAPP.SQLstrValue(TXTDTL045.Text + "");
        string sDTLE046 = DDLDTL046.SelectedValue + "";
        string sDTLE047 = BASEAPP.SQLstrValue(TXTDTL047.Text + "");
        string sDTLE048 = BASEAPP.SQLstrValue(TXTDTL048.Text + "");
        string sDTLE049 = DDLDTL049.SelectedValue + "";
        string sDTLE050 = BASEAPP.SQLstrValue(TXTDTL050.Text + "");
        string sDTLE051 = BASEAPP.SQLstrValue(TXTDTL051.Text + "");
        string sDTLE052 = DDLDTL052.SelectedValue + "";
        string sDTLE053 = BASEAPP.SQLstrValue(TXTDTL053.Text + "");
        string sDTLE054 = BASEAPP.SQLstrValue(TXTDTL054.Text + "");
        string sDTLE055 = DDLDTL055.SelectedValue + "";
        string sDTLE056 = BASEAPP.SQLstrValue(TXTDTL056.Text + "");
        string sDTLE057 = BASEAPP.SQLstrValue(TXTDTL057.Text + "");
        string sDTLE058 = DDLDTL058.SelectedValue + "";
        string sDTLE059 = BASEAPP.SQLstrValue(TXTDTL059.Text + "");
        string sDTLE060 = BASEAPP.SQLstrValue(TXTDTL060.Text + "");
        string sDTLE061 = DDLDTL061.SelectedValue + "";
        string sDTLE062 = BASEAPP.SQLstrValue(TXTDTL062.Text + "");
        string sDTLE063 = BASEAPP.SQLstrValue(TXTDTL063.Text + "");
        string sDTLE064 = DDLDTL064.SelectedValue + "";
        string sDTLE065 = BASEAPP.SQLstrValue(TXTDTL065.Text + "");
        string sDTLE066 = BASEAPP.SQLstrValue(TXTDTL066.Text + "");
        string sDTLE067 = DDLDTL067.SelectedValue + "";
        string sDTLE068 = BASEAPP.SQLstrValue(TXTDTL068.Text + "");
        string sDTLE069 = BASEAPP.SQLstrValue(TXTDTL069.Text + "");
        string sDTLE070 = DDLDTL070.SelectedValue + "";
        string sDTLE071 = BASEAPP.SQLstrValue(TXTDTL071.Text + "");
        string sDTLE072 = BASEAPP.SQLstrValue(TXTDTL072.Text + "");
        string sDTLE073 = DDLDTL073.SelectedValue + "";
        string sDTLE074 = BASEAPP.SQLstrValue(TXTDTL074.Text + "");
        string sDTLE075 = BASEAPP.SQLstrValue(TXTDTL075.Text + "");
        string sDTLE076 = DDLDTL076.SelectedValue + "";
        string sDTLE077 = BASEAPP.SQLstrValue(TXTDTL077.Text + "");
        string sDTLE078 = BASEAPP.SQLstrValue(TXTDTL078.Text + "");
        string sDTLE079 = BASEAPP.SQLstrValue(TXTDTL079.Text + "");
        string sDTLE080 = BASEAPP.SQLstrValue(TXTDTL080.Text + "");
        string sDTLE081 = BASEAPP.SQLstrValue(TXTDTL081.Text + "");
        string sDTLE082 = BASEAPP.SQLstrValue(TXTDTL082.Text + "");
        string sDTLE083 = BASEAPP.SQLstrValue(TXTDTL083.Text + "");
        string sDTLE084 = BASEAPP.SQLstrValue(TXTDTL084.Text + "");
        string sDTLE085 = BASEAPP.SQLstrValue(TXTDTL085.Text + "");
        string sDTLE086 = BASEAPP.SQLstrValue(TXTDTL086.Text + "");
        string sDTLE087 = BASEAPP.SQLstrValue(TXTDTL087.Text + "");
        
        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL05 "; 
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLE000 = '" + sDTLE000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL05 (SWC000,DTLE000) VALUES ('" + sSWC000 + "','" + sDTLE000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL05 Set ";

            sEXESQLSTR = sEXESQLSTR + " DTLE001 = DTLE000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLE002 ='" + sDTLE002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE003 ='" + sDTLE003 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE004 ='" + sDTLE004 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE005 ='" + sDTLE005 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE006 ='" + sDTLE006 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE007 ='" + sDTLE007 + "', ";

            sEXESQLSTR = sEXESQLSTR + " DTLE016 ='" + sDTLE016 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE017 ='" + sDTLE017 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE018 ='" + sDTLE018 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE019 ='" + sDTLE019 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE020 ='" + sDTLE020 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE021 ='" + sDTLE021 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE022 ='" + sDTLE022 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE023 ='" + sDTLE023 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE024 ='" + sDTLE024 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE025 ='" + sDTLE025 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE026 ='" + sDTLE026 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE027 ='" + sDTLE027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE028 ='" + sDTLE028 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE029 ='" + sDTLE029 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE030 ='" + sDTLE030 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE031 ='" + sDTLE031 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE032 ='" + sDTLE032 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE033 ='" + sDTLE033 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE034 ='" + sDTLE034 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE035 ='" + sDTLE035 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE036 ='" + sDTLE036 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE037 ='" + sDTLE037 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE038 ='" + sDTLE038 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE039 ='" + sDTLE039 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE040 ='" + sDTLE040 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE041 ='" + sDTLE041 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE042 ='" + sDTLE042 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE043 ='" + sDTLE043 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE044 ='" + sDTLE044 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE045 ='" + sDTLE045 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE046 ='" + sDTLE046 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE047 ='" + sDTLE047 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE048 ='" + sDTLE048 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE049 ='" + sDTLE049 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE050 ='" + sDTLE050 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE051 ='" + sDTLE051 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE052 ='" + sDTLE052 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE053 ='" + sDTLE053 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE054 ='" + sDTLE054 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE055 ='" + sDTLE055 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE056 ='" + sDTLE056 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE057 ='" + sDTLE057 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE058 ='" + sDTLE058 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE059 ='" + sDTLE059 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE060 ='" + sDTLE060 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE061 ='" + sDTLE061 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE062 ='" + sDTLE062 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE063 ='" + sDTLE063 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE064 ='" + sDTLE064 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE065 ='" + sDTLE065 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE066 ='" + sDTLE066 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE067 ='" + sDTLE067 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE068 ='" + sDTLE068 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE069 ='" + sDTLE069 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE070 ='" + sDTLE070 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE071 ='" + sDTLE071 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE072 ='" + sDTLE072 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE073 ='" + sDTLE073 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE074 ='" + sDTLE074 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE075 ='" + sDTLE075 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE076 ='" + sDTLE076 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE077 ='" + sDTLE077 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE078 ='" + sDTLE078 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE079 ='" + sDTLE079 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE080 ='" + sDTLE080 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE081 ='" + sDTLE081 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE082 ='" + sDTLE082 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE083 ='" + sDTLE083 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE084 ='" + sDTLE084 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE085 ='" + sDTLE085 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE086 ='" + sDTLE086 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE087 ='" + sDTLE087 + "', ";
            
            //sEXESQLSTR = sEXESQLSTR + " DTLE005 ='" + sDTLE005 + "', ";

            //sEXESQLSTR = sEXESQLSTR + " DTLE008 ='" + sDTLE008 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE009 ='" + sDTLE009 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE010 ='" + sDTLE010 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE011 ='" + sDTLE011 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE012 ='" + sDTLE012 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE013 ='" + sDTLE013 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE014 ='" + sDTLE014 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE015 ='" + sDTLE015 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLE000 = '" + sDTLE000 + "'";

            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";

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
                    // 限制檔案大小，限制為 10MB
                    if (filesize > 10000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    // 限制檔案大小，限制為 50MB
                    if (filesize > 50000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp"] + CaseId;

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
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        
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
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                break;
            case "DOC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
    }



    protected void TXTDTL083_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL083_FileUpload, TXTDTL083, "TXTDTL083", "_" + rDTLNO + "_05_doc1", null, Link083);

    }

    protected void TXTDTL084_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL084_FileUpload, TXTDTL084, "TXTDTL084", "_" + rDTLNO + "_05_doc2", null, Link084);

    }

    protected void TXTDTL083_fileclean_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL083, null, Link083, "DTLE083", "TXTDTL083", 0, 0);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLE000 = LBDTL001.Text + "";

        string sEXESQLSTR = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL05 "; 
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLE000 = '" + sDTLE000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL05 (SWC000,DTLE000) VALUES ('" + sSWC000 + "','" + sDTLE000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL05 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLE000 = '" + sDTLE000 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }

        SaveCase_Click(sender, e);

    }

    protected void TXTDTL085_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL085_FileUpload, TXTDTL085, "TXTDTL085", "_" + rDTLNO + "_05_sign1", null, HyperLink085);
    }
    protected void TXTDTL086_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL086_FileUpload, TXTDTL086, "TXTDTL086", "_" + rDTLNO + "_05_sign2", null, HyperLink086);

    }
    protected void TXTDTL087_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL087_FileUpload, TXTDTL087, "TXTDTL087", "_" + rDTLNO + "_05_sign3", null, HyperLink087);
    }

    protected void TXTDTL085_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL085, null, HyperLink085, "DTLE085", "TXTDTL085", 0, 0);
    }

    protected void TXTDTL086_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL086, null, HyperLink086, "DTLE086", "TXTDTL086", 0, 0);
    }

    protected void TXTDTL087_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL087, null, HyperLink087, "DTLE087", "TXTDTL087", 0, 0);
    }
}