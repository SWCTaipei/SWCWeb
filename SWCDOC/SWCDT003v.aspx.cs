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

public partial class SWCDOC_SWCDT003 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";

        string ssJobTitle = Session["JobTitle"] + "";
        GBClass001 SBApp = new GBClass001();

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId == "")
        {
            Response.Redirect("SWC000.aspx");
        }

        if (!IsPostBack)
        {
            Data2Page(rCaseId, rDTLId);

        }

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + "，您好";
        }

        //全區供用

        SBApp.ViewRecord("水土保持施工監督檢查紀錄", "view", "");

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
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC023 = readeSwc["SWC023"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";

                LBSWC000.Text = v;
                LBSWC005.Text = tSWC005;
                LBSWC005a.Text = tSWC005;
                LBSWC007.Text = tSWC007;
                LBSWC013ID.Text = tSWC013ID;
                LBSWC013.Text = tSWC013;
                LBSWC014.Text = tSWC014;
                LBSWC021.Text = tSWC045;
                LBSWC021Name.Text = SBApp.GetETUser(tSWC045ID, "OrgName");
                LBSWC021OrgIssNo.Text = SBApp.GetETUser(tSWC045ID, "OrgIssNo");
                LBSWC021OrgGUINo.Text = SBApp.GetETUser(tSWC045ID, "OrgGUINo");
                LBSWC021OrgTel.Text = SBApp.GetETUser(tSWC045ID, "OrgTel");
                LBSWC023.Text = tSWC023;
                LBSWC038.Text = SBApp.DateView(tSWC038, "00");
                LBSWC039.Text = tSWC039;
                LBSWC043.Text = SBApp.DateView(tSWC043, "00");
                LBSWC044.Text = tSWC044;
                LBSWC051.Text = SBApp.DateView(tSWC051, "00");
                LBSWC052.Text = SBApp.DateView(tSWC052, "00");
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
                string strSQLRV2 = " select * from SWCDTL03 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + " and DTLC001 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLC001 = readeDTL["DTLC001"] + "";
                    string tDTLC002 = readeDTL["DTLC002"] + "";
                    string tDTLC003 = readeDTL["DTLC003"] + "";
                    string tDTLC004 = readeDTL["DTLC004"] + "";
                    string tDTLC005 = readeDTL["DTLC005"] + "";
                    string tDTLC006 = readeDTL["DTLC006"] + "";
                    string tDTLC007 = readeDTL["DTLC007"] + "";
                    string tDTLC008 = readeDTL["DTLC008"] + "";
                    string tDTLC009 = readeDTL["DTLC009"] + "";
                    string tDTLC010 = readeDTL["DTLC010"] + "";
                    string tDTLC011 = readeDTL["DTLC011"] + "";
                    string tDTLC012 = readeDTL["DTLC012"] + "";
                    string tDTLC013 = readeDTL["DTLC013"] + "";
                    string tDTLC014 = readeDTL["DTLC014"] + "";
                    string tDTLC015 = readeDTL["DTLC015"] + "";
                    string tDTLC016 = readeDTL["DTLC016"] + "";
                    string tDTLC017 = readeDTL["DTLC017"] + "";
                    string tDTLC018 = readeDTL["DTLC018"] + "";
                    string tDTLC019 = readeDTL["DTLC019"] + "";
                    string tDTLC020 = readeDTL["DTLC020"] + "";
                    string tDTLC021 = readeDTL["DTLC021"] + "";
                    string tDTLC022 = readeDTL["DTLC022"] + "";
                    string tDTLC023 = readeDTL["DTLC023"] + "";
                    string tDTLC024 = readeDTL["DTLC024"] + "";
                    string tDTLC025 = readeDTL["DTLC025"] + "";
                    string tDTLC026 = readeDTL["DTLC026"] + "";
                    string tDTLC027 = readeDTL["DTLC027"] + "";
                    string tDTLC028 = readeDTL["DTLC028"] + "";
                    string tDTLC029 = readeDTL["DTLC029"] + "";
                    string tDTLC030 = readeDTL["DTLC030"] + "";
                    string tDTLC031 = readeDTL["DTLC031"] + "";
                    string tDTLC032 = readeDTL["DTLC032"] + "";
                    string tDTLC033 = readeDTL["DTLC033"] + "";
                    string tDTLC034 = readeDTL["DTLC034"] + "";
                    string tDTLC035 = readeDTL["DTLC035"] + "";
                    string tDTLC036 = readeDTL["DTLC036"] + "";
                    string tDTLC037 = readeDTL["DTLC037"] + "";
                    string tDTLC038 = readeDTL["DTLC038"] + "";
                    string tDTLC039 = readeDTL["DTLC039"] + "";
                    string tDTLC040 = readeDTL["DTLC040"] + "";
                    string tDTLC041 = readeDTL["DTLC041"] + "";
                    string tDTLC042 = readeDTL["DTLC042"] + "";
                    string tDTLC043 = readeDTL["DTLC043"] + "";
                    string tDTLC044 = readeDTL["DTLC044"] + "";
                    string tDTLC045 = readeDTL["DTLC045"] + "";
                    string tDTLC046 = readeDTL["DTLC046"] + "";
                    string tDTLC047 = readeDTL["DTLC047"] + "";
                    string tDTLC048 = readeDTL["DTLC048"] + "";
                    string tDTLC049 = readeDTL["DTLC049"] + "";
                    string tDTLC050 = readeDTL["DTLC050"] + "";
                    string tDTLC051 = readeDTL["DTLC051"] + "";
                    string tDTLC052 = readeDTL["DTLC052"] + "";
                    string tDTLC053 = readeDTL["DTLC053"] + "";
                    string tDTLC054 = readeDTL["DTLC054"] + "";
                    string tDTLC055 = readeDTL["DTLC055"] + "";
                    string tDTLC056 = readeDTL["DTLC056"] + "";
                    string tDTLC057 = readeDTL["DTLC057"] + "";
                    string tDTLC058 = readeDTL["DTLC058"] + "";
                    string tDTLC059 = readeDTL["DTLC059"] + "";
                    string tDTLC060 = readeDTL["DTLC060"] + "";
                    string tDTLC061 = readeDTL["DTLC061"] + "";
                    string tDTLC062 = readeDTL["DTLC062"] + "";
                    string tDTLC063 = readeDTL["DTLC063"] + "";
                    string tDTLC064 = readeDTL["DTLC064"] + "";
                    string tDTLC065 = readeDTL["DTLC065"] + "";
                    string tDTLC066 = readeDTL["DTLC066"] + "";
                    string tDTLC067 = readeDTL["DTLC067"] + "";
                    string tDTLC068 = readeDTL["DTLC068"] + "";
                    string tDTLC070 = readeDTL["DTLC070"] + "";

                    string tDateLock = readeDTL["DATALOCK"] + "";
                    string tSaveDate = readeDTL["savedate"] + "";

                    LBDTL001.Text = tDTLC001;
                    TXTDTL002.Text = SBApp.DateView(tDTLC002, "00");
                    TXTDTL003.Text = tDTLC003;
                    TXTDTL004.Text = tDTLC004;
                    TXTDTL005.Text = tDTLC005;
                    DDLDTL006.Text = tDTLC006;
                    TXTDTL007.Text = tDTLC007;
                    DDLDTL008.Text = tDTLC008;
                    TXTDTL009.Text = tDTLC009;
                    DDLDTL010.Text = tDTLC010;
                    TXTDTL011.Text = tDTLC011;
                    DDLDTL012.Text = tDTLC012;
                    TXTDTL013.Text = tDTLC013;
                    DDLDTL014.Text = tDTLC014;
                    TXTDTL015.Text = tDTLC015;
                    DDLDTL016.Text = tDTLC016;
                    TXTDTL017.Text = tDTLC017;
                    DDLDTL018.Text = tDTLC018;
                    TXTDTL019.Text = tDTLC019;
                    DDLDTL020.Text = tDTLC020;
                    TXTDTL021.Text = tDTLC021;
                    DDLDTL022.Text = tDTLC022;
                    TXTDTL023.Text = tDTLC023;
                    DDLDTL024.Text = tDTLC024;
                    TXTDTL025.Text = tDTLC025;
                    DDLDTL026.Text = tDTLC026;
                    TXTDTL027.Text = tDTLC027;
                    DDLDTL028.Text = tDTLC028;
                    TXTDTL029.Text = tDTLC029;
                    DDLDTL030.Text = tDTLC030;
                    TXTDTL031.Text = tDTLC031;
                    DDLDTL032.Text = tDTLC032;
                    TXTDTL033.Text = tDTLC033;
                    DDLDTL034.Text = tDTLC034;
                    TXTDTL035.Text = tDTLC035;
                    DDLDTL036.Text = tDTLC036;
                    TXTDTL037.Text = tDTLC037;
                    DDLDTL038.Text = tDTLC038;
                    TXTDTL039.Text = tDTLC039;
                    DDLDTL040.Text = tDTLC040;
                    TXTDTL041.Text = tDTLC041;
                    DDLDTL042.Text = tDTLC042;
                    TXTDTL043.Text = tDTLC043;
                    DDLDTL044.Text = tDTLC044;
                    TXTDTL045.Text = tDTLC045;
                    DDLDTL046.Text = tDTLC046;
                    TXTDTL047.Text = tDTLC047;
                    TXTDTL048.Text = tDTLC048;
                    TXTDTL049.Text = SBApp.DateView(tDTLC049, "00");
                    TXTDTL050.Text = tDTLC050;
                    TXTDTL051.Text = tDTLC051;
                    TXTDTL052.Text = tDTLC052;
                    DDLDTL053.Text = tDTLC053;
                    TXTDTL054.Text = tDTLC054;
                    TXTDTL055.Text = tDTLC055.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");
                    TXTDTL056.Text = tDTLC056;
                    TXTDTL057.Text = tDTLC057;
                    TXTDTL058.Text = tDTLC058;
                    TXTDTL059.Text = tDTLC059;
                    TXTDTL060.Text = tDTLC060;
                    TXTDTL061.Text = tDTLC061;
                    TXTDTL062.Text = tDTLC062;
                    TXTDTL063.Text = tDTLC063;
                    TXTDTL064.Text = tDTLC064;
                    TXTDTL065.Text = tDTLC065;
                    TXTDTL066.Text = tDTLC066;
                    TXTDTL067.Text = tDTLC067;
                    TXTDTL068.Text = tDTLC068;

                    if (tDateLock=="Y")
                    {
                        TXTSENDDATE.Text = SBApp.DateView(tSaveDate, "00");

                        DateTime tDate1 = Convert.ToDateTime(SBApp.DateView(tDTLC002, "00"));
                        DateTime tDate2 = Convert.ToDateTime(SBApp.DateView(tSaveDate, "00"));

                        TimeSpan ts = tDate2 - tDate1;
                        double days = ts.TotalDays;

                        if (days==0) { TXTSENDDAY.Text = "當日送達"; } else { TXTSENDDAY.Text = days.ToString()+"天"; }
                        
                    }

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLC070 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link070 };

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
                    string[] arrayFileName2 = new string[] { tDTLC056, tDTLC057, tDTLC059, tDTLC061, tDTLC063, tDTLC065, tDTLC067 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj2 = new System.Web.UI.WebControls.HyperLink[] { HyperLink056, HyperLink057, HyperLink059, HyperLink061, HyperLink063, HyperLink065, HyperLink067 };

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
                    //string[] arrayFileName = new string[] { tDTLC057, tDTLC059, tDTLC061, tDTLC063, tDTLC065, tDTLC067 };
                    //System.Web.UI.WebControls.Image[] arrayImgAppobj = new System.Web.UI.WebControls.Image[] { TXTDTL057_img, TXTDTL059_img, TXTDTL061_img, TXTDTL063_img, TXTDTL065_img, TXTDTL067_img };

                    //for (int i = 0; i < arrayFileName.Length; i++)
                    //{
                    //    string strFileName = arrayFileName[i];
                    //    System.Web.UI.WebControls.Image ImgFileObj = arrayImgAppobj[i];

                    //    if (strFileName == "")
                    //    {
                    //    }
                    //    else
                    //    {
                    //        string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                    //        ImgFileObj.Attributes.Add("src", tempImgPateh + "?ts=" + DateTime.Now.Millisecond);
                    //    }
                    //}

                }
            }

        }
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL056", "TXTDTL057", "TXTDTL059", "TXTDTL061", "TXTDTL063", "TXTDTL065", "TXTDTL067" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL056, TXTDTL057, TXTDTL059, TXTDTL061, TXTDTL063, TXTDTL065, TXTDTL067 };
        string csUpLoadField = "TXTDTL056";
        TextBox csUpLoadAppoj = TXTDTL056;

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
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RC" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RC" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLC000) AS MAXID from SWCDTL03 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLC000,7) = '" + tempVal + "' ";

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
                    List<string> allowedExtextsion02 = new List<string> { ".jpg", ".png", "doc", "pdf", "dwg", "dxf" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG DOC PDF DWG DXF 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 5000000)
            {
                error_msg.Text = "請選擇 5Mb 以下檔案上傳，謝謝!!";
                return;
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
    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        Response.Redirect("../SwcReport/PdfSwcDtl03.aspx?SWCNO=" + rCaseId + "&DTLNO=" + rDTLId);
    }
}