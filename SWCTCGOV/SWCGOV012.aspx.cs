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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCBase001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rBBTID = Request.QueryString["BillBID"] + "";

        string SessETID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        if (ssUserType == "03")
        {
            if (!IsPostBack) { GenerateDropDownList(); GetBillBoard(rBBTID); }
        } else
        {
            Response.Redirect("../SWCDOC/SWC001.aspx");
        }




        //以下全區公用
        SBApp.ViewRecord("公佈欄", "Update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }
    private void GenerateDropDownList()
    {
        //是否顯示
        string[] array_DropDownList1 = new string[] { "是", "否" };
        RABBShow.DataSource = array_DropDownList1;
        RABBShow.DataBind();

        //公佈單位
        string[] array_DropDownList2 = new string[] { "大地工程處審查科", "多維空間資訊" };
        DDLBBUnit.DataSource = array_DropDownList2;
        DDLBBUnit.DataBind();


    }

    private void GetBillBoard(string rCaseID)
    {
        if (rCaseID == "ADDNEW")
        {
            string sDEID = GetNewId();
            TXTBBNO.Text = sDEID;
        } else
        {
            ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
            {
                SwcConn.Open();

                string gDNSQLStr = " select * from BillBoard ";
                gDNSQLStr = gDNSQLStr + " where BBNo ='" + rCaseID + "' ";

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

                    TXTBBNO.Text = sBBNo;
                    TXTBBDateStart.Text= sBBDateStart;
                    TXTBBDateEnd.Text= sBBDateEnd;
                    TXTBBTitle.Text= sBBTitle;
                    TXTBBText.Text= sBBText;
                    RABBShow.SelectedValue= sBBShow;
                    DDLBBUnit.SelectedValue= sBBUnit;

                }
                readerDN.Close();
                objCmdDN.Dispose();

            }
        }
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string gBBNO = TXTBBNO.Text + "";
        string gBBDate1 = TXTBBDateStart.Text + "";
        string gBBDate2 = TXTBBDateEnd.Text + "";
        string gBBTitle = TXTBBTitle.Text+"";
        string gBBText = TXTBBText.Text + "";
        string gBBShow = RABBShow.SelectedValue;
        string gBBUnit = DDLBBUnit.SelectedValue;

        string sEXESQLUPD = "";

        if (gBBText.Length>300) { gBBText = gBBText.Substring(0, 300); }

        //RABBShow

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select BBNo from BillBoard ";
            gDNSQLStr = gDNSQLStr + " where BBNo ='" + gBBNO + "' ";

            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();

            if (!readerDN.HasRows)
            {
                sEXESQLUPD = " INSERT INTO BillBoard (BBNo) VALUES ('" + gBBNO + "');";
            }
            readerDN.Close();
            objCmdDN.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update BillBoard Set ";
            sEXESQLUPD = sEXESQLUPD + " BBDateStart = '" + gBBDate1.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBDateEnd = '" + gBBDate2.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBTitle = '" + gBBTitle.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBText = '" + gBBText.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBShow = '" + gBBShow.Replace("'", "''") + "', ";
            sEXESQLUPD = sEXESQLUPD + " BBUnit = '" + gBBUnit.Replace("'", "''") + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " where BBNo = '" + gBBNO + "' ";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWCGOV012.aspx?BillBID=" + gBBNO + "';</script>");
                    break;
            }

        }
    }

    private string GetNewId()
    {
        string SearchValA = "BB" + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1911).ToString() + DateTime.Now.ToString("MM");
        string MaxKeyVal = "001";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRVa = " select MAX(BBNo) as MAXID from BillBoard ";
            strSQLRVa = strSQLRVa + " where LEFT(BBNo,7) ='" + SearchValA + "' ";

            SqlDataReader readerSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRVa, SwcConn);
            readerSwc = objCmdSwc.ExecuteReader();

            if (readerSwc.HasRows)
            {
                while (readerSwc.Read())
                {
                    string tempMaxKeyVal = readerSwc["MAXID"] + "";

                    if (tempMaxKeyVal != "") {
                        string tempValue = (Convert.ToInt32(tempMaxKeyVal.Substring(tempMaxKeyVal.Length - 3, 3)) + 1).ToString();
                        MaxKeyVal = tempValue.PadLeft(3, '0');
                    }
                }
            }
            objCmdSwc.Dispose();
            readerSwc.Close();
            SwcConn.Close();
        }
        return SearchValA+ MaxKeyVal;
    }
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWCGOV011.aspx");
    }
    

}