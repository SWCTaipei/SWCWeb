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

public partial class SwcReport_PdfSwcDtl03 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenPdf();
    }

    private void GenPdf()
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/DTL007.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        string ExeSqlStr = " select SWC.SWC005,D7.* from SWCDTL07 D7 ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON D7.SWC000=SWC.SWC000 ";
        ExeSqlStr = ExeSqlStr + " where D7.SWC000 = '" + rCaseId + "' ";
        ExeSqlStr = ExeSqlStr + "   and D7.DTLG000 = '" + rDTLId + "' ";

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

                string tDTLG002 = readeSwc["DTLG002"] + "";
                string tDTLG004 = readeSwc["DTLG004"] + "";
                string tDTLG005 = readeSwc["DTLG005"] + "";
                string tDTLG006 = readeSwc["DTLG006"] + "";
                string tDTLG007 = readeSwc["DTLG007"] + "";
                string tDTLG008 = readeSwc["DTLG008"] + "";
                string tDTLG009 = readeSwc["DTLG009"] + "";
                string tDTLG012 = readeSwc["DTLG012"] + "";
                string tDTLG014 = readeSwc["DTLG014"] + "";
                string tDTLG015 = readeSwc["DTLG015"] + "";
                string tDTLG016 = readeSwc["DTLG016"] + "";
                string tDTLG018 = readeSwc["DTLG018"] + "";
                string tDTLG020 = readeSwc["DTLG020"] + "";
                string tDTLG022 = readeSwc["DTLG022"] + "";
                string tDTLG023 = readeSwc["DTLG023"] + "";
                string tDTLG024 = readeSwc["DTLG024"] + "";
                string tDTLG025 = readeSwc["DTLG025"] + "";
                string tDTLG026 = readeSwc["DTLG026"] + "";
                string tDTLG027 = readeSwc["DTLG027"] + "";
                string tDTLG028 = readeSwc["DTLG028"] + "";
                string tDTLG030 = readeSwc["DTLG030"] + "";
                string tDTLG031 = readeSwc["DTLG031"] + "";
                string tDTLG032 = readeSwc["DTLG032"] + "";
                string tDTLG033 = readeSwc["DTLG033"] + "";
                string tDTLG035 = readeSwc["DTLG035"] + "";
                string tDTLG036 = readeSwc["DTLG036"] + "";
                string tDTLG037 = readeSwc["DTLG037"] + "";
                string tDTLG038 = readeSwc["DTLG038"] + "";
                string tDTLG040 = readeSwc["DTLG040"] + "";
                string tDTLG042 = readeSwc["DTLG042"] + "";
                string tDTLG044 = readeSwc["DTLG044"] + "";
                string tDTLG045 = readeSwc["DTLG045"] + "";
                string tDTLG046 = readeSwc["DTLG046"] + "";
                string tDTLG047 = readeSwc["DTLG047"] + "";
                string tDTLG048 = readeSwc["DTLG048"] + "";
                string tDTLG049 = readeSwc["DTLG049"] + "";
                string tDTLG050 = readeSwc["DTLG050"] + "";
                string tDTLG051 = readeSwc["DTLG051"] + "";
                string tDTLG052 = readeSwc["DTLG052"] + "";
                string tDTLG053 = readeSwc["DTLG053"] + "";
                string tDTLG054 = readeSwc["DTLG054"] + "";
                string tDTLG055 = readeSwc["DTLG055"] + "";
                string tDTLG056 = readeSwc["DTLG056"] + "";
                string tDTLG057 = readeSwc["DTLG057"] + "";
                string tDTLG058 = readeSwc["DTLG058"] + "";
                string tDTLG059 = readeSwc["DTLG059"] + "";
                string tDTLG061 = readeSwc["DTLG061"] + "";
                string tDTLG062 = readeSwc["DTLG062"] + "";
                string tDTLG063 = readeSwc["DTLG063"] + "";
                string tDTLG064 = readeSwc["DTLG064"] + "";
                string tDTLG065 = readeSwc["DTLG065"] + "";
                string tDTLG066 = readeSwc["DTLG066"] + "";
                string tDTLG067 = readeSwc["DTLG067"] + "";
                string tDTLG068 = readeSwc["DTLG068"] + "";
                string tDTLG069 = readeSwc["DTLG069"] + "";
                string tDTLG070 = readeSwc["DTLG070"] + "";
                string tDTLG071 = readeSwc["DTLG071"] + "";
                string tDTLG072 = readeSwc["DTLG072"] + "";
                string tDTLG073 = readeSwc["DTLG073"] + "";
                string tDTLG074 = readeSwc["DTLG074"] + "";
                string tDTLG075 = readeSwc["DTLG075"] + "";
                string tDTLG076 = readeSwc["DTLG076"] + "";
                string tDTLG077 = readeSwc["DTLG077"] + "";
                string tDTLG078 = readeSwc["DTLG078"] + "";

                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //0.檢查日期
                string Year = " " + Convert.ToString(Convert.ToDateTime(tDTLG002).Year - 1911);
                string Month = " " + Convert.ToString(Convert.ToDateTime(tDTLG002).Month);
                string Day = " " + Convert.ToString(Convert.ToDateTime(tDTLG002).Day);

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL002a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL002a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL002b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL002b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL002c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL002c");

                //2.計畫名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC005");

                //3.地點
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL007");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG007, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL007");

                //4.基地面積(公頃)
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL008");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG008, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL008");

                //5.現行義務人姓名或名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL004");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG004, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL004");

                //8.現行義務人地址
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL005");

                //9.現行義務人電話
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL006");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG006, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL006");

                //10.圖說來源
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL009");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG009, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL009");

                //11.基地現況建物
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL012");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG012, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL012");

                //12.基地現況道路
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL014");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL014");

                //13.基地現況其他
                string ptDTLG015 = "□";
                if (tDTLG015 == "1") { ptDTLG015 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL015");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG015, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL015");

                //14.基地現況其他說明
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL016");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG016, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL016");

                //15.排水設施
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL018");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG018, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12,  iTextSharp.text.Element.ALIGN_MIDDLE);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL018");

                //16.擋土設施
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL020");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG020, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL020");

                //17.滯洪沉砂設施
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL022");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG022, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL022");

                //18.滯洪量 
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL023");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG023+ "m³", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL023");

                //19.沉砂量
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL024");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG024 + "m³", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL024");

                //20.其他
                string ptDTLG025 = "□";
                if (tDTLG025 == "1") { ptDTLG025 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL025");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG025, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL025");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL026");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG026, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL026");

                //14.基地現況其他說明
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL027");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG027, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL027");

                //14.基地現況其他說明
                string ptDTLG028a = "□";
                string ptDTLG028b = "□";
                if (tDTLG028 == "功能正常") { ptDTLG028a = "■"; }
                if (tDTLG028 == "未清疏") { ptDTLG028b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL028a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG028a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL028a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL028b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG028b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL028b");

                //13.有變更使用
                string ptDTLG030a = "□";
                string ptDTLG030b = "□";
                if (tDTLG030 == "功能正常") { ptDTLG030a = "■"; }
                if (tDTLG030 == "排水設施有損壞") { ptDTLG030b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL030a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG030a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL030a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL030b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG030b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL030b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL031");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG031, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL031");

                //14.基地現況其他說明
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL032");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG032, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL032");

                //13.有變更使用
                string ptDTLG033a = "□";
                string ptDTLG033b = "□";
                if (tDTLG033 == "功能正常") { ptDTLG033a = "■"; }
                if (tDTLG033 == "有淤積或堵塞") { ptDTLG033b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL033a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG033a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL033a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL033b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG033b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL033b");

                //13.有變更使用
                string ptDTLG035a = "□";
                string ptDTLG035b = "□";
                if (tDTLG035 == "功能正常") { ptDTLG035a = "■"; }
                if (tDTLG035 == "排水設施有損壞") { ptDTLG035b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL035a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG035a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL035a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL035b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG035b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL035b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL036");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG036, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL036");

                //14.基地現況其他說明
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL037");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG037, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL037");

                //13.有變更使用
                string ptDTLG038a = "□";
                string ptDTLG038b = "□";
                string ptDTLG038b1 = "□";
                string ptDTLG038b2 = "□";
                string ptDTLG038b3 = "□";
                if (tDTLG038 == "") { ptDTLG038a = "■"; }
                if (tDTLG038 != "") { ptDTLG038b = "■"; }
                if (tDTLG038 == "排水孔無異常") { ptDTLG038b1 = "■"; }
                if (tDTLG038 == "排水孔堵塞") { ptDTLG038b2 = "■"; }
                if (tDTLG038 == "排水孔不正常出水") { ptDTLG038b3 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL038a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG038a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL038a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL038b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG038b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL038b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL038b1");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG038b1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL038b1");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL038b2");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG038b2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL038b2");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL038b3");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG038b3, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL038b3");

                //13.有變更使用
                string ptDTLG040a = "□";
                string ptDTLG040b = "□";
                if (tDTLG040 == "無明顯外凸變形") { ptDTLG040a = "■"; }
                if (tDTLG040 == "有外凸變形") { ptDTLG040b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL040a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG040a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL040a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL040b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG040b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL040b");

                //13.有變更使用
                string ptDTLG042a = "□";
                string ptDTLG042b = "□";
                if (tDTLG042 == "無明顯龜裂") { ptDTLG042a = "■"; }
                if (tDTLG042 == "有明顯龜裂") { ptDTLG042b = "■"; }
                
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL042a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG042a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL042a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL042b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG042b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL042b");

                //13.有變更使用
                string ptDTLG044a = "□";
                string ptDTLG044b = "□";
                if (tDTLG044 == "無諮詢事項") { ptDTLG044a = "■"; }
                if (tDTLG044 == "有諮詢事項") { ptDTLG044b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL044a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG044a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL044a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL044b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG044b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL044b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL045");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG045, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL045");

                //這邊的資料新的表單沒有存拿掉了，所以只能空著了…
                ps = Pdfstamper.AcroFields.GetFieldPositions("Text1");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("□", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("Text1");

                ps = Pdfstamper.AcroFields.GetFieldPositions("Text2");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase("", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("Text2");

                //13.有變更使用
                string ptDTLG046 = "□";
                if (tDTLG046 == "1") { ptDTLG046 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL046");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG046, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL046");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL047");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG047, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL047");

                //13.有變更使用
                string ptDTLG048 = "□";
                if (tDTLG048 == "1") { ptDTLG048 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL048");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG048, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL048");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL049");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG049, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL049");

                //13.有變更使用
                string ptDTLG050 = "□";
                if (tDTLG050 == "1") { ptDTLG050 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL050");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG050, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL050");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL051");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL051");

                //13.有變更使用
                string ptDTLG052 = "□";
                if (tDTLG052 == "1") { ptDTLG052 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL052");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL052");

                //13.有變更使用
                string ptDTLG053 = "□";
                if (tDTLG053 == "1") { ptDTLG053 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL053");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG053, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL053");

                //0.檢查日期
                string Year54 = " " + Convert.ToString(Convert.ToDateTime(tDTLG054).Year - 1911);
                string Month54 = " " + Convert.ToString(Convert.ToDateTime(tDTLG054).Month);
                string Day54 = " " + Convert.ToString(Convert.ToDateTime(tDTLG054).Day);

                if (Convert.ToInt32(Year54) < 0) { Month54 = ""; Day54 = ""; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL054a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month54, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL054a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL054b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day54, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL054b");

                //13.有變更使用
                string ptDTLG055 = "□";
                if (tDTLG055 == "1") { ptDTLG055 = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL055");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG055, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL055");

                //13.影響安全情事
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL056");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG056, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL056");

                //13.前次檢查之改正事項辦理情形
                string ptDTLG057 = "□";
                string ptDTLG058a = "□";
                string ptDTLG058b = "□";
                if (tDTLG057 == "1") { ptDTLG057 = "■"; }
                if (tDTLG058 == "已改善") { ptDTLG058a = "■"; }
                if (tDTLG058 == "未改善") { ptDTLG058b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL057");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG057, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL057");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL058a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG058a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL058a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL058b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(ptDTLG058b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 10)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL058b");

                //40.相關單位及人員簽名
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTL059");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLG059, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTL059");

                //42~47.相片說明文字
                //string[] arrayPicRemark = new string[] { tDTLG061, tDTLG063, tDTLG064, tDTLG066, tDTLG067, tDTLG069, tDTLG070, tDTLG072, tDTLG073, tDTLG075, tDTLG076, tDTLG078 };
                //string[] arrayPDFView05 = new string[] { "DTL061", "DTL063", "DTL064", "DTL066", "DTL067", "DTL069", "DTL070", "DTL072", "DTL073", "DTL075", "DTL076", "DTL078" };
                string[] arrayPicRemark = new string[] { tDTLG061, tDTLG063, tDTLG064, tDTLG066, tDTLG067, tDTLG069, tDTLG070, tDTLG072, tDTLG073, tDTLG075, tDTLG076, tDTLG078 };
                string[] arrayPDFView05 = new string[] { "DTL061", "DTL063", "DTL064", "DTL066", "DTL067", "DTL069", "DTL070", "DTL072", "DTL073", "DTL075", "DTL076", "DTL078" };

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

                //加圖片1進去, A4 大小 寬:0~595 高:0~842
                if (tDTLG062.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLG062;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(660 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片2進去
                if (tDTLG065.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLG065;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(434 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(660 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片3進去
                if (tDTLG068.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLG068;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(432 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片4進去
                if (tDTLG071.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLG071;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(434 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(432 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片5進去
                if (tDTLG074.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLG074;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(204 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }

                //加圖片6進去
                if (tDTLG077.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLG077;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(255), Convert.ToSingle(139));

                    x = Convert.ToSingle(434 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(204 - (pdfimageobj.ScaledHeight / 2));
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