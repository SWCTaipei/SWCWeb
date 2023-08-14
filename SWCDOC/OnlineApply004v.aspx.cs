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

public partial class SWCDOC_OnlineApply004 : System.Web.UI.Page
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
            C20.swcLogRC("OnlineApply004v", "開工/復工申請", "詳情", "瀏覽", rSWCNO + "," + rOLANO);
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
				string LINK = "OnlineApply004v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
                Response.Redirect(LINK);
            }
            else
                if (rOLANO == "" || ssUserName == "") { Response.Redirect("SWC001.aspx"); }
                else
                    GetOLA02Data(rSWCNO, rOLANO);
        }

        #region 全區供用
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        #endregion
    }
	protected void Page_LoadComplete(object sender, EventArgs e)
    {
        //判斷顯示開工或復工
        LBSWC004_1.Text = LBSWC004.Text;
        LBSWC004_2.Text = LBSWC004.Text;
        LBSWC004_3.Text = LBSWC004.Text;
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
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
                string tSWC041 = readeSwc["SWC041"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;
                LBSWC013.Text = tSWC013;
                //LBSWC013a.Text = tSWC013;
                LBSWC013ID.Text = tSWC013ID;
                LBSWC014.Text = tSWC014;
                LBSWC038.Text = SBApp.DateView(tSWC038,"00");
                LBSWC039.Text = tSWC039;
                LBSWC041.Text = tSWC041;
                LBSWC045Name.Text = SBApp.GetETUser(tSWC045ID,"Name");
                //LBSWC045a.Text = SBApp.GetETUser(tSWC045ID, "Name");
                LBSWC045OrgIssNo.Text = SBApp.GetETUser(tSWC045ID, "OrgIssNo");
                LBSWC045OrgName.Text = SBApp.GetETUser(tSWC045ID, "OrgName");
                LBSWC045OrgGUINo.Text = SBApp.GetETUser(tSWC045ID, "OrgGUINo");
                LBSWC045OrgAddr.Text = SBApp.GetETUser(tSWC045ID, "OrgAddr");
                

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
                string strSQLRV2 = " select * from OnlineApply04 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and ONA04001 = '" + v2 + "' ";

                SqlDataReader readeONA;
                SqlCommand objCmdONA = new SqlCommand(strSQLRV2, SwcConn);
                readeONA = objCmdONA.ExecuteReader();

                while (readeONA.Read())
                {
                    string tONA002 = readeONA["ONA04002"] + "";
                    string tONA003 = readeONA["ONA04003"] + "";
                    string tONA004 = readeONA["ONA04004"] + "";
                    string tONA005 = readeONA["ONA04005"] + "";
                    string tONA006 = readeONA["ONA04006"] + "";
                    string tONA007 = readeONA["ONA04007"] + "";
                    string tONA008 = readeONA["ONA04008"] + "";
                    string tONA009 = readeONA["ONA04009"] + "";
                    string tONA010 = readeONA["ONA04010"] + "";
                    string tONA011 = readeONA["ONA04011"] + "";
                    string tONA012 = readeONA["ONA04012"] + "";
                    string tONA013 = readeONA["ONA04013"] + "";
                    string tONA014 = readeONA["ONA04014"] + "";
                    string tONA015 = readeONA["ONA04015"] + "";
                    string tONA016 = readeONA["ONA04016"] + "";
                    string tONA017 = readeONA["ONA04017"] + "";
                    string tONA018 = readeONA["ONA04018"] + "";
                    string tONA019 = readeONA["ONA04019"] + "";
                    string tONA020 = readeONA["ONA04020"] + "";
                    string tONA021 = readeONA["ONA04021"] + "";
                    string tONA023 = readeONA["ONA04023"] + "";

                    if (tONA014 == "") { tONA014 = TXTONA014.Value; }

                    string tReviewResults = readeONA["ReviewResults"] + "";
                    string tResultsExplain = readeONA["ResultsExplain"] + "";
                    string tReviewDoc = readeONA["ReviewDoc"] + "";
                    string tLOCKUSER2 = readeONA["LOCKUSER2"] + "";
                    string tReviewDirections = readeONA["ReviewDirections"] + "";
                    string tReSendDeadline = readeONA["ReSendDeadline"] + "";

                    tDATALOCK = readeONA["DATALOCK"] + "";
                    DataLock2 = readeONA["DATALOCK2"] + "";
					
					LBONA018.Text = tONA018;
					LBONA019.Text = tONA019;
					LBONA020.Text = tONA020;
					LBONA021.Text = tONA021;

                    TXTONA001.Text = v2;
                    TXTONA002.Text = SBApp.DateView(tONA002, "00");
                    TXTONA003.Text = SBApp.DateView(tONA003, "00");
                    TXTONA004.Text = SBApp.DateView(tONA004, "00");
                    TXTONA005.Text = tONA005;
                    TXTONA006.Text = tONA006;
                    TXTONA007.Text = tONA007;
                    TXTONA008.Text = tONA008;
                    TXTONA009.Text = tONA009;
                    TXTONA010.Text = tONA010;
                    //TXTONA011.Text = SBApp.DateView(tONA011, "03"); 
                    TXTONA012.Text = SBApp.DateView(tONA012, "00");
                    TXTONA013.Text = tONA013;
                    TXTONA014.Value = tONA014;
                    LBSWC045Name.Text = SBApp.GetETUser(tONA014, "Name");
                    //LBSWC045a.Text = SBApp.GetETUser(tSWC045ID, "Name");
                    LBSWC045OrgIssNo.Text = SBApp.GetETUser(tONA014, "OrgIssNo");
                    LBSWC045OrgName.Text = SBApp.GetETUser(tONA014, "OrgName");
                    LBSWC045OrgGUINo.Text = SBApp.GetETUser(tONA014, "OrgGUINo");
                    LBSWC045OrgAddr.Text = SBApp.GetETUser(tONA014, "OrgAddr");
                    if (tONA015 == "1") { CHKONA015.Checked = true; }
                    if (SBApp.DateView(tONA016, "00") == "") { TXTONA016.Text = SBApp.DateView(tONA004, "00"); } else { TXTONA016.Text = SBApp.DateView(tONA016, "00"); }
                    if (SBApp.DateView(tONA017, "00") == "") { TXTONA017.Text = SBApp.DateView(tONA012, "00"); } else { TXTONA017.Text = SBApp.DateView(tONA017, "00"); }
					LBSWC004.Text = tONA023 == "" ? "開工/復工" : tONA023;
					
                    if (tReviewResults == "1") { CHKRRa.Checked = true; Button1.Visible = true; OutPdf.Visible = true; LBRR.Text = "審查結果：准予通過，核准完工期限 " + SBApp.DateView(tONA016, "00") + "&nbsp;&nbsp; ; &nbsp;&nbsp;目的事業核定完工期限 " + SBApp.DateView(tONA017, "00"); }
                    if (tReviewResults == "0") { CHKRRb.Checked = true; LBRR.Text = "審查結果：駁回"; }
                    if (tReviewResults == "2") { CHKRRc.Checked = true; LBRR.Text = "審查結果：退補正"; LBResultsExplain.Text = tReviewDirections + "<br>補正期限：" + SBApp.DateView(tReSendDeadline, "00"); }
                    if (tResultsExplain.Trim() != "") { LBResultsExplain.Text = "：" + tResultsExplain; }
                    if (tReviewResults == "1" && DataLock2.Trim() == "Y") { OutPdf.Visible = true; }

                    ResultsExplain.Text = tResultsExplain;
                    TXTReviewDoc.Text = tReviewDoc;
                    ReviewID.Text = SBApp.GetGeoUser(tLOCKUSER2, "Name");
                    ReviewDirections.Text = tReviewDirections;
                    TXTDeadline.Text = SBApp.DateView(tReSendDeadline, "00");


                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tONA005, tONA006, tONA007, tONA008, tONA009, tONA010, tONA013, tReviewDoc };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link005, Link006, Link007, Link008, Link009, Link010, Link013, LinkReviewDoc };

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
                            C1.FilesSortOut(strFileName,v,"");
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/"+ v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }
                }
            }
        }
        string ssUserType = Session["UserType"] + "";
        if (ssUserType == "03" && DataLock2 != "Y")
        {
            LBResultsExplain.Visible = false;
            ReviewResults.Visible = true;
            DataLock.Visible = true;
            SaveCase.Visible = true;
            ChkDateArea.Visible = true;

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
        {
            ReviewResults.Visible = false;
        }
		
		//為了不讓簽核內容及按鈕出現
		ResultsExplain.Visible = false;
		DataLock.Visible = false;
		SaveCase.Visible = false;
		
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA04" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA04" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA04001) AS MAXID from OnlineApply04 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA04001,9) = '" + tempVal + "' ";

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

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
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
        string csCaseID2 = TXTONA001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update OnlineApply04 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and ONA04001 = '" + csCaseID2 + "' ";

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

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sONA04001 = TXTONA001.Text + "";
        string sONA04016 = TXTONA016.Text + "";
        string sONA04017 = TXTONA017.Text + "";
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

            sEXESQLUPD = sEXESQLUPD + " Update OnlineApply04 Set ";
            sEXESQLUPD = sEXESQLUPD + " ONA04016 = '" + sONA04016 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA04017 = '" + sONA04017 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewResults = '" + sReviewResults + "', ";
            sEXESQLUPD = sEXESQLUPD + " ResultsExplain = '" + sResultsExplain + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDoc = '" + sReviewDoc + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReviewDirections = '" + sReviewDirections + "', ";
            sEXESQLUPD = sEXESQLUPD + " ReSendDeadline = '" + sReSendDeadline + "', ";

            sEXESQLUPD = sEXESQLUPD + " LOCKUSER2 = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " LOCKDATE = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " Where ONA04001 = '" + sONA04001 + "'";

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


    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        SaveCase_Click(sender, e);
        string ssUserID = Session["ID"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sONA04001 = TXTONA001.Text + "";
        string sReviewDirections = (ReviewDirections.Text + "").Length > 200 ? (ReviewDirections.Text + "").Substring(0, 200) : ReviewDirections.Text + "";
        string sReSendDeadline = TXTDeadline.Text + "";

        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply04 Set ";
            if (CHKRRc.Checked) { sEXESQLSTR += " DATALOCK='',LOCKUSER='',LOCKDATE=null, "; }
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK2 = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + "  LOCKUSER2 = '" + ssUserID + "' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA04001 = '" + sONA04001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string sReviewResults = CHKRRa.Checked ? "核准" : CHKRRb.Checked ? "駁回" : CHKRRc.Checked ? "退補正" : "";
            SBApp.RecordTrunHistory(sSWC000, sSWC002, sONA04001, sReviewResults, ssUserID, sReviewDirections, sReSendDeadline);

            switch (sReviewResults)
            {
                case "核准":
                    string sSWC045 = LBSWC045Name.Text;
                    string sONA003 = TXTONA003.Text + "";
                    string sONA014 = TXTONA014.Value + "";
                    string sONA016 = TXTONA016.Text + "";
                    string sONA017 = TXTONA017.Text + "";
                    string sSWC047 = SBApp.GetETUser(sONA014, "ETTel");

                    //2019-03-28 核准後，直接將【核准展延完工期限、目的事業核定展延完工期限】代入 水保申請案的【核定完工日期 SWC52、目的事業主管機關核定完工期限 SWC112】
                    string exeSQL = " Update SWCCASE Set SWC045=N'" + sSWC045 + "',SWC045ID=N'" + sONA014 + "',SWC047=N'" + sSWC047 + "',SWC051=N'" + sONA003 + "',SWC052=N'" + sONA016 + "',SWC112=N'" + sONA017 + "' Where SWC000 = '" + sSWC000 + "' ";
                    string exeSQL55 = " Update SWCSWC Set SWC45=N'" + sSWC045 + "',SWC045ID=N'" + sONA014 + "',SWC47=N'" + sSWC047 + "',SWC51=N'" + sONA003 + "',SWC52=N'" + sONA016 + "',SWC112=N'" + sONA017 + "' Where SWC00 = '" + sSWC000 + "' ";

                    using (SqlConnection SWCUpdConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        SWCUpdConn.Open();

                        SqlCommand objCmdUpdSwc = new SqlCommand(exeSQL, SWCUpdConn);
                        objCmdUpdSwc.ExecuteNonQuery();
                        objCmdUpdSwc.Dispose();
                    }

                    ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                    using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
                    {
                        TslmConn.Open();

                        SqlCommand objCmdTsl = new SqlCommand(exeSQL55, TslmConn);
                        objCmdTsl.ExecuteNonQuery();
                        objCmdTsl.Dispose();
                    }
                    break;
            }

            SendMailNotice(sSWC000, sReviewResults);
            
            Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
        }

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

        //送出提醒名單：義務人、監造技師、檢查公會

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
                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                string ssMailSub01 = "您好，" + "水土保持計畫【" + tSWC002 + "】申報開工已" + gReview;
                string ssMailBody01 = "您好，【" + tSWC005 + "】申報開工已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                string[] arraySentMail02 = new string[] { tETEmail, tSWC108 };
                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】申報開工已" + gReview;
                string ssMailBody02 = "您好，【" + tSWC005 + "】申報開工已" + gReview + "，詳情請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    protected void TXTReviewDoc_fileuploadok_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTReviewDoc_fileupload, TXTReviewDoc, "TXTReviewDoc", "_" + rONANO + "_04_ReviewDoc", null, LinkReviewDoc, 60);
    }

    protected void TXTReviewDoc_fileclean_Click(object sender, EventArgs e)
    {
        string rONANO = TXTONA001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTReviewDoc, null, LinkReviewDoc, "ReviewDoc", "TXTReviewDoc", 320, 180);
    }
    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        Response.Redirect("../SwcReport/PdfSwcOLA04.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO);
    }

    protected void OutPdf_Click1(object sender, ImageClickEventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        Response.Redirect("../SwcReport/A24P1.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        Response.Redirect("../SwcReport/A24P1.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO);
    }
}