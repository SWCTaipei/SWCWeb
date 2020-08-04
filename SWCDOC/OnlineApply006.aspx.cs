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

        string rRRPG = Request.QueryString["RRPG"] + "";
        string rReceiveID = SBApp.Decrypt(Request.QueryString["ID"] + "");
        string rReceivePW = SBApp.Decrypt(Request.QueryString["PD"] + "");

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";
        
        if (!IsPostBack)
        {
            if (rRRPG == "55")
            {
                Boolean LoginR = false;
                LoginR = SBApp.GetLoginStatus(rReceiveID, rReceivePW, "03");

                if (LoginR)
                {
                    string ssUserName2 = Session["NAME"] + "";
                    LoadApp.LoadSwcCase("03", ssUserName);
                }
                string LINK = "OnlineApply006v.aspx?SWCNO=" + rSWCNO + "&OLANO=" + rOLANO;
                Response.Redirect(LINK);

            }
            else
            {
                if (rOLANO == "") { Response.Redirect("SWC001.aspx"); }
                else
                {
                    GetOLA02Data(rSWCNO, rOLANO);
                }
            }

        }
        


        //全區供用
        SBApp.ViewRecord("水土保持計畫義務人及技師變更報備", "update", "");

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

                LBSWC000.Text = tSWC000;
                LBSWC002.Text = tSWC002;
                LBSWC005.Text = tSWC005;

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

                    

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tONA006, tONA010, tONA014 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link006, Link010, Link014 };

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
                }
            }
        }
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

        strSQLClearFieldValue = " update OnlineApply06 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and ONA06001 = '" + csCaseID2 + "' ";

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

        string[] arryUpLoadField = new string[] { "TXTONA006", "TXTONA010", "TXTONA014" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTONA006, TXTONA010, TXTONA014 };
        string csUpLoadField = "TXTONA006";
        TextBox csUpLoadAppoj = TXTONA006;

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
    protected void SaveCase_Click(object sender, EventArgs e)
    {
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
            sEXESQLUPD = sEXESQLUPD + " ONA06003 = '" + sONA06003 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06004 = '" + sONA06004 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06005 = '" + sONA06005 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06006 = '" + sONA06006 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06007 = '" + sONA06007 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06008 = '" + sONA06008 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06009 = '" + sONA06009 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06010 = '" + sONA06010 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06011 = '" + sONA06011 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06012 = '" + sONA06012 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06013 = '" + sONA06013 + "', ";
            sEXESQLUPD = sEXESQLUPD + " ONA06014 = '" + sONA06014 + "', ";

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
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string sSWC000 = LBSWC000.Text;
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

            sEXESQLSTR = sEXESQLSTR + " Update OnlineApply06 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKUSER = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " LOCKDATE = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and ONA06001 = '" + sONA06001 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

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

        //送出提醒名單：承辦人、主管（科長，正工，股長，系統管理員）

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
                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() )
                    {
                        //SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = aUserName + aJobTitle + "您好，" + tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增義務人/技師變更報備申請";
                        string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增義務人/技師變更報備申請，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                }
                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增暫停審查申請";
                //string ssMailBody01 = tSWC012 + "轄區【" + tSWC005 + "】已新增暫停審查申請，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
}