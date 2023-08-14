using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class errPage_er404 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "claire@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";

        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：<br/>";
        mailText += "錯誤訊息：使用者指定錯誤的連結或是檔案不存在。<br/>";

        C1.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
    }
}