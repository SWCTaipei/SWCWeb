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
        string rBillId = Request.QueryString["pno"] + "";
        GBClass001 CA = new GBClass001();
        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/大地繳費單_保證金.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //資料開始
        string sBillDesc = "", sPayMoney = "", sText = "", sLastDate = "";
        string sQRcode_1 = "", sQRcode_2 = "", sQRcode_3 = "", sQRcode_4 = "";
        string Today = CA.DateView(DateTime.Now.ToString("yyyy-MM-dd"), "");

        string strSQLData = " select b.*,s.swc05 from SwcBill b left join swcswc s on s.swc00=b.fr001 where SB004='"+ rBillId + "'; ";
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConn = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConn.Open();
            using (var cmd = TslmConn.CreateCommand())
            {
                cmd.CommandText = strSQLData;
                cmd.ExecuteNonQuery();

                using (SqlDataReader readerTslm = cmd.ExecuteReader())
                {
                    if (readerTslm.HasRows)
                        while (readerTslm.Read())
                        {
                            sBillDesc = readerTslm["swc05"] + "";
                            sQRcode_1 = "*" + readerTslm["SB002"] + "*";
                            sQRcode_2 = "*" + readerTslm["SB003"] + "*";
                            sQRcode_3 = "*" + readerTslm["SB004"] + "*";
                            sQRcode_4 = "*" + readerTslm["SB005"] + "*";
                            sLastDate = CA.DateView(readerTslm["SB007"] + "","00");
                            sPayMoney = readerTslm["SB008"] + "";

                        }
                    readerTslm.Close();
                }
                cmd.Cancel();
            }
        }

        AcroFields.FieldPosition p;
        IList<AcroFields.FieldPosition> ps;
        ColumnText ct;

        //帳號14
        ps = Pdfstamper.AcroFields.GetFieldPositions("count1");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_1.Replace("*", ""), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("count1");

        ps = Pdfstamper.AcroFields.GetFieldPositions("count2");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_1.Replace("*", ""), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("count2");

        //帳號16
        ps = Pdfstamper.AcroFields.GetFieldPositions("count4");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_1.Replace("*", ""), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("count4");

        ps = Pdfstamper.AcroFields.GetFieldPositions("number");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("number");

        //條碼1
        ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_1, new iTextSharp.text.Font(BaseFont.CreateFont(ConfigurationManager.AppSettings["BarCodeFont"], BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 28)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("QRcode");

        //14碼帳號
        ps = Pdfstamper.AcroFields.GetFieldPositions("count3");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_1.Replace("*", ""), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("count3");

        //計畫名稱
        ps = Pdfstamper.AcroFields.GetFieldPositions("plan1");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sBillDesc, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("plan1");

        ps = Pdfstamper.AcroFields.GetFieldPositions("plan2");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sBillDesc, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("plan2");

        //期限
        ps = Pdfstamper.AcroFields.GetFieldPositions("date1");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sLastDate, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("date1");

        ps = Pdfstamper.AcroFields.GetFieldPositions("date2");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sLastDate, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("date2");

        //金額
        ps = Pdfstamper.AcroFields.GetFieldPositions("money1");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sPayMoney, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("money1");

        ps = Pdfstamper.AcroFields.GetFieldPositions("money2");
        p = ps[0];
        ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        ct.SetSimpleColumn(new iTextSharp.text.Phrase(sPayMoney, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        ct.Go();
        Pdfstamper.AcroFields.RemoveField("money2");
        








        ////列印日期
        //ps = Pdfstamper.AcroFields.GetFieldPositions("copydate");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(Today, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("copydate");

        ////16碼帳號
        //ps = Pdfstamper.AcroFields.GetFieldPositions("count_1");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_3.Replace("*", ""), new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 8)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("count_1");

        ////條碼1
        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_1");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_3, new iTextSharp.text.Font(BaseFont.CreateFont(ConfigurationManager.AppSettings["BarCodeFont"], BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 18)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_1");

        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_n1");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_3, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 8)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_n1");

        ////條碼2
        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_2");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_2, new iTextSharp.text.Font(BaseFont.CreateFont(ConfigurationManager.AppSettings["BarCodeFont"], BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 18)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_2");

        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_n2");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 8)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_n2");

        ////條碼3
        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_3");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_3, new iTextSharp.text.Font(BaseFont.CreateFont(ConfigurationManager.AppSettings["BarCodeFont"], BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 18)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_3");

        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_n3");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_3, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 8)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_n3");

        ////條碼4
        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_4");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_4, new iTextSharp.text.Font(BaseFont.CreateFont(ConfigurationManager.AppSettings["BarCodeFont"], BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 18)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_4");

        //ps = Pdfstamper.AcroFields.GetFieldPositions("QRcode_n4");
        //p = ps[0];
        //ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
        //ct.SetSimpleColumn(new iTextSharp.text.Phrase(sQRcode_4, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 8)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
        //ct.Go();
        //Pdfstamper.AcroFields.RemoveField("QRcode_n4");

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