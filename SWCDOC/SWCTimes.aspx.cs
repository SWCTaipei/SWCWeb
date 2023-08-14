using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCTimes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string rCaseId = Request.QueryString["SWCNO"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        GBClass001 SBApp = new GBClass001();

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        if (!IsPostBack)
        {
			GenerateTable_InsertData(rCaseId);
        }

        
        //全區供用

        SBApp.ViewRecord("管制時程表", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
    }
   

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }

    protected void OutPdf_Click(object sender, ImageClickEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";

        Response.Redirect("../SwcReport/PdfSwcTimes.aspx?SWCNO=" + rCaseId);
    }
	
	private void GenerateTable_InsertData(string v)
	{
		string rSWC005 = "";
		string rSWC013 = "";
		string rSWC021 = "";
		string rSWC087 = "";
		string rSWC034 = "";
		string rSWC109 = "";
		string rSWC088 = "";
		string rSWC125 = "";
		int count = 0;
		string sqlstr = "";
		string savedate_1 = "";
		string savedate_2 = "";
		string savedate_3 = "";
		string savedate_4 = "";
		string no = "";
		double delay = 0;
		TimeSpan ts = new TimeSpan();
		GBClass001 SBApp = new GBClass001();
		ConnectionStringSettings connectionStringSWC = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionStringSWC.ConnectionString))
        {
            SWCConn.Open();
			sqlstr = " SELECT SWC005,SWC013,SWC021,SWC087,dateadd(DAY,20,SWC034) SWC034,SWC109,SWC088,SWC125 FROM SWCCASE where SWC000=@SWC000;";
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
					{
                        while (readerSWC.Read())
                        {
                            rSWC005 = readerSWC["SWC005"].ToString();
							rSWC013 = readerSWC["SWC013"].ToString();
							rSWC021 = readerSWC["SWC021"].ToString();
							rSWC087 = readerSWC["SWC087"].ToString();
							rSWC034 = readerSWC["SWC034"].ToString();
							rSWC109 = readerSWC["SWC109"].ToString();
							rSWC088 = readerSWC["SWC088"].ToString();
							rSWC125 = readerSWC["SWC125"].ToString();
						}
					}
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
			
			sqlstr = " SELECT ISNULL(MAX(DTLA006),'0') DTLA006 FROM SWCDTL01 where SWC000=@SWC000;";
			using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
					{
                        while (readerSWC.Read())
                        {
                            count = Convert.ToInt32(readerSWC["DTLA006"].ToString());
						}
					}
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
		LBSWC005.Text = rSWC005;
		LBSWC013.Text = rSWC013;
		LBSWC021.Text = rSWC021;
		
		
		string tLBSAOID = "";
		string UserName = "";
		int ii = 0;
		string exeSqlStr = " select E.ETName,E.ETID,ISNULL(RGSID,'0') AS RGSID from GuildGroup G Left Join ETUsers E on G.ETID=E.ETID where G.swc000='" + v + "' order by convert(float,RGSID) ";
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection DDL01Conn = new SqlConnection(connectionString.ConnectionString))
		{
			DDL01Conn.Open();
			SqlDataReader readerItemGG;
			SqlCommand objCmdItemGG = new SqlCommand(exeSqlStr, DDL01Conn);
			readerItemGG = objCmdItemGG.ExecuteReader();

			while (readerItemGG.Read())
			{
				string tmpUserName = readerItemGG["ETName"] + "";
				string tmpUserID = readerItemGG["ETID"] + "";
				string tmpRGSID = readerItemGG["RGSID"] + "";
				int aaa = Convert.ToInt32(tmpRGSID);
				if (aaa > 0 && aaa < 11) //1~10
				{
					UserName += tmpUserName.Trim()+ " ";
				}
			}
		}
		LBSWC087.Text = UserName;
		
		
		dt.Controls.Clear();
		
        
		TableRow tr;
		TableCell tc;
		
		tr = new TableRow();
		dt.Controls.Add(tr);
		tc = new TableCell();
		tc.Style.Add("width", "200px");
		tc.Style.Add("text-align", "center");
		tc.Text = "審查次別";
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.ColumnSpan = 2;
		tc.Text = "時間";
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("width", "200px");
		tc.Style.Add("text-align", "center");
		tc.Text = "逾期天數";
		tr.Controls.Add(tc);
		
		//1 rSWC034 SWCDTL01(1 savedate)
		//2~ ShareFiles(001~) SWCDTL01(2~ savedate)
        for (int i = 1; i <= count; i++)
        {
			switch(i){
				case 2:
					no="001";
					break;
				case 3:
					no="002";
					break;
				case 4:
					no="003";
					break;
				case 5:
					no="004";
					break;
				case 6:
					no="005";
					break;
				case 7:
					no="006";
					break;
				case 8:
					no="007";
					break;
				case 9:
					no="008";
					break;
				case 10:
					no="009";
					break;
			}
			using (SqlConnection SWCConn = new SqlConnection(connectionStringSWC.ConnectionString))
			{
				SWCConn.Open();
				sqlstr = " SELECT MIN(savedate) savedate FROM SWCDTL01 where SWC000=@SWC000 and DTLA006=@DTLA006;";
				using (var cmd = SWCConn.CreateCommand())
				{
					cmd.CommandText = sqlstr;
					cmd.Parameters.Add(new SqlParameter("@SWC000", v));
					cmd.Parameters.Add(new SqlParameter("@DTLA006", i.ToString()));
					cmd.ExecuteNonQuery();
		
					using (SqlDataReader readerSWC = cmd.ExecuteReader())
					{
						if (readerSWC.HasRows)
						{
							while (readerSWC.Read())
							{
								savedate_1 = readerSWC["savedate"].ToString();
							}
						}
						readerSWC.Close();
					}
					cmd.Cancel();
				}
			}
			if(i!=1){
				using (SqlConnection SWCConn = new SqlConnection(connectionStringSWC.ConnectionString))
				{
					SWCConn.Open();
					sqlstr = " SELECT dateadd(day,20,savedate) savedate FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE;";
					using (var cmd = SWCConn.CreateCommand())
					{
						cmd.CommandText = sqlstr;
						cmd.Parameters.Add(new SqlParameter("@SWC000", v));
						cmd.Parameters.Add(new SqlParameter("@SFTYPE", no));
						cmd.ExecuteNonQuery();
		
						using (SqlDataReader readerSWC = cmd.ExecuteReader())
						{
							if (readerSWC.HasRows)
							{
								while (readerSWC.Read())
								{
									savedate_2 = readerSWC["savedate"].ToString();
								}
							}
							readerSWC.Close();
						}
						cmd.Cancel();
					}
				}
				
			}
			if(i==1){
				tr = new TableRow();
				dt.Controls.Add(tr);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				tc.Text = "第"+i+"次審查";
				tr.Controls.Add(tc);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				tc.Text = "發文期限：";
				if(SBApp.DateView(rSWC034,"03")=="")
					tc.Text="-";
				else
					tc.Text+=SBApp.DateView(rSWC034,"03");
				tr.Controls.Add(tc);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				tc.Text = "發文日期：";
				if(SBApp.DateView(savedate_1,"03")=="")
					tc.Text="-";
				else
					tc.Text+=SBApp.DateView(savedate_1,"03");
				tr.Controls.Add(tc);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				ts = new TimeSpan();
				if(SBApp.DateView(savedate_1,"03") != "" && SBApp.DateView(rSWC034,"03") != "")
					ts = new TimeSpan(Convert.ToDateTime(savedate_1).Ticks-Convert.ToDateTime(rSWC034).Ticks);
				if(ts.Days>0){delay+=ts.Days;tc.Text = ts.Days.ToString() + "天";}
				else{tc.Text = "";}
				tr.Controls.Add(tc);
	
			}else{
				tr = new TableRow();
				dt.Controls.Add(tr);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				tc.Text = "第"+i+"次審查";
				tr.Controls.Add(tc);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				tc.Text = "發文期限：";
				if(SBApp.DateView(savedate_2,"03")=="")
					tc.Text="-";
				else
					tc.Text+=SBApp.DateView(savedate_2,"03");
				tr.Controls.Add(tc);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				tc.Text = "發文日期：";
				if(SBApp.DateView(savedate_1,"03")=="")
					tc.Text="-";
				else
					tc.Text+=SBApp.DateView(savedate_1,"03");
				tr.Controls.Add(tc);
				tc = new TableCell();
				tc.Style.Add("text-align", "center");
				ts = new TimeSpan();
				if(SBApp.DateView(savedate_1,"03") != "" && SBApp.DateView(savedate_2,"03") != "")
					ts = new TimeSpan(Convert.ToDateTime(savedate_1).Ticks-Convert.ToDateTime(savedate_2).Ticks);
				if(ts.Days>0){delay+=ts.Days;tc.Text = ts.Days.ToString() + "天";}
				else{tc.Text = "";}
				tr.Controls.Add(tc);
			}
            
        }
		
		
		/*
		switch(count){
			case 1:
				no="001";
				break;
			case 2:
				no="002";
				break;
			case 3:
				no="003";
				break;
			case 4:
				no="006";
				break;
			case 5:
				no="007";
				break;
		}
		using (SqlConnection SWCConn = new SqlConnection(connectionStringSWC.ConnectionString))
		{
			SWCConn.Open();
			sqlstr = " SELECT dateadd(day,20,savedate) savedate FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE;";
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = sqlstr;
				cmd.Parameters.Add(new SqlParameter("@SWC000", v));
				cmd.Parameters.Add(new SqlParameter("@SFTYPE", no));
				cmd.ExecuteNonQuery();
			
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
				{
					if (readerSWC.HasRows)
					{
						while (readerSWC.Read())
						{
							savedate_3 = readerSWC["savedate"].ToString();
						}
					}
					readerSWC.Close();
				}
				cmd.Cancel();
			}
		}
		*/
		
		ConnectionStringSettings connectionStringTSLM = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
		using (SqlConnection SWCConn = new SqlConnection(connectionStringTSLM.ConnectionString))
		{
			SWCConn.Open();
			sqlstr = " SELECT dateadd(day,20,MIN(SAA002)) AS DATE FROM SwcApply2001 where SWC000=@SWC000 ;";
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = sqlstr;
				cmd.Parameters.Add(new SqlParameter("@SWC000", v));
				cmd.ExecuteNonQuery();
			
				using (SqlDataReader readerSWC = cmd.ExecuteReader())
				{
					if (readerSWC.HasRows)
					{
						while (readerSWC.Read())
						{
							savedate_3 = readerSWC["DATE"].ToString();
						}
					}
					readerSWC.Close();
				}
				cmd.Cancel();
			}
		}
		
		//核定查核定稿本最小值 rSWC109
		tr = new TableRow();
		dt.Controls.Add(tr);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Text = "定稿本製作";
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Text = "發文期限：";
		if(SBApp.DateView(savedate_3,"03")=="")
			tc.Text="-";
		else
			tc.Text+=SBApp.DateView(savedate_3,"03");
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Text = "發文日期：";
		if(SBApp.DateView(rSWC109,"03")=="")
			tc.Text="-";
		else
			tc.Text+=SBApp.DateView(rSWC109,"03");
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		ts = new TimeSpan();
		if(SBApp.DateView(rSWC109,"03") != "" && SBApp.DateView(savedate_3,"03") != "")
			ts = new TimeSpan(Convert.ToDateTime(rSWC109).Ticks-Convert.ToDateTime(savedate_3).Ticks);
		if(ts.Days>0){delay+=ts.Days;tc.Text = ts.Days.ToString() + "天";}
		else{tc.Text = "";}
		tr.Controls.Add(tc);
		
		//rSWC088 rSWC109
		tr = new TableRow();
		dt.Controls.Add(tr);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Text = "總審查期限";
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Text = "發文期限：";
		if(SBApp.DateView(rSWC088,"03")=="")
			tc.Text="-";
		else
			tc.Text+=SBApp.DateView(rSWC088,"03");
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Text = "發文日期：";
		if(SBApp.DateView(rSWC109,"03")=="")
			tc.Text="-";
		else
			tc.Text+=SBApp.DateView(rSWC109,"03");
		tr.Controls.Add(tc);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		ts = new TimeSpan();
		if(SBApp.DateView(rSWC109, "03") != "" && SBApp.DateView(rSWC088, "03") != "")
			ts = new TimeSpan(Convert.ToDateTime(rSWC109).Ticks-Convert.ToDateTime(rSWC088).Ticks);
		if(ts.Days>0){delay+=ts.Days;tc.Text = ts.Days.ToString() + "天";}
		else{tc.Text = "";}
		tr.Controls.Add(tc);
		
		
		//rSWC125 SwcApply2001多筆撈最大
		using (SqlConnection TSLMConn = new SqlConnection(connectionStringTSLM.ConnectionString))
        {
            TSLMConn.Open();
			sqlstr = " SELECT MAX(savedate) savedate FROM SwcApply2001 where SWC000=@SWC000;";
            using (var cmd = TSLMConn.CreateCommand())
            {
                cmd.CommandText = sqlstr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTSLM = cmd.ExecuteReader())
                {
                    if (readerTSLM.HasRows)
					{
                        while (readerTSLM.Read())
                        {
                            savedate_4 = readerTSLM["savedate"].ToString();
						}
					}
                    readerTSLM.Close();
                }
                cmd.Cancel();
            }
		}
		if(SBApp.DateView(rSWC125,"03")==""){}
		else{
			tr = new TableRow();
			dt.Controls.Add(tr);
			tc = new TableCell();
			tc.Style.Add("text-align", "center");
			tc.Text = "建議核定/不予核定補正";
			tr.Controls.Add(tc);
			tc = new TableCell();
			tc.Style.Add("text-align", "center");
			tc.Text = "發文期限：";
			if(SBApp.DateView(rSWC125,"03")=="")
				tc.Text="-";
			else
				tc.Text+=SBApp.DateView(rSWC125,"03");
			tr.Controls.Add(tc);
			tc = new TableCell();
			tc.Style.Add("text-align", "center");
			tc.Text = "發文日期：";
			if(SBApp.DateView(savedate_4,"03")=="")
				tc.Text="-";
			else
				tc.Text+=SBApp.DateView(savedate_4,"03");
			tr.Controls.Add(tc);
			tc = new TableCell();
			tc.Style.Add("text-align", "center");
			ts = new TimeSpan();
			if(SBApp.DateView(rSWC125,"03") != "" && SBApp.DateView(savedate_4,"03") != "")
				ts = new TimeSpan(Convert.ToDateTime(savedate_4).Ticks-Convert.ToDateTime(rSWC125).Ticks);
			if(ts.Days>0){delay+=ts.Days;tc.Text = ts.Days.ToString() + "天";}
			else{tc.Text = "";}
			tr.Controls.Add(tc);
		}
		
		
		
		tr = new TableRow();
		dt.Controls.Add(tr);
		tc = new TableCell();
		tc.Style.Add("text-align", "center");
		tc.Style.Add("font-size", "20px");
		tc.Style.Add("font-weight", "bold");
		tc.ColumnSpan = 4;
		tc.Text = "總逾期天數：" + delay.ToString() +" 天";
		tr.Controls.Add(tc);
		
		tr = new TableRow();
		dt.Controls.Add(tr);
		tc = new TableCell();
		tc.Style.Add("font-size", "16px");
		tc.Style.Add("font-weight", "bold");
		tc.Style.Add("color", "red");
		tc.ColumnSpan = 4;
		tc.Text = "※歷次審查階段發文日期逾收文日期次日起20日者，每日扣罰委託服務費千分之三"+"<br>"+"※建議核定日期逾審查期限者，每日扣罰委託服務費千分之三"+"<br>"+"※補正日期逾補正期限者，每日扣罰委託服務費千分之三並暫停輪值一次";
		tr.Controls.Add(tc);
	}
}