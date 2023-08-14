using iTextSharp.text;
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
        GBClass001 SBApp = new GBClass001();
        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string ExeSqlStr = " select SWC.SWC005,SWC.SWC007,SWC.SWC013,SWC.SWC013ID,SWC.SWC013TEL,SWC.SWC014,SWC.SWC038,SWC.SWC039,SWC.SWC043,SWC.SWC044,SWC.SWC045,SWC.SWC045ID,SWC.SWC051,SWC.SWC052,D3.* from SWCDTL03 D3 ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON D3.SWC000=SWC.SWC000 ";
        ExeSqlStr = ExeSqlStr + " where D3.SWC000 = '" + rCaseId + "' ";
        ExeSqlStr = ExeSqlStr + "   and D3.DTLC000 = '" + rDTLId + "' ";

        string dSWC005="", dSWC013="", dSWC013ID="", dSWC013TEL="", dSWC014="", dSWC038="", dSWC039="", dSWC03839="", dSWC043 = "", dSWC044 = "", dSWC04344= "", dSWC045ID = "", dSWC045="", dSWC051="", dSWC052="", dT002 = "", dT005="", dT006 = "", dT007 = "", dT008 = "", dT009 = "", dT010 = "", dT011 = "", dT012 = "", dT013 = "", dT014 = "", dT015 = "", dT016 = "", dT017 = "", dT018 = "", dT019 = "", dT020 = "", dT021 = "", dT022 = "", dT023 = "", dT024 = "", dT025 = "", dT026 = "", dT027 = "", dT028 = "", dT029 = "", dT030 = "", dT031 = "", dT032 = "", dT033 = "", dT034 = "", dT035 = "", dT036 = "", dT037 = "", dT038 = "", dT039 = "", dT040 = "", dT041 = "", dT042 = "", dT043 = "", dT044 = "", dT045 = "", dT046 = "", dT047 = "", dT048 ="", dT049="", dT050="", dT051 = "", dT052 = "", dT055="", dT057="", dT058="", dT059="", dT060="", dT061="", dT062="", dT063="", dT064="", dT065="", dT066="", dT067 ="", dT068="", dT071="", dT072="", dT073="", dT074="", dT075="", savedate="", saveday="";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(ExeSqlStr, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                dSWC005 = readeSwc["SWC005"] + "";
                dSWC013 = readeSwc["SWC013"] + "";
                dSWC013ID = readeSwc["SWC013ID"] + "";
				dSWC013TEL = readeSwc["SWC013TEL"] + "";
                dSWC014 = readeSwc["SWC014"] + "";
                dSWC038 = readeSwc["SWC038"] + "";
                dSWC039 = readeSwc["SWC039"] + "";
                dSWC043 = readeSwc["SWC043"] + "";
                dSWC044 = readeSwc["SWC044"] + "";
                dSWC045 = readeSwc["SWC045"] + "";
                dSWC045ID = readeSwc["SWC045ID"] + "";
                dSWC051 = readeSwc["SWC051"] + "";
                dSWC052 = readeSwc["SWC052"] + "";
                dT002 = readeSwc["DTLC002"] + "";
                dT005 = readeSwc["DTLC005"] + ""; 
                dT006 = readeSwc["DTLC006"] + "";
                dT007 = readeSwc["DTLC007"] + "";
                dT008 = readeSwc["DTLC008"] + "";
                dT009 = readeSwc["DTLC009"] + "";
                dT010 = readeSwc["DTLC010"] + "";
                dT011 = readeSwc["DTLC011"] + "";
                dT012 = readeSwc["DTLC012"] + "";
                dT013 = readeSwc["DTLC013"] + "";
                dT014 = readeSwc["DTLC014"] + "";
                dT015 = readeSwc["DTLC015"] + "";
                dT016 = readeSwc["DTLC016"] + "";
                dT017 = readeSwc["DTLC017"] + "";
                dT018 = readeSwc["DTLC018"] + "";
                dT019 = readeSwc["DTLC019"] + "";
                dT020 = readeSwc["DTLC020"] + "";
                dT021 = readeSwc["DTLC021"] + "";
                dT022 = readeSwc["DTLC022"] + "";
                dT023 = readeSwc["DTLC023"] + "";
                dT024 = readeSwc["DTLC024"] + "";
                dT025 = readeSwc["DTLC025"] + "";
                dT026 = readeSwc["DTLC026"] + "";
                dT027 = readeSwc["DTLC027"] + "";
                dT028 = readeSwc["DTLC028"] + "";
                dT029 = readeSwc["DTLC029"] + "";
                dT030 = readeSwc["DTLC030"] + "";
                dT031 = readeSwc["DTLC031"] + "";
                dT032 = readeSwc["DTLC032"] + "";
                dT033 = readeSwc["DTLC033"] + "";
                dT034 = readeSwc["DTLC034"] + "";
                dT035 = readeSwc["DTLC035"] + "";
                dT036 = readeSwc["DTLC036"] + "";
                dT037 = readeSwc["DTLC037"] + "";
                dT038 = readeSwc["DTLC038"] + "";
                dT039 = readeSwc["DTLC039"] + "";
                dT040 = readeSwc["DTLC040"] + "";
                dT041 = readeSwc["DTLC041"] + "";
                dT042 = readeSwc["DTLC042"] + "";
                dT043 = readeSwc["DTLC043"] + "";
                dT044 = readeSwc["DTLC044"] + "";
                dT045 = readeSwc["DTLC045"] + "";
                dT046 = readeSwc["DTLC046"] + "";
                dT047 = readeSwc["DTLC047"] + "";
                dT048 = readeSwc["DTLC048"] + "";
                dT049 = readeSwc["DTLC049"] + "";
                dT050 = readeSwc["DTLC050"] + "";
                dT051 = readeSwc["DTLC051"] + "";
                dT052 = readeSwc["DTLC052"] + "";
                dT055 = readeSwc["DTLC055"] + "";
                dT057 = readeSwc["DTLC057"] + "";
                dT058 = readeSwc["DTLC058"] + "";
                dT059 = readeSwc["DTLC059"] + "";
                dT060 = readeSwc["DTLC060"] + "";
                dT061 = readeSwc["DTLC061"] + "";
                dT062 = readeSwc["DTLC062"] + "";
                dT063 = readeSwc["DTLC063"] + "";
                dT064 = readeSwc["DTLC064"] + "";
                dT065 = readeSwc["DTLC065"] + "";
                dT066 = readeSwc["DTLC066"] + "";
                dT067 = readeSwc["DTLC067"] + "";
                dT068 = readeSwc["DTLC068"] + "";
                dT071 = readeSwc["DTLC071"] + "";
                dT072 = readeSwc["DTLC072"] + "";
                dT073 = readeSwc["DTLC073"] + "";
                dT074 = readeSwc["DTLC074"] + "";
                dT075 = readeSwc["DTLC075"] + "";
                savedate = readeSwc["savedate"] + "";
				
				
                DateTime tDate1 = Convert.ToDateTime(SBApp.DateView(dT002, "00"));
                DateTime tDate2 = Convert.ToDateTime(SBApp.DateView(savedate, "00"));

                TimeSpan ts = tDate2 - tDate1;
                double days = ts.TotalDays;

                if (days==0) { saveday = "當日送達"; } else { saveday = days.ToString()+"天"; }

                dT002 = Convert.ToString(Convert.ToDateTime(dT002).Year - 1911) +"年"+ Convert.ToString(Convert.ToDateTime(dT002).Month) + "月" + Convert.ToString(Convert.ToDateTime(dT002).Day)+"日";
                dSWC03839 = "臺北市政府" + Convert.ToDateTime(dSWC038).ToString("yyyy-MM-dd") + dSWC039;
                dSWC04344 = "臺北市政府" + Convert.ToDateTime(dSWC043).ToString("yyyy-MM-dd") + dSWC044;
                dSWC051 = SBApp.DateView(dSWC051, "00");
                dSWC052 = SBApp.DateView(dSWC052, "00"); 
                dT049 = SBApp.DateView(dT049, "00"); 
                dT074 = SBApp.DateView(dT074, "00"); 
                savedate = SBApp.DateView(savedate, "00");
            }


        }


        var doc1 = new Document(PageSize.A4, 30, 30, 30, 30);
        MemoryStream Memory = new MemoryStream();
        PdfWriter PdfWriter = PdfWriter.GetInstance(doc1, Memory);

        BaseFont bfChinese = BaseFont.CreateFont(@"C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        Font ChFont = new Font(bfChinese, 12);
        Font ChFont_title = new Font(bfChinese, 22, Font.BOLD);
        Font ChFont_PS = new Font(bfChinese, 15);

        doc1.Open();
        PdfPTable table1 = new PdfPTable(new float[] { 1, 5 ,10});  //2欄
        table1.TotalWidth = 500f;
        table1.LockedWidth = true;

        PdfPCell header_1 = new PdfPCell(new Phrase("水土保持施工監督檢查紀錄", ChFont_title));
        header_1.Colspan = 3;   //跨欄
        header_1.MinimumHeight = 40f;//設定行高
        header_1.HorizontalAlignment = Element.ALIGN_CENTER;
        header_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        header_1.Border = PdfPCell.NO_BORDER;
        
        table1.AddCell(header_1);

        string dataT2 = "檢查日期："+ dT002 + "      " + "案件編號："+ rDTLId;

        PdfPCell header_2 = new PdfPCell(new Phrase(dataT2, ChFont));
        header_2.Colspan = 3;   //跨欄
        header_2.MinimumHeight = 35f;//設定行高
        header_2.HorizontalAlignment = Element.ALIGN_CENTER;
        header_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        header_2.Border = PdfPCell.NO_BORDER;
        
        table1.AddCell(header_2);

        PdfPCell t1_1 = new PdfPCell(new Phrase("水\r\n土\r\n保\r\n持\r\n書\r\n件", ChFont));
        t1_1.Rowspan = 7;   //跨欄
        t1_1.MinimumHeight = 18f;//設定行高
        t1_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t1_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t1_1);

        PdfPCell t1_2 = new PdfPCell(new Phrase("計畫名稱", ChFont));
        t1_2.MinimumHeight = 18f;//設定行高
        t1_2.HorizontalAlignment = Element.ALIGN_CENTER;
        t1_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t1_2);

        PdfPCell t1_3 = new PdfPCell(new Phrase(dSWC005, ChFont));
        //t1_2.Colspan = 3;
        t1_3.MinimumHeight = 18f;//設定行高
        t1_3.HorizontalAlignment = Element.ALIGN_LEFT;
        t1_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t1_3);

        PdfPCell t2_1 = new PdfPCell(new Phrase("核定日期文號", ChFont));
        t2_1.MinimumHeight = 18f;//設定行高
        t2_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t2_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t2_1);

        PdfPCell t2_2 = new PdfPCell(new Phrase(dSWC03839, ChFont));
        t2_2.MinimumHeight = 18f;//設定行高
        t2_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t2_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t2_2);

        PdfPCell t3_1 = new PdfPCell(new Phrase("水土保持施工許可證\r\n日期文號", ChFont));
        t3_1.MinimumHeight = 18f;//設定行高
        t3_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t3_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t3_1);

        PdfPCell t3_2 = new PdfPCell(new Phrase(dSWC04344, ChFont));
        t3_2.MinimumHeight = 18f;//設定行高
        t3_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t3_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t3_2);

        PdfPCell t4_1 = new PdfPCell(new Phrase("開工日期", ChFont));
        t4_1.MinimumHeight = 18f;//設定行高
        t4_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t4_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t4_1);

        PdfPCell t4_2 = new PdfPCell(new Phrase(dSWC051, ChFont));
        t4_2.MinimumHeight = 18f;//設定行高
        t4_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t4_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t4_2);

        PdfPCell t5_1 = new PdfPCell(new Phrase("預定完工日期", ChFont));
        t5_1.MinimumHeight = 18f;//設定行高
        t5_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t5_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t5_1);

        PdfPCell t5_2 = new PdfPCell(new Phrase(dSWC052, ChFont));
        t5_2.MinimumHeight = 18f;//設定行高
        t5_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t5_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t5_2);
		
		PdfPCell t5_11 = new PdfPCell(new Phrase("工程進度", ChFont));
        t5_11.MinimumHeight = 18f;//設定行高
        t5_11.HorizontalAlignment = Element.ALIGN_CENTER;
        t5_11.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t5_11);

        PdfPCell t5_21 = new PdfPCell(new Phrase(dT075, ChFont));
        t5_21.MinimumHeight = 18f;//設定行高
        t5_21.HorizontalAlignment = Element.ALIGN_LEFT;
        t5_21.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t5_21);
		
		PdfPCell t5_12 = new PdfPCell(new Phrase("檢查結果", ChFont));
        t5_12.MinimumHeight = 18f;//設定行高
        t5_12.HorizontalAlignment = Element.ALIGN_CENTER;
        t5_12.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t5_12);

        PdfPCell t5_22 = new PdfPCell(new Phrase(dT071, ChFont));
        t5_22.MinimumHeight = 18f;//設定行高
        t5_22.HorizontalAlignment = Element.ALIGN_LEFT;
        t5_22.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t5_22);

        //區22222222
        PdfPCell t6_1 = new PdfPCell(new Phrase("水\r\n土\r\n保\r\n持\r\n義\r\n務\r\n人", ChFont));
        t6_1.Rowspan = 3;   //跨欄
        t6_1.MinimumHeight = 18f;//設定行高
        t6_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t6_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t6_1);

        PdfPCell t7_2 = new PdfPCell(new Phrase("姓名或名稱", ChFont));
        t7_2.MinimumHeight = 18f;//設定行高
        t7_2.HorizontalAlignment = Element.ALIGN_CENTER;
        t7_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t7_2);

        PdfPCell t7_3 = new PdfPCell(new Phrase(dSWC013, ChFont));
        t7_3.MinimumHeight = 18f;//設定行高
        t7_3.HorizontalAlignment = Element.ALIGN_LEFT;
        t7_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t7_3);

        //PdfPCell t8_1 = new PdfPCell(new Phrase("身分證或\r\n營利事業統一編號", ChFont));
        //t8_1.MinimumHeight = 18f;//設定行高
        //t8_1.HorizontalAlignment = Element.ALIGN_CENTER;
        //t8_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table1.AddCell(t8_1);
		//
        //PdfPCell t9_2 = new PdfPCell(new Phrase(dSWC013ID, ChFont));
        //t9_2.MinimumHeight = 18f;//設定行高
        //t9_2.HorizontalAlignment = Element.ALIGN_LEFT;
        //t9_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table1.AddCell(t9_2);

        PdfPCell t10_1 = new PdfPCell(new Phrase("住居所或營業所", ChFont));
        t10_1.MinimumHeight = 18f;//設定行高
        t10_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t10_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t10_1);

        PdfPCell t10_2 = new PdfPCell(new Phrase(dSWC014, ChFont));
        t10_2.MinimumHeight = 18f;//設定行高
        t10_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t10_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t10_2);
		
		PdfPCell t8_1 = new PdfPCell(new Phrase("電話", ChFont));
        t8_1.MinimumHeight = 18f;//設定行高
        t8_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t8_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t8_1);
		
        PdfPCell t9_2 = new PdfPCell(new Phrase(dSWC013TEL, ChFont));
        t9_2.MinimumHeight = 18f;//設定行高
        t9_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t9_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t9_2);

        //區3333333333

        PdfPCell t11_1 = new PdfPCell(new Phrase("承\r\n辦\r\n監\r\n造\r\n技\r\n師", ChFont)); 
        t11_1.Rowspan = 4;   //跨欄
        t11_1.MinimumHeight = 18f;//設定行高
        t11_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t11_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t11_1);

        PdfPCell t11_2 = new PdfPCell(new Phrase("姓名", ChFont));
        t11_2.MinimumHeight = 18f;//設定行高
        t11_2.HorizontalAlignment = Element.ALIGN_CENTER;
        t11_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t11_2);

        PdfPCell t11_3 = new PdfPCell(new Phrase(dSWC045, ChFont));
        //t1_2.Colspan = 3;
        t11_3.MinimumHeight = 18f;//設定行高
        t11_3.HorizontalAlignment = Element.ALIGN_LEFT;
        t11_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t11_3);

        PdfPCell t12_1 = new PdfPCell(new Phrase("執業機構名稱", ChFont));
        t12_1.MinimumHeight = 18f;//設定行高
        t12_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t12_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t12_1);

        PdfPCell t12_2 = new PdfPCell(new Phrase(SBApp.GetETUser(dSWC045ID, "OrgName"), ChFont));
        t12_2.MinimumHeight = 18f;//設定行高
        t12_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t12_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t12_2);

        //PdfPCell t13_1 = new PdfPCell(new Phrase("執業執照字號", ChFont));
        //t13_1.MinimumHeight = 18f;//設定行高
        //t13_1.HorizontalAlignment = Element.ALIGN_CENTER;
        //t13_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table1.AddCell(t13_1);
		//
        //PdfPCell t13_2 = new PdfPCell(new Phrase(SBApp.GetETUser(dSWC045ID, "OrgIssNo"), ChFont));
        //t13_2.MinimumHeight = 18f;//設定行高
        //t13_2.HorizontalAlignment = Element.ALIGN_LEFT;
        //t13_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table1.AddCell(t13_2);
		//
        //PdfPCell t14_1 = new PdfPCell(new Phrase("營利事業統一編號", ChFont));
        //t14_1.MinimumHeight = 18f;//設定行高
        //t14_1.HorizontalAlignment = Element.ALIGN_CENTER;
        //t14_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table1.AddCell(t14_1);
		//
        //PdfPCell t14_2 = new PdfPCell(new Phrase(SBApp.GetETUser(dSWC045ID, "OrgGUINo"), ChFont));
        //t14_2.MinimumHeight = 18f;//設定行高
        //t14_2.HorizontalAlignment = Element.ALIGN_LEFT;
        //t14_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table1.AddCell(t14_2);
		
		PdfPCell t13_1 = new PdfPCell(new Phrase("執業機構地址", ChFont));
        t13_1.MinimumHeight = 18f;//設定行高
        t13_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t13_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t13_1);
		
        PdfPCell t13_2 = new PdfPCell(new Phrase(SBApp.GetETUser(dSWC045ID, "OrgAddr"), ChFont));
        t13_2.MinimumHeight = 18f;//設定行高
        t13_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t13_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t13_2);

        PdfPCell t15_1 = new PdfPCell(new Phrase("電話", ChFont));
        t15_1.MinimumHeight = 18f;//設定行高
        t15_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t15_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t15_1);

        PdfPCell t15_2 = new PdfPCell(new Phrase(SBApp.GetETUser(dSWC045ID, "OrgTel"), ChFont));
        t15_2.MinimumHeight = 18f;//設定行高
        t15_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t15_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t15_2);

        //lastline
        PdfPCell t16_1 = new PdfPCell(new Phrase("實施地點土地標示", ChFont));
        t16_1.Colspan = 2;   //跨欄
        t16_1.MinimumHeight = 18f;//設定行高
        t16_1.HorizontalAlignment = Element.ALIGN_CENTER;
        t16_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t16_1);

        PdfPCell t16_2 = new PdfPCell(new Phrase(dT005, ChFont));
        t16_2.MinimumHeight = 18f;//設定行高
        t16_2.HorizontalAlignment = Element.ALIGN_LEFT;
        t16_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        table1.AddCell(t16_2);

        doc1.Add(table1);
        //doc1.NewPage();

        //**********************************

        PdfPTable table2 = new PdfPTable(new float[] { 4,6,2 });
        table2.TotalWidth = 500f;
        table2.LockedWidth = true;

        PdfPCell table2_11 = new PdfPCell(new Phrase("一、檢查項目", ChFont));
        table2_11.MinimumHeight = 25f;//設定行高
        table2_11.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_11.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_11);

        PdfPCell table2_12 = new PdfPCell(new Phrase("目前執行情形", ChFont));
        table2_12.MinimumHeight = 25f;//設定行高
        table2_12.HorizontalAlignment = Element.ALIGN_CENTER;
        table2_12.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_12);

        PdfPCell table2_13 = new PdfPCell(new Phrase("備註", ChFont));
        table2_13.MinimumHeight = 25f;//設定行高
        table2_13.HorizontalAlignment = Element.ALIGN_CENTER;
        table2_13.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_13);

        PdfPCell table2_21 = new PdfPCell(new Phrase(" （一）水土保持施工", ChFont));
        table2_21.MinimumHeight = 25f;//設定行高
        table2_21.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_21.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_21);

        string dataT222 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT006)
        {
            case "依計畫施作":
                dataT222 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT222 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT222 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT007  = "無此項 \n" + dT007;
                break;
        }
        PdfPCell table2_22 = new PdfPCell(new Phrase(dT006, ChFont));
        table2_22.MinimumHeight = 25f;//設定行高
        table2_22.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_22.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_22);

        PdfPCell table2_23 = new PdfPCell(new Phrase(dT007, ChFont));
        table2_23.MinimumHeight = 25f;//設定行高
        table2_23.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_23.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_23);

        PdfPCell table2_31 = new PdfPCell(new Phrase(" （二）開發範圍界樁", ChFont));
        table2_31.MinimumHeight = 25f;//設定行高
        table2_31.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_31.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_31);

        string dataT232 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT008)
        {
            case "依計畫施作":
                dataT232 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT232 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT232 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT009 = "無此項 \n" + dT009;
                break;
        }
        PdfPCell table2_32 = new PdfPCell(new Phrase(dT008, ChFont));
        table2_32.MinimumHeight = 25f;//設定行高
        table2_32.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_32.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_32);

        PdfPCell table2_33 = new PdfPCell(new Phrase(dT009, ChFont));
        table2_33.MinimumHeight = 25f;//設定行高
        table2_33.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_33.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_33);

        PdfPCell table2_41 = new PdfPCell(new Phrase(" （三）開挖整地範圍界樁", ChFont));
        table2_41.MinimumHeight = 25f;//設定行高
        table2_41.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_41.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_41);

        string dataT242 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT010)
        {
            case "依計畫施作":
                dataT242 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT242 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT242 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT011 = "無此項 \n" + dT011;
                break;
        }
        PdfPCell table2_42 = new PdfPCell(new Phrase(dT010, ChFont));
        table2_42.MinimumHeight = 25f;//設定行高
        table2_42.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_42.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_42);

        PdfPCell table2_43 = new PdfPCell(new Phrase(dT011, ChFont));
        table2_43.MinimumHeight = 25f;//設定行高
        table2_43.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_43.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_43);

        PdfPCell table2_51 = new PdfPCell(new Phrase(" （四）臨時性防災措施", ChFont));
        table2_51.MinimumHeight = 25f;//設定行高
        table2_51.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_51.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_51);

        PdfPCell table2_52 = new PdfPCell(new Phrase("", ChFont));
        table2_52.MinimumHeight = 25f;//設定行高
        table2_52.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_52.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_52);

        PdfPCell table2_53 = new PdfPCell(new Phrase("", ChFont));
        table2_53.MinimumHeight = 25f;//設定行高
        table2_53.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_53.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_53);

        PdfPCell table2_61 = new PdfPCell(new Phrase("    1.排水設施", ChFont));
        table2_61.MinimumHeight = 25f;//設定行高
        table2_61.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_61.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_61);

        string dataT262 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT012)
        {
            case "依計畫施作":
                dataT262 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT262 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT262 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT013 = "無此項 \n" + dT013;
                break;
        }
        PdfPCell table2_62 = new PdfPCell(new Phrase(dT012, ChFont));
        table2_62.MinimumHeight = 25f;//設定行高
        table2_62.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_62.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_62);

        PdfPCell table2_63 = new PdfPCell(new Phrase(dT013, ChFont));
        table2_63.MinimumHeight = 25f;//設定行高
        table2_63.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_63.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_63);

        PdfPCell table2_71 = new PdfPCell(new Phrase("    2.沉砂設施", ChFont));
        table2_71.MinimumHeight = 25f;//設定行高
        table2_71.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_71.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_71);

        string dataT272 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT014)
        {
            case "依計畫施作":
                dataT272 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT272 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT272 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT015 = "無此項 \n" + dT015;
                break;
        }
        PdfPCell table2_72 = new PdfPCell(new Phrase(dT014, ChFont));
        table2_72.MinimumHeight = 25f;//設定行高
        table2_72.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_72.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_72);

        PdfPCell table2_73 = new PdfPCell(new Phrase(dT015, ChFont));
        table2_73.MinimumHeight = 25f;//設定行高
        table2_73.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_73.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_73);

        PdfPCell table2_81 = new PdfPCell(new Phrase("    3.滯洪設施", ChFont));
        table2_81.MinimumHeight = 25f;//設定行高
        table2_81.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_81.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_81);

        string dataT282 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT016)
        {
            case "依計畫施作":
                dataT282 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT282 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT282 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT017 = "無此項 \n" + dT017;
                break;
        }
        PdfPCell table2_82 = new PdfPCell(new Phrase(dT016, ChFont));
        table2_82.MinimumHeight = 25f;//設定行高
        table2_82.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_82.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_82);

        PdfPCell table2_93 = new PdfPCell(new Phrase(dT017, ChFont));
        table2_93.MinimumHeight = 25f;//設定行高
        table2_93.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_93.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_93);

        PdfPCell table2_101 = new PdfPCell(new Phrase("    4.土方暫置", ChFont));
        table2_101.MinimumHeight = 25f;//設定行高
        table2_101.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_101.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_101);

        string dataT2102 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT018)
        {
            case "依計畫施作":
                dataT2102 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2102 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2102 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT019 = "無此項 \n" + dT019;
                break;
        }
        PdfPCell table2_102 = new PdfPCell(new Phrase(dT018, ChFont));
        table2_102.MinimumHeight = 25f;//設定行高
        table2_102.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_102.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_102);

        PdfPCell table2_103 = new PdfPCell(new Phrase(dT019, ChFont));
        table2_103.MinimumHeight = 25f;//設定行高
        table2_103.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_103.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_103);

        PdfPCell table2_111 = new PdfPCell(new Phrase("    5.邊坡保護措施", ChFont));
        table2_111.MinimumHeight = 25f;//設定行高
        table2_111.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_111.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_111);

        string dataT2112 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT020)
        {
            case "依計畫施作":
                dataT2112 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2112 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2112 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT021 = "無此項 \n" + dT021;
                break;
        }
        PdfPCell table2_112 = new PdfPCell(new Phrase(dT020, ChFont));
        table2_112.MinimumHeight = 25f;//設定行高
        table2_112.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_112.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_112);

        PdfPCell table2_113 = new PdfPCell(new Phrase(dT021, ChFont));
        table2_113.MinimumHeight = 25f;//設定行高
        table2_113.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_113.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_113);

        PdfPCell table2_121 = new PdfPCell(new Phrase("    6.施工便道", ChFont));
        table2_121.MinimumHeight = 25f;//設定行高
        table2_121.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_121.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_121);

        string dataT2122 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT022)
        {
            case "依計畫施作":
                dataT2122 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2122 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2122 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT023 = "無此項 \n" + dT023;
                break;
        }
        PdfPCell table2_122 = new PdfPCell(new Phrase(dT022, ChFont));
        table2_122.MinimumHeight = 25f;//設定行高
        table2_122.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_122.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_122);

        PdfPCell table2_123 = new PdfPCell(new Phrase(dT023, ChFont));
        table2_123.MinimumHeight = 25f;//設定行高
        table2_123.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_123.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_123);
		
		PdfPCell table2_1211 = new PdfPCell(new Phrase("    7.臨時攔砂設施(如砂包、防溢座等)", ChFont));
        table2_1211.MinimumHeight = 25f;//設定行高
        table2_1211.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_1211.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_1211);

        PdfPCell table2_1221 = new PdfPCell(new Phrase(dT072, ChFont));
        table2_1221.MinimumHeight = 25f;//設定行高
        table2_1221.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_1221.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_1221);

        PdfPCell table2_1231 = new PdfPCell(new Phrase(dT073, ChFont));
        table2_1231.MinimumHeight = 25f;//設定行高
        table2_1231.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_1231.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_1231);

        PdfPCell table2_131 = new PdfPCell(new Phrase("    8.其他", ChFont));
        table2_131.MinimumHeight = 25f;//設定行高
        table2_131.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_131.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_131);

        string dataT2132 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT024)
        {
            case "依計畫施作":
                dataT2132 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2132 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2132 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT025 = "無此項 \n" + dT025;
                break;
        }
        PdfPCell table2_132 = new PdfPCell(new Phrase(dT024, ChFont));
        table2_132.MinimumHeight = 25f;//設定行高
        table2_132.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_132.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_132);

        PdfPCell table2_133 = new PdfPCell(new Phrase(dT025, ChFont));
        table2_133.MinimumHeight = 25f;//設定行高
        table2_133.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_133.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_133);

        PdfPCell table2_141 = new PdfPCell(new Phrase(" （五）永久性防災措施", ChFont));
        table2_141.MinimumHeight = 25f;//設定行高
        table2_141.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_141.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_141);

        PdfPCell table2_142 = new PdfPCell(new Phrase("", ChFont));
        table2_142.MinimumHeight = 25f;//設定行高
        table2_142.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_142.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_142);

        PdfPCell table2_153 = new PdfPCell(new Phrase("", ChFont));
        table2_153.MinimumHeight = 25f;//設定行高
        table2_153.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_153.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_153);

        PdfPCell table2_161 = new PdfPCell(new Phrase("    1.排水設施", ChFont));
        table2_161.MinimumHeight = 25f;//設定行高
        table2_161.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_161.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_161);

        string dataT2162 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT026)
        {
            case "依計畫施作":
                dataT2162 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2162 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2162 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT027 = "無此項 \n" + dT027;
                break;
        }
        PdfPCell table2_162 = new PdfPCell(new Phrase(dT026, ChFont));
        table2_162.MinimumHeight = 25f;//設定行高
        table2_162.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_162.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_162);

        PdfPCell table2_163 = new PdfPCell(new Phrase(dT027, ChFont));
        table2_163.MinimumHeight = 25f;//設定行高
        table2_163.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_163.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_163);

        PdfPCell table2_171 = new PdfPCell(new Phrase("    2.沉砂設施", ChFont));
        table2_171.MinimumHeight = 25f;//設定行高
        table2_171.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_171.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_171);

        string dataT2172 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT028)
        {
            case "依計畫施作":
                dataT2172 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2172 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2172 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT029 = "無此項 \n" + dT029;
                break;
        }
        PdfPCell table2_172 = new PdfPCell(new Phrase(dT028, ChFont));
        table2_172.MinimumHeight = 25f;//設定行高
        table2_172.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_172.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_172);

        PdfPCell table2_173 = new PdfPCell(new Phrase(dT029, ChFont));
        table2_173.MinimumHeight = 25f;//設定行高
        table2_173.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_173.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_173);

        PdfPCell table2_181 = new PdfPCell(new Phrase("    3.滯洪設施", ChFont));
        table2_181.MinimumHeight = 25f;//設定行高
        table2_181.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_181.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_181);

        string dataT2182 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT030)
        {
            case "依計畫施作":
                dataT2182 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2182 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2182 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT031 = "無此項 \n" + dT031;
                break;
        }
        PdfPCell table2_182 = new PdfPCell(new Phrase(dT030, ChFont));
        table2_182.MinimumHeight = 25f;//設定行高
        table2_182.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_182.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_182);

        PdfPCell table2_193 = new PdfPCell(new Phrase(dT031, ChFont));
        table2_193.MinimumHeight = 25f;//設定行高
        table2_193.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_193.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_193);

        PdfPCell table2_201 = new PdfPCell(new Phrase("    4.聯外排水", ChFont));
        table2_201.MinimumHeight = 25f;//設定行高
        table2_201.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_201.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_201);

        string dataT2202 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT032)
        {
            case "依計畫施作":
                dataT2202 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2202 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2202 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT033 = "無此項 \n" + dT033;
                break;
        }
        PdfPCell table2_202 = new PdfPCell(new Phrase(dT032, ChFont));
        table2_202.MinimumHeight = 25f;//設定行高
        table2_202.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_202.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_202);

        PdfPCell table2_203 = new PdfPCell(new Phrase(dT033, ChFont));
        table2_203.MinimumHeight = 25f;//設定行高
        table2_203.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_203.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_203);

        PdfPCell table2_211 = new PdfPCell(new Phrase("    5.擋土設施", ChFont));
        table2_211.MinimumHeight = 25f;//設定行高
        table2_211.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_211.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_211);

        string dataT2212 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT034)
        {
            case "依計畫施作":
                dataT2212 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2212 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2212 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT035 = "無此項 \n" + dT035;
                break;
        }
        PdfPCell table2_212 = new PdfPCell(new Phrase(dT034, ChFont));
        table2_212.MinimumHeight = 25f;//設定行高
        table2_212.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_212.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_212);

        PdfPCell table2_213 = new PdfPCell(new Phrase(dT035, ChFont));
        table2_213.MinimumHeight = 25f;//設定行高
        table2_213.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_213.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_213);

        PdfPCell table2_221 = new PdfPCell(new Phrase("    6.植生工程", ChFont));
        table2_221.MinimumHeight = 25f;//設定行高
        table2_221.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_221.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_221);

        string dataT2222 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT036)
        {
            case "依計畫施作":
                dataT2222 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2222 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2222 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT037 = "無此項 \n" + dT037;
                break;
        }
        PdfPCell table2_222 = new PdfPCell(new Phrase(dT036, ChFont));
        table2_222.MinimumHeight = 25f;//設定行高
        table2_222.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_222.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_222);

        PdfPCell table2_223 = new PdfPCell(new Phrase(dT037, ChFont));
        table2_223.MinimumHeight = 25f;//設定行高
        table2_223.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_223.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_223);

        PdfPCell table2_231 = new PdfPCell(new Phrase("    7.邊坡穩定措施", ChFont));
        table2_231.MinimumHeight = 25f;//設定行高
        table2_231.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_231.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_231);

        string dataT2232 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT038)
        {
            case "依計畫施作":
                dataT2232 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT2232 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT2232 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT039 = "無此項 \n" + dT039;
                break;
        }
        PdfPCell table2_232 = new PdfPCell(new Phrase(dT038, ChFont));
        table2_232.MinimumHeight = 25f;//設定行高
        table2_232.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_232.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_232);

        PdfPCell table2_233 = new PdfPCell(new Phrase(dT039, ChFont));
        table2_233.MinimumHeight = 25f;//設定行高
        table2_233.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_233.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_233);

        PdfPCell table2_2311 = new PdfPCell(new Phrase("    8.其他", ChFont));
        table2_2311.MinimumHeight = 25f;//設定行高
        table2_2311.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_2311.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_2311);

        string dataT22321 = "□依計畫施作 □未依計畫施作 □尚未施作";
        switch (dT040)
        {
            case "依計畫施作":
                dataT22321 = "■依計畫施作 □未依計畫施作 □尚未施作";
                break;
            case "未依計畫施作":
                dataT22321 = "□依計畫施作 ■未依計畫施作 □尚未施作";
                break;
            case "尚未施作":
                dataT22321 = "□依計畫施作 □未依計畫施作 ■尚未施作";
                break;
            case "無此項":
                dT041 = "無此項 \n" + dT041;
                break;
        }
        PdfPCell table2_2321 = new PdfPCell(new Phrase(dT040, ChFont));
        table2_2321.MinimumHeight = 25f;//設定行高
        table2_2321.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_2321.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_2321);

        PdfPCell table2_2331 = new PdfPCell(new Phrase(dT041, ChFont));
        table2_2331.MinimumHeight = 25f;//設定行高
        table2_2331.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_2331.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_2331);

        PdfPCell table2_241 = new PdfPCell(new Phrase(" （六）承辦監造技師是否在場", ChFont));
        table2_241.MinimumHeight = 25f;//設定行高
        table2_241.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_241.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_241);

        string dataT2421 = "□是 □否";
        switch (dT042)
        {
            case "是":
                dataT2421 = "■是 □否";
                break;
            case "否":
                dataT2421 = "□是 ■否";
                break;
        }
        PdfPCell table2_242 = new PdfPCell(new Phrase(dT042, ChFont));
        table2_242.MinimumHeight = 25f;//設定行高
        table2_242.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_242.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_242);

        PdfPCell table2_243 = new PdfPCell(new Phrase(dT043, ChFont));
        table2_243.MinimumHeight = 25f;//設定行高
        table2_243.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_243.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_243);

        PdfPCell table2_251 = new PdfPCell(new Phrase(" （七）是否備妥監造紀錄", ChFont));
        table2_251.MinimumHeight = 25f;//設定行高
        table2_251.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_251.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_251);

        string dataT2521 = "□是 □否";
        switch (dT044)
        {
            case "是":
                dataT2521 = "■是 □否";
                break;
            case "否":
                dataT2521 = "□是 ■否";
                break;
        }
        PdfPCell table2_252 = new PdfPCell(new Phrase(dT044, ChFont));
        table2_252.MinimumHeight = 25f;//設定行高
        table2_252.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_252.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_252);

        PdfPCell table2_253 = new PdfPCell(new Phrase(dT045, ChFont));
        table2_253.MinimumHeight = 25f;//設定行高
        table2_253.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_253.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_253);

        PdfPCell table2_261 = new PdfPCell(new Phrase(" （八）災害搶救小組是否成立", ChFont));
        table2_261.MinimumHeight = 25f;//設定行高
        table2_261.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_261.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_261);

        string dataT2621 = "□是 □否";
        switch (dT046)
        {
            case "是":
                dataT2621 = "■是 □否";
                break;
            case "否":
                dataT2621 = "□是 ■否";
                break;
        }
        PdfPCell table2_262 = new PdfPCell(new Phrase(dT046, ChFont));
        table2_262.MinimumHeight = 25f;//設定行高
        table2_262.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_262.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_262);

        PdfPCell table2_263 = new PdfPCell(new Phrase(dT047, ChFont));
        table2_263.MinimumHeight = 25f;//設定行高
        table2_263.HorizontalAlignment = Element.ALIGN_LEFT;
        table2_263.VerticalAlignment = Element.ALIGN_MIDDLE;
        table2.AddCell(table2_263);

        doc1.Add(table2);

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        PdfPTable table3 = new PdfPTable(new float[] { 1 });
        table3.TotalWidth = 500f;
        table3.LockedWidth = true;

        PdfPCell table3_1 = new PdfPCell(new Phrase("二、通知水土保持義務人及營造單位施工缺失改正事項：\r\n"+ dT048 + "\r\n　　改正期限：" + dT049, ChFont));
        table3_1.MinimumHeight = 35f;//設定行高
        table3_1.HorizontalAlignment = Element.ALIGN_LEFT;
        table3_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table3_1.Border = PdfPCell.NO_BORDER; //無線
        table3.AddCell(table3_1);

		
        PdfPCell table3_3 = new PdfPCell(new Phrase("三、未依計畫施作事項及改正期限：\r\n"+ dT051 + "\r\n　　改正期限：" + dT074, ChFont));
        table3_3.MinimumHeight = 35f;//設定行高
        table3_3.HorizontalAlignment = Element.ALIGN_LEFT;
        table3_3.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table3_3.Border = PdfPCell.NO_BORDER; //無線
        table3.AddCell(table3_3);
		
		
        PdfPCell table3_2 = new PdfPCell(new Phrase("四、其他注意事項：\r\n" + dT050, ChFont));
        table3_2.MinimumHeight = 35f;//設定行高
        table3_2.HorizontalAlignment = Element.ALIGN_LEFT;
        table3_2.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table3_2.Border = PdfPCell.NO_BORDER; //無線
        table3.AddCell(table3_2);

        PdfPCell table3_4 = new PdfPCell(new Phrase("五、相關單位及人員簽名\r\n" + dT055, ChFont));
        table3_4.MinimumHeight = 35f;//設定行高
        table3_4.HorizontalAlignment = Element.ALIGN_LEFT;
        table3_4.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table3_4.Border = PdfPCell.NO_BORDER; //無線
        table3.AddCell(table3_4);

        string dataT35 = "　（一）本項檢查係屬行政監督檢查，檢查困難、隱蔽或不影響水保設施正常功能者（如圖面未標示之尺寸、水溝蓋板之型式、滯洪沉沙池之告示牌…等）得免查驗，應由水土保持義務人及承辦監造技師負責。\r\n";
        dataT35 += "　（二）承辦監造技師未能到場時，應以書面方式委任符合水土保持法規定之技師代理之。\r\n";
        PdfPCell table3_5 = new PdfPCell(new Phrase("六、說明：\r\n"+ dataT35, ChFont));
        table3_5.MinimumHeight = 35f;//設定行高
        table3_5.HorizontalAlignment = Element.ALIGN_LEFT;
        table3_5.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table3_5.Border = PdfPCell.NO_BORDER; //無線
        table3.AddCell(table3_5);

        string dataT36 = "　（一）如有未依核定計畫施作情形，應說明與計畫不符事項。\r\n";
        dataT36 += "　（二）前次監督檢查缺失及應注意事項之複查，請註明辦理情形及是否同意結案（或持續列管）。\r\n";
        dataT36 += "　（三）滯洪、沉砂池檢查項目及標準如下：\r\n";
        dataT36 += "　　　◆ 滯洪、沉砂量體：增減不得逾 20%，且不得小於所須最小滯洪、沉砂量。\r\n";
        dataT36 += "　　　◆ 放流口及溢洪口通水斷面積：增加不超過 20%或減少不超過 10%。\r\n";
        dataT36 += "　（四）植生工程檢查項目及標準如下：\r\n";
        dataT36 += "　　　◆ 植生面積：增減不得逾 20%。\r\n";
        dataT36 += "　　　◆ 覆蓋率：\r\n";
        dataT36 += "　　　　 a.以種子撒播及草皮鋪植等方式直接栽植者，以植被生長後之覆蓋率審認。\r\n";
        dataT36 += "　　　　 b.以噴植、植生帶、土袋植生及草袋等配合資材方式栽植者，以資材覆蓋率審認。\r\n";
        dataT36 += "　（五）相片說明應與紀錄文字勾稽。\r\n";
        PdfPCell table3_6 = new PdfPCell(new Phrase("七、填表注意事項：\r\n" + dataT36, ChFont));
        table3_6.MinimumHeight = 35f;//設定行高
        table3_6.HorizontalAlignment = Element.ALIGN_LEFT;
        table3_6.VerticalAlignment = Element.ALIGN_MIDDLE;
        //table3_6.Border = PdfPCell.NO_BORDER; //無線
        table3.AddCell(table3_6);

        doc1.Add(table3);


        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //照片
        //doc1.NewPage();
        PdfPTable table4 = new PdfPTable(new float[] { 1,1 });
        table4.TotalWidth = 500f;
        table4.LockedWidth = true;

        PdfPCell table4_1 = new PdfPCell(new Phrase(dSWC005, ChFont));
        table4_1.Colspan = 2;   //跨欄
        table4_1.MinimumHeight = 35f;//設定行高
        table4_1.HorizontalAlignment = Element.ALIGN_CENTER;
        table4_1.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_1);
        SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFileUrl20"] + "SWCDOC/UpLoadFiles/SwcCaseFile/";

        iTextSharp.text.Image jpg;
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + dT057;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);
        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
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
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + dT059;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(80f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);
        }

        PdfPCell table4_31 = new PdfPCell(new Phrase(dT058, ChFont));
        table4_31.MinimumHeight = 18f;//設定行高
        table4_31.HorizontalAlignment = Element.ALIGN_LEFT;
        table4_31.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_31);

        PdfPCell table4_32 = new PdfPCell(new Phrase(dT060, ChFont));
        table4_32.MinimumHeight = 18f;//設定行高
        table4_32.HorizontalAlignment = Element.ALIGN_LEFT;
        table4_32.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_32);


        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + dT061;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(80f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);
        }
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + dT063;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(80f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);
        }

        PdfPCell table4_51 = new PdfPCell(new Phrase(dT062, ChFont));
        table4_51.MinimumHeight = 18f;//設定行高
        table4_51.HorizontalAlignment = Element.ALIGN_LEFT;
        table4_51.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_51);

        PdfPCell table4_52 = new PdfPCell(new Phrase(dT064, ChFont));
        table4_52.MinimumHeight = 18f;//設定行高
        table4_52.HorizontalAlignment = Element.ALIGN_LEFT;
        table4_52.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_52);


        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + dT065;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(80f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);
        }
        try
        {
            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + dT067;
            jpg = iTextSharp.text.Image.GetInstance(new Uri(imageurl));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(180f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);

        }
        catch
        {
            jpg = iTextSharp.text.Image.GetInstance(new Uri("D://web//SWCWeb//SwcReport//1586338390597.jpg"));
            //jpg.ScaleToFitHeight = true;
            jpg.ScaleToFit(80f, 200f);
            //設定圖片的CELL
            PdfPCell jpgcell = new PdfPCell(jpg);
            jpgcell.HorizontalAlignment = Element.ALIGN_CENTER;
            jpgcell.VerticalAlignment = Element.ALIGN_MIDDLE;
            jpgcell.Padding = 5f;
            table4.AddCell(jpgcell);
        }

        PdfPCell table4_71 = new PdfPCell(new Phrase(dT066, ChFont));
        table4_71.MinimumHeight = 18f;//設定行高
        table4_71.HorizontalAlignment = Element.ALIGN_LEFT;
        table4_71.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_71);

        PdfPCell table4_72 = new PdfPCell(new Phrase(dT068, ChFont));
        table4_72.MinimumHeight = 18f;//設定行高
        table4_72.HorizontalAlignment = Element.ALIGN_LEFT;
        table4_72.VerticalAlignment = Element.ALIGN_MIDDLE;
        table4.AddCell(table4_72);

        doc1.Add(table4);





























        doc1.NewPage();
        doc1.Close();



        //使用者下載，另存新檔 \r\n
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=水土保持施工監督檢查紀錄_" + (DateTime.Now.Year - 1911) + (DateTime.Now.ToString("MMddmmss")) + ".pdf");
        Response.ContentType = "application/octet-stream";
        Response.OutputStream.Write(Memory.GetBuffer(), 0, Memory.GetBuffer().Length);
        Response.OutputStream.Flush();
        Response.OutputStream.Close();
        Response.Flush();
        Response.End();
        //ooooooooooooooooooooooooooooooooooooooooooooooo
















        //GBClass001 SBApp = new GBClass001();

        ////PDF套表開始
        //PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/swcchg.pdf"));
        //string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        //PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));


        ////@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        ////資料開始

        //{

        //    while (readeSwc.Read())
        //    {
        //        string tSWC005 = readeSwc["SWC005"] + "";
        //        string tSWC007 = readeSwc["SWC007"] + "";
        //        string tSWC013 = readeSwc["SWC013"] + "";
        //        string tSWC013ID = readeSwc["SWC013ID"] + "";
        //        string tSWC014 = readeSwc["SWC014"] + "";
        //        string tSWC038 = readeSwc["SWC038"] + "";
        //        string tSWC039 = readeSwc["SWC039"] + "";
        //        string tSWC043 = readeSwc["SWC043"] + "";
        //        string tSWC044 = readeSwc["SWC044"] + "";
        //        string tSWC045 = readeSwc["SWC045"] + "";
        //        string tSWC045ID = readeSwc["SWC045ID"] + "";
        //        string tSWC051 = readeSwc["SWC051"] + "";
        //        string tSWC052 = readeSwc["SWC052"] + "";

        //        string tDTLC001 = readeSwc["DTLC001"] + "";
        //        string tDTLC002 = readeSwc["DTLC002"] + "";
        //        string tDTLC005 = readeSwc["DTLC005"] + "";
        //        string tDTLC006 = readeSwc["DTLC006"] + "";
        //        string tDTLC007 = readeSwc["DTLC007"] + "";
        //        string tDTLC008 = readeSwc["DTLC008"] + "";
        //        string tDTLC009 = readeSwc["DTLC009"] + "";
        //        string tDTLC010 = readeSwc["DTLC010"] + "";
        //        string tDTLC011 = readeSwc["DTLC011"] + "";
        //        string tDTLC012 = readeSwc["DTLC012"] + "";
        //        string tDTLC013 = readeSwc["DTLC013"] + "";
        //        string tDTLC014 = readeSwc["DTLC014"] + "";
        //        string tDTLC015 = readeSwc["DTLC015"] + "";
        //        string tDTLC016 = readeSwc["DTLC016"] + "";
        //        string tDTLC017 = readeSwc["DTLC017"] + "";
        //        string tDTLC018 = readeSwc["DTLC018"] + "";
        //        string tDTLC019 = readeSwc["DTLC019"] + "";
        //        string tDTLC020 = readeSwc["DTLC020"] + "";
        //        string tDTLC021 = readeSwc["DTLC021"] + "";
        //        string tDTLC022 = readeSwc["DTLC022"] + "";
        //        string tDTLC023 = readeSwc["DTLC023"] + "";
        //        string tDTLC024 = readeSwc["DTLC024"] + "";
        //        string tDTLC025 = readeSwc["DTLC025"] + "";
        //        string tDTLC026 = readeSwc["DTLC026"] + "";
        //        string tDTLC027 = readeSwc["DTLC027"] + "";
        //        string tDTLC028 = readeSwc["DTLC028"] + "";
        //        string tDTLC029 = readeSwc["DTLC029"] + "";
        //        string tDTLC030 = readeSwc["DTLC030"] + "";
        //        string tDTLC031 = readeSwc["DTLC031"] + "";
        //        string tDTLC032 = readeSwc["DTLC032"] + "";
        //        string tDTLC033 = readeSwc["DTLC033"] + "";
        //        string tDTLC034 = readeSwc["DTLC034"] + "";
        //        string tDTLC035 = readeSwc["DTLC035"] + "";
        //        string tDTLC036 = readeSwc["DTLC036"] + "";
        //        string tDTLC037 = readeSwc["DTLC037"] + "";
        //        string tDTLC038 = readeSwc["DTLC038"] + "";
        //        string tDTLC039 = readeSwc["DTLC039"] + "";
        //        string tDTLC040 = readeSwc["DTLC040"] + "";
        //        string tDTLC041 = readeSwc["DTLC041"] + "";
        //        string tDTLC042 = readeSwc["DTLC042"] + "";
        //        string tDTLC043 = readeSwc["DTLC043"] + "";
        //        string tDTLC044 = readeSwc["DTLC044"] + "";
        //        string tDTLC045 = readeSwc["DTLC045"] + "";
        //        string tDTLC046 = readeSwc["DTLC046"] + "";
        //        string tDTLC047 = readeSwc["DTLC047"] + "";
        //        string tDTLC048 = readeSwc["DTLC048"] + "";
        //        string tDTLC049 = readeSwc["DTLC049"] + "";
        //        string tDTLC050 = readeSwc["DTLC050"] + "";
        //        string tDTLC051 = readeSwc["DTLC051"] + "";
        //        string tDTLC052 = readeSwc["DTLC052"] + "";
        //        string tDTLC055 = readeSwc["DTLC055"] + "";
        //        string tDTLC056 = readeSwc["DTLC056"] + "";
        //        string tDTLC057 = readeSwc["DTLC057"] + "";
        //        string tDTLC058 = readeSwc["DTLC058"] + "";
        //        string tDTLC059 = readeSwc["DTLC059"] + "";
        //        string tDTLC060 = readeSwc["DTLC060"] + "";
        //        string tDTLC061 = readeSwc["DTLC061"] + "";
        //        string tDTLC062 = readeSwc["DTLC062"] + "";
        //        string tDTLC063 = readeSwc["DTLC063"] + "";
        //        string tDTLC064 = readeSwc["DTLC064"] + "";
        //        string tDTLC065 = readeSwc["DTLC065"] + "";
        //        string tDTLC066 = readeSwc["DTLC066"] + "";
        //        string tDTLC067 = readeSwc["DTLC067"] + "";
        //        string tDTLC068 = readeSwc["DTLC068"] + "";

        //        tSWC038 = SBApp.DateView(tSWC038, "00");
        //        tSWC043 = SBApp.DateView(tSWC043, "00");
        //        tSWC051 = SBApp.DateView(tSWC051, "00");
        //        tSWC052 = SBApp.DateView(tSWC052, "00");

        //        tDTLC049 = SBApp.DateView(tDTLC049, "00");

        //        AcroFields.FieldPosition p;
        //        IList<AcroFields.FieldPosition> ps;
        //        ColumnText ct;

        //        //0.檢查日期
        //        string Year = " " + Convert.ToString(Convert.ToDateTime(tDTLC002).Year - 1911);
        //        string Month = " " + Convert.ToString(Convert.ToDateTime(tDTLC002).Month);
        //        string Day = " " + Convert.ToString(Convert.ToDateTime(tDTLC002).Day);

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg001");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg001");

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg002");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg002");

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg003");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg003");

        //        //1.列印案件編號
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg004");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC001, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg004");

        //        //2.計畫名稱
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg005");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg005");

        //        //3.水保與簡易水保
        //        string dwSwc007a = "□";
        //        string dwSwc007b = "□";
        //        if (tSWC007 == "水土保持計畫") { dwSwc007a = "■"; } else { dwSwc007b = "■"; }

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg006");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg006");

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg007");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg007");

        //        //4.核定日期文號
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg008");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg008");

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg009");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC039, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg009");

        //        //5.施工許可證日期文號
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg010");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg010");

        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg011");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC044, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg011");

        //        //6.開工日期
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg012");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg012");

        //        //7.預定完工日期
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg013");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg013");

        //        //8.水保義務人姓名或名稱
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg014");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg014");

        //        //8.水保義務人身分證或統編
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg015");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013ID, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg015");

        //        //9.水保義務人地址
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg016");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg016");

        //        //10.承辦監造計師姓名
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg017");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC045, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg017");

        //        //11.承辦監造計師機構名稱
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg018");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgName"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg018");

        //        //12.承辦監造計師執業執照字號
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg019");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgIssNo"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg019");

        //        //13.承辦監造計師統編
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg020");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgGUINo"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg020");

        //        //14.承辦監造計師電話
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg021");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgTel"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg021");

        //        //15.實施地點土地標是
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg022");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg022");

        //        //16.水保告示牌....(下拉+說明)
        //        string[] arrayCheckBoxStr = new string[] { tDTLC006, tDTLC008, tDTLC010, tDTLC012, tDTLC014, tDTLC016, tDTLC018, tDTLC020, tDTLC022, tDTLC024, tDTLC026, tDTLC028, tDTLC030, tDTLC032, tDTLC034, tDTLC036, tDTLC038, tDTLC040 };
        //        string[] arrayRemarkTxStr = new string[] { tDTLC007, tDTLC009, tDTLC011, tDTLC013, tDTLC015, tDTLC017, tDTLC019, tDTLC021, tDTLC023, tDTLC025, tDTLC027, tDTLC029, tDTLC031, tDTLC033, tDTLC035, tDTLC037, tDTLC039, tDTLC041 };

        //        for (int i = 0; i < arrayCheckBoxStr.Length; i++)
        //        {
        //            string strCheckBox = arrayCheckBoxStr[i];
        //            string strRemarkTx = arrayRemarkTxStr[i];

        //            string dwDTLCa = "□";
        //            string dwDTLCb = "□";
        //            string dwDTLCc = "□";

        //            switch (strCheckBox)
        //            {
        //                case "依計畫施作":
        //                    dwDTLCa = "■";
        //                    break;
        //                case "未依計畫施作":
        //                    dwDTLCb = "■";
        //                    break;
        //                case "尚未施作":
        //                    dwDTLCc = "■";
        //                    break;
        //                case "無此項":
        //                    strRemarkTx = "無此項 \n" + strRemarkTx;
        //                    break;
        //            }

        //            //選項用
        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 1).ToString());
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCa, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 1).ToString());

        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 2).ToString());
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCb, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 2).ToString());

        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 3).ToString());
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCc, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 3).ToString());

        //            //說明用
        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 4).ToString());
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(strRemarkTx, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 4).ToString());

        //        }


        //        string[] arrayCheckBox02 = new string[] { tDTLC042, tDTLC044, tDTLC046 };
        //        string[] arrayRemarkTx02 = new string[] { tDTLC043, tDTLC045, tDTLC047 };
        //        //監造計師是否在場 ， "swcchg095","swcchg096","swcchg097"

        //        for (int i = 0; i < arrayCheckBox02.Length; i++)
        //        {
        //            string strCheckBox02 = arrayCheckBox02[i];
        //            string strRemarkTx02 = arrayRemarkTx02[i];

        //            string dwDTLC2a = "□";
        //            string dwDTLC2b = "□";

        //            switch (strCheckBox02)
        //            {
        //                case "是":
        //                    dwDTLC2a = "■";
        //                    break;
        //                case "否":
        //                    dwDTLC2b = "■";
        //                    break;

        //            }
        //            //選項用
        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 1).ToString()).PadLeft(3, '0'));
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLC2a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 1).ToString()).PadLeft(3, '0'));

        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 2).ToString()).PadLeft(3, '0'));
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLC2b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 2).ToString()).PadLeft(3, '0'));

        //            //說明用
        //            ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 3).ToString()).PadLeft(3, '0'));
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(strRemarkTx02, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 3).ToString()).PadLeft(3, '0'));
        //        }



        //        //37.實施與計畫或規定不符事項及改正奇現
        //        string t4849 = tDTLC048 + "\n\n改正期限：" + tDTLC049;
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg104");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(t4849, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg104");

        //        //38.其他注意事項
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg105");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC050, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg105");

        //        //39.前次改正事項
        //        string t5152 = tDTLC051;
        //        if (tDTLC052.Trim() != "") { t5152 = t5152 + "\n\n現場改正情形\n" + tDTLC052; };
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg106");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(t5152, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg106");

        //        //40.簽名
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg107");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC055, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg107");

        //        //41.相片標題(水保計畫名)
        //        ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg108");
        //        p = ps[0];
        //        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //        ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //        ct.Go();
        //        Pdfstamper.AcroFields.RemoveField("swcchg108");

        //        //42~47.相片說明文字
        //        string[] arrayPicRemark = new string[] { tDTLC058, tDTLC060, tDTLC062, tDTLC064, tDTLC066, tDTLC068 };
        //        string[] arrayPDFView05 = new string[] { "swcchg109", "swcchg110", "swcchg111", "swcchg112", "swcchg113", "swcchg114" };

        //        for (int i = 0; i < arrayPicRemark.Length; i++)
        //        {
        //            string aPicRemark = arrayPicRemark[i] + "";
        //            string aPdfView05 = arrayPDFView05[i] + "";

        //            ps = Pdfstamper.AcroFields.GetFieldPositions(aPdfView05);
        //            p = ps[0];
        //            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //            ct.SetSimpleColumn(new iTextSharp.text.Phrase(aPicRemark, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //            ct.Go();
        //            Pdfstamper.AcroFields.RemoveField(aPdfView05);

        //        }



        //        //以下處理圖片
        //        iTextSharp.text.Image pdfimageobj;
        //        float x = 0;
        //        float y = 0;

        //        //加圖片1進去, A4 大小 寬:0~595 高:0~842
        //        if (tDTLC057.Trim() != "")
        //        {
        //            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC057;
        //            pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
        //            pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

        //            x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
        //            y = Convert.ToSingle(630.5 - (pdfimageobj.ScaledHeight / 2));
        //            pdfimageobj.SetAbsolutePosition(x, y);
        //            Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
        //        }
        //        //加圖片2進去
        //        if (tDTLC059.Trim() != "")
        //        {
        //            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC059;
        //            pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
        //            pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

        //            x = Convert.ToSingle(416 - (pdfimageobj.ScaledWidth / 2));
        //            y = Convert.ToSingle(630.5 - (pdfimageobj.ScaledHeight / 2));
        //            pdfimageobj.SetAbsolutePosition(x, y);
        //            Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
        //        }
        //        //加圖片3進去
        //        if (tDTLC061.Trim() != "")
        //        {
        //            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC061;
        //            pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
        //            pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

        //            x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
        //            y = Convert.ToSingle(431 - (pdfimageobj.ScaledHeight / 2));
        //            pdfimageobj.SetAbsolutePosition(x, y);
        //            Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
        //        }
        //        //加圖片4進去
        //        if (tDTLC063.Trim() != "")
        //        {
        //            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC063;
        //            pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
        //            pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

        //            x = Convert.ToSingle(416 - (pdfimageobj.ScaledWidth / 2));
        //            y = Convert.ToSingle(431 - (pdfimageobj.ScaledHeight / 2));
        //            pdfimageobj.SetAbsolutePosition(x, y);
        //            Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
        //        }
        //        //加圖片5進去
        //        if (tDTLC065.Trim() != "")
        //        {
        //            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC065;
        //            pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
        //            pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

        //            x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
        //            y = Convert.ToSingle(233 - (pdfimageobj.ScaledHeight / 2));
        //            pdfimageobj.SetAbsolutePosition(x, y);
        //            Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
        //        }
        //        //加圖片6進去
        //        if (tDTLC067.Trim() != "")
        //        {
        //            string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC067;
        //            pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
        //            pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

        //            x = Convert.ToSingle(416 - (pdfimageobj.ScaledWidth / 2));
        //            y = Convert.ToSingle(233 - (pdfimageobj.ScaledHeight / 2));
        //            pdfimageobj.SetAbsolutePosition(x, y);
        //            Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
        //        }
        //    }
        //    readeSwc.Close();
        //    objCmdSwc.Dispose();


        //}


        ////@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        //Pdfstamper.Close();
        //Pdfreader.Close();

        ////把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        //System.IO.Stream iStream;

        //// 以10K為單位暫存:
        //Byte[] buffer = new byte[10000];
        //int length = 0;
        //long dataToRead = 0;

        //// 制定文件路徑
        //string filepath = Server.MapPath("~\\OutputFile\\" + pdfnewname);
        //string filepathm = Server.MapPath("~\\OutputFile\\m" + pdfnewname);

        //// 得到文件名
        //string filename = System.IO.Path.GetFileName(filepath);

        ////Try
        //// 打開文件
        //iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        //// 得到文件大小
        //dataToRead = iStream.Length;
        //Response.ContentType = "application/x-rar-compressed";
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename));

        //while (dataToRead > 0)
        //{
        //    if (Response.IsClientConnected)
        //    {
        //        length = iStream.Read(buffer, 0, 10000);
        //        Response.OutputStream.Write(buffer, 0, length);
        //        Response.Flush();
        //        dataToRead = dataToRead - length;
        //    }
        //    else
        //    {
        //        dataToRead = -1;
        //    }
        //}
        //if (iStream.Length != 0)
        //{
        //    //關閉文件
        //    iStream.Close();
        //    File.Delete(filepath);
        //}
    }

    private void GenPdfo()
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        GBClass001 SBApp = new GBClass001();

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/swcchg.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));
        
        string ExeSqlStr = " select SWC.SWC005,SWC.SWC007,SWC.SWC013,SWC.SWC013ID,SWC.SWC014,SWC.SWC038,SWC.SWC039,SWC.SWC043,SWC.SWC044,SWC.SWC045,SWC.SWC045ID,SWC.SWC051,SWC.SWC052,D3.* from SWCDTL03 D3 ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON D3.SWC000=SWC.SWC000 ";
        ExeSqlStr = ExeSqlStr + " where D3.SWC000 = '" + rCaseId + "' ";
        ExeSqlStr = ExeSqlStr + "   and D3.DTLC000 = '" + rDTLId + "' ";

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
                string tSWC013 = readeSwc["SWC013"] + "";
                string tSWC013ID = readeSwc["SWC013ID"] + "";
                string tSWC014 = readeSwc["SWC014"] + "";
                string tSWC038 = readeSwc["SWC038"] + "";
                string tSWC039 = readeSwc["SWC039"] + "";
                string tSWC043 = readeSwc["SWC043"] + "";
                string tSWC044 = readeSwc["SWC044"] + "";
                string tSWC045 = readeSwc["SWC045"] + "";
                string tSWC045ID = readeSwc["SWC045ID"] + "";
                string tSWC051 = readeSwc["SWC051"] + "";
                string tSWC052 = readeSwc["SWC052"] + "";

                string tDTLC001 = readeSwc["DTLC001"] + "";
                string tDTLC002 = readeSwc["DTLC002"] + "";
                string tDTLC005 = readeSwc["DTLC005"] + "";
                string tDTLC006 = readeSwc["DTLC006"] + "";
                string tDTLC007 = readeSwc["DTLC007"] + "";
                string tDTLC008 = readeSwc["DTLC008"] + "";
                string tDTLC009 = readeSwc["DTLC009"] + "";
                string tDTLC010 = readeSwc["DTLC010"] + "";
                string tDTLC011 = readeSwc["DTLC011"] + "";
                string tDTLC012 = readeSwc["DTLC012"] + "";
                string tDTLC013 = readeSwc["DTLC013"] + "";
                string tDTLC014 = readeSwc["DTLC014"] + "";
                string tDTLC015 = readeSwc["DTLC015"] + "";
                string tDTLC016 = readeSwc["DTLC016"] + "";
                string tDTLC017 = readeSwc["DTLC017"] + "";
                string tDTLC018 = readeSwc["DTLC018"] + "";
                string tDTLC019 = readeSwc["DTLC019"] + "";
                string tDTLC020 = readeSwc["DTLC020"] + "";
                string tDTLC021 = readeSwc["DTLC021"] + "";
                string tDTLC022 = readeSwc["DTLC022"] + "";
                string tDTLC023 = readeSwc["DTLC023"] + "";
                string tDTLC024 = readeSwc["DTLC024"] + "";
                string tDTLC025 = readeSwc["DTLC025"] + "";
                string tDTLC026 = readeSwc["DTLC026"] + "";
                string tDTLC027 = readeSwc["DTLC027"] + "";
                string tDTLC028 = readeSwc["DTLC028"] + "";
                string tDTLC029 = readeSwc["DTLC029"] + "";
                string tDTLC030 = readeSwc["DTLC030"] + "";
                string tDTLC031 = readeSwc["DTLC031"] + "";
                string tDTLC032 = readeSwc["DTLC032"] + "";
                string tDTLC033 = readeSwc["DTLC033"] + "";
                string tDTLC034 = readeSwc["DTLC034"] + "";
                string tDTLC035 = readeSwc["DTLC035"] + "";
                string tDTLC036 = readeSwc["DTLC036"] + "";
                string tDTLC037 = readeSwc["DTLC037"] + "";
                string tDTLC038 = readeSwc["DTLC038"] + "";
                string tDTLC039 = readeSwc["DTLC039"] + "";
                string tDTLC040 = readeSwc["DTLC040"] + "";
                string tDTLC041 = readeSwc["DTLC041"] + "";
                string tDTLC042 = readeSwc["DTLC042"] + "";
                string tDTLC043 = readeSwc["DTLC043"] + "";
                string tDTLC044 = readeSwc["DTLC044"] + "";
                string tDTLC045 = readeSwc["DTLC045"] + "";
                string tDTLC046 = readeSwc["DTLC046"] + "";
                string tDTLC047 = readeSwc["DTLC047"] + "";
                string tDTLC048 = readeSwc["DTLC048"] + "";
                string tDTLC049 = readeSwc["DTLC049"] + "";
                string tDTLC050 = readeSwc["DTLC050"] + "";
                string tDTLC051 = readeSwc["DTLC051"] + "";
                string tDTLC052 = readeSwc["DTLC052"] + "";
                string tDTLC055 = readeSwc["DTLC055"] + "";
                string tDTLC056 = readeSwc["DTLC056"] + "";
                string tDTLC057 = readeSwc["DTLC057"] + "";
                string tDTLC058 = readeSwc["DTLC058"] + "";
                string tDTLC059 = readeSwc["DTLC059"] + "";
                string tDTLC060 = readeSwc["DTLC060"] + "";
                string tDTLC061 = readeSwc["DTLC061"] + "";
                string tDTLC062 = readeSwc["DTLC062"] + "";
                string tDTLC063 = readeSwc["DTLC063"] + "";
                string tDTLC064 = readeSwc["DTLC064"] + "";
                string tDTLC065 = readeSwc["DTLC065"] + "";
                string tDTLC066 = readeSwc["DTLC066"] + "";
                string tDTLC067 = readeSwc["DTLC067"] + "";
                string tDTLC068 = readeSwc["DTLC068"] + "";

                tSWC038 = SBApp.DateView(tSWC038, "00");
                tSWC043 = SBApp.DateView(tSWC043, "00");
                tSWC051 = SBApp.DateView(tSWC051, "00");
                tSWC052 = SBApp.DateView(tSWC052, "00");

                tDTLC049 = SBApp.DateView(tDTLC049, "00");

                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //0.檢查日期
                string Year = " "+ Convert.ToString(Convert.ToDateTime(tDTLC002).Year-1911);
                string Month = " " + Convert.ToString(Convert.ToDateTime(tDTLC002).Month);
                string Day = " " + Convert.ToString(Convert.ToDateTime(tDTLC002).Day);

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg001");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg001");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg002");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg002");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg003");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg003");

                //1.列印案件編號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg004");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC001, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg004");

                //2.計畫名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg005");

                //3.水保與簡易水保
                string dwSwc007a = "□";
                string dwSwc007b = "□";
                if (tSWC007 == "水土保持計畫") { dwSwc007a = "■"; } else { dwSwc007b = "■"; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg006");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg006");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg007");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwSwc007b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg007");

                //4.核定日期文號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg008");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC038, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg008");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg009");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC039, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg009");

                //5.施工許可證日期文號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg010");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC043, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg010");

                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg011");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC044, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg011");

                //6.開工日期
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg012");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC051, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg012");

                //7.預定完工日期
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg013");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC052, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg013");

                //8.水保義務人姓名或名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg014");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg014");

                //8.水保義務人身分證或統編
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg015");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC013ID, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg015");

                //9.水保義務人地址
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg016");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC014, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg016");

                //10.承辦監造計師姓名
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg017");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC045, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg017");

                //11.承辦監造計師機構名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg018");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgName"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg018");

                //12.承辦監造計師執業執照字號
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg019");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgIssNo"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg019");

                //13.承辦監造計師統編
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg020");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgGUINo"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg020");

                //14.承辦監造計師電話
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg021");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(SBApp.GetETUser(tSWC045ID, "OrgTel"), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg021");

                //15.實施地點土地標是
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg022");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg022");

                //16.水保告示牌....(下拉+說明)
                string[] arrayCheckBoxStr = new string[] { tDTLC006, tDTLC008, tDTLC010, tDTLC012, tDTLC014, tDTLC016, tDTLC018, tDTLC020, tDTLC022, tDTLC024, tDTLC026, tDTLC028, tDTLC030, tDTLC032, tDTLC034, tDTLC036, tDTLC038, tDTLC040 };
                string[] arrayRemarkTxStr = new string[] { tDTLC007, tDTLC009, tDTLC011, tDTLC013, tDTLC015, tDTLC017, tDTLC019, tDTLC021, tDTLC023, tDTLC025, tDTLC027, tDTLC029, tDTLC031, tDTLC033, tDTLC035, tDTLC037, tDTLC039, tDTLC041 };

                for (int i = 0; i < arrayCheckBoxStr.Length; i++)
                {
                    string strCheckBox = arrayCheckBoxStr[i];
                    string strRemarkTx = arrayRemarkTxStr[i];

                    string dwDTLCa = "□";
                    string dwDTLCb = "□";
                    string dwDTLCc = "□";

                    switch (strCheckBox)
                    {
                        case "依計畫施作":
                            dwDTLCa = "■";
                            break;
                        case "未依計畫施作":
                            dwDTLCb = "■";
                            break;
                        case "尚未施作":
                            dwDTLCc = "■";
                            break;
                        case "無此項":
                            strRemarkTx = "無此項 \n" + strRemarkTx;
                            break;
                    }

                    //選項用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0"+(22+i*4+1).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCa, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 1).ToString());

                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 2).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCb, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 2).ToString());

                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 3).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLCc, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 3).ToString());

                    //說明用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg0" + (22 + i * 4 + 4).ToString());
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(strRemarkTx, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg0" + (22 + i * 4 + 4).ToString());

                }


                string[] arrayCheckBox02 = new string[] { tDTLC042, tDTLC044, tDTLC046 };
                string[] arrayRemarkTx02 = new string[] { tDTLC043, tDTLC045, tDTLC047 };
                //監造計師是否在場 ， "swcchg095","swcchg096","swcchg097"

                for (int i = 0; i < arrayCheckBox02.Length; i++)
                {
                    string strCheckBox02 = arrayCheckBox02[i];
                    string strRemarkTx02 = arrayRemarkTx02[i];

                    string dwDTLC2a = "□";
                    string dwDTLC2b = "□";

                    switch (strCheckBox02)
                    {
                        case "是":
                            dwDTLC2a = "■";
                            break;
                        case "否":
                            dwDTLC2b = "■";
                            break;

                    }
                    //選項用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 1).ToString()).PadLeft(3,'0'));
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLC2a, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 1).ToString()).PadLeft(3, '0'));

                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 2).ToString()).PadLeft(3, '0'));
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwDTLC2b, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 2).ToString()).PadLeft(3, '0'));
                    
                    //說明用
                    ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg" + ((94 + i * 3 + 3).ToString()).PadLeft(3, '0'));
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(strRemarkTx02, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("swcchg" + ((94 + i * 3 + 3).ToString()).PadLeft(3, '0'));
                }



                //37.實施與計畫或規定不符事項及改正奇現
                string t4849 = tDTLC048 + "\n\n改正期限：" + tDTLC049;
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg104");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(t4849, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg104");

                //38.其他注意事項
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg105");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC050, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg105");

                //39.前次改正事項
                string t5152 = tDTLC051;
                if (tDTLC052.Trim() != "") { t5152 = t5152 + "\n\n現場改正情形\n" + tDTLC052; };
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg106");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(t5152, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg106");

                //40.簽名
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg107");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tDTLC055, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg107");

                //41.相片標題(水保計畫名)
                ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg108");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("swcchg108");

                //42~47.相片說明文字
                string[] arrayPicRemark = new string[] { tDTLC058, tDTLC060, tDTLC062, tDTLC064, tDTLC066, tDTLC068 };
                string[] arrayPDFView05 = new string[] { "swcchg109", "swcchg110", "swcchg111", "swcchg112", "swcchg113", "swcchg114" };

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
                if (tDTLC057.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC057;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

                    x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(630.5 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                //加圖片2進去
                if (tDTLC059.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC059;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

                    x = Convert.ToSingle(416 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(630.5 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                //加圖片3進去
                if (tDTLC061.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC061;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

                    x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(431 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                //加圖片4進去
                if (tDTLC063.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC063;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

                    x = Convert.ToSingle(416 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(431 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                //加圖片5進去
                if (tDTLC065.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC065;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

                    x = Convert.ToSingle(178.5 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(233 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
                }
                //加圖片6進去
                if (tDTLC067.Trim() != "")
                {
                    string imageurl = SwcUpLoadFilePath + rCaseId + "/" + tDTLC067;
                    pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl);
                    pdfimageobj.ScaleToFit(Convert.ToSingle(212.5), Convert.ToSingle(141.2));

                    x = Convert.ToSingle(416 - (pdfimageobj.ScaledWidth / 2));
                    y = Convert.ToSingle(233 - (pdfimageobj.ScaledHeight / 2));
                    pdfimageobj.SetAbsolutePosition(x, y);
                    Pdfstamper.GetOverContent(5).AddImage(pdfimageobj);
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