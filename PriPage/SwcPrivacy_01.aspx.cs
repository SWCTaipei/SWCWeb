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

public partial class PriPage_Privacy_01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();

        if (CheckPrivacy(ssUserType, ssUserID, ssUserPW)) {
            Response.Redirect("../SWCDOC/SWC001.aspx");
        }

        //全區供用

        //SBApp.ViewRecord("臺北市山坡地水土保持設施維護檢查及輔導紀錄表", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
    }

    private bool CheckPrivacy(string ssUserType, string ssUserID, string ssUserPW)
    {
        bool tPrivacy = true;

        if (ssUserType == "01") {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                string strSQLRV = " SELECT * FROM SWCPrivacy ";
                strSQLRV = strSQLRV + " WHERE ObilgorID = '"+ ssUserID + "' and ObilgorPhone = '" + ssUserPW + "'";

                SqlDataReader readerSwcDoc;
                SqlCommand objCmdSwcDoc = new SqlCommand(strSQLRV, SWCConn);
                readerSwcDoc = objCmdSwcDoc.ExecuteReader();

                if (readerSwcDoc.HasRows) { } else {
                    tPrivacy = false;
                }
            }
        }
        return tPrivacy;
    }

    protected void GoSwcDoc_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";

        string strSQLINS = " INSERT INTO SWCPrivacy (ObilgorID,ObilgorPhone,savedate) VALUES ";  ;
        strSQLINS = strSQLINS + " ('"+ ssUserID + "','"+ ssUserPW + "',GETDATE()) ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlCommand objCmdUser = new SqlCommand(strSQLINS, SWCConn);
            objCmdUser.ExecuteNonQuery();
            objCmdUser.Dispose();
        }
        Response.Redirect("../SWCDOC/SWC001.aspx");
    }
}