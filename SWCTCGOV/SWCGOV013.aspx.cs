using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;

public partial class SWCTCGOV_SWCGOV013 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();
        string rBBTID = Request.QueryString["BillBID"] + "";
        //rBBTID = "BB10811001";

        if (!IsPostBack)
        {
            C20.swcLogRC("SWCGOV013", "公佈欄", "詳情", "瀏覽", rBBTID);
            GetBillBoard(rBBTID);
			GetBillBoardMessageAndLike(rBBTID);
			
			string sessionKey = "BB" + rBBTID;
            if (Session[sessionKey] == null)//若此文章沒被點閱過，則
            {
                //瀏覽人數+1
				GetViewers(rBBTID);
                //並標記此文章已點閱過
                Session[sessionKey] = true;
            }
        }
    }
	private void GetViewers(string rCaseID)
    {
		if(LBViewers.Text == "") LBViewers.Text = "0";
		LBViewers.Text = (Convert.ToInt32(LBViewers.Text) + 1).ToString();
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			string gDNSQLStr = "Update BillBoard set BBViewers = @BBViewers where BBNO = @BBNO;";
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = gDNSQLStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@BBNO", rCaseID));
                cmd.Parameters.Add(new SqlParameter("@BBViewers", LBViewers.Text));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
	}
    private void GetBillBoard(string rCaseID)
    {
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open();
	
			string gDNSQLStr = " select * from BillBoard ";
			gDNSQLStr = gDNSQLStr + " where BBNo ='" + rCaseID + "' ";
	
			SqlDataReader readerDN;
			SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
			readerDN = objCmdDN.ExecuteReader();
	
			while (readerDN.Read())
			{
				string sBBNo = readerDN["BBNo"] + "";
				string sBBDateStart = readerDN["BBDateStart"] + "";
				string sBBDateEnd = readerDN["BBDateEnd"] + "";
				string sBBTitle = readerDN["BBTitle"] + "";
				string sBBText = readerDN["BBText"] + "";
				string sBBShow = readerDN["BBShow"] + "";
				string sBBUnit = readerDN["BBUnit"] + "";
				string sBBFile = readerDN["BBFile"] + "";
				string sBBMain = readerDN["BBMain"] + "";
				string sBBViewers = readerDN["BBViewers"] + "";
	
				TXTBBNO.Text = sBBNo;
				TXTBBDateStart.Text = sBBDateStart;
				TXTBBDateEnd.Text = sBBDateEnd;
				TXTBBTitle.Text = sBBTitle;
				TXTBBText.Text = sBBText.Replace(System.Environment.NewLine, "<br/>");
				DDLBBUnit.Text = sBBUnit;
                LBViewers.Text = sBBViewers;
	
				#region 檔案類處理
				string[] arrayFileNameLink = new string[] { sBBFile };
				System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { LinkFile };
	
				for (int i = 0; i < arrayFileNameLink.Length; i++)
				{
					string strFileName = arrayFileNameLink[i];
					System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];
	
					FileLinkObj.Visible = false;
					if (strFileName == "")
					{
					}
					else
					{
						string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + sBBNo + "/" + strFileName;
						FileLinkObj.Text = strFileName;
						FileLinkObj.NavigateUrl = tempLinkPateh;
						FileLinkObj.Visible = true;
					}
				}
				#endregion
			}
			readerDN.Close();
			objCmdDN.Dispose();
	
		}
    }
	private void GetBillBoardMessageAndLike(string rCaseID)
    {
		//bool TF = false;
        string ssUserName = Session["NAME"] + "";
        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select * from BillBoardMessage ";
            gDNSQLStr = gDNSQLStr + " where BBNo ='" + rCaseID + "' order by BBDateTime DESC ";

            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();
			
			
			HtmlTableRow trow;
			HtmlTableCell tcell;
			TBSearchList.Controls.Clear();
            while (readerDN.Read())
            {
				
				trow = new HtmlTableRow();
				TBSearchList.Controls.Add(trow);
				
				tcell = new HtmlTableCell("th");
				tcell.InnerHtml = "<label style='color:blue; font-weight:bold;'>" + Server.HtmlEncode(readerDN["BBWho"].ToString()) + "</label>" +  "<label style='color:gray;'>" + Convert.ToDateTime(readerDN["BBDateTime"]).ToString("yyyy-MM-dd HH:mm") + "</label>";
				trow.Controls.Add(tcell);
				
				trow = new HtmlTableRow();
				TBSearchList.Controls.Add(trow);
				
				tcell = new HtmlTableCell();
				tcell.InnerHtml = readerDN["BBMessage"].ToString().Replace(System.Environment.NewLine, "<br/>") + "<br/><hr>";
				trow.Controls.Add(tcell);
            }
            readerDN.Close();
            objCmdDN.Dispose();
        }
		lb_messagecount.Text = (TBSearchList.Rows.Count/2).ToString();
        //using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        //{
        //    SwcConn.Open();
        //
        //    string gDNSQLStr = " select * from BillBoardLike ";
        //    gDNSQLStr = gDNSQLStr + " where BBNo ='" + rCaseID + "' and BBWho = '" + ssUserName + "' ";
        //
        //    SqlDataReader readerDN;
        //    SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
        //    readerDN = objCmdDN.ExecuteReader();
		//	
        //    while (readerDN.Read())
        //    {
		//		TF = true;
        //    }
        //    readerDN.Close();
        //    objCmdDN.Dispose();
        //}
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select count(*) as a from BillBoardLike ";
            gDNSQLStr = gDNSQLStr + " where BBNo ='" + rCaseID + "' ";

            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();
			
            while (readerDN.Read())
            {
				lb_likecount.Text = readerDN["a"].ToString();
            }
            readerDN.Close();
            objCmdDN.Dispose();
        }
		//if(TF == true)
		//{
			Btn_Like.Attributes["src"] = "../images/icon/good2.png";
		//}
		//else
		//{
		//	Btn_Like.Attributes["src"] = "../images/icon/good1.png";
		//}
		
		SqlDataSource.SelectCommand = " select * from (select BBWHO,convert(varchar, BBDATETIME, 120) BBDATETIME,BBMESSAGE from BillBoardMessage where BBNO = '" + TXTBBNO.Text + "' union all select BBWHO,convert(varchar, BBDATETIME, 120) BBDATETIME,BBMESSAGE from BillBoardMessage where BBNO = '" + TXTBBNO.Text + "') A ORDER BY [BBDateTime] DESC ";
    }
	protected void Btn_Like_Click(object sender, ImageClickEventArgs e)
    {
		string gDNSQLStr = "";
		string rBBTID = Request.QueryString["BillBID"] + "";
        string ssUserName = Session["NAME"] + "";
		//bool TF = false;
		//if (ssUserName == "") 
		//{
		//	Response.Write("<script>alert('請先登入');</script>");
		//}
		//else
		//{
			ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
			//using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
			//{
			//	SwcConn.Open();
			//	
			//	gDNSQLStr += "select * from BillBoardLike where BBNO = '" + rBBTID + "';";
			//	
			//	SqlDataReader readerDN;
			//	SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
			//	readerDN = objCmdDN.ExecuteReader();
			//	
			//	
			//	HtmlTableRow trow;
			//	HtmlTableCell tcell;
			//	TBSearchList.Controls.Clear();
			//	while (readerDN.Read())
			//	{
			//		if(readerDN["BBWho"].ToString() == ssUserName)
			//			TF = true;
			//	}
			//	readerDN.Close();
			//	objCmdDN.Dispose();
			//}
			////已按讚=>退讚
			//if(TF == true){
			//	gDNSQLStr = " Delete BillBoardLike where BBNO = '" + rBBTID + "' and BBWho = '" + Session["Name"].ToString() + "'; ";
			//	Btn_Like.Attributes["src"] = "../images/icon/good1.png";
			//}
			////未按讚=>按讚
			//else{
			//	gDNSQLStr = " Insert into BillBoardLike (BBNO,BBWho) values ('" + rBBTID + "','" + Session["Name"].ToString() + "'); ";
			//	Btn_Like.Attributes["src"] = "../images/icon/good2.png";
			//}
			gDNSQLStr = " Insert into BillBoardLike (BBNO,BBWho) values ('" + rBBTID + "','" + ssUserName + "'); ";
			
			using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
			{
				SwcConn.Open();
				
				gDNSQLStr += "select ISNULL(count(*),0) as a from BillBoardLike where BBNO = '" + rBBTID + "';";
				
				SqlDataReader readerDN;
				SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
				readerDN = objCmdDN.ExecuteReader();
				
				
				TBSearchList.Controls.Clear();
				while (readerDN.Read())
				{
					lb_likecount.Text = readerDN["a"].ToString();
				}
				readerDN.Close();
				objCmdDN.Dispose();
			}
		//}
		GetBillBoardMessageAndLike(rBBTID);
		
    }
	
	protected void BTN_SendMessage_Click(object sender, EventArgs e)
    {
		string rBBTID = Request.QueryString["BillBID"] + "";
        //string ssUserName = Session["NAME"] + "";
		if(TXT_Name.Text == "") TXT_Name.Text="匿名";

		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open();
			
			string gDNSQLStr = "INSERT INTO BillBoardMessage (BBNO,BBMessage,BBWho,BBDateTime) values (@BBNO,@BBMessage,@BBWho,getdate());";
			using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = gDNSQLStr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@BBNO", rBBTID));
				cmd.Parameters.Add(new SqlParameter("@BBMessage", TXT_Message.Text));
				cmd.Parameters.Add(new SqlParameter("@BBWho", TXT_Name.Text));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
		//寄信
		SendMailNotice();
		GetBillBoardMessageAndLike(rBBTID);
		TXT_Name.Text = "";
		TXT_Message.Text = "";
    }
	protected void GVBILLBOARDLIST_DataBound(object sender, EventArgs e)
    {
        int aaaaaa = 0;

        foreach (GridViewRow GV_Row in GVBILLBOARDLIST.Rows)
        {

            if (++aaaaaa % 2 == 0)
            {
            }
            else
            {
				GVBILLBOARDLIST.Rows[aaaaaa].Cells[0].Visible = false;
				//GVBILLBOARDLIST.Rows[aaaaaa].Cells[1].Visible = false;
				GVBILLBOARDLIST.Rows[aaaaaa-1].Cells[1].Visible = false;
			}
        }
    }
	private void SendMailNotice()
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();

        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);

        //送出提醒名單：系統管理員、tcge7
        //寄件名單
        string SentMail = "tcge7@geovector.com.tw";
        string SentMailGroup = "";
        for (int i = 1; i < arrayUserId.Length; i++)
        {
            string aUserId = arrayUserId[i];
            string aUserName = arrayUserName[i];
            string aJobTitle = arrayJobTitle[i];
            string aUserMail = arrayUserMail[i];
            string aMBGROUP = arrayMBGROUP[i];

            if (aMBGROUP.Trim() == "系統管理員")
                SentMailGroup = SentMailGroup + ";;" + aUserMail;
        }
        string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
        string ssMailSub01 = "書件平台【" + TXTBBTitle.Text + "】公告於【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】有新留言";
        string ssMailBody01 = "【" + TXTBBTitle.Text + "】於【" + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "】有【" + TXT_Name.Text + "】的新留言，留言內容如下：<br><br>";
		ssMailBody01 = ssMailBody01 + "【" + TXT_Message.Text + "】";
        bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
		
		string[] arraySentMail02 = SentMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub01, ssMailBody01);
    }
}