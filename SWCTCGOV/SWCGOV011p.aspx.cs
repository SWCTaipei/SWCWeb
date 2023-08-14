using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCTCGOV_SWCGOV011p : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();
        string rBBTID = Request.QueryString["BillBID"] + "";
        //rBBTID = "BB10811001";

        if (!IsPostBack)
        {
            C20.swcLogRC("SWCGOV013", "公佈欄", "詳情", "瀏覽", rBBTID);
            //GetBillBoard(rBBTID);
        }
    }

    protected void BtnView_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";

        Response.Write("<script language='javascript'>window.open('SWCGOV013.aspx?BillBID=" + tDENo + "');</script>");

        //Response.Redirect("SWCGOV013.aspx?BillBID=" + tDENo);
    }
	
	protected void GVSWCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:
				if (e.Row.Cells[4].Text == "&nbsp;") e.Row.Cells[4].Text = "0";
				e.Row.Cells[4].Text = "<span class='read'><b>" + e.Row.Cells[4].Text + "人</b> 已看過</span>";
				
                break;
        }
    }

}