using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BaseData_PPayBillSM01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string rSWC000 = Request.QueryString["SWCNO"] + "";
        string rONA001 = Request.QueryString["OLANO"] + "";

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/水土保持施工許可證.pdf"));
        string pdfnewname = "水土保持施工許可證_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //資料開始
        GBClass001 CL01 = new GBClass001();
        string sSWC002 = "", sSWC005 = "", sSWC013="", sSWC013ID="", sSWC014 = "", sSWC021ID="", sSWC038="", sSWC039="", sSWC043 = "", sSWC045ID="";
        string sET01 = "", sET02 = "", sET03 = "", sET04 = "", sET05 = "", sET01a = "", sET02a = "", sET03a = "", sET04a = "", sET05a = "";

        string qStr1 = " select * from SWCSWC where SWC00='"+ rSWC000 + "';";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = qStr1;
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            sSWC002 = readerTslm["SWC02"] + "";
                            sSWC005 = readerTslm["SWC05"] + "";
                            sSWC013 = readerTslm["SWC13"] + "";
                            sSWC013ID = readerTslm["SWC013ID"] + "";
                            sSWC013ID = sSWC013ID.Length > 5 ? sSWC013ID.Substring(0, sSWC013ID.Length-4) +"****" : "****";
                            sSWC014 = readerTslm["SWC14"] + "";
                            sSWC038 = readerTslm["SWC38"] + "";
                            sSWC039 = readerTslm["SWC39"] + "";
                            sSWC043 = readerTslm["SWC43"] + "";
                            sSWC021ID = readerTslm["SWC021ID"] + "";
                            sSWC045ID = readerTslm["SWC045ID"] + "";
                            sSWC038 = CL01.DateView(sSWC038, "00");
                            sET01 = CL01.GetETUser(sSWC045ID, "Name");
                            sET02 = CL01.GetETUser(sSWC045ID, "OrgName");
                            sET03 = CL01.GetETUser(sSWC045ID, "OrgAddr");
                            sET04 = CL01.GetETUser(sSWC045ID, "OrgGUINo");
                            sET05 = CL01.GetETUser(sSWC045ID, "ETTel");
                            sET01a = CL01.GetETUser(sSWC021ID, "Name");
                            sET02a = CL01.GetETUser(sSWC021ID, "OrgName");
                            sET03a = CL01.GetETUser(sSWC021ID, "OrgAddr");
                            sET04a = CL01.GetETUser(sSWC021ID, "OrgGUINo");
                            sET05a = CL01.GetETUser(sSWC021ID, "ETTel");
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        string addr1 = "", addr2 = "", addr3 = "", addr4 = "", addr5="";
        string qStr2 = " select top 1 * from [relationLand] where 行政管理案件編號='"+ sSWC002 + "' order by 流水號 ;";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = qStr2;
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            addr1 = readerTslm["區"] + "";
                            addr2 = readerTslm["段"] + "";
                            addr3 = readerTslm["小段"] + "";
                            addr5 = readerTslm["地號"] + "";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        string qStr3 = " select count(*) as ddr4 from [relationLand] where 行政管理案件編號='" + sSWC002 + "';";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = qStr3;
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            addr4 = readerTslm["ddr4"] + "";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        string cf1 = "", cf2 = "", cf3 = "", cf4 = "", cf5="", C414 = "";
        string qStr4 = " select * from OnlineApply04chk where SWC000='"+ rSWC000 + "' and ONA04001='"+ rONA001 + "';";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = qStr4;
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            C414 = readerTslm["OA4C414"] + "";
                            cf1 = readerTslm["OA4CF01"] + "";
                            cf2 = readerTslm["OA4CF02"] + "";
                            cf3 = readerTslm["OA4CF03"] + "";
                            cf4 = readerTslm["OA4CF04"] + "";
                            cf5 = readerTslm["OA4CF05"] + "";
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        string tONA003 = "", tONA004 = "";
        string qStr5 = " select * from TCGESWC.dbo.OnlineApply04 where SWC000='" + rSWC000 + "' and ONA04001='" + rONA001 + "';";
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = qStr5;
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            tONA003 = readerTslm["ONA04003"] + "";
                            tONA004 = readerTslm["ONA04004"] + "";
                            tONA003 = CL01.DateView(tONA003, "00");
                            tONA004 = CL01.DateView(tONA004, "00");
                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }
        string tR004 = "", tR004y = "" ,tR004m = "" ,tR004d = ""; tR004 = sSWC043;
        tR004y = (Convert.ToDateTime(tR004).Year - 1911).ToString();
        tR004m = Convert.ToDateTime(tR004).Month.ToString();
        tR004d = Convert.ToDateTime(tR004).Day.ToString();

        #region 不重要的地方 20200727
        //string qStr6 = " select* from SignRCD where SWC000 = '"+ rSWC000 + "' and ONA001 = '"+ rONA001 + "' and R003 = '決行';";
        //using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        //{
        //    TslmConn.Open();
        //    using (var cmd = TslmConn.CreateCommand())
        //    {
        //        cmd.CommandText = qStr6;
        //        cmd.ExecuteNonQuery();
        //        using (SqlDataReader readerTslm = cmd.ExecuteReader())
        //        {
        //            if (readerTslm.HasRows)
        //                while (readerTslm.Read())
        //                {
        //                    tR004 = readerTslm["R004"] + "";
        //                    tR004 = CL01.DateView(tR004, "00");
        //                    if (tR004 != "") {
        //                        tR004y = (Convert.ToDateTime(tR004).Year-1911).ToString();
        //                        tR004m = Convert.ToDateTime(tR004).Month.ToString();
        //                        tR004d = Convert.ToDateTime(tR004).Day.ToString();
        //                    }
        //                }
        //            readerTslm.Close();
        //        }
        //        cmd.Cancel();
        //    }
        //}
        #endregion

        AcroFields.FieldPosition p;
        IList<AcroFields.FieldPosition> ps;
        ColumnText ct;

        //書件名稱
        ps = Pdfstamper.AcroFields.GetFieldPositions("plan");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("plan");

        //核定日期及字號
        ps = Pdfstamper.AcroFields.GetFieldPositions("day01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sSWC038+ sSWC039, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("day01");

        //土地標示-區
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(addr1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD01");

        //土地標示-段
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD02");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(addr2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD02");

        //土地標示-小段
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD03");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(addr3, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD03");

        //土地標示-地號
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD03a");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(addr5, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD03a");

        //土地標示-筆
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD04");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(addr4, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD04");

        //工程摘要
        ps = Pdfstamper.AcroFields.GetFieldPositions("main");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(cf1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("main");

        //核定工程造價
        ps = Pdfstamper.AcroFields.GetFieldPositions("dollor01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(cf2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("dollor01");

        //保證金
        ps = Pdfstamper.AcroFields.GetFieldPositions("dollor02");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(cf3, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("dollor02");

        //義務人-姓名
        ps = Pdfstamper.AcroFields.GetFieldPositions("name01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sSWC013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("name01");

        //義務人-證號
        ps = Pdfstamper.AcroFields.GetFieldPositions("number01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sSWC013ID, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("number01");

        //義務人-居所
        ps = Pdfstamper.AcroFields.GetFieldPositions("home01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sSWC014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("home01");

        //承辦技師-姓名
        ps = Pdfstamper.AcroFields.GetFieldPositions("name02"); 
         p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET01a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("name02");

        //承辦技師-機構
        ps = Pdfstamper.AcroFields.GetFieldPositions("company01");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET02a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("company01");

        //承辦技師-地址
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD05");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET03a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD05");

        //承辦技師-統編
        ps = Pdfstamper.AcroFields.GetFieldPositions("number02");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET04a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("number02");

        //承辦技師-電話
        ps = Pdfstamper.AcroFields.GetFieldPositions("phone1");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET05a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("phone1");

        //監造技師-姓名
        ps = Pdfstamper.AcroFields.GetFieldPositions("name03");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET01, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("name03");

        //監造技師-機構
        ps = Pdfstamper.AcroFields.GetFieldPositions("company02");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET02, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("company02");

        //監造技師-地址
        ps = Pdfstamper.AcroFields.GetFieldPositions("AD06");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET03, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("AD06");

        //監造技師-統編
        ps = Pdfstamper.AcroFields.GetFieldPositions("number03");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET04, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("number03");

        //監造技師-電話
        ps = Pdfstamper.AcroFields.GetFieldPositions("phone2");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sET05, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("phone2");

        //目的事業主管機關核發開發或許可文件
        ps = Pdfstamper.AcroFields.GetFieldPositions("agree");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(cf4, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("agree");

        //繳費日期
        ps = Pdfstamper.AcroFields.GetFieldPositions("money");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(cf5, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("money");

        ////完工
        //ps = Pdfstamper.AcroFields.GetFieldPositions("compelet");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA004, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("compelet");

        //開工
        ps = Pdfstamper.AcroFields.GetFieldPositions("start");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA003, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("start");

        //完工
        ps = Pdfstamper.AcroFields.GetFieldPositions("compelet2");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(C414, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("compelet2");


        //決行日-年
        ps = Pdfstamper.AcroFields.GetFieldPositions("year");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tR004y, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("year");

        //決行日-月
        ps = Pdfstamper.AcroFields.GetFieldPositions("mounth");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tR004m, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("mounth");

        //決行日-日
        ps = Pdfstamper.AcroFields.GetFieldPositions("day");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tR004d, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("day");




        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        Pdfstamper.Close();
        Pdfreader.Close();

        //把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        System.IO.Stream iStream;

        // 以10K為單位暫存:
        Byte[] buffer = new byte[10000];
        int length = 0;
        long dataToRead = 0;

        // 制定文件路徑
        string filepath = Server.MapPath("~\\OutputFile\\" + pdfnewname);
        string filepathm = Server.MapPath("~\\OutputFile\\m" + pdfnewname);

        // 得到文件名
        string filename = System.IO.Path.GetFileName(filepath);

        //Try
        // 打開文件
        iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        // 得到文件大小
        dataToRead = iStream.Length;
        Response.ContentType = "application/x-rar-compressed";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));

        while (dataToRead > 0)
        {
            if (Response.IsClientConnected)
            {
                length = iStream.Read(buffer, 0, 10000);
                Response.OutputStream.Write(buffer, 0, length);
                Response.Flush();
                dataToRead = dataToRead - length;
            }
            else
            {
                dataToRead = -1;
            }
        }
        if (iStream.Length != 0)
        {
            //關閉文件
            iStream.Close();
            File.Delete(filepath);
        }
    }
}