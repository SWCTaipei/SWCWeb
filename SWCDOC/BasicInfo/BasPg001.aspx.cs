using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class SWCDOC_BasicInfo_BasPg001 : System.Web.UI.Page
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        Class1 C1 = new Class1();
        string[] mailTo = new string[] { "tim@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";

        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：" + errUrl + "<br/>";
        mailText += "錯誤訊息：" + errMsg + "<br/>";

        C1.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
        Response.Redirect("~/errPage/500.htm");
        Server.ClearError();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssGuild01 = Session["Guild01"] + "";
        string ssGuild02 = Session["Guild02"] + "";

        if (ssUserID == "" || !(ssGuild01 == "1" || ssGuild02 == "1")) Response.Redirect("../SWC001.aspx");
        if (IsPostBack) return;
        SetGridViewdata(ssUserID);
        SetGridViewdata2(ssUserID);
        if (ssGuild01 == "0" && ssGuild02 == "1") TextBox1.Text = "2";
        if (ssGuild01 == "1" && ssGuild02 == "1") TextBox1.Text = "3";

        SBApp.ViewRecord("權限設定", "view", "");
        #region 全區公用

        SBApp.ViewRecord("選擇公會技師", "view", ssUserID);

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        #endregion
    }

    private void SetGridViewdata(string ssUserGuild)
    {
        #region 審查公會
        string exeSqlStr = "select E.ETID,E.ETName AS Name,E.ETTel AS Phone,G.GuildName,'../../tcgefile/SWCDOC/UpLoadFiles/SwcUser/'+E.ETID+'/'+G.Certificate AS FileA,'../../tcgefile/SWCDOC/UpLoadFiles/SwcUser/'+E.ETID+'/'+G.Experience AS FileB,'公會證書' AS FileAText,CONVERT(varchar(100), G.EXP, 23) AS EXP,CASE WHEN ISNULL(G.Experience,'') != '' THEN '審查經歷' ELSE '' END AS FileBText,CASE WHEN ISNULL(E.GuildTcgeChk,'0') = '1' THEN '通過' ELSE '' END AS TCGECHK,CASE WHEN  (ISNULL(E.GuildSubstitute,'') = '' or E.GuildSubstitute<>'" + ssUserGuild + "') THEN 'false' ELSE 'true' END AS SELCHK from ETUOwnGuild G LEFT JOIN ETUsers E ON E.ETID=G.ETID where G.EtGuild='" + ssUserGuild + "' AND (ISNULL(E.GuildSubstitute,'') = '' or ISNULL(E.GuildSubstitute,'') = '" + ssUserGuild + "') and isnull(experience,'')<>''; ";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SWCConnStr"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(exeSqlStr, strConnString);

        DataTable dt = new DataTable();
        da.Fill(dt);
        GVETUser.DataSource = dt;
        GVETUser.DataBind();
        connect.Close();
        #endregion
    }
    private void SetGridViewdata2(string ssUserGuild)
    {
        #region 檢查公會
        string exeSqlStr = "select E.ETID,E.ETName AS Name,E.ETTel AS Phone,G.GuildName,'../../tcgefile/SWCDOC/UpLoadFiles/SwcUser/'+E.ETID+'/'+G.Certificate AS FileA,'../../tcgefile/SWCDOC/UpLoadFiles/SwcUser/'+E.ETID+'/'+G.Experience AS FileB,'公會證書' AS FileAText,CONVERT(varchar(100), G.EXP, 23) AS EXP,CASE WHEN ISNULL(G.Experience,'') != '' THEN '審查經歷' ELSE '' END AS FileBText,CASE WHEN ISNULL(E.GuildTcgeChk2,'0') = '1' THEN '通過' ELSE '' END AS TCGECHK,CASE WHEN  (ISNULL(E.GuildSubstitute2,'') = '' or E.GuildSubstitute2<>'" + ssUserGuild + "') THEN 'false' ELSE 'true' END AS SELCHK from ETUOwnGuild G LEFT JOIN ETUsers E ON E.ETID=G.ETID where G.EtGuild='" + ssUserGuild + "' AND (ISNULL(E.GuildSubstitute2,'') = '' or ISNULL(E.GuildSubstitute2,'') = '" + ssUserGuild + "'); ";

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["SWCConnStr"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(exeSqlStr, strConnString);

        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
        connect.Close();
        #endregion
    }
    protected void SaveData_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";
        string vSelectETUserStr = "''";
        string vUnSelectETUserStr = "''";
        GBClass001 SBApp = new GBClass001();

        #region-存檔
        foreach (GridViewRow GVRow in GVETUser.Rows) {
            HiddenField HDETID = (HiddenField)GVRow.FindControl("HDETID");
            CheckBox BtnChk = (CheckBox)GVRow.FindControl("CHKSub");
            if (BtnChk.Checked) vSelectETUserStr += ",'"+ HDETID.Value+"'";else vUnSelectETUserStr += ",'" + HDETID.Value + "'";
        }
        string exeSqlStr = " Update ETUsers set GuildSubstitute='' Where GuildSubstitute='" + ssUserID + "' ; Update ETUsers set GuildSubstitute='" + ssUserID + "' Where ETID in ("+ vSelectETUserStr + ");Update ETUsers set GuildSubstitute='' Where ETID in (" + vUnSelectETUserStr + "); ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }

        vSelectETUserStr = "''";
        vUnSelectETUserStr = "''";
        foreach (GridViewRow GVRow in GridView1.Rows)
        {
            HiddenField HDETID = (HiddenField)GVRow.FindControl("HDETID");
            CheckBox BtnChk = (CheckBox)GVRow.FindControl("CHKSub");
            if (BtnChk.Checked) vSelectETUserStr += ",'" + HDETID.Value + "'"; else vUnSelectETUserStr += ",'" + HDETID.Value + "'";
        }
        exeSqlStr = " Update ETUsers set GuildSubstitute2='" + ssUserID + "' Where ETID in (" + vSelectETUserStr + ");Update ETUsers set GuildSubstitute2='' Where ETID in (" + vUnSelectETUserStr + "); ";

        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }
        #endregion
        #region.寄信
        SendMailNotice();
        #endregion

        SBApp.ViewRecord("權限設定", "Update", "");
        Response.Write("<script>alert('資料已存檔'); location.href='../SWC001.aspx'; </script>");
    }
    private void SendMailNotice()
    {
        string tGuildID = Session["ID"]+"";
        string tGuildName = Session["NAME"] + "";
        string tToday = DateTime.Now.ToString("yyyy-MM-dd");
        string tMailTitle = "【"+ tGuildName + "】已於 "+ tToday + " 送出審查委員名冊申請，請至坡地管理資料庫查看";
        string tMailText = "【"+ tGuildName + "】已於 "+ tToday + " 送出審查委員名冊申請，請至臺北市坡地管理資料庫查看";

        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();
        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";
		
        string ChkMailGroup1 = arrayChkUserMsg[6] + "";
        string ChkMailGroup2 = arrayChkUserMsg[7] + "";
        string ChkMailGroup3 = arrayChkUserMsg[8] + "";
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);
		
        string[] arrayMailGroup1 = ChkMailGroup1.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMailGroup2 = ChkMailGroup2.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMailGroup3 = ChkMailGroup3.Split(new string[] { ";;" }, StringSplitOptions.None);

        //21.公會送出審查委員名單清冊
        //舊：送出提醒名單：科長、正工、股長、管理者、琮元 (ge-50707)
        //2021-01-26 起 送出提醒名單：琮元 (ge-50707)、許巽舜 (gv-hsun)  
		//2022-02-24 起 送出提醒名單：陳世豪 (ge-40523)、許巽舜 (gv-hsun)
		//2022-05-06 送出提醒名單：陳世豪 (ge-40523)、審查階段通知收信窗口群組
        string SentMailGroup = "";
        for (int i = 1; i < arrayUserId.Length; i++)
        {
            string aUserId = arrayUserId[i];
            string aUserName = arrayUserName[i];
            string aJobTitle = arrayJobTitle[i];
            string aUserMail = arrayUserMail[i];
            string aMBGROUP = arrayMBGROUP[i];
            string aMailGroup2 = arrayMailGroup2[i];

            //if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工程司" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserId.Trim() == "ge-50707")
            //if (aUserId.Trim() == "ge-40523" || aUserId.Trim() == "gv-hsun")
			if (aUserId.Trim() == "ge-40523" || aMailGroup2.Trim() == "Y")
            {
                SentMailGroup = SentMailGroup + ";;" + aUserMail;

                //一人一封不打結
                string[] arraySentMail01 = new string[] { aUserMail };
                string ssMailSub01 = tMailTitle;
                string ssMailBody01 = tMailText;
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

                bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
            }
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string ssUserGuild = Session["WMGuild"] + "";
        GridView1.PageIndex = e.NewPageIndex;
        getCheckItemsAndSaveIntoSession();
        //SetGridViewdata2(ssUserGuild);
    }

    protected void GVETUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string ssUserGuild = Session["WMGuild"] + "";
        GVETUser.PageIndex = e.NewPageIndex;        
        //SetGridViewdata(ssUserGuild);
    }    
    protected void getCheckItemsAndSaveIntoSession()
    {
        ArrayList al4 = new ArrayList();

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            bool isChecked = ((CheckBox)GridView1.Rows[i].FindControl("chkSelect")).Checked;
            if (isChecked)
            {
                //如果還不曾在別頁勾選過，並存入 Session 中的 ArrayList 的話，就將本頁所勾選的 選項 ID，存入 ArrayList；
                //若已曾在別頁勾選過，並存入 Session 中的 ArrayList 的話，就先將 Session 中 ArrayList 所存的別頁的選項取出，再將本頁的勾選值，再附加上去。
                if (Session["theCheckedID"] == null)
                {
                    al4.Add(GridView1.Rows[i].Cells[1].Text.Trim());
                }
                else
                {
                    al4 = (ArrayList)Session["theCheckedID"];
                    al4.Add(GridView1.Rows[i].Cells[1].Text.Trim());
                }

                //把這一頁和別頁中，已存至 ArrayList 中，所勾選資料列的 選項 ID，全部轉存到 Session 中
                Session["theCheckedID"] = al4;
                //Response.Write("您已把 ArrayList 整個存到 Session 中<br>");
            }
        }
    }
}