using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class DouCode_SwcDtl03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        if (ssUserName == "")
        {
            Response.Redirect("GeoLogin.aspx");
        }
        if (!IsPostBack)
        {
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
        string Sql03Str = "";

        Sql03Str = " select D3.DTLC000,CONVERT(varchar(100), D3.DTLC002, 111) AS DTLC002 ,SWC.SWC005,SWC.SWC013,SWC.SWC021,SWC.SWC000 from SWCDTL03 D3 LEFT JOIN SWCCASE SWC ON SWC.SWC000 = D3.SWC000 ORDER BY D3.DTLC000 desc ";
                
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection DTLConn = new SqlConnection(connectionString.ConnectionString))
        {
            DTLConn.Open();

            SqlDataReader readerItem03;
            SqlCommand objCmdItem03 = new SqlCommand(Sql03Str, DTLConn);
            readerItem03 = objCmdItem03.ExecuteReader();

            int ni = 0;
            while (readerItem03.Read())
            {
                string dDTLC01 = readerItem03["DTLC000"] + "";
                string dDTLC02 = readerItem03["DTLC002"] + "";
                string dDTLC03 = readerItem03["SWC005"] + "";
                string dDTLC04 = readerItem03["SWC013"] + "";
                string dDTLC05 = readerItem03["SWC021"] + "";
                string dDTLC06 = readerItem03["SWC000"] + "";

                DataTable OBJ_GV03 = (DataTable)ViewState["GV03"];
                DataTable DTGV03 = new DataTable();

                if (OBJ_GV03 == null)
                {
                    DTGV03.Columns.Add(new DataColumn("DTLC001", typeof(string)));
                    DTGV03.Columns.Add(new DataColumn("DTLC002", typeof(string)));
                    DTGV03.Columns.Add(new DataColumn("DTLC003", typeof(string)));
                    DTGV03.Columns.Add(new DataColumn("DTLC004", typeof(string)));
                    DTGV03.Columns.Add(new DataColumn("DTLC005", typeof(string)));
                    DTGV03.Columns.Add(new DataColumn("DTLC006", typeof(string)));
                    DTGV03.Columns.Add(new DataColumn("DTLC007", typeof(string)));

                    ViewState["GV03"] = DTGV03;
                    OBJ_GV03 = DTGV03;
                }
                DataRow dr03 = OBJ_GV03.NewRow();

                dr03["DTLC001"] = dDTLC01;
                dr03["DTLC002"] = dDTLC02;
                dr03["DTLC003"] = dDTLC03;
                dr03["DTLC004"] = dDTLC04;
                dr03["DTLC005"] = dDTLC05;
                dr03["DTLC006"] = dDTLC06;
                dr03["DTLC007"] = ni++;

                OBJ_GV03.Rows.Add(dr03);

                ViewState["GV03"] = OBJ_GV03;

                SWCDTL03.DataSource = OBJ_GV03;
                SWCDTL03.DataBind();


            }
        }
    }


    protected void ButtonDTL03_Click(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["CaseId"] + "";
        Button LButton = (Button)sender;

        string LINK = "SWCDT003.aspx?SWCNO=" + rCaseId + "&DTLNO=" + LButton.CommandArgument;
        Response.Redirect(LINK);
    }
    protected void SWCDTL03_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string jAct = e.CommandName;

        switch (jAct)
        {
            case "EditPage":
                int aa = SWCDTL03.Rows[Convert.ToInt32(e.CommandArgument) % 20].RowIndex;
                string jKeyValue1 = SWCDTL03.Rows[aa].Cells[0].Text;
                string jKeyValue2 = SWCDTL03.Rows[aa].Cells[5].Text;

                string LINK = "SWCDT003.aspx?SWCNO=" + jKeyValue2 + "&DTLNO=" + jKeyValue1;
                Response.Redirect(LINK);

                break;
        }
    }
    
    protected void SWCDTL03_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //要隱藏的欄位    
            e.Row.Cells[5].Visible = false;
        }
        //SWCDTL03.Columns[6].Visible = false;
    }

    protected void BtnLogOut_Click(object sender, EventArgs e)
    {
        string LINK = "GeoLogin.aspx?Act=Out";
        Response.Redirect(LINK);
    }
}