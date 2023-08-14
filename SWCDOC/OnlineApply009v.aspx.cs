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
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        LoadSwcClass01 LoadApp = new LoadSwcClass01();
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        if (!IsPostBack)
        {
            SBApp.ViewRecord("水土保持計畫暫停審查", "update", "");
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
                string LINK = "OnlineApply009v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
                Response.Redirect(LINK);
            }
            else{
				if (rOLANO == "" || ssUserName == "") { Response.Redirect("SWC001.aspx"); }
                else
                    GetOLA02Data(rSWCNO, rOLANO);
			}
                
        }
        #region 全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        #endregion
    }

    private void GetOLA02Data(string v, string v2)
    {
        string ssONLINEAPPLY = Session["ONLINEAPPLY"] + "";

        GBClass001 SBApp = new GBClass001();

        string tDATALOCK = "";
        string DataLock2 = "";

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
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
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

                    string tReviewResults = readeONA["ReviewResults"] + "";
                    string tResultsExplain = readeONA["ResultsExplain"] + "";
                    string tReviewDoc = readeONA["ReviewDoc"] + "";
                    string tLOCKUSER2 = readeONA["LOCKUSER2"] + "";
                    string tReviewDirections = readeONA["ReviewDirections"] + "";
                    string tReSendDeadline = readeONA["ReSendDeadline"] + "";

                    tDATALOCK = readeONA["DATALOCK"] + "";
                    DataLock2 = readeONA["DATALOCK2"] + "";

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = tONA003;
                    TXTONA004.Text = tONA004;
                    //TXTONA005.Text = SBApp.DateView(tONA005, "03");
                    TXTONA007.Text = tONA007;
                    LBONA010.Text = tONA010;
                    LBONA011.Text = tONA011;
                    LBONA012.Text = tONA012;
                    LBONA013.Text = tONA013;
                    LBONA014.Text = tONA014;
                    if (tONA009 == "1") { CHKONA009.Checked = true; }
                    TXTONA015.Text = tONA015;
                    if (tONA016 == "1") { CHKONA016.Checked = true; }

                    if (tReviewResults == "1") { CHKRRa.Checked = true; LBRR.Text = "審查結果：准予通過"; LBResultsExplain.Text = tResultsExplain; }
                    if (tReviewResults == "0") { CHKRRb.Checked = true; LBRR.Text = "審查結果：駁回"; LBResultsExplain.Text = tResultsExplain; }
                    if (tReviewResults == "2") { CHKRRc.Checked = true; LBRR.Text = "審查結果：退補正"; LBResultsExplain.Text = tReviewDirections + "<br>補正期限：" + SBApp.DateView(tReSendDeadline, "00"); }

                    ResultsExplain.Text = tResultsExplain;
                    TXTReviewDoc.Text = tReviewDoc;
                    ReviewDirections.Text = tReviewDirections;
                    TXTDeadline.Text = SBApp.DateView(tReSendDeadline, "00");
                    ReviewID.Text = SBApp.GetGeoUser(tLOCKUSER2, "Name");

                    //檔案類處理
                    //string[] arrayFileNameLink = new string[] { tONA003, tONA004, tONA007, tONA008, tONA014, tReviewDoc };
                    //System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link003, Link004, Link007, Link008, Link014, LinkReviewDoc };
                    string[] arrayFileNameLink = new string[] { tONA003, tONA015, tONA004, tONA007, tONA008, tONA014, tReviewDoc };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link003, Link015, Link004, Link007, Link008, Link014, LinkReviewDoc };

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
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                }
            }
        }

        SetDtlData(v, v2);

        string ssUserType = Session["UserType"] + "";
        if (ssUserType == "03" && DataLock2 != "Y")
        {
            LBResultsExplain.Visible = false;
            ReviewResults.Visible = true;
            DataLock.Visible = true;
            SaveCase.Visible = true;
        }
        if (DataLock2 == "Y")
        {
            ReViewUL.Visible = false;
            ResultsExplain.Visible = false;
            ReviewResults.Visible = true;
            LBRR.Visible = true;
            CHKRRa.Visible = false;
            CHKRRb.Visible = false;
            DataLock.Visible = false;
            SaveCase.Visible = false;
            Panel1.Visible = false;
        }
        if (ssONLINEAPPLY != "Y" && DataLock2 != "Y")
            ReviewResults.Visible = false;
		
		//為了不讓簽核內容及按鈕出現
		ResultsExplain.Visible = false;
		DataLock.Visible = false;
		SaveCase.Visible = false;
		
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
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
    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize)
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
                    List<string> allowedExtextsion02 = new List<string> { ".pdf", ".PDF", ".odt", ".ODT", ".doc", ".DOC", ".docx", ".DOCX" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF、ODT、DOC 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
            }

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > _FileMaxSize * 1000000)
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
    
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA09001 = TXTONA001.Text + "";
        string sReviewDoc = TXTReviewDoc.Text + "";
        string sResultsExplain = ResultsExplain.Text + "";
        string sReviewResults = CHKRRa.Checked ? "1" : CHKRRb.Checked ? "0" : CHKRRc.Checked ? "2" : "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text;

        string sEXESQLUPD = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply09 Set ";
            sEXESQLUPD = sEXESQLUPD + " ReviewResults = '" + sReviewResults + "', ";
            sEXESQLUPD = sEXESQLUPD + " ResultsExplain = '" + sResultsExplain + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDoc = '" + sReviewDoc + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDirections = '" + sReviewDirections + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReSendDeadline = '" + sReSendDeadline + "', ";

            sEXESQLUPD = sEXESQLUPD + " LOCKUSER2 = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " LOCKDATE = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA09001 = '" + sONA09001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        }
    }

    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTReviewDoc" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTReviewDoc };
        string csUpLoadField = "TXTReviewDoc";
        TextBox csUpLoadAppoj = TXTReviewDoc;

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
        GBClass001 GBC = new GBClass001();
        SaveCase_Click(sender, e);
        string ssUserID = Session["ID"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sONA09001 = TXTONA001.Text + "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply09 Set ";
            if (CHKRRc.Checked) { sEXESQLSTR += " DATALOCK='',LOCKUSER='',LOCKDATE=null, "; }
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '" + ssUserID + "' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA09001 = '" + sONA09001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string sReviewResults = CHKRRa.Checked ? "核准" : CHKRRb.Checked ? "駁回" : CHKRRc.Checked ? "退補正" : "";
            GBC.RecordTrunHistory(sSWC000, sSWC002, sONA09001, sReviewResults, ssUserID, sReviewDirections, sReSendDeadline);
            SendMailNotice(sSWC000, sReviewResults);

            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }
    }
    protected void TXTReviewDoc_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTReviewDoc_fileupload, TXTReviewDoc, "TXTReviewDoc", "_" + rONANO + "_09_ReviewDoc", null, LinkReviewDoc, 60);
    }

    protected void TXTReviewDoc_fileclean_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTReviewDoc, null, LinkReviewDoc, "ReviewDoc", "TXTReviewDoc", 320, 180);
    }
    private void SendMailNotice(string gSWC000, string gReview)
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

        //送出提醒名單：義務人、監造技師、檢查公會 義務人、承辦技師、審查公會

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC024, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
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
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄件名單
                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aUserName.Trim() == tSWC024.Trim())
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;
                    }
                }

                //一人一封不打結…
                //檢查公會
                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                string ssMailSub01 = tSWC024 + "您好，" + "水土保持計畫【" + tSWC002 + "】申請完工已" + gReview;
                string ssMailBody01 = "您好，【" + tSWC005 + "】申請完工已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                //監造技師
                string[] arraySentMail02 = new string[] { tETEmail };
                string ssMailSub02 = tETName + "技師您好，" + "水土保持計畫【" + tSWC002 + "】申請完工已" + gReview;
                string ssMailBody02 = "您好，【" + tSWC005 + "】申請完工已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);

                //義務人
                string[] arraySentMail03 = new string[] { tSWC108 };
                string ssMailSub03 = tSWC013 + "您好，" + "水土保持計畫【" + tSWC002 + "】申請完工已" + gReview;
                string ssMailBody03 = "您好，【" + tSWC005 + "】申請完工已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody03 = ssMailBody03 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody03 = ssMailBody03 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo03 = SBApp.Mail_Send(arraySentMail03, ssMailSub03, ssMailBody03);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
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
                    SDITB.Columns.Add(new DataColumn("SDIFD012_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD019", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK009", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }
                if (sSDI017 != "完成") sSDI017 = "未完成";

                string sSIC002 = "-";
                string sSIC007 = "-";

                if (sSDI006D == "")
                    if (sSDI019 == "是")
						//sSDI006D = sSDI006 + "~" + sSDI006_1 + sSDI007;
						sSDI006D = sSDI006 + sSDI007;
                    else
                        sSDI006D = sSDI006 + sSDI007;
                else
                    //if (sSDI019 == "是") { var a = sSDI006D.Split('~'); sSDI006D = a[0] + "~" + a[1]; }
                    sSDI006D = sSDI006D;
				//if (sSDI012D.Trim() == "")
                if (sSDI003 == "邊坡穩定措施") sSDI012D = sSDI012D;
				else{
                    if(sSDI019=="是")
					    switch (sSDI011) { case "1": sSDI012D = sSDI012 + "~" + sSDI012_1 + sSDI015; break; case "2": sSDI012D = sSDI012 + "~" + sSDI012_1 + "×" + sSDI013 + "~" + sSDI013_1 + sSDI015; break; case "3": sSDI012D = sSDI012 + "~" + sSDI012_1 + "×" + sSDI013 + "~" + sSDI013_1 + "×" + sSDI014 + "~" + sSDI014_1  + sSDI015; break; }
				    else
                        switch (sSDI011) { case "1": sSDI012D = sSDI012 + sSDI015; break; case "2": sSDI012D = sSDI012 + "×" + sSDI013 + sSDI015; break; case "3": sSDI012D = sSDI012 + "×" + sSDI013 + "×" + sSDI014 + sSDI015; break; }
                }

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
                SDITBRow["SDIFD012D"] = sSDI012D;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDIFD019"] = sSDI019;
                SDITBRow["SDICHK001"] = "";
                SDITBRow["SDICHK001D"] = sSDI006D;
                SDITBRow["SDICHK002"] = "";// "-";
                SDITBRow["SDICHK004"] = "";
                SDITBRow["SDICHK004D"] = sSDI012D;
                SDITBRow["SDICHK005"] = "";
                SDITBRow["SDICHK006"] = "";
                SDITBRow["SDICHK007"] = "";// "-";
                SDITBRow["SDICHK008"] = "";// "-";
                SDITBRow["SDICHK009"] = "";

                tbSDIVS.Rows.Add(SDITBRow);

                //DB
                if (v2 == "AddNew")
                {
                    using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                    {
                        ItemConnS.Open();

                        string strSQLRVS = "select MAX(DTLE001) as MAXKEY from SWCDTL05";
                        strSQLRVS += " Where SWC000 = '" + rSWCNO + "' ";
                        strSQLRVS += "   AND DATALOCK='Y' ";

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


                        if (tSIC01D.Trim() == "")
                            if (sSDI019=="是")
                                //tSIC01D = tSIC01 + "~" + tSIC01_1 + sSDI007;
								tSIC01D = tSIC01 + sSDI007;
                            else
                                tSIC01D = tSIC01 + sSDI007;
                        if (tSIC04D.Trim() == "") {
                            if (sSDI019 == "是")
                                switch (tSIC03) { case "1": tSIC04D = tSIC04 + "~" + tSIC04_1 + sSDI015; break; case "2": tSIC04D = tSIC04 + "~" + tSIC04_1 + "×" + tSIC05 + "~" + tSIC05_1 + sSDI015; break; case "3": tSIC04D = tSIC04 + "~" + tSIC04_1 + "×" + tSIC05 + "~" + tSIC05_1 + "×" + tSIC06 + "~" + tSIC06_1  + sSDI015; break; }
                            else
                                switch (tSIC03) { case "1": tSIC04D = tSIC04 + sSDI015; break; case "2": tSIC04D = tSIC04 + "×" + tSIC05 + sSDI015; break; case "3": tSIC04D = tSIC04 + "×" + tSIC05 + "×" + tSIC06 + sSDI015; break; }
                        }

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
                    SDITBRow2["SDIFD012D"] = sSDI012D;
                    SDITBRow2["SDIFD013"] = sSDI013;
                    SDITBRow2["SDIFD014"] = sSDI014;
                    SDITBRow2["SDIFD015"] = sSDI015;
                    SDITBRow2["SDIFD016"] = sSDI016;
                    SDITBRow2["SDIFD019"] = sSDI019;
                    SDITBRow2["SDICHK001"] = tSIC01;
                    SDITBRow2["SDICHK001D"] = tSIC01D;
                    if (sSDI019 == "是")
                        SDITBRow2["SDICHK002"] = tSIC02 + "％"; //+ tSIC02_1 + "％";
                    else
                        SDITBRow2["SDICHK002"] = tSIC02 + "％";
                    SDITBRow2["SDICHK004"] = tSIC04;
                    SDITBRow2["SDICHK004D"] = tSIC04D;
                    SDITBRow2["SDICHK005"] = tSIC05;
                    SDITBRow2["SDICHK006"] = tSIC06;
                    if (sSDI019 == "是")
                        SDITBRow2["SDICHK007"] = tSIC07 + "％" + tSIC07_1 + "％";
                    else
                        SDITBRow2["SDICHK007"] = tSIC07 + "％";
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
}