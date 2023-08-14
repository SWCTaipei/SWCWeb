using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestFolder_ReSMS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        SBApp.SendSMS(TextBox1.Text, TextBox2.Text);
        TextBox1.Text = "";

        Response.Write("<script>alert('簡訊已傳送！'); </script>");
    }
}