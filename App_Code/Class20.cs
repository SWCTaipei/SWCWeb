using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

/// <summary>
/// Class20 的摘要描述
/// </summary>
public class Class20: System.Web.UI.Page
{
    public Class20()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    #region 台北通登入-new
    public Boolean GetLoginStatus(string lID, string lPw, string lType)
    {
        Boolean _ReturnValue = false;
        string SQL_PW = "", SQL_Unit = "", SQL_NAME = "", SQL_department = "", SQL_AuthMwall = "", SQL_Status = "", SQL_DataView = "", SQL_DateEdit = "", SQL_jobtitle = "", SQL_jobTitle = "", SQL_EDIT = "", SQL_GuildSubstitute = "", SQL_GuildSubstitute2 = "", SQL_GuildTcgeChk = "", SQL_GuildTcgeChk2 = "", SQL_Guild01 = "", SQL_Guild02 = "", SQL_ServiceSubstitute = "",SQL_IDNO="", SQL_USERID="";
        
		Session["UserType"] = "";
        Session["ID"] = "";
        Session["PW"] = "";
        Session["NAME"] = "";
        Session["Unit"] = "";
        Session["JobTitle"] = "";
        Session["Edit4"] = "";
        Session["WMGuild"] = "";
        Session["Guild01"] = "";
        Session["Guild02"] = "";
        Session["ETU_Guild01"] = "";
        Session["ETU_Guild02"] = "";
        Session["ETU_Guild03"] = "";

        switch (lType)
        {
            case "01":
                Session["UserType"] = lType;
                Session["ID"] = lID;
                Session["PW"] = lPw;
                _ReturnValue = true;
                break;
            case "02":
            case "08":
                ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection UserConn = new SqlConnection(connectionStringSwc.ConnectionString))
                {
                    UserConn.Open();

                    string strSQLRV = "select * from ETUsers ";
                    strSQLRV = strSQLRV + " where ETID COLLATE Latin1_General_CS_AI ='" + lID + "' ";
                    //strSQLRV = strSQLRV + "   and status = '已開通' ";
                    strSQLRV = strSQLRV + "   and ETStatus = '1' ";

                    SqlDataReader readerUser;
                    SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
                    readerUser = objCmdUSER.ExecuteReader();

                    if (readerUser.HasRows)
                    {
                        while (readerUser.Read())
                        {
                            SQL_PW = readerUser["ETPW"] + "";
                            SQL_NAME = readerUser["ETName"] + "";
                            SQL_GuildSubstitute = readerUser["GuildSubstitute"] + "";
                            SQL_GuildSubstitute2 = readerUser["GuildSubstitute2"] + "";
                            SQL_GuildTcgeChk = readerUser["GuildTcgeChk"] + "";
                            SQL_GuildTcgeChk2 = readerUser["GuildTcgeChk2"] + "";
                            SQL_jobTitle = readerUser["ETIdentity"] + "";
                            SQL_ServiceSubstitute = readerUser["ServiceSubstitute"] + "";
                        
							//if (lPw == SQL_PW) 使用台北卡認證
                        
                            Session["UserType"] = lType;
                            Session["ID"] = lID;
                            Session["PW"] = SQL_PW;
                            Session["NAME"] = SQL_NAME;
                            Session["Unit"] = SQL_Unit;
                            Session["JobTitle"] = SQL_jobTitle;

                            //ge-50702	水土保持服務團
                            if (SQL_GuildTcgeChk == "1") Session["ETU_Guild01"] = SQL_GuildSubstitute; //審查公會子帳號
                            if (SQL_GuildTcgeChk2 == "1") Session["ETU_Guild02"] = SQL_GuildSubstitute2;//檢查公會子帳號
                            if (SQL_ServiceSubstitute == "Y") Session["ETU_Guild03"] = "ge-50702";

                            _ReturnValue = true;
                        }
                    }
                }
                break;
            case "03":
            case "04":
            case "09":
				ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
				using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
				{
					UserConn.Open();
	
					string strSQLRV = "select * from geouser ";
					strSQLRV = strSQLRV + " where (userid='" + lID + "' or IDNO = '" + lID + "'  or OID = '" + lID + "') ";
					strSQLRV = strSQLRV + "   and status = '正常' ";
					if (lType == "03") //&& lID != "gv-admin")
					{
						if(lID != "gv-admin"){
							strSQLRV = strSQLRV + "   and unit ='工務局大地工程處' ";
							strSQLRV = strSQLRV + "   and (tcgedataview like '%書件水土保持案件;%' or TcgeDataedit like '%書件水土保持案件;%') ";
						}
					}
					if ((lType == "04" || lType == "09") && lID != "gv-admin")
					{
						strSQLRV = strSQLRV + "   and unit ='技師公會' ";
					}
	
					SqlDataReader readerUser;
					SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
					readerUser = objCmdUSER.ExecuteReader();
	
					if (readerUser.HasRows)
					{
						while (readerUser.Read())
						{
							SQL_PW = readerUser["passwd"] + "";
							SQL_NAME = readerUser["name"] + "";
							SQL_Unit = readerUser["unit"] + "";
							SQL_EDIT = readerUser["TcgeDataedit"] + "";
							SQL_Guild01 = readerUser["Guild01"] + "";
							SQL_Guild02 = readerUser["Guild02"] + "";
							SQL_IDNO = readerUser["IDNO"] + "";
							//完工設施維護檢查表登錄
							
							SQL_USERID = readerUser["userid"] + "";
						}
	
						if (lPw == SQL_PW || lID==SQL_IDNO || 1==1)
						{
							Session["UserType"] = lType;
							//Session["ID"] = lID;
							Session["ID"] = SQL_USERID;
							Session["PW"] = SQL_PW;
							Session["NAME"] = SQL_NAME;
							Session["Unit"] = SQL_Unit;
							Session["Guild01"] = SQL_Guild01;
							Session["Guild02"] = SQL_Guild02;
							if (SQL_EDIT.IndexOf("完工設施維護檢查表登錄;") >= 0)
							{
								Session["Edit4"] = "Y";
							}
							//if (SQL_EDIT.IndexOf("書件水土保持案件;") >= 0 || lID == "gv-admin")
							if (SQL_EDIT.IndexOf("書件水土保持案件;") >= 0 || SQL_USERID == "gv-admin")
							{
								Session["ONLINEAPPLY"] = "Y";
							}
							//Session["ETU_Guild01"] = lID;
							//Session["ETU_Guild02"] = lID;
							//Session["ETU_Guild03"] = lID;
							Session["ETU_Guild01"] = SQL_USERID;
							Session["ETU_Guild02"] = SQL_USERID;
							Session["ETU_Guild03"] = SQL_USERID;
							_ReturnValue = true;
	
						}
						else
						{
							Session["UserType"] = "";
							Session["ID"] = "";
							Session["PW"] = "";
							Session["NAME"] = "";
							Session["Unit"] = "";
							Session["Edit4"] = "";
							Session["Guild01"] = "";
							Session["Guild02"] = "";
							Session["ETU_Guild01"] = "";
							Session["ETU_Guild02"] = "";
							Session["ETU_Guild03"] = "";
						}
					}
				}
				if(lID=="") _ReturnValue = false;
				break;
			case "05":
				ConnectionStringSettings connectionStringO = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection UserConn = new SqlConnection(connectionStringO.ConnectionString))
                {
                    UserConn.Open();
			
                    string strSQLRV = "select * from Organ ";
                    strSQLRV = strSQLRV + " where AccountNo = '" + lID +"' ";
					strSQLRV = strSQLRV + " and status = '已開通' ";
			
                    SqlDataReader readerUser;
                    SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
                    readerUser = objCmdUSER.ExecuteReader();
			
                    if (readerUser.HasRows)
                    {
                        while (readerUser.Read())
                        {
                            SQL_PW = "";
                            SQL_NAME = readerUser["Name"] + "";
                            SQL_Unit = readerUser["UnitName"] + "";
                            SQL_IDNO = readerUser["IDNo"] + "";
                            //完工設施維護檢查表登錄
                        }
			
                        if (lPw == SQL_PW || lID==SQL_IDNO || 1==1)
                        {
                            Session["UserType"] = lType;
                            Session["ID"] = lID;
                            Session["PW"] = SQL_PW;
                            Session["NAME"] = SQL_NAME;
                            Session["Unit"] = SQL_Unit;
							Session["IDNO"] = SQL_IDNO;
                            _ReturnValue = true;
			
                        }
                        else
                        {
                            Session["UserType"] = "";
                            Session["ID"] = "";
                            Session["PW"] = "";
                            Session["NAME"] = "";
                            Session["Unit"] = "";
                            Session["Edit4"] = "";
                            Session["Guild01"] = "";
                            Session["Guild02"] = "";
                            Session["ETU_Guild01"] = "";
                            Session["ETU_Guild02"] = "";
                            Session["ETU_Guild03"] = "";
							Session["IDNO"] = "";
                        }
                    }
                }
                break;
			case "06":
				ConnectionStringSettings connectionStringA = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection UserConn = new SqlConnection(connectionStringA.ConnectionString))
                {
                    UserConn.Open();
			
                    string strSQLRV = "select * from Architect ";
                    strSQLRV = strSQLRV + " where 帳號 = '" + lID +"' ";
					strSQLRV = strSQLRV + " and 狀態 = '正常' ";
			
                    SqlDataReader readerUser;
                    SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
                    readerUser = objCmdUSER.ExecuteReader();
			
                    if (readerUser.HasRows)
                    {
                        while (readerUser.Read())
                        {
                            SQL_PW = "";
                            SQL_NAME = readerUser["姓名"] + "";
                            SQL_Unit = readerUser["事務所名稱"] + "";
                            SQL_IDNO = readerUser["密碼"] + "";
                            //完工設施維護檢查表登錄
                        }
			
                        if (lPw == SQL_PW || lID==SQL_IDNO || 1==1)
                        {
                            Session["UserType"] = lType;
                            Session["ID"] = lID;
                            Session["PW"] = SQL_PW;
                            Session["NAME"] = SQL_NAME;
                            Session["Unit"] = SQL_Unit;
							Session["IDNO"] = SQL_IDNO;
                            _ReturnValue = true;
			
                        }
                        else
                        {
                            Session["UserType"] = "";
                            Session["ID"] = "";
                            Session["PW"] = "";
                            Session["NAME"] = "";
                            Session["Unit"] = "";
                            Session["Edit4"] = "";
                            Session["Guild01"] = "";
                            Session["Guild02"] = "";
                            Session["ETU_Guild01"] = "";
                            Session["ETU_Guild02"] = "";
                            Session["ETU_Guild03"] = "";
							Session["IDNO"] = "";
                        }
                    }
                }
                break;
			case "07":
                Session["UserType"] = lType;
                Session["ID"] = lID;
                Session["PW"] = lPw;
                _ReturnValue = true;
                break;
        }

        string tempLS = "0";
        if (_ReturnValue) { tempLS = "1"; }
        LoginNotes(lID, lType, tempLS);

        return _ReturnValue;
    }
    public void LoginNotes(string UserID, string UserType, string LoginStatus)
    {
        string clientIP = GetClientIP();
        string strSQLErr = "";

        LoginStatus = LoginStatus + "";
        if (LoginStatus != "0") { LoginStatus = "1"; }

        //從資料庫取得資料
        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        //strSQLErr = " update geouser set loginstaus = '" + LoginStatus + "' where userid='" + UserID + "';";
        strSQLErr = strSQLErr + " insert into loginnotes (userid,usertype,success,logtime,loginip) values ";
        strSQLErr = strSQLErr + " ('" + UserID + "','" + UserType + "','" + LoginStatus + "',getdate(),'" + clientIP + "');";

        SqlCommand objCmdRV = new SqlCommand(strSQLErr, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();
    }
    #endregion
    #region 使用姓名取GeoUser之資料
    public string GetGeoUserID(string tUserName, string tFiled)
    {
        string rValue = "";
        string sqlStr = " select * from GeoUser Where [name]=@USERNAME; ";
        #region
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@USERNAME", tUserName));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                            try { rValue = readerTslm[tFiled] + ""; } catch { }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion
	#region 使用姓名取GeoUser之資料-(使用中)
    public string GetGeoUserID_Use(string tUserName, string tFiled)
    {
        string rValue = "";
        string sqlStr = " select * from GeoUser Where [name]=@USERNAME and [status]='正常'; ";
        #region
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@USERNAME", tUserName));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                            try { rValue = readerTslm[tFiled] + ""; } catch { }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion
    #region syslog.v2
    public void swcLogRC(string vPageAspx, string vPageDesc, string vPageType, string vPageCRUD, string vKeyID)
    {
        #region syslog v2
        string ssUserID = "";
        string tClientIP = GetClientIP();
        try { ssUserID = HttpContext.Current.Session["ID"] + ""; }
        catch { ssUserID = ""; }
        if (ssUserID == "") return;
        string ssUserName = "";
        try { ssUserName = HttpContext.Current.Session["NAME"] + ""; }
        catch { ssUserName = ""; }

        string iRecord = " INSERT INTO swcLog20 (pageAspx,pageDesc,pageType,pageCRUD,remark,UserIp,logUser,logTime,logName) VALUES ";
        iRecord += "(@pageAspx,@pageDesc,@pageType,@pageCRUD,@remark,@UserIp,@logUser,getdate(),@logName)";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = iRecord;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@pageAspx", vPageAspx));
                cmd.Parameters.Add(new SqlParameter("@pageDesc", vPageDesc));
                cmd.Parameters.Add(new SqlParameter("@pageType", vPageType));
                cmd.Parameters.Add(new SqlParameter("@pageCRUD", vPageCRUD));
                cmd.Parameters.Add(new SqlParameter("@remark", vKeyID));
                cmd.Parameters.Add(new SqlParameter("@UserIp", tClientIP));
                cmd.Parameters.Add(new SqlParameter("@logUser", ssUserID));
                cmd.Parameters.Add(new SqlParameter("@logName", ssUserName));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
    }

    #endregion
    #region getIP
    public string GetClientIP()
    {
        string ip;
        if (Context.Request.ServerVariables["HTTP_VIA"] != null)
            ip = Context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ""; // Return real client IP. 得到客户端地址
        else
            ip = Context.Request.ServerVariables["REMOTE_ADDR"]+""; //While it can't get the Client IP, it will return proxy IP. 得到服務端的地址
        return ip;
    }
    #endregion
    public bool swcDtlAuthority(string vSWC000,string vEtUserID, string vType)
    {
        //vType_審查:S3 檢查：S4
        #region 技師代公會是否指定
        bool rValue = false;
        string sqlStr = " select * from GuildGroup where swc000=@SWC000 and RGType=@RGType AND ETID=@ETID and CHGType='1'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", vSWC000));
                cmd.Parameters.Add(new SqlParameter("@RGType", vType));
                cmd.Parameters.Add(new SqlParameter("@ETID", vEtUserID));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if(readerTslm.HasRows)
                        rValue = true;
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #region CHIEF
    public void CHIEFRC(string vfileAspx, string vfileDesc, string vfileName, string vfilePath, string vfileUrl, string vfileAct)
    {
        #region CHIEF
        string iRecord = " INSERT INTO CHIEFFilesDLRC (fileAspx,fileDesc,fileName,filePath,fileUrl,fileAct,uploadTime) VALUES ";
        iRecord += "(@fileAspx,@fileDesc,@fileName,@filePath,@fileUrl,@fileAct,getdate())";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = iRecord;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@fileAspx", vfileAspx));
                cmd.Parameters.Add(new SqlParameter("@fileDesc", vfileDesc));
                cmd.Parameters.Add(new SqlParameter("@fileName", vfileName));
                cmd.Parameters.Add(new SqlParameter("@filePath", vfilePath));
                cmd.Parameters.Add(new SqlParameter("@fileUrl", vfileUrl));
                cmd.Parameters.Add(new SqlParameter("@fileAct", vfileAct));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
    }
    #endregion
    #region chengDaApi
    public string[] CadastralInfo(string vArea, string vSection, string vNum)
    {
        string[] rValue = { "", "", "", "", "", "", "", "", "", "", "" };
        string[] rValue07 = { "", "", "", "", "", "", "", "", "", "" };
        string tAreaCode = giveMeAreaCode(vArea, vSection);
        string tSectionCode = giveMeKCNTCode(vSection);
        //int teSt = vNum.IndexOf("-");
        //bool TEST = vNum.IndexOf("-") < 0;
        //String test = vNum.PadLeft(4, '0') + "0000";
        //String tesT = vNum.Substring(0, vNum.IndexOf("-")).PadLeft(4, '0') + vNum.Substring(vNum.IndexOf("-")+1).PadLeft(4, '0');
        string tNumCode = vNum.IndexOf("-") < 0 ? vNum.PadLeft(4, '0') + "0000" : vNum.Substring(0, vNum.IndexOf("-")).PadLeft(4, '0') + vNum.Substring(vNum.IndexOf("-") + 1).PadLeft(4, '0');

        #region V2PG
        string tempCountry_code = "A";
        //country_code：A-台北市
        string gAPIURL01 = "";
        
        #region 取得json資料
        WebClient wc = new WebClient();
        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        string s = wc.DownloadString(gAPIURL01);

        WebClient webClient = new WebClient();
        byte[] JsonStringR = webClient.DownloadData(gAPIURL01);

        string JsonString = Encoding.UTF8.GetString(JsonStringR);
        string NewJsonStr = JsonString;
        try
        {
            //Response.Write(JsonString);
            string toDes = JsonString.Replace(" (", "").Replace(")", "");
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(toDes)))
            {
                DataContractJsonSerializer deseralizer = new DataContractJsonSerializer(typeof(Rootobject));
                Rootobject model = (Rootobject)deseralizer.ReadObject(ms);// //反序列化ReadObject
                rValue[0] = model.山坡地範圍+"";
                rValue[1] = model.山坡地可利用限度宜林地 + "";
                rValue[2] = model.山坡地可利用限度宜農牧地 + "";
                rValue[3] = model.陽明山國家公園範圍+"";
                rValue[4] = model.保安林範圍 + "";
                rValue[5] = model.地質敏感區範圍+"";
                rValue[6] = model.林地範圍 + "";
                string QQ = "";
                rValue07 = model.土地使用分區;
                for (int i = 0; i < rValue07.Length; i++)
                {
                    if (rValue07.Length == 1) { QQ += rValue07[i].ToString(); }
                    else { QQ += rValue07[i].ToString() + ","; }
                }
                rValue[7] = QQ;
				rValue[8] = model.巡查區與資訊局圖台 + "";
				
				string JsonString1 = model.土地標示資料;
                string toDes1 = JsonString1.Replace(" (", "").Replace(")", "");
				using (var ms1 = new MemoryStream(Encoding.Unicode.GetBytes(toDes1)))
				{
					DataContractJsonSerializer deseralizer1 = new DataContractJsonSerializer(typeof(Rootobject3));
					Rootobject3 model1 = (Rootobject3)deseralizer1.ReadObject(ms1);// //反序列化ReadObject

					rValue[10] = (model1.AA10).ToString();
				}
                string tmpFile = "CD" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
                //rValue[9] = getChengDaImg(tAreaCode, tSectionCode, tNumCode, tmpFile);
            }
        }
        catch (Exception ex) { }
        #endregion
        #endregion

        return rValue;
    }
    private string giveMeAreaCode(string tmpArea, string tmpArea2)
    {
        string rValue = "";
        switch (tmpArea)
        {
            case "文山":
                rValue = "AA";
                break;
            case "萬華":
            case "大同":
                rValue = "AB";
                break;
            case "中正":
                if (tmpArea2.IndexOf("永昌") > -1 || tmpArea2.IndexOf("河堤") > -1 || tmpArea2.IndexOf("南海") > -1 || tmpArea2.IndexOf("福和") > -1)
                    rValue = "AA";
                if (tmpArea2.IndexOf("中正") > -1 || tmpArea2.IndexOf("公園") > -1 || tmpArea2.IndexOf("成功") > -1 || tmpArea2.IndexOf("城中") > -1 || tmpArea2.IndexOf("臨沂") > -1)
                    rValue = "AB";
                //AA: 永昌、河堤、南海、福和
                //AB: 中正、公園、成功、城中、臨沂
                break;
            case "中山":
            case "內湖":
                rValue = "AC";
                break;
            case "松山":
            case "南港":
            case "信義":
                rValue = "AD";
                break;
            case "士林":
            case "北投":
                rValue = "AE";
                break;
            case "大安":
                rValue = "AF";
                break;
        }
        return rValue;

        //地政司：2020-09-30 資料
        //臺北市,A,松山,AD,松山區,01
        //臺北市,A,大安,AF,大安區,02
        //臺北市,A,古亭,AA,中正區,03
        //臺北市,A,建成,AB,中正區,03
        //臺北市,A,建成,AB,萬華區,05
        //臺北市,A,建成,AB,大同區,09
        //臺北市,A,中山,AC,中山區,10
        //臺北市,A,古亭,AA,文山區,11
        //臺北市,A,松山,AD,南港區,13
        //臺北市,A,中山,AC,內湖區,14
        //臺北市,A,士林,AE,士林區,15
        //臺北市,A,士林,AE,北投區,16
        //臺北市,A,松山,AD,信義區,17

    }
    public class Rootobject
    {
        public string 山坡地範圍 { get; set; }
        public string 山坡地可利用限度宜林地 { get; set; }
        public string 山坡地可利用限度宜農牧地 { get; set; }
        public string 陽明山國家公園範圍 { get; set; }
        public string 保安林範圍 { get; set; }
        public string 地質敏感區範圍 { get; set; }
        public string 林地範圍 { get; set; }
        public string[] 土地使用分區 { get; set; }
        public string 土地標示資料 { get; set; }
        public string 巡查區與資訊局圖台 { get; set; }
    }
    public class Rootobject2
    {
        public string img_base64 { get; set; }
    }
    public class Rootobject3
    {
        public decimal AA10 { get; set; }
    }
    private string giveMeKCNTCode(string tmpKCNT)
    {
        string rValue = "";
        string sqlStr = " select top 1 * from LAND where KCNT=@KCNT order by THEKEY desc; ";
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection tslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            tslmConn.Open();
            using (var cmd = tslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@KCNT", tmpKCNT));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readereTslm = cmd.ExecuteReader())
                {
                    if (readereTslm.HasRows)
                        while (readereTslm.Read())
                            rValue = readereTslm["AA48"] + "";
                    readereTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }

    public string[,] getSwccaseRelationMan(string gSWC000)
    {
        string[,] rValue = new string[,] { { "", "","","" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" }, { "", "", "", "" } };
        int nI = 0;
        string sqlStr = " select G.ETID,E.ETName,E.ETEmail,G.RGType from GuildGroup G LEFT JOIN ETUsers E ON G.ETID=E.ETID where G.SWC000=@SWC000 order by G.id ";
        #region
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection tslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            tslmConn.Open();
            using (var cmd = tslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", gSWC000));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readereTslm = cmd.ExecuteReader())
                {
                    if (readereTslm.HasRows)
                        while (readereTslm.Read())
                        {
                            rValue[nI, 0] = readereTslm["ETID"] + "";
                            rValue[nI, 1] = readereTslm["ETName"] + "";
                            rValue[nI, 2] = readereTslm["ETEmail"] + "";
                            rValue[nI, 3] = readereTslm["RGType"] + "";
                            nI+=1;
                        }
                    readereTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion

        return rValue;
    }
    #endregion
	#region 使用姓名取Organ之資料-(使用中)
    public string GetOrganID(string tUserName, string tFiled)
    {
        string rValue = "";
        string sqlStr = " select * from Organ Where [Name]=@USERNAME and [status]='已開通'; ";
        #region
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@USERNAME", tUserName));
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            try { rValue = reader[tFiled] + ""; } catch { }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion
	#region 使用IDNO取Organ之資料-(使用中)
    public string GetOrganData(string tId, string tFiled)
    {
        string rValue = "";
        string sqlStr = " select * from Organ Where IDNo=@idno and [status]='已開通'; ";
        #region
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@idno", tId));
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            try { rValue = reader[tFiled] + ""; } catch { }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion
	#region 使用ACCTNO取Organ之資料-(使用中)
    public string GetOrganData2(string tAcctNo, string tFiled)
    {
        string rValue = "";
        string sqlStr = " select * from Organ Where AccountNo=@accountno ; ";
        #region
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@accountno", tAcctNo));
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            try { rValue = reader[tFiled] + ""; } catch { }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion
	#region 使用身分證取Architect之資料-(使用中)
    public string GetArchitectData(string tIDNO, string tFiled)
    {
        string rValue = "";
        string sqlStr = " select * from Architect Where 密碼=@IDNO and 狀態='正常'; ";
        #region
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@IDNO", tIDNO));
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            try { rValue = reader[tFiled] + ""; } catch { }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion
}