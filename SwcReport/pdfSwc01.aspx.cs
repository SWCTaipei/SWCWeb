using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web;

public partial class SwcReport_pdfSwc301 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenPdf();
    }
    private void GenPdf()
    {
        string rCaseId = Request.QueryString["CaseId"] + "";
        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/水土保持完工證明書.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        string ExeSqlStr = " select * from SWCSWC where SWC00=@SWC000; ";
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //資料開始 
        ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["TSLMSWCCONN"];
        using (SqlConnection TslmConnInsBill = new SqlConnection(connectionString.ConnectionString))
        {
            TslmConnInsBill.Open();
            using (var cmd = TslmConnInsBill.CreateCommand())
            {
                cmd.CommandText = ExeSqlStr;
                #region.設定值
                cmd.Parameters.Add(new SqlParameter("@SWC000", rCaseId));
                #endregion
                cmd.ExecuteNonQuery();
                using (SqlDataReader readerSwc = cmd.ExecuteReader())
                {
                    if (readerSwc.HasRows)
                        while (readerSwc.Read())
                        {
                            string qSWC005 = readerSwc["SWC05"] + "";
                            string qSWC059 = readerSwc["SWC59"] + "";

                            AcroFields.FieldPosition p;
                            IList<AcroFields.FieldPosition> ps;
                            ColumnText ct;

                            //1.書件名稱
                            ps = Pdfstamper.AcroFields.GetFieldPositions("SWC05");
                            p = ps[0];
                            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                            ct.SetSimpleColumn(new iTextSharp.text.Phrase(qSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 20)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 22, iTextSharp.text.Element.ALIGN_CENTER);
                            ct.Go();
                            Pdfstamper.AcroFields.RemoveField("SWC05");

                            //0.完工證明書核發日期
                            string Year = " " + Convert.ToString(Convert.ToDateTime(qSWC059).Year - 1911);
                            string Month = " " + Convert.ToString(Convert.ToDateTime(qSWC059).Month);
                            string Day = " " + Convert.ToString(Convert.ToDateTime(qSWC059).Day);

                            ps = Pdfstamper.AcroFields.GetFieldPositions("SWC60(year)");
                            p = ps[0];
                            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                            ct.SetSimpleColumn(new iTextSharp.text.Phrase(Year, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 20)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                            ct.Go();
                            Pdfstamper.AcroFields.RemoveField("SWC60(year)");

                            ps = Pdfstamper.AcroFields.GetFieldPositions("SWC60(month)");
                            p = ps[0];
                            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                            ct.SetSimpleColumn(new iTextSharp.text.Phrase(Month, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 20)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                            ct.Go();
                            Pdfstamper.AcroFields.RemoveField("SWC60(month)");

                            ps = Pdfstamper.AcroFields.GetFieldPositions("SWC60(day)");
                            p = ps[0];
                            ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                            ct.SetSimpleColumn(new iTextSharp.text.Phrase(Day, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 20)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_CENTER);
                            ct.Go();
                            Pdfstamper.AcroFields.RemoveField("SWC60(day)");



                        }
                    readerSwc.Close();
                    Pdfstamper.Close();
                    Pdfreader.Close();
                }
                cmd.Cancel();
            }
        }
        #region 把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
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
            File.Delete(Server.MapPath("~\\OutputFile\\" + pdfnewname));
            //File.Delete(Server.MapPath("~\\OutputFile\\" + pdfnewnameR));
        }
        #endregion
    }
}