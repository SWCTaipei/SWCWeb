using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCOrganAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		GBClass001 SBApp = new GBClass001();
		Class20 C20 = new Class20();
		string ssUserType = Session["UserType"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
		string idno = Session["IDNO"] + "";

		
		if (!IsPostBack)
        {
			switch (ssUserType)
			{
				case "05":
					if(C20.GetOrganData(idno,"Role")!="系統管理員"){
						Response.Write("<script>alert('您沒有權限'); location.href='HaloPage001.aspx';</script>");
					}
					break;
				default:
					Response.Redirect("HaloPage001.aspx");
					break;
			}
			GenerateDDL();
			string accountno = Request.QueryString["ID"] + "";
			LBACCTNO.Text = accountno;
			LBNAME.Text = C20.GetOrganData2(accountno,"Name");
			LBUNITNAME.Text = C20.GetOrganData2(accountno,"UnitName");
			LBCELLPHONE.Text = C20.GetOrganData2(accountno,"Cellphone");
			LBTELEPHONE.Text = C20.GetOrganData2(accountno,"Telephone");
			LBIDNO.Text = C20.GetOrganData2(accountno,"AccountNo");
			LBEMAIL.Text = C20.GetOrganData2(accountno,"Email");
			
			TXTDEPARTMENT.Text = C20.GetOrganData2(accountno,"Department");
			TXTJOBTITLE.Text = C20.GetOrganData2(accountno,"JobTitle");
			DDLROLE.SelectedValue = C20.GetOrganData2(accountno,"Role");
			
			DDLSTATUS.SelectedValue = C20.GetOrganData2(accountno,"Status");
			TXTREASON.Text = C20.GetOrganData2(accountno,"Reason");
		}
		
		SBApp.ViewRecord("帳號審核(其他機關)", "view", "");
		ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();
    }
	
	protected void GenerateDDL()
	{
		string[] array_DropDownList1 = new string[] { "一般使用者", "系統管理員" };
        DDLROLE.DataSource = array_DropDownList1;
        DDLROLE.DataBind();
        DDLROLE.SelectedValue = "一般使用者";
		
		string[] array_DropDownList2 = new string[] { "請選擇", "已開通", "駁回", "停用" };
        DDLSTATUS.DataSource = array_DropDownList2;
        DDLSTATUS.DataBind();
        DDLSTATUS.SelectedValue = "請選擇";
	}
	protected void SaveAcc_Click(object sender, EventArgs e)
    {
		string accountno = LBACCTNO.Text;
		string department = TXTDEPARTMENT.Text;
		string jobtitle = TXTJOBTITLE.Text;
		string role = DDLROLE.SelectedValue;
		string status = DDLSTATUS.SelectedValue;
		string reason = TXTREASON.Text;
		
		string sqlStr = " Update Organ set Department = @Department,JobTitle = @JobTitle,Role = @Role,Status = @Status,Reason = @Reason where AccountNo = @AccountNo ";
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
		{
			SWCConn.Open();
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = sqlStr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@Department", department));
				cmd.Parameters.Add(new SqlParameter("@JobTitle", jobtitle));
				cmd.Parameters.Add(new SqlParameter("@Role", role));
				cmd.Parameters.Add(new SqlParameter("@Status", status));
				cmd.Parameters.Add(new SqlParameter("@Reason", reason));
				cmd.Parameters.Add(new SqlParameter("@AccountNo", accountno));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
		Response.Write("<script>alert('資料已送出'); location.href='SWCOrganList.aspx';</script>");
	}
	protected void GoHome_Click(object sender, EventArgs e)
    {
		Response.Redirect("SWCOrganList.aspx");
	}
	protected bool CheckInputData()
    {
		return true;
	}
}