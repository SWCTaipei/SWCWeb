using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SWCRD_FPage003 : System.Web.UI.Page
{
    string optojs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
		string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        if (!IsPostBack)
        {
			if (!(ssUserName != "" && (ssUserType == "02"||ssUserType == "04"))) { Response.Write("<script>alert('很抱歉，您尚未開通此功能權限，如有疑問請洽承辦單位或系統管理員聯絡洽詢。');location.href='../SWCDOC/SWC001.aspx';</script>"); }
			else{getGVData();}
        }
        #region 全區供用
        //SBApp.ViewRecord("線上行事曆", "", "");
        #endregion 全區供用
    }

    private void getGVData()
    {
        GBClass001 CLS = new GBClass001();
		optojs = "";
		Session["optojs"] = "";
		
        string ssUserID = Session["ID"] + "";
		
        string myGV2Data = " select A.*,E.LAND001 from swcswc A left join (select SWC000,MIN(LAND000) as LAND000 from TCGESWC.dbo.SWCLAND group by SWC000) D ON A.SWC00 = D.SWC000 left join (select SWC000,LAND000,LAND001 as LAND001 from TCGESWC.dbo.SWCLAND) E ON A.SWC00 = E.SWC000 and D.LAND000 = E.LAND000 where (SWC04='審查中' or SWC04='施工中') and (SWC021ID='"+ssUserID+"' or SWC045ID='"+ssUserID+"' or SWC00 in (select distinct SWC000 from TCGESWC.dbo.GuildGroup where ETID='" + ssUserID +"')) order by SWC00 desc ; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            SqlDataReader readerItem02;
            SqlCommand objCmdItem02 = new SqlCommand(myGV2Data, TslmConn);
            readerItem02 = objCmdItem02.ExecuteReader();

            //if (readerItem01.HasRows) { } else { Gridpanel.Visible = false; }
            while (readerItem02.Read())
            {
                string tmpSWC000 = readerItem02["SWC00"] + "";

                //書件;起;迄;義務人;案件編號;地點;地區;狀態;顏色;技師;公會
				if( readerItem02["SWC04"] + "" == "審查中" && CLS.DateView(readerItem02["SWC113"] + "","00") + " " + CLS.DateView(readerItem02["SWC113"] + "","06") + ":" + CLS.DateView(readerItem02["SWC113"] + "", "07") != " :" && CLS.DateView(readerItem02["SWC113_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC113_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC113_2"] + "", "07") != " :")
					optojs += readerItem02["SWC05"].ToString().Trim() + "|" + CLS.DateView(readerItem02["SWC113"] + "","00") + " " + CLS.DateView(readerItem02["SWC113"] + "","06") + ":" + CLS.DateView(readerItem02["SWC113"] + "", "07") + "|" + CLS.DateView(readerItem02["SWC113_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC113_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC113_2"] + "", "07") + "|" + readerItem02["SWC13"] + "|" + tmpSWC000 + "|" + readerItem02["SWC120"] + "|" + readerItem02["LAND001"] + "|" + readerItem02["SWC04"] + "|#9987B5|" + readerItem02["SWC21"] + "|" + readerItem02["SWC22"] + "/";
				if( readerItem02["SWC04"] + "" == "施工中" && CLS.DateView(readerItem02["SWC114"] + "","00") + " " + CLS.DateView(readerItem02["SWC114"] + "","06") + ":" + CLS.DateView(readerItem02["SWC114"] + "", "07") != " :" && CLS.DateView(readerItem02["SWC114_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC114_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC114_2"] + "", "07") != " :")
					optojs += readerItem02["SWC05"].ToString().Trim() + "|" + CLS.DateView(readerItem02["SWC114"] + "","00") + " " + CLS.DateView(readerItem02["SWC114"] + "","06") + ":" + CLS.DateView(readerItem02["SWC114"] + "", "07") + "|" + CLS.DateView(readerItem02["SWC114_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC114_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC114_2"] + "", "07") + "|" + readerItem02["SWC13"] + "|" + tmpSWC000 + "|" + readerItem02["SWC121"] + "|" + readerItem02["LAND001"] + "|" + readerItem02["SWC04"] + "|#ba9880|" + readerItem02["SWC45"] + "|" + readerItem02["SWC24"] + "/";
				if( haveONA9D(readerItem02["SWC00"].ToString()) == true && CLS.DateView(readerItem02["SWC115"] + "","00") + " " + CLS.DateView(readerItem02["SWC115"] + "","06") + ":" + CLS.DateView(readerItem02["SWC115"] + "", "07") != " :" && CLS.DateView(readerItem02["SWC115_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC115_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC115_2"] + "", "07") != " :")
					optojs += readerItem02["SWC05"].ToString().Trim() + "|" + CLS.DateView(readerItem02["SWC115"] + "","00") + " " + CLS.DateView(readerItem02["SWC115"] + "","06") + ":" + CLS.DateView(readerItem02["SWC115"] + "", "07") + "|" + CLS.DateView(readerItem02["SWC115_2"] + "","00") + " " + CLS.DateView(readerItem02["SWC115_2"] + "","06") + ":" + CLS.DateView(readerItem02["SWC115_2"] + "", "07") + "|" + readerItem02["SWC13"] + "|" + tmpSWC000 + "|" + readerItem02["SWC122"] + "|" + readerItem02["LAND001"] + "|" + readerItem02["SWC04"] + "|#ba9880|" + readerItem02["SWC45"] + "|" + readerItem02["SWC24"] + "/";
            }
        }
		Session["optojs"] = optojs.Replace(System.Environment.NewLine,"");
		//Response.Write(optojs);
		ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "calendar_test2", "<script>cc('" + optojs.Replace(System.Environment.NewLine,"") + "');</script>");
    }
	
	private bool haveONA9D(string tmpSWC000)
    {
        bool rValue = false;
        string testSql = " select * from OnlineApply09 where SWC000=@SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = testSql;
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    if (readerSwc.HasRows)
                        rValue = true;
                    readerSwc.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
}