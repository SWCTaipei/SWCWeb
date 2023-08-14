using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

public partial class SWCDOC_SWCSCHED02 : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
		string tsql = "INSERT INTO SwcTimeAnalysis ";
		tsql += " select ";
		tsql += " A.SWC00 SWC00, ";
		tsql += " A.SWC04 SWC04, ";
		tsql += " A.SWC07 SWC07, ";
		tsql += " E20.SAA001 ONA001, ";
		tsql += " A.CaseDate01 a1, ";
		tsql += " B2.R004 a2, ";
		tsql += " B.savedate b1, ";
		tsql += " C2.R004 b2, ";
		tsql += " C.savedate b3, ";
		tsql += " D2.R004 b4, ";
		tsql += " D.savedate b5, ";
		tsql += " case when ISNULL(D.savedate,'') != '' then D.savedate when ISNULL(C.savedate,'') != '' then C.savedate when ISNULL(B.savedate,'') != '' then B.savedate end c, ";
		tsql += " case when substring(convert(nvarchar(10),A.SWC33),1,2)='19' then null else A.SWC33 end d2, ";
		tsql += " case when substring(convert(nvarchar(10),SWC34),1,2)='19' then null else SWC34 end d2, ";
		tsql += " E1.savedate e1, ";
		tsql += " E2.createdate e2, ";
		tsql += " E3.savedate e3, ";
		tsql += " E4.createdate e4, ";
		tsql += " E5.savedate e5, ";
		tsql += " E6.createdate e6, ";
		tsql += " E7.savedate e7, ";
		tsql += " E8.createdate e8, ";
		tsql += " E9.savedate e9, ";
		tsql += " E10.createdate e10, ";
		tsql += " E11.savedate e11, ";
		tsql += " E12.createdate e12, ";
		tsql += " E13.savedate e13, ";
		tsql += " E14.createdate e14, ";
		tsql += " E15.savedate e15, ";
		tsql += " E16.createdate e16, ";
		tsql += " E17.savedate e17, ";
		tsql += " E18.createdate e18, ";
		tsql += " E19.savedate e19, ";
		tsql += " E20.SAA002 e20, ";
		tsql += " F.ONA02002 f, ";
		tsql += " A.SWC136 g, ";
		tsql += " H.R004 h, ";
		tsql += " null i1,null i2,null i3,null i4,null i5,null i6,null i7,null i8,null i9,null i10,null i11,null i12,null i13,null i14,null i15,null i16,null i17,null i18, ";
		tsql += " A.SWC38 i, ";
		tsql += " null AA, ";
		tsql += " null BB, ";
		tsql += " null CC, ";
		tsql += " null EE, ";
		tsql += " null AAA, ";
		tsql += " null BBB, ";
		tsql += " null CCC, ";
		tsql += " null DDD, ";
		tsql += " null EEE, ";
		tsql += " null FFF, ";
		tsql += " null GGG, ";
		tsql += " null Remark ";
		tsql += " from swcswc A ";
		tsql += " left join SwcApply2002 B on A.SWC00 = B.SWC000 and SUBSTRING(B.SAB001,Len(B.SAB001),1) = '1' ";
		tsql += " left join (select ONA001,Min(R004) R004 from SignRCD group by ONA001) B2 on B.SAB001 = B2.ONA001 ";
		tsql += " left join SwcApply2002 C on A.SWC00 = C.SWC000 and SUBSTRING(C.SAB001,Len(C.SAB001),1) = '2' ";
		tsql += " left join (select ONA001,Min(R004) R004 from SignRCD group by ONA001) C2 on C.SAB001 = C2.ONA001 ";
		tsql += " left join SwcApply2002 D on A.SWC00 = D.SWC000 and SUBSTRING(D.SAB001,Len(D.SAB001),1) = '3' ";
		tsql += " left join (select ONA001,Min(R004) R004 from SignRCD group by ONA001) D2 on D.SAB001 = D2.ONA001 ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E1 on A.SWC00 = E1.SWC000 and E1.DTLA006 = '1' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E2 on A.SWC00 = E2.SWC000 and E2.SFTYPE = '001' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E3 on A.SWC00 = E3.SWC000 and E3.DTLA006 = '2' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E4 on A.SWC00 = E4.SWC000 and E4.SFTYPE = '002' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E5 on A.SWC00 = E5.SWC000 and E5.DTLA006 = '3' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E6 on A.SWC00 = E6.SWC000 and E6.SFTYPE = '003' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E7 on A.SWC00 = E7.SWC000 and E7.DTLA006 = '4' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E8 on A.SWC00 = E8.SWC000 and E8.SFTYPE = '004' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E9 on A.SWC00 = E9.SWC000 and E9.DTLA006 = '5' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E10 on A.SWC00 = E10.SWC000 and E10.SFTYPE = '005' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E11 on A.SWC00 = E11.SWC000 and E11.DTLA006 = '6' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E12 on A.SWC00 = E12.SWC000 and E12.SFTYPE = '006' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E13 on A.SWC00 = E13.SWC000 and E13.DTLA006 = '7' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E14 on A.SWC00 = E14.SWC000 and E14.SFTYPE = '007' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E15 on A.SWC00 = E15.SWC000 and E15.DTLA006 = '8' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E16 on A.SWC00 = E16.SWC000 and E16.SFTYPE = '008' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E17 on A.SWC00 = E17.SWC000 and E17.DTLA006 = '9' ";
		tsql += " left join TCGESWC.dbo.ShareFiles E18 on A.SWC00 = E18.SWC000 and E18.SFTYPE = '009' ";
		tsql += " left join (select SWC000,DTLA006,MIN(savedate) savedate from TCGESWC.dbo.SWCDTL01 group by SWC000,DTLA006) E19 on A.SWC00 = E19.SWC000 and E19.DTLA006 = '10' ";
		//tsql += " left join TCGESWC.dbo.ShareFiles E20 on A.SWC00 = E20.SWC000 and E20.SFTYPE = '099' ";
		tsql += " left join (select SWC000,ONA001 from SignRCD where Substring(ONA001,1,6) = 'SA2001' and R003 = '決行' group by SWC000,ONA001) E20_T on A.SWC00 = E20_T.SWC000 ";
		tsql += " left join SwcApply2001 E20 on E20_T.ONA001 = E20.SAA001 ";
		tsql += " left join (select SWC000,MIN(ONA02002) ONA02002 from TCGESWC.dbo.OnlineApply02 group by SWC000) F on A.SWC00 = F.SWC000 ";
		tsql += " left join (select SWC000,Min(R004) R004 from SignRCD where Substring(ONA001,1,6) = 'SA2001' and R003 = '送出' group by SWC000) H on A.SWC00 = H.SWC000 ";
		tsql += " where ISNULL(A.SWC04,'') != '' and ISNULL(A.SWC38,'') != '' and ISNULL(B2.R004,'') != '' and A.SWC00 not in (select SWC00 from SwcTimeAnalysis) order by A.SWC00 ;"; //and A.SWC33 >= '2021-08-11' 
		//條件為 (案件狀態不為空 and 核定日期有值 and 第一張受理查核表有送出紀錄)
		using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
		{
			SWCConn.Open();
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = tsql;
				#region.設定值
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}

		tsql = " select * from SwcTimeAnalysis where AA is null ;";
		DataTable dt = new DataTable();
		//更新i1~i18
		using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
		{
			SWCConn.Open();
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = tsql;
				#region.設定值
				#endregion
				cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					dt.Load(reader);
				}
				cmd.Cancel();
			}
		}

		foreach (DataRow dr in dt.Rows)
		{
			string tsql_1 = "";
			Dictionary<string, string> i = new Dictionary<string, string>();
			for (int ii = 1; ii <= 18; ii++)
			{
				i.Add(String.Format("i{0}", ii.ToString()), "");
			}

			tsql = "select * from SignRCD where SWC000=@SWC000 and R001=@R001 and R003=@R003 and ONA001=@ONA001 order by R004;";
			using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
			{
				SWCConn.Open();
				using (var cmd = SWCConn.CreateCommand())
				{
					cmd.CommandText = tsql;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("SWC000", dr["SWC00"] + ""));
					cmd.Parameters.Add(new SqlParameter("R001", "退補正"));
					cmd.Parameters.Add(new SqlParameter("R003", "決行"));
					cmd.Parameters.Add(new SqlParameter("ONA001", dr["ONA001"] + ""));
					#endregion
					cmd.ExecuteNonQuery();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							int ii = 1;
							while (reader.Read())
							{
								if (ii <= 17)
								{
									i["i" + ii.ToString()] = reader["R004"] + "";
								}
								ii += 2;
							}
						}
						reader.Close();
					}
					cmd.Cancel();
				}
			}

			tsql = "select * from SignRCD where SWC000=@SWC000 and R003=@R003 and ONA001=@ONA001 order by R004;";
			using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
			{
				SWCConn.Open();
				using (var cmd = SWCConn.CreateCommand())
				{
					cmd.CommandText = tsql;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("SWC000", dr["SWC00"] + ""));
					cmd.Parameters.Add(new SqlParameter("R003", "送出"));
					cmd.Parameters.Add(new SqlParameter("ONA001", dr["ONA001"] + ""));
					#endregion
					cmd.ExecuteNonQuery();
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.HasRows)
						{
							int ii = 0;
							while (reader.Read())
							{
								if (ii >= 2 && ii <= 18)
								{
									i["i" + ii.ToString()] = reader["R004"] + "";
								}
								ii += 2;
							}
						}
						reader.Close();
					}
					cmd.Cancel();
				}
			}

			for (int ii = 1; ii <= 18; ii++)
			{
				if (i["i" + ii.ToString()] != "")
				{
					//i["i" + ii.ToString()];
					tsql_1 += ",i" + ii.ToString() + "='" + Convert.ToDateTime(i["i" + ii.ToString()]).ToString("yyyy-MM-dd") + "' ";
				}
			}
			//tsql_1 = "update SwcTimeAnalysis set SWC00=@SWC00 " + tsql_1 + " where SWC00=@SWC00;";
			tsql_1 = "update SwcTimeAnalysis set SWC00='" + dr["SWC00"] + "' " + tsql_1 + " where SWC00='" + dr["SWC00"] + "' ;";


			//Response.Write(tsql_1 + "<br>");
			using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
			{
				SWCConn.Open();
				using (var cmd = SWCConn.CreateCommand())
				{
					cmd.CommandText = tsql_1;
					#region.設定值
					cmd.Parameters.Add(new SqlParameter("SWC00", dr["SWC00"] + ""));
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}

		tsql = " select * from SwcTimeAnalysis ;";
		string _updatesql = "";
		using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
		{
			SWCConn.Open();
			using (var cmd = SWCConn.CreateCommand())
			{
				cmd.CommandText = tsql;
				#region.設定值
				#endregion
				cmd.ExecuteNonQuery();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						int AA = 0; int BB = 0; int CC = 0; int EE = 0;
						if (reader["b2"] + "" != "")
						{
							TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(reader["b2"].ToString()).Ticks - Convert.ToDateTime(reader["b1"].ToString()).Ticks);
							AA += ts1.Days;
						}
						if (reader["b4"] + "" != "")
						{
							TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(reader["b4"].ToString()).Ticks - Convert.ToDateTime(reader["b3"].ToString()).Ticks);
							AA += ts2.Days;
						}
						//-----------------------------------------------------------------
						//cnt = 1, 3, 5, 7, 9, 11, 13, 15, 17, 19
						for (int cnt = 1; cnt <= 19; cnt += 2)
						{
							//cnt 有日期 cnt+1 有日期
							if (reader["e" + (cnt + 1).ToString()] + "" != "" && reader["e" + (cnt).ToString()] + "" != "")
							{
								//cnt == 19 或 cnt+2 無日期 cnt+3 無日期 => 用檢視本扣
								if(cnt == 19 || (reader["e" + (cnt + 3).ToString()] + "" == "" && reader["e" + (cnt + 2).ToString()] + "" == ""))
								{
									TimeSpan ts3 = new TimeSpan(Convert.ToDateTime(reader["e20"]).Ticks - Convert.ToDateTime(reader["e" + (cnt).ToString()]).Ticks);
									if (ts3.Days > 0) BB += ts3.Days;
								}
								//用修正本扣
								else
								{
									TimeSpan ts3_1 = new TimeSpan(Convert.ToDateTime(reader["e" + (cnt + 1).ToString()]).Ticks - Convert.ToDateTime(reader["e" + (cnt).ToString()]).Ticks);
									if (ts3_1.Days > 0) BB += ts3_1.Days;
								}
								
							}
							//cnt 有日期 cnt+1 沒日期
							else if (reader["e" + (cnt + 1).ToString()] + "" == "" && reader["e" + (cnt).ToString()] + "" != "")
							{
								//cnt == 19 或 cnt+2 無日期 cnt+3 無日期 => 用檢視本扣
								if (cnt == 19 || (reader["e" + (cnt + 2).ToString()] + "" == "" && reader["e" + (cnt + 3).ToString()] + "" == ""))
								{
									TimeSpan ts3_2 = new TimeSpan(Convert.ToDateTime(reader["e20"]).Ticks - Convert.ToDateTime(reader["e" + (cnt).ToString()]).Ticks);
									if (ts3_2.Days > 0) BB += ts3_2.Days;
								}
								//cnt+2 有日期 => 用cnt+2來扣
								else if(reader["e" + (cnt + 2).ToString()] + "" != "")
								{
									TimeSpan ts3_3 = new TimeSpan(Convert.ToDateTime(reader["e" + (cnt + 2).ToString()]).Ticks - Convert.ToDateTime(reader["e" + (cnt).ToString()]).Ticks);
									if (ts3_3.Days > 0) BB += ts3_3.Days;
								}
							}
						}
						if (reader["g"] + "" != "" && reader["f"] + "" != "" && (Convert.ToDateTime(reader["h"].ToString()) >= Convert.ToDateTime(reader["f"].ToString()) ))
						{
							TimeSpan ts6 = new TimeSpan(Convert.ToDateTime(reader["g"].ToString()).Ticks - Convert.ToDateTime(reader["f"].ToString()).Ticks);
							BB -= ts6.Days;
						}
						//-----------------------------------------------------------------
						if (reader["d"] + "" != "")
						{
							TimeSpan ts5 = new TimeSpan(Convert.ToDateTime(reader["h"].ToString()).Ticks - Convert.ToDateTime(reader["d"].ToString()).Ticks);
							CC += ts5.Days;
						}
						else
						{
							TimeSpan ts5_1 = new TimeSpan(Convert.ToDateTime(reader["h"].ToString()).Ticks - Convert.ToDateTime(reader["d2"].ToString()).Ticks);
							CC += ts5_1.Days;
						}
						CC -= BB;
						if (reader["g"] + "" != "" && reader["f"] + "" != "" && (Convert.ToDateTime(reader["h"].ToString()) >= Convert.ToDateTime(reader["f"].ToString())))
						{
							TimeSpan ts6_1 = new TimeSpan(Convert.ToDateTime(reader["g"].ToString()).Ticks - Convert.ToDateTime(reader["f"].ToString()).Ticks);
							CC -= ts6_1.Days;
						}
						//-----------------------------------------------------------------
						int i = 1;
						while (reader["i" + (i + 1).ToString()] + "" != "" && reader["i" + i.ToString()] + "" != "")
						{
							TimeSpan ts7 = new TimeSpan(Convert.ToDateTime(reader["i" + (i + 1).ToString()].ToString()).Ticks - Convert.ToDateTime(reader["i" + i.ToString()].ToString()).Ticks);
							EE += ts7.Days;
							i += 2;
						}
						
						if (reader["g"] + "" != "" && reader["f"] + "" != "" && (Convert.ToDateTime(reader["h"].ToString()) < Convert.ToDateTime(reader["f"].ToString())))
						{
							TimeSpan ts7_1 = new TimeSpan(Convert.ToDateTime(reader["g"].ToString()).Ticks - Convert.ToDateTime(reader["f"].ToString()).Ticks);
							EE -= ts7_1.Days;
						}

						//=================================================================

						int AAA = 0; int BBB = 0; int CCC = 0; int DDD = 0; int EEE = 0; int FFF = 0; int GGG = 0;

						TimeSpan ts8 = new TimeSpan(Convert.ToDateTime(reader["c"].ToString()).Ticks - Convert.ToDateTime(reader["a2"].ToString()).Ticks);
						AAA += ts8.Days;
						AAA -= AA;

						BBB = AA;

						if (reader["d"] + "" != "")
						{
							TimeSpan ts9 = new TimeSpan(Convert.ToDateTime(reader["d"].ToString()).Ticks - Convert.ToDateTime(reader["c"].ToString()).Ticks);
							CCC = ts9.Days < 0 ? 0 : ts9.Days;
						}
						else if (reader["d2"] + "" != "")
						{
							TimeSpan ts9_1 = new TimeSpan(Convert.ToDateTime(reader["d2"].ToString()).Ticks - Convert.ToDateTime(reader["c"].ToString()).Ticks);
							CCC = ts9_1.Days < 0 ? 0 : ts9_1.Days;
						}

						DDD = CC;


						EEE = BB;


						TimeSpan ts10 = new TimeSpan(Convert.ToDateTime(reader["i"].ToString()).Ticks - Convert.ToDateTime(reader["h"].ToString()).Ticks);
						FFF = ts10.Days;
						FFF -= EE;
						
						if (reader["g"] + "" != "" && reader["f"] + "" != "" && (Convert.ToDateTime(reader["h"].ToString()) < Convert.ToDateTime(reader["f"].ToString())))
						{
							TimeSpan ts10_1 = new TimeSpan(Convert.ToDateTime(reader["g"].ToString()).Ticks - Convert.ToDateTime(reader["f"].ToString()).Ticks);
							FFF -= ts10_1.Days;
						}


						GGG = EE;
						_updatesql += "update SwcTimeAnalysis set AA = '" + AA.ToString() + "',BB = '" + BB.ToString() + "',CC = '" + CC.ToString() + "',EE = '" + EE.ToString() + "',AAA = '" + AAA.ToString() + "',BBB = '" + BBB.ToString() + "',CCC = '" + CCC.ToString() + "',DDD = '" + DDD.ToString() + "',EEE = '" + EEE.ToString() + "',FFF = '" + FFF.ToString() + "',GGG = '" + GGG.ToString() + "' where SWC00 = '" + reader["SWC00"] + "' ;";
					}
				}
				cmd.Cancel();
			}
		}
		if (_updatesql != "")
		{
			using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
			{
				SWCConn.Open();
				using (var cmd = SWCConn.CreateCommand())
				{
					cmd.CommandText = _updatesql;
					#region.設定值
					#endregion
					cmd.ExecuteNonQuery();
					cmd.Cancel();
				}
			}
		}
	}
}