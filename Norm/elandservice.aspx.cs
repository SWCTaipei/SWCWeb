using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Norm_elandservice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //PostBack後停留在原畫面
        Page.MaintainScrollPositionOnPostBack = true;

        if (!IsPostBack) {
            //C20.swcLogRC("elandservice", "水土保持諮詢服務線上預約","民眾預約","新增","");
            GenerateDropDownList();
            THISID.Text = "APID" + DateTime.Now.ToString("yyyyMMddHHmmssffffff");
        }
    }
    private void GenerateDropDownList()
    {
        string[,] array_District = new string[,] { { "0", "" }, { "16", "北投" }, { "15", "士林" }, { "14", "內湖" }, { "10", "中山" }, { "03", "中正" }, { "17", "信義" }, { "02", "大安" }, { "13", "南港" }, { "11", "文山" }, { "09", "大同" }, { "05", "萬華" }, { "01", "松山" } };
        List<ListItem> ListZip = new List<ListItem>();
        for (int te = 0; te <= array_District.GetUpperBound(0); te++)
        {
            ListItem li = new ListItem(array_District[te, 1], array_District[te, 0]);
            ListZip.Add(li);
        }
        DDLDistrict.Items.AddRange(ListZip.ToArray());
    }
    #region 地籍選單
    private void DDLCadastralChange(string dTYPE, DropDownList vDP01, DropDownList vDP02, DropDownList vDP03, DropDownList vDP04)
    {
        //區、段、小段、地號
        string strDP01 = vDP01.Text;
        string strDP02 = vDP02.Text;
        string strDP03 = vDP03.Text;

        if (strDP01 == "0")
        {
            vDP03.Items.Clear();
            vDP02.Items.Clear();
        }
        else
        {
            string strSQLCAADDR = " ";
            switch (dTYPE)
            {
                case "01":
                    strSQLCAADDR += " SELECT DISTINCT LEFT(KCNT,2) AS KCNT FROM LAND ";
                    strSQLCAADDR += " WHERE AA46='" + strDP01 + "' ";
                    break;

                case "02":
                    strSQLCAADDR += " SELECT DISTINCT SUBSTRING(KCNT, LEN(KCNT) - 2, 1) AS KCNT2 FROM LAND ";
                    strSQLCAADDR += " WHERE AA46='" + strDP01 + "' ";
                    strSQLCAADDR += "   AND substring(KCNT, 1, 2)='" + strDP02 + "' ";
                    break;

                case "03":
                    string csKCNT = strDP02 + "段" + strDP03 + "小段";
                    strSQLCAADDR += " SELECT DISTINCT CADA_TEXT FROM LAND ";
                    strSQLCAADDR += " WHERE AA46='" + strDP01 + "' ";
                    strSQLCAADDR += "   AND KCNT='" + csKCNT + "' ";
                    strSQLCAADDR += " ORDER BY CADA_TEXT  ";
                    break;
            }

            //連DB
            ConnectionStringSettings settingCAADDR = ConfigurationManager.ConnectionStrings["GEOINFOCONN"];
            SqlConnection ConnCAADDR = new SqlConnection();
            ConnCAADDR.ConnectionString = settingCAADDR.ConnectionString;
            ConnCAADDR.Open();

            SqlDataReader readerCAADDR;
            SqlCommand objCmdGETCAADDR = new SqlCommand(strSQLCAADDR, ConnCAADDR);
            readerCAADDR = objCmdGETCAADDR.ExecuteReader();

            if (readerCAADDR.HasRows)
            {
                switch (dTYPE)
                {
                    case "01":
                        vDP03.Items.Clear();
                        vDP02.Items.Clear();
                        vDP02.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            vDP02.Items.Add(readerCAADDR["KCNT"].ToString());
                        }
                        break;
                    case "02":
                        vDP03.Items.Clear();
                        vDP03.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            vDP03.Items.Add(readerCAADDR["KCNT2"].ToString());
                        }
                        break;
                    case "03":
                        vDP04.Items.Add("");

                        while (readerCAADDR.Read())
                        {
                            if (readerCAADDR["CADA_TEXT"].ToString() != "") { vDP04.Items.Add(readerCAADDR["CADA_TEXT"].ToString()); }
                        }
                        break;
                }
            }

            objCmdGETCAADDR.Dispose();
            readerCAADDR.Close();

            ConnCAADDR.Close();
            ConnCAADDR.Dispose();

        }

    }
    #endregion

    protected void DDLDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLCadastralChange("01", DDLDistrict, DDLSection, DDLSection2, null);
    }

    protected void DDLSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLCadastralChange("02", DDLDistrict, DDLSection, DDLSection2, null);
    }


    protected void ADDLIST01_Click(object sender, EventArgs e)
    {
        Class20 C20 = new Class20();
        string addData01 = DDLDistrict.Text + "";
        string addData02 = DDLSection.Text + "";
        string addData03 = DDLSection2.Text + "";
        string addData04 = TXTNumber.Text + "";

        if (addData01 == "" || addData02 == "" || addData03 == "" || addData04 == "")
        {
            Response.Write("<script>alert('請輸入完整地段號');</script>");
            return;
        }

        DataTable tbCadastral = (DataTable)ViewState["SwcCadastral"];

        if (tbCadastral == null)
        {
            DataTable GVTBCD = new DataTable();

            GVTBCD.Columns.Add(new DataColumn("序號", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("區", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("段", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("小段", typeof(string)));
            GVTBCD.Columns.Add(new DataColumn("地號", typeof(string)));

            ViewState["SwcCadastral"] = GVTBCD;
            tbCadastral = (DataTable)ViewState["SwcCadastral"];
        }

        string adArea = DDLDistrict.SelectedItem.Text;
        string adSection = DDLSection.SelectedItem.Text + "段" + DDLSection2.SelectedItem.Text + "小段";
        string adNum = TXTNumber.Text;

        DataRow GVTBCDRow = tbCadastral.NewRow();
        CDNO.Text = (Convert.ToInt32(CDNO.Text) + 1).ToString();
        GVTBCDRow["序號"] = CDNO.Text;
        GVTBCDRow["區"] = DDLDistrict.SelectedItem.Text;
        GVTBCDRow["段"] = DDLSection.SelectedItem.Text;
        GVTBCDRow["小段"] = DDLSection2.SelectedItem.Text;
        GVTBCDRow["地號"] = TXTNumber.Text;

        tbCadastral.Rows.Add(GVTBCDRow);

        //Store the DataTable in ViewState
        GVCadastral.DataSource = ViewState["SwcCadastral"];
        GVCadastral.DataBind();

        TXTNumber.Text = "";
    }
    protected void GVCadastral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        GVCadastral.PageIndex = e.NewPageIndex;
    }
    protected void GVCadastral_PageIndexChanged(object sender, EventArgs e)
    {
        GVCadastral.DataSource = ViewState["SwcCadastral"];
        GVCadastral.DataBind();
    }
    protected void GVCadastral_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;
        switch (ExcAction)
        {
            case "delfile001":
                int aa = Convert.ToInt32(e.CommandArgument);
                int number = Convert.ToInt32(GVCadastral.Rows[aa].Cells[0].Text);
                DataTable VS_GV1 = (DataTable)ViewState["SwcCadastral"];
                int i = 0;
                VS_GV1.Rows.RemoveAt(aa);
                ViewState["SwcCadastral"] = VS_GV1;
                GVCadastral.DataSource = VS_GV1;
                GVCadastral.DataBind();
                break;
        }
    }

    protected void chkswcother_CheckedChanged(object sender, EventArgs e)
    {
        if (chkswcother.Checked)
            othertext.Enabled = true;
        else { othertext.Enabled = false; othertext.Text = ""; }
    }

    protected void chkswcphoto_CheckedChanged(object sender, EventArgs e)
    {
        if (chkswcphoto.Checked) {
            swcphoto.Enabled = true;
            swcphotoupload.Enabled = true;
        } else {
            swcphoto.Enabled = false;
            swcphoto.Dispose();
            swcphotoupload.Enabled = false;
            swcphotopanel.Visible = false;
            //swcimage.ImageUrl = "image/waitupload.jpg"
        }
    }
    protected void swcphotoupload_Click(object sender, EventArgs e)
    {
        int _FileMaxSize = 5;   //檔案大小限制5mb
        string thisID = THISID.Text;
        string thisFilesDesc = TBFileDesc.Text;
        string filename = swcphoto.FileName;    // UpLoadBar.FileName 只有 "檔案名稱.附檔名"，並沒有 Client 端的完整理路徑
        string extension = Path.GetExtension(filename).ToLowerInvariant();

        #region 檔案檢查
        //副檔名
        List<string> allowedExtextsion = new List<string> { ".jpg", ".JPG", ".png", ".PNG" };
        if (allowedExtextsion.IndexOf(extension) == -1) {
            Response.Write("<script>alert('請選擇 JPG 或 PNG 檔案格式上傳，謝謝');</script>"); return;
        }
        
        //大小
        int filesize = swcphoto.PostedFile.ContentLength;
        if (filesize > _FileMaxSize * 1000000) {
            Response.Write("<script>alert('請選擇 " + _FileMaxSize + "Mb 以下檔案上傳，謝謝!!');</script>"); return;
        }

        //檢查 Server 上該資料夾是否存在，不存在就自動建立
        string serverDir = ConfigurationManager.AppSettings["SwcServicePath"] + DateTime.Now.ToString("yyyy");
        if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);
        #endregion
        swcphotopanel.Visible = true;
        string reFileName = thisID + "_" + thisFilesDesc + extension; updateFilesDesc();
        string serverFilePath = Path.Combine(serverDir, reFileName);    //D:\公用區\唯讀區\elandservice\Uploads\2020\XXXXXX.jpg
        swcphoto.SaveAs(serverFilePath);
        string filestempPath = ConfigurationManager.AppSettings["SwcServiceUrl"] + DateTime.Now.ToString("yyyy") + "/";

        //上傳完畢自動加入表格
        string tFiles001No = Files001No.Text;
        Files001No.Text = (Convert.ToInt32(tFiles001No) + 1).ToString();

        DataTable File001C = (DataTable)ViewState["File001C"];

        if (File001C == null)
        {
            DataTable GVFILE001 = new DataTable();

            GVFILE001.Columns.Add(new DataColumn("File001000", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001001", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001002", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001003", typeof(string)));
            GVFILE001.Columns.Add(new DataColumn("File001004", typeof(string)));

            ViewState["File001C"] = GVFILE001;
            File001C = (DataTable)ViewState["File001C"];
        }
        DataRow GVFILE001Row = File001C.NewRow();

        GVFILE001Row["File001000"] = Files001No.Text;
        GVFILE001Row["File001001"] = "001";
        GVFILE001Row["File001002"] = "";
        GVFILE001Row["File001003"] = reFileName;
        GVFILE001Row["File001004"] = filestempPath + reFileName;

        File001C.Rows.Add(GVFILE001Row);

        //Store the DataTable in ViewState
        ViewState["File001C"] = File001C;
        GridView2.DataSource = File001C;
        GridView2.DataBind();
    }

    private void updateFilesDesc()
    {
        //僅支援最多上傳200個檔案，超過請調整程式。
        string countFileDesc = TBFileDesc.Text;
        if (countFileDesc.Length == 2)
            if (countFileDesc == "99")
                TBFileDesc.Text = "A01";
            else
                TBFileDesc.Text = (Convert.ToInt32(countFileDesc) + 1).ToString().PadLeft(2, '0');
        else if (countFileDesc.Substring(1,2) == "99")
                TBFileDesc.Text = "B01";
        else
            TBFileDesc.Text = (Convert.ToInt32(countFileDesc.Substring(1, 2)) + 1).ToString().PadLeft(2, '0');
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;
        switch (ExcAction)
        {
            case "delfile002":
                int aa = Convert.ToInt32(e.CommandArgument);
                int number = Convert.ToInt32(GridView2.Rows[aa].Cells[0].Text);
                DataTable VS_GV1 = (DataTable)ViewState["File001C"];
                int i = 0;
                VS_GV1.Rows.RemoveAt(aa);
                ViewState["File001C"] = VS_GV1;
                GridView2.DataSource = VS_GV1;
                GridView2.DataBind();
                break;
        }
    }

    protected void selfchange()
    {
        if (radself.Checked)
        {
            radtel.Checked = false;
            telchange();
            radneedpeople.Checked = false;
            needpeoplechange();
            selfdatatext.Enabled = true;
        }
        else { 
            selfdatatext.Text = "";
            selfdatatext.Enabled = false;
        }
    }

    private void needpeoplechange()
    {
        if (radneedpeople.Checked) {
            radself.Checked = false;
            selfchange();
            radtel.Checked = false;
            telchange();
        }
    }

    private void telchange()
    {
        if (radtel.Checked)
        {
            radself.Checked = false;
            selfchange();
            radneedpeople.Checked = false;
            needpeoplechange();
            teldatatext.Enabled = true;
            chk0910.Enabled = true;
            chk1011.Enabled = true;
            chk1112.Enabled = true;
        } else {
            teldatatext.Text = "";
            teldatatext.Enabled = false;
            chk0910.Enabled = false;
            chk1011.Enabled = false;
            chk1112.Enabled = false;
        }
    }

    protected void radself_CheckedChanged(object sender, EventArgs e)
    {
        selfchange();
    }

    protected void radneedpeople_CheckedChanged(object sender, EventArgs e)
    {
        needpeoplechange();
    }

    protected void radtel_CheckedChanged(object sender, EventArgs e)
    {
        telchange();
    }

    protected void servicesend_Click(object sender, EventArgs e)
    {
        GBClass001 GBC = new GBClass001();
        Class20 C20 = new Class20();
        #region 必填檢查
		
		if(GVCadastral.Rows.Count == 0){
			Response.Write("<script>alert('您好，地籍資料尚未填登，於勾選地號後點擊新增以確認地籍資料登錄完成。');</script>");
			return;
		}
		
		if (!(radself.Checked || radtel.Checked || radneedpeople.Checked))
        {
            Response.Write("<script>alert('您好，記得勾選預約時段及選擇預約類型。');</script>");
            return;
        }
		
        string tbSelfText = selfdatatext.Text;
        if (radself.Checked && tbSelfText.Trim() == "") {
            Response.Write("<script>alert('親自前來請務必挑選日期，謝謝!!');</script>"); return;
        }

        string tbTelText = teldatatext.Text;
        if (radtel.Checked && tbTelText.Trim() == "") {
            Response.Write("<script>alert('電話聯絡時段請務必挑選日期，謝謝!!');</script>"); return;
        }
        if (radtel.Checked && tbTelText.Trim() != "" && !chk0910.Checked && !chk1011.Checked && !chk1112.Checked) {
            Response.Write("<script>alert('電話聯絡時段請務必挑選一個時段以供聯絡，謝謝!!');</script>"); return;
        }

        string reUserName = nametext.Text;
        if (reUserName.Trim() == "") { 
            Response.Write("<script>alert('請輸入申請人姓名，謝謝!!');</script>"); return;
        }
        string reUserPhone = mobiletext.Text;
        if (reUserPhone.Trim() == "") { 
            Response.Write("<script>alert('請輸入行動電話號碼，謝謝!!');</script>"); return;
        }
        #endregion

        #region saveDb
        string gNo = THISID.Text;
        #region page值
        string gItem01 = chkswccase.Checked ? "1" : "0";
        string gItem02 = chkswcprotect.Checked ? "1" : "0";
        string gItem03 = chkswcselfcheck.Checked ? "1" : "0";
        string gItem04 = chkswcilg.Checked ? "1" : "0";
        string gItem05 = chkswcfix.Checked ? "1" : "0";
        string gItem06 = chkswclandtype.Checked ? "1" : "0";
        string gItem07 = chkswcother.Checked ? "1" : "0";
        string gItem07Desc = othertext.Text;
        string gTime01 = radself.Checked ? "1" : "0";
        string gTime01Date = selfdatatext.Text;
        string gTime02 = radtel.Checked ? "1" : "0";
        string gTime02Date = teldatatext.Text;
        string gTime02T1 = chk0910.Checked ? "1" : "0";
        string gTime02T2 = chk1011.Checked ? "1" : "0";
        string gTime02T3 = chk1112.Checked ? "1" : "0";
        string gTime03 = radneedpeople.Checked ? "1" : "0";
        string gApplyName = nametext.Text;
        string gApplyTel = teltext.Text;
        string gApplyPhone = mobiletext.Text;
        string gApplySms = chksms.Checked ? "1" : "0";
        string gApplyAddr = addresstext.Text;
        #endregion
        string sqlSavStr = " Insert Into servicedate (表單編號, 程序諮詢, 技術指導, 自助檢查指導, 改正輔導, 搶修輔導及建議, 地質諮詢, 其他, 其他說明, 親自前來, 親自前來日期, 電話回覆, 電話回覆日期, 時段1, 時段2, 時段3, 現場輔導, 申請人, 連絡電話, 手機號碼, 簡訊通知, 住址,applyDate,applyIp) Values (@formNo,@item01,@item02,@item03,@item04,@item05,@item06,@item07,@item07Desc,@Time01,@Time01Date,@Time02,@Time02Date,@Time02T1,@Time02T2,@Time02T3,@Time03,@ApplyName,@ApplyTel,@ApplyPhone,@ApplySms,@ApplyAddr,getdate(),@ApplyIP)";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["elandservicelogconnstring"];
        using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
        {
            dbConn.Open();
            using (var cmd = dbConn.CreateCommand())
            {
                cmd.CommandText = sqlSavStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@formNo", gNo)); //formNo
                cmd.Parameters.Add(new SqlParameter("@item01", gItem01)); //程序諮詢
                cmd.Parameters.Add(new SqlParameter("@item02", gItem02)); //技術指導
                cmd.Parameters.Add(new SqlParameter("@item03", gItem03)); //自助檢查指導
                cmd.Parameters.Add(new SqlParameter("@item04", gItem04)); //改正輔導
                cmd.Parameters.Add(new SqlParameter("@item05", gItem05)); //搶修輔導及建議
                cmd.Parameters.Add(new SqlParameter("@item06", gItem06)); //地質諮詢
                cmd.Parameters.Add(new SqlParameter("@item07", gItem07)); //其他
                cmd.Parameters.Add(new SqlParameter("@item07Desc", gItem07Desc)); //其他說明
                cmd.Parameters.Add(new SqlParameter("@Time01", gTime01)); //親自前來
                cmd.Parameters.Add(new SqlParameter("@Time01Date", gTime01Date)); //親自前來日期
                cmd.Parameters.Add(new SqlParameter("@Time02", gTime02)); //電話回覆
                cmd.Parameters.Add(new SqlParameter("@Time02Date", gTime02Date)); //電話回覆日期
                cmd.Parameters.Add(new SqlParameter("@Time02T1", gTime02T1)); //時段1
                cmd.Parameters.Add(new SqlParameter("@Time02T2", gTime02T2)); //時段2
                cmd.Parameters.Add(new SqlParameter("@Time02T3", gTime02T3)); //時段3
                cmd.Parameters.Add(new SqlParameter("@Time03", gTime03)); //現場輔導
                cmd.Parameters.Add(new SqlParameter("@ApplyName", gApplyName)); //申請人
                cmd.Parameters.Add(new SqlParameter("@ApplyTel", gApplyTel)); //連絡電話
                cmd.Parameters.Add(new SqlParameter("@ApplyPhone", gApplyPhone)); //手機號碼
                cmd.Parameters.Add(new SqlParameter("@ApplySms", gApplySms)); //簡訊通知
                cmd.Parameters.Add(new SqlParameter("@ApplyAddr", gApplyAddr)); //住址
                cmd.Parameters.Add(new SqlParameter("@ApplyIP", GBC.GetClientIP())); //住址
                #endregion
                cmd.ExecuteNonQuery();
                cmd.Cancel();
            }
        }
        #endregion
        #region 地籍、附件存檔
        #region 地籍
        string sqlSaveCadastralStr = "";
        string updCadastralD01 = "", updCadastralD02 = "", updCadastralD03 = "";
        //DataTable dtCD = new DataTable();
        //dtCD = (DataTable)ViewState["SwcCadastral"];
        //if (dtCD != null)
        //{
		for ( int i = 0; i <= (GVCadastral.Rows.Count) - 1; i++)
		{
			string tLAND000 = GVCadastral.Rows[i].Cells[0].Text;
			string tLAND001 = GVCadastral.Rows[i].Cells[1].Text;
			string tLAND002 = GVCadastral.Rows[i].Cells[2].Text;
			string tLAND003 = GVCadastral.Rows[i].Cells[3].Text;
			string tLAND004 = GVCadastral.Rows[i].Cells[4].Text;
			string tKCNT = tLAND002 + "段" + tLAND003 + "小段";
		
			sqlSaveCadastralStr += " INSERT INTO tslm2.dbo.relationLand ([序號] , [區] , [段], [小段], [KCNT], [地號], [SwcServiceId] ) VALUES ";
			sqlSaveCadastralStr += " ('" + tLAND000 + "','" + tLAND001 + "' ,'" + tLAND002 + "' ,'" + tLAND003 + "' ,'" + tKCNT + "' ,'" + tLAND004 + "' ,'"+ gNo + "'); ";
		
			updCadastralD01 += tLAND001 + "區;";
			updCadastralD02 += tLAND002 + "段"+ tLAND003+"小段;";
			updCadastralD03 += tLAND004 + ";";
		}
		
		sqlSaveCadastralStr += " Update elandservice.dbo.servicedate Set 區='"+ updCadastralD01 + "',小段='"+ updCadastralD02 + "',地號='"+ updCadastralD03 + "',土地筆數='"+((GVCadastral.Rows.Count) - 1).ToString()+"' Where 表單編號='"+ gNo + "' ";
		using (SqlConnection dbConn = new SqlConnection(connectionString.ConnectionString))
		{
			dbConn.Open();
			using (var cmd = dbConn.CreateCommand())
			{
				cmd.CommandText = sqlSaveCadastralStr;
				cmd.ExecuteNonQuery();
				cmd.Cancel();
			}
		}
        //}
        #endregion
        #region 附件
        string sqlSaveFileStr = "";
        DataTable dtSWCFile = new DataTable();
        dtSWCFile = (DataTable)ViewState["File001C"];
        if (dtSWCFile != null)
        {
            int i = 0;
            for (i = 0; i <= (Convert.ToInt32(dtSWCFile.Rows.Count) - 1); i++)
            {
                string tFILE000 = (i + 1).ToString();
                string tFILE003 = dtSWCFile.Rows[i]["File001003"].ToString().Trim();
                string tFILE004 = dtSWCFile.Rows[i]["File001004"].ToString().Trim();
                string tPath = ConfigurationManager.AppSettings["SwcServicePath"] + DateTime.Now.ToString("yyyy")+"\\"+ tFILE003;

                sqlSaveFileStr += " insert into tslm2.dbo.SWCFILES (SFType,SFTypeDesc,SFID,SF001,SF002,SF003,SF004,SF005) VALUES ";
                sqlSaveFileStr += " ('SSVC','水保預約申請','" + gNo + "','" + (i+1).ToString() + "',N'" + tFILE003 + "',N'"+ tPath + "',N'"+ tFILE004 + "',getdate()); ";
                C20.CHIEFRC("elandservice", "水保預約申請", tFILE003, tPath, tFILE004,"新增");
            }
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();
                SqlCommand objCmdItem2 = new SqlCommand(sqlSaveFileStr, SWCConn);
                objCmdItem2.ExecuteNonQuery();

                objCmdItem2.Cancel();
                objCmdItem2.Dispose();
            }
        }
        #endregion
        #endregion
        #region 寄信/簡訊
        //"ge-40754@mail.taipei.gov.tw", "黃凱暉"
        //"ge-40727@mail.taipei.gov.tw", "廖麗禎"
        //"ge-10706@mail.taipei.gov.tw", "章姿隆"
		
		//20220804
		//章姿隆 ge-10706
		//廖麗禎 ge-40727
		//施柏宇 ge-10754
		
		GBClass001 SBApp = new GBClass001();
        string[] arrayChkUserMsg = SBApp.GetUserMailData();

        string ChkUserId = arrayChkUserMsg[0] + "";
        string ChkMail = arrayChkUserMsg[3] + "";
		
        string[] arrayUserId = ChkUserId.Split(new string[] { ";;" }, StringSplitOptions.None);
        string[] arrayUserMail = ChkMail.Split(new string[] { ";;" }, StringSplitOptions.None);
		string SentMailGroup = "";
		string ssMailSub01 = "水土保持服務預約申請";
		string ssMailBody01 = "申請人：" + reUserName + "<br/>聯絡電話：" + gApplyTel + "<br/>手機號碼：" + gApplyPhone + "<br/>地址：" + gApplyAddr + "<br/>系統管理者 敬上";
            
        for (int i = 1; i < arrayUserId.Length; i++)
        {
            string aUserId = arrayUserId[i];
            string aUserMail = arrayUserMail[i];

            if (aUserId.Trim() == "ge-10706" || aUserId.Trim() == "ge-40727" || aUserId.Trim() == "ge-10754")
            {
                SentMailGroup = SentMailGroup + ";;" + aUserMail;
            }
        }
		string[] arraySentMail01 = SentMailGroup.Split(new string[] { ";;" }, StringSplitOptions.None);
		bool MailTo01 = SBApp.Mail_Send(arraySentMail01, ssMailSub01, ssMailBody01);
		
		if(chksms.Checked)
		{
			string tSMSText = gApplyName + "您好:您的預約申請山坡地水土保持諮詢服務，本處水土保持服務團相關人員將儘快與您聯絡。臺北市政府工務局大地工程處敬上";
			GBC.SendSMS(gApplyPhone,tSMSText);
		}
        #endregion

        Response.Write("<script>alert('您的預約單已送出，謝謝您的使用!!');location.href='../SWCDOC/SWC001.aspx';</script>"); return;

    }

    protected void selfdatatext_TextChanged(object sender, EventArgs e)
    {
        bool before = false;
        string txtSelfDate = selfdatatext.Text;
        if (Convert.ToDateTime(txtSelfDate) < DateTime.Now)
        {
            Response.Write("<script>alert('請選擇今天以後的日期，謝謝。\n\r水保服務團只有星期二上午提供諮詢，我們會自動幫您把預約時間挪到下一個星期二，謝謝!!');</script>");
            txtSelfDate = DateTime.Now.ToString("yyyy-MM-dd");
            selfdatatext.Text = txtSelfDate;
            before = true;
        }
        int dayno = Convert.ToInt32(Convert.ToDateTime(txtSelfDate).DayOfWeek.ToString("d"));
        switch (dayno)
        {
            case 0:
            case 1:
                selfdatatext.Text = Convert.ToDateTime(txtSelfDate).AddDays(2 - dayno).ToString("yyyy-MM-dd");
                if (!before)
                    Response.Write("<script>alert('水保服務團只有星期二上午提供諮詢，我們會自動幫您把預約時間挪到下一個星期二，謝謝!!');</script>");
                break;
            case 2:
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                selfdatatext.Text = Convert.ToDateTime(txtSelfDate).AddDays(9- dayno).ToString("yyyy-MM-dd");
                if (!before)
                    Response.Write("<script>alert('水保服務團只有星期二上午提供諮詢，我們會自動幫您把預約時間挪到下一個星期二，謝謝!!');</script>");
                break;
        }
        before = false;
    }

    protected void teldatatext_TextChanged(object sender, EventArgs e)
    {
        bool before = false;
        string txtSelfDate = teldatatext.Text;
        if (Convert.ToDateTime(txtSelfDate) < DateTime.Now)
        {
            Response.Write("<script>alert('請選擇今天以後的日期，謝謝。\n\r水保服務團只有星期二上午提供諮詢，我們會自動幫您把預約時間挪到下一個星期二，謝謝!!');</script>");
            txtSelfDate = DateTime.Now.ToString("yyyy-MM-dd");
            teldatatext.Text = txtSelfDate;
            before = true;
        }
        int dayno = Convert.ToInt32(Convert.ToDateTime(txtSelfDate).DayOfWeek.ToString("d"));
        switch (dayno)
        {
            case 0:
            case 1:
                teldatatext.Text = Convert.ToDateTime(txtSelfDate).AddDays(2 - dayno).ToString("yyyy-MM-dd");
                if (!before)
                    Response.Write("<script>alert('水保服務團只有星期二上午提供諮詢，我們會自動幫您把預約時間挪到下一個星期二，謝謝!!');</script>");
                break;
            case 2:
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                teldatatext.Text = Convert.ToDateTime(txtSelfDate).AddDays(9- dayno).ToString("yyyy-MM-dd");
                if (!before)
                    Response.Write("<script>alert('水保服務團只有星期二上午提供諮詢，我們會自動幫您把預約時間挪到下一個星期二，謝謝!!');</script>");
                break;
        }
        before = false;
    }
}