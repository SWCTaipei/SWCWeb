using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Security.Application;

public partial class SWCDOC_SWC000 : System.Web.UI.Page
{
	public string authUrl = "https://id.taipei/isso/oauth2/authorize?response_type=code&client_id=                         &redirectUri=                 &scope=uuid account member_type verify_level formatted_name real_name email phone_no id_no addresses resident_address";
	
	string SwcUpLoadFilePath = "UpLoadFiles\\SwcCaseFile\\";

    protected void Page_Error(object sender, EventArgs e) {
        Exception objErr = Server.GetLastError().GetBaseException();
        Response.Write(objErr.Message);
        Response.Write("<script>alert('"+ objErr.Message + "');</script>");

        Server.ClearError();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string rACTion = Request.QueryString["ACT"] + "";
        
        GBClass001 SBApp = new GBClass001();

        if (!Page.IsPostBack)
        {
            GenDropDownList();
            NewUser.NavigateUrl = "SWCBase001.aspx?ETID=NEW";

            if (rACTion== "LogOut") {
                //登出
                Session["UserType"] = "";
                Session["ID"] = "";
                Session["PW"] = "";
                Session["NAME"] = "";
                Session["Unit"] = "";
                Session["Edit4"] = "";  //完工公會
                Session.RemoveAll();
            }
            Session["runrunruncount"] = 0;
            MarqueeTimer_Tick(MarqueeTimer, EventArgs.Empty);
        }

        //全區供用

        SBApp.ViewRecord("首頁登入", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        //全區供用
    }

    private void GenDropDownList()
    {
        string[] array_loginChange = new string[] { "水土保持義務人", "技師/各類委員", "審查/檢查單位", "其他機關", "工務局大地工程處", "建築師" };
        loginChange.DataSource = array_loginChange;
        loginChange.DataBind();
    }

    protected void SwcLogin_Click(object sender, ImageClickEventArgs e)
    {
        Boolean LoginR = false;

        string sUserType = loginChange.SelectedValue + "";
        string sInputID = Encoder.HtmlEncode(TXTID.Text + "");
        string sInputPW = Encoder.HtmlEncode(TXTPW.Text + "");

        GBClass001 SwcApp = new GBClass001();
        LoadSwcClass01 LoadApp = new LoadSwcClass01();

        error_msg.Text = "";

        sInputID = SwcApp.SDQQSTR(sInputID);
        //sInputPW = SwcApp.SDQQSTR(sInputPW);

        if (sInputID == "" || sInputPW == "")
        {
            error_msg.Text = SwcApp.AlertMsg("請輸入帳號或密碼!!!");
        }
        else
        {
            switch (sUserType) {
                case "水土保持義務人":
                    LoginR = SwcApp.GetLoginStatus(sInputID, sInputPW, "01");
                    break;

                case "技師/各類委員":
                    LoginR = SwcApp.GetLoginStatus(sInputID, sInputPW, "02");
                    break;

                case "工務局大地工程處":
                    LoginR = SwcApp.GetLoginStatus(sInputID, sInputPW, "03");

                    if (LoginR)
                    {
                        string ssUserName = Session["NAME"] + "";
                        //LoadApp.LoadSwcCase("03", ssUserName);
                    }
                    break;

                case "審查/檢查單位":
                    LoginR = SwcApp.GetLoginStatus(sInputID, sInputPW, "04");

                    if (LoginR)
                    {
                        string ssUserName = Session["NAME"] + "";
                        //LoadApp.LoadSwcCase("04", ssUserName);
                    }
                    break;
            }

            if (LoginR)
            {
                Response.Redirect("~/PriPage/SwcPrivacy_01.aspx");
            }
            else
            {
                error_msg.Text = SwcApp.AlertMsg("密碼輸入錯誤 如忘記密碼請聯繫大地工程處02-27591109");
            }
        }
    }
    protected void MarqueeTimer_Tick(object sender, EventArgs e)
    {
		bool GV = false;
        try
        {
            string MMM = Marqueeclick.Text + "";
        int MMMCount = Convert.ToInt32(Session["runrunruncount"]);

        string NewMsg01 = "";
        string NewMsg02 = "";
        string NewMsg03 = "";
        string NewMsg04 = "";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select * from BillBoard ";
            gDNSQLStr += " where left(convert(varchar, getdate(), 120), 10) >= BBDateStart and left(convert(varchar, getdate(), 120), 10) <= BBDateEnd ";
            gDNSQLStr += " and (BBMainGV = '是') order by id DESC ";
            
            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();

            while (readerDN.Read())
            {
                string sBBNo = readerDN["BBNo"] + "";
                string sBBDateStart = readerDN["BBDateStart"] + "";
                string sBBDateEnd = readerDN["BBDateEnd"] + "";
                string sBBTitle = readerDN["BBTitle"] + "";
                string sBBText = readerDN["BBText"] + "";
                string sBBShow = readerDN["BBShow"] + "";
                string sBBUnit = readerDN["BBUnit"] + "";
                string sBBFile = readerDN["BBFile"] + "";

                NewMsg01 += sBBTitle+";;;";
                NewMsg02 += sBBText + ";;;";
                NewMsg03 += sBBFile + ";;;";
                NewMsg04 += sBBNo + ";;;";
				GV = true;
            }
            readerDN.Close();
            objCmdDN.Dispose();
        }
		if (NewMsg01 == "") {
			using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
			{
				SwcConn.Open();
	
				string gDNSQLStr = " select * from BillBoard ";
				gDNSQLStr += " where left(convert(varchar, getdate(), 120), 10) >= BBDateStart and left(convert(varchar, getdate(), 120), 10) <= BBDateEnd ";
				gDNSQLStr += " and (BBMain = '是') order by id DESC ";
				
				SqlDataReader readerDN;
				SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
				readerDN = objCmdDN.ExecuteReader();
	
				while (readerDN.Read())
				{
					string sBBNo = readerDN["BBNo"] + "";
					string sBBDateStart = readerDN["BBDateStart"] + "";
					string sBBDateEnd = readerDN["BBDateEnd"] + "";
					string sBBTitle = readerDN["BBTitle"] + "";
					string sBBText = readerDN["BBText"] + "";
					string sBBShow = readerDN["BBShow"] + "";
					string sBBUnit = readerDN["BBUnit"] + "";
					string sBBFile = readerDN["BBFile"] + "";
	
					NewMsg01 += sBBTitle+";;;";
					NewMsg02 += sBBText + ";;;";
					NewMsg03 += sBBFile + ";;;";
					NewMsg04 += sBBNo + ";;;";
				}
				readerDN.Close();
				objCmdDN.Dispose();
			}
		}

        if (NewMsg01 == "") {
            MarqueeButton.Style["display"] = "none";
        } else
        {
            string[] arrayMSG01 = NewMsg01.Split(new string[] { ";;;" }, StringSplitOptions.None);
            string[] arrayMSG02 = NewMsg02.Split(new string[] { ";;;" }, StringSplitOptions.None);
            string[] arrayMSG03 = NewMsg03.Split(new string[] { ";;;" }, StringSplitOptions.None);
            string[] arrayMSG04 = NewMsg04.Split(new string[] { ";;;" }, StringSplitOptions.None);

            if (MMM == "ShowDtl") { }
            else
            {
                MMMCount += 1;
                MarqueeButton.Text = arrayMSG01[MMMCount % (arrayMSG01.Length - 1)];
				if(GV)
					MarqueeButton.Attributes["class"] = "news_buttom_red";
				else
					MarqueeButton.Attributes["class"] = "news_buttom";
				MarqueeMessageLabel.Text = arrayMSG02[MMMCount % (arrayMSG01.Length - 1)].Replace(System.Environment.NewLine, "<br/>");
                string tF= arrayMSG03[MMMCount % (arrayMSG01.Length - 1)];
				LinkFile.Text = "";
				LinkFile.NavigateUrl = "";
                if (tF.Trim()!="") {
                    LinkFile.Text = tF;
                    LinkFile.NavigateUrl= ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + arrayMSG04[MMMCount % (arrayMSG01.Length - 1)] + "/" + tF;
                }
                Session["runrunruncount"] = MMMCount.ToString();
            }
            }
        }
        catch
        {
            //Response.Redirect("~/errPage/500.htm");
        }
    }
	
	protected void TaipeiLogin_Click(object sender, EventArgs e)
    {
        string pLogInSelUserType = DropDownList1.Text;
		string PWA = Request.QueryString["PWA"] + "";

        HttpCookie cookie = new HttpCookie("LogInUtype");
        cookie.Value = Server.UrlEncode(pLogInSelUserType);
        cookie.Expires = DateTime.Now.AddMinutes(30);
		
        HttpCookie cookie1 = new HttpCookie("PWA");
        cookie1.Value = Server.UrlEncode(PWA);
        cookie1.Expires = DateTime.Now.AddMinutes(30);
		
        Response.Cookies.Add(cookie);
        Response.Cookies.Add(cookie1);

        Response.Redirect(authUrl);
    }
	protected void BTN_Login_Click(object sender, EventArgs e)
    {
        Boolean LoginR = false;
		string sInputID = Server.HtmlEncode(TXTID1.Text + "");
        string sInputPW = Server.HtmlEncode(TXTPW1.Text + "");
		Session["IDNO"] = sInputID;

        GBClass001 SwcApp = new GBClass001();
		LoginR = SwcApp.GetLoginStatus(sInputID, sInputPW, "01");
		if (LoginR)
        {
            Response.Redirect("PriPage/SwcPrivacy_01.aspx");
        }
    }
	protected void BTN_Login2_Click(object sender, EventArgs e)
    {
        Boolean LoginR = false;
		string sInputID = Server.HtmlEncode(TXTID2.Text + "");
        string sInputPW = Server.HtmlEncode(TXTPW2.Text + "");
		//Session["IDNO"] = sInputID;

        GBClass001 SwcApp = new GBClass001();
        Class20 C20 = new Class20();

		
		string OrgUserId = C20.GetOrganData(sInputID, "AccountNo");
		
		LoginR = C20.GetLoginStatus(OrgUserId, sInputPW, "05");
		if (!LoginR) 
		{
			if (OrgUserId != "")
			{
				Response.Write("<script>alert('請確認身分證字號/統一編號及手機輸入正確');</script>");
				return;
			}
			else
			{
				Response.Redirect("SWCDOC/SWCBase002.aspx?ETID=NEW");
			}
		}
		else
		{
			Response.Redirect("SWCDOC/HaloPage001.aspx");
		}
    }
}