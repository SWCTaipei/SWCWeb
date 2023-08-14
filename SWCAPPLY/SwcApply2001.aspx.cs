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

public partial class SWCAPPLY_SwcApply2001 : System.Web.UI.Page
{
    string SwcUpLoadFilePath = "..\\UpLoadFiles\\SwcCaseFile\\";
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
        GBClass001 SBApp = new GBClass001();
        string ssUserName = Session["NAME"] + "";
        string pSwcId = Request.QueryString["SWC000"] + "";
        string pCaseId = Request.QueryString["SA20ID"] + "";
		
        if (ssUserName == "") { Response.Redirect("../SWCDOC/SWC001.aspx"); }
        if (pSwcId.Trim() == "" || pCaseId.Trim() == "") Response.Redirect("~/SWCDOC/SWC001.aspx");
        if (checkOnlyApply(pSwcId) || pCaseId.Trim()!= "addnewapply")
        {
            if (!IsPostBack) { SetPageData(pSwcId, pCaseId); SBApp.ViewRecord("水土保持計畫工期展延", "update", ""); }
        } else
        {
            Response.Write("<script>alert('目前尚有建議核定在審核中，勿重覆申請');location.href='../SWCDOC/SWC003.aspx?SWCNO=" + pSwcId + "';</script>");
        }
		//預設文字
		if (pCaseId.Trim()== "addnewapply"){TBA01.Text = getShareFiles99Date(pSwcId);}
    }

    private bool checkOnlyApply(string pSwcId)
    {
        bool rValue = true;
		//string sqlStr = " select SING007,TMPSN01,* from SwcApply2001 where swc000=@SWC000 and ((DATALOCK='Y' and SING007<>'結案' and SING007<>'決行') or ( DATALOCK<>'Y' and (SING007='結案' or SING007='決行') and TMPSN01='退補正')) ";
        string sqlStr = " select * from  SwcApply2001 where swc000=@SWC000 ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSwcId));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        rValue = false;
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
	
	//判斷是否可送出或暫存
	private bool checkOnlyApply_1(string SWC000,string SWC002,string SAA001,string DATALOCK)
    {
        bool rValue = true;
        string sqlStr = " select * from  SwcApply2001 where swc000=@SWC000 and SWC002=@SWC002 ";
		if(SAA001!=""){
			sqlStr = sqlStr + " and SAA001!=@SAA001 ";
		}if(DATALOCK!=""){
			sqlStr = sqlStr + " and DATALOCK=@DATALOCK ";
		}
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", SWC000));
				cmd.Parameters.Add(new SqlParameter("@SWC002", SWC002));
				if(SAA001!=""){
					cmd.Parameters.Add(new SqlParameter("@SAA001", SAA001));
				}
				if(DATALOCK!=""){
					cmd.Parameters.Add(new SqlParameter("@DATALOCK", DATALOCK));
				}
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        rValue = false;
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
	
    private void SetPageData(string v,string v2)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();
        string ssUserID = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string pgType = Request.QueryString["M"] + "";
        string sqlSTR1 = " select * from swcswc where swc00='"+v+"';";
        string sqlSTR2 = " select * from SwcApply2001 where [SWC000]='" + v + "' and [SAA001]='"+v2+"' ;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();

            SqlDataReader readeTslm;
            SqlCommand objCmdTslm = new SqlCommand(sqlSTR1, TslmConn);
            readeTslm = objCmdTslm.ExecuteReader();

            while (readeTslm.Read())
            {
                string rSWC000 = readeTslm["SWC00"] + "";
                string rSWC002 = readeTslm["SWC02"] + "";
                string rSWC004 = readeTslm["SWC04"] + "";
                string rSWC005 = readeTslm["SWC05"] + "";
                string rSWC007 = readeTslm["SWC07"] + "";
                string rSWC025 = readeTslm["SWC25"] + "";
                string rSWC022ID = readeTslm["SWC022ID"] + ""; 
                string rSWC029 = readeTslm["SWC29"] + "";
                string rSWC029CAD = readeTslm["SWC029CAD"] + "";
                string rSWC030 = readeTslm["SWC30"] + "";
                string rSWC030CAD = readeTslm["SWC030CAD"] + "";
                string rSWC080 = readeTslm["SWC80"] + "";
                string rSWC110 = readeTslm["SWC110"] + "";
                string rSWC118 = readeTslm["SWC118"] + "";
                string rSWC119 = readeTslm["SWC119"] + "";

                if (rSWC119 == "1")
                    CBSWC119.Checked = true;
                else
                    CBSWC119.Checked = false;
                if (rSWC004 == "審查中" && ssUserType=="04" && (rSWC022ID == ssUserID || C20.swcDtlAuthority(rSWC000, ssUserID, "S3"))) { } else { pgType = "EE"; }

                LBSWC000.Text = rSWC000;
                LBSWC002.Text = rSWC002;
                LBSWC005.Text = rSWC005;
                LBSWC007.Text = rSWC007;
                LBSWC025.Text = rSWC025;

                //檔案類處理
                string[] arrayFileNameLink = new string[] { rSWC029, rSWC029CAD, rSWC030, rSWC030CAD, rSWC080, rSWC110, rSWC118 };
                TextBox[] arrayFileNameText = new TextBox[] { TXTSWC029, TXTSWC029CAD, TXTSWC030, TXTSWC030CAD, TXTSWC080, TXTSWC110,TXTSWC118 };
                string[] arrayFileType = new string[] { "TXTSWC029", "TXTSWC029CAD", "TXTSWC030", "TXTSWC030CAD", "TXTSWC080", "TXTSWC110", "TXTSWC118" };
                System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link029, Link029CAD, Link030, Link030CAD, Link080, Link110, Link118 };

                for (int i = 0; i < arrayFileNameLink.Length; i++)
                {
                    string strFileName = Path.GetFileName(arrayFileNameLink[i]);
                    System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];
                    System.Web.UI.WebControls.TextBox FileTextObj = arrayFileNameText[i];

                    FileLinkObj.Visible = false;
                    if (strFileName == "")
                    {
                    }
                    else
                    {
                        string extension = Path.GetExtension(strFileName).ToLowerInvariant();

                        string NewUpath = @"~\OutputFile\" + strFileName;
                        //string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                        string tempLinkPateh = SBApp.getFileUrl(v, rSWC002, rSWC007, arrayFileType[i]) + strFileName;
                        //if (extension == ".pdf") if (SBApp.DLFileReMark(v, strFileName, ""))
                        //tempLinkPateh = NewUpath;

                        Class1 C1 = new Class1();
                        C1.FilesSortOut(strFileName, v, "");

                        FileTextObj.Text = strFileName;
                        FileLinkObj.Text = strFileName;
                        FileLinkObj.NavigateUrl = tempLinkPateh;
                        FileLinkObj.Visible = true;
                    }

                }
                if (rSWC029.Trim() != "")
                {
                    string tSWC029 = Path.GetFileName(rSWC029);
                    BTNLINK029.Text = tSWC029;
                    BTNLINK029.Visible = true;
                    Link029.Visible = false;
                }
                if (rSWC030.Trim() != "")
                {
                    string tSWC030 = Path.GetFileName(rSWC030);
                    BTNLINK030.Text = tSWC030;
                    BTNLINK030.Visible = true;
                    Link030.Visible = false;
                }
                if (rSWC080.Trim() != "")
                {
                    string tSWC080 = Path.GetFileName(rSWC080);
                    BTNLINK080.Text = tSWC080;
                    BTNLINK080.Visible = true;
                    Link080.Visible = false;
                }
                if (rSWC110.Trim() != "")
                {
                    string tSWC110 = Path.GetFileName(rSWC110);
                    BTNLINK110.Text = tSWC110;
                    BTNLINK110.Visible = true;
                    Link110.Visible = false;
                }
            }
        }
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();

            SqlDataReader readeTslm;
            SqlCommand objCmdTslm = new SqlCommand(sqlSTR2, TslmConn);
            readeTslm = objCmdTslm.ExecuteReader();

            while (readeTslm.Read())
            {
                string rSAA001 = readeTslm["SAA001"] + "";
                string rSAA002 = readeTslm["SAA002"] + "";
                string rSAA003 = readeTslm["SAA003"] + "";
                string rSAA004 = readeTslm["SAA004"] + "";
                string rSAACBL = readeTslm["SAACBL"] + "";
                string rSAACBL1 = readeTslm["SAACBL1"] + "";

                LBSA01.Text = rSAA001;
                TBA01.Text = SBApp.DateView(rSAA002, "00");
				if(TBA01.Text == "")
					TBA01.Text = getShareFiles99Date(v);
                switch (rSAA003) {
                    case "1":
                        LBRA.Text = "核定";
                        RadioButton1.Checked = true;
                        break;
                    case "0":
                        LBRA.Text = "不予核定";
                        RadioButton2.Checked = true;
                        break;
                }
                TBRB2.Text = rSAA004;
                CBL.Checked = rSAACBL == "1" ? true : false;
                CBL1.Checked = rSAACBL1 == "1" ? true : false;

                #region 審查結果
                string tDataLock2 = readeTslm["DATALOCK2"] + "";
                string tReviewResults = readeTslm["ReviewResults"] + "";
                string tResultsExplain = readeTslm["ResultsExplain"] + "";
                string LBReviewResults = "審查結果：";
                switch (tReviewResults) {
                    case "0":
                        LBReviewResults += "駁回";
                        break;
                    case "1":
                        LBReviewResults += "准予通過";
                        break;
                    case "2":
                        LBReviewResults += "退補正";
                        break;
                }

                if (tDataLock2 == "Y")
                {
                    ReviewResults.Visible = true;
                    LBRADIORS.Text = LBReviewResults; 
                    LBResultsExplain.Text = tResultsExplain;
                }
                #endregion
            }
        }
        if (v2 == "addnewapply")
        {
            LBSA01.Text = GenCaseId("SA");
        }
        else { }
        if (pgType == "EE")
            pageShowMode();

		//改正日期只抓第一次(也就是主表的值)
        SqlDataSourceSign.SelectCommand = " select left(convert(char, TH001, 120),10) as TH001n,case when SWC125 = null then '' when SWC125 = '1900-01-01 00:00:00.000' then '' else left(convert(char, SWC125, 120),10) end as TH005n,[name] as THName,TH004 from [TrunHistory] h left join tslm2.dbo.geouser u on h.TH003=u.userid left join swccase s on h.ID001=s.SWC000 where TH002 = '退補正' and ID001='" + v + "' and ID003='" + v2 + "' order by h.id; ";
        TXTSWC080_fileuploadok.OnClientClick = string.Format("return UpLoadChk5('{0}');", TXTSWC080_fileupload.ClientID);
        TXTSWC118_fileuploadok.OnClientClick = string.Format("return UpLoadChk5('{0}');", TXTSWC118_fileupload.ClientID);
        TXTSWC029CAD_fileuploadok.OnClientClick = string.Format("return UpLoadChk3('{0}');", TXTSWC029CAD_fileupload.ClientID);
        TXTSWC029_fileuploadok.OnClientClick = string.Format("return UpLoadChk2('{0}');", TXTSWC029_fileupload.ClientID);
        TXTSWC030CAD_fileuploadok.OnClientClick = string.Format("return UpLoadChk4('{0}');", TXTSWC030CAD_fileupload.ClientID);
        TXTSWC030_fileuploadok.OnClientClick = string.Format("return UpLoadChk2('{0}');", TXTSWC030_fileupload.ClientID);
        TXTSWC110_fileuploadok.OnClientClick = string.Format("return UpLoadChk2('{0}');", TXTSWC110_fileupload.ClientID);
    }
    protected void BTNLINK_Click(object sender, EventArgs e)
    {
        Class1 C1 = new Class1();
        string strFileName = "";
        string btnType = ((Button)(sender)).ID;
        string fileType = "";
		
        switch (btnType)
        {
            case "BTNLINK029":
                strFileName = Link029.Text;
                fileType = "6-1";
                break;
            case "BTNLINK030":
                strFileName = Link030.Text;
                fileType = "7-1";
                break;
            case "BTNLINK080":
                strFileName = Link080.Text;
                fileType = "核定本";
                break;
            case "BTNLINK110":
                strFileName = Link110.Text;
                fileType = "審查單位查核表";
                break;
        }
        string sSWC000 = LBSWC000.Text;
        string sSWC002 = LBSWC002.Text;
        string sSWC007 = LBSWC007.Text;
        string extension = Path.GetExtension(strFileName).ToLowerInvariant();
        string NewUpath = @"..\\OutputFile\\" + strFileName;
        string tempLinkPateh = SwcUpLoadFilePath + sSWC000 + "/" + strFileName;
        if (extension == ".pdf") if (C1.DLFileReMark(sSWC000, strFileName, "", sSWC002, sSWC007, fileType)) tempLinkPateh = NewUpath;
        Response.Write("<script>window.open('" + tempLinkPateh + "');</script>");
		
		
    }
    private string GenCaseId(string sType)
    {
        string _ReturnVal = "";
                Random R = new Random();
                string gg = System.DateTime.Now.ToString("yyyyMMddHHmmss");
        _ReturnVal = "SA2001" + gg.Substring(2);// + R.Next(0, 10).ToString().PadLeft(1, '0');
        return _ReturnVal;
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string pSA01= LBSA01.Text + "";
        string pSWC000 = LBSWC000.Text + "";
        string pSWC002 = LBSWC002.Text + "";
        string pSWC005 = LBSWC005.Text + "";
        string pSWC025 = LBSWC025.Text + "";
        string sqlChkCaseStatus = " select * from SwcApply2001 where SWC000=@SWC000 and SAA001=@SAA001 ";
		
        //2021-04-01 修正重複新建問題->再過濾一次是否可送出
        if (!Checkanycase(pSWC000, pSA01))
        {
            Response.Write("<script>alert('目前尚有建議核定在審核中，勿重覆申請');location.href='../SWCDOC/SWC003.aspx?SWCNO=" + pSWC000 + "';</script>");
			return;
		}

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlChkCaseStatus;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                cmd.Parameters.Add(new SqlParameter("@SAA001", pSA01));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows) {
                        pageShowMode();
						//調整
                        //Response.Write("<script>alert('資料已送出，目前僅供瀏覽。');</script>");
                    }
                    else {
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        if (SavePageData("DATALOCK"))
        {
            SaveCase_Click(sender,e); pSA01 = LBSA01.Text + "";
            //2021-04-01 修正重複新建問題->再過濾一次是否可送出
            if(Checkanycase(pSWC000, pSA01))
            {
                string sqlLock = " Update SwcApply2001 Set DATALOCK = 'Y',DATALOCK2 = '',LOCKUSER2 = '',ReviewResults = '',ReviewDoc = '',ResultsExplain = '',ReviewDirections = '',ReSendDeadline = null,SWC005=@SWC005,SWC025=@SWC025,SING002=@SWC025,SING004=@SING004,SING006=@SING006,SING007=N'送出',ONAHEAD=N'建議核定',SING008 = N'待簽辦',LOCKUSER=@USERID,LOCKDATE = getdate() Where SWC000=@SWC000 and SAA001=@SAA001; ";
                sqlLock += " Update SWCSWC set SWC109=getdate() Where SWC00=@SWC000 and (ISNULL(SWC109,'')='' OR SWC109<'1980-01-01' ); Update TCGESWC.dbo.SWCCASE set SWC109=getdate() Where SWC000=@SWC000 and (ISNULL(SWC109,'')='' OR SWC109<'1980-01-01' ); ";
                using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
                {
                    TslmConn.Open();
                    using (var cmd = TslmConn.CreateCommand())
                    {
                        cmd.CommandText = sqlLock;
                        cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                        cmd.Parameters.Add(new SqlParameter("@SWC005", pSWC005));
                        cmd.Parameters.Add(new SqlParameter("@SWC025", pSWC025));
                        cmd.Parameters.Add(new SqlParameter("@SAA001", pSA01));
                        cmd.Parameters.Add(new SqlParameter("@SING004", pSWC025 + ";"));
                        cmd.Parameters.Add(new SqlParameter("@SING006", ssUserName));
                        cmd.Parameters.Add(new SqlParameter("@USERID", ssUserID));
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                string strSQL3 = " INSERT INTO SignRCD ([SWC000],[SWC002],[SWC005],[SWC025],[ONA001],[R001],[R002],[R003],[R004],[R005],[R006],[R007],[R008],[R009],[R010]) VALUES (@SWC000,@SWC002,@SWC005,@SWC025,@SAA001,@R001,@R002,@R003,getdate(),@R005,@R006,@R007,@R008,@R009,@R010) ";
                ConnectionStringSettings connectionString2 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection TslmConn = new SqlConnection(connectionString2.ConnectionString))
                {
                    TslmConn.Open();
                    using (var cmd = TslmConn.CreateCommand())
                    {
                        cmd.CommandText = strSQL3;
                        #region.設定值
                        cmd.Parameters.Add(new SqlParameter("@SWC000", pSWC000));
                        cmd.Parameters.Add(new SqlParameter("@SWC002", pSWC002));
                        cmd.Parameters.Add(new SqlParameter("@SWC005", pSWC005));
                        cmd.Parameters.Add(new SqlParameter("@SWC025", pSWC025));
                        cmd.Parameters.Add(new SqlParameter("@SAA001", pSA01));
                        cmd.Parameters.Add(new SqlParameter("@R001", ""));
                        cmd.Parameters.Add(new SqlParameter("@R002", ""));
                        cmd.Parameters.Add(new SqlParameter("@R003", "送出"));
                        //cmd.Parameters.Add(new SqlParameter("@R004", qSWC000));
                        cmd.Parameters.Add(new SqlParameter("@R005", ""));
                        cmd.Parameters.Add(new SqlParameter("@R006", ""));
                        cmd.Parameters.Add(new SqlParameter("@R007", ssUserGuild == "" ? ssUserName : getGuildName(ssUserGuild)));
                        cmd.Parameters.Add(new SqlParameter("@R008", "審查單位"));
                        cmd.Parameters.Add(new SqlParameter("@R009", ssUserName));
                        cmd.Parameters.Add(new SqlParameter("@R010", DateTime.Now.ToString("MMdd/HHmm")));
                        #endregion
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                GBC.RecordTrunHistory(pSWC000, pSWC002, pSA01, "申請中", ssUserID, "", "");
                SendMailNotice(pSWC000);

                Response.Write("<script>alert('資料已送出。');location.href='../SWCDOC/SWC003.aspx?ARCTL=5&SWCNO=" + pSWC000 + "';</script>");
            }
            else
            {
                //Response.Write("<script>alert('目前尚有建議核定在審核中，勿重覆申請');location.href='../SWCDOC/SWC003.aspx?SWCNO=" + pSWC000 + "';</script>");
            }
        }
        else { }
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

    private void SendMailNotice(string v)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();

        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);
		
		string YN = RadioButton1.Checked ? "核定" : RadioButton2.Checked ? "不予核定" : "";

        //送出提醒名單：股長、系統管理員、承辦人員、章姿隆(ge-10706)
		
        string caseStr = " select * from swcswc where SWC00=@SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = caseStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    while (readerTslm.Read())
                    {
                        string tSWC005 = readerTslm["SWC05"] + "";
                        string tSWC012 = readerTslm["SWC12"] + "";
                        string tSWC025 = readerTslm["SWC25"] + "";
                        string SentMailGroup = "";
                        for (int i = 1; i < arrayUserId.Length; i++)
                        {
                            string aUserId = arrayUserId[i];
                            string aUserName = arrayUserName[i];
                            string aJobTitle = arrayJobTitle[i];
                            string aUserMail = arrayUserMail[i];
                            string aMBGROUP = arrayMBGROUP[i];

                            if (aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10706")
                                SentMailGroup = SentMailGroup + ";;" + aUserMail;
                        }
                        //轄區【水土保持計畫】，公會已於(建議核定表單送出日期)建議核定，請至系統查看。
                        string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                        string ssMailSub01 = tSWC012 + "【" + tSWC005 + "】，已於" + DateTime.Now.ToString("yyyy-MM-dd") + "建議" + YN;
                        string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】，已於" + DateTime.Now.ToString("yyyy-MM-dd") + "建議" + YN + "，請至系統查看。" + "<br><br>";
                        ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                        ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
    }

    private void pageShowMode()
    {
        LBRA.Text = RadioButton1.Checked ? "核定" : RadioButton2.Checked ? "不予核定" : "";
        LBRA.Visible = true;
        RadioButton1.Visible = false;
        RadioButton2.Visible = false;
        TBRB2.Enabled = false;
        CBL.Enabled = false;
        CBL1.Enabled = false;
        DataLock.Visible = false;
        SaveCase.Visible = false;
        CBSWC119.Enabled = false;
        FileUpload[] arrayFileUpload = new FileUpload[] { TXTSWC080_fileupload,TXTSWC029CAD_fileupload, TXTSWC029_fileupload, TXTSWC030CAD_fileupload, TXTSWC030_fileupload, TXTSWC110_fileupload, TXTSWC118_fileupload };
        Button[] arrayButtonO = new Button[] { TXTSWC080_fileuploadok,TXTSWC029CAD_fileuploadok, TXTSWC029_fileuploadok,TXTSWC030CAD_fileuploadok, TXTSWC030_fileuploadok,TXTSWC110_fileuploadok,TXTSWC118_fileuploadok };
        Button[] arrayButtonC = new Button[] { TXTSWC080_fileclean,TXTSWC029CAD_fileclean, TXTSWC029_fileclean, TXTSWC030CAD_fileclean,TXTSWC030_fileclean, TXTSWC110_fileclean, TXTSWC118_fileclean };
        for (int i=0;i<arrayFileUpload.Length;i++) { arrayFileUpload[i].Visible=false; arrayButtonO[i].Visible = false; arrayButtonC[i].Visible = false; }
        TextBoxJSE.Text = "HH";
    }

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        string pCaseId = Request.QueryString["SA20ID"] + "";
        string pgCaseId = pCaseId == "addnewapply" ? GenCaseId("SA") : LBSA01.Text+"";
        string sSWC000 = LBSWC000.Text + "";
        string sSWC002 = LBSWC002.Text + "";
        string sSWC029 = TXTSWC029.Text + "";
        string sSWC029CAD = TXTSWC029CAD.Text + "";
        string sSWC030 = TXTSWC030.Text + "";
        string sSWC030CAD = TXTSWC030CAD.Text + "";
        string sSWC080 = TXTSWC080.Text + "";
        string sSWC110 = TXTSWC110.Text + "";
        string sSWC118 = TXTSWC118.Text + "";
        string sSWC119 = CBSWC119.Checked ? "1" : "0";
        string sSAA002 = TBA01.Text + "";
        string sSAA003 = RadioButton1.Checked ? "1":RadioButton2.Checked?"0":"";
        string sSAA004 = TBRB2.Text + "";
        string sSAACBL = CBL.Checked ? "1" : "0";
        string sSAACBL1 = CBL1.Checked ? "1" : "0";

		string Act = ((Button)sender).ID + "";
		switch (Act)
		{
			case "SaveCase":
				if(checkOnlyApply_1(sSWC000,sSWC002,pgCaseId,"")==false){
					Response.Write("<script>alert('已經有建議核定單');location.href='../SWCDOC/SWC003.aspx?&SWCNO=" + sSWC000 + "';</script>");
					return;
				}
				break;
		}

        if (SavePageData("")) {
            string updSql = pCaseId=="addnewapply" ? " INSERT INTO SwcApply2001 (SWC000,SWC002,SAA001) VALUES (@SWC000,@SWC002,@SAA001); " : "";
            updSql += " Update SwcApply2001 Set SAA002=@SAA002,SAA003=@SAA003,SAA004=@SAA004,SAA005='1',SAACBL=@SAACBL,SAACBL1=@SAACBL1,savedate=getdate(),saveuser=@saveuser Where SWC000=@SWC000 and SAA001=@SAA001; ";
            updSql += " Update SWCSWC Set SWC29=@SWC029,SWC029CAD=@SWC029CAD,SWC30=@SWC030,SWC030CAD=@SWC030CAD,SWC80=@SWC080,SWC110=@SWC110,SWC118=@SWC118,SWC119=@SWC119 Where SWC00=@SWC000; ";
            updSql += " Update TCGESWC.dbo.SWCCASE Set SWC029=@SWC029,SWC029CAD=@SWC029CAD,SWC030=@SWC030,SWC030CAD=@SWC030CAD,SWC080=@SWC080,SWC110=@SWC110,SWC118=@SWC118,SWC119=@SWC119 Where SWC000=@SWC000; ";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = updSql;
                    cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                    cmd.Parameters.Add(new SqlParameter("@SWC002", sSWC002));
                    cmd.Parameters.Add(new SqlParameter("@SWC029", sSWC029));
                    cmd.Parameters.Add(new SqlParameter("@SWC029CAD", sSWC029CAD));
                    cmd.Parameters.Add(new SqlParameter("@SWC030", sSWC030));
                    cmd.Parameters.Add(new SqlParameter("@SWC030CAD", sSWC030CAD));
                    cmd.Parameters.Add(new SqlParameter("@SWC080", sSWC080));
                    cmd.Parameters.Add(new SqlParameter("@SWC110", sSWC110));
                    cmd.Parameters.Add(new SqlParameter("@SWC118", sSWC118));
                    cmd.Parameters.Add(new SqlParameter("@SWC119", sSWC119));
                    cmd.Parameters.Add(new SqlParameter("@SAA001", pgCaseId));
                    cmd.Parameters.Add(new SqlParameter("@SAA002", sSAA002));
                    cmd.Parameters.Add(new SqlParameter("@SAA003", sSAA003));
                    cmd.Parameters.Add(new SqlParameter("@SAA004", sSAA004));
                    cmd.Parameters.Add(new SqlParameter("@SAACBL", sSAACBL));
                    cmd.Parameters.Add(new SqlParameter("@SAACBL1", sSAACBL1));
                    cmd.Parameters.Add(new SqlParameter("@saveuser", ssUserID));
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
            LBSA01.Text = pgCaseId;
            string thisPageAct = ((Button)sender).ID + "";
            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='../SWCDOC/SWC003.aspx?ARCTL=5&SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
        } else { }
    }

    private bool SavePageData(string button)
    {
		if(button == "DATALOCK")
		{
			Class1 C1 = new Class1();
			GBClass001 SBApp = new GBClass001();
			string pgTBA01 = TBA01.Text + "";
	
			if (TBA01.Text.Trim()=="" && RadioButton2.Checked == false) { Response.Write("<script>alert('您好，檢視本收件日期欄位不可為空白，請確認承辦技師是否已上傳檢視本。');</script>"); TBA01.Focus(); return false; }
			if (!C1.chkDateFormat(pgTBA01) && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');</script>"); TBA01.Focus(); return false; }
			if (TXTSWC080.Text.Trim() == "" && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您尚未上傳核定本，請重新輸入。');</script>"); TXTSWC080.Focus(); return false; }
			
			string FileFullPath = SBApp.getFilePath(LBSWC000.Text, LBSWC002.Text, LBSWC007.Text) + LBSWC002.Text + "//掃描檔//掃描檔//" + TXTSWC080.Text.Trim();
			if (!File.Exists(FileFullPath) && RadioButton2.Checked == false)
			{
				Response.Write("<script>alert('提醒您，您尚未上傳核定本或是上傳失敗，敬請重新上傳。');</script>"); TXTSWC080.Focus();
				return false;
			}
			
			if (TXTSWC029CAD.Text.Trim() == "" && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您尚未上傳水土保持設施配置圖，請重新輸入。');</script>"); TXTSWC029CAD.Focus(); return false; }
			if (TXTSWC029.Text.Trim() == "" && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您尚未上傳水土保持設施配置圖，請重新輸入。');</script>"); TXTSWC029.Focus(); return false; }
			if (TXTSWC030CAD.Text.Trim() == "" && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您尚未上傳臨時性防災設施配置圖，請重新輸入。');</script>"); TXTSWC030CAD.Focus(); return false; }
			if (TXTSWC030.Text.Trim() == "" && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您尚未上傳臨時性防災設施配置圖，請重新輸入。');</script>"); TXTSWC030.Focus(); return false; }
			if (TXTSWC110.Text.Trim() == "") { Response.Write("<script>alert('提醒您，您尚未上傳審查單位查核表，請重新輸入。');</script>"); TXTSWC110.Focus(); return false; }
			if (RadioButton1.Checked == false && RadioButton2.Checked == false) { Response.Write("<script>alert('提醒您，您尚未點選公會建議。');</script>"); RadioButton1.Focus(); return false; };
			if (CBL.Checked == false) { Response.Write("<script>alert('提醒您，您尚未勾選「請確認上方欄位資料是否正確，如需修改請回到主案編輯」。');</script>"); CBL.Focus(); return false; };
			if (CBL1.Checked == false) { Response.Write("<script>alert('提醒您，您尚未勾選「已完成應檢核清單的確認」。');</script>"); CBL1.Focus(); return false; };
		}
		return true;
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWC000"] + "";
        string rPrevious = Request.QueryString["PV"] + "";
        if (rPrevious == "4") { Response.Redirect("../SWCDOC/SWC004.aspx?SWCNO=" + vCaseID); } else { Response.Redirect("../SWCDOC/SWC003.aspx?ARCTL=5&SWCNO=" + vCaseID); }
    }


    protected void TXTSWC080_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC080.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC080_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("PDF", TXTSWC080_fileupload, TXTSWC080, "TXTSWC080", "_" + rDTLNO + "_pdf02", null, Link080, 500); //500MB
    }

    protected void TXTSWC080_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC080.Text;
        string rDTLNO = LBSWC000.Text + "";
        BTNLINK080.Visible = false;
        DeleteUpLoadFile("PDF", TXTSWC080, null, Link080, "SWC080", "TXTSWC080", 0, 0);
    }

    protected void TXTSWC029CAD_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC029CAD.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC029CAD_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("CAD6-1", TXTSWC029CAD_fileupload, TXTSWC029CAD, "TXTSWC029CAD", "_" + rDTLNO + "_CAD", null, Link029CAD, 50); //50MB
    }

    protected void TXTSWC029CAD_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC029CAD.Text;
        string rDTLNO = LBSWC000.Text + "";
        DeleteUpLoadFile("CAD", TXTSWC029CAD, null, Link029CAD, "SWC029CAD", "TXTSWC029CAD", 0, 0);
    }
    protected void TXTSWC030CAD_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC030CAD.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC030CAD_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("CAD7-1", TXTSWC030CAD_fileupload, TXTSWC030CAD, "TXTSWC030CAD", "_" + rDTLNO + "_CAD", null, Link030CAD, 150); //50MB
    }

    protected void TXTSWC030CAD_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC030CAD.Text;
        string rDTLNO = LBSWC000.Text + "";
        DeleteUpLoadFile("CAD", TXTSWC030CAD, null, Link030CAD, "SWC030CAD", "TXTSWC030CAD", 0, 0);
    }

    protected void TXTSWC029_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC029.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC029_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("PDF6-1", TXTSWC029_fileupload, TXTSWC029, "TXTSWC029", "_" + rDTLNO + "", null, Link029, 50); //50MB
    }

    protected void TXTSWC029_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC029.Text;
        string rDTLNO = LBSWC000.Text + "";
        BTNLINK029.Visible = false;
        DeleteUpLoadFile("PDF", TXTSWC029, null, Link029, "SWC029", "TXTSWC029", 0, 0);
    }
    protected void TXTSWC030_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC030.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC030_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("PDF7-1", TXTSWC030_fileupload, TXTSWC030, "TXTSWC030", "_" + rDTLNO + "", null, Link030, 50); //50MB
    }

    protected void TXTSWC030_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC030.Text;
        string rDTLNO = LBSWC000.Text + "";
        BTNLINK030.Visible = false;
        DeleteUpLoadFile("PDF", TXTSWC030, null, Link030, "SWC030", "TXTSWC030", 0, 0);
    }
    protected void TXTSWC110_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC110.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC110_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("PDF", TXTSWC110_fileupload, TXTSWC110, "TXTSWC110", "_" + rDTLNO + "", null, Link110, 50); //50MB
    }

    protected void TXTSWC110_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC110.Text;
        string rDTLNO = LBSWC000.Text + "";
        BTNLINK110.Visible = false;
        DeleteUpLoadFile("PDF", TXTSWC110, null, Link110, "SWC110", "TXTSWC110", 0, 0);
    }
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csCaseID2 = LBSWC002.Text + "";
        string strSQLClearFieldValue = "", strSQLClearFieldValue55 = "";

        //從資料庫取得資料
        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCCASE set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";

        string DelFieldValue55 = DelFieldValue;
        if (DelFieldValue55 == "SWC029CAD" || DelFieldValue55 == "SWC030CAD") { }
        else
        {
            DelFieldValue55 = DelFieldValue.Replace("SWC0", "SWC");
        }

        strSQLClearFieldValue55 = " update SWCSWC set ";
        strSQLClearFieldValue55 = strSQLClearFieldValue55 + DelFieldValue55 + "='' ";
        strSQLClearFieldValue55 = strSQLClearFieldValue55 + " where SWC00 = '" + csCaseID + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();

            SqlCommand objCmdTsl = new SqlCommand(strSQLClearFieldValue55, TslmConn);
            objCmdTsl.ExecuteNonQuery();
            objCmdTsl.Dispose();
        }
		
        //刪實體檔
        GBClass001 SBApp = new GBClass001();
        string strFileName = ImgText.Text + "";
        string tmpSWC000 = LBSWC000.Text + "";
        string tmpSWC002 = LBSWC002.Text + "";
        string tmpSWC007 = LBSWC007.Text + "";
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

        string DelFileName = ImgText.Text;
        //string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        string FileFullPath = SBApp.getFilePath(tmpSWC000, tmpSWC002, tmpSWC007) + tmpSWC002;

        switch (DelFieldValue)
        {
            case "SWC029":
                FileFullPath += "//審查//6-1//";
                break;
            case "SWC029CAD":
                FileFullPath += "//審查//6-1-CAD//";
                break;
            case "SWC030":
                FileFullPath += "//審查//7-1//";
                break;
            case "SWC030CAD":
                FileFullPath += "//審查//7-1-CAD//";
                break;
            case "SWC080":
                FileFullPath += "//掃描檔//掃描檔//";
                break;
            case "SWC110":
                FileFullPath += "//審查單位查核表//審查單位查核表//";
                break;
            case "SWC118":
                FileFullPath += "//公開核定本//公開核定本//";
                break;
        }
        FileFullPath += strFileName;
        //try
        //{
        //    if (File.Exists(TempFileFullPath))
        //    {
        //        File.Delete(TempFileFullPath);
        //    }
        //}
        //catch
        //{
        //}
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
            case "CAD":
            case "PDF":
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

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = LBSWC000.Text + "";
        string FileId = LBSWC002.Text + "";
        string sSWC007 = LBSWC007.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

            string extension = Path.GetExtension(filename).ToLowerInvariant();
            #region 檢查
            // 判斷是否為允許上傳的檔案附檔名

            switch (ChkType)
            {
                case "竣工圖說CAD":
                case "CAD6-1":
                    List<string> allowedExtextsion04 = new List<string> { ".dwg", ".DWG" };

                    if (allowedExtextsion04.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 CAD 檔案格式上傳，謝謝!!');</script>");
                        return;
                    }
                    break;
                case "CAD7-1":
                    List<string> allowedExtextsion05 = new List<string> { ".dwg", ".DWG",".7z" };

                    if (allowedExtextsion05.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 CAD 7z 檔案格式上傳，謝謝!!');</script>");
                        return;
                    }
                    break;
                case "PDF6-1":
                case "PDF7-1":
                case "PDF":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".PDF" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 PDF 檔案格式上傳，謝謝!!');</script>");
                        return;
                    }
                    break;
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 JPG PNG 檔案格式上傳，謝謝!!');</script>");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".xls", ".xlsx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 Excel 檔案格式上傳，謝謝!!');</script>");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 5MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > _FileMaxSize * 1000000)
            //if (filesize > 5000000)
            {
                Response.Write("<script>alert('請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!');</script>");
                return;
            }
            #endregion

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = MyBassAppPj.getFilePath(CaseId, FileId, sSWC007) + FileId;
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = Path.GetFileNameWithoutExtension(filename) + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);

            switch (ChkType)
            {
                case "CAD6-1":
                    SwcFileName = FileId + "_6-1.dwg";
                    serverDir += "\\審查";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    serverDir += "\\6-1-CAD";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    break;
                case "PDF6-1":
                    SwcFileName = FileId + "R_6-1.pdf";
                    serverDir += "\\審查";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    serverDir += "\\6-1";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    break;
                case "CAD7-1":
                    SwcFileName = FileId + "_7-1"+ extension;
                    serverDir += "\\審查";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    serverDir += "\\7-1-CAD";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    break;
                case "PDF7-1":
                    SwcFileName = FileId + "R_7-1.pdf";
                    serverDir += "\\審查";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    serverDir += "\\7-1";
                    if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                    break;
                default:
                    switch (UpLoadStr) {
                        case "TXTSWC080":
                            SwcFileName = FileId + "R_核定本.pdf";
                            serverDir += "\\掃描檔";
                            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                            serverDir += "\\掃描檔";
                            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                            break;
                        case "TXTSWC110":
                            SwcFileName = FileId + "_審查單位查核表.pdf";
                            serverDir += "\\審查單位查核表";
                            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                            serverDir += "\\審查單位查核表";
                            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                            break;
                        case "TXTSWC118":
                            SwcFileName = FileId + "_核定本R.pdf";
                            serverDir += "\\公開核定本";
                            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                            serverDir += "\\公開核定本";
                            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
                            break;
                    }
                    break;
                    
            }
			
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
            string pdfWMGimg = Session["ETU_Guild01"] + "";
            try
            {
                UpLoadBar.SaveAs(serverFilePath);

                string newFileName = CaseId.Substring(CaseId.Length - 5, 5);
				
                switch (UpLoadStr)
                {
                    case "TXTSWC029":
                        newFileName = "PDF6-1_" + newFileName + ".pdf";
                        newFileName = FileId + "_6-1.pdf";
                        //浮水印處理                        
                        MyBassAppPj.PDFWaterMark(serverFilePath, Path.Combine(serverDir, newFileName), pdfWMGimg);
                        System.IO.File.Delete(@serverDir+"\\"+ SwcFileName);
                        SwcFileName = newFileName;
                        break;
                    case "TXTSWC030":
                        newFileName = "PDF7-1_" + newFileName + ".pdf";
                        newFileName = FileId + "_7-1.pdf";
                        //浮水印處理
                        MyBassAppPj.PDFWaterMark(serverFilePath, Path.Combine(serverDir, newFileName), pdfWMGimg);
                        System.IO.File.Delete(@serverDir + "\\" + SwcFileName);
                        SwcFileName = newFileName;
                        break;
                    case "TXTSWC080":
                        string tmpFileName = "核定本S_" + newFileName + ".pdf";
                        newFileName = "核定本_" + newFileName + ".pdf";
                        SwcFileName = newFileName;
                        //SwcFileName = tmpFileName;
                        //浮水印處理
                        MyBassAppPj.PDFWaterMark(serverFilePath, Path.Combine(serverDir, tmpFileName), pdfWMGimg);
                        MyBassAppPj.PDFWaterMark2(Path.Combine(serverDir, tmpFileName), Path.Combine(serverDir, newFileName), pdfWMGimg, LBSWC000.Text);
                        string delFile01 = serverFilePath;
                        string delFile02 = Path.Combine(serverDir, tmpFileName);
                        if (File.Exists(delFile01))
                            File.Delete(delFile01);
                        if (File.Exists(delFile02))
                            File.Delete(delFile02);
                        break;
                }
                //UpLoadStr

                //error_msg.Text = "檔案上傳成功";

                switch (ChkType)
                {
                    case "PIC":
                        UpLoadView.Attributes.Add("src", "..\\UpLoadFiles\\temp\\" + CaseId + "\\" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "竣工圖說CAD":
                    case "CAD6-1":
                    case "PDF6-1":
                    case "CAD7-1":
                    case "PDF7-1":
                    case "PDF":
                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = MyBassAppPj.getFileUrl(CaseId, FileId, sSWC007, UpLoadStr) + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;

                }
                UpLoadText.Text = SwcFileName;
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

    protected void SqlDataSourceSign_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        ReqCount.Text = e.AffectedRows.ToString();
    }

    protected void SWC118_fileuploadok_Click(object sender, EventArgs e)
    {
        string vTempValue = TXTSWC118.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return;
        }
        string tUpLoadFile = TXTSWC118_fileupload.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return;
        }
        string rDTLNO = LBSWC000.Text + "";
        FileUpLoadApp("PDF", TXTSWC118_fileupload, TXTSWC118, "TXTSWC118", "_" + rDTLNO + "_核定本", null, Link118, 500); //150MB
    }

    protected void TXTSWC118_fileclean_Click(object sender, EventArgs e)
    {
        string sourceFILENAME = TXTSWC118.Text;
        string rDTLNO = LBSWC000.Text + "";
        BTNLINK080.Visible = false;
        DeleteUpLoadFile("PDF", TXTSWC118, null, Link118, "SWC118", "TXTSWC118", 0, 0);
    }
	
    public bool Checkanycase(string swc000, string saa001)
    {
        bool QQ = false;
        bool AA = false, BB = false, CC = false;
        string TMPSN01 = "", DATALOCK = "", SAA001 = "";
        string[] tmpsn01, datalock, sa001;
        //string sqlStr = " select * from  SwcApply2001 where SWC000=@SWC000 and SAA001 != @SAA001 ";
        string sqlStr = " select * from  SwcApply2001 where SWC000=@SWC000 ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", swc000));
                cmd.Parameters.Add(new SqlParameter("@SAA001", saa001));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                    {
                        while (readerTslm.Read())
                        {
                            SAA001 += readerTslm["SAA001"] + ";";
                            TMPSN01 += readerTslm["TMPSN01"] + ";";
                            DATALOCK += readerTslm["DATALOCK"] + ";";
                        }
                    }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        tmpsn01 = TMPSN01.Split(';'); TMPSN01 = "";
        datalock = DATALOCK.Split(';'); DATALOCK = "";
        sa001 = SAA001.Split(';'); SAA001 = "";

        for (int i = 0; i < sa001.Length; i++)
        {
            if(tmpsn01[i] != "")
            {
                if(datalock[i] != "")
                {
                    CC = true;
                    DATALOCK += sa001[i] + ";";
                }
                else
                {
                    BB = true;
                    TMPSN01 += sa001[i] + ";";
                }
            }
            else
            {
                AA = true;
            }

            if(BB || CC) { AA = false; }
        }

        if (AA) { QQ = true; }
        else
        {
            tmpsn01 = TMPSN01.Split(';'); TMPSN01 = "";
            datalock = DATALOCK.Split(';'); DATALOCK = "";

            if (BB)
            {
                foreach(string Q in tmpsn01)
                {
                    if(Q == saa001) { QQ = true; }
                }
            }
            else
            {
                if (CC)
                {
                    foreach (string Q in datalock)
                    {
                        if (Q == saa001) { QQ = true; }
                    }
                }
            }

            if (CC)
            {
                foreach (string Q in datalock)
                {
                    if (Q == saa001) { QQ = true; }
                    else { QQ = false; }
                }
            }

        }


        return QQ;
    }
	//取得檢視本上傳日期
	protected string getShareFiles99Date(string swc000)
    {
		GBClass001 SBApp = new GBClass001();
		string sqlStr = " select * from  Sharefiles where SWC000=@SWC000 and SFTYPE='099' ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();
            using (var cmd = Conn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", swc000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            return SBApp.DateView(reader["savedate"]+"","00");
                        }
                    }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
		return "";
	}

}