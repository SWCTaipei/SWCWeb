using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SWCDOC_PWA_list : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ClientScript.RegisterStartupScript(ClientScript.GetType(), "tempdata1", "<script>tempdata1();</script>");
		ClientScript.RegisterStartupScript(ClientScript.GetType(), "tempdata2", "<script>tempdata2();</script>");
		ClientScript.RegisterStartupScript(ClientScript.GetType(), "tempdata3", "<script>tempdata3();</script>");
    }
}