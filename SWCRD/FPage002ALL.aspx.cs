using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class SWCRD_FPage002ALL : System.Web.UI.Page
{
    string pubDDLUID = "";
    string pubDDLUNAME = "";
    string optojs = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserName = Session["NAME"] + "";
        string ssUserType = Session["UserType"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        //if (ssUserName == "" || ssUserType != "03") { Response.Redirect("../SWCDOC/SWC001.aspx"); }

        if (!IsPostBack) { 
            getGVDate();
        }
		TextUserName.Text = "您好";
        #region 全區供用
        SBApp.ViewRecord("設定檢查公會技師", "update", "");
        #endregion 全區供用
		//ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "calendar_test1", "<script>c('" + Session["optojs"] + "');</script>");
    }
    private void getGVDate()
    {
		optojs = "";
		Session["optojs"] = "";
        GBClass001 CLS = new GBClass001();
        string ssUserGuild = Session["ETU_Guild01"] + "";
        GenerateDropDownList();
        RemoveSelSession();

        string pageQ001 = TBQ001.Text;
        string pageQ002 = DDLQ002.Text;
        string pageQ003 = TBQ003.Text;
        string pageQ004a = TBQ004a.Text;
        string pageQ004b = TBQ004b.Text;
        string pageQ005a = TBQ005a.Text;
        string pageQ005b = TBQ005b.Text;
        string pageQ006 = TBQ006.Text;

        Session["PageSearch001"] = pageQ001;
        Session["PageSearch002"] = pageQ002;
        Session["PageSearch003"] = pageQ003;
        Session["PageSearch004a"] = pageQ004a;
        Session["PageSearch004b"] = pageQ004b;
        Session["PageSearch005a"] = pageQ005a;
        Session["PageSearch005b"] = pageQ005b;
        Session["PageSearch006"] = pageQ006;

        string pageSearchSel = "";
        if (pageQ001.Trim() != "") pageSearchSel += " AND SWC02 like N'%" + pageQ001 + "%' ";
        if (pageQ002.Trim() != "") pageSearchSel += " AND SWC12 = '" + pageQ002 + "' ";
        if (pageQ003.Trim() != "") pageSearchSel += " AND SWC05 like N'%" + pageQ003 + "%' ";
        if (pageQ004a.Trim() != "") pageSearchSel += " AND SWC114 >= '" + pageQ004a + "' ";
        if (pageQ004b.Trim() != "") pageSearchSel += " AND SWC114 <= '" + pageQ004b + "' ";
        if (pageQ005a.Trim() != "") pageSearchSel += " AND SWC115 >= '" + pageQ005a + "' ";
        if (pageQ005b.Trim() != "") pageSearchSel += " AND SWC115 <= '" + pageQ005b + "' ";
        if (pageQ006.Trim() != "") pageSearchSel += " AND SWC45 = '" + pageQ006 + "' ";

        DataTable OBJ_GV02 = null, OBJ_GV03 = null;
        DataTable DTGV02 = new DataTable();
        DTGV02.Columns.Add(new DataColumn("SWC000", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC002", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC005", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC012", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC013", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC013TEL", typeof(string))); 
        DTGV02.Columns.Add(new DataColumn("SWC024", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC024ID", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC025", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC045", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC045ID", typeof(string)));
        //DTGV02.Columns.Add(new DataColumn("SWC088", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC108", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC114", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC114H", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC114M", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC114_2", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC114_2H", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC114_2M", typeof(string)));
        //DTGV02.Columns.Add(new DataColumn("CHKUS", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("CHKSET", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("CHKSETID", typeof(string)));
        DTGV02.Columns.Add(new DataColumn("SWC121", typeof(string)));
        ViewState["S7GVGS22"] = DTGV02;
        OBJ_GV02 = DTGV02;
        DataTable DTGV03 = new DataTable();
        DTGV03.Columns.Add(new DataColumn("SWC000", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC002", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC005", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC012", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC013", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC013TEL", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC024", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC024ID", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC025", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC045", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC045ID", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC108", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC115", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC115H", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC115M", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC115_2", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC115_2H", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC115_2M", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("CHKSET", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("CHKSETID", typeof(string)));
        DTGV03.Columns.Add(new DataColumn("SWC122", typeof(string)));
        ViewState["S7GVGS23"] = DTGV03;
        OBJ_GV03 = DTGV03;

        string myGV2Data = " select A.*,E.LAND001 from swcswc A left join (select SWC000,MIN(LAND000) as LAND000 from TCGESWC.dbo.SWCLAND group by SWC000) D ON A.SWC00 = D.SWC000 left join (select SWC000,LAND000,LAND001 as LAND001 from TCGESWC.dbo.SWCLAND) E ON A.SWC00 = E.SWC000 and D.LAND000 = E.LAND000 where SWC04='施工中' " + pageSearchSel + " order by SWC00 desc; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = myGV2Data;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tmpSwc000 = readerTslm["SWC00"] + "";
                            string tmpSwc002 = readerTslm["SWC02"] + "";
                            string tmpSwc005 = readerTslm["SWC05"] + "";
                            string tmpSwc012 = readerTslm["SWC12"] + "";
                            string tmpSwc013 = readerTslm["SWC13"] + "";
                            string tmpSwc013TEL = readerTslm["SWC013TEL"] + "";
                            string tmpSwc024 = readerTslm["SWC24"] + "";
                            string tmpSwc024ID = readerTslm["SWC024ID"] + "";
                            string tmpSwc025 = readerTslm["SWC25"] + "";
                            string tmpSwc045 = readerTslm["SWC45"] + "";
                            string tmpSwc045ID = readerTslm["SWC045ID"] + "";
                            string tmpSwc108 = readerTslm["SWC108"] + "";
                            string tmpSwc114 = readerTslm["SWC114"] + "";
                            string tmpSwc115 = readerTslm["SWC115"] + "";
                            string[] tmpCHKSET = getChkSet(tmpSwc000);
							string tmpSwc121 = readerTslm["SWC121"] + "";
							string tmpSwc122 = readerTslm["SWC122"] + "";
							
							//書件;起;迄;義務人;案件編號;地點;地區;顏色;監造技師;檢查公會
							if(CLS.DateView(readerTslm["SWC114"] + "","00") !="" && CLS.DateView(readerTslm["SWC114_2"] + "","00") !=""){
								optojs += readerTslm["SWC05"].ToString().Trim() + "|" + CLS.DateView(readerTslm["SWC114"] + "","00") + " " + CLS.DateView(readerTslm["SWC114"] + "","06") + ":" + CLS.DateView(readerTslm["SWC114"] + "", "07") + "|" + CLS.DateView(readerTslm["SWC114_2"] + "","00") + " " + CLS.DateView(readerTslm["SWC114_2"] + "","06") + ":" + CLS.DateView(readerTslm["SWC114_2"] + "", "07") + "|" + readerTslm["SWC13"] + "|" + tmpSwc000 + "|" + readerTslm["SWC121"] + "|" + readerTslm["LAND001"] + "|#7BAAC5|" + readerTslm["SWC45"] + "|" + readerTslm["SWC24"] + "/";
							}
                            #region 檢查委員
                            #endregion

                            DataRow dr01 = OBJ_GV02.NewRow();
                            dr01["SWC000"] = tmpSwc000;
                            dr01["SWC002"] = tmpSwc002;
                            dr01["SWC005"] = tmpSwc005;
                            dr01["SWC012"] = tmpSwc012;
                            dr01["SWC013"] = tmpSwc013;
                            dr01["SWC013TEL"] = tmpSwc013TEL;
                            dr01["SWC024"] = tmpSwc024;
                            dr01["SWC024ID"] = tmpSwc024ID;
                            dr01["SWC025"] = tmpSwc025;
                            dr01["SWC045"] = tmpSwc045;
                            dr01["SWC045ID"] = tmpSwc045ID;
                            dr01["SWC108"] = tmpSwc108;
							dr01["SWC114"] = CLS.DateView(readerTslm["SWC114"] + "", "00");
							dr01["SWC114H"] = CLS.DateView(readerTslm["SWC114"] + "", "06");
							dr01["SWC114M"] = CLS.DateView(readerTslm["SWC114"] + "", "07");
							dr01["SWC114_2"] = CLS.DateView(readerTslm["SWC114_2"] + "", "00");
							dr01["SWC114_2H"] = CLS.DateView(readerTslm["SWC114_2"] + "", "06");
							dr01["SWC114_2M"] = CLS.DateView(readerTslm["SWC114_2"] + "", "07");
                            dr01["CHKSET"] = tmpCHKSET[0];
                            dr01["CHKSETID"] = tmpCHKSET[1];
							dr01["SWC121"] = tmpSwc121;
							
							
							
                            OBJ_GV02.Rows.Add(dr01);
                            ViewState["S7GVGS22"] = OBJ_GV02;

                            if (haveONA9D(tmpSwc000))
                            {
								//書件;起;迄;義務人;案件編號;地點;地區;顏色;監造技師;檢查公會
								if(CLS.DateView(readerTslm["SWC115"] + "","00") !="" && CLS.DateView(readerTslm["SWC115_2"] + "","00") !=""){
									optojs += readerTslm["SWC05"].ToString().Trim() + "|" + CLS.DateView(readerTslm["SWC115"] + "","00") + " " + CLS.DateView(readerTslm["SWC115"] + "","06") + ":" + CLS.DateView(readerTslm["SWC115"] + "", "07") + "|" + CLS.DateView(readerTslm["SWC115_2"] + "","00") + " " + CLS.DateView(readerTslm["SWC115_2"] + "","06") + ":" + CLS.DateView(readerTslm["SWC115_2"] + "", "07") + "|" + readerTslm["SWC13"] + "|" + tmpSwc000 + "|" + readerTslm["SWC122"] + "|" + readerTslm["LAND001"] + "|#88CB7F|" + readerTslm["SWC45"] + "|" + readerTslm["SWC24"] + "/";
								}
							
                                DataRow dr03 = OBJ_GV03.NewRow();
                                dr03["SWC000"] = tmpSwc000;
                                dr03["SWC002"] = tmpSwc002;
                                dr03["SWC005"] = tmpSwc005;
                                dr03["SWC012"] = tmpSwc012;
                                dr03["SWC013"] = tmpSwc013;
                                dr03["SWC013TEL"] = tmpSwc013TEL;
                                dr03["SWC024"] = tmpSwc024;
                                dr03["SWC024ID"] = tmpSwc024ID;
                                dr03["SWC025"] = tmpSwc025;
                                dr03["SWC045"] = tmpSwc045;
                                dr03["SWC045ID"] = tmpSwc045ID;
                                dr03["SWC108"] = tmpSwc108;
                                dr03["SWC115"] = CLS.DateView(tmpSwc115 + "", "00");
                                dr03["CHKSET"] = tmpCHKSET[0];
                                dr03["CHKSETID"] = tmpCHKSET[1];

                                OBJ_GV03.Rows.Add(dr03);
                                ViewState["S7GVGS23"] = OBJ_GV03;
                            }
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
		Session["optojs"] = optojs.Replace(System.Environment.NewLine,"");
		ClientScript.RegisterClientScriptBlock(ClientScript.GetType(), "calendar_test1", "<script>c('" + Session["optojs"] + "');</script>");
        GridView2.DataSource = OBJ_GV02;
        GridView2.DataBind();
        GridView3.DataSource = OBJ_GV03;
        GridView3.DataBind();
    }
    #region haveONA9D
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
    #endregion 
    #region getChkSet
    private string[] getChkSet(string tmpSWC000)
    {
        GBClass001 C1 = new GBClass001();
        string[] rValue = new string[] {"","" };
        string tmpSql = " select * from tcgeswc.dbo.GuildGroup where RGType = 'S4' and SWC000='" + tmpSWC000 + "' order by convert(float,RGSID) ;";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = tmpSql;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tmpETID = readerTslm["ETID"] + "";
                            rValue[0] += rValue[0].Trim() == "" ? C1.GetETUser(tmpETID, "Name") : ";;" + C1.GetETUser(tmpETID, "Name");
                            rValue[1] += rValue[1].Trim() == "" ? tmpETID : ";;" + tmpETID;
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        return rValue;
    }
    #endregion
    private void GenerateDropDownList()
    {
        #region DropDownList value
        pubDDLUID = "";
        pubDDLUNAME = "";
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string serviceGuild1 = ssUserGuild == "ge-50702" ? "OR ISNULL(ServiceSubstitute,'')= 'Y'" : "";
        string tmpSql = " select * from TCGESWC.dbo.ETUsers Where((ISNULL(GuildSubstitute,'') = '" + ssUserGuild + "' and GuildTcgeChk = '1')" + serviceGuild1 + " ) AND STATUS = '已開通'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = tmpSql;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tETID = readerTslm["ETID"] + "";
                            string tETName = readerTslm["ETName"] + "";

                            pubDDLUID += tETID + ";;";
                            pubDDLUNAME += tETName + ";;";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        #endregion
    }

    protected void RowInfo_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("RSWC00");
        string tmpLink = "../SWCDOC/SWC003.aspx?SWCNO=" + hiddenDate01.Value; ;
        Response.Write("<script>window.open('" + tmpLink + "','_blank')</script>");
    }

    protected void RowInfo2_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("RSWC00");
        string tmpLink = "../SWCDOC/SWC003.aspx?SWCNO=" + hiddenDate01.Value; ;
        Response.Write("<script>window.open('" + tmpLink + "','_blank')</script>");

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                break;
            case DataControlRowType.DataRow:
                HiddenField HDFD1 = (HiddenField)e.Row.Cells[0].FindControl("DDLValue");
                DropDownList DDLList1 = (DropDownList)e.Row.Cells[5].FindControl("DropDownList1");
                DropDownList DDLList2 = (DropDownList)e.Row.Cells[5].FindControl("DropDownList2");
                string[] aID = pubDDLUID.Split(new string[] { ";;" }, StringSplitOptions.None);
                string[] aName = pubDDLUNAME.Split(new string[] { ";;" }, StringSplitOptions.None);
                string[] aValue = (HDFD1.Value+";;;;").Split(new string[] { ";;" }, StringSplitOptions.None);

                DDLList1.Items.Clear(); DDLList2.Items.Clear();
                DDLList1.Items.Add(""); DDLList2.Items.Add("");
                for (int te = 0; te < aID.Length; te++)
                {
                    if (aID[te].Trim() != "") {
                        DDLList1.Items.Add(new ListItem(aName[te].ToString(), aID[te].ToString()));
                        DDLList2.Items.Add(new ListItem(aName[te].ToString(), aID[te].ToString())); }
                }
                if (aValue[0].Trim() != "") DDLList1.Text = aValue[0];
                if (aValue[1].Trim() != "") DDLList2.Text = aValue[1];
				
				HiddenField HFHour = (HiddenField)e.Row.Cells[0].FindControl("HFHour");
				HiddenField HFHour_2 = (HiddenField)e.Row.Cells[0].FindControl("HFHour_2");
				
				HiddenField HFMinute = (HiddenField)e.Row.Cells[0].FindControl("HFMinute");
				HiddenField HFMinute_2 = (HiddenField)e.Row.Cells[0].FindControl("HFMinute_2");
				
				TextBox SWC114_NEW = (TextBox)e.Row.Cells[0].FindControl("SWC114_NEW");
				TextBox SWC114_2NEW = (TextBox)e.Row.Cells[0].FindControl("SWC114_2NEW");
				HiddenField HFDate = (HiddenField)e.Row.Cells[0].FindControl("HFDate");
				HiddenField HFDate_2 = (HiddenField)e.Row.Cells[0].FindControl("HFDate_2");
				SWC114_NEW.Text = HFDate.Value + "T" + HFHour.Value + ":" + HFMinute.Value;
				SWC114_2NEW.Text = HFDate_2.Value + "T" + HFHour_2.Value + ":" + HFMinute_2.Value;
                break;
        }

    }

    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:
                break;
            case DataControlRowType.DataRow:
                HiddenField HDFD1 = (HiddenField)e.Row.Cells[0].FindControl("DDLValue");
                DropDownList DDLList1 = (DropDownList)e.Row.Cells[5].FindControl("DropDownList1");
                DropDownList DDLList2 = (DropDownList)e.Row.Cells[5].FindControl("DropDownList2");
                string[] aID = pubDDLUID.Split(new string[] { ";;" }, StringSplitOptions.None);
                string[] aName = pubDDLUNAME.Split(new string[] { ";;" }, StringSplitOptions.None);
                string[] aValue = (HDFD1.Value + ";;;;").Split(new string[] { ";;" }, StringSplitOptions.None);

                DDLList1.Items.Clear(); DDLList2.Items.Clear();
                DDLList1.Items.Add(""); DDLList2.Items.Add("");
                for (int te = 0; te < aID.Length; te++)
                {
                    if (aID[te].Trim() != "")
                    {
                        DDLList1.Items.Add(new ListItem(aName[te].ToString(), aID[te].ToString()));
                        DDLList2.Items.Add(new ListItem(aName[te].ToString(), aID[te].ToString()));
                    }
                }
                if (aValue[0].Trim() != "") DDLList1.Text = aValue[0];
                if (aValue[1].Trim() != "") DDLList2.Text = aValue[1];
				
				HiddenField HFHour_SWC115 = (HiddenField)e.Row.Cells[0].FindControl("HFHour_SWC115");
				HiddenField HFHour_SWC115_2 = (HiddenField)e.Row.Cells[0].FindControl("HFHour_SWC115_2");
				
				HiddenField HFMinute_SWC115 = (HiddenField)e.Row.Cells[0].FindControl("HFMinute_SWC115");
				HiddenField HFMinute_SWC115_2 = (HiddenField)e.Row.Cells[0].FindControl("HFMinute_SWC115_2");
				
				TextBox SWC115_NEW = (TextBox)e.Row.Cells[0].FindControl("SWC115_NEW");
				TextBox SWC115_2NEW = (TextBox)e.Row.Cells[0].FindControl("SWC115_2NEW");
				HiddenField HFDate_SWC115 = (HiddenField)e.Row.Cells[0].FindControl("HFDate_SWC115");
				HiddenField HFDate_SWC115_2 = (HiddenField)e.Row.Cells[0].FindControl("HFDate_SWC115_2");
				SWC115_NEW.Text = HFDate_SWC115.Value + "T" + HFHour_SWC115.Value + ":" + HFMinute_SWC115.Value;
				SWC115_2NEW.Text = HFDate_SWC115_2.Value + "T" + HFHour_SWC115_2.Value + ":" + HFMinute_SWC115_2.Value;
                break;
        }
    }

    protected void SaveRow_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("RSWC00");
        string tmpSWC000 = hiddenDate01.Value;
        DropDownList DDL01 = (DropDownList)GridView2.Rows[aaa].Cells[5].FindControl("DropDownList1");
        DropDownList DDL02 = (DropDownList)GridView2.Rows[aaa].Cells[5].FindControl("DropDownList2");
        HiddenField hiddenDate02 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("HDSWC045ID");
        HiddenField hiddenDate03 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("HDSWC024ID");
        HiddenField hiddenDate04 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("HDSWC025");
        HiddenField hiddenDate05 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("HDSWC108");
        HiddenField hiddenDate06 = (HiddenField)GridView2.Rows[aaa].Cells[7].FindControl("HDSWC013TEL");        
        TextBox TB01 = (TextBox)GridView2.Rows[aaa].Cells[6].FindControl("SWC114_NEW");
        TextBox TB01_2 = (TextBox)GridView2.Rows[aaa].Cells[6].FindControl("SWC114_2NEW");
		CheckBox CBOX01 = (CheckBox)GridView2.Rows[aaa].Cells[6].FindControl("CheckBox1");
        TextBox TBSWC121 = (TextBox)GridView2.Rows[aaa].Cells[3].FindControl("TBSWC121");

        string tmpSWC002 = GridView2.Rows[aaa].Cells[0].Text;
        string tmpSWC005 = GridView2.Rows[aaa].Cells[2].Text;
        string tmpSWC012 = GridView2.Rows[aaa].Cells[1].Text;
        string tmpSWC013TEL = hiddenDate06.Value;
        string tmpSWC024ID = hiddenDate03.Value;
        string tmpSWC025 = hiddenDate04.Value;
        string tmpSWC045ID = hiddenDate02.Value;
        string tmpSWC114 = TB01.Text.Replace("T"," ");
        string tmpSWC114_2 = TB01_2.Text.Replace("T"," ");
        string tmpSWC108 = hiddenDate05.Value;
        string tmpEtU01 = DDL01.SelectedItem.Value;
        string tmpEtU02 = DDL02.SelectedItem.Value;
		string tmpSWC121 = TBSWC121.Text;

        string updSql = " update tslm2.dbo.swcswc set SWC114=@SWC114,SWC114_2=@SWC114_2,SWC121=@SWC121 where SWC00=@SWC000; ";
        updSql += " update tcgeswc.dbo.swccase set  SWC114=@SWC114,SWC114_2=@SWC114_2,SWC121=@SWC121 where SWC000=@SWC000; ";
        updSql += " Delete tcgeswc.dbo.GuildGroup Where SWC000=@SWC000 and RGSID in ('11','12'); ";
        if (tmpEtU01.Trim() != "") updSql += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES (@SWC000,@SWC002,'11',@ETUSER1,'S4','1','" + ssUserID + "',getdate());";
        if (tmpEtU02.Trim() != "") updSql += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES (@SWC000,@SWC002,'12',@ETUSER2,'S4','1','" + ssUserID + "',getdate());";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = updSql;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC002", tmpSWC002));
				if(tmpSWC114 != "")
					cmd.Parameters.Add(new SqlParameter("@SWC114", tmpSWC114));
                else
					cmd.Parameters.Add(new SqlParameter("@SWC114", DBNull.Value));
				if(tmpSWC114_2 != "")
					cmd.Parameters.Add(new SqlParameter("@SWC114_2", tmpSWC114_2));
                else
					cmd.Parameters.Add(new SqlParameter("@SWC114_2", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ETUSER1", tmpEtU01));
                cmd.Parameters.Add(new SqlParameter("@ETUSER2", tmpEtU02));
                cmd.Parameters.Add(new SqlParameter("@SWC121", tmpSWC121));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        if (CBOX01.Checked)
        {
            string tMailSub = "", tMailText="", SentMailGroup = "";
            string tGuildName = ssUserName;
            //若狀態為「施工中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「施工檢查時間」
            //股長、管理者、承辦人員、監造技師、檢查委員(2位)、公會、義務人

            tMailSub = "提醒您，【" + tGuildName + "】已訂於 " + tmpSWC114 + " ~ " + tmpSWC114_2 + " 進行【" + tmpSWC005 + "】施工監督檢查。";
            tMailText = "提醒您，【" + tGuildName + "】已訂於 " + tmpSWC114 + " ~ " + tmpSWC114_2 + " 進行【" + tmpSWC005 + "】施工監督檢查。<br><br>";
            tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br>";
            tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";

            string exeSQLSTR = " select ETEmail as EMAIL from GuildGroup G Left Join ETUsers E On G.ETID = E.ETID where G.swc000 = '" + tmpSWC000 + "' and((ETEmail is not null and RGType = 'S4') or e.etid = '" + tmpSWC045ID + "') ";
            exeSQLSTR += "UNION ALL select email as EMAIL from tslm2.dbo.geouser g where ((department = '審查管理科' and ((jobtitle = '股長' and Tcgearea01 like '%" + tmpSWC012 + "%') or mbgroup02 = '系統管理員' or[name] = '" + tmpSWC025 + "')) or(userid = '" + tmpSWC024ID + "')) and status <> '停用' ";

            #region.名單
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                SqlDataReader readerItemS;
                SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR, SwcConn);
                readerItemS = objCmdItemS.ExecuteReader();

                while (readerItemS.Read())
                {
                    string tEMail = readerItemS["EMAIL"] + "";
                    SentMailGroup += ";;" + tEMail;
                }
                readerItemS.Close();
                objCmdItemS.Dispose();
            }
            SentMailGroup += ";;" + tmpSWC108;
            string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
            #endregion

            bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub, tMailText);
            SBApp.SendSMS(tmpSWC013TEL, tMailSub);
        }
		Response.Redirect("FPage002.aspx");
    }
    protected void SaveRow2_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        Button LButton = (Button)sender;
        int aaa = Convert.ToInt32(LButton.CommandArgument);
        HiddenField hiddenDate01 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("RSWC00");
        string tmpSWC000 = hiddenDate01.Value;
        DropDownList DDL01 = (DropDownList)GridView3.Rows[aaa].Cells[5].FindControl("DropDownList1");
        DropDownList DDL02 = (DropDownList)GridView3.Rows[aaa].Cells[5].FindControl("DropDownList2");
        HiddenField hiddenDate02 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("HDSWC045ID");
        HiddenField hiddenDate03 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("HDSWC024ID");
        HiddenField hiddenDate04 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("HDSWC025");
        HiddenField hiddenDate05 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("HDSWC108");
        HiddenField hiddenDate06 = (HiddenField)GridView3.Rows[aaa].Cells[7].FindControl("HDSWC013TEL");
        TextBox TB01 = (TextBox)GridView3.Rows[aaa].Cells[6].FindControl("SWC115_NEW");
        TextBox TB01_2 = (TextBox)GridView3.Rows[aaa].Cells[6].FindControl("SWC115_2NEW");
        CheckBox CBOX01 = (CheckBox)GridView3.Rows[aaa].Cells[6].FindControl("CheckBox1");
        TextBox TBSWC122 = (TextBox)GridView3.Rows[aaa].Cells[3].FindControl("TBSWC122");

        string tmpSWC002 = GridView3.Rows[aaa].Cells[0].Text;
        string tmpSWC005 = GridView3.Rows[aaa].Cells[2].Text;
        string tmpSWC012 = GridView3.Rows[aaa].Cells[1].Text;
        string tmpSWC013TEL = hiddenDate06.Value;
        string tmpSWC024ID = hiddenDate03.Value;
        string tmpSWC025 = hiddenDate04.Value;
        string tmpSWC045ID = hiddenDate02.Value;
        string tmpSWC115 = TB01.Text.Replace("T"," ");
		string tmpSWC115_2 = TB01_2.Text.Replace("T"," ");
        string tmpSWC108 = hiddenDate05.Value;
        string tmpEtU01 = DDL01.SelectedItem.Value;
        string tmpEtU02 = DDL02.SelectedItem.Value;
		string tmpSWC122 = TBSWC122.Text;

        string updSql = " update tslm2.dbo.swcswc set SWC115=@SWC115,SWC115_2=@SWC115_2,SWC122=@SWC122 where SWC00=@SWC000; ";
        updSql += " update tcgeswc.dbo.swccase set  SWC115=@SWC115,SWC115_2=@SWC115_2,SWC122=@SWC122 where SWC000=@SWC000; ";
        updSql += " Delete tcgeswc.dbo.GuildGroup Where SWC000=@SWC000 and RGSID in ('11','12'); ";
        if (tmpEtU01.Trim() != "") updSql += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES (@SWC000,@SWC002,'11',@ETUSER1,'S4','1','" + ssUserID + "',getdate());";
        if (tmpEtU02.Trim() != "") updSql += " INSERT INTO [GuildGroup] ([SWC000],[SWC002],[RGSID],[ETID],[RGType],[CHGType],[Saveuser],[savedate]) VALUES (@SWC000,@SWC002,'12',@ETUSER2,'S4','1','" + ssUserID + "',getdate());";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = updSql;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", tmpSWC000));
                cmd.Parameters.Add(new SqlParameter("@SWC002", tmpSWC002));
				if(tmpSWC115 != "")
					cmd.Parameters.Add(new SqlParameter("@SWC115", tmpSWC115));
                else
					cmd.Parameters.Add(new SqlParameter("@SWC115", DBNull.Value));
				if(tmpSWC115_2 != "")
					cmd.Parameters.Add(new SqlParameter("@SWC115_2", tmpSWC115_2));
                else
					cmd.Parameters.Add(new SqlParameter("@SWC115_2", DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@ETUSER1", tmpEtU01));
                cmd.Parameters.Add(new SqlParameter("@ETUSER2", tmpEtU02));
				cmd.Parameters.Add(new SqlParameter("@SWC122", tmpSWC122));
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        if (CBOX01.Checked)
        {
            string tMailSub = "", tMailText = "", SentMailGroup = "";
            string tGuildName = ssUserName;
            //若狀態為「施工中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「施工檢查時間」
            //股長、管理者、承辦人員、監造技師、檢查委員(2位)、公會、義務人
            SBApp.SendSMS(tmpSWC013TEL, tMailSub);

            tMailSub = "提醒您，【" + tGuildName + "】已訂於 " + tmpSWC115 + " ~ " + tmpSWC115_2 + " 舉辦【" + tmpSWC005 + "】完工檢查。";
            tMailText = "提醒您，【" + tGuildName + "】已訂於 " + tmpSWC115 + " ~ " + tmpSWC115_2 + " 舉辦【" + tmpSWC005 + "】完工檢查。<br><br>";
            tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br>";
            tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
            //若狀態為「施工中」，檢核是否勾選「存檔後發信通知委員」，若有則發信通知「完工檢查時間」
            //股長、管理者、承辦人員、監造技師、檢查委員、公會、義務人

            string exeSQLSTR = " select ETEmail as EMAIL from GuildGroup G Left Join ETUsers E On G.ETID = E.ETID where G.swc000 = '" + tmpSWC000 + "' and((ETEmail is not null and RGType = 'S4') or e.etid = '" + tmpSWC045ID + "') ";
            exeSQLSTR += "UNION ALL select email as EMAIL from tslm2.dbo.geouser g where ((department = '審查管理科' and ((jobtitle = '股長' and Tcgearea01 like '%" + tmpSWC012 + "%') or mbgroup02 = '系統管理員' or[name] = '" + tmpSWC025 + "')) or(userid = '" + tmpSWC024ID + "')) and status <> '停用' ";

            #region.名單
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                SqlDataReader readerItemS;
                SqlCommand objCmdItemS = new SqlCommand(exeSQLSTR, SwcConn);
                readerItemS = objCmdItemS.ExecuteReader();

                while (readerItemS.Read())
                {
                    string tEMail = readerItemS["EMAIL"] + "";
                    SentMailGroup += ";;" + tEMail;
                }
                readerItemS.Close();
                objCmdItemS.Dispose();
            }
            SentMailGroup += ";;" + tmpSWC108;
            string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
            #endregion
            bool MailTo01 = SBApp.Mail_Send(arraySentMail01, tMailSub, tMailText);
            SBApp.SendSMS(tmpSWC013TEL, tMailSub);
        }
    }
    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        getGVDate();
    }
    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        TBQ001.Text = "";
        DDLQ002.Text = "";
        TBQ003.Text = "";
        TBQ004a.Text = "";
        TBQ004b.Text = "";
        TBQ005a.Text = "";
        TBQ005b.Text = "";
        TBQ006.Text = "";
        RemoveSelSession();
        getGVDate();
    }

    private void RemoveSelSession()
    {
        Session["PageSearch001"] = "";
        Session["PageSearch002"] = "";
        Session["PageSearch003"] = "";
        Session["PageSearch004a"] = "";
        Session["PageSearch004b"] = "";
        Session["PageSearch005a"] = "";
        Session["PageSearch005b"] = "";
        Session["PageSearch006"] = "";
    }

    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
    {
        GetEtuserList();
        string sPage = e.SortExpression;
        string tmpSort = ViewState["SortFP0022" + sPage] + "";
        string thisSort = "";
        switch (tmpSort)
        {
            case "":
            case "Asc":
                thisSort = "Desc"; ViewState["SortFP0022" + sPage] = thisSort;
                break;
            case "Desc":
                thisSort = "Asc"; ViewState["SortFP0022" + sPage] = thisSort;
                break;
        }
        DataTable OJB_GV = (DataTable)ViewState["S7GVGS22"];
        OJB_GV.DefaultView.Sort = sPage + " " + thisSort;
        GridView2.DataSource = OJB_GV;
        GridView2.DataBind();
    }

    private void GetEtuserList()
    {
        pubDDLUID = "";
        pubDDLUNAME = "";
        string ssUserGuild = Session["ETU_Guild01"] + "";
        string serviceGuild1 = ssUserGuild == "ge-50702" ? "OR ISNULL(ServiceSubstitute,'')= 'Y'" : "";
        string tmpSql = " select * from TCGESWC.dbo.ETUsers Where((ISNULL(GuildSubstitute,'') = '" + ssUserGuild + "' and GuildTcgeChk = '1')" + serviceGuild1 + " ) AND STATUS = '已開通'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = tmpSql;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tETID = readerTslm["ETID"] + "";
                            string tETName = readerTslm["ETName"] + "";

                            pubDDLUID += tETID + ";;";
                            pubDDLUNAME += tETName + ";;";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
    }

    protected void GridView3_Sorting(object sender, GridViewSortEventArgs e)
    {
        GetEtuserList();
        string sPage = e.SortExpression;
        string tmpSort = ViewState["SortFP0023" + sPage] + "";
        string thisSort = "";
        switch (tmpSort)
        {
            case "":
            case "Asc":
                thisSort = "Desc"; ViewState["SortFP0023" + sPage] = thisSort;
                break;
            case "Desc":
                thisSort = "Asc"; ViewState["SortFP0023" + sPage] = thisSort;
                break;
        }
        DataTable OJB_GV = (DataTable)ViewState["S7GVGS23"];
        OJB_GV.DefaultView.Sort = sPage + " " + thisSort;
        GridView3.DataSource = OJB_GV;
        GridView3.DataBind();
    }
}