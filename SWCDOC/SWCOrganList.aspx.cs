using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCOrganList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string idno = Session["IDNO"] + "";

        GBClass001 SBApp = new GBClass001();
		Class20 C20 = new Class20();

		if (!IsPostBack)
        {
			switch (ssUserType)
			{
				case "05":
					if(C20.GetOrganData(idno,"Role")!="系統管理員"){
						Response.Write("<script>alert('無權限'); location.href='HaloPage001.aspx';</script>");
					}
					break;
				default:
					Response.Write("<script>alert('無權限'); location.href='HaloPage001.aspx';</script>");
					break;
			}
		
			GetGVData();
		}
        

        //全區供用
        SBApp.ViewRecord("帳號列表(其他機關)", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }

    protected void GVOrganList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                break;
        }
    }
	
	protected void GetGVData(){
		SqlDataSource.SelectCommand = "select AccountNo,Name,Role,Status from Organ ORDER BY [CreateDate] DESC";
	}
	
	protected void BtnView_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tID = LButton.CommandArgument + "";

        Response.Redirect("SWCOrganAccount.aspx?ID=" + tID);
    }
	
	protected void SqlDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        CaseCount.Text = e.AffectedRows.ToString();
    }
}