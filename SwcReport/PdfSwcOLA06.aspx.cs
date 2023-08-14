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

        string rSWCNO = Request.QueryString["SWCNO"] + "";
        string rOLANO = Request.QueryString["OLANO"] + "";

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath20"].Trim();

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/OLA006.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

        string ExeSqlStr = " select SWC.SWC005,OA6.*,EU.ETName,E8.ETName AS ONA06008n,E12.ETName AS ONA06012n from OnlineApply06 OA6 ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON OA6.SWC000=SWC.SWC000 ";
        ExeSqlStr += " left JOIN ETUsers EU on OA6.LOCKUSER=EU.ETID ";
        ExeSqlStr += " left JOIN ETUsers E8 on OA6.ONA06008=E8.ETID ";
        ExeSqlStr += " left JOIN ETUsers E12 on OA6.ONA06012=E12.ETID ";
        ExeSqlStr = ExeSqlStr + " where OA6.SWC000 = '" + rSWCNO + "' ";
        ExeSqlStr = ExeSqlStr + "   and OA6.ONA06001 = '" + rOLANO + "' ";

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //資料開始
		
		string tONA06003 = "";//義務人
		
		string str = "select * from OnlineApply06_SWCObligor where SWC000 = '" + rSWCNO + "' and OA06 = '" + rOLANO + "'; ";
		ConnectionStringSettings connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(str, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
				tONA06003 += readeSwc["SWC013"] + ",";
			}
            readeSwc.Close();
            objCmdSwc.Dispose();
        }
		tONA06003 = tONA06003.Substring(0,tONA06003.Length-1);

        connectionString = ConfigurationManager.ConnectionStrings["SWCConnStr"];
        using (SqlConnection SwcConn = new SqlConnection(connectionString.ConnectionString))
        {
            SwcConn.Open();

            SqlDataReader readeSwc;
            SqlCommand objCmdSwc = new SqlCommand(ExeSqlStr, SwcConn);
            readeSwc = objCmdSwc.ExecuteReader();

            while (readeSwc.Read())
            {
                string tSWC005 = readeSwc["SWC005"] + "";
                string tONA06002 = readeSwc["ONA06002"] + "";
                //string tONA06003 = readeSwc["ONA06003"] + "";
                string tONA06007 = readeSwc["ONA06007"] + "";
                string tONA06008 = readeSwc["ONA06008n"] + "";
                string tONA06011 = readeSwc["ONA06011"] + "";
                string tONA06012 = readeSwc["ONA06012n"] + "";
                string tETName = readeSwc["ETName"] + "";
                string tLOCKDATE = readeSwc["LOCKDATE"] + "";
                string tDATALOCK2 = readeSwc["DATALOCK2"] + "";
                string tLOCKDATE2 = "";

                if (tDATALOCK2=="Y") { tLOCKDATE2 = readeSwc["savedate"] + ""; }

                AcroFields.FieldPosition p;
                IList<AcroFields.FieldPosition> ps;
                ColumnText ct;

                //1.計畫名稱
                ps = Pdfstamper.AcroFields.GetFieldPositions("SWC005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tSWC005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 16)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("SWC005");

                //3.變更義務人
                string dwONA06002 = "□";
                if (tONA06002 == "1") { dwONA06002 = "■"; } else { dwONA06002 = ""; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA06002");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA06002, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA06002");

                //3.變更義務人-2
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA06003");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA06003, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA06003");

                //3.變更承辦技師
                string dwONA06007 = "□";
                if (tONA06007 == "1") { dwONA06007 = "■"; } else { tONA06008 = ""; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA06007");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA06007, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA06007");

                //3.變更承辦技師-2
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA06008");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA06008, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA06008");

                //3.變更監造技師
                string dwONA06011 = "□";
                if (tONA06011 == "1") { dwONA06011 = "■"; } else { tONA06012 = ""; }

                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA06011");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(dwONA06011, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA06011");

                //3.變更監造技師-2
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA06012");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA06012, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA06012");

                //3.申請人
                ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKUSER");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tETName, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("LOCKUSER");

                //4.申請人送出日
                string Yearld1 = " " + Convert.ToString(Convert.ToDateTime(tLOCKDATE).Year - 1911);
                string Monthld1 = " " + Convert.ToString(Convert.ToDateTime(tLOCKDATE).Month);
                string Dayld1 = " " + Convert.ToString(Convert.ToDateTime(tLOCKDATE).Day);
                //判斷日期1900-01-01
                if (Convert.ToString(Convert.ToDateTime(tLOCKDATE).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tLOCKDATE).Month) == "1" && Convert.ToString(Convert.ToDateTime(tLOCKDATE).Day) == "1")
                {
                    Yearld1 = " ";
                    Monthld1 = " ";
                    Dayld1 = " ";
                }

                ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKDATEa");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Yearld1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("LOCKDATEa");

                ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKDATEb");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Monthld1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("LOCKDATEb");

                ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKDATEc");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(Dayld1, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("LOCKDATEc");

                //5.准駁日
                if (tDATALOCK2 == "Y")
                {
                    string Yearld2 = " " + Convert.ToString(Convert.ToDateTime(tLOCKDATE2).Year - 1911);
                    string Monthld2 = " " + Convert.ToString(Convert.ToDateTime(tLOCKDATE2).Month);
                    string Dayld2 = " " + Convert.ToString(Convert.ToDateTime(tLOCKDATE2).Day);
                    //判斷日期1900-01-01
                    if (Convert.ToString(Convert.ToDateTime(tLOCKDATE2).Year) == "1900" && Convert.ToString(Convert.ToDateTime(tLOCKDATE2).Month) == "1" && Convert.ToString(Convert.ToDateTime(tLOCKDATE2).Day) == "1")
                    {
                        Yearld2 = " ";
                        Monthld2 = " ";
                        Dayld2 = " ";
                    }

                    ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKDATA2a");
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(Yearld2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("LOCKDATA2a");

                    ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKDATA2b");
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(Monthld2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("LOCKDATA2b");

                    ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKDATA2c");
                    p = ps[0];
                    ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                    ct.SetSimpleColumn(new iTextSharp.text.Phrase(Dayld2, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                    ct.Go();
                    Pdfstamper.AcroFields.RemoveField("LOCKDATA2c");
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