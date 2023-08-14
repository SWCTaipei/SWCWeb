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

public partial class SWCDOC_SWCBase001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rDETID = Request.QueryString["DisEventId"] + "";

        string SessETID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        GBClass001 SBApp = new GBClass001();

        if (ssUserType == "03")
        {
            if (!IsPostBack) { GenerateDropDownList(); GetDisasterEventData(rDETID); }
        } else
        {
            Response.Redirect("../SWCDOC/SWC001.aspx");
        }

        switch (ssUserType)
        {
            case "01":
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            case "02":
                TitleLink00.Visible = true;
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            case "03":  //大地人員
                GoTslm.Visible = true;
                GOVMG.Visible = true;
                //if (!IsPostBack) { GenerateDropDownList(); }
                break;
            case "04":
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            default:
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
        }




        //以下全區公用
        SBApp.ViewRecord("防災事件通知", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
		Page.MaintainScrollPositionOnPostBack = true;
    }
    private void GenerateDropDownList()
    {
        //通知事件
        string[,] array_District1 = new string[,] { { "0104", "颱風豪雨通知回傳自主檢查表" }, { "0203", "違規案件防災整備通知" } };
													   
        for (int te = 0; te < array_District1.GetLength(0); te++)
        {
            DDLDETYPE.Items.Add(new ListItem(array_District1[te, 1], array_District1[te, 0]));
							 
        }
													 

        //水保案件狀態
        //string[] array_DropDownList1 = new string[] { "退補件", "不予受理", "受理中", "審查中", "暫停審查", "撤銷", "不予核定", "已核定", "施工中", "停工中", "已完工", "廢止", "失效", "已變更" };
        //CHKSWCSTATUS.DataSource = array_DropDownList1;
        //CHKSWCSTATUS.DataBind();

        //水保通知對象
        string[] array_DropDownList2 = new string[] { "義務人", "承辦技師", "審查單位", "監造技師", "檢查單位"};
        CHKSENDMBR.DataSource = array_DropDownList2;
        CHKSENDMBR.DataBind();

        //水保通知管道
        string[] array_DropDownList3 = new string[] { "簡訊", "E-mail" };
        CHKSENDFUN.DataSource = array_DropDownList3;
        CHKSENDFUN.DataBind();
		
		//行政區
        string[] array_DropDownList4 = new string[] { "北投", "士林", "大安", "中山", "內湖", "文山", "中正", "信義", "南港" };
        CHKDISTRICT.DataSource = array_DropDownList4;
        CHKDISTRICT.DataBind();
		
		//違規案件狀態
        string[] array_DropDownList5 = new string[] { "續予處分", "改正中", "無改正事項", "程序補正中" };
        CHKSWCSTATUS_ILG.DataSource = array_DropDownList5;
        CHKSWCSTATUS_ILG.DataBind();

        //違規通知對象
        string[] array_DropDownList6 = new string[] { "受處分人" };
        CHKSENDMBR_ILG.DataSource = array_DropDownList6;
        CHKSENDMBR_ILG.DataBind();

        //違規通知管道
        string[] array_DropDownList7 = new string[] { "簡訊" };
        CHKSENDFUN_ILG.DataSource = array_DropDownList7;
        CHKSENDFUN_ILG.DataBind();
		
    }

    private void GetDisasterEventData(string rCaseID)
    {
        if (rCaseID == "ADDNEW")
        {
            string sDEID = GetNewId();
            string sTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            string sMailText = "臺北市政府工務局大地工程處(審查管理科)通報施工中水土保持計畫水保義務人及監造技師注意：為防範【】颱風來襲，請儘速完成工地自主檢查及防災整備，並於【"+ sTomorrow + "】下午6時前至「臺北市水土保持申請書件管理平台」填寫「颱風豪雨設施自主檢查表」，如有災情應儘速處理，並回報目的事業主管機關及本處。";
            TXTDENO.Text = sDEID;
            DDLDETYPE.SelectedItem.Text = "颱風豪雨通知回傳自主檢查表";
            TXTDENTTEXT.Text = sMailText;
            CHKSWCSTATUS.SelectedValue = "施工中";
			
			LockSWC(true);
			LockILG(false);
        } else
        {
            ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
            {
                SwcConn.Open();

                string gDNSQLStr = " select * from DisasterEvent ";
                gDNSQLStr = gDNSQLStr + " where DENo ='" + rCaseID + "' ";

                SqlDataReader readerDN;
                SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
                readerDN = objCmdDN.ExecuteReader();

                while (readerDN.Read())
                { 
                    string sDENo = readerDN["DENo"] + "";
                    string sDETYPE = readerDN["DETYPE"] + "";
                    
					string sDESWCSTATUS = readerDN["SWCSTATUS"] + "";
                    string sSENDMBR = readerDN["SENDMBR"] + "";
                    string sSENDFUN = readerDN["SENDFUN"] + "";
					
                    string sDEILGSTATUS = readerDN["ILGSTATUS"] + "";
                    string sILGSENDMBR = readerDN["ILGSENDMBR"] + "";
                    string sILGSENDFUN = readerDN["ILGSENDFUN"] + "";
					
                    string sDENAME = readerDN["DENAME"] + "";
                    string sDEDATE = readerDN["DEDATE"] + "";
                    string sDENTTEXT = readerDN["DENTTEXT"] + "";
					
					string sDISTRICT = readerDN["DEDISTRICT"] + "";

                    TXTDENO.Text = sDENo;
                    DDLDETYPE.SelectedValue = sDETYPE;
                    TXTDENAME.Text = sDENAME;
                    //TXTDEDATE.Text = sDEDATE;
                    TXTDENTTEXT.Text = sDENTTEXT;

					//水保

                    string[] arrayDESTATUS = sDESWCSTATUS.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i=0;i < arrayDESTATUS.Length-1;i++) {
                        string aDESTATUS = arrayDESTATUS[i] + "";

                        if (aDESTATUS != "") {
                            CHKSWCSTATUS.Items.FindByValue(aDESTATUS).Selected = true;
                            //CHKSWCSTATUS.SelectedIndex = CHKSWCSTATUS.Items.IndexOf(CHKSWCSTATUS.Items.FindByValue(aDESTATUS));
                        }
                    }

                    string[] arraySENDMBR = sSENDMBR.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arraySENDMBR.Length - 1; i++)
                    {
                        string aSENDMBR = arraySENDMBR[i] + "";

                        if (aSENDMBR != "")
                        {
                            CHKSENDMBR.Items.FindByValue(aSENDMBR).Selected = true;
                            //CHKSWCSTATUS.SelectedIndex = CHKSWCSTATUS.Items.IndexOf(CHKSWCSTATUS.Items.FindByValue(aDESTATUS));
                        }
                    }

                    string[] arraySENDFUN = sSENDFUN.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arraySENDFUN.Length - 1; i++)
                    {
                        string aSENDFUN = arraySENDFUN[i] + "";

                        if (aSENDFUN != "")
                        {
                            CHKSENDFUN.Items.FindByValue(aSENDFUN).Selected = true;
                            //CHKSWCSTATUS.SelectedIndex = CHKSWCSTATUS.Items.IndexOf(CHKSWCSTATUS.Items.FindByValue(aDESTATUS));
                        }
                    }
					
					//違規
					
					string[] arrayDEILGSTATUS = sDEILGSTATUS.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i=0;i < arrayDEILGSTATUS.Length-1;i++) {
                        string aDEILGSTATUS = arrayDEILGSTATUS[i] + "";

                        if (aDEILGSTATUS != "") {
                            CHKSWCSTATUS_ILG.Items.FindByValue(aDEILGSTATUS).Selected = true;
                        }
                    }

                    string[] arrayILGSENDMBR = sILGSENDMBR.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrayILGSENDMBR.Length - 1; i++)
                    {
                        string aILGSENDMBR = arrayILGSENDMBR[i] + "";

                        if (aILGSENDMBR != "")
                        {
                            CHKSENDMBR_ILG.Items.FindByValue(aILGSENDMBR).Selected = true;
                        }
                    }

                    string[] arrayILGSENDFUN = sILGSENDFUN.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrayILGSENDFUN.Length - 1; i++)
                    {
                        string aILGSENDFUN = arrayILGSENDFUN[i] + "";

                        if (aILGSENDFUN != "")
                        {
                            CHKSENDFUN_ILG.Items.FindByValue(aILGSENDFUN).Selected = true;
                        }
                    }
					
					//行政區
					string[] arrayDistrict = sDISTRICT.Split(new string[] { "," }, StringSplitOptions.None);

                    for (int i = 0; i < arrayDistrict.Length - 1; i++)
                    {
                        string aDistrict = arrayDistrict[i] + "";

                        if (aDistrict != "")
                        {
                            CHKDISTRICT.Items.FindByValue(aDistrict).Selected = true;
                        }
                    }

                }
                readerDN.Close();
                objCmdDN.Dispose();

            }
        }
    }
    protected void SaveCase_Click(object sender, EventArgs e)
    {
        string ssUserID = Session["ID"] + "";

        string gDENO = TXTDENO.Text + "";
        string gDETYPE = DDLDETYPE.SelectedValue + "";
		
		//水保
        string gSWCSTATUS = "";
        string gSENDMBR = "";
        string gSENDFUN = "";
		//違規
		string gILGSTATUS = "";
        string gILGSENDMBR = "";
        string gILGSENDFUN = "";
		//行政區
		string gDISTRICT = "";
		
        string gDENAME = TXTDENAME.Text + "";
        //string gDEDATE = TXTDEDATE.Text + "";
        string gDENTTEXT = TXTDENTTEXT.Text + "";
        string sEXESQLUPD = "";

		//水保
        for (int it = 0; it <= CHKSWCSTATUS.Items.Count-1; it++)
        {
            if (CHKSWCSTATUS.Items[it].Selected) {
                gSWCSTATUS += CHKSWCSTATUS.Items[it].Value+ ",";
            }
        }
        for (int it = 0; it <= CHKSENDMBR.Items.Count - 1; it++)
        {
            if (CHKSENDMBR.Items[it].Selected)
            {
                gSENDMBR += CHKSENDMBR.Items[it].Value + ",";
            }
        }
        for (int it = 0; it <= CHKSENDFUN.Items.Count - 1; it++)
        {
            if (CHKSENDFUN.Items[it].Selected)
            {
                gSENDFUN += CHKSENDFUN.Items[it].Value + ",";
            }
        }
		//違規
		for (int it = 0; it <= CHKSWCSTATUS_ILG.Items.Count-1; it++)
        {
            if (CHKSWCSTATUS_ILG.Items[it].Selected) {
                gILGSTATUS += CHKSWCSTATUS_ILG.Items[it].Value+ ",";
            }
        }
        for (int it = 0; it <= CHKSENDMBR_ILG.Items.Count - 1; it++)
        {
            if (CHKSENDMBR_ILG.Items[it].Selected)
            {
                gILGSENDMBR += CHKSENDMBR_ILG.Items[it].Value + ",";
            }
        }
        for (int it = 0; it <= CHKSENDFUN_ILG.Items.Count - 1; it++)
        {
            if (CHKSENDFUN_ILG.Items[it].Selected)
            {
                gILGSENDFUN += CHKSENDFUN_ILG.Items[it].Value + ",";
            }
        }
		//行政區
        for (int it = 0; it <= CHKDISTRICT.Items.Count - 1; it++)
        {
            if (CHKDISTRICT.Items[it].Selected)
            {
                gDISTRICT += CHKDISTRICT.Items[it].Value + ",";
            }
        }

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string gDNSQLStr = " select DENO from DisasterEvent ";
            gDNSQLStr = gDNSQLStr + " where DENo ='" + gDENO + "' ";

            SqlDataReader readerDN;
            SqlCommand objCmdDN = new SqlCommand(gDNSQLStr, SwcConn);
            readerDN = objCmdDN.ExecuteReader();

            if (!readerDN.HasRows)
            {
                sEXESQLUPD = " INSERT INTO DisasterEvent (DENo) VALUES ('" + gDENO + "');";
            }
            readerDN.Close();
            objCmdDN.Dispose();

            sEXESQLUPD = sEXESQLUPD + " Update DisasterEvent Set ";
            sEXESQLUPD = sEXESQLUPD + " DETYPE = '" + gDETYPE + "', ";
            sEXESQLUPD = sEXESQLUPD + " SWCSTATUS = '" + gSWCSTATUS + "', ";
            sEXESQLUPD = sEXESQLUPD + " SENDMBR = '" + gSENDMBR + "', ";
            sEXESQLUPD = sEXESQLUPD + " SENDFUN = '" + gSENDFUN + "', ";
            sEXESQLUPD = sEXESQLUPD + " ILGSTATUS = '" + gILGSTATUS + "', ";
            sEXESQLUPD = sEXESQLUPD + " ILGSENDMBR = '" + gILGSENDMBR + "', ";
            sEXESQLUPD = sEXESQLUPD + " ILGSENDFUN = '" + gILGSENDFUN + "', ";
            sEXESQLUPD = sEXESQLUPD + " DENAME = '" + gDENAME + "', ";
            sEXESQLUPD = sEXESQLUPD + " DENTTEXT = '" + gDENTTEXT + "', ";
            sEXESQLUPD = sEXESQLUPD + " DEDISTRICT = '" + gDISTRICT + "', ";

            sEXESQLUPD = sEXESQLUPD + " saveuser = '" + ssUserID + "', ";
            sEXESQLUPD = sEXESQLUPD + " savedate = getdate() ";

            sEXESQLUPD = sEXESQLUPD + " where DENo = '" + gDENO + "' ";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "SaveCase":
                    Response.Write("<script>alert('資料已存檔');location.href='SWCGOV001.aspx';</script>");
                    break;
                case "DataLock":
                    SWCNotice(gSWCSTATUS, gDENO, gSENDFUN, gSENDMBR, gDISTRICT);
					ILGNotice(gILGSTATUS, gDENO, gILGSENDFUN, gILGSENDMBR, gDISTRICT);
					Response.Write("<script>alert('資料已送出');location.href='SWCGOV001.aspx';</script>");
                    break;
            }
        }
    }

    private void SWCNotice(string gSWCSTATUS, string gDENO,string gSENDFUN,string gSENDMBR,string gDISTRICT)
    {
        string ssUserID = Session["ID"] + "";

        string addSwcDtl04str = "";
        string sendSMSNo = "";
        string sendEmail = "";
        string sendSWC002 = "";
        string[] arrayStatus = gSWCSTATUS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
		string[] arrayDistrict = gDISTRICT.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];

        for (int i = 0; i < arrayStatus.Length; i++)
        {
            string tmpSWC004 = arrayStatus[i];
			for (int j = 0; j < arrayDistrict.Length; j++)
			{
				string tmpDistrict = arrayDistrict[j];
				string tmpCaseSqlStr = " select SWC.SWC000,SWC.SWC002,SWC.SWC013TEL,SWC.SWC108,u.phone as SWC022TEL,u.email as SWC022MAIL,u2.phone as SWC024TEL,u2.email as SWC024MAIL,e.ETTel as SWC021TEL,e.ETEmail as SWC021MAIL,e2.ETTel as SWC045TEL,e2.ETEmail as SWC045MAIL from SWCCASE SWC ";
				tmpCaseSqlStr += " left join tslm2.dbo.geouser u on swc.swc022 = u.userid ";
				tmpCaseSqlStr += " left join tslm2.dbo.geouser u2 on swc.swc024 = u2.userid ";
				tmpCaseSqlStr += " left join ETUsers e on swc.SWC021ID=e.ETID ";
				tmpCaseSqlStr += " left join ETUsers e2 on swc.SWC045ID=e2.ETID ";
				tmpCaseSqlStr +=  "where swc004 ='" + tmpSWC004 + "' and swc008 like '%" + tmpDistrict + "%' ";
	
				//水保案件
				using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
				{
					SWCConn.Open();
	
					SqlDataReader readerSwc;
					SqlCommand objCmdSwc = new SqlCommand(tmpCaseSqlStr, SWCConn);
					readerSwc = objCmdSwc.ExecuteReader();
	
					while (readerSwc.Read())
					{
						string sSWC000 = readerSwc["SWC000"] + "";
						string sSWC002 = readerSwc["SWC002"] + "";
						string sSWC013TEL = readerSwc["SWC013TEL"] + "";
						string sSWC108 = readerSwc["SWC108"] + "";
						string sSWC022TEL = readerSwc["SWC022TEL"] + "";
						string sSWC022MAIL = readerSwc["SWC022MAIL"] + "";
						string sSWC024TEL = readerSwc["SWC024TEL"] + "";
						string sSWC024MAIL = readerSwc["SWC024MAIL"] + "";
						string sSWC021TEL = readerSwc["SWC021TEL"] + "";
						string sSWC021MAIL = readerSwc["SWC021MAIL"] + "";
						string sSWC045TEL = readerSwc["SWC045TEL"] + "";
						string sSWC045MAIL = readerSwc["SWC045MAIL"] + "";
	
						//string sSwcDtl04NewId = GetDTLAID(sSWC000);
						//addSwcDtl04str += " INSERT INTO SWCDTL04 (SWC000,DTLD000,DTLD001,DTLD085,DENo) VALUES ('" + sSWC000 + "','" + sSwcDtl04NewId + "','" + sSwcDtl04NewId + "','" + gDENO + "','"+ gDENO + "');";
	
						if (gSENDMBR.IndexOf("義務人") > 0) { sendSMSNo += ";;"+ sSWC013TEL; sendEmail += sSWC108; }
						if (gSENDMBR.IndexOf("審查單位") > 0) { sendSMSNo += ";;" + sSWC022TEL; sendEmail += ";;" + sSWC022MAIL; sendSWC002 += ";;" + sSWC002; }
						if (gSENDMBR.IndexOf("檢查單位") > 0) { sendSMSNo += ";;" + sSWC024TEL; sendEmail += ";;" + sSWC024MAIL; sendSWC002 += ";;" + sSWC002; }
						if (gSENDMBR.IndexOf("承辦技師") > 0) { sendSMSNo += ";;" + sSWC021TEL; sendEmail += ";;" + sSWC021MAIL; sendSWC002 += ";;" + sSWC002; }
						if (gSENDMBR.IndexOf("監造技師") > 0) { sendSMSNo += ";;" + sSWC045TEL; sendEmail += ";;" + sSWC045MAIL; sendSWC002 += ";;" + sSWC002; }
						
					}
					objCmdSwc.Dispose();
					readerSwc.Close();
					SWCConn.Close();
				}
			}
        }

        addSwcDtl04str = " Update DisasterEvent Set DESENT='Y',DEDATE=CONVERT(varchar(100), GETDATE(), 23),saveuser = '" + ssUserID + "',savedate = getdate() Where DENo = '" + gDENO + "'; " + addSwcDtl04str;
		
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
		
            SqlCommand objCmdUpd = new SqlCommand(addSwcDtl04str, SWCConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }
        //Response.Write(addSwcDtl04str);

        //e-mail或簡訊通知

        GBClass001 SBApp = new GBClass001();

        string[] arraySWC002 = sendSWC002.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayEmail = sendEmail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arraySMSNo = sendSMSNo.Split(new string[] { ";;" }, StringSplitOptions.None);

        if (gSENDFUN.IndexOf("E-mail") >= 0)
        {
            for (int i = 0; i < arrayEmail.Length; i++)
            {
                string tmpSWC002 = arraySWC002[i] + "";
                string tmpEmail = arrayEmail[i] + "";
                string[] arraySentMail01 = new string[] { tmpEmail };

                bool MailTo01 = SBApp.Mail_Send_dp(arraySentMail01, MailSub(tmpSWC002), MailBody());
            }
        }
        if (gSENDFUN.IndexOf("簡訊") >= 0)
        {
            for (int i = 0; i < arraySMSNo.Length; i++) {
                string SendPhoneNo = arraySMSNo[i];
                string SMSText = TXTDENTTEXT.Text;

                if (SendPhoneNo !="") { SBApp.SendSMS(SendPhoneNo, SMSText); }
            }
        }

    }
	
	private void ILGNotice(string gILGSTATUS, string gDENO,string gILGSENDFUN,string gILGSENDMBR,string gDISTRICT)
    {
        string ssUserID = Session["ID"] + "";

        //string addSwcDtl04str = "";
        string sendSMSNo = "";
        string sendEmail = "";
        string sendSWC002 = "";
        string[] arrayStatus = gILGSTATUS.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
		string[] arrayDistrict = gDISTRICT.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
		
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];

        for (int i = 0; i < arrayStatus.Length; i++)
        {
            string tmpILG005 = arrayStatus[i];
			for (int j = 0; j < arrayDistrict.Length; j++)
			{
				string tmpDistrict = arrayDistrict[j];
				string tmpCaseSqlStr = " select * from tslm2.dbo.ILGILG ";
				tmpCaseSqlStr +=  "where ILG005 ='" + tmpILG005 + "' and ILG011 like '%" + tmpDistrict + "%' ";
	
				//違規案件
				using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
				{
					SWCConn.Open();
	
					SqlDataReader readerSwc;
					SqlCommand objCmdSwc = new SqlCommand(tmpCaseSqlStr, SWCConn);
					readerSwc = objCmdSwc.ExecuteReader();
	
					while (readerSwc.Read())
					{
						string sILG009 = readerSwc["ILG009"] + "";
	
						if (gILGSENDMBR.IndexOf("受處分人") > 0) { sendSMSNo += ";;"+ sILG009; }
					}
					objCmdSwc.Dispose();
					readerSwc.Close();
					SWCConn.Close();
				}
			}
        }

        //簡訊通知

        GBClass001 SBApp = new GBClass001();

        string[] arraySMSNo = sendSMSNo.Split(new string[] { ";;" }, StringSplitOptions.None);

        if (gILGSENDFUN.IndexOf("簡訊") >= 0)
        {
            for (int i = 0; i < arraySMSNo.Length; i++) {
                string SendPhoneNo = arraySMSNo[i];
                string SMSText = TXTDENTTEXT.Text;

                if (SendPhoneNo !="") { SBApp.SendSMS(SendPhoneNo, SMSText); }
            }
        }

    }

    private string GetNewId()
    {
        string SearchValA = "DE" + (Convert.ToInt32(DateTime.Now.ToString("yyyy")) - 1911).ToString() + DateTime.Now.ToString("MM");
        string MaxKeyVal = "0001";

        ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRVa = " select MAX(DENo) as MAXID from DisasterEvent ";
            strSQLRVa = strSQLRVa + " where LEFT(DENo,7) ='" + SearchValA + "' ";

            SqlDataReader readerSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRVa, SwcConn);
            readerSwc = objCmdSwc.ExecuteReader();

            if (readerSwc.HasRows)
            {
                while (readerSwc.Read())
                {
                    string tempMaxKeyVal = readerSwc["MAXID"] + "";

                    if (tempMaxKeyVal != "") {
                        string tempValue = (Convert.ToInt32(tempMaxKeyVal.Substring(tempMaxKeyVal.Length - 4, 4)) + 1).ToString();
                        MaxKeyVal = tempValue.PadLeft(4, '0');
                    }
                }
            }
            objCmdSwc.Dispose();
            readerSwc.Close();
            SwcConn.Close();
        }

        return SearchValA+ MaxKeyVal;
    }
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RD" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RD" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLD000) AS MAXID from SWCDTL04 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLD000,7) = '" + tempVal + "' ";

            SqlDataReader readerSWC;
            SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
            readerSWC = objCmdSWC.ExecuteReader();

            while (readerSWC.Read())
            {
                string GetMaxID = readerSWC["MAXID"] + "";

                if (GetMaxID != "")
                {
                    string tempvalue = (Convert.ToInt32(GetMaxID.Substring(GetMaxID.Length - 3, 3)) + 1).ToString();

                    _ReturnVal = tempVal + tempvalue.PadLeft(3, '0');
                }
            }
        }
        return _ReturnVal;
    }

    private string GetSYSID()
    {
        DateTime oTime = DateTime.Now;
        string strTime = string.Format("{0:yyyyMMddHHmmss}{1:0000}", oTime, oTime.Millisecond);

        return strTime;
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWCGOV001.aspx");
    }

    private string MailSub(string gSWC02)
    {
        string rValue = "臺北市大地處通知【"+ gSWC02 + "】應完成防災整備檢查";
        return rValue;
    }
    private string MailBody()
    {
        string mMailText = TXTDENTTEXT.Text;

        string rValue = "";//"您好，請您至「臺北市水土保持書件管理平台」系統填登「颱風豪雨設施自主檢查表」。<br><br>";

        rValue = rValue + mMailText + "<br><br>";

        rValue = rValue + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        rValue = rValue + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
        
        return rValue;
    }
    private string[] GetMailTo()
    {
        string MailStr = "geocheck@geovector.com.tw;;";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
        using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
        {
            UserConn.Open();

            string strSQLRV = " select * from geouser ";
            strSQLRV = strSQLRV + " where (department = '審查管理科' and mbgroup02 = '系統管理員') ";
            strSQLRV = strSQLRV + "    or (department = '審查管理科' and jobtitle = '股長') ";

            SqlDataReader readeUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
            readeUser = objCmdUser.ExecuteReader();

            string mbMail = "";
            while (readeUser.Read())
            {
                mbMail = readeUser["email"] + "";
                MailStr = MailStr + mbMail + ";;";
            }
        }

        string[] arrayMailMb = MailStr.Split(new string[] { ";;" }, StringSplitOptions.None);
        return arrayMailMb;
    }
	
	protected void DDLDETYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
		string sTomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
		if(DDLDETYPE.SelectedValue == "0104")
		{
			LockSWC(true);
			LockILG(false);
			TXTDENTTEXT.Text = "臺北市政府工務局大地工程處(審查管理科)通報施工中水土保持計畫水保義務人及監造技師注意：為防範【OO颱風】颱風來襲，請儘速完成工地自主檢查及防災整備，並於【"+ sTomorrow + "】下午6時前至「臺北市水土保持申請書件管理平台」填寫「颱風豪雨設施自主檢查表」，如有災情應儘速處理，並回報目的事業主管機關及本處。";
		}
		else if(DDLDETYPE.SelectedValue == "0203")
		{
			LockSWC(false);
			LockILG(true);
			TXTDENTTEXT.Text = "臺北市政府工務局大地工程處通報水土保持義務人注意：臺端尚有水保違規案件改正中，為防範【OO颱風】來襲，請注意做好防災整備及水土保持處理與維護，倘未依規定做好水土保持處理與維護，致生災害或危及公私有設施，應自負相關刑事及民事責任，如有災情請通報本處(1999或02-2720-8889)。";
		}
		
	}
	protected void LockSWC(bool re)
	{
		CHKSWCSTATUS.Enabled = re;
		CHKSENDMBR.Enabled = re;
		CHKSENDFUN.Enabled = re;
		if(!re)
			CHKSWCSTATUS.ClearSelection();
	}
	protected void LockILG(bool re)
	{
		CHKSWCSTATUS_ILG.Enabled = re;
		CHKSENDMBR_ILG.Enabled = re;
		CHKSENDFUN_ILG.Enabled = re;
		if(!re)
			CHKSWCSTATUS_ILG.ClearSelection();
	}

}