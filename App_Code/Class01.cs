using EASendMail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Class01 的摘要描述
/// </summary>
public class Class01: System.Web.UI.Page
{
    public Class01()
    {
        //
        // TODO: 在這裡新增建構函式邏輯
        //
    }
    public Boolean ChkUserAuth(string cPage)
    {
        string SS_ID = Session["ID"] + "";
        string SS_PW = Session["PW"] + "";
        
        string NormalAuth = "Y";

        if (SS_ID == "")
        {
            NormalAuth = "N";
            //Response.Redirect("../Login.aspx");
        }
        

        
        //'Session("name") = "系統管理員"
        //'Session("grade") = ""
        //'Session("uid") = "gv-admin"
        //'Session("right") = "系統管理員"

        

        if (NormalAuth == "Y") { return true; } else { return false; } 









        


        //if (NormalAuth == "Y")
        //{
        //    if (!CheckPageAuth(cPage, SS_ID, SS_PW))
        //    {
        //        NormalAuth = "N";
        //    }
        //}

        

    }

    public string SQLstrValue(string cSQLstr)
    {
        //變數，過濾單引

        //string okSQLstr = cSQLstr.Replace("'", "''");

        return cSQLstr;
    }
    private Boolean CheckPageAuth(string cPage, string UserID, string UserPW)
    {
        Boolean UserAuth = false;
        string UserMbGroup = "", UserDataView = "", UserDataEdit = "";
        string strSQLRV = "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            strSQLRV = "select * from geouser where userid='" + UserID + "' and passwd = '" + UserPW + "' and status <>'停用' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUSER = new SqlCommand(strSQLRV, UserConn);
            readerUser = objCmdUSER.ExecuteReader();

            while (readerUser.Read())
            {
                UserMbGroup = readerUser["mbgroup"] + "";
                UserDataView = ";" + readerUser["tcgedataview"] + "";
                UserDataEdit = ";" + readerUser["tcgedataedit"] + "";

            }
            readerUser.Close();
            objCmdUSER.Dispose();

            switch (cPage)
            {
                case "UserEditAuth":
                    if (UserMbGroup == "系統管理員")
                    {
                        UserAuth = true;
                    }
                    break;

                default:
                    if (UserDataView.IndexOf(";" + cPage + ";", StringComparison.OrdinalIgnoreCase) >= 0) { UserAuth = true; }
                    if (UserDataEdit.IndexOf(";" + cPage + ";", StringComparison.OrdinalIgnoreCase) >= 0) { UserAuth = true; }
                    break;
            }


        }

        return UserAuth;
    }
    
    public void LoginNotes(string UserID, string LoginStatus)
    {
        Class1 C1 = new Class1();
        string clientIP = C1.GetClientIP();
        string strSQLErr = "";

        LoginStatus = LoginStatus + "";
        if (LoginStatus != "0") { LoginStatus = "1"; }

        //從資料庫取得資料
        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        //strSQLErr = " update geouser set loginstaus = '" + LoginStatus + "' where userid='" + UserID + "';";
        strSQLErr = strSQLErr + " insert into loginnotes (userid,success,logtime,loginip) values ";
        strSQLErr = strSQLErr + " ('" + UserID + "','" + LoginStatus + "',getdate(),'" + clientIP + "');";

        SqlCommand objCmdRV = new SqlCommand(strSQLErr, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

    }
    public void UpdatePayMent(string tPayCode14)
    {
        GBClass001 GBA = new GBClass001();
        //tPayCode14, 14碼
        string tFR001 = "", tSB006 = "", tSB011 = "", tSB012 = "", exeSql1 = "", exeSql2 = "";
        string tPayCode16 = tPayCode14.Substring(0, 5) + "00" + tPayCode14.Substring(5);

        string exeSQLstrR = " select * from SwcBill where SB002='" + tPayCode14 + "'";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            SqlDataReader readerTslm;
            SqlCommand objCmdTslm = new SqlCommand(exeSQLstrR, TslmConn);
            readerTslm = objCmdTslm.ExecuteReader();

            while (readerTslm.Read())
            {
                tFR001 = readerTslm["FR001"] + "";
                tSB006 = readerTslm["SB006"] + "";
                tSB011 = readerTslm["SB011"] + "";
                tSB012 = readerTslm["SB012"] + "";
                tSB012 = GBA.DateView(tSB012, "00");


            }
            readerTslm.Close();
            objCmdTslm.Dispose();
        }
        #region 已繳審查費處理
        Class20 C20 = new Class20();
        Class1 C1 = new Class1();		
        if (tSB006 == "審查費") {
            string tmpPayDate = GBA.DateView(C1.getSWCSWCData(tFR001,"SWC33"),"00");
            string tmpCaseSataus = C1.getSWCSWCData(tFR001, "SWC04");
            string tmpSWC002 = C1.getSWCSWCData(tFR001, "SWC02");
            string tmpSWC005 = C1.getSWCSWCData(tFR001, "SWC05");
            string tmpSWC023 = C1.getSWCSWCData(tFR001, "SWC23");
            string tmpSWC108 = C1.getSWCSWCData(tFR001, "SWC108");
            string tmpSWC013TEL = C1.getSWCSWCData(tFR001, "SWC013TEL");
            string tmpSWC021ID = C1.getSWCSWCData(tFR001, "SWC021ID");
            string tmpSWC022ID = C1.getSWCSWCData(tFR001, "SWC022ID");
            string tETEmail = GBA.DateView(GBA.GetETUser(tmpSWC021ID, "Email"), "00");
            string tmpSWC025 = C1.getSWCSWCData(tFR001, "SWC25");
            string tmpSWC025Mail = C20.GetGeoUserID(tmpSWC025, "email");
            //string tmpSWC088 = tmpSWC002.IndexOf("-")>0 ? Convert.ToDateTime(tSB012).AddDays(60).ToString("yyyy-MM-dd"): Convert.ToDateTime(tSB012).AddDays(90).ToString("yyyy-MM-dd");	//受理日期+90為審查期限, 但變更設計是+60天
            string tmpSWC088 = tmpSWC002.IndexOf("-") > 0 ? Convert.ToDateTime(tSB012).AddDays(60).ToString("yyyy-MM-dd") : Convert.ToDateTime(tSB012).AddDays(92).ToString("yyyy-MM-dd");	//20210114 凱暉剛說  改"+92"天，受理日期+90為審查期限, 但變更設計是+60天
          
			if (tmpPayDate.Trim() == "") {
                if (tmpCaseSataus== "受理中"){
                    #region 案件狀態為「受理中」，繳交審查費後，案件狀態改為「審查中」
                    string sqlStr = " update SWCSWC set SWC04=@SWC004,SWC34=@SWC034,SWC88=@SWC088 where SWC00=@CaseId; ";
                    sqlStr += " update TCGESWC.dbo.SWCCASE set SWC004=@SWC004,SWC034=@SWC034,SWC088=@SWC088 where SWC000=@CaseId; ";
                    using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        TslmConn.Open();
                        using (var cmd = TslmConn.CreateCommand())
                        {
                            cmd.CommandText = sqlStr;
                            cmd.Parameters.Add(new SqlParameter("@CaseId", tFR001));
                            cmd.Parameters.Add(new SqlParameter("@SWC004", "審查中"));
                            cmd.Parameters.Add(new SqlParameter("@SWC034", tSB012));
                            cmd.Parameters.Add(new SqlParameter("@SWC088", tmpSWC088));
                            cmd.ExecuteNonQuery();
                            cmd.Cancel();
                        }
                    }
                    #endregion
                    WriteGuildListRC(tFR001, tmpSWC002, tmpSWC005, tmpSWC022ID);
					
                    //寄信通知：
                    //承辦技師寄email、義務人傳簡訊、聯絡人寄email、承辦人email
                    //您好，【水土保持計畫】已委託公會審查，審查期限至【審查期限】；如因法令變更或其他不可抗力因素，可申請暫停審查(以1次且不超過3個月為限)。
                    string[] arraySentMail01 = new string[] { tETEmail, tmpSWC108, tmpSWC025Mail };
                    string ssMailSub01 = "您好，【" + tmpSWC005 + "】已委託審查單位審查。";
                    string ssMailBody01 = "您好，【" + tmpSWC005 + "】已委託審查單位審查，審查期限至【"+ tmpSWC088 + "】；如因法令變更或其他不可抗力因素，可申請暫停審查(以1次且不超過3個月為限)。" + "<br><br>";
					ssMailBody01 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                    ssMailBody01 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                    bool MailTo01 = GBA.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
					
                    //義務人(簡訊)
                    string msmUserMsg01 = "您好，【" + tmpSWC005 + "】已委託審查單位審查，審查期限至【" + tmpSWC088 + "】；如因法令變更或其他不可抗力因素，可申請暫停審查(以1次且不超過3個月為限)。";
                    
					//GBA.SendSMS(tmpSWC013TEL, msmUserMsg01);
					string[] arrayPhoneNo = tmpSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
					GBA.SendSMS_Arr(arrayPhoneNo, msmUserMsg01);
					
                    //審查公會
                    //【審查公會】您好，【水土保持計畫】已委託貴公會審查：
                    //一、本案面積為【計畫面積】公頃，審查小組組織及審議規則請依「臺北市政府工務局大地工程處代辦水土保持申請書件審查工作共同供應契約審查小組組織及審議規定」辦理；第一次審查會議應邀承辦設計建築師出席說明。【本案屬林地、保安林應邀請森林遊憩科會同審查】【本案屬聯外排水排放至溪溝應邀請土石流防治科會同審查】【本案屬列管步道、山區道路應邀請道路步道科會同審查】【本案屬國家公園應邀請陽管處會同審查】【本案屬農業開發應邀請產業發展局會同審查】【本案屬聯外排水事項應邀請水利工程處會同審查】。
                    //二、本水土保持計畫請依「臺北市山坡地水土保持管理 - 水土保持計畫審核標準作業流程」規定，於【審查期限】前(含技師補正作業時間）完成審查，並將審查結果（建議核定或不予核定）提交本處，審查過程如須補正，授權貴公會彈性調整補正期限及次數；相關行政作業請利用臺北市水土保持申請書件管理平台辦理。
                    //三、如因法令變更或其他不可抗力因素，致水土保持計畫須大幅修正，水土保持義務人得申請暫停審查，以1次為限， 暫停期間不超過3個月為原則。
                    string tmpGuildName = GBA.GetGeoUser(tmpSWC022ID, "Name");
					tmpGuildName = tmpGuildName==""?C1.getSWCSWCData(tFR001, "SWC22"):tmpGuildName;
                    string tmpSwc121 = swcData1(tFR001, "SWC121");
					
                    string[] arraySentMail02 = new string[] { GBA.GetGeoUser(tmpSWC022ID, "Email") };
                    string ssMailSub02 = "【"+ tmpGuildName + "】您好，【" + tmpSWC005 + "】已委託貴單位審查。";
                    string ssMailBody02 = "【" + tmpGuildName + "】您好，【" + tmpSWC005 + "】已委託貴單位審查：" + "<br><br>";
                    ssMailBody02 += "一、本案面積為【"+tmpSWC023+"】公頃，審查小組組織及審議規則請依「臺北市政府工務局大地工程處代辦水土保持申請書件審查工作共同供應契約審查小組組織及審議規定」辦理；第一次審查會議應邀承辦設計建築師出席說明。";
                    ssMailBody02 += tmpSwc121.IndexOf("森林遊憩科") > -1 ? "【本案屬林地、保安林應邀請森林遊憩科會同檢查】" : "";
                    ssMailBody02 += tmpSwc121.IndexOf("土石流防治科") > -1 ? "【本案屬聯外排水排放至溪溝應邀請土石流防治科會同檢查】" : "";
                    ssMailBody02 += tmpSwc121.IndexOf("道路步道科") > -1 ? "【本案屬列管步道、山區道路應邀請道路步道科會同檢查】" : "";
                    ssMailBody02 += tmpSwc121.IndexOf("國家公園") > -1 ? "【本案屬國家公園應邀請陽管處會同檢查】" : "";
                    ssMailBody02 += tmpSwc121.IndexOf("產業發展局") > -1 ? "【本案屬農業開發應邀請產業發展局會同檢查】" : "";
                    ssMailBody02 += tmpSwc121.IndexOf("水利工程處") > -1 ? "【本案屬聯外排水事項應邀請水利工程處會同檢查】" : "";
                    ssMailBody02 += "。<br><br>";
                    ssMailBody02 += "二、本水土保持計畫請依「臺北市山坡地水土保持管理 - 水土保持計畫審核標準作業流程」規定，於【"+ tmpSWC088 + "】前(含技師補正作業時間）完成審查，並將審查結果（建議核定或不予核定）提交本處，審查過程如須補正，授權貴單位彈性調整補正期限及次數；相關行政作業請利用臺北市水土保持申請書件管理平台辦理。" + "<br><br>";
                    ssMailBody02 += "三、如因法令變更或其他不可抗力因素，致水土保持計畫須大幅修正，水土保持義務人得申請暫停審查，以1次為限， 暫停期間不超過3個月為原則。" + "<br><br>";
                    ssMailBody02 += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                    ssMailBody02 += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                    bool MailTo02 = GBA.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02);
				}
			}
		}
        #endregion
		
        string sTTLPay1 = EPayCoin(tFR001, tSB006);
        string sTTLPay2 = EPayCoinM(tFR001, tSB006);
        string ppppppp = Convert.ToInt32(sTTLPay1.Replace(",", "")) == Convert.ToInt32(sTTLPay2) ? "繳清" : "";

        switch (tSB006)
        {
            case "罰鍰":
                exeSql1 = " update ILGILG set ILG043='" + ppppppp + "', ILG045='" + sTTLPay2 + "', ILG046='" + tSB012 + "' where ILG001=@CaseId;";
                break;
            case "代為履行":
                exeSql1 = " update ILGILG set ILG083='" + ppppppp + "', ILG085='" + sTTLPay2 + "', ILG086='" + tSB012 + "' where ILG001=@CaseId;";
                break;
            case "審查費":
                exeSql1 = " update SWCSWC set SWC33='" + tSB012 + "' where SWC00=@CaseId;";
                exeSql2 = " update TCGESWC.dbo.SWCCASE set SWC033='" + tSB012 + "' where SWC000=@CaseId;";
                break;
            case "保證金":
                exeSql1 = " update SWCSWC set SWC41='已繳交' where SWC00=@CaseId;";
                exeSql2 = " update TCGESWC.dbo.SWCCASE set SWC041='已繳交' where SWC000=@CaseId;";
                break;
        }

        string exeSQLstr = " Update CasePaymentInfo set CPI004=@CPI004,CPI006=@CPI006 where BillID=@BillID ";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = exeSql1 + exeSql2 + exeSQLstr;
                cmd.Parameters.Add(new SqlParameter("@BillID", tPayCode16));
                cmd.Parameters.Add(new SqlParameter("@CaseId", tFR001));
                cmd.Parameters.Add(new SqlParameter("@CPI004", tSB012));
                cmd.Parameters.Add(new SqlParameter("@CPI006", "已繳納"));
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
    }
    private void WriteGuildListRC(string vSWC000, string vSWC002, string vSWC005, string vGurildId)
    {
        string ssName = Session["name"] + "";
        Class1 C1 = new Class1();
        string tmpSqlStr = " INSERT INTO [GuildShiftsList] ([SWC000],[SWC002],[SWC005],[GSLType],[GSL001],[GSL002],[GSL003],[Savedate],[saveuser]) VALUES ";
        tmpSqlStr += "(@SWC000,@SWC002,@SWC005,@GSLType,@GSL001,@GSL002,@GSL003,getdate(),@saveuser); ";
        tmpSqlStr += " Update GuildShifts Set GS005=ISNULL(GS005,0)+1, GS007=ISNULL(GS007,0)-1 Where GS002='" + vGurildId + "' and GS001=1; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = tmpSqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", vSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC002", vSWC002));
                cmd.Parameters.Add(new SqlParameter("@SWC005", vSWC005));
                cmd.Parameters.Add(new SqlParameter("@GSLType", "審查"));
                cmd.Parameters.Add(new SqlParameter("@GSL001", vGurildId));   //公會id
                cmd.Parameters.Add(new SqlParameter("@GSL002", GetGSL002(vSWC000)));    //指派原因
                cmd.Parameters.Add(new SqlParameter("@GSL003", DateTime.Now.ToString("yyyy-MM-dd")));    //開工日期
                cmd.Parameters.Add(new SqlParameter("@saveuser", ssName));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
		
        //輪值公會log
        GuildShifts_Log(vSWC000, "", "", "", "", vGurildId, "-1", "+1", "審查", "對帳");
    }
    private string GetGSL002(string vSWC000)
    {
        string rValue = "依輪值序位排審";
        string sqlStr = " select top 1 * from SwcApply2002 where SWC000=@SWC000 order by id desc; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                cmd.Parameters.Add(new SqlParameter("@SWC000", vSWC000));
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        while (reader.Read())
                            rValue = (reader["SABC014D2a"] + "").Trim()==""? "依輪值序位排審" : reader["SABC014D2a"] + "";
                    reader.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
    public string swcData1(string tSwcId, string tFD)
    {
        string rValue = "";
        #region swcArea1
        string sqlStr1 = " SELECT * FROM SWCSWCA where SWC000=@SWC000; ";
        ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionStringTslm.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
                cmd.Parameters.Add(new SqlParameter("@SWC000", tSwcId));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string qSWC120 = readerTslm[tFD] + "";
                            rValue = qSWC120;
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    private string EPayCoinM(string tCode, string tPayType)
    {
        string rValue = "";
        string exeSQLstrZ = "";
        switch (tPayType)
        {
            case "罰鍰":
                exeSQLstrZ = " select sum(convert(int,replace(isnull(CPI003,'0'),',',''))) as xxx from CasePaymentInfo where caseid = '" + tCode + "' and casetype='罰鍰' and CPI006='已繳納' ";
                break;
            case "代為履行":
                exeSQLstrZ = " select sum(convert(int,replace(isnull(CPI003,'0'),',',''))) as xxx from CasePaymentInfo where caseid = '" + tCode + "' and casetype='代為履行' and CPI006='已繳納' ";
                break;
            case "審查費":
                //exeSQLstrZ = " select EPayPrice from SWCSWC where SWC00='" + tCode + "'; ";
                break;
            case "保證金":
                //exeSQLstrZ = " select EPayPrice from SWCSWC where SWC00='" + tCode + "'; ";
                break;
        }
        if (exeSQLstrZ != "")
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
            {
                TslmConn.Open();
                SqlDataReader readerTslm;
                SqlCommand objCmdTslm = new SqlCommand(exeSQLstrZ, TslmConn);
                readerTslm = objCmdTslm.ExecuteReader();

                while (readerTslm.Read())
                {
                    rValue = readerTslm["xxx"] + "";
                }
                readerTslm.Close();
                objCmdTslm.Dispose();
            }
        }
        if (rValue.Trim() == "")
            rValue = "0";
        return rValue;
    }
    private string EPayCoin(string tCode, string tPayType)
    {
        string rValue = "";
        string exeSQLstrZ = "";
        switch (tPayType)
        {
            case "罰鍰":
                exeSQLstrZ = " select ILG041 as EPayPrice from ILGILG where ILG001='" + tCode + "'; ";
                break;
            case "代為履行":
                exeSQLstrZ = " select ILG081 as EPayPrice from ILGILG where ILG001='" + tCode + "'; ";
                break;
            case "審查費":
                //exeSQLstrZ = " select EPayPrice from SWCSWC where SWC00='" + tCode + "'; ";
                break;
            case "保證金":
                //exeSQLstrZ = " select EPayPrice from SWCSWC where SWC00='" + tCode + "'; ";
                break;
        }
        if (exeSQLstrZ != "")
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
            using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
            {
                TslmConn.Open();
                SqlDataReader readerTslm;
                SqlCommand objCmdTslm = new SqlCommand(exeSQLstrZ, TslmConn);
                readerTslm = objCmdTslm.ExecuteReader();

                while (readerTslm.Read())
                {
                    rValue = readerTslm["EPayPrice"] + "";
                }
                readerTslm.Close();
                objCmdTslm.Dispose();
            }
        }
        if (rValue.Trim() == "")
            rValue = "0";
        return rValue;
    }
	
    //輪值公會Log
    public void GuildShifts_Log(string qGSL001, string qGSL0011, string qGSL002, string qGSL0021, string qGSL0022, string qGSL003, string qGSL0031, string qGSL0032, string qGSL004, string qGSL005)
    {
        string strSQL = " INSERT INTO GuildShifts_Log ([GSL001], [GSL0011], [GSL002], [GSL0021], [GSL0022], [GSL003], [GSL0031], [GSL0032], [GSL004], [GSL005], [savedate], [saveuser]) VALUES ";
        strSQL += "(@GSL001, @GSL0011, @GSL002, @GSL0021, @GSL0022, @GSL003, @GSL0031, @GSL0032, @GSL004, @GSL005, getdate(), @saveuser)";

        string ssName = Session["name"] + "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection HeoConn = new SqlConnection(connectionString.ConnectionString))
        {
            HeoConn.Open();
            using (var cmd = HeoConn.CreateCommand())
            {
                cmd.CommandText = strSQL;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@GSL001", qGSL001));
                cmd.Parameters.Add(new SqlParameter("@GSL0011", qGSL0011));
                cmd.Parameters.Add(new SqlParameter("@GSL002", qGSL002));
                cmd.Parameters.Add(new SqlParameter("@GSL0021", qGSL0021));
                cmd.Parameters.Add(new SqlParameter("@GSL0022", qGSL0022));
                cmd.Parameters.Add(new SqlParameter("@GSL003", qGSL003));
                cmd.Parameters.Add(new SqlParameter("@GSL0031", qGSL0031));
                cmd.Parameters.Add(new SqlParameter("@GSL0032", qGSL0032));
                cmd.Parameters.Add(new SqlParameter("@GSL004", qGSL004));
                cmd.Parameters.Add(new SqlParameter("@GSL005", qGSL005));
                cmd.Parameters.Add(new SqlParameter("@saveuser", ssName));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
            HeoConn.Close();
        }
    }
	
}