using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NF000_mailcheck2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //46.僅處理「我的待辦」寄信功能。
        //每日檢查待辦報表清單，並寄信列出清單
        checkMyEvent();
    }

    private void checkMyEvent()
    {
        GBClass001 SBApp = new GBClass001();
        Class1 C1 = new Class1();
        List<sendList> MailList = new List<sendList>();

        string exeSQL2 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA02001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply02 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL3 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA03001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply03 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL4 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA04001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply04 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL5 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA05001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply05 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL6 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA06001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply06 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL7 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA07001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply07 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL8 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA08001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply08 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";
        string exeSQL9 = " select SING002 as TNAME,SWC000,SWC002,SWC005,ONA09001 as ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from OnlineApply09 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存' ; ";

        string exeSQL16 = " select SING002 as TNAME,SWC000,SWC002,SWC005,DTLF001 AS ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from SWCDTL06 where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存'; ";

        string exeSQL21 = " select SING002 as TNAME,SWC000,SWC002,SWC005,SAA001 AS ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from tslm2.dbo.SwcApply2001 Where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存'; ";
        string exeSQL22 = " select SING002 as TNAME,SWC000,SWC002,SWC005,SAB001 AS ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from tslm2.dbo.SwcApply2002 Where SING008='待簽辦' or SING008='待覆閱' or SING008='暫存'; ";
        
        string exeSQLDuty = "select CASE WHEN A.簽核狀態='陳核中' THEN 承辦人員 WHEN A.簽核狀態='退文' and ISNULL(承辦人員,'')='' THEN 執勤人員 WHEN A.簽核狀態='退文' and ISNULL(承辦人員,'')!='' THEN 承辦人員 WHEN A.簽核狀態='決行中' THEN 決行人員 WHEN A.簽核狀態='覆閱中' and ISNULL(執勤員覆閱,'')='' and ISNULL(承辦員覆閱,'')='' THEN 執勤人員+';'+承辦人員 WHEN A.簽核狀態='覆閱中' and ISNULL(執勤員覆閱,'')='' THEN 執勤人員 WHEN A.簽核狀態='覆閱中' and ISNULL(承辦員覆閱,'')='' THEN 承辦人員 ELSE '' END as TNAME, '' as SWC000,'' as SWC002,'' as SWC005,A.報表編號 as ONA001,簽核狀態 as ONAStatus,'' as reView,'' as reViewOK from tslm2.dbo.duty A left join (select 報表編號,MAX(流水號) as 流水號 from tslm2.dbo.dutyapprove group by 報表編號) B ON A.報表編號=B.報表編號 left join tslm2.dbo.dutyapprove C ON B.流水號 = C.流水號 ;";
		string exeSQLPatrol = "select CASE WHEN A.簽核狀態='陳核中' THEN 承辦人員 WHEN A.簽核狀態='退文' and ISNULL(承辦人員,'')='' THEN 執勤人員 WHEN A.簽核狀態='退文' and ISNULL(承辦人員,'')!='' THEN 承辦人員 WHEN A.簽核狀態='決行中' THEN 決行人員 WHEN A.簽核狀態='覆閱中' and ISNULL(執勤員覆閱,'')='' and ISNULL(承辦員覆閱,'')='' THEN 執勤人員+';'+承辦人員 WHEN A.簽核狀態='覆閱中' and ISNULL(執勤員覆閱,'')='' THEN 執勤人員 WHEN A.簽核狀態='覆閱中' and ISNULL(承辦員覆閱,'')='' THEN 承辦人員 ELSE '' END as TNAME, '' as SWC000,'' as SWC002,'' as SWC005,A.報表編號 as ONA001,簽核狀態 as ONAStatus,'' as reView,'' as reViewOK from tslm2.dbo.patrol A left join (select 報表編號,MAX(流水號) as 流水號 from tslm2.dbo.patrolapprove group by 報表編號) B ON A.報表編號=B.報表編號 left join tslm2.dbo.patrolapprove C ON B.流水號 = C.流水號 ;";
		string exeSQLSwcgtb = " select SING002 as TNAME,'' as SWC000,'' as SWC002,'' as SWC005,報表編號 AS ONA001,SING008 as ONAStatus,SING004 as reView,SING005 as reViewOK from tslm2.dbo.Swcgtb ;";
		string exeSQLSatellite = "select CASE WHEN A.簽核狀態='陳核中' and ISNULL(承辦人員,'')='' THEN 執勤人員 WHEN A.簽核狀態='陳核中' and ISNULL(承辦人員,'')!='' THEN 承辦人員 WHEN A.簽核狀態='退文' and ISNULL(決行人員,'')='' THEN 承辦人員 WHEN A.簽核狀態='退文' and ISNULL(承辦人員,'')!='' THEN 執勤人員 WHEN A.簽核狀態='覆閱中' and ISNULL(執勤員覆閱,'')='' and ISNULL(承辦員覆閱,'')='' THEN 執勤人員+';'+承辦人員 WHEN A.簽核狀態='覆閱中' and ISNULL(執勤員覆閱,'')='' THEN 執勤人員 WHEN A.簽核狀態='覆閱中' and ISNULL(承辦員覆閱,'')='' THEN 承辦人員 ELSE '' END as TNAME, '' as SWC000,'' as SWC002,'' as SWC005,A.報表編號 as ONA001,簽核狀態 as ONAStatus,'' as reView,'' as reViewOK from tslm2.dbo.satellite A  left join (select * from earthService.dbo.earth_tabel) B ON A.報表編號 = convert(nvarchar(30),B.year)+'-'+convert(nvarchar(30),B.month)+'-'+convert(nvarchar(30),B.num)+'-'+convert(nvarchar(30),B.case_type) ;";
		
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        string[] aExeSql = new string[] { exeSQL2, exeSQL3, exeSQL4, exeSQL5, exeSQL6, exeSQL7, exeSQL8, exeSQL9, exeSQL16, exeSQL21, exeSQL22, exeSQLDuty, exeSQLPatrol, exeSQLSwcgtb, exeSQLSatellite };
        string[] arrayHeader1Str = new string[] { "暫停審查", "開工/復工展延", "水土保持計畫開工/復工申報書", "設施調整報備", "義務人及技師變更報備", "工期展延", "停工申請", "完工申報", "完工查核表", "建議核定", "受理查核表", "執勤日報表", "市地巡查紀錄表", "水保輔導複查表", "衛星變異點查證表"};
        for (int j = 0; j < aExeSql.Length; j++)
        {
            string exeSQL = aExeSql[j];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();
                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(exeSQL, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                while (readeSwc.Read())
                {
                    string tmpName= readeSwc["TNAME"] + "";
                    string tmpSWC000 = readeSwc["SWC000"] + "";
                    //string tmpSWC002 = readeSwc["SWC002"] + "";
                    string tmpSWC005 = readeSwc["SWC005"] + "";
                    string tmpONA001 = readeSwc["ONA001"] + "";
                    string tmpStatus = readeSwc["ONAStatus"] + "";
                    string tmpReView = readeSwc["reView"] + "";
                    string tmpReViewOK = readeSwc["reViewOK"] + "";
					string tmpLDate = ""; string tmpSWC002 = ""; string tmpText = ""; string[] aReViewOK = {}; string reView = ""; string sendAry = ""; string[] arraySentMail01 = {};
					if(arrayHeader1Str[j] != "執勤日報表" && arrayHeader1Str[j] != "市地巡查紀錄表" && arrayHeader1Str[j] != "水保輔導複查表" && arrayHeader1Str[j] != "衛星變異點查證表"){
						tmpLDate = getSendDate(tmpSWC000, tmpONA001);
						tmpSWC002 = C1.getSWCSWCData(tmpSWC000, "SWC02");
						tmpText = "【"+ tmpSWC005 + "】("+ tmpSWC002 + ") 【"+ arrayHeader1Str [j]+ "】，已於【"+ tmpLDate + "】陳核至您的待辦清單，尚未進行簽辦作業。";
					
						aReViewOK = tmpReViewOK.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
						for (int z = 0; z < aReViewOK.Length; z++)
							tmpReView = tmpReView.Replace(aReViewOK[z],"");
						reView = tmpReView;
						sendAry = tmpStatus == "待覆閱" ? tmpReView.Replace(";;",";") : tmpName + ";";
						arraySentMail01 = sendAry.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
					}else{
						if(arrayHeader1Str[j] == "水保輔導複查表")
						{
							tmpText = "【"+ arrayHeader1Str [j] + "】("+ tmpONA001 + ")已陳核至您的待辦清單，尚未進行簽辦作業。";
						
							aReViewOK = tmpReViewOK.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
							for (int z = 0; z < aReViewOK.Length; z++)
								tmpReView = tmpReView.Replace(aReViewOK[z],"");
							reView = tmpReView;
							sendAry = tmpStatus == "待覆閱" ? tmpReView.Replace(";;",";") : tmpName + ";";
							arraySentMail01 = sendAry.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
						}
						
						else{
							tmpText = "【"+ arrayHeader1Str[j] + "】("+ tmpONA001 + ")已陳核至您的待辦清單，尚未進行簽辦作業。";
							
							sendAry = tmpName;
							arraySentMail01 = sendAry.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
						}
					}
                    for (int z = 0; z < arraySentMail01.Length; z++) {
                        string strName = arraySentMail01[z];
                        if (reView.IndexOf(strName) > -1 || tmpStatus!= "待覆閱") {
                            reView = reView.Replace(strName, "");
                            sendList mail = MailList.Where(p => p.uName == strName).FirstOrDefault();
                            if (mail == null)
                            {
                                string tmpMail = getUserMsg(strName, "email"); //tmpMail = "tim@geovector.com.tw";
                                string tmpSub = "【" + strName + "】【" + getUserMsg(strName, "jobtitle") + "】您好，【" + DateTime.Now.ToString("yyyy-MM-dd") + "】待辦報表檢核通知信";
                                bool tmpSend = false;
								if(getUserMsg(strName, "status")=="正常") tmpSend = true;
								MailList.Add(new sendList() { uName = strName, uEmail = tmpMail, uMailSub = tmpSub, mailText = tmpText, send = tmpSend });
								
                            }
                            else
                            {
                                mail.mailText += "<br/>" + tmpText;
                            }
                        }
                    }
                }
                readeSwc.Close();
                objCmdSwc.Dispose();
            }
        }
		bool ssssssss=true;
        foreach (sendList p in MailList)
        {
			if(p.send == true){
				string[] mailTo = new string[]{ p.uEmail };
				ssssssss = SBApp.Mail_Send(mailTo,p.uMailSub,p.mailText);
			}
		}
		if(ssssssss){
        Response.Write("寄信完畢");}else{
        Response.Write("寄信失敗");}
    }

    private string getSendDate(string tmpSWC000,string tmpONA001)
    {
        string rValue = "";
        string exeSql = " select top 1 CONVERT(char(10), R004, 120) as LDate from tslm2.dbo.SignRCD where swc000='" + tmpSWC000 + "' and ONA001='"+ tmpONA001 + "' order by id desc; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            SqlDataReader readeTslm;
            SqlCommand objCmdTslm = new SqlCommand(exeSql, TslmConn);
            readeTslm = objCmdTslm.ExecuteReader();

            while (readeTslm.Read())
                rValue = readeTslm["LDate"] + "";
            readeTslm.Close();
            objCmdTslm.Dispose();
        }
        return rValue;
    }

    private string getUserMsg(string uName,string uType)
    {
        string rValue = "";
        string exeSql = " select * from geouser where [name] = '"+ uName .Trim()+ "'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            SqlDataReader readeTslm;
            SqlCommand objCmdTslm = new SqlCommand(exeSql, TslmConn);
            readeTslm = objCmdTslm.ExecuteReader();

            while (readeTslm.Read())
                rValue = readeTslm[uType] + "";
            readeTslm.Close();
            objCmdTslm.Dispose();
        }
        return rValue;
    }

    public class sendList
    {
        public string uName { get; set; }
        public string uEmail { get; set; }
        public string uMailSub { get; set; }
        public string mailText { get; set; }
        public bool send { get; set; }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        checkMyEvent();
    }
}