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
                Response.Redirect("../PriPage/SwcPrivacy_01.aspx");
            }
            else
            {
                error_msg.Text = SwcApp.AlertMsg("密碼輸入錯誤 如忘記密碼請聯繫大地工程處02-27591109");
            }

            
        }

    }
}