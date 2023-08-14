using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        if(!canDoModify(rCaseId, rDTLId))
            Response.Redirect("SWCDT006v.aspx?SWCNO=" + rCaseId + "&DTLNO=" + rDTLId);

        if (rCaseId == "")
            Response.Redirect("SWC001.aspx");

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();
		

        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";



        if (!IsPostBack)
        {
            C20.swcLogRC("SWCDT006", "完工檢查紀錄表", "詳情", "瀏覽", rCaseId + "," + rDTLId);
            GenerateDropDownList();
			GV2Page(rCaseId, rDTLId);
            Data2Page(rCaseId, rDTLId);
			
			if(ssUserType == "09") DataLock.Visible = false;
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

    private bool canDoModify(string pSWC000,string pDTLF000)
    {
        Class1 C1 = new Class1();
        bool rValue = true;
        string ssUserID = Session["ID"] + "";

        string sqlStr = " select * from SWCDTL06 Where SWC000=@SWC000 and DTLF000=@DTLF000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                cmd.Parameters.Add(new SqlParameter("@DTLF000", pDTLF000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read()) {
                        string tmpDataLock = readerTslm["DATALOCK"] + "";
                        string tmpDataLock2 = readerTslm["DATALOCK2"] + "";
                        string tmpSaveuser = readerTslm["Saveuser"] + "";
                        string tmpReviewResults = readerTslm["ReviewResults"] + "";

                        if (tmpDataLock == "Y")
                            rValue = false;
                        else if(tmpDataLock2 != "Y" && tmpSaveuser!= ssUserID && C1.getSWCSWCData(pSWC000, "SWC024ID") != ssUserID)
                            rValue = false;

                        //if (tmpDataLock != "Y" && tmpDataLock2 == "Y" && tmpReviewResults == "2" || (C1.getSWCSWCData(pSWC000, "SWC024ID") == ssUserID || chkSubAuth(ssUserID))) { } else { rValue = false; }
						//Modified by Tim 20210615
						//1.退補正要判斷該公會及檢查委員可編輯 2.未送出自己可編輯
						if ((tmpDataLock != "Y" && tmpDataLock2 == "Y" && tmpReviewResults == "2" && (C1.getSWCSWCData(pSWC000, "SWC024ID") == ssUserID || chkSubAuth(ssUserID))) || (C1.getSWCSWCData(pSWC000, "SWC024ID") == ssUserID || chkSubAuth(ssUserID))) { } else { rValue = false; }

                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;



    }

    private bool chkSubAuth(string ssUserID)
    {
        bool rValue = false;
        string rCaseId = Request.QueryString["SWCNO"] + "";

        string sqlStr = " select * from GuildGroup where SWC000=@SWC000 and ETID=@SSUID and RGType = 'S4'; ";
        #region
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", rCaseId));
                cmd.Parameters.Add(new SqlParameter("@SSUID", ssUserID));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        rValue = true;
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }

    private void Data2Page(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

        string tSWC024 = "";
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
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                //string tSWC021ID = readeSwc["SWC021ID"] + "";
                //string tSWC021 = readeSwc["SWC021"] + "";
                tSWC024 = readeSwc["SWC024"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
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
                LBSWC002.Text = tSWC002;
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
                LBSWC025.Text = tSWC025;
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
                string ssGuildId = Session["ETU_Guild02"] + "";

                LBDTL001.Text = nIDF;
                TXTDTL004.Text = tSWC024;
            }
            else
            {
                string strSQLRV2 = " select * from SWCDTL06 ";
                strSQLRV2 = strSQLRV2 + " where SWC000 = '" + v + "' ";
                strSQLRV2 = strSQLRV2 + "   AND DTLF000 = '" + v2 + "' ";
                C20.swcLogRC("SWCDT006", "完工申報書","詳情","瀏覽",v+";;"+v2);
                SqlDataReader readeDTL;
                SqlCommand objCmdDTL = new SqlCommand(strSQLRV2, SwcConn);
                readeDTL = objCmdDTL.ExecuteReader();

                while (readeDTL.Read())
                {
                    string tDTLF001 = readeDTL["DTLF001"] + "";
                    string tDTLF002 = readeDTL["DTLF002"] + "";
                    string tDTLF003 = readeDTL["DTLF003"] + "";
                    string tDTLF004 = readeDTL["DTLF004"] + "";                    
                    #region
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
                    string tDTLF043 = readeDTL["DTLF043"] + "";
                    string tDTLF044 = readeDTL["DTLF044"] + "";
                    string tDATALOCK = readeDTL["DATALOCK"] + "";
                    #endregion
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
                    TXTONA003.Text = tDTLF043;
                    TXTONA008.Text = tDTLF044;

                    //檔案類處理
                    string[] arrayFileNameLink = new string[] { tDTLF024, tDTLF042, tDTLF043, tDTLF044 };
                    System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link024, Link042, Link003, Link008 };

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
                            string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                            FileLinkObj.Text = strFileName;
                            FileLinkObj.NavigateUrl = tempLinkPateh;
                            FileLinkObj.Visible = true;
                        }

                    }

                    string tempImgPateh29 = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + tDTLF029;
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
                            string tempImgPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
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
                            string tempImgPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
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
        SetDtlData(v,v2);
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,left(convert(char, TH005, 120),10) as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
    }
    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }

    protected void GenerateDropDownList()
    {
        TXTDTL028.Text = "（一）完工檢查單位及人員：" + System.Environment.NewLine + "（二）會同完工檢查單位及人員：" + System.Environment.NewLine + "（三）承辦監造技師：" + System.Environment.NewLine + "（四）水土保持義務人：";
         
 
 

    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserID = Session["ID"] + "";
        string ssGuildId = Session["ETU_Guild02"] + "";

        string sSWC000 = rCaseId;
        #region
        string sDTLF000 = LBDTL001.Text + "";
        string sDTLF002 = TXTDTL002.Text;
        //string sDTLF004 = SBApp.GetGeoUser(ssGuildId, "Name");// TXTDTL004.Text;
        string sDTLF004 = TXTDTL004.Text;
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
        string sDTLF043 = TXTONA003.Text;
        string sDTLF044 = TXTONA008.Text;
        string Q = "";

        if (sDTLF025.Length > 300) { sDTLF025 = sDTLF025.Substring(0, 300); }
        if (sDTLF026.Length > 300) { sDTLF026 = sDTLF026.Substring(0, 300); }
        if (sDTLF028.Length > 800) { sDTLF028 = sDTLF028.Substring(0, 800); }
        if (sDTLF031.Length > 300) { sDTLF031 = sDTLF031.Substring(0, 300); }
        if (sDTLF033.Length > 300) { sDTLF033 = sDTLF033.Substring(0, 300); }
        if (sDTLF035.Length > 300) { sDTLF035 = sDTLF035.Substring(0, 300); }
        if (sDTLF037.Length > 300) { sDTLF037 = sDTLF037.Substring(0, 300); }
        if (sDTLF039.Length > 300) { sDTLF039 = sDTLF039.Substring(0, 300); }
        if (sDTLF041.Length > 300) { sDTLF041 = sDTLF041.Substring(0, 300); }
        #endregion

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
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL06 (SWC000,DTLF000) VALUES ('" + sSWC000 + "','" + sDTLF000 + "');"; Q = "A";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();

            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL06 Set ";
            sEXESQLSTR = sEXESQLSTR + " DTLF001 = DTLF000, ";
            C20.swcLogRC("SWCDT006", "完工檢查紀錄表", "詳情", "存檔", sSWC000 + "," + sDTLF000);
            #region
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
            sEXESQLSTR = sEXESQLSTR + " DTLF043 =N'" + sDTLF043 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLF044 =N'" + sDTLF044 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLF000 = '" + sDTLF000 + "'";

            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";
            #endregion
            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
			//存image列表
			SAVE_IMAGE_LIST(sSWC000,sDTLF000);
            #region.
            if (Q == "")
            {
                string strSQLText = " select * from SwcItemChk where SWC000 = '" + sSWC000 + "' and DTLRPNO = '" + sDTLF000 + "' ";

                SqlDataReader readerTest;
                SqlCommand objCmdText = new SqlCommand(strSQLText, SwcConn);
                readerTest = objCmdText.ExecuteReader();

                if (!readerTest.HasRows) Q = "A";
                readeSwc.Close();
                objCmdSwc.Dispose();
            }
            #endregion
            SavChkSwcItem(Q);
            
            string vCaseID = Request.QueryString["SWCNO"] + "";
            Response.Write("<script>location.href='SWC003.aspx?SWCNO=" + vCaseID + "';</script>");

        }
    }
    private void SavChkSwcItem(string v)
    {
        int gLine = 0;
        string exeSqlStr = "";
        string nMSG01 = "";
        string ssUserID = Session["ID"] + "";

        DataTable dtSDI = new DataTable();
        dtSDI = (DataTable)ViewState["SwcDocItem"];

        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            if (++gLine % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[gLine - 2].Cells[3].Text;

                HiddenField HDF001 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI001");
				//********************				
				HiddenField HDF008 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI008");
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
                TextBox RCH10 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH10");
                TextBox TSIC2 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK002");
                TextBox TSIC7 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK007");
                DropDownList DDLPASS = (DropDownList)SDIList.Rows[gLine - 1].Cells[3].FindControl("DDLPASS");

                string mSWC000 = LBSWC000.Text;
                string mDTLE001 = LBDTL001.Text;

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
                //************************************
				string mSIC08b = HDF008.Value;
				string mSIC10a = RCH10.Text;
                string mSIC10b = DDLPASS.SelectedItem.Text;
                string mSIC19 = LBSDI019.Text;
                //bool mSIC10c = DDLPASS.Enabled;

                if (mSIC10b != "") mSIC08 = TXTDTL002.Text;

                //數量差異百分比：SIC02=(D2-D1)/D1
                double mSIC02 = 0; double mSIC02_1 = 0;
                /*
				if (mSIC19 == "是")
                {
                    string[] sArray = mSIC01a.Split('~');
                    string Arr_a = sArray[0];
                    string Arr_b = sArray[1];
                    if (Convert.ToDouble(Arr_a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(Arr_a)) / Convert.ToDouble(Arr_a) * 100, 2);
                    if (Convert.ToDouble(Arr_b) != 0) mSIC02_1 = Math.Round((double)(Convert.ToDouble(mSIC01c) - Convert.ToDouble(Arr_b)) / Convert.ToDouble(Arr_b) * 100, 2);
                }
                else
                {
                    if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
                }
				*/
				if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
                
                //尺寸差異百分比：1:(A2-A1)/A1，2:((A2*B2)-(A1*B1))/(A1*B1)，3:((A2*B2*C2)-(A1*B1*C1))/(A1*B1*C1)
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
                v = ck2021NCC(mSWC000, mDTLE001, mSDI001);
                if (tSDIFD004 == "其他" || mSIC01r != mSIC01b || /* mSIC01r2 != mSIC01c ||*/ mSIC04r != mSIC04b || mSIC04r2 != mSIC04c || mSIC05r != mSIC05b || mSIC05r2 != mSIC05c || mSIC06r != mSIC06b || mSIC06r2 != mSIC06c || mSIC10a != mSIC10b || v == "A" || mSIC01Da != mSIC01Dr || mSIC04Da != mSIC04Dr)
                {
                    if (v == "A") { 
						exeSqlStr = " insert into SwcItemChk (SWC000,DTLRPNO,SDI001,DTLTYPE,SIC01,SIC01_1,SIC01D,SIC02,SIC02_1,SIC03,SIC04,SIC04_1,SIC04D,SIC05,SIC05_1,SIC06,SIC06_1,SIC07,SIC07_1,SIC08,SIC10,SaveUser,SaveDate) values (@SWC000,@DTLRPNO,@SDI001,'D6',@SIC01,@SIC01_1,@SIC01D,@SIC02,@SIC02_1,@SIC03,@SIC04,@SIC04_1,@SIC04D,@SIC05,@SIC05_1,@SIC06,@SIC06_1,@SIC07,@SIC07_1,@SIC08,@SIC10,@SaveUser,getdate());"; 
						//exeSqlStr = " insert into SwcItemChk (SWC000,DTLRPNO,SDI001,DTLTYPE,SIC01,SIC01D,SIC02,SIC03,SIC04,SIC04D,SIC05,SIC06,SIC07,SIC10,SaveUser,SaveDate) values (@SWC000,@DTLRPNO,@SDI001,'D6',@SIC01,@SIC01D,@SIC02,@SIC03,@SIC04,@SIC04D,@SIC05,@SIC06,@SIC07,@SIC10,@SaveUser,getdate());"; 
						
					} 
					else 
					{ 
						exeSqlStr = " update SwcItemChk set SIC01=@SIC01,SIC01_1=@SIC01_1,SIC01D=@SIC01D,SIC02=@SIC02,SIC02_1=@SIC02_1,SIC03=@SIC03,SIC04=@SIC04,SIC04_1=@SIC04_1,SIC04D=@SIC04D,SIC05=@SIC05,SIC05_1=@SIC05_1,SIC06=@SIC06,SIC06_1=@SIC06_1,SIC07=@SIC07,SIC07_1=@SIC07_1,SIC08=@SIC08,SIC10=@SIC10,SaveUser=@SaveUser,SaveDate=getdate() where SWC000=@SWC000 and DTLRPNO=@DTLRPNO and SDI001=@SDI001; "; 
						//exeSqlStr = " update SwcItemChk set SIC01=@SIC01,SIC01D=@SIC01D,SIC02=@SIC02,SIC03=@SIC03,SIC04=@SIC04,SIC04D=@SIC04D,SIC05=@SIC05,SIC06=@SIC06,SIC07=@SIC07,SIC10=@SIC10,SaveUser=@SaveUser,SaveDate=getdate() where SWC000=@SWC000 and DTLRPNO=@DTLRPNO and SDI001=@SDI001; "; 
					}
					if(exeSqlStr != "")
					{
						ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
						using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
						{
							SWCConn.Open();
	
							using (var cmd = SWCConn.CreateCommand())
							{
								cmd.CommandText = exeSqlStr;
								//設定值
								#region
								
								if (mSIC10b=="通過" && v == "A" && mSIC01r == mSIC01b && /* mSIC01r2 == mSIC01c &&*/ mSIC04r == mSIC04b && mSIC04r2 == mSIC04c && mSIC05r == mSIC05b && mSIC05r2 == mSIC05c && mSIC06r == mSIC06b && mSIC06r2 == mSIC06c && mSIC10a == mSIC10b && mSIC01Da == mSIC01Dr && mSIC04Da == mSIC04Dr)
									mSIC08 = ToSimpleUSDate(mSIC08b);
								
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
								cmd.Parameters.Add(new SqlParameter("@SIC10", mSIC10b));
								cmd.Parameters.Add(new SqlParameter("@SaveUser", ssUserID));
								#endregion
								cmd.ExecuteNonQuery();
								cmd.Cancel();
							}
						}
					}
                }
            }
        }
        if (nMSG01 != "") Response.Write("<script>alert('" + nMSG01 + "');</script>");
    }
	private string ToSimpleUSDate(string tDate)
    {
        string rValue = tDate;
        if(rValue !="")
        rValue = tDate.Replace(tDate.Substring(0, 3), (Int32.Parse(tDate.Substring(0, 3)) +1911).ToString());
        return rValue;
    }

    private string ck2021NCC(string mSWC000, string mDTLE001, string mSDI001)
    {
        string rValue = "";
        string sqlStr = " select * from SwcItemChk where SWC000=@SWC000 and DTLRPNO=@RGType and SDI001=@ETID; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", mSWC000));
                cmd.Parameters.Add(new SqlParameter("@RGType", mDTLE001));
                cmd.Parameters.Add(new SqlParameter("@ETID", mSDI001));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (!readerTslm.HasRows)
                        rValue = "A";
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL024", "TXTDTL029", "TXTDTL030", "TXTDTL032", "TXTDTL034", "TXTDTL036", "TXTDTL038", "TXTDTL040", "TXTDTL042", "TXTDTL043", "TXTDTL044" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL024, TXTDTL029, TXTDTL030, TXTDTL032, TXTDTL034, TXTDTL036, TXTDTL038, TXTDTL040, TXTDTL042, TXTONA003, TXTONA008 };
        string csUpLoadField = "TXTDTL024";
        TextBox csUpLoadAppoj = TXTDTL024;

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
                case "PIC3":
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
                case "CAD":
                    List<string> allowedExtextsion04 = new List<string> { ".dwg", ".DWG" };

                    if (allowedExtextsion04.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 CAD 檔案格式上傳，謝謝!!');</script>");
                        return;
                    }
                    break;
            }

            // 限制檔案大小，限制為 50MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > 50000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50 Mb 以下檔案上傳，謝謝!!");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + CaseId;

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
                string filesUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/";
				string thUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;

                switch (ChkType)
                {
					case "PIC3":
                        UpLoadLink.ImageUrl = SwcFileName;
                        UpLoadLink.NavigateUrl = thUrl;
						IMAGE_TO_LIST("2", SwcFileName);
						break;
						
                    case "PIC":
                        UpLoadLink.ImageUrl = filesUrl + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.NavigateUrl = filesUrl + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        break;

                    case "PIC2":
                        UpLoadView.Attributes.Add("src", filesUrl + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "/" + SwcFileName, 320, 180);
                        break;

                    case "CAD":
                    case "DOC":
                    case "DOC2":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = filesUrl + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
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
		//目前最大NO
		string no_now = "";
		//目前最大NO+1(最新的)
		string no_new = "01";
		if(GVIMAGE.Rows.Count > 0)
		{
			no_now = GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text;
			no_new = "0" + (Convert.ToInt32(GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text) + 1).ToString();
			no_new = no_new.Substring(no_new.Length-2,2);
		}
		
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC3", TXTDTL029_fileupload, TXTDTL029, "TXTDTL029", "_" + rDTLNO + "_06" + no_new + "_sign", null, HyperLink029);
    }
    protected void TXTDTL029_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTDTL029, TXTDTL029_img, null, "DTLF029", "TXTDTL029", 320, 180);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
        Class20 C20 = new Class20();

        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        //string ssUserGuild = Session["ETU_Guild01"] + "";
		string ssUserGuild = Session["ETU_Guild02"] + "";
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC005 = LBSWC005.Text;
        string sSWC025 = LBSWC025.Text;
        string sDTLF000 = LBDTL001.Text + "";
        string sEXESQLSTR = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from TCGESWC.dbo.SWCDTL06 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   AND DTLF000 = '" + sDTLF000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO TCGESWC.dbo.SWCDTL06 (SWC000,DTLF000) VALUES ('" + sSWC000 + "','" + sDTLF000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR += " Update TCGESWC.dbo.SWCDTL06 Set ";
            sEXESQLSTR += " SWC005 = N'" + sSWC005 + "', ";
            sEXESQLSTR += " SWC025 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING002 = N'" + sSWC025 + "', ";
            sEXESQLSTR += " SING004 = N'" + sSWC025 + ";', ";
            sEXESQLSTR += " SING006 = N'" + ssUserName + "', ";
            sEXESQLSTR += " SING007 = N'送出',ONAHEAD=N'水土保持計畫完工查核表',SING008 = N'待簽辦', ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLF000 = '" + sDTLF000 + "';";
            string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@ONA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
            C20.swcLogRC("SWCDT006", "完工檢查紀錄表", "詳情", "送出", sSWC000 + "," + sDTLF000);
            ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = strSQL3 + sEXESQLSTR;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                    cmd.Parameters.Add(new SqlParameter("@SWC005", sSWC005));
                    cmd.Parameters.Add(new SqlParameter("@SWC025", sSWC025));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", sDTLF000));
                    cmd.Parameters.Add(new SqlParameter("@R001", ""));
                    cmd.Parameters.Add(new SqlParameter("@R002", ""));
                    cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                    //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                    cmd.Parameters.Add(new SqlParameter("@R005", ""));
                    cmd.Parameters.Add(new SqlParameter("@R006", ""));
                    cmd.Parameters.Add(new SqlParameter("@R007", ssUserGuild == "" ? ssUserName : getGuildName(ssUserGuild)));
                    cmd.Parameters.Add(new SqlParameter("@R008", "檢查單位"));
                    cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                    cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/HHmm")));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
        }
        SaveCase_Click(sender, e);
        UpdateSwcDataNFiles();
        SendMailNotice(sSWC000);
    }
    private string getGuildName(string v)
    {
        string rValue = v;
        string sqlStr = " select * from geouser where userid=@userid ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@userid", v));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        rValue = readerTslm["name"] + "";
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
    private void UpdateSwcDataNFiles()
    {
        GBClass001 SBApp = new GBClass001();
        bool rValue = false, folderExists = false;
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
                cmd.CommandText = sqlStrT + sqlStrD;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC101", tmpSWC101));
                cmd.Parameters.Add(new SqlParameter("@SWC101CAD", tmpSWC101CAD));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
                string filePath01 = "";
                string filePath02 = "";
                if (tmpSWC101.Trim() != "")
                {
                    filePath01 = ConfigurationManager.AppSettings["SwcFilePath20"] + tmpSWC000 + "\\" + tmpSWC101;
                    filePath02 = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002 + "\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += "竣工圖說\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += "竣工圖說\\";
                    folderExists = Directory.Exists(filePath02);
                    if (folderExists == false)
                        Directory.CreateDirectory(filePath02);
                    filePath02 += tmpSWC101;
                    moveFiles(filePath01, filePath02);
                    //D:\公用區\唯讀區\107年掃描圖檔\水保申請案件\水保計畫\UA0210705001-2\竣工圖說\竣工圖說CAD
                }
                if (tmpSWC101CAD.Trim() != "")
                {
                    filePath01 = ConfigurationManager.AppSettings["SwcFilePath20"] + tmpSWC000 + "\\" + tmpSWC101CAD;
                    folderExists = Directory.Exists(SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002 + "\\竣工圖說\\竣工圖說CAD");
					if (folderExists == false)
					{
						Directory.CreateDirectory(SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002 + "\\竣工圖說\\竣工圖說CAD");
					}
					filePath02 = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002 + "\\竣工圖說\\竣工圖說CAD\\" + tmpSWC101CAD;
                    moveFiles(filePath01, filePath02);
                }
            }
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
		
		//送出提醒名單：股長、管理者、承辦人員、沈漢國(ge-10755)、章姿隆(ge-10706)

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

                string SentMailGroup = "";
                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10755" || aUserId.Trim() == "ge-10706")
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
                
                string[] arraySWC013TEL = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
				SBApp.SendSMS_Arr(arraySWC013TEL, ssMailBody03);
            }

            readeSwc.Close();
            objCmdSwc.Dispose();
        }
    }
    private void moveFiles(string filePath01, string filePath02)
    {
        Boolean fileExists;
        fileExists = File.Exists(filePath01);
        if (fileExists)
        {
            if (File.Exists(filePath02))
                File.Delete(filePath02);
            File.Copy(filePath01, filePath02);
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
            Label LabelX1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LabelX1");
            Label LabelX2 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LabelX2");
            TextBox TXTCHK002 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK002");
            TextBox TXTCHK002_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK002_1");
            TextBox TXTCHK007 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK007");
            TextBox TXTCHK007_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK007_1");
            Label LBCHK002 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002");
            Label LBCHK002_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002_1");
            Label LBCHK002pers = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002pers");
            Label LBCHK002pers_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002pers_1");
            Label ITNONE03 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE03");
            Label ITNONE04 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE04");
            Label LB004D = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB004D");
            Label ITNONE05 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE05");
            Label LBCHK007 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007");
            Label LBCHK007_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007_1");
            Label LBCHK007pers = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007pers");
            Label LBCHK007pers_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007pers_1");
            Label LB019 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB019");

            if (++aaaaaa % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 2].Cells[3].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");
                DropDownList DDLPASS = (DropDownList)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("DDLPASS");
                TextBox RCH010 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("RCH10");
                Button MODIFYDATA = (Button)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("MODIFYDATA");


                Label A1_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A1");
                Label A2_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A2");
                Label A3_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A3");
                Label A4_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A4");
                TextBox CHK01_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK001_1");
                TextBox CHK04_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK004_1");
                TextBox CHK05_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK005_1");
                TextBox CHK06_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK006_1");

                LBIT06.Visible = false; LBIT06D.Visible = false;
                ITNONE03.Visible = false;
                ITNONE04.Visible = false;
                ITNONE05.Visible = false;
                LB004D.Visible = false;
                DDLPASS.Visible = true;
				
				//水土保持設施類別非臨時開頭移除"已由永久設施取代"
                if (SDIList.Rows[aaaaaa - 2].Cells[1].Text.Substring(0, 2) != "臨時")
                {
                    DDLPASS.Items.Remove("已由永久設施取代");
                }
				
                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; CHK06.Enabled = true; CHK06_1_onoff.Enabled = true; break; }
				}
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = false; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; CHK06.Enabled = true; CHK06_1_onoff.Enabled = false; break; }
				}
                
                if (LB019.Text == "是" && tSDIFD004 != "其他") { A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; }
                else { A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; A2_onoff.Visible = false; CHK04_1_onoff.Visible = false; A3_onoff.Visible = false; CHK05_1_onoff.Visible = false; A4_onoff.Visible = false; CHK06_1_onoff.Visible = false; }

                if (RCH010.Text == "通過" || RCH010.Text == "已由永久設施取代") { CHK01D.Enabled = false; CHK04D.Enabled = false; MODIFYDATA.Visible = true; CHK01.Enabled = false; CHK01_1_onoff.Enabled = false; CHK04.Enabled = false; CHK04_1_onoff.Enabled = false; CHK05.Enabled = false; CHK05_1_onoff.Enabled = false; CHK06.Enabled = false; CHK06_1_onoff.Enabled = false; DDLPASS.Enabled = false; TXTCHK002.Enabled = false; TXTCHK002_1.Enabled = false; TXTCHK007.Enabled = false; TXTCHK007_1.Enabled = false; } else { CHK01D.Enabled = true; CHK04D.Enabled = true; }
				
                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = true; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; CHK06.Visible = true; CHK06_1_onoff.Visible = true; break; }
                    /*LBCHK002_1.Visible = true; LBCHK002pers_1.Visible = true;*/
					LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false;
					LBCHK007_1.Visible = true; LBCHK007pers_1.Visible = true;
                }
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = false; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; CHK06.Visible = true; CHK06_1_onoff.Visible = false; break; }
                    LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false; LBCHK007_1.Visible = false; LBCHK007pers_1.Visible = false;
                }
				
				if (tSDIFD004 == "其他") { CHK01.Visible = false; CHK01D.Visible = true; CHK04D.Visible = true; CHK04.Visible = false; CHK05.Visible = false; CHK06.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; CHK04D.Visible = true; LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = true; TXTCHK007_1.Visible = false; LBCHK002.Visible = false; LBCHK002_1.Visible = false; TXTCHK002.Visible = true; TXTCHK002_1.Visible = false; }
                else { CHK01.Visible = true; CHK01D.Visible = false; CHK04.Visible = true; CHK05.Visible = true; CHK06.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; CHK04D.Visible = false; LBCHK007.Visible = true; LBCHK007_1.Visible = false; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK002.Visible = true; LBCHK002_1.Visible = false; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; }

            }
            else
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 1].Cells[3].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");

                CHK01.Visible = false; A1.Visible = false; CHK01_1.Visible = false; CHK01D.Visible = false;
                CHK04.Visible = false; A2.Visible = false; CHK04_1.Visible = false; CHK04D.Visible = false;
                CHK05.Visible = false; A3.Visible = false; CHK05_1.Visible = false;
                CHK06.Visible = false; A4.Visible = false; CHK06_1.Visible = false;
                switch (HDF011.Value) { case "1": ITNONE04.Text = "-"; ITNONE05.Text = "-"; break; case "2": ITNONE05.Text = "-"; break; }
                LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK007pers.Visible = false; LBCHK007pers_1.Visible = false; LBCHK002.Visible = false; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; LBCHK002pers.Visible = false; LBCHK002pers_1.Visible = false;
                if (tSDIFD004 == "其他") { LBIT06.Visible = false; LBIT06D.Visible = true; LB004D.Visible = true; ITNONE03.Visible = false; ITNONE04.Visible = false; ITNONE05.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; }
                else { LBIT06.Visible = true; LBIT06D.Visible = false; LB004D.Visible = false; ITNONE03.Visible = true; ITNONE04.Visible = true; ITNONE05.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; }
            }
			ITNONE03.Text = Server.HtmlDecode(ITNONE03.Text);
            ITNONE04.Text = Server.HtmlDecode(ITNONE04.Text);
            ITNONE05.Text = Server.HtmlDecode(ITNONE05.Text);
            LBIT06.Text = Server.HtmlDecode(LBIT06.Text);
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
                string sSDI018 = readerItem["SDI018"] + "";
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
                    SDITB.Columns.Add(new DataColumn("SDIFD017", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD018", typeof(string)));
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
                    SDITB.Columns.Add(new DataColumn("SDICHK010", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }
                if (sSDI017 != "完成") sSDI017 = "未完成";
                if (sSDI019 == "是" && sSDI005 != "其他")
                {
                    //sSDI006 = sSDI006 + "~" + sSDI006_1;
                    sSDI006 = sSDI006;
					sSDI012 = sSDI012 + "~" + sSDI012_1;
                    sSDI013 = sSDI013 + "~" + sSDI013_1;
                    sSDI014 = sSDI014 + "~" + sSDI014_1;
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
                SDITBRow["SDIFD012_1"] = sSDI012_1;
                SDITBRow["SDIFD012D"] = sSDI012D;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD013_1"] = sSDI013_1;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD014_1"] = sSDI014_1;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDIFD017"] = sSDI017;
                SDITBRow["SDIFD018"] = sSDI018;
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
                SDITBRow["SDICHK010"] = sSDI018;

                tbSDIVS.Rows.Add(SDITBRow);

                //DB
                if (v2 == "AddNew")
                {
                    using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                    {
                        ItemConnS.Open();

                        string strSQLRVS = " select top 1 DTLC001 as MAXKEY  from SWCDTL03 D3 left join SwcItemChk Ck on D3.swc000=Ck.SWC000 and D3.DTLC000=Ck.DTLRPNO where D3.swc000='" + rSWCNO + "' and D3.DATALOCK='Y' and isnull(Ck.SWC000,'') <>'' order by D3.savedate DESC ";
                        
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
                    SDITBRow2["SDIFD012D"] = sSDI012D;
                    SDITBRow2["SDIFD013"] = sSDI013;
                    SDITBRow2["SDIFD014"] = sSDI014;
                    SDITBRow2["SDIFD015"] = sSDI015;
                    SDITBRow2["SDIFD016"] = sSDI016;
                    SDITBRow2["SDIFD017"] = "";
                    SDITBRow2["SDIFD018"] = tSIC10;
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
                    SDITBRow2["SDICHK010"] = tSIC10.Trim();

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
                TextBox CHK02 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK002");                
                TextBox CHK04 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006_1");
                TextBox CHK07 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK007");

                DropDownList DDLPASS = (DropDownList)SDIList.Rows[aa].Cells[3].FindControl("DDLPASS");

                CHK01.Enabled = true; CHK01_1.Enabled = true; CHK02.Enabled = true; CHK07.Enabled = true;
                switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; CHK06.Enabled = true; CHK06_1.Enabled = true; break; }
                DDLPASS.Enabled = true;
                CHK01D.Enabled = true; CHK04D.Enabled = true;
                break;
        }

    }

    protected void TXTONA003_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC2", TXTONA003_fileupload, TXTONA003, "TXTDTL043", "_" + rDTLNO + "_06_竣工圖說", null, Link003);
    }

    protected void TXTONA008_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("CAD", TXTONA008_fileupload, TXTONA008, "TXTDTL044", "_" + rDTLNO + "_06_竣工圖說CAD", null, Link008);        
    }

    protected void TXTONA003_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA003, null, Link003, "DTLF043", "TXTDTL043", 320, 180);
    }

    protected void TXTONA008_fileuploaddel_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        DeleteUpLoadFile("DOC", TXTONA008, null, Link008, "DTLF044", "TXTDTL044", 320, 180);
    }
	
	private void GV2Page(string v, string v2)
    {
		string sqlStr = " select * from SWCDTL06_IMAGE where SWC000=@SWC000 and DTLF000=@DTLF000 order by NO; ";
		
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];

		if (tbIMAGE == null)
		{
			DataTable GVTBIMAGE = new DataTable();
		
			GVTBIMAGE.Columns.Add(new DataColumn("NO", typeof(string)));
			GVTBIMAGE.Columns.Add(new DataColumn("IDENTITY", typeof(string)));
			GVTBIMAGE.Columns.Add(new DataColumn("NAME", typeof(string)));
			GVTBIMAGE.Columns.Add(new DataColumn("IMAGENAME", typeof(string)));
		
			ViewState["SwcIMAGE"] = GVTBIMAGE;
			tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		}
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.Parameters.Add(new SqlParameter("@DTLF000", v2));
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                        while (readerSWC.Read())
                        {			
							DataRow GVTBIMAGERow = tbIMAGE.NewRow();
			
							GVTBIMAGERow["NO"] = readerSWC["NO"] + "";
							GVTBIMAGERow["IDENTITY"] = readerSWC["IDENTITY"] + "";
							GVTBIMAGERow["NAME"] = readerSWC["NAME"] + "";
							GVTBIMAGERow["IMAGENAME"] = readerSWC["IMAGENAME"] + "";
			
							tbIMAGE.Rows.Add(GVTBIMAGERow);
			
							//Store the DataTable in ViewState
							ViewState["SwcIMAGE"] = tbIMAGE;
			
							GVIMAGE.DataSource = tbIMAGE;
							GVIMAGE.DataBind();
                        }
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
	}
	protected void GVIMAGE_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                break;
            case DataControlRowType.DataRow:
                //HyperLink Hyper = new HyperLink();
                //Hyper.Text = e.Row.Cells[3].Text;
				//Hyper.Target = "_blank";
                //Hyper.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + e.Row.Cells[3].Text;
                //e.Row.Cells[3].Controls.Add(Hyper);
				
				HyperLink hl = (HyperLink)e.Row.FindControl("link"); 
				if (hl != null) 
				{
					hl.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + hl.Text;
				}
                break;
        }
    }
	//加入清單 p1=>簽名(1)上傳(2) p2=>上傳檔名
	private void IMAGE_TO_LIST(string p1, string p2)
    {
		string rCaseId = Request.QueryString["SWCNO"] + "";
		string rDTLNO = LBDTL001.Text + "";
		
		//目前最大NO
		string no_now = "";
		string no_new = "01";
		if(GVIMAGE.Rows.Count > 0)
		{
			no_now = GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text;
			//目前最大NO+1(最新的)
			no_new = "0" + (Convert.ToInt32(GVIMAGE.Rows[GVIMAGE.Rows.Count-1].Cells[0].Text) + 1).ToString();
			no_new = no_new.Substring(no_new.Length-2,2);
		}
		
		
		string SwcFileName = "";
		
		//手寫簽名
		if(p1 == "1")
		{
			//上傳檔案
			string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + rCaseId;
			
			if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
			
			SwcFileName = rCaseId + "_" + rDTLNO + "_06"+no_new+"_sign.png";
			
			string serverFilePath = Path.Combine(serverDir, SwcFileName);
			
			
			//---------------------------------------
			string dir = serverDir;
			bool dirExists = Directory.Exists(dir);
			if (!dirExists)
			    Directory.CreateDirectory(dir);
			string SavePath = serverFilePath;
			var bytes = Convert.FromBase64String(hfImageData.Text.Replace("data:image/png;base64,",""));
			using (var imageFile = new FileStream(SavePath, FileMode.Create))
			{
			    imageFile.Write(bytes, 0, bytes.Length);
			    imageFile.Flush();
			}
			//---------------------------------------
			
			string thUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + rCaseId + "/" + SwcFileName;
			
			
		}
		//上傳檔案
		else
		{
			SwcFileName = p2;
		}
		//加入清單
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		DataRow GVTBIMAGERow = tbIMAGE.NewRow();
		
		GVTBIMAGERow["NO"] = no_new;
		GVTBIMAGERow["IDENTITY"] = DDL_Sign.SelectedValue;
		GVTBIMAGERow["NAME"] = TB_Sign.Text;
		GVTBIMAGERow["IMAGENAME"] = SwcFileName;
		
		tbIMAGE.Rows.Add(GVTBIMAGERow);
		
		//Store the DataTable in ViewState
		ViewState["SwcIMAGE"] = tbIMAGE;
		
		GVIMAGE.DataSource = tbIMAGE;
		GVIMAGE.DataBind();
	}
	protected void btn2_Click(object sender, EventArgs e)
    {
        IMAGE_TO_LIST("1","");
    }
	protected void GVIMAGE_RowDeleting(object sender, GridViewDeleteEventArgs e)
	{
		int index = Convert.ToInt32(e.RowIndex);
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		tbIMAGE.Rows[index].Delete();
		ViewState["SwcIMAGE"] = tbIMAGE;
		GVIMAGE.DataSource = tbIMAGE;
		GVIMAGE.DataBind();
	}
	protected void SAVE_IMAGE_LIST(string sSWC000, string ssDTL000)
	{
		string sqlstr = " delete SWCDTL06_IMAGE where SWC000=@SWC000 and DTLF000=@DTLF000;";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = sqlstr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
				cmd.Parameters.Add(new SqlParameter("@DTLF000", ssDTL000));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
		
		DataTable tbIMAGE = (DataTable)ViewState["SwcIMAGE"];
		for(int i = 0; i < tbIMAGE.Rows.Count; i++)
		{
			sqlstr = " insert into SWCDTL06_IMAGE(SWC000,DTLF000,NO,[IDENTITY],NAME,IMAGENAME) values (@SWC000,@DTLF000,@NO,@IDENTITY,@NAME,@IMAGENAME); ";
			
			using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
			{
				SwcConn.Open();
				
				using (var cmd = SwcConn.CreateCommand())
				{
					cmd.CommandText = sqlstr;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
					cmd.Parameters.Add(new SqlParameter("@DTLF000", ssDTL000));
					cmd.Parameters.Add(new SqlParameter("@NO", tbIMAGE.Rows[i].ItemArray[0]));
					cmd.Parameters.Add(new SqlParameter("@IDENTITY", tbIMAGE.Rows[i].ItemArray[1]));
					cmd.Parameters.Add(new SqlParameter("@NAME", tbIMAGE.Rows[i].ItemArray[2]));
					cmd.Parameters.Add(new SqlParameter("@IMAGENAME", tbIMAGE.Rows[i].ItemArray[3]));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
	}
	
}