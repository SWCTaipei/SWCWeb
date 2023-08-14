using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public partial class SwcReport_PdfSwcDtl01 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenPdf();
        //GenPdfo();
    }

    private void GenPdf()
    {
        GBClass001 SBApp = new GBClass001();
        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ExeSqlStr = "select SWC.SWC000,SWC.SWC005,SWC.SWC022";
        ExeSqlStr = ExeSqlStr + ",D1.DTLA006,D1.DTLA007,D1.DTLA008,D1.DTLA009,D1.DTLA010,D1.DTLA011,D1.DTLA012,D1.DTLA013,D1.DTLA018,D1.DTLA021,D1.DTLA022";
        ExeSqlStr = ExeSqlStr + ",D1.DTLA023,D1.DTLA024,D1.DTLA025,D1.DTLA026,D1.DTLA027,D1.DTLA028,D1.DTLA029,D1.DTLA030,D1.DTLA031,D1.DTLA032,";
        ExeSqlStr = ExeSqlStr + "D1.savedate from SWCDTL01 D1";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON D1.SWC000=SWC.SWC000";
        ExeSqlStr = ExeSqlStr + " where D1.SWC000 = '" + rCaseId + "' ";
        ExeSqlStr = ExeSqlStr + "   and D1.DTLA000 = '" + rDTLId + "' ";

        string tsavedate = "", tswc022 = "", tdtla006 = "", tdtla007 = "", tdtla008 = "", tdtla009 = "", tdtla010 = "", tdtla011 = "", tdtla012 = "", tDTLA013 = "";
        string tDTLA018 = "", tswc005 = "", tdtla013 = "", tdtla021 = "", tdtla022 = "", tdtla023 = "", tdtla024 = "", tdtla025 = "", tdtla026 = "", tdtla027 = "";
        string tdtla028 = "", tdtla029 = "", tdtla030 = "", tdtla031 = "", tdtla032 = "";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(ExeSqlStr, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                tsavedate = readeSwc["savedate"] + "";
                tswc022 = readeSwc["SWC022"] + "";
                tdtla006 = readeSwc["DTLA006"] + "";
                tdtla007 = readeSwc["DTLA007"] + "";
                tdtla008 = readeSwc["DTLA008"] + "";
                tdtla009 = readeSwc["DTLA009"] + "";
                tdtla010 = readeSwc["DTLA010"] + "";
                tdtla011 = readeSwc["DTLA011"] + "";
                tdtla012 = readeSwc["DTLA012"] + "";
                tDTLA013 = readeSwc["DTLA013"] + "";
                tDTLA018 = readeSwc["DTLA018"] + "";
                tswc005 = readeSwc["SWC005"] + "";
                tdtla013 = readeSwc["DTLA013"] + "";
                tdtla021 = readeSwc["DTLA021"] + "";
                tdtla022 = readeSwc["DTLA022"] + "";
                tdtla023 = readeSwc["DTLA023"] + "";
                tdtla024 = readeSwc["DTLA024"] + "";
                tdtla025 = readeSwc["DTLA025"] + "";
                tdtla026 = readeSwc["DTLA026"] + "";
                tdtla027 = readeSwc["DTLA027"] + "";
                tdtla028 = readeSwc["DTLA028"] + "";
                tdtla029 = readeSwc["DTLA029"] + "";
                tdtla030 = readeSwc["DTLA030"] + "";
                tdtla031 = readeSwc["DTLA031"] + "";
                tdtla032 = readeSwc["DTLA032"] + "";

                tsavedate = Convert.ToString(Convert.ToDateTime(tsavedate).Year - 1911) + "年" + Convert.ToString(Convert.ToDateTime(tsavedate).Month) + "月" + Convert.ToString(Convert.ToDateTime(tsavedate).Day) + "日";
                tdtla007 = Convert.ToString(Convert.ToDateTime(tdtla007).Year - 1911) + "年" + Convert.ToString(Convert.ToDateTime(tdtla007).Month) + "月" + Convert.ToString(Convert.ToDateTime(tdtla007).Day) + "日";
            }

        }


        var doc1 = new Document(PageSize.A4, 30, 30, 30, 30);
        MemoryStream Memory = new MemoryStream();
        PdfWriter PdfWriter = PdfWriter.GetInstance(doc1, Memory);

        BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        Font ChFont = new Font(bfChinese, 12);
        Font ChFont_bold = new Font(bfChinese, 12, Font.BOLD);
        Font ChFont_title = new Font(bfChinese, 22, Font.BOLD);
        Font ChFont_PS = new Font(bfChinese, 15);

        doc1.Open();
        PdfPTable table1 = new PdfPTable(new float[] { 8, 34 });  //2欄
        table1.TotalWidth = 500f;
        table1.LockedWidth = true;

        PdfPCell header_1 = new PdfPCell(new Phrase("水土保持申請書件審查會議紀錄", ChFont_title));
        header_1.Colspan = 3;   //跨欄
        header_1.MinimumHeight = 80;//設定行高
        header_1.HorizontalAlignment = Element.ALIGN_CENTER;
        header_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        header_1.Border = PdfPCell.NO_BORDER;

        table1.AddCell(header_1);

        //發送時間
        PdfPCell t1_1 = new PdfPCell(new Phrase("發送時間：", ChFont_bold));
        t1_1.MinimumHeight = 18f;//設定行高
        t1_1.HorizontalAlignment = Element.ALIGN_RIGHT;
        t1_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_1.Border = PdfPCell.NO_BORDER;

        table1.AddCell(t1_1);

        PdfPCell t1_2 = new PdfPCell(new Phrase(tsavedate, ChFont));
        t1_2.MinimumHeight = 18f;//設定行高
        t1_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t1_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_2.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_2);

        //審查單位
        PdfPCell t1_3 = new PdfPCell(new Phrase("審查單位：", ChFont_bold));
        //t1_2.Colspan = 3;
        t1_3.MinimumHeight = 18f;//設定行高
        t1_3.HorizontalAlignment = Element.ALIGN_RIGHT;
        t1_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_3.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_3);

        PdfPCell t1_4 = new PdfPCell(new Phrase(tswc022, ChFont));
        //t1_2.Colspan = 3;
        t1_4.MinimumHeight = 18f;//設定行高
        t1_4.HorizontalAlignment = Element.ALIGN_LEFT;
        t1_4.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_4.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_4);

        //會議事由
        PdfPCell t1_5 = new PdfPCell(new Phrase("會議事由：", ChFont_bold));
        t1_5.MinimumHeight = 18f;//設定行高
        t1_5.HorizontalAlignment = Element.ALIGN_RIGHT;
        t1_5.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_5.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_5);

        string td06 = "第 " + tdtla006 + " 次審查會";
        PdfPCell t1_6 = new PdfPCell(new Phrase(td06, ChFont));
        t1_6.MinimumHeight = 18f;//設定行高
        t1_6.HorizontalAlignment = Element.ALIGN_LEFT;
        t1_6.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_6.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_6);

        //會議時間
        PdfPCell t1_7 = new PdfPCell(new Phrase("會議時間：", ChFont_bold));
        t1_7.MinimumHeight = 18f;//設定行高
        t1_7.HorizontalAlignment = Element.ALIGN_RIGHT;
        t1_7.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_7.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_7);

        PdfPCell t1_8 = new PdfPCell(new Phrase(tdtla007, ChFont));
        t1_8.MinimumHeight = 18f;//設定行高
        t1_8.HorizontalAlignment = Element.ALIGN_LEFT;
        t1_8.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_8.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_8);

        //會議地點
        PdfPCell t1_9 = new PdfPCell(new Phrase("會議地點：", ChFont_bold));
        t1_9.MinimumHeight = 18f;//設定行高
        t1_9.HorizontalAlignment = Element.ALIGN_RIGHT;
        t1_9.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_9.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_9);

        PdfPCell t1_10 = new PdfPCell(new Phrase(tdtla008, ChFont));
        t1_10.MinimumHeight = 18f;//設定行高
        t1_10.HorizontalAlignment = Element.ALIGN_LEFT;
        t1_10.VerticalAlignment = Element.ALIGN_MIDDLE;
        t1_10.Border = PdfPCell.NO_BORDER;
        table1.AddCell(t1_10);

        doc1.Add(table1);
        //doc1.NewPage();

        //**********************************

        PdfPTable table2 = new PdfPTable(new float[] { 5, 30 });
        table2.TotalWidth = 500f;
        table2.LockedWidth = true;

        //主席
        PdfPCell t2_1 = new PdfPCell(new Phrase("    主席：", ChFont_bold));
        t2_1.MinimumHeight = 18f;//設定行高
        t2_1.HorizontalAlignment = Element.ALIGN_RIGHT;
        t2_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        t2_1.Border = PdfPCell.NO_BORDER;
        table2.AddCell(t2_1);

        PdfPCell t2_2 = new PdfPCell(new Phrase(tdtla009, ChFont));
        t2_2.MinimumHeight = 18f;//設定行高
        t2_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t2_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        t2_2.Border = PdfPCell.NO_BORDER;
        table2.AddCell(t2_2);

        doc1.Add(table2);
        //doc1.NewPage();

        //**********************************

        PdfPTable table3 = new PdfPTable(new float[] { 8, 34 });
        table3.TotalWidth = 500f;
        table3.LockedWidth = true;

        //出席人員
        PdfPCell t3_1 = new PdfPCell(new Phrase("出席人員：", ChFont_bold));
        t3_1.MinimumHeight = 18f;//設定行高
        t3_1.HorizontalAlignment = Element.ALIGN_RIGHT;
        t3_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_1.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_1);

        PdfPCell t3_2 = new PdfPCell(new Phrase(tdtla010, ChFont));
        t3_2.MinimumHeight = 18f;//設定行高
        t3_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t3_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_2.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_2);

        //列席人員
        PdfPCell t3_3 = new PdfPCell(new Phrase("列席人員：", ChFont_bold));
        t3_3.MinimumHeight = 18f;//設定行高
        t3_3.HorizontalAlignment = Element.ALIGN_RIGHT;
        t3_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_3.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_3);

        PdfPCell t3_4 = new PdfPCell(new Phrase(tdtla011, ChFont));
        t3_4.MinimumHeight = 18f;//設定行高
        t3_4.HorizontalAlignment = Element.ALIGN_LEFT;
        t3_4.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_4.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_4);

        //紀錄人員
        PdfPCell t3_5 = new PdfPCell(new Phrase("紀錄人員：", ChFont_bold));
        t3_5.MinimumHeight = 18f;//設定行高
        t3_5.HorizontalAlignment = Element.ALIGN_RIGHT;
        t3_5.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_5.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_5);

        PdfPCell t3_6 = new PdfPCell(new Phrase(tdtla012, ChFont));
        t3_6.MinimumHeight = 18f;//設定行高
        t3_6.HorizontalAlignment = Element.ALIGN_LEFT;
        t3_6.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_6.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_6);

        //書件名稱
        PdfPCell t3_7 = new PdfPCell(new Phrase("書件名稱：", ChFont_bold));
        t3_7.MinimumHeight = 18f;//設定行高
        t3_7.HorizontalAlignment = Element.ALIGN_RIGHT;
        t3_7.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_7.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_7);

        PdfPCell t3_8 = new PdfPCell(new Phrase(tswc005, ChFont));
        t3_8.MinimumHeight = 18f;//設定行高
        t3_8.HorizontalAlignment = Element.ALIGN_LEFT;
        t3_8.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_8.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_8);

        //會議結論
        PdfPCell t3_9 = new PdfPCell(new Phrase("會議結論：", ChFont_bold));
        t3_9.MinimumHeight = 18f;//設定行高
        t3_9.HorizontalAlignment = Element.ALIGN_RIGHT;
        t3_9.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_9.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_9);

        PdfPCell t3_10 = new PdfPCell(new Phrase(tDTLA013, ChFont));
        t3_10.MinimumHeight = 18f;//設定行高
        t3_10.HorizontalAlignment = Element.ALIGN_LEFT;
        t3_10.VerticalAlignment = Element.ALIGN_MIDDLE;
        t3_10.Border = PdfPCell.NO_BORDER;
        table3.AddCell(t3_10);

        doc1.Add(table3);
        //doc1.NewPage();

        //**********************************

        PdfPTable table4 = new PdfPTable(new float[] { 1 });
        table4.TotalWidth = 500f;
        table4.LockedWidth = true;

        PdfPCell t4_1 = new PdfPCell(new Phrase(" 　　相關單位及人員簽名：", ChFont_bold));
        t4_1.MinimumHeight = 18f;//設定行高
        t4_1.HorizontalAlignment = Element.ALIGN_LEFT;
        t4_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        t4_1.Border = PdfPCell.NO_BORDER;
        table4.AddCell(t4_1);

        //PdfPCell t4_2 = new PdfPCell(new Phrase(tDTLA018, ChFont));
        //t4_2.MinimumHeight = 18f;//設定行高
        //t4_2.HorizontalAlignment = Element.ALIGN_LEFT;
        //t4_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        //t4_2.Border = PdfPCell.NO_BORDER;
        //table4.AddCell(t4_2);

        //doc1.NewPage();

        iTextSharp.text.Image jpg;
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLA018;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            jpgcell.Border = PdfPCell.NO_BORDER;
            table4.AddCell(jpgcell);
        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);

            //PdfPCell table4_21b = new PdfPCell(new Phrase("", ChFont));
            //table4_21b.MinimumHeight = 18f;//設定行高
            //table4_21b.HorizontalAlignment = Element.ALIGN_LEFT;
            //table4_21b.VerticalAlignment = Element.ALIGN_MIDDLE;
            //table4.AddCell(table4_21b);
        }

        //PdfPCell t4_2 = new PdfPCell(new Phrase(tdtla022, ChFont));
        //t4_2.Colspan = 2;   //跨欄
        //t4_2.MinimumHeight = 18f;//設定行高
        //t4_2.HorizontalAlignment = Element.ALIGN_CENTER;
        //t4_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table4.AddCell(t4_2);

        doc1.Add(table4);

        //**********************************

        PdfPTable table5 = new PdfPTable(new float[] { 8, 34 });
        table5.TotalWidth = 500f;
        table5.LockedWidth = true;

        PdfPCell t5_1 = new PdfPCell(new Phrase("現場照片：", ChFont_bold));
        t5_1.MinimumHeight = 22f;//設定行高
        t5_1.HorizontalAlignment = Element.ALIGN_RIGHT;
        t5_1.VerticalAlignment = Element.ALIGN_CENTER;
        t5_1.Border = PdfPCell.NO_BORDER;
        table5.AddCell(t5_1);

        PdfPCell t5_2 = new PdfPCell(new Phrase("", ChFont));
        t5_2.MinimumHeight = 22f;//設定行高
        t5_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t5_2.VerticalAlignment = Element.ALIGN_CENTER;
        t5_2.Border = PdfPCell.NO_BORDER;
        table5.AddCell(t5_2);

        doc1.Add(table5);


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //照片
        //doc1.NewPage();
        PdfPTable table6 = new PdfPTable(new float[] { 1 });
        table6.TotalWidth = 500f;
        table6.LockedWidth = true;

        PdfPCell t6_1 = new PdfPCell(new Phrase("現場照片一", ChFont_bold));
        t6_1.Colspan = 2;   //跨欄
        t6_1.MinimumHeight = 25f;//設定行高
        t6_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_1);

        //iTextSharp.text.Image jpg;
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla021;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);
        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);

            //PdfPCell table4_21b = new PdfPCell(new Phrase("", ChFont));
            //table4_21b.MinimumHeight = 18f;//設定行高
            //table4_21b.HorizontalAlignment = Element.ALIGN_LEFT;
            //table4_21b.VerticalAlignment = Element.ALIGN_MIDDLE;
            //table4.AddCell(table4_21b);
        }

        PdfPCell t6_2 = new PdfPCell(new Phrase(tdtla022, ChFont));
        t6_2.Colspan = 2;   //跨欄
        t6_2.MinimumHeight = 18f;//設定行高
        t6_2.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_2);

        PdfPCell t6_3 = new PdfPCell(new Phrase("現場照片二", ChFont_bold));
        t6_3.Colspan = 2;   //跨欄
        t6_3.MinimumHeight = 25f;//設定行高
        t6_3.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_3);

        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla023;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);
        }

        PdfPCell t6_4 = new PdfPCell(new Phrase(tdtla024, ChFont));
        t6_4.Colspan = 2;   //跨欄
        t6_4.MinimumHeight = 18f;//設定行高
        t6_4.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_4.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_4);

        PdfPCell t6_5 = new PdfPCell(new Phrase("現場照片三", ChFont_bold));
        t6_5.Colspan = 2;   //跨欄
        t6_5.MinimumHeight = 25f;//設定行高
        t6_5.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_5.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_5);

        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla025;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);
        }

        PdfPCell t6_6 = new PdfPCell(new Phrase(tdtla026, ChFont));
        t6_6.Colspan = 2;   //跨欄
        t6_6.MinimumHeight = 18f;//設定行高
        t6_6.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_6.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_6);

        PdfPCell t6_7 = new PdfPCell(new Phrase("現場照片四", ChFont_bold));
        t6_7.Colspan = 2;   //跨欄
        t6_7.MinimumHeight = 25f;//設定行高
        t6_7.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_7.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_7);
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla027;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);
        }
        PdfPCell t6_8 = new PdfPCell(new Phrase(tdtla028, ChFont));
        t6_8.Colspan = 2;   //跨欄
        t6_8.MinimumHeight = 18f;//設定行高
        t6_8.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_8.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_8);

        PdfPCell t6_9 = new PdfPCell(new Phrase("現場照片五", ChFont_bold));
        t6_9.Colspan = 2;   //跨欄
        t6_9.MinimumHeight = 25f;//設定行高
        t6_9.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_9.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_9);

        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla029;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);
        }

        PdfPCell t6_10 = new PdfPCell(new Phrase(tdtla030, ChFont));
        t6_10.Colspan = 2;   //跨欄
        t6_10.MinimumHeight = 18f;//設定行高
        t6_10.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_10.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_10);

        PdfPCell t6_11 = new PdfPCell(new Phrase("現場照片六", ChFont_bold));
        t6_11.Colspan = 2;   //跨欄
        t6_11.MinimumHeight = 25f;//設定行高
        t6_11.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_11.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_11);

        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla031;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table6.AddCell(jpgcell);
        }

        PdfPCell t6_12 = new PdfPCell(new Phrase(tdtla030, ChFont));
        t6_12.Colspan = 2;   //跨欄
        t6_12.MinimumHeight = 18f;//設定行高
        t6_12.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_12.VerticalAlignment = Element.ALIGN_MIDDLE;
        table6.AddCell(t6_12);

        PdfPCell t6_13 = new PdfPCell(new Phrase("審查意見：", ChFont));
        t6_13.Colspan = 2;   //跨欄
        t6_13.MinimumHeight = 18f;//設定行高
        t6_13.HorizontalAlignment = Element.ALIGN_LEFT;
        t6_13.VerticalAlignment = Element.ALIGN_MIDDLE;
        t6_13.Border = PdfPCell.NO_BORDER;
        table6.AddCell(t6_13);

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //新增審查意見pdf
        string ExeSqlStrR = "select DTLR01,DTLR02 from SWCDTL01R";
        ExeSqlStrR = ExeSqlStrR + " where SWC000 = '" + rCaseId + "' ";
        ExeSqlStrR = ExeSqlStrR + "   and DTLA001 = '" + rDTLId + "' ";
        ExeSqlStrR = ExeSqlStrR + " order by DTLR01 ";

        ConnectionStringSettings connectionString1 = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConnR = new SqlConnection(connectionString1.ConnectionString))
        {
            SwcConnR.Open();

            int pcount = 0;

            SqlDataReader readeSwcR;
            SqlCommand objCmdSwcR = new SqlCommand(ExeSqlStrR, SwcConnR);
            readeSwcR = objCmdSwcR.ExecuteReader();

            while (readeSwcR.Read())
            {

                pcount++;

                string tdtlr02 = readeSwcR["DTLR02"] + "";

                PdfPCell t6_14 = new PdfPCell(new Paragraph(tdtlr02.ToString() + "\n", ChFont));
                t6_14.Colspan = 2;   //跨欄
                t6_14.MinimumHeight = 18f;//設定行高
                t6_14.HorizontalAlignment = Element.ALIGN_LEFT;
                t6_14.VerticalAlignment = Element.ALIGN_MIDDLE;
                t6_14.Border = PdfPCell.NO_BORDER;
                table6.AddCell(t6_14);

            }

            readeSwcR.Close();
            objCmdSwcR.Dispose();

        }



        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        doc1.Add(table6);



        doc1.NewPage();
        doc1.Close();



        //使用者下載，另存新檔 \r\n
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=水土保持申請書件審查會議紀錄_" + (DateTime.Now.Year - 1911) + (DateTime.Now.ToString("MMddmmss")) + ".pdf");
        Response.ContentType = "application/octet-stream";
        Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
        Response.OutputStream.Flush();
        Response.OutputStream.Close();
        Response.Flush();
        Response.End();
        //ooooooooooooooooooooooooooooooooooooooooooooooo

    }

    private void GenPdfo()
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/DTL001.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        string pdfnewnameR = DateTime.Now.ToString("yyyyMMddHHmmss")  + "R.pdf";
        string pdfnewnameS = DateTime.Now.ToString("yyyyMMddHHmmss") + "S.pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));
        PdfImportedPage page = Pdfstamper.GetImportedPage(Pdfreader, 5);
        
        string ExeSqlStr = "select SWC.SWC000,SWC.SWC005,SWC.SWC022";
        ExeSqlStr = ExeSqlStr + ",D1.DTLA006,D1.DTLA007,D1.DTLA008,D1.DTLA009,D1.DTLA010,D1.DTLA011,D1.DTLA012,D1.DTLA013,D1.DTLA018,D1.DTLA021,D1.DTLA022";
        ExeSqlStr = ExeSqlStr + ",D1.DTLA023,D1.DTLA024,D1.DTLA025,D1.DTLA026,D1.DTLA027,D1.DTLA028,D1.DTLA029,D1.DTLA030,D1.DTLA031,D1.DTLA032,";
        ExeSqlStr = ExeSqlStr + "D1.savedate from SWCDTL01 D1";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON D1.SWC000=SWC.SWC000";
        ExeSqlStr = ExeSqlStr + " where D1.SWC000 = '" + rCaseId + "' ";
        ExeSqlStr = ExeSqlStr + "   and D1.DTLA000 = '" + rDTLId + "' ";

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
                string tsavedate = readeSwc["savedate"] + "";
                string tswc022 = readeSwc["SWC022"] + "";
                string tdtla006 = readeSwc["DTLA006"] + "";
                string tdtla007 = readeSwc["DTLA007"] + "";
                string tdtla008 = readeSwc["DTLA008"] + "";
                string tdtla009 = readeSwc["DTLA009"] + "";
                string tdtla010 = readeSwc["DTLA010"] + "";
                string tdtla011 = readeSwc["DTLA011"] + "";
                string tdtla012 = readeSwc["DTLA012"] + "";
                string tDTLA013 = readeSwc["DTLA013"] + "";
                string tDTLA018 = readeSwc["DTLA018"] + "";
                string tswc005 = readeSwc["SWC005"] + "";
                string tdtla013 = readeSwc["DTLA013"] + "";
                string tdtla021 = readeSwc["DTLA021"] + "";
                string tdtla022 = readeSwc["DTLA022"] + "";
                string tdtla023 = readeSwc["DTLA023"] + "";
                string tdtla024 = readeSwc["DTLA024"] + "";
                string tdtla025 = readeSwc["DTLA025"] + "";
                string tdtla026 = readeSwc["DTLA026"] + "";
                string tdtla027 = readeSwc["DTLA027"] + "";
                string tdtla028 = readeSwc["DTLA028"] + "";
                string tdtla029 = readeSwc["DTLA029"] + "";
                string tdtla030 = readeSwc["DTLA030"] + "";
                string tdtla031 = readeSwc["DTLA031"] + "";
                string tdtla032 = readeSwc["DTLA032"] + "";

                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //0.發送時間
                string Year = " " + Convert.ToString(Convert.ToDateTime(tsavedate).Year - 1911);
                string Month = " " + Convert.ToString(Convert.ToDateTime(tsavedate).Month);
                string Day = " " + Convert.ToString(Convert.ToDateTime(tsavedate).Day);

                ps = Pdfstamper.AcroFields.GetFieldPositions("savedatea");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("savedatea");

                ps = Pdfstamper.AcroFields.GetFieldPositions("savedateb");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("savedateb");

                ps = Pdfstamper.AcroFields.GetFieldPositions("savedatec");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("savedatec");

                //1.審查單位
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC022");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tswc022, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC022");

                //2.會議事由
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA006");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tdtla006, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA006");

                //3.會議時間
                string Year07 = " " + Convert.ToString(Convert.ToDateTime(tdtla007).Year - 1911);
                string Month07 = " " + Convert.ToString(Convert.ToDateTime(tdtla007).Month);
                string Day07 = " " + Convert.ToString(Convert.ToDateTime(tdtla007).Day);

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA007a");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year07, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA007a");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA007b");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month07, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA007b");

                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA007c");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day07, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA007c");

                //4.會議地點
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA008");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tdtla008, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA008");

                //5.主席
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA009");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tdtla009, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA009");

                //6.出席人員
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA010");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tdtla010, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA010");

                //7.列席人員
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA011");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tdtla011, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA011");

                //8.紀錄人員
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA012");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tdtla012, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA012");

                //8.書件名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tswc005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC005");

                //8.書件名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("DTLA013");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLA013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("DTLA013");

                //9~20.相片說明文字
                string[] arrayPicRemark = new string[] { tdtla022, tdtla024, tdtla026, tdtla028, tdtla030, tdtla032 };
                string[] arrayPDFView05 = new string[] { "DTLA022", "DTLA024", "DTLA026", "DTLA028", "DTLA030", "DTLA032" };

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

                //相關單位及人員簽名
                if (tDTLA018.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLA018;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(490), Convert.ToSingle(600));

                    x = Convert.ToSingle(290+5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(605-160 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(2).AddImage(pdfimageobj);
                }

                //加圖片1進去, A4 大小 寬:0~595 高:0~842
                if (tdtla021.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla021;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(501), Convert.ToSingle(248));

                    x = Convert.ToSingle(290+9.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(605-2 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }
                //加圖片2進去
                if (tdtla023.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla023;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(501), Convert.ToSingle(248));

                    x = Convert.ToSingle(290+9.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(262 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(3).AddImage(pdfimageobj);
                }
                //加圖片3進去
                if (tdtla025.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla025;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(501), Convert.ToSingle(248));

                    x = Convert.ToSingle(290+9.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(605-2 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
                }
                //加圖片4進去
                if (tdtla027.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla027;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(501), Convert.ToSingle(248));

                    x = Convert.ToSingle(290+9.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(262 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
                }
                //加圖片5進去
                if (tdtla029.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla029;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(501), Convert.ToSingle(248));

                    x = Convert.ToSingle(290+9.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(605-2 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                //加圖片6進去
                if (tdtla031.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tdtla031;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(501), Convert.ToSingle(248));

                    x = Convert.ToSingle(290+9.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(262 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                ////
                //int tLastPage = 6;

                //string ExeSqlStrL = "select DTLR01,DTLR02 from SWCDTL01R";
                //ExeSqlStrL = ExeSqlStrL + " where SWC000 = '" + rCaseId + "' ";
                //ExeSqlStrL = ExeSqlStrL + "   and DTLA001 = '" + rDTLId + "' ";

                //using (SqlConnection SwcConnL = new SqlConnection(connectionString.ConnectionString))
                //{
                //    SwcConnL.Open();
                    
                //    SqlDataReader readeSwcL;
                //    SqlCommand objCmdSwcL = new SqlCommand(ExeSqlStrL, SwcConnL);
                //    readeSwcL = objCmdSwcL.ExecuteReader();


                //    //document.Add(new Paragraph("審查意見：\n", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12, iTextSharp.text.Element.ALIGN_LEFT, new BaseColor(0, 0, 0))));

                //    while (readeSwcL.Read())
                //    {
                //        string ListText = readeSwcL["DTLR02"] + "";

                //        if (tLastPage == 6) { ListText = "審查意見：\n" + ListText; }

                //        Rectangle pagesize;
                //        Rectangle rect = Pdfreader.GetCropBox(1);
                //        Pdfstamper.InsertPage(tLastPage, rect);



                //        //浮水印
                //        //PdfContentByte over = Pdfstamper.GetOverContent(tLastPage);
                //        pagesize = Pdfreader.GetPageSize(tLastPage);
                //        float xx = (pagesize.Left + pagesize.Right) / 2;
                //        float yy = (pagesize.Bottom + pagesize.Top) / 2;
                //        //PdfGState gs = new PdfGState();
                //        //gs.FillOpacity = 0.3f;
                //        //over.SaveState();
                //        //over.SetGState(gs);
                //        //over.SetRGBColorFill(200, 200, 0);
                //        //ColumnText.ShowTextAligned(over, Element.ALIGN_CENTER,
                //        //    new Phrase("????", new Font(Font.FontFamily.HELVETICA, 120)),
                //        //    xx, yy, 45);
                //        //over.RestoreState();
                        
                //        // PdfContentByte from stamper to add content to the pages over the original content
                //        PdfContentByte pbover = Pdfstamper.GetOverContent(tLastPage);

                //        //請注意，不設定字型不出現中文，add content to the page using ColumnText
                //        BaseFont baseFont = BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf",BaseFont.IDENTITY_H,BaseFont.EMBEDDED);
                //        Font font = new Font(baseFont);
                //        font.Size = 12;
                //        xx = 100;yy +=360;

                //        ColumnText.ShowTextAligned(pbover, Element.ALIGN_CENTER, new Phrase(ListText, font), xx, yy, 0);

                //        tLastPage++;
                //    }

                //    readeSwcL.Close();
                //    objCmdSwcL.Dispose();

                //}
            }
            readeSwc.Close();
            objCmdSwc.Dispose();
            Pdfstamper.Close();
            Pdfreader.Close();


            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            //新增審查意見pdf
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewnameR), FileMode.Create));
            string ExeSqlStrR = "select DTLR01,DTLR02 from SWCDTL01R";
            ExeSqlStrR = ExeSqlStrR + " where SWC000 = '" + rCaseId + "' ";
            ExeSqlStrR = ExeSqlStrR + "   and DTLA001 = '" + rDTLId + "' ";
            ExeSqlStrR = ExeSqlStrR + " order by DTLR01 ";

            using (SqlConnection SwcConnR = new SqlConnection(connectionString.ConnectionString))
            {
                SwcConnR.Open();
                document.Open();

                int pcount = 0;

                SqlDataReader readeSwcR;
                SqlCommand objCmdSwcR = new SqlCommand(ExeSqlStrR, SwcConn);
                readeSwcR = objCmdSwcR.ExecuteReader();
                document.Add(new Paragraph("審查意見：\n", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12, iTextSharp.text.Element.ALIGN_LEFT, new BaseColor(0, 0, 0))));

                while (readeSwcR.Read())
                {

                    pcount++;

                    string tdtlr02 = readeSwcR["DTLR02"] + "";

                    document.Add(new Paragraph(tdtlr02.ToString() + "\n", new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12, iTextSharp.text.Element.ALIGN_LEFT, new BaseColor(0, 0, 0))));
                    document.NewPage();
                }

                readeSwcR.Close();
                objCmdSwcR.Dispose();
                document.Close();

            }

            //插入審查意見  
            //PdfReader reader = new PdfReader(Server.MapPath("~/OutputFile/" + pdfnewname));
            PdfReader readerR = new PdfReader(Server.MapPath("~/OutputFile/" + pdfnewnameR));
            var imp = Pdfstamper.GetImportedPage(readerR, 1);
            Pdfstamper.Close();
            readerR.Close();
        }

        string[] pdflist = new string[2];
        pdflist[0] = pdfnewname;
        pdflist[1] = pdfnewnameR;
        mergePDFFiles(pdflist, pdfnewnameS);


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@



        //把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        System.IO.Stream iStream;
        // 以10K為單位暫存:
        Byte[] buffer = new byte[10000];
        int length = 0;
        long dataToRead = 0;

        // 制定文件路徑
        string filepath = Server.MapPath("~\\OutputFile\\" + pdfnewnameS);
        string filepathm = Server.MapPath("~\\OutputFile\\m" + pdfnewnameS);

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
            File.Delete(Server.MapPath("~\\OutputFile\\" + pdfnewname));
            File.Delete(Server.MapPath("~\\OutputFile\\" + pdfnewnameR));
        }
    }
    private void mergePDFFiles(string[] fileList, string outMergeFile)
    {
        outMergeFile = Server.MapPath("~/OutputFile/" + outMergeFile);
        PdfReader reader;
        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outMergeFile, FileMode.Create));
        document.Open();
        PdfContentByte cb = writer.DirectContent;
        PdfImportedPage newPage;
        for (int i = 0; i < fileList.Length; i++)
        {
            reader = new PdfReader(Server.MapPath("~/OutputFile/"+fileList[i]));
            int iPageNum = reader.NumberOfPages;
            for (int j = 1; j <= iPageNum; j++)
            {
                document.NewPage();
                newPage = writer.GetImportedPage(reader, j);
                cb.AddTemplate(newPage, 0, 0);
            }
        }
        document.Close();
    }
}