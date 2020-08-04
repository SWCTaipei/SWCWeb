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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class DouCode_SwcDtl07 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        if (ssUserName == "")
        {
            Response.Redirect("GeoLogin.aspx");
        }
        if (!IsPostBack) { 
            ShowList();
        }

        //全區供用        
        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }
        //全區供用
    }

    private void ShowList()
    {
        string Sql07Str = "";

        Sql07Str = " select D7.DTLG000,SWC.SWC002,CONVERT(varchar(100), D7.DTLG002, 111) AS DTLG002 ,SWC.SWC005,SWC.SWC013,SWC.SWC000 from SWCDTL07 D7 LEFT JOIN SWCCASE SWC ON SWC.SWC000=D7.SWC000 ORDER BY D7.DTLG000 desc ";
        
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            SqlDataReader readerItem07;
            SqlCommand objCmdItem07 = new SqlCommand(Sql07Str, DTLConn);
            readerItem07 = objCmdItem07.ExecuteReader();

            int nI = 0;
            while (readerItem07.Read())
            {
                string dDTLG01 = readerItem07["DTLG000"] + "";
                string dDTLG02 = readerItem07["SWC002"] + "";
                string dDTLG03 = readerItem07["DTLG002"] + "";
                string dDTLG04 = readerItem07["SWC005"] + "";
                string dDTLG05 = readerItem07["SWC013"] + "";
                string dDTLG06 = readerItem07["SWC000"] + "";

                DataTable OBJ_GV07 = (DataTable)ViewState["GV07"];
                DataTable DTGV07 = new DataTable();

                if (OBJ_GV07 == null)
                {
                    DTGV07.Columns.Add(new DataColumn("DTLG001", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG002", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG003", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG004", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG005", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG006", typeof(string)));
                    DTGV07.Columns.Add(new DataColumn("DTLG007", typeof(string)));

                    ViewState["GV07"] = DTGV07;
                    OBJ_GV07 = DTGV07;
                }
                DataRow dr07 = OBJ_GV07.NewRow();

                dr07["DTLG001"] = dDTLG01;
                dr07["DTLG002"] = dDTLG02;
                dr07["DTLG003"] = dDTLG03;
                dr07["DTLG004"] = dDTLG04;
                dr07["DTLG005"] = dDTLG05;
                dr07["DTLG006"] = dDTLG06;
                dr07["DTLG007"] = nI++;

                OBJ_GV07.Rows.Add(dr07);

                ViewState["GV07"] = OBJ_GV07;

                SWCDTL07.DataSource = OBJ_GV07;
                SWCDTL07.DataBind();
            }
        }

    }

    protected void SWCDTL07_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                //HiddenField Lock07 = (HiddenField)e.Row.Cells[2].FindControl("Lock07");
                //string tempLock = Lock07.Value;

                //if (tempLock == "Y")
                //{
                //    Button btn = (Button)e.Row.Cells[1].FindControl("ButtonDEL07");

                //    btn.Text = "搞啥";
                //    btn.Visible = false;
                //}
                break;
        }

    }
    protected void ButtonDEL07_Click(object sender, EventArgs e)
    {
        //Button LButton = (Button)sender;

        //string tSwc000 = LBSWC000.Text + "";
        //string tDtl000 = LButton.CommandArgument + "";

        //string exeSqlStr = " delete SWCDTL07 where swc000='" + tSwc000 + "' and dtlG000 ='" + tDtl000 + "' ";

        //ConnectionStringSettings connectionString03 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn03 = new SqlConnection(connectionString03.ConnectionString))
        //{
        //    SwcConn03.Open();

        //    SqlDataReader readeSwc03;
        //    SqlCommand objCmdSwc03 = new SqlCommand(exeSqlStr, SwcConn03);
        //    readeSwc03 = objCmdSwc03.ExecuteReader();
        //}
        //Response.Write("<script>alert('資料已刪除'); location.href='SWC002.aspx?SWCNO=" + tSwc000 + "'; </script>");

    }
    protected void SWCDTL07_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SWCDTL07.PageIndex = e.NewPageIndex;
    }
    protected void SWCDTL07_PageIndexChanged(object sender, EventArgs e)
    {
        SWCDTL07.DataSource = ViewState["GV07"];
        SWCDTL07.DataBind();

    }

    protected void SWCDTL07_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            e.Row.Cells[5].Visible = false;
        }
        //SWCDTL03.Columns[6].Visible = false;
    }

    protected void SWCDTL07_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string jAct = e.CommandName;

        switch (jAct)
        {
            case "EditPage":
                int aa = SWCDTL07.Rows[Convert.ToInt32(e.CommandArgument) % 20].RowIndex;
                string jKeyValue1 = SWCDTL07.Rows[aa].Cells[0].Text;
                string jKeyValue2 = SWCDTL07.Rows[aa].Cells[5].Text;
                
                string LINK = "SWCDT007.aspx?SWCNO=" + jKeyValue2 + "&DTLNO=" + jKeyValue1;
                Response.Redirect(LINK);

                break;


        }
    }
    protected void BtnLogOut_Click(object sender, EventArgs e)
    {
        string LINK = "GeoLogin.aspx?Act=Out";
        Response.Redirect(LINK);
    }

    protected void ExeQSel_Click(object sender, EventArgs e)
    {
        RemoveSelSession();

        string s001 = Sh001.Text + "";
        string s002 = Sh002.Text + "";
        string s003 = Sh003.Text + "";



    }

    private void RemoveSelSession()
    {
        throw new NotImplementedException();
    }

    protected void RemoveSel_Click(object sender, EventArgs e)
    {
        Session["PageSearch001"] = "";
        Session["PageSearch002"] = "";

    }
}