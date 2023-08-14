using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SWCDOC_SWCBase001 : System.Web.UI.Page
{
    string UserUpLoadFilePath = "..\\UpLoadFiles\\SwcUser\\";
    string GlobalUpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
    protected void Page_Load(object sender, EventArgs e)
    {
        string rETID = Request.QueryString["ETID"] + "";

        string SessETID = Session["ID"] + "";
        string ssUserName = Session["NAME"] + "";
        string ssJobTitle = Session["JobTitle"] + "";

        GBClass001 SBApp = new GBClass001();

        if (!IsPostBack)
        {
            Session["AddGuildGVFile"] = "";
            Session["GuildSelect"] = "";

            if (rETID == "" && SessETID =="")
            {
                Response.Redirect("SWC001.aspx");
            }
            if (rETID == "NEW")
            {
                JSID.Text = rETID;
                Image1.ImageUrl = "../images/title/title-apply.png";
                GetUserData("NEW");
                TitleLink01.Visible = false;
                GenerateDropDownList();
            }
            else
            {
                if (SessETID == "")
                {
                    Response.Redirect("SWC001.aspx");
                }
                else
                {
                    GenerateDropDownList();
                    Image1.ImageUrl = "../images/title/title-account.png";
                    JSID.Text = SessETID;
                    TXTACTION.Text = SessETID;
                    GetUserData(SessETID);
                    TitleLink01.Visible = true;
                    CHGPW.Visible=true;
                }
            }
        }
        TXTETPWOLD.Attributes.Add("value", TXTETPWOLD.Text);
        TXTETPW.Attributes.Add("value", TXTETPW.Text);
        TXTETPWChk.Attributes.Add("value", TXTETPWChk.Text);
        GoTslm.Visible = false;
        //以下全區公用

        SBApp.ViewRecord("帳號管理", "view", "");

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
        //身份別
        string[] array_Identity = new string[] { "", "學校老師", "公務員", "技師" };
        DDLIdentity.DataSource = array_Identity;
        DDLIdentity.DataBind();
        #region-公會選單
        DDLGuild.Items.Clear();
        DDLGuild.Items.Add(new ListItem("選擇公會名稱", ""));

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            //公會
            string strSQLCase = " SELECT * FROM [geouser]  where unit = '技師公會' order by id ";

            SqlDataReader readerData;
            SqlCommand objCmdRV = new SqlCommand(strSQLCase, SWCConn);
            readerData = objCmdRV.ExecuteReader();

            while (readerData.Read())
            {
                string tuserid = readerData["userid"] + "";
                string tname = readerData["name"] + "";

                DDLGuild.Items.Add(new ListItem(tname, tuserid));
            }
            readerData.Close();
            objCmdRV.Dispose();
        }
        #endregion
    }

    private void GetUserData(string v)
    {
        GBClass001 SBApp = new GBClass001();

        AddNewAcc.Visible = false;
        SaveAccount.Visible = false;
        TextUserName.Visible = false; ;
        OLDPWAREA.Visible = false;

        switch (v)
        {
            case "NEW":
                TXTSYSID.Text = GetSYSID();
                AddNewAcc.Visible = true;
                LBPWNew1.Text = "密碼";
                LBPWNew2.Text = "密碼確認";
                break;

            default:
                TextUserName.Visible = true;
                OLDPWAREA.Visible = true;
                SaveAccount.Visible = true;
                break;
        }

        if (v == "NEW")
        {
        }
        else
        {
            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection UserConn = new SqlConnection(connectionString.ConnectionString))
            {
                UserConn.Open();

                string strSQLRV = " select * from ETUsers ";
                strSQLRV = strSQLRV + " where ETID = '" + v + "' ";

                SqlDataReader readeUser;
                SqlCommand objCmdUser = new SqlCommand(strSQLRV, UserConn);
                readeUser = objCmdUser.ExecuteReader();

                while (readeUser.Read())
                {
                    #region-取得db資料
                    string tSYSID = readeUser["SYSID"] + "";
                    string tETID = readeUser["ETID"] + "";
                    string tETPW = readeUser["ETPW"] + "";
                    string tETName = readeUser["ETName"] + "";
                    string tETTCNo01 = readeUser["ETTCNo01"] + "";
                    string tETTCNo02 = readeUser["ETTCNo02"] + "";
                    string tETTCNo03 = readeUser["ETTCNo03"] + "";
                    string tETTCNo04 = readeUser["ETTCNo04"] + "";
                    string tETTCNo05 = readeUser["ETTCNo05"] + "";
                    string tETTel = readeUser["ETTel"] + "";
                    string tETEmail = readeUser["ETEmail"] + "";
                    string tETOrgName = readeUser["ETOrgName"] + "";
                    string tETOrgGUINo = readeUser["ETOrgGUINo"] + "";
                    string tETOrgIssNo = readeUser["ETOrgIssNo"] + "";
                    string tETOrgAddr = readeUser["ETOrgAddr"] + "";
                    string tETOrgAddr2 = readeUser["ETOrgAddr2"] + "";
                    string tETOrgTel = readeUser["ETOrgTel"] + "";
                    string tETCOPC = readeUser["ETCOPC"] + "";
                    string tTCNo01ED = readeUser["TCNo01ED"] + "";
                    string tTCNo02ED = readeUser["TCNo02ED"] + "";
                    string tTCNo03ED = readeUser["TCNo03ED"] + "";
                    string tTCNo04ED = readeUser["TCNo04ED"] + "";
                    string tTCNo05ED = readeUser["TCNo05ED"] + "";
                    string tETCOPCExp = readeUser["ETCOPCExp"] + "";
                    string tETIdentity = readeUser["ETIdentity"] + "";
                    #endregion

                    TXTSYSID.Text = tSYSID;
                    TXTETIDNo.Text = tETID;
                    LBETIDNo.Text = tETID;
                    TXTETName.Text = tETName;
                    TXTETTCNo01.Text = tETTCNo01;
                    TXTETTCNo02.Text = tETTCNo02;
                    TXTETTCNo03.Text = tETTCNo03;
                    TXTETTCNo04.Text = tETTCNo04;
                    TXTETTCNo05.Text = tETTCNo05;
                    TXTETTel.Text = tETTel;
                    TXTETEmail.Text = tETEmail;
                    TXTETOrgName.Text = tETOrgName;
                    TXTETOrgGUINo.Text = tETOrgGUINo;
                    TXTETOrgIssNo.Text = tETOrgIssNo;
                    TXTETOrgAddr.Text = tETOrgAddr;
                    TXTETOrgAddr2.Text = tETOrgAddr2;
                    TXTETOrgTel.Text = tETOrgTel;
                    DDLIdentity.SelectedItem.Text = tETIdentity;

                    TXTTCNo01ED.Text = SBApp.DateView(tTCNo01ED, "00");
                    TXTTCNo02ED.Text = SBApp.DateView(tTCNo02ED, "00");
                    TXTTCNo03ED.Text = SBApp.DateView(tTCNo03ED, "00");
                    TXTTCNo04ED.Text = SBApp.DateView(tTCNo04ED, "00");
                    TXTTCNo05ED.Text = SBApp.DateView(tTCNo05ED, "00");

                    TXTETIDNo.Visible = false;
                    LBETIDNo.Visible = true;

                    string tETIDNo = readeUser["ETIDNo"] + "";
                    string tETStatus = readeUser["ETStatus"] + "";
                    string tApproved = readeUser["Approved"] + "";
                    string tApprovedDate = readeUser["ApprovedDate"] + "";
                    string tSaveuser = readeUser["Saveuser"] + "";
                    string tsavedate = readeUser["savedate"] + "";
                    string tstatus = readeUser["status"] + "";
                    string tGDSub01 = readeUser["GuildSubstitute"] + "";
                    string tGDSub01Chk = readeUser["GuildTcgeChk"] + "";
                    string tGDSub02 = readeUser["GuildSubstitute2"] + "";

                    TextBoxGD01.Text = tGDSub01;
                    TextBoxGD01Chk.Text = tGDSub01Chk;
                    TextBoxGD02.Text = tGDSub02;

                    //圖片類處理
                    string[] arrayFileName = new string[] { tETTCNo01, tETTCNo02, tETTCNo03, tETTCNo04, tETTCNo05};
                    System.Web.UI.WebControls.Image[] arrayImgAppobj = new System.Web.UI.WebControls.Image[] { TXTETTCNo01_img, TXTETTCNo02_img, TXTETTCNo03_img, TXTETTCNo04_img, TXTETTCNo05_img };

                    for (int i = 0; i < arrayFileName.Length; i++)
                    {
                        string strFileName = arrayFileName[i];
                        System.Web.UI.WebControls.Image ImgFileObj = arrayImgAppobj[i];

                        if (strFileName == "")
                        {
                        }
                        else
                        {
                            string tempImgPateh = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcUser/" + v + "/" + strFileName;
                            ImgFileObj.Attributes.Add("src", tempImgPateh + "?ts=" + DateTime.Now.Millisecond);
                        }
                    }
                }

                readeUser.Close();
                objCmdUser.Dispose();

            }

            #region 公會列表
            string exeSqlStr = " select * from ETUOwnGuild where ETID='"+v+"' order by No ";

            using (SqlConnection GuildConn = new SqlConnection(connectionString.ConnectionString))
            {
                GuildConn.Open();

                SqlDataReader readerData;
                SqlCommand objCmdRV = new SqlCommand(exeSqlStr, GuildConn);
                readerData = objCmdRV.ExecuteReader();

                //float nj = 0;
                string tGuildSel = "";
                while (readerData.Read())
                {
                    string tNo = readerData["No"] + "";
                    string tGuildId = readerData["EtGuild"] + "";
                    string tGuildName = readerData["GuildName"] + "";
                    string tCertificate = readerData["Certificate"] + "";
                    string tExperience = readerData["Experience"] + "";
                    string tEXP = readerData["EXP"] + "";
                    string tExperienceText = "";
                    string tGuildFileLink = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcUser/" + v + "/" + tCertificate; //GuildImage.ImageUrl;
                    string tExperienceLink = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcUser/" + v + "/" + tExperience;
                    tGuildSel += tGuildId + ";";

                    if (tExperience != "") tExperienceText = "審查經歷";

                    DataTable tbGuild = (DataTable)ViewState["OwnGuild"];

                    if (tbGuild == null)
                    {
                        DataTable GVGuild = new DataTable();

                        GVGuild.Columns.Add(new DataColumn("NI", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("GuildName", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("GuildId", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("GuildText", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("GuildFileName", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("GuildFileLink", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("ExperienceText", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("ExperienceFileName", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("ExperienceFileLink", typeof(string)));
                        GVGuild.Columns.Add(new DataColumn("EXPDate", typeof(string)));

                        ViewState["OwnGuild"] = GVGuild;
                        tbGuild = (DataTable)ViewState["OwnGuild"];
                    }

                    DataRow GVGuildRow = tbGuild.NewRow();

                    GVGuildRow["NI"] = tNo;
                    GVGuildRow["GuildName"] = tGuildName;
                    GVGuildRow["GuildId"] = tGuildId;
                    GVGuildRow["GuildText"] = "公會證書";
                    GVGuildRow["GuildFileName"] = tCertificate;
                    GVGuildRow["GuildFileLink"] = tGuildFileLink;
                    GVGuildRow["ExperienceText"] = tExperienceText;
                    GVGuildRow["ExperienceFileName"] = tExperience;
                    GVGuildRow["ExperienceFileLink"] = tExperienceLink;
                    GVGuildRow["EXPDate"] = SBApp.DateView(tEXP,"00");

                    tbGuild.Rows.Add(GVGuildRow);
                    ViewState["OwnGuild"] = tbGuild;

                    GuildList.DataSource = tbGuild;
                    GuildList.DataBind();

                    GuildCount.Text = tNo;
                }
                readerData.Close();
                objCmdRV.Dispose();
                Session["GuildSelect"] = tGuildSel;
            }
            #endregion
        }
    }

    private string GetSYSID()
    {
        DateTime oTime = DateTime.Now;
        string strTime = string.Format("{0:yyyyMMddHHmmss}{1:0000}", oTime, oTime.Millisecond);

        return strTime;
    }

    protected void GoHomePage_Click(object sender, EventArgs e)
    {
        Response.Redirect("SWC000.aspx");
    }

    protected void SaveAccount_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";

        string SSPW = Session["PW"]+"";

        #region-取得頁面值
        string SaveDate = "Y";
        string NewAccount = TXTETIDNo.Text + "";
        string gETID1 = TXTETPW.Text + "";
        string gETID2 = TXTETPWChk.Text + "";
        string gETName = TXTETName.Text + "";
        string gETTel = TXTETTel.Text + "";
        string gETEmail = TXTETEmail.Text + "";
        string gETOrgName = TXTETOrgName.Text + "";
        string gETOrgGUINo = TXTETOrgGUINo.Text + "";
        string gETOrgAddr = TXTETOrgAddr.Text + "";
        string gETOrgAddr2 = TXTETOrgAddr2.Text + "";
        string gETOrgTel = TXTETOrgTel.Text + "";
        string gETTCNo01 = TXTETTCNo01.Text + "";
        string gETTCNo02 = TXTETTCNo02.Text + "";
        string gETTCNo03 = TXTETTCNo03.Text + "";
        string gETTCNo04 = TXTETTCNo04.Text + "";
        string gETTCNo05 = TXTETTCNo05.Text + "";
        string gETOrgIssNo = TXTETOrgIssNo.Text + "";
        string gSYSID = TXTSYSID.Text + "";
        string gOldPw = TXTETPWOLD.Text + "";
        string gTCNo01ED = TXTTCNo01ED.Text + "";
        string gTCNo02ED = TXTTCNo02ED.Text + "";
        string gTCNo03ED = TXTTCNo03ED.Text + "";
        string gTCNo04ED = TXTTCNo04ED.Text + "";
        string gTCNo05ED = TXTTCNo05ED.Text + "";
        string gIdentity = DDLIdentity.SelectedItem.Text + "";
        float gGuildCount = GuildList.Rows.Count;
        #endregion

        #region-基本檢查
        GBClass001 SBApp = new GBClass001();

        if (gOldPw == "" && (gETID1 != "" || gETID2 !=""))
        {
            SaveDate = "N";
            error_msg.Text = SBApp.AlertMsg("請輸入舊密碼，謝謝!!");
            TXTETIDNo.Focus();
            return;

        }
            if (gOldPw != "" && (SSPW != gOldPw))
            {
                SaveDate = "N";
                error_msg.Text = SBApp.AlertMsg("密碼不正確請重新輸入，謝謝!!");
                TXTETIDNo.Focus();
                return;
            }

        if (gETID1 != gETID2)
        {
            SaveDate = "N";
            error_msg.Text = SBApp.AlertMsg("密碼與確認密碼不符");
            TXTETPW.Focus();
            return;
        }

        if (gIdentity == "技師")
        {
            if (gGuildCount < 1) { Response.Write("<script>alert('請上傳公會會員證書'); </script>"); return; }
            if (gETOrgIssNo == "") { Response.Write("<script>alert('請上傳公會會員證書'); </script>"); return; }
            if (gTCNo01ED == "" & gTCNo02ED == "" & gTCNo03ED == "" & gTCNo04ED == "") { Response.Write("<script>alert('請上傳至少一張執業執照');</script>"); return; }
        }
        #endregion

        if (SaveDate == "Y")
        {
            string UserSqlStr = "";

            UserSqlStr = UserSqlStr + " Update ETUsers Set ";
            if (gETID1 !="")
            {
                UserSqlStr = UserSqlStr + " ETPW ='" + gETID1 + "', ";
                Session["PW"] = gETID1;
            }
            #region-存入db
            UserSqlStr = UserSqlStr + " ETName =N'" + gETName + "', ";
            UserSqlStr = UserSqlStr + " ETTel =N'" + gETTel + "', ";
            UserSqlStr = UserSqlStr + " ETEmail =N'" + gETEmail + "', ";
            UserSqlStr = UserSqlStr + " ETOrgName =N'" + gETOrgName + "', ";
            UserSqlStr = UserSqlStr + " ETOrgGUINo =N'" + gETOrgGUINo + "', ";
            UserSqlStr = UserSqlStr + " ETOrgAddr =N'" + gETOrgAddr + "', ";
            UserSqlStr = UserSqlStr + " ETOrgAddr2 =N'" + gETOrgAddr2 + "', ";
            UserSqlStr = UserSqlStr + " ETOrgTel =N'" + gETOrgTel + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo01 =N'" + gETTCNo01 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo02 =N'" + gETTCNo02 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo03 =N'" + gETTCNo03 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo04 =N'" + gETTCNo04 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo05 =N'" + gETTCNo05 + "', ";
            UserSqlStr = UserSqlStr + " ETOrgIssNo =N'" + gETOrgIssNo + "', ";
            UserSqlStr = UserSqlStr + " TCNo01ED =N'" + gTCNo01ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo02ED =N'" + gTCNo02ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo03ED =N'" + gTCNo03ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo04ED =N'" + gTCNo04ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo05ED =N'" + gTCNo05ED + "', ";
            UserSqlStr = UserSqlStr + " SYSID =N'" + gSYSID + "', ";
            UserSqlStr = UserSqlStr + " ETIdentity =N'" + gIdentity + "', ";
            UserSqlStr = UserSqlStr + " saveuser = N'" + NewAccount + "', ";
            UserSqlStr = UserSqlStr + " savedate = getdate() ";
            UserSqlStr = UserSqlStr + " Where ETIDNo = '" + NewAccount + "'";

            ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                SqlCommand objCmdUser = new SqlCommand(UserSqlStr, SWCConn);
                objCmdUser.ExecuteNonQuery();
                objCmdUser.Dispose();

            }
            #endregion

            //上傳檔案…
            UpLoadTempFileMoveChk(NewAccount);

            #region-SaveGuildData
            SaveGuildData(NewAccount);
            #endregion

            GetUserData(NewAccount);
            
            Response.Write("<script>alert('資料已存檔'); location.href='SWC001.aspx'; </script>");
            
        }
    }

    private void SaveGuildData(string newAccount)
    {
        #region-公會會員證書存檔
        DataTable dtGuild = new DataTable();

        string exeSQLStr = " delete ETUOwnGuild Where ETID = '" + newAccount + "' ; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();
            SqlCommand objCmdItem = new SqlCommand(exeSQLStr, SWCConn);
            objCmdItem.ExecuteNonQuery();

            objCmdItem.Cancel();
            objCmdItem.Dispose();
        }

        dtGuild = (DataTable)ViewState["OwnGuild"];

        if (dtGuild != null)
        {
            int i = 0;

            for (i = 0; i <= (Convert.ToInt32(dtGuild.Rows.Count) - 1); i++)
            {
                string tNo = dtGuild.Rows[i]["NI"].ToString().Trim();
                string tEtGuild = dtGuild.Rows[i]["GuildId"].ToString().Trim();
                string tGuildName = dtGuild.Rows[i]["GuildName"].ToString().Trim();
                string tCertificate = dtGuild.Rows[i]["GuildFileName"].ToString().Trim();
                string tExperience = dtGuild.Rows[i]["ExperienceFileName"].ToString().Trim();
                string tEXP = dtGuild.Rows[i]["EXPDate"].ToString().Trim();

                exeSQLStr = " insert into ETUOwnGuild (No,ETID,EtGuild,GuildName,Certificate,Experience,EXP) VALUES ";
                exeSQLStr += " (@No,@ETID,@EtGuild,@GuildName,@Certificate,@Experience,@EXP); ";
                #region-存入db
                using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
                {
                    SWCConn.Open();

                    using (var cmd = SWCConn.CreateCommand())
                    {
                        cmd.CommandText = exeSQLStr;
                        #region-設定值
                        cmd.Parameters.Add(new SqlParameter("@No", tNo));
                        cmd.Parameters.Add(new SqlParameter("@ETID", newAccount));
                        cmd.Parameters.Add(new SqlParameter("@EtGuild", tEtGuild));
                        cmd.Parameters.Add(new SqlParameter("@GuildName", tGuildName));
                        cmd.Parameters.Add(new SqlParameter("@Certificate", tCertificate));
                        cmd.Parameters.Add(new SqlParameter("@Experience", tExperience));
                        cmd.Parameters.Add(new SqlParameter("@EXP", tEXP));
                        #endregion
                        cmd.ExecuteNonQuery();
                        cmd.Cancel();
                    }
                }
                #endregion
            }            
        }
        #endregion
    }

    protected void AddNewAcc_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();

        string SaveDate = "Y";
		string FirstApply="true";
        #region-取得畫面資料
        string NewAccount = TXTETIDNo.Text + "";
        string gETID1 = TXTETPW.Text + "";
        string gETID2 = TXTETPWChk.Text + "";
        string gETName = TXTETName.Text + "";
        string gETTel = TXTETTel.Text + "";
        string gETEmail = TXTETEmail.Text + "";
        string gETOrgName = TXTETOrgName.Text + "";
        string gETOrgGUINo = TXTETOrgGUINo.Text + "";
        string gETOrgAddr = TXTETOrgAddr.Text + "";
        string gETOrgAddr2 = TXTETOrgAddr2.Text + "";
        string gETOrgTel = TXTETOrgTel.Text + "";
        string gETTCNo01 = TXTETTCNo01.Text + "";
        string gETTCNo02 = TXTETTCNo02.Text + "";
        string gETTCNo03 = TXTETTCNo03.Text + "";
        string gETTCNo04 = TXTETTCNo04.Text + "";
        string gETTCNo05 = TXTETTCNo05.Text + "";
        string gETOrgIssNo = TXTETOrgIssNo.Text + "";
        string gSYSID = TXTSYSID.Text + "";
        string gTCNo01ED = TXTTCNo01ED.Text + "";
        string gTCNo02ED = TXTTCNo02ED.Text + "";
        string gTCNo03ED = TXTTCNo03ED.Text + "";
        string gTCNo04ED = TXTTCNo04ED.Text + "";
        string gTCNo05ED = TXTTCNo05ED.Text + "";
        string gIdentity = DDLIdentity.SelectedItem.Text+"";
        float gGuildCount = GuildList.Rows.Count;

        NewAccount = NewAccount.ToUpper();

        if (gIdentity.Trim() == "") { Response.Write("<script>alert('請選擇身分別'); </script>"); DDLIdentity.Focus(); return; }
        if (gETName.Trim() == "") { Response.Write("<script>alert('請輸入姓名'); </script>"); TXTETName.Focus(); return; }
        if (NewAccount == "") { Response.Write("<script>alert('身分證字號務必填登，謝謝!!'); </script>"); TXTETIDNo.Focus(); return; }
        if (gETID1 == "") { Response.Write("<script>alert('請輸入密碼'); </script>"); TXTETIDNo.Focus(); return; }
        if (gETID1 != gETID2) { Response.Write("<script>alert('密碼與確認密碼不符'); </script>"); TXTETPW.Focus(); return; }
        if (gETTel == "") { Response.Write("<script>alert('請輸入手機'); </script>"); TXTETTel.Focus(); return; }
        if (gETEmail == "") { Response.Write("<script>alert('請輸入電子信箱'); </script>"); TXTETEmail.Focus(); return; }
        if (gETOrgAddr == "") { Response.Write("<script>alert('請輸入通訊地址'); </script>"); TXTETOrgAddr.Focus(); return; }
        if (gETOrgName == "") { Response.Write("<script>alert('請輸入執業機構名稱'); </script>"); TXTETOrgName.Focus(); return; }
        if (gETOrgGUINo == "") { Response.Write("<script>alert('請輸入執業機構統一編號'); </script>"); TXTETOrgGUINo.Focus(); return; }
        if (gIdentity == "技師") {
            if(gETOrgIssNo==""){Response.Write("<script>alert('請輸入執業執照字號'); </script>"); return; }
            if (gGuildCount<1) { Response.Write("<script>alert('請上傳公會會員證書'); </script>"); return; }
            if (gTCNo01ED == "" & gTCNo02ED == "" & gTCNo03ED == "" & gTCNo04ED == "")
			{
				Response.Write("<script>alert('請上傳至少一張執業執照');</script>");
				return;
			}
		}
        #endregion


        
        //帳號重覆檢查 如果狀態為駁回 要能給他重新申請
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
        {
            SWCConn.Open();

            string strSQLUS = " select ETIDNo,status from ETUsers ";
            strSQLUS = strSQLUS + " where ETIDNo ='" + NewAccount + "' ";

            SqlDataReader readerUser;
            SqlCommand objCmdUser = new SqlCommand(strSQLUS, SWCConn);
            readerUser = objCmdUser.ExecuteReader();

            if (readerUser.HasRows)
            {
				while (readerUser.Read())
                {
                    if(readerUser["status"] + "" == "駁回")
						FirstApply = "false";
					else{
						Response.Write("<script>alert('您好，此帳號已重複申請，請再次確認密碼，或與大地工程處聯繫，謝謝。'); location.href='SWC000.aspx'; </script>");
						TXTETIDNo.Focus();
						SaveDate = "N";
						return;
					}
                }
            }
        }

        string UserSqlStr = "";
        if (SaveDate == "Y") {
			if(FirstApply == "false")
				UserSqlStr = UserSqlStr + " Update ETUsers set ETID = '" + NewAccount + "',ETIDNo = '" + NewAccount + "',ETStatus = '0',status = '申請中' where ETID = '" + NewAccount + "' ;";
			else
				UserSqlStr = UserSqlStr + " INSERT INTO ETUsers (ETID,ETIDNo,ETStatus,status) VALUES ('" + NewAccount + "','"+ NewAccount + "','0','申請中') ;";
			
            UserSqlStr = UserSqlStr + " Update ETUsers Set ";

            UserSqlStr = UserSqlStr + " ETPW =N'" + gETID1 + "', ";
            UserSqlStr = UserSqlStr + " ETName =N'" + gETName + "', ";
            UserSqlStr = UserSqlStr + " ETTel =N'" + gETTel + "', ";
            UserSqlStr = UserSqlStr + " ETEmail =N'" + gETEmail + "', ";
            UserSqlStr = UserSqlStr + " ETOrgName =N'" + gETOrgName + "', ";
            UserSqlStr = UserSqlStr + " ETOrgGUINo =N'" + gETOrgGUINo + "', ";
            UserSqlStr = UserSqlStr + " ETOrgAddr =N'" + gETOrgAddr + "', ";
            UserSqlStr = UserSqlStr + " ETOrgAddr2 =N'" + gETOrgAddr2 + "', ";
            UserSqlStr = UserSqlStr + " ETOrgTel =N'" + gETOrgTel + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo01 =N'" + gETTCNo01 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo02 =N'" + gETTCNo02 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo03 =N'" + gETTCNo03 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo04 =N'" + gETTCNo04 + "', ";
            UserSqlStr = UserSqlStr + " ETTCNo05 =N'" + gETTCNo05 + "', ";
            UserSqlStr = UserSqlStr + " ETOrgIssNo =N'" + gETOrgIssNo + "', ";
            UserSqlStr = UserSqlStr + " TCNo01ED =N'" + gTCNo01ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo02ED =N'" + gTCNo02ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo03ED =N'" + gTCNo03ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo04ED =N'" + gTCNo04ED + "', ";
            UserSqlStr = UserSqlStr + " TCNo05ED =N'" + gTCNo05ED + "', ";
            UserSqlStr = UserSqlStr + " SYSID ='" + gSYSID + "', ";
            UserSqlStr = UserSqlStr + " ETIdentity =N'" + gIdentity + "', ";
            UserSqlStr = UserSqlStr + " saveuser = '" + NewAccount + "', ";
            UserSqlStr = UserSqlStr + " savedate = getdate() ";
            UserSqlStr = UserSqlStr + " Where ETIDNo = '" + NewAccount + "'";

            using (SqlConnection SWCConn = new SqlConnection(connectionString.ConnectionString))
            {
                SWCConn.Open();

                SqlCommand objCmdUser = new SqlCommand(UserSqlStr, SWCConn);
                objCmdUser.ExecuteNonQuery();
                objCmdUser.Dispose();
            }

            //上傳檔案…
            UpLoadTempFileMoveChk(NewAccount);

            SaveGuildData(NewAccount);
        }
        SaveAccount_Click(sender,e);
        GetUserData(NewAccount);
		GBClass001 CL01 = new GBClass001();
		CL01.Mail_Send(GetMailTo(), MailSub(), MailBody());
        
        Response.Write("<script>alert('已送出帳號申請，請等待審核通知，申請結果將以E-mail通知。'); location.href='SWC000.aspx'; </script>");

    }
    private string MailSub()
    {
        string rValue = "「臺北市水土保持書件管理平台」有新帳號申請";
        return rValue;
    }
    private string MailBody()
    {
        string mName = TXTETName.Text;
        string mName2 = TXTETOrgName.Text;
        string mTel = TXTETTel.Text;

        string rValue = "您好，「臺北市水土保持書件管理平台」有"+ mName + "的新帳號申請單，請系統管理員前往「坡地管理資料庫 - 帳號管理」進行審核。<br><br>";
        rValue = rValue + "申請人："+ mName+ "<br>";
        rValue = rValue + "執業機構名稱：" + mName2 + "<br>";
        rValue = rValue + "手機：" + mTel + "<br><br>";
        rValue = rValue + "「臺北市水土保持書件管理平台」系統管理員 敬上<br><br><br>";
        rValue = rValue + "＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞";
        
        return rValue;
    }
    private string[] GetMailTo()
    {
        string mailStr = "";
        mailStr += TXTETEmail.Text + ";;";

        string sqlStr = " select * from geouser where department = '審查管理科' and status = '正常'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        #region
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = sqlStr;
                #region.設定值
                //cmd.Parameters.Add(new SqlParameter("@SWC000", v));
                #endregion
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            string tmpMail = readerTslm["email"] + "";
                            string tmpUID = readerTslm["userid"] + "";
                            string tmpMailGroup1 = readerTslm["MailGroup1"] + "";

                            //42.大地處開通技師帳號時寄信。
                            //2021-01-26 名單:申請人、黃凱暉(ge-40732)、許巽舜(gv-hsun)
                            //2022-05-05 名單:申請人、系統相關通知收信窗口群組
                            if (tmpMailGroup1.Trim()== "Y")
                                mailStr += tmpMail + ";;";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        string[] rValue = mailStr.Split(new string[] { ";;" }, StringSplitOptions.None);
        #endregion
        return rValue;
    }
    private void UpLoadTempFileMoveChk(string CaseId)
    {
        Boolean folderExists;

        string[] arryUpLoadField = new string[] { "TXTETTCNo01", "TXTETTCNo02", "TXTETTCNo03", "TXTETTCNo04", "TXTETTCNo05" };
        TextBox[] arryUpLoadAppoj = new TextBox[] { TXTETTCNo01, TXTETTCNo02, TXTETTCNo03, TXTETTCNo04, TXTETTCNo05 };
        string csUpLoadField = "TXTETTCNo01";
        TextBox csUpLoadAppoj = TXTETTCNo01;

        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string UserCaseFolderPath = ConfigurationManager.AppSettings["UserFilePath20"];

        folderExists = Directory.Exists(UserCaseFolderPath);
        if (folderExists == false)
        {
            Directory.CreateDirectory(UserCaseFolderPath);
        }

        folderExists = Directory.Exists(UserCaseFolderPath + CaseId);
        if (folderExists == false)
        {
            Directory.CreateDirectory(UserCaseFolderPath + CaseId);
        }

        for (int i = 0; i < arryUpLoadField.Length; i++)
        {
            csUpLoadField = arryUpLoadField[i];
            csUpLoadAppoj = arryUpLoadAppoj[i];

            if (Session[csUpLoadField] + "" == "有檔案")
            {
                Boolean fileExists;
                string TempFilePath = TempFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;
                string SwcCaseFilePath = UserCaseFolderPath + CaseId + "\\" + csUpLoadAppoj.Text;

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
        #region-
        string tAddFile = Session["AddGuildGVFile"] + "";
        string[] arrayAddFiles = tAddFile.Split(new string[] { ";" }, StringSplitOptions.None);

        for (int i = 0; i < arrayAddFiles.Length; i++)
        {
            string tmpFile = arrayAddFiles[i];

            Boolean fileExists;
            string TempFilePath = TempFolderPath + CaseId + "\\" + tmpFile;
            string SwcCaseFilePath = UserCaseFolderPath + CaseId + "\\" + tmpFile;

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
        Session["AddGuildGVFile"] = "";
        #endregion

    }

    private void FileUpLoadApp(string ChkType, FileUpload UpLoadBar, TextBox UpLoadText, string UpLoadStr, string UpLoadType, System.Web.UI.WebControls.Image UpLoadView, HyperLink UpLoadLink,int MaxSize)
    {
        GBClass001 MyBassAppPj = new GBClass001();
        string SwcFileName = "";
        string CaseId = TXTETIDNo.Text + "";

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
                case "PDF":
                    List<string> allowedExtextsion02 = new List<string> { ".pdf" };

                    if (allowedExtextsion02.IndexOf(extension) == -1)
                    {
                        error_msg.Text = MyBassAppPj.AlertMsg("請選擇 PDF 檔案格式上傳，謝謝!!");
                        return;
                    }
                    break;

            }

            // 限制檔案大小，限制為 10MB
            int filesize = UpLoadBar.PostedFile.ContentLength;

            if (filesize > MaxSize * 1000000)
            {
                error_msg.Text = MyBassAppPj.AlertMsg("請選擇 "+ MaxSize + "Mb 以下檔案上傳，謝謝!!");
                return;
            }

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            string serverDir = ConfigurationManager.AppSettings["SwcFileTemp20"] + CaseId;

            if (Directory.Exists(serverDir) == false) Directory.CreateDirectory(serverDir);

            Session[UpLoadStr] = "有檔案";
            //SwcFileName = CaseId + UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
            SwcFileName = UpLoadType + System.IO.Path.GetExtension(UpLoadBar.FileName);
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
                        UpLoadView.Attributes.Add("src", ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + CaseId + "/" + SwcFileName + "?ts=" + System.DateTime.Now.Millisecond);
                        //UpLoadView.ImageUrl = "..\\UpLoadFiles\\temp\\" + CaseId +"\\"+ geohfilename;

                        imagestitch(UpLoadView, serverDir + "\\" + SwcFileName, 320, 180);
                        break;

                    case "PDF":
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
        {
            Session[UpLoadStr] = "";
        }

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
        string csCaseID = TXTSYSID.Text + "";
        string csCaseID2 = TXTETIDNo.Text + "";
        string strSQLClearFieldValue = "";

        //從資料庫取得資料

        //ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        //using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))

        ConnectionStringSettings settingGeoUser = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        SqlConnection ConnERR = new SqlConnection();
        ConnERR.ConnectionString = settingGeoUser.ConnectionString;
        ConnERR.Open();

        strSQLClearFieldValue = " update ETUsers set ";
        strSQLClearFieldValue = strSQLClearFieldValue + DelFieldValue + "='' ";
        strSQLClearFieldValue = strSQLClearFieldValue + " where SYSID = '" + csCaseID + "' ";

        SqlCommand objCmdRV = new SqlCommand(strSQLClearFieldValue, ConnERR);
        objCmdRV.ExecuteNonQuery();
        objCmdRV.Dispose();

        ConnERR.Close();

        //刪實體檔
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp20"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath20"];

        string DelFileName = ImgText.Text;
        string TempFileFullPath = TempFolderPath + csCaseID + "\\" + ImgText.Text;
        string FileFullPath = SwcCaseFolderPath + csCaseID2 + "\\" + ImgText.Text;

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
                ImgView.Attributes.Clear();
                ImgView.ImageUrl = "";
                ImgView.Width = NoneWidth;
                ImgView.Height = NoneHeight;
                break;
            case "DOC":
                FileLink.Text = "";
                FileLink.NavigateUrl = "";
                FileLink.Visible = false;
                break;
        }
        //畫面顯示、值的處理
        ImgText.Text = "";
        Session[AspxFeildId] = "";

    }
    protected void TXTETTCNo01_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC", TXTETTCNo01_FileUpload, TXTETTCNo01, "TXTETTCNo01", "_" + rID + "_photo1", TXTETTCNo01_img, null,10);
    }

    protected void TXTETTCNo01_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo01, TXTETTCNo01_img, null, "TCNo01ED", "TXTETTCNo01", 0, 0);
    }

    protected void TXTETTCNo02_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTETTCNo02_FileUpload, TXTETTCNo02, "TXTETTCNo02", "_" + rID + "_photo2", TXTETTCNo02_img, null, 10);
    }

    protected void TXTETTCNo02_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo02, TXTETTCNo02_img, null, "TCNo02ED", "TXTETTCNo02", 0, 0);
    }

    protected void TXTETTCNo03_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";
        error_msg.Text = "";
        FileUpLoadApp("PIC", TXTETTCNo03_FileUpload, TXTETTCNo03, "TXTETTCNo03", "_" + rID + "_photo3", TXTETTCNo03_img, null, 10);
    }

    protected void TXTETTCNo03_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";
        DeleteUpLoadFile("PIC", TXTETTCNo03, TXTETTCNo03_img, null, "TCNo03ED", "TXTETTCNo03", 0, 0);
    }

    protected void TXTETTCNo04_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC", TXTETTCNo04_FileUpload, TXTETTCNo04, "TXTETTCNo04", "_" + rID + "_photo4", TXTETTCNo04_img, null, 10);

    }

    protected void TXTETTCNo04_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";

        DeleteUpLoadFile("PIC", TXTETTCNo04, TXTETTCNo04_img, null, "TCNo04ED", "TXTETTCNo04", 0, 0);
    }

    protected void TXTETTCNo05_fileuploadok_Click(object sender, EventArgs e)
    {
        string rID = TXTSYSID.Text + "";

        error_msg.Text = "";

        FileUpLoadApp("PIC", TXTETTCNo05_FileUpload, TXTETTCNo05, "TXTETTCNo05", "_" + rID + "_photo5", TXTETTCNo05_img, null, 10);

    }
    protected void TXTETTCNo05_fileuploaddel_Click(object sender, EventArgs e)
    {
        error_msg.Text = "";

        DeleteUpLoadFile("PIC", TXTETTCNo05, TXTETTCNo05_img, null, "TCNo05ED", "TXTETTCNo05", 0, 0);
    }

    protected void FileUpLoad_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string SendId = btn.ID.ToString();
        string UserId = TXTETIDNo.Text;

        #region-預設值
        FileUpload oUpLoadBar = null;
        TextBox oUpLoadTxt = null;
        System.Web.UI.WebControls.Image oShowImg = null;
        HyperLink oFileLink = null;
        string fFileType = "DOC";
        string fSSReMark = "";
        string fReName = "";
        int fMaxSize = 10;
        string GuildFileCount = Convert.ToInt16(GuildCount.Text)+1.ToString();        
        #endregion

        switch (SendId) {
            case "GuildFile_UpLoad":
                fFileType = "PIC";
                fMaxSize = 10;
                oUpLoadBar = GuildFile_FileUpload;
                oUpLoadTxt = TxtGuildFile;
                fSSReMark = "TxtGuildFile";
                oShowImg = GuildImage;
                fReName = UserId + "_Guild_" + GuildFileCount.PadLeft(3,'0');
                break;
            case "Experience_UpLoad":
                fFileType = "PDF";
                fMaxSize = 50;
                oUpLoadBar = Experience_FileUpload;
                oUpLoadTxt = TxtExperience;
                fSSReMark = "TxtExperience";
                oFileLink = ExperienceLink;
                fReName = UserId + "_Experience_" + GuildFileCount.PadLeft(3, '0');
                break;
        }

        string rID = TXTSYSID.Text + "";
        error_msg.Text = "";
        FileUpLoadApp(fFileType, oUpLoadBar, oUpLoadTxt, fSSReMark, fReName, oShowImg, oFileLink, fMaxSize);

    }

    protected void ADDLIST_Click(object sender, EventArgs e)
    {
        GBClass001 SBApp = new GBClass001();
        
        string tmpNo = TXTETIDNo.Text;
        string UpLoadTempFilePath = "..\\UpLoadFiles\\temp\\";
        string tOwnGuild = DDLGuild.SelectedItem.Value;
        string tOwnGuiName = DDLGuild.SelectedItem.Text;
        string tGuildFile = TxtGuildFile.Text + "";
        string tGuildFileLink = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/temp/" + tmpNo+"/" + tGuildFile; //GuildImage.ImageUrl;
        string tExperience = TxtExperience.Text + "";
        string tExperienceText = "";
        string tExperienceLink = ExperienceLink.NavigateUrl;
        string tEXP = TXTEXP.Text + "";
        string tGuildSelect = "";
        string tADDFile = Session["AddGuildGVFile"] +"";

        #region-基本檢查
        tGuildSelect = Session["GuildSelect"] + "";
        if (tOwnGuild.Trim()=="") { Response.Write("<script>alert('請選擇公會名稱');</script>"); return; }
        if(tGuildSelect.IndexOf(tOwnGuild) >= 0) { Response.Write("<script>alert('您所選擇之公會資料已存在，請選擇其他公會。');</script>"); return; }
        if (tGuildFile.Trim() == "") { Response.Write("<script>alert('請上傳公會會員證書');</script>"); return; }
        if (tEXP.Trim() == "") { Response.Write("<script>alert('請選擇有效日期');</script>"); return; }
        if (SBApp.IsDate(tEXP.Trim())) { if (Convert.ToDateTime(tEXP.Trim()) < DateTime.Now) { Response.Write("<script>alert('請選擇正確的有效日期');</script>"); return; } }else { Response.Write("<script>alert('請選擇正確的有效日期');</script>"); return; }
        if (tExperience.Trim() != "") tExperienceText = "審查經歷";
        #endregion

        GuildCount.Text = (Convert.ToInt32(GuildCount.Text) + 1).ToString();

        DataTable tbGuild = (DataTable)ViewState["OwnGuild"];

        if (tbGuild==null)
        {
            DataTable GVGuild = new DataTable();

            GVGuild.Columns.Add(new DataColumn("NI", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("GuildName", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("GuildId", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("GuildText", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("GuildFileName", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("GuildFileLink", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("ExperienceText", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("ExperienceFileName", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("ExperienceFileLink", typeof(string)));
            GVGuild.Columns.Add(new DataColumn("EXPDate", typeof(string)));

            ViewState["OwnGuild"] = GVGuild;
            tbGuild = (DataTable)ViewState["OwnGuild"];
        }

        DataRow GVGuildRow = tbGuild.NewRow();

        GVGuildRow["NI"] = GuildCount.Text;
        GVGuildRow["GuildName"] = tOwnGuiName;
        GVGuildRow["GuildId"] = tOwnGuild;
        GVGuildRow["GuildText"] = "公會證書";
        GVGuildRow["GuildFileName"] = tGuildFile;
        GVGuildRow["GuildFileLink"] = tGuildFileLink;
        GVGuildRow["ExperienceText"] = tExperienceText;
        GVGuildRow["ExperienceFileName"] = tExperience;
        GVGuildRow["ExperienceFileLink"] = tExperienceLink;
        GVGuildRow["EXPDate"] = tEXP;

        tbGuild.Rows.Add(GVGuildRow);
        ViewState["OwnGuild"] = tbGuild;
        tADDFile += ";"+ tGuildFile+";"+ tExperience;

        Session["AddGuildGVFile"] = tADDFile;
        GuildList.DataSource = tbGuild;
        GuildList.DataBind();

        #region-清空畫面
        Session["GuildSelect"] += tOwnGuild + ";";

        DDLGuild.Text = "";
        TxtGuildFile.Text = "";
        GuildImage.Attributes.Clear();
        GuildImage.ImageUrl = "";
        GuildImage.Width = 0;
        GuildImage.Height = 0;
        TxtExperience.Text = "";
        ExperienceLink.NavigateUrl = "";
        ExperienceLink.Text = "";
        TXTEXP.Text = "";
        #endregion
    }

    protected void GuildList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ExcAction = e.CommandName;

        switch (ExcAction)
        {
            case "DelGuild":
                int aa = Convert.ToInt32(e.CommandArgument);
                int number = Convert.ToInt32(GuildList.Rows[aa].Cells[0].Text);
                string tmpGuild = ";" + Session["GuildSelect"] + "";
                string tmpGuildFile = Session["AddGuildGVFile"] + ";";
                DataTable VS_GV1 = (DataTable)ViewState["OwnGuild"];
                string delGuildId = VS_GV1.Rows[aa]["GuildId"].ToString().Trim();
                string delGuildFile1 = VS_GV1.Rows[aa]["GuildFileName"].ToString().Trim();
                string delGuildFile2 = VS_GV1.Rows[aa]["ExperienceFileName"].ToString().Trim();
                VS_GV1.Rows.RemoveAt(aa);
                tmpGuild = tmpGuild.Replace(";" + delGuildId + ";", ";").Replace(";;",";");
                Session["GuildSelect"] = tmpGuild;
                ViewState["OwnGuild"] = VS_GV1;
                tmpGuildFile = tmpGuildFile.Replace(";" + delGuildFile1 + ";", ";").Replace(";" + delGuildFile2 + ";", ";").Replace(";" + tmpGuildFile + ";", ";").Replace(";;", ";");
                Session["AddGuildGVFile"] = tmpGuildFile;
                GuildList.DataSource = VS_GV1;
                GuildList.DataBind();

                break;
        }
    }

    protected void FileDel_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string SendId = btn.ID.ToString();
        string UserId = TXTETIDNo.Text;
        string tAct = "Clear";
        string tFileName = "";
        string tFileType = "";

        #region-
        TextBox oUpLoadTxt = null;
        System.Web.UI.WebControls.Image oShowImg = null;
        HyperLink oFileLink = null;
        #endregion

        switch (SendId)
        {
            case "GuildFile_FileDel":
                tAct = "";
                tFileName = TxtGuildFile.Text+"";
                oShowImg = GuildImage;
                oUpLoadTxt = TxtGuildFile;
                tFileType = "PIC";
                break;
            case "Experience_FileDel":
                tAct = "";
                tFileName = TxtExperience.Text + "";
                oFileLink = ExperienceLink;
                oUpLoadTxt = TxtExperience;
                tFileType = "DOC";
                break;

        }

        //刪資料庫
        if (tAct == "Clear") { }

        //刪實體檔
        DeleteUpLoadFile2(tFileName,"");

        #region-清空畫面
        oUpLoadTxt.Text = "";
        switch (tFileType) { case "PIC":
        oShowImg.Attributes.Clear();
        oShowImg.ImageUrl = "";
        oShowImg.Width = 0;
        oShowImg.Height = 0;
                break;
            case "DOC":
        oFileLink.NavigateUrl = "";
        oFileLink.Text = "";
                break;
        }
        #endregion
        
    }
    #region-刪檔案
    private void DeleteUpLoadFile2(string DelFileName,string FileDocPath)
    {
        string TempFolderPath = ConfigurationManager.AppSettings["SwcFileTemp"];
        string SwcCaseFolderPath = ConfigurationManager.AppSettings["SwcFilePath"];
        string delTempFile= TempFolderPath + FileDocPath + DelFileName;
        string delRealFile = SwcCaseFolderPath + FileDocPath + DelFileName;

        if (FileDocPath !="") {
        try { if (File.Exists(delTempFile)) File.Delete(delTempFile); } catch { }
        try { if (File.Exists(delRealFile)) File.Delete(delRealFile); } catch { }
        }
    }
    #endregion

    protected void GuildList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.Header: 
                break;
            case DataControlRowType.DataRow:
                Button BtnDel = (Button)e.Row.Cells[5].FindControl("BtnDel");
                HiddenField HFID = (HiddenField)e.Row.Cells[5].FindControl("GuildID");
                string vGV = HFID.Value;
                string vGVChk = TextBoxGD01Chk.Text;
                string vGD1 = TextBoxGD01.Text;
                string vGD2 = TextBoxGD02.Text;
                if ((vGV== vGD1 && vGVChk == "1") || vGV == vGD2) { BtnDel.Visible = false; }
                break;
        }

    }
}