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
        
        if (ssUserType == "03")
        {
            if (!IsPostBack) { GenerateDropDownList();}
        }
        else
        {
            Response.Redirect("../SWCDOC/SWC001.aspx");
        }

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
    private void GenerateDropDownList()
    {
        //公佈單位
        string[] array_DropDownList2 = new string[] {"","大地工程處審查科", "多維空間資訊" };
        STUnit.DataSource = array_DropDownList2;
        STUnit.DataBind();


    }

    protected void NewSwc_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("SWCGOV012.aspx?BillBID=ADDNEW");
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";
        
        Response.Redirect("SWCGOV002.aspx?DisEventId="+ tDENo);
    }
    
    protected void BtnView_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";

        Response.Redirect("SWCGOV012.aspx?BillBID=" + tDENo);
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        RemoveSelSession();

        string shBBNo = STNO.Text + "";
        string shBBTitle = STTitle.Text + "";
        string shBBText = STText.Text + "";
        string shBBUnit = STUnit.SelectedValue + "";

        string sqlSelectStr = " select replace(replace(BBShow,1,'Y'),0,'N') as Display ,* from BillBoard Where 1=1 ";
        if (shBBNo != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND BBNo = '" + shBBNo.Replace("'","''") + "' ";
        }
        if (shBBTitle != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND BBTitle like N'%" + shBBTitle.Replace("'", "''") + "%' ";
        }
        if (shBBText != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND BBText like N'%" + shBBText.Replace("'", "''") + "%' ";
        }
        if (shBBUnit != "")
        {
            sqlSelectStr = sqlSelectStr + "  AND BBUnit = '" + shBBUnit.Replace("'", "''") + "' ";
        }
        sqlSelectStr = sqlSelectStr + " ORDER BY [BBNo] DESC ";

        SqlDataSource.SelectCommand = sqlSelectStr;

        //回上一頁用
        Session["BBNo"] = shBBNo;
        Session["BBTitle"] = shBBTitle;
        Session["BBText"] = shBBText;
        Session["BBUnit"] = shBBUnit;

    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        RemoveSelSession();

        STNO.Text = "";
        STTitle.Text = "";
        STText.Text= "";
        STUnit.SelectedValue = "";

        string sqlSelectStr = " select replace(replace(BBShow,1,'Y'),0,'N') as Display ,* from BillBoard ";
        sqlSelectStr = sqlSelectStr + " ORDER BY [BBNo] DESC ";

        SqlDataSource.SelectCommand = sqlSelectStr;
    }

    private void RemoveSelSession()
    {
        Session["BBNo"] = "";
        Session["BBTitle"] = "";
        Session["BBText"] = "";
        Session["BBUnit"] = "";

    }

    protected void SqlDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        CaseCount.Text = CaseCount.Text = e.AffectedRows.ToString();
    }
}