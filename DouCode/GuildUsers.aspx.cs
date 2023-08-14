using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class DouCode_GuildUsers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        LBDataDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        OutPutPageData();
    }

    private void OutPutPageData()
    {
        string EtNameStr = "";
        string exeSqlStr = " select ROW_NUMBER() OVER(ORDER BY userid) AS ROWID,userid,name from tslm2.dbo.geouser where unit='技師公會' order by userid; ";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(exeSqlStr, SwcConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string tGuildId = readerData["userid"] + "";
                string tGuildName = readerData["name"] + "";
                string exeSqlStrDTL = " select ETName from ETUsers where GuildSubstitute='"+ tGuildId + "' and GuildTcgeChk='1' order by ETName; ";

                EtNameStr = "";
                using (SqlConnection SwcConnD = new SqlConnection(connectionString.ConnectionString))
                {
                    SwcConnD.Open();

                    SqlDataReader readerData2;
                    SqlCommand objCmdRV2 = new SqlCommand(exeSqlStrDTL, SwcConnD);
                    readerData2 = objCmdRV2.ExecuteReader();

                    while (readerData2.Read())
                    {
                        EtNameStr += readerData2["ETName"] + "、";//";;";
                    }
                }
                if(EtNameStr !="")
                    ShowYourTable(tGuildId,tGuildName, EtNameStr.Substring(0,EtNameStr.Length-1) +";;");
            }
            readerData.Close();
            objCmdRV.Dispose();
        }

    }

    private void ShowYourTable(string GuildID, string GuildName,string ETNameStr)
    {
        Table GuildUserTB = new Table();
        GuildUserTB.ID = GuildID;
        GuildUserTB.CssClass = "SWCbbd";

        TableRow TBRowH = new TableRow();
        GuildUserTB.Rows.Add(TBRowH);
        

        TableCell TBCell_H = new TableCell();
        TBRowH.Cells.Add(TBCell_H);
        TBRowH.CssClass = "bbdtitle";

        string[] arrayName = ETNameStr.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayNaem = ETNameStr.Split(new string[] { "、" }, StringSplitOptions.None);

        Label GuildTBH = new Label();
        GuildTBH.Text = GuildName;
        Label UserCount = new Label();
        UserCount.Text = "總計：" + (arrayNaem.Length) + "人";
        UserCount.Style["float"] = "right";
        TBCell_H.Controls.Add(GuildTBH);
        TBCell_H.Controls.Add(UserCount);

        //GuildUserTB.Rows[0].Cells[0].ColumnSpan = 20;

        TableRow TBRowDate = new TableRow();     //create a new object of type TableRow
        GuildUserTB.Rows.Add(TBRowDate);

        for (int ii = 0; ii < arrayName.Length-1; ii++)
        {
            if (ii%20 == 0 && ii>0)
            {
                TableRow TBRowDate2 = new TableRow();     //create a new object of type TableRow
                GuildUserTB.Rows.Add(TBRowDate2);
            }

            TableCell TBCell_Name = new TableCell();
            TBCell_Name.Text = arrayName[ii];
            TBCell_Name.CssClass = "showtd";
            TBRowDate.Cells.Add(TBCell_Name);

        }
        Area01.Controls.Add(GuildUserTB);

        #region.參考
        //table.Attributes.Add("border", "1px");  //自己加一個屬性
        //table.Style["font-size"] = "9pt";         //設定Style
        //table.Style["width"] = "100%";
        //table.CssClass = "SWCbbd";


        //string[] arraytest = new string[] { "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "abc", "OVER" };


        //for (int ii = 0; ii < 10; ii++)
        //{
        //    TableRow tRow = new TableRow();     //create a new object of type TableRow
        //    table.Rows.Add(tRow);


        //if(ii>1)
        //table.Rows[ii-1].Cells[0].ColumnSpan = 2;

        //TableCell tCell_0 = new TableCell();
        //tCell_0.Text = ii.ToString();
        //tRow.Cells.Add(tCell_0);



        //Label VV = new Label();
        //VV.Text = "第1欄";
        //Label VVt = new Label();
        //VVt.Text = "第1欄___";

        //TableCell tCell_1 = new TableCell();  //create a new object of type TableCell 
        //tCell_1.Width = 50;
        //tCell_1.VerticalAlign = VerticalAlign.Top;
        //tCell_1.Controls.Add(VV);
        //tCell_1.Controls.Add(VVt);
        //tRow.Cells.Add(tCell_1);


        ////if (ii == 9) 
        //{
        //    TableCell tCell_3 = new TableCell();
        //    tCell_3.Text = "第3欄";
        //    tRow.Cells.Add(tCell_3);
        //}
        ////if (ii == 9)
        //{
        //    TableCell tCell_4 = new TableCell();
        //    tCell_4.Text = "第4欄";
        //    tRow.Cells.Add(tCell_4);
        //}
        //}

        //table.Rows[0].Cells[0].ColumnSpan = 20;

        ////Page.Controls.Add(table);
        //Area01.Controls.Add(table);
        #endregion
    }
}