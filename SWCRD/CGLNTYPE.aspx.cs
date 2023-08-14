using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCRD_CGLNTYPE : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["UserType"] = "04";
        Response.Redirect("../SWCDOC/SWC001.aspx");
    }
}