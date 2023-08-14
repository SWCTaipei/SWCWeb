using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;

public partial class SWCDOC_HaloPage001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        whatsYourArea();
        //toDayNews();
    }

    #region toDayNews
    private void toDayNews()
    {
        string sqlStr = " select *,'../SWCTCGOV/SWCGOV013.aspx?BillBID='+BBNo as BBURL from BillBoard where left(convert(varchar, getdate(), 120), 10) >= BBDateStart and left(convert(varchar, getdate(), 120), 10) <= BBDateEnd and BBShow = '是' order by BBDateStart DESC;";
        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SWCConnStr"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlStr, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GVNews.DataSource = dt;
        GVNews.DataBind();
        connect.Close();
    }
    #endregion

    private void whatsYourArea()
    {
        string ssUserID = Session["ID"] + "";
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string ssUserType = Session["UserType"] + "";   //大地工程處人員
        if (ssUserID != ssUserGuild && ssUserType=="04") { ssUserType = "02"; Session["UserType"] = "02"; }
        string ssEdit4 = Session["Edit4"]+"";   //完工檢查公會
        if (ssUserType.Trim() =="") { Response.Redirect("../Default.aspx"); }
        switch (ssUserType) {
            case "01": Panel1.Visible = true; break;
            case "02": Panel2.Visible = true; if(CheckExpireOrNot()==true){LB_YN.Text="T"; } break;
            case "04": Panel3.Visible = true; break;
			case "05": Panel4.Visible = true; break;
			case "06": Panel5.Visible = true; break;
			case "07": Panel6.Visible = true; break;
			case "08": Panel7.Visible = true; break;
			case "09": Panel8.Visible = true; break;
        }
        //完公檢查公會可見
        if (ssEdit4 == "Y") { HyperLink2.Visible = true; }

        //大地工程處人員可見
        if (ssUserType == "03") { HyperLink2.Visible = true; Response.Redirect("SWC001.aspx"); }
    }
	
	private bool CheckExpireOrNot()
	{
        string ssUserID = Session["ID"] + "";
		bool YN = false;
		string gDNSQLStr =  "select * from ETUsers where ((TCNo01ED < getdate() and TCNo01ED != '1900-01-01 00:00:00.000') ";
		gDNSQLStr += " or (TCNo02ED < getdate()  and TCNo02ED != '1900-01-01 00:00:00.000') ";
		gDNSQLStr += " or (TCNo03ED < getdate()  and TCNo03ED != '1900-01-01 00:00:00.000') ";
		gDNSQLStr += " or (TCNo04ED < getdate()  and TCNo04ED != '1900-01-01 00:00:00.000')) and ETID = @ETID ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = gDNSQLStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@ETID", ssUserID));
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                        YN = true;
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
		return YN;
	}
	
	protected void LB1_Click(object sender, EventArgs e)
    {
		//*******************
        //TOKEN
        string ssUserID = Session["ID"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string token_temp = Session["PW"] + "|" + Session["Unit"] + "|" + Session["JobTitle"] + "|" + Session["Edit4"] + "|" + Session["WMGuild"] + "|" + Session["Guild01"] + "|" + Session["Guild02"] + "|" + Session["ETU_Guild01"] + "|" + Session["ETU_Guild02"] + "|" + Session["ETU_Guild03"] + "|" + Session["ONLINEAPPLY"] + "|" + Session["NUIDNO"] + "|" + Session["NUNAME"] + "|" + Session["NUCELL"] + "|" + Session["NUMAIL"] + "|" + Session["Department"] + "|" + Session["uid"] + "|" + Session["right"] + "|" + Session["grade"] + "|" + Session["TcgeDataedit"] + "|" + Session["TcgeDataview"] + "|" + Session["SuperUser"] + "|" + Session["presented"] + "";

        string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        string source = "書件";
        // 0 Session["ID"]
        // 1 Session["UserType"]
        // 2 Session["NAME"]
        // 3 時間
        // 4 來源
        // 5 Session["PW"]
        // 6 Session["Unit"]
        // 7 Session["JobTitle"]
        // 8 Session["Edit4"]
        // 9 Session["WMGuild"]
        //10 Session["Guild01"]
        //11 Session["Guild02"]
        //12 Session["ETU_Guild01"]
        //13 Session["ETU_Guild02"]
        //14 Session["ETU_Guild03"]
        //15 Session["ONLINEAPPLY"]
        //16 Session["NUIDNO"]
        //17 Session["NUNAME"]
        //18 Session["NUCELL"]
        //19 Session["NUMAIL"]
        //20 Session["Department"]
        //21 Session["uid"]
        //22 Session["right"]
        //23 Session["grade"]
        //24 Session["TcgeDataedit"]
        //25 Session["TcgeDataview"]
        //26 Session["SuperUser"]
        //27 Session["presented"]
        string token = ssUserID + "|" + ssUserType + "|" + ssUserName + "|" + dt + "|" + source + "|" + token_temp;
        //Response.Write(token);
        byte[] b = Encoding.UTF8.GetBytes(token);
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        var Key = new Byte[] {  };
        var IV = new Byte[] {  };
        ICryptoTransform ict = des.CreateEncryptor(Key, IV);
        byte[] outData = ict.TransformFinalBlock(b, 0, b.Length);
        var op = Convert.ToBase64String(outData);
        //*******************second part
        //0 dt
		//1 Session["IDNO"]
        string token2 = dt + "|" + Session["IDNO"];
        byte[] b2 = Encoding.UTF8.GetBytes(token2);
        DESCryptoServiceProvider des2 = new DESCryptoServiceProvider();
        var Key2 = "TGEOTGEO";
        var IV2 = "TGEOTGEO";
        ICryptoTransform ict2 = des.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes(Key2), ASCIIEncoding.ASCII.GetBytes(IV2));
        byte[] outData2 = ict2.TransformFinalBlock(b2, 0, b2.Length);
        var op2 = Convert.ToBase64String(outData2);
        //*******************
        Session["NCKU"] = "T";

        string url = "https://tgeo.swc.taipei?UTYPE=" + ssUserType + "&UNAME=" + ssUserName + "&Token=" + op + "&Source=" + source + "&Token2=" + op2;

        Response.Write("<script>window.open('" + url + "','_blank');</script>");



        //Response.Redirect(url);
    }
}