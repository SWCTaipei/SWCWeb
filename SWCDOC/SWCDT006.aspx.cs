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

public partial class SWCDOC_SWCDT006 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();

        if (rCaseId == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        switch (ssUserType)
        {
            case "02":

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

        SBApp.ViewRecord("水土保持完工檢查紀錄表", "update", "");

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
                //string tSWC021ID = readeSwc["SWC021ID"] + "";
                //string tSWC021 = readeSwc["SWC021"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";
                string tSWC058 = readeSwc["SWC058"] + "";

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
                LBSWC038.Text = SBApp.DateView(tSWC038, "00");
                LBSWC039.Text = tSWC039;
                LBSWC043.Text = SBApp.DateView(tSWC043,"00");
                LBSWC044.Text = tSWC044;
                LBSWC051.Text = SBApp.DateView(tSWC051, "00");
                LBSWC052.Text = SBApp.DateView(tSWC052, "00");
                LBSWC058.Text = SBApp.DateView(tSWC058, "00");
            }

            readeSwc.Close();
            objCmdSwc.Dispose();

            if (v2 == "AddNew")
            {
                string nIDF = GetDTLFID(v);
                string ssUserName = Session["NAME"] + "";

                LBDTL001.Text = nIDF;
                TXTDTL004.Text = ssUserName;
            }
            else
            {
                string strSQLRV2 = " select * from SWCDTL06 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   AND DTLF000 = '" + v2 + "' ";

                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLF001 = readeDTL["DTLF001"] + "";
                    string tDTLF002 = readeDTL["DTLF002"] + "";
                    string tDTLF003 = readeDTL["DTLF003"] + "";
                    string tDTLF004 = readeDTL["DTLF004"] + "";

                    string tDTLF013 = readeDTL["DTLF013"] + "";
                    string tDTLF014 = readeDTL["DTLF014"] + "";

                    string tDTLF023 = readeDTL["DTLF023"] + "";
                    string tDTLF024 = readeDTL["DTLF024"] + "";
                    string tDTLF025 = readeDTL["DTLF025"] + "";
                    string tDTLF026 = readeDTL["DTLF026"] + "";
                    string tDTLF027 = readeDTL["DTLF027"] + "";
                    string tDTLF028 = readeDTL["DTLF028"] + "";
                    string tDTLF029 = readeDTL["DTLF029"] + "";
                    string tDTLF030 = readeDTL["DTLF030"] + "";
                    string tDTLF031 = readeDTL["DTLF031"] + "";
                    string tDTLF032 = readeDTL["DTLF032"] + "";
                    string tDTLF033 = readeDTL["DTLF033"] + "";
                    string tDTLF034 = readeDTL["DTLF034"] + "";
                    string tDTLF035 = readeDTL["DTLF035"] + "";
                    string tDTLF036 = readeDTL["DTLF036"] + "";
                    string tDTLF037 = readeDTL["DTLF037"] + "";
                    string tDTLF038 = readeDTL["DTLF038"] + "";
                    string tDTLF039 = readeDTL["DTLF039"] + "";
                    string tDTLF040 = readeDTL["DTLF040"] + "";
                    string tDTLF041 = readeDTL["DTLF041"] + "";
                    string tDTLF042 = readeDTL["DTLF042"] + "";

                    string tDATALOCK = readeDTL["DATALOCK"] + "";

                    LBDTL001.Text = tDTLF001;
                    TXTDTL002.Text = SBApp.DateView(tDTLF002, "00");
                    TXTDTL004.Text = tDTLF004;
                    
                    TXTDTL023.Text = tDTLF023;
                    TXTDTL024.Text = tDTLF024;
                    TXTDTL025.Text = tDTLF025;
                    TXTDTL026.Text = tDTLF026;
                    DDLDTL027.SelectedValue = tDTLF027;
                    TXTDTL028.Text = tDTLF028;
                    TXTDTL029.Text = tDTLF029;
                    TXTDTL030.Text = tDTLF030;
                    TXTDTL031.Text = tDTLF031;
                    TXTDTL032.Text = tDTLF032;
                    TXTDTL033.Text = tDTLF033;
                    TXTDTL034.Text = tDTLF034;
                    TXTDTL035.Text = tDTLF035;
                    TXTDTL036.Text = tDTLF036;
                    TXTDTL037.Text = tDTLF037;
                    TXTDTL038.Text = tDTLF038;
                    TXTDTL039.Text = tDTLF039;
                    TXTDTL040.Text = tDTLF040;
                    TXTDTL041.Text = tDTLF041;
                    TXTDTL042.Text = tDTLF042;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLF024, tDTLF042 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link024, Link042 };

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

                    string tempImgPateh29 = SwcUpLoadFilePath + v + "/" + tDTLF029;
                    HyperLink029.ImageUrl = tempImgPateh29;
                    HyperLink029.NavigateUrl = tempImgPateh29;

                    //點擊放大圖片類處理
                    string[] arrayFileName = new string[] { tDTLF029 };
                    System.Web.UI.WebControls.HyperLink[] arrayImgAppobj = new System.Web.UI.WebControls.HyperLink[] { HyperLink029 };

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

                    //圖片類處理
                    string[] arrayFileName2 = new string[] { tDTLF030, tDTLF032, tDTLF034, tDTLF036, tDTLF038, tDTLF040 };
                    System.Web.UI.WebControls.Image[] arrayImgAppobj2 = new System.Web.UI.WebControls.Image[] { TXTDTL030_img, TXTDTL032_img, TXTDTL034_img, TXTDTL036_img, TXTDTL038_img, TXTDTL040_img };

                    for (int i = 0; i < arrayFileName2.Length; i++)
                    {
                        string strFileName = arrayFileName2[i];
                        System.Web.UI.WebControls.Image ImgFileObj = arrayImgAppobj2[i];

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
                        
                        Response.Write("<script>alert('資料已送出，目前僅供瀏覽。'); location.href='SWCDT006v.aspx?SWCNO=" + v + "&DTLNO=" + v2 + "'; </script>");
                    }
                }
            }

        }
    }

    protected void GenerateDropDownList()
    {
        TXTDTL028.Text = "（一）完工檢查單位及人員：" + System.Environment.NewLine + "（二）會同完工檢查單位及人員：" + System.Environment.NewLine + "（三）承辦監造技師：" + System.Environment.NewLine + "（四）水土保持義務人：";
         
 
 

    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";

        string sSWC000 = rCaseId;
        string sDTLF000 = LBDTL001.Text + "";
        string sDTLF002 = TXTDTL002.Text;
        string sDTLF004 = Session["NAME"]+"";// TXTDTL004.Text;
        string sDTLF023 = TXTDTL023.Text;
        string sDTLF024 = TXTDTL024.Text;
        string sDTLF025 = TXTDTL025.Text;
        string sDTLF026 = TXTDTL026.Text;
        string sDTLF027 = DDLDTL027.SelectedValue;
        string sDTLF028 = TXTDTL028.Text;
        string sDTLF029 = TXTDTL029.Text;
        string sDTLF030 = TXTDTL030.Text;
        string sDTLF031 = TXTDTL031.Text;
        string sDTLF032 = TXTDTL032.Text;
        string sDTLF033 = TXTDTL033.Text;
        string sDTLF034 = TXTDTL034.Text;
        string sDTLF035 = TXTDTL035.Text;
        string sDTLF036 = TXTDTL036.Text;
        string sDTLF037 = TXTDTL037.Text;
        string sDTLF038 = TXTDTL038.Text;
        string sDTLF039 = TXTDTL039.Text;
        string sDTLF040 = TXTDTL040.Text;
        string sDTLF041 = TXTDTL041.Text;
        string sDTLF042 = TXTDTL042.Text;

        if (sDTLF025.Length > 300) { sDTLF025 = sDTLF025.Substring(0, 300); }
        if (sDTLF026.Length > 300) { sDTLF026 = sDTLF026.Substring(0, 300); }
        if (sDTLF028.Length > 800) { sDTLF028 = sDTLF028.Substring(0, 800); }
        if (sDTLF031.Length > 300) { sDTLF031 = sDTLF031.Substring(0, 300); }
        if (sDTLF033.Length > 300) { sDTLF033 = sDTLF033.Substring(0, 300); }
        if (sDTLF035.Length > 300) { sDTLF035 = sDTLF035.Substring(0, 300); }
        if (sDTLF037.Length > 300) { sDTLF037 = sDTLF037.Substring(0, 300); }
        if (sDTLF039.Length > 300) { sDTLF039 = sDTLF039.Substring(0, 300); }
        if (sDTLF041.Length > 300) { sDTLF041 = sDTLF041.Substring(0, 300); }

        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   AND DTLF000 = '" + sDTLF000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL06 (SWC000,DTLF000) VALUES ('" + sSWC000 + "','" + sDTLF000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL06 Set ";
            sEXESQLSTR = sEXESQLSTR + " DTLF001 = DTLF000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLF002 =N'" + sDTLF002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF003 =N'" + sDTLF027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF004 =N'" + sDTLF004 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF023 =N'" + sDTLF023 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF024 =N'" + sDTLF024 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF025 =N'" + sDTLF025 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF026 =N'" + sDTLF026 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF027 =N'" + sDTLF027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF028 =N'" + sDTLF028 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF029 =N'" + sDTLF029 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF030 =N'" + sDTLF030 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF031 =N'" + sDTLF031 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF032 =N'" + sDTLF032 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF033 =N'" + sDTLF033 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF034 =N'" + sDTLF034 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF035 =N'" + sDTLF035 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF036 =N'" + sDTLF036 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF037 =N'" + sDTLF037 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF038 =N'" + sDTLF038 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF039 =N'" + sDTLF039 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF040 =N'" + sDTLF040 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF041 =N'" + sDTLF041 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF042 =N'" + sDTLF042 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLF000 = '" + sDTLF000 + "'";

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

        string[] arryUpLoadField = new string[] { "TXTDTL024", "TXTDTL029", "TXTDTL030", "TXTDTL032", "TXTDTL034", "TXTDTL036", "TXTDTL038", "TXTDTL040", "TXTDTL042" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL024, TXTDTL029, TXTDTL030, TXTDTL032, TXTDTL034, TXTDTL036, TXTDTL038, TXTDTL040, TXTDTL042 };
        string csUpLoadField = "TXTDTL024";
        TextBox csUpLoadAppoj = TXTDTL024;

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

    private string GetDTLFID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RF" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RF" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLF000) AS MAXID from SWCDTL06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLF000,7) = '" + tempVal + "' ";

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

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC002.aspx?CaseId=" + vCaseID);
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
                case "PIC2":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".xls",".xlsx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 excel 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC2":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".doc",".docx",".odt" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF ODT WORD 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 10MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 10000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10 Mb 以下檔案上傳，謝謝!!");
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
                        UpLoadLink.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.NavigateUrl = "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        break;

                    case "PIC2":
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "DOC":
                    case "DOC2":
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

        strSQLClearFieldValue = " update SWCDTL06 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLF001 = '" + csDTLID + "' ";

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

    protected void TXTDTL024_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL024_fileupload, TXTDTL024, "TXTDTL024", "_" + rDTLNO + "_06_chkitem", null, Link024);
    }
    protected void TXTDTL024_fileclean_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL024, null, Link024, "DTLF024", "TXTDTL024", 0, 0);
    }
    protected void TXTDTL029_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL029_fileupload, TXTDTL029, "TXTDTL029", "_" + rDTLNO + "_06_sign", null, HyperLink029);
    }
    protected void TXTDTL029_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL029, TXTDTL029_img, null, "DTLF029", "TXTDTL029", 320, 180);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLF000 = LBDTL001.Text + "";

        string sEXESQLSTR = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   AND DTLF000 = '" + sDTLF000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL06 (SWC000,DTLF000) VALUES ('" + sSWC000 + "','" + sDTLF000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL06 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLF000 = '" + sDTLF000 + "'";

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

            string strSQLRV = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC108 from SWCCASE SWC ";
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
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";
                
                //寄件名單, 注意：ge-10754	施柏宇，寫死，必需寄！

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10754")
                    {
                        SentMailGroup = SentMailGroup + ";;" + aUserMail;

                        //一人一封不打結
                        string[] arraySentMail01 = new string[] { aUserMail };
                        string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增完工檢查紀錄";
                        string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增完工檢查紀錄，請上管理平台查看" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                    }
                }

                //string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                //string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增完工檢查紀錄";
                //string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增完工檢查紀錄，請上管理平台查看" + "<br><br>";
                //ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                //ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                //bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);

                string[] arraySentMail02a = new string[] { tETEmail  };
                string[] arraySentMail02b = new string[] { tSWC108 };
                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】已新增完工檢查紀錄";
                string ssMailBody02 = "您好，" + "【" + tSWC005 + "】已新增完工檢查紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                
                bool MailTo02a = SBApp.Mail_Send(arraySentMail02a, ssMailSub02, ssMailBody02);
                bool MailTo02b = SBApp.Mail_Send(arraySentMail02b, ssMailSub02, ssMailBody02);

                string ssMailBody03 = "您好，【" + tSWC005 + "】已新增完工檢查紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。";
                
                SBApp.SendSMS(tSWC013TEL, ssMailBody03);

            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    protected void TXTDTL030_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL030_fileupload, TXTDTL030, "TXTDTL030", "_" + rDTLNO + "_06_photo1", TXTDTL030_img, null);
    }

    protected void TXTDTL032_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL032_fileupload, TXTDTL032, "TXTDTL032", "_" + rDTLNO + "_06_photo2", TXTDTL032_img, null);
    }

    protected void TXTDTL034_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL034_fileupload, TXTDTL034, "TXTDTL034", "_" + rDTLNO + "_06_photo3", TXTDTL034_img, null);
    }

    protected void TXTDTL036_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL036_fileupload, TXTDTL036, "TXTDTL036", "_" + rDTLNO + "_06_photo4", TXTDTL036_img, null);
    }

    protected void TXTDTL038_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL038_fileupload, TXTDTL038, "TXTDTL038", "_" + rDTLNO + "_06_photo5", TXTDTL038_img, null);
    }

    protected void TXTDTL040_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC2", TXTDTL040_fileupload, TXTDTL040, "TXTDTL040", "_" + rDTLNO + "_06_photo6", TXTDTL040_img, null);
    }

    protected void TXTDTL042_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC2", TXTDTL042_fileupload, TXTDTL042, "TXTDTL042", "_" + rDTLNO + "_06_DOC", null, Link042);
    }

    protected void TXTDTL030_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL030, TXTDTL030_img, null, "DTLF030", "TXTDTL030", 320, 180);
    }

    protected void TXTDTL032_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL032, TXTDTL032_img, null, "DTLF032", "TXTDTL032", 320, 180);

    }

    protected void TXTDTL034_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL034, TXTDTL034_img, null, "DTLF034", "TXTDTL034", 320, 180);

    }

    protected void TXTDTL036_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL036, TXTDTL036_img, null, "DTLF036", "TXTDTL036", 320, 180);

    }

    protected void TXTDTL038_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL038, TXTDTL038_img, null, "DTLF038", "TXTDTL038", 320, 180);

    }

    protected void TXTDTL040_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL040, TXTDTL040_img, null, "DTLF040", "TXTDTL040", 320, 180);

    }

    protected void TXTDTL042_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTDTL042, null, Link042, "DTLF042", "TXTDTL042", 320, 180);
    }
}