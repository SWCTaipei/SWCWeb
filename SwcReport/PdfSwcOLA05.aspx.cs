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
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/OLA005_0406.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));

		string ExeSqlStr = " select SWC.SWC005,OA5.*,EU.ETName,RCD.DATE_RCD from OnlineApply05 OA5 ";
        ExeSqlStr = ExeSqlStr + " left JOIN (SELECT SWC000,ONA001,CONVERT(NVARCHAR(10),MAX(R004)) DATE_RCD from tslm2.dbo.SignRCD WHERE R003 = '決行' GROUP BY SWC000,ONA001) RCD ON RCD.SWC000=OA5.SWC000 AND RCD.ONA001=OA5.ONA05001  ";
        ExeSqlStr = ExeSqlStr + " left JOIN SWCCASE SWC ON OA5.SWC000=SWC.SWC000 ";
        ExeSqlStr += " left JOIN ETUsers EU on OA5.LOCKUSER=EU.ETID ";
        ExeSqlStr = ExeSqlStr + " where OA5.SWC000 = '" + rSWCNO + "' ";
        ExeSqlStr = ExeSqlStr + "   and OA5.ONA05001 = '" + rOLANO + "' ";

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
                string tETName = readeSwc["ETName"] + "";
                string tLOCKDATE = readeSwc["LOCKDATE"] + "";
                string tDATALOCK2 = readeSwc["DATALOCK2"] + "";
                string tLOCKDATE2 = "";

                if (tDATALOCK2=="Y") { 
					//tLOCKDATE2 = readeSwc["savedate"] + ""; 
					tLOCKDATE2 = readeSwc["DATE_RCD"] + "";
				}

                string tONA003 = readeSwc["ONA05003"] + "";
                string tONA004 = readeSwc["ONA05004"] + "";
                string tONA005 = readeSwc["ONA05005"] + "";
                string tONA006 = readeSwc["ONA05006"] + "";
                string tONA007 = readeSwc["ONA05007"] + "";
                string tONA008 = readeSwc["ONA05008"] + "";
                string tONA009 = readeSwc["ONA05009"] + "";
                string tONA010 = readeSwc["ONA05010"] + "";
                string tONA017 = readeSwc["ONA05017"] + "";

                if (tONA003 == "1") { tONA003 = "V"; } else { tONA003 = ""; }
                if (tONA004 == "1") { tONA004 = "V"; } else { tONA004 = ""; }
                if (tONA005 == "1") { tONA005 = "V"; } else { tONA005 = ""; }
                if (tONA006 == "1") { tONA006 = "V"; } else { tONA006 = ""; }
                if (tONA007 == "1") { tONA007 = "V"; } else { tONA007 = ""; }
                if (tONA008 == "1") { tONA008 = "V"; } else { tONA008 = ""; }
                if (tONA009 == "1") { tONA009 = "V"; } else { tONA009 = ""; }
                if (tONA010 == "1") { tONA010 = "V"; } else { tONA010 = ""; }

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
                
                //2.申請調整說明
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA017");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA017, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA017");

                //3.申請調整項目一
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA003");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA003, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA003");

                //4.申請調整項目二
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA004");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA004, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA004");

                //5.申請調整項目三
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA005");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA005, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA005");

                //6.申請調整項目四
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA006");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA006, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA006");

                //7.申請調整項目五
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA007");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA007, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA007");

                //8.申請調整項目六
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA008");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA008, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA008");

                //9.申請調整項目七
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA009");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA009, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA009");

                //10.申請調整項目八
                ps = Pdfstamper.AcroFields.GetFieldPositions("ONA010");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tONA010, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("ONA010");

                //11.申請人
                ps = Pdfstamper.AcroFields.GetFieldPositions("LOCKUSER");
                p = ps[0];
                ct = new ColumnText(Pdfstamper.GetOverContent(p.page));
                ct.SetSimpleColumn(new iTextSharp.text.Phrase(tETName, new iTextSharp.text.Font(BaseFont.CreateFont("c:/windows/fonts/kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 12)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, iTextSharp.text.Element.ALIGN_LEFT);
                ct.Go();
                Pdfstamper.AcroFields.RemoveField("LOCKUSER");

                //12.申請人送出日
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

                //13.准駁日
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