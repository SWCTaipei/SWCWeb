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

public partial class SWCDOC_OnlineApply009 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        string rSWCNO = Request.QueryString["SWCNO"] + "";

        GBClass001 SBApp = new GBClass001();

        if (rSWCNO == "") {
            //Response.Redirect("SWC001.aspx");
        }
        else
        {
        }












        //全區供用
        SBApp.ViewRecord("水土保持計畫暫停審查", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        if (ssUserType == "02") { TitleLink00.Visible = true; }
        //全區供用

    }
    
    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?CaseId=" + vCaseID);
    }

    protected void GVSWCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string jAct = e.CommandName;
        string vCaseID = Request.QueryString["SWCNO"] + "";

        switch (jAct)
        {
            case "detail":
                int aa = GVSWCList.Rows[Convert.ToInt32(e.CommandArgument)].RowIndex;
                string jKeyValue = GVSWCList.Rows[aa].Cells[0].Text;
                Response.Redirect("OnlineApply001v.aspx?SWCNO=" + vCaseID + "&OACode=" + jKeyValue);
                break;
        }
        
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        SelectSearchCaseList("");
    }

    private void SelectSearchCaseList(string v)
    {
        RemoveSelSession();

        string sPage001 = TXTS001.Text + "";
        string sPage002 = TXTS002.Text + "";
        string sPage003a = TXTS003a.Text + "";
        string sPage003b = TXTS003b.Text + "";
        string sPage004 = TXTS004.Text + "";
        string sPage005 = TXTS005.Text + "";
        string sPage006 = TXTS006.Text + "";
        string sPage007 = TXTS007.Text + "";
        string sPage008 = TXTS008.Text + "";

        string tempSTR = " select ONA.id,ONA.ONA01001,ONA.SWC002,SWC.SWC005,left(convert(char, ONA.ONA01002, 120),10) as ONA01002,ONA01004 from OnlineApply01 ONA LEFT JOIN SWCCASE SWC ON ONA.SWC002=SWC.SWC002 ";
        tempSTR = tempSTR + " Where 1=1 ";
        if (sPage001.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01001 like '%" + sPage001 + "%' ";
        }
        if (sPage002.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.SWC002 like '%" + sPage002 + "%' ";
        }
        if (sPage003a.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01002 <= '" + sPage003a + "' ";
        }
        if (sPage003b.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01002 >= '" + sPage003b + "' ";
        }
        if (sPage004.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01004 like '%" + sPage004 + "%' ";
        }
        if (sPage005.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01007 like '%" + sPage005 + "%' ";
        }
        if (sPage006.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01006 like '%" + sPage006 + "%' ";
        }
        if (sPage007.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01003 like '%" + sPage007 + "%' ";
        }
        if (sPage008.Trim() != "")
        {
            tempSTR = tempSTR + "   and ONA.ONA01005 like '%" + sPage008 + "%' ";
        }
        tempSTR = tempSTR + " ORDER BY [id]  ";

        //回上一頁用
        Session["PageSearch001"] = sPage001;
        Session["PageSearch002"] = sPage002;
        Session["PageSearch003a"] = sPage003a;
        Session["PageSearch003b"] = sPage003b;
        Session["PageSearch004"] = sPage004;
        Session["PageSearch005"] = sPage005;
        Session["PageSearch006"] = sPage006;
        Session["PageSearch007"] = sPage007;
        Session["PageSearch008"] = sPage008;

        SqlDataSource.SelectCommand = tempSTR;
    }

    private void RemoveSelSession()
    {
        Session["PageSearch001"] = "";
        Session["PageSearch002"] = "";
        Session["PageSearch003a"] = "";
        Session["PageSearch003b"] = "";
        Session["PageSearch004"] = "";
        Session["PageSearch005"] = "";
        Session["PageSearch006"] = "";
        Session["PageSearch007"] = "";
        Session["PageSearch008"] = "";
    }

    protected void SqlDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        CaseCount.Text = e.AffectedRows.ToString();
    }

    protected void GVSWCList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["mySorting"] == null)
        {
            e.SortDirection = SortDirection.Ascending;
            ViewState["mySorting"] = "Ascending";
        }
        else
        {
            if (ViewState["mySorting"] + "" == "Ascending")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["mySorting"] = "Descending";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["mySorting"] = "Ascending";
            }
        }
        ExeQSel_Click(sender, e);
    }

    protected void GVSWCList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        RemoveSelSession();

        TXTS001.Text = "";
        TXTS002.Text = "";
        TXTS003a.Text = "";
        TXTS003b.Text = "";
        TXTS004.Text = "";
        TXTS005.Text = "";
        TXTS006.Text = "";
        TXTS007.Text = "";
        TXTS008.Text = "";

        SelectSearchCaseList("");
    }
}