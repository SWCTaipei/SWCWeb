using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SWCDOC_PWA_list1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
		if (!IsPostBack)
        {
			SWCDTList();
			if(ssUserName == "") Response.Redirect("SWC000.aspx");
		}
    }
	
	//PWA列表
    public void SWCDTList()
    {
        string ssUserID = Session["ID"] + "";
		string tSWC024ID = "";
		string tSWC045ID = "";
        string ssUserGuild2 = Session["ETU_Guild02"] + "";
        string ssUserGuild3 = Session["ETU_Guild03"] + "";
        PanelTable.Controls.Clear();
		
		HtmlTableRow trow;
        HtmlTableCell tcell;
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        string sqlStr = "select * from SWCCASE where (SWC004 = '施工中' or SWC004 = '停工中') and (SWC024ID = '" + ssUserGuild2 + "' or SWC024ID = '" + ssUserGuild3 + "' or SWC045ID = '" + ssUserID + "');";
        using (SqlConnection Conn = new SqlConnection(connectionString.ConnectionString))
        {
            Conn.Open();
            using (var cmd = Conn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                #endregion
                cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
					{
						int i=1;
						while (reader.Read())
						{
							tSWC024ID = reader["SWC024ID"] + "";
							tSWC045ID = reader["SWC045ID"] + "";
							
							HtmlTable table = new HtmlTable();
							var a = new HtmlGenericControl("a");
			
							//table.Attributes["id"] = "TB_" + i.ToString();
							table.Attributes["class"] = "listTB";
							table.Attributes["id"] = "table_" + i.ToString();
							PanelTable.Controls.Add(table);
							
							trow = new HtmlTableRow();
							table.Controls.Add(trow);
							
							tcell = new HtmlTableCell("td");
							tcell.Attributes["colspan"] = "3";
							tcell.InnerHtml = reader["SWC002"] + "";
							trow.Controls.Add(tcell);
							
							
							
							
							trow = new HtmlTableRow();
							table.Controls.Add(trow);
							
							tcell = new HtmlTableCell("th");
							tcell.InnerHtml = i.ToString() + "、";
							trow.Controls.Add(tcell);
							
							tcell = new HtmlTableCell("th");
							tcell.Attributes["colspan"] = "2";
							tcell.InnerHtml = reader["SWC005"] + "";
							trow.Controls.Add(tcell);
							
							
							
							
							
							
							trow = new HtmlTableRow();
							table.Controls.Add(trow);
							
							tcell = new HtmlTableCell("td");
							tcell.Attributes["colspan"] = "3";
							tcell.Attributes["class"] = "none";
							tcell.InnerHtml = reader["SWC000"] + "";
							trow.Controls.Add(tcell);
							
							
							
							
							
							
							trow = new HtmlTableRow();
							table.Controls.Add(trow);
							
							tcell = new HtmlTableCell("td");
							tcell.Attributes["colspan"] = "3";
							tcell.Attributes["class"] = "none";
							tcell.InnerHtml = reader["SWC025"] + "";
							trow.Controls.Add(tcell);
							
							
							
							trow = new HtmlTableRow();
							table.Controls.Add(trow);
							
							tcell = new HtmlTableCell("td");
							tcell.Attributes["colspan"] = "3";
							tcell.Attributes["class"] = "none";
							tcell.InnerHtml = reader["SWC012"] + "";
							trow.Controls.Add(tcell);
							
							
							
							
							
							trow = new HtmlTableRow();
							table.Controls.Add(trow);
							
							tcell = new HtmlTableCell("td");
							tcell.Attributes["colspan"] = "3";
							tcell.Attributes["class"] = "none";
							tcell.InnerHtml = reader["SWC008"] + "";
							trow.Controls.Add(tcell);
							
							
							
							
							//SWCDT003
							if (tSWC024ID == ssUserGuild2 || tSWC024ID == ssUserGuild3)
							{
								trow = new HtmlTableRow();
								table.Controls.Add(trow);
								
								tcell = new HtmlTableCell("td");
								tcell.InnerHtml = "";
								trow.Controls.Add(tcell);
								
								tcell = new HtmlTableCell("td");
								tcell.InnerHtml = "施工監督檢查";
								trow.Controls.Add(tcell);
								
								tcell = new HtmlTableCell("td");
								HyperLink hlf1 = new HyperLink();
								hlf1.Text = "詳情";
								hlf1.Target="_blank";
								hlf1.NavigateUrl = "PWA_SWCDT003/SWCDT003PWA.aspx?SWCNO=" + reader["SWC000"] + "&DTLNO=AddNew";
								tcell.Controls.Add(hlf1);
								trow.Controls.Add(tcell);
							}
							
							
							
							//SWCDT004
							if (tSWC045ID == ssUserID)
							{
								trow = new HtmlTableRow();
								table.Controls.Add(trow);
								
								tcell = new HtmlTableCell("td");
								tcell.InnerHtml = "";
								trow.Controls.Add(tcell);
								
								tcell = new HtmlTableCell("td");
								tcell.InnerHtml = "颱風及豪雨設施自主檢查";
								trow.Controls.Add(tcell);
								
								tcell = new HtmlTableCell("td");
								HyperLink hlf2 = new HyperLink();
								hlf2.Text = "詳情";
								hlf2.Target="_blank";
								hlf2.NavigateUrl = "PWA_SWCDT004/SWCDT004PWA.aspx?SWCNO=" + reader["SWC000"] + "&DTLNO=AddNew";
								tcell.Controls.Add(hlf2);
								trow.Controls.Add(tcell);
							}
							
							
							
							
							//SWCDT006
							if (tSWC024ID == ssUserGuild2 || tSWC024ID == ssUserGuild3)
							{
								trow = new HtmlTableRow();
								table.Controls.Add(trow);
								
								tcell = new HtmlTableCell("td");
								tcell.InnerHtml = "";
								trow.Controls.Add(tcell);
								
								tcell = new HtmlTableCell("td");
								tcell.InnerHtml = "完工檢查";
								trow.Controls.Add(tcell);
								
								tcell = new HtmlTableCell("td");
								HyperLink hlf3 = new HyperLink();
								hlf3.Text = "詳情";
								hlf3.Target="_blank";
								hlf3.NavigateUrl = "PWA_SWCDT006/SWCDT006PWA.aspx?SWCNO=" + reader["SWC000"] + "&DTLNO=AddNew";
								tcell.Controls.Add(hlf3);
								trow.Controls.Add(tcell);
							}
							
							
							
							
							i++;
						}
					}
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
    }
}