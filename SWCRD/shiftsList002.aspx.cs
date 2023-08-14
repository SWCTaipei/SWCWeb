using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class SWCRD_shiftsList001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        setGridViewData();
        SET();
    }

    private void setGridViewData()
    {
        #region GV1
        DataTable OBJ_GV01 = (DataTable)ViewState["GV01"];
        DataTable DTGV01 = new DataTable();
        DTGV01.Columns.Add(new DataColumn("ROWID", typeof(string)));    //輪值順位
        DTGV01.Columns.Add(new DataColumn("GS003", typeof(string)));    //廠商名稱
        ViewState["GV01"] = DTGV01;
        OBJ_GV01 = DTGV01;

        string strGS2a = "", strGS2b = "";int tmin=5000,tmax = 0;
        string gvData02 = " select * from GuildShifts where GS001='2' and GS002<>'ge-50702' and isnull(GS009,'1')='1' order by isnull(GS004, 0);";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = gvData02;
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tmpGS003 = readerTslm["GS003"] + "";
                            string tmpGS005 = readerTslm["GS005"] + ""; //輪職次數
                            string tmpGS006 = readerTslm["GS006"] + ""; //暫停次數
                            string tmpGS007 = readerTslm["GS007"] + ""; //建議公會
                            string tmpGS008 = readerTslm["GS008"] + ""; //未繳逾期
                            tmpGS005 = tmpGS005.Trim() == "" ? "0" : tmpGS005;
                            tmpGS006 = tmpGS006.Trim() == "" ? "0" : tmpGS006;
                            tmpGS007 = tmpGS007.Trim() == "" ? "0" : tmpGS007;
                            tmpGS008 = tmpGS008.Trim() == "" ? "0" : tmpGS008;
                            int tmpCount = Convert.ToInt32(tmpGS005) + Convert.ToInt32(tmpGS006) + Convert.ToInt32(tmpGS007) - Convert.ToInt32(tmpGS008);
                            tmin = tmpCount < tmin ? tmpCount : tmin;
                            tmax = tmpCount > tmax ? tmpCount : tmax;

                            strGS2a += tmpGS003 + ";;";
                            strGS2b += tmpCount.ToString() + ";;";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        int pgShow = 4;
        int gvC = 1;
        string[] aD1 = strGS2a.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] aD2 = strGS2b.Split(new string[] { ";;" }, StringSplitOptions.None);
        while (pgShow>0)
        {
            for (int i = 0; i < aD1.Length; i++)
            {
                if (aD2[i].Trim()!=""&& pgShow>0) { 
                if (Convert.ToInt32(aD2[i]) == tmin)
                {
                    DataRow dr01 = OBJ_GV01.NewRow();

                    dr01["ROWID"] = gvC++;
                    dr01["GS003"] = aD1[i];

                    OBJ_GV01.Rows.Add(dr01);
                    ViewState["GV01"] = OBJ_GV01;
                    aD2[i] = (Convert.ToInt32(aD2[i]) + 1).ToString();
                        pgShow--; 
                        
                    }
                }
            }
            tmin++;
        }
        GridView1.DataSource = OBJ_GV01;
        GridView1.DataBind();
        #endregion
    }

    protected void SET()
    {
        DataSet ds = new DataSet();
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        SqlConnection Conn = new SqlConnection();       //宣告SQL的連線
                                                        //  SqlCommand cmd = new SqlCommand();              //宣告對SQL執行的語法
                                                        // SqlDataAdapter da = new SqlDataAdapter();       //SQL 資料庫的連接與執行命令

        string strSQLRV = " SELECT top 10 S.SWC02, S.SWC05, S.SWC22, G.GSLType,u.name as SWC24, G.GSL002, convert(varchar, G.GSL003, 23) as GSL003, G.Savedate, G.saveuser FROM GuildShiftsList G LEFT JOIN SWCSWC S ON G.SWC000 = S.SWC00 LEFT JOIN geouser u ON u.userid = G.GSL001 WHERE G.GSLType = '檢查' and SWC05 not like '%測試案件test%' order by G.Savedate desc; ";

        






        SqlDataSource1.SelectCommand = strSQLRV;
/*
 * 
 * 
 * 
 * 
 * 
        string strSql = strSQLRV;

        string strConnString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["GEOINFOCONN"].ToString();
        SqlConnection connect = new SqlConnection(strConnString);
        connect.Open();
        SqlDataAdapter da = new SqlDataAdapter(strSql, strConnString);
        DataTable dt = new DataTable();
        da.Fill(dt);
        GridView1.DataSourceID = "";
        GridView1.DataSource = dt;
        GridView1.DataBind();
        connect.Close();
        */

    }
}