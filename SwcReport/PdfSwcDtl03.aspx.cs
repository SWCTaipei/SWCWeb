/*  Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
    Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License as
    published by the Free Software Foundation, either version 3 of the
    License, or any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU Affero General Public License for more details.

    You should have received a copy of the GNU Affero General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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

        string SwcUpLoadFilePath = ConfigurationManager.AppSettings["SwcFilePath"].Trim();

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
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
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
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
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
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
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
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
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
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
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
                    Pdfstamper.GetOverContent(4).AddImage(pdfimageobj);
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