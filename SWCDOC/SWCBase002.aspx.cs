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

public partial class SWCDOC_SWCBase002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		GBClass001 SBApp = new GBClass001();
		Class20 C20 = new Class20();
		//首次辦帳號
		if(HttpContext.Current.Request.Url.AbsoluteUri.IndexOf("NAME")>-1){
			string name = Request.QueryString["NAME"] + "";
			string cellphone = Request.QueryString["CELLPHONE"] + "";
			string idno = Request.QueryString["IDNO"] + "";
			string email = Request.QueryString["EMAIL"] + "";
			TXTNAME.Text = name;
			TXTCELLPHONE.Text = cellphone;
			TXTIDNO.Text = idno;
			TXTEMAIL.Text = email;
		}else{
			if(Session["UserType"].ToString().Trim()=="05"){
				string idno = Session["IDNO"] + "";
				TXTACCTNO.Text = C20.GetOrganData(idno,"AccountNo");
				TXTNAME.Text = C20.GetOrganData(idno,"Name");
				TXTCELLPHONE.Text = C20.GetOrganData(idno,"Cellphone");
				TXTTELEPHONE.Text = C20.GetOrganData(idno,"Telephone");
				TXTIDNO.Text = idno;
				TXTEMAIL.Text = C20.GetOrganData(idno,"Email");
				DDLUNITNAME.Enabled = false;
				TXTNAME.Enabled = false;
				TXTIDNO.Enabled = false;
				CheckBoxPrivacy.Checked = true; CheckBoxPrivacy.Enabled = false;
				AddNewAcc.Visible = false; SaveAcc.Visible = true;
			}
		}
		
		SBApp.ViewRecord("帳號管理(其他機關)", "view", "");
		ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();
    }
	
	protected void AddNewAcc_Click(object sender, EventArgs e)
    {
		if(CheckInputData() == false){
			Response.Write("<script>alert('請確實填寫');</script>");
			return;
		}
		if(CheckAccount() == true){
			string unitname = DDLUNITNAME.SelectedItem.Text + "";
			string accountno = TXTACCTNO.Text + "";
			string name = TXTNAME.Text + "";
			string cellphone = TXTCELLPHONE.Text + "";
			string telephone = TXTTELEPHONE.Text + "";
			string idno = TXTIDNO.Text + "";
			string email = TXTEMAIL.Text + "";
			string sqlStr = " INSERT INTO Organ (UnitName,AccountNo,Name,Cellphone,Telephone,IDNo,Email,CreateDate,SaveDate,Status) VALUES (@UnitName,@AccountNo,@Name,@Cellphone,@Telephone,@IDNo,@Email,@CreateDate,@SaveDate,@Status); ";
			ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
			using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
			{
				TslmConn.Open();
				using (var cmd = TslmConn.CreateCommand())
				{
					cmd.CommandText = sqlStr;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("@UnitName", unitname));
					cmd.Parameters.Add(new SqlParameter("@AccountNo", accountno));
					cmd.Parameters.Add(new SqlParameter("@Name", name));
					cmd.Parameters.Add(new SqlParameter("@Cellphone", cellphone));
					cmd.Parameters.Add(new SqlParameter("@Telephone", telephone));
					cmd.Parameters.Add(new SqlParameter("@IDNo", idno));
					cmd.Parameters.Add(new SqlParameter("@Email", email));
					cmd.Parameters.Add(new SqlParameter("@CreateDate", DateTime.Now));
					cmd.Parameters.Add(new SqlParameter("@SaveDate", DateTime.Now));
					cmd.Parameters.Add(new SqlParameter("@Status", "申請中"));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
		Response.Write("<script>alert('帳號申請中'); location='../Default.aspx';</script>");
    }
	protected void SaveAcc_Click(object sender, EventArgs e)
    {
		if(CheckInputData() == false){
			Response.Write("<script>alert('請確實填寫');</script>");
			return;
		}
		string unitname = DDLUNITNAME.SelectedItem.Text;
		string accountno = TXTACCTNO.Text;
		string name = TXTNAME.Text;
		string cellphone = TXTCELLPHONE.Text;
		string telephone = TXTTELEPHONE.Text;
		string idno = TXTIDNO.Text;
		string email = TXTEMAIL.Text;
		string sqlStr = " Update Organ set UnitName = @UnitName,AccountNo = @AccountNo,Cellphone = @Cellphone,Telephone = @Telephone,Email = @Email,SaveDate = @SaveDate where IDNo = @IDNo ";
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
		{
			TslmConn.Open();
			using (var cmd = TslmConn.CreateCommand())
			{
				cmd.CommandText = sqlStr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@UnitName", unitname));
				cmd.Parameters.Add(new SqlParameter("@AccountNo", accountno));
				cmd.Parameters.Add(new SqlParameter("@Name", name));
				cmd.Parameters.Add(new SqlParameter("@Cellphone", cellphone));
				cmd.Parameters.Add(new SqlParameter("@Telephone", telephone));
				cmd.Parameters.Add(new SqlParameter("@IDNo", idno));
				cmd.Parameters.Add(new SqlParameter("@Email", email));
				cmd.Parameters.Add(new SqlParameter("@SaveDate", DateTime.Now));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
		Response.Write("<script>alert('已儲存'); location.href='HaloPage001.aspx';</script>");
	}
	protected bool CheckInputData()
    {
		if(TXTACCTNO.Text == "") return false;
		if(TXTNAME.Text == "") return false;
		if(TXTCELLPHONE.Text == "") return false;
		if(TXTIDNO.Text == "") return false;
		if(TXTEMAIL.Text == "") return false;
		if(CheckBoxPrivacy.Checked == false) return false;
		return true;
	}
	protected bool CheckAccount()
    {
		bool _re = true;
		string idno = TXTIDNO.Text + "";
		
        string sqlStr = " select * from Organ where IDNo = @IDNO ; ";
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
			SWCConn.Open();
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = sqlStr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@IDNo", idno));
				#endregion
				cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            _re = false;
                        }
                    reader.Close();
                }
				cmd.Cancel();
			}
        }
		return _re;
    }
}