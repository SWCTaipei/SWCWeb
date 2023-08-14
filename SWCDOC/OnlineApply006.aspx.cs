using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_OnlineApply006 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();
        LoadSwcClass01 LoadApp = new LoadSwcClass01();
        Class20 C20 = new Class20();

        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        if (!IsPostBack)
        {
            C20.swcLogRC("OnlineApply006", "義務人及技師變更報備", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
            if (rRRPG == "55")
            {
                string LINK = "";
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");
                if (LoginR) { LINK = "OnlineApply006v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO; } else { Response.Redirect("SWC001.aspx"); }
                Response.Redirect(LINK);
            }
            else
            {
                switch (ssUserType)
                {
                    case "":
                        Response.Redirect("SWC001.aspx");
                        break;
                    case "01":
                        Response.Redirect("OnlineApply006v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO);
                        break;
                    case "02":
                    case "03":
                    case "04":
                    case "08":
                    case "09":
                        if (rOLANO == "") { Response.Redirect("SWC001.aspx"); }
                        else { GetOLA02Data(rSWCNO, rOLANO); }
                        break;
                    default:
                        Response.Redirect("SWC000.aspx");
                        break;
                }
				if(ssUserType == "08") DataLock.Visible = false;
            }
        }
        else { if (ssUserType == "") { Response.Redirect("SWC001.aspx"); } }

        //全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用
    }

    private void GetOLA02Data(string v, string v2)
    {
        string ssUserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";

        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";

        GenDropDownList();

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
                string tSWC000 = readeSwc["SWC000"] + "";
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;

                #region Label                           
                string[] aLBValue = new string[] { tSWC025 };
                Label[] aLabel = new Label[] { LBSWC025 };
                for (int i = 0; i < aLBValue.Length; i++)
                {
                    string strTBValue = aLBValue[i];
                    System.Web.UI.WebControls.Label LabelObj = aLabel[i];
                    LabelObj.Text = strTBValue;
                }
                #endregion
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string rONA000 = GetONAID();
                TXTONA001.Text = rONA000;

                switch (ssUserType)
                {
                    case "01":
                        TXTONA004.Text = ssUserID;
                        TXTONA005.Text = ssUserPW;
                        break;
                    case "02":
                        DDLDTL008.SelectedValue = ssUserID;
                        TXTONA009.Text = SBApp.GetETUser(ssUserID, "OrgIssNo");

                        DDLDTL012.SelectedValue = ssUserID;
                        TXTONA013.Text = SBApp.GetETUser(ssUserID, "OrgIssNo");
                        break;
                }
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply06 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA06001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA06002"] + "";
                    string tONA003 = readeONA["ONA06003"] + "";
                    string tONA004 = readeONA["ONA06004"] + "";
                    string tONA005 = readeONA["ONA06005"] + "";
                    string tONA006 = readeONA["ONA06006"] + "";
                    string tONA007 = readeONA["ONA06007"] + "";
                    string tONA008 = readeONA["ONA06008"] + "";
                    string tONA009 = readeONA["ONA06009"] + "";
                    string tONA010 = readeONA["ONA06010"] + "";
                    string tONA011 = readeONA["ONA06011"] + "";
                    string tONA012 = readeONA["ONA06012"] + "";
                    string tONA013 = readeONA["ONA06013"] + "";
                    string tONA014 = readeONA["ONA06014"] + "";
                    string tONA016 = readeONA["ONA06016"] + "";
                    string tONA017 = readeONA["ONA06017"] + "";

                    tDATALOCK = readeONA["DATALOCK"] + "";

                    TXTONA001.Text = v2;
                    if (tONA002 == "1") { CHKONA002.Checked = true; }
                    if (tONA007 == "1") { CHKONA007.Checked = true; }
                    if (tONA011 == "1") { CHKONA011.Checked = true; }

                    DDLDTL008.SelectedValue = tONA008;
                    DDLDTL012.SelectedValue = tONA012;

                    TXTONA003.Text = tONA003;
                    TXTONA004.Text = tONA004;
                    TXTONA005.Text = tONA005;
                    TXTONA006.Text = tONA006;
                    TXTONA009.Text = tONA009;
                    TXTONA010.Text = tONA010;
                    TXTONA013.Text = tONA013;
                    TXTONA014.Text = tONA014;
                    TXTONA016.Text = tONA016;
                    TXTONA017.Text = tONA017;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tONA006, tONA010, tONA014, tONA017 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link006, Link010, Link014, Link017 };

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
                            Class1 C1 = new Class1();
                            C1.FilesSortOut(strFileName, v, "");
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/"+ v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                }
            }
        }
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
		
		#region-義務人資訊
        AddressApiClass AAC = new AddressApiClass();
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
        
		
		using (SqlConnection ObliConn = new SqlConnection(connectionString.ConnectionString))
        {
            ObliConn.Open();

            string SqlStr = "";
			
			if(v2 == "AddNew")
			{
				SqlStr = SqlStr + " select * from SWCObligor ";
				SqlStr = SqlStr + "  Where SWC000 = '" + v + "' ";
				SqlStr = SqlStr + "  order by 序號 ";
			}
			else
			{
				SqlStr = SqlStr + " select * from OnlineApply06_SWCObligor ";
				SqlStr = SqlStr + "  Where SWC000 = '" + v + "' and OA06 = '" + v2 + "' ";
				SqlStr = SqlStr + "  order by 序號 ";
			}
			
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
	}

    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA06" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA06" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA06001) AS MAXID from OnlineApply06 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA06001,9) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 6, 6)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(6, '0');
                }
            }
        }
        return _ReturnVal;
    }

    private void GenDropDownList()
    {
        //技師名單

        string strSQLETUser = " select * from ETUsers where status='已開通' Order by ETName ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readerUser;
            SqlCommand objCmdRV = new SqlCommand(strSQLETUser, SwcConn);
            readerUser = objCmdRV.ExecuteReader();

            DDLDTL008.Items.Clear();
            DDLDTL012.Items.Clear();
            this.DDLDTL008.Items.Add(new ListItem("", ""));
            this.DDLDTL012.Items.Add(new ListItem("", ""));

            while (readerUser.Read())
            {
                string tETID   = readerUser["ETID"] + "";
                string tETName = readerUser["ETName"] + "";

                this.DDLDTL008.Items.Add(new ListItem(tETName, tETID));
                this.DDLDTL012.Items.Add(new ListItem(tETName, tETID));
            }
            readerUser.Close();
            objCmdRV.Dispose();

        }
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
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
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".pdf", ".odt", ".xls", ".xlsx", ".doc", ".docx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇pdf、odt或doc檔案格式上傳，謝謝!!");
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
                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
        string csCaseID2 = TXTONA001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update OnlineApply06 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and ONA06001 = '" + csCaseID2 + "' ";

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
    protected void TXTONA006_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTONA006_fileupload, TXTONA006, "TXTONA006", "_" + rONANO + "_ONA06_DOC1", null, Link006);

    }

    protected void TXTONA006_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA006, null, Link006, "ONA06006", "TXTONA006", 320, 180);
    }

    protected void TXTONA010_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTONA010_fileupload, TXTONA010, "TXTONA010", "_" + rONANO + "_ONA10_DOC2", null, Link010);

    }

    protected void TXTONA010_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA010, null, Link010, "ONA06010", "TXTONA010", 320, 180);

    }

    protected void TXTONA014_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTONA014_fileupload, TXTONA014, "TXTONA014", "_" + rONANO + "_ONA14_DOC3", null, Link014);

    }

    protected void TXTONA014_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA014, null, Link014, "ONA06014", "TXTONA014", 320, 180);

    }

    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTONA006", "TXTONA010", "TXTONA014", "TXTONA017" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTONA006, TXTONA010, TXTONA014, TXTONA017 };
        string csUpLoadField = "TXTONA006";
        TextBox csUpLoadAppoj = TXTONA006;

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
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        GBClass001 SWCApp = new GBClass001();

        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA06001 = TXTONA001.Text + "";
        string sONA06002 = CHKONA002.Checked + "";
        string sONA06003 = TXTONA003.Text + "";
        string sONA06004 = TXTONA004.Text + "";
        string sONA06005 = TXTONA005.Text + "";
        string sONA06006 = TXTONA006.Text + "";
        string sONA06007 = CHKONA007.Checked + "";
        string sONA06008 = DDLDTL008.SelectedValue + "";
        string sONA06009 = TXTONA009.Text + "";
        string sONA06010 = TXTONA010.Text + "";
        string sONA06011 = CHKONA011.Checked + "";
        string sONA06012 = DDLDTL012.SelectedValue + "";
        string sONA06013 = TXTONA013.Text + "";
        string sONA06014 = TXTONA014.Text + "";
        string sONA06016 = TXTONA016.Text + "";
        string sONA06017 = TXTONA017.Text + "";

        if (sONA06002 == "True") { sONA06002 = "1"; } else { sONA06002 = "0"; }
        if (sONA06007 == "True") { sONA06007 = "1"; } else { sONA06007 = "0"; }
        if (sONA06011 == "True") { sONA06011 = "1"; } else { sONA06011 = "0"; }

        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA06001 = '" + sONA06001 + "'";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply06 (SWC000,SWC002,ONA06001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA06001 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply06 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA06002 = '" + sONA06002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06003 = N'" + SWCApp.SQLstrValue(sONA06003) + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06004 = N'" + SWCApp.SQLstrValue(sONA06004) + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06005 = N'" + SWCApp.SQLstrValue(sONA06005) + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06006 = '" + sONA06006 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06007 = '" + sONA06007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06008 = '" + sONA06008 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06009 = '" + sONA06009 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06010 = '" + sONA06010 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06011 = '" + sONA06011 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06012 = '" + sONA06012 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06013 = '" + sONA06013 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06014 = '" + sONA06014 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06016 = N'" + SWCApp.SQLstrValue(sONA06016) + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06017 = '" + sONA06017 + "', ";

            sEXESQLUPD = sEXESQLUPD + " ONA06015 = '" + ssUserID + "', ";   //申請人
            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " where SWC000 = '" + sSWC000 + "' ";
            sEXESQLUPD = sEXESQLUPD + "   and ONA06001 = '" + sONA06001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);

            string thisPageAct= ((Button)sender).ID+"";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        }
		#region-義務人資訊
		if(CHKONA002.Checked){
			DataTable dtSWCP = new DataTable();
			dtSWCP = (DataTable)ViewState["SwcPeople"];
	
			string exeSQLStr = " delete OnlineApply06_SWCObligor where SWC000=@SWC000 and OA06=@OA06 ; ";
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
						exeSQLStr += " insert into OnlineApply06_SWCObligor (SWC000,序號,SWC013,SWC013ID,SWC013TEL,SWC014Zip,SWC014City,SWC014District,SWC014Address,OA06) VALUES (@SWC000,@序號,@SWC013,@SWC013ID,@SWC013TEL,@SWC014Zip,@SWC014City,@SWC014District,@SWC014Address,@OA06); ";
					else
						exeSQLStr = " insert into OnlineApply06_SWCObligor (SWC000,序號,SWC013,SWC013ID,SWC013TEL,SWC014Zip,SWC014City,SWC014District,SWC014Address,OA06) VALUES (@SWC000,@序號,@SWC013,@SWC013ID,@SWC013TEL,@SWC014Zip,@SWC014City,@SWC014District,@SWC014Address,@OA06); ";
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
							cmd.Parameters.Add(new SqlParameter("@OA06", TXTONA001.Text));
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
						cmd.Parameters.Add(new SqlParameter("@OA06", LBSWC002.Text));
						#endregion
						cmd.ExecuteNonQuery();
						cmd.Cancel();
					}
				}
			}
		}
		#endregion
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC005 = LBSWC005.Text;
        string sSWC025 = LBSWC025.Text;
        string sONA06001 = TXTONA001.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA06001 = '" + sONA06001 + "' ";
            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (readeSwc.HasRows)
            {
                while (readeSwc.Read())
                {
                    string sDATALOCK = readeSwc["DATALOCK"] + "";
                    if (sDATALOCK == "Y")
                    {
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply006v.aspx?SWCNO=" + sSWC000 + "&OLANO=" + sONA06001 + "'; </script>");
                        return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            SaveCase_Click(sender, e);

            //LOCKUSER2,ReviewResults,ReviewDoc,ResultsExplain,ReviewDirections,ReSendDeadline
            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply06 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewResults = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDoc = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ResultsExplain = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReviewDirections = '', ";
            sEXESQLSTR = sEXESQLSTR + "  ReSendDeadline = null, ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y', ";
            sEXESQLSTR += " SWC005 = N'" + sSWC005 + "', ";
            sEXESQLSTR += " SWC025 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING002 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING004 = N'" + sSWC025 + ";', ";
            sEXESQLSTR += " SING006 = N'" + ssUserName + "', ";
            sEXESQLSTR += " SING007 = N'送出',ONAHEAD=N'水土保持計畫義務人及技師變更報備',SING008 = N'待簽辦', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKUSER = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKDATE = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA06001 = '" + sONA06001 + "'";
            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@ONA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
            ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = strSQL3;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sSWC005));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sSWC025));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA06001));
                    cmd.Parameters.Add(new SqlParameter("@R001", ""));
                    cmd.Parameters.Add(new SqlParameter("@R002", ""));
                    cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                    //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                    cmd.Parameters.Add(new SqlParameter("@R005", ""));
                    cmd.Parameters.Add(new SqlParameter("@R006", ""));
                    cmd.Parameters.Add(new SqlParameter("@R007", GBC.GetETUser(ssUserID, "OrgName")));
                    cmd.Parameters.Add(new SqlParameter("@R008", GBC.GetETUser(ssUserID, "ETIdentity")));
                    cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/hhmm")));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA06001, "申請中", ssUserID, "", "");
            SendMailNotice(sSWC000);

            Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }
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

        //送出提醒名單：承辦人員、主管（科長，正工程司，股長，系統管理員）、章姿隆

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

                //寄件名單
                //寫死名單：章姿隆  ge-10706

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工程司" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706")
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = aUserName + " " + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增義務人/技師變更報備申請";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增義務人/技師變更報備申請，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }

    protected void UpLoadFileOk_Click(object sender, EventArgs e)
    {
        string thisPageAct = ((Button)sender).ID + "";
        string rONANO = TXTONA001.Text + "";

        error_msg.Text = "";

        switch (thisPageAct)
        {
            case "TXTONA017_fileuploadok":
                FileUpLoadApp("DOC", TXTONA017_fileupload, TXTONA017, "TXTONA017", "_" + rONANO + "_ONA17_DOC1", null, Link017);
                break;
        }
    }

    protected void UpLoadFileDel_Click(object sender, EventArgs e)
    {
        string thisPageAct = ((Button)sender).ID + "";
        string rONANO = TXTONA001.Text + "";

        error_msg.Text = "";

        switch (thisPageAct)
        {
            case "TXTONA017_fileuploaddel":
                DeleteUpLoadFile("DOC", TXTONA017, null, Link017, "ONA06017", "TXTONA017", 320, 180);
                break;
        }
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
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
	
}