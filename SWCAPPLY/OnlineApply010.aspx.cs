using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SWCAPPLY_OnlineApply010 : System.Web.UI.Page
{
	string max = "000";
    protected void Page_Load(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();
        string ssUserID = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string vCaseID = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";
        if (vCaseID.Trim() == "" || ssUserID.Trim() == "") Response.Redirect("~/SWCDOC/SWC001.aspx");

        C20.swcLogRC("OnlineApply010", "恢復審查", "詳情", "瀏覽", vCaseID + "," + rOLANO);
        setPageValue(vCaseID, rOLANO);
		if(ssUserType == "08") DataLock.Visible = false;
    }

    private void setPageValue(string v, string v2)
    {
        Class21 C21 = new Class21();
        GBClass001 SBApp = new GBClass001();

        LBSWC000.Text = v;
        LBSWC002.Text = C21.getSwcData(v,"SWC02");
        if (v2 == "AddNew") {
            string sqlStr = " select * from swcswc where SWC00=@SWC000; ";
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection tslmConn = new SqlConnection(connectionString.ConnectionString))
            {
                tslmConn.Open();
                using (var cmd = tslmConn.CreateCommand())
                {
                    cmd.CommandText = sqlStr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                    #endregion
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerTslm = cmd.ExecuteReader())
                    {
                        if (readerTslm.HasRows)
                        {
                            while (readerTslm.Read())
                            {
                                string qONA001 = GetONAID();
                                string qONA002 = readerTslm["SWC88"] + "";
                                qONA002 = SBApp.DateView(qONA002,"00");
                                string qONA003 = getDayLeft(v, qONA002);
                                string qONA004 = Convert.ToDateTime(DateTime.Now).AddDays(Convert.ToInt32(qONA003)).ToString("yyyy-MM-dd");
								
								//***新增假日判斷
								bool qholiday = true;
								DateTime dt = Convert.ToDateTime(qONA004);
								dt = dt.AddDays(-1);
								while (qholiday == true){
									dt = dt.AddDays(1);
									qholiday = SBApp.isHoliday(dt);
								}
								qONA004 = dt.ToString("yyyy-MM-dd");
								//***新增假日判斷
								
								Label[] aLB = new Label[] { LBOA001, LBOA002, LBOA003, LBOA004 };
                                string[] aLBD = new string[] { qONA001, qONA002, qONA003, qONA004 };
                                for (int i = 0; i < aLB.Length; i++)
                                    aLB[i].Text = aLBD[i];
                            }
                        }
                        readerTslm.Close();
                    }
                    cmd.Cancel();
                }
            }
        } else {
            string sqlStr = " select * from OnlineApply10 where SWC000=@SWC000 and ONA10001=@ONA001; ";
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection swcConn = new SqlConnection(connectionString.ConnectionString))
            {
                swcConn.Open();
                using (var cmd = swcConn.CreateCommand())
                {
                    cmd.CommandText = sqlStr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                    cmd.Parameters.Add(new SqlParameter("@ONA001", v2));
                    #endregion
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerSwc = cmd.ExecuteReader())
                    {
                        if (readerSwc.HasRows) {
                            while (readerSwc.Read()) {
                                string qONA001 = readerSwc["ONA10001"] + "";
                                string qONA002 = readerSwc["ONA10002"] + "";
                                string qONA003 = readerSwc["ONA10003"] + "";
                                string qONA004 = readerSwc["ONA10004"] + "";
                                string qFileName = readerSwc["FileName"] + "";
                                string qFileNameNavigateUrl = readerSwc["FileNameNavigateUrl"] + "";

                                Label[] aLB = new Label[] { LBOA001, LBOA002, LBOA003, LBOA004 };
                                string[] aLBD = new string[] { qONA001, qONA002, qONA003, qONA004 };
                                for (int i = 0; i < aLB.Length; i++)
                                    aLB[i].Text = aLBD[i];
								
								if(qFileName!=""){
									ONA10FileLink.Text = qFileName;
									ONA10FileLink.NavigateUrl = qFileNameNavigateUrl;
									ONA10FileLink.Visible = true;
								}
                            }
                        }
                        readerSwc.Close();
                    }
                    cmd.Cancel();
                }
            }
        }
    }

    private string getDayLeft(string v, string limitDate)
    {
        GBClass001 SBApp = new GBClass001();

        string rValue = "0";
        string sqlStr = " select top 1 * from OnlineApply02 where SWC000=@SWC000 and DATALOCK='Y' order by id desc; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection swcConn = new SqlConnection(connectionString.ConnectionString))
        {
            swcConn.Open();
            using (var cmd = swcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    if (readerSwc.HasRows)
                    {
                        while (readerSwc.Read())
                        {
                            string qONA02D = SBApp.DateView(readerSwc["LOCKDATE"] + "","00");
                            if (qONA02D.Trim()!="" && limitDate!="") {
                                DateTime date1 = Convert.ToDateTime(qONA02D);
                                DateTime date2 = Convert.ToDateTime(limitDate);
                                double s = new TimeSpan(date2.Ticks - date1.Ticks).Days;
                                rValue = s.ToString();
                            }
                        }
                    }
                    readerSwc.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    private string GetONAID()
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "OA10" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "OA10" + Year.ToString() + Month.PadLeft(2, '0') + "000001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(ONA10001) AS MAXID from OnlineApply10 ";
            strSQLRV = strSQLRV + "   where LEFT(ONA10001,9) = '" + tempVal + "' ";

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

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        string rPrevious = Request.QueryString["PV"] + "";
        if (rPrevious == "4") { Response.Redirect("../SWCDOC/SWC004.aspx?SWCNO=" + vCaseID); } else { Response.Redirect("../SWCDOC/SWC003.aspx?ARCTL=5&SWCNO=" + vCaseID); }
    }

    protected void DataLock_Click(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();
        string ssUserID = Session["ID"] + "";

        string tmpSWC000 = LBSWC000.Text;
        string tmpSWC002 = LBSWC002.Text;
        string tmpONA10001 = LBOA001.Text;
        string tmpONA10002 = LBOA002.Text;
        string tmpONA10003 = LBOA003.Text;
        string tmpONA10004 = LBOA004.Text;
		string tmpFileName = ONA10FileLink.Text;
		string tmpFileNameNavigateUrl = ONA10FileLink.NavigateUrl.ToString();
		
        string sqlStr = " INSERT INTO OnlineApply10 ([SWC000],[SWC002],[ONA10001],[ONA10002],[ONA10003],[ONA10004],[FileName],[FileNameNavigateUrl],[FileNameSaveDate],[FileNameSaveUser],[DATALOCK],[LOCKDTAE],[LOCKUSER]) VALUES ";
        sqlStr += "(@SWC000,@SWC002,@ONA10001,@ONA10002,@ONA10003,@ONA10004,@FileName,@FileNameNavigateUrl,getdate(),@FileNameSaveUser,'Y',getdate(),@USERID)";

        sqlStr += " update tslm2.dbo.SWCSWC set SWC04=@SWC004,SWC88=@SWC088 where SWC00=@SWC000; ";
        sqlStr += " update tcgeswc.dbo.SWCCASE set SWC004=@SWC004,SWC088=@SWC088 where SWC000=@SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection swcConn = new SqlConnection(connectionString.ConnectionString))
        {
            swcConn.Open();
            using (var cmd = swcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                C20.swcLogRC("OnlineApply010", "恢復審查", "詳情", "新增", tmpSWC000 + "," + tmpONA10001);
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC002", tmpSWC002));
                cmd.Parameters.Add(new SqlParameter("@SWC004", "審查中"));
                cmd.Parameters.Add(new SqlParameter("@SWC088", tmpONA10004));
                cmd.Parameters.Add(new SqlParameter("@ONA10001", tmpONA10001));
                cmd.Parameters.Add(new SqlParameter("@ONA10002", tmpONA10002));
                cmd.Parameters.Add(new SqlParameter("@ONA10003", tmpONA10003));
                cmd.Parameters.Add(new SqlParameter("@ONA10004", tmpONA10004));
                cmd.Parameters.Add(new SqlParameter("@FileName", tmpFileName));
                cmd.Parameters.Add(new SqlParameter("@FileNameNavigateUrl", tmpFileNameNavigateUrl));
                //cmd.Parameters.Add(new SqlParameter("@FileNameSaveDate", "getdate()"));
                cmd.Parameters.Add(new SqlParameter("@FileNameSaveUser", ssUserID));
                cmd.Parameters.Add(new SqlParameter("@USERID", ssUserID));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        SendMailNotice(tmpSWC000);
		
        Response.Redirect("../SWCDOC/SWC003.aspx?SWCNO=" + tmpSWC000);

    }
	
	
	protected void Btn_UPSFile_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string btnType = ((Button)(sender)).ID;
        string SWC000 = LBSWC000.Text + "";
        string SWC002 = LBSWC002.Text + "";
        string ONA001 = LBOA001.Text + "";
        bool rUP = false;
		string tSFType = "", tSFName = "", filename = "", extension = "";
        string ssUserNAME = Session["NAME"] + "";
        string exeSqlStr = "";
		
		
		
        string tsql = " select ISNULL(max(DTLA006),'0') AS DTLA006 from SWCDTL01 Where SWC000='" + SWC000 + "';";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader reader;
            SqlCommand sc = new SqlCommand(tsql, SWCConn);
            reader = sc.ExecuteReader();

            while (reader.Read())
                max = reader["DTLA006"].ToString();
            reader.Close();
            sc.Dispose();
        }
		max = ("00" + max).Substring(("00" + max).Length - 3, 3);
		
        #region.值設定
        switch (btnType)
        {
            case "Btn_UPONA10File":
				tSFType = max;
				tSFName = ONA001 + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                filename = ONA10File_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", ONA10File_FileUpload, tSFName, null, ONA10FileLink, 150, ""); //150MB
                break;
        }
        #endregion

        #region.存入db
		if (rUP)
        {
            tSFName += extension;
            string updDB = " Update ShareFiles Set [SFName]='" + tSFName + "',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + SWC000 + "' and SFTYPE = '" + tSFType + "' ";

            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from ShareFiles  Where [SWC000]='" + SWC000 + "' and SFTYPE = '" + tSFType + "' ";

                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                if (!readeSwc.HasRows)
                {
                    exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE,createdate) VALUES ('" + SWC000 + "','" + tSFType + "',getdate());";
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                exeSqlStr += updDB;
                SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
        }
        #endregion
    }


    #region.檔案交換區
    private bool FileUpLoadApp2(string ChkType, FileUpload UpLoadBar, string UpLoadReName, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize, string SubFolder)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string CaseId = LBSWC000.Text + "";
        string UpLoadFileName = UpLoadReName;
        bool rValue = false;


        #region.檔案上傳
        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名
            switch (ChkType)
            {
                case "PDF":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".PDF" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 PDF 檔案格式上傳，謝謝!!');</script>");
                        return rValue;
                    }
                    break;
            }

            //檔案大小限制
            int filesize = UpLoadBar.PostedFile.ContentLength;
            if (filesize > _FileMaxSize * 1000000)
            {
                Response.Write("<script>alert('請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!');</script>");
                return rValue;
            }
            UpLoadFileName += extension;

            #region.上傳設定
            //檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + CaseId;
            if (SubFolder.Trim() != "") serverDir += "\\" + SubFolder;
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            string serverFilePath = Path.Combine(serverDir, UpLoadFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(UpLoadFileName);

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);

                switch (ChkType)
                {
                    case "PDF":
                        UpLoadLink.Text = UpLoadFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC\\UpLoadFiles\\SwcCaseFile\\" + CaseId + "\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;
                }

                #region.上傳成功存db
                rValue = true;
                #endregion
            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }
            #endregion
        }
        return rValue;
        #endregion
    }
    #endregion
	
	
	protected void Btn_DelSFile_Click(object sender, EventArgs e)
    {
        string btnType = ((Button)(sender)).ID;

        string rDTLNO = LBSWC000.Text + "";
        string delFileName = "";
		string tSFType = "";
        bool tdel = false;
		HyperLink pgLink = new HyperLink();
		
        tdel = DelPageFile(ONA10FileLink.Text, "");
		//DelPageFile(ONA10FileLink.Text, "");

		
		string tsql = " select ISNULL(max(DTLA006),'0') AS DTLA006 from SWCDTL01 Where SWC000='" + rDTLNO + "';";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader reader;
            SqlCommand sc = new SqlCommand(tsql, SWCConn);
            reader = sc.ExecuteReader();

            while (reader.Read())
                max = reader["DTLA006"].ToString();
            reader.Close();
            sc.Dispose();
        }
		max = ("00" + max).Substring(("00" + max).Length - 3, 3);
			
        #region.存db
		
        if (tdel)
        {
            switch (btnType)
            {
                case "Btn_DelONA10File":
                    pgLink = ONA10FileLink; tSFType = max;
                    break;
            }
			
            string DelDBDA = " Update ShareFiles Set SFName='' Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                SqlCommand objCmdSWC = new SqlCommand(DelDBDA, SwcConn);
                objCmdSWC.ExecuteNonQuery();
                objCmdSWC.Dispose();
            }

            
        }
		
		pgLink.Text = "";
		pgLink.NavigateUrl = "";
		pgLink.Visible = false;
		
        #endregion
    }
    #region.刪檔
    private bool DelPageFile(string delFileName, string delFileType)
    {
        bool rValue = false;
        string csCaseID = LBSWC000.Text + "";

        //刪實體檔
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + delFileName;

        try
        {
            if (File.Exists(FileFullPath)) File.Delete(FileFullPath);
            rValue = true;
        }
        catch { }

        return rValue;
    }
    #endregion

	
	

    private void SendMailNotice(string v)
    {
		GBClass001 SBApp = new GBClass001();
		string tSWC005 = "";				//水土保持計畫名稱
		string tDeadline = LBOA004.Text;	//審查期限
		
		string tSWC013TEL = "";				//義務人手機
        string tSendMail = "";				//聯絡人e-mail
        string tSWC025 = "";				//承辦人員姓名
        string tSWC022ID = "";				//審查公會ID
		
        string tSWC021 = "";				//承辦技師姓名
        string tSWC021ID = "";				//承辦技師ID
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCCASE ";
            strSQLRV = strSQLRV + "   where SWC000 = '" + v + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                tSWC005 = readerSWC["SWC005"]+"";
				
                tSWC013TEL = readerSWC["SWC013TEL"]+"";
				tSendMail = readerSWC["SWC108"]+"";
				tSWC025 = readerSWC["SWC025"]+"";
				tSWC022ID = readerSWC["SWC022ID"]+"";
				
				tSWC021 = readerSWC["SWC021"]+"";
				tSWC021ID = readerSWC["SWC021ID"]+"";
            }
        }
		
        string tMailSub1  = "",tMailText1 = "",SentMailGroup1 = ""; 
        string tMailSub2  = "",tMailText2 = "",SentMailGroup2 = ""; 


		tMailSub1  = "您好，【" + tSWC005 + "】已恢復審查，審查期限至【"+ tDeadline + "】。";
		tMailText1 = "您好，【" + tSWC005 + "】已恢復審查，審查期限至【" + tDeadline + "】。<br><br>";
		tMailText1 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		tMailText1 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
		
		tMailSub2  = "【" + tSWC021 + "】您好，【" + tSWC005 + "】已恢復審查，審查期限至【"+ tDeadline + "】。";
		tMailText2 = "【" + tSWC021 + "】您好，【" + tSWC005 + "】已恢復審查，審查期限至【"+ tDeadline + "】。<br><br>";
		tMailText2 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		tMailText2 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

		string exeSQLSTR1 = " select email as EMAIL from tslm2.dbo.geouser g where ([name] = '"+ tSWC025 + "' or userid = '"+ tSWC022ID + "') and status <> '停用' ;";
		string exeSQLSTR2 = " select ETEmail as EMAIL from ETUsers where etid = '"+ tSWC021ID + "' ;";
		
		#region.名單
		using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
		{
			SwcConn.Open();
	
			SqlDataReader readerItemS;
			SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR1, SwcConn);
			readerItemS = objCmdItemS.ExecuteReader();
	
			while (readerItemS.Read())
			{
				string tEMail = readerItemS["EMAIL"] + "";
				SentMailGroup1 += ";;" + tEMail;
			}
			readerItemS.Close();
			objCmdItemS.Dispose();
		}
		SentMailGroup1 += ";;" + tSendMail;
		string[] arraySentMail01 = SentMailGroup1.Split(new string[] { ";;" }, StringSplitOptions.None);
		bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub1, tMailText1);
		
		
		using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
		{
			SwcConn.Open();
	
			SqlDataReader readerItemS;
			SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR2, SwcConn);
			readerItemS = objCmdItemS.ExecuteReader();
	
			while (readerItemS.Read())
			{
				string tEMail = readerItemS["EMAIL"] + "";
				SentMailGroup2 += ";;" + tEMail;
			}
			readerItemS.Close();
			objCmdItemS.Dispose();
		}
		string[] arraySentMail02 = SentMailGroup2.Split(new string[] { ";;" }, StringSplitOptions.None);
		bool MailTo02 = SBApp.Mail_Send(arraySentMail02, tMailSub2, tMailText2);

		#endregion
		//SBApp.SendSMS(tSWC013TEL, tMailSub1);
		string[] arrayPhoneNo = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
		SBApp.SendSMS_Arr(arrayPhoneNo, tMailSub1);
    }
	
}