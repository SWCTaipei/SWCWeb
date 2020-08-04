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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWC000 : System.Web.UI.Page
{
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
        string[] array_loginChange = new string[] { "水土保持義務人", "承辦/監造技師", "審查/檢查單位", "工務局大地工程處" };
        loginChange.DataSource = array_loginChange;
        loginChange.DataBind();
    }

    protected void SwcLogin_Click(object sender, ImageClickEventArgs e)
    {
        Boolean LoginR = false;

        string sUserType = loginChange.SelectedValue + "";
        string sInputID = TXTID.Text + "";
        string sInputPW = TXTPW.Text + "";

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

                case "承辦/監造技師":
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
        string MMM = Marqueeclick.Text + "";
        int MMMCount = Convert.ToInt32(Session["runrunruncount"]);

        string NewMsg01 = "";
        string NewMsg02 = "";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select * from BillBoard ";
            gDNSQLStr += " where left(convert(varchar, getdate(), 120), 10) >= BBDateStart and left(convert(varchar, getdate(), 120), 10) <= BBDateEnd ";
            gDNSQLStr += " and BBShow = '是' order by id ";
            
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

                NewMsg01 += sBBTitle+";;;";
                NewMsg02 += sBBText + ";;;";
            }
            readerDN.Close();
            objCmdDN.Dispose();

        }

        if (NewMsg01 == "") {
            MarqueeButton.Style["display"] = "none";
        } else
        {
            string[] arrayMSG01 = NewMsg01.Split(new string[] { ";;;" }, StringSplitOptions.None);
            string[] arrayMSG02 = NewMsg02.Split(new string[] { ";;;" }, StringSplitOptions.None);

            if (MMM == "ShowDtl") { }
            else
            {
                MMMCount += 1;
                MarqueeButton.Text = arrayMSG01[MMMCount % (arrayMSG01.Length - 1)];
                MarqueeMessageLabel.Text = arrayMSG02[MMMCount % (arrayMSG01.Length - 1)];
                Session["runrunruncount"] = MMMCount.ToString();
            }
        }
    }
}