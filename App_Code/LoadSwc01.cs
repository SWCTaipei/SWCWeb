/*  Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// LoadSwcClass01 的摘要描述
/// </summary>
public class LoadSwcClass01 : System.Web.UI.Page
{
    public LoadSwcClass01()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public void LoadSwcCase(string uType,string uKeyVal)
    {
    //    string InCaseStr = "";

    //    ConnectionStringSettings SWCconnectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
    //    using (SqlConnection SWCConn = new SqlConnection(SWCconnectionString.ConnectionString))
    //    {
    //        SWCConn.Open();

    //        string strSQLRV = " SELECT * FROM RelationSwc ";
    //        strSQLRV = strSQLRV + " WHERE Upd01='Y' ";

    //        SqlDataReader readerReSwc;
    //        SqlCommand objCmdRV = new SqlCommand(strSQLRV, SWCConn);
    //        readerReSwc = objCmdRV.ExecuteReader();

    //        while (readerReSwc.Read())
    //        {
    //            string tempSwc00 = readerReSwc["Key01"] + "";

    //            InCaseStr = InCaseStr + ";;" + tempSwc00.Trim() + "A";
    //        }

    //    }

    //    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
    //    using (SqlConnection TSLMConn = new SqlConnection(connectionString.ConnectionString))
    //    {
    //        TSLMConn.Open();

    //        string strSQLRV = " SELECT * FROM SWCSWC ";
    //        strSQLRV = strSQLRV + " WHERE 1=1 ";

    //        switch (uType)
    //        {
    //            case "03":
    //                break;
    //            case "04":
    //                strSQLRV = strSQLRV + "   AND SWC22='"+ uKeyVal.Trim() + "' ";
    //                break;
    //        }

    //        SqlDataReader readerTslmSwc;
    //        SqlCommand objCmdTslmSwc = new SqlCommand(strSQLRV, TSLMConn);
    //        readerTslmSwc = objCmdTslmSwc.ExecuteReader();

    //        while (readerTslmSwc.Read()) {
    //            string tempSWC00 = readerTslmSwc["SWC00"]+"";

    //            if (InCaseStr.IndexOf(tempSWC00) < 0)
    //            {
    //                InCaseStr = InCaseStr + ";;" + tempSWC00.Trim() + "B";
    //            }
    //        }

    //    }

    //    string ExeInsStr = "";

    //    string[] arraySWC00 = InCaseStr.Split(new string[] { ";;" },StringSplitOptions.RemoveEmptyEntries);

    //    for (int Ci = 0; Ci < arraySWC00.Length ; Ci++)
    //    {
    //        string insSWC00 = arraySWC00[Ci]+"";
            
    //        string insSWC00_ID = insSWC00.Substring(0, insSWC00.Length - 1);
    //        string insSWC00_Type = insSWC00.Substring(insSWC00.Length - 1,1);

    //        using (SqlConnection TSLMConn = new SqlConnection(connectionString.ConnectionString))
    //        {
    //            TSLMConn.Open();

    //            string strSQLRV = " SELECT * FROM SWCSWC ";
    //            strSQLRV = strSQLRV + " WHERE SWC00='"+ insSWC00_ID + "' ";

    //            SqlDataReader readerTslmSwc;
    //            SqlCommand objCmdTslmSwc = new SqlCommand(strSQLRV, TSLMConn);
    //            readerTslmSwc = objCmdTslmSwc.ExecuteReader();

    //            while (readerTslmSwc.Read()) {
    //                string tslmSWC00 = readerTslmSwc["SWC00"] + "";
    //                string tslmSWC02 = readerTslmSwc["SWC02"] + "";
    //                string tslmSWC04 = readerTslmSwc["SWC04"] + "";
    //                string tslmSWC05 = readerTslmSwc["SWC05"] + "";
    //                string tslmSWC07 = readerTslmSwc["SWC07"] + "";
    //                string tslmSWC13 = readerTslmSwc["SWC13"] + "";

    //                if (insSWC00_Type == "B")
    //                {
    //                    string strSQLINSID = " insert into SWCCASE (SWC000,SWC002,SWC004,SWC005,SWC007,SWC013) values ";
    //                    strSQLINSID = strSQLINSID + " ('"+ tslmSWC00 + "','"+ tslmSWC02 + "','"+ tslmSWC04 + "','"+ tslmSWC05 + "','"+ tslmSWC07 + "','"+ tslmSWC13 + "') ";

    //                    ExeInsStr = ExeInsStr + strSQLINSID+";";

    //                    strSQLINSID = " insert into RelationSwc (Key01,Upd01) values ";
    //                    strSQLINSID = strSQLINSID + " ('" + tslmSWC00 + "','Y') ";

    //                    ExeInsStr = ExeInsStr + strSQLINSID + ";";
    //                }
    //            }

    //        }
    //    }

    //    if (ExeInsStr != "") {
    //        using (SqlConnection SWCConn = new SqlConnection(SWCconnectionString.ConnectionString)) {
    //            SWCConn.Open();

    //            SqlCommand objCmdIns = new SqlCommand(ExeInsStr, SWCConn);

    //            objCmdIns.ExecuteNonQuery();
    //            objCmdIns.Dispose();
    //            SWCConn.Close();

    //        }
    //    }

    }
}