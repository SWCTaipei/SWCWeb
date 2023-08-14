using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

public partial class SWCTCGOV_SWCGOV001 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserType = Session["UserType"] + "";
        string ssUserID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        string rqSearch = Request["SR"] + "";

        GBClass001 SBApp = new GBClass001();

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
                if (!IsPostBack) { GenerateDropDownList(); }
                break;
            case "04":
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
            default:
                Response.Redirect("../SWCDOC/SWC000.aspx");
                break;
        }
        

        //全區供用
        SBApp.ViewRecord("書件目錄查詢", "view", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
    }

    private void GenerateDropDownList()
    {
        //通知事件
        string[,] array_District1 = new string[,] { { "0104", "颱風豪雨通知回傳自主檢查表" }, { "0203", "違規案件防災整備通知" } };
													   
        for (int te = 0; te < array_District1.GetLength(0); te++)
        {
            DDLDETYPE.Items.Add(new ListItem(array_District1[te, 1], array_District1[te, 0]));
							 
        }
													 

        //案件狀態
        //string[] array_DropDownList1 = new string[] { "退補件", "不予受理", "受理中", "審查中", "暫停審查", "撤銷", "不予核定", "已核定", "施工中", "停工中", "已完工", "廢止", "失效", "已變更" };
        //DDLSWCSTATUS.DataSource = array_DropDownList1;
        //DDLSWCSTATUS.DataBind();
        //DDLSWCSTATUS.SelectedValue = "施工中";

        //案件狀態
        string[] array_DropDownList2 = new string[] { "義務人", "承辦技師", "審查單位", "監造技師", "檢查單位", "受處分人" };
        CHKSENDMBR.DataSource = array_DropDownList2;
        CHKSENDMBR.DataBind();

        //案件狀態
        string[] array_DropDownList3 = new string[] { "簡訊", "E-mail" };
        CHKSENDFUN.DataSource = array_DropDownList3;
        CHKSENDFUN.DataBind();
        
    }

    protected void NewSwc_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWCGOV002.aspx?DisEventId=ADDNEW");
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";
        
        Response.Redirect("SWCGOV002.aspx?DisEventId="+ tDENo);
    }

    protected void GVSWCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                string gvDENo = Convert.ToString(e.Row.Cells[0].Text);

                HiddenField hiddenDate01 = (HiddenField)e.Row.Cells[6].FindControl("HiddenField1");

                string gvHDData01 = hiddenDate01.Value;

                Button btnView = (Button)e.Row.Cells[0].FindControl("BtnView");
                Button btnEdit = (Button)e.Row.Cells[0].FindControl("BtnEdit");
                Button btnDelete = (Button)e.Row.Cells[0].FindControl("BtnDelete");

                if (gvHDData01 == "Y")
                {
                    btnView.Visible = true;
                }
                else
                {
                    btnEdit.Visible = true;
                    btnDelete.Visible = true;
                }
                break;
        }

    }

    protected void BtnView_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";

        Response.Redirect("SWCGOV003.aspx?DisEventId=" + tDENo);
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        RemoveSelSession();

        string shDETYPE = DDLDETYPE.SelectedValue + "";
        //string shSWCSTATUS = DDLSWCSTATUS.SelectedValue + "";
        string shSWCSTATUS = "";
        string shSENDMBR = "";
        string shDENAME = TXTDENAME.Text;
        string shDENTTEXT = TXTDENTTEXT.Text;
        string shDEDATE1 = TXTDATE1.Text;
        string shDEDATE2 = TXTDATE2.Text;
        string shSENDFUN = "";
		
		for (int it = 0; it <= CHKSWCSTATUS.Items.Count - 1; it++)
        {
            if (CHKSWCSTATUS.Items[it].Selected)
            {
                shSWCSTATUS += CHKSWCSTATUS.Items[it].Value + ",";
            }
        }
        for (int it = 0; it <= CHKSENDMBR.Items.Count - 1; it++)
        {
            if (CHKSENDMBR.Items[it].Selected)
            {
                shSENDMBR += CHKSENDMBR.Items[it].Value + ",";
            }
        }
        for (int it = 0; it <= CHKSENDFUN.Items.Count - 1; it++)
        {
            if (CHKSENDFUN.Items[it].Selected)
            {
                shSENDFUN += CHKSENDFUN.Items[it].Value + ",";
            }
        }
        
        string SearchSqlStr = " select *,case DETYPE when '0104' then '颱風豪雨通知回傳自主檢查表' when '0203' then '違規案件防災整備通知' end DETYPE1 from DisasterEvent ";
        SearchSqlStr += " where 1=1 ";
        if (shDETYPE != "" ) { SearchSqlStr += " and DETYPE = '"+ shDETYPE + "' "; }
        if (shSWCSTATUS != "")
		{ 
			for (int it = 0; it <= CHKSWCSTATUS.Items.Count - 1; it++)
            {
                if (CHKSWCSTATUS.Items[it].Selected)
                {
                    string tmpValue = CHKSWCSTATUS.Items[it].Value;
                    SearchSqlStr += " and (SWCSTATUS like '%" + tmpValue + "%' or ILGSTATUS like '%" + tmpValue + "%') ";
                }
            }
		}
        if (shSENDMBR != "")
        {
            for (int it = 0; it <= CHKSENDMBR.Items.Count - 1; it++)
            {
                if (CHKSENDMBR.Items[it].Selected)
                {
                    string tmpValue = CHKSENDMBR.Items[it].Value;
                    SearchSqlStr += " and (SENDMBR like '%" + tmpValue + "%' or ILGSENDMBR like '%" + tmpValue + "%') ";
                }
            }
        }
        if (shDENAME != "") { SearchSqlStr += " and DENAME like '%" + shDENAME + "%' "; }
        if (shDENTTEXT != "") { SearchSqlStr += " and DENTTEXT like '%" + shDENTTEXT + "%' "; }
        if (shDEDATE1 != "") { SearchSqlStr += " and DEDATE >= '" + shDEDATE1 + "' "; }
        if (shDEDATE2 != "") { SearchSqlStr += " and DEDATE <= '" + shDEDATE2 + "' "; }
        if (shSENDFUN != "")
        {
            for (int it = 0; it <= CHKSENDFUN.Items.Count - 1; it++)
            {
                if (CHKSENDFUN.Items[it].Selected)
                {
                    string tmpValue = CHKSENDFUN.Items[it].Value;
                    SearchSqlStr += " and (SENDFUN like '%" + tmpValue + "%' or ILGSENDFUN like '%" + tmpValue + "%') ";
                }
            }
        }
        SearchSqlStr += " ORDER BY [DENO] DESC ";

        SqlDataSource.SelectCommand = SearchSqlStr;

        //回上一頁用
        Session["DETYPE"] = shDETYPE;
        Session["SWCSTATUS"] = shSWCSTATUS;
        Session["SENDMBR"] = shSENDMBR;
        Session["DENAME"] = shDENAME;
        Session["DENTTEXT"] = shDENTTEXT;
        Session["DEDATE1"] = shDEDATE1;
        Session["DEDATE2"] = shDEDATE2;
        Session["SENDMBR"] = shSENDMBR;

    }
    private void RemoveSelSession()
    {
        Session["DETYPE"] = "";
        Session["SWCSTATUS"] = "";
        Session["SENDMBR"] = "";
        Session["DENAME"] = "";
        Session["DENTTEXT"] = "";
        Session["DEDATE1"] = "";
        Session["DEDATE2"] = "";
        Session["SENDMBR"] = "";

    }
    protected void SqlDataSource_Selected(object sender, SqlDataSourceStatusEventArgs e)
    {
        CaseCount.Text = e.AffectedRows.ToString();
    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        RemoveSelSession();
        Response.Redirect("SWCGOV001.aspx");

    }

    protected void GVSWCList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVSWCList.PageIndex = e.NewPageIndex;
    }
	
	protected void BtnDelete_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDENo = LButton.CommandArgument + "";
		
		ConnectionStringSettings connectionStringSwc = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionStringSwc.ConnectionString))
		{
			SwcConn.Open();
			
			string gDNSQLStr = "delete DisasterEvent where DENo = @DENo;";
			using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = gDNSQLStr;
				#region.設定值
				cmd.Parameters.Add(new SqlParameter("@DENo", tDENo));
				#endregion
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
		
        Response.Redirect("SWCGOV001.aspx");
    }
}