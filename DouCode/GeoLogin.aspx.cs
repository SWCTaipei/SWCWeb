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
using System.Data.SqlClient;
using System.Configuration;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnenter_Click(object sender, EventArgs e)
    {
        string pGoaspx = Request.QueryString["goaspx"] + "";
        string pPageType = Request.QueryString["PageType"] + "";
        string pAct = Request.QueryString["Act"] + "";

        if (pAct== "Out")
        {
            //登出
            Session["UserType"] = "";
            Session["ID"] = "";
            Session["PW"] = "";
            Session["NAME"] = "";
            Session["Unit"] = "";
            Session["Edit4"] = "";  //完工公會            
        }

        int i = 5;
        if (pGoaspx == "")
        {
            pGoaspx = "DouCode/SwcDtl07.aspx";
        }
        if (pGoaspx == "" || pGoaspx.Substring(pGoaspx.Length - i, i).ToLower() != ".aspx")
        {
            //pGoaspx = "UserList.aspx";
            laberror.Text = pGoaspx.Substring(pGoaspx.Length - i, i).ToLower();
        }

        string InuptID = txtuid.Text+"";
        string InuptPW = txtpassword.Text+"";

        Class01 BASEAPP = new Class01();

        InuptID = BASEAPP.SQLstrValue(InuptID);
        InuptPW = BASEAPP.SQLstrValue(InuptPW);

        if (InuptID.Trim() != "" && InuptPW.Trim() != "")
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();

                string strSQLRV = " select * from  geouser ";
                strSQLRV = strSQLRV + " Where userid = '" + InuptID + "' ";
                strSQLRV = strSQLRV + " and status = '正常' ";

                SqlDataReader readerUser;
                SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
                readerUser = objCmdUser.ExecuteReader();

                if (readerUser.Read())
                {
                    string tUserid = readerUser["userid"] + "";
                    string tUserPw = readerUser["passwd"] + "";

                    string tName = readerUser["name"] + "";
                    string tMbGroup = readerUser["mbgroup02"] + "";
                    string tJobTitle = readerUser["jobtitle"] + "";

                    if (InuptPW == tUserPw) {
                        laberror.Text = "";

                        Session["ID"] = tUserid;
                        Session["PW"] = tUserPw;
                        Session["jobtitle"] = tJobTitle;

                        Session["uid"] = tUserid;
                        Session["name"] = tName;
                        Session["right"] = tMbGroup;
                        Session["grade"] = tJobTitle;
                        userlog(tUserid, tName);
                        
                        if (pPageType != "1") {
                            pGoaspx = "../" + pGoaspx;
                        }
                        pGoaspx = pGoaspx.Replace(".aspxandand", ".aspx?"); ;
                        pGoaspx = pGoaspx.Replace("andand", "&");
                        Response.Redirect(pGoaspx);
                    }
                    else {
                        laberror.Text = "帳號或密碼錯誤!!";
                    }
                }
                else
                {
                    laberror.Text = "帳號或密碼錯誤!!";
                }
                readerUser.Close();
                objCmdUser.Dispose();
            }
        }
        else {
            laberror.Text = "帳號或密碼錯誤!!";
        }

        
    }
    protected void userlog(string sqlID,string sqlName) {
        String strIPAddr ;
        string sSQLLogStr = "";
        if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null || Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == "")
        {
            strIPAddr = Request.ServerVariables["REMOTE_ADDR"];
        }
        else if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(",") == -1)
        {
            strIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(0, Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(","));
        }

        else if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";") == -1)
        {
            strIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(0, Request.ServerVariables["HTTP_X_FORWARDED_FOR"].IndexOf(";"));
        }
        else {
            strIPAddr = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Substring(0, 30).Trim();
        }

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["tslmConnectionString2"];
        //using (SqlConnection LoginConn = new SqlConnection(connectionString.ConnectionString))
        //{
        //    LoginConn.Open();

        //    sSQLLogStr = "insert into [userlog] (userid,username,userip,logintime) values('"+ sqlID + "','"+ sqlName + "','"+ strIPAddr + "',getdate())";

        //    SqlCommand objCmdLog = new SqlCommand(sSQLLogStr, LoginConn);
        //    objCmdLog.ExecuteNonQuery();
        //    objCmdLog.Dispose();
        //    LoginConn.Close();
            
        //}
    }
}