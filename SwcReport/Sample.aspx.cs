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












        //'定義欄位
        //'找到定義好的某欄位絕對位置，利用SetSimpleColumn把ColunmText貼上去，賦予值，再使用 RemoveField 將原來Acrobat的欄位移除。
        //'之所以會這樣作，是因為Acrobat的欄位(Field)似乎會誘使ItextSharp將完整的字型字集嵌入，即使已設定 font not.embed也一樣。
        //'網路上有文章說將Acrobat欄位鎖定便可防止內嵌，但那是沒用的。
        //Dim p As AcroFields.FieldPosition
        //Dim ps As IList(Of AcroFields.FieldPosition)
        //Dim ct As ColumnText
        //'填第一頁的文字欄跟圖片
        //' 委外案件日期_年
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg001")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(Trim(Int(Left(swcchdate.Text, 4)) - 1911).ToString, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg001")
        //' 委外案件日期_月
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg002")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(Trim(Mid(swcchdate.Text, 6, 2)), New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg002")
        //' 委外案件日期_日
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg003")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(Trim(Right(swcchdate.Text, 2)), New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg003")
        //' 1.列印案件編號
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg004")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext003.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg004")
        //' 2.計畫名稱
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg005")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext006.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg005")
        //' 3.水保與簡易水保
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg006")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton1.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg006")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg007")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton2.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg007")
        //' 4.核定日期文號
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg008")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext008.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase("", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase(Trim(Int(Left(swcchgtext008.Text, 4)) - 1911).ToString + "." + Trim(Mid(swcchgtext008.Text, 6, 2)) + "." + Trim(Right(swcchgtext008.Text, 2)), New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg008")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg009")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext009.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase("", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase(swcchgtext009.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg009")
        //' 5.施工許可證日期文號
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg010")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext010.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase("", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase(Trim(Int(Left(swcchgtext010.Text, 4)) - 1911).ToString + "." + Trim(Mid(swcchgtext010.Text, 6, 2)) + "." + Trim(Right(swcchgtext010.Text, 2)), New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg010")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg011")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext011.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase("", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase(swcchgtext011.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg011")
        //' 6.開工日期
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg012")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext012.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase("", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase(Trim(Int(Left(swcchgtext012.Text, 4)) - 1911).ToString + "." + Trim(Mid(swcchgtext012.Text, 6, 2)) + "." + Trim(Right(swcchgtext012.Text, 2)), New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg012")
        //' 7.預定完工日期
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg013")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext013.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase("", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase(Trim(Int(Left(swcchgtext013.Text, 4)) - 1911).ToString + "." + Trim(Mid(swcchgtext013.Text, 6, 2)) + "." + Trim(Right(swcchgtext013.Text, 2)), New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg013")
        //' 8.水保義務人姓名或名稱
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg014")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext014.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase(swcchgtext017.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //Else
        //    ct.SetSimpleColumn(New Phrase(swcchgtext014.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg014")
        //' 8.水保義務人身分證或統編
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg015")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext015.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase(swcchgtext018.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //Else
        //    ct.SetSimpleColumn(New Phrase(swcchgtext015.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg015")
        //' 9.水保義務人地址
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg016")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If swcchgtext016.Text = "" Then
        //    ct.SetSimpleColumn(New Phrase(swcchgtext019.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //Else
        //    ct.SetSimpleColumn(New Phrase(swcchgtext016.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg016")
        //' 10.承辦監造計師姓名
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg017")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext020.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg017")
        //' 11.承辦監造計師機構名稱
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg018")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext021.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg018")
        //' 12.承辦監造計師執業執照字號
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg019")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext022.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg019")
        //' 13.承辦監造計師統編
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg020")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext023.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg020")
        //' 14.承辦監造計師電話
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg021")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext024.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg021")
        //' 15.實施地點土地標是
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg022")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext025.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg022")
        //' 16.水保告示牌
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg023")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton3.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg023")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg024")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton4.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg024")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg025")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton5.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg025")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg026")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext027.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg026")
        //' 17~36.各式檢核
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg027")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton6.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg027")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg028")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton7.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg028")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg029")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton8.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg029")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg030")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext029.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg030")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg031")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton9.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg031")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg032")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton10.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg032")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg033")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton11.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg033")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg034")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext031.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg034")
        //'臨時性防災措施
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg035")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton12.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg035")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg036")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton13.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg036")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg037")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton14.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg037")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg038")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext033.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg038")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg039")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton15.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg039")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg040")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton16.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg040")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg041")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton17.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg041")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg042")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext035.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg042")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg043")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton18.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg043")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg044")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton19.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg044")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg045")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton20.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg045")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg046")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext037.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg046")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg047")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton21.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg047")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg048")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton22.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg048")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg049")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton23.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg049")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg050")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext039.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg050")
        //'第一頁到此

        //'第二頁開始
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg051")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton24.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg051")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg052")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton25.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg052")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg053")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton26.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg053")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg054")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext041.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg054")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg055")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton27.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg055")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg056")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton28.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg056")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg057")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton29.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg057")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg058")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext043.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg058")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg059")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton30.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg059")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg060")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton31.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg060")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg061")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton32.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg061")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg062")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext045.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg062")
        //' 永久性防災措試
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg063")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton33.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg063")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg064")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton34.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg064")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg065")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton35.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg065")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg066")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext047.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg066")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg067")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton36.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg067")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg068")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton37.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg068")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg069")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton38.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg069")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg070")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext049.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg070")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg071")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton39.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg071")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg072")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton40.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg072")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg073")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton41.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg073")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg074")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext051.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg074")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg075")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton42.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg075")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg076")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton43.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg076")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg077")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton44.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg077")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg078")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext053.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg078")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg079")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton45.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg079")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg080")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton46.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg080")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg081")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton47.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg081")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg082")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext055.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg082")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg083")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton48.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg083")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg084")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton49.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg084")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg085")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton50.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg085")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg086")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext057.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg086")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg087")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton51.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg087")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg088")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton52.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg088")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg089")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton53.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg089")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg090")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext059.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg090")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg091")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton54.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg091")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg092")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton55.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg092")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg093")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton56.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg093")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg094")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext061.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg094")
        //'監造計師是否在場
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg095")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton60.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg095")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg096")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton61.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg096")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg097")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext083.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg097")
        //'監造紀錄
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg098")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton62.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg098")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg099")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton63.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg099")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg100")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext085.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg100")
        //'災害搶救小組
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg101")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton64.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg101")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg102")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //If(RadioButton65.Checked) Then
        //    ct.SetSimpleColumn(New Phrase("■", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //Else
        //    ct.SetSimpleColumn(New Phrase("□", New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //End If
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg102")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg103")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext087.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg103")
        //' 37.實施與計畫或規定不符事項及改正奇現
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg104")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext062.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg104")
        //' 38.其他注意事項
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg105")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext064.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg105")
        //' 39.前次改正事項
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg106")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext065.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg106")
        //' 40.簽名
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg107")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext067.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_LEFT)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg107")
        //' 41.相片標題
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg108")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext006.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg108")
        //' 42~47.相片說明
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg109")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext070.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg109")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg110")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext072.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg110")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg111")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext074.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg111")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg112")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext076.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg112")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg113")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext078.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg113")
        //ps = Pdfstamper.AcroFields.GetFieldPositions("swcchg114")
        //p = ps(0)
        //ct = New ColumnText(Pdfstamper.GetOverContent(p.page))
        //ct.SetSimpleColumn(New Phrase(swcchgtext080.Text, New iTextSharp.text.Font(BaseFont.CreateFont("C:\WINDOWS\Fonts\kaiu.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED), 9)), p.position.Left, p.position.Bottom, p.position.Right, p.position.Top, 12, Element.ALIGN_CENTER)
        //ct.Go()
        //Pdfstamper.AcroFields.RemoveField("swcchg114")


        //' 以下處理圖片
        //Dim imageurl As String = ""
        //Dim pdfimageobj As iTextSharp.text.Image
        //'A4 大小 寬:0~595 高:0~842
        //'加圖片1進去
        //If swcchgtext069.Text <> "" Then
        //    Try
        //        imageurl = Image2.ImageUrl
        //        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //        pdfimageobj.ScaleToFit(212.5, 141.2)
        //        pdfimageobj.SetAbsolutePosition(178.5 - (pdfimageobj.ScaledWidth / 2), 630.5 - (pdfimageobj.ScaledHeight / 2))
        //        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //    Catch ex As Exception

        //    End Try
        //End If
        //'加圖片2進去
        //If swcchgtext071.Text <> "" Then
        //    Try
        //        imageurl = Image3.ImageUrl
        //        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //        pdfimageobj.ScaleToFit(212.5, 141.2)
        //        pdfimageobj.SetAbsolutePosition(416 - (pdfimageobj.ScaledWidth / 2), 630.5 - (pdfimageobj.ScaledHeight / 2))
        //        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //    Catch ex As Exception

        //    End Try
        //End If
        //'加圖片3進去
        //If swcchgtext073.Text <> "" Then
        //    Try
        //        imageurl = Image4.ImageUrl
        //        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //        pdfimageobj.ScaleToFit(212.5, 141.2)
        //        pdfimageobj.SetAbsolutePosition(178.5 - (pdfimageobj.ScaledWidth / 2), 431 - (pdfimageobj.ScaledHeight / 2))
        //        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //    Catch ex As Exception

        //    End Try
        //End If
        //'加圖片4進去
        //If swcchgtext075.Text <> "" Then
        //    Try
        //        imageurl = Image5.ImageUrl
        //        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //        pdfimageobj.ScaleToFit(212.5, 141.2)
        //        pdfimageobj.SetAbsolutePosition(416 - (pdfimageobj.ScaledWidth / 2), 431 - (pdfimageobj.ScaledHeight / 2))
        //        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //    Catch ex As Exception

        //    End Try
        //End If
        //'加圖片5進去
        //If swcchgtext077.Text <> "" Then
        //    Try
        //        imageurl = Image6.ImageUrl
        //        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //        pdfimageobj.ScaleToFit(212.5, 141.2)
        //        pdfimageobj.SetAbsolutePosition(178.5 - (pdfimageobj.ScaledWidth / 2), 233 - (pdfimageobj.ScaledHeight / 2))
        //        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //    Catch ex As Exception

        //    End Try
        //End If
        //'加圖片6進去
        //If swcchgtext068.Text <> "" Then
        //    Try
        //        imageurl = Image7.ImageUrl
        //        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //        pdfimageobj.ScaleToFit(212.5, 141.2)
        //        pdfimageobj.SetAbsolutePosition(416 - (pdfimageobj.ScaledWidth / 2), 233 - (pdfimageobj.ScaledHeight / 2))
        //        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //    Catch ex As Exception

        //    End Try
        //End If

        //''簽名進去(要算位置哦)
        //'If swcchgtext067.Text <> "" Then
        //'    Try
        //'        imageurl = Image1.ImageUrl
        //'        'imageurl = ConfigurationManager.AppSettings("swcchpdfexportimageweb") + imageurl
        //'        pdfimageobj = iTextSharp.text.Image.GetInstance(imageurl)
        //'        pdfimageobj.ScaleToFit(212.5, 141.2)
        //'        pdfimageobj.SetAbsolutePosition(416 - (pdfimageobj.ScaledWidth / 2), 233 - (pdfimageobj.ScaledHeight / 2))
        //'        Pdfstamper.GetOverContent(4).AddImage(pdfimageobj)
        //'    Catch ex As Exception

        //'    End Try
        //'End If

        //Pdfstamper.Close()
        //Pdfreader.Close()
        //'PDF套表結束



        //''第四頁 圖說
        //''Dim reader As PdfReader
        //''Dim reader2nd As PdfReader
        //''Dim pdfdoc As Document = New Document()
        //''Dim writer As PdfWriter = PdfWriter.GetInstance(pdfdoc, New FileStream(Server.MapPath("~\\pdf\\m" + pdfnewname), FileMode.Append))
        //''pdfdoc.Open()
        //''Dim cb As PdfContentByte = writer.DirectContent
        //''Dim newPage As PdfImportedPage
        //''reader = New PdfReader(Server.MapPath("~\\pdf\\" + pdfnewname))
        //''reader2nd = New PdfReader(Server.MapPath("~\\swcch.pdf"))
        //''Dim iPageNum As Integer
        //''iPageNum = reader.NumberOfPages
        //''For i = 1 To iPageNum
        //''    pdfdoc.NewPage()
        //''    newPage = writer.GetImportedPage(reader, i)
        //''    cb.AddTemplate(newPage, 0, 0)
        //''    'Pdfstamper.GetOverContent(3 + j).Add(cb)
        //''Next i
        //''iPageNum = reader2nd.NumberOfPages
        //''For j = 1 To iPageNum
        //''    pdfdoc.NewPage()
        //''    newPage = writer.GetImportedPage(reader2nd, j)
        //''    cb.AddTemplate(newPage, 0, 0)
        //''    'Pdfstamper.GetOverContent(3 + j).Add(cb)
        //''Next j
        //''reader.Close()
        //''reader2nd.Close()

        //' ''pdfdoc.Close()






        //'    //把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        //Dim iStream As System.IO.Stream
        //'    //以10K為單位暫存:
        //'    Byte[] buffer = new byte[10000];
        //Dim buffer(10000) As Byte
        //Dim length As Integer = 0
        //Dim dataToRead As Long = 0
        //'    // 制定文件路徑.
        //Dim filepath As String = Server.MapPath("~\\pdf\\" + pdfnewname)
        //Dim filepathm As String = Server.MapPath("~\\pdf\\m" + pdfnewname)

        //'    //  得到文件名.
        //Dim filename As String = System.IO.Path.GetFileName(filepath)
        //'    //Try
        //'    // 打開文件.
        //iStream = New System.IO.FileStream(filepath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
        //'    // 得到文件大小:
        //dataToRead = iStream.Length
        //Response.ContentType = "application/x-rar-compressed"
        //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename))
        //Do
        //    If(Response.IsClientConnected) Then
        //        length = iStream.Read(buffer, 0, 10000)
        //        Response.OutputStream.Write(buffer, 0, length)
        //        Response.Flush()
        //        dataToRead = dataToRead - length
        //    Else
        //        dataToRead = -1
        //    End If
        //Loop While(dataToRead > 0)

        //If(iStream.Length <> 0) Then

        //    '        //關閉文件
        //    iStream.Close()
        //    File.Delete(filepath)
        //    'File.Delete(filepathm)

        //    '        //My.Computer.FileSystem.DeleteFile(filepath, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently);

        //End If
        //'DetailCase()
    }

    private void GenPdf()
    {
        string rCaseId = Request.QueryString["SWCNO"] + "";
        string rDTLId = Request.QueryString["DTLNO"] + "";

        //PDF套表開始
        PdfReader Pdfreader = new PdfReader(Server.MapPath("../OutputFile/Sample/swcchg.pdf"));
        string pdfnewname = DateTime.Now.ToString("yyyyMMddHHmmss") + ".pdf";
        PdfStamper Pdfstamper = new PdfStamper(Pdfreader, new FileStream(Server.MapPath("~/OutputFile/" + pdfnewname), FileMode.Create));


























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