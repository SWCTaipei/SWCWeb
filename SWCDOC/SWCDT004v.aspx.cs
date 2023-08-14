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
        string rRRPage = Request.QueryString["RRPG"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        //if (rCaseId == "" || ssUserID=="")
        //{
        //    Response.Redirect("SWC001.aspx");
        //}

        if (!IsPostBack)
        {
            GenerateDropDownList();
            Data2Page(rCaseId, rDTLId);
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

        //全區供用

        SBApp.ViewRecord("颱風豪雨設施自主檢查表", "view", "");

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
                string strSQLRV2 = " select D4.*,ISNULL(DE.DENAME,DE2.DENAME) AS DTLD085DESC from SWCDTL04 D4 ";
                strSQLRV2 += " LEFT JOIN DisasterEvent DE ON D4.DTLD085=DE.DENo ";
                strSQLRV2 += " LEFT JOIN DisasterEvent DE2 ON D4.DENo=DE2.DENo ";
                strSQLRV2 = strSQLRV2 + " where D4.SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   and D4.DTLD000 = '" + v2 + "' ";

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
                    string tDTLD085 = readeDTL["DTLD085"] + "";
                    string tDTLD085Desc = readeDTL["DTLD085DESC"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

                    LBSWC000.Text = v;
                    LBDTL001.Text = tDTLD001;
                    TXTDTL002.Text = SBApp.DateView(tDTLD002, "04");
                    TXTDTL003.Text = tDTLD003;
                    TXTDTL004.Text = tDTLD004;
                    
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

                    TXTDTL077.Text = tDTLD077.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL079.Text = tDTLD079.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL081.Text = tDTLD081.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL083.Text = tDTLD083.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                    TXTDTL085.Text = tDTLD085Desc;

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
                            Class1 C1 = new Class1();
                            C1.FilesSortOut(strFileName,v,"");
                            string tempImgPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName + "?ts=" + System.DateTime.Now.Millisecond;
                            ImgFileObj.ImageUrl = tempImgPateh;
                            ImgFileObj.NavigateUrl = tempImgPateh;
                        }
                    }
                    //按鈕處理
                    if (tDATALOCK == "Y")
                    {
                        DataLock.Visible = false;
                        SaveCase.Visible = false;

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
        string[] array_ALLItem = new string[] { "", "是", "否" };
        //DDLDTL006.DataSource = array_DTL006;
        //DDLDTL006.DataBind();

        TXTDTL079.Text = "(一)水土保持義務人：" + System.Environment.NewLine + "(二)承辦監造技師：" + System.Environment.NewLine + "(三)施工廠商：";
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
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }
    
}