using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCDT005 : System.Web.UI.Page
{
    protected void Page_Error(object sender, EventArgs e)
    {
        Exception objErr = Server.GetLastError().GetBaseException(); // 獲取錯誤
        string errUrl = Request.Url.ToString();
        string errMsg = objErr.Message.ToString();
        GBClass001 SBApp = new GBClass001();
        string[] mailTo = new string[] { "tcge7@geovector.com.tw" };
        string ssUserName = Session["NAME"] + "";

        string mailText = "使用者：" + ssUserName + "<br/>";
        mailText += "時間：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "<br/>";
        mailText += "url：" + errUrl + "<br/>";
        mailText += "錯誤訊息：" + errMsg + "<br/>";

        SBApp.Mail_Send(mailTo, "臺北市水土保持書件管理平台-系統錯誤通知", mailText);
        Response.Redirect("~/errPage/500.htm");
        Server.ClearError();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        Class20 C20 = new Class20();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string ssUserType = Session["UserType"] + "";

        if (rCaseId == "" || ssUserID == "")
            Response.Redirect("SWC001.aspx");

        switch (ssUserType)
        {
            case "02":
                TitleLink00.Visible = true;
                break;
            case "03":
                GoTslm.Visible = true;
                break;
        }

        if (!IsPostBack)
        {
			if(rDTLId == "AddNew")
				C20.swcLogRC("SWCDT005", "監造紀錄", "詳情", "新建", rCaseId + "," + rDTLId);
			else
				C20.swcLogRC("SWCDT005", "監造紀錄", "詳情", "瀏覽", rCaseId + "," + rDTLId);
            GenerateDropDownList();
            Data2Page(rCaseId, rDTLId);
			
			if(ssUserType == "08") DataLock.Visible = false;
        }

        #region allArea
        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        #endregion
    }
    protected string Week()
    {
        string[] weekdays = { "日", "一", "二", "三", "四", "五", "六" };
        string week = weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
        return week;
    }
    protected void SDIList_DataBound(object sender, EventArgs e)
    {
        int aaaaaa = 0;

        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            Label LBIT06 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("SDILB006");
            Label LBIT06D = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("SDILB006D");
            TextBox CHK01 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001");
            Label A1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A1");
            TextBox CHK01_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001_1");
            TextBox CHK01D = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK001D");
            TextBox CHK04 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004");
            Label A2 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A2");
            TextBox CHK04_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004_1");
            TextBox CHK04D = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK004D");
            TextBox CHK05 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK005");
            Label A3 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A3");
            TextBox CHK05_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK005_1");
            TextBox CHK06 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK006");
            Label A4 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("A4");
            TextBox CHK06_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("CHK006_1");
            Label LabelX1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LabelX1");
            Label LabelX2 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LabelX2");
            TextBox TXTCHK002 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK002");
            TextBox TXTCHK002_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK002_1");
            TextBox TXTCHK007 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK007");
            TextBox TXTCHK007_1 = (TextBox)SDIList.Rows[aaaaaa].Cells[3].FindControl("TXTCHK007_1");
            Label LBCHK002 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002");
            Label LBCHK002_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002_1");
            Label LBCHK002pers = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002pers");
            Label LBCHK002pers_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK002pers_1");
            Label ITNONE03 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE03");
            Label ITNONE04 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE04");
            Label LB004D = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB004D");
            Label ITNONE05 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("ITNONE05");
            Label LBCHK007 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007");
            Label LBCHK007_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007_1");
            Label LBCHK007pers = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007pers");
            Label LBCHK007pers_1 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LBCHK007pers_1");
            Label LB019 = (Label)SDIList.Rows[aaaaaa].Cells[3].FindControl("LB019");

            if (++aaaaaa % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 2].Cells[2].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");
                
				Label A1_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A1");
                Label A2_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A2");
                Label A3_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A3");
                Label A4_onoff = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("A4");
                TextBox CHK01_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK001_1");
                TextBox CHK04_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK004_1");
                TextBox CHK05_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK005_1");
                TextBox CHK06_1_onoff = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("CHK006_1");

                Label LB001D = (Label)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("LB001D");
                DropDownList DDLDONE = (DropDownList)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("DDLDONE");
                TextBox RCH009 = (TextBox)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("RCH009");
                Button MODIFYDATA = (Button)SDIList.Rows[aaaaaa - 1].Cells[3].FindControl("MODIFYDATA"); 

                LBIT06.Visible = false; LBIT06D.Visible = false;
                ITNONE03.Visible = false;
                ITNONE04.Visible = false;
                ITNONE05.Visible = false;
                DDLDONE.Visible = true;
                LB004D.Visible = false;
				
				//水土保持設施類別非臨時開頭移除"已由永久設施取代"
                if (SDIList.Rows[aaaaaa - 2].Cells[0].Text.Substring(0, 2) != "臨時")
                {
                    DDLDONE.Items.Remove("已由永久設施取代");
                }
				
				
                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = true; CHK05.Enabled = true; CHK05_1_onoff.Enabled = true; CHK06.Enabled = true; CHK06_1_onoff.Enabled = true; break; }
                }
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = false; break; case "2": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; break; case "3": CHK04.Enabled = true; CHK04_1_onoff.Enabled = false; CHK05.Enabled = true; CHK05_1_onoff.Enabled = false; CHK06.Enabled = true; CHK06_1_onoff.Enabled = false; break; }
                }

                
                if (LB019.Text == "是" && tSDIFD004 != "其他") { A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; }
                else { A1_onoff.Visible = false; CHK01_1_onoff.Visible = false; A2_onoff.Visible = false; CHK04_1_onoff.Visible = false; A3_onoff.Visible = false; CHK05_1_onoff.Visible = false; A4_onoff.Visible = false; CHK06_1_onoff.Visible = false; }

                if (RCH009.Text == "完成" || RCH009.Text == "已由永久設施取代") { CHK01D.Enabled = false; CHK04D.Enabled = false; MODIFYDATA.Visible = true; CHK01.Enabled = false; CHK01_1_onoff.Enabled = false; CHK04.Enabled = false; CHK04_1_onoff.Enabled = false; CHK05.Enabled = false; CHK05_1_onoff.Enabled = false; CHK06.Enabled = false; CHK06_1_onoff.Enabled = false; DDLDONE.Enabled = false; TXTCHK002.Enabled = false; TXTCHK002_1.Enabled = false; TXTCHK007.Enabled = false; TXTCHK007_1.Enabled = false; } else { CHK01D.Enabled = true; CHK04D.Enabled = true; }

                if (LB019.Text == "是")
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = true; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = true; CHK05.Visible = true; CHK05_1_onoff.Visible = true; CHK06.Visible = true; CHK06_1_onoff.Visible = true; break; }
                    /*LBCHK002_1.Visible = true; LBCHK002pers_1.Visible = true;*/
					LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false;
					LBCHK007_1.Visible = true; LBCHK007pers_1.Visible = true;
				}
                else
                {
                    switch (HDF011.Value) { case "1": CHK04.Visible = true; CHK04_1.Visible = false; break; case "2": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; break; case "3": CHK04.Visible = true; CHK04_1_onoff.Visible = false; CHK05.Visible = true; CHK05_1_onoff.Visible = false; CHK06.Visible = true; CHK06_1_onoff.Visible = false; break; }
                    LBCHK002_1.Visible = false; LBCHK002pers_1.Visible = false; LBCHK007_1.Visible = false; LBCHK007pers_1.Visible = false;
                }
				
				if (tSDIFD004 == "其他") { CHK01.Visible = false; CHK01D.Visible = true; CHK04D.Visible = true; CHK04.Visible = false; CHK05.Visible = false; CHK06.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; CHK04D.Visible = true; LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = true; TXTCHK007_1.Visible = false; LBCHK002.Visible = false; LBCHK002_1.Visible = false; TXTCHK002.Visible = true; TXTCHK002_1.Visible = false; }
                else { CHK01.Visible = true; CHK01D.Visible = false; CHK04.Visible = true; CHK05.Visible = true; CHK06.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; CHK04D.Visible = false; LBCHK007.Visible = true; LBCHK007_1.Visible = true; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK002.Visible = true; /* LBCHK002_1.Visible = true; */ LBCHK002_1.Visible = false;  TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; }

			}
            else
            {
                string tSDIFD004 = SDIList.Rows[aaaaaa - 1].Cells[2].Text;
                HiddenField HDF011 = (HiddenField)SDIList.Rows[aaaaaa - 1].Cells[0].FindControl("HDSDI011");

                CHK01.Visible = false; A1.Visible = false; CHK01_1.Visible = false; CHK01D.Visible = false;
                CHK04.Visible = false; A2.Visible = false; CHK04_1.Visible = false; CHK04D.Visible = false;
                CHK05.Visible = false; A3.Visible = false; CHK05_1.Visible = false;
                CHK06.Visible = false; A4.Visible = false; CHK06_1.Visible = false;
                switch (HDF011.Value) { case "1": ITNONE04.Text = "-"; ITNONE05.Text = "-"; break; case "2": ITNONE05.Text = "-"; break;}
                LBCHK007.Visible = false; LBCHK007_1.Visible = false; TXTCHK007.Visible = false; TXTCHK007_1.Visible = false; LBCHK007pers.Visible = false; LBCHK007pers_1.Visible = false; LBCHK002.Visible = false; TXTCHK002.Visible = false; TXTCHK002_1.Visible = false; LBCHK002pers.Visible = false; LBCHK002pers_1.Visible = false;
                if (tSDIFD004 == "其他") { LBIT06.Visible = false; LBIT06D.Visible = true;  LB004D.Visible = true; ITNONE03.Visible = false; ITNONE04.Visible = false; ITNONE05.Visible = false; LabelX1.Visible = false; LabelX2.Visible = false; }
                else { LBIT06.Visible = true; LBIT06D.Visible = false; LB004D.Visible = false; ITNONE03.Visible = true; ITNONE04.Visible = true; ITNONE05.Visible = true; LabelX1.Visible = true; LabelX2.Visible = true; }
			}
			ITNONE03.Text = Server.HtmlDecode(ITNONE03.Text);
            ITNONE04.Text = Server.HtmlDecode(ITNONE04.Text);
            ITNONE05.Text = Server.HtmlDecode(ITNONE05.Text);
            LBIT06.Text = Server.HtmlDecode(LBIT06.Text);
        }

    }
    private void Data2Page(string v, string v2)
    {
        GBClass001 SBApp = new GBClass001();

        string sqlStr = " select * from SWCCASE where SWC000=@SWC000; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        #region SWCCASE
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readeSwc = cmd.ExecuteReader())
                {
                    if (readeSwc.HasRows)
                    {
                        while (readeSwc.Read())
                        {
                            string tSWC005 = readeSwc["SWC005"] + "";
                            string tSWC013 = readeSwc["SWC013"] + "";
                            string tSWC043 = readeSwc["SWC043"] + "";
                            string tSWC044 = readeSwc["SWC044"] + "";
                            string tSWC045 = readeSwc["SWC045"] + "";
                            string tSWC048 = readeSwc["SWC048"] + "";
                            string tSWC052 = readeSwc["SWC052"] + "";

                            LBSWC000.Text = v;
                            LBSWC005.Text = tSWC005;
                            LBSWC005_01.Text = tSWC005;
                            LBSWC013.Text = tSWC013;
                            LBSWC043.Text = SBApp.DateView(tSWC043, "00");
                            LBSWC044.Text = tSWC044;
                            LBSWC045.Text = tSWC045;
                            LBSWC048.Text = tSWC048;
                            LBSWC052.Text = SBApp.DateView(tSWC052, "00");
                        }
                    }
                    readeSwc.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion

        string tmpDTL000 = getDTL000(v,v2);
        string sqlStrDtl = " select * from SWCDTL05 where SWC000=@SWC000 and DTLE000=@DTL000; ";
        #region SWCDTL
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStrDtl;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.Parameters.Add(new SqlParameter("@DTL000", tmpDTL000));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readeDTL = cmd.ExecuteReader())
                {
                    if (readeDTL.HasRows)
                    {
                        while (readeDTL.Read())
                        {
                            #region
                            string tDTLE001 = v2== "AddNew" ? GetDTLAID(v) : readeDTL["DTLE001"] + "";
                            string tDTLE002 = readeDTL["DTLE002"] + "";
                            string tDTLE003 = readeDTL["DTLE003"] + "";
                            string tDTLE004 = readeDTL["DTLE004"] + "";
                            string tDTLE005 = readeDTL["DTLE005"] + "";
                            string tDTLE006 = readeDTL["DTLE006"] + "";
                            string tDTLE007 = readeDTL["DTLE007"] + "";

                            string tDTLE016 = readeDTL["DTLE016"] + "";
                            string tDTLE017 = readeDTL["DTLE017"] + "";
                            string tDTLE018 = readeDTL["DTLE018"] + "";
                            string tDTLE019 = readeDTL["DTLE019"] + "";
                            string tDTLE020 = readeDTL["DTLE020"] + "";
                            string tDTLE021 = readeDTL["DTLE021"] + "";
                            string tDTLE022 = readeDTL["DTLE022"] + "";
                            string tDTLE023 = readeDTL["DTLE023"] + "";
                            string tDTLE024 = readeDTL["DTLE024"] + "";
                            string tDTLE025 = readeDTL["DTLE025"] + "";
                            string tDTLE026 = readeDTL["DTLE026"] + "";
                            string tDTLE027 = readeDTL["DTLE027"] + "";
                            string tDTLE028 = readeDTL["DTLE028"] + "";
                            string tDTLE029 = readeDTL["DTLE029"] + "";
                            string tDTLE030 = readeDTL["DTLE030"] + "";
                            string tDTLE031 = readeDTL["DTLE031"] + "";
                            string tDTLE032 = readeDTL["DTLE032"] + "";
                            string tDTLE033 = readeDTL["DTLE033"] + "";
                            string tDTLE034 = readeDTL["DTLE034"] + "";
                            string tDTLE035 = readeDTL["DTLE035"] + "";
                            string tDTLE036 = readeDTL["DTLE036"] + "";
                            string tDTLE037 = readeDTL["DTLE037"] + "";
                            string tDTLE038 = readeDTL["DTLE038"] + "";
                            string tDTLE039 = readeDTL["DTLE039"] + "";
                            string tDTLE040 = readeDTL["DTLE040"] + "";
                            string tDTLE041 = readeDTL["DTLE041"] + "";
                            string tDTLE042 = readeDTL["DTLE042"] + "";
                            string tDTLE043 = readeDTL["DTLE043"] + "";
                            string tDTLE044 = readeDTL["DTLE044"] + "";
                            string tDTLE045 = readeDTL["DTLE045"] + "";
                            string tDTLE046 = readeDTL["DTLE046"] + "";
                            string tDTLE047 = readeDTL["DTLE047"] + "";
                            string tDTLE048 = readeDTL["DTLE048"] + "";
                            string tDTLE049 = readeDTL["DTLE049"] + "";
                            string tDTLE050 = readeDTL["DTLE050"] + "";
                            string tDTLE051 = readeDTL["DTLE051"] + "";
                            string tDTLE052 = readeDTL["DTLE052"] + "";
                            string tDTLE053 = readeDTL["DTLE053"] + "";
                            string tDTLE054 = readeDTL["DTLE054"] + "";
                            string tDTLE055 = readeDTL["DTLE055"] + "";
                            string tDTLE056 = readeDTL["DTLE056"] + "";
                            string tDTLE057 = readeDTL["DTLE057"] + "";
                            string tDTLE058 = readeDTL["DTLE058"] + "";
                            string tDTLE059 = readeDTL["DTLE059"] + "";
                            string tDTLE060 = readeDTL["DTLE060"] + "";
                            string tDTLE061 = readeDTL["DTLE061"] + "";
                            string tDTLE062 = readeDTL["DTLE062"] + "";
                            string tDTLE063 = readeDTL["DTLE063"] + "";
                            string tDTLE064 = readeDTL["DTLE064"] + "";
                            string tDTLE065 = readeDTL["DTLE065"] + "";
                            string tDTLE066 = readeDTL["DTLE066"] + "";
                            string tDTLE067 = readeDTL["DTLE067"] + "";
                            string tDTLE068 = readeDTL["DTLE068"] + "";
                            string tDTLE069 = readeDTL["DTLE069"] + "";
                            string tDTLE070 = readeDTL["DTLE070"] + "";
                            string tDTLE071 = readeDTL["DTLE071"] + "";
                            string tDTLE072 = readeDTL["DTLE072"] + "";
                            string tDTLE073 = readeDTL["DTLE073"] + "";
                            string tDTLE074 = readeDTL["DTLE074"] + "";
                            string tDTLE075 = readeDTL["DTLE075"] + "";
                            string tDTLE076 = readeDTL["DTLE076"] + "";
                            string tDTLE077 = readeDTL["DTLE077"] + "";
                            string tDTLE078 = readeDTL["DTLE078"] + "";
                            string tDTLE079 = readeDTL["DTLE079"] + "";
                            string tDTLE080 = readeDTL["DTLE080"] + "";
                            string tDTLE081 = readeDTL["DTLE081"] + "";
                            string tDTLE082 = readeDTL["DTLE082"] + "";
                            string tDTLE083 = readeDTL["DTLE083"] + "";
                            string tDTLE084 = readeDTL["DTLE084"] + "";
                            string tDTLE085 = readeDTL["DTLE085"] + "";
                            string tDTLE086 = readeDTL["DTLE086"] + "";
                            string tDTLE087 = readeDTL["DTLE087"] + "";
                            string tDTLE088 = readeDTL["DTLE088"] + "";
                            string tDTLE089 = readeDTL["DTLE089"] + "";
                            string tDTLE090 = readeDTL["DTLE090"] + "";
                            string tDTLE091 = readeDTL["DTLE091"] + "";
                            string tDTLE092 = readeDTL["DTLE092"] + "";
                            string tDTLE093 = readeDTL["DTLE093"] + "";

                            string tDATALOCK = readeDTL["DATALOCK"] + "";

                            //string tDTLE006 = readeDTL["DTLE006"] + "";
                            //string tDTLE007 = readeDTL["DTLE007"] + "";
                            //string tDTLE008 = readeDTL["DTLE008"] + "";
                            //string tDTLE009 = readeDTL["DTLE009"] + "";
                            //string tDTLE010 = readeDTL["DTLE010"] + "";
                            //string tDTLE011 = readeDTL["DTLE011"] + "";
                            //string tDTLE012 = readeDTL["DTLE012"] + "";
                            //string tDTLE013 = readeDTL["DTLE013"] + "";
                            //string tDTLE014 = readeDTL["DTLE014"] + "";
                            //string tDTLE015 = readeDTL["DTLE015"] + "";


                            #region DropDownList
                            DropDownList[] aDDL1 = new DropDownList[] { DDLDTL004, DDLDTL088, DDLDTL089, DDLDTL090 };
                            string[] aDDLv1 = new string[] { tDTLE004, tDTLE088, tDTLE089, tDTLE090 };
                            for (int d = 0; d < aDDL1.Length; d++)
                            {
                                aDDL1[d].SelectedValue = aDDLv1[d];
                            }
                            #endregion
                            #region TextBox
                            TextBox[] aTB = new TextBox[] { TXTDTL003, TXTDTL091 };
                            string[] aTBv1 = new string[] { tDTLE003, tDTLE091 };
                            for (int d = 0; d < aTB.Length; d++)
                            {
                                aTB[d].Text = aTBv1[d];
                            }
                            #endregion


                            LBDTL001.Text = tDTLE001;
                            TXTDTL002.Text = SBApp.DateView(tDTLE002, "00");
                            TXTDTL003.Text = tDTLE003;
                            TXTDTL004.Text = tDTLE004;
                            TXTDTL005.Text = SBApp.DateView(tDTLE005, "00");
                            DDLDTL016.SelectedValue = tDTLE016;
                            TXTDTL017.Text = tDTLE017;
                            TXTDTL018.Text = tDTLE018;
                            DDLDTL019.SelectedValue = tDTLE019;
                            TXTDTL020.Text = tDTLE020;
                            TXTDTL021.Text = tDTLE021;
                            DDLDTL022.SelectedValue = tDTLE022;
                            TXTDTL023.Text = tDTLE023;
                            TXTDTL024.Text = tDTLE024;
                            DDLDTL025.SelectedValue = tDTLE025;
                            TXTDTL026.Text = tDTLE026;
                            TXTDTL027.Text = tDTLE027;
                            DDLDTL028.SelectedValue = tDTLE028;
                            TXTDTL029.Text = tDTLE029;
                            TXTDTL030.Text = tDTLE030;
                            DDLDTL031.SelectedValue = tDTLE031;
                            TXTDTL032.Text = tDTLE032;
                            TXTDTL033.Text = tDTLE033;
                            DDLDTL034.SelectedValue = tDTLE034;
                            TXTDTL035.Text = tDTLE035;
                            TXTDTL036.Text = tDTLE036;
                            DDLDTL037.SelectedValue = tDTLE037;
                            TXTDTL038.Text = tDTLE038;
                            TXTDTL039.Text = tDTLE039;
                            DDLDTL040.SelectedValue = tDTLE040;
                            TXTDTL041.Text = tDTLE041;
                            TXTDTL042.Text = tDTLE042;
                            DDLDTL043.SelectedValue = tDTLE043;
                            TXTDTL044.Text = tDTLE044;
                            TXTDTL045.Text = tDTLE045;
                            DDLDTL046.SelectedValue = tDTLE046;
                            TXTDTL047.Text = tDTLE047;
                            TXTDTL048.Text = tDTLE048;
                            DDLDTL049.SelectedValue = tDTLE049;
                            TXTDTL050.Text = tDTLE050;
                            TXTDTL051.Text = tDTLE051;
                            DDLDTL052.SelectedValue = tDTLE052;
                            TXTDTL053.Text = tDTLE053;
                            TXTDTL054.Text = tDTLE054;
                            DDLDTL055.SelectedValue = tDTLE055;
                            TXTDTL056.Text = tDTLE056;
                            TXTDTL057.Text = tDTLE057;
                            DDLDTL058.SelectedValue = tDTLE058;
                            TXTDTL059.Text = tDTLE059;
                            TXTDTL060.Text = tDTLE060;
                            DDLDTL061.SelectedValue = tDTLE061;
                            TXTDTL062.Text = tDTLE062;
                            TXTDTL063.Text = tDTLE063;
                            DDLDTL064.SelectedValue = tDTLE064;
                            TXTDTL065.Text = tDTLE065;
                            TXTDTL066.Text = tDTLE066;
                            DDLDTL067.SelectedValue = tDTLE067;
                            TXTDTL068.Text = tDTLE068;
                            TXTDTL069.Text = tDTLE069;
                            DDLDTL070.SelectedValue = tDTLE070;
                            TXTDTL071.Text = tDTLE071;
                            TXTDTL072.Text = tDTLE072;
                            DDLDTL073.SelectedValue = tDTLE073;
                            TXTDTL074.Text = tDTLE074;
                            TXTDTL075.Text = tDTLE075;
                            DDLDTL076.SelectedValue = tDTLE076;
                            TXTDTL077.Text = tDTLE077;
                            TXTDTL078.Text = tDTLE078;
                            TXTDTL079.Text = tDTLE079;
                            TXTDTL080.Text = tDTLE080;
                            TXTDTL081.Text = SBApp.DateView(tDTLE081, "00");
                            TXTDTL083.Text = tDTLE083;
                            TXTDTL084.Text = tDTLE084;
                            TXTDTL085.Text = tDTLE085;
                            TXTDTL086.Text = tDTLE086;
                            TXTDTL087.Text = tDTLE087;
                            TXTDTL093.Text = SBApp.DateView(tDTLE093, "00");
                            #endregion

                            #region Label
                            string[] tmpText = doDtle092Txt();
                            tDTLE080 = tmpText[1].Replace("】【", "】<br/>【");
                            tDTLE092 = tmpText[0].Replace("】【", "】<br/>【");
                            Label[] aLB = new Label[] { LBDTL004, TXTDTL080, TXTDTL082, LBDTL092 };
                            string[] aLBv1 = new string[] { tDTLE088, tDTLE080, tDTLE003, tDTLE092 };
                            for (int d = 0; d < aLB.Length; d++)
                                aLB[d].Text = aLBv1[d];
                            #endregion

                            //檔案類處理
                            string[] arrayFileNameLink = new string[] { tDTLE083, tDTLE084 };
                            System.Web.UI.WebControls.HyperLink[] arrayLinkAppobj = new System.Web.UI.WebControls.HyperLink[] { Link083, Link084 };

                            for (int i = 0; i < arrayFileNameLink.Length; i++)
                            {
                                string strFileName = arrayFileNameLink[i];
                                System.Web.UI.WebControls.HyperLink FileLinkObj = arrayLinkAppobj[i];

                                FileLinkObj.Visible = false;
                                if (strFileName == "")
                                {
                                }
                                else
                                {
                                    //string tempLinkPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                                    string tempLinkPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName + "?ts=" + System.DateTime.Now.Millisecond;
                                    FileLinkObj.Text = strFileName;
                                    FileLinkObj.NavigateUrl = tempLinkPateh;
                                    FileLinkObj.Visible = true;
                                }

                            }

                            //點擊放大圖片類處理
                            string[] arrayFileName = new string[] { tDTLE085, tDTLE086, tDTLE087 };
                            System.Web.UI.WebControls.HyperLink[] arrayImgAppobj = new System.Web.UI.WebControls.HyperLink[] { HyperLink085, HyperLink086, HyperLink087 };

                            for (int i = 0; i < arrayFileName.Length; i++)
                            {
                                string strFileName = arrayFileName[i];
                                System.Web.UI.WebControls.HyperLink ImgFileObj = arrayImgAppobj[i];

                                if (strFileName == "")
                                {
                                }
                                else
                                {
                                    //string tempImgPateh = SwcUpLoadFilePath + v + "/" + strFileName;
                                    string tempImgPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/" + v + "/" + strFileName;
                                    ImgFileObj.ImageUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                                    ImgFileObj.NavigateUrl = tempImgPateh + "?ts=" + DateTime.Now.Millisecond;
                                }
                            }

                            //按鈕處理
                            if (tDATALOCK == "Y" && v2 != "AddNew")
                            {
                                DataLock.Visible = false;
                                SaveCase.Visible = false;

                                Response.Write("<script>alert('資料已提交大地處，目前僅供瀏覽。'); location.href='SWCDT005v2.aspx?SWCNO=" + v + "&DTLNO=" + v2 + "'; </script>");
                            }
                        }
                    }
                    else
                    {
                        LBDTL001.Text = GetDTLAID(v);
                    }
                    readeDTL.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion

        SetDtlData(v,v2);
    }

    private string[] doDtle092Txt()
    {
        string[] rValue = new string[] {"","" };
        string[] arrayItmeStr = new string[] { "水土保持施工告示牌", "開發範圍界樁", "開挖整地範圍界樁", "臨時防災設施-排水設施", "臨時防災設施-沉砂設施", "臨時防災設施-滯洪設施", "臨時防災設施-土方暫置", "臨時防災設施-邊坡保護措施", "臨時防災設施-施工便道", "臨時防災設施-臨時攔砂設施(如砂包、防溢座等)", "永久性防災措施-排水設施", "永久性防災措施-沉砂設施", "永久性防災措施-滯洪設施", "永久性防災措施-聯外排水", "永久性防災措施-擋土設施", "永久性防災措施-植生工程", "永久性防災措施-邊坡穩定措施"};
		DropDownList[] aDDL = new DropDownList[] { DDLDTL016, DDLDTL019, DDLDTL022, DDLDTL031, DDLDTL034, DDLDTL037, DDLDTL040, DDLDTL043, DDLDTL046, DDLDTL090, DDLDTL055, DDLDTL058, DDLDTL061, DDLDTL064, DDLDTL067, DDLDTL070, DDLDTL073 };
        TextBox[] aTB = new TextBox[] {TXTDTL018,TXTDTL021,TXTDTL024,TXTDTL033,TXTDTL036,TXTDTL039,TXTDTL042,TXTDTL045,TXTDTL048,TXTDTL091,TXTDTL057,TXTDTL060,TXTDTL063,TXTDTL066,TXTDTL069,TXTDTL072,TXTDTL075 };
        for (int i=0;i<aDDL.Length;i++) {
            if (aDDL[i].Text.Trim().IndexOf("通知大地處")>-1) { rValue[0] += "【" + arrayItmeStr[i] + ":" + aTB[i].Text.Trim() + "】"; }
            else if(aDDL[i].Text.Trim().IndexOf("應改善") > -1) { rValue[1] += "【" + arrayItmeStr[i] + ":" + aTB[i].Text.Trim() + "】"; }
        }
        return rValue;
    }

    private string getDTL000(string v, string v2)
    {
        string rValue = v2;
        if (v2 == "AddNew") {
            string sqlStr = " select top 1 * from SWCDTL05 where SWC000=@SWC000 and DATALOCK='Y' order by id desc; ";
            #region
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();
                using (var cmd = SWCConn.CreateCommand())
                {
                    cmd.CommandText = sqlStr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                    #endregion
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerTslm = cmd.ExecuteReader())
                    {
                        while (readerTslm.Read())
                        {
                            rValue = readerTslm["DTLE000"] + "";
                        }
                        readerTslm.Close();
                    }
                    cmd.Cancel();
                }
            }
            #endregion
        }
        return rValue;
    }
    #region pageinputcheck
    private bool chkPageInput()
    {
        if (DDLDTL088.SelectedItem.Text.Trim() == "") { Response.Write("<script>alert('請選擇監造日期。'); </script>"); return false; }
        if (DDLDTL089.SelectedItem.Text == "") { Response.Write("<script>alert('請選擇工程狀態。'); </script>"); return false; }
        if (TXTDTL003.Text.Trim() == "") { Response.Write("<script>alert('請填寫工程進度。'); </script>"); return false; }
        if (DDLDTL004.SelectedItem.Text == "") { Response.Write("<script>alert('請選擇監造結果。'); </script>"); return false; }
		string temp = TXTDTL082.Text;
		if (TXTDTL082.Text == "") temp = "0";
		if(Convert.ToDouble(temp.Replace("%",""))>Convert.ToDouble(TXTDTL003.Text.Replace("%",""))) {Response.Write("<script>alert('您好，工程進度應大於前次累計進度百分比，請再次確認後送出。'); </script>"); return false;}

        Class1 C1 = new Class1();
        string sDTLE002 = TXTDTL002.Text + "";
        string sDTLE003 = C1.maxStr(TXTDTL003.Text.ToString(), 200); TXTDTL003.Text = sDTLE003;
        string sDTLE004 = C1.maxStr(TXTDTL004.Text.ToString(), 200); TXTDTL004.Text = sDTLE004;
        string sDTLE079 = C1.maxStr(TXTDTL079.Text.ToString(), 200); TXTDTL003.Text = sDTLE003;
        string sDTLE081 = TXTDTL081.Text + "";
        string sDTLE093 = TXTDTL093.Text + "";

        if (!C1.chkDateFormat(sDTLE002)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');document.getElementById('TXTDTL002').focus();</script>"); return false; }
        if (!C1.chkDateFormat(sDTLE081)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');document.getElementById('TXTDTL081').focus();</script>"); return false; }
        if (!C1.chkDateFormat(sDTLE093)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');document.getElementById('TXTDTL093').focus();</script>"); return false; }
        return true;
    }
    #endregion

    protected void GenerateDropDownList()
    {
        #region 監造日期
        string todayWeek = Week();
        switch (todayWeek) {
            case "日":
            case "一":
            case "二":
            case "六":
                string[] aDDL088a = new string[] { "", dateRang(DateTime.Now.AddDays(-7)) };
                DDLDTL088.DataSource = aDDL088a;
                DDLDTL088.DataBind();
                break;
            case "三":
                string[] aDDL088b = new string[] { "", dateRang(DateTime.Now.AddDays(-7)), dateRang(DateTime.Now) };
                DDLDTL088.DataSource = aDDL088b;
                DDLDTL088.DataBind();
                break;
            case "四":
            case "五":
                string[] aDDL088c = new string[] { "", dateRang(DateTime.Now.AddDays(-14)), dateRang(DateTime.Now.AddDays(-7)) };
                DDLDTL088.DataSource = aDDL088c;
                DDLDTL088.DataBind();
                break;
        }
        #endregion
        #region 2018-05-07
        //2018-05-07，第五次工作會議加預設值...
        /*
        string[] array_DTL016 = new string[] { "", "是", "否" };
        DDLDTL016.DataSource = array_DTL016;
        DDLDTL016.DataBind();
        DDLDTL016.SelectedValue = "是";
        #region.
        string[] array_DTL019 = new string[] { "", "是", "否" };
        DDLDTL019.DataSource = array_DTL019;
        DDLDTL019.DataBind();
        DDLDTL019.SelectedValue = "是";

        string[] array_DTL022 = new string[] { "", "是", "否" };
        DDLDTL022.DataSource = array_DTL022;
        DDLDTL022.DataBind();
        DDLDTL022.SelectedValue = "是";

        string[] array_DTL025 = new string[] { "", "是", "否" };
        DDLDTL025.DataSource = array_DTL025;
        DDLDTL025.DataBind();
        DDLDTL025.SelectedValue = "是";

        string[] array_DTL028 = new string[] { "", "是", "否" };
        DDLDTL028.DataSource = array_DTL028;
        DDLDTL028.DataBind();
        DDLDTL028.SelectedValue = "是";

        string[] array_DTL031 = new string[] { "", "是", "否" };
        DDLDTL031.DataSource = array_DTL031;
        DDLDTL031.DataBind();
        DDLDTL031.SelectedValue = "是";

        string[] array_DTL034 = new string[] { "", "是", "否" };
        DDLDTL034.DataSource = array_DTL034;
        DDLDTL034.DataBind();
        DDLDTL034.SelectedValue = "是";

        string[] array_DTL037 = new string[] { "", "是", "否" };
        DDLDTL037.DataSource = array_DTL037;
        DDLDTL037.DataBind();
        DDLDTL037.SelectedValue = "是";

        string[] array_DTL040 = new string[] { "", "是", "否" };
        DDLDTL040.DataSource = array_DTL040;
        DDLDTL040.DataBind();
        DDLDTL040.SelectedValue = "是";

        string[] array_DTL043 = new string[] { "", "是", "否" };
        DDLDTL043.DataSource = array_DTL043;
        DDLDTL043.DataBind();
        DDLDTL043.SelectedValue = "是";

        string[] array_DTL046 = new string[] { "", "是", "否" };
        DDLDTL046.DataSource = array_DTL046;
        DDLDTL046.DataBind();
        DDLDTL046.SelectedValue = "是";

        string[] array_DTL049 = new string[] { "", "是", "否" };
        DDLDTL049.DataSource = array_DTL049;
        DDLDTL049.DataBind();
        DDLDTL049.SelectedValue = "是";

        string[] array_DTL052 = new string[] { "", "是", "否" };
        DDLDTL052.DataSource = array_DTL052;
        DDLDTL052.DataBind();
        DDLDTL052.SelectedValue = "是";

        string[] array_DTL055 = new string[] { "", "是", "否" };
        DDLDTL055.DataSource = array_DTL055;
        DDLDTL055.DataBind();
        DDLDTL055.SelectedValue = "是";

        string[] array_DTL058 = new string[] { "", "是", "否" };
        DDLDTL058.DataSource = array_DTL058;
        DDLDTL058.DataBind();
        DDLDTL058.SelectedValue = "是";

        string[] array_DTL061 = new string[] { "", "是", "否" };
        DDLDTL061.DataSource = array_DTL061;
        DDLDTL061.DataBind();
        DDLDTL061.SelectedValue = "是";

        string[] array_DTL064 = new string[] { "", "是", "否" };
        DDLDTL064.DataSource = array_DTL064;
        DDLDTL064.DataBind();
        DDLDTL064.SelectedValue = "是";

        string[] array_DTL067 = new string[] { "", "是", "否" };
        DDLDTL067.DataSource = array_DTL067;
        DDLDTL067.DataBind();
        DDLDTL067.SelectedValue = "是";

        string[] array_DTL070 = new string[] { "", "是", "否" };
        DDLDTL070.DataSource = array_DTL070;
        DDLDTL070.DataBind();
        DDLDTL070.SelectedValue = "是";

        string[] array_DTL073 = new string[] { "", "是", "否" };
        DDLDTL073.DataSource = array_DTL073;
        DDLDTL073.DataBind();
        DDLDTL073.SelectedValue = "是";

        string[] array_DTL076 = new string[] { "", "是", "否" };
        DDLDTL076.DataSource = array_DTL076;
        DDLDTL076.DataBind();
        DDLDTL076.SelectedValue = "是";
        

        TXTDTL002.Attributes.Add("readonly", "readonly");
        TXTDTL006.Attributes.Add("readonly", "readonly");
        TXTDTL007.Attributes.Add("readonly", "readonly");
        TXTDTL081.Attributes.Add("readonly", "readonly");
        #endregion
        */
        //TXTDTL021.Text = "(一)檢查單位及人員：" + System.Environment.NewLine + "(二)承辦監造技師：" + System.Environment.NewLine + "(三)水土保持義務人：";

        #endregion
    }

    private string dateRang(DateTime dateTime)
    {
        string rValue = "";
        string[] tmpSDED = getWeekStarEnd(dateTime.ToString("yyyy-MM-dd"));
        rValue = tmpSDED[0] + "~" + tmpSDED[1];
        return rValue;
    }
    #region 取得周起始終止日
    private string[] getWeekStarEnd(string thisDay)
    {
        string[] rValue = new string[] { "", "" };
        #region 本週定義指 週三(第一天)到週二(最後一天)的日期區間
        int Less = Convert.ToInt32(Convert.ToDateTime(thisDay).DayOfWeek.ToString("d"));
        int add = 3 - Convert.ToInt32(Convert.ToDateTime(thisDay).DayOfWeek.ToString("d"));
        add = add < 0 ? add+7 : add;
        string starDay = Convert.ToDateTime(thisDay).AddDays(add).ToString("yyyy-MM-dd");
        string endDay = Convert.ToDateTime(thisDay).AddDays(add+6).ToString("yyyy-MM-dd");
        rValue[0] = starDay;
        rValue[1] = endDay;
        #endregion
        return rValue;
    }
    #endregion
    private string GetDTLAID(string v)
    {
        int Year = System.DateTime.Now.Year - 1911;
        string Month = System.DateTime.Now.Month.ToString();
        string tempVal = "RE" + Year.ToString() + Month.PadLeft(2, '0');
        string _ReturnVal = "RE" + Year.ToString() + Month.PadLeft(2, '0') + "001";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select MAX(DTLE000) AS MAXID from SWCDTL05 ";
            strSQLRV = strSQLRV + " where SWC000 = '" + v + "' ";
            strSQLRV = strSQLRV + "   and LEFT(DTLE000,7) = '" + tempVal + "' ";

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

    protected void SaveCase_Click(object sender, EventArgs e)
    {
        Class1 C1 = new Class1();
        Class20 C20 = new Class20();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string sDTLE000 = LBDTL001.Text + "";
        string updSqlStr = "",Q="";

        #region pageValue
        if (!chkPageInput()) { return; }
        string pgDTL004 = DDLDTL004.SelectedItem.Text;
        string pgDTL088 = DDLDTL088.SelectedItem.Text;
        string pgDTL088a = pgDTL088.Substring(0, pgDTL088.IndexOf("~"));
        string pgDTL088b = pgDTL088.Substring(pgDTL088.IndexOf("~"),pgDTL088.Length- pgDTL088.IndexOf("~")).Replace("~","");
        string pgDTL089 = DDLDTL089.SelectedItem.Text;
        string pgDTL090 = DDLDTL090.SelectedItem.Text;
        string pgDTL091 = TXTDTL091.Text.Length > 50 ? TXTDTL091.Text.Substring(0,50): TXTDTL091.Text;
        string[] tmpText = doDtle092Txt();
        string pgDTL080 = C1.maxStr(tmpText[1], 800); TXTDTL080.Text = pgDTL080;
        string pgDTL092 = tmpText[0]; LBDTL092.Text = pgDTL092;
        string pgDTL093 = TXTDTL093.Text + "";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        #endregion
        if (chkPageInput()) {
            if (newCase(rCaseId, sDTLE000)) { updSqlStr += " INSERT INTO SWCDTL05 (SWC000,DTLE000) VALUES (@SWC000,@DTL000);"; Q = "A"; }
            updSqlStr += " Update SWCDTL05 Set DTLE004=@DTLE004,DTLE080=@DTLE080,DTLE088=@DTLE088,DTLE088a=@DTLE088a,DTLE088b=@DTLE088b,DTLE089=@DTLE089,DTLE090=@DTLE090,DTLE091=@DTLE091,DTLE092=@DTLE092,DTLE093=@DTLE093 Where SWC000=@SWC000 and DTLE000=@DTL000;";
			
			if(((Button)sender).ID + "" == "DataLock")
				C20.swcLogRC("SWCDT005", "監造紀錄", "詳情", "送出", rCaseId + "," + sDTLE000);
			else
				C20.swcLogRC("SWCDT005", "監造紀錄", "詳情", "修改", rCaseId + "," + sDTLE000);
			
			using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
            {
                dbConn.Open();
                using (var cmd = dbConn.CreateCommand())
                {
                    cmd.CommandText = updSqlStr;
                    #region.設定值
                    cmd.Parameters.Add(new SqlParameter("@SWC000", rCaseId));
                    cmd.Parameters.Add(new SqlParameter("@DTL000", sDTLE000));
                    cmd.Parameters.Add(new SqlParameter("@DTLE004", pgDTL004));
                    cmd.Parameters.Add(new SqlParameter("@DTLE080", pgDTL080));
                    cmd.Parameters.Add(new SqlParameter("@DTLE088", pgDTL088));
                    cmd.Parameters.Add(new SqlParameter("@DTLE088a", pgDTL088a));
                    cmd.Parameters.Add(new SqlParameter("@DTLE088b", pgDTL088b));
                    cmd.Parameters.Add(new SqlParameter("@DTLE089", pgDTL089));
                    cmd.Parameters.Add(new SqlParameter("@DTLE090", pgDTL090));
                    cmd.Parameters.Add(new SqlParameter("@DTLE091", pgDTL091));
                    cmd.Parameters.Add(new SqlParameter("@DTLE092", pgDTL092));
                    cmd.Parameters.Add(new SqlParameter("@DTLE093", pgDTL093));
                    #endregion
                    cmd.ExecuteNonQuery();
                    cmd.Cancel();
                }
            }
        } else { return; }






        string ssUserID = Session["ID"] + "";
        #region pagedata
        string sDTLE002 = TXTDTL002.Text + "";
        string sDTLE003 = C1.maxStr(TXTDTL003.Text.ToString(), 200); TXTDTL003.Text = sDTLE003;
        string sDTLE079 = C1.maxStr(TXTDTL079.Text.ToString(), 200); TXTDTL003.Text = sDTLE003;
        string sDTLE081 = TXTDTL081.Text + "";

        if (!C1.chkDateFormat(sDTLE002)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');document.getElementById('TXTDTL002').focus();</script>"); return; }
        if (!C1.chkDateFormat(sDTLE081)) { Response.Write("<script>alert('提醒您，您輸入的日期格式不正確，請重新輸入。');document.getElementById('TXTDTL081').focus();</script>"); return; }

        #region savDB
        #endregion
        #endregion





        #region.
        Class01 BASEAPP = new Class01();
        string sSWC000 = rCaseId;
        string sDTLE005 = BASEAPP.SQLstrValue(TXTDTL005.Text + "");
        string sDTLE016 = DDLDTL016.SelectedValue + "";
        string sDTLE017 = BASEAPP.SQLstrValue(TXTDTL017.Text + "");
        string sDTLE018 = BASEAPP.SQLstrValue(TXTDTL018.Text + "");
        string sDTLE019 = DDLDTL019.SelectedValue + "";
        string sDTLE020 = BASEAPP.SQLstrValue(TXTDTL020.Text + "");
        string sDTLE021 = BASEAPP.SQLstrValue(TXTDTL021.Text + "");
        string sDTLE022 = DDLDTL022.SelectedValue + "";
        string sDTLE023 = BASEAPP.SQLstrValue(TXTDTL023.Text + "");
        string sDTLE024 = BASEAPP.SQLstrValue(TXTDTL024.Text + "");
        string sDTLE025 = DDLDTL025.SelectedValue + "";
        string sDTLE026 = BASEAPP.SQLstrValue(TXTDTL026.Text + "");
        string sDTLE027 = BASEAPP.SQLstrValue(TXTDTL027.Text + "");
        string sDTLE028 = DDLDTL028.SelectedValue + "";
        string sDTLE029 = BASEAPP.SQLstrValue(TXTDTL029.Text + "");
        string sDTLE030 = BASEAPP.SQLstrValue(TXTDTL030.Text + "");
        string sDTLE031 = DDLDTL031.SelectedValue + "";
        string sDTLE032 = BASEAPP.SQLstrValue(TXTDTL032.Text + "");
        string sDTLE033 = BASEAPP.SQLstrValue(TXTDTL033.Text + "");
        string sDTLE034 = DDLDTL034.SelectedValue + "";
        string sDTLE035 = BASEAPP.SQLstrValue(TXTDTL035.Text + "");
        string sDTLE036 = BASEAPP.SQLstrValue(TXTDTL036.Text + "");
        string sDTLE037 = DDLDTL037.SelectedValue + "";
        string sDTLE038 = BASEAPP.SQLstrValue(TXTDTL038.Text + "");
        string sDTLE039 = BASEAPP.SQLstrValue(TXTDTL039.Text + "");
        string sDTLE040 = DDLDTL040.SelectedValue + "";
        string sDTLE041 = BASEAPP.SQLstrValue(TXTDTL041.Text + "");
        string sDTLE042 = BASEAPP.SQLstrValue(TXTDTL042.Text + "");
        string sDTLE043 = DDLDTL043.SelectedValue + "";
        string sDTLE044 = BASEAPP.SQLstrValue(TXTDTL044.Text + "");
        string sDTLE045 = BASEAPP.SQLstrValue(TXTDTL045.Text + "");
        string sDTLE046 = DDLDTL046.SelectedValue + "";
        string sDTLE047 = BASEAPP.SQLstrValue(TXTDTL047.Text + "");
        string sDTLE048 = BASEAPP.SQLstrValue(TXTDTL048.Text + "");
        string sDTLE049 = DDLDTL049.SelectedValue + "";
        string sDTLE050 = BASEAPP.SQLstrValue(TXTDTL050.Text + "");
        string sDTLE051 = BASEAPP.SQLstrValue(TXTDTL051.Text + "");
        string sDTLE052 = DDLDTL052.SelectedValue + "";
        string sDTLE053 = BASEAPP.SQLstrValue(TXTDTL053.Text + "");
        string sDTLE054 = BASEAPP.SQLstrValue(TXTDTL054.Text + "");
        string sDTLE055 = DDLDTL055.SelectedValue + "";
        string sDTLE056 = BASEAPP.SQLstrValue(TXTDTL056.Text + "");
        string sDTLE057 = BASEAPP.SQLstrValue(TXTDTL057.Text + "");
        string sDTLE058 = DDLDTL058.SelectedValue + "";
        string sDTLE059 = BASEAPP.SQLstrValue(TXTDTL059.Text + "");
        string sDTLE060 = BASEAPP.SQLstrValue(TXTDTL060.Text + "");
        string sDTLE061 = DDLDTL061.SelectedValue + "";
        string sDTLE062 = BASEAPP.SQLstrValue(TXTDTL062.Text + "");
        string sDTLE063 = BASEAPP.SQLstrValue(TXTDTL063.Text + "");
        string sDTLE064 = DDLDTL064.SelectedValue + "";
        string sDTLE065 = BASEAPP.SQLstrValue(TXTDTL065.Text + "");
        string sDTLE066 = BASEAPP.SQLstrValue(TXTDTL066.Text + "");
        string sDTLE067 = DDLDTL067.SelectedValue + "";
        string sDTLE068 = BASEAPP.SQLstrValue(TXTDTL068.Text + "");
        string sDTLE069 = BASEAPP.SQLstrValue(TXTDTL069.Text + "");
        string sDTLE070 = DDLDTL070.SelectedValue + "";
        string sDTLE071 = BASEAPP.SQLstrValue(TXTDTL071.Text + "");
        string sDTLE072 = BASEAPP.SQLstrValue(TXTDTL072.Text + "");
        string sDTLE073 = DDLDTL073.SelectedValue + "";
        string sDTLE074 = BASEAPP.SQLstrValue(TXTDTL074.Text + "");
        string sDTLE075 = BASEAPP.SQLstrValue(TXTDTL075.Text + "");
        string sDTLE076 = DDLDTL076.SelectedValue + "";
        string sDTLE077 = BASEAPP.SQLstrValue(TXTDTL077.Text + "");
        string sDTLE078 = BASEAPP.SQLstrValue(TXTDTL078.Text + "");
        string sDTLE082 = BASEAPP.SQLstrValue(TXTDTL082.Text + "");
        string sDTLE083 = BASEAPP.SQLstrValue(TXTDTL083.Text + "");
        string sDTLE084 = BASEAPP.SQLstrValue(TXTDTL084.Text + "");
        string sDTLE085 = BASEAPP.SQLstrValue(TXTDTL085.Text + "");
        string sDTLE086 = BASEAPP.SQLstrValue(TXTDTL086.Text + "");
        string sDTLE087 = BASEAPP.SQLstrValue(TXTDTL087.Text + "");
        int tmpDTL082 = 0;
        try { tmpDTL082 = Convert.ToInt32(sDTLE082); } catch { }
        #endregion

        GBClass001 SBApp = new GBClass001();

        string sEXESQLSTR = "";
        string sEXESQLUPD = "";

        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();


            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL05 Set ";
            #region.
            sEXESQLSTR = sEXESQLSTR + " DTLE001 = DTLE000, ";
            sEXESQLSTR = sEXESQLSTR + " DTLE002 ='" + sDTLE002 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE003 ='" + sDTLE003 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE005 ='" + sDTLE005 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE016 ='" + sDTLE016 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE017 ='" + sDTLE017 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE018 ='" + sDTLE018 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE019 ='" + sDTLE019 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE020 ='" + sDTLE020 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE021 ='" + sDTLE021 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE022 ='" + sDTLE022 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE023 ='" + sDTLE023 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE024 ='" + sDTLE024 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE025 ='" + sDTLE025 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE026 ='" + sDTLE026 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE027 ='" + sDTLE027 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE028 ='" + sDTLE028 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE029 ='" + sDTLE029 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE030 ='" + sDTLE030 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE031 ='" + sDTLE031 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE032 ='" + sDTLE032 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE033 ='" + sDTLE033 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE034 ='" + sDTLE034 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE035 ='" + sDTLE035 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE036 ='" + sDTLE036 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE037 ='" + sDTLE037 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE038 ='" + sDTLE038 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE039 ='" + sDTLE039 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE040 ='" + sDTLE040 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE041 ='" + sDTLE041 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE042 ='" + sDTLE042 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE043 ='" + sDTLE043 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE044 ='" + sDTLE044 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE045 ='" + sDTLE045 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE046 ='" + sDTLE046 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE047 ='" + sDTLE047 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE048 ='" + sDTLE048 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE049 ='" + sDTLE049 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE050 ='" + sDTLE050 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE051 ='" + sDTLE051 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE052 ='" + sDTLE052 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE053 ='" + sDTLE053 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE054 ='" + sDTLE054 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE055 ='" + sDTLE055 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE056 ='" + sDTLE056 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE057 ='" + sDTLE057 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE058 ='" + sDTLE058 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE059 ='" + sDTLE059 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE060 ='" + sDTLE060 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE061 ='" + sDTLE061 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE062 ='" + sDTLE062 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE063 ='" + sDTLE063 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE064 ='" + sDTLE064 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE065 ='" + sDTLE065 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE066 ='" + sDTLE066 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE067 ='" + sDTLE067 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE068 ='" + sDTLE068 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE069 ='" + sDTLE069 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE070 ='" + sDTLE070 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE071 ='" + sDTLE071 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE072 ='" + sDTLE072 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE073 ='" + sDTLE073 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE074 ='" + sDTLE074 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE075 ='" + sDTLE075 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE076 ='" + sDTLE076 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE077 ='" + sDTLE077 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE078 ='" + sDTLE078 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE079 ='" + sDTLE079 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE081 ='" + sDTLE081 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE082 ='" + tmpDTL082.ToString() + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE083 ='" + sDTLE083 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE084 ='" + sDTLE084 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE085 ='" + sDTLE085 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE086 ='" + sDTLE086 + "', ";
            sEXESQLSTR = sEXESQLSTR + " DTLE087 ='" + sDTLE087 + "', ";
            
            //sEXESQLSTR = sEXESQLSTR + " DTLE005 ='" + sDTLE005 + "', ";

            //sEXESQLSTR = sEXESQLSTR + " DTLE008 ='" + sDTLE008 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE009 ='" + sDTLE009 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE010 ='" + sDTLE010 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE011 ='" + sDTLE011 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE012 ='" + sDTLE012 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE013 ='" + sDTLE013 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE014 ='" + sDTLE014 + "', ";
            //sEXESQLSTR = sEXESQLSTR + " DTLE015 ='" + sDTLE015 + "', ";

            sEXESQLSTR = sEXESQLSTR + " saveuser = '" + ssUserID + "', ";
            sEXESQLSTR = sEXESQLSTR + " savedate = getdate() ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLE000 = '" + sDTLE000 + "'";
#endregion
            sEXESQLUPD = sEXESQLUPD + " Update RelationSwc set  ";
            sEXESQLUPD = sEXESQLUPD + " Upd02 = 'Y', ";
            sEXESQLUPD = sEXESQLUPD + " Savdate02 = getdate() ";
            sEXESQLUPD = sEXESQLUPD + " Where Key01 = '" + sSWC000 + "'";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR + sEXESQLUPD, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();

            //上傳檔案…
            UpLoadTempFileMoveChk(sSWC000);
            #region.
            if (Q == "")
            {
                string strSQLText = " select * from SwcItemChk where SWC000 = '" + sSWC000 + "' and DTLRPNO = '" + sDTLE000 + "' ";

                SqlDataReader readerTest;
                SqlCommand objCmdText = new SqlCommand(strSQLText, SwcConn);
                readerTest = objCmdText.ExecuteReader();

                if (!readerTest.HasRows)Q = "A";
                readerTest.Close();
                objCmdText.Dispose();
            }
            #endregion
            SavChkSwcItem(Q);

            string thisPageAct = ((Button)sender).ID + "";

            switch (thisPageAct)
            {
                case "DataLock":
                    string exeSQLStr = " update SwcDocItem set SDI017=null where SWC000=@SWC000; update SwcDocItem set SDI017=@SDI017 where SWC000=@SWC000 and sdi001 in (select sdi001 from SwcItemChk where SWC000=@SWC000 and sic09 = '完成' ) and isnull(sdi017,'')= '' ";

                    using (SqlConnection SWCConnCI = new SqlConnection(connectionString.ConnectionString))
                    {
                        SWCConnCI.Open();

                        using (var cmd = SWCConnCI.CreateCommand())
                        {
                            cmd.CommandText = exeSQLStr;
                            //設定值
                            #region.
                            cmd.Parameters.Add(new SqlParameter("@SWC000", sSWC000));
                            cmd.Parameters.Add(new SqlParameter("@SDI017", "完成"));
                            #endregion
                            cmd.ExecuteNonQuery();
                            cmd.Cancel();
                        }
                    }


                    Response.Write("<script>alert('資料已存檔');location.href='SWC003.aspx?SWCNO=" + sSWC000 + "';</script>");
                    break;
            }
            string vCaseID = Request.QueryString["SWCNO"] + "";
            Response.Write("<script>location.href='SWC003.aspx?SWCNO=" + vCaseID + "';</script>");

        }
    }
    #region havecase
    private bool newCase(string v,string v2)
    {
        bool rValue = false;
        string sqlStr = " select * from SWCDTL05 where SWC000=@SWC000 and DTLE000=@DTL000;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        #region swc
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                cmd.Parameters.Add(new SqlParameter("@DTL000", v2));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readeSwc = cmd.ExecuteReader())
                {
                    if (!readeSwc.HasRows)
                        rValue = true;
                    readeSwc.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
        return rValue;
    }
    #endregion

    private void SavChkSwcItem(string v)
    {
        int gLine = 0;
        string exeSqlStr = "";
        string nMSG01 = "";
        string ssUserID = Session["ID"] + "";

        DataTable dtSDI = new DataTable();
        dtSDI = (DataTable)ViewState["SwcDocItem"];

        foreach (GridViewRow GV_Row in SDIList.Rows)
        {
            if (++gLine % 2 == 0)
            {
                string tSDIFD004 = SDIList.Rows[gLine - 2].Cells[2].Text;

                HiddenField HDF001 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI001");
                HiddenField HDF008 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI008");
                HiddenField HDF011 = (HiddenField)SDIList.Rows[gLine - 1].Cells[0].FindControl("HDSDI011");
                Label LBSDI006 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("SDILB006");
                Label LBSDI003 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE03");
                Label LBSDI004 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE04");
                Label LBSDI005 = (Label)SDIList.Rows[gLine - 1].Cells[0].FindControl("ITNONE05");
                Label LBSDI019 = (Label)SDIList.Rows[gLine - 1].Cells[3].FindControl("LB019");
                TextBox CHK01 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001");
                TextBox CHK01_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001_1");
                TextBox CHK01D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK001D");
                TextBox CHK04 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("CHK006_1");
                TextBox RCH01 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001");
                TextBox RCH01_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001_1");
                TextBox RCH01D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH001D");
                TextBox RCH04 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004");
                TextBox RCH04_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004_1");
                TextBox RCH04D = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH004D");
                TextBox RCH05 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH005");
                TextBox RCH05_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH005_1");
                TextBox RCH06 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH006");
                TextBox RCH06_1 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH006_1");
                TextBox RCH09 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("RCH009");
                TextBox TSIC2 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK002");
                TextBox TSIC7 = (TextBox)SDIList.Rows[gLine - 1].Cells[3].FindControl("TXTCHK007");
                DropDownList DDLDONE = (DropDownList)SDIList.Rows[gLine - 1].Cells[3].FindControl("DDLDONE");

                string mSWC000 = LBSWC000.Text;
                string mDTLE001 = LBDTL001.Text;

                string mSDI001 = HDF001.Value;
                string mSIC01a = LBSDI006.Text; if (mSIC01a.Trim() == "") mSIC01a = "0";
                //string CHK01temp = CHK01.Text == "" ? "0" : CHK01.Text;
                //string CHK01_1temp = CHK01_1.Text == "" ? "0" : CHK01_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK01temp) > Convert.ToDouble(CHK01_1temp)) { CHK01.Text= CHK01_1temp; CHK01_1.Text = CHK01temp; }
                string mSIC01b = CHK01.Text; if (mSIC01b.Trim() == "") mSIC01b = "0";
                string mSIC01c = CHK01_1.Text; if (mSIC01c.Trim() == "") mSIC01c = "0";
                string mSIC01r = RCH01.Text; if (mSIC01r.Trim() == "") mSIC01r = "0";
                //string mSIC01r2 = RCH01_1.Text; if (mSIC01r2.Trim() == "") mSIC01r2 = "0";
                string mSIC01Da = CHK01D.Text; 
                string mSIC01Dr = RCH01D.Text;
                string mSIC04a = LBSDI003.Text; if (mSIC04a.Trim() == "") mSIC04a = "0";
                string CHK04temp = CHK04.Text == "" ? "0" : CHK04.Text;
                string CHK04_1temp = CHK04.Text == "" ? "0" : CHK04_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK04temp) > Convert.ToDouble(CHK04_1temp)) { CHK04.Text = CHK04_1temp; CHK04_1.Text = CHK04temp; }
                string mSIC04b = CHK04.Text; if (mSIC04b.Trim() == "") mSIC04b = "0";
                string mSIC04c = CHK04_1.Text; if (mSIC04c.Trim() == "") mSIC04c = "0";
                string mSIC04r = RCH04.Text; if (mSIC04r.Trim() == "") mSIC04r = "0";
                string mSIC04r2 = RCH04_1.Text; if (mSIC04r2.Trim() == "") mSIC04r2 = "0";
                string mSIC04Da = CHK04D.Text;
                string mSIC04Dr = RCH04D.Text;
                string mSIC05a = LBSDI004.Text; if (mSIC05a.Trim() == "") mSIC05a = "0";
                string CHK05temp = CHK05.Text == "" ? "0" : CHK05.Text;
                string CHK05_1temp = CHK05.Text == "" ? "0" : CHK05_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK05temp) > Convert.ToDouble(CHK05_1temp)) { CHK05.Text = CHK05_1temp; CHK05_1.Text = CHK05temp; }
                string mSIC05b = CHK05.Text; if (mSIC05b.Trim() == "") mSIC05b = "0";
                string mSIC05c = CHK05_1.Text; if (mSIC05c.Trim() == "") mSIC05c = "0";
                string mSIC05r = RCH05.Text; if (mSIC05r.Trim() == "") mSIC05r = "0";
                string mSIC05r2 = RCH05_1.Text; if (mSIC05r2.Trim() == "") mSIC05r2 = "0";
                string mSIC06a = LBSDI005.Text; if (mSIC06a.Trim() == "") mSIC06a = "0";
                string CHK06temp = CHK06.Text == "" ? "0" : CHK06.Text;
                string CHK06_1temp = CHK06.Text == "" ? "0" : CHK06_1.Text;
                //if (LBSDI019.Text == "是" && Convert.ToDouble(CHK06temp) > Convert.ToDouble(CHK06_1temp)) { CHK06.Text = CHK06_1temp; CHK06_1.Text = CHK06temp; }
                string mSIC06b = CHK06.Text; if (mSIC06b.Trim() == "") mSIC06b = "0";
                string mSIC06c = CHK06_1.Text; if (mSIC06c.Trim() == "") mSIC06c = "0";
                string mSIC06r = RCH06.Text; if (mSIC06r.Trim() == "") mSIC06r = "0";
                string mSIC06r2 = RCH06_1.Text; if (mSIC06r2.Trim() == "") mSIC06r2 = "0";
                string mSIC08 = "";
                string mSIC08b = HDF008.Value;
                string mSIC09a = RCH09.Text;
                string mSIC09b = DDLDONE.SelectedItem.Text;
                string mSIC19 = LBSDI019.Text;
                bool mSIC09c = DDLDONE.Enabled;
                //if (mSIC09b == "完成")mSIC08 = TXTDTL002.Text;
				if (mSIC09b == "完成" || mSIC09b == "已由永久設施取代")mSIC08 = DateTime.Now.ToString("d");

                //數量差異百分比：SIC02=(D2-D1)/D1
                double mSIC02 = 0; double mSIC02_1 = 0;
                /*
				if (mSIC19 == "是")
                {
                    string[] sArray = mSIC01a.Split('~');
                    string Arr_a = sArray[0];
                    string Arr_b = sArray[1];
                    if (Convert.ToDouble(Arr_a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(Arr_a)) / Convert.ToDouble(Arr_a) * 100, 2);
                    //if (Convert.ToDouble(Arr_b) != 0) mSIC02_1 = Math.Round((double)(Convert.ToDouble(mSIC01c) - Convert.ToDouble(Arr_b)) / Convert.ToDouble(Arr_b) * 100, 2);
                }
                else
                {
                    if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
                }
				*/
				if (Convert.ToDouble(mSIC01a) != 0) mSIC02 = Math.Round((double)(Convert.ToDouble(mSIC01b) - Convert.ToDouble(mSIC01a)) / Convert.ToDouble(mSIC01a) * 100, 2);
                

                //尺寸差異百分比：1:(A2-A1)/A1，2:((A2*B2)-(A1*B1))/(A1*B1)，3:((A2*B2*C2)-(A1*B1*C1))/(A1*B1*C1)
                string mSIC03 = HDF011.Value;
                double mSIC07 = 0; double mSIC07_1 = 0;
                if (mSIC19 == "是")
                {
                    double cA1 = Convert.ToDouble(mSIC04a.Split('~')[0]), cB1 = Convert.ToDouble(mSIC05a.Split('~')[0]), cC1 = Convert.ToDouble(mSIC06a.Split('~')[0]);
                    double cA2 = Convert.ToDouble(mSIC04b), cB2 = Convert.ToDouble(mSIC05b), cC2 = Convert.ToDouble(mSIC06b);
                    if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                    if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                    if (cA1 * cB1 * cC1 != 0) mSIC07 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);

                    cA1 = Convert.ToDouble(mSIC04a.Split('~')[1]); cB1 = Convert.ToDouble(mSIC05a.Split('~')[1]); cC1 = Convert.ToDouble(mSIC06a.Split('~')[1]);
                    cA2 = Convert.ToDouble(mSIC04c); cB2 = Convert.ToDouble(mSIC05c); cC2 = Convert.ToDouble(mSIC06c);
                    if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                    if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                    if (cA1 * cB1 * cC1 != 0) mSIC07_1 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);

                }
                else
                {
                    if (tSDIFD004 == "其他")
                    {
                        if (TSIC2.Text + "" != "") { mSIC02 = Convert.ToDouble(TSIC2.Text + ""); } else { mSIC02 = 0; }
                        if (TSIC2.Text + "" != "") { mSIC07 = Convert.ToDouble(TSIC7.Text + ""); } else { mSIC07 = 0; }
                    }
                    else
                    {
                        double cA1 = Convert.ToDouble(mSIC04a), cB1 = Convert.ToDouble(mSIC05a), cC1 = Convert.ToDouble(mSIC06a);
                        double cA2 = Convert.ToDouble(mSIC04b), cB2 = Convert.ToDouble(mSIC05b), cC2 = Convert.ToDouble(mSIC06b);
                        if (mSIC03 == "2") { cC1 = 1; cC2 = 1; }
                        if (mSIC03 == "1") { cB1 = 1; cB2 = 1; cC1 = 1; cC2 = 1; }
                        if (cA1 * cB1 * cC1 != 0) mSIC07 = Math.Round((double)(cA2 * cB2 * cC2 - cA1 * cB1 * cC1) / (cA1 * cB1 * cC1) * 100, 2);
                    }
                }
                
                if (mSIC02 > 20 || mSIC02 < -20) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20 %、構造物斷面及通水斷面之面積增加不超過20 % 或減少不超過10 %，且不影響原構造物正常功能，否則應辦理變更設計。";
                if (mSIC07 > 20 || mSIC02 < -10) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20 %、構造物斷面及通水斷面之面積增加不超過20 % 或減少不超過10 %，且不影響原構造物正常功能，否則應辦理變更設計。";
                //if (mSIC02 > 20 || mSIC02 < -20) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、尺寸差異百分比不得增加不得超過20%，減少不得超過10%，否則應辦理變更設計。";
                //if (mSIC07 > 20 || mSIC02 < -10) nMSG01 = "提醒您，依水土保持計劃審核監督辦法第十九條規定，各項水土保持設施，數量差異百分比增減不得超過20%、尺寸差異百分比不得增加不得超過20%，減少不得超過10%，否則應辦理變更設計。";
                
                if (tSDIFD004 == "其他" || mSIC01r!= mSIC01b || /* mSIC01r2 != mSIC01c || */ mSIC04r != mSIC04b || mSIC04r2 != mSIC04c || mSIC05r != mSIC05b || mSIC05r2 != mSIC05c || mSIC06r != mSIC06b || mSIC06r2 != mSIC06c || mSIC09a != mSIC09b || v=="A" )
                {
                    
					if (v == "A")
					{
						exeSqlStr = " insert into SwcItemChk (SWC000,DTLRPNO,SDI001,DTLTYPE,SIC01,SIC01_1,SIC01D,SIC02,SIC02_1,SIC03,SIC04,SIC04_1,SIC04D,SIC05,SIC05_1,SIC06,SIC06_1,SIC07,SIC07_1,SIC08,SIC09,SaveUser,SaveDate) values (@SWC000,@DTLRPNO,@SDI001,'D5',@SIC01,@SIC01_1,@SIC01D,@SIC02,@SIC02_1,@SIC03,@SIC04,@SIC04_1,@SIC04D,@SIC05,@SIC05_1,@SIC06,@SIC06_1,@SIC07,@SIC07_1,@SIC08,@SIC09,@SaveUser,getdate());";
					}
					else
					{
						if (mSIC09c)
						{
							exeSqlStr = " update SwcItemChk set SIC01=@SIC01,SIC01_1=@SIC01_1,SIC01D=@SIC01D,SIC02=@SIC02,SIC02_1=@SIC02_1,SIC03=@SIC03,SIC04=@SIC04,SIC04_1=@SIC04_1,SIC04D=@SIC04D,SIC05=@SIC05,SIC05_1=@SIC05_1,SIC06=@SIC06,SIC06_1=@SIC06_1,SIC07=@SIC07,SIC07_1=@SIC07_1,SIC08=@SIC08,SIC09=@SIC09,SaveUser=@SaveUser,SaveDate=getdate() where SWC000=@SWC000 and DTLRPNO=@DTLRPNO and SDI001=@SDI001; ";
						}
					}
					if(exeSqlStr != "")
					{
						ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
						using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
						{
							SWCConn.Open();
	
							using (var cmd = SWCConn.CreateCommand())
							{
								cmd.CommandText = exeSqlStr;
								//設定值
								#region
								if ((mSIC09b == "完成" || mSIC09b == "已由永久設施取代") && v == "A" && mSIC01r == mSIC01b && /*mSIC01r2 == mSIC01c &&*/ mSIC04r == mSIC04b && mSIC04r2 == mSIC04c && mSIC05r == mSIC05b && mSIC05r2 == mSIC05c && mSIC06r == mSIC06b && mSIC06r2 == mSIC06c && mSIC09a == mSIC09b)
									mSIC08 = ToSimpleUSDate(mSIC08b);
								
								cmd.Parameters.Add(new SqlParameter("@SWC000", mSWC000));
								cmd.Parameters.Add(new SqlParameter("@DTLRPNO", mDTLE001));
								cmd.Parameters.Add(new SqlParameter("@SDI001", mSDI001));
								cmd.Parameters.Add(new SqlParameter("@SIC01", mSIC01b));
								cmd.Parameters.Add(new SqlParameter("@SIC01_1", mSIC01c));
								cmd.Parameters.Add(new SqlParameter("@SIC01D", mSIC01Da));
								cmd.Parameters.Add(new SqlParameter("@SIC02", mSIC02));
								cmd.Parameters.Add(new SqlParameter("@SIC02_1", mSIC02_1));
								cmd.Parameters.Add(new SqlParameter("@SIC03", mSIC03));
								cmd.Parameters.Add(new SqlParameter("@SIC04", mSIC04b));
								cmd.Parameters.Add(new SqlParameter("@SIC04_1", mSIC04c));
								cmd.Parameters.Add(new SqlParameter("@SIC04D", mSIC04Da));
								cmd.Parameters.Add(new SqlParameter("@SIC05", mSIC05b));
								cmd.Parameters.Add(new SqlParameter("@SIC05_1", mSIC05c));
								cmd.Parameters.Add(new SqlParameter("@SIC06", mSIC06b));
								cmd.Parameters.Add(new SqlParameter("@SIC06_1", mSIC06c));
								cmd.Parameters.Add(new SqlParameter("@SIC07", mSIC07));
								cmd.Parameters.Add(new SqlParameter("@SIC07_1", mSIC07_1));
								cmd.Parameters.Add(new SqlParameter("@SIC08", mSIC08));
								cmd.Parameters.Add(new SqlParameter("@SIC09", mSIC09b));
								cmd.Parameters.Add(new SqlParameter("@SaveUser", ssUserID));
								#endregion
								cmd.ExecuteNonQuery();
								cmd.Cancel();
							}
						}
					}
                }
            }
        }
        if (nMSG01 != "") Response.Write("<script>alert('"+ nMSG01 + "');</script>");
    }
	private string ToSimpleUSDate(string tDate)
    {
        string rValue = tDate;
        if(rValue !="")
        rValue = tDate.Replace(tDate.Substring(0, 3), (Int32.Parse(tDate.Substring(0, 3)) +1911).ToString());
        return rValue;
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTDTL083", "TXTDTL084", "TXTDTL085", "TXTDTL086", "TXTDTL087" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTDTL083, TXTDTL084, TXTDTL085, TXTDTL086, TXTDTL087 };
        string csUpLoadField = "TXTDTL083";
        TextBox csUpLoadAppoj = TXTDTL083;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

        folderExists = Directory.Exists(SwcCaseFolderPath);
        if (folderExists == false)
        {
            Directory.CreateDirectory(SwcCaseFolderPath);
        }

        folderExists = Directory.Exists(SwcCaseFolderPath + CaseId);
        if (folderExists == false)
        {
            Directory.CreateDirectory(SwcCaseFolderPath + CaseId);
        }


        for (int i = 0; i < arryUpLoadField.Length; i++)
        {
            csUpLoadField = arryUpLoadField[i];
            csUpLoadAppoj = arryUpLoadAppoj[i];

            if (Session[csUpLoadField] + "" == "有檔案")
            {
                Boolean fileExists;
                string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;
                string SwcCaseFilePath = SwcCaseFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;

                fileExists = File.Exists(TempFilePath);
                if (fileExists)
                {
                    if (File.Exists(SwcCaseFilePath))
                    {
                        File.Delete(SwcCaseFilePath);
                    }
                    File.Move(TempFilePath, SwcCaseFilePath);
                }

            }
        }


    }
    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = LBSWC000.Text + "";

        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑

            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名

            switch (ChkType)
            {
                case "PIC":
                    List<string> allowedExtextsion01 = new List<string> { ".jpg", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 JPG PNG 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    List<string> allowedExtextsion02 = new List<string> { ".xls", ".xlsx" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 excel 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            
            int filesize = UpLoadBar.PostedFile.ContentLength;

            switch (ChkType)
            {
                case "PIC":
                    // 限制檔案大小，限制為 10MB
                    if (filesize > 10000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 10Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
                case "DOC":
                    // 限制檔案大小，限制為 50MB
                    if (filesize > 50000000)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 50Mb 以下檔案上傳，謝謝!!");
                        return;
                    }
                    break;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            UpLoadText.Text = SwcFileName;

            // 判斷 Server 上檔案名稱是否有重覆情況，有的話必須進行更名
            // 使用 Path.Combine 來集合路徑的優點
            //  以前發生過儲存 Table 內的是 \\ServerName\Dir（最後面沒有 \ 符號），
            //  直接跟 FileName 來進行結合，會變成 \\ServerName\DirFileName 的情況，
            //  資料夾路徑的最後面有沒有 \ 符號變成還需要判斷，但用 Path.Combine 來結合的話，
            //  資料夾路徑沒有 \ 符號，會自動補上，有的話，就直接結合

            string serverFilePath = Path.Combine(serverDir, SwcFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(SwcFileName);
            int fileCount = 1;

            //while (File.Exists(serverFilePath))
            //{
            //    // 重覆檔案的命名規則為 檔名_1、檔名_2 以此類推
            //    filename = string.Concat(fileNameOnly, "_", fileCount, extension);
            //    serverFilePath = Path.Combine(serverDir, filename);
            //    fileCount++;
            //}

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);
                //error_msg.Text = "檔案上傳成功";

                switch (ChkType)
                {
                    case "PIC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.ImageUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;

                        break;

                    case "DOC":
                        UpLoadLink.Text = SwcFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;
                }

            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }
        }
        else
            Session[UpLoadStr] = "";
    }
    protected void imagestitch(System.Web.UI.WebControls.Image UpLoadView, string sourcePath, int ShowWidth, int ShowHeight)
    {  //影像調整，處理照片顯示

        if (File.Exists(sourcePath))
        {
            System.Drawing.Image image = new Bitmap(sourcePath);

            int width = image.Width;
            int height = image.Height;

            int ShowUpPicWidth = 0;
            int ShowUpPicHeight = 0;

            if (width < height)
            {
                ShowUpPicWidth = Convert.ToInt32(width * ShowHeight / height);
                ShowUpPicHeight = ShowHeight;
            }
            else
            {
                ShowUpPicWidth = ShowWidth;
                ShowUpPicHeight = Convert.ToInt32(height * ShowWidth / width);
            }
            UpLoadView.Width = ShowUpPicWidth;
            UpLoadView.Height = ShowUpPicHeight;

            image.Dispose();
        }
    }
    private void DeleteUpLoadFile(string DelType, TextBox ImgText, System.Web.UI.WebControls.Image ImgView, HyperLink FileLink, string DelFieldValue, string AspxFeildId, int NoneWidth, int NoneHeight)
    {
        string csCaseID = LBSWC000.Text + "";
        string csDTLID = LBDTL001.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update SWCDTL05 set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SWC000 = '" + csCaseID + "' ";
        strSQLClearFieldValue = strSQLClearFieldValue + "   and DTLE001 = '" + csDTLID + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];

        string DelFileName = ImgText.Text;
        string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + ImgText.Text;

        try
        {
            if (File.Exists(TempFileFullPath))
            {
                File.Delete(TempFileFullPath);
            }
        }
        catch
        {
        }
        try
        {
            if (File.Exists(FileFullPath))
            {
                File.Delete(FileFullPath);
            }
        }
        catch
        {
        }

        switch (DelType)
        {
            case "PIC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                break;
            case "DOC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }



    protected void TXTDTL083_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("DOC", TXTDTL083_FileUpload, TXTDTL083, "TXTDTL083", "_" + rDTLNO + "_05_doc1", null, Link083);
    }

    protected void TXTDTL084_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("DOC", TXTDTL084_FileUpload, TXTDTL084, "TXTDTL084", "_" + rDTLNO + "_05_doc2", null, Link084);

    }

    protected void TXTDTL083_fileclean_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL083, null, Link083, "DTLE083", "TXTDTL083", 0, 0);
    }
    protected void DataLock_Click(object sender, EventArgs e)
    {
        string sSWC000 = LBSWC000.Text;
        string sDTLE000 = LBDTL001.Text + "";

        string sEXESQLSTR = "";
		
        if (!chkPageInput()) { return; }
        SaveCase_Click(sender, e);
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            string strSQLRV = " select * from SWCDTL05 "; 
            strSQLRV = strSQLRV + " where SWC000 = '" + sSWC000 + "' ";
            strSQLRV = strSQLRV + "   and DTLE000 = '" + sDTLE000 + "' ";

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            if (!readeSwc.HasRows)
            {
                sEXESQLSTR = sEXESQLSTR + " INSERT INTO SWCDTL05 (SWC000,DTLE000) VALUES ('" + sSWC000 + "','" + sDTLE000 + "');";
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
			
            sEXESQLSTR = sEXESQLSTR + " Update SWCDTL05 Set ";
            sEXESQLSTR = sEXESQLSTR + "  DATALOCK = 'Y' ";
            sEXESQLSTR = sEXESQLSTR + " Where SWC000 = '" + sSWC000 + "'";
            sEXESQLSTR = sEXESQLSTR + "   and DTLE000 = '" + sDTLE000 + "'; ";
			
            sEXESQLSTR = sEXESQLSTR + "  Update SWCCASE set SWC091 = '"+DDLDTL089.SelectedValue+"',SWC140 = '"+TXTDTL003.Text+"' where SWC000='"+sSWC000+"'; ";
			sEXESQLSTR = sEXESQLSTR + "  Update tslm2.dbo.SWCSWC set SWC91 = '"+DDLDTL089.SelectedValue+"',SWC140 = '"+TXTDTL003.Text+"' where SWC00='"+sSWC000+"'; ";

            SqlCommand objCmdUpd = new SqlCommand(sEXESQLSTR, SwcConn);
            objCmdUpd.ExecuteNonQuery();
            objCmdUpd.Dispose();
        }
        #region 檢查結果為【缺失應改未改或未依計畫施工，通知大地處查處】時系統發信通知
        string pgDTL004 = DDLDTL004.SelectedItem.Text;
        if (pgDTL004 == "缺失應改未改或未依計畫施工，通知大地處查處")
            SendMailNotice(sSWC000);
        #endregion

        string rCaseId = Request.QueryString["SWCNO"] + "";
        Response.Write("<script>location.href='SWC003.aspx?SWCNO=" + rCaseId + "';</script>");
    }

    private void SendMailNotice(string sSWC000)
    {
        GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();
        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkUserName = arrayChkUserMsg[1] + "";
        string ChkJobTitle = arrayChkUserMsg[2] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
        string ChkMBGROUP = arrayChkUserMsg[4] + "";
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserName = ChkUserName.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayJobTitle = ChkJobTitle.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayMBGROUP = ChkMBGROUP.Split(new string[] { ";;" }, StringSplitOptions.None);

        string sqlStr = " select SWC.SWC002,SWC.SWC005, SWC.SWC012, SWC.SWC013,SWC.SWC013TEL, SWC.SWC025, SWC.SWC045ID,U.ETName,U.ETEmail,SWC.SWC024ID,SWC.SWC108 from SWCCASE SWC ";
        sqlStr += " LEFT JOIN ETUsers U on SWC.SWC045ID = U.ETID where SWC.SWC000 = '" + sSWC000 + "'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        #region 
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(sqlStr, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            string SentMailGroup = "";
            while (readeSwc.Read())
            {
                string tSWC002 = readeSwc["SWC002"] + "";
                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC012 = readeSwc["SWC012"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013TEL = readeSwc["SWC013TEL"] + "";
                string tSWC024ID = readeSwc["SWC024ID"] + "";
                string tSWC025 = readeSwc["SWC025"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tETEmail = readeSwc["ETEmail"] + "";
                string tSWC108 = readeSwc["SWC108"] + "";

                //寄件名單
                //科長、正工程司、股長、管理者、承辦人員、施純民(ge-10755)
                //監造技師email(SWC045)、義務人(簡訊)、聯絡人(email)、檢查公會(email)


                for (int i = 1; i < arrayUserId.Length; i++)
                {
                    string aUserId = arrayUserId[i];
                    string aUserName = arrayUserName[i];
                    string aJobTitle = arrayJobTitle[i];
                    string aUserMail = arrayUserMail[i];
                    string aMBGROUP = arrayMBGROUP[i];

                    if (aJobTitle.Trim() == "科長" || aJobTitle.Trim() == "正工程司" || aJobTitle.Trim() == "股長" || aMBGROUP.Trim() == "系統管理員" || aUserName.Trim() == tSWC025.Trim() || aUserId.Trim() == "ge-10755")
                        SentMailGroup += aUserMail + ";;";
                }

                //轄區【水土保持計畫】已新增監造紀錄，監造結果為【缺失應改未改或未依計畫施工，通知大地處查處】改正期限為【改正期限(八)】，未依計畫施作項目如下：【八、未依計畫施作事項及改正期限欄位】
                string pgDTL004 = DDLDTL004.SelectedItem.Text;  //缺失應改未改或未依計畫施工，通知大地處查處
                string pgDTL081 = TXTDTL081.Text;   //改正期限
                string pgDTL092 = LBDTL092.Text;   //未依計畫施作事項

                if (pgDTL004.Trim()== "缺失應改未改或未依計畫施工，通知大地處查處") { }
                string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
                string ssMailSub01 = tSWC012 + "水土保持計畫【" + tSWC002 + "】已新增監造紀錄";
                string ssMailBody01 = tSWC012 + "【" + tSWC005 + "】已新增監造紀錄，監造結果為【"+ pgDTL004 + "】改正期限為【"+ pgDTL081 + "】，未依計畫施作項目如下：<br>【"+ pgDTL092 + "】" + "<br><br>";
                ssMailBody01 = ssMailBody01 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody01 = ssMailBody01 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                if (pgDTL004.Trim() == "缺失應改未改或未依計畫施工，通知大地處查處") { bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01); }

                //您好，【水土保持計畫】已新增監造紀錄，監造結果為【缺失應改未改或未依計畫施工，通知大地處查處】請於【改正期限(八)】前改正，未依計畫施作項目如下：【八、未依計畫施作事項及改正期限欄位】
                string[] arraySentMail02 = new string[] { tETEmail, tSWC108, SBApp.GetGeoUser(tSWC024ID, "Email") };
                string ssMailSub02 = "您好，" + "水土保持計畫【" + tSWC002 + "】已新增監造紀錄";
                string ssMailBody02 = "您好，" + "【" + tSWC005 + "】已新增監造紀錄，監造結果為【" + pgDTL004 + "】請於【" + pgDTL081 + "】前改正，未依計畫施作項目如下：<br>【" + pgDTL092 + "】" + "<br><br>";
                ssMailBody02 = ssMailBody02 + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
                ssMailBody02 = ssMailBody02 + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
                if (pgDTL004.Trim() == "缺失應改未改或未依計畫施工，通知大地處查處") { bool MailTo02 = SBApp.Mail_Send(arraySentMail02, ssMailSub02, ssMailBody02); }

                string ssMailBody03 = "您好，【" + tSWC005 + "】已新增監造紀錄，請至臺北市水土保持申請書件管理平台上瀏覽。";
                ssMailBody03 = ssMailBody02;
                if (pgDTL004.Trim() == "缺失應改未改或未依計畫施工，通知大地處查處")
				{
					string[] arraySWC013TEL = tSWC013TEL.Split(new string[] { ";" }, StringSplitOptions.None);
					SBApp.SendSMS_Arr(arraySWC013TEL, ssMailBody03);
				}
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
        }
        #endregion
    }

    protected void TXTDTL085_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL085_FileUpload, TXTDTL085, "TXTDTL085", "_" + rDTLNO + "_05_sign1", null, HyperLink085);
    }
    protected void TXTDTL086_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL086_FileUpload, TXTDTL086, "TXTDTL086", "_" + rDTLNO + "_05_sign2", null, HyperLink086);

    }
    protected void TXTDTL087_fileuploadok_Click(object sender, EventArgs e)
    {
        string rDTLNO = LBDTL001.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTDTL087_FileUpload, TXTDTL087, "TXTDTL087", "_" + rDTLNO + "_05_sign3", null, HyperLink087);
    }

    protected void TXTDTL085_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL085, null, HyperLink085, "DTLE085", "TXTDTL085", 0, 0);
    }

    protected void TXTDTL086_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL086, null, HyperLink086, "DTLE086", "TXTDTL086", 0, 0);
    }

    protected void TXTDTL087_fileuploaddel_Click(object sender, EventArgs e)
    {
        DeleteUpLoadFile("DOC", TXTDTL087, null, HyperLink087, "DTLE087", "TXTDTL087", 0, 0);
    }

    private void SetDtlData(string rSWCNO,string v2)
    {
        GBClass001 SBApp = new GBClass001();
        bool bb = true;

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //分段驗收核定項目
        using (SqlConnection ItemConn = new SqlConnection(connectionString.ConnectionString))
        {
            ItemConn.Open();

            int ni = 0;

            string strSQLRV = "select * from SwcDocItem";
            strSQLRV = strSQLRV + " Where SWC000 = '" + rSWCNO + "' ";
            strSQLRV = strSQLRV + " order by SDI001 ";

            SqlDataReader readerItem;
            SqlCommand objCmdItem = new SqlCommand(strSQLRV, ItemConn);
            readerItem = objCmdItem.ExecuteReader();

            while (readerItem.Read())
            {
                bb = false;
                string sSDI001 = readerItem["SDI001"] + "";
                string sSDI002 = readerItem["SDI002"] + "";
                string sSDI003 = readerItem["SDI003"] + "";
                string sSDI004 = readerItem["SDI004"] + "";
                string sSDI005 = readerItem["SDI005"] + "";
                string sSDI006 = readerItem["SDI006"] + "";
                string sSDI006_1 = readerItem["SDI006_1"] + "";
                string sSDI006D = readerItem["SDI006D"] + "";
                string sSDI007 = readerItem["SDI007"] + "";
                string sSDI008 = readerItem["SDI008"] + "";
                string sSDI009 = readerItem["SDI009"] + "";
                string sSDI010 = readerItem["SDI010"] + "";
                string sSDI011 = readerItem["SDI011"] + "";
                string sSDI012 = readerItem["SDI012"] + "";
                string sSDI012_1 = readerItem["SDI012_1"] + "";
                string sSDI012D = readerItem["SDI012D"] + "";
                string sSDI013 = readerItem["SDI013"] + "";
                string sSDI013_1 = readerItem["SDI013_1"] + "";
                string sSDI014 = readerItem["SDI014"] + "";
                string sSDI014_1 = readerItem["SDI014_1"] + "";
                string sSDI015 = readerItem["SDI015"] + "";
                string sSDI016 = readerItem["SDI016"] + "";
                string sSDI017 = readerItem["SDI017"] + "";
                string sSDI019 = readerItem["SDI019"] + "";

                DataTable tbSDIVS = (DataTable)ViewState["SwcDocItem"];

                if (tbSDIVS == null)
                {
                    DataTable SDITB = new DataTable();

                    SDITB.Columns.Add(new DataColumn("SDIFDNI", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD003", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD006D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD009", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD010", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD011", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD012_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD013_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD014_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD015", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDIFD016", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDILB019", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK001D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK002_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK004D", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK005_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK006_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK007_1", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK008", typeof(string)));
                    SDITB.Columns.Add(new DataColumn("SDICHK009", typeof(string)));

                    ViewState["SwcDocItem"] = SDITB;
                    tbSDIVS = (DataTable)ViewState["SwcDocItem"];
                }
                if (sSDI017 != "完成" && sSDI017 != "已由永久設施取代") sSDI017 ="未完成";
                if (sSDI019 == "是" && sSDI005 != "其他")
                {
                    //sSDI006 = sSDI006 + "~" + sSDI006_1;
                    sSDI006 = sSDI006;
					sSDI012 = sSDI012 + "~" + sSDI012_1;
                    sSDI013 = sSDI013 + "~" + sSDI013_1;
                    sSDI014 = sSDI014 + "~" + sSDI014_1;
                }

                string sSIC002 = "-";
                string sSIC007 = "-";

                DataRow SDITBRow = tbSDIVS.NewRow();

                SDITBRow["SDIFDNI"] = ni.ToString();
                SDITBRow["SDIFD001"] = sSDI001;
                SDITBRow["SDIFD002"] = sSDI002;
                SDITBRow["SDIFD003"] = sSDI003;
                SDITBRow["SDIFD004"] = sSDI004;
                SDITBRow["SDIFD005"] = sSDI005;
                SDITBRow["SDIFD006"] = sSDI006;
                SDITBRow["SDIFD006D"] = sSDI006D;
                SDITBRow["SDIFD007"] = sSDI007;
                SDITBRow["SDIFD008"] = sSDI008;
                SDITBRow["SDIFD009"] = sSDI009;
                SDITBRow["SDIFD010"] = sSDI010;
                SDITBRow["SDIFD011"] = sSDI011;
                SDITBRow["SDIFD012"] = sSDI012;
                SDITBRow["SDIFD012_1"] = sSDI012_1;
                SDITBRow["SDIFD013"] = sSDI013;
                SDITBRow["SDIFD013_1"] = sSDI013_1;
                SDITBRow["SDIFD014"] = sSDI014;
                SDITBRow["SDIFD014_1"] = sSDI014_1;
                SDITBRow["SDIFD015"] = sSDI015;
                SDITBRow["SDIFD016"] = sSDI016;
                SDITBRow["SDICHK001"] = "";
                SDITBRow["SDICHK002"] = "-";
                SDITBRow["SDICHK004"] = "";
                SDITBRow["SDICHK004_1"] = "";
                SDITBRow["SDICHK004D"] = sSDI012D;
                SDITBRow["SDICHK005"] = "";
                SDITBRow["SDICHK005_1"] = "";
                SDITBRow["SDICHK006"] = "";
                SDITBRow["SDICHK006_1"] = "";
                SDITBRow["SDICHK007"] = "-";
                SDITBRow["SDICHK008"] = "";// "-";
                SDITBRow["SDICHK009"] = sSDI017;

                tbSDIVS.Rows.Add(SDITBRow);

                //DB
                if (v2 == "AddNew")
                {
                    using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                    {
                        ItemConnS.Open();
                        string strSQLRVS = " select top 1 DTLE000 as MAXKEY  from SWCDTL05 D5 left join SwcItemChk Ck on D5.swc000=Ck.SWC000 and D5.DTLE000=Ck.DTLRPNO where D5.swc000='" + rSWCNO + "' and D5.DATALOCK='Y' and isnull(Ck.SWC000,'') <>'' order by D5.savedate Desc ";

                        SqlDataReader readerItemS;
                        SqlCommand objCmdItemS = new SqlCommand(strSQLRVS, ItemConnS);
                        readerItemS = objCmdItemS.ExecuteReader();

                        while (readerItemS.Read()) { v2 = readerItemS["MAXKEY"] + ""; }
                    }

                }

                using (SqlConnection ItemConnS = new SqlConnection(connectionString.ConnectionString))
                {
                    ItemConnS.Open();

                    string tSIC01 = "";
                    string tSIC01_1 = "";
                    string tSIC01D = "";
                    string tSIC02 = "";
                    string tSIC02_1 = "";
                    string tSIC03 = "";
                    string tSIC04 = "";
                    string tSIC04_1 = "";
                    string tSIC04D = "";
                    string tSIC05 = "";
                    string tSIC05_1 = "";
                    string tSIC06 = "";
                    string tSIC06_1 = "";
                    string tSIC07 = "";
                    string tSIC07_1 = "";
                    string tSIC08 = "";
                    string tSIC09 = "未完成";
                    string tSIC10 = "";

                    string strSQLRVS = "select * from SwcItemChk";
                    strSQLRVS += " Where SWC000 = '" + rSWCNO + "' ";
                    strSQLRVS += "   and DTLRPNO = '" + v2 + "' ";
                    strSQLRVS += "   and SDI001 = '" + sSDI001 + "' ";

                    SqlDataReader readerItemS;
                    SqlCommand objCmdItemS = new SqlCommand(strSQLRVS, ItemConnS);
                    readerItemS = objCmdItemS.ExecuteReader();

                    while (readerItemS.Read())
                    {
                        tSIC01 = readerItemS["SIC01"] + "";
                        tSIC01_1 = readerItemS["SIC01_1"] + "";
                        tSIC01D = readerItemS["SIC01D"] + "";
                        tSIC02 = readerItemS["SIC02"] + "";
                        tSIC02_1 = readerItemS["SIC02_1"] + "";
                        tSIC03 = readerItemS["SIC03"] + "";
                        tSIC04 = readerItemS["SIC04"] + "";
                        tSIC04_1 = readerItemS["SIC04_1"] + "";
                        tSIC04D = readerItemS["SIC04D"] + "";
                        tSIC05 = readerItemS["SIC05"] + "";
                        tSIC05_1 = readerItemS["SIC05_1"] + "";
                        tSIC06 = readerItemS["SIC06"] + "";
                        tSIC06_1 = readerItemS["SIC06_1"] + "";
                        tSIC07 = readerItemS["SIC07"] + "";
                        tSIC07_1 = readerItemS["SIC07_1"] + "";
                        tSIC08 = readerItemS["SIC08"] + "";
                        tSIC09 = readerItemS["SIC09"] + "";
                        tSIC10 = readerItemS["SIC10"] + "";
                    }
                    DataRow SDITBRow2 = tbSDIVS.NewRow();

                    SDITBRow2["SDIFDNI"] = ni.ToString();
                    SDITBRow2["SDIFD001"] = sSDI001;
                    SDITBRow2["SDIFD002"] = sSDI002;
                    SDITBRow2["SDIFD003"] = "";
                    SDITBRow2["SDIFD004"] = "";
                    SDITBRow2["SDIFD005"] = "";
                    SDITBRow2["SDIFD006"] = sSDI006;
                    SDITBRow2["SDIFD006D"] = sSDI006D;
                    SDITBRow2["SDIFD007"] = sSDI007;
                    SDITBRow2["SDIFD008"] = sSDI008;
                    SDITBRow2["SDIFD009"] = sSDI009;
                    SDITBRow2["SDIFD010"] = sSDI010;
                    SDITBRow2["SDIFD011"] = sSDI011;
                    SDITBRow2["SDIFD012"] = sSDI012;
                    SDITBRow2["SDIFD013"] = sSDI013;
                    SDITBRow2["SDIFD014"] = sSDI014;
                    SDITBRow2["SDIFD015"] = sSDI015;
                    SDITBRow2["SDIFD016"] = sSDI016;
                    SDITBRow2["SDILB019"] = sSDI019;
                    SDITBRow2["SDICHK001"] = tSIC01;
                    SDITBRow2["SDICHK001_1"] = tSIC01_1;
                    SDITBRow2["SDICHK001D"] = tSIC01D;
                    SDITBRow2["SDICHK002"] = tSIC02;
                    SDITBRow2["SDICHK002_1"] = tSIC02_1;
                    SDITBRow2["SDICHK004"] = tSIC04;
                    SDITBRow2["SDICHK004_1"] = tSIC04_1;
                    SDITBRow2["SDICHK004D"] = tSIC04D;
                    SDITBRow2["SDICHK005"] = tSIC05;
                    SDITBRow2["SDICHK005_1"] = tSIC05_1;
                    SDITBRow2["SDICHK006"] = tSIC06;
                    SDITBRow2["SDICHK006_1"] = tSIC06_1;
                    SDITBRow2["SDICHK007"] = tSIC07;
                    SDITBRow2["SDICHK007_1"] = tSIC07_1;
                    SDITBRow2["SDICHK008"] = SBApp.DateView(tSIC08,"04");
                    SDITBRow2["SDICHK009"] = tSIC09.Trim();

                    tbSDIVS.Rows.Add(SDITBRow2);
                }

                ViewState["SwcDocItem"] = tbSDIVS;

                SDIList.DataSource = tbSDIVS;
                SDIList.DataBind();

                //TXTSDINI.Text = ni.ToString();
            }
            readerItem.Close();
        }
        GVMSG.Visible = bb;
    }

    protected void SDIList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "Modify":
                int aa = Convert.ToInt32(e.CommandArgument);

                HiddenField HDF011 = (HiddenField)SDIList.Rows[aa].Cells[0].FindControl("HDSDI011");
                TextBox CHK01 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001");
                TextBox CHK01_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001_1");
                TextBox CHK01D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK001D");
                TextBox CHK04 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004");
                TextBox CHK04_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004_1");
                TextBox CHK04D = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK004D");
                TextBox CHK05 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005");
                TextBox CHK05_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK005_1");
                TextBox CHK06 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006");
                TextBox CHK06_1 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("CHK006_1");
                TextBox CHK02 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK002");
                TextBox CHK07 = (TextBox)SDIList.Rows[aa].Cells[3].FindControl("TXTCHK007");

                DropDownList DDLDONE = (DropDownList)SDIList.Rows[aa].Cells[3].FindControl("DDLDONE");

                CHK01.Enabled = true; /*CHK01_1.Enabled = true;*/ CHK01_1.Enabled = false; CHK02.Enabled = true; CHK07.Enabled = true;
                switch (HDF011.Value) { case "1": CHK04.Enabled = true; CHK04_1.Enabled = true; break; case "2": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; break; case "3": CHK04.Enabled = true; CHK04_1.Enabled = true; CHK05.Enabled = true; CHK05_1.Enabled = true; CHK06.Enabled = true; CHK06_1.Enabled = true; break; }
                DDLDONE.Enabled = true;
                CHK01D.Enabled = true; CHK04D.Enabled = true;
                break;
        }

    }
}