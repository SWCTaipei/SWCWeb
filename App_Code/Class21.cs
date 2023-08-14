using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Class21 的摘要描述
/// </summary>
public class Class21 : System.Web.UI.Page
{
    public Class21()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public string getSwcData(string vSWC000,string vField) {
        string rValue = "";
        string sqlStr = " select * from SWCSWC where SWC00=@SWC000; ";
        #region
        try
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
            {
                TslmConn.Open();
                using (var cmd = TslmConn.CreateCommand())
                {
                    cmd.CommandText = sqlStr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", vSWC000));
                    #endregion
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerTslm = cmd.ExecuteReader())
                    {
                        if (readerTslm.HasRows)
                            while (readerTslm.Read())
                                rValue = readerTslm[vField] + "";
                        readerTslm.Close();
                    }
                    cmd.Cancel();
                }
            }
        } catch { }
        #endregion
        return rValue;
    }
}