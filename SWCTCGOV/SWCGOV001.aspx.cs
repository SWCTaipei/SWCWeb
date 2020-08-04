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

public partial class SWCTCGOV_SWCGOV001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rqSearch = Request["SR"] + "";

        GBClass001 SBApp = new GBClass001();

        switch (ssUserType)
        {
            case "01":
                break;
            case "02":
                TitleLink00.Visible = true;
                break;
            case "03":  //大地人員
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                break;
            case "04":
                break;
            default:
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
        }

        //全區供用
        SBApp.ViewRecord("書件目錄查詢", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }

    protected void NewSwc_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SWCGOV002.aspx?DisEventId=ADDNEW");
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";
        
        Response.Redirect("SWCGOV002.aspx?DisEventId="+ tDENo);
    }

    protected void GVSWCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                string gvDENo = Convert.ToString(e.Row.Cells[0].Text);

                HiddenField hiddenDate01 = (HiddenField)e.Row.Cells[6].FindControl("HiddenField1");

                string gvHDData01 = hiddenDate01.Value;

                Button btnView = (Button)e.Row.Cells[0].FindControl("BtnView");
                Button btnEdit = (Button)e.Row.Cells[0].FindControl("BtnEdit");
                Button btnDelete = (Button)e.Row.Cells[0].FindControl("BtnDelete");

                if (gvHDData01 == "Y")
                {
                    btnView.Visible = true;
                }
                else
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
                break;
        }

    }

    protected void BtnView_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";

        Response.Redirect("SWCGOV003.aspx?DisEventId=" + tDENo);
    }
}