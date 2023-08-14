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

public partial class SWCDOC_SWCProxy002 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack) { 
			getGridViewData();
			getGridViewRecordData();
		}
    }
	protected void DataLock_Click(object sender, EventArgs e)
    {
		string ssUserID = Session["ID"] + "";
		if(ssUserID == "") return;
		//檢查是否存在於別人或自己已啟用該代理人 true=>存在 false=>不存在
		if(!CheckExistsOrNot(""))
		{
			ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
			using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
			{
				Conn.Open();
	
				string strSQL = "Insert into SWCProxy(IDNO,Proxy_Type,Proxy_Name,Proxy_ID,Proxy_PhoneNo,Proxy_Status) Values (@IDNO,@Proxy_Type,@Proxy_Name,@Proxy_ID,@Proxy_PhoneNo,@Proxy_Status) ;";
				strSQL += "Insert into SWCProxy_record(IDNO,Proxy_Type,Proxy_Name,Proxy_ID,Proxy_PhoneNo,Proxy_Status,DateTime) Values (@IDNO,@Proxy_Type,@Proxy_Name,@Proxy_ID,@Proxy_PhoneNo,@Proxy_Status1,getdate()) ;";
				using (var cmd = Conn.CreateCommand())
				{
					cmd.CommandText = strSQL;
					cmd.Parameters.Add(new SqlParameter("@IDNO", ssUserID));
					cmd.Parameters.Add(new SqlParameter("@Proxy_Type", "公會"));
					cmd.Parameters.Add(new SqlParameter("@Proxy_Name", TXTProxyName.Text));
					cmd.Parameters.Add(new SqlParameter("@Proxy_ID", TXTProxyID.Text));
					cmd.Parameters.Add(new SqlParameter("@Proxy_PhoneNo", TXTProxyPhoneNo.Text));
					cmd.Parameters.Add(new SqlParameter("@Proxy_Status", "啟用"));
					cmd.Parameters.Add(new SqlParameter("@Proxy_Status1", "新增"));
					cmd.ExecuteNonQuery();
				}
			}
			TXTProxyName.Text = "";
			TXTProxyID.Text = "";
			TXTProxyPhoneNo.Text = "";
			getGridViewData();
			getGridViewRecordData();
		}
		else
		{
			Response.Write("<script>alert('該代理人已存在且啟用或你已經新增該代理人，無法新增代理人。');</script>");
		}
	}
	private void getGridViewData()
    {
		string ssUserID = Session["ID"] + "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();

            string strSQL = "select * from SWCProxy where IDNO=@IDNO ;";
			using (var cmd = Conn.CreateCommand())
            {
				cmd.CommandText = strSQL;
                cmd.Parameters.Add(new SqlParameter("@IDNO", ssUserID));
                cmd.ExecuteNonQuery();
				
				DataTable tbPROXY = new DataTable();
				DataTable TB_Proxy = new DataTable();
				TB_Proxy.Columns.Add(new DataColumn("Proxy_Name", typeof(string)));
				TB_Proxy.Columns.Add(new DataColumn("Proxy_ID", typeof(string)));
				TB_Proxy.Columns.Add(new DataColumn("Proxy_PhoneNo", typeof(string)));
				TB_Proxy.Columns.Add(new DataColumn("Proxy_Status", typeof(string)));
				ViewState["SwcProxy"] = TB_Proxy;
				tbPROXY = (DataTable)ViewState["SwcProxy"];
					
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
						{
							string sProxy_Name = reader["Proxy_Name"] + "";
							string sProxy_ID = reader["Proxy_ID"] + "";
							string sProxy_PhoneNo = reader["Proxy_PhoneNo"] + "";
							string sProxy_Status = reader["Proxy_Status"] + "";
							
							
							DataRow ProxyRow = tbPROXY.NewRow();
							
							ProxyRow["Proxy_Name"] = sProxy_Name;
							ProxyRow["Proxy_ID"] = sProxy_ID;
							ProxyRow["Proxy_PhoneNo"] = sProxy_PhoneNo;
							ProxyRow["Proxy_Status"] = sProxy_Status;
							
							tbPROXY.Rows.Add(ProxyRow);
							ProxyList.DataSource = tbPROXY;
							ProxyList.DataBind();
						}
                }
			}
        }
    }
	private void getGridViewRecordData()
    {
		string ssUserID = Session["ID"] + "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();

            string strSQL = "select * from SWCProxy_record where IDNO=@IDNO order by DateTime desc;";
			using (var cmd = Conn.CreateCommand())
            {
				cmd.CommandText = strSQL;
                cmd.Parameters.Add(new SqlParameter("@IDNO", ssUserID));
                cmd.ExecuteNonQuery();
				
				DataTable tbPROXY_record = new DataTable();
				DataTable TB_Proxy_record = new DataTable();
				TB_Proxy_record.Columns.Add(new DataColumn("Proxy_Name", typeof(string)));
				TB_Proxy_record.Columns.Add(new DataColumn("Proxy_ID", typeof(string)));
				TB_Proxy_record.Columns.Add(new DataColumn("Proxy_PhoneNo", typeof(string)));
				TB_Proxy_record.Columns.Add(new DataColumn("Proxy_Status", typeof(string)));
				TB_Proxy_record.Columns.Add(new DataColumn("DateTime", typeof(string)));
				ViewState["SwcProxy_record"] = TB_Proxy_record;
				tbPROXY_record = (DataTable)ViewState["SwcProxy_record"];
					
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
						{
							string sProxy_Name = reader["Proxy_Name"] + "";
							string sProxy_ID = reader["Proxy_ID"] + "";
							string sProxy_PhoneNo = reader["Proxy_PhoneNo"] + "";
							string sProxy_Status = reader["Proxy_Status"] + "";
							string sDateTime = reader["DateTime"] + "";
							
							
							DataRow ProxyRow_record = tbPROXY_record.NewRow();
							
							ProxyRow_record["Proxy_Name"] = sProxy_Name;
							ProxyRow_record["Proxy_ID"] = sProxy_ID;
							ProxyRow_record["Proxy_PhoneNo"] = sProxy_PhoneNo;
							ProxyRow_record["Proxy_Status"] = sProxy_Status;
							ProxyRow_record["DateTime"] = sDateTime;
							
							tbPROXY_record.Rows.Add(ProxyRow_record);
							ProxyList_record.DataSource = tbPROXY_record;
							ProxyList_record.DataBind();
						}
                }
			}
        }
    }
	protected void ProxyList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
		string ssUserID = Session["ID"] + "";
		if(ssUserID == "") return;
		
        string jAct = e.CommandName;
        if(e.CommandName=="update_ProxyStatus")
		{
			//檢查是否存在於別人或自己已啟用該代理人 true=>存在 false=>不存在
			if(!CheckExistsOrNot("change"))
			{
				int index = Convert.ToInt32(e.CommandArgument);
				
				//GridViewRow row = ProxyList.Rows[index];
				//string sProxy_Name = row.Cells[0].Text;
				//string sProxy_ID = row.Cells[1].Text;
				//string sProxy_PhoneNo = row.Cells[2].Text;
				//string sProxy_Status = row.Cells[3].Text;
				
				TextBox Proxy_Name = (TextBox)ProxyList.Rows[index].Cells[0].FindControl("Proxy_Name");
				TextBox Proxy_ID = (TextBox)ProxyList.Rows[index].Cells[1].FindControl("Proxy_ID");
				TextBox Proxy_PhoneNo = (TextBox)ProxyList.Rows[index].Cells[2].FindControl("Proxy_PhoneNo");
				DropDownList Proxy_Status = (DropDownList)ProxyList.Rows[index].Cells[3].FindControl("Proxy_Status");
				string sProxy_Name = Proxy_Name.Text;
				string sProxy_ID = ProxyList.Rows[index].Cells[1].Text;
				string sProxy_PhoneNo = Proxy_PhoneNo.Text;
				string sProxy_Status = Proxy_Status.SelectedItem.Value;
				//Response.Write(sProxy_Name);
				//Response.Write(sProxy_ID);
				//Response.Write(sProxy_PhoneNo);
				//Response.Write(sProxy_Status);
				ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
				using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
				{
					Conn.Open();
		
					string strSQL = "update SWCProxy set Proxy_Name=@Proxy_Name,Proxy_PhoneNo=@Proxy_PhoneNo,Proxy_Status=@Proxy_Status where IDNO=@IDNO and Proxy_ID=@Proxy_ID ;";
					strSQL += " Insert into SWCProxy_record (IDNO,Proxy_Type,Proxy_Name,Proxy_ID,Proxy_PhoneNo,Proxy_Status,DateTime) values (@IDNO,@Proxy_Type,@Proxy_Name,@Proxy_ID,@Proxy_PhoneNo,@Proxy_Status,getdate());";
					using (var cmd = Conn.CreateCommand())
					{
						cmd.CommandText = strSQL;
						cmd.Parameters.Add(new SqlParameter("@Proxy_Name", sProxy_Name));
						cmd.Parameters.Add(new SqlParameter("@Proxy_PhoneNo", sProxy_PhoneNo));
						cmd.Parameters.Add(new SqlParameter("@Proxy_Status", sProxy_Status));
						cmd.Parameters.Add(new SqlParameter("@IDNO", ssUserID));
						cmd.Parameters.Add(new SqlParameter("@Proxy_ID", sProxy_ID));
						cmd.Parameters.Add(new SqlParameter("@Proxy_Type", "公會"));
						cmd.ExecuteNonQuery();
					}
				}
				getGridViewData();
				getGridViewRecordData();
			}
			else
			{
				Response.Write("<script>alert('該代理人已存在且啟用，無法變更代理人狀態。');</script>");
			}
		}
    }
	protected bool CheckExistsOrNot(string type)
	{
		string ssUserID = Session["ID"] + "";
		
		bool re = false;
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();

            string strSQL = "select * from SWCProxy where (Proxy_ID=@Proxy_ID and Proxy_Status=@Proxy_Status and Proxy_Type=@Proxy_Type) ";
			if(type != "change")
				strSQL +=  " or (Proxy_ID=@Proxy_ID and IDNO=@IDNO) ";
			using (var cmd = Conn.CreateCommand())
            {
				cmd.CommandText = strSQL;
                cmd.Parameters.Add(new SqlParameter("@IDNO", ssUserID));
                cmd.Parameters.Add(new SqlParameter("@Proxy_ID", TXTProxyID.Text));
                cmd.Parameters.Add(new SqlParameter("@Proxy_Status", "啟用"));
                cmd.Parameters.Add(new SqlParameter("@Proxy_Type", "公會"));
                cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        re = true;
                }
			}
        }
		return re;
	}
}