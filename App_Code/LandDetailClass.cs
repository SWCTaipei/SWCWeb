using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// LandDetailClass 的摘要描述
/// </summary>
public class LandDetailClass : System.Web.UI.Page
{
    public LandDetailClass()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }

    public bool getSWC(string area, string section, string subsection, string number)
    {
        bool ret = false;
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();

            string strSQL = "SELECT distinct SWC02 from SWCSWC WHERE ( (SWC04 <> '') and ((SWC08 like '%" + area + "%') and (SWC09 like '%" + section + "%') and (SWC10 like '%" + subsection + "%') and (SWC11 like '%;" + number + ";%')))";
            SqlDataReader readerTslm;
            SqlCommand objCmdTslm = new SqlCommand(strSQL, TslmConn);
            readerTslm = objCmdTslm.ExecuteReader();
            while (readerTslm.Read())
            {
                ret = true;
            }
            readerTslm.Close();
            objCmdTslm.Cancel();

            strSQL = "SELECT distinct SWC02 from SWCSWC WHERE ( (SWC04 <> '') and (SWC02 in ( select distinct 行政管理案件編號 from relationLand where ((行政管理案件編號 <> '') and (區 ='" + area + "') and (段 = '" + section + "') and (小段 = '" + subsection + "') and (地號 = '" + number + "')) )) ) ";
            objCmdTslm = new SqlCommand(strSQL, TslmConn);
            readerTslm = objCmdTslm.ExecuteReader();
            while (readerTslm.Read())
            {
                ret = true;
            }
            readerTslm.Close();
            objCmdTslm.Cancel();
        }
        return ret;
    }

    public bool getILG(string area, string section, string subsection, string number)
    {
        bool ret = false;
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();

            string strSQL = "SELECT distinct ILG001 FROM ILGILG WHERE ( ((ILG005 <> '') and (ILG005 <> '撤銷') ) and ((ILG011 like '%" + area + "%') and (ILG012 like '%" + section + "%') and (ILG013 like '%" + subsection + "%') and (ILG014 like '%;" + number + ";%')))";
            SqlDataReader readerTslm;
            SqlCommand objCmdTslm = new SqlCommand(strSQL, TslmConn);
            readerTslm = objCmdTslm.ExecuteReader();
            while (readerTslm.Read())
            {
                ret = true;
            }
            readerTslm.Close();
            objCmdTslm.Cancel();

            strSQL = "SELECT distinct ILG001 FROM ILGILG WHERE ( ((ILG005 <> '') and (ILG005 <> '撤銷') ) and (ILG001 in ( select distinct 違規案件編號 from relationLand where ((違規案件編號 <> '') and (區 ='" + area + "') and (段 = '" + section + "') and (小段 = '" + subsection + "') and (地號 = '" + number + "')) ))) ";
            objCmdTslm = new SqlCommand(strSQL, TslmConn);
            readerTslm = objCmdTslm.ExecuteReader();
            while (readerTslm.Read())
            {
                ret = true;
            }
            readerTslm.Close();
            objCmdTslm.Cancel();
        }
        return ret;
    }
}