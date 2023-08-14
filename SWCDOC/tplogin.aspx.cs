using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_tplogin : System.Web.UI.Page
{

    //大地 正式機的認證參數--開始
    public string accesstokenUrltaipei = "";
    public string accesstokenUrl = "";
	
    public string accesstokenUrltaipei_querystring = "";
	public string accesstokenUrl_querystring = "";
	
	public string getprovatedateURLtaipei = "";
    public string getprovatedateURL = "";
	
	public string codestring = "";
    public string access_token_string = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();
		codestring = "";
        codestring = Request.QueryString["code"];

        string taipeiUserMsg = "";
        if (codestring != "")
        {
			access_token_string = "";
            access_token_string = GetTaipeiToken();
            if (access_token_string == "") { } else {
                #region 通過認證
                taipeiUserMsg = GetPrivate();
                string[] aUserMsg = taipeiUserMsg.Split(new string[] { ";" }, StringSplitOptions.None);
                if (aUserMsg[10] == "0" && aUserMsg[3].Trim() != "0963909388")
                {
                    Response.Write("<script>alert('您好，本系統僅提供金質會員使用，請至台北通升級為金質會員，再重新登入。'); location.href='../Default.aspx'; </script>"); return;
                }
                else
                    updUserDate(taipeiUserMsg);
                #endregion
            }
        }
    }

    private void updUserDate(string taipeiUserMsg)
    {
        Class20 C20 = new Class20();
        string tkID = Session["TPTOKEN"] + "";
        HttpCookie cookie = Request.Cookies["LogInUtype"];
        HttpCookie cookie1 = Request.Cookies["PWA"];
        string ssLoginMsg = Server.UrlDecode(cookie.Value);
        string ssPWA = Server.UrlDecode(cookie1.Value);
        string[] aUserMsg = taipeiUserMsg.Split(new string[] { ";" }, StringSplitOptions.None);

        //Response.Write("okok---<br/>");
        //Response.Write(aUserMsg[0] + "<br/>1.");
        //Response.Write(aUserMsg[1] + "<br/>2.");
        //Response.Write(aUserMsg[2]+ "<br/>3.");
        //Response.Write(aUserMsg[3] + "<br/>4.");
        //Response.Write(aUserMsg[4] + "<br/>10.");
        //Response.Write(aUserMsg[10] + "<br/>11.");
        //Response.Write(ssLoginMsg + "<br/>");
        //Response.Write(taipeiUserMsg);
        //Response.Write("okok+++");
		Session["IDNO"] = aUserMsg[4];
		
        //測試用
        if (aUserMsg[3].Trim() == "0963909388")
            aUserMsg[4] = "L223511115";

        Boolean LoginR = false;
        switch (ssLoginMsg) {
            case "水土保持義務人":
                LoginR = C20.GetLoginStatus(aUserMsg[4], tkID, "01");
                Session["NAME"] = aUserMsg[2];
				Session["UserType"] = "01";
                Session["ID"] = aUserMsg[4];
                //Session["PW"] = 手機;
                Session["PW"] = aUserMsg[5];

                #region 更新基本資料
                string sqlStr1 = " select * from tcgeswc.dbo.UserInfo Where [UI000]=@UI000; ";
                #region
                string sqlStr2 = "";
                ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
                {
                    TslmConn.Open();
                    using (var cmd = TslmConn.CreateCommand())
                    {
                        cmd.CommandText = sqlStr1;
                        cmd.Parameters.Add(new SqlParameter("@UI000", aUserMsg[4]));
                        cmd.ExecuteNonQuery();

                        using (SqlDataReader readerTslm = cmd.ExecuteReader())
                        {
                            if (!readerTslm.HasRows) {
                                sqlStr2 += " INSERT INTO tcgeswc.dbo.UserInfo (UI000) VALUES (@UI000); ";
                            }
                            readerTslm.Close();
                        }
                        cmd.Cancel();
                    }
                }
                sqlStr2 += " Update tcgeswc.dbo.UserInfo set UI001=@UI001,UI002=@UI002,UI003=@UI003,UI004=@UI004,UI005=@UI005,UPDDATE=GETDATE() Where [UI000]=@UI000; ";
                //Response.Write("<br/>"+ sqlStr2+ "<br/>"+ aUserMsg[4] + "<br/>"+ aUserMsg[3]+ "<br/>");
                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
                {
                    dbConn.Open();
                    using (var cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = sqlStr2;
                        #region.設定值
                        cmd.Parameters.Add(new SqlParameter("@UI000", aUserMsg[4]));
                        cmd.Parameters.Add(new SqlParameter("@UI001", aUserMsg[2]));
                        cmd.Parameters.Add(new SqlParameter("@UI002", aUserMsg[4]));
                        cmd.Parameters.Add(new SqlParameter("@UI003", aUserMsg[5]));
                        cmd.Parameters.Add(new SqlParameter("@UI004", aUserMsg[6]));
                        cmd.Parameters.Add(new SqlParameter("@UI005", aUserMsg[7]));
                        #endregion
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                #endregion
                #endregion
				Response.Redirect("~/PriPage/SwcPrivacy_01.aspx");
                break;
            case "技師/各類委員":
				LoginR = C20.GetLoginStatus(aUserMsg[4], tkID, "02");
				if (!LoginR)
				{
					Session["NUIDNO"] = aUserMsg[4];
					Session["NUNAME"] = aUserMsg[2];
					Session["NUCELL"] = aUserMsg[5];
					Session["NUMAIL"] = aUserMsg[6];
					Response.Redirect("SWCBase001.aspx?ETID=NEW");
				}
                break;
            case "審查/檢查單位":
				LoginR = C20.GetLoginStatus(aUserMsg[4], tkID, "04");
				//台北卡什麼資料能對到帳號取得id，目前不確定。
				if (!LoginR) { Response.Write("<script>alert('您好，使用者帳號登入權限尚未開通，請洽系統管理員。'); location.href='../Default.aspx'; </script>"); return; }
                break;
            case "工務局大地工程處":
				string geoUserId = C20.GetGeoUserID_Use(aUserMsg[2], "userid");
				LoginR = C20.GetLoginStatus(geoUserId, tkID, "03");
				if (!LoginR) {Response.Write("<script>alert('您好，使用者帳號登入權限尚未開通，請洽系統管理員。'); location.href='../Default.aspx'; </script>"); return; }
				break;
			case "其他機關":
				string OrgUserId = C20.GetOrganData(aUserMsg[4], "AccountNo");
				LoginR = C20.GetLoginStatus(OrgUserId, aUserMsg[5], "05");
				if (!LoginR) {
					Response.Redirect("SWCBase002.aspx?ETID=NEW&NAME="+aUserMsg[2]+"&CELLPHONE="+aUserMsg[5]+"&IDNO="+aUserMsg[4]+"&EMAIL="+aUserMsg[6]);
				}
				break;
			case "建築師":
				string ArchitectUserId = C20.GetArchitectData(aUserMsg[4], "帳號");
				LoginR = C20.GetLoginStatus(ArchitectUserId, tkID, "06");
				if (!LoginR) {
					Response.Write("<script>alert('您好，由於系統尚未比對到您所登錄之帳號資訊，請先洽詢台北市建築師公會洽詢協助申請開通相關功能，或可電洽大地工程處審查管理科02-27593001#3729詢問相關使用疑問。'); location.href='../Default.aspx'; </script>");
				}
				break;
			case "營造單位/工地負責人":
				//身分證 統編
				LoginR = C20.GetLoginStatus(aUserMsg[4], tkID, "07");
                Session["NAME"] = aUserMsg[2];
				Session["UserType"] = "07";
                Session["ID"] = aUserMsg[4];
                Session["PW"] = aUserMsg[5];
				
                #region 更新基本資料
                string sqlStr3 = " select * from tcgeswc.dbo.ConstructUnit Where [UI000]=@UI000; ";
                string sqlStr4 = "";
                ConnectionStringSettings connectionStringTslm1 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm1.ConnectionString))
                {
                    TslmConn.Open();
                    using (var cmd = TslmConn.CreateCommand())
                    {
                        cmd.CommandText = sqlStr3;
                        cmd.Parameters.Add(new SqlParameter("@UI000", aUserMsg[4]));
                        cmd.ExecuteNonQuery();
				
                        using (SqlDataReader readerTslm = cmd.ExecuteReader())
                        {
                            if (!readerTslm.HasRows) {
                                sqlStr4 += " INSERT INTO tcgeswc.dbo.ConstructUnit (UI000,SaveDate) VALUES (@UI000,GETDATE()); ";
                            }
                            readerTslm.Close();
                        }
                        cmd.Cancel();
                    }
                }
                sqlStr4 += " Update tcgeswc.dbo.ConstructUnit set UI001=@UI001,UI002=@UI002,UI003=@UI003,UI004=@UI004,UI005=@UI005,UpdDate=GETDATE() Where [UI000]=@UI000; ";
                //Response.Write("<br/>"+ sqlStr4+ "<br/>"+ aUserMsg[4] + "<br/>"+ aUserMsg[3]+ "<br/>");
                ConnectionStringSettings connectionString1 = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
                using (SqlConnection dbConn = new SqlConnection(connectionString1.ConnectionString))
                {
                    dbConn.Open();
                    using (var cmd = dbConn.CreateCommand())
                    {
                        cmd.CommandText = sqlStr4;
                        #region.設定值
                        cmd.Parameters.Add(new SqlParameter("@UI000", aUserMsg[4]));
                        cmd.Parameters.Add(new SqlParameter("@UI001", aUserMsg[2]));
                        cmd.Parameters.Add(new SqlParameter("@UI002", aUserMsg[4]));
                        cmd.Parameters.Add(new SqlParameter("@UI003", aUserMsg[5]));
                        cmd.Parameters.Add(new SqlParameter("@UI004", aUserMsg[6]));
                        cmd.Parameters.Add(new SqlParameter("@UI005", aUserMsg[7]));
                        #endregion
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                #endregion
                Response.Redirect("~/PriPage/SwcPrivacy_01.aspx");
                break;
			case "代理技師":
				//改抓代理人身分證字號
				LoginR = C20.GetLoginStatus(Get02ID(aUserMsg[4]), tkID, "08");
                break;
			case "代理公會":
				//改抓公會統一編號(唯一識別碼)
				LoginR = C20.GetLoginStatus(Get04ID(aUserMsg[4]), tkID, "09");
                break;
        }
		if(LoginR == true){
			C20.swcLogRC("tplogin", "登入", ssLoginMsg, "成功", aUserMsg[4]);
			if(ssPWA == "1" && ssLoginMsg == "技師/各類委員")
				Response.Redirect("PWA_list1.aspx");
			else if(ssPWA == "2" && ssLoginMsg == "工務局大地工程處")
				Response.Redirect("PWA_list2.aspx");
			else
				Response.Redirect("HaloPage001.aspx");
		}
    }

    private string GetPrivate()
    {
        string rValue = "";
        string taipeicardloginreturn = "";
		
		if (codestring.Length == 6)
		{
			getprovatedateURLtaipei = getprovatedateURLtaipei + access_token_string;
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(getprovatedateURLtaipei);
			req.Method = "GET";
	
			try
			{
				WebResponse wr = req.GetResponse();
				//Stream dataStream = wr.GetRequestStream();
				using (Stream dataStream = wr.GetResponseStream())
				{
					// Open the stream using a StreamReader for easy access.
					StreamReader reader = new StreamReader(dataStream);
					// Read the content.
					string responseFromServer = reader.ReadToEnd();
					taipeicardloginreturn = responseFromServer;
					// Display the content.
					Console.WriteLine(responseFromServer);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳個資 = " + responseFromServer;
				}
				// Close the response.
				wr.Close();
			}
			catch (Exception)
			{
				taipeicardloginreturn = "";
				//taipeitoken.Text = taipeitoken.Text + "\n" + "取得個資的伺服器回應錯誤";
				//throw;
			}
	
			if (taipeicardloginreturn == "")
			{
				taipeicardloginreturn = "";
			}
			else
			{
				//開始處理 jason 字串
				var jo = JObject.Parse(taipeicardloginreturn);
	
				string sqlStr = " INSERT INTO [TPTLogin] ([tkid],[account],[username],[realName],[idNo],[email],[phoneNo],[birthday],[bmemberType],[verifyLevel],[residentAddress],[citizen],[nativePeople],[cityInternetUid],[datatime]) ";
				sqlStr += " VALUES (@tkid,@account,@username,@realName,@idNo,@email,@phoneNo,@birthday,@bmemberType,@verifyLevel,@residentAddress,@citizen,@nativePeople,@cityInternetUid,getdate()); ";
				#region 
				ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
				using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
				{
					dbConn.Open();
					using (var cmd = dbConn.CreateCommand())
					{
						cmd.CommandText = sqlStr;
						#region.設定值
						cmd.Parameters.Add(new SqlParameter("@tkid", Convert.ToString(jo["id"])));
						cmd.Parameters.Add(new SqlParameter("@account", Convert.ToString(jo["account"])));
						cmd.Parameters.Add(new SqlParameter("@username", Convert.ToString(jo["username"])));
						cmd.Parameters.Add(new SqlParameter("@realName", Convert.ToString(jo["realName"])));
						cmd.Parameters.Add(new SqlParameter("@idNo", Convert.ToString(jo["idNo"])));
						cmd.Parameters.Add(new SqlParameter("@email", Convert.ToString(jo["email"])));
						cmd.Parameters.Add(new SqlParameter("@phoneNo", Convert.ToString(jo["phoneNo"])));
						cmd.Parameters.Add(new SqlParameter("@birthday", Convert.ToString(jo["birthday"])));
						cmd.Parameters.Add(new SqlParameter("@bmemberType", Convert.ToString(jo["bmemberType"])));
						cmd.Parameters.Add(new SqlParameter("@verifyLevel", Convert.ToString(jo["verifyLevel"])));
						cmd.Parameters.Add(new SqlParameter("@residentAddress", Convert.ToString(jo["residentAddress"])));
						cmd.Parameters.Add(new SqlParameter("@citizen", Convert.ToString(jo["citizen"])));
						cmd.Parameters.Add(new SqlParameter("@nativePeople", Convert.ToString(jo["nativePeople"])));
						cmd.Parameters.Add(new SqlParameter("@cityInternetUid", Convert.ToString(jo["cityInternetUid"])));
						#endregion
						cmd.ExecuteNonQuery();
						cmd.Cancel();
					}
				}
				#endregion
				
				
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳id = " + Convert.ToString(jo["id"]);
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳username = " + Convert.ToString(jo["username"]);
				////真實姓名
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳realName = " + Convert.ToString(jo["realName"]);
				////身分證
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳idNo = " + Convert.ToString(jo["idNo"]);
				////taipeitoken.Text = taipeitoken.Text + "\n" + "回傳birthday = " + Convert.ToString(jo["birthday"]);
				////住址
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳addresses = " + Convert.ToString(jo["addresses"]);
				////真實住址
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳residentAddress = " + Convert.ToString(jo["residentAddress"]);
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳phoneNo = " + Convert.ToString(jo["phoneNo"]);
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳email = " + Convert.ToString(jo["email"]);
				////帳號類型(個人/組織/外國人/員工)
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳memberType = " + Convert.ToString(jo["memberType"]);
				////驗證是否金質會員(一般=0,金質>0)
				//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳verifyLevel = " + Convert.ToString(jo["verifyLevel"]);
	
	
				rValue += Convert.ToString(jo["id"]) + ";";
				rValue += Convert.ToString(jo["realName"]) + ";";
				rValue += Convert.ToString(jo["username"]) + ";";
				rValue += Convert.ToString(jo["account"]) + ";";
				rValue += Convert.ToString(jo["idNo"]) + ";";
				rValue += Convert.ToString(jo["phoneNo"]) + ";";
				rValue += Convert.ToString(jo["email"]) + ";";
				rValue += Convert.ToString(jo["addresses"]) + ";";
				rValue += Convert.ToString(jo["residentAddress"]) + ";";
				rValue += Convert.ToString(jo["memberType"]) + ";";
				rValue += Convert.ToString(jo["verifyLevel"]) + ";";
	
				Session["UID"] = Convert.ToString(jo["id"]) + ";";
	
				//taipeicardloginreturn = Convert.ToString(jo["username"]);
				//{"id":"a8cf414b-db89-44a0-bd7b-2054b4368174","username":"簡明正","birthday":"0001/01/01","addresses":[{"addressType":"","address":""}],"account":"0905236989","memberType":"personal","verifyLevel":"0","idNo":"","idNoUnverified":"","email":"","residentAddress":"","realName":"","phoneNo":"0905236989","citizen":false,"nativePeople":false}
			}
	
			//return taipeicardloginreturn;
			return rValue;
		}
		else
		{
			getprovatedateURL = getprovatedateURL;
			HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(getprovatedateURL);
			req.Method = "GET";
			req.Headers.Add("Authorization", "Bearer " + access_token_string);
	
			try
			{
				WebResponse wr = req.GetResponse();
				//Stream dataStream = wr.GetRequestStream();
				using (Stream dataStream = wr.GetResponseStream())
				{
					// Open the stream using a StreamReader for easy access.
					StreamReader reader = new StreamReader(dataStream);
					// Read the content.
					string responseFromServer = reader.ReadToEnd();
					taipeicardloginreturn = responseFromServer;
					// Display the content.
					Console.WriteLine(responseFromServer);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳個資 = " + responseFromServer;
				}
				// Close the response.
				wr.Close();
			}
			catch (Exception)
			{
				taipeicardloginreturn = "";
				//taipeitoken.Text = taipeitoken.Text + "\n" + "取得個資的伺服器回應錯誤";
				//throw;
			}
			
			if (taipeicardloginreturn == "")
			{
				taipeicardloginreturn = "";
			}
			else
			{
				//開始處理 jason 字串
				var jo = JObject.Parse(taipeicardloginreturn);
				if(Convert.ToString(jo["status"]) == "0")
                {
					jo = JObject.Parse(Convert.ToString(jo["data"]));
					string sqlStr = " INSERT INTO [TPTLogin] ([tkid],[account],[username],[realName],[idNo],[email],[phoneNo],[birthday],[bmemberType],[verifyLevel],[residentAddress],[citizen],[nativePeople],[cityInternetUid],[datatime]) ";
					sqlStr += " VALUES (@tkid,@account,@username,@realName,@idNo,@email,@phoneNo,@birthday,@bmemberType,@verifyLevel,@residentAddress,@citizen,@nativePeople,@cityInternetUid,getdate()); ";
					#region 
					ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
					using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
					{
						dbConn.Open();
						using (var cmd = dbConn.CreateCommand())
						{
							cmd.CommandText = sqlStr;
							#region.設定值
							cmd.Parameters.Add(new SqlParameter("@tkid", Convert.ToString(jo["id"])));
							cmd.Parameters.Add(new SqlParameter("@account", Convert.ToString(jo["account"])));
							cmd.Parameters.Add(new SqlParameter("@username", Convert.ToString(jo["username"])));
							cmd.Parameters.Add(new SqlParameter("@realName", Convert.ToString(jo["realName"])));
							cmd.Parameters.Add(new SqlParameter("@idNo", Convert.ToString(jo["idNo"])));
							cmd.Parameters.Add(new SqlParameter("@email", Convert.ToString(jo["email"])));
							cmd.Parameters.Add(new SqlParameter("@phoneNo", Convert.ToString(jo["phoneNo"])));
							cmd.Parameters.Add(new SqlParameter("@birthday", Convert.ToString(jo["birthday"])));
							cmd.Parameters.Add(new SqlParameter("@bmemberType", Convert.ToString(jo["bmemberType"])));
							cmd.Parameters.Add(new SqlParameter("@verifyLevel", Convert.ToString(jo["verifyLevel"])));
							cmd.Parameters.Add(new SqlParameter("@residentAddress", Convert.ToString(jo["residentAddress"])));
							cmd.Parameters.Add(new SqlParameter("@citizen", Convert.ToString(jo["citizen"])));
							cmd.Parameters.Add(new SqlParameter("@nativePeople", Convert.ToString(jo["nativePeople"])));
							cmd.Parameters.Add(new SqlParameter("@cityInternetUid", Convert.ToString(jo["cityInternetUid"])));
							#endregion
							cmd.ExecuteNonQuery();
							cmd.Cancel();
						}
					}
					#endregion
					
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳id = " + Convert.ToString(jo["id"]);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳username = " + Convert.ToString(jo["username"]);
					////真實姓名
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳realName = " + Convert.ToString(jo["realName"]);
					////身分證
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳idNo = " + Convert.ToString(jo["idNo"]);
					////taipeitoken.Text = taipeitoken.Text + "\n" + "回傳birthday = " + Convert.ToString(jo["birthday"]);
					////住址
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳addresses = " + Convert.ToString(jo["addresses"]);
					////真實住址
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳residentAddress = " + Convert.ToString(jo["residentAddress"]);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳phoneNo = " + Convert.ToString(jo["phoneNo"]);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳email = " + Convert.ToString(jo["email"]);
					////帳號類型(個人/組織/外國人/員工)
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳memberType = " + Convert.ToString(jo["memberType"]);
					////驗證是否金質會員(一般=0,金質>0)
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳verifyLevel = " + Convert.ToString(jo["verifyLevel"]);
		
		
					rValue += Convert.ToString(jo["id"]) + ";";
					rValue += Convert.ToString(jo["realName"]) + ";";
					rValue += Convert.ToString(jo["username"]) + ";";
					rValue += Convert.ToString(jo["account"]) + ";";
					rValue += Convert.ToString(jo["idNo"]) + ";";
					rValue += Convert.ToString(jo["phoneNo"]) + ";";
					rValue += Convert.ToString(jo["email"]) + ";";
					rValue += Convert.ToString(jo["addresses"]) + ";";
					rValue += Convert.ToString(jo["residentAddress"]) + ";";
					rValue += Convert.ToString(jo["memberType"]) + ";";
					rValue += Convert.ToString(jo["verifyLevel"]) + ";";
		
					Session["UID"] = Convert.ToString(jo["id"]) + ";";
		
					//taipeicardloginreturn = Convert.ToString(jo["username"]);
					//{"id":"a8cf414b-db89-44a0-bd7b-2054b4368174","username":"簡明正","birthday":"0001/01/01","addresses":[{"addressType":"","address":""}],"account":"0905236989","memberType":"personal","verifyLevel":"0","idNo":"","idNoUnverified":"","email":"","residentAddress":"","realName":"","phoneNo":"0905236989","citizen":false,"nativePeople":false}
				}
			}
	
			//return taipeicardloginreturn;
			return rValue;
		}
    }
	//舊的台北通
	/*
    private string GetTaipeiToken(string TaipeiCode)
    {
        string posttokenstring = "";
        ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        HttpWebRequest request = HttpWebRequest.Create(accesstokenUrl) as HttpWebRequest;
        request.Method = "POST";

        //進行POST資料的字元處理 
        //建立 URL 的 post 的送出去的參數  
        string querystring = accesstokenUrl_querystring + TaipeiCode;
        byte[] byteArray = Encoding.UTF8.GetBytes(querystring);
        //設定ContentType property of the WebRequest.  
        request.ContentType = "application/x-www-form-urlencoded";
        //設定ContentLength property of the WebRequest.  
        request.ContentLength = byteArray.Length;
        // Get the request stream.  
        Stream dataStream = request.GetRequestStream();
        // Write the data to the request stream.  
        dataStream.Write(byteArray, 0, byteArray.Length);
        // Close the Stream object.  
        dataStream.Close();

        try
        {
            //取得回傳資料  
            WebResponse response = request.GetResponse();
            // The using block ensures the stream is automatically closed.
            using (dataStream = response.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                posttokenstring = responseFromServer;
                // Display the content.  
                Console.WriteLine(responseFromServer);
                //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳存取碼 = " + responseFromServer;
            }
            // Close the response.  
            response.Close();
        }
        catch (Exception)
        {
            posttokenstring = "";
            //taipeitoken.Text = taipeitoken.Text + "\n" + "取得存取碼的伺服器回應錯誤";
            //throw;
        }

        if (posttokenstring == "")
        {
            posttokenstring = "";
        }
        else
        {
            //開始處理 jason 字串
            var jo = JObject.Parse(posttokenstring);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳access_token = " + Convert.ToString(jo["access_token"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳token_type = " + Convert.ToString(jo["token_type"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳refresh_token = " + Convert.ToString(jo["refresh_token"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳expires_in = " + Convert.ToString(jo["expires_in"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳scope = " + Convert.ToString(jo["scope"]);
            posttokenstring = Convert.ToString(jo["access_token"]);
        }
        Session["TPTOKEN"] = posttokenstring;
        return posttokenstring;
    }
	*/
	//新的台北通+ISSO
    private string GetTaipeiToken()
    {
        string posttokenstring = "";
		ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        
		if (codestring.Length == 6)
		{
			HttpWebRequest request = HttpWebRequest.Create(accesstokenUrltaipei) as HttpWebRequest;
			request.Method = "POST";
			
			//進行POST資料的字元處理
			//建立 URL 的 post 的送出去的參數
			string querystring = accesstokenUrltaipei_querystring + codestring;
			byte[] byteArray = Encoding.UTF8.GetBytes(querystring);
			//設定ContentType property of the WebRequest.
			request.ContentType = "application/x-www-form-urlencoded";
			//設定ContentLength property of the WebRequest.
			request.ContentLength = byteArray.Length;
			// Get the request stream.
			Stream dataStream = request.GetRequestStream();
			// Write the data to the request stream.
			dataStream.Write(byteArray, 0, byteArray.Length);
			// Close the Stream object.
			dataStream.Close();
	
			try
			{
				//取得回傳資料  
				WebResponse response = request.GetResponse();
				// The using block ensures the stream is automatically closed.
				using (dataStream = response.GetResponseStream())
				{
					// Open the stream using a StreamReader for easy access.
					StreamReader reader = new StreamReader(dataStream);
					// Read the content.
					string responseFromServer = reader.ReadToEnd();
					posttokenstring = responseFromServer;
					// Display the content.
					Console.WriteLine(responseFromServer);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳存取碼 = " + responseFromServer;
				}
				// Close the response.
				response.Close();
			}
			catch (Exception)
			{
				posttokenstring = "";
				//taipeitoken.Text = taipeitoken.Text + "\n" + "取得存取碼的伺服器回應錯誤";
				//throw;
			}
		}
		else
		{
			HttpWebRequest request = HttpWebRequest.Create(accesstokenUrl) as HttpWebRequest;
			request.Method = "POST";
			
			//進行POST資料的字元處理
			//建立 URL 的 post 的送出去的參數
			string querystring = accesstokenUrl_querystring + codestring;
			byte[] byteArray = Encoding.UTF8.GetBytes(querystring);
			//設定ContentType property of the WebRequest.
			request.ContentType = "application/x-www-form-urlencoded";
			//設定ContentLength property of the WebRequest.
			request.ContentLength = byteArray.Length;
			
			request.Headers.Add("Authorization", "Basic ZWYyMTIyNWIzODMwOTk0OWI1YjVkYjEwYWU4NTcxYzhhZGY3ZmQ1Njo0NGY3MzViZjkxNWE3MTc2YmE2ODkwYmM1NGQ2NTc1ODU0Yzk5Mjgz");
			
			// Get the request stream.
			Stream dataStream = request.GetRequestStream();
			// Write the data to the request stream.  
			dataStream.Write(byteArray, 0, byteArray.Length);
			// Close the Stream object.  
			dataStream.Close();
	
			try
			{
				//取得回傳資料  
				WebResponse response = request.GetResponse();
				// The using block ensures the stream is automatically closed.
				using (dataStream = response.GetResponseStream())
				{
					// Open the stream using a StreamReader for easy access.
					StreamReader reader = new StreamReader(dataStream);
					// Read the content.
					string responseFromServer = reader.ReadToEnd();
					posttokenstring = responseFromServer;
					// Display the content.
					Console.WriteLine(responseFromServer);
					//taipeitoken.Text = taipeitoken.Text + "\n" + "回傳存取碼 = " + responseFromServer;
				}
				// Close the response.
				response.Close();
			}
			catch (Exception)
			{
				posttokenstring = "";
				//taipeitoken.Text = taipeitoken.Text + "\n" + "取得存取碼的伺服器回應錯誤";
				//throw;
			}
		}
		
        

        if (posttokenstring == "")
        {
            posttokenstring = "";
        }
        else
        {
            //開始處理 jason 字串
            var jo = JObject.Parse(posttokenstring);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳access_token = " + Convert.ToString(jo["access_token"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳token_type = " + Convert.ToString(jo["token_type"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳refresh_token = " + Convert.ToString(jo["refresh_token"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳expires_in = " + Convert.ToString(jo["expires_in"]);
            //taipeitoken.Text = taipeitoken.Text + "\n" + "回傳scope = " + Convert.ToString(jo["scope"]);
            posttokenstring = Convert.ToString(jo["access_token"]);
        }
        Session["TPTOKEN"] = posttokenstring;
        return posttokenstring;
    }
	
	private string Get02ID(string proxyid)
    {
		string id = "";
		string sqlStr = "select * from SWCProxy where Proxy_ID=@Proxy_ID and Proxy_Status=@Proxy_Status and Proxy_Type=@Proxy_Type";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();
            using (var cmd = Conn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@Proxy_ID", proxyid));
                cmd.Parameters.Add(new SqlParameter("@Proxy_Status", "啟用"));
                cmd.Parameters.Add(new SqlParameter("@Proxy_Type", "技師"));
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            id = reader["IDNO"] + "";
                        }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
		return id;
    }
	private string Get04ID(string proxyid)
    {
		string id = "";
		string sqlStr = "select * from SWCProxy where Proxy_ID=@Proxy_ID and Proxy_Status=@Proxy_Status and Proxy_Type=@Proxy_Type";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();
            using (var cmd = Conn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@Proxy_ID", proxyid));
                cmd.Parameters.Add(new SqlParameter("@Proxy_Status", "啟用"));
                cmd.Parameters.Add(new SqlParameter("@Proxy_Type", "公會"));
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            id = reader["IDNO"] + "";
                        }
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
		return id;
    }
	private bool is_50702(string name)
    {
		bool re = false;
		string sqlStr = "select * from geouser where unit = '工務局大地工程處' and department = '審查管理科' and status = '正常' and jobtitle in ('幫工程司', '科員', '工程員', '助理工程員', '助理員', '約僱技術員', '約僱工程員', '約僱人員', '約聘人員', '聘用技術員', '派遣人員', '按日計酬技術員', '辦事員', '臨時人員', '臨僱技術師', '專案工程師', '臨僱技術員') and name = @name; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();
            using (var cmd = Conn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@name", name));
                #endregion
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