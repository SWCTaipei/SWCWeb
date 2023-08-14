using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCRD_Dlist001 : System.Web.UI.Page
{
    int ChkOverDay = 6;
    protected void Page_Load(object sender, EventArgs e)
    {
        setGVDate();
    }

    private void setGVDate()
    {
        string ssUserID = Session["ID"] + "";
        string tmpSWC000Str = "";
        string tmpONA02 = " select DISTINCT SWC000 from OnlineApply02  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA03 = " select DISTINCT SWC000 from OnlineApply03  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA04 = " select DISTINCT SWC000 from OnlineApply04  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA05 = " select DISTINCT SWC000 from OnlineApply05  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA06 = " select DISTINCT SWC000 from OnlineApply06  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA07 = " select DISTINCT SWC000 from OnlineApply07  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA08 = " select DISTINCT SWC000 from OnlineApply08  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string tmpONA09 = " select DISTINCT SWC000 from OnlineApply09  where ISNULL(DATALOCK,'')<>'Y' and Saveuser=@USERID; ";
        string[] aApply = new string[] { tmpONA02, tmpONA03, tmpONA04, tmpONA05, tmpONA06, tmpONA07, tmpONA08, tmpONA09 };
        for (int i = 0; i < aApply.Length; i++) {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();
                using (var cmd = SwcConn.CreateCommand())
                {
                    cmd.CommandText = aApply[i];
                    cmd.Parameters.Add(new SqlParameter("@USERID", ssUserID));
                    cmd.ExecuteNonQuery();

                    using (SqlDataReader readerSwc = cmd.ExecuteReader())
                    {
                        if (readerSwc.HasRows)
                            while (readerSwc.Read())
                            {
                                string tSWC000 = readerSwc["SWC000"] + "";
                                if (tmpSWC000Str.IndexOf(tSWC000) < 0)
                                    tmpSWC000Str += tSWC000 + ";;";
                            }
                        readerSwc.Close();
                    }
                    cmd.Cancel();
                }
            }
        }
        string swcRange = tmpSWC000Str=="" ? "" : tmpSWC000Str.Substring(0, tmpSWC000Str.Length-2).Replace(";;","','"); swcRange = "'" + swcRange + "'";
        string tmpSwcswc = " select * from SWCCASE where swc000 in (" + swcRange + ") order by swc000 desc";
        SqlDataSource.SelectCommand = tmpSwcswc;
    }

    protected void BtnEdit_Click(object sender, EventArgs e)
    {
        Button LButton = (Button)sender;
        string tDtl000 = LButton.CommandArgument + "";
        string sLink = "../SWCDOC/SWC002.aspx?CaseId=" + tDtl000;
        Response.Write("<script>window.open('" + sLink + "','_blank')</script>");
    }

    protected void GVSWCList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string jAct = e.CommandName;

        switch (jAct)
        {
            case "detail":
                int aa = GVSWCList.Rows[Convert.ToInt32(e.CommandArgument)].RowIndex;
                string jKeyValue = GVSWCList.Rows[aa].Cells[1].Text;
                string sLink = "../SWCDOC/SWC003.aspx?SWCNO=" + jKeyValue;
                Response.Write("<script>window.open('" + sLink + "','_blank')</script>");
                break;
        }
    }

    protected void GVSWCList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string ShowEditBtn = "N";
        string ssUserID = Session["ID"] + "";
        string ssUserPW = Session["PW"] + "";
        string ssUserType = Session["UserType"] + "";

        switch (e.Row.RowType)
        {
            case DataControlRowType.Header:     //是表頭你想幹嘛？

                break;

            case DataControlRowType.DataRow:

                string tempCaseSwc000 = Convert.ToString(e.Row.Cells[1].Text);
                string tempCaseSwc004 = Convert.ToString(e.Row.Cells[3].Text);
                string tempCaseSwc024ID = "";

                ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
                using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                {
                    string strSQLRV = " select * from SWCCASE where SWC000='" + tempCaseSwc000 + "' ";

                    SwcConn.Open();

                    SqlDataReader readerSWC;
                    SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
                    readerSWC = objCmdSWC.ExecuteReader();

                    while (readerSWC.Read())
                    {
                        ShowEditBtn = "N";
                        string tempSWC004 = readerSWC["SWC004"] + "";
                        string tempSWC007 = readerSWC["SWC007"] + "";
                        string tempSWC021ID = readerSWC["SWC021ID"] + "";
                        string tempSWC022ID = readerSWC["SWC022ID"] + "";
                        string tempSWC024ID = readerSWC["SWC024ID"] + "";
                        string tempSWC045ID = readerSWC["SWC045ID"] + "";

                        tempCaseSwc024ID = tempSWC024ID;

                        switch (ssUserType.ToString())
                        {
                            case "01":
                                if (tempSWC007 == "簡易水保" && (tempSWC004 == "退補件" || tempSWC004 == "申請中"))
                                    ShowEditBtn = "Y";
                                break;
                            case "02":
                                /* 2020版 - 編輯只剩基本資料可以修改、原本受理中可修改，改為不可修改
                                //承辦技師
                                if (tempSWC021ID == ssUserID && (tempSWC004 == "退補件" || tempSWC004 == "受理中" || tempSWC004 == "申請中" || tempSWC004 == "審查中"))
                                    ShowEditBtn = "Y";
                                //監造技師
                                if (tempSWC045ID == ssUserID && tempSWC004 == "施工中")
                                    ShowEditBtn = "Y";
                                */
                                //承辦技師
                                if (tempSWC021ID == ssUserID && (tempSWC004 == "退補件" || tempSWC004 == "申請中" || tempSWC004 == "受理中" || tempSWC004 == "審查中"))
                                    ShowEditBtn = "Y";
                                break;
                            case "03":
                                //OdsPointDtl3Date.Visible = true;
                                break;
                            case "04":
                                /* 2020版 - 編輯只剩基本資料可以修改
                                //審查公會
                                if (tempSWC022ID == ssUserID && (tempSWC004 == "審查中" || tempSWC004 == "暫停審查"))
                                {
                                    ShowEditBtn = "Y";
                                }
                                //檢查公會
                                if (tempSWC024ID == ssUserID && tempSWC004 == "施工中")
                                {
                                    ShowEditBtn = "Y";
                                }
                                //完工檢查公會
                                if (Session["Edit4"] + "" == "Y" && tempSWC004 == "已完工" || tempSWC004 == "撤銷" || tempSWC004 == "已變更")
                                {
                                    ShowEditBtn = "Y";
                                }
                                //召集人
                                string chkEidtStr= " select * from GuildGroup where ETID='"+ ssUserID + "' and SWC000 ='"+ tempCaseSwc000 + "' and CHGType='1' ";
                                using (SqlConnection GGConn = new SqlConnection(connectionString.ConnectionString))
                                {
                                    GGConn.Open();

                                    SqlDataReader readerGG;
                                    SqlCommand objCmdGG = new SqlCommand(chkEidtStr, GGConn);
                                    readerGG = objCmdGG.ExecuteReader();

                                    while (readerGG.Read())
                                    {
                                        string tmpRGtype = readerGG["RGtype"] +"";
                                        ShowEditBtn = (tmpRGtype == "S3" && (tempSWC004 == "審查中" || tempSWC004 == "暫停審查")) ? "Y": ShowEditBtn;
                                        ShowEditBtn = (tmpRGtype == "S4" && tempSWC004 == "施工中") ? "Y" : ShowEditBtn;
                                    }
                                }
                                */
                                break;
                        }
                    }
                }

                if (ShowEditBtn == "Y")
                {
                    Button btn = (Button)e.Row.Cells[0].FindControl("BtnEdit");
                    btn.Visible = true;
                }

                //施工檢查檢核(警示燈)
                if (tempCaseSwc024ID == ssUserID && tempCaseSwc004 == "施工中")
                {
                    using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
                    {
                        string strSQLRV = " select MAX(DTLC002) AS DTLC002  from SWCDTL03 where SWC000='" + tempCaseSwc000 + "' ";

                        SwcConn.Open();

                        SqlDataReader readerSWC;
                        SqlCommand objCmdSWC = new SqlCommand(strSQLRV, SwcConn);
                        readerSWC = objCmdSWC.ExecuteReader();

                        while (readerSWC.Read())
                        {
                            if ((readerSWC["DTLC002"] + "").Trim() != "")
                            {
                                DateTime tDTLC002 = Convert.ToDateTime(readerSWC["DTLC002"] + "");
                                DateTime tTodey = DateTime.Now;

                                TimeSpan ts = tTodey - tDTLC002;
                                double days = ts.TotalDays;

                                if (days > ChkOverDay)
                                {
                                    e.Row.Cells[0].Attributes.Add("style", "text-align:center");
                                    e.Row.Cells[0].ForeColor = System.Drawing.Color.Red;
                                    e.Row.Cells[0].Text = "●";
                                }
                            }
                        }
                    }
                }

                //審查期限

                String tDATE5 = e.Row.Cells[5].Text + "";

                switch (tempCaseSwc004)
                {
                    case "退補件":
                    case "不予受理":
                    case "受理中":
                    case "申請中":
                    case "審查中":
                    case "暫停審查":
                    case "撤銷":
                    case "不予核定":
                        if (tDATE5.Trim() != "")
                        {
                            tDATE5 = SBApp.DateView(tDATE5, "02");
                        }
                        if (tDATE5.Trim().PadLeft(4) == "1900")
                        {
                            tDATE5 = "";
                        }
                        e.Row.Cells[5].Text = tDATE5;
                        break;

                    case "已核定":
                    case "施工中":
                    case "已完工":
                    case "廢止":
                    case "失效":
                    case "已變更":
                        e.Row.Cells[5].Text = "審查完成";
                        break;
                    default:
                        e.Row.Cells[5].Text = "";
                        break;
                }


                if (tDATE5.Trim().PadLeft(4) == "1900")
                {
                    tDATE5 = "";
                    e.Row.Cells[5].Text = tDATE5;
                }
                break;
        }

    }

    protected void GVSWCList_Sorting(object sender, GridViewSortEventArgs e)
    {
        if (ViewState["mySorting"] == null)
        {
            e.SortDirection = SortDirection.Ascending;
            ViewState["mySorting"] = "Ascending";
        }
        else
        {
            if (ViewState["mySorting"] + "" == "Ascending")
            {
                e.SortDirection = SortDirection.Descending;
                ViewState["mySorting"] = "Descending";
            }
            else
            {
                e.SortDirection = SortDirection.Ascending;
                ViewState["mySorting"] = "Ascending";
            }
        }
        setGVDate();
    }
}