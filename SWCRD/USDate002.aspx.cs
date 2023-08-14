using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SWCRD_USDate002 : System.Web.UI.Page
{
	protected bool a = false;
	protected void Page_Init(object sender, EventArgs e)
	{
		GenerateTable();
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		string ssUserID = Session["ID"] + "";
		string ssUserType = Session["UserType"] + "";

		if (!IsPostBack)
		{
			if (ssUserType != "01")
				Response.Write("<script>location.href='../Default.aspx'</script>");
		}
	}

	protected void GenerateTable()
	{
		PanelTable.Controls.Clear();
		PanelTable.Controls.Add(new LiteralControl("<br>"));
		PanelTable.Controls.Add(new LiteralControl("<br>"));

		string ssUserID = Session["ID"] + "";
		string ssUserPW = Session["PW"] + "";
		string sql = "select * from SWCCASE where ";
		sql += " (SWC013ID like '%'+@SWC013ID+'%' ";
		//sql += " OR (SWC013TEL like '%'+@SWC013TEL+'%' and ISNULL(SWC013TEL,'')!='') ";
		sql += " OR (SWC016 = @SWC016 and ISNULL(SWC016,'')!='')) ";

		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open();

			using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = sql;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@SWC013ID", ssUserID));
				//cmd.Parameters.Add(new SqlParameter("@SWC013TEL", ssUserPW));
				cmd.Parameters.Add(new SqlParameter("@SWC016", ssUserPW));
				#endregion
				cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
				{
					if (readerSWC.HasRows)
					{
						int i = 1;
						while (readerSWC.Read())
						{
							string SWC000 = readerSWC["SWC000"] + "";
							string SWC002 = readerSWC["SWC002"] + "";
							string SWC004 = readerSWC["SWC004"] + "";
							string SWC005 = readerSWC["SWC005"] + "";
							string SWC015 = readerSWC["SWC015"] + "";
							string SWC016 = readerSWC["SWC016"] + "";
							string SWC108 = readerSWC["SWC108"] + "";

							PanelTable.Controls.Add(new LiteralControl("<fieldset>"));
							PanelTable.Controls.Add(new LiteralControl("<legend>" + i.ToString() + "</legend>"));
							PanelTable.Controls.Add(new LiteralControl("<div class='upmin'>"));
							PanelTable.Controls.Add(new LiteralControl("<table class='tetst'>"));
							//tr
							PanelTable.Controls.Add(new LiteralControl("<tr>"));
							
							
							PanelTable.Controls.Add(new LiteralControl("<th style='white-space:nowrap;'><b>案件編號</b></th>"));
							PanelTable.Controls.Add(new LiteralControl("<td>"));
							PanelTable.Controls.Add(new LiteralControl("<span>"));
							Label lb_swc000 = new Label();
							lb_swc000.Text = SWC000;
							lb_swc000.ID = "LB_SWC000_" + i.ToString();
							PanelTable.Controls.Add(lb_swc000);
							PanelTable.Controls.Add(new LiteralControl("</span>"));
							PanelTable.Controls.Add(new LiteralControl("</td>"));
							
							
							PanelTable.Controls.Add(new LiteralControl("<th style='white-space:nowrap;'><b>水保局編號</b></th>"));
							PanelTable.Controls.Add(new LiteralControl("<td>"));
							PanelTable.Controls.Add(new LiteralControl("<span>"));
							Label lb_swc002 = new Label();
							lb_swc002.Text = SWC002;
							lb_swc002.ID = "LB_SWC002_" + i.ToString();
							PanelTable.Controls.Add(lb_swc002);
							PanelTable.Controls.Add(new LiteralControl("</span>"));
							PanelTable.Controls.Add(new LiteralControl("</td>"));

							PanelTable.Controls.Add(new LiteralControl("<th style='white-space:nowrap;'><b>案件狀態</b></th>"));
							PanelTable.Controls.Add(new LiteralControl("<td><span>" + SWC004 + "</span></td>"));

							PanelTable.Controls.Add(new LiteralControl("</tr>"));
							//tr
							
							PanelTable.Controls.Add(new LiteralControl("<tr style='margin-top:20px;'>"));

							PanelTable.Controls.Add(new LiteralControl("<th colspan='1'><b style=' width:84px; line-height:1.2; display:block;'>水土保持書件名稱</b></th>")); ;
							PanelTable.Controls.Add(new LiteralControl("<td colspan='5'><span style='display:block; padding-bottom:3px; vertical-align:middle;'>" + SWC005 + "</span></td>"));
							
							PanelTable.Controls.Add(new LiteralControl("</tr>"));
							
							
							PanelTable.Controls.Add(new LiteralControl("</table>"));
							PanelTable.Controls.Add(new LiteralControl("</div>"));

							PanelTable.Controls.Add(new LiteralControl("<div style='clear:both'></div>"));
							PanelTable.Controls.Add(new LiteralControl("<span class='upTT'>義務人資訊</span>"));
							PanelTable.Controls.Add(new LiteralControl("<br>"));
							PanelTable.Controls.Add(new LiteralControl("<br>"));





							a = false;
							//義務人資料
							GenerateSWCObligor(SWC000, i.ToString());



							PanelTable.Controls.Add(new LiteralControl("<br />"));
							PanelTable.Controls.Add(new LiteralControl("<br />"));

							PanelTable.Controls.Add(new LiteralControl("<span class='upTT'>聯絡人</span>"));
							//PanelTable.Controls.Add(new LiteralControl("<span class='upTTR'>帶入個人資料</span>"));

							PanelTable.Controls.Add(new LiteralControl("<br />"));
							PanelTable.Controls.Add(new LiteralControl("<br />"));

							PanelTable.Controls.Add(new LiteralControl("<table class='updateA'>"));
							PanelTable.Controls.Add(new LiteralControl("<tr>"));
							//PanelTable.Controls.Add(new LiteralControl("<th>序號</th>"));
							PanelTable.Controls.Add(new LiteralControl("<th>姓名</th>"));
							PanelTable.Controls.Add(new LiteralControl("<th style='width:20%;'>手機</th>"));
							PanelTable.Controls.Add(new LiteralControl("<th>E-mail</th>"));
							PanelTable.Controls.Add(new LiteralControl("</tr>"));
							PanelTable.Controls.Add(new LiteralControl("<tr>"));
							//PanelTable.Controls.Add(new LiteralControl("<td>1</td>"));

							PanelTable.Controls.Add(new LiteralControl("<td>"));
							TextBox tb_name = new TextBox();
							tb_name.Text = SWC015;
							tb_name.ID = "TB_Name" + i.ToString();
							if(SWC016 != ssUserPW && !a) tb_name.Enabled = false;
							PanelTable.Controls.Add(tb_name);
							PanelTable.Controls.Add(new LiteralControl("</td>"));

							PanelTable.Controls.Add(new LiteralControl("<td>"));
							TextBox tb_phone = new TextBox();
							tb_phone.Text = SWC016;
							tb_phone.ID = "TB_Phone" + i.ToString();
							if(SWC016 != ssUserPW && !a) tb_phone.Enabled = false;
							PanelTable.Controls.Add(tb_phone);
							PanelTable.Controls.Add(new LiteralControl("</td>"));

							PanelTable.Controls.Add(new LiteralControl("<td>"));
							TextBox tb_email = new TextBox();
							tb_email.Text = SWC108;
							tb_email.ID = "TB_Email" + i.ToString();
							if(SWC016 != ssUserPW && !a) tb_email.Enabled = false;
							PanelTable.Controls.Add(tb_email);
							PanelTable.Controls.Add(new LiteralControl("</td>"));

							PanelTable.Controls.Add(new LiteralControl("<tr>"));
							PanelTable.Controls.Add(new LiteralControl("</table>"));

							PanelTable.Controls.Add(new LiteralControl("</fieldset>"));
							PanelTable.Controls.Add(new LiteralControl("<br>"));
							PanelTable.Controls.Add(new LiteralControl("<div class='center'>"));

							Button btn = new Button();
							btn.Text = "更新此筆案件";
							btn.ID = "Btn_Update" + i.ToString();
							btn.Click += new EventHandler(this.Btn_Update_Click);
							PanelTable.Controls.Add(btn);

							//PanelTable.Controls.Add(new LiteralControl("<input type='submit' id='Btn_Update"+i.ToString()+"' value='更新此筆案件' runat='server' OnClick='Btn_Update_Click' />"));
							PanelTable.Controls.Add(new LiteralControl("</div>"));
							PanelTable.Controls.Add(new LiteralControl("<br>"));
							PanelTable.Controls.Add(new LiteralControl("<br>"));

							i++;
						}
					}
					readerSWC.Close();
				}
				cmd.Cancel();
			}
		}

	}
	protected void GenerateSWCObligor(string SWC000, string i)
	{
		string ssUserID = Session["ID"] + "";
		string ssUserPW = Session["PW"] + "";
		
		Table table = new Table();
		table.CssClass = "updateA";
		table.ID = "Table_Obligor" + i.ToString();

		int m = 1;
		string sql_1 = " select * from SWCObligor where SWC000=@SWC000 order by 序號 ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open(); using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = sql_1;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@SWC000", SWC000));
				#endregion
				cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
				{
					if (readerSWC.HasRows)
					{
						TableHeaderRow thr = new TableHeaderRow();
						for (int k = 0; k < 5; k++)
						{
							TableHeaderCell thc = new TableHeaderCell();
							if (k == 0) thc.Text = "序號";
							if (k == 1) thc.Text = "姓名";
							if (k == 2) thc.Text = "身分證字號/統一編號";
							if (k == 3) thc.Text = "手機";
							if (k == 4) thc.Text = "地址";
							thr.Cells.Add(thc);
						}
						table.Rows.Add(thr);

						while (readerSWC.Read())
						{
							string no = readerSWC["序號"] + "";
							string SWC013 = readerSWC["SWC013"] + "";
							string SWC013ID = readerSWC["SWC013ID"] + "";
							string SWC013TEL = readerSWC["SWC013TEL"] + "";
							string SWC014 = readerSWC["SWC014Zip"] + "" + readerSWC["SWC014City"] + "" + readerSWC["SWC014District"] + "" + readerSWC["SWC014Address"] + "";

							TableRow tr = new TableRow();
							for (int l = 0; l < 5; l++)
							{
								TableCell tc = new TableCell();
								TextBox tb = new TextBox();
								if (l == 0) tc.Text = no.ToString();
								if (l == 1) tc.Text = SWC013.ToString();
								if (l == 2) tc.Text = SWC013ID.ToString();
								if (l == 3)
								{
									if(SWC013ID != ssUserID && SWC013TEL != ssUserPW) 
									{
										tb.Enabled = false;
									}
									else
									{
										a = true;
									}
									tb.ID = "TB_Phone_1_" + i.ToString() + "_" + m.ToString();
									tb.Text = SWC013TEL.ToString();
									tc.Controls.Add(tb);
								}
								if (l == 4) tc.Text = SWC014.ToString();
								tr.Cells.Add(tc);
							}
							table.Rows.Add(tr);
							m++;
						}
					}
					readerSWC.Close();
				}
				cmd.Cancel();
			}
		}



		PanelTable.Controls.Add(table);
	}

	protected void Btn_Update_Click(object sender, EventArgs e)
	{
		Button btn = (Button)sender;
		string name = btn.ID.Replace("Btn_Update", "TB_Name");
		string phone = btn.ID.Replace("Btn_Update", "TB_Phone");
		string email = btn.ID.Replace("Btn_Update", "TB_Email");
		string swc000 = btn.ID.Replace("Btn_Update", "LB_SWC000_");
		string swc002 = btn.ID.Replace("Btn_Update", "LB_SWC002_");
		string table_bligor = btn.ID.Replace("Btn_Update", "Table_Obligor");

		TextBox TB_Name = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(name);
		TextBox TB_Phone = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(phone);
		TextBox TB_Email = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(email);
		Label LB_SWC002 = (Label)Master.FindControl("ContentPlaceHolder1").FindControl(swc002);
		Table Obligor = (Table)Master.FindControl("ContentPlaceHolder1").FindControl(table_bligor);
		
		
		//聯絡人資料
		//Response.Write("<script>alert('"+TB_Name.Text+"')</script>");
		//Response.Write("<script>alert('"+TB_Phone.Text+"')</script>");
		//Response.Write("<script>alert('"+TB_Email.Text+"')</script>");
		
		//水保局編號
		//Response.Write("<script>alert('"+LB_SWC002.Text+"')</script>");
		
		//義務人
		//Response.Write("<script>alert('" + Obligor.Rows.Count-1 + "')</script>");
		//Response.Write("<script>alert('" + Obligor.Rows[1].Cells[0].Text + "')</script>");
		//Response.Write("<script>alert('" + Obligor.Rows[1].Cells[1].Text + "')</script>");
		//Response.Write("<script>alert('" + Obligor.Rows[1].Cells[2].Text + "')</script>");
		//string phone_1 = btn.ID.Replace("Btn_Update", "TB_Phone_1_");
		//phone_1 += "_1";
		//TextBox TB_Phone_1 = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(phone_1);
		//Response.Write("<script>alert('"+TB_Phone_1.Text+"')</script>");
		//Response.Write("<script>alert('" + Obligor.Rows[1].Cells[4].Text + "')</script>");
		string swc013tel = "";
		string sql_1 = "";
		for(int i = 1; i <= Obligor.Rows.Count-1; i++)
		{
			string phone_1 = btn.ID.Replace("Btn_Update", "TB_Phone_1_");
			phone_1 += "_" + i.ToString();
			TextBox TB_Phone_1 = (TextBox)Master.FindControl("ContentPlaceHolder1").FindControl(phone_1);
			swc013tel = i == Obligor.Rows.Count-1 ? swc013tel + TB_Phone_1.Text : swc013tel + TB_Phone_1.Text + ";";
			sql_1 = "Update SWCObligor set SWC013TEL = @SWC013TEL where SWC000 = @SWC000 and 序號 = @no;";
			ConnectionStringSettings connectionStringSwc1 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
			using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc1.ConnectionString))
			{
				SwcConn.Open();using (var cmd = SwcConn.CreateCommand())
				{
					cmd.CommandText = sql_1;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("@SWC013TEL", TB_Phone_1.Text));
					cmd.Parameters.Add(new SqlParameter("@SWC000", swc000));
					cmd.Parameters.Add(new SqlParameter("@no", Obligor.Rows[i].Cells[0].Text));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
		
		string sql_2 = " update SWCCASE set SWC013TEL=@SWC013TEL,SWC015=@SWC015,SWC016=@SWC016,SWC108=@SWC108 where SWC002=@SWC002; ";
		sql_2 += " update tslm2.dbo.SWCSWC set SWC013TEL=@SWC013TEL,SWC15=@SWC015,SWC16=@SWC016,SWC108=@SWC108 where SWC02=@SWC002; ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
		    SwcConn.Open();using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = sql_2;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@SWC002", LB_SWC002.Text));
				cmd.Parameters.Add(new SqlParameter("@SWC015", TB_Name.Text));
				cmd.Parameters.Add(new SqlParameter("@SWC013TEL", swc013tel));
				cmd.Parameters.Add(new SqlParameter("@SWC016", TB_Phone.Text));
				cmd.Parameters.Add(new SqlParameter("@SWC108", TB_Email.Text));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}

	}
}