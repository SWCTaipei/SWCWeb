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

public partial class SWCDOC_SWCDT004 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        if (rCaseId == "" || ssUserID=="")
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
        
        //全區供用

        SBApp.ViewRecord("颱風豪雨設施自主檢查表", "update", "");

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
        
        LBSWC000.Text = v;

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

                LBSWC005.Text = tSWC005;
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
                string strSQLRV2 = " select * from SWCDTL04 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   and DTLD000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLD001 = readeDTL["DTLD001"] + "";
                    string tDTLD002 = readeDTL["DTLD002"] + "";
                    string tDTLD003 = readeDTL["DTLD003"] + "";
                    string tDTLD004 = readeDTL["DTLD004"] + "";
                    string tDTLD005 = readeDTL["DTLD005"] + "";
                    string tDTLD006 = readeDTL["DTLD006"] + "";
                    string tDTLD007 = readeDTL["DTLD007"] + "";
                    string tDTLD008 = readeDTL["DTLD008"] + "";
                    string tDTLD009 = readeDTL["DTLD009"] + "";
                    string tDTLD010 = readeDTL["DTLD010"] + "";
                    string tDTLD011 = readeDTL["DTLD011"] + "";
                    string tDTLD012 = readeDTL["DTLD012"] + "";
                    string tDTLD013 = readeDTL["DTLD013"] + "";
                    string tDTLD014 = readeDTL["DTLD014"] + "";
                    string tDTLD015 = readeDTL["DTLD015"] + "";
                    string tDTLD016 = readeDTL["DTLD016"] + "";
                    string tDTLD017 = readeDTL["DTLD017"] + "";
                    string tDTLD018 = readeDTL["DTLD018"] + "";
                    string tDTLD019 = readeDTL["DTLD019"] + "";
                    string tDTLD020 = readeDTL["DTLD020"] + "";
                    string tDTLD021 = readeDTL["DTLD021"] + "";
                    string tDTLD022 = readeDTL["DTLD022"] + "";
                    string tDTLD023 = readeDTL["DTLD023"] + "";
                    string tDTLD024 = readeDTL["DTLD024"] + "";
                    string tDTLD025 = readeDTL["DTLD025"] + "";
                    string tDTLD026 = readeDTL["DTLD026"] + "";
                    string tDTLD027 = readeDTL["DTLD027"] + "";
                    string tDTLD028 = readeDTL["DTLD028"] + "";
                    string tDTLD029 = readeDTL["DTLD029"] + "";
                    string tDTLD030 = readeDTL["DTLD030"] + "";
                    string tDTLD031 = readeDTL["DTLD031"] + "";
                    string tDTLD032 = readeDTL["DTLD032"] + "";
                    string tDTLD033 = readeDTL["DTLD033"] + "";
                    string tDTLD034 = readeDTL["DTLD034"] + "";
                    string tDTLD035 = readeDTL["DTLD035"] + "";
                    string tDTLD036 = readeDTL["DTLD036"] + "";
                    string tDTLD037 = readeDTL["DTLD037"] + "";
                    string tDTLD038 = readeDTL["DTLD038"] + "";
                    string tDTLD039 = readeDTL["DTLD039"] + "";
                    string tDTLD040 = readeDTL["DTLD040"] + "";
                    string tDTLD041 = readeDTL["DTLD041"] + "";
                    string tDTLD042 = readeDTL["DTLD042"] + "";
                    string tDTLD043 = readeDTL["DTLD043"] + "";
                    string tDTLD044 = readeDTL["DTLD044"] + "";
                    string tDTLD045 = readeDTL["DTLD045"] + "";
                    string tDTLD046 = readeDTL["DTLD046"] + "";
                    string tDTLD047 = readeDTL["DTLD047"] + "";
                    string tDTLD048 = readeDTL["DTLD048"] + "";
                    string tDTLD049 = readeDTL["DTLD049"] + "";
                    string tDTLD050 = readeDTL["DTLD050"] + "";
                    string tDTLD051 = readeDTL["DTLD051"] + "";
                    string tDTLD052 = readeDTL["DTLD052"] + "";
                    string tDTLD053 = readeDTL["DTLD053"] + "";
                    string tDTLD054 = readeDTL["DTLD054"] + "";
                    string tDTLD055 = readeDTL["DTLD055"] + "";
                    string tDTLD056 = readeDTL["DTLD056"] + "";
                    string tDTLD057 = readeDTL["DTLD057"] + "";
                    string tDTLD058 = readeDTL["DTLD058"] + "";
                    string tDTLD059 = readeDTL["DTLD059"] + "";
                    string tDTLD060 = readeDTL["DTLD060"] + "";
                    string tDTLD061 = readeDTL["DTLD061"] + "";
                    string tDTLD062 = readeDTL["DTLD062"] + "";
                    string tDTLD063 = readeDTL["DTLD063"] + "";
                    string tDTLD064 = readeDTL["DTLD064"] + "";
                    string tDTLD065 = readeDTL["DTLD065"] + "";
                    string tDTLD066 = readeDTL["DTLD066"] + "";
                    string tDTLD067 = readeDTL["DTLD067"] + "";
                    string tDTLD068 = readeDTL["DTLD068"] + "";
                    string tDTLD069 = readeDTL["DTLD069"] + "";
                    string tDTLD070 = readeDTL["DTLD070"] + "";
                    string tDTLD071 = readeDTL["DTLD071"] + "";
                    string tDTLD072 = readeDTL["DTLD072"] + "";
                    string tDTLD073 = readeDTL["DTLD073"] + "";
                    string tDTLD074 = readeDTL["DTLD074"] + "";
                    string tDTLD075 = readeDTL["DTLD075"] + "";
                    string tDTLD076 = readeDTL["DTLD076"] + "";
                    string tDTLD077 = readeDTL["DTLD077"] + "";
                    string tDTLD078 = readeDTL["DTLD078"] + "";
                    string tDTLD079 = readeDTL["DTLD079"] + "";
                    string tDTLD080 = readeDTL["DTLD080"] + "";
                    string tDTLD081 = readeDTL["DTLD081"] + "";
                    string tDTLD082 = readeDTL["DTLD082"] + "";
                    string tDTLD083 = readeDTL["DTLD083"] + "";
                    string tDTLD084 = readeDTL["DTLD084"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

                    LBSWC000.Text = v;
                    LBDTL001.Text = tDTLD001;
                    TXTDTL002.Text = SBApp.DateView(tDTLD002, "00");
                    DDLDTL003.SelectedValue = tDTLD003;
                    DDLDTL004.SelectedValue = tDTLD004;
                    
                    if (tDTLD005 == "1") { CHKDTL005.Checked = true; }
                    DDLDTL006.SelectedValue = tDTLD006;
                    TXTDTL007.Text = tDTLD007;
                    TXTDTL008.Text = tDTLD008;
                    if (tDTLD009 == "1") { CHKDTL009.Checked = true; }
                    DDLDTL010.SelectedValue = tDTLD010;
                    TXTDTL011.Text = tDTLD011;
                    TXTDTL012.Text = tDTLD012;
                    if (tDTLD013 == "1") { CHKDTL013.Checked = true; }
                    DDLDTL014.SelectedValue = tDTLD014;
                    TXTDTL015.Text = tDTLD015;
                    TXTDTL016.Text = tDTLD016;
                    if (tDTLD017 == "1") { CHKDTL017.Checked = true; }
                    DDLDTL018.SelectedValue = tDTLD018;
                    TXTDTL019.Text = tDTLD019;
                    TXTDTL020.Text = tDTLD020;
                    if (tDTLD021 == "1") { CHKDTL021.Checked = true; }
                    DDLDTL022.SelectedValue = tDTLD022;
                    TXTDTL023.Text = tDTLD023;
                    TXTDTL024.Text = tDTLD024;
                    if (tDTLD025 == "1") { CHKDTL025.Checked = true; }
                    DDLDTL026.SelectedValue = tDTLD026;
                    TXTDTL027.Text = tDTLD027;
                    TXTDTL028.Text = tDTLD028;
                    if (tDTLD029 == "1") { CHKDTL029.Checked = true; }
                    DDLDTL030.SelectedValue = tDTLD030;
                    TXTDTL031.Text = tDTLD031;
                    TXTDTL032.Text = tDTLD032;
                    if (tDTLD033 == "1") { CHKDTL033.Checked = true; }
                    DDLDTL034.SelectedValue = tDTLD034;
                    TXTDTL035.Text = tDTLD035;
                    TXTDTL036.Text = tDTLD036;
                    if (tDTLD037 == "1") { CHKDTL037.Checked = true; }
                    DDLDTL038.SelectedValue = tDTLD038;
                    TXTDTL039.Text = tDTLD039;
                    TXTDTL040.Text = tDTLD040;
                    if (tDTLD041 == "1") { CHKDTL041.Checked = true; }
                    DDLDTL042.SelectedValue = tDTLD042;
                    TXTDTL043.Text = tDTLD043;
                    TXTDTL044.Text = tDTLD044;
                    if (tDTLD045 == "1") { CHKDTL045.Checked = true; }
                    DDLDTL046.SelectedValue = tDTLD046;
                    TXTDTL047.Text = tDTLD047;
                    TXTDTL048.Text = tDTLD048;
                    if (tDTLD049 == "1") { CHKDTL049.Checked = true; }
                    DDLDTL050.SelectedValue = tDTLD050;
                    TXTDTL051.Text = tDTLD051;
                    TXTDTL052.Text = tDTLD052;
                    if (tDTLD053 == "1") { CHKDTL053.Checked = true; }
                    DDLDTL054.SelectedValue = tDTLD054;
                    TXTDTL055.Text = tDTLD055;
                    TXTDTL056.Text = tDTLD056;
                    if (tDTLD057 == "1") { CHKDTL057.Checked = true; }
                    DDLDTL058.SelectedValue = tDTLD058;
                    TXTDTL059.Text = tDTLD059;
                    TXTDTL060.Text = tDTLD060;
                    if (tDTLD061 == "1") { CHKDTL061.Checked = true; }
                    DDLDTL062.SelectedValue = tDTLD062;
                    TXTDTL063.Text = tDTLD063;
                    TXTDTL064.Text = tDTLD064;
                    if (tDTLD065 == "1") { CHKDTL065.Checked = true; }
                    DDLDTL066.SelectedValue = tDTLD066;
                    TXTDTL067.Text = tDTLD067;
                    TXTDTL068.Text = tDTLD068;
                    if (tDTLD069 == "1") { CHKDTL069.Checked = true; }
                    DDLDTL070.SelectedValue = tDTLD070;
                    TXTDTL071.Text = tDTLD071;
                    TXTDTL072.Text = tDTLD072;
                    if (tDTLD073 == "1") { CHKDTL073.Checked = true; }
                    DDLDTL074.SelectedValue = tDTLD074;
                    TXTDTL075.Text = tDTLD075;
                    TXTDTL076.Text = tDTLD076;

                    TXTDTL077.Text = tDTLD077;
                    TXTDTL078.Text = SBApp.DateView(tDTLD078, "00");
                    TXTDTL079.Text = tDTLD079;
                    TXTDTL080.Text = tDTLD080;
                    TXTDTL081.Text = tDTLD081;
                    TXTDTL082.Text = tDTLD082;
                    TXTDTL083.Text = tDTLD083;
                    TXTDTL084.Text = tDTLD084;

                    //點擊放大圖片類處理
                    string[] arrayFileName2 = new string[] { tDTLD080, tDTLD082, tDTLD084 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj2 = new System.Web.UI.WebControls.HyperLink[] { HyperLink080, HyperLink082, HyperLink084 };

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

                    //圖片類處理
                    string[] arrayFileName = new string[] { tDTLD080, tDTLD082 };
                    System.Web.UI.WebControls.Image[] arrayImgAppobj = new System.Web.UI.WebControls.Image[] { TXTDTL080_img, TXTDTL082_img };

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
                    if (tDATALOCK == "Y")
                    {
                        DataLock.Visible = false;
                        SaveCase.Visible = false;

                        Response.Write("<script>alert('資料已提交大地處，目前僅供瀏覽。'); location.href='SWCDT004v.aspx?SWCNO=" + v + "&DTLNO=" + v2 + "'; </script>");
                    }
                }
            }

        }
    }
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RD" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RD" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLD000) AS MAXID from SWCDTL04 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLD000,7) = '" + tempVal + "' ";

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
        string[] array_DTL003 = new string[] { "颱風", "豪雨", "地震" };
        DDLDTL003.DataSource = array_DTL003;
        DDLDTL003.DataBind();

        string[] array_DTL004 = new string[] { "整備完成", "待改善"}; 
        DDLDTL004.DataSource = array_DTL004;
        DDLDTL004.DataBind();

        TXTDTL079.Text = "(一)水土保持義務人：" + System.Environment.NewLine + "(二)承辦監造技師：" + System.Environment.NewLine + "(三)施工廠商：";
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        string sDTLD000 = LBDTL001.Text + "";
        
        string sDTLD002 = TXTDTL002.Text + "";
        string sDTLD003 = DDLDTL003.SelectedValue + "";
        string sDTLD004 = DDLDTL004.SelectedValue + "";
        string sDTLD005 = CHKDTL005.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD006 = DDLDTL006.SelectedValue + "";
        string sDTLD007 = TXTDTL007.Text + "";
        string sDTLD008 = TXTDTL008.Text + "";
        string sDTLD009 = CHKDTL009.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD010 = DDLDTL010.SelectedValue + "";
        string sDTLD011 = TXTDTL011.Text + "";
        string sDTLD012 = TXTDTL012.Text + "";
        string sDTLD013 = CHKDTL013.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD014 = DDLDTL014.SelectedValue + "";
        string sDTLD015 = TXTDTL015.Text + "";
        string sDTLD016 = TXTDTL016.Text + "";
        string sDTLD017 = CHKDTL017.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD018 = DDLDTL018.SelectedValue + "";
        string sDTLD019 = TXTDTL019.Text + "";
        string sDTLD020 = TXTDTL020.Text + "";
        string sDTLD021 = CHKDTL021.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD022 = DDLDTL022.SelectedValue + "";
        string sDTLD023 = TXTDTL023.Text + "";
        string sDTLD024 = TXTDTL024.Text + "";
        string sDTLD025 = CHKDTL025.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD026 = DDLDTL026.SelectedValue + "";
        string sDTLD027 = TXTDTL027.Text + "";
        string sDTLD028 = TXTDTL028.Text + "";
        string sDTLD029 = CHKDTL029.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD030 = DDLDTL030.SelectedValue + "";
        string sDTLD031 = TXTDTL031.Text + "";
        string sDTLD032 = TXTDTL032.Text + "";
        string sDTLD033 = CHKDTL033.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD034 = DDLDTL034.SelectedValue + "";
        string sDTLD035 = TXTDTL035.Text + "";
        string sDTLD036 = TXTDTL036.Text + "";
        string sDTLD037 = CHKDTL037.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD038 = DDLDTL038.SelectedValue + "";
        string sDTLD039 = TXTDTL039.Text + "";
        string sDTLD040 = TXTDTL040.Text + "";
        string sDTLD041 = CHKDTL041.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD042 = DDLDTL042.SelectedValue + "";
        string sDTLD043 = TXTDTL043.Text + "";
        string sDTLD044 = TXTDTL044.Text + "";
        string sDTLD045 = CHKDTL045.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD046 = DDLDTL046.Text + "";
        string sDTLD047 = TXTDTL047.Text + "";
        string sDTLD048 = TXTDTL048.Text + "";
        string sDTLD049 = CHKDTL049.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD050 = DDLDTL050.SelectedValue + "";
        string sDTLD051 = TXTDTL051.Text + "";
        string sDTLD052 = TXTDTL052.Text + "";
        string sDTLD053 = CHKDTL053.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD054 = DDLDTL054.SelectedValue + "";
        string sDTLD055 = TXTDTL055.Text + "";
        string sDTLD056 = TXTDTL056.Text + "";
        string sDTLD057 = CHKDTL057.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD058 = DDLDTL058.SelectedValue + "";
        string sDTLD059 = TXTDTL059.Text + "";
        string sDTLD060 = TXTDTL060.Text + "";
        string sDTLD061 = CHKDTL061.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD062 = DDLDTL062.SelectedValue + "";
        string sDTLD063 = TXTDTL063.Text + "";
        string sDTLD064 = TXTDTL064.Text + "";
        string sDTLD065 = CHKDTL065.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD066 = DDLDTL066.SelectedValue + "";
        string sDTLD067 = TXTDTL067.Text + "";
        string sDTLD068 = TXTDTL068.Text + "";
        string sDTLD069 = CHKDTL069.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD070 = DDLDTL070.SelectedValue + "";
        string sDTLD071 = TXTDTL071.Text + "";
        string sDTLD072 = TXTDTL072.Text + "";
        string sDTLD073 = CHKDTL073.Checked.ToString().Replace("False", "").Replace("True", "1");
        string sDTLD074 = DDLDTL074.SelectedValue + "";
        string sDTLD075 = TXTDTL075.Text + "";
        string sDTLD076 = TXTDTL076.Text + "";
        string sDTLD077 = TXTDTL077.Text + "";
        string sDTLD078 = TXTDTL078.Text + "";
        string sDTLD079 = TXTDTL079.Text + "";
        string sDTLD080 = TXTDTL080.Text + "";
        string sDTLD081 = TXTDTL081.Text + "";
        string sDTLD082 = TXTDTL082.Text + "";
        string sDTLD083 = TXTDTL083.Text + "";
        string sDTLD084 = TXTDTL084.Text + "";


        if (sDTLD077.Length > 800) { sDTLD077 = sDTLD077.Substring(0, 800); }
        if (sDTLD079.Length > 800) { sDTLD079 = sDTLD079.Substring(0, 800); }
        if (sDTLD081.Length > 200) { sDTLD081 = sDTLD081.Substring(0, 200); }
        if (sDTLD082.Length > 200) { sDTLD082 = sDTLD082.Substring(0, 200); }
        if (sDTLD083.Length > 200) { sDTLD083 = sDTLD083.Substring(0, 200); }


        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL04 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLD000 = '" + sDTLD000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL04 (SWC000,DTLD000) VALUES ('" + sSWC000 + "','" + sDTLD000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL04 Set ";
            sEXESQLSTR = sEXESQLSTR + " DTLD001 = DTLD000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLD002 ='" + sDTLD002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD003 ='" + sDTLD003 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD004 ='" + sDTLD004 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD005 ='" + sDTLD005 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD006 ='" + sDTLD006 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD007 ='" + sDTLD007 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD008 ='" + sDTLD008 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD009 ='" + sDTLD009 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD010 ='" + sDTLD010 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD011 ='" + sDTLD011 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD012 ='" + sDTLD012 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD013 ='" + sDTLD013 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD014 ='" + sDTLD014 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD015 ='" + sDTLD015 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD016 ='" + sDTLD016 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD017 ='" + sDTLD017 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD018 ='" + sDTLD018 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD019 ='" + sDTLD019 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD020 ='" + sDTLD020 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD021 ='" + sDTLD021 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD022 ='" + sDTLD022 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD023 ='" + sDTLD023 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD024 ='" + sDTLD024 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD025 ='" + sDTLD025 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD026 ='" + sDTLD026 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD027 ='" + sDTLD027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD028 ='" + sDTLD028 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD029 ='" + sDTLD029 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD030 ='" + sDTLD030 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD031 ='" + sDTLD031 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD032 ='" + sDTLD032 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD033 ='" + sDTLD033 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD034 ='" + sDTLD034 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD035 ='" + sDTLD035 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD036 ='" + sDTLD036 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD037 ='" + sDTLD037 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD038 ='" + sDTLD038 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD039 ='" + sDTLD039 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD040 ='" + sDTLD040 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD041 ='" + sDTLD041 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD042 ='" + sDTLD042 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD043 ='" + sDTLD043 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD044 ='" + sDTLD044 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD045 ='" + sDTLD045 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD046 ='" + sDTLD046 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD047 ='" + sDTLD047 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD048 ='" + sDTLD048 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD049 ='" + sDTLD049 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD050 ='" + sDTLD050 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD051 ='" + sDTLD051 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD052 ='" + sDTLD052 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD053 ='" + sDTLD053 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD054 ='" + sDTLD054 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD055 ='" + sDTLD055 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD056 ='" + sDTLD056 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD057 ='" + sDTLD057 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD058 ='" + sDTLD058 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD059 ='" + sDTLD059 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD060 ='" + sDTLD060 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD061 ='" + sDTLD061 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD062 ='" + sDTLD062 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD063 ='" + sDTLD063 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD064 ='" + sDTLD064 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD065 ='" + sDTLD065 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD066 ='" + sDTLD066 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD067 ='" + sDTLD067 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD068 ='" + sDTLD068 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD069 ='" + sDTLD069 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD070 ='" + sDTLD070 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD071 ='" + sDTLD071 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD072 ='" + sDTLD072 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD073 ='" + sDTLD073 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD074 ='" + sDTLD074 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD075 ='" + sDTLD075 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD076 ='" + sDTLD076 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD077 ='" + sDTLD077 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD078 ='" + sDTLD078 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD079 ='" + sDTLD079 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD080 ='" + sDTLD080 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD081 ='" + sDTLD081 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD082 ='" + sDTLD082 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD083 ='" + sDTLD083 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLD084 ='" + sDTLD084 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLD000 = '" + sDTLD000 + "'";

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
            //Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
            Response.Write("<script>alert('資料已存檔'); location.href='SWC002.aspx?CaseId=" + sSWC000 + "'; </script>");
        }
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL080", "TXTDTL082", "TXTDTL084" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL080, TXTDTL082, TXTDTL084 };
        string csUpLoadField = "TXTDTL080";
        TextBox csUpLoadAppoj = TXTDTL080;

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
                case "PIC2":
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".jpg", ".png", ".doc", ".pdf", ".dwg", ".dxf" };

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
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "PIC2":
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

        strSQLClearFieldValue = " update SWCDTL04 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLD001 = '" + csDTLID + "' ";

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
                FileLink.ImageUrl = "";
                FileLink.NavigateUrl = "";
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
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
    }

    protected void TXTDTL080_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL080_fileupload, TXTDTL080, "TXTDTL080", "_" + rDTLNO + "_04_photo1", null, HyperLink080);
    }

    protected void TXTDTL082_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL082_fileupload, TXTDTL082, "TXTDTL082", "_" + rDTLNO + "_04_photo2", null, HyperLink082);
    }

    protected void TXTDTL080_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL080, null, HyperLink080, "DTLD080", "TXTDTL080", 320, 180);
    }
    protected void TXTDTL082_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL082, null, HyperLink082, "DTLD082", "TXTDTL082", 320, 180);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLD000 = LBDTL001.Text + "";

        string sEXESQLSTR = "";

        SaveCase_Click(sender, e);

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL04 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLD000 = '" + sDTLD000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL04 (SWC000,DTLD000) VALUES ('" + sSWC000 + "','" + sDTLD000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL04 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLD000 = '" + sDTLD000 + "'";

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

                //寄件名單, 注意：ge-40523	陳世豪，寫死，必需寄！
                //寄件名單, 注意：ge-10754	施柏宇，寫死，必需寄！

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId == "ge-40523" || aUserId.Trim() == "ge-10754")
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;

                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增颱風防災自主檢查表";
                        string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增颱風防災自主檢查表，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }

                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增颱風防災自主檢查表";
                //string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增颱風防災自主檢查表，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                string[] arraySentMail02a = new string[] { SBApp.GetGeoUser(tSWC024ID,"Email") };
                string[] arraySentMail02b = new string[] { tSWC108 };

                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】已新增颱風防災自主檢查表";
                string ssMailBody02 = "您好，" + "【" + tSWC005 + "】已新增颱風防災自主檢查表，請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                
                bool MailTo02a = SBApp.Mail_Send(arraySentMail02a, ssMailSub02, ssMailBody02);
                bool MailTo02b = SBApp.Mail_Send(arraySentMail02b, ssMailSub02, ssMailBody02);

                string ssMailBody03 = "您好，【" + tSWC005 + "】已新增颱風防災自主檢查表，請至臺北市水土保持申請書件管理平台上瀏覽。";
                
                SBApp.SendSMS(tSWC013TEL, ssMailBody03);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }

    protected void TXTDTL084_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL084_fileupload, TXTDTL084, "TXTDTL084", "_" + rDTLNO + "_sign", null, HyperLink084);
    }

    protected void TXTDTL084_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL084, null, HyperLink084, "DTLD084", "TXTDTL084", 320, 180);
    }
}