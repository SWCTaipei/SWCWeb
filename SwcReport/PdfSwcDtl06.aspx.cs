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

public partial class SwcReport_PdfSwcDtl06 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenPdf();
    }
    private void GenPdf()
    {
        GBClass001 SBApp = new GBClass001();

        

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/DTL006.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        string ExeSqlStr = " select SWC.*,D6.* from SWCDTL06 D6 ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON D6.SWC000=SWC.SWC000 ";
        ExeSqlStr = ExeSqlStr + " where D6.SWC000 = '" + rCaseId + "' ";
        ExeSqlStr = ExeSqlStr + "   and D6.DTLF000 = '" + rDTLId + "' ";

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //資料開始

        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(ExeSqlStr, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {

                string tSWC005 = readeSwc["SWC005"] + "";
                string tSWC007 = readeSwc["SWC007"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC021 = readeSwc["SWC021"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";
                string tSWC058 = readeSwc["SWC058"] + "";

                string tDTLF001 = readeSwc["DTLF001"] + "";
                string tDTLF002 = readeSwc["DTLF002"] + "";
                string tDTLF003 = readeSwc["DTLF003"] + "";
                string tDTLF004 = readeSwc["DTLF004"] + "";
                string tDTLF023 = readeSwc["DTLF023"] + "";
                string tDTLF024 = readeSwc["DTLF024"] + "";
                string tDTLF025 = readeSwc["DTLF025"] + "";
                string tDTLF026 = readeSwc["DTLF026"] + "";
                string tDTLF027 = readeSwc["DTLF027"] + "";
                string tDTLF028 = readeSwc["DTLF028"] + "";
                string tDTLF029 = readeSwc["DTLF029"] + "";
                string tDTLF030 = readeSwc["DTLF030"] + "";
                string tDTLF031 = readeSwc["DTLF031"] + "";
                string tDTLF032 = readeSwc["DTLF032"] + "";
                string tDTLF033 = readeSwc["DTLF033"] + "";
                string tDTLF034 = readeSwc["DTLF034"] + "";
                string tDTLF035 = readeSwc["DTLF035"] + "";
                string tDTLF036 = readeSwc["DTLF036"] + "";
                string tDTLF037 = readeSwc["DTLF037"] + "";
                string tDTLF038 = readeSwc["DTLF038"] + "";
                string tDTLF039 = readeSwc["DTLF039"] + "";
                string tDTLF040 = readeSwc["DTLF040"] + "";
                string tDTLF041 = readeSwc["DTLF041"] + "";
                string tDTLF042 = readeSwc["DTLF042"] + "";

                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //0.檢查日期
                string Year002 = " " + Convert.ToString(Convert.ToDateTime(tDTLF002).Year - 1911);
                string Month002 = " " + Convert.ToString(Convert.ToDateTime(tDTLF002).Month);
                string Day002 = " " + Convert.ToString(Convert.ToDateTime(tDTLF002).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tDTLF002).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tDTLF002).Month) == "1" && Convert.ToString(Convert.ToDateTime(tDTLF002).Day) == "1")
                {
                    Year002 = " ";
                    Month002 = " ";
                    Day002 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL002a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL002a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL002b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL002b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL002c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL002c");

                //1.案件編號
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC002");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF001, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC002");

                //2.計畫名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC005");

                //3.水保與簡易水保
                string dwSwc007a = "□";
                string dwSwc007b = "□";
                if (tSWC007 == "水土保持計畫") { dwSwc007a = "■"; } else { dwSwc007b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC007a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC007a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC007b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC007b");

                //4.核定日期文號
                string Year038 = " " + Convert.ToString(Convert.ToDateTime(tSWC038).Year - 1911);
                string Month038 = " " + Convert.ToString(Convert.ToDateTime(tSWC038).Month);
                string Day038 = " " + Convert.ToString(Convert.ToDateTime(tSWC038).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tSWC038).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC038).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC038).Day) == "1")
                {
                    Year038 = " ";
                    Month038 = " ";
                    Day038 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC038a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC038a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC038b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC038b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC038c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC038c");

                string swc039num = System.Text.RegularExpressions.Regex.Replace(tSWC039, @"[^0-9]+", ""); ;//取出數字

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC039");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(swc039num, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC039");

                //5.施工許可證日期文號
                string Year043 = " " + Convert.ToString(Convert.ToDateTime(tSWC043).Year - 1911);
                string Month043 = " " + Convert.ToString(Convert.ToDateTime(tSWC043).Month);
                string Day043 = " " + Convert.ToString(Convert.ToDateTime(tSWC043).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tSWC043).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC043).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC043).Day) == "1")
                {
                    Year043 = " ";
                    Month043 = " ";
                    Day043 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC043a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC043a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC043b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC043b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC043c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC043c");

                string swc044num = System.Text.RegularExpressions.Regex.Replace(tSWC044, @"[^0-9]+", ""); ;//取出數字

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC044");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(swc044num, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC044");

                //6.開工日期
                string Year051 = " " + Convert.ToString(Convert.ToDateTime(tSWC051).Year - 1911);
                string Month051 = " " + Convert.ToString(Convert.ToDateTime(tSWC051).Month);
                string Day051 = " " + Convert.ToString(Convert.ToDateTime(tSWC051).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tSWC051).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC051).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC051).Day) == "1")
                {
                    Year051 = " ";
                    Month051 = " ";
                    Day051 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC051a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC051a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC051b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC051b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC051c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC051c");


                //7.核定完工日期
                string Year052 = " " + Convert.ToString(Convert.ToDateTime(tSWC052).Year - 1911);
                string Month052 = " " + Convert.ToString(Convert.ToDateTime(tSWC052).Month);
                string Day052 = " " + Convert.ToString(Convert.ToDateTime(tSWC052).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tSWC052).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC052).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC052).Day) == "1")
                {
                    Year052 = " ";
                    Month052 = " ";
                    Day052 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC052a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC052a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC052b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC052b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC052c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC052c");


                //8.完工申報日期
                string Year058 = " " + Convert.ToString(Convert.ToDateTime(tSWC058).Year - 1911);
                string Month058 = " " + Convert.ToString(Convert.ToDateTime(tSWC058).Month);
                string Day058 = " " + Convert.ToString(Convert.ToDateTime(tSWC058).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tSWC058).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC058).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC058).Day) == "1")
                {
                    Year058 = " ";
                    Month058 = " ";
                    Day058 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC058a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year058, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC058a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC058b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month058, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC058b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC058c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day058, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC058c");

                //9.姓名或名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC013");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC013");

                //10.身分證或營利事業統一編號
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC013ID");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013ID, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC013ID");

                //11.住居所或營業處
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC014");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC014");

                //12.姓名
                ps = Pdfstamper.AcroFields.GetFieldPositions("ETName");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC045, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ETName");

                //13.執業機構名稱
                string orgname = SBApp.GetETUser(tSWC045ID, "OrgName");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgName");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgname, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ETOrgName");

                //14.執業執照字號
                string orgissno = SBApp.GetETUser(tSWC045ID, "OrgIssNo");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgIssNo");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgissno, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ETOrgIssNo");

                //15.營利事業統一編號
                string orgguino = SBApp.GetETUser(tSWC045ID, "OrgGUINo");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgGUINo");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgguino, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ETOrgGUINo");

                //16.電話
                string orgtel = SBApp.GetETUser(tSWC045ID, "OrgTel");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgTel");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgtel, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ETOrgTel");

                //17.實施地點土地標示
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL023");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF023, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_MIDDLE);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL023");

                //18.完工抽驗項目
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL024");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF024, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL024");

                //19.實施與計畫或規定不符之限期改正期限
                //string dtl025 = tDTLF025.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL025");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF025, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL025");

                //20.其他注意事項
                //string dtl026 = tDTLF026.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL026");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF026, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL026");

                //21.檢查結果
                string chk027a = "□";
                string chk027b = "□";
                if (tDTLF027 == "已達完工標準") {chk027a = "■";} else { chk027b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL027a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(chk027a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL027a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL027b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(chk027b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL027b");

                //string dtl028 = tDTLF028.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL028");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF028, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL028");



                //    //23~28.相片說明文字
                //    //string[] arrayPicRemark = new string[] { tDTLG061, tDTLG063, tDTLG064, tDTLG066, tDTLG067, tDTLG069, tDTLG070, tDTLG072, tDTLG073, tDTLG075, tDTLG076, tDTLG078 };
                //    //string[] arrayPDFView05 = new string[] { "DTL061", "DTL063", "DTL064", "DTL066", "DTL067", "DTL069", "DTL070", "DTL072", "DTL073", "DTL075", "DTL076", "DTL078" };
                string[] arrayPicRemark = new string[] { tDTLF031, tDTLF033, tDTLF035, tDTLF037, tDTLF039, tDTLF041};
                string[] arrayPDFView05 = new string[] { "DTL031", "DTL033", "DTL035", "DTL037", "DTL039", "DTL041"};

                for (int i = 0; i < arrayPicRemark.Length; i++)
                {
                    string aPicRemark = arrayPicRemark[i] + "";
                    string aPdfView05 = arrayPDFView05[i] + "";

                    ps = Pdfstamper.AcroFields.GetFieldPositions(aPdfView05);
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(aPicRemark, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField(aPdfView05);
                }

                //以下處理圖片
                iTextSharp.text.Image pdfimageobj;
                float x = 0;
                float y = 0;
				SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/";

                //加圖片1進去, A4 大小 寬:0~595 高:0~842
                if (tDTLF030.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF030;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(165 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(670 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片2進去
                if (tDTLF032.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF032;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(409 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(670 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片3進去
                if (tDTLF034.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF034;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(165 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(444.5 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片4進去
                if (tDTLF036.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF036;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(409 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(444.5 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片5進去
                if (tDTLF038.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF038;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(165 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(224 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片6進去
                if (tDTLF040.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF040;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(409 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(224 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
        }

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