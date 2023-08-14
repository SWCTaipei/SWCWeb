using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using Newtonsoft.Json;

/// <summary>
/// AddressApiClass 的摘要描述
/// </summary>
public class AddressApiClass : System.Web.UI.Page
{
    public AddressApiClass()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
	//取得縣市
    public string[] GetCity()
    {
		string Re = "";
        string gSQLStr =  "select No,City from addresscode group by No,City order by convert(int,No) ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = gSQLStr;
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    while (readerSWC.Read())
                        Re = Re + readerSWC["City"].ToString() + ";;";
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
		return Re.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
    }
	//縣市取得鄉鎮市區
    public string[] GetArea(string city)
    {
        string Re = "";
        string gSQLStr =  "select *from addresscode where City = @City order by convert(int,Zip) ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = gSQLStr;
                cmd.Parameters.Add(new SqlParameter("@City", city));
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    while (readerSWC.Read())
                        Re = Re + readerSWC["Area"].ToString() + ";;";
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
		return Re.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
    }
	//縣市鄉鎮市區取得郵遞區號
	public string GetZip(string city,string area)
    {
        string Re = "";
        string gSQLStr =  "select *from addresscode where City = @City and Area = @Area order by convert(int,Zip) ";
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();
			
			using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = gSQLStr;
                cmd.Parameters.Add(new SqlParameter("@City", city));
                cmd.Parameters.Add(new SqlParameter("@Area", area));
                cmd.ExecuteNonQuery();
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    while (readerSWC.Read())
                        Re = readerSWC["Zip"].ToString();
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
		return Re;
    }
}