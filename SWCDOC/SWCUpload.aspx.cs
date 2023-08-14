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
using System.Web.UI.HtmlControls;
using System.Data;

public partial class SWCDOC_SWCUpload : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string rSWCNO = Request.QueryString["SWCNO"] + "";
        //動態生成審查歷程
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        string tsql = " select ISNULL(max(DTLA006),'0') AS DTLA006 from SWCDTL01 Where SWC000='" + rSWCNO + "' and DATALOCK='Y' ;";
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader reader;
            SqlCommand sc = new SqlCommand(tsql, SWCConn);
            reader = sc.ExecuteReader();

            while (reader.Read())
                LBMAX.Text = reader["DTLA006"].ToString();

            reader.Close();
            sc.Dispose();
        }


        GenerateApprove(rSWCNO);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        LBCASEID.Text = rCaseId;
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";
        GBClass001 SBApp = new GBClass001();

        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (rCaseId == "")
        {
            Response.Redirect("SWC001.aspx");
        }

        if (!IsPostBack)
        {
            SetSwcCase(rCaseId);
        }


        //全區供用

        SBApp.ViewRecord("管制時程表", "update", "");

        ToDay.Text = DateTime.Now.ToString("yyyy.M.d");
        Visitor.Text = SBApp.GetVisitorsCount();

        TextUserName.Text = "";
        if (ssUserName != "")
        {
            TextUserName.Text = ssUserName + ssJobTitle + "，您好";
        }

        //全區供用
    }

    protected void SetSwcCase(string s)
    {


        string SFName = ""; string createdate = ""; string savedate = ""; string saveuser = "";
        string sqlStr1 = " SELECT * FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE";

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
                cmd.Parameters.Add(new SqlParameter("@SWC000", s));
                LBNO.Text = "00" + LBMAX.Text;
                cmd.Parameters.Add(new SqlParameter("@SFTYPE", LBNO.Text.Substring(LBNO.Text.Length - 3, 3)));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                        while (readerSWC.Read())
                        {
                            SFName = readerSWC["SFName"] + "";
                            createdate = readerSWC["createdate"] + "";
                            savedate = readerSWC["savedate"] + "";
                            saveuser = readerSWC["Saveuser"] + "";
                        }
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
        LBDATE1.Text = createdate;
        LBDATE2.Text = savedate;
        LBPEOPLE.Text = saveuser;
        if (SFName != "")
        {
            SFLINKXX.Text = SFName;
            SFLINKXX.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC\\UpLoadFiles\\SwcCaseFile\\" + s + "\\\\" + SFName + "?ts=" + System.DateTime.Now.Millisecond;
            SFLINKXX.Visible = true;
        }

		SFName = ""; createdate = ""; savedate = ""; saveuser = "";
        sqlStr1 = " SELECT * FROM ShareFiles where SWC000=@SWC000 and SFTYPE=@SFTYPE";
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            using (var cmd = SWCConn.CreateCommand())
            {
                cmd.CommandText = sqlStr1;
                cmd.Parameters.Add(new SqlParameter("@SWC000", s));
                cmd.Parameters.Add(new SqlParameter("@SFTYPE", "099"));
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerSWC = cmd.ExecuteReader())
                {
                    if (readerSWC.HasRows)
                        while (readerSWC.Read())
                        {
                            SFName = readerSWC["SFName"] + "";
                            createdate = readerSWC["createdate"] + "";
                            savedate = readerSWC["savedate"] + "";
                            saveuser = readerSWC["Saveuser"] + "";
                        }
                    readerSWC.Close();
                }
                cmd.Cancel();
            }
        }
        if (SFName != "")
        {
            SFLINK99.Text = SFName;
            SFLINK99.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC\\UpLoadFiles\\SwcCaseFile\\" + s + "\\\\" + SFName + "?ts=" + System.DateTime.Now.Millisecond;
            SFLINK99.Visible = true;
        }
    }
    private void GenerateApprove(string s)
    {
        GBClass001 SBApp = new GBClass001();
        pdata.Controls.Clear();
        var span = new HtmlGenericControl("span");
        var a = new HtmlGenericControl("a");
        var button = new HtmlGenericControl("Button");
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];

        int i = Convert.ToInt32(LBMAX.Text);

        //span 第幾次審查
        span = new HtmlGenericControl("span");
        span.InnerText = "第" + i.ToString() + "次審查";
        span.Attributes["class"] = "R_title RT1";
        pdata.Controls.Add(span);
        //span hr
        span = new HtmlGenericControl("span");
        span.Attributes["class"] = "br";
        pdata.Controls.Add(span);

        DataTable OBJ_GV01 = (DataTable)ViewState["GV"];
        DataTable DTGV01 = new DataTable();
        if (OBJ_GV01 == null)
        {
            DTGV01.Columns.Add(new DataColumn("GV_1", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_2", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_3", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_4", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_5", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_6", typeof(string)));
            //DTGV01.Columns.Add(new DataColumn("GV_7", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_8", typeof(string)));
            DTGV01.Columns.Add(new DataColumn("GV_9", typeof(string)));

            ViewState["GV"] = DTGV01;
            OBJ_GV01 = DTGV01;
        }

        string tsql = " select * from SWCDTL01 Where SWC000='" + s + "' and DTLA006='" + i.ToString() + "' and DATALOCK='Y' order by DTLA001;";
        GridView gv = new GridView();
        //gv.ID = OBJ_GV01.TableName;
        gv.ID = "GV" + i.ToString();
        gv.AllowPaging = false;
        gv.AllowSorting = true;
        gv.AutoGenerateColumns = false;
        gv.CssClass = "R_TB1";

        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            SqlDataReader reader = null;
            SqlCommand sc = new SqlCommand(tsql, SWCConn);
            reader = sc.ExecuteReader();



            while (reader.Read())
            {
                DataRow dr01 = OBJ_GV01.NewRow();
                dr01["GV_1"] = reader["DTLA001"];
                dr01["GV_2"] = SBApp.DateView(reader["savedate"].ToString(), "00");
                dr01["GV_3"] = SBApp.DateView(reader["DTLA003"].ToString(), "00");
                dr01["GV_4"] = reader["DTLA004"];
                dr01["GV_5"] = SBApp.DateView(reader["DTLA007"].ToString(), "00");
                dr01["GV_6"] = reader["DTLA034"];
                //dr01["GV_7"] = i.ToString();
                dr01["GV_8"] = reader["DTLA001"];
                dr01["GV_9"] = reader["DATALOCK"];
                OBJ_GV01.Rows.Add(dr01);
            }

            reader.Close();
            sc.Dispose();
        }


        foreach (DataColumn c in OBJ_GV01.Columns)
        {
            if (c.ColumnName == "GV_1") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "審查表單編號"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_2") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "函送日期"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_3") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "補正期限"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_4") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "主旨"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_5") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "開會日期"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_6") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "重新上傳原因"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_7") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; bf.HeaderText = "TEST"; gv.Columns.Add(bf); }
            if (c.ColumnName == "GV_8")
            {
                //HyperLinkField hlf = new HyperLinkField();
                //hlf.Text = "詳情";
                //hlf.NavigateUrl = "SWCDT001.aspx?SWCNO=" + s + "&DTLNO=";
                //hlf.Visible = false;
                //gv.Columns.Add(hlf);
                BoundField bf = new BoundField();
                gv.Columns.Add(bf);
            }
            if (c.ColumnName == "GV_9") { BoundField bf = new BoundField(); bf.DataField = c.ColumnName; gv.Columns.Add(bf); }
        }
        gv.DataSource = OBJ_GV01;
        gv.DataBind();
        pdata.Controls.Add(gv);
        OBJ_GV01.Clear();

        foreach (GridViewRow rw in gv.Rows)
        {
            if (rw.RowType == DataControlRowType.DataRow)
            {
                //HyperLinkField hl = ((HyperLinkField)rw.Cells[6].FindControl("fd"));
                //rw.Cells[0].Text = "";
                //hl.NavigateUrl = "";
                //Button btn1 = new Button();
                //btn1.Text = "詳情";
                //btn1.ID = gv.ID;
                //btn1.Click += new EventHandler(this.GoCase_Click);
                //rw.Cells[6].Controls.Add(btn1);

                HyperLink hlf = new HyperLink();
                if (rw.Cells[7].Text == "Y")
                    hlf.Text = "詳情";
                else
                    hlf.Text = "編輯";
                hlf.NavigateUrl = "SWCDT001.aspx?SWCNO=" + s + "&DTLNO=" + rw.Cells[0].Text;
                rw.Cells[6].Controls.Add(hlf);
            }
        }
        //隱藏最後一欄
        gv.Columns[7].Visible = false;



    }
    protected void Btn_UPSFile_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        string btnType = ((Button)(sender)).ID;
        string rDTLNO = LBCASEID.Text;
        bool rUP = false;
        string tSFType = "", tSFName = "", filename = "", extension = "";
        string ssUserNAME = Session["NAME"] + "";
        string exeSqlStr = "";
        Label pgTBName = new Label();
        Label pgTBDate = new Label();



        #region.值設定
        switch (btnType)
        {
            case "Btn_UPSFileXX":
                tSFType = LBNO.Text.Substring(LBNO.Text.Length - 3, 3);
                if (tSFType == "001") tSFName = "UPSFA_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "002") tSFName = "UPSFB_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "003") tSFName = "UPSFC_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "004") tSFName = "UPSFD_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "005") tSFName = "UPSFE_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "006") tSFName = "UPSFF_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "007") tSFName = "UPSFG_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "008") tSFName = "UPSFH_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "009") tSFName = "UPSFI_" + rDTLNO.Substring(rDTLNO.Length - 3);
                if (tSFType == "010") tSFName = "UPSFJ_" + rDTLNO.Substring(rDTLNO.Length - 3);
                filename = SFFileXX_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFileXX_FileUpload, TXTSFileXX, "TXTSFileXX", tSFName, null, SFLINKXX, 150, ""); //150MB
                break;
            case "Btn_UPSFile99":
                tSFType = "099"; tSFName = "UPSFZ_" + rDTLNO.Substring(rDTLNO.Length - 3);
                filename = SFFile99_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();
                rUP = FileUpLoadApp2("PDF", SFFile99_FileUpload, TXTSFile99, "TXTSFile99", tSFName, null, SFLINK99, 150, ""); //150MB
                break;
        }
        #endregion

        if (rUP)
        {
            switch (btnType)
            {
                case "Btn_UPSFileXX":
                    UP1.Text = "O"+tSFName+extension;
                    break;
                case "Btn_UPSFile99":
                    UP2.Text = "O"+tSFName+extension;
                    break;
            }
        }

        //搬到確認按鈕
        #region.存入db
        //if (rUP)
        //{
        //    tSFName += extension;
        //    pgTBName.Text = ssUserNAME;
        //    pgTBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        //    string updDB = " Update ShareFiles Set [SFName]='" + tSFName + "',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";
        //
        //    ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //    using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        //    {
        //        SwcConn.Open();
        //
        //        string strSQLRV = " select * from ShareFiles  Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";
        //
        //        SqlDataReader readeSwc;
        //        SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
        //        readeSwc = objCmdSwc.ExecuteReader();
        //
        //        if (!readeSwc.HasRows)
        //        {
        //            exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE,createdate) VALUES ('" + rDTLNO + "','" + tSFType + "',GETDATE());";
        //        }
        //        readeSwc.Close();
        //        objCmdSwc.Dispose();
        //
        //        exeSqlStr += updDB;
        //        SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
        //        objCmdUpd.ExecuteNonQuery();
        //        objCmdUpd.Dispose();
        //    }
        //}
        #endregion

        #region.寄信
        //審查公會、公會審查召集人、審查委員
        //string tToDay = DateTime.Now.ToString("yyyy-MM-dd");
        ////****
        //string tSWC005 = LBSWC005.Text;
        //string tSWC022ID = LBSWC022ID.Text;
        ////****
        //string tLBSAOID = ";;" + LBSAOID.Text.Replace(";;;;", "");
        //string tMailSub = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        //string tMailText = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】修正本，請至書件管理平台查看。";
        //tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        //tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
        //
        //switch (btnType)
        //{
        //    case "Btn_UPSFile01":
        //    case "Btn_UPSFile02":
        //    case "Btn_UPSFile03":
        //        string[] arraySentMail = new string[] { "" };
        //        string[] arrayMailToP = tLBSAOID.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
        //
        //        arraySentMail[0] = SBApp.GetGeoUser(tSWC022ID, "Email");
        //        bool MailTo01 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
        //
        //        for (int i = 0; i < arrayMailToP.Length; i++)
        //        {
        //            arraySentMail[0] = SBApp.GetETUser(arrayMailToP[i], "Email");
        //            bool MailTo02 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
        //        }
        //        break;
        //}

        #endregion
    }

    protected void Btn_DelSFile_Click(object sender, EventArgs e)
    {
        string btnType = ((Button)(sender)).ID;

        string rDTLNO = LBCASEID.Text;
        string delFileName = "", tSFType = "";
        bool tdel = false;
        HyperLink pgLink = new HyperLink();
        TextBox pgTB = new TextBox();
        Label pgTBName = new Label();
        Label pgTBDate = new Label();

        tdel = DelPageFile(delFileName, "");

        #region.存db
        if (tdel)
        {
            switch (btnType)
            {
                case "Btn_DelSFileXX":
                    pgLink = SFLINKXX; pgTB = TXTSFileXX;
                    tSFType = LBNO.Text.Substring(LBNO.Text.Length - 3, 3);
					UP1.Text="X";
                    break;
                case "Btn_DelSFile99":
                    pgLink = SFLINK99; pgTB = TXTSFile99; tSFType = "099";
					UP2.Text="X";
                    break;
            }

            //string DelDBDA = " Update ShareFiles set SFName='' Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";
			//
            //ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            //using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
            //{
            //    SwcConn.Open();
			//
            //    SqlCommand objCmdSWC = new SqlCommand(DelDBDA, SwcConn);
            //    objCmdSWC.ExecuteNonQuery();
            //    objCmdSWC.Dispose();
            //}

            pgLink.Text = "";
            pgLink.NavigateUrl = "";
            pgLink.Visible = false;
            pgTB.Text = "";
            pgTBName.Text = "";
            pgTBDate.Text = "";
        }
        #endregion
    }

    private bool DelPageFile(string delFileName, string delFileType)
    {
        bool rValue = false;
        string csCaseID = LBCASEID.Text;

        //刪實體檔
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];
        string FileFullPath = SwcCaseFolderPath + csCaseID + "\\" + delFileName;

        try
        {
            if (File.Exists(FileFullPath)) File.Delete(FileFullPath);
            rValue = true;
        }
        catch { }

        return rValue;
    }
    private bool FileUpLoadApp2(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadReName, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink, float _FileMaxSize, string SubFolder)
    {
        GBClass001 MyBassAppPj = new GBClass001();

        string CaseId = LBCASEID.Text;
        string UpLoadFileName = UpLoadReName;
        bool rValue = false;

        #region.基本檢查
        string vTempValue = UpLoadText.Text + "";
        if (vTempValue.Trim() != "")
        {
            Response.Write("<script>alert('已經有檔案，若要覆蓋，請先刪除原有檔案');</script>");
            return rValue;
        }
        string tUpLoadFile = UpLoadBar.FileName + "";
        if (tUpLoadFile == "")
        {
            Response.Write("<script>alert('請選擇欲上傳的檔案');</script>");
            return rValue;
        }
        #endregion

        #region.檔案上傳
        if (UpLoadBar.HasFile)
        {
            string filename = UpLoadBar.FileName;   // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
            string extension = Path.GetExtension(filename).ToLowerInvariant();

            // 判斷是否為允許上傳的檔案附檔名
            switch (ChkType)
            {
                case "PDF":
                    List<string> allowedExtextsion03 = new List<string> { ".pdf", ".PDF" };

                    if (allowedExtextsion03.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 PDF 檔案格式上傳，謝謝!!');</script>");
                        return rValue;
                    }
                    break;
                case "PDFJPGPNG":
                    List<string> allowedExtextsion01 = new List<string> { ".pdf", ".PDF", ".JPG", ".jpg", ".PNG", ".png" };

                    if (allowedExtextsion01.IndexOf(extension) == -1)
                    {
                        Response.Write("<script>alert('請選擇 PDF、JPG、PNG 檔案格式上傳，謝謝!!');</script>");
                        return rValue;
                    }
                    break;
            }

            //檔案大小限制
            int filesize = UpLoadBar.PostedFile.ContentLength;
            if (filesize > _FileMaxSize * 1000000)
            {
                Response.Write("<script>alert('請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!');</script>");
                return rValue;
            }
            UpLoadFileName += extension;

            #region.上傳設定
            //檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFilePath20"] + CaseId;
            if (SubFolder.Trim() != "") serverDir += "\\" + SubFolder;
            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
            Session[UpLoadStr] = "有檔案";

            string serverFilePath = Path.Combine(serverDir, UpLoadFileName);
            string fileNameOnly = Path.GetFileNameWithoutExtension(UpLoadFileName);

            // 把檔案傳入指定的 Server 內路徑
            try
            {
                UpLoadBar.SaveAs(serverFilePath);

                switch (ChkType)
                {
                    case "PDF":
                    case "PDFJPGPNG":
                        UpLoadLink.Text = UpLoadFileName;
                        UpLoadLink.NavigateUrl = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC\\UpLoadFiles\\SwcCaseFile\\" + CaseId + "\\" + SubFolder + "\\" + UpLoadFileName + "?ts=" + System.DateTime.Now.Millisecond;
                        UpLoadLink.Visible = true;
                        break;
                }
                UpLoadText.Text = UpLoadFileName;

                #region.上傳成功存db
                rValue = true;
                #endregion
            }
            catch (Exception ex)
            {
                //error_msg.Text = "檔案上傳失敗";
            }
            #endregion
        }
        else
        {
            Session[UpLoadStr] = "";
        }
        return rValue;
        #endregion
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        string vCaseID = Request.QueryString["SWCNO"] + "";
        Response.Redirect("SWC003.aspx?SWCNO=" + vCaseID);
    }

    protected void Save_Click(object sender, EventArgs e)
    {
        string ssUserNAME = Session["NAME"] + "";
        string exeSqlStr = "";
        string tSFType = "", tSFName = "", filename = "", extension = "";
        string rDTLNO = LBCASEID.Text;

        Label pgTBName = new Label();
        Label pgTBDate = new Label();

        if (UP1.Text != "" && UP1.Text.Substring(0,1) == "O")
        {
            tSFType = LBNO.Text.Substring(LBNO.Text.Length - 3, 3);
            //if (tSFType == "001") tSFName = "UPSFA_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "002") tSFName = "UPSFB_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "003") tSFName = "UPSFC_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "004") tSFName = "UPSFD_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "005") tSFName = "UPSFE_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "006") tSFName = "UPSFF_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "007") tSFName = "UPSFG_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "008") tSFName = "UPSFH_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "009") tSFName = "UPSFI_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //if (tSFType == "010") tSFName = "UPSFJ_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //filename = SFFileXX_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();

            //搬到確認按鈕
            #region.存入db
			
            //filename += extension;
			filename = UP1.Text.Substring(1,UP1.Text.Length-1);
            pgTBName.Text = ssUserNAME;
            pgTBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string updDB = " Update ShareFiles Set [SFName]='" + filename + "',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from ShareFiles  Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                if (!readeSwc.HasRows)
                {
                    exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE,createdate) VALUES ('" + rDTLNO + "','" + tSFType + "',GETDATE());";
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                exeSqlStr += updDB;
                SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
            #endregion
			Send_Mail("1");
        }

        if (UP2.Text != "" && UP2.Text.Substring(0,1) == "O")
        {
            tSFType = "099"; tSFName = "UPSFZ_" + rDTLNO.Substring(rDTLNO.Length - 3);
            //filename = SFFile99_FileUpload.FileName; extension = Path.GetExtension(filename).ToLowerInvariant();


            //搬到確認按鈕
            #region.存入db

            //filename += extension;
			filename = UP2.Text.Substring(1,UP2.Text.Length-1);
            pgTBName.Text = ssUserNAME;
            pgTBDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            string updDB = " Update ShareFiles Set [SFName]='" + filename + "',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConn.Open();

                string strSQLRV = " select * from ShareFiles  Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";

                SqlDataReader readeSwc;
                SqlCommand objCmdSwc = new SqlCommand(strSQLRV, SwcConn);
                readeSwc = objCmdSwc.ExecuteReader();

                if (!readeSwc.HasRows)
                {
                    exeSqlStr = " INSERT INTO ShareFiles (SWC000,SFTYPE,createdate) VALUES ('" + rDTLNO + "','" + tSFType + "',GETDATE());";
                }
                readeSwc.Close();
                objCmdSwc.Dispose();

                exeSqlStr += updDB;
                SqlCommand objCmdUpd = new SqlCommand(exeSqlStr, SwcConn);
                objCmdUpd.ExecuteNonQuery();
                objCmdUpd.Dispose();
            }
            #endregion
			Send_Mail("2");
        }
		if(UP1.Text == "X"){
			tSFType = LBNO.Text.Substring(LBNO.Text.Length - 3, 3);
			string DelDBDA = " Update ShareFiles Set [SFName]='',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";
		
            ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
            {
                SwcConn.Open();
			
                SqlCommand objCmdSWC = new SqlCommand(DelDBDA, SwcConn);
                objCmdSWC.ExecuteNonQuery();
                objCmdSWC.Dispose();
            }
		}
		if(UP2.Text == "X"){
			tSFType = "099";
			string DelDBDA = " Update ShareFiles Set [SFName]='',saveuser = '" + ssUserNAME + "',savedate = getdate() Where [SWC000]='" + rDTLNO + "' and SFTYPE = '" + tSFType + "' ";
		
            ConnectionStringSettings connectionStringTslm = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SwcConn = new SqlConnection(connectionStringTslm.ConnectionString))
            {
                SwcConn.Open();
			
                SqlCommand objCmdSWC = new SqlCommand(DelDBDA, SwcConn);
                objCmdSWC.ExecuteNonQuery();
                objCmdSWC.Dispose();
            }
		}
		Response.Write("<script>alert('已儲存。');location.href='SWC003.aspx?SWCNO=" + rDTLNO + "';</script>");

    }
	private void Send_Mail(string s)
    {
		string t = "";
		if(s=="1") t = "修正本";
		if(s=="2") t = "檢視本";
		GBClass001 SBApp = new GBClass001();
        string vCaseID = Request.QueryString["SWCNO"] + "";
		string tLBSAOID = "";
        string exeSqlStr = " select E.ETName,E.ETID,ISNULL(RGSID,'0') AS RGSID from GuildGroup G Left Join ETUsers E on G.ETID=E.ETID where G.SWC000=@SWC000 order by convert(float,RGSID); ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
		using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
			SwcConn.Open();
            using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = exeSqlStr;
				cmd.Parameters.Add(new SqlParameter("@SWC000", vCaseID));
				cmd.ExecuteNonQuery();
				
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.HasRows)
						while (reader.Read())
						{
							string tmpUserID = reader["ETID"] + "";
							string tmpRGSID = reader["RGSID"] + "";
							int aaa = Convert.ToInt32(tmpRGSID);
							if (aaa > 0 && tmpUserID != "")
							{
								tLBSAOID += tmpUserID + ";;";
							}
						}
					reader.Close();
				}
				cmd.Cancel();
			}
        }
		
		//審查公會、公會審查召集人、審查委員
        string sqlStr1 = " SELECT * FROM SWCCASE where SWC000=@SWC000 ";
		string tSWC005 = ""; string tSWC022ID = ""; string tToDay = DateTime.Now.ToString("yyyy-MM-dd");
		using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
		{
			SwcConn.Open();
			using (var cmd = SwcConn.CreateCommand())
			{
				cmd.CommandText = sqlStr1;
				cmd.Parameters.Add(new SqlParameter("@SWC000", vCaseID));
				cmd.ExecuteNonQuery();
		
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.HasRows)
						while (reader.Read())
						{
							tSWC005 = reader["SWC005"] + "";
							tSWC022ID = reader["SWC022ID"] + "";
						}
					reader.Close();
				}
				cmd.Cancel();
			}
		}
		tLBSAOID = ";;" + tLBSAOID.Replace(";;;;", "");
		string tMailSub = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】"+t;
		string tMailText = "承辦技師已於 " + tToDay + " 上傳【" + tSWC005 + "】"+t+"，請至臺北市水土保持申請書件管理平台查看。";
		tMailText += "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
		tMailText += "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
		
		string[] arraySentMail = new string[] { "" };
		string[] arrayMailToP = tLBSAOID.Split(new string[] { ";;" }, StringSplitOptions.RemoveEmptyEntries);
		
		arraySentMail[0] = SBApp.GetGeoUser(tSWC022ID, "Email");
		bool MailTo01 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
		
		for (int i = 0; i < arrayMailToP.Length; i++)
		{
		    arraySentMail[0] = SBApp.GetETUser(arrayMailToP[i], "Email");
		    bool MailTo02 = SBApp.Mail_Send(arraySentMail, tMailSub, tMailText);
		}
	}

}