using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SWC001_PWA_list2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		SWCDTList();
    }
	
	//PWA列表
    public void SWCDTList()
    {
        PanelTable.Controls.Clear();
		
		HtmlTableRow trow;
        HtmlTableCell tcell;
		
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        string sqlStr = "select * from SWCCASE where SWC004 = '施工中' or SWC004 = '停工中';";
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
							tcell.InnerHtml = "";
							trow.Controls.Add(tcell);
							
							tcell = new HtmlTableCell("td");
							tcell.InnerHtml = "水土保持施工抽查紀錄";
							trow.Controls.Add(tcell);
							
							tcell = new HtmlTableCell("td");
							HyperLink hlf = new HyperLink();
							hlf.Text = "詳情";
							hlf.Target="_blank";
							hlf.NavigateUrl = "PWA_SWCDT002/SWCDT002PWA.aspx?SWCNO=" + reader["SWC000"] + "&DTLNO=AddNew";
							tcell.Controls.Add(hlf);
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