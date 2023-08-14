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

public partial class SWCDOC_OnlineApply009 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
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

        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        if (!IsPostBack)
        {
            C20.swcLogRC("OnlineApply009", "完工申請", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                string LINK = "";
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");
                if (LoginR) { LINK = "OnlineApply009v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO; } else { LINK="SWC001.aspx"; }
                Response.Redirect(LINK);
            }
            else
            {
                if (rOLANO == "") { Response.Redirect("SWC001.aspx"); }
                else
                {
                    GetOLA02Data(rSWCNO, rOLANO);
                }
				if(ssUserType == "08") DataLock.Visible = false;
            }
        }
        else { if (ssUserName == "") { Response.Redirect("SWC001.aspx"); } }
		
		//簡易水保隱藏
        if(LBSWC007.Text != "簡易水保") { Required_02.Visible = true; }

        //全區供用
        SBApp.ViewRecord("水土保持計畫完工申報書", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用
		Page.MaintainScrollPositionOnPostBack = true;

    }

    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }
    private void GetOLA02Data(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";

        string tDATALOCK = "";
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
				string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC058 = readeSwc["SWC058"] + "";
                string tSWC117 = readeSwc["SWC117"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
				LBSWC007.Text = tSWC007;
                LBSWC025.Text = tSWC025;
                LBSWC013.Text = tSWC013;
                //LBSWC013a.Text = tSWC013;
                LBSWC013ID.Text = tSWC013ID;
                LBSWC014.Text = tSWC014;
                LBSWC038.Text = SBApp.DateView(tSWC038, "00");
                LBSWC039.Text = tSWC039;
                LBSWC043.Text = SBApp.DateView(tSWC043, "00");
                LBSWC044.Text = tSWC044;
                //LBSWC045a.Text = SBApp.GetETUser(tSWC045ID, "Name");
                LBSWC045Name.Text = SBApp.GetETUser(tSWC045ID, "Name");
                LBSWC045OrgIssNo.Text = SBApp.GetETUser(tSWC045ID, "OrgIssNo");
                LBSWC045OrgName.Text = SBApp.GetETUser(tSWC045ID, "OrgName");
                LBSWC045OrgGUINo.Text = SBApp.GetETUser(tSWC045ID, "OrgGUINo");
                LBSWC045OrgAddr.Text = SBApp.GetETUser(tSWC045ID, "OrgAddr");
                LBSWC045OrgTel.Text = SBApp.GetETUser(tSWC045ID, "OrgTel");
                LBSWC051.Text = SBApp.DateView(tSWC051, "00");
                LBSWC117.Text = tSWC117;

            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            ShowGvAll(v);

            if (v2 == "AddNew")
            {
                string rONA000 = GetONAID();

                TXTONA001.Text = rONA000;
                TXTONA002.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                string strSQLRV2 = " select * from OnlineApply09 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA09001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA09002"] + "";
                    string tONA003 = readeONA["ONA09003"] + "";
                    string tONA004 = readeONA["ONA09004"] + "";
                    string tONA005 = readeONA["ONA09005"] + "";
                    string tONA006 = readeONA["ONA09006"] + "";
                    string tONA007 = readeONA["ONA09007"] + "";
                    string tONA008 = readeONA["ONA09008"] + "";
                    string tONA009 = readeONA["ONA09009"] + "";
                    string tONA010 = readeONA["ONA09010"] + "";
                    string tONA011 = readeONA["ONA09011"] + "";
                    string tONA012 = readeONA["ONA09012"] + "";
                    string tONA013 = readeONA["ONA09013"] + "";
                    string tONA014 = readeONA["ONA09014"] + "";
                    string tONA015 = readeONA["ONA09015"] + "";
                    string tONA016 = readeONA["ONA09016"] + "";
                    tDATALOCK = readeONA["DATALOCK"] + "";

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = tONA003;
                    TXTONA004.Text = tONA004;
                    //TXTONA005.Text = SBApp.DateView(tONA005, "00");
                    TXTONA007.Text = tONA007;
                    TXTONA008.Text = tONA008;
                    if (tONA009 == "1") { CHKONA009.Checked = true; }
                    TBONA010.Text = tONA010;
                    DDLONA011.Text= tONA011;
                    TBONA012.Text = tONA012;
                    TBONA013.Text = tONA013;
                    TBONA014.Text = tONA014;
					TXTONA015.Text = tONA015;
					
                    if (tONA016 == "1") { CHKONA016.Checked = true; }
                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tONA003, tONA004, tONA007, tONA008, tONA014, tONA015};
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link003, Link004, Link007, Link008,Link014, Link015 };

                    for (int i = 0; i < arrayFileNameLink.Length; i++)
                    {
                        string strFileName = arrayFileNameLink[i];
                        System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];

                        //FileLinkObj.Visible = false;
                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            Class1 C1 = new Class1();
                            C1.FilesSortOut(strFileName, v, "");
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                using (SqlConnection SwcConnTest = new SqlConnection(connectionString.ConnectionString))
                {
                    SwcConnTest.Open();

                    strSQLRV = " select * from SwcItemChk where SWC000 = '" + v + "' and DTLRPNO = '" + v2 + "' ";

                    SqlDataReader readeTest;
                    SqlCommand objCmdTest = new SqlCommand(strSQLRV, SwcConnTest);
                    readeTest = objCmdTest.ExecuteReader();

                    if (!readeTest.HasRows)
                        v2 = "AddNew";
                    readeTest.Close();
                    objCmdTest.Dispose();
                }
            }
        }
        SetDtlData(v, v2);
    }

    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA09" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA09" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA09001) AS MAXID from OnlineApply09 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA09001,9) = '" + tempVal + "' ";

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

    private void ShowGvAll(string v)
    {
        //地籍...
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int nj = 0;

            string strSQLRV2 = "select * from SWCLAND";
            strSQLRV2 = strSQLRV2 + " Where SWC000 = '" + v + "' ";
            strSQLRV2 = strSQLRV2 + " order by convert(int,LAND000)  ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV2, ItemConn);
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

            }
            readerItem.Close();
        }
    }
    protected void GVCadastral_PageIndexChanged(object sender, EventArgs e)
    {
        GVCadastral.DataSource = ViewState["SwcCadastral"]; //GV分頁2 ViewState暫存網頁 關掉就消失

        //GridView1.DataSource = Session["Date"]; //GV分頁2 暫存網頁

        GVCadastral.DataBind();
    }
    protected void GVCadastral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string rCaseId = Request.QueryString["CaseId"] + "";
        GVCadastral.PageIndex = e.NewPageIndex; //抓取GV分頁頁數
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
                    List<string> allowedExtextsion02 = new List<string> { ".pdf", ".odt", ".xls", ".xlsx",".doc",".docx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇pdf、odt或doc檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

                case "CAD":
                    List<string> allowedExtextsion03 = new List<string> { ".dwg", ".DWG" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇DWG檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "IMG":
                    List<string> allowedExtextsion04 = new List<string> { ".jpg", ".JPG",".png",".PNG",".pdf",".PDF" };

                    if (allowedExtextsion04.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇jpg、png或pdf檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                    
            }

            // 限制檔案大小，限制為 50MB
            int filesize2 = UpLoadBar.PostedFile.ContentLength;

            if (filesize2 > 50000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50 Mb 以下檔案上傳，謝謝!!");
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
                    case "CAD":
                    case "DOC":
                    case "IMG":
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

        strSQLClearFieldValue = " update OnlineApply09 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and ONA09001 = '" + csCaseID2 + "' ";

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
            case "IMG":
            case "DOC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                FileLink.Visible = false;
                break;
            case "CAD":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                FileLink.Visible = false;
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }

    protected void TXTONA003_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTONA003_fileupload, TXTONA003, "TXTONA003", "_" + rONANO + "_ONA09_DOC1", null, Link003);
    }

    protected void TXTONA003_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA003, null, Link003, "ONA09003", "TXTONA003", 320, 180);

    }

    protected void TXTONA004_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTONA004_fileupload, TXTONA004, "TXTONA004", "_" + rONANO + "_ONA09_DOC2", null, Link004);
    }

    protected void TXTONA004_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA004, null, Link004, "ONA09004", "TXTONA004", 320, 180);
    }

    protected void TXTONA007_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTONA007_fileupload, TXTONA007, "TXTONA007", "_" + rONANO + "_ONA07_DOC3", null, Link007);

    }

    protected void TXTONA007_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA007, null, Link007, "ONA09007", "TXTONA007", 320, 180);
    }

    protected void TXTONA008_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("CAD", TXTONA008_fileupload, TXTONA008, "TXTONA008", "_" + rONANO + "_ONA09_CAD1", null, Link008);
    }
    protected void TXTONA008_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("CAD", TXTONA008, null, Link008, "ONA09008", "TXTONA008", 320, 180);
    }
    protected void TXTONA015_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("IMG", TXTONA015_fileupload, TXTONA015, "TXTONA015", "_" + rONANO + "_ONA09_CAD4", null, Link015);
    }
    protected void TXTONA015_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("CAD", TXTONA015, null, Link015, "ONA09015", "TXTONA015", 320, 180);
    }	
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        string pgSWC117 = LBSWC117.Text + "";
        string pgONA007 = TXTONA007.Text + "";
        #region 頁面資料檢查
        if (pgSWC117 == "是" && pgONA007 == "") { Response.Write("<script>alert('聯外排水屬抽排者，檢附水土保持專業技師簽證之查驗成果及後續管理維護計畫(包含至少3年期專業技師管理檢測委託契約)。');</script>");return; }
		#endregion

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA09001 = TXTONA001.Text + "";
        string sONA09002 = TXTONA002.Text + "";
        string sONA09003 = TXTONA003.Text + "";
        string sONA09004 = TXTONA004.Text + "";
        //string sONA09005 = TXTONA005.Text + "";
        string sONA09007 = TXTONA007.Text + "";
        string sONA09008 = TXTONA008.Text + "";
        string sONA09009 = CHKONA009.Checked + "";
        string sONA09010 = TBONA010.Text + "";
        string sONA09011 = DDLONA011.Text + "";
        string sONA09012 = TBONA012.Text + "";
        string sONA09013 = TBONA013.Text + "";
        string sONA09014 = TBONA014.Text + "";
        string sONA09015 = TXTONA015.Text + "";
        string sONA09016 = CHKONA016.Checked + "";
        string Q = "";

        if (sONA09009 == "True") { sONA09009 = "1"; } else { sONA09009 = "0"; }
        if (sONA09016 == "True") { sONA09016 = "1"; } else { sONA09016 = "0"; }

        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply09 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + " and ONA09001 = '" + sONA09001 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLUPD = " INSERT INTO OnlineApply09 (SWC000,SWC002,ONA09001) VALUES ('" + sSWC000 + "','" + sSWC002 + "','" + sONA09001 + "');"; Q = "A";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply09 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA09002 = '" + sONA09002 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09003 = '" + sONA09003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09004 = '" + sONA09004 + "', ";
            //sEXESQLUPD = sEXESQLUPD + " ONA09005 = '" + sONA09005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09007 = '" + sONA09007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09008 = '" + sONA09008 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09009 = '" + sONA09009 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09010 = '" + sONA09010 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09011 = '" + sONA09011 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09012 = '" + sONA09012 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09013 = '" + sONA09013 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09014 = '" + sONA09014 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09015 = '" + sONA09015 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA09016 = '" + sONA09016 + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA09001 = '" + sONA09001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
            #region.
            if (Q == "")
            {
                string strSQLText = " select * from SwcItemChk where SWC000 = '" + sSWC000 + "' and DTLRPNO = '" + sONA09001 + "' ";

                SqlDataReader readerTest;
                SqlCommand objCmdText = new SqlCommand(strSQLText, SwcConn);
                readerTest = objCmdText.ExecuteReader();

                if (!readerTest.HasRows) Q = "A";
                readeSwc.Close();
                objCmdSwc.Dispose();
            }
            #endregion
            SavChkSwcItem(Q);

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        }
        Session["SaveOk"] = "Y";
    }
    private void SavChkSwcItem(string v)
    {
        int gLine = 0;
        string exeSqlStr = "";
        string ssUserID = Session["ID"] + "";
        string nMSG01 = "";

        DataTable dtSDI = new DataTable();
        dtSDI = (DataTable)ViewState["SwcDocItem"];

        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            if (++gLine % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[gLine - 2].Cells[2].Text;

                HiddenField HDF001 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI001");
                HiddenField HDF011 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI011");
                Label LBSDI006 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("SDILB006");
                Label LBSDI003 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE03");
                Label LBSDI004 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE04");
                Label LBSDI005 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE05");
				Label LBSDI019 = (Label)SDIList.Rows[gLine - 1].Cells[3].FindControl("LB019");
                TextBox CHK01 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001");
                TextBox CHK01_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001_1");
                TextBox CHK01D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001D");
                TextBox CHK04 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK006_1");
                TextBox RCH01 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001");
                TextBox RCH01_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001_1");
                TextBox RCH01D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001D");
                TextBox RCH04 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004");
                TextBox RCH04_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004_1");
                TextBox RCH04D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004D");
                TextBox RCH05 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH005");
                TextBox RCH05_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH005_1");
                TextBox RCH06 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH006");
                TextBox RCH06_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH006_1");
                TextBox RCH09 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH009");
                TextBox TSIC2 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK002");
                TextBox TSIC7 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK007");
                DropDownList DDLDONE = (DropDownList)SDIList.Rows[gLine - 1].Cells[3].FindControl("DDLDONE");

                string mSWC000 = LBSWC000.Text;
                string mDTLE001 = TXTONA001.Text;

                string mSDI001 = HDF001.Value;
                string mSIC01a = LBSDI006.Text; if (mSIC01a.Trim() == "") mSIC01a = "0";
                //string CHK01temp = CHK01.Text == "" ? "0" : CHK01.Text;
                //string CHK01_1temp = CHK01_1.Text == "" ? "0" : CHK01_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK01temp) > Convert.ToDouble(CHK01_1temp)) { CHK01.Text= CHK01_1temp; CHK01_1.Text = CHK01temp; }
                string mSIC01b = CHK01.Text; if (mSIC01b.Trim() == "") mSIC01b = "0";
                string mSIC01c = CHK01_1.Text; if (mSIC01c.Trim() == "") mSIC01c = "0";
                string mSIC01r = RCH01.Text; if (mSIC01r.Trim() == "") mSIC01r = "0";
                //string mSIC01r2 = RCH01_1.Text; if (mSIC01r2.Trim() == "") mSIC01r2 = "0";
                string mSIC01Da = CHK01D.Text;
                string mSIC01Dr = RCH01D.Text;
                string mSIC04a = LBSDI003.Text; if (mSIC04a.Trim() == "") mSIC04a = "0";
                string CHK04temp = CHK04.Text == "" ? "0" : CHK04.Text;
                string CHK04_1temp = CHK04.Text == "" ? "0" : CHK04_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK04temp) > Convert.ToDouble(CHK04_1temp)) { CHK04.Text = CHK04_1temp; CHK04_1.Text = CHK04temp; }
                string mSIC04b = CHK04.Text; if (mSIC04b.Trim() == "") mSIC04b = "0";
                string mSIC04c = CHK04_1.Text; if (mSIC04c.Trim() == "") mSIC04c = "0";
                string mSIC04r = RCH04.Text; if (mSIC04r.Trim() == "") mSIC04r = "0";
                string mSIC04r2 = RCH04_1.Text; if (mSIC04r2.Trim() == "") mSIC04r2 = "0";
                string mSIC04Da = CHK04D.Text;
                string mSIC04Dr = RCH04D.Text;
                string mSIC05a = LBSDI004.Text; if (mSIC05a.Trim() == "") mSIC05a = "0";
                string CHK05temp = CHK05.Text == "" ? "0" : CHK05.Text;
                string CHK05_1temp = CHK05.Text == "" ? "0" : CHK05_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK05temp) > Convert.ToDouble(CHK05_1temp)) { CHK05.Text = CHK05_1temp; CHK05_1.Text = CHK05temp; }
                string mSIC05b = CHK05.Text; if (mSIC05b.Trim() == "") mSIC05b = "0";
                string mSIC05c = CHK05_1.Text; if (mSIC05c.Trim() == "") mSIC05c = "0";
                string mSIC05r = RCH05.Text; if (mSIC05r.Trim() == "") mSIC05r = "0";
                string mSIC05r2 = RCH05_1.Text; if (mSIC05r2.Trim() == "") mSIC05r2 = "0";
                string mSIC06a = LBSDI005.Text; if (mSIC06a.Trim() == "") mSIC06a = "0";
                string CHK06temp = CHK06.Text == "" ? "0" : CHK06.Text;
                string CHK06_1temp = CHK06.Text == "" ? "0" : CHK06_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK06temp) > Convert.ToDouble(CHK06_1temp)) { CHK06.Text = CHK06_1temp; CHK06_1.Text = CHK06temp; }
                string mSIC06b = CHK06.Text; if (mSIC06b.Trim() == "") mSIC06b = "0";
                string mSIC06c = CHK06_1.Text; if (mSIC06c.Trim() == "") mSIC06c = "0";
                string mSIC06r = RCH06.Text; if (mSIC06r.Trim() == "") mSIC06r = "0";
                string mSIC06r2 = RCH06_1.Text; if (mSIC06r2.Trim() == "") mSIC06r2 = "0";
                string mSIC08 = "";
                string mSIC09a = RCH09.Text;
                string mSIC09b = DDLDONE.SelectedItem.Text;
				string mSIC19 = LBSDI019.Text;

                if (mSIC09b == "完成") mSIC08 = TXTONA002.Text;

                //數量差異百分比：SIC02=(D2-D1)/D1
                double mSIC02 = 0; double mSIC02_1 = 0;
                /*
				if (mSIC19 == "是"){
                    string[] sArray = mSIC01a.Split('~');
                    string Arr_a = sArray[0];
                    string Arr_b = sArray[1];
                    if (Convert.ToDouble(Arr_a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(Arr_a)) / Convert.ToDouble(Arr_a) * 100, 2);
                    if (Convert.ToDouble(Arr_b) != 0) mSIC02_1 = Math.Round((double)(Convert.ToDouble(mSIC01c) - Convert.ToDouble(Arr_b)) / Convert.ToDouble(Arr_b) * 100, 2);
                }
                else {
                    if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
                }
				*/
				if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);

                //尺寸差異百分比：1:(A2-A1)/A1，2:((A2*B2)-(A1*B1))/(A1*B1)，3:((A2*B2*C2)-(A1*B1*C1))/(A1*B1*C1)
                //維度
                string mSIC03 = HDF011.Value;
                double mSIC07 = 0; double mSIC07_1 = 0;
                if (mSIC19 == "是")
                {
					double cA1 = Convert.ToDouble(mSIC04a.Split('~')[0]), cB1 = Convert.ToDouble(mSIC05a.Split('~')[0]), cC1 = Convert.ToDouble(mSIC06a.Split('~')[0]);
					double cA2 = Convert.ToDouble(mSIC04b), cB2 = Convert.ToDouble(mSIC05b), cC2 = Convert.ToDouble(mSIC06b);
					if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
					if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
					if (cA1 * cB1 * cC1 != 0) mSIC07 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);
					
					cA1 = Convert.ToDouble(mSIC04a.Split('~')[1]); cB1 = Convert.ToDouble(mSIC05a.Split('~')[1]); cC1 = Convert.ToDouble(mSIC06a.Split('~')[1]);
					cA2 = Convert.ToDouble(mSIC04c); cB2 = Convert.ToDouble(mSIC05c); cC2 = Convert.ToDouble(mSIC06c);
					if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
					if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
					if (cA1 * cB1 * cC1 != 0) mSIC07_1 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);
                }
                else
                {
                    if (tSDIFD004 == "其他")
                    {
                        if (TSIC2.Text + "" != "") { mSIC02 = Convert.ToDouble(TSIC2.Text + ""); } else { mSIC02 = 0; }
                        if (TSIC2.Text + "" != "") { mSIC07 = Convert.ToDouble(TSIC7.Text + ""); } else { mSIC07 = 0; }
                    }
                    else
                    {
                        double cA1 = Convert.ToDouble(mSIC04a), cB1 = Convert.ToDouble(mSIC05a), cC1 = Convert.ToDouble(mSIC06a);
                        double cA2 = Convert.ToDouble(mSIC04b), cB2 = Convert.ToDouble(mSIC05b), cC2 = Convert.ToDouble(mSIC06b);
                        if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                        if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                        if (cA1 * cB1 * cC1 != 0) mSIC07 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);
                    }
                }

                if (mSIC02 > 20 || mSIC02 < -20) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、構造物斷面及通水斷面之面積增加不超過20%或減少不超過10%，且不影響原構造物正常功能，否則應辦理變更設計。";
                if (mSIC07 > 20 || mSIC02 < -10) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、構造物斷面及通水斷面之面積增加不超過20%或減少不超過10%，且不影響原構造物正常功能，否則應辦理變更設計。";
                //if (mSIC02 > 20 || mSIC02 < -20) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、尺寸差異百分比不得增加不得超過20%，減少不得超過10%，否則應辦理變更設計。";
                //if (mSIC07 > 20 || mSIC02 < -10) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、尺寸差異百分比不得增加不得超過20%，減少不得超過10%，否則應辦理變更設計。";

                if (mSIC01r != mSIC01b || /* mSIC01r2 != mSIC01c || */ mSIC04r != mSIC04b || mSIC04r2 != mSIC04c || mSIC05r != mSIC05b || mSIC05r2 != mSIC05c || mSIC06r != mSIC06b || mSIC06r2 != mSIC06c || mSIC09a != mSIC09b || v == "A" || tSDIFD004 == "其他")
                {
                    if (v == "A" || !chkNewItemOrNot(mSWC000,mDTLE001,mSDI001)) 
					{ 
						exeSqlStr = " insert into SwcItemChk (SWC000,DTLRPNO,SDI001,DTLTYPE,SIC01,SIC01_1,SIC01D,SIC02,SIC02_1,SIC03,SIC04,SIC04_1,SIC04D,SIC05,SIC05_1,SIC06,SIC06_1,SIC07,SIC07_1,SIC08,SIC09,SaveUser,SaveDate) values (@SWC000,@DTLRPNO,@SDI001,'L9',@SIC01,@SIC01_1,@SIC01D,@SIC02,@SIC02_1,@SIC03,@SIC04,@SIC04_1,@SIC04D,@SIC05,@SIC05_1,@SIC06,@SIC06_1,@SIC07,@SIC07_1,@SIC08,@SIC09,@SaveUser,getdate());"; 
					}		
					else 
					{ 
						exeSqlStr = " update SwcItemChk set SIC01=@SIC01,SIC01_1=@SIC01_1,SIC01D=@SIC01D,SIC02=@SIC02,SIC02_1=@SIC02_1,SIC03=@SIC03,SIC04=@SIC04,SIC04_1=@SIC04_1,SIC04D=@SIC04D,SIC05=@SIC05,SIC05_1=@SIC05_1,SIC06=@SIC06,SIC06_1=@SIC06_1,SIC07=@SIC07,SIC07_1=@SIC07_1,SIC08=@SIC08,SIC09=@SIC09,SaveUser=@SaveUser,SaveDate=getdate() where SWC000=@SWC000 and DTLRPNO=@DTLRPNO and SDI001=@SDI001; "; 
					}

                    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                    using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SWCConn.Open();

                        using (var cmd = SWCConn.CreateCommand())
                        {
                            cmd.CommandText = exeSqlStr;
                            //設定值
                            #region
                            cmd.Parameters.Add(new SqlParameter("@SWC000", mSWC000));
                            cmd.Parameters.Add(new SqlParameter("@DTLRPNO", mDTLE001));
                            cmd.Parameters.Add(new SqlParameter("@SDI001", mSDI001));
                            cmd.Parameters.Add(new SqlParameter("@SIC01", mSIC01b));
                            cmd.Parameters.Add(new SqlParameter("@SIC01_1", mSIC01c));
                            cmd.Parameters.Add(new SqlParameter("@SIC01D", mSIC01Da));
                            cmd.Parameters.Add(new SqlParameter("@SIC02", mSIC02));
                            cmd.Parameters.Add(new SqlParameter("@SIC02_1", mSIC02_1));
                            cmd.Parameters.Add(new SqlParameter("@SIC03", mSIC03));
                            cmd.Parameters.Add(new SqlParameter("@SIC04", mSIC04b));
                            cmd.Parameters.Add(new SqlParameter("@SIC04_1", mSIC04c));
                            cmd.Parameters.Add(new SqlParameter("@SIC04D", mSIC04Da));
                            cmd.Parameters.Add(new SqlParameter("@SIC05", mSIC05b));
                            cmd.Parameters.Add(new SqlParameter("@SIC05_1", mSIC05c));
                            cmd.Parameters.Add(new SqlParameter("@SIC06", mSIC06b));
                            cmd.Parameters.Add(new SqlParameter("@SIC06_1", mSIC06c));
                            cmd.Parameters.Add(new SqlParameter("@SIC07", mSIC07));
                            cmd.Parameters.Add(new SqlParameter("@SIC07_1", mSIC07_1));
                            cmd.Parameters.Add(new SqlParameter("@SIC08", mSIC08));
                            cmd.Parameters.Add(new SqlParameter("@SIC09", mSIC09b));
                            cmd.Parameters.Add(new SqlParameter("@SaveUser", ssUserID));
                            #endregion
                            cmd.ExecuteNonQuery();
                            cmd.Cancel();
                        }
                    }
                }
            }
        }
        if (nMSG01 != "") Response.Write("<script>alert('" + nMSG01 + "');</script>");
    }

    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        //string[] arryUpLoadField = new string[] { "TXTONA003", "TXTONA004", "TXTONA007", "TXTONA008", "TBONA014" };
        //TextBox[] arryUpLoadAppoj = new TextBox[] { TXTONA003, TXTONA004, TXTONA007, TXTONA008,TBONA014 };
        string[] arryUpLoadField = new string[] { "TXTONA003", "TXTONA004", "TXTONA007", "TXTONA008", "TBONA014", "TXTONA015" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTONA003, TXTONA004, TXTONA007, TXTONA008, TBONA014, TXTONA015 };
        string csUpLoadField = "TXTONA003";
        TextBox csUpLoadAppoj = TXTONA003;

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

    protected void DataLock_Click(object sender, EventArgs e)
    {
        Class1 C1 = new Class1();
        string sEXESQLSTR = "";

        #region 將竣工圖說帶至水保案
        string fromFilePath = "", toFilePath = "";

        #region Copy files
        if (fromFilePath.Trim() != "") {
        File.Move(fromFilePath, toFilePath); }
        #endregion
        #endregion

        GBClass001 GBC = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC005 = LBSWC005.Text;
        string sSWC025 = LBSWC025.Text;
        string sONA09001 = TXTONA001.Text + "";

        #region.
        bool allOver = true;
        int gLine = 0;
        DataTable dtSDI = new DataTable();
        dtSDI = (DataTable)ViewState["SwcDocItem"];

		string mSIC09b = "";
        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            if (++gLine % 2 == 0) {
                DropDownList DDLDONE = (DropDownList)SDIList.Rows[gLine - 1].Cells[3].FindControl("DDLDONE");
                mSIC09b = DDLDONE.SelectedItem.Text;
                if (mSIC09b != "完成" && mSIC09b != "已由永久設施取代") allOver = false;
            }
        }
        if (!allOver)
            { string nMSG01 = "請確認設施是否施作完成。"; Response.Write("<script>alert('" + nMSG01 + "');</script>"); return; }
        #endregion
        
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from OnlineApply09 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and ONA09001 = '" + sONA09001 + "' ";

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
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='OnlineApply009v.aspx?SWCNO=" + sSWC000 + "&OLANO=" + sONA09001 + "'; </script>");
                        return;
                    }
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            Session["SaveOk"] = "N";
            SaveCase_Click(sender, e);
            string ssSaveOk = Session["SaveOk"] + "";
            if (ssSaveOk == "Y") { } else { return; }

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply09 Set ";
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
            sEXESQLSTR = sEXESQLSTR + "   and ONA09001 = '" + sONA09001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //完工申報書的表單建檔日期帶入完工申報日期欄位
            string strSQL4 = " update SWCSWC set SWC58=getdate() where SWC00=@SWC000 and isnull(SWC58,'')<'1950-01-01';";
            string strSQL5 = " update TCGESWC.dbo.SWCCASE set SWC058=getdate() where SWC000=@SWC000 and isnull(SWC058,'')<'1950-01-01';";
            string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@ONA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
            ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = strSQL3+ strSQL4+ strSQL5;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sSWC005));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sSWC025));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sONA09001));
                    cmd.Parameters.Add(new SqlParameter("@R001", ""));
                    cmd.Parameters.Add(new SqlParameter("@R002", ""));
                    cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                    //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                    cmd.Parameters.Add(new SqlParameter("@R005", ""));
                    cmd.Parameters.Add(new SqlParameter("@R006", ""));
                    cmd.Parameters.Add(new SqlParameter("@R007", GBC.GetETUser(ssUserID, "OrgName")));
                    cmd.Parameters.Add(new SqlParameter("@R008", GBC.GetETUser(ssUserID, "ETIdentity")));
                    cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/HHmm")));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA09001, "申請中", ssUserID, "", "");
            SendMailNotice(sSWC000);

        }

        UpdateSwcDataNFiles();
        Response.Write("<script>alert('資料已送出。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
    }

    private void UpdateSwcDataNFiles()
    {
        GBClass001 SBApp = new GBClass001();
        bool rValue = false;
        string tmpSWC000 = LBSWC000.Text;
        string tmpSWC002 = LBSWC002.Text;
        string tmpSWC007 = LBSWC007.Text;
        string tmpSWC101 = TXTONA003.Text;
        string tmpSWC101CAD = TXTONA008.Text;
        string sqlStrT = " Update SWCSWC set SWC101=@SWC101,SWC101CAD=@SWC101CAD where SWC00=@SWC000; ";
        string sqlStrD = " Update tcgeswc.dbo.SWCCASE set SWC101=@SWC101,SWC101CAD=@SWC101CAD where SWC000=@SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStrT+ sqlStrD;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC101", tmpSWC101));
                cmd.Parameters.Add(new SqlParameter("@SWC101CAD", tmpSWC101CAD));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                string filePath01 = "";
                string filePath02 = "";
                bool folderExists;
                if (tmpSWC101.Trim() != "") {
                    filePath01 = ConfigurationManager.AppSettings["SwcFilePath20"] + tmpSWC000 + "\\" + tmpSWC101;
                    //filePath02 = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002 + "\\竣工圖說\\竣工圖說\\" + tmpSWC101;
                    filePath02 = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007);
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += tmpSWC002 + "\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += "\\竣工圖說\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += "竣工圖說\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += tmpSWC101;
                    CopyFiles(filePath01, filePath02);
                    //D:\公用區\唯讀區\107年掃描圖檔\水保申請案件\水保計畫\UA0210705001-2\竣工圖說\竣工圖說CAD
                }
                if (tmpSWC101CAD.Trim() != "")
                {
                    filePath01 = ConfigurationManager.AppSettings["SwcFilePath20"] + tmpSWC000 + "\\" + tmpSWC101CAD;
                    //filePath02 = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002 + "\\竣工圖說\\竣工圖說CAD\\" + tmpSWC101;
                    filePath02 = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007);
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += tmpSWC002 + "\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += "竣工圖說\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += "竣工圖說CAD\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += tmpSWC101CAD;
                    CopyFiles(filePath01, filePath02);
                }
            }
        }
    }

    private void CopyFiles(string filePath01, string filePath02)
    {
        Boolean fileExists;
        fileExists = File.Exists(filePath01);
        if (fileExists)
        {
            if (File.Exists(filePath02))
            {
                File.Delete(filePath02);
            }
            File.Copy(filePath01, filePath02);
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

        //送出提醒名單：承辦人、主管（科長，正工，股長，系統管理員）、檢查公會

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
                //寫死名單：章姿隆  ge-10706@mail.taipei.gov.tw

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
                        string ssMailSub01 = aUserName + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增完工申報";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增完工申報，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
					else if (aUserName.Trim() == tSWC024.Trim())
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail02 = new string[] { aUserMail };
                        string ssMailSub02 = aUserName + aJobTitle + "您好，水土保持計畫【" + tSWC002 + "】已新增完工申報";
                        string ssMailBody02 = "【" + tSWC005 + "】已新增完工申報，請上臺北市水土保持申請書件管理平台查看" + "<br><br>";
                        ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
						
                        bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);
                    }
                }
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    protected void SDIList_DataBound(object sender, EventArgs e)
    {
        int aaaaaa = 0;
        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            Label LBIT06 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("SDILB006");
            Label LBIT06D = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("SDILB006D");
            TextBox CHK01 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001");
			Label A1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A1");
            TextBox CHK01_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001_1");
            TextBox CHK01D = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001D");
            TextBox CHK04 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004");
			Label A2 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A2");
            TextBox CHK04_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004_1");
            TextBox CHK04D = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004D");
            TextBox CHK05 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK005");
			Label A3 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A3");
            TextBox CHK05_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK005_1");
            TextBox CHK06 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK006");
			Label A4 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A4");
            TextBox CHK06_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK006_1");
            Label LB019 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB019");

            if (++aaaaaa % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 2].Cells[2].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");
				
				Label A1_onoff = (Label)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("A1");
				Label A2_onoff = (Label)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("A2");
				Label A3_onoff = (Label)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("A3");
				Label A4_onoff = (Label)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("A4");
				TextBox CHK01_1_onoff = (TextBox)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("CHK001_1");
				TextBox CHK04_1_onoff = (TextBox)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("CHK004_1");
				TextBox CHK05_1_onoff = (TextBox)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("CHK005_1");
				TextBox CHK06_1_onoff = (TextBox)SDIList.Rows[aaaaaa-1].Cells[3].FindControl("CHK006_1");
				
                Label LBCHK002 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002");
				Label LBCHK002_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002_1");
                Label LBCHK002pers_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002pers_1");
                Label ITNONE03 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("ITNONE03");
                Label ITNONE04 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("ITNONE04");
                Label LB001D = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LB001D");
                Label LB004D = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LB004D");
                Label ITNONE05 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("ITNONE05");
                Label LBCHK007 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007");
                Label LBCHK007_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007_1");
                Label LBCHK007pers_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007pers_1");
                Label LabelX1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LabelX1");
                Label LabelX2 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LabelX2");
                DropDownList DDLDONE = (DropDownList)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("DDLDONE");
                TextBox RCH009 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("RCH009");
                TextBox TXTCHK002 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK002");
                TextBox TXTCHK002_1 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK002_1");
                TextBox TXTCHK007 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK007");
                TextBox TXTCHK007_1 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK007_1");
                Button MODIFYDATA = (Button)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("MODIFYDATA");
                LBIT06.Visible = false; LBIT06D.Visible = false;
                ITNONE03.Visible = false;
                ITNONE04.Visible = false;
                ITNONE05.Visible = false;
                DDLDONE.Visible = true;
                LB004D.Visible = false;
				
				//水土保持設施類別非臨時開頭移除"已由永久設施取代"
                if (SDIList.Rows[aaaaaa - 2].Cells[0].Text.Substring(0, 2) != "臨時")
                {
                    DDLDONE.Items.Remove("已由永久設施取代");
                }
				
				if (LB019.Text=="是"){
					switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; CHK06.Enabled = true; CHK06_1_onoff.Enabled = true; break; }
                }else{
					switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = false; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; CHK06.Enabled = true; CHK06_1_onoff.Enabled = false; break; }
				}
				
                if (LB019.Text == "是" && tSDIFD004 != "其他"){ A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; }
				else {A1_onoff.Visible = false; CHK01_1_onoff.Visible = false;A2_onoff.Visible = false; CHK04_1_onoff.Visible = false;A3_onoff.Visible = false; CHK05_1_onoff.Visible = false;A4_onoff.Visible = false; CHK06_1_onoff.Visible = false;}
				
				if (RCH009.Text == "完成" || RCH009.Text == "已由永久設施取代") { CHK01D.Enabled = false; CHK04D.Enabled = false; MODIFYDATA.Visible = true; CHK01.Enabled = false; CHK01_1_onoff.Enabled = false; CHK04.Enabled = false; CHK04_1_onoff.Enabled = false; CHK05.Enabled = false; CHK05_1_onoff.Enabled = false; CHK06.Enabled = false; CHK06_1_onoff.Enabled = false; DDLDONE.Enabled = false; TXTCHK002.Enabled = false; TXTCHK002_1.Enabled = false; TXTCHK007.Enabled = false; TXTCHK007_1.Enabled = false; } else { CHK01D.Enabled = true; CHK04D.Enabled = true; }

                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = true; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; CHK06.Visible = true; CHK06_1_onoff.Visible = true; break; }
					 /*LBCHK002_1.Visible = true; LBCHK002pers_1.Visible = true;*/
					LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false;
					LBCHK007_1.Visible=true; LBCHK007pers_1.Visible=true;
				}
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = false; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; CHK06.Visible = true; CHK06_1_onoff.Visible = false; break; }
					LBCHK002_1.Visible=false; LBCHK002pers_1.Visible=false; LBCHK007_1.Visible=false; LBCHK007pers_1.Visible=false;
				}
				
				if (tSDIFD004 == "其他") { CHK01.Visible = false; CHK01D.Visible = true; CHK04D.Visible = true; CHK04.Visible = false; CHK05.Visible = false; CHK06.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; CHK04D.Visible = true; LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = true; TXTCHK007_1.Visible = false; LBCHK002.Visible = false; LBCHK002_1.Visible = false; TXTCHK002.Visible = true; TXTCHK002_1.Visible = false; }
                else { CHK01.Visible = true; CHK01D.Visible = false; CHK04.Visible = true; CHK05.Visible = true; CHK06.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; CHK04D.Visible = false; LBCHK007.Visible = true; LBCHK007_1.Visible = true; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK002.Visible = true; LBCHK002_1.Visible = true; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; }

			}
            else
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 1].Cells[2].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");
                TextBox TXTCHK002 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK002");
                TextBox TXTCHK002_1 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK002_1");
                TextBox TXTCHK007 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK007");
                TextBox TXTCHK007_1 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("TXTCHK007_1");
                Label LBCHK002 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002");
                Label LBCHK002_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002_1");
                Label LBCHK002pers = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002pers");
                Label LBCHK002pers_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK002pers_1");
                Label ITNONE03 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("ITNONE03");
                Label ITNONE04 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("ITNONE04");
                Label LB004D = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LB004D");
                Label ITNONE05 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("ITNONE05");
                Label LBCHK007 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007");
                Label LBCHK007_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007_1");
                Label LBCHK007pers = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007pers");
                Label LBCHK007pers_1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LBCHK007pers_1");
                Label LabelX1 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LabelX1");
                Label LabelX2 = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LabelX2");

                CHK01.Visible = false; A1.Visible = false; CHK01_1.Visible = false; CHK01D.Visible = false; 
                CHK04.Visible = false; A2.Visible = false; CHK04_1.Visible = false; CHK04D.Visible = false;
                CHK05.Visible = false; A3.Visible = false; CHK05_1.Visible = false; 
                CHK06.Visible = false; A4.Visible = false; CHK06_1.Visible = false; 
				switch (HDF011.Value) { case "1": ITNONE04.Text = "-"; ITNONE05.Text = "-"; break; case "2": ITNONE05.Text = "-"; break; }
                LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK007pers.Visible = false; LBCHK007pers_1.Visible = false; LBCHK002.Visible = false; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; LBCHK002pers.Visible = false; LBCHK002pers_1.Visible = false;
                if (tSDIFD004 == "其他") { LBIT06.Visible = false; LBIT06D.Visible = true; LB004D.Visible = true; ITNONE03.Visible = false; ITNONE04.Visible = false; ITNONE05.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; }
                else { LBIT06.Visible = true; LBIT06D.Visible = false; LB004D.Visible = false; ITNONE03.Visible = true; ITNONE04.Visible = true; ITNONE05.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; }

                ITNONE03.Text = Server.HtmlDecode(ITNONE03.Text);
                ITNONE04.Text = Server.HtmlDecode(ITNONE04.Text);
                ITNONE05.Text = Server.HtmlDecode(ITNONE05.Text);
                LBIT06.Text = Server.HtmlDecode(LBIT06.Text);
			}
        }
    }
    protected void SDIList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "Modify":
                int aa = Convert.ToInt32(e.CommandArgument);

                HiddenField HDF011 = (HiddenField)SDIList.Rows[aa].Cells[0].FindControl("HDSDI011");
                TextBox CHK01 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001");
                TextBox CHK01_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001_1");
                TextBox CHK01D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001D");
                TextBox CHK04 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006_1");
                TextBox CHK02 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK002");
                TextBox CHK07 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK007");

                DropDownList DDLDONE = (DropDownList)SDIList.Rows[aa].Cells[3].FindControl("DDLDONE");

                CHK01.Enabled = true; /*CHK01_1.Enabled = true;*/ CHK01_1.Enabled = false; CHK02.Enabled = true; CHK07.Enabled = true;
                switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; CHK06.Enabled = true; CHK06_1.Enabled = true; break; }
                DDLDONE.Enabled = true;
                CHK01D.Enabled = true; CHK04D.Enabled = true;

                break;
        }

    }
    private void SetDtlData(string rSWCNO, string v2)
    {
        GBClass001 SBApp = new GBClass001();
        bool bb = true;

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //分段驗收核定項目
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
                bb = false;
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
                string sSDI017 = readerItem["SDI017"] + "";
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
                    SDITB.Columns.Add(new DataColumn("SDIFD006D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD009", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD010", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD011", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDILB019", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK009", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }
                if (sSDI017 != "完成" && sSDI017 != "已由永久設施取代") sSDI017 = "未完成";
				if (sSDI019 == "是" && sSDI005 != "其他")
				{
					//sSDI006 = sSDI006 + "~" + sSDI006_1;
                    sSDI006 = sSDI006;
					sSDI012 = sSDI012+"~"+sSDI012_1;
					sSDI013 = sSDI013+"~"+sSDI013_1;
					sSDI014 = sSDI014+"~"+sSDI014_1;
				}
				
                string sSIC002 = "-";
                string sSIC007 = "-";
				
                DataRow SDITBRow = tbSDIVS.NewRow();

                SDITBRow["SDIFDNI"] = ni.ToString();
                SDITBRow["SDIFD001"] = sSDI001;
                SDITBRow["SDIFD002"] = sSDI002;
                SDITBRow["SDIFD003"] = sSDI003;
                SDITBRow["SDIFD004"] = sSDI004;
                SDITBRow["SDIFD005"] = sSDI005;
                SDITBRow["SDIFD006"] = sSDI006;
                SDITBRow["SDIFD006D"] = sSDI006D;
                SDITBRow["SDIFD007"] = sSDI007;
                SDITBRow["SDIFD008"] = sSDI008;
                SDITBRow["SDIFD009"] = sSDI009;
                SDITBRow["SDIFD010"] = sSDI010;
                SDITBRow["SDIFD011"] = sSDI011;
                SDITBRow["SDIFD012"] = sSDI012;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDICHK001"] = "";
                SDITBRow["SDICHK002"] = "-";
                SDITBRow["SDICHK004"] = "";
                SDITBRow["SDICHK004_1"] = "";
                SDITBRow["SDICHK004D"] = sSDI012D;
                SDITBRow["SDICHK005"] = "";
                SDITBRow["SDICHK005_1"] = "";
                SDITBRow["SDICHK006"] = "";
                SDITBRow["SDICHK006_1"] = "";
                SDITBRow["SDICHK007"] = "-";
                SDITBRow["SDICHK008"] = "";// "-";
                SDITBRow["SDICHK009"] = sSDI017;

                tbSDIVS.Rows.Add(SDITBRow);

                //DB
                if (v2 == "AddNew")
                {
                    using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                    {
                        ItemConnS.Open();

                        string strSQLRVS = " select top 1 DTLE000 as MAXKEY  from SWCDTL05 D5 left join SwcItemChk Ck on D5.swc000=Ck.SWC000 and D5.DTLE000=Ck.DTLRPNO where D5.swc000='"+ rSWCNO + "' and D5.DATALOCK='Y' and isnull(Ck.SWC000,'') <>'' order by D5.savedate DESC ";
          
                        SqlDataReader readerItemS;
                        SqlCommand objCmdItemS = new SqlCommand(strSQLRVS, ItemConnS);
                        readerItemS = objCmdItemS.ExecuteReader();

                        while (readerItemS.Read()) { v2 = readerItemS["MAXKEY"] + ""; }
                    }

                }

                using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                {
                    ItemConnS.Open();

                    string tSIC01 = "";
                    string tSIC01_1 = "";
                    string tSIC01D = "";
                    string tSIC02 = "";
                    string tSIC02_1 = "";
                    string tSIC03 = "";
                    string tSIC04 = "";
                    string tSIC04_1 = "";
                    string tSIC04D = "";
                    string tSIC05 = "";
                    string tSIC05_1 = "";
                    string tSIC06 = "";
                    string tSIC06_1 = "";
                    string tSIC07 = "";
                    string tSIC07_1 = "";
                    string tSIC08 = "";
                    string tSIC09 = "未完成";
                    string tSIC10 = "";

                    string strSQLRVS = "select * from SwcItemChk";
                    strSQLRVS += " Where SWC000 = '" + rSWCNO + "' ";
                    strSQLRVS += "   and DTLRPNO = '" + v2 + "' ";
                    strSQLRVS += "   and SDI001 = '" + sSDI001 + "' ";

                    SqlDataReader readerItemS;
                    SqlCommand objCmdItemS = new SqlCommand(strSQLRVS, ItemConnS);
                    readerItemS = objCmdItemS.ExecuteReader();

                    while (readerItemS.Read())
                    {
                        tSIC01 = readerItemS["SIC01"] + "";
                        tSIC01_1 = readerItemS["SIC01_1"] + "";
                        tSIC01D = readerItemS["SIC01D"] + "";
                        tSIC02 = readerItemS["SIC02"] + "";
                        tSIC02_1 = readerItemS["SIC02_1"] + "";
                        tSIC03 = readerItemS["SIC03"] + "";
                        tSIC04 = readerItemS["SIC04"] + "";
                        tSIC04_1 = readerItemS["SIC04_1"] + "";
                        tSIC04D = readerItemS["SIC04D"] + "";
                        tSIC05 = readerItemS["SIC05"] + "";
                        tSIC05_1 = readerItemS["SIC05_1"] + "";
                        tSIC06 = readerItemS["SIC06"] + "";
                        tSIC06_1 = readerItemS["SIC06_1"] + "";
                        tSIC07 = readerItemS["SIC07"] + "";
                        tSIC07_1 = readerItemS["SIC07_1"] + "";
                        tSIC08 = readerItemS["SIC08"] + "";
                        tSIC09 = readerItemS["SIC09"] + "";
                        tSIC10 = readerItemS["SIC10"] + "";
                    }
                    DataRow SDITBRow2 = tbSDIVS.NewRow();

                    SDITBRow2["SDIFDNI"] = ni.ToString();
                    SDITBRow2["SDIFD001"] = sSDI001;
                    SDITBRow2["SDIFD002"] = sSDI002;
                    SDITBRow2["SDIFD003"] = "";
                    SDITBRow2["SDIFD004"] = "";
                    SDITBRow2["SDIFD005"] = "";
                    SDITBRow2["SDIFD006"] = sSDI006;
                    SDITBRow2["SDIFD006D"] = sSDI006D;
                    SDITBRow2["SDIFD007"] = sSDI007;
                    SDITBRow2["SDIFD008"] = sSDI008;
                    SDITBRow2["SDIFD009"] = sSDI009;
                    SDITBRow2["SDIFD010"] = sSDI010;
                    SDITBRow2["SDIFD011"] = sSDI011;
                    SDITBRow2["SDIFD012"] = sSDI012;
                    SDITBRow2["SDIFD013"] = sSDI013;
                    SDITBRow2["SDIFD014"] = sSDI014;
                    SDITBRow2["SDIFD015"] = sSDI015;
                    SDITBRow2["SDIFD016"] = sSDI016;
                    SDITBRow2["SDILB019"] = sSDI019;
                    SDITBRow2["SDICHK001"] = tSIC01;
                    SDITBRow2["SDICHK001_1"] = tSIC01_1;
                    SDITBRow2["SDICHK001D"] = tSIC01D;
                    SDITBRow2["SDICHK002"] = tSIC02;
                    SDITBRow2["SDICHK002_1"] = tSIC02_1;
                    SDITBRow2["SDICHK004"] = tSIC04;
                    SDITBRow2["SDICHK004_1"] = tSIC04_1;
                    SDITBRow2["SDICHK004D"] = tSIC04D;
                    SDITBRow2["SDICHK005"] = tSIC05;
                    SDITBRow2["SDICHK005_1"] = tSIC05_1;
                    SDITBRow2["SDICHK006"] = tSIC06;
                    SDITBRow2["SDICHK006_1"] = tSIC06_1;
                    SDITBRow2["SDICHK007"] = tSIC07;
                    SDITBRow2["SDICHK007_1"] = tSIC07_1;
                    SDITBRow2["SDICHK008"] = SBApp.DateView(tSIC08, "04");
                    SDITBRow2["SDICHK009"] = tSIC09.Trim();

                    tbSDIVS.Rows.Add(SDITBRow2);
                }

                ViewState["SwcDocItem"] = tbSDIVS;

                SDIList.DataSource = tbSDIVS;
                SDIList.DataBind();

                //TXTSDINI.Text = ni.ToString();
            }
            readerItem.Close();
        }
        GVMSG.Visible = bb;
    }


    protected void TBONA014_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("IMG", TBONA014_fileupload, TBONA014, "TBONA014", "_" + rONANO + "_ONA09_DOC14", null, Link014);

    }

    protected void TBONA014_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("IMG", TBONA014, null, Link014, "ONA09014", "TBONA014", 320, 180);
    }
	//判斷每個設施是否是新加的
	protected bool chkNewItemOrNot(string mSWC000, string mDTLE001, string mSDI001)
    {
		bool re = false;
        string sqlStr1 = "SELECT * FROM SwcItemChk where SWC000=@SWC000 and DTLRPNO=@DTLRPNO and SDI001=@SDI001 ;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();
            using (var cmd = Conn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
				cmd.Parameters.Add(new SqlParameter("@SWC000", mSWC000));
				cmd.Parameters.Add(new SqlParameter("@DTLRPNO", mDTLE001));
				cmd.Parameters.Add(new SqlParameter("@SDI001", mSDI001));
                cmd.ExecuteNonQuery();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        re = true;
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
		return re;
    }
}