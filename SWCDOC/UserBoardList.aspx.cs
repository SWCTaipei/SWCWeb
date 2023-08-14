using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_UserBoard : System.Web.UI.Page
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
                UserBoard00.Visible = true;
                break;
            case "02":
                TitleLink00.Visible = true;
                UserBoard00.Visible = true;
                break;
            case "03":
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                break;
            case "04":
                UserBoard00.Visible = true;
                break;
            default:
                Response.Redirect("SWC000.aspx");
                break;
        }

        //全區供用

        SBApp.ViewRecord("留言版-留言", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }

    protected void NewCase_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("UserBoard.aspx");
    }

    protected void GVUserBoard_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                string QQ = e.Row.Cells[5].Text;
                //更換日期
                if (QQ != "")
                {
                    if(DateTime.Parse(QQ).ToString("yyyy-MM-dd") == "1900-01-01") { QQ = ""; }
                    else { QQ = DateTime.Parse(QQ).ToString("yyyy-MM-dd"); }
                }

                e.Row.Cells[5].Text = QQ;

            }
            catch (Exception)
            {

            }
        }
    }


}