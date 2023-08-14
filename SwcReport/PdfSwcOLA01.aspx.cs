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

        string rSWCNO = Request.QueryString["SWCNO02"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/OLA001.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        string ExeSqlStr = " select OA1.*,EU.ETName from OnlineApply01 OA1 ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON OA1.SWC000=SWC.SWC000 ";
        ExeSqlStr += " left JOIN ETUsers EU on OA1.LOCKUSER=EU.ETID ";
        ExeSqlStr = ExeSqlStr + " where OA1.SWC002 = '" + rSWCNO + "' ";
        ExeSqlStr = ExeSqlStr + "   and OA1.ONA01001 = '" + rOLANO + "' ";

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
                string tSWC002 = readeSwc["SWC002"] + "";

                string tONA01002 = readeSwc["ONA01002"] + "";
                string tONA01003 = readeSwc["ONA01003"] + "";
                string tONA01004 = readeSwc["ONA01004"] + "";
                string tONA01005 = readeSwc["ONA01005"] + "";
                string tONA01006 = readeSwc["ONA01006"] + "";
                string tONA01007 = readeSwc["ONA01007"] + "";
                string tONA01008 = readeSwc["ONA01008"] + "";
                string tONA01009 = readeSwc["ONA01009"] + "";
                string tONA01010 = readeSwc["ONA01010"] + "";
                string tONA01011 = readeSwc["ONA01011"] + "";
                string tONA01012 = readeSwc["ONA01012"] + "";
                string tONA01013 = readeSwc["ONA01013"] + "";
                string tONA01014 = readeSwc["ONA01014"] + "";
                string tONA01015 = readeSwc["ONA01015"] + "";
                string tONA01016 = readeSwc["ONA01016"] + "";
                string tONA01017 = readeSwc["ONA01017"] + "";
                string tONA01018 = readeSwc["ONA01018"] + "";
                string tONA01019 = readeSwc["ONA01019"] + "";
                string tONA01020 = readeSwc["ONA01020"] + "";
                string tONA01021 = readeSwc["ONA01021"] + "";
                string tONA01022 = readeSwc["ONA01022"] + "";
                string tONA01023 = readeSwc["ONA01023"] + "";
                string tONA01024 = readeSwc["ONA01024"] + "";
                string tONA01025 = readeSwc["ONA01025"] + "";

                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //1.案件編號
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC002");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC002");

                //2.檢查日期
                string Year002 = " " + Convert.ToString(Convert.ToDateTime(tONA01002).Year - 1911);
                string Month002 = " " + Convert.ToString(Convert.ToDateTime(tONA01002).Month);
                string Day002 = " " + Convert.ToString(Convert.ToDateTime(tONA01002).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tONA01002).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tONA01002).Month) == "1" && Convert.ToString(Convert.ToDateTime(tONA01002).Day) == "1")
                {
                    Year002 = " ";
                    Month002 = " ";
                    Day002 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01002a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01002a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01002b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01002b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01002c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01002c");

                //3.社區(設施)地址
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01003");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01003, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01003");

                //4.義務人(聯絡人)
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01004");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01004, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01004");

                //5.聯絡地址
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01005");

                //6.聯絡電話
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01006");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01006, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01006");

                //7.行動電話
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01007");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01007, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01007");

                ////8.滯洪沉砂設施
                string dwONA008a = "□";
                string dwONA008b = "□";
                if (tONA01008 == "1") { dwONA008a = "■"; } else { dwONA008b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01008a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA008a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01008a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01008b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA008b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01008b");

                //09
                string dwONA009a = "□";
                string dwONA009b = "□";
                if (tONA01009 == "1") { dwONA009a = "■"; } else { dwONA009b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01009a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA009a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01009a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01009b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA009b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01009b");

                //10
                string dwONA010a = "□";
                string dwONA010b = "□";
                if (tONA01010 == "1") { dwONA010a = "■"; } else { dwONA010b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01010a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA010a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01010a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01010b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA010b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01010b");

                //11
                string dwONA011a = "□";
                string dwONA011b = "□";
                if (tONA01011 == "1") { dwONA011a = "■"; } else { dwONA011b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01011a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA011a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01011a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01011b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA011b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01011b");

                //12
                string dwONA012a = "□";
                string dwONA012b = "□";
                if (tONA01012 == "1") { dwONA012a = "■"; } else { dwONA012b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01012a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA012a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01012a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01012b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA012b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01012b");

                //13
                string dwONA013a = "□";
                string dwONA013b = "□";
                if (tONA01013 == "1") { dwONA013a = "■"; } else { dwONA013b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01013a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA013a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01013a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01013b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA013b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01013b");

                //14.
                string dwONA014a = "□";
                string dwONA014b = "□";
                if (tONA01014 == "1") { dwONA014a = "■"; } else { dwONA014b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01014a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA014a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01014a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01014b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA014b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01014b");

                //15.
                string dwONA015a = "□";
                string dwONA015b = "□";
                if (tONA01015 == "1") { dwONA015a = "■"; } else { dwONA015b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01015a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA015a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01015a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01015b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA015b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01015b");

                //16.
                string dwONA016a = "□";
                string dwONA016b = "□";
                if (tONA01016 == "1") { dwONA016a = "■"; } else { dwONA016b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01016a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA016a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01016a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01016b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA016b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01016b");

                //17.
                string dwONA017a = "□";
                string dwONA017b = "□";
                string dwONA017c = "□";
                if (tONA01017 == "1") { dwONA017b = "■"; } else if (tONA01017 == "2") { dwONA017a = "■"; } else { dwONA017c = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01017a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA017a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01017a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01017b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA017b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01017b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01017c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA017c, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01017c");

                //18.
                string dwONA018a = "□";
                string dwONA018b = "□";
                if (tONA01018 == "1") { dwONA018a = "■"; }  else { dwONA018b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01018a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA018a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01018a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01018b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA018b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01018b");

                //19.
                string dwONA019a = "□";
                string dwONA019b = "□";
                if (tONA01019 == "1") { dwONA019a = "■"; } else { dwONA019b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01019a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA019a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01019a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01019b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA019b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01019b");

                //20.
                string dwONA020a = "□";
                string dwONA020b = "□";
                if (tONA01020 == "1") { dwONA020a = "■"; } else { dwONA020b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01020a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA020a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01020a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01020b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA020b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01020b");

                //21.5-1
                string dwONA021a = "□";
                string dwONA021b = "□";
                if (tONA01021 == "1") { dwONA021a = "■"; } else { dwONA021b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01021a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA021a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01021a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01021b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA021b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01021b");

                //22.檢查人員
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01022");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01022, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01022");

                //23.5-2
                string dwONA023a = "□";
                string dwONA023b = "□";
                if (tONA01023 == "1") { dwONA023a = "■"; } else { dwONA023b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01023a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA023a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01023a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01023b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA023b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01023b");

                //24.5-2，日期
                string Year024 = "";
                string Month024 = "";
                string Day024 = "";

                if (tONA01024 == "")
                {
                    Year024 = " ";
                    Month024 = " ";
                    Day024 = " ";
                } else
                {
                    Year024 = " " + Convert.ToString(Convert.ToDateTime(tONA01024).Year - 1911);
                    Month024 = " " + Convert.ToString(Convert.ToDateTime(tONA01024).Month);
                    Day024 = " " + Convert.ToString(Convert.ToDateTime(tONA01024).Day);
                    //判斷日期1900-01-01
                    if (Convert.ToString(Convert.ToDateTime(tONA01024).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tONA01024).Month) == "1" && Convert.ToString(Convert.ToDateTime(tONA01024).Day) == "1")
                    {
                        Year024 = " ";
                        Month024 = " ";
                        Day024 = " ";
                    }
                }
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01024a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month024, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01024a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01024b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day024, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01024b");

                //25.檢查人員
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA01025");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA01025, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA01025");


                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC007a");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC007a");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC007b");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC007b");

                ////4.核定日期文號
                //string Year038 = " " + Convert.ToString(Convert.ToDateTime(tSWC038).Year - 1911);
                //string Month038 = " " + Convert.ToString(Convert.ToDateTime(tSWC038).Month);
                //string Day038 = " " + Convert.ToString(Convert.ToDateTime(tSWC038).Day);
                ////判斷日期1900-01-01
                //if (Convert.ToString(Convert.ToDateTime(tSWC038).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC038).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC038).Day) == "1")
                //{
                //    Year038 = " ";
                //    Month038 = " ";
                //    Day038 = " ";
                //}

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC038a");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC038a");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC038b");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC038b");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC038c");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC038c");

                //string swc039num = System.Text.RegularExpressions.Regex.Replace(tSWC039, @"[^0-9]+", ""); ;//取出數字

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC039");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(swc039num, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC039");

                ////5.施工許可證日期文號
                //string Year043 = " " + Convert.ToString(Convert.ToDateTime(tSWC043).Year - 1911);
                //string Month043 = " " + Convert.ToString(Convert.ToDateTime(tSWC043).Month);
                //string Day043 = " " + Convert.ToString(Convert.ToDateTime(tSWC043).Day);
                ////判斷日期1900-01-01
                //if (Convert.ToString(Convert.ToDateTime(tSWC043).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC043).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC043).Day) == "1")
                //{
                //    Year043 = " ";
                //    Month043 = " ";
                //    Day043 = " ";
                //}

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC043a");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC043a");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC043b");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC043b");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC043c");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC043c");

                //string swc044num = System.Text.RegularExpressions.Regex.Replace(tSWC044, @"[^0-9]+", ""); ;//取出數字

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC044");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(swc044num, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC044");


                ////7.核定完工日期
                //string Year052 = " " + Convert.ToString(Convert.ToDateTime(tSWC052).Year - 1911);
                //string Month052 = " " + Convert.ToString(Convert.ToDateTime(tSWC052).Month);
                //string Day052 = " " + Convert.ToString(Convert.ToDateTime(tSWC052).Day);
                ////判斷日期1900-01-01
                //if (Convert.ToString(Convert.ToDateTime(tSWC052).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC052).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC052).Day) == "1")
                //{
                //    Year052 = " ";
                //    Month052 = " ";
                //    Day052 = " ";
                //}

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC052a");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC052a");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC052b");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC052b");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC052c");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC052c");


                ////8.完工申報日期
                //string Year058 = " " + Convert.ToString(Convert.ToDateTime(tSWC058).Year - 1911);
                //string Month058 = " " + Convert.ToString(Convert.ToDateTime(tSWC058).Month);
                //string Day058 = " " + Convert.ToString(Convert.ToDateTime(tSWC058).Day);
                ////判斷日期1900-01-01
                //if (Convert.ToString(Convert.ToDateTime(tSWC058).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tSWC058).Month) == "1" && Convert.ToString(Convert.ToDateTime(tSWC058).Day) == "1")
                //{
                //    Year058 = " ";
                //    Month058 = " ";
                //    Day058 = " ";
                //}

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC058a");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year058, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC058a");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC058b");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month058, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC058b");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC058c");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day058, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC058c");

                ////9.姓名或名稱
                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC013");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC013");

                ////10.身分證或營利事業統一編號
                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC013ID");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013ID, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC013ID");

                ////11.住居所或營業處
                //ps = Pdfstamper.AcroFields.GetFieldPositions("SWC014");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("SWC014");

                ////12.姓名
                //ps = Pdfstamper.AcroFields.GetFieldPositions("ETName");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC021, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("ETName");

                ////13.執業機構名稱
                //string orgname = SBApp.GetETUser(tSWC045ID, "OrgName");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgName");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgname, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("ETOrgName");

                ////14.執業執照字號
                //string orgissno = SBApp.GetETUser(tSWC045ID, "OrgIssNo");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgIssNo");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgissno, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("ETOrgIssNo");

                ////15.營利事業統一編號
                //string orgguino = SBApp.GetETUser(tSWC045ID, "OrgGUINo");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgGUINo");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgguino, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("ETOrgGUINo");

                ////16.電話
                //string orgtel = SBApp.GetETUser(tSWC045ID, "OrgTel");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("ETOrgTel");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(orgtel, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("ETOrgTel");

                ////17.實施地點土地標示
                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL023");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF023, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_MIDDLE);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL023");

                ////18.完工抽驗項目
                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL024");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF024, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL024");

                ////19.實施與計畫或規定不符之限期改正期限
                //string dtl025 = tDTLF025.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL025");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(dtl025, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL025");

                ////20.其他注意事項
                //string dtl026 = tDTLF026.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL026");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(dtl026, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL026");

                ////21.檢查結果
                //string chk027a = "□";
                //string chk027b = "□";
                //if (tDTLF027 == "已達完工標準") {chk027a = "■";} else { chk027b = "■"; }

                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL027a");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(chk027a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL027a");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL027b");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(chk027b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL027b");

                ////string dtl028 = tDTLF028.Replace("\r\n", "<br/>").Replace("\n\r", "<br/>");

                //ps = Pdfstamper.AcroFields.GetFieldPositions("DTL028");
                //p = ps[0];
                //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLF028, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //ct.Go();
                //Pdfstamper.AcroFields.RemoveField("DTL028");



                ////    //23~28.相片說明文字
                ////    //string[] arrayPicRemark = new string[] { tDTLG061, tDTLG063, tDTLG064, tDTLG066, tDTLG067, tDTLG069, tDTLG070, tDTLG072, tDTLG073, tDTLG075, tDTLG076, tDTLG078 };
                ////    //string[] arrayPDFView05 = new string[] { "DTL061", "DTL063", "DTL064", "DTL066", "DTL067", "DTL069", "DTL070", "DTL072", "DTL073", "DTL075", "DTL076", "DTL078" };
                //string[] arrayPicRemark = new string[] { tDTLF031, tDTLF033, tDTLF035, tDTLF037, tDTLF039, tDTLF041};
                //string[] arrayPDFView05 = new string[] { "DTL031", "DTL033", "DTL035", "DTL037", "DTL039", "DTL041"};

                //for (int i = 0; i < arrayPicRemark.Length; i++)
                //{
                //    string aPicRemark = arrayPicRemark[i] + "";
                //    string aPdfView05 = arrayPDFView05[i] + "";

                //    ps = Pdfstamper.AcroFields.GetFieldPositions(aPdfView05);
                //    p = ps[0];
                //    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                //    ct.SetSimpleColumn(new iTextSharp.text.Phrase(aPicRemark, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                //    ct.Go();
                //    Pdfstamper.AcroFields.RemoveField(aPdfView05);
                //}

                ////以下處理圖片
                //iTextSharp.text.Image pdfimageobj;
                //float x = 0;
                //float y = 0;

                ////加圖片1進去, A4 大小 寬:0~595 高:0~842
                //if (tDTLF030.Trim() != "")
                //{
                //    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF030;
                //    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                //    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                //    x = Convert.ToSingle(165 - (pdfimageobj.ScaledWidth / 2));
                //    y = Convert.ToSingle(670 - (pdfimageobj.ScaledHeight / 2));
                //    pdfimageobj.SetAbsolutePosition(x, y);
                //    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                //}

                ////加圖片2進去
                //if (tDTLF032.Trim() != "")
                //{
                //    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF032;
                //    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                //    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                //    x = Convert.ToSingle(409 - (pdfimageobj.ScaledWidth / 2));
                //    y = Convert.ToSingle(670 - (pdfimageobj.ScaledHeight / 2));
                //    pdfimageobj.SetAbsolutePosition(x, y);
                //    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                //}

                ////加圖片3進去
                //if (tDTLF034.Trim() != "")
                //{
                //    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF034;
                //    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                //    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                //    x = Convert.ToSingle(165 - (pdfimageobj.ScaledWidth / 2));
                //    y = Convert.ToSingle(444.5 - (pdfimageobj.ScaledHeight / 2));
                //    pdfimageobj.SetAbsolutePosition(x, y);
                //    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                //}

                ////加圖片4進去
                //if (tDTLF036.Trim() != "")
                //{
                //    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF036;
                //    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                //    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                //    x = Convert.ToSingle(409 - (pdfimageobj.ScaledWidth / 2));
                //    y = Convert.ToSingle(444.5 - (pdfimageobj.ScaledHeight / 2));
                //    pdfimageobj.SetAbsolutePosition(x, y);
                //    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                //}

                ////加圖片5進去
                //if (tDTLF038.Trim() != "")
                //{
                //    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF038;
                //    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                //    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                //    x = Convert.ToSingle(165 - (pdfimageobj.ScaledWidth / 2));
                //    y = Convert.ToSingle(224 - (pdfimageobj.ScaledHeight / 2));
                //    pdfimageobj.SetAbsolutePosition(x, y);
                //    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                //}

                ////加圖片6進去
                //if (tDTLF040.Trim() != "")
                //{
                //    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLF040;
                //    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                //    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                //    x = Convert.ToSingle(409 - (pdfimageobj.ScaledWidth / 2));
                //    y = Convert.ToSingle(224 - (pdfimageobj.ScaledHeight / 2));
                //    pdfimageobj.SetAbsolutePosition(x, y);
                //    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                //}
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