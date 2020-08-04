'Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
'Copyright (C) <2020>  <Geotechnical Engineering Office, Public Works Department, Taipei City Government>
'
'This program is free software: you can redistribute it and/or modify
'it under the terms of the GNU Affero General Public License as
'published by the Free Software Foundation, either version 3 of the
'License, or any later version.
'This program is distributed in the hope that it will be useful,
'but WITHOUT ANY WARRANTY; without even the implied warranty of
'MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'GNU Affero General Public License for more details.

'You should have received a copy of the GNU Affero General Public License
'along with this program.  If not, see <https://www.gnu.org/licenses/>.

Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports AjaxControlToolkit
Imports System.IO
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Drawing
Imports System.Runtime.InteropServices.Marshal


Partial Class swc_104
    Inherits System.Web.UI.Page

    Dim chfilecount As Integer
    Dim swc_c_count As Integer
    Dim i As Integer
    Dim baseDate As Date = "2014-12-25"  '開工期限的比對日期基準日，以前的都以這一天起算
    Dim setdate As Date = "2018-12-25"   '開工期限的比對日期前的都以這一天當作開工期限

    Sub gettempid()
        '第一、連結資料庫
        Dim swcconnstringsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        swcconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        swcconn = New SqlConnection(swcconnstringsetting.ConnectionString)
        swcconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT [SWC02] FROM SWCSWC where ([SWC02] like 'TT99" + Trim(Convert.ToInt16(Now.Year.ToString("0000") - 1911).ToString("000")) + Now.Month.ToString("00") + "%') ORDER BY [SWC02]", swcconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swc")
        '第四、關閉資料庫的連接與相關資源
        swcconn.Close()
        If ds.Tables("swc").Rows.Count > 0 Then '有本月的暫存檔
            tempswc02.Text = "TT99" + Trim(Convert.ToInt16(Now.Year.ToString("0000") - 1911).ToString("000")) + Now.Month.ToString("00") + (Convert.ToInt16(Right(ds.Tables("swc").Rows(ds.Tables("swc").Rows.Count - 1)("SWC02").ToString(), 3)) + 1).ToString("000")
        Else   '沒有本月的暫存檔
            tempswc02.Text = "TT99" + Trim(Convert.ToInt16(Now.Year.ToString("0000") - 1911).ToString("000")) + Now.Month.ToString("00") + "001"
        End If
    End Sub
    Sub MB(ByVal messagetext As String)
        Try
            Dim myMsg As New Literal()
            myMsg.Text = "<script>alert('" & messagetext & "')</script>"
            Page.Controls.Add(myMsg)
        Catch
        End Try
    End Sub

    Function getstates(ByVal dist As String, ByVal casestate As String) As String
        Dim listitemtemp As String = "" '要傳回去的值
        Dim landconnstringsetting As ConnectionStringSettings
        Dim landconn As SqlConnection
        Dim landcommand As SqlCommand
        Dim landreader As SqlDataReader
        Dim selectstring As String
        Dim err As Boolean = False
        landconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        landconn = New SqlConnection(landconnstringsetting.ConnectionString)
        landconn.Open()
        selectstring = ""
        If Len(dist) > 0 Then
            dist = Left(dist, Len(dist) - 1)
        End If
        Select Case casestate
            Case "受理中"
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton11.Text) + Convert.ToInt32(LinkButton21.Text) + Convert.ToInt32(LinkButton31.Text) + Convert.ToInt32(LinkButton41.Text) + Convert.ToInt32(LinkButton51.Text) + Convert.ToInt32(LinkButton61.Text) + Convert.ToInt32(LinkButton71.Text) + Convert.ToInt32(LinkButton81.Text) + Convert.ToInt32(LinkButton91.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "審查中"
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton12.Text) + Convert.ToInt32(LinkButton22.Text) + Convert.ToInt32(LinkButton32.Text) + Convert.ToInt32(LinkButton42.Text) + Convert.ToInt32(LinkButton52.Text) + Convert.ToInt32(LinkButton62.Text) + Convert.ToInt32(LinkButton72.Text) + Convert.ToInt32(LinkButton82.Text) + Convert.ToInt32(LinkButton92.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "已核定"
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton13.Text) + Convert.ToInt32(LinkButton23.Text) + Convert.ToInt32(LinkButton33.Text) + Convert.ToInt32(LinkButton43.Text) + Convert.ToInt32(LinkButton53.Text) + Convert.ToInt32(LinkButton63.Text) + Convert.ToInt32(LinkButton73.Text) + Convert.ToInt32(LinkButton83.Text) + Convert.ToInt32(LinkButton93.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "施工中"
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton14.Text) + Convert.ToInt32(LinkButton24.Text) + Convert.ToInt32(LinkButton34.Text) + Convert.ToInt32(LinkButton44.Text) + Convert.ToInt32(LinkButton54.Text) + Convert.ToInt32(LinkButton64.Text) + Convert.ToInt32(LinkButton74.Text) + Convert.ToInt32(LinkButton84.Text) + Convert.ToInt32(LinkButton94.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "已完工"
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton15.Text) + Convert.ToInt32(LinkButton25.Text) + Convert.ToInt32(LinkButton35.Text) + Convert.ToInt32(LinkButton45.Text) + Convert.ToInt32(LinkButton55.Text) + Convert.ToInt32(LinkButton65.Text) + Convert.ToInt32(LinkButton75.Text) + Convert.ToInt32(LinkButton85.Text) + Convert.ToInt32(LinkButton95.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "已廢止"
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton16.Text) + Convert.ToInt32(LinkButton26.Text) + Convert.ToInt32(LinkButton36.Text) + Convert.ToInt32(LinkButton46.Text) + Convert.ToInt32(LinkButton56.Text) + Convert.ToInt32(LinkButton66.Text) + Convert.ToInt32(LinkButton76.Text) + Convert.ToInt32(LinkButton86.Text) + Convert.ToInt32(LinkButton96.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "不予受理"
                If dist = "" Then
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case "撤銷"
                If dist = "" Then
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC04]='" + casestate + "'"
                Else
                    selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "' and [SWC04]='" + casestate + "'"
                End If
            Case ""
                If dist = "" Then
                    '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                    listitemtemp = Trim((Convert.ToInt32(LinkButton01.Text) + Convert.ToInt32(LinkButton02.Text) + Convert.ToInt32(LinkButton03.Text) + Convert.ToInt32(LinkButton04.Text) + Convert.ToInt32(LinkButton05.Text) + Convert.ToInt32(LinkButton06.Text)).ToString())
                    'Return listitemtemp
                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC "
                Else
                    Select Case dist
                        Case "北投"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton11.Text) + Convert.ToInt32(LinkButton12.Text) + Convert.ToInt32(LinkButton13.Text) + Convert.ToInt32(LinkButton14.Text) + Convert.ToInt32(LinkButton15.Text) + Convert.ToInt32(LinkButton16.Text)).ToString())
                            'Return listitemtemp
                        Case "士林"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton21.Text) + Convert.ToInt32(LinkButton22.Text) + Convert.ToInt32(LinkButton23.Text) + Convert.ToInt32(LinkButton24.Text) + Convert.ToInt32(LinkButton25.Text) + Convert.ToInt32(LinkButton26.Text)).ToString())
                            'Return listitemtemp
                        Case "大安"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton31.Text) + Convert.ToInt32(LinkButton32.Text) + Convert.ToInt32(LinkButton33.Text) + Convert.ToInt32(LinkButton34.Text) + Convert.ToInt32(LinkButton35.Text) + Convert.ToInt32(LinkButton36.Text)).ToString())
                            'Return listitemtemp
                        Case "中山"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton41.Text) + Convert.ToInt32(LinkButton42.Text) + Convert.ToInt32(LinkButton43.Text) + Convert.ToInt32(LinkButton44.Text) + Convert.ToInt32(LinkButton45.Text) + Convert.ToInt32(LinkButton46.Text)).ToString())
                            'Return listitemtemp
                        Case "內湖"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton51.Text) + Convert.ToInt32(LinkButton52.Text) + Convert.ToInt32(LinkButton53.Text) + Convert.ToInt32(LinkButton54.Text) + Convert.ToInt32(LinkButton55.Text) + Convert.ToInt32(LinkButton56.Text)).ToString())
                            'Return listitemtemp
                        Case "文山"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton61.Text) + Convert.ToInt32(LinkButton62.Text) + Convert.ToInt32(LinkButton63.Text) + Convert.ToInt32(LinkButton64.Text) + Convert.ToInt32(LinkButton65.Text) + Convert.ToInt32(LinkButton66.Text)).ToString())
                            'Return listitemtemp
                        Case "中正"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton71.Text) + Convert.ToInt32(LinkButton72.Text) + Convert.ToInt32(LinkButton73.Text) + Convert.ToInt32(LinkButton74.Text) + Convert.ToInt32(LinkButton75.Text) + Convert.ToInt32(LinkButton76.Text)).ToString())
                            'Return listitemtemp
                        Case "信義"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton81.Text) + Convert.ToInt32(LinkButton82.Text) + Convert.ToInt32(LinkButton83.Text) + Convert.ToInt32(LinkButton84.Text) + Convert.ToInt32(LinkButton85.Text) + Convert.ToInt32(LinkButton86.Text)).ToString())
                            'Return listitemtemp
                        Case "南港"
                            '為了怕梅田區的案件統計數字錯誤，所以改成用加的來算
                            listitemtemp = Trim((Convert.ToInt32(LinkButton91.Text) + Convert.ToInt32(LinkButton92.Text) + Convert.ToInt32(LinkButton93.Text) + Convert.ToInt32(LinkButton94.Text) + Convert.ToInt32(LinkButton95.Text) + Convert.ToInt32(LinkButton96.Text)).ToString())
                            'Return listitemtemp
                        Case Else
                            err = True
                    End Select

                    'selectstring = "SELECT count(""SWC00"") FROM SWCSWC where [SWC08]='" + dist + "'"
                End If

            Case Else
                err = True
        End Select
        If err = False And listitemtemp = "" Then
            landcommand = New SqlCommand(selectstring, landconn)
            landreader = landcommand.ExecuteReader()
            'Dim listitemtemp As String
            'listitemtemp = ""
            While landreader.Read
                listitemtemp = landreader.Item(0).ToString()
            End While
            landreader.Close()
            landcommand.Dispose()
        End If
        landconn.Close()
        landconn.Dispose()
        Return listitemtemp
    End Function

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Dim sSearchType As String = Request("SearchType")
        Session("menuitem") = "imgmenu01"

        '修正統計表日期
        Label20.Text = "(85年至" + Trim((Convert.ToInt32(Left(Now.Date.ToString("yyyyMMdd"), 4)) - 1911).ToString()) + "年" + Trim(Convert.ToInt32(Now.Date.ToString("yyyyMMdd").Substring(4, 2)).ToString("00")) + "月" + Trim(Convert.ToInt32(Now.Date.ToString("yyyyMMdd").Substring(6, 2)).ToString("00")) + "日)"

        '以下測試時候先設定方便的()
        'Session("name") = "系統管理員"
        'Session("grade") = ""
        'Session("uid") = "gv-admin"
        'Session("right") = "系統管理員"

        '以上測試時候先設定方便的
        If IsDBNull(Session("name")) Or Session("name") = "" Then
            Dim tempurl As String = Request.Url.Query.Replace("?", "andand")
            Dim tempurlaa As String = tempurl.Replace("&", "andand")
            Response.Redirect("login.aspx?page=swc_104.aspx" + tempurlaa)
        Else
            Page.MaintainScrollPositionOnPostBack = True
            labname.Text = "您好，" + Session("name").ToString() + Session("grade").ToString()
        End If

        If Not IsPostBack Then

            swcdbwrite("login")
            '清除查詢框
            'cleanquery()
            '報表
            LinkButton11.Text = getstates("北投區", "受理中")
            LinkButton21.Text = getstates("士林區", "受理中")
            LinkButton31.Text = getstates("大安區", "受理中")
            LinkButton41.Text = getstates("中山區", "受理中")
            LinkButton51.Text = getstates("內湖區", "受理中")
            LinkButton61.Text = getstates("文山區", "受理中")
            LinkButton71.Text = getstates("中正區", "受理中")
            LinkButton81.Text = getstates("信義區", "受理中")
            LinkButton91.Text = getstates("南港區", "受理中")
            LinkButton01.Text = getstates("", "受理中")
            LinkButton12.Text = getstates("北投區", "審查中")
            LinkButton22.Text = getstates("士林區", "審查中")
            LinkButton32.Text = getstates("大安區", "審查中")
            LinkButton42.Text = getstates("中山區", "審查中")
            LinkButton52.Text = getstates("內湖區", "審查中")
            LinkButton62.Text = getstates("文山區", "審查中")
            LinkButton72.Text = getstates("中正區", "審查中")
            LinkButton82.Text = getstates("信義區", "審查中")
            LinkButton92.Text = getstates("南港區", "審查中")
            LinkButton02.Text = getstates("", "審查中")
            LinkButton13.Text = getstates("北投區", "已核定")
            LinkButton23.Text = getstates("士林區", "已核定")
            LinkButton33.Text = getstates("大安區", "已核定")
            LinkButton43.Text = getstates("中山區", "已核定")
            LinkButton53.Text = getstates("內湖區", "已核定")
            LinkButton63.Text = getstates("文山區", "已核定")
            LinkButton73.Text = getstates("中正區", "已核定")
            LinkButton83.Text = getstates("信義區", "已核定")
            LinkButton93.Text = getstates("南港區", "已核定")
            LinkButton03.Text = getstates("", "已核定")
            LinkButton14.Text = getstates("北投區", "施工中")
            LinkButton24.Text = getstates("士林區", "施工中")
            LinkButton34.Text = getstates("大安區", "施工中")
            LinkButton44.Text = getstates("中山區", "施工中")
            LinkButton54.Text = getstates("內湖區", "施工中")
            LinkButton64.Text = getstates("文山區", "施工中")
            LinkButton74.Text = getstates("中正區", "施工中")
            LinkButton84.Text = getstates("信義區", "施工中")
            LinkButton94.Text = getstates("南港區", "施工中")
            LinkButton04.Text = getstates("", "施工中")
            LinkButton15.Text = getstates("北投區", "已完工")
            LinkButton25.Text = getstates("士林區", "已完工")
            LinkButton35.Text = getstates("大安區", "已完工")
            LinkButton45.Text = getstates("中山區", "已完工")
            LinkButton55.Text = getstates("內湖區", "已完工")
            LinkButton65.Text = getstates("文山區", "已完工")
            LinkButton75.Text = getstates("中正區", "已完工")
            LinkButton85.Text = getstates("信義區", "已完工")
            LinkButton95.Text = getstates("南港區", "已完工")
            LinkButton05.Text = getstates("", "已完工")
            LinkButton16.Text = getstates("北投區", "已廢止")
            LinkButton26.Text = getstates("士林區", "已廢止")
            LinkButton36.Text = getstates("大安區", "已廢止")
            LinkButton46.Text = getstates("中山區", "已廢止")
            LinkButton56.Text = getstates("內湖區", "已廢止")
            LinkButton66.Text = getstates("文山區", "已廢止")
            LinkButton76.Text = getstates("中正區", "已廢止")
            LinkButton86.Text = getstates("信義區", "已廢止")
            LinkButton96.Text = getstates("南港區", "已廢止")
            LinkButton06.Text = getstates("", "已廢止")
            LinkButton17.Text = getstates("北投區", "")
            LinkButton27.Text = getstates("士林區", "")
            LinkButton37.Text = getstates("大安區", "")
            LinkButton47.Text = getstates("中山區", "")
            LinkButton57.Text = getstates("內湖區", "")
            LinkButton67.Text = getstates("文山區", "")
            LinkButton77.Text = getstates("中正區", "")
            LinkButton87.Text = getstates("信義區", "")
            LinkButton97.Text = getstates("南港區", "")
            LinkButton07.Text = getstates("", "")
            LinkButton11.CommandArgument = "北投區,受理中"
            LinkButton21.CommandArgument = "士林區,受理中"
            LinkButton31.CommandArgument = "大安區,受理中"
            LinkButton41.CommandArgument = "中山區,受理中"
            LinkButton51.CommandArgument = "內湖區,受理中"
            LinkButton61.CommandArgument = "文山區,受理中"
            LinkButton71.CommandArgument = "中正區,受理中"
            LinkButton81.CommandArgument = "信義區,受理中"
            LinkButton91.CommandArgument = "南港區,受理中"
            LinkButton01.CommandArgument = ",受理中"
            LinkButton12.CommandArgument = "北投區,審查中"
            LinkButton22.CommandArgument = "士林區,審查中"
            LinkButton32.CommandArgument = "大安區,審查中"
            LinkButton42.CommandArgument = "中山區,審查中"
            LinkButton52.CommandArgument = "內湖區,審查中"
            LinkButton62.CommandArgument = "文山區,審查中"
            LinkButton72.CommandArgument = "中正區,審查中"
            LinkButton82.CommandArgument = "信義區,審查中"
            LinkButton92.CommandArgument = "南港區,審查中"
            LinkButton02.CommandArgument = ",審查中"
            LinkButton13.CommandArgument = "北投區,已核定"
            LinkButton23.CommandArgument = "士林區,已核定"
            LinkButton33.CommandArgument = "大安區,已核定"
            LinkButton43.CommandArgument = "中山區,已核定"
            LinkButton53.CommandArgument = "內湖區,已核定"
            LinkButton63.CommandArgument = "文山區,已核定"
            LinkButton73.CommandArgument = "中正區,已核定"
            LinkButton83.CommandArgument = "信義區,已核定"
            LinkButton93.CommandArgument = "南港區,已核定"
            LinkButton03.CommandArgument = ",已核定"
            LinkButton14.CommandArgument = "北投區,施工中"
            LinkButton24.CommandArgument = "士林區,施工中"
            LinkButton34.CommandArgument = "大安區,施工中"
            LinkButton44.CommandArgument = "中山區,施工中"
            LinkButton54.CommandArgument = "內湖區,施工中"
            LinkButton64.CommandArgument = "文山區,施工中"
            LinkButton74.CommandArgument = "中正區,施工中"
            LinkButton84.CommandArgument = "信義區,施工中"
            LinkButton94.CommandArgument = "南港區,施工中"
            LinkButton04.CommandArgument = ",施工中"
            LinkButton15.CommandArgument = "北投區,已完工"
            LinkButton25.CommandArgument = "士林區,已完工"
            LinkButton35.CommandArgument = "大安區,已完工"
            LinkButton45.CommandArgument = "中山區,已完工"
            LinkButton55.CommandArgument = "內湖區,已完工"
            LinkButton65.CommandArgument = "文山區,已完工"
            LinkButton75.CommandArgument = "中正區,已完工"
            LinkButton85.CommandArgument = "信義區,已完工"
            LinkButton95.CommandArgument = "南港區,已完工"
            LinkButton05.CommandArgument = ",已完工"
            LinkButton16.CommandArgument = "北投區,已廢止"
            LinkButton26.CommandArgument = "士林區,已廢止"
            LinkButton36.CommandArgument = "大安區,已廢止"
            LinkButton46.CommandArgument = "中山區,已廢止"
            LinkButton56.CommandArgument = "內湖區,已廢止"
            LinkButton66.CommandArgument = "文山區,已廢止"
            LinkButton76.CommandArgument = "中正區,已廢止"
            LinkButton86.CommandArgument = "信義區,已廢止"
            LinkButton96.CommandArgument = "南港區,已廢止"
            LinkButton06.CommandArgument = ",已廢止"
            LinkButton17.CommandArgument = "北投區,"
            LinkButton27.CommandArgument = "士林區,"
            LinkButton37.CommandArgument = "大安區,"
            LinkButton47.CommandArgument = "中山區,"
            LinkButton57.CommandArgument = "內湖區,"
            LinkButton67.CommandArgument = "文山區,"
            LinkButton77.CommandArgument = "中正區,"
            LinkButton87.CommandArgument = "信義區,"
            LinkButton97.CommandArgument = "南港區,"
            LinkButton07.CommandArgument = ","

            '文字框禁用ENTER
            Dim textobject As TextBox
            Dim objectobj As Object
            Dim objtypename As String
            LandAddlandno.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")
            'SWC_r_no.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")
            SWC_r_c.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")
            SWC_r_f.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")
            '查巡的文字框禁用ENTER
            For i = 1 To 9
                objectobj = FindControl("textboxq" + Trim(i.ToString("0")))
                objtypename = objectobj.GetType.Name
                Select Case objtypename
                    Case "TextBox"  '文字框
                        textobject = objectobj
                        textobject.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")

                End Select
            Next i
            For i = 10 To 14
                objectobj = FindControl("textboxq" + Trim(i.ToString("00")))
                objtypename = objectobj.GetType.Name
                Select Case objtypename
                    Case "TextBox"  '文字框
                        textobject = objectobj
                        textobject.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")

                End Select
            Next i
            '案件詳情文字框禁用ENTER
            For i = 1 To 99
                If i <> 5 Or i <> 13 Or i <> 14 Or i <> 24 Or i <> 46 Or i <> 48 Or i <> 66 Or i <> 79 Or i <> 87 Or i <> 96 Or i <> 97 Or i <> 98 Then
                    Try
                        objectobj = FindControl("SWC" + Trim(i.ToString("00")))
                        objtypename = objectobj.GetType.Name
                        Select Case objtypename
                            Case "TextBox"  '文字框
                                textobject = objectobj
                                textobject.Attributes.Add("onkeypress", "if( event.keyCode == 13 ) { return false; }")
                        End Select
                    Catch ex As Exception

                    End Try
                End If
            Next i
            '編修的時候可以上傳跟刪除
            Dim gridviewdel As Boolean = False   '看不到刪除上傳復健 X
            Dim fileuploaddel As Boolean = False '看不到刪除上傳檔案 X
            Select Case Request("EditMode")
                Case "addnew"
                    'If Not IsPostBack Then
                    '    '存放附件檔名的SESSION
                    '    Session("messagemailpsfilename") = ""
                    '    '存放有沒有上除附件的SESSION
                    '    Session("mailps") = ""
                    '    '給一個臨時的用來辨識的暫存ID，以避免和其他人混在一起，項附件阿等等就是以這個資料夾編號區隔
                    '    Session("mssagemailid") = Now.Year.ToString("0000") + Now.Month.ToString("00") + Now.Day.ToString("00") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00")
                    'End If
                    '取得SWC02案件暫存編號，以便先存放附件資料夾
                    gettempid()  '暫時的ID範例 temp201603XXX  2016年3月的999件暫存
                    Gridpanel.Visible = False
                    Detailpanel.Visible = True
                    Buttonpanel.Visible = True
                    SWChistorypanel.Visible = False
                    cleartxt()
                    InitGVFrontendTypeSetting()
                    'getswcdata()
                    'getlanddata()
                    'getswccheckdata()
                    'getuploadfile()
                    txteditable()
                    buttonmode()
                    'getswccasehistorydata()  '原始案件與變更設計案件導入
                    If Request("copy") = "copy" Then '複製案件
                        copycasea("get")
                    End If
                    gridviewdel = True
                    fileuploaddel = True
                Case "detail"
                    Try
                        Dim SWC02id As String = Request("SWC02").ToString
                        '從圖臺來的，所以要把SWC02找到對應的SWC00再重攳進入一次detail
                        '第一、連結資料庫
                        Dim swcconnstringsetting As ConnectionStringSettings
                        Dim swcconn As SqlConnection
                        swcconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
                        swcconn = New SqlConnection(swcconnstringsetting.ConnectionString)
                        swcconn.Open()
                        '第二、執行SQL指令，取出資料
                        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT [SWC00], [SWC02] FROM SWCSWC where ([SWC02] = '" + SWC02id + " ') ORDER BY [SWC02]", swcconn)
                        Dim ds As DataSet = New DataSet()
                        da.Fill(ds, "swc")
                        '第四、關閉資料庫的連接與相關資源
                        swcconn.Close()
                        If ds.Tables("swc").Rows.Count = 1 Then
                            Response.Redirect(ConfigurationManager.AppSettings("thisip") + "tcge/SWC_104.aspx?Editmode=detail&SWC00=" + Trim(ds.Tables("swc").Rows(0)("SWC00").ToString()))
                        Else
                            Response.Redirect("SWC_104.aspx")
                        End If
                    Catch ex As Exception
                        '正常的詳情
                        swcdbwrite("detail")
                        Gridpanel.Visible = False
                        Detailpanel.Visible = True
                        Buttonpanel.Visible = True
                        SWChistorypanel.Visible = True
                        cleartxt()
                        InitGVFrontendTypeSetting()
                        getswcdata()
                        getlanddata()
                        getswccheckdata()
                        getuploadfile()
                        GetVrFilesData("view")
                        txteditable()
                        buttonmode()
                        getswccasehistorydata()  '原始案件與變更設計案件導入
                        '寫入使用者紀錄
                        swcdbwrite("detail")
                        gridviewdel = False
                        fileuploaddel = False
                    End Try

                Case "edit"
                    Gridpanel.Visible = False
                    Detailpanel.Visible = True
                    Buttonpanel.Visible = True
                    SWChistorypanel.Visible = False
                    cleartxt()
                    InitGVFrontendTypeSetting()
                    getswcdata()
                    getlanddata()
                    getswccheckdata()
                    getuploadfile()
                    GetVrFilesData("edit")
                    txteditable()
                    buttonmode()
                    'getswccasehistorydata()  '原始案件與變更設計案件導入
                    gridviewdel = True
                    fileuploaddel = True
                Case Else
                    Gridpanel.Visible = True
                    Detailpanel.Visible = False
                    Buttonpanel.Visible = False

                    If sSearchType = "1" Then
                        pageSearhSet()
                        Call qButton1_Click(sender, e)
                    End If
            End Select

            '右側上傳復健的刪除要可以用嗎
            receivefileuploadpanel.Visible = gridviewdel
            receivefileGridView.Columns.Item(2).Visible = gridviewdel
            CheckfileuploadPanel.Visible = gridviewdel
            CheckfileGridView.Columns.Item(2).Visible = gridviewdel
            douploadpanel.Visible = gridviewdel
            doGridView.Columns.Item(2).Visible = gridviewdel
            finishuploadpanel.Visible = gridviewdel
            finishGridView.Columns.Item(2).Visible = gridviewdel
            elseuploadPanel.Visible = gridviewdel
            elseGridView.Columns.Item(2).Visible = gridviewdel
            '中間的公開的GRIDVIEW
            GridView1.Columns.Item(9).Visible = gridviewdel
            swccheckGridView.Columns.Item(5).Visible = gridviewdel
        End If


    End Sub
    Sub pageSearhSet()
        Dim Search01 As String
        Dim Search02 As String
        Dim Search03 As String
        Dim Search04 As String
        Dim Search05 As String
        Dim Search06 As String
        Dim Search07 As String
        Dim Search08 As String
        Dim Search09 As String
        Dim Search10 As String
        Dim Search11 As String
        Dim Search12 As String
        Dim Search13 As String
        Dim Search14 As String

        Dim sTextBoxq1 As String
        Dim sTextBoxq2 As String
        Dim sTextBoxq3 As String
        Dim sTextBoxq4 As String
        Dim sTextBoxq5 As String
        Dim sTextBoxq6 As String
        Dim sTextBoxq7 As String
        Dim sTextBoxq8 As String
        Dim sTextBoxq9 As String
        Dim sTextBoxq10 As String
        Dim sTextBoxq11 As String
        Dim sTextBoxq12 As String
        Dim sTextBoxq13 As String
        Dim sTextBoxq14 As String
        Dim sTextBoxq15 As String

        Dim sDorplistq3 As String
        Dim sDorplistq4 As String
        Dim sDorplistq5 As String

        Search01 = Session("CheckBoxq1")
        Search02 = Session("CheckBoxq2")
        Search03 = Session("CheckBoxq3")
        Search04 = Session("CheckBoxq4")
        Search05 = Session("CheckBoxq5")
        Search06 = Session("CheckBoxq6")
        Search07 = Session("CheckBoxq7")
        Search08 = Session("CheckBoxq8")
        Search09 = Session("CheckBoxq9")
        Search10 = Session("CheckBoxq10")
        Search11 = Session("CheckBoxq11")
        Search12 = Session("CheckBoxq12")
        Search13 = Session("CheckBoxq13")
        Search14 = Session("CheckBoxq14")

        sTextBoxq1 = Session("TextBoxq1")
        sTextBoxq2 = Session("TextBoxq2")
        sTextBoxq3 = Session("TextBoxq3")
        sTextBoxq4 = Session("TextBoxq4")
        sTextBoxq5 = Session("TextBoxq5")
        sTextBoxq6 = Session("TextBoxq6")
        sTextBoxq7 = Session("TextBoxq7")
        sTextBoxq8 = Session("TextBoxq8")
        sTextBoxq9 = Session("TextBoxq9")
        sTextBoxq10 = Session("TextBoxq10")
        sTextBoxq11 = Session("TextBoxq11")
        sTextBoxq12 = Session("TextBoxq12")
        sTextBoxq13 = Session("TextBoxq13")
        sTextBoxq14 = Session("TextBoxq14")
        sTextBoxq15 = Session("TextBoxq15")

        sDorplistq3 = Session("Dorplistq3")
        sDorplistq4 = Session("Dorplistq4")
        sDorplistq5 = Session("Dorplistq5")

        If Search01 <> "" Then
            CheckBoxq1.Checked = True
        End If
        If Search02 <> "" Then
            CheckBoxq2.Checked = True
        End If
        If Search03 <> "" Then
            CheckBoxq3.Checked = True
        End If
        If Search04 <> "" Then
            CheckBoxq4.Checked = True
        End If
        If Search05 <> "" Then
            CheckBoxq5.Checked = True
        End If
        If Search06 <> "" Then
            CheckBoxq6.Checked = True
        End If
        If Search07 <> "" Then
            CheckBoxq7.Checked = True
        End If
        If Search08 <> "" Then
            CheckBoxq8.Checked = True
        End If
        If Search09 <> "" Then
            CheckBoxq9.Checked = True
        End If
        If Search10 <> "" Then
            CheckBoxq10.Checked = True
        End If
        If Search11 <> "" Then
            CheckBoxq11.Checked = True
        End If
        If Search12 <> "" Then
            CheckBoxq12.Checked = True
        End If
        If Search13 <> "" Then
            CheckBoxq13.Checked = True
        End If
        If Search14 <> "" Then
            CheckBoxq14.Checked = True
        End If

        TextBoxq1.Text = sTextBoxq1
        TextBoxq2.Text = sTextBoxq2
        TextBoxq3.Text = sTextBoxq3
        TextBoxq4.Text = sTextBoxq4
        TextBoxq5.Text = sTextBoxq5
        TextBoxq6.Text = sTextBoxq6
        TextBoxq7.Text = sTextBoxq7
        TextBoxq8.Text = sTextBoxq8
        TextBoxq9.Text = sTextBoxq9
        TextBoxq10.Text = sTextBoxq10
        TextBoxq11.Text = sTextBoxq11
        TextBoxq12.Text = sTextBoxq12
        TextBoxq13.Text = sTextBoxq13
        TextBoxq14.Text = sTextBoxq14
        TextBoxq15.Text = sTextBoxq15

        Dorplistq3.Text = sDorplistq3
        Dorplistq3_SelectedIndexChanged(Dorplistq3, System.EventArgs.Empty)

        Dorplistq4.Text = sDorplistq4
        Dorplistq4_SelectedIndexChanged(Dorplistq4, System.EventArgs.Empty)

        Dorplistq5.Text = sDorplistq5

    End Sub
    Sub RemoveSearchSession()
        Session("CheckBoxq1") = ""
        Session("CheckBoxq2") = ""
        Session("CheckBoxq3") = ""
        Session("CheckBoxq4") = ""
        Session("CheckBoxq5") = ""
        Session("CheckBoxq6") = ""
        Session("CheckBoxq7") = ""
        Session("CheckBoxq8") = ""
        Session("CheckBoxq9") = ""
        Session("CheckBoxq10") = ""
        Session("CheckBoxq11") = ""
        Session("CheckBoxq12") = ""
        Session("CheckBoxq13") = ""
        Session("CheckBoxq14") = ""
        Session("TextBoxq1") = ""
        Session("TextBoxq2") = ""
        Session("TextBoxq3") = ""
        Session("TextBoxq4") = ""
        Session("TextBoxq5") = ""
        Session("TextBoxq6") = ""
        Session("TextBoxq7") = ""
        Session("TextBoxq8") = ""
        Session("TextBoxq9") = ""
        Session("TextBoxq10") = ""
        Session("TextBoxq11") = ""
        Session("TextBoxq12") = ""
        Session("TextBoxq13") = ""
        Session("TextBoxq14") = ""
        Session("TextBoxq15") = ""

        Session("Dorplistq3") = ""
        Session("Dorplistq4") = ""
        Session("Dorplistq5") = ""

    End Sub
    Sub getswccasehistorydata()

        If SWC02.Text = "" Then
            Exit Sub
        End If
        '第一、連結資料庫
        Dim swcconnstringsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        swcconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        swcconn = New SqlConnection(swcconnstringsetting.ConnectionString)
        swcconn.Open()
        '第二、執行SQL指令，取出資料
        Dim likecaseid As String = Left(SWC02.Text, 12)
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT [SWC00], [SWC02] FROM SWCSWC where ([SWC02] like '" + likecaseid + "%' and [SWC02] <> '" + SWC02.Text + "') ORDER BY [SWC02]", swcconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swchistory")

        '第三、執行SQL指令之後，把資料庫撈出來的結果，交由畫面上 TextBox控制項來呈現。
        Dim swchistorytbCategory As DataTable = ViewState("swchistorytbCategory")
        Try
            swchistorytbCategory.Clear()
        Catch ex As Exception

        End Try
        If ds.Tables("swchistory").Rows.Count > 0 Then
            Try
                For i = 1 To ds.Tables("swchistory").Rows.Count
                    swchistorytbCategory.Rows.Add(swchistorytbCategory.NewRow())
                    swchistorytbCategory.Rows(i - 1)("行政審查案件編號") = Trim(ds.Tables("swchistory").Rows(i - 1)("SWC02").ToString())
                    swchistorytbCategory.Rows(i - 1)("行政審查案件編號URL") = "swc_104.aspx?EditMode=detail&SWC00=" + Trim(ds.Tables("swchistory").Rows(i - 1)("SWC00").ToString())
                Next
            Catch ex As Exception

            End Try
        End If
        '
        '第四、關閉資料庫的連接與相關資源
        swcconn.Close()

        ViewState("swchistorytbCategory") = swchistorytbCategory
        'Bind the table to the gridview    
        SWChistroygridview.DataSource = ViewState("swchistorytbCategory")
        SWChistroygridview.DataBind()

    End Sub

    Sub getuploadfile()
        '先設定檔案的目錄
        Dim filename As String = SWC02.Text.Substring(0, 12)
        Dim FileYear As Integer
        Dim FileYearS As String
        Dim targetDirectory As String
        Dim targetDirectoryurl As String
        Dim newtargetDirectory As String
        Dim newtargetDirectoryurl As String
        Dim defaultpath() As String
        Dim defaulturl() As String
        Dim defaultpathdo() As String
        Dim defaulturldo() As String
        Dim defaultpathitem As String
        Dim defaulturlitem As String
        Dim defaultpathitem67 As String
        Dim defaulturlitem67 As String
        Dim defaultpathitem67new As String
        Dim defaulturlitem67new As String
        Dim kk As Integer = 0
        Dim kkdo As Integer = 0
        Dim receivefloder As String = "受理"
        Dim checkfloder As String = "審查"
        Dim dofloder As String = "施工中"
        Dim finishfloder As String = "已完工"
        Dim elsefloder As String = "其他"
        Dim psfloder As String = "備註"
        Dim fileExists As Boolean

        Try
            FileYear = Convert.ToInt16(filename.Substring(4, 3))
            FileYearS = FileYear.ToString()
            If (FileYear > 93) Then
                FileYearS = FileYearS + "年掃描圖檔"
            Else
                FileYearS = "93年度暨以前掃描圖檔"
            End If
            targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
            targetDirectoryurl = ConfigurationManager.AppSettings("swcpsurl") + FileYearS + "/水保申請案件/水保計畫"
            If (SWC07.Text = "簡易水保") Then
                targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
                targetDirectoryurl = ConfigurationManager.AppSettings("swcpsurl") + FileYearS + "/水保申請案件/簡易水保"
            End If
            newtargetDirectory = targetDirectory
            newtargetDirectoryurl = targetDirectoryurl

            defaultpathitem67 = targetDirectory + "\" + filename
            defaulturlitem67 = targetDirectoryurl + "/" + filename
            defaultpathitem67new = targetDirectory + "\" + SWC02.Text
            defaulturlitem67new = targetDirectoryurl + "/" + SWC02.Text
            Dim subdirectoryEntries() As String = Directory.GetDirectories(targetDirectory)
            Dim subdirectory As String
            Dim tk As Integer
            Dim tkdo As Integer
            For Each subdirectory In subdirectoryEntries
                Dim di As DirectoryInfo = New DirectoryInfo(subdirectory)
                If di.Name <> SWC02.Text Then
                    'If di.Name.Length > 11 Then '簡易水保資料夾有檔名長度小於13者會出現錯誤，會導致無法讀取審查資料夾
                    If di.Name.Length > 13 Then '簡易水保資料夾有檔名長度小於13者會出現錯誤，會導致無法讀取審查資料夾
                        If (filename = di.Name.Substring(0, 12)) Then
                            If (di.Name.Length = 12) Or ((di.Name.Length > 11) And (di.Name.Substring(12, 2) <> "-1") And (di.Name.Substring(12, 2) <> "-2") And (di.Name.Substring(12, 2) <> "-3") And (di.Name.Substring(12, 2) <> "-4") And (di.Name.Substring(12, 2) <> "-5") And (di.Name.Substring(12, 2) <> "-6") And (di.Name.Substring(12, 2) <> "-7") And (di.Name.Substring(12, 2) <> "-8") And (di.Name.Substring(12, 2) <> "-9")) Then
                                If kk = 0 Then
                                    ReDim defaultpath(kk)
                                    ReDim defaulturl(kk)
                                Else
                                    Dim ttpath(kk - 1) As String
                                    Dim ttpathurl(kk - 1) As String
                                    For tk = 0 To kk - 1
                                        ttpath(tk) = defaultpath(tk)
                                        ttpathurl(tk) = defaulturl(tk)
                                    Next
                                    ReDim defaultpath(kk)
                                    ReDim defaulturl(kk)
                                    For tk = 0 To kk - 1
                                        defaultpath(tk) = ttpath(tk)
                                        defaulturl(tk) = ttpathurl(tk)
                                    Next
                                End If
                                defaultpath(kk) = targetDirectory + "\" + di.Name.ToString
                                defaulturl(kk) = targetDirectoryurl + "/" + di.Name.ToString
                                kk = kk + 1
                            End If
                            '以下是為了處理施工中藥不管甚麼資料夾有沒有變更，通通要出來所作呃
                            If (di.Name.Length > 11) Then
                                If kkdo = 0 Then
                                    ReDim defaultpathdo(kkdo)
                                    ReDim defaulturldo(kkdo)
                                Else
                                    Dim ttpathdo(kkdo - 1) As String
                                    Dim ttpathurldo(kkdo - 1) As String
                                    For tkdo = 0 To kkdo - 1
                                        ttpathdo(tkdo) = defaultpathdo(tkdo)
                                        ttpathurldo(tkdo) = defaulturldo(tkdo)
                                    Next
                                    ReDim defaultpathdo(kkdo)
                                    ReDim defaulturldo(kkdo)
                                    For tkdo = 0 To kkdo - 1
                                        defaultpathdo(tkdo) = ttpathdo(tkdo)
                                        defaulturldo(tkdo) = ttpathurldo(tkdo)
                                    Next
                                End If
                                defaultpathdo(kkdo) = targetDirectory + "\" + di.Name.ToString
                                defaulturldo(kkdo) = targetDirectoryurl + "/" + di.Name.ToString
                                kkdo = kkdo + 1
                            End If
                        End If
                    End If
                End If

            Next
        Catch
        End Try
        If Len(SWC02.Text) > 12 Then
            Dim aa As String
            aa = Right(SWC02.Text, 1)
            Select Case aa
                Case "1"
                    checkfloder = "一變"
                Case "2"
                    checkfloder = "二變"
                Case "3"
                    checkfloder = "三變"
                Case "4"
                    checkfloder = "四變"
                Case "5"
                    checkfloder = "五變"
                Case "6"
                    checkfloder = "六變"
                Case "7"
                    checkfloder = "七變"
                Case "8"
                    checkfloder = "八變"
                Case "9"
                    checkfloder = "九變"

            End Select
        End If
        '先處理圖6-1跟圖7-1
        Dim folderExists As Boolean
        Dim filelist6() As String
        Dim filelist7() As String
        Dim filelists() As String
        '新的直接放自己的資料夾的,_1,_2......
        defaultpathitem67 = targetDirectory + "\" + SWC02.Text
        defaulturlitem67 = targetDirectoryurl + "/" + SWC02.Text
        Dim b61 As Boolean = False
        Dim b71 As Boolean = False
        folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\審查\6-1\")
        If folderExists Then
            filelist6 = Directory.GetFiles(defaultpathitem67 + "\審查\6-1\")
            Try
                i = 0
                If filelist6(i) <> "" Then
                    If My.Computer.FileSystem.GetFileInfo(filelist6(i)).Name = "Thumbs.db" Then
                        i = i + 1
                    End If
                    SWC29_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist6(i)).Name
                    SWC29_hyperlink.NavigateUrl = defaulturlitem67 + "/審查/6-1/" + SWC29_hyperlink.Text
                    b61 = True
                End If
            Catch ex As Exception
                SWC29_hyperlink.Text = ""
                SWC29_hyperlink.NavigateUrl = ""
            End Try
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\審查\7-1\")
        If folderExists Then
            filelist7 = Directory.GetFiles(defaultpathitem67 + "\審查\7-1\")
            Try
                i = 0
                If filelist7(i) <> "" Then
                    If My.Computer.FileSystem.GetFileInfo(filelist7(i)).Name = "Thumbs.db" Then
                        i = i + 1
                    End If
                    SWC30_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist7(i)).Name
                    SWC30_hyperlink.NavigateUrl = defaulturlitem67 + "/審查/7-1/" + SWC30_hyperlink.Text
                    b71 = True
                End If
            Catch ex As Exception
                SWC30_hyperlink.Text = ""
                SWC30_hyperlink.NavigateUrl = ""
            End Try
        End If
        '舊的存放在一變二變裡面的
        defaultpathitem67 = targetDirectory + "\" + filename
        defaulturlitem67 = targetDirectoryurl + "/" + filename
        If b61 = False Then
            folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\" + checkfloder + "\6-1\")
            If folderExists Then
                filelist6 = Directory.GetFiles(defaultpathitem67 + "\" + checkfloder + "\6-1\")
                Try
                    i = 0
                    If filelist6(i) <> "" Then
                        If My.Computer.FileSystem.GetFileInfo(filelist6(i)).Name = "Thumbs.db" Then
                            i = i + 1
                        End If
                        SWC29_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist6(i)).Name
                        SWC29_hyperlink.NavigateUrl = defaulturlitem67 + "/" + checkfloder + "/6-1/" + SWC29_hyperlink.Text
                    End If
                Catch ex As Exception
                    'SWC29_hyperlink.Text = ""
                    'SWC29_hyperlink.NavigateUrl = ""
                End Try

            End If
        End If
        If b71 = False Then
            folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\" + checkfloder + "\7-1\")
            If folderExists Then
                filelist7 = Directory.GetFiles(defaultpathitem67 + "\" + checkfloder + "\7-1\")
                Try
                    i = 0
                    If filelist7(i) <> "" Then
                        If My.Computer.FileSystem.GetFileInfo(filelist7(i)).Name = "Thumbs.db" Then
                            i = i + 1
                        End If
                        SWC30_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist7(i)).Name
                        SWC30_hyperlink.NavigateUrl = defaulturlitem67 + "/" + checkfloder + "/7-1/" + SWC30_hyperlink.Text
                    End If
                Catch ex As Exception
                    'SWC30_hyperlink.Text = ""
                    'SWC30_hyperlink.NavigateUrl = ""
                End Try
            End If
        End If

        '處理掃描檔，已經變成核定本了，只是資夾還試用掃描檔
        '新的直接放自己的資料夾的,_1,_2......
        defaultpathitem67 = targetDirectory + "\" + SWC02.Text
        defaulturlitem67 = targetDirectoryurl + "/" + SWC02.Text
        Dim bs As Boolean = False
        folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\掃描檔\掃描檔\")
        If folderExists Then
            filelists = Directory.GetFiles(defaultpathitem67 + "\掃描檔\掃描檔\")
            Try
                i = 0
                If filelists(i) <> "" Then
                    If My.Computer.FileSystem.GetFileInfo(filelists(i)).Name = "Thumbs.db" Then
                        i = i + 1
                    End If
                    SWC80_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelists(i)).Name
                    SWC80_hyperlink.NavigateUrl = defaulturlitem67 + "/掃描檔/掃描檔/" + SWC80_hyperlink.Text
                    bs = True
                End If
            Catch ex As Exception
                SWC80_hyperlink.Text = ""
                SWC80_hyperlink.NavigateUrl = ""
            End Try
        End If
        '舊的存放在一變二變裡面的
        If bs = False Then
            defaultpathitem67 = targetDirectory + "\" + filename
            defaulturlitem67 = targetDirectoryurl + "/" + filename
            folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\掃描檔\掃描檔\")
            If folderExists Then
                filelists = Directory.GetFiles(defaultpathitem67 + "\掃描檔\掃描檔\")
                Try
                    i = 0
                    If filelists(i) <> "" Then
                        If My.Computer.FileSystem.GetFileInfo(filelists(i)).Name = "Thumbs.db" Then
                            i = i + 1
                        End If
                        SWC80_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelists(i)).Name
                        SWC80_hyperlink.NavigateUrl = defaulturlitem67 + "/掃描檔/掃描檔/" + SWC80_hyperlink.Text
                    End If
                Catch ex As Exception
                    SWC80_hyperlink.Text = ""
                    SWC80_hyperlink.NavigateUrl = ""
                End Try
            End If
        End If

        '處理受理附件
        Dim rctbCategory As DataTable = ViewState("rctbCategory")
        Try
            rctbCategory.Clear()
        Catch ex As Exception

        End Try

        Dim filelist() As String
        Dim rcfile As String
        chfilecount = 0
        '處理新的
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\受理\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\受理\")
            Try
                For Each rcfile In filelist
                    If My.Computer.FileSystem.GetFileInfo(rcfile).Name <> "Thumbs.db" Then
                        rctbCategory.Rows.Add(rctbCategory.NewRow())
                        rctbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                        rctbCategory.Rows(chfilecount)("附件URL") = newtargetDirectoryurl + "/" + SWC02.Text + "/受理/" + My.Computer.FileSystem.GetFileInfo(rcfile).Name
                        rctbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(rcfile).Name
                        rctbCategory.Rows(chfilecount)("附件路徑") = rcfile
                        chfilecount = chfilecount + 1
                    End If

                Next
            Catch ex As Exception

            End Try

        End If
        '在處理舊的

        Dim rcflodercounter As Integer
        If IsArray(defaultpath) Then
            'For rcflodercounter = 0 To defaultpath.Length - 1
            For rcflodercounter = defaultpath.Length - 1 To 0 Step -1
                folderExists = My.Computer.FileSystem.DirectoryExists(defaultpath(rcflodercounter) + "\" + receivefloder + "\")
                If folderExists Then
                    filelist = Directory.GetFiles(defaultpath(rcflodercounter) + "\" + receivefloder + "\")
                    Try
                        For Each rcfile In filelist
                            If My.Computer.FileSystem.GetFileInfo(rcfile).Name <> "Thumbs.db" Then
                                rctbCategory.Rows.Add(rctbCategory.NewRow())
                                rctbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                                rctbCategory.Rows(chfilecount)("附件URL") = defaulturl(rcflodercounter) + "/" + receivefloder + "/" + My.Computer.FileSystem.GetFileInfo(rcfile).Name
                                rctbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(rcfile).Name
                                rctbCategory.Rows(chfilecount)("附件路徑") = rcfile
                                chfilecount = chfilecount + 1
                            End If

                        Next
                    Catch ex As Exception

                    End Try

                End If
            Next
        End If
        Try
            rctbCategory.DefaultView.Sort = "附件檔名 asc"
        Catch ex As Exception

        End Try

        ViewState("rctbCategory") = rctbCategory
        'Bind the table to the gridview    
        receivefileGridView.DataSource = ViewState("rctbCategory")
        receivefileGridView.DataBind()

        '處理審查附件
        Dim cftbCategory As DataTable = ViewState("cftbCategory")
        Try
            cftbCategory.Clear()
        Catch ex As Exception

        End Try
        Dim chfile As String
        chfilecount = 0
        '先處理新的自己的資料夾
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\審查\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\審查\")
            Try
                For Each chfile In filelist
                    If My.Computer.FileSystem.GetFileInfo(chfile).Name <> "Thumbs.db" Then
                        cftbCategory.Rows.Add(cftbCategory.NewRow())
                        cftbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                        cftbCategory.Rows(chfilecount)("附件URL") = newtargetDirectoryurl + "/" + SWC02.Text + "/審查/" + My.Computer.FileSystem.GetFileInfo(chfile).Name
                        cftbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(chfile).Name
                        cftbCategory.Rows(chfilecount)("附件路徑") = chfile
                        chfilecount = chfilecount + 1
                    End If

                Next
            Catch ex As Exception

            End Try

        End If

        '在處理舊的一變二變那種資料夾
        'If checkfloder <> "審查" Then
        'Dim filelist() As String
        Dim chflodercounter As Integer
        If IsArray(defaultpath) Then
            'For chflodercounter = 0 To defaultpath.Length - 1
            For chflodercounter = defaultpath.Length - 1 To 0 Step -1
                folderExists = My.Computer.FileSystem.DirectoryExists(defaultpath(chflodercounter) + "\" + checkfloder + "\")
                If folderExists Then
                    filelist = Directory.GetFiles(defaultpath(chflodercounter) + "\" + checkfloder + "\")
                    Try
                        For Each chfile In filelist
                            If My.Computer.FileSystem.GetFileInfo(chfile).Name <> "Thumbs.db" Then
                                cftbCategory.Rows.Add(cftbCategory.NewRow())
                                cftbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                                cftbCategory.Rows(chfilecount)("附件URL") = defaulturl(chflodercounter) + "/" + checkfloder + "/" + My.Computer.FileSystem.GetFileInfo(chfile).Name
                                cftbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(chfile).Name
                                cftbCategory.Rows(chfilecount)("附件路徑") = chfile
                                chfilecount = chfilecount + 1
                            End If

                        Next
                    Catch ex As Exception

                    End Try

                End If
            Next
        End If
        'End If
        Try
            cftbCategory.DefaultView.Sort = "附件檔名 asc"
        Catch ex As Exception

        End Try

        ViewState("cftbCategory") = cftbCategory
        'Bind the table to the gridview    
        CheckfileGridView.DataSource = ViewState("cftbCategory")
        CheckfileGridView.DataBind()


        '處理施工中附件，這邊注意，施工中是要所有的案件不管有沒有變更，通通所有資料夾都要顯示，所以在搜尋舊的一變二變的時候，檔案夾要記得用defaultpathdo()根defaulturldo
        Dim dotbCategory As DataTable = ViewState("dotbCategory")
        Try
            dotbCategory.Clear()
        Catch ex As Exception

        End Try
        Dim dofile As String
        chfilecount = 0
        '先處理新的
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\施工中\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\施工中\")
            Try
                For Each dofile In filelist
                    If My.Computer.FileSystem.GetFileInfo(dofile).Name <> "Thumbs.db" Then
                        dotbCategory.Rows.Add(dotbCategory.NewRow())
                        dotbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                        dotbCategory.Rows(chfilecount)("附件URL") = newtargetDirectoryurl + "/" + SWC02.Text + "/施工中/" + My.Computer.FileSystem.GetFileInfo(dofile).Name
                        dotbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(dofile).Name
                        dotbCategory.Rows(chfilecount)("附件路徑") = dofile
                        chfilecount = chfilecount + 1
                    End If

                Next
            Catch ex As Exception

            End Try
        End If

        '如果是有-1 -2，在處理一個新的12碼的新案
        If Len(SWC02.Text) > 12 Then

            folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + Left(SWC02.Text, 12) + "\施工中\")

            If folderExists Then
                filelist = Directory.GetFiles(newtargetDirectory + "\" + Left(SWC02.Text, 12) + "\施工中\")

                Try
                    For Each dofile In filelist
                        If My.Computer.FileSystem.GetFileInfo(dofile).Name <> "Thumbs.db" Then
                            dotbCategory.Rows.Add(dotbCategory.NewRow())
                            dotbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                            dotbCategory.Rows(chfilecount)("附件URL") = newtargetDirectoryurl + "/" + Left(SWC02.Text, 12) + "/施工中/" + My.Computer.FileSystem.GetFileInfo(dofile).Name
                            dotbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(dofile).Name
                            dotbCategory.Rows(chfilecount)("附件路徑") = dofile
                            chfilecount = chfilecount + 1
                        End If

                    Next
                Catch ex As Exception

                End Try
            End If

        End If
        '在處理舊的方式
        Dim doflodercounter As Integer
        If IsArray(defaultpath) Then
            'For doflodercounter = 0 To defaultpath.Length - 1
            For doflodercounter = defaultpath.Length - 1 To 0 Step -1
                folderExists = My.Computer.FileSystem.DirectoryExists(defaultpath(doflodercounter) + "\" + dofloder + "\")
                If folderExists Then
                    filelist = Directory.GetFiles(defaultpath(doflodercounter) + "\" + dofloder + "\")
                    Try
                        For Each dofile In filelist
                            If My.Computer.FileSystem.GetFileInfo(dofile).Name <> "Thumbs.db" Then
                                dotbCategory.Rows.Add(dotbCategory.NewRow())
                                dotbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                                dotbCategory.Rows(chfilecount)("附件URL") = defaulturl(doflodercounter) + "/" + dofloder + "/" + My.Computer.FileSystem.GetFileInfo(dofile).Name
                                dotbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(dofile).Name
                                dotbCategory.Rows(chfilecount)("附件路徑") = dofile
                                chfilecount = chfilecount + 1
                            End If

                        Next
                    Catch ex As Exception

                    End Try

                End If
            Next
        End If
        Try
            dotbCategory.DefaultView.Sort = "附件檔名 asc"
        Catch ex As Exception

        End Try

        ViewState("dotbCategory") = dotbCategory
        'Bind the table to the gridview  
        doGridView.DataSource = ViewState("dotbCategory")
        doGridView.DataBind()

        '處理已完工附件
        Dim finishtbCategory As DataTable = ViewState("finishtbCategory")
        Try
            finishtbCategory.Clear()
        Catch ex As Exception

        End Try
        Dim finishfile As String
        chfilecount = 0
        '先處理新的
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\已完工\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\已完工\")
            Try
                For Each finishfile In filelist
                    If My.Computer.FileSystem.GetFileInfo(finishfile).Name <> "Thumbs.db" Then
                        finishtbCategory.Rows.Add(finishtbCategory.NewRow())
                        finishtbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                        finishtbCategory.Rows(chfilecount)("附件URL") = newtargetDirectoryurl + "/" + SWC02.Text + "/已完工/" + My.Computer.FileSystem.GetFileInfo(finishfile).Name
                        finishtbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(finishfile).Name
                        finishtbCategory.Rows(chfilecount)("附件路徑") = finishfile
                        chfilecount = chfilecount + 1
                    End If

                Next
            Catch ex As Exception

            End Try

        End If
        '在處理舊的()
        Dim finishflodercounter As Integer
        If IsArray(defaultpath) Then
            'For finishflodercounter = 0 To defaultpath.Length - 1
            For finishflodercounter = defaultpath.Length - 1 To 0 Step -1
                folderExists = My.Computer.FileSystem.DirectoryExists(defaultpath(finishflodercounter) + "\" + finishfloder + "\")
                If folderExists Then
                    filelist = Directory.GetFiles(defaultpath(finishflodercounter) + "\" + finishfloder + "\")
                    Try
                        For Each finishfile In filelist
                            If My.Computer.FileSystem.GetFileInfo(finishfile).Name <> "Thumbs.db" Then
                                finishtbCategory.Rows.Add(finishtbCategory.NewRow())
                                finishtbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                                finishtbCategory.Rows(chfilecount)("附件URL") = defaulturl(finishflodercounter) + "/" + finishfloder + "/" + My.Computer.FileSystem.GetFileInfo(finishfile).Name
                                finishtbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(finishfile).Name
                                finishtbCategory.Rows(chfilecount)("附件路徑") = finishfile
                                chfilecount = chfilecount + 1
                            End If

                        Next
                    Catch ex As Exception

                    End Try

                End If
            Next
        End If
        Try
            finishtbCategory.DefaultView.Sort = "附件檔名 asc"
        Catch ex As Exception

        End Try

        ViewState("finishtbCategory") = finishtbCategory
        'Bind the table to the gridview  
        finishGridView.DataSource = ViewState("finishtbCategory")
        finishGridView.DataBind()

        '處理其他附件
        Dim elsetbCategory As DataTable = ViewState("elsetbCategory")
        Try
            elsetbCategory.Clear()
        Catch ex As Exception

        End Try
        Dim elsefile As String
        chfilecount = 0
        '先處理新的
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\其他\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\其他\")
            Try
                For Each elsefile In filelist
                    If My.Computer.FileSystem.GetFileInfo(elsefile).Name <> "Thumbs.db" Then
                        elsetbCategory.Rows.Add(elsetbCategory.NewRow())
                        elsetbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                        elsetbCategory.Rows(chfilecount)("附件URL") = newtargetDirectoryurl + "/" + SWC02.Text + "/其他/" + My.Computer.FileSystem.GetFileInfo(elsefile).Name
                        elsetbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(elsefile).Name
                        elsetbCategory.Rows(chfilecount)("附件路徑") = elsefile
                        chfilecount = chfilecount + 1
                    End If

                Next
            Catch ex As Exception

            End Try

        End If
        '在處理舊的
        Dim elseflodercounter As Integer
        If IsArray(defaultpath) Then
            'For elseflodercounter = 0 To defaultpath.Length - 1
            For elseflodercounter = defaultpath.Length - 1 To 0 Step -1
                folderExists = My.Computer.FileSystem.DirectoryExists(defaultpath(elseflodercounter) + "\" + elsefloder + "\")
                If folderExists Then
                    filelist = Directory.GetFiles(defaultpath(elseflodercounter) + "\" + elsefloder + "\")
                    Try
                        For Each elsefile In filelist
                            If My.Computer.FileSystem.GetFileInfo(elsefile).Name <> "Thumbs.db" Then
                                elsetbCategory.Rows.Add(elsetbCategory.NewRow())
                                elsetbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                                elsetbCategory.Rows(chfilecount)("附件URL") = defaulturl(elseflodercounter) + "/" + elsefloder + "/" + My.Computer.FileSystem.GetFileInfo(elsefile).Name
                                elsetbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(elsefile).Name
                                elsetbCategory.Rows(chfilecount)("附件路徑") = elsefile
                                chfilecount = chfilecount + 1
                            End If

                        Next
                    Catch ex As Exception

                    End Try

                End If
            Next
        End If
        Try
            elsetbCategory.DefaultView.Sort = "附件檔名 asc"
        Catch ex As Exception

        End Try

        ViewState("elsetbCategory") = elsetbCategory
        'Bind the table to the gridview      
        elseGridView.DataSource = ViewState("elsetbCategory")
        elseGridView.DataBind()

        '處理備註附件
        Dim pstbCategory As DataTable = ViewState("pstbCategory")
        Try
            pstbCategory.Clear()
        Catch ex As Exception

        End Try
        Dim psfile As String
        chfilecount = 0
        '先處理新的
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\其他\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\其他\")
            Try
                For Each psfile In filelist
                    If My.Computer.FileSystem.GetFileInfo(psfile).Name <> "Thumbs.db" Then
                        pstbCategory.Rows.Add(pstbCategory.NewRow())
                        pstbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                        pstbCategory.Rows(chfilecount)("附件URL") = defaulturl(elseflodercounter) + "/" + elsefloder + "/" + My.Computer.FileSystem.GetFileInfo(psfile).Name
                        pstbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(psfile).Name
                        pstbCategory.Rows(chfilecount)("附件路徑") = psfile
                        chfilecount = chfilecount + 1
                    End If

                Next
            Catch ex As Exception

            End Try

        End If
        '在處理舊的
        Dim psflodercounter As Integer
        If IsArray(defaultpath) Then
            'For psflodercounter = 0 To defaultpath.Length - 1
            For psflodercounter = defaultpath.Length - 1 To 0 Step -1
                folderExists = My.Computer.FileSystem.DirectoryExists(defaultpath(psflodercounter) + "\" + psfloder + "\")
                If folderExists Then
                    filelist = Directory.GetFiles(defaultpath(psflodercounter) + "\" + psfloder + "\")
                    Try
                        For Each psfile In filelist
                            If My.Computer.FileSystem.GetFileInfo(psfile).Name <> "Thumbs.db" Then
                                pstbCategory.Rows.Add(pstbCategory.NewRow())
                                pstbCategory.Rows(chfilecount)("序號") = chfilecount + 1
                                pstbCategory.Rows(chfilecount)("附件URL") = defaulturl(psflodercounter) + "/" + psfloder + "/" + My.Computer.FileSystem.GetFileInfo(psfile).Name
                                pstbCategory.Rows(chfilecount)("附件檔名") = My.Computer.FileSystem.GetFileInfo(psfile).Name
                                pstbCategory.Rows(chfilecount)("附件路徑") = psfile
                                chfilecount = chfilecount + 1
                            End If

                        Next
                    Catch ex As Exception

                    End Try

                End If
            Next
        End If
        Try
            pstbCategory.DefaultView.Sort = "附件檔名 asc"
        Catch ex As Exception

        End Try


        '處理公開附件1,公開附件2,公開附件3
        '公開附件1
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\公開附件\附件1\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\公開附件\附件1\")
            Try
                i = 0
                If filelist(i) <> "" Then
                    If My.Computer.FileSystem.GetFileInfo(filelist(i)).Name = "Thumbs.db" Then
                        i = i + 1
                    End If
                    otheropen1_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist(i)).Name
                    otheropen1_hyperlink.NavigateUrl = newtargetDirectoryurl + "/" + SWC02.Text + "/公開附件/附件1/" + My.Computer.FileSystem.GetFileInfo(filelist(i)).Name
                End If
            Catch ex As Exception
                otheropen1_hyperlink.Text = ""
                otheropen1_hyperlink.NavigateUrl = ""
            End Try
        End If
        '公開附件2
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\公開附件\附件2\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\公開附件\附件2\")
            Try
                i = 0
                If filelist(i) <> "" Then
                    If My.Computer.FileSystem.GetFileInfo(filelist(i)).Name = "Thumbs.db" Then
                        i = i + 1
                    End If
                    otheropen2_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist(i)).Name
                    otheropen2_hyperlink.NavigateUrl = newtargetDirectoryurl + "/" + SWC02.Text + "/公開附件/附件2/" + My.Computer.FileSystem.GetFileInfo(filelist(i)).Name
                End If
            Catch ex As Exception
                otheropen2_hyperlink.Text = ""
                otheropen2_hyperlink.NavigateUrl = ""
            End Try
        End If
        '公開附件3
        folderExists = My.Computer.FileSystem.DirectoryExists(newtargetDirectory + "\" + SWC02.Text + "\公開附件\附件3\")
        If folderExists Then
            filelist = Directory.GetFiles(newtargetDirectory + "\" + SWC02.Text + "\公開附件\附件3\")
            Try
                i = 0
                If filelist(i) <> "" Then
                    If My.Computer.FileSystem.GetFileInfo(filelist(i)).Name = "Thumbs.db" Then
                        i = i + 1
                    End If
                    otheropen3_hyperlink.Text = My.Computer.FileSystem.GetFileInfo(filelist(i)).Name
                    otheropen3_hyperlink.NavigateUrl = newtargetDirectoryurl + "/" + SWC02.Text + "/公開附件/附件3/" + My.Computer.FileSystem.GetFileInfo(filelist(i)).Name
                End If
            Catch ex As Exception
                otheropen3_hyperlink.Text = ""
                otheropen3_hyperlink.NavigateUrl = ""
            End Try
        End If



        ViewState("pstbCategory") = pstbCategory
        'Bind the table to the gridview      
        psGridView.DataSource = ViewState("pstbCategory")
        psGridView.DataBind()



    End Sub

    Sub txteditable()
        Dim readonlyboolean As Boolean
        Dim enableboolean As Boolean
        Dim bbcolor As Drawing.Color
        Select Case Request("EditMode")
            Case "detail"
                readonlyboolean = True
                enableboolean = False
                icon_editcase.Visible = True
                icon_webmap.Visible = True
                icon_copycase.Visible = True
                icon_exportpdf.Visible = False
                bbcolor = Color.White

            Case "edit", "addnew"
                readonlyboolean = False
                enableboolean = True
                icon_editcase.Visible = False
                icon_webmap.Visible = False
                icon_copycase.Visible = False
                icon_exportpdf.Visible = False
                bbcolor = Color.Pink
            Case Else

        End Select
        Dim objectobj As Object
        Dim textobject As TextBox
        Dim dropobject As DropDownList
        Dim chkobject As CheckBox
        Dim radobject As RadioButton
        Dim objtypename As String
        Dim btnobject As Button
        Dim fileuploadobj As FileUpload
        Dim hyperlinkobj As HyperLink
        Dim calobject As CalendarExtender
        For i = 1 To 99
            If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                objectobj = FindControl("SWC" + Trim(i.ToString("00")))
                objtypename = objectobj.GetType.Name
                Select Case objtypename
                    Case "TextBox"  '文字框
                        textobject = objectobj
                        If i <> 8 And i <> 9 And i <> 10 And i <> 11 And i <> 25 Then
                            textobject.BackColor = bbcolor
                        End If
                        Select Case i
                            Case 23, 35, 40, 62, 64, 68, 69, 70, 72, 74    '以下判斷數字欄位預設要為0，避免存檔入資料庫錯誤
                                textobject.ReadOnly = readonlyboolean
                            Case 31, 32, 33, 34, 37, 38, 42, 43, 51, 52, 53, 55, 57, 58, 59, 78, 82, 84, 88, 89    '以下判斷日期欄位
                                textobject.ReadOnly = readonlyboolean
                                calobject = FindControl("SWC" + Trim(i.ToString("00")) + "_CalendarExtender")
                                calobject.Enabled = enableboolean
                                If i = 82 Or i = 84 Then
                                    If objectobj.text <> "" Then
                                        calobject.StartDate = DateValue(objectobj.text)
                                        calobject.EndDate = DateValue(objectobj.text).AddMonths(6)
                                    End If
                                End If

                            Case 29, 30, 80    '以下判斷檔案上傳
                                btnobject = FindControl("SWC" + Trim(i.ToString("00")) + "_fileclean")
                                btnobject.Enabled = enableboolean
                            Case 54, 76    '以下判斷安檢紀錄表的超連結

                            Case 81        '以下判斷違規的超連結
                                textobject.ReadOnly = readonlyboolean
                            'Case 84    '以下判斷停工到期日，這兩個式系統自動展生的
                            '    textobject.ReadOnly = True
                            Case 1, 8, 9, 10, 11, 25  '以下判斷核定編號，舊地籍的欄位，大地處人員攔位
                                textobject.ReadOnly = True
                            Case 2 '以下判斷行政管理編號
                                textobject.ReadOnly = True
                                If Request("EditMode") = "addnew" Or SWC02.Text = "" Or Left(SWC02.Text, 4) = "TT99" Then
                                    textobject.ReadOnly = False
                                    If Request("EditMode") = "addnew" Then
                                        SWC02.Text = tempswc02.Text
                                    Else
                                        tempswc02.Text = SWC02.Text
                                    End If
                                End If
                                tempswc02.Text = SWC02.Text
                            Case Else
                                textobject.ReadOnly = readonlyboolean
                        End Select

                    Case "DropDownList"   '下拉式選單
                        dropobject = objectobj
                        dropobject.Enabled = enableboolean
                        dropobject.BackColor = bbcolor

                    Case "Button"    '單選的清除按鈕
                        btnobject = objectobj
                        btnobject.Enabled = enableboolean
                        btnobject.Visible = enableboolean
                        Dim radok As Boolean = True
                        Dim radindex As Integer = 1
                        Do While radok
                            Try
                                radobject = FindControl("SWC" + Trim(i.ToString("00")) + Trim(radindex.ToString("00")))
                                radobject.Enabled = enableboolean
                                radindex = radindex + 1
                            Catch ex As Exception
                                radok = False
                            End Try
                        Loop

                    Case "CheckBox"    '多選的何取方塊
                        chkobject = objectobj
                        chkobject.Enabled = enableboolean

                End Select
            End If
        Next i
        '確認遺下Checkbox連動的Textbox
        If Not SWC61.Checked Then
            SWC62.ReadOnly = True
        End If
        If Not SWC63.Checked Then
            SWC64.ReadOnly = True
        End If
        If Not SWC65.Checked Then
            SWC66.ReadOnly = True
        End If
        If Not SWC67.Checked Then
            SWC68.ReadOnly = True
            SWC69.ReadOnly = True
            SWC70.ReadOnly = True
        End If
        If Not SWC71.Checked Then
            SWC72.ReadOnly = True
        End If
        If Not SWC73.Checked Then
            SWC74.ReadOnly = True
        End If
    End Sub

    Sub buttonmode()
        Select Case Request("EditMode")
            Case "detail"
                '處理PANEL不要看到詳情的部分
                LandAdd.Visible = False
                Panel61.Visible = False
                Panel71.Visible = False
                Panelcheckrecord.Visible = False
                'checkrecordedit.Visible = False
                Panelscane.Visible = False
                AddNewOK.Visible = False
                UpdatecaseOK.Visible = False
                CancelOK.Visible = False
                geouser.Visible = False
                Panelotheropen1.Visible = False
                Panelotheropen2.Visible = False
                Panelotheropen3.Visible = False
                otheropen1_fileclean.Enabled = False
                otheropen2_fileclean.Enabled = False
                otheropen3_fileclean.Enabled = False
                '處理下拉式選單不要出來的問題
                Dim letters() As String = {"04", "07", "12", "17", "22", "83", "24", "91", "92"}
                Dim dropboxobj As DropDownList
                Dim labelobj As Label
                For Each letter As String In letters
                    labelobj = FindControl("SWC" + letter + "Label")
                    dropboxobj = FindControl("SWC" + letter)
                    labelobj.Text = dropboxobj.Text
                    labelobj.Visible = True
                    dropboxobj.Visible = False
                Next
                PSlabelshow(False)

            Case "edit", "addnew"
                '處理PANEL要看到編修的部分
                LandAdd.Visible = True
                Panel61.Visible = True
                Panel71.Visible = True
                Panelcheckrecord.Visible = True
                'checkrecordedit.Visible = True
                Panelscane.Visible = True
                If Request("EditMode") = "edit" Then
                    AddNewOK.Visible = False
                    UpdatecaseOK.Visible = True
                End If
                If Request("EditMode") = "addnew" Then
                    AddNewOK.Visible = True
                    UpdatecaseOK.Visible = False
                End If
                CancelOK.Visible = True
                geouser.Visible = True
                Panelotheropen1.Visible = True
                Panelotheropen2.Visible = True
                Panelotheropen3.Visible = True
                otheropen1_fileclean.Enabled = True
                otheropen2_fileclean.Enabled = True
                otheropen3_fileclean.Enabled = True
                '處理下拉式選單出來的問題
                Dim letters() As String = {"04", "07", "12", "17", "22", "83", "24", "91", "92"}
                Dim dropboxobj As DropDownList
                Dim labelobj As Label
                For Each letter As String In letters
                    labelobj = FindControl("SWC" + letter + "Label")
                    dropboxobj = FindControl("SWC" + letter)
                    'labelobj.Text = dropboxobj.Text
                    labelobj.Visible = False
                    dropboxobj.Visible = True
                Next
                PSlabelshow(True)
            Case Else
                AddNewOK.Visible = False
                UpdatecaseOK.Visible = False
                CancelOK.Visible = False
        End Select
    End Sub

    Sub PSlabelshow(look As Boolean)
        '說明
        Dim psletters() As String = {"81", "95", "58", "92", "84", "50", "47", "52", "83", "82", "34", "28", "16", "13", "02"}
        Dim labelobj As Label
        For Each letter As String In psletters
            labelobj = FindControl("SWC" + letter + "PS")
            labelobj.Visible = look
        Next
        '計算字數的
        Dim countletters() As String = {"79", "81", "98", "97", "96", "76", "66", "95", "94", "93", "54", "99", "50", "49", "48", "47", "46", "90", "45", "87", "86", "85", "21", "18", "16", "15", "14", "13", "05"}
        For Each letter As String In countletters
            labelobj = FindControl("SWC" + letter + "_count")
            labelobj.Visible = look
        Next
    End Sub

    Sub cleartxt()
        Dim objectobj As Object
        Dim textobject As TextBox
        Dim dropobject As DropDownList
        Dim chkobject As CheckBox
        Dim radobject As RadioButton
        Dim objtypename As String
        Dim fileuploadobj As FileUpload
        Dim hyperlinkobj As HyperLink
        For i = 1 To 99
            If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                objectobj = FindControl("SWC" + Trim(i.ToString("00")))
                objtypename = objectobj.GetType.Name
                Select Case objtypename
                    Case "TextBox"  '文字框
                        textobject = objectobj
                        Select Case i
                            Case 39, 44    '以下判斷發文文號預設文字，避免存檔入資料庫錯誤
                                textobject.Text = "北市工地審字第號"
                            Case 23, 35, 40, 62, 64, 68, 69, 70, 72, 74    '以下判斷數字欄位預設要為0，避免存檔入資料庫錯誤
                                textobject.Text = "0"
                            Case 29, 30, 80    '以下判斷檔案上傳
                                textobject.Text = ""  '檔案名稱的文字框
                                fileuploadobj = FindControl("SWC" + Trim(i.ToString("00")) + "_fileupload")   '上傳檔案名稱fileupload的檔案選單
                                fileuploadobj.Dispose()
                                hyperlinkobj = FindControl("SWC" + Trim(i.ToString("00")) + "_hyperlink")     'hyperlink檔案名稱的URL
                                hyperlinkobj.NavigateUrl = ""
                                hyperlinkobj.Text = ""
                            Case Else
                                textobject.Text = ""
                        End Select
                    Case "DropDownList"   '下拉式選單
                        dropobject = objectobj
                        dropobject.ClearSelection()
                        dropobject.Items(0).Selected = True
                    Case "Button"    '單選的清除按鈕
                        Dim radok As Boolean = True
                        Dim radindex As Integer = 1
                        Do While radok
                            Try
                                radobject = FindControl("SWC" + Trim(i.ToString("00")) + Trim(radindex.ToString("00")))
                                radobject.Checked = False
                                radindex = radindex + 1
                            Catch ex As Exception
                                radok = False
                            End Try
                        Loop
                    Case "CheckBox"    '多選的何取方塊
                        chkobject = objectobj
                        chkobject.Checked = False

                End Select
            End If
        Next i

        '以下清除審查紀錄的相關資料
        SWC_r_no.Text = ""
        SWC_r_c.Text = ""
        SWC_r_f.Text = ""

    End Sub

    Sub getswcdata()    '水土保持案件資料到入
        '第一、連結資料庫
        Dim swcconnstringsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        swcconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        swcconn = New SqlConnection(swcconnstringsetting.ConnectionString)
        swcconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM SWCSWC where [SWC00]='" + Request("SWC00") + "'", swcconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swc")

        '第三、執行SQL指令之後，把資料庫撈出來的結果，交由畫面上 TextBox控制項來呈現。
        Dim objectobj As Object
        Dim textobject As TextBox
        Dim dropobject As DropDownList
        Dim chkobject As CheckBox
        Dim radobject As RadioButton
        Dim objtypename As String
        Dim fileuploadobj As FileUpload
        Dim hyperlinkobj As HyperLink
        Dim labelobj As Label
        If IsDBNull(ds.Tables("swc").Rows(0)("lightps")) Then
            lightps.Text = ""
        Else
            If Trim(ds.Tables("swc").Rows(0)("lightps").ToString()) = "" Then
                lightps.Text = ""
            Else
                lightps.Text = Trim(ds.Tables("swc").Rows(0)("lightps").ToString())
            End If
        End If
        For i = 1 To 99
            If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                objectobj = FindControl("SWC" + Trim(i.ToString("00")))
                objtypename = objectobj.GetType.Name
                Select Case objtypename
                    Case "TextBox"  '文字框
                        textobject = objectobj
                        Select Case i
                            Case 39, 44    '以下判段發文預設，避免存檔入資料庫錯誤
                                If Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()) <> "" Then
                                    textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                                End If
                            Case 22, 23, 35, 40, 62, 64, 68, 69, 70, 72, 74    '以下判斷數字欄位預設要為0，避免存檔入資料庫錯誤
                                If Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()) <> "" Then
                                    textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                                End If
                            'Case 84    '以下判斷停工到期日日期欄位
                            '    If Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()) <> "" And DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Year.ToString("0000") <> "1900" Then
                            '        'textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                            '        textobject.Text = DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Year.ToString("0000") + "-" + DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Month.ToString("00") + "-" + DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Day.ToString("00")
                            '    End If
                            Case 31, 32, 33, 34, 37, 38, 42, 43, 51, 52, 53, 55, 57, 58, 59, 78, 82, 84, 88, 89    '以下判斷日期欄位
                                Try
                                    If Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()) <> "" And DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Year.ToString("0000") <> "1900" Then
                                        'textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                                        textobject.Text = DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Year.ToString("0000") + "-" + DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Month.ToString("00") + "-" + DateValue(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())).Day.ToString("00")
                                    End If
                                Catch ex As Exception
                                    textobject.Text = ""
                                End Try

                            Case 29, 30, 80    '以下判斷檔案上傳
                                textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())  '檔案名稱的文字框
                                'fileuploadobj = FindControl("SWC" + Trim(i.ToString("00")) + "_fileupload")   '上傳檔案名稱fileupload的檔案選單
                                'fileuploadobj.Dispose()
                                hyperlinkobj = FindControl("SWC" + Trim(i.ToString("00")) + "_hyperlink")     'hyperlink檔案名稱的URL
                                If Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()) <> "" Then
                                    hyperlinkobj.NavigateUrl = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                                    hyperlinkobj.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                                Else
                                    hyperlinkobj.NavigateUrl = ""
                                    hyperlinkobj.Text = ""
                                End If
                            Case 54, 76    '以下判斷安檢紀錄表的超連結
                                'http://tcgeswc.taipei.gov.tw/tslmservice/swcch.aspx?swcchid=SWCCH20150827113155927415&EditMode=detail
                                'http://tcgeswc.taipei.gov.tw/tslmservice/swcchg.aspx?swcchgid=SWCCHG20150924044914&EditMode=detail
                                labelobj = FindControl("LabelSWC" + Trim(i.ToString("00")))     'label名稱
                                labelobj.Text = ""
                                textobject.Text = ""
                                Dim swcchurl As String = ""
                                '第一、連結資料庫
                                Dim swcchforswcconnstringsetting As ConnectionStringSettings
                                Dim swcchforswcconn As SqlConnection
                                swcchforswcconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
                                swcchforswcconn = New SqlConnection(swcchforswcconnstringsetting.ConnectionString)
                                swcchforswcconn.Open()
                                '第二、執行SQL指令，取出資料
                                Dim swcchda As SqlDataAdapter
                                If i = 54 Then  '施工中
                                    swcchda = New SqlDataAdapter("SELECT [施工檢查案件編號] FROM swcchg where left([行政管理案件編號],12)='" + Left(SWC02.Text, 12) + "'", swcchforswcconn)
                                    swcchurl = "http://tcgeswc.taipei.gov.tw/tslmservice/swcchg.aspx?EditMode=detail&swcchgid="
                                End If
                                If i = 76 Then  '已完工
                                    swcchda = New SqlDataAdapter("SELECT [案件編號] FROM swcch where [水保案件編號]='" + SWC02.Text + "' and 備註 <> '隱藏'", swcchforswcconn)
                                    swcchurl = "http://tcgeswc.taipei.gov.tw/tslmservice/swcch.aspx?EditMode=detail&swcchid="
                                End If
                                Dim swcchds As DataSet = New DataSet()
                                swcchda.Fill(swcchds, "swcchforswc")

                                '第三、執行SQL指令之後，把資料庫撈出來的結果，交由畫面上 TextBox控制項來呈現。
                                'casevalue.Text = Trim(ds.Tables("ilgforswc").Rows(0)("ILGindex").ToString())
                                For dscount = 1 To swcchds.Tables("swcchforswc").Rows.Count
                                    Try
                                        If labelobj.Text = "" Then
                                            labelobj.Text = "<a target='SWCCH' href='" + swcchurl + Trim(swcchds.Tables("swcchforswc").Rows(dscount - 1)(0).ToString()) + "'>" + swcchds.Tables("swcchforswc").Rows(dscount - 1)(0).ToString() + "</a>"
                                            textobject.Text = swcchds.Tables("swcchforswc").Rows(dscount - 1)(0).ToString()
                                        Else
                                            labelobj.Text = labelobj.Text + " , " + "<a target='SWCCH' href='" + swcchurl + Trim(swcchds.Tables("swcchforswc").Rows(dscount - 1)(0).ToString()) + "'>" + swcchds.Tables("swcchforswc").Rows(dscount - 1)(0).ToString() + "</a>"
                                            textobject.Text = textobject.Text + "," + swcchds.Tables("swcchforswc").Rows(dscount - 1)(0).ToString()
                                        End If
                                    Catch ex As Exception

                                    End Try
                                Next dscount


                                '第四、關閉資料庫的連接與相關資源
                                swcchforswcconn.Close()
                            Case 81        '以下判斷違規的超連結
                                textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                                '處理違規紀錄的超連接
                                labelobj = FindControl("LabelSWC" + Trim(i.ToString("00")))     'label名稱
                                labelobj.Visible = False
                                labelobj.Text = ""
                                textobject.Visible = True
                                If Request("EditMode") = "detail" Then
                                    If textobject.Text <> "" Then
                                        Dim CANO() As String = textobject.Text.Split(",")
                                        labelobj.Text = ""
                                        For kl = 1 To CANO.Length
                                            If Trim(CANO(kl - 1)) <> "" Then
                                                'http://localhost:17639/web/ilgforswc.aspx?ilgforswcsnindex=1531&EditMode=detail
                                                '取得違規案件序號
                                                '([ILG01] like '%" + Trim(TextBox1.Text) + "%')"
                                                ''第一、連結資料庫
                                                'Dim ilgforswcconnstringsetting As ConnectionStringSettings
                                                'Dim ilgforswcconn As SqlConnection
                                                'ilgforswcconnstringsetting = ConfigurationManager.ConnectionStrings("ilgforswcconnstring")
                                                'ilgforswcconn = New SqlConnection(ilgforswcconnstringsetting.ConnectionString)
                                                'ilgforswcconn.Open()
                                                ''第二、執行SQL指令，取出資料
                                                ''Dim ilgda As SqlDataAdapter = New SqlDataAdapter("SELECT [ILGindex], [ILG01] FROM ILG where [ILG01]='" + CANO(kl - 1) + "'", ilgforswcconn)
                                                'Dim ilgda As SqlDataAdapter = New SqlDataAdapter("SELECT [ILGindex], [ILG001] FROM ILGILG where [ILG01]='" + CANO(kl - 1) + "'", ilgforswcconn)
                                                'Dim ilgds As DataSet = New DataSet()
                                                'ilgda.Fill(ilgds, "ilgforswc")

                                                '第三、執行SQL指令之後，把資料庫撈出來的結果，交由畫面上 TextBox控制項來呈現。
                                                'casevalue.Text = Trim(ds.Tables("ilgforswc").Rows(0)("ILGindex").ToString())
                                                Try
                                                    If labelobj.Text = "" Then
                                                        'labelobj.Text = "<a target='ILG' href='ilgforswc.aspx?ilgforswcsnindex=" + Trim(ilgds.Tables("ilgforswc").Rows(0)("ILGindex").ToString()) + "&EditMode=detail'>" + CANO(kl - 1) + "</a>"
                                                        labelobj.Text = "<a target='ILG' href='ilg_104.aspx?ilg001=" + Trim(CANO(kl - 1)) + "&EditMode=detail'>" + CANO(kl - 1) + "</a>"
                                                    Else
                                                        'labelobj.Text = labelobj.Text + " , " + "<a target='ILG' href='ilgforswc.aspx?ilgforswcsnindex=" + Trim(ilgds.Tables("ilgforswc").Rows(0)("ILGindex").ToString()) + "&EditMode=detail'>" + CANO(kl - 1) + "</a>"
                                                        labelobj.Text = labelobj.Text + " , " + "<a target='ILG' href='ilg_104.aspx?ilg001=" + Trim(CANO(kl - 1)) + "&EditMode=detail'>" + CANO(kl - 1) + "</a>"
                                                    End If
                                                Catch ex As Exception

                                                End Try

                                                ''第四、關閉資料庫的連接與相關資源
                                                'ilgforswcconn.Close()
                                            End If
                                        Next kl
                                    End If
                                    labelobj.Visible = True
                                    textobject.Visible = False
                                End If
                            Case Else
                                textobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                        End Select
                    Case "DropDownList"   '下拉式選單
                        dropobject = objectobj
                        dropobject.ClearSelection()
                        dropobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())

                        '無選項，加選項，並重新選取…
                        If i = 22 Or i = 24 Then
                            If dropobject.Text = "" Then
                                dropobject.Items.Add(Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()))

                                dropobject.ClearSelection()
                                dropobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString())
                            End If
                        End If

                    Case "Button"    '單選的清除按鈕
                        Dim radok As Boolean = True
                        Dim radindex As Integer = 1
                        Do While radok
                            Try
                                radobject = FindControl("SWC" + Trim(i.ToString("00")) + Trim(radindex.ToString("00")))
                                If radobject.Text = Trim(ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00"))).ToString()) Then
                                    radobject.Checked = True
                                Else
                                    radobject.Checked = False
                                End If
                                radindex = radindex + 1
                            Catch ex As Exception
                                radok = False
                            End Try
                        Loop
                    Case "CheckBox"    '多選的何取方塊
                        chkobject = objectobj
                        Try
                            chkobject.Checked = ds.Tables("swc").Rows(0)("SWC" + Trim(i.ToString("00")))
                        Catch ex As Exception
                            chkobject.Checked = False
                        End Try


                End Select
            End If
        Next i




        '第四、關閉資料庫的連接與相關資源
        swcconn.Close()
    End Sub

    Protected Sub qButton1_Click(sender As Object, e As System.EventArgs) Handles qButton1.Click
        swcquereytext.Text = ""
        Dim swcquereytemp As String = ""

        RemoveSearchSession()
        Session("lightps") = ""

        If CheckBoxq1.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq1.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq1.Text) + "')"
            End If
            Session("CheckBoxq1") = CheckBoxq1.Text
        End If
        If CheckBoxq2.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq2.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq2.Text) + "')"
            End If
            Session("CheckBoxq2") = CheckBoxq2.Text
        End If
        If CheckBoxq3.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq3.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq3.Text) + "')"
            End If
            Session("CheckBoxq3") = CheckBoxq3.Text
        End If
        If CheckBoxq4.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq4.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq4.Text) + "')"
            End If
            Session("CheckBoxq4") = CheckBoxq4.Text
        End If
        If CheckBoxq5.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq5.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq5.Text) + "')"
            End If
            Session("CheckBoxq5") = CheckBoxq5.Text
        End If
        If CheckBoxq6.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq6.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq6.Text) + "')"
            End If
            Session("CheckBoxq6") = CheckBoxq6.Text
        End If
        If CheckBoxq7.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq7.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq7.Text) + "')"
            End If
            Session("CheckBoxq7") = CheckBoxq7.Text

        End If
        If CheckBoxq8.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq8.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq8.Text) + "')"
            End If
            Session("CheckBoxq8") = CheckBoxq8.Text
        End If
        If CheckBoxq9.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq9.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq9.Text) + "')"
            End If
            Session("CheckBoxq9") = CheckBoxq9.Text
        End If
        If CheckBoxq10.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq10.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq10.Text) + "')"
            End If
            Session("CheckBoxq10") = CheckBoxq10.Text
        End If
        If CheckBoxq11.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq11.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq11.Text) + "')"
            End If
            Session("CheckBoxq11") = CheckBoxq11.Text
        End If
        If CheckBoxq14.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC04]='" + Trim(CheckBoxq14.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC04]='" + Trim(CheckBoxq14.Text) + "')"
            End If
            Session("CheckBoxq14") = CheckBoxq14.Text
        End If
        '判斷是不是有前面的，有的話因為是 OR 所以要錢後加括號
        If swcquereytemp <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "(" + swcquereytemp + ")"
            Else
                swcquereytext.Text = swcquereytext.Text + " and (" + swcquereytemp + ")"
            End If
        End If
        swcquereytemp = ""
        If CheckBoxq12.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC07]='" + Trim(CheckBoxq12.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC07]='" + Trim(CheckBoxq12.Text) + "')"
            End If
            Session("CheckBoxq12") = CheckBoxq12.Text
        End If
        If CheckBoxq13.Checked Then
            If swcquereytemp = "" Then
                swcquereytemp = "([SWC07]='" + Trim(CheckBoxq13.Text) + "')"
            Else
                swcquereytemp = swcquereytemp + " or ([SWC07]='" + Trim(CheckBoxq13.Text) + "')"
            End If
            Session("CheckBoxq13") = CheckBoxq13.Text
        End If
        '判斷是不是有前面的，有的話因為是 OR 所以要錢後加括號
        If swcquereytemp <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "(" + swcquereytemp + ")"
            Else
                swcquereytext.Text = swcquereytext.Text + " and (" + swcquereytemp + ")"
            End If
        End If
        If TextBoxq1.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC05] like '%" + Trim(TextBoxq1.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC05] like '%" + Trim(TextBoxq1.Text) + "%')"
            End If
            Session("TextBoxq1") = TextBoxq1.Text
        End If
        If TextBoxq2.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC02] like '%" + Trim(TextBoxq2.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC02] like '%" + Trim(TextBoxq2.Text) + "%')"
            End If
            Session("TextBoxq2") = TextBoxq2.Text
        End If
        If TextBoxq3.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC03] like '%" + Trim(TextBoxq3.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC03] like '%" + Trim(TextBoxq3.Text) + "%')"
            End If
            Session("TextBoxq3") = TextBoxq3.Text
        End If
        If TextBoxq4.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC13] like '%" + Trim(TextBoxq4.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC13] like '%" + Trim(TextBoxq4.Text) + "%')"
            End If
            Session("TextBoxq4") = TextBoxq4.Text
        End If
        If TextBoxq5.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC21] like '%" + Trim(TextBoxq5.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC21] like '%" + Trim(TextBoxq5.Text) + "%')"
            End If
            Session("TextBoxq5") = TextBoxq5.Text
        End If
        If TextBoxq6.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC22] like '%" + Trim(TextBoxq6.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC22] like '%" + Trim(TextBoxq6.Text) + "%')"
            End If
            Session("TextBoxq6") = TextBoxq6.Text
        End If
        If TextBoxq11.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC45] like '%" + Trim(TextBoxq11.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC45] like '%" + Trim(TextBoxq11.Text) + "%')"
            End If
            Session("TextBoxq11") = TextBoxq11.Text
        End If
        If TextBoxq12.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC24] like '%" + Trim(TextBoxq12.Text) + "%')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC24] like '%" + Trim(TextBoxq12.Text) + "%')"
            End If
            Session("TextBoxq12") = TextBoxq12.Text
        End If
        If TextBoxq7.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC38] >= '" + Trim(TextBoxq7.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC38] >= '" + Trim(TextBoxq7.Text) + "')"
            End If
            Session("TextBoxq7") = TextBoxq7.Text
        End If
        If TextBoxq8.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC38] <= '" + Trim(TextBoxq8.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC38] <= '" + Trim(TextBoxq8.Text) + "')"
            End If
            Session("TextBoxq8") = TextBoxq8.Text
        End If
        If TextBoxq9.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC51] >= '" + Trim(TextBoxq9.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC51] >= '" + Trim(TextBoxq9.Text) + "')"
            End If
            Session("TextBoxq9") = TextBoxq9.Text
        End If
        If TextBoxq10.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC51] <= '" + Trim(TextBoxq10.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC51] <= '" + Trim(TextBoxq10.Text) + "')"
            End If
            Session("TextBoxq10") = TextBoxq10.Text
        End If
        If TextBoxq13.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC58] >= '" + Trim(TextBoxq13.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC58] >= '" + Trim(TextBoxq13.Text) + "')"
            End If
            Session("TextBoxq13") = TextBoxq13.Text
        End If
        If TextBoxq14.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC58] <= '" + Trim(TextBoxq14.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC58] <= '" + Trim(TextBoxq14.Text) + "')"
            End If
            Session("TextBoxq14") = TextBoxq14.Text
        End If
        If Dorplistq1.Text <> "" Then
            If swcquereytext.Text = "" Then
                swcquereytext.Text = "([SWC12] = '" + Trim(Dorplistq1.Text) + "')"
            Else
                swcquereytext.Text = swcquereytext.Text + " and ([SWC12] = '" + Trim(Dorplistq1.Text) + "')"
            End If
            Session("Dorplistq1") = Dorplistq1.Text
        End If
        Dim ttswcquereytext As String = swcquereytext.Text
        '以下處理地籍
        Dim havelandquery As Boolean = False
        If (Dorplistq3.Text <> "") Then
            havelandquery = True
            If (swcquereytext.Text.Length > 0) Then
                swcquereytext.Text = swcquereytext.Text + " and "
            End If
            swcquereytext.Text = swcquereytext.Text + " ([SWC08] like '%" + Dorplistq3.SelectedItem.Text + "%')"

            Session("Dorplistq3") = Dorplistq3.Text
        End If
        If (Dorplistq4.Text <> "") Then
            havelandquery = True
            If (swcquereytext.Text.Length > 0) Then
                swcquereytext.Text = swcquereytext.Text + " and "
            End If
            swcquereytext.Text = swcquereytext.Text + " ([SWC09] like '%" + Dorplistq4.Text + "%')"
            Session("Dorplistq4") = Dorplistq4.Text
        End If
        If (Dorplistq5.Text <> "") Then
            havelandquery = True
            If (swcquereytext.Text.Length > 0) Then
                swcquereytext.Text = swcquereytext.Text + " and "
            End If
            swcquereytext.Text = swcquereytext.Text + " ([SWC10] like '%" + Dorplistq5.Text + "%')"
            Session("Dorplistq5") = Dorplistq5.Text
        End If
        If (TextBoxq15.Text <> "") Then
            havelandquery = True
            If (swcquereytext.Text.Length > 0) Then
                swcquereytext.Text = swcquereytext.Text + " and "
            End If
            swcquereytext.Text = swcquereytext.Text + " ([SWC11] like '%;" + TextBoxq15.Text + ";%')"
            Session("TextBoxq15") = TextBoxq15.Text
        End If
        'Dim ttswcquereytext As String = swcquereytext.Text
        '有地籍查詢的話要多去對應新的地籍與案件對應表
        If havelandquery = True Then
            '查地籍案件對應表
            Dim conn As SqlConnection = New SqlConnection()
            Dim sqlcom As SqlCommand = New SqlCommand()
            Dim dr2 As SqlDataReader
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
            conn.Open()
            sqlcom.Connection = conn
            If TextBoxq15.Text <> "" Then
                'sqlcom.CommandText = "SELECT distinct 行政管理案件編號 FROM relationLand WHERE 區 like '%" + Dorplistq3.SelectedItem.Text + "%' and 段 like '%" + Dorplistq4.Text + "%' and 小段 like '%" + Dorplistq5.Text + "%' and 地號 = '" + TextBoxq15.Text + "' "
                sqlcom.CommandText = "SELECT distinct 行政管理案件編號 FROM relationLand WHERE 區 like '%" + Dorplistq3.SelectedItem.Text + "%' and 段 like '%" + Dorplistq4.Text + "%' and 小段 like '%" + Dorplistq5.Text + "%' and 地號 = '" + TextBoxq15.Text + "' "
            Else
                'sqlcom.CommandText = "SELECT distinct 行政管理案件編號 FROM relationLand WHERE 區 like '%" + Dorplistq3.SelectedItem.Text + "%' and 段 like '%" + Dorplistq4.Text + "%' and 小段 like '%" + Dorplistq5.Text + "%' "
                sqlcom.CommandText = "SELECT distinct 行政管理案件編號 FROM relationLand WHERE 區 like '%" + Dorplistq3.SelectedItem.Text + "%' and 段 like '%" + Dorplistq4.Text + "%' and 小段 like '%" + Dorplistq5.Text + "%' "
            End If

            dr2 = sqlcom.ExecuteReader()
            '寫入swcquerytext
            While (dr2.Read())
                If ttswcquereytext.Length > 0 Then
                    '看看這一筆合不合條件
                    If Trim(dr2(0).ToString) <> "" Then
                        Dim ttconn As SqlConnection = New SqlConnection()
                        Dim ttsqlcom As SqlCommand = New SqlCommand()
                        Dim ttdr2 As SqlDataReader
                        ttconn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
                        ttconn.Open()
                        ttsqlcom.Connection = ttconn
                        ttsqlcom.CommandText = "SELECT SWC02 FROM SWCSWC WHERE " + ttswcquereytext + " and [SWC02] = '" + Trim(dr2(0).ToString) + "'"
                        ttdr2 = ttsqlcom.ExecuteReader()
                        While ttdr2.Read
                            swcquereytext.Text = swcquereytext.Text + " or ([SWC02] = '" + Trim(dr2(0).ToString) + "')"
                            Exit While
                        End While
                        ttdr2.Close()
                        ttdr2.Dispose()
                        ttsqlcom.Dispose()
                        ttconn.Close()
                    End If
                Else
                    If swcquereytext.Text.Length > 0 Then
                        If Trim(dr2(0).ToString) <> "" Then
                            swcquereytext.Text = swcquereytext.Text + " or ([SWC02] = '" + Trim(dr2(0).ToString) + "')"
                        End If
                    Else
                        If Trim(dr2(0).ToString) <> "" Then
                            swcquereytext.Text = " ([SWC02] = '" + Trim(dr2(0).ToString) + "')"
                        End If
                    End If
                End If
                'If swcquereytext.Text.Length > 0 Then
                '    swcquereytext.Text = swcquereytext.Text + " or ([SWC02] = '" + Trim(dr2(0).ToString) + "')"
                'Else
                '    swcquereytext.Text = " ([SWC02] = '" + Trim(dr2(0).ToString) + "')"
                'End If

            End While
            dr2.Close()
            dr2.Dispose()
            sqlcom.Dispose()
            conn.Close()
        End If
        '地籍處理到這邊
        If swcquereytext.Text = "" Then
            SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] ORDER BY [SWC00]"
            Session("TextSwcquerey") = ""
        Else
            SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
            Session("TextSwcquerey") = swcquereytext.Text
        End If

        '寫入使用者紀錄
        swcdbwrite("query")
        SqlDataSource2.DataBind()
        GridView2.DataBind()
    End Sub

    Protected Sub btnlogout_Click(sender As Object, e As System.EventArgs) Handles btnlogout.Click
        Session.RemoveAll()
        Session.Abandon()
        Response.Redirect(ConfigurationManager.AppSettings("thisip") + "tslm/")
    End Sub

    Protected Sub btswc_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btswc.Click
        Response.Redirect("swc_104.aspx")
    End Sub

    Protected Sub btswcdp_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btswcdp.Click
        Response.Redirect("swcdp.aspx")
    End Sub

    Protected Sub btswcnn_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btswcnn.Click
        Response.Redirect("swcnn.aspx")
    End Sub

    Protected Sub btswcg_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btswcg.Click
        Response.Redirect("swcg.aspx")
    End Sub

    Protected Sub qButton2_Click(sender As Object, e As System.EventArgs) Handles qButton2.Click
        cleanquery()
        Session("lightps") = ""
        SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] ORDER BY [SWC00]"
        SqlDataSource2.DataBind()
        GridView2.DataBind()
    End Sub

    Sub cleanquery()
        TextBoxq1.Text = ""
        TextBoxq2.Text = ""
        TextBoxq3.Text = ""
        TextBoxq4.Text = ""
        TextBoxq5.Text = ""
        TextBoxq6.Text = ""
        TextBoxq7.Text = ""
        TextBoxq8.Text = ""
        TextBoxq9.Text = ""
        TextBoxq10.Text = ""
        TextBoxq11.Text = ""
        TextBoxq12.Text = ""
        TextBoxq13.Text = ""
        TextBoxq14.Text = ""
        TextBoxq15.Text = ""
        Dorplistq1.ClearSelection()
        Dorplistq1.Items(0).Selected = True
        Dorplistq3.ClearSelection()
        Dorplistq3.Items(0).Selected = True
        Dorplistq4.ClearSelection()
        Dorplistq4.Items.Clear()
        Dorplistq5.ClearSelection()
        Dorplistq5.Items.Clear()
        CheckBoxq1.Checked = "false"
        CheckBoxq2.Checked = "false"
        CheckBoxq3.Checked = "false"
        CheckBoxq4.Checked = "false"
        CheckBoxq5.Checked = "false"
        CheckBoxq6.Checked = "false"
        CheckBoxq7.Checked = "false"
        CheckBoxq8.Checked = "false"
        CheckBoxq9.Checked = "false"
        CheckBoxq10.Checked = "false"
        CheckBoxq11.Checked = "false"
        CheckBoxq12.Checked = "false"
        CheckBoxq13.Checked = "false"
        CheckBoxq14.Checked = "false"
        'listboxarea.Items.FindByValue("00").Selected = True
        'listboxsection.Dispose()
        swcquereytext.Text = ""
        RemoveSearchSession()

    End Sub

    Protected Sub GridView2_DataBound(sender As Object, e As System.EventArgs) Handles GridView2.DataBound

    End Sub

    Protected Sub GridView2_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView2.PageIndexChanging
        If swcquereytext.Text = "" Then
            SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] ORDER BY [SWC00]"
        Else
            SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
        End If
        SqlDataSource2.DataBind()
    End Sub

    Protected Sub GridView2_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "detail" Then
            Dim aa As Integer
            aa = GridView2.Rows(e.CommandArgument).RowIndex
            Response.Redirect("swc_104.aspx?Editmode=detail&SWC00=" + Trim(GridView2.Rows(aa).Cells(0).Text) + "&tempswc02=" + Trim(GridView2.Rows(aa).Cells(2).Text))
        End If
    End Sub

    Protected Sub GridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowCreated

    End Sub

    Protected Sub GridView2_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound
        If Session("uid") = "gv-admin" Then
            If e.Row.RowType = DataControlRowType.DataRow Then
                'GridView2.Columns.Item(12).Visible = True 
                GridView2.Columns.Item(12).Visible = True  '地籍藍未顯示的時候要加4 現在先不顯示所以減了4
            End If
        End If
    End Sub

    Protected Sub GridView2_RowDeleted(sender As Object, e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles GridView2.RowDeleted

    End Sub

    Protected Sub GridView2_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView2.RowDeleting

    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles icon_goback.Click
        Response.Redirect("swc_104.aspx?SearchType=1")
    End Sub

    Protected Sub btswcgm_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btswcgm.Click
        Response.Redirect("swcgdatem.aspx")
    End Sub

    Sub InitGVFrontendTypeSetting() '初始化GridView1 
        '初始劃地籍的GridView1
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        dt.Columns.Add(New DataColumn("區", GetType(System.String)))
        dt.Columns.Add(New DataColumn("段", GetType(System.String)))
        dt.Columns.Add(New DataColumn("小段", GetType(System.String)))
        dt.Columns.Add(New DataColumn("地號", GetType(System.String)))
        dt.Columns.Add(New DataColumn("土地使用分區", GetType(System.String)))
        dt.Columns.Add(New DataColumn("土地可利用限度", GetType(System.String)))
        dt.Columns.Add(New DataColumn("林地類別", GetType(System.String)))
        dt.Columns.Add(New DataColumn("地質敏感區", GetType(System.String)))
        ViewState("tbCategory") = dt
        'Bind the table to the gridview      
        GridView1.DataSource = dt
        GridView1.DataBind()
        '初始劃審查紀錄檔案附件的swccheckGridView
        Dim swcchreportdt As DataTable = New DataTable()
        swcchreportdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("審查次數", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("審查日期", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("補正期限", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("意見函檔", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("行政審查案件編號", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("修改人員", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("最後修改日期", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("備註", GetType(System.String)))
        swcchreportdt.Columns.Add(New DataColumn("新增修改已存檔", GetType(System.String)))
        ViewState("swcchreporttbCategory") = swcchreportdt
        'Bind the table to the gridview      
        swccheckGridView.DataSource = swcchreportdt
        swccheckGridView.DataBind()

        '初始劃受理檔案附件的CheckfileGridView
        Dim rcdt As DataTable = New DataTable()
        rcdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        rcdt.Columns.Add(New DataColumn("附件URL", GetType(System.String)))
        rcdt.Columns.Add(New DataColumn("附件檔名", GetType(System.String)))
        rcdt.Columns.Add(New DataColumn("附件路徑", GetType(System.String)))
        ViewState("rctbCategory") = rcdt
        'Bind the table to the gridview      
        receivefileGridView.DataSource = rcdt
        receivefileGridView.DataBind()
        '初始劃審查檔案附件的CheckfileGridView
        Dim cfdt As DataTable = New DataTable()
        cfdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        cfdt.Columns.Add(New DataColumn("附件URL", GetType(System.String)))
        cfdt.Columns.Add(New DataColumn("附件檔名", GetType(System.String)))
        cfdt.Columns.Add(New DataColumn("附件路徑", GetType(System.String)))
        ViewState("cftbCategory") = cfdt
        'Bind the table to the gridview      
        CheckfileGridView.DataSource = cfdt
        CheckfileGridView.DataBind()
        '初始劃施工中檔案附件的doGridView
        Dim dodt As DataTable = New DataTable()
        dodt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        dodt.Columns.Add(New DataColumn("附件URL", GetType(System.String)))
        dodt.Columns.Add(New DataColumn("附件檔名", GetType(System.String)))
        dodt.Columns.Add(New DataColumn("附件路徑", GetType(System.String)))
        ViewState("dotbCategory") = dodt
        'Bind the table to the gridview      
        doGridView.DataSource = dodt
        doGridView.DataBind()
        '初始劃已完工檔案附件的finishGridView
        Dim finishdt As DataTable = New DataTable()
        finishdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        finishdt.Columns.Add(New DataColumn("附件URL", GetType(System.String)))
        finishdt.Columns.Add(New DataColumn("附件檔名", GetType(System.String)))
        finishdt.Columns.Add(New DataColumn("附件路徑", GetType(System.String)))
        ViewState("finishtbCategory") = finishdt
        'Bind the table to the gridview      
        finishGridView.DataSource = finishdt
        finishGridView.DataBind()
        '初始劃其他檔案附件的elseGridView
        Dim elsedt As DataTable = New DataTable()
        elsedt.Columns.Add(New DataColumn("序號", GetType(System.String)))
        elsedt.Columns.Add(New DataColumn("附件URL", GetType(System.String)))
        elsedt.Columns.Add(New DataColumn("附件檔名", GetType(System.String)))
        elsedt.Columns.Add(New DataColumn("附件路徑", GetType(System.String)))
        ViewState("elsetbCategory") = elsedt
        'Bind the table to the gridview      
        elseGridView.DataSource = elsedt
        elseGridView.DataBind()
        '初始劃歷程紀錄的swchistoryGridView
        Dim swchistorydt As DataTable = New DataTable()
        swchistorydt.Columns.Add(New DataColumn("行政審查案件編號", GetType(System.String)))
        swchistorydt.Columns.Add(New DataColumn("行政審查案件編號URL", GetType(System.String)))
        ViewState("swchistorytbCategory") = swchistorydt
        'Bind the table to the gridview      
        SWChistroygridview.DataSource = swchistorydt
        SWChistroygridview.DataBind()

    End Sub


    Sub getswccheckdata()    '審查紀錄資料到入
        '第一、連結資料庫
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()

        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM SWCCheckreport where [SWCCheckreport05]='" + Trim(SWC02.Text) + "' order by SWCCheckreport02 ", conn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swccheckreport")

        '第三、執行SQL指令之後，把資料庫撈出來的結果，交由GridView1控制項來呈現。
        For i = 0 To ds.Tables("swccheckreport").Rows.Count - 1
            swcchreportlbno.Text = (Convert.ToInt16(swcchreportlbno.Text) + 1).ToString()
            Dim swcchreporttbCategory As DataTable = ViewState("swcchreporttbCategory")
            If swcchreporttbCategory Is Nothing Then
                Dim swcchreportdt As DataTable = New DataTable()
                swcchreportdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("審查次數", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("審查日期", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("補正期限", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("意見函檔", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("行政審查案件編號", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("修改人員", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("最後修改日期", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("備註", GetType(System.String)))
                swcchreportdt.Columns.Add(New DataColumn("新增修改已存檔", GetType(System.String)))
                ViewState("swcchreporttbCategory") = swcchreportdt
                swcchreporttbCategory = ViewState("swcchreporttbCategory")
            Else
                'Dim i As Integer
                'For i = 0 To tbCategory.Rows.Count - 1
                '    tbCategory.Rows(i)("序號") = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
                '    tbCategory.Rows(i)("區") = GridView1.Rows(i).Cells(1).Text
                '    tbCategory.Rows(i)("段") = GridView1.Rows(i).Cells(2).Text
                '    tbCategory.Rows(i)("小段") = GridView1.Rows(i).Cells(3).Text
                '    tbCategory.Rows(i)("地號") = GridView1.Rows(i).Cells(4).Text
                'Next i
            End If
            'Add new row
            swcchreporttbCategory.Rows.Add(swcchreporttbCategory.NewRow())
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("序號") = Convert.ToInt16(swcchreportlbno.Text)
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("審查次數") = ds.Tables("swccheckreport").Rows(i).Item(1).ToString
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("審查日期") = DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(2).ToString())).Year.ToString("0000") + "-" + DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(2).ToString())).Month.ToString("00") + "-" + DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(2).ToString())).Day.ToString("00")
            If DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(3).ToString())).Year.ToString("0000") <> "1900" Then
                swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("補正期限") = DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(3).ToString())).Year.ToString("0000") + "-" + DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(3).ToString())).Month.ToString("00") + "-" + DateValue(Trim(ds.Tables("swccheckreport").Rows(i).Item(3).ToString())).Day.ToString("00")
            Else
                swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("補正期限") = ""
            End If
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("意見函檔") = ds.Tables("swccheckreport").Rows(i).Item(4).ToString
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("行政審查案件編號") = ds.Tables("swccheckreport").Rows(i).Item(5).ToString
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("修改人員") = ds.Tables("swccheckreport").Rows(i).Item(6).ToString
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("最後修改日期") = ds.Tables("swccheckreport").Rows(i).Item(7).ToString
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("備註") = ds.Tables("swccheckreport").Rows(i).Item(8).ToString
            swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("新增修改已存檔") = ds.Tables("swccheckreport").Rows(i).Item(9).ToString
            ViewState("swcchreporttbCategory") = swcchreporttbCategory
            'Bind the table to the gridview
            swccheckGridView.DataSource = swcchreporttbCategory
            swccheckGridView.DataBind()

        Next


        '第四、關閉資料庫的連接與相關資源
        conn.Close()

    End Sub

    Sub getlanddata()    '地籍資料到入
        '第一、連結資料庫
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()

        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM relationLand where [行政管理案件編號]='" + Trim(SWC02.Text) + "'", conn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "land")

        '第三、執行SQL指令之後，把資料庫撈出來的結果，交由GridView1控制項來呈現。
        For i = 0 To ds.Tables("land").Rows.Count - 1
            lbno.Text = (Convert.ToInt16(lbno.Text) + 1).ToString()
            Dim tbCategory As DataTable = ViewState("tbCategory")
            If tbCategory Is Nothing Then
                Dim dt As DataTable = New DataTable()
                dt.Columns.Add(New DataColumn("序號", GetType(System.String)))
                dt.Columns.Add(New DataColumn("區", GetType(System.String)))
                dt.Columns.Add(New DataColumn("段", GetType(System.String)))
                dt.Columns.Add(New DataColumn("小段", GetType(System.String)))
                dt.Columns.Add(New DataColumn("地號", GetType(System.String)))
                dt.Columns.Add(New DataColumn("土地使用分區", GetType(System.String)))
                dt.Columns.Add(New DataColumn("土地可利用限度", GetType(System.String)))
                dt.Columns.Add(New DataColumn("林地類別", GetType(System.String)))
                dt.Columns.Add(New DataColumn("地質敏感區", GetType(System.String)))
                ViewState("tbCategory") = dt
                tbCategory = ViewState("tbCategory")
            Else
                'Dim i As Integer
                'For i = 0 To tbCategory.Rows.Count - 1
                '    tbCategory.Rows(i)("序號") = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
                '    tbCategory.Rows(i)("區") = GridView1.Rows(i).Cells(1).Text
                '    tbCategory.Rows(i)("段") = GridView1.Rows(i).Cells(2).Text
                '    tbCategory.Rows(i)("小段") = GridView1.Rows(i).Cells(3).Text
                '    tbCategory.Rows(i)("地號") = GridView1.Rows(i).Cells(4).Text
                'Next i
            End If
            'Add new row
            tbCategory.Rows.Add(tbCategory.NewRow())
            tbCategory.Rows(tbCategory.Rows.Count - 1)("序號") = Convert.ToInt16(lbno.Text)
            tbCategory.Rows(tbCategory.Rows.Count - 1)("區") = ds.Tables("land").Rows(i).Item(1).ToString
            tbCategory.Rows(tbCategory.Rows.Count - 1)("段") = ds.Tables("land").Rows(i).Item(2).ToString
            tbCategory.Rows(tbCategory.Rows.Count - 1)("小段") = ds.Tables("land").Rows(i).Item(3).ToString
            tbCategory.Rows(tbCategory.Rows.Count - 1)("地號") = ds.Tables("land").Rows(i).Item(5).ToString
            '以下的5要改，SQL還沒見這三個欄未
            tbCategory.Rows(tbCategory.Rows.Count - 1)("土地使用分區") = ds.Tables("land").Rows(i).Item(12).ToString
            tbCategory.Rows(tbCategory.Rows.Count - 1)("土地可利用限度") = ds.Tables("land").Rows(i).Item(13).ToString
            tbCategory.Rows(tbCategory.Rows.Count - 1)("林地類別") = ds.Tables("land").Rows(i).Item(14).ToString
            tbCategory.Rows(tbCategory.Rows.Count - 1)("地質敏感區") = ds.Tables("land").Rows(i).Item(15).ToString
            ViewState("tbCategory") = tbCategory
            'Bind the table to the gridview
            GridView1.DataSource = tbCategory
            GridView1.DataBind()
            LandAddlandno.Text = ""
        Next


        '第四、關閉資料庫的連接與相關資源
        conn.Close()

    End Sub

    Protected Sub LandAddarea_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LandAddarea.SelectedIndexChanged  '外部多筆地籍的下拉式選單區
        LandAddsection.Items.Clear()
        LandAddsection.Items.Add("")
        LandAddsubsection.Items.Clear()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        Dim KCNTS As String = ""
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "SELECT distinct KCNT FROM LAND WHERE AA46 ='" + LandAddarea.SelectedValue + "' order by KCNT"
        dr2 = sqlcom.ExecuteReader()
        While (dr2.Read())
            Dim listItemK As ListItem = New ListItem()
            listItemK.Text = dr2(0).ToString().Substring(0, 2)
            If Not (KCNTS = dr2(0).ToString().Substring(0, 2)) Then
                LandAddsection.Items.Add(listItemK)
                KCNTS = dr2(0).ToString().Substring(0, 2)

            End If
        End While
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        conn.Dispose()
    End Sub

    Protected Sub LandAddsection_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles LandAddsection.SelectedIndexChanged
        LandAddsubsection.Items.Clear()
        LandAddsubsection.Items.Add("")
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        Dim KCNTS As String = ""
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "select aa from(SELECT distinct SUBSTRING(KCNT, LEN(KCNT) - 2, 1)  as aa FROM LAND WHERE len(KCNT)>4 and AA46 ='" + LandAddarea.SelectedValue + "' and substring(KCNT,1,2)='" + LandAddsection.SelectedValue + "') as aa order by CHARINDEX(aa, '一,二,三,四,五,六,七,八,九,十')"
        dr2 = sqlcom.ExecuteReader()
        While (dr2.Read())
            KCNTS = dr2(0).ToString()
            Dim listItemK As ListItem = New ListItem()
            listItemK.Text = KCNTS
            LandAddsubsection.Items.Add(listItemK)
        End While
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        conn.Dispose()
    End Sub

    Protected Sub landaddok_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles landaddok.Click
        'Exit Sub
        If LandAddarea.Text = "" Or LandAddsection.Text = "" Or LandAddsubsection.Text = "" Or LandAddlandno.Text = "" Then
            MB("請輸入完整地段號")
            Exit Sub
        End If
        lbno.Text = (Convert.ToInt16(lbno.Text) + 1).ToString()
        Dim tbCategory As DataTable = ViewState("tbCategory")
        If tbCategory Is Nothing Then
            Dim dt As DataTable = New DataTable()
            dt.Columns.Add(New DataColumn("序號", GetType(System.String)))
            dt.Columns.Add(New DataColumn("區", GetType(System.String)))
            dt.Columns.Add(New DataColumn("段", GetType(System.String)))
            dt.Columns.Add(New DataColumn("小段", GetType(System.String)))
            dt.Columns.Add(New DataColumn("地號", GetType(System.String)))
            dt.Columns.Add(New DataColumn("土地使用分區", GetType(System.String)))
            dt.Columns.Add(New DataColumn("土地可利用限度", GetType(System.String)))
            dt.Columns.Add(New DataColumn("林地類別", GetType(System.String)))
            dt.Columns.Add(New DataColumn("地質敏感區", GetType(System.String)))
            ViewState("tbCategory") = dt
            tbCategory = ViewState("tbCategory")
        Else
            'Dim i As Integer
            'For i = 0 To tbCategory.Rows.Count - 1
            '    tbCategory.Rows(i)("序號") = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
            '    tbCategory.Rows(i)("區") = GridView1.Rows(i).Cells(1).Text
            '    tbCategory.Rows(i)("段") = GridView1.Rows(i).Cells(2).Text
            '    tbCategory.Rows(i)("小段") = GridView1.Rows(i).Cells(3).Text
            '    tbCategory.Rows(i)("地號") = GridView1.Rows(i).Cells(4).Text
            'Next i
        End If
        'Add new row
        tbCategory.Rows.Add(tbCategory.NewRow())
        tbCategory.Rows(tbCategory.Rows.Count - 1)("序號") = Convert.ToInt16(lbno.Text)
        tbCategory.Rows(tbCategory.Rows.Count - 1)("區") = LandAddarea.SelectedItem.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("段") = LandAddsection.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("小段") = LandAddsubsection.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("地號") = LandAddlandno.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("土地使用分區") = Landuse.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("土地可利用限度") = Landlimite.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("林地類別") = Landforest.Text
        tbCategory.Rows(tbCategory.Rows.Count - 1)("地質敏感區") = Landsensative.Text
        ViewState("tbCategory") = tbCategory
        'Bind the table to the gridview
        GridView1.DataSource = tbCategory
        GridView1.DataBind()
        LandAddlandno.Text = ""

    End Sub

    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging
        GridView1.PageIndex = e.NewPageIndex
        Dim tbCategory As DataTable = ViewState("tbCategory")
        GridView1.DataSource = tbCategory
        GridView1.DataBind()
    End Sub


    Protected Sub icon_editcase_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles icon_editcase.Click
        'MB("aa")
        If Request("tempswc02") = "" Then
            Response.Redirect("swc_104.aspx?Editmode=edit&SWC00=" + Request("SWC00") + "&tempswc02=" + Trim(SWC02.Text))
        Else
            Response.Redirect("swc_104.aspx?Editmode=edit&SWC00=" + Request("SWC00") + "&tempswc02=" + Request("tempswc02"))
        End If

    End Sub

    Protected Sub GridView1_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GridView1.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = GridView1.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(GridView1.Rows(aa).Cells(0).Text)
                Dim tbCategory As DataTable = ViewState("tbCategory")
                Dim i As Integer
                For i = 0 To (Int(tbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(tbCategory.Rows(i)("序號")) = number) Then
                        tbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("tbCategory") = tbCategory
                GridView1.DataSource = tbCategory
                GridView1.DataBind()
        End Select
    End Sub

    Protected Sub GridView1_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound
        If Request("EditMode") = "detail" Then
            'GridView1.Columns.Item(5).Visible = False
            'If e.Row.RowType = DataControlRowType.DataRow Then
            '    GridView1.Columns.Item(5).Visible = False
            'Else
            '    GridView1.Columns.Item(5).Visible = True
            'End If
        Else
            'GridView1.Columns.Item(5).Visible = True
        End If
    End Sub

    Protected Sub icon_newcase_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles icon_newcase.Click
        Response.Redirect("swc_104.aspx?Editmode=addnew&SWC00=SWC" + Now.Date.ToString("yyyyMMdd") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00"))
        'Response.Redirect("swc_104.aspx?Editmode=addnew")
    End Sub

    Sub swcdbwrite(ByVal dowhat As String)
        '資料庫開啟
        Dim swcconnstringsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        Dim swccommand As SqlCommand
        Dim commandstring As String
        '開啟資料庫
        swcconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        swcconn = New SqlConnection(swcconnstringsetting.ConnectionString)
        swcconn.Open()
        '寫入目前登入的人
        Dim nowstring As String = Now.ToString("yyyy/MM/dd HH:mm:ss")
        commandstring = ""
        '取得IP
        Dim ClientIP As String = Request.ServerVariables("REMOTE_ADDR")
        Select Case dowhat
            Case "Addnew"
                commandstring = "Insert Into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_新增案件_" + Request("SWC00") + ", " + SWC02.Text + "', '0','0')"
            Case "Update"
                commandstring = "insert into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_更新案件_" + Request("SWC00") + ", " + SWC02.Text + "', '0','0') "
            Case "detail"
                commandstring = "insert into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_案件詳情_" + Request("SWC00") + ", " + SWC02.Text + "', '0','0') "
            Case "login"
                commandstring = "insert into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_使用者登入', '0','0') "
            Case "delfile"
                commandstring = "insert into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_刪除檔案_" + Request("SWC00") + ", " + Session("delfilename") + "', '0','0') "
                Session("delfilename") = ""
            Case "uploadfile"
                commandstring = "insert into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_上傳檔案_" + Request("SWC00") + ", " + Session("uploadfilename") + "', '0','0') "
                Session("uploadfilename") = ""
            Case "query"
                Dim qq As String = Left(swcquereytext.Text, 220)
                qq = qq.Replace("""", "_")
                qq = qq.Replace("'", "_")
                commandstring = "insert into swcuserlog  ( 來源位置, 事件時間, 使用網頁, 發生事件 , x, y) Values ('" + Trim(ClientIP) + "', '" + nowstring + "', '合法案件頁面', '" + Session("name").ToString() + "_查詢_" + qq + "', '0','0') "
        End Select
        swccommand = New SqlCommand(commandstring, swcconn)
        swccommand.ExecuteNonQuery()
        swccommand.Dispose()
        'write a copy to tempuserlog
        commandstring = commandstring.Replace("swcuserlog", "tempuserlog")
        swccommand = New SqlCommand(commandstring, swcconn)
        swccommand.ExecuteNonQuery()
        swccommand.Dispose()
        swcconn.Close()
        swcconn.Dispose()

    End Sub

    Sub getswcyearcount()
        If SWC01.Text <> "" Then
            Exit Sub
        End If
        If SWC04.Text = "" Or SWC04.Text = "退補件" Or SWC04.Text = "不予受理" Or SWC04.Text = "受理中" Or SWC04.Text = "審查中" Or SWC04.Text = "撤銷" Or SWC04.Text = "不予核定" Then
            '已核定之前不給核定編號
            Exit Sub
        End If
        If SWC38.Text = "" Then
            '還沒填核定日期
            MB("核定日期請務必填登，以利核定編號產生")
            Exit Sub
        End If
        '取得SWC年度數量到這邊
        'Dim FileYear As Integer = Convert.ToInt16(SWC02.Text.Substring(4, 3))
        'Dim FileYear As Integer = Convert.ToInt16(Now.Year.ToString)

        '改成用核定日期的年去換算民國
        Dim FileYear As Integer = Convert.ToInt16(DateValue(SWC38.Text).Year.ToString)
        Dim FileYearS As String = FileYear.ToString()
        Dim FileYearSS As String = Trim((FileYear - 1911).ToString)
        Dim landconnstringsetting As ConnectionStringSettings
        Dim landconn As SqlConnection
        Dim landcommand As SqlCommand
        Dim landreader As SqlDataReader
        Dim selectstring As String
        landconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        landconn = New SqlConnection(landconnstringsetting.ConnectionString)
        landconn.Open()
        'selectstring = "select count(""SWC02"") from [SWCSWC] where [SWC02] like '____" + FileYearSS + "%'"
        'landcommand = New SqlCommand(selectstring, landconn)
        'landreader = landcommand.ExecuteReader()
        'Dim listitemtemp As Integer
        'listitemtemp = ""
        'While landreader.Read
        '    listitemtemp = Convert.ToInt32(landreader.Item(0) + 1)
        'End While
        'selectstring = "select [SWC01] from [SWCSWC] where [SWC02] like '____" + FileYearSS + "%' and [SWC01] <> ''"
        If SWC07.Text = "簡易水保" Then
            selectstring = "select [SWC01] from [SWCSWC] where [SWC01] like 'A" + FileYearSS + "%'"
        End If
        If SWC07.Text = "水土保持計畫" Then
            selectstring = "select [SWC01] from [SWCSWC] where [SWC01] like '" + FileYearSS + "%'"
        End If
        landcommand = New SqlCommand(selectstring, landconn)
        landreader = landcommand.ExecuteReader()
        Dim listitemtemp As Integer
        listitemtemp = 0
        Dim tempcount As Integer
        While landreader.Read
            Try
                tempcount = Convert.ToInt32(Right(landreader.Item(0), 2))
                If listitemtemp < tempcount Then
                    listitemtemp = tempcount
                End If
            Catch ex As Exception

            End Try
        End While
        landreader.Close()
        landcommand.Dispose()
        landconn.Close()
        landconn.Dispose()
        If listitemtemp = 99 Then
            MB("年度案件已超過99筆，請洽主管與系統商，謝謝!!")
        Else
            If SWC07.Text = "簡易水保" Then
                SWC01.Text = "A" + FileYearSS + "-" + Trim((listitemtemp + 1).ToString("00"))
            Else
                SWC01.Text = FileYearSS + "-" + Trim((listitemtemp + 1).ToString("00"))
            End If
        End If
        '取得年度數量到這邊
    End Sub

    Function checkform() As String
        Dim result As String = ""
        Dim dateSWC37 As Date = DateValue("1900-01-01")
        Dim dateSWC38 As Date = DateValue("1900-01-01")
        Dim dateSWC42 As Date = DateValue("1900-01-01")
        Dim dateSWC43 As Date = DateValue("1900-01-01")
        Dim dateSWC51 As Date = DateValue("1900-01-01")
        Dim dateSWC52 As Date = DateValue("1900-01-01")
        Dim dateSWC53 As Date = DateValue("1900-01-01")
        Dim dateSWC55 As Date = DateValue("1900-01-01")
        Dim dateSWC57 As Date = DateValue("1900-01-01")
        Dim dateSWC58 As Date = DateValue("1900-01-01")
        Dim dateSWC59 As Date = DateValue("1900-01-01")
        'Try
        '    dateSWC37 = DateValue(SWC37.Text)
        'Catch ex As Exception

        'End Try
        Try
            dateSWC38 = DateValue(SWC38.Text)
        Catch ex As Exception

        End Try
        'Try
        '    dateSWC42 = DateValue(SWC42.Text)
        'Catch ex As Exception

        'End Try
        Try
            dateSWC43 = DateValue(SWC43.Text)
        Catch ex As Exception

        End Try
        Try
            dateSWC51 = DateValue(SWC51.Text)
        Catch ex As Exception

        End Try
        Try
            dateSWC52 = DateValue(SWC52.Text)
        Catch ex As Exception

        End Try
        Try
            dateSWC53 = DateValue(SWC53.Text)
        Catch ex As Exception

        End Try
        'Try
        '    dateSWC55 = DateValue(SWC55.Text)
        'Catch ex As Exception

        'End Try
        'Try
        '    dateSWC57 = DateValue(SWC57.Text)
        'Catch ex As Exception

        'End Try
        Try
            dateSWC58 = DateValue(SWC58.Text)
        Catch ex As Exception

        End Try
        Try
            dateSWC59 = DateValue(SWC59.Text)
        Catch ex As Exception

        End Try
        If SWC02.Text = "" Then
            result = result + "行政審查案件編號請務必填登，謝謝!!<br />"
        End If
        If Len(SWC02.Text) < 12 Or SWC02.Text = "" Then
            result = result + "行政審查案件編號輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。<br />"
        Else
            Dim comparechar As String
            For i = 1 To Len(SWC02.Text)
                comparechar = Mid(SWC02.Text, i, 1)
                If i = 1 Or i = 2 Then
                    Select Case comparechar
                        Case "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "x", "Y", "Z"
                        Case Else
                            result = result + "行政審查案件編號輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。<br />"
                    End Select
                ElseIf i < 13 Or i > 13 Then
                    Select Case comparechar
                        Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
                        Case Else
                            result = result + "行政審查案件編號輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。<br />"
                    End Select
                ElseIf i = 13 Then
                    Select Case comparechar
                        Case "-"
                        Case Else
                            result = result + "行政審查案件編號輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。<br />"
                    End Select
                End If
            Next
        End If
        If SWC05.Text = "" Then
            result = result + "水土保持書件名稱請務必填登，謝謝!!<br />"
        End If
        If SWC07.Text = "" Then
            result = result + "水土保持申請書件類別請務必填登，謝謝!!<br />"
        End If
        If SWC13.Text = "" Then
            result = result + "義務人請務必填登，謝謝!!<br />"
        End If
        If SWC15.Text = "" Then
            result = result + "聯絡人請務必填登，謝謝!!<br />"
        End If
        If SWC16.Text = "" Then
            result = result + "聯絡人手機請務必填登，謝謝!!<br />"
        End If
        If SWC21.Text = "" Then
            result = result + "承辦技師請務必填登，謝謝!!<br />"
        End If
        If Not checklocation() Then
            result = result + "座標位置不在臺北市，請重新輸入，謝謝!!<br />"
        End If
        If SWC53.Text <> "" And SWC04.Text <> "停工中" Then
            result = result + "有停工日期，但是案件狀態並不是停工中，請檢查停工日期與案件狀態欄位，謝謝!!<br />"
        End If
        '===========================================================================================================================


        '若審查費核銷非勾選『無審查費』，審查費核銷日期應該核定日期當日或後
        If SWC3602.Checked = False Then
            If (dateSWC37 <> "1900-01-01" And dateSWC38 <> "1900-01-01") And (dateSWC37 < dateSWC38) Then
                result = result + "『審查費核銷日期』應於 " + SWC38.Text + " 後!!<br />"
            End If
        End If
        '若水土保持書件類別為簡易水保，填入核定日期或文號後，檢核保證金繳納的吳保證金是否勾選
        If SWC07.Text = "簡易水保" Then
            If dateSWC38 > "1912-12-30" Or SWC39.Text <> "" Then
                If Not SWC4102.Checked Then
                    result = result + "本案為簡易水保，『保證金繳納』應勾選『無保證金』!!<br />"
                End If
            End If
        End If
        '若水土保持書件類別為水保計畫，保證金繳納日期應在核定日期後
        'If SWC07.Text = "水保計畫" Then
        '    If (dateSWC42 <> "1900-01-01" And dateSWC38 <> "1900-01-01") And (dateSWC42 < dateSWC38) Then
        '        result = result + "『保證金繳納日期』應於 " + SWC38.Text + " 後!!<br />"
        '    End If
        'End If
        '若水土保持書件類別為簡易水保，施工許可證核發日期應在核定日期後
        If SWC07.Text = "簡易水保" And Now.Date > DateValue("2016-10-31") Then
            If (dateSWC43 <> "1900-01-01" And dateSWC38 <> "1900-01-01") And (dateSWC43 < dateSWC38) And (Len(SWC02.Text) = 12) Then
                result = result + "『施工許可證核發日期』應於 " + SWC38.Text + " 後!!<br />"
            End If
        End If
        '若水土保持書件類別為水保計畫，施工許可證核發日期應在核定日期與保證金繳納日期後
        If SWC07.Text = "水保計畫" And Now.Date > DateValue("2016-10-31") Then
            'If (dateSWC43 <> "1900-01-01" And dateSWC38 <> "1900-01-01" And dateSWC42 <> "1900-01-01") And (dateSWC43 < dateSWC38) And (dateSWC43 < dateSWC42) And (Len(SWC02.Text) = 12) Then
            '    result = result + "『施工許可證核發日期』應於 " + SWC38.Text + " 與 " + SWC42.Text + " 後!!<br />"
            'End If
            If (dateSWC43 <> "1900-01-01" And dateSWC38 <> "1900-01-01" And dateSWC42 <> "1900-01-01") And (dateSWC43 < dateSWC38) And (Len(SWC02.Text) = 12) Then
                result = result + "『施工許可證核發日期』應於 " + SWC38.Text + " 後!!<br />"
            End If
        End If
        ''施工許可證核發文號應為11碼數字
        ''從最後的號算回來
        'If SWC44.Text = "" Then
        'Else
        '    Dim comparechar As String = ""
        '    Dim comparelen As Integer = Len(SWC44.Text)
        '    Dim chari As Integer
        '    For chari = comparelen To 1 Step -1
        '        comparechar = Mid(SWC44.Text, chari, 1)
        '        If chari = comparelen Then
        '            '判斷最後一個文字
        '            If comparechar <> "號" Then
        '                '最後一個字不是"號"，代表填錯了
        '                result = result + "『施工許可證核發文號』，必須是11碼。<br />"
        '                Exit For
        '            End If
        '        ElseIf chari >= comparelen - 11 And chari <= comparelen - 1 Then
        '            '判斷這11碼是不是數字，記得是倒回來判斷的喔!!
        '            Select Case comparechar
        '                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        '                    '如果這11碼是數字的話就沒錯
        '                Case Else
        '                    If chari = comparelen - 1 Then
        '                        '判斷倒數第二個是不是"第"，也就是說預設值還沒填字號
        '                        If comparechar <> "第" Then
        '                            result = result + "『施工許可證核發文號』，必須是11碼。<br />"
        '                            Exit For
        '                        Else
        '                            Exit For
        '                        End If
        '                    Else
        '                        result = result + "『施工許可證核發文號』，必須是11碼。<br />"
        '                        Exit For
        '                    End If
        '            End Select
        '        ElseIf chari = comparelen - 12 Then
        '            If comparechar <> "第" Then
        '                result = result + "『施工許可證核發文號』，必須是11碼。<br />"
        '                Exit For
        '            End If
        '        End If
        '    Next
        'End If
        '開工日期應於施工許可證核發日期之後
        If (dateSWC43 <> "1900-01-01" And dateSWC51 <> "1900-01-01") And (dateSWC51 < dateSWC43.AddDays(-10)) Then
            'result = result + "『開工日期』應於 " + SWC43.Text + " 當日或後!!<br />"
            result = result + "『開工日期』應於 " + dateSWC43.AddDays(-10).ToString("yyyy-MM-dd") + " 當日或後!!<br />"

        End If
        '預定完工日期應於開工工日期之後
        If (dateSWC52 <> "1900-01-01" And dateSWC51 <> "1900-01-01") And (dateSWC52 < dateSWC51) Then
            result = result + "『預定日期』應於 " + SWC51.Text + " 後!!<br />"
        End If
        '停工日期應於開工工日期之後
        If (dateSWC53 <> "1900-01-01" And dateSWC51 <> "1900-01-01") And (dateSWC53 < dateSWC51) Then
            result = result + "『停工日期』應於 " + SWC51.Text + " 後!!<br />"
        End If
        '完工申報日期應於開工工日期之後
        If (dateSWC55 <> "1900-01-01" And dateSWC51 <> "1900-01-01") And (dateSWC55 < dateSWC51) Then
            result = result + "『完工申報日期』應於 " + SWC51.Text + " 後!!<br />"
        End If
        '完工日期應於開工工日期之後
        If (dateSWC58 <> "1900-01-01" And dateSWC51 <> "1900-01-01") And (dateSWC58 < dateSWC51) Then
            result = result + "『完工日期』應於 " + SWC51.Text + " 後!!<br />"
        End If
        '完工證明書核發日期應於完工日期之後
        If (dateSWC59 <> "1900-01-01" And dateSWC58 <> "1900-01-01") And (dateSWC59 < dateSWC58) Then
            result = result + "『完工證明書核發日期』應於 " + SWC58.Text + " 後!!<br />"
        End If
        ''完工證明書核發文號應為11碼數字
        ''從最後的號算回來
        'If SWC60.Text = "" Then
        'Else
        '    Dim comparechar As String = ""
        '    Dim comparelen As Integer = Len(SWC60.Text)
        '    Dim chari As Integer
        '    For chari = comparelen To 1 Step -1
        '        comparechar = Mid(SWC60.Text, chari, 1)
        '        If chari = comparelen Then
        '            '判斷最後一個文字
        '            If comparechar <> "號" Then
        '                '最後一個字不是"號"，代表填錯了
        '                result = result + "『完工證明書核發文號』，必須是11碼。<br />"
        '                Exit For
        '            End If
        '        ElseIf chari >= comparelen - 11 And chari <= comparelen - 1 Then
        '            '判斷這11碼是不是數字，記得是倒回來判斷的喔!!
        '            Select Case comparechar
        '                Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
        '                    '如果這11碼是數字的話就沒錯
        '                Case Else
        '                    If chari = comparelen - 1 Then
        '                        '判斷倒數第二個是不是"第"，也就是說預設值還沒填字號
        '                        If comparechar <> "第" Then
        '                            result = result + "『完工證明書核發文號』，必須是11碼。<br />"
        '                            Exit For
        '                        Else
        '                            Exit For
        '                        End If
        '                    Else
        '                        result = result + "『完工證明書核發文號』，必須是11碼。<br />"
        '                        Exit For
        '                    End If
        '            End Select
        '        ElseIf chari = comparelen - 12 Then
        '            If comparechar <> "第" Then
        '                result = result + "『完工證明書核發文號』，必須是11碼。<br />"
        '                Exit For
        '            End If
        '        End If
        '    Next
        'End If
        ''若水土保持書件類別為簡易水保，檢核保證金退還欄位的無保證金是不是在完工證明書核發日期或文號填登之後有確認勾選
        'If SWC07.Text = "簡易水保" Then
        '    If dateSWC59 > "1912-12-30" Or SWC60.Text <> "" Then
        '        If Not SWC5602.Checked Then
        '            result = result + "本案為簡易水保，『保證金退還』應勾選『無保證金』!!<br />"
        '        End If
        '    End If
        'End If
        ''若水土保持書件類別為水保計畫，檢核保證金退還日期應在完工日期後
        'If SWC07.Text = "水保計畫" Then
        '    If (dateSWC57 <> "1900-01-01" And dateSWC58 <> "1900-01-01") And (dateSWC57 < dateSWC58) Then
        '        result = result + "『保證金退還日期』應於 " + SWC58.Text + " 後!!<br />"
        '    End If
        'End If
        Return result
    End Function

    Protected Sub AddNewOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles AddNewOK.Click
        Dim checkformresult As String = checkform()
        If checkformresult <> "" Then
            MB(checkformresult)
            Exit Sub
        End If
        '寫使用者紀錄檔
        swcdbwrite("Addnew")
        '把資料寫入資料庫()
        Dim radok As Boolean
        Dim radindex As Integer
        Dim radobject As RadioButton
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "INSERT INTO [SWCSWC] ([SWC01] ,[SWC02] ,[SWC03] ,[SWC04] ,[SWC05] ,[SWC06] ,[SWC07] ,[SWC08] ,[SWC09] ,[SWC10] ,[SWC11] ,[SWC12] ,[SWC13] ,[SWC14] ,[SWC15] ,[SWC16] ,[SWC17] ,[SWC18] ,[SWC19] ,[SWC20] ,[SWC21] ,[SWC22] ,[SWC23] ,[SWC24] ,[SWC25] ,[SWC26] ,[SWC27] ,[SWC28] ,[SWC29] ,[SWC30] ,[SWC31] ,[SWC32] ,[SWC33] ,[SWC34] ,[SWC35] ,[SWC36] ,[SWC37] ,[SWC38] ,[SWC39] ,[SWC40] ,[SWC41] ,[SWC42] ,[SWC43] ,[SWC44] ,[SWC45] ,[SWC46] ,[SWC47] ,[SWC48] ,[SWC49] ,[SWC50] ,[SWC51] ,[SWC52] ,[SWC53] ,[SWC54] ,[SWC55] ,[SWC56] ,[SWC57] ,[SWC58] ,[SWC59] ,[SWC60] ,[SWC61] ,[SWC62] ,[SWC63] ,[SWC64] ,[SWC65] ,[SWC66] ,[SWC67] ,[SWC68] ,[SWC69] ,[SWC70] ,[SWC71] ,[SWC72] ,[SWC73] ,[SWC74] ,[SWC75] ,[SWC76] ,[SWC77] ,[SWC78] ,[SWC79] ,[SWC80] ,[SWC81] ,[SWC00] ,[SWC82] ,[SWC83] ,[SWC84] ,[SWC85] ,[SWC86] ,[SWC87] ,[SWC88] ,[SWC89] ,[SWC90] ,[SWC91] ,[SWC92] ,[SWC93] ,[SWC94] ,[SWC95] ,[SWC96] ,[SWC97] ,[SWC98] ,[SWC99]  ,[X] ,[Y]) VALUES (@SWC01 ,@SWC02 ,@SWC03 ,@SWC04 ,@SWC05 ,@SWC06 ,@SWC07 ,@SWC08 ,@SWC09 ,@SWC10 ,@SWC11 ,@SWC12 ,@SWC13 ,@SWC14 ,@SWC15 ,@SWC16 ,@SWC17 ,@SWC18 ,@SWC19 ,@SWC20 ,@SWC21 ,@SWC22 ,@SWC23 ,@SWC24 ,@SWC25 ,@SWC26 ,@SWC27 ,@SWC28 ,@SWC29 ,@SWC30 ,@SWC31 ,@SWC32 ,@SWC33 ,@SWC34 ,@SWC35 ,@SWC36 ,@SWC37 ,@SWC38 ,@SWC39 ,@SWC40 ,@SWC41 ,@SWC42 ,@SWC43 ,@SWC44 ,@SWC45 ,@SWC46 ,@SWC47 ,@SWC48 ,@SWC49 ,@SWC50 ,@SWC51 ,@SWC52 ,@SWC53 ,@SWC54 ,@SWC55 ,@SWC56 ,@SWC57 ,@SWC58 ,@SWC59 ,@SWC60 ,@SWC61 ,@SWC62 ,@SWC63 ,@SWC64 ,@SWC65 ,@SWC66 ,@SWC67 ,@SWC68 ,@SWC69 ,@SWC70 ,@SWC71 ,@SWC72 ,@SWC73 ,@SWC74 ,@SWC75 ,@SWC76 ,@SWC77 ,@SWC78 ,@SWC79 ,@SWC80 ,@SWC81 ,@SWC00, @SWC82 ,@SWC83 ,@SWC84 ,@SWC85 ,@SWC86 ,@SWC87 ,@SWC88 ,@SWC89 ,@SWC90 ,@SWC91 ,@SWC92 ,@SWC93 ,@SWC94 ,@SWC95 ,@SWC96 ,@SWC97 ,@SWC98 ,@SWC99  ,@X ,@Y)"
        '取得SWC年度數量到這邊
        getswcyearcount()
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@SWC01", SWC01.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC02", SWC02.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC03", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC04", SWC04.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC05", SWC05.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC06", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC07", SWC07.Text))
        If SWC08.Text = "" Then
            Dim aatbCategory As DataTable = ViewState("tbCategory")
            Try
                SWC08.Text = Trim(aatbCategory.Rows(0)("區"))
            Catch ex As Exception

            End Try
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC08", SWC08.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC09", SWC09.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC10", SWC10.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC11", SWC11.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC12", SWC12.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC13", SWC13.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC14", SWC14.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC15", SWC15.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC16", SWC16.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC17", SWC17.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC18", SWC18.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC19", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC20", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC21", SWC21.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC22", SWC22.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC23", SWC23.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC24", SWC24.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC25", SWC25.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC26", SWC26.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC27", SWC27.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC28", SWC28.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC29", SWC29.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC30", SWC30.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC31", SWC31.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC32", SWC32.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC33", SWC33.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC34", SWC34.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC35", SWC35.Text))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC36", ""))
        Do While radok
            Try
                radobject = FindControl("SWC36" + Trim(radindex.ToString("00")))
                If radobject.Checked Then
                    sqlcom.Parameters.RemoveAt(35)
                    sqlcom.Parameters.Add(New SqlParameter("@SWC36", radobject.Text))
                    Exit Do
                End If
                radindex = radindex + 1
            Catch ex As Exception
                radok = False
            End Try
        Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC37", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC38", SWC38.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC39", SWC39.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC40", SWC40.Text))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC41", ""))
        Do While radok
            Try
                radobject = FindControl("SWC41" + Trim(radindex.ToString("00")))
                If radobject.Checked Then
                    sqlcom.Parameters.RemoveAt(40)
                    sqlcom.Parameters.Add(New SqlParameter("@SWC41", radobject.Text))
                    Exit Do
                End If
                radindex = radindex + 1
            Catch ex As Exception
                radok = False
            End Try
        Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC42", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC43", SWC43.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC44", SWC44.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC45", SWC45.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC46", SWC46.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC47", SWC47.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC48", SWC48.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC49", SWC49.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC50", SWC50.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC51", SWC51.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC52", SWC52.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC53", SWC53.Text))
        'sqlcom.Parameters.Add(New SqlParameter("@SWC54", SWC54.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC54", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC55", ""))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC56", ""))
        Do While radok
            Try
                radobject = FindControl("SWC56" + Trim(radindex.ToString("00")))
                If radobject.Checked Then
                    sqlcom.Parameters.RemoveAt(55)
                    sqlcom.Parameters.Add(New SqlParameter("@SWC56", radobject.Text))
                    Exit Do
                End If
                radindex = radindex + 1
            Catch ex As Exception
                radok = False
            End Try
        Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC57", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC58", SWC58.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC59", SWC59.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC60", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC61", SWC61.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC62", SWC62.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC63", SWC63.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC64", SWC64.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC65", SWC65.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC66", SWC66.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC67", SWC67.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC68", SWC68.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC69", SWC69.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC70", SWC70.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC71", SWC71.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC72", SWC72.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC73", SWC73.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC74", SWC74.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC75", SWC75.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC76", SWC76.Text))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC77", ""))
        'Do While radok
        '    Try
        '        radobject = FindControl("SWC77" + Trim(radindex.ToString("00")))
        '        If radobject.Checked Then
        '            sqlcom.Parameters.RemoveAt(76)
        '            sqlcom.Parameters.Add(New SqlParameter("@SWC77", radobject.Text))
        '            Exit Do
        '        End If
        '        radindex = radindex + 1
        '    Catch ex As Exception
        '        radok = False
        '    End Try
        'Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC78", SWC78.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC79", SWC79.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC80", SWC80.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC81", SWC81.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC00", Request("SWC00")))
        If SWC82.Text = "" Then
            If SWC07.Text = "水土保持計畫" Then
                If SWC38.Text <> "" Then
                    Dim sdate As Date
                    If DateValue(SWC38.Text) > baseDate Then
                        sdate = DateValue(SWC38.Text).AddYears(3)
                    Else
                        sdate = DateValue(setdate)
                    End If

                    SWC82.Text = sdate.Year.ToString("0000") + "-" + sdate.Month.ToString("00") + "-" + sdate.Day.ToString("00")

                Else
                    SWC82.Text = "1900-01-01"
                End If
            End If
            If SWC07.Text = "簡易水保" Then
                If SWC38.Text <> "" Then
                    Dim sdate As Date
                    If DateValue(SWC38.Text) > baseDate Then
                        sdate = DateValue(SWC38.Text).AddYears(1)
                    Else
                        sdate = DateValue(setdate)
                    End If

                    SWC82.Text = sdate.Year.ToString("0000") + "-" + sdate.Month.ToString("00") + "-" + sdate.Day.ToString("00")

                Else
                    SWC82.Text = "1900-01-01"
                End If
            End If
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC82", SWC82.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC83", SWC83.Text))
        If SWC53.Text <> "" Then
            Dim sdate As Date = DateValue(SWC53.Text).AddYears(2)
            SWC84.Text = sdate.Year.ToString("0000") + "-" + sdate.Month.ToString("00") + "-" + sdate.Day.ToString("00")
        Else
            SWC84.Text = "1900-01-01"
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC84", SWC84.Text))
        Dim intx As Integer = 0
        If SWC27.Text = "" Then
            intx = 0
        Else
            intx = Convert.ToInt32(SWC27.Text)
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC85", SWC85.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC86", SWC86.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC87", SWC87.Text))
        If SWC88.Text = "" Then
            sqlcom.Parameters.Add(New SqlParameter("@SWC88", "1900-01-01"))
        Else
            sqlcom.Parameters.Add(New SqlParameter("@SWC88", SWC88.Text))
        End If
        If SWC89.Text = "" Then
            sqlcom.Parameters.Add(New SqlParameter("@SWC89", "1900-01-01"))
        Else
            sqlcom.Parameters.Add(New SqlParameter("@SWC89", SWC89.Text))
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC90", SWC90.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC91", SWC91.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC92", SWC92.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC93", SWC93.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC94", SWC94.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC95", SWC95.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC96", SWC96.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC97", SWC97.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC98", SWC98.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC99", SWC99.Text))
        sqlcom.Parameters.Add(New SqlParameter("@X", intx))
        Dim inty As Integer = 0
        If SWC28.Text = "" Then
            inty = 0
        Else
            inty = Convert.ToInt32(SWC28.Text)
        End If
        sqlcom.Parameters.Add(New SqlParameter("@Y", inty))
        '參數設定到這邊
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        conn.Close()
        conn.Dispose()

        '更新地籍
        updaterelateland()
        '更新審查公開資訊
        updatecheckopen()
        '比對SWC02案件編號有沒有修改
        compareSWC02()
        '寫入使用者紀錄
        swcdbwrite("Addnew")
        ''把違規案件的地籍資料寫入資料庫()
        'conn = New SqlConnection()
        'sqlcom = New SqlCommand()
        'conn.ConnectionString = ConfigurationManager.ConnectionStrings("relationlandconnstring").ConnectionString
        'conn.Open()

        ' ''先刪除既有的地籍資料 新增不用刪除，因為沒填過
        ' ''sqlcom.CommandText = "UPDATE [ILG] SET [ILG01] = @ILG01 ,[ILG02] = @ILG02 ,[ILG03] = @ILG03 ,[ILG04] = @ILG04 ,[ILG05] = @ILG05 ,[ILG06] = @ILG06 ,[ILG07] = @ILG07 ,[ILG08] = @ILG08 ,[ILG09] = @ILG09 ,[ILG10] = @ILG10 ,[ILG11] = @ILG11 ,[ILG12] = @ILG12 ,[ILG13] = @ILG13 ,[ILG14] = @ILG14 ,[ILG15] = @ILG15 ,[ILG16] = @ILG16 ,[ILG17] = @ILG17 ,[ILG18] = @ILG18 ,[ILG19] = @ILG19 ,[ILG20] = @ILG20 ,[ILG21] = @ILG21 ,[ILG22] = @ILG22 ,[ILG23] = @ILG23 ,[ILG24] = @ILG24 ,[ILG25] = @ILG25 ,[ILG26] = @ILG26 ,[ILG27] = @ILG27 ,[ILG28] = @ILG28 ,[ILG29] = @ILG29 ,[ILG30] = @ILG30 ,[ILG31] = @ILG31 ,[ILG32] = @ILG32 ,[ILG33] = @ILG33 ,[ILG34] = @ILG34 ,[ILG35] = @ILG35 ,[ILG36] = @ILG36 ,[ILG37] = @ILG37 ,[ILG00] = @ILG00 ,[ILGSTATUS] = @ILGSTATUS  WHERE [ILGindex]='" + Request("ilgforswcsnindex").ToString + "'"
        ''sqlcom.CommandText = "DELETE FROM [relationLand] WHERE [違規案件編號] = '" + Trim(TextBoxswc02.Text) + "'"
        ''sqlcom.ExecuteNonQuery()
        ''sqlcom.Cancel()
        'Dim tbCategory As DataTable = ViewState("tbCategory")
        'Dim i As Integer
        'For i = 0 To (Int(tbCategory.Rows.Count) - 1)
        '    sqlcom = New SqlCommand()
        '    sqlcom.Connection = conn
        '    '新增新的修正過的地籍資料
        '    sqlcom.CommandText = "INSERT INTO [relationLand] ([區] ,[段] ,[小段] ,[KCNT] ,[地號] ,[水保案件編號] ,[行政管理案件編號] ,[違規案件編號] ,[備註] ) VALUES (@區 ,@段 ,@小段 ,@KCNT ,@地號 ,@水保案件編號 ,@行政管理案件編號 ,@違規案件編號 ,@備註)"
        '    '參數設定從這邊開始
        '    sqlcom.Parameters.Add(New SqlParameter("@區", Trim(tbCategory.Rows(i)("區"))))
        '    sqlcom.Parameters.Add(New SqlParameter("@段", Trim(tbCategory.Rows(i)("段"))))
        '    sqlcom.Parameters.Add(New SqlParameter("@小段", Trim(tbCategory.Rows(i)("小段"))))
        '    sqlcom.Parameters.Add(New SqlParameter("@KCNT", Trim(tbCategory.Rows(i)("段")) + "段" + Trim(tbCategory.Rows(i)("小段") + "小段")))
        '    sqlcom.Parameters.Add(New SqlParameter("@地號", Trim(tbCategory.Rows(i)("地號"))))
        '    sqlcom.Parameters.Add(New SqlParameter("@水保案件編號", Trim(SWC01.Text)))
        '    sqlcom.Parameters.Add(New SqlParameter("@行政管理案件編號", Trim(SWC02.Text)))
        '    sqlcom.Parameters.Add(New SqlParameter("@違規案件編號", ""))
        '    sqlcom.Parameters.Add(New SqlParameter("@備註", ""))
        '    '參數設定到這邊
        '    sqlcom.ExecuteNonQuery()
        '    sqlcom.Cancel()
        '    sqlcom.Dispose()
        'Next i
        'conn.Close()
        'conn.Dispose()

        '回到詳細資料
        Response.Redirect("swc_104.aspx?Editmode=detail&SWC00=" + Request("SWC00") + "&tempswc02=" + SWC02.Text)
    End Sub

    Function checklocation() As Boolean
        If SWC27.Text = "" And SWC28.Text = "" Then
            Return True
        End If
        If Int(SWC27.Text) >= 295190 And Int(SWC27.Text) <= 317350 And Int(SWC28.Text) >= 2761450 And Int(SWC28.Text) <= 2789250 Then
            Return True
        End If
        Return False
    End Function

    Protected Sub UpdatecaseOK_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UpdatecaseOK.Click
        Dim checkformresult As String = checkform()
        If checkformresult <> "" Then
            MB(checkformresult)
            Exit Sub
        End If
        '寫使用者紀錄檔
        swcdbwrite("Update")
        '把資料寫入資料庫()
        Dim radok As Boolean
        Dim radindex As Integer
        Dim radobject As RadioButton
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        'sqlcom.CommandText = "INSERT INTO [swc] ([SWC01] ,[SWC02] ,[SWC03] ,[SWC04] ,[SWC05] ,[SWC06] ,[SWC07] ,[SWC08] ,[SWC09] ,[SWC10] ,[SWC11] ,[SWC12] ,[SWC13] ,[SWC14] ,[SWC15] ,[SWC16] ,[SWC17] ,[SWC18] ,[SWC19] ,[SWC20] ,[SWC21] ,[SWC22] ,[SWC23] ,[SWC24] ,[SWC25] ,[SWC26] ,[SWC27] ,[SWC28] ,[SWC29] ,[SWC30] ,[SWC31] ,[SWC32] ,[SWC33] ,[SWC34] ,[SWC35] ,[SWC36] ,[SWC37] ,[SWC38] ,[SWC39] ,[SWC40] ,[SWC41] ,[SWC42] ,[SWC43] ,[SWC44] ,[SWC45] ,[SWC46] ,[SWC47] ,[SWC48] ,[SWC49] ,[SWC50] ,[SWC51] ,[SWC52] ,[SWC53] ,[SWC54] ,[SWC55] ,[SWC56] ,[SWC57] ,[SWC58] ,[SWC59] ,[SWC60] ,[SWC61] ,[SWC62] ,[SWC63] ,[SWC64] ,[SWC65] ,[SWC66] ,[SWC67] ,[SWC68] ,[SWC69] ,[SWC70] ,[SWC71] ,[SWC72] ,[SWC73] ) VALUES (@SWC01 ,@SWC02 ,@SWC03 ,@SWC04 ,@SWC05 ,@SWC06 ,@SWC07 ,@SWC08 ,@SWC09 ,@SWC10 ,@SWC11 ,@SWC12 ,@SWC13 ,@SWC14 ,@SWC15 ,@SWC16 ,@SWC17 ,@SWC18 ,@SWC19 ,@SWC20 ,@SWC21 ,@SWC22 ,@SWC23 ,@SWC24 ,@SWC25 ,@SWC26 ,@SWC27 ,@SWC28 ,@SWC29 ,@SWC30 ,@SWC31 ,@SWC32 ,@SWC33 ,@SWC34 ,@SWC35 ,@SWC36 ,@SWC37 ,@SWC38 ,@SWC39 ,@SWC40 ,@SWC41 ,@SWC42 ,@SWC43 ,@SWC44 ,@SWC45 ,@SWC46 ,@SWC47 ,@SWC48 ,@SWC49 ,@SWC50 ,@SWC51 ,@SWC52 ,@SWC53 ,@SWC54 ,@SWC55 ,@SWC56 ,@SWC57 ,@SWC58 ,@SWC59 ,@SWC60 ,@SWC61 ,@SWC62 ,@SWC63 ,@SWC64 ,@SWC65 ,@SWC66 ,@SWC67 ,@SWC68 ,@SWC69 ,@SWC70 ,@SWC71 ,@SWC72 ,@SWC73)"
        sqlcom.CommandText = "UPDATE [SWCSWC] SET [SWC01] = @SWC01 ,[SWC02] = @SWC02 ,[SWC03] = @SWC03 ,[SWC04] = @SWC04 ,[SWC05] = @SWC05 ,[SWC06] = @SWC06 ,[SWC07] = @SWC07 ,[SWC08] = @SWC08 ,[SWC09] = @SWC09 ,[SWC10] = @SWC10 ,[SWC11] = @SWC11 ,[SWC12] = @SWC12 ,[SWC13] = @SWC13 ,[SWC14] = @SWC14 ,[SWC15] = @SWC15 ,[SWC16] = @SWC16 ,[SWC17] = @SWC17 ,[SWC18] = @SWC18 ,[SWC19] = @SWC19 ,[SWC20] = @SWC20 ,[SWC21] = @SWC21 ,[SWC22] = @SWC22 ,[SWC23] = @SWC23 ,[SWC24] = @SWC24 ,[SWC25] = @SWC25 ,[SWC26] = @SWC26 ,[SWC27] = @SWC27 ,[SWC28] = @SWC28 ,[SWC29] = @SWC29 ,[SWC30] = @SWC30 ,[SWC31] = @SWC31 ,[SWC32] = @SWC32 ,[SWC33] = @SWC33 ,[SWC34] = @SWC34 ,[SWC35] = @SWC35 ,[SWC36] = @SWC36 ,[SWC37] = @SWC37 ,[SWC38] = @SWC38 ,[SWC39] = @SWC39 ,[SWC40] = @SWC40 ,[SWC41] = @SWC41 ,[SWC42] = @SWC42 ,[SWC43] = @SWC43 ,[SWC44] = @SWC44 ,[SWC45] = @SWC45 ,[SWC46] = @SWC46 ,[SWC47] = @SWC47 ,[SWC48] = @SWC48 ,[SWC49] = @SWC49 ,[SWC50] = @SWC50 ,[SWC51] = @SWC51 ,[SWC52] = @SWC52 ,[SWC53] = @SWC53 ,[SWC54] = @SWC54 ,[SWC55] = @SWC55 ,[SWC56] = @SWC56 ,[SWC57] = @SWC57 ,[SWC58] = @SWC58 ,[SWC59] = @SWC59 ,[SWC60] = @SWC60 ,[SWC61] = @SWC61 ,[SWC62] = @SWC62 ,[SWC63] = @SWC63 ,[SWC64] = @SWC64 ,[SWC65] = @SWC65 ,[SWC66] = @SWC66 ,[SWC67] = @SWC67 ,[SWC68] = @SWC68 ,[SWC69] = @SWC69 ,[SWC70] = @SWC70 ,[SWC71] = @SWC71 ,[SWC72] = @SWC72 ,[SWC73] = @SWC73 ,[SWC74] = @SWC74 ,[SWC75] = @SWC75 ,[SWC76] = @SWC76 ,[SWC77] = @SWC77 ,[SWC78] = @SWC78 ,[SWC79] = @SWC79 ,[SWC80] = @SWC80 ,[SWC81]  = @SWC81 ,[SWC82] = @SWC82 ,[SWC83] = @SWC83 ,[SWC84]= @SWC84 ,[SWC85] = @SWC85 ,[SWC86] = @SWC86 ,[SWC87] = @SWC87 ,[SWC88] = @SWC88 ,[SWC89] = @SWC89 ,[SWC90] = @SWC90 ,[SWC91] = @SWC91 ,[SWC92] = @SWC92 ,[SWC93] = @SWC93 ,[SWC94] = @SWC94 ,[SWC95] = @SWC95 ,[SWC96] = @SWC96 ,[SWC97] = @SWC97 ,[SWC98] = @SWC98 ,[SWC99] = @SWC99 ,[X]= @X ,[Y]=@Y WHERE [SWC00]='" + Request("SWC00").ToString + "'"
        If Request("copy") = "copy" Then
            getswcyearcount()
        End If
        '取得SWC年度數量到這邊
        getswcyearcount()
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@SWC01", SWC01.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC02", SWC02.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC03", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC04", SWC04.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC05", SWC05.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC06", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC07", SWC07.Text))
        If SWC08.Text = "" Then
            Dim aatbCategory As DataTable = ViewState("tbCategory")
            Try
                SWC08.Text = Trim(aatbCategory.Rows(0)("區"))
            Catch ex As Exception

            End Try
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC08", SWC08.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC09", SWC09.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC10", SWC10.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC11", SWC11.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC12", SWC12.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC13", SWC13.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC14", SWC14.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC15", SWC15.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC16", SWC16.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC17", SWC17.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC18", SWC18.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC19", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC20", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC21", SWC21.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC22", SWC22.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC23", SWC23.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC24", SWC24.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC25", SWC25.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC26", SWC26.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC27", SWC27.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC28", SWC28.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC29", SWC29.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC30", SWC30.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC31", SWC31.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC32", SWC32.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC33", SWC33.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC34", SWC34.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC35", SWC35.Text))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC36", ""))
        Do While radok
            Try
                radobject = FindControl("SWC36" + Trim(radindex.ToString("00")))
                If radobject.Checked Then
                    sqlcom.Parameters.RemoveAt(35)
                    sqlcom.Parameters.Add(New SqlParameter("@SWC36", radobject.Text))
                    Exit Do
                End If
                radindex = radindex + 1
            Catch ex As Exception
                radok = False
            End Try
        Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC37", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC38", SWC38.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC39", SWC39.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC40", SWC40.Text))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC41", ""))
        Do While radok
            Try
                radobject = FindControl("SWC41" + Trim(radindex.ToString("00")))
                If radobject.Checked Then
                    sqlcom.Parameters.RemoveAt(40)
                    sqlcom.Parameters.Add(New SqlParameter("@SWC41", radobject.Text))
                    Exit Do
                End If
                radindex = radindex + 1
            Catch ex As Exception
                radok = False
            End Try
        Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC42", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC43", SWC43.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC44", SWC44.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC45", SWC45.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC46", SWC46.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC47", SWC47.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC48", SWC48.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC49", SWC49.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC50", SWC50.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC51", SWC51.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC52", SWC52.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC53", SWC53.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC54", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC55", ""))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC56", ""))
        Do While radok
            Try
                radobject = FindControl("SWC56" + Trim(radindex.ToString("00")))
                If radobject.Checked Then
                    sqlcom.Parameters.RemoveAt(55)
                    sqlcom.Parameters.Add(New SqlParameter("@SWC56", radobject.Text))
                    Exit Do
                End If
                radindex = radindex + 1
            Catch ex As Exception
                radok = False
            End Try
        Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC57", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC58", SWC58.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC59", SWC59.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC60", ""))
        sqlcom.Parameters.Add(New SqlParameter("@SWC61", SWC61.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC62", SWC62.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC63", SWC63.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC64", SWC64.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC65", SWC65.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC66", SWC66.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC67", SWC67.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC68", SWC68.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC69", SWC69.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC70", SWC70.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC71", SWC71.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC72", SWC72.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC73", SWC73.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC74", SWC74.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC75", SWC75.Checked))
        sqlcom.Parameters.Add(New SqlParameter("@SWC76", SWC76.Text))
        radok = True
        radindex = 1
        sqlcom.Parameters.Add(New SqlParameter("@SWC77", ""))
        'Do While radok
        '    Try
        '        radobject = FindControl("SWC77" + Trim(radindex.ToString("00")))
        '        If radobject.Checked Then
        '            sqlcom.Parameters.RemoveAt(76)
        '            sqlcom.Parameters.Add(New SqlParameter("@SWC77", radobject.Text))
        '            Exit Do
        '        End If
        '        radindex = radindex + 1
        '    Catch ex As Exception
        '        radok = False
        '    End Try
        'Loop
        sqlcom.Parameters.Add(New SqlParameter("@SWC78", SWC78.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC79", SWC79.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC80", SWC80.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC81", SWC81.Text))
        If SWC82.Text = "" Then
            If SWC07.Text = "水土保持計畫" Then
                If SWC38.Text <> "" Then
                    Dim sdate As Date
                    If DateValue(SWC38.Text) > baseDate Then
                        sdate = DateValue(SWC38.Text).AddYears(3)
                    Else
                        sdate = DateValue(setdate)
                    End If

                    SWC82.Text = sdate.Year.ToString("0000") + "-" + sdate.Month.ToString("00") + "-" + sdate.Day.ToString("00")

                Else
                    SWC82.Text = "1900-01-01"
                End If
            End If
            If SWC07.Text = "簡易水保" Then
                If SWC38.Text <> "" Then
                    Dim sdate As Date
                    If DateValue(SWC38.Text) > baseDate Then
                        sdate = DateValue(SWC38.Text).AddYears(1)
                    Else
                        sdate = DateValue(setdate)
                    End If

                    SWC82.Text = sdate.Year.ToString("0000") + "-" + sdate.Month.ToString("00") + "-" + sdate.Day.ToString("00")

                Else
                    SWC82.Text = "1900-01-01"
                End If
            End If
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC82", SWC82.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC83", SWC83.Text))
        If SWC53.Text <> "" Then
            Dim sdate As Date = DateValue(SWC53.Text).AddYears(2)
            SWC84.Text = sdate.Year.ToString("0000") + "-" + sdate.Month.ToString("00") + "-" + sdate.Day.ToString("00")
        Else
            SWC84.Text = "1900-01-01"
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC84", SWC84.Text))
        Dim intx As Integer = 0
        If SWC27.Text = "" Then
            intx = 0
        Else
            intx = Convert.ToInt32(SWC27.Text)
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC85", SWC85.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC86", SWC86.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC87", SWC87.Text))
        If SWC88.Text = "" Then
            sqlcom.Parameters.Add(New SqlParameter("@SWC88", "1900-01-01"))
        Else
            sqlcom.Parameters.Add(New SqlParameter("@SWC88", SWC88.Text))
        End If
        If SWC89.Text = "" Then
            sqlcom.Parameters.Add(New SqlParameter("@SWC89", "1900-01-01"))
        Else
            sqlcom.Parameters.Add(New SqlParameter("@SWC89", SWC89.Text))
        End If
        sqlcom.Parameters.Add(New SqlParameter("@SWC90", SWC90.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC91", SWC91.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC92", SWC92.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC93", SWC93.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC94", SWC94.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC95", SWC95.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC96", SWC96.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC97", SWC97.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC98", SWC98.Text))
        sqlcom.Parameters.Add(New SqlParameter("@SWC99", SWC99.Text))
        sqlcom.Parameters.Add(New SqlParameter("@X", intx))
        Dim inty As Integer = 0
        If SWC28.Text = "" Then
            inty = 0
        Else
            inty = Convert.ToInt32(SWC28.Text)
        End If
        sqlcom.Parameters.Add(New SqlParameter("@Y", inty))
        '參數設定到這邊
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        conn.Close()
        conn.Dispose()

        '更新地籍
        updaterelateland()
        '更新審查公開資訊
        updatecheckopen()
        '比對SWC02案件編號有沒有修改
        compareSWC02()

        '環景照片存入DB
        VrFile2DB()

        '寫入使用者紀錄
        swcdbwrite("Update")

        '回到詳細資料
        Response.Redirect("swc_104.aspx?Editmode=detail&SWC00=" + Request("SWC00") + "&tempswc02=" + SWC02.Text)
    End Sub

    Sub compareSWC02()
        'MB(SWC02.Text + " ; " + tempswc02.Text)
        Dim targetDirectorytemp As String
        Dim targetDirectory As String
        If SWC02.Text <> tempswc02.Text Then
            '資料夾更名
            '先判斷有資料夾嗎，有的話再更名
            Dim folderExists As Boolean
            Dim Filename As String = SWC02.Text.Substring(0, 12)
            Dim FileYear As Integer = Convert.ToInt16(Filename.Substring(4, 3))
            Dim FileYearS As String = FileYear.ToString()
            If (FileYear > 93) Then
                FileYearS = FileYearS + "年掃描圖檔"
            Else
                FileYearS = "93年度暨以前掃描圖檔"
            End If
            'Dim tempfolderExists As Boolean
            Dim tempFilename As String = tempswc02.Text.Substring(0, 12)
            Dim tempFileYear As Integer = Convert.ToInt16(tempFilename.Substring(4, 3))
            'MB(tempFileYear)
            Dim tempFileYearS As String = tempFileYear.ToString()
            'MB(tempFileYearS)
            If (tempFileYear > 93) Then
                tempFileYearS = tempFileYearS + "年掃描圖檔"
            Else
                tempFileYearS = "93年度暨以前掃描圖檔"
            End If

            folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
            If Not folderExists Then
                My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
            End If
            folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
            If Not folderExists Then
                My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
            End If
            folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
            If Not folderExists Then
                My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
            End If
            folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
            If Not folderExists Then
                My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
            End If
            'targetDirectorytemp = ConfigurationManager.AppSettings("swcpspath") + Trim(Convert.ToInt16(Now.Year.ToString("0000") - 1911).ToString("000")) + "年掃描圖檔\水保申請案件\水保計畫"
            targetDirectorytemp = ConfigurationManager.AppSettings("swcpspath") + tempFileYearS + "\水保申請案件\水保計畫"
            targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
            If (SWC07.Text = "簡易水保") Then
                'targetDirectorytemp = ConfigurationManager.AppSettings("swcpspath") + Trim(Convert.ToInt16(Now.Year.ToString("0000") - 1911).ToString("000")) + "年掃描圖檔\水保申請案件\簡易水保"
                targetDirectorytemp = ConfigurationManager.AppSettings("swcpspath") + tempFileYearS + "\水保申請案件\簡易水保"
                targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
            End If
            folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text)
            If Not folderExists Then
                '檔案夾不存在，建正式案件編號的資料夾
                My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text)
            End If
            Try
                'COPY資料夾
                'MB("copy從" + targetDirectorytemp + "\" + tempswc02.Text + " 到 " + targetDirectory + "\" + SWC02.Text)
                My.Computer.FileSystem.CopyDirectory(targetDirectorytemp + "\" + tempswc02.Text, targetDirectory + "\" + SWC02.Text)
                '刪除既有的資料夾
                'MB("del " + targetDirectorytemp + "\" + tempswc02.Text)
                My.Computer.FileSystem.DeleteDirectory(targetDirectorytemp + "\" + tempswc02.Text, FileIO.DeleteDirectoryOption.DeleteAllContents)
            Catch ex As Exception

            End Try
            '因為案件編號更動了，要把OPENSWC公開水保查詢的資料庫也要跟著動
            '先刪掉舊的，再重新新增一筆
            OPENSWC("del", tempswc02.Text)
            OPENSWC("update", SWC02.Text)
        Else
            '案件編號沒有動，把OPENSWC公開水保查詢的資料庫更新即可
            OPENSWC("update", SWC02.Text)
        End If
        If Request("tempswc02") = "" Then
            tempswc02.Text = SWC02.Text
        Else
            tempswc02.Text = Request("tempswc02")
        End If

    End Sub

    Sub OPENSWC(ByVal dowhat As String, ByVal SWC02TEXT As String)
        Select Case dowhat
            Case "del" '刪除這一筆資料
                Dim conn As SqlConnection = New SqlConnection()
                Dim sqlcom As SqlCommand = New SqlCommand()
                conn = New SqlConnection()
                sqlcom = New SqlCommand()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                '先刪除既有的地籍資料
                'sqlcom.CommandText = "UPDATE [ILG] SET [ILG01] = @ILG01 ,[ILG02] = @ILG02 ,[ILG03] = @ILG03 ,[ILG04] = @ILG04 ,[ILG05] = @ILG05 ,[ILG06] = @ILG06 ,[ILG07] = @ILG07 ,[ILG08] = @ILG08 ,[ILG09] = @ILG09 ,[ILG10] = @ILG10 ,[ILG11] = @ILG11 ,[ILG12] = @ILG12 ,[ILG13] = @ILG13 ,[ILG14] = @ILG14 ,[ILG15] = @ILG15 ,[ILG16] = @ILG16 ,[ILG17] = @ILG17 ,[ILG18] = @ILG18 ,[ILG19] = @ILG19 ,[ILG20] = @ILG20 ,[ILG21] = @ILG21 ,[ILG22] = @ILG22 ,[ILG23] = @ILG23 ,[ILG24] = @ILG24 ,[ILG25] = @ILG25 ,[ILG26] = @ILG26 ,[ILG27] = @ILG27 ,[ILG28] = @ILG28 ,[ILG29] = @ILG29 ,[ILG30] = @ILG30 ,[ILG31] = @ILG31 ,[ILG32] = @ILG32 ,[ILG33] = @ILG33 ,[ILG34] = @ILG34 ,[ILG35] = @ILG35 ,[ILG36] = @ILG36 ,[ILG37] = @ILG37 ,[ILG00] = @ILG00 ,[ILGSTATUS] = @ILGSTATUS  WHERE [ILGindex]='" + Request("ilgforswcsnindex").ToString + "'"
                sqlcom.CommandText = "DELETE FROM [OPENSWC] WHERE [OPENSWC02] = '" + Trim(SWC02TEXT) + "'"
                sqlcom.ExecuteNonQuery()
                sqlcom.Cancel()
            Case "update"   '看看有沒有這一筆資料，沒有的畫增加，有的畫修改
                '先檢查有沒有這一筆資料
                Dim conn As SqlConnection = New SqlConnection()
                Dim sqlcom As SqlCommand = New SqlCommand()
                Dim dr2 As SqlDataReader
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                sqlcom.CommandText = "select OPENSWC02 from OPENSWC where OPENSWC02='" + SWC02TEXT + "'"
                dr2 = sqlcom.ExecuteReader()
                Dim haverec As Boolean '是不是在公開的資料庫(OPENSWC)已經有這一筆資料
                If (dr2.Read()) Then
                    haverec = True
                Else
                    haverec = False
                End If
                dr2.Close()
                dr2.Dispose()
                sqlcom.Dispose()
                conn.Close()
                conn.Dispose()
                '以下要開始新增或是更新資料了
                conn = New SqlConnection()
                sqlcom = New SqlCommand()
                conn = New SqlConnection()
                sqlcom = New SqlCommand()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                sqlcom = New SqlCommand()
                sqlcom.Connection = conn
                If haverec Then
                    '更新
                    sqlcom.CommandText = "UPDATE [OPENSWC] SET [OPENSWC02]=@OPENSWC02 ,[OPENSWC05]=@OPENSWC05 ,[OPENSWC13]=@OPENSWC13 ,[OPENSWC21]=@OPENSWC21 ,[OPENSWC22]=@OPENSWC22 ,[OPENSWC45]=@OPENSWC45 ,[OPENSWC24]=@OPENSWC24 ,[OPENSWC80]=@OPENSWC80 ,[OPENSWCPATH]=@OPENSWCPATH ,[OPENSWC04]=@OPENSWC04 ,[OPENSWC25]=@OPENSWC25 WHERE [OPENSWC02]='" + SWC02TEXT + "'"
                Else
                    '新增
                    sqlcom.CommandText = "INSERT INTO [OPENSWC] ([OPENSWC02] ,[OPENSWC05] ,[OPENSWC13] ,[OPENSWC21] ,[OPENSWC22] ,[OPENSWC45] ,[OPENSWC24] ,[OPENSWC80] ,[OPENSWCPATH] ,[OPENSWC04] ,[OPENSWC25] ) VALUES (@OPENSWC02 ,@OPENSWC05 ,@OPENSWC13 ,@OPENSWC21 ,@OPENSWC22 ,@OPENSWC45 ,@OPENSWC24 ,@OPENSWC80 ,@OPENSWCPATH ,@OPENSWC04 ,@OPENSWC25)"
                End If
                '參數設定從這邊開始
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC02", SWC02.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC05", SWC05.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC13", SWC13.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC21", SWC21.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC22", SWC22.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC45", SWC45.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC24", SWC24.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC80", SWC80.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC04", SWC04.Text))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC25", SWC25.Text))
                '以下處理路徑
                Dim folderExists As Boolean
                Dim Filename As String = SWC02TEXT.Substring(0, 12)
                Dim FileYear As Integer = Convert.ToInt16(Filename.Substring(4, 3))
                Dim FileYearS As String = FileYear.ToString()
                Dim targetDirectory As String
                If (FileYear > 93) Then
                    FileYearS = FileYearS + "年掃描圖檔"
                Else
                    FileYearS = "93年度暨以前掃描圖檔"
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
                End If
                targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
                If (SWC07.Text = "簡易水保") Then
                    targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
                End If
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWCPATH", Right(targetDirectory, Len(targetDirectory) - Len(ConfigurationManager.AppSettings("swcpspath")))))
                sqlcom.ExecuteNonQuery()
                sqlcom.Cancel()
                sqlcom.Dispose()
                conn.Close()
                conn.Dispose()
        End Select
    End Sub


    Sub OPENSWCPS(ByVal SWC02TEXT As String)
        '看看有沒有這一筆資料，沒有的畫增加，有的畫修改
        '先檢查有沒有這一筆資料
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "select OPENSWC02 from OPENSWC where OPENSWC02='" + SWC02TEXT + "'"
        dr2 = sqlcom.ExecuteReader()
        Dim haverec As Boolean '是不是在公開的資料庫(OPENSWC)已經有這一筆資料
        If (dr2.Read()) Then
            haverec = True
        Else
            haverec = False
        End If
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        conn.Dispose()
        '以下要開始新增或是更新資料了
        conn = New SqlConnection()
        sqlcom = New SqlCommand()
        conn = New SqlConnection()
        sqlcom = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom = New SqlCommand()
        sqlcom.Connection = conn
        If haverec Then
            '更新
            sqlcom.CommandText = "UPDATE [OPENSWC] SET [OPENSWC02]=@OPENSWC02 ,[OPENSWCHAVECOPY]=@OPENSWCHAVECOPY ,[OPENSWCPATH]=@OPENSWCPATH WHERE [OPENSWC02]='" + SWC02TEXT + "'"
        Else
            '新增
            sqlcom.CommandText = "INSERT INTO [OPENSWC] ([OPENSWC02] ,[OPENSWCHAVECOPY] ,[OPENSWCPATH]) VALUES (@OPENSWC02 ,@OPENSWCHAVECOPY ,@OPENSWCPATH)"
        End If
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@OPENSWC02", SWC02.Text))
        sqlcom.Parameters.Add(New SqlParameter("@OPENSWCHAVECOPY", "0"))
        '以下處理路徑
        Dim folderExists As Boolean
        Dim Filename As String = SWC02TEXT.Substring(0, 12)
        Dim FileYear As Integer = Convert.ToInt16(Filename.Substring(4, 3))
        Dim FileYearS As String = FileYear.ToString()
        Dim targetDirectory As String
        If (FileYear > 93) Then
            FileYearS = FileYearS + "年掃描圖檔"
        Else
            FileYearS = "93年度暨以前掃描圖檔"
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
        End If
        targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
        If (SWC07.Text = "簡易水保") Then
            targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
        End If
        sqlcom.Parameters.Add(New SqlParameter("@OPENSWCPATH", Right(targetDirectory, Len(targetDirectory) - Len(ConfigurationManager.AppSettings("swcpspath")))))
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        sqlcom.Dispose()
        conn.Close()
        conn.Dispose()

    End Sub

    Sub updatecheckopen()
        '把審查紀錄公開化寫入資料庫()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn = New SqlConnection()
        sqlcom = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        '先刪除既有的地籍資料
        'sqlcom.CommandText = "UPDATE [ILG] SET [ILG01] = @ILG01 ,[ILG02] = @ILG02 ,[ILG03] = @ILG03 ,[ILG04] = @ILG04 ,[ILG05] = @ILG05 ,[ILG06] = @ILG06 ,[ILG07] = @ILG07 ,[ILG08] = @ILG08 ,[ILG09] = @ILG09 ,[ILG10] = @ILG10 ,[ILG11] = @ILG11 ,[ILG12] = @ILG12 ,[ILG13] = @ILG13 ,[ILG14] = @ILG14 ,[ILG15] = @ILG15 ,[ILG16] = @ILG16 ,[ILG17] = @ILG17 ,[ILG18] = @ILG18 ,[ILG19] = @ILG19 ,[ILG20] = @ILG20 ,[ILG21] = @ILG21 ,[ILG22] = @ILG22 ,[ILG23] = @ILG23 ,[ILG24] = @ILG24 ,[ILG25] = @ILG25 ,[ILG26] = @ILG26 ,[ILG27] = @ILG27 ,[ILG28] = @ILG28 ,[ILG29] = @ILG29 ,[ILG30] = @ILG30 ,[ILG31] = @ILG31 ,[ILG32] = @ILG32 ,[ILG33] = @ILG33 ,[ILG34] = @ILG34 ,[ILG35] = @ILG35 ,[ILG36] = @ILG36 ,[ILG37] = @ILG37 ,[ILG00] = @ILG00 ,[ILGSTATUS] = @ILGSTATUS  WHERE [ILGindex]='" + Request("ilgforswcsnindex").ToString + "'"
        sqlcom.CommandText = "DELETE FROM [SWCCheckreport] WHERE [SWCCheckreport05] = '" + Trim(SWC02.Text) + "'"
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()

        Dim swcchreporttbCategory As DataTable = ViewState("swcchreporttbCategory")
        Dim i As Integer
        For i = 0 To (Int(swcchreporttbCategory.Rows.Count) - 1)
            sqlcom = New SqlCommand()
            sqlcom.Connection = conn
            '新增新的修正過的地籍資料
            sqlcom.CommandText = "INSERT INTO [SWCCheckreport] ([SWCCheckreport01] ,[SWCCheckreport02] ,[SWCCheckreport03] ,[SWCCheckreport04] ,[SWCCheckreport05] ,[SWCCheckreport06] ,[SWCCheckreport07] ,[SWCCheckreport08] ,[SWCCheckreport09] ) VALUES (@SWCCheckreport01 ,@SWCCheckreport02 ,@SWCCheckreport03 ,@SWCCheckreport04 ,@SWCCheckreport05 ,@SWCCheckreport06 ,@SWCCheckreport07 ,@SWCCheckreport08 ,@SWCCheckreport09)"
            '參數設定從這邊開始
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport01", Trim(swcchreporttbCategory.Rows(i)("審查次數"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport02", Trim(swcchreporttbCategory.Rows(i)("審查日期"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport03", Trim(swcchreporttbCategory.Rows(i)("補正期限"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport04", Trim(swcchreporttbCategory.Rows(i)("意見函檔"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport05", Trim(tempswc02.Text)))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport06", Trim(swcchreporttbCategory.Rows(i)("修改人員"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport07", ""))
            'sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport07", Trim(swcchreporttbCategory.Rows(i)("最後修改日期"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport08", Trim(swcchreporttbCategory.Rows(i)("備註"))))
            sqlcom.Parameters.Add(New SqlParameter("@SWCCheckreport09", Trim(swcchreporttbCategory.Rows(i)("新增修改已存檔"))))
            '參數設定到這邊
            sqlcom.ExecuteNonQuery()
            sqlcom.Cancel()
            sqlcom.Dispose()
        Next i
        conn.Close()
        conn.Dispose()

    End Sub

    Sub updaterelateland()
        '把違規案件的地籍資料寫入資料庫()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn = New SqlConnection()
        sqlcom = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("relationlandconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        '先刪除既有的地籍資料
        'sqlcom.CommandText = "UPDATE [ILG] SET [ILG01] = @ILG01 ,[ILG02] = @ILG02 ,[ILG03] = @ILG03 ,[ILG04] = @ILG04 ,[ILG05] = @ILG05 ,[ILG06] = @ILG06 ,[ILG07] = @ILG07 ,[ILG08] = @ILG08 ,[ILG09] = @ILG09 ,[ILG10] = @ILG10 ,[ILG11] = @ILG11 ,[ILG12] = @ILG12 ,[ILG13] = @ILG13 ,[ILG14] = @ILG14 ,[ILG15] = @ILG15 ,[ILG16] = @ILG16 ,[ILG17] = @ILG17 ,[ILG18] = @ILG18 ,[ILG19] = @ILG19 ,[ILG20] = @ILG20 ,[ILG21] = @ILG21 ,[ILG22] = @ILG22 ,[ILG23] = @ILG23 ,[ILG24] = @ILG24 ,[ILG25] = @ILG25 ,[ILG26] = @ILG26 ,[ILG27] = @ILG27 ,[ILG28] = @ILG28 ,[ILG29] = @ILG29 ,[ILG30] = @ILG30 ,[ILG31] = @ILG31 ,[ILG32] = @ILG32 ,[ILG33] = @ILG33 ,[ILG34] = @ILG34 ,[ILG35] = @ILG35 ,[ILG36] = @ILG36 ,[ILG37] = @ILG37 ,[ILG00] = @ILG00 ,[ILGSTATUS] = @ILGSTATUS  WHERE [ILGindex]='" + Request("ilgforswcsnindex").ToString + "'"
        sqlcom.CommandText = "DELETE FROM [relationLand] WHERE [行政管理案件編號] = '" + Trim(tempswc02.Text) + "'"
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()

        Dim tbCategory As DataTable = ViewState("tbCategory")
        Dim i As Integer
        For i = 0 To (Int(tbCategory.Rows.Count) - 1)
            sqlcom = New SqlCommand()
            sqlcom.Connection = conn
            '新增新的修正過的地籍資料
            sqlcom.CommandText = "INSERT INTO [relationLand] ([區] ,[段] ,[小段] ,[KCNT] ,[地號] ,[水保案件編號] ,[行政管理案件編號] ,[違規案件編號] ,[備註] ,[土地權屬] ,[地目] ,[土地使用分區] ,[土地可利用限度] ,[林地類別] ,[地質敏感區] ) VALUES (@區 ,@段 ,@小段 ,@KCNT ,@地號 ,@水保案件編號 ,@行政管理案件編號 ,@違規案件編號 ,@備註 ,@土地權屬 ,@地目 ,@土地使用分區 ,@土地可利用限度 ,@林地類別 ,@地質敏感區)"
            '參數設定從這邊開始
            sqlcom.Parameters.Add(New SqlParameter("@區", Trim(tbCategory.Rows(i)("區"))))
            sqlcom.Parameters.Add(New SqlParameter("@段", Trim(tbCategory.Rows(i)("段"))))
            sqlcom.Parameters.Add(New SqlParameter("@小段", Trim(tbCategory.Rows(i)("小段"))))
            sqlcom.Parameters.Add(New SqlParameter("@KCNT", Trim(tbCategory.Rows(i)("段")) + "段" + Trim(tbCategory.Rows(i)("小段") + "小段")))
            sqlcom.Parameters.Add(New SqlParameter("@地號", Trim(tbCategory.Rows(i)("地號"))))
            sqlcom.Parameters.Add(New SqlParameter("@水保案件編號", Trim(SWC01.Text)))
            sqlcom.Parameters.Add(New SqlParameter("@行政管理案件編號", Trim(SWC02.Text)))
            sqlcom.Parameters.Add(New SqlParameter("@違規案件編號", ""))
            sqlcom.Parameters.Add(New SqlParameter("@備註", ""))
            sqlcom.Parameters.Add(New SqlParameter("@土地權屬", ""))
            sqlcom.Parameters.Add(New SqlParameter("@地目", ""))
            sqlcom.Parameters.Add(New SqlParameter("@土地使用分區", Trim(tbCategory.Rows(i)("土地使用分區"))))
            sqlcom.Parameters.Add(New SqlParameter("@土地可利用限度", Trim(tbCategory.Rows(i)("土地可利用限度"))))
            sqlcom.Parameters.Add(New SqlParameter("@林地類別", Trim(tbCategory.Rows(i)("林地類別"))))
            sqlcom.Parameters.Add(New SqlParameter("@地質敏感區", Trim(tbCategory.Rows(i)("地質敏感區"))))
            '參數設定到這邊
            sqlcom.ExecuteNonQuery()
            sqlcom.Cancel()
            sqlcom.Dispose()
        Next i
        conn.Close()
        conn.Dispose()

    End Sub


    Protected Sub GridView1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles GridView1.RowDeleting

    End Sub

    Protected Sub checkfileuploadok_Click(sender As Object, e As System.EventArgs) Handles receivefileuploadok.Click, checkfileuploadok.Click, dofileuploadok.Click, finishfileuploadok.Click, elsefileuploadok.Click, psfileuploadok.Click
        If Len(SWC02.Text) < 12 Or SWC02.Text = "" Then
            MB("行政審查案件編號未輸入，或輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。檔案無法上傳")
            Exit Sub
        End If
        If SWC07.Text = "" Then
            MB("書件類別請務必選擇")
            Exit Sub
        End If
        Dim filelistboxhere As Object
        filelistboxhere = ""
        Dim fileuploadhere As Object
        fileuploadhere = ""
        Dim tempfilename As String
        tempfilename = ""
        Dim saveflodername As String
        Dim savefilename As String
        Select Case sender.id
            Case "receivefileuploadok"  '受理的檔案上傳按鈕
                fileuploadhere = receivefileupload
                tempfilename = receivefileupload.FileName
                saveflodername = "受理"
                Session("uploadfilename") = "上傳受理中"
            Case "checkfileuploadok"  '審查的檔案上傳按鈕
                fileuploadhere = checkfileupload
                tempfilename = checkfileupload.FileName
                saveflodername = "審查"
                Session("uploadfilename") = "上傳審查中"
            Case "dofileuploadok"   '施工中的檔案上傳按鈕
                fileuploadhere = dofileupload
                tempfilename = dofileupload.FileName
                saveflodername = "施工中"
                Session("uploadfilename") = "上傳施工中"
            Case "finishfileuploadok"   '已完工檔案上傳按鈕
                fileuploadhere = finishfileupload
                tempfilename = finishfileupload.FileName
                saveflodername = "已完工"
                Session("uploadfilename") = "上傳已完工"
            Case "elsefileuploadok"   '其他檔案上傳按鈕
                fileuploadhere = elsefileupload
                tempfilename = elsefileupload.FileName
                saveflodername = "其他"
                Session("uploadfilename") = "上傳其他"
            Case "psfileuploadok"   '備註檔案上傳按鈕
                fileuploadhere = psfileupload
                tempfilename = psfileupload.FileName
                saveflodername = "備註"
                Session("uploadfilename") = "上傳備註"

        End Select

        '沒有選取檔案
        If Not fileuploadhere.hasfile Then
            MB("請選擇欲上傳的檔案。")
            Exit Sub
        End If
        '副檔名檢察
        If LCase(Right(fileuploadhere.FileName, 3)) <> "pdf" Then
            fileuploadhere.Dispose()
            MB("請選擇 PDF 檔案格式上傳，謝謝")
            Exit Sub
        End If
        '檔案大小檢察
        If fileuploadhere.FileBytes.Length > 52428800 Then
            fileuploadhere.Dispose()
            MB("請選擇 50Mb 以下檔案上傳，謝謝!!")
            Exit Sub
        End If
        Dim folderExists As Boolean
        Dim extname As String = Right(fileuploadhere.FileName, 4)
        Dim Filename As String = SWC02.Text.Substring(0, 12)
        Dim FileYear As Integer = Convert.ToInt16(Filename.Substring(4, 3))
        Dim FileYearS As String = FileYear.ToString()
        If (FileYear > 93) Then
            FileYearS = FileYearS + "年掃描圖檔"
        Else
            FileYearS = "93年度暨以前掃描圖檔"
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
        End If
        Dim targetDirectory As String = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
        If (SWC07.Text = "簡易水保") Then
            targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
        End If
        'folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text.Substring(0, 12))
        folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text)
        If Not folderExists Then
            'My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text.Substring(0, 12))
            My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text)
        End If

        'If saveflodername = "審查" Then
        '    If Len(SWC02.Text) > 12 And sender.id = "checkfileuploadok" Then
        '        Select Case SWC02.Text.Substring(13, 1)
        '            Case 1
        '                saveflodername = "一變"
        '            Case 2
        '                saveflodername = "二變"
        '            Case 3
        '                saveflodername = "三變"
        '            Case 4
        '                saveflodername = "四變"
        '            Case 5
        '                saveflodername = "五變"
        '            Case 6
        '                saveflodername = "六變"
        '            Case 7
        '                saveflodername = "七變"
        '            Case 8
        '                saveflodername = "八變"
        '            Case 9
        '                saveflodername = "九變"
        '        End Select
        '    End If
        'End If
        'folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text.Substring(0, 12) + "\" + saveflodername)
        folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text + "\" + saveflodername)
        If Not folderExists Then
            'My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text.Substring(0, 12) + "\" + saveflodername)
            My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text + "\" + saveflodername)
        End If
        'targetDirectory = targetDirectory + "\" + SWC02.Text.Substring(0, 12) + "\" + saveflodername
        targetDirectory = targetDirectory + "\" + SWC02.Text + "\" + saveflodername
        savefilename = targetDirectory + "\" + tempfilename
        fileuploadhere.SaveAs(savefilename)
        'filelistboxhere.Items.Add(tempfilename)
        'filelistboxhere.Items.Item(filelistboxhere.Items.Count - 1).Value = savefilename
        Session("uploadfilename") = Session("uploadfilename") + savefilename
        swcdbwrite("uploadfile")
        getuploadfile()
    End Sub

    Protected Sub SWC29_fileuploadok_Click(sender As Object, e As System.EventArgs) Handles SWC29_fileuploadok.Click, SWC30_fileuploadok.Click, SWC_r_file_fileuploadok.Click, SWC80_fileuploadok.Click, otheropen1_fileuploadok.Click, otheropen2_fileuploadok.Click, otheropen3_fileuploadok.Click, VrFileUploadOk.Click
        If Len(SWC02.Text) < 12 Or SWC02.Text = "" Then
            MB("行政審查案件編號未輸入，或輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。檔案無法上傳")
            Exit Sub
        End If
        If SWC07.Text = "" Then
            MB("書件類別請務必選擇")
            Exit Sub
        End If
        Dim filelistboxhere As HyperLink
        Dim fileuploadhere As Object
        fileuploadhere = ""
        Dim tempfilename As String
        tempfilename = ""
        Dim saveflodername As String
        Dim savefilename As String
        Dim txtobj6171 As TextBox
        Select Case sender.id
            Case "SWC29_fileuploadok"  '圖6-1
                fileuploadhere = SWC29_fileupload
                tempfilename = SWC29_fileupload.FileName
                filelistboxhere = SWC29_hyperlink
                Session("uploadfilename") = "6-1"
                saveflodername = "審查"
                txtobj6171 = SWC29
            Case "SWC30_fileuploadok"   '圖7-1
                fileuploadhere = SWC30_fileupload
                tempfilename = SWC30_fileupload.FileName
                Session("uploadfilename") = "7-1"
                filelistboxhere = SWC30_hyperlink
                saveflodername = "審查"
                txtobj6171 = SWC30
            Case "SWC_r_file_fileuploadok"   '審查紀錄
                fileuploadhere = SWC_r_file_fileupload
                tempfilename = SWC_r_file_fileupload.FileName
                Session("uploadfilename") = "checkopen"
                filelistboxhere = SWC_r_file_hyperlink
                saveflodername = "審查紀錄公開"
            Case "SWC80_fileuploadok"   '掃描檔
                fileuploadhere = SWC80_fileupload
                tempfilename = SWC80_fileupload.FileName
                Session("uploadfilename") = "掃描檔"
                filelistboxhere = SWC80_hyperlink
                saveflodername = "掃描檔"
            Case "otheropen1_fileuploadok"   '公開的其他附件上傳1
                fileuploadhere = otheropen1_fileupload
                tempfilename = otheropen1_fileupload.FileName
                Session("uploadfilename") = "附件1"
                filelistboxhere = otheropen1_hyperlink
                saveflodername = "公開附件"
                txtobj6171 = otheropen1
            Case "otheropen2_fileuploadok"   '公開的其他附件上傳2
                fileuploadhere = otheropen2_fileupload
                tempfilename = otheropen2_fileupload.FileName
                Session("uploadfilename") = "附件2"
                filelistboxhere = otheropen2_hyperlink
                saveflodername = "公開附件"
                txtobj6171 = otheropen2
            Case "otheropen3_fileuploadok"   '公開的其他附件上傳3
                fileuploadhere = otheropen3_fileupload
                tempfilename = otheropen3_fileupload.FileName
                Session("uploadfilename") = "附件3"
                filelistboxhere = otheropen3_hyperlink
                saveflodername = "公開附件"
                txtobj6171 = otheropen3
            Case "VrFileUploadOk"   '環景照片檔案上傳
                fileuploadhere = VrFileUploadBar
                tempfilename = VrFileUploadBar.FileName
                Session("uploadfilename") = "環景照片"
                filelistboxhere = VrFileUpload_hyperlink
                saveflodername = "環景照片"
        End Select

        '沒有選取檔案
        If Not fileuploadhere.hasfile Then
            MB("請選擇欲上傳的檔案。")
            Exit Sub
        End If
        '副檔名檢察
        If LCase(Right(fileuploadhere.FileName, 3)) <> "pdf" And LCase(Right(fileuploadhere.FileName, 3)) <> "jpg" Then
            fileuploadhere.Dispose()
            MB("請選擇 PDF 或 JPG 檔案格式上傳，謝謝")
            Exit Sub
        End If
        '檔案大小檢察
        '合訂本(掃描黨的例外)
        If sender.id = "SWC80_fileuploadok" Then
            If fileuploadhere.FileBytes.Length > 52428800 Then
                fileuploadhere.Dispose()
                MB("請選擇 50Mb 以下檔案上傳，謝謝!!")
                Exit Sub
            End If
        Else
            If fileuploadhere.FileBytes.Length > 52428800 Then
                fileuploadhere.Dispose()
                MB("請選擇 50Mb 以下檔案上傳，謝謝!!")
                Exit Sub
            End If
        End If

        If filelistboxhere.Text <> "" Then
            MB("已經有檔案，若要覆蓋，請先刪除原有檔案")
            Exit Sub
        End If
        Dim folderExists As Boolean
        Dim extname As String = Right(fileuploadhere.FileName, 4)
        Dim Filename As String = SWC02.Text.Substring(0, 12)
        Dim FileYear As Integer = Convert.ToInt16(Filename.Substring(4, 3))
        Dim FileYearS As String = FileYear.ToString()
        If (FileYear > 93) Then
            FileYearS = FileYearS + "年掃描圖檔"
        Else
            FileYearS = "93年度暨以前掃描圖檔"
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
        End If
        Dim targetDirectory As String = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
        Dim targetURL As String = ConfigurationManager.AppSettings("swcpsurl") + FileYearS + "\水保申請案件\水保計畫"
        If (SWC07.Text = "簡易水保") Then
            targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
            targetURL = ConfigurationManager.AppSettings("swcpsurl") + FileYearS + "\水保申請案件\簡易水保"
        End If
        'folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text.Substring(0, 12))
        folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text)
        If Not folderExists Then
            'My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text.Substring(0, 12))
            My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text)
        End If

        'If Len(SWC02.Text) > 12 And sender.id <> "SWC80_fileuploadok" Then
        '    Select Case SWC02.Text.Substring(13, 1)
        '        Case 1
        '            saveflodername = "一變"
        '        Case 2
        '            saveflodername = "二變"
        '        Case 3
        '            saveflodername = "三變"
        '        Case 4
        '            saveflodername = "四變"
        '        Case 5
        '            saveflodername = "五變"
        '        Case 6
        '            saveflodername = "六變"
        '        Case 7
        '            saveflodername = "七變"
        '        Case 8
        '            saveflodername = "八變"
        '        Case 9
        '            saveflodername = "九變"

        '    End Select
        'End If
        'folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text.Substring(0, 12) + "\" + saveflodername)
        folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + "\" + SWC02.Text + "\" + saveflodername)
        If Not folderExists Then
            'My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text.Substring(0, 12) + "\" + saveflodername)
            My.Computer.FileSystem.CreateDirectory(targetDirectory + "\" + SWC02.Text + "\" + saveflodername)
        End If
        'targetDirectory = targetDirectory + "\" + SWC02.Text.Substring(0, 12) + "\" + saveflodername + "\"
        targetDirectory = targetDirectory + "\" + SWC02.Text + "\" + saveflodername + "\"
        'targetURL = targetURL + "/" + SWC02.Text.Substring(0, 12) + "/" + saveflodername + "/"
        targetURL = targetURL + "/" + SWC02.Text + "/" + saveflodername + "/"


        'Session("uploadfilename") '圖6-1,圖7-1的目錄
        folderExists = My.Computer.FileSystem.DirectoryExists(targetDirectory + Session("uploadfilename"))
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(targetDirectory + Session("uploadfilename"))
        End If
        targetDirectory = targetDirectory + Session("uploadfilename")
        targetURL = targetURL + Session("uploadfilename")
        'If sender.id = "SWC_r_file_fileuploadok" Or sender.id <> "SWC80_fileuploadok" Then '審查紀錄公開化
        If sender.id = "SWC_r_file_fileuploadok" Or sender.id = "SWC80_fileuploadok" Or sender.id = "VrFileUploadOk" Then '審查紀錄公開化
            savefilename = targetDirectory + "\" + fileuploadhere.FileName
            fileuploadhere.SaveAs(savefilename)
            filelistboxhere.NavigateUrl = targetURL + "/" + fileuploadhere.FileName
            filelistboxhere.Text = fileuploadhere.FileName
            'filelistboxhere.Items.Item(filelistboxhere.Items.Count - 1).Value = savefilename
            Session("uploadfilename") = fileuploadhere.FileName
            If sender.id = "SWC_r_file_fileuploadok" Then
                SWC_r_file.Text = fileuploadhere.FileName
            ElseIf sender.id = "SWC80_fileuploadok" Then
                SWC80.Text = fileuploadhere.FileName
            ElseIf sender.id = "VrFileUploadOk" Then '處理環景照片
                'Dim targetURL As String = ConfigurationManager.AppSettings("swcpsurl") + FileYearS + "\處分案件\處分書"
                VrFile_img.ImageUrl = targetURL + "/" + fileuploadhere.FileName
                VrFileUploadTxt.Text = fileuploadhere.FileName
                VrFileUploadPathTxt.Text = targetURL + "/" + fileuploadhere.FileName

            End If

        Else '圖6-1,圖7-1,公開附件1,公開附件2,公開附件3
            savefilename = targetDirectory + "\" + SWC02.Text + "_" + Session("uploadfilename") + "." + Right(fileuploadhere.FileName, 3)
            fileuploadhere.SaveAs(savefilename)
            filelistboxhere.NavigateUrl = targetURL + "/" + SWC02.Text + "_" + Session("uploadfilename") + "." + Right(fileuploadhere.FileName, 3)
            filelistboxhere.Text = SWC02.Text + "_" + Session("uploadfilename") + "." + Right(fileuploadhere.FileName, 3)
            'filelistboxhere.Items.Item(filelistboxhere.Items.Count - 1).Value = savefilename
            Session("uploadfilename") = SWC02.Text + "_" + Session("uploadfilename") + "." + Right(fileuploadhere.FileName, 3)
            txtobj6171.Text = savefilename
        End If
        swcdbwrite("uploadfile")
        'MB(filelistboxhere.NavigateUrl + "___" + filelistboxhere.Text)
        '去OPENSWC登錄這筆資料的附件有變更，晚上要執行同步
        OPENSWCPS(SWC02.Text)


    End Sub

    Protected Sub SWC29_fileclean_Click(sender As Object, e As System.EventArgs) Handles SWC29_fileclean.Click, SWC30_fileclean.Click, SWC_r_file_fileclean.Click, SWC80_fileclean.Click, otheropen1_fileclean.Click, otheropen2_fileclean.Click, otheropen3_fileclean.Click
        Dim filelistboxhere As HyperLink
        Dim fileuploadhere As Object
        fileuploadhere = ""
        Dim tempfilename As String
        tempfilename = ""
        Dim txtobj6171 As TextBox
        Select Case sender.id
            Case "SWC29_fileclean"  '圖6-1
                fileuploadhere = SWC29_fileupload
                tempfilename = SWC29_fileupload.FileName
                filelistboxhere = SWC29_hyperlink
                Session("uploadfilename") = "6-1"
                Session("filelog") = "6-1"
                txtobj6171 = SWC29
            Case "SWC30_fileclean"   '圖7-1
                fileuploadhere = SWC30_fileupload
                tempfilename = SWC30_fileupload.FileName
                Session("uploadfilename") = "7-1"
                Session("filelog") = "7-1"
                txtobj6171 = SWC30
                filelistboxhere = SWC30_hyperlink
            Case "SWC_r_file_fileclean"   '審查紀錄
                fileuploadhere = SWC_r_file_fileupload
                tempfilename = SWC_r_file_fileupload.FileName
                Session("uploadfilename") = "checkopen"
                Session("filelog") = "審查紀錄公開"
                filelistboxhere = SWC_r_file_hyperlink
            Case "SWC80_fileclean"   '掃描檔
                fileuploadhere = SWC80_fileupload
                tempfilename = SWC80_fileupload.FileName
                Session("uploadfilename") = "掃描檔"
                filelistboxhere = SWC80_hyperlink
                Session("filelog") = "核訂本"
            Case "otheropen1_fileclean"  '公開附件1
                fileuploadhere = otheropen1_fileupload
                tempfilename = otheropen1_fileupload.FileName
                filelistboxhere = otheropen1_hyperlink
                Session("uploadfilename") = "附件1"
                Session("filelog") = "公開附件1"
                txtobj6171 = SWC29
            Case "otheropen2_fileclean"  '公開附件2
                fileuploadhere = otheropen2_fileupload
                tempfilename = otheropen2_fileupload.FileName
                filelistboxhere = otheropen2_hyperlink
                Session("uploadfilename") = "附件2"
                Session("filelog") = "公開附件2"
                txtobj6171 = SWC29
            Case "otheropen3_fileclean"   '公開附件3
                fileuploadhere = otheropen3_fileupload
                tempfilename = otheropen3_fileupload.FileName
                filelistboxhere = otheropen3_hyperlink
                Session("uploadfilename") = "附件3"
                Session("filelog") = "公開附件3"
                txtobj6171 = SWC30

        End Select
        If filelistboxhere.Text = "" Then
            Exit Sub
        End If
        Dim deletefilename As String
        Dim deletefileurl As String = filelistboxhere.NavigateUrl
        deletefilename = ConfigurationManager.AppSettings("swcpspath") + Replace(Right(deletefileurl, Len(deletefileurl) - Len(ConfigurationManager.AppSettings("swcpsurl"))), "/", "\")
        Dim fileExists As Boolean = My.Computer.FileSystem.FileExists(deletefilename)
        If Not fileExists Then
            'MB(deletefilename + "_no")
        Else
            'MB(deletefilename + "_yes")
            '寫入使用者紀錄
            Session("delfilename") = Session("filelog") + "_" + deletefilename
            swcdbwrite("delfile")
            Session("filelog") = ""
            My.Computer.FileSystem.DeleteFile(deletefilename, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently, FileIO.UICancelOption.DoNothing)
            filelistboxhere.Text = ""
            filelistboxhere.NavigateUrl = ""
            If sender.id = "SWC29_fileclean" Or sender.id = "SWC30_fileclean" Then
                txtobj6171.Text = ""
            End If
        End If
        '去OPENSWC登錄這筆資料的附件有變更，晚上要執行同步
        OPENSWCPS(SWC02.Text)
    End Sub

    Protected Sub CheckfileGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles CheckfileGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = CheckfileGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(CheckfileGridView.Rows(aa).Cells(0).Text)
                Dim cftbCategory As DataTable = ViewState("cftbCategory")
                Dim i As Integer
                For i = 0 To (Int(cftbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(cftbCategory.Rows(i)("序號")) = number) Then
                        '寫入使用者紀錄
                        Session("delfilename") = "審查附件_" + cftbCategory.Rows(i)("附件檔名")
                        swcdbwrite("delfile")
                        File.Delete(cftbCategory.Rows(i)("附件路徑"))
                        cftbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("cftbCategory") = cftbCategory
                CheckfileGridView.DataSource = cftbCategory
                CheckfileGridView.DataBind()
        End Select
    End Sub

    Protected Sub CheckfileGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles CheckfileGridView.RowDeleting

    End Sub
    Protected Sub receivefileGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles receivefileGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = receivefileGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(receivefileGridView.Rows(aa).Cells(0).Text)
                Dim rctbCategory As DataTable = ViewState("rctbCategory")
                Dim i As Integer
                For i = 0 To (Int(rctbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(rctbCategory.Rows(i)("序號")) = number) Then
                        '寫入使用者紀錄
                        Session("delfilename") = "受理附件_" + rctbCategory.Rows(i)("附件檔名")
                        swcdbwrite("delfile")
                        File.Delete(rctbCategory.Rows(i)("附件路徑"))
                        rctbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("rctbCategory") = rctbCategory
                receivefileGridView.DataSource = rctbCategory
                receivefileGridView.DataBind()
        End Select
    End Sub

    Protected Sub receivefileGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles receivefileGridView.RowDeleting

    End Sub
    Protected Sub doGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles doGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = doGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(doGridView.Rows(aa).Cells(0).Text)
                Dim dotbCategory As DataTable = ViewState("dotbCategory")
                Dim i As Integer
                For i = 0 To (Int(dotbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(dotbCategory.Rows(i)("序號")) = number) Then
                        '寫入使用者紀錄
                        Session("delfilename") = "施工中附件_" + dotbCategory.Rows(i)("附件檔名")
                        swcdbwrite("delfile")
                        File.Delete(dotbCategory.Rows(i)("附件路徑"))
                        dotbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("dotbCategory") = dotbCategory
                doGridView.DataSource = dotbCategory
                doGridView.DataBind()
        End Select
    End Sub

    Protected Sub doGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles doGridView.RowDeleting

    End Sub

    Protected Sub elseGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles elseGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = elseGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(elseGridView.Rows(aa).Cells(0).Text)
                Dim elsetbCategory As DataTable = ViewState("elsetbCategory")
                Dim i As Integer
                For i = 0 To (Int(elsetbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(elsetbCategory.Rows(i)("序號")) = number) Then
                        '寫入使用者紀錄
                        Session("delfilename") = "其他附件_" + elsetbCategory.Rows(i)("附件檔名")
                        swcdbwrite("delfile")
                        File.Delete(elsetbCategory.Rows(i)("附件路徑"))
                        elsetbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("elsetbCategory") = elsetbCategory
                elseGridView.DataSource = elsetbCategory
                elseGridView.DataBind()
        End Select
    End Sub

    Protected Sub elseGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles elseGridView.RowDeleting

    End Sub

    Protected Sub finishGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles finishGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = finishGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(finishGridView.Rows(aa).Cells(0).Text)
                Dim finishtbCategory As DataTable = ViewState("finishtbCategory")
                Dim i As Integer
                For i = 0 To (Int(finishtbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(finishtbCategory.Rows(i)("序號")) = number) Then
                        '寫入使用者紀錄
                        Session("delfilename") = "已完工附件_" + finishtbCategory.Rows(i)("附件檔名")
                        swcdbwrite("delfile")
                        File.Delete(finishtbCategory.Rows(i)("附件路徑"))
                        finishtbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("finishtbCategory") = finishtbCategory
                finishGridView.DataSource = finishtbCategory
                finishGridView.DataBind()
        End Select
    End Sub

    Protected Sub finishGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles finishGridView.RowDeleting

    End Sub

    Protected Sub psGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles psGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = psGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(psGridView.Rows(aa).Cells(0).Text)
                Dim pstbCategory As DataTable = ViewState("pstbCategory")
                Dim i As Integer
                For i = 0 To (Int(pstbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(pstbCategory.Rows(i)("序號")) = number) Then
                        '寫入使用者紀錄
                        Session("delfilename") = "備註附件_" + pstbCategory.Rows(i)("附件檔名")
                        swcdbwrite("delfile")
                        File.Delete(pstbCategory.Rows(i)("附件路徑"))
                        pstbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("pstbCategory") = pstbCategory
                psGridView.DataSource = pstbCategory
                psGridView.DataBind()


        End Select
    End Sub

    Protected Sub psGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles psGridView.RowDeleting

    End Sub

    Protected Sub LinkButton11_Click(sender As Object, e As System.EventArgs) Handles LinkButton11.Click, LinkButton12.Click, LinkButton13.Click, LinkButton14.Click, LinkButton15.Click, LinkButton16.Click, LinkButton17.Click, LinkButton21.Click, LinkButton22.Click, LinkButton23.Click, LinkButton24.Click, LinkButton25.Click, LinkButton26.Click, LinkButton27.Click, LinkButton31.Click, LinkButton32.Click, LinkButton33.Click, LinkButton34.Click, LinkButton35.Click, LinkButton36.Click, LinkButton37.Click, LinkButton41.Click, LinkButton42.Click, LinkButton43.Click, LinkButton44.Click, LinkButton45.Click, LinkButton46.Click, LinkButton47.Click, LinkButton51.Click, LinkButton52.Click, LinkButton53.Click, LinkButton54.Click, LinkButton55.Click, LinkButton56.Click, LinkButton57.Click, LinkButton61.Click, LinkButton62.Click, LinkButton63.Click, LinkButton64.Click, LinkButton65.Click, LinkButton66.Click, LinkButton67.Click, LinkButton71.Click, LinkButton72.Click, LinkButton73.Click, LinkButton74.Click, LinkButton75.Click, LinkButton76.Click, LinkButton77.Click, LinkButton81.Click, LinkButton82.Click, LinkButton83.Click, LinkButton84.Click, LinkButton85.Click, LinkButton86.Click, LinkButton87.Click, LinkButton91.Click, LinkButton92.Click, LinkButton93.Click, LinkButton94.Click, LinkButton95.Click, LinkButton96.Click, LinkButton97.Click, LinkButton01.Click, LinkButton02.Click, LinkButton03.Click, LinkButton04.Click, LinkButton05.Click, LinkButton06.Click, LinkButton07.Click
        'cleanquery()
        Dim aa() As String = sender.CommandArgument.Split(",")
        Dim dist As String = aa(0)
        Dim casestate As String = aa(1)
        'MB(aa(0) + "," + aa(1))
        If Len(dist) = 3 Then
            dist = Left(dist, 2)
        End If
        swcquereytext.Text = "[SWC04] Like '%" + casestate + "%' and [SWC08] like '%" + dist + "%'"
        SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
        SqlDataSource2.DataBind()

    End Sub

    Protected Sub geouser_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles geouser.SelectedIndexChanged
        SWC25.Text = geouser.Text
    End Sub

    Protected Sub CancelOK_Click(sender As Object, e As System.EventArgs) Handles CancelOK.Click
        tempswc02.Text = ""
        Response.Redirect("swc_104.aspx?SearchType=1")
    End Sub

    'Protected Sub SWC36_Click(sender As Object, e As System.EventArgs) Handles SWC36.Click, SWC41.Click, SWC56.Click, SWC77.Click
    '    Dim radobject As RadioButton
    '    Dim radok As Boolean = True
    '    Dim radindex As Integer = 1
    '    Do While radok
    '        Try
    '            radobject = FindControl(sender.id + Trim(radindex.ToString("00")))
    '            radobject.Checked = False
    '            radindex = radindex + 1
    '        Catch ex As Exception
    '            radok = False
    '        End Try
    '    Loop
    'End Sub

    Protected Sub SWC_r_updata_Click(sender As Object, e As System.EventArgs) Handles SWC_r_updata.Click
        If Len(SWC02.Text) < 12 Or SWC02.Text = "" Then
            MB("行政審查案件編號未輸入，或輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。檔案無法上傳")
            Exit Sub
        End If
        'If SWC_r_no.Text = "" Then
        '    MB("審查次數請務必填登，謝謝!!")
        '    Exit Sub
        'End If
        If SWC_r_c.Text = "" Then
            MB("審查日期請務必填登，謝謝!!")
            Exit Sub
        End If
        swcchreportlbno.Text = (Convert.ToInt16(swcchreportlbno.Text) + 1).ToString()
        Dim swcchreporttbCategory As DataTable = ViewState("swcchreporttbCategory")
        If swcchreporttbCategory Is Nothing Then
            Dim swcchreportdt As DataTable = New DataTable()
            swcchreportdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("審查次數", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("審查日期", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("補正期限", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("意見函檔", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("行政審查案件編號", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("修改人員", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("最後修改日期", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("備註", GetType(System.String)))
            swcchreportdt.Columns.Add(New DataColumn("新增修改已存檔", GetType(System.String)))
            ViewState("swcchreporttbCategory") = swcchreportdt
            swcchreporttbCategory = ViewState("swcchreporttbCategory")
        Else
            'Dim i As Integer
            'For i = 0 To tbCategory.Rows.Count - 1
            '    tbCategory.Rows(i)("序號") = Convert.ToInt32(GridView1.Rows(i).Cells(0).Text)
            '    tbCategory.Rows(i)("區") = GridView1.Rows(i).Cells(1).Text
            '    tbCategory.Rows(i)("段") = GridView1.Rows(i).Cells(2).Text
            '    tbCategory.Rows(i)("小段") = GridView1.Rows(i).Cells(3).Text
            '    tbCategory.Rows(i)("地號") = GridView1.Rows(i).Cells(4).Text
            'Next i
        End If
        'Add new row
        Dim nowstring As String = Now.ToString("yyyy-MM-dd HH:mm:ss")
        swcchreporttbCategory.Rows.Add(swcchreporttbCategory.NewRow())
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("序號") = Convert.ToInt16(swcchreportlbno.Text)
        'swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("審查次數") = Int(SWC_r_no.Text)
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("審查次數") = "0"
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("審查日期") = SWC_r_c.Text
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("補正期限") = SWC_r_f.Text
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("意見函檔") = SWC_r_file.Text
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("行政審查案件編號") = SWC02.Text
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("修改人員") = Session("name").ToString
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("最後修改日期") = nowstring
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("備註") = SWC_r_file_hyperlink.NavigateUrl
        swcchreporttbCategory.Rows(swcchreporttbCategory.Rows.Count - 1)("新增修改已存檔") = ""
        ViewState("swcchreporttbCategory") = swcchreporttbCategory
        'Bind the table to the gridview
        swccheckGridView.DataSource = swcchreporttbCategory
        swccheckGridView.DataBind()
        SWC_r_no.Text = ""
        SWC_r_c.Text = ""
        SWC_r_f.Text = ""
        SWC_r_file.Text = ""
        SWC_r_file_hyperlink.Text = ""
        SWC_r_file_hyperlink.NavigateUrl = ""
    End Sub

    Protected Sub SWC_r_cancel_Click(sender As Object, e As System.EventArgs) Handles SWC_r_cancel.Click
        SWC29_fileclean_Click(SWC_r_file_fileclean, EventArgs.Empty)
        SWC_r_no.Text = ""
        SWC_r_c.Text = ""
        SWC_r_f.Text = ""
        SWC_r_file.Text = ""
        SWC_r_file_hyperlink.Text = ""
        SWC_r_file_hyperlink.NavigateUrl = ""
    End Sub

    Protected Sub swccheckGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles swccheckGridView.RowCommand
        Select Case e.CommandName
            Case "Delete"
                Dim aa As Integer
                aa = swccheckGridView.Rows(e.CommandArgument).RowIndex
                Dim number As Integer = Convert.ToInt32(swccheckGridView.Rows(aa).Cells(0).Text)
                
                Dim swcchreporttbCategory As DataTable = ViewState("swcchreporttbCategory")
                
                Dim i As Integer
                For i = 0 To (Int(swcchreporttbCategory.Rows.Count) - 1)
                    If (Convert.ToInt32(swcchreporttbCategory.Rows(i)("序號")) = number) Then
                        '這邊要多處理附件刪除的動作
                        '寫入使用者紀錄
                        Session("delfilename") = "審查公開附件附件_" + swcchreporttbCategory.Rows(i)("意見函檔")
                        swcdbwrite("delfile")
                        SWC_r_file_hyperlink.Text = swcchreporttbCategory.Rows(i)("意見函檔")
                        SWC_r_file_hyperlink.NavigateUrl = swcchreporttbCategory.Rows(i)("備註")
                        SWC29_fileclean_Click(SWC_r_file_fileclean, EventArgs.Empty)
                        SWC_r_file_hyperlink.Text = ""
                        SWC_r_file_hyperlink.NavigateUrl = ""
                        swcchreporttbCategory.Rows.RemoveAt(i)
                        Exit For
                    End If
                Next i
                ViewState("swcchreporttbCategory") = swcchreporttbCategory
                swccheckGridView.DataSource = swcchreporttbCategory
                swccheckGridView.DataBind()
        End Select
    End Sub

    Protected Sub swccheckGridView_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles swccheckGridView.RowDeleting
    
    End Sub

    Protected Sub icon_webmap_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles icon_webmap.Click
        '以下要改，先拿輔導預約管理的指令看一下
        Select Case SWC04.Text
            Case "退補件", "受理中", "審查中"
                Response.Write("<script> window.open('" + ConfigurationManager.AppSettings("thisip") + "tslm/WEBGIS1.aspx?TYPE=SWCnotok&ID=" + SWC02.Text + "','WEBGIS');</script>")
            Case "施工中"
                Response.Write("<script> window.open(" + ConfigurationManager.AppSettings("thisip") + "tslm/WEBGIS1.aspx?TYPE=SWCdoing&ID=" + SWC02.Text + "','WEBGIS');</script>")
            Case "不予受理", "撤銷", "不予核定", "已核定", "停工中", "已完工", "廢止", "失效"
                Response.Write("<script> window.open('" + ConfigurationManager.AppSettings("thisip") + "tslm/WEBGIS1.aspx?TYPE=SWCother&ID=" + SWC02.Text + "','WEBGIS');</script>")
        End Select
        'Response.Write("<script> window.open('http://172.28.100.55/tslm/WEBGIS1.aspx?TYPE=SWC&ID=" + SWC02.Text + "','WEBGIS');</script>")
    End Sub

    Protected Sub icon_copycase_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles icon_copycase.Click
        If Len(SWC02.Text) > 12 Then
            If Right(SWC02.Text, 1) = "9" Then
                MB("變更次數已達系統設計極限，請洽主管與系統商，謝謝!!")
                Exit Sub
            End If
        End If
        copycasea("copy")
        Response.Redirect("swc_104.aspx?copy=copy&Editmode=addnew&SWC00=SWC" + Now.Date.ToString("yyyyMMdd") + Now.Hour.ToString("00") + Now.Minute.ToString("00") + Now.Second.ToString("00"))
    End Sub

    Sub copycasea(dowhat As String)
        Dim objectca As Object
        Select Case dowhat
            Case "copy" '複製元案件
                For i = 1 To 28
                    If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                        objectca = FindControl("SWC" + Trim(i.ToString("00")))
                        Session("copycase" + Trim(i.ToString("00"))) = objectca.text
                    End If
                Next
                For i = 43 To 52
                    If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                        objectca = FindControl("SWC" + Trim(i.ToString("00")))
                        Session("copycase" + Trim(i.ToString("00"))) = objectca.text
                    End If
                Next
            Case "get"  '取得案件備份
                For i = 1 To 28
                    If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                        objectca = FindControl("SWC" + Trim(i.ToString("00")))
                        If i = 1 Or i = 4 Then
                            objectca.text = ""
                        Else
                            objectca.text = Session("copycase" + Trim(i.ToString("00")))
                        End If
                    End If
                Next
                For i = 43 To 52
                    If i <> 3 And i <> 6 And i <> 19 And i <> 20 And i <> 37 And i <> 42 And i <> 55 And i <> 57 And i <> 60 And i <> 77 Then
                        objectca = FindControl("SWC" + Trim(i.ToString("00")))
                        objectca.text = Session("copycase" + Trim(i.ToString("00")))
                    End If
                Next
                '處理核定編號
                '取得SWC年度數量到這邊
                getswcyearcount()
                '處理行政審查案件編號多加一
                If Len(SWC02.Text) > 12 Then
                    SWC02.Text = Left(SWC02.Text, Len(SWC02.Text) - 1) + Trim((Int(Right(SWC02.Text, 1)) + 1).ToString("0"))
                Else
                    SWC02.Text = SWC02.Text + "-1"
                End If
                ''變更設計多加一
                'SWC06.Text = Trim((Int(SWC06.Text) + 1).ToString("0"))
                '取得地籍資料
                getlanddata()
        End Select
    End Sub

    Protected Sub SqlDataSource2_Selected(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceStatusEventArgs) Handles SqlDataSource2.Selected
        qLabel1.Text = Trim(e.AffectedRows.ToString) + "筆"
    End Sub


    Protected Sub exportexcel_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles exportexcel.Click
        '設定儲存檔名，不用設定副檔名，系統自動判斷 excel 版本，產生 .xls 或 .xlsx 副檔名
        Dim folderExists As Boolean
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("cpath") + "exceltemp")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("cpath") + "exceltemp")
        End If
        Dim excelpathfile As String = ConfigurationManager.AppSettings("cpath") + "exceltemp\ilg_" + Now().Year.ToString("0000") + Now().Month.ToString("00") + Now().Day.ToString("00") + "_" + Now().Hour.ToString("00") + Now().Minute.ToString("00") + Now().Second.ToString("00")
        Dim excelApp As Excel.Application
        Dim wBook As Excel._Workbook
        Dim wSheet As Excel._Worksheet
        Dim wRange As Excel.Range

        '開啟一個新的應用程式
        excelApp = New Excel.Application()
        '讓Excel文件可見
        excelApp.Visible = True
        '停用警告訊息
        excelApp.DisplayAlerts = False
        '加入新的活頁簿
        excelApp.Workbooks.Add(Type.Missing)
        '引用第一個活頁簿
        wBook = excelApp.Workbooks(1)
        '設定活頁簿焦點
        wBook.Activate()

        Try
            '引用第一個工作表
            wSheet = wBook.Worksheets(1)
            '命名工作表的名稱
            wSheet.Name = "水保申請案件統計表"
            '設定工作表焦點
            wSheet.Activate()
            excelApp.Cells(1, 1) = "水保申請案件行政區案件狀態統計表"
            excelApp.Cells(2, 1) = Label20.Text

            '設定總和公式 =SUM(B2:B4)
            'excelApp.Cells(5, 2).Formula = String.Format("=SUM(B{0}:B{1})", 2, 4)

            '設定表頭資料
            excelApp.Cells(3, 1) = ""
            excelApp.Cells(3, 2) = "受理中"
            excelApp.Cells(3, 3) = "審查中"
            excelApp.Cells(3, 4) = "已核定"
            excelApp.Cells(3, 5) = "施工中"
            excelApp.Cells(3, 6) = "已完工"
            excelApp.Cells(3, 7) = "已廢止"
            excelApp.Cells(3, 8) = "小計"
            '設定表頭顏色
            wRange = wSheet.Range(wSheet.Cells(3, 1), wSheet.Cells(3, 7))
            wRange.Select()
            wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
            wRange.Interior.Color = ColorTranslator.ToOle(Color.SkyBlue)
            '#0099FF
            '設定北投區資料
            excelApp.Cells(4, 1) = "北投區"
            excelApp.Cells(4, 2) = LinkButton11.Text
            excelApp.Cells(4, 3) = LinkButton12.Text
            excelApp.Cells(4, 4) = LinkButton13.Text
            excelApp.Cells(4, 5) = LinkButton14.Text
            excelApp.Cells(4, 6) = LinkButton15.Text
            excelApp.Cells(4, 7) = LinkButton16.Text
            excelApp.Cells(4, 8) = LinkButton17.Text
            '設定士林區資料
            excelApp.Cells(5, 1) = "士林區"
            excelApp.Cells(5, 2) = LinkButton21.Text
            excelApp.Cells(5, 3) = LinkButton22.Text
            excelApp.Cells(5, 4) = LinkButton23.Text
            excelApp.Cells(5, 5) = LinkButton24.Text
            excelApp.Cells(5, 6) = LinkButton25.Text
            excelApp.Cells(5, 7) = LinkButton26.Text
            excelApp.Cells(5, 8) = LinkButton27.Text
            '設定大安區資料
            excelApp.Cells(6, 1) = "大安區"
            excelApp.Cells(6, 2) = LinkButton31.Text
            excelApp.Cells(6, 3) = LinkButton32.Text
            excelApp.Cells(6, 4) = LinkButton33.Text
            excelApp.Cells(6, 5) = LinkButton34.Text
            excelApp.Cells(6, 6) = LinkButton35.Text
            excelApp.Cells(6, 7) = LinkButton36.Text
            excelApp.Cells(6, 8) = LinkButton37.Text
            '設定中山區資料
            excelApp.Cells(7, 1) = "中山區"
            excelApp.Cells(7, 2) = LinkButton41.Text
            excelApp.Cells(7, 3) = LinkButton42.Text
            excelApp.Cells(7, 4) = LinkButton43.Text
            excelApp.Cells(7, 5) = LinkButton44.Text
            excelApp.Cells(7, 6) = LinkButton45.Text
            excelApp.Cells(7, 7) = LinkButton46.Text
            excelApp.Cells(7, 8) = LinkButton47.Text
            '設定內湖區資料
            excelApp.Cells(8, 1) = "內湖區"
            excelApp.Cells(8, 2) = LinkButton51.Text
            excelApp.Cells(8, 3) = LinkButton52.Text
            excelApp.Cells(8, 4) = LinkButton53.Text
            excelApp.Cells(8, 5) = LinkButton54.Text
            excelApp.Cells(8, 6) = LinkButton55.Text
            excelApp.Cells(8, 7) = LinkButton56.Text
            excelApp.Cells(8, 8) = LinkButton57.Text
            '設定文山區資料
            excelApp.Cells(9, 1) = "文山區"
            excelApp.Cells(9, 2) = LinkButton61.Text
            excelApp.Cells(9, 3) = LinkButton62.Text
            excelApp.Cells(9, 4) = LinkButton63.Text
            excelApp.Cells(9, 5) = LinkButton64.Text
            excelApp.Cells(9, 6) = LinkButton65.Text
            excelApp.Cells(9, 7) = LinkButton66.Text
            excelApp.Cells(9, 8) = LinkButton67.Text
            '設定中正區資料
            excelApp.Cells(10, 1) = "中正區"
            excelApp.Cells(10, 2) = LinkButton71.Text
            excelApp.Cells(10, 3) = LinkButton72.Text
            excelApp.Cells(10, 4) = LinkButton73.Text
            excelApp.Cells(10, 5) = LinkButton74.Text
            excelApp.Cells(10, 6) = LinkButton75.Text
            excelApp.Cells(10, 7) = LinkButton76.Text
            excelApp.Cells(10, 8) = LinkButton77.Text
            '設定信義區資料
            excelApp.Cells(11, 1) = "信義區"
            excelApp.Cells(11, 2) = LinkButton81.Text
            excelApp.Cells(11, 3) = LinkButton82.Text
            excelApp.Cells(11, 4) = LinkButton83.Text
            excelApp.Cells(11, 5) = LinkButton84.Text
            excelApp.Cells(11, 6) = LinkButton85.Text
            excelApp.Cells(11, 7) = LinkButton86.Text
            excelApp.Cells(11, 8) = LinkButton87.Text
            '設定南港區資料
            excelApp.Cells(12, 1) = "南港區"
            excelApp.Cells(12, 2) = LinkButton91.Text
            excelApp.Cells(12, 3) = LinkButton92.Text
            excelApp.Cells(12, 4) = LinkButton93.Text
            excelApp.Cells(12, 5) = LinkButton94.Text
            excelApp.Cells(12, 6) = LinkButton95.Text
            excelApp.Cells(12, 7) = LinkButton96.Text
            excelApp.Cells(12, 8) = LinkButton97.Text
            '設定小計資料
            excelApp.Cells(13, 1) = "小計"
            excelApp.Cells(13, 2) = LinkButton01.Text
            excelApp.Cells(13, 3) = LinkButton02.Text
            excelApp.Cells(13, 4) = LinkButton03.Text
            excelApp.Cells(13, 5) = LinkButton04.Text
            excelApp.Cells(13, 6) = LinkButton05.Text
            excelApp.Cells(13, 7) = LinkButton06.Text
            excelApp.Cells(13, 8) = LinkButton07.Text
            '合併儲存格
            wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(1, 8))
            wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            wRange.Merge()
            '合併儲存格
            wRange = wSheet.Range(wSheet.Cells(2, 1), wSheet.Cells(2, 8))
            wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            wRange.Merge()
            '自動調整欄寬
            wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(13, 8))
            wRange.Select()
            wRange.Columns.AutoFit()
            Try
                '另存活頁簿
                wBook.SaveAs(excelpathfile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
                'Console.WriteLine("儲存文件於 " + Environment.NewLine + excelpathfile)
            Catch ex As Exception
                MB("儲存檔案出錯，檔案可能正在使用!_" + ex.Message)
                Exit Sub
            End Try

        Catch ex As Exception
            MB("產生報表時出錯！_" + ex.Message)
            Exit Sub
        End Try

        '關閉活頁簿
        wBook.Close(False, Type.Missing, Type.Missing)
        '關閉Excel
        excelApp.Quit()
        '釋放Excel資源
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        FinalReleaseComObject(wBook)
        FinalReleaseComObject(wSheet)
        FinalReleaseComObject(wRange)
        FinalReleaseComObject(excelApp)
        GC.Collect()


        '把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        Dim iStream As System.IO.Stream
        '以10K為單位暫存:
        Dim buffer(10000) As Byte
        Dim length As Integer
        Dim dataToRead As Long
        '  得到文件名.
        excelpathfile = excelpathfile + ".xlsx"
        Dim filename As String = System.IO.Path.GetFileName(excelpathfile)
        Try
            ' 打開文件.
            iStream = New System.IO.FileStream(excelpathfile, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
            ' 得到文件大小:
            dataToRead = iStream.Length
            Response.ContentType = "application/x-rar-compressed"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename))
            Do While (dataToRead > 0)
                '保証client連接
                If (Response.IsClientConnected) Then
                    length = iStream.Read(buffer, 0, 10000)
                    Response.OutputStream.Write(buffer, 0, length)
                    Response.Flush()
                    dataToRead = dataToRead - length
                Else
                    '結束循環
                    dataToRead = -1
                End If
            Loop
            If iStream.Length <> 0 Then
                '關閉文件
                iStream.Close()
                My.Computer.FileSystem.DeleteFile(excelpathfile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
        Catch ex As Exception
            ' error
            Response.Write("Error : " + ex.Message)

        Finally

        End Try
    End Sub

    Protected Sub icon_excelexport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles icon_excelexport.Click
        'Response.Clear()
        'Response.Write("<meta http-equiv=Content-Type content=text/html;charset=utf-8>")
        'Dim excelFileName As String = "合法案件_" + DateTime.Now.ToString("yyyyMMdd") + ".xls"
        ''excel檔名
        'Response.AddHeader("content-disposition", "attachment;filename=" + Server.UrlEncode(excelFileName))
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.Charset = ""
        'Dim sw As System.IO.StringWriter = New System.IO.StringWriter()
        'Dim htw As System.Web.UI.HtmlTextWriter = New System.Web.UI.HtmlTextWriter(sw)
        'Dim dg As DataGrid = New DataGrid()
        'dg.DataSource = SqlDataSource2.Select(DataSourceSelectArguments.Empty)
        'dg.DataBind()
        'dg.RenderControl(htw)
        'Response.Write("合法案件")
        'Response.Write(sw.ToString())
        'Response.End()

        '以下是新的程式碼
        '先取得欄位名稱
        Dim connf As SqlConnection = New SqlConnection()
        Dim sqlcomf As SqlCommand = New SqlCommand()
        Dim dr2f As SqlDataReader
        connf.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        connf.Open()
        sqlcomf.Connection = connf
        sqlcomf.CommandText = "SELECT * FROM ::fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', 'SWCSWC', 'column', NULL)"
        dr2f = sqlcomf.ExecuteReader()

        '再取得資料
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        If swcquereytext.Text <> "" Then
            sqlcom.CommandText = "SELECT * FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
        Else
            sqlcom.CommandText = "SELECT * FROM [SWCSWC] ORDER BY [SWC00]"
        End If
        dr2 = sqlcom.ExecuteReader()
        '設定EXCEK輸出
        '設定儲存檔名，不用設定副檔名，系統自動判斷 excel 版本，產生 .xls 或 .xlsx 副檔名
        Dim folderExists As Boolean
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("cpath") + "exceltemp")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("cpath") + "exceltemp")
        End If
        Dim excelpathfile As String = ConfigurationManager.AppSettings("cpath") + "exceltemp\swc_" + Now().Year.ToString("0000") + Now().Month.ToString("00") + Now().Day.ToString("00") + "_" + Now().Hour.ToString("00") + Now().Minute.ToString("00") + Now().Second.ToString("00")
        Dim excelApp As Excel.Application
        Dim wBook As Excel._Workbook
        Dim wSheet As Excel._Worksheet
        Dim wRange As Excel.Range

        '開啟一個新的應用程式
        excelApp = New Excel.Application()
        '讓Excel文件可見
        excelApp.Visible = True
        '停用警告訊息
        excelApp.DisplayAlerts = False
        '加入新的活頁簿
        excelApp.Workbooks.Add(Type.Missing)
        '引用第一個活頁簿
        wBook = excelApp.Workbooks(1)
        '設定活頁簿焦點
        wBook.Activate()
        '引用第一個工作表
        wSheet = wBook.Worksheets(1)
        '命名工作表的名稱
        wSheet.Name = "水保申請案件清冊"
        '設定工作表焦點
        wSheet.Activate()
        excelApp.Cells(1, 1) = "水保申請案件清冊"
        'excelApp.Cells(2, 1) = Label20.Text
        '設定表頭資料
        excelApp.Cells(2, 1) = "序號"
        Dim kk As Integer = 1
        While (dr2f.Read())
            kk = kk + 1
            excelApp.Cells(2, kk) = dr2f(3).ToString
            excelApp.Cells(3, kk) = dr2f(1).ToString
        End While
        '關閉欄位資料庫連線
        dr2f.Close()
        dr2f.Dispose()
        sqlcomf.Dispose()
        connf.Close()
        ''設定表頭顏色
        'wRange = wSheet.Range(wSheet.Cells(3, 1), wSheet.Cells(3, 7))
        'wRange.Select()
        'wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        'wRange.Interior.Color = ColorTranslator.ToOle(Color.SkyBlue)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(1, kk))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        ''自動調整欄寬
        'wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(13, 8))
        'wRange.Select()
        'wRange.Columns.AutoFit()
        '開始讀取抓到的資料
        '從第4航開始
        Dim excelrow As Integer = 4
        While (dr2.Read())
            excelApp.Cells(excelrow, 1) = Convert.ToString(excelrow - 3)
            For i = 1 To dr2.VisibleFieldCount
                'MB(Trim(dr2(i).ToString))
                '設定美列資料
                If i = 32 Or i = 33 Or i = 34 Or i = 35 Or i = 38 Or i = 39 Or i = 43 Or i = 44 Or i = 52 Or i = 53 Or i = 54 Or i = 56 Or i = 58 Or i = 59 Or i = 60 Or i = 79 Or i = 83 Or i = 85 Then '日期欄位
                    If IsDBNull(dr2(i - 1)) Then
                        excelApp.Cells(excelrow, i + 1) = ""
                    End If
                    If Left(Trim(dr2(i - 1).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(i - 1).ToString), 10) = "1900-01-01" Then
                        excelApp.Cells(excelrow, i + 1) = ""
                    Else
                        excelApp.Cells(excelrow, i + 1) = DateValue(Trim(dr2(i - 1).ToString)).ToString("yyyy-MM-dd")
                    End If
                Else  '不是日期欄位
                    excelApp.Cells(excelrow, i + 1) = Trim(dr2(i - 1).ToString)
                End If
            Next
            excelrow = excelrow + 1
        End While
        '關閉資料庫連線
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        'EXCEL存檔
        Try
            '另存活頁簿
            wBook.SaveAs(excelpathfile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            'Console.WriteLine("儲存文件於 " + Environment.NewLine + excelpathfile)
        Catch ex As Exception
            MB("儲存檔案出錯，檔案可能正在使用!_" + ex.Message)
            Exit Sub
        End Try
        '關閉活頁簿
        wBook.Close(False, Type.Missing, Type.Missing)
        '關閉Excel
        excelApp.Quit()
        '釋放Excel資源
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        FinalReleaseComObject(wBook)
        FinalReleaseComObject(wSheet)
        FinalReleaseComObject(wRange)
        FinalReleaseComObject(excelApp)
        GC.Collect()


        '把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        Dim iStream As System.IO.Stream
        '以10K為單位暫存:
        Dim buffer(10000) As Byte
        Dim length As Integer
        Dim dataToRead As Long
        '  得到文件名.
        excelpathfile = excelpathfile + ".xlsx"
        Dim filename As String = System.IO.Path.GetFileName(excelpathfile)
        Try
            ' 打開文件.
            iStream = New System.IO.FileStream(excelpathfile, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
            ' 得到文件大小:
            dataToRead = iStream.Length
            Response.ContentType = "application/x-rar-compressed"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename))
            Do While (dataToRead > 0)
                '保証client連接
                If (Response.IsClientConnected) Then
                    length = iStream.Read(buffer, 0, 10000)
                    Response.OutputStream.Write(buffer, 0, length)
                    Response.Flush()
                    dataToRead = dataToRead - length
                Else
                    '結束循環
                    dataToRead = -1
                End If
            Loop
            If iStream.Length <> 0 Then
                '關閉文件
                iStream.Close()
                My.Computer.FileSystem.DeleteFile(excelpathfile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
        Catch ex As Exception
            ' error
            Response.Write("Error : " + ex.Message)

        Finally

        End Try
    End Sub
    Protected Sub Dorplistq3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dorplistq3.SelectedIndexChanged
        Dorplistq4.Items.Clear()
        Dorplistq4.Items.Add("")
        Dorplistq5.Items.Clear()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        Dim KCNTS As String = ""
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "SELECT distinct KCNT FROM LAND WHERE AA46 ='" + Dorplistq3.SelectedValue + "' order by KCNT"
        dr2 = sqlcom.ExecuteReader()
        While (dr2.Read())
            Dim listItemK As ListItem = New ListItem()
            listItemK.Text = dr2(0).ToString().Substring(0, 2)
            If (KCNTS <> dr2(0).ToString().Substring(0, 2)) Then
                Dorplistq4.Items.Add(listItemK)
                KCNTS = dr2(0).ToString().Substring(0, 2)
            End If
        End While
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
    End Sub
    Protected Sub Dorplistq4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dorplistq4.SelectedIndexChanged
        Dorplistq5.Items.Clear()
        Dorplistq5.Items.Add("")
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        Dim KCNTS As String = ""
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "select aa from(SELECT distinct SUBSTRING(KCNT, LEN(KCNT) - 2, 1)  as aa FROM LAND WHERE len(KCNT)>4 and AA46 ='" + Dorplistq3.SelectedValue + "' and substring(KCNT,1,2)='" + Dorplistq4.SelectedValue + "') as aa order by CHARINDEX(aa, '一,二,三,四,五,六,七,八,九,十')"
        dr2 = sqlcom.ExecuteReader()
        While (dr2.Read())
            KCNTS = dr2(0).ToString()
            Dim listItemK As ListItem = New ListItem()
            listItemK.Text = KCNTS
            Dorplistq5.Items.Add(listItemK)
        End While
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        conn.Dispose()
    End Sub

    Protected Sub icon_mexcelexport_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles icon_mexcelexport.Click
        '以下是新的程式碼
        ''先取得欄位名稱
        'Dim connf As SqlConnection = New SqlConnection()
        'Dim sqlcomf As SqlCommand = New SqlCommand()
        'Dim dr2f As SqlDataReader
        'connf.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        'connf.Open()
        'sqlcomf.Connection = connf
        'sqlcomf.CommandText = "SELECT * FROM ::fn_listextendedproperty(NULL, 'schema', 'dbo', 'table', 'SWCSWC', 'column', NULL)"
        'dr2f = sqlcomf.ExecuteReader()

        '再取得資料
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("tslmConnectionString2").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        'If swcquereytext.Text <> "" Then
        '    sqlcom.CommandText = "SELECT * FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
        'Else
        '    sqlcom.CommandText = "SELECT * FROM [SWCSWC] ORDER BY [SWC00]"
        'End If
        'sqlcom.CommandText = "SELECT TOP(10) * FROM [SWCSWC] ORDER BY [SWC00]"
        sqlcom.CommandText = "SELECT * FROM [SWCSWC] ORDER BY [SWC00]"
        dr2 = sqlcom.ExecuteReader()
        '設定EXCEK輸出
        '設定儲存檔名，不用設定副檔名，系統自動判斷 excel 版本，產生 .xls 或 .xlsx 副檔名
        Dim folderExists As Boolean
        folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("cpath") + "exceltemp")
        If Not folderExists Then
            My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("cpath") + "exceltemp")
        End If
        Dim excelpathfile As String = ConfigurationManager.AppSettings("cpath") + "exceltemp\swc_月報_" + Now().Year.ToString("0000") + Now().Month.ToString("00") + Now().Day.ToString("00") + "_" + Now().Hour.ToString("00") + Now().Minute.ToString("00") + Now().Second.ToString("00")
        Dim excelApp As Excel.Application
        Dim wBook As Excel._Workbook
        Dim wSheet As Excel._Worksheet
        Dim wRange As Excel.Range

        '開啟一個新的應用程式
        excelApp = New Excel.Application()
        '讓Excel文件可見
        excelApp.Visible = True
        '停用警告訊息
        excelApp.DisplayAlerts = False
        '加入新的活頁簿
        excelApp.Workbooks.Add(Type.Missing)
        '引用第一個活頁簿
        wBook = excelApp.Workbooks(1)
        '設定活頁簿焦點
        wBook.Activate()
        '引用第一個工作表
        wSheet = wBook.Worksheets(1)
        '命名工作表的名稱
        wSheet.Name = "水保申請案件清冊"
        '設定工作表焦點
        wSheet.Activate()
        excelApp.Cells(1, 1) = "水保申請案件清冊"
        'excelApp.Cells(2, 1) = Label20.Text
        '設定表頭資料
        excelApp.Cells(1, 1) = "序號"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(2, 1))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.DarkOrange)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(2, 1))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(1, 2) = "行政審查案件編號"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 2), wSheet.Cells(2, 2))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Yellow)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 2), wSheet.Cells(2, 2))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(1, 3) = "案件狀態"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 3), wSheet.Cells(2, 3))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Green)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 3), wSheet.Cells(2, 3))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(1, 4) = "基本資料"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 4), wSheet.Cells(1, 5))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.SkyBlue)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 4), wSheet.Cells(1, 5))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(2, 4) = "計畫名稱"
        excelApp.Cells(2, 5) = "水土保持義務人"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(2, 4), wSheet.Cells(2, 5))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.SkyBlue)
        '#0099FF
        '設定表頭資料
        excelApp.Cells(1, 6) = "委外審查"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 6), wSheet.Cells(1, 10))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.LawnGreen)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 6), wSheet.Cells(1, 10))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(2, 6) = "承辦技師"
        excelApp.Cells(2, 7) = "補正期限"
        excelApp.Cells(2, 8) = "繳費發文日期"
        excelApp.Cells(2, 9) = "送審日期"
        excelApp.Cells(2, 10) = "審查單位"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(2, 6), wSheet.Cells(2, 10))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.LawnGreen)
        '#0099FF
        '設定表頭資料
        excelApp.Cells(1, 11) = "核定"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 11), wSheet.Cells(1, 12))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Yellow)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 11), wSheet.Cells(1, 12))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(2, 11) = "核定日期"
        excelApp.Cells(2, 12) = "開工期限"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(2, 11), wSheet.Cells(2, 12))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Yellow)
        '#0099FF
        '設定表頭資料
        excelApp.Cells(1, 13) = ""
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 13), wSheet.Cells(1, 22))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Green)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 13), wSheet.Cells(1, 22))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(2, 13) = "監造技師"
        excelApp.Cells(2, 14) = "手機"
        excelApp.Cells(2, 15) = "工地負責人"
        excelApp.Cells(2, 16) = "手機"
        excelApp.Cells(2, 17) = "開工日期"
        excelApp.Cells(2, 18) = "完工日期"
        excelApp.Cells(2, 19) = "委外檢查公會"
        excelApp.Cells(2, 20) = "最近檢查日期"
        excelApp.Cells(2, 21) = "進度"
        excelApp.Cells(2, 22) = "展延次數"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(2, 13), wSheet.Cells(2, 22))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Green)
        '#0099FF
        '設定表頭資料
        excelApp.Cells(1, 23) = "完工"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 23), wSheet.Cells(1, 24))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Violet)
        '#0099FF
        '合併儲存格
        wRange = wSheet.Range(wSheet.Cells(1, 23), wSheet.Cells(1, 24))
        wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        wRange.Merge()
        '設定表頭資料
        excelApp.Cells(2, 23) = "完工日期"
        excelApp.Cells(2, 24) = "同意完工日期"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(2, 23), wSheet.Cells(2, 24))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.Violet)
        '#0099FF
        '設定表頭資料
        excelApp.Cells(1, 25) = "停工廢止不予核定"
        excelApp.Cells(2, 25) = "日期"
        '設定表頭顏色
        wRange = wSheet.Range(wSheet.Cells(1, 25), wSheet.Cells(2, 25))
        wRange.Select()
        wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        wRange.Interior.Color = ColorTranslator.ToOle(Color.PaleVioletRed)
        '#0099FF
        'Dim kk As Integer = 1
        'While (dr2f.Read())
        '    kk = kk + 1
        '    excelApp.Cells(2, kk) = dr2f(3).ToString
        '    excelApp.Cells(3, kk) = dr2f(1).ToString
        'End While
        ''關閉欄位資料庫連線
        'dr2f.Close()
        'dr2f.Dispose()
        'sqlcomf.Dispose()
        'connf.Close()
        ''設定表頭顏色
        'wRange = wSheet.Range(wSheet.Cells(3, 1), wSheet.Cells(3, 7))
        'wRange.Select()
        'wRange.Font.Color = ColorTranslator.ToOle(Color.Black)
        'wRange.Interior.Color = ColorTranslator.ToOle(Color.SkyBlue)
        '#0099FF
        ''合併儲存格
        'wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(1, kk))
        'wRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
        'wRange.Merge()
        '自動調整欄寬
        wRange = wSheet.Range(wSheet.Cells(1, 1), wSheet.Cells(2, 25))
        wRange.Select()
        wRange.Columns.AutoFit()
        '開始讀取抓到的資料
        '從第3航開始
        Dim excelrow As Integer = 3
        While (dr2.Read())
            excelApp.Cells(excelrow, 1) = Convert.ToString(excelrow - 2)
            excelApp.Cells(excelrow, 2) = Trim(dr2(2).ToString)
            excelApp.Cells(excelrow, 3) = Trim(dr2(4).ToString)
            excelApp.Cells(excelrow, 4) = Trim(dr2(5).ToString)
            excelApp.Cells(excelrow, 5) = Trim(dr2(13).ToString)
            excelApp.Cells(excelrow, 6) = Trim(dr2(21).ToString)
            If Left(Trim(dr2(32).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(32).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 7) = ""
            Else
                excelApp.Cells(excelrow, 7) = DateValue(Trim(dr2(32).ToString)).ToString("yyyy-MM-dd") '日
            End If
            If Left(Trim(dr2(33).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(33).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 8) = ""
            Else
                excelApp.Cells(excelrow, 8) = DateValue(Trim(dr2(33).ToString)).ToString("yyyy-MM-dd") '日
            End If
            If Left(Trim(dr2(34).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(34).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 9) = ""
            Else
                excelApp.Cells(excelrow, 9) = DateValue(Trim(dr2(34).ToString)).ToString("yyyy-MM-dd") '日
            End If
            excelApp.Cells(excelrow, 10) = Trim(dr2(22).ToString)
            If Left(Trim(dr2(38).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(38).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 11) = ""
            Else
                excelApp.Cells(excelrow, 11) = DateValue(Trim(dr2(38).ToString)).ToString("yyyy-MM-dd") '日
            End If
            If Left(Trim(dr2(82).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(82).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 12) = ""
            Else
                excelApp.Cells(excelrow, 12) = DateValue(Trim(dr2(82).ToString)).ToString("yyyy-MM-dd") '日
            End If
            excelApp.Cells(excelrow, 13) = Trim(dr2(45).ToString)
            excelApp.Cells(excelrow, 14) = Trim(dr2(47).ToString)
            excelApp.Cells(excelrow, 15) = Trim(dr2(49).ToString)
            excelApp.Cells(excelrow, 16) = Trim(dr2(50).ToString)
            If Left(Trim(dr2(51).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(51).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 17) = ""
            Else
                excelApp.Cells(excelrow, 17) = DateValue(Trim(dr2(51).ToString)).ToString("yyyy-MM-dd") '日
            End If
            If Left(Trim(dr2(52).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(52).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 18) = ""
            Else
                excelApp.Cells(excelrow, 18) = DateValue(Trim(dr2(52).ToString)).ToString("yyyy-MM-dd") '日
            End If
            excelApp.Cells(excelrow, 19) = ""  '委外檢查公會
            excelApp.Cells(excelrow, 20) = ""  '最近檢查日期   '日 
            excelApp.Cells(excelrow, 21) = ""  '進度 
            excelApp.Cells(excelrow, 22) = Trim(dr2(83).ToString)
            If Left(Trim(dr2(58).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(58).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 23) = ""
            Else
                excelApp.Cells(excelrow, 23) = DateValue(Trim(dr2(58).ToString)).ToString("yyyy-MM-dd") '日
            End If
            If Left(Trim(dr2(59).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(59).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 24) = ""
            Else
                excelApp.Cells(excelrow, 24) = DateValue(Trim(dr2(59).ToString)).ToString("yyyy-MM-dd") '日
            End If  '完工證明書核發日期
            If Left(Trim(dr2(78).ToString), 8) = "1900/1/1" Or Left(Trim(dr2(78).ToString), 10) = "1900-01-01" Then
                excelApp.Cells(excelrow, 25) = ""
            Else
                excelApp.Cells(excelrow, 25) = DateValue(Trim(dr2(78).ToString)).ToString("yyyy-MM-dd") '日
            End If

            excelrow = excelrow + 1
        End While
        '關閉資料庫連線
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        'EXCEL存檔
        Try
            '另存活頁簿
            wBook.SaveAs(excelpathfile, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing)
            'Console.WriteLine("儲存文件於 " + Environment.NewLine + excelpathfile)
        Catch ex As Exception
            MB("儲存檔案出錯，檔案可能正在使用!_" + ex.Message)
            Exit Sub
        End Try
        '關閉活頁簿
        wBook.Close(False, Type.Missing, Type.Missing)
        '關閉Excel
        excelApp.Quit()
        '釋放Excel資源
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp)
        FinalReleaseComObject(wBook)
        FinalReleaseComObject(wSheet)
        FinalReleaseComObject(wRange)
        FinalReleaseComObject(excelApp)
        GC.Collect()


        '把檔案作串流以供 CLIENT 端下載，不做串流檔案過大時會無法下載
        Dim iStream As System.IO.Stream
        '以10K為單位暫存:
        Dim buffer(10000) As Byte
        Dim length As Integer
        Dim dataToRead As Long
        '  得到文件名.
        excelpathfile = excelpathfile + ".xlsx"
        Dim filename As String = System.IO.Path.GetFileName(excelpathfile)
        Try
            ' 打開文件.
            iStream = New System.IO.FileStream(excelpathfile, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read)
            ' 得到文件大小:
            dataToRead = iStream.Length
            Response.ContentType = "application/x-rar-compressed"
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename))
            Do While (dataToRead > 0)
                '保証client連接
                If (Response.IsClientConnected) Then
                    length = iStream.Read(buffer, 0, 10000)
                    Response.OutputStream.Write(buffer, 0, length)
                    Response.Flush()
                    dataToRead = dataToRead - length
                Else
                    '結束循環
                    dataToRead = -1
                End If
            Loop
            If iStream.Length <> 0 Then
                '關閉文件
                iStream.Close()
                My.Computer.FileSystem.DeleteFile(excelpathfile, FileIO.UIOption.OnlyErrorDialogs, FileIO.RecycleOption.DeletePermanently)
            End If
        Catch ex As Exception
            ' error
            Response.Write("Error : " + ex.Message)

        Finally

        End Try
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        Dim dr2 As SqlDataReader
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "select SWC02, SWC05, SWC13, SWC21, SWC22, SWC45, SWC24, SWC80, SWC07 from SWCSWC "
        dr2 = sqlcom.ExecuteReader()
        While dr2.Read()
            OPENSWCALL("update", dr2(0).ToString(), dr2(1).ToString(), dr2(2).ToString(), dr2(3).ToString(), dr2(4).ToString(), dr2(5).ToString(), dr2(6).ToString(), dr2(7).ToString(), dr2(8).ToString())
        End While
        dr2.Close()
        dr2.Dispose()
        sqlcom.Dispose()
        conn.Close()
        conn.Dispose()
    End Sub


    Sub OPENSWCALL(ByVal dowhat As String, ByVal SWC02TEXT As String, ByVal SWC05TEXT As String, ByVal SWC13TEXT As String, ByVal SWC21TEXT As String, ByVal SWC22TEXT As String, ByVal SWC45TEXT As String, ByVal SWC24TEXT As String, ByVal SWC80TEXT As String, ByVal SWC07TEXT As String)
        Select Case dowhat
            Case "del" '刪除這一筆資料
                Dim conn As SqlConnection = New SqlConnection()
                Dim sqlcom As SqlCommand = New SqlCommand()
                conn = New SqlConnection()
                sqlcom = New SqlCommand()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                '先刪除既有的地籍資料
                sqlcom.CommandText = "DELETE FROM [OPENSWC] WHERE [OPENSWC02] = '" + Trim(SWC02TEXT) + "'"
                sqlcom.ExecuteNonQuery()
                sqlcom.Cancel()
            Case "update"   '看看有沒有這一筆資料，沒有的畫增加，有的畫修改
                '先檢查有沒有這一筆資料
                Dim conn As SqlConnection = New SqlConnection()
                Dim sqlcom As SqlCommand = New SqlCommand()
                Dim dr2 As SqlDataReader
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                sqlcom.CommandText = "select OPENSWC02 from OPENSWC where OPENSWC02='" + SWC02TEXT + "'"
                dr2 = sqlcom.ExecuteReader()
                Dim haverec As Boolean '是不是在公開的資料庫(OPENSWC)已經有這一筆資料
                If (dr2.Read()) Then
                    haverec = True
                Else
                    haverec = False
                End If
                dr2.Close()
                dr2.Dispose()
                sqlcom.Dispose()
                conn.Close()
                conn.Dispose()
                '以下要開始新增或是更新資料了
                conn = New SqlConnection()
                sqlcom = New SqlCommand()
                conn = New SqlConnection()
                sqlcom = New SqlCommand()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                sqlcom = New SqlCommand()
                sqlcom.Connection = conn
                If haverec Then
                    '更新
                    sqlcom.CommandText = "UPDATE [OPENSWC] SET [OPENSWC02]=@OPENSWC02 ,[OPENSWC05]=@OPENSWC05 ,[OPENSWC13]=@OPENSWC13 ,[OPENSWC21]=@OPENSWC21 ,[OPENSWC22]=@OPENSWC22 ,[OPENSWC45]=@OPENSWC45 ,[OPENSWC24]=@OPENSWC24 ,[OPENSWC80]=@OPENSWC80 ,[OPENSWCPATH]=@OPENSWCPATH WHERE [OPENSWC02]='" + SWC02TEXT + "'"
                Else
                    '新增
                    sqlcom.CommandText = "INSERT INTO [OPENSWC] ([OPENSWC02] ,[OPENSWC05] ,[OPENSWC13] ,[OPENSWC21] ,[OPENSWC22] ,[OPENSWC45] ,[OPENSWC24] ,[OPENSWC80] ,[OPENSWCPATH] ) VALUES (@OPENSWC02 ,@OPENSWC05 ,@OPENSWC13 ,@OPENSWC21 ,@OPENSWC22 ,@OPENSWC45 ,@OPENSWC24 ,@OPENSWC80 ,@OPENSWCPATH)"
                End If
                '參數設定從這邊開始
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC02", SWC02TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC05", SWC05TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC13", SWC13TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC21", SWC21TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC22", SWC22TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC45", SWC45TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC24", SWC24TEXT))
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWC80", SWC80TEXT))
                '以下處理路徑
                Dim folderExists As Boolean
                Dim Filename As String = SWC02TEXT.Substring(0, 12)
                Dim FileYear As Integer = Convert.ToInt16(Filename.Substring(4, 3))
                Dim FileYearS As String = FileYear.ToString()
                Dim targetDirectory As String
                If (FileYear > 93) Then
                    FileYearS = FileYearS + "年掃描圖檔"
                Else
                    FileYearS = "93年度暨以前掃描圖檔"
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS)
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件")
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫")
                End If
                folderExists = My.Computer.FileSystem.DirectoryExists(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
                If Not folderExists Then
                    My.Computer.FileSystem.CreateDirectory(ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保")
                End If
                targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\水保計畫"
                If (SWC07TEXT = "簡易水保") Then
                    targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
                End If
                sqlcom.Parameters.Add(New SqlParameter("@OPENSWCPATH", Right(targetDirectory, Len(targetDirectory) - Len(ConfigurationManager.AppSettings("swcpspath")))))
                sqlcom.ExecuteNonQuery()
                sqlcom.Cancel()
                sqlcom.Dispose()
                conn.Close()
                conn.Dispose()
        End Select
    End Sub

    Protected Sub icon_lightps_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles icon_lightps.Click
        swcquereytext.Text = "light = 'image/red.png'"
        SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
        SqlDataSource2.DataBind()
        GridView2.DataBind()
        Session("lightps") = "1"
        '有多少筆，在SqlDataSource2_Selected會自動計算
    End Sub

    Protected Sub SWC12_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles SWC12.SelectedIndexChanged
        '處理承辦人員的預先設定值
        '承辦人下拉式選單geouser
        '承辦人欄位SWC25
        '第一、連結使用人員資料庫
        Dim userconnstringsetting As ConnectionStringSettings
        Dim userconn As SqlConnection
        userconnstringsetting = ConfigurationManager.ConnectionStrings("swcconnstring")
        userconn = New SqlConnection(userconnstringsetting.ConnectionString)
        userconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT 姓名, 職稱, 電子郵件, 區 FROM [user] where 科室='審查管理科' and 職稱<>'技工' and 狀態='正常' and 區 like '%" + SWC12.Text + "%'", userconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "user")
        Dim i As Integer
        Dim al As Integer
        Dim arealist() As String
        Dim areauser As String
        For i = 0 To ds.Tables("user").Rows.Count - 1
            '處理區跟人的對應，寫入到areauser
            If Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "專門委員" And Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "科長" And Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "正工程司" And Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "股長" Then
                arealist = Trim(ds.Tables("user").Rows(i)("區").ToString()).Split(";")
                For al = 0 To arealist.Length - 1
                    Select Case arealist(al)
                        Case "北一區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北二區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北三區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北四區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北五區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北六區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北七區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北八區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北九區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南一區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南二區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南三區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南四區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南五區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南六區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南七區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南八區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南九區"
                            areauser = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                    End Select
                Next al
            End If

        Next i
        userconn.Close()
        geouser.ClearSelection()
        If areauser = "" Then
            geouser.Items.Item(0).Selected = True

        Else
            For i = 1 To geouser.Items.Count - 1
                If geouser.Items.Item(i).Text = areauser Then
                    geouser.Items.Item(i).Selected = True
                End If
            Next i
        End If
        SWC25.Text = geouser.Text
    End Sub
    Protected Sub GridView2_Sorting(sender As Object, e As GridViewSortEventArgs)

        If Session("lightps") = "1" Then
            swcquereytext.Text = "light = 'image/red.png'"
            SqlDataSource2.SelectCommand = "SELECT [SWC00], [SWC01], [SWC02], [SWC04], [SWC05], [SWC07], [SWC08], [SWC09], [SWC10], [SWC11], [SWC13], [light] FROM [SWCSWC] where " + swcquereytext.Text + " ORDER BY [SWC00]"
            SqlDataSource2.DataBind()
            GridView2.DataBind()
        Else
            qButton1_Click(sender, e)
        End If
    End Sub

    Protected Sub SWC07_SelectedIndexChanged(sender As Object, e As EventArgs) Handles SWC07.SelectedIndexChanged
        '選簡易水保的話，預設無保證金要勾選
        If SWC07.SelectedIndex = 2 Then
            If SWC4101.Checked = False And SWC4102.Checked = False Then
                SWC4102.Checked = True
            End If
        End If
    End Sub

    Protected Sub SWC4102_CheckedChanged(sender As Object, e As EventArgs) Handles SWC4102.CheckedChanged
        If SWC4102.Checked = True Then
            If SWC5601.Checked = False And SWC5602.Checked = False Then
                SWC5602.Checked = True
            End If
        End If
    End Sub

    Protected Sub SWC53_TextChanged(sender As Object, e As EventArgs) Handles SWC53.TextChanged
        If SWC53.Text = "" Then
        Else
            If SWC84.Text = "" Then
                Try
                    SWC84.Text = DateValue(SWC53.Text).AddYears(2).ToString("yyyy-MM-dd")
                    SWC84_CalendarExtender.EndDate = DateValue(SWC53.Text).AddYears(2).AddMonths(6)
                    SWC84_CalendarExtender.StartDate = DateValue(SWC53.Text)
                Catch ex As Exception

                End Try
            End If
        End If
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

    End Sub


    Protected Sub AddVrFileList_Click(sender As Object, e As System.EventArgs) Handles AddVrFileList.Click
        If Len(SWC02.Text) < 12 Or SWC02.Text = "" Then
            MB("行政審查案件編號未輸入，或輸入錯誤，必須是12碼以上，且前兩個字為英文，第五六七字元為民國年。檔案無法上傳")
            Exit Sub
        End If
        If SWC07.Text = "" Then
            MB("書件類別請務必選擇")
            Exit Sub
        End If

        VrFilesNo.Text = (Convert.ToInt16(VrFilesNo.Text) + 1).ToString()
        Dim VrFilesCategory As DataTable = ViewState("VrFilesCategory")

        If VrFilesCategory Is Nothing Then
            Dim VrFilesTable As DataTable = New DataTable()

            VrFilesTable.Columns.Add(New DataColumn("序號", GetType(System.String)))
            VrFilesTable.Columns.Add(New DataColumn("實體檔案名稱", GetType(System.String)))
            VrFilesTable.Columns.Add(New DataColumn("環景照片名稱", GetType(System.String)))
            VrFilesTable.Columns.Add(New DataColumn("實體檔案路徑", GetType(System.String)))
            VrFilesTable.Columns.Add(New DataColumn("修改人員", GetType(System.String)))
            VrFilesTable.Columns.Add(New DataColumn("最後修改日期", GetType(System.String)))
            VrFilesTable.Columns.Add(New DataColumn("vrid", GetType(System.String)))
            ViewState("VrFilesCategory") = VrFilesTable
            VrFilesCategory = ViewState("VrFilesCategory")

        End If

        'Add new row
        Dim nowstring As String = Now.ToString("yyyy-MM-dd HH:mm:ss")
        VrFilesCategory.Rows.Add(VrFilesCategory.NewRow())
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("序號") = Convert.ToInt16(VrFilesNo.Text)
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("實體檔案名稱") = VrFileUploadTxt.Text
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("環景照片名稱") = TextVrDesc.Text
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("實體檔案路徑") = VrFileUploadPathTxt.Text
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("修改人員") = Session("name").ToString
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("最後修改日期") = nowstring
        VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("vrid") = Convert.ToInt16(VrFilesNo.Text)
        ViewState("VrFilesCategory") = VrFilesCategory
        'Bind the table to the gridview

        VrGridView.DataSource = VrFilesCategory
        VrGridView.DataBind()

        VrFileUploadTxt.Text = ""
        TextVrDesc.Text = ""
        'VrFilesNo.Text = ""
        VrFileUploadTxt.Text = ""
        VrFileUpload_hyperlink.NavigateUrl = ""
        VrFileUpload_hyperlink.Text = ""
        VrFile_img.ImageUrl = ""

        VrGridView.Columns.Item(5).Visible = True
        VrGridView.Columns.Item(4).Visible = False
        VrFileUpLoadON()

    End Sub


    Protected Sub VrGridView_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles VrGridView.RowCommand
        Select Case e.CommandName
            Case "DeleteVr"
                Dim aa As Integer
                aa = VrGridView.Rows(e.CommandArgument).RowIndex

                Dim number As Integer = Convert.ToInt32(VrGridView.Rows(aa).Cells(0).Text)
                Dim VrFilesCategory As DataTable = ViewState("VrFilesCategory")
                Dim i As Integer

                VrFilesCategory.Rows.RemoveAt(aa)

                ViewState("VrFilesCategory") = VrFilesCategory
                VrGridView.DataSource = VrFilesCategory
                VrGridView.DataBind()

            Case "ViewVr"
                Dim aa As Integer
                aa = VrGridView.Rows(e.CommandArgument).RowIndex

                Dim number As Integer = Convert.ToInt32(VrGridView.Rows(aa).Cells(6).Text)

                Dim VrPathSrc As String = "iFraPage/VrFileDemo.aspx?Case=" + Trim(SWC02.Text) + "&VrPic=" + number.ToString()

                VrViewArea.Text = "<iframe id='MyVrView' width='571px' height='405px' frameborder='0' runat='server' scrolling='no' src='" + VrPathSrc + "' title=''></iframe>"

            Case "ViewVrBig"
                Dim aa As Integer

                aa = VrGridView.Rows(e.CommandArgument).RowIndex

                Dim number As Integer = Convert.ToInt32(VrGridView.Rows(aa).Cells(6).Text)

                Dim VrPathSrc As String = "iFraPage/VrFileDemo.aspx?Case=" + Trim(SWC02.Text) + "&VrPic=" + number.ToString()

                Response.Write("<script language=javascript> window.open('" + VrPathSrc + "','Chart','toolbar=no, menubar=no, location=no, status=no'); </script>")



        End Select
    End Sub

    Sub GetVrFilesData(SubType As String)    '環景照片資料倒入
        Dim DataHave As String = "N"
        '第一、連結資料庫
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("swcconnstring").ConnectionString
        conn.Open()

        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("select * from Vrfiles where [caseid]='" + Trim(SWC02.Text) + "'order by caseid ", conn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "VrFilseReport")

        '第三、執行SQL指令之後，把資料庫撈出來的結果，交由GridView1控制項來呈現。
        For i = 0 To ds.Tables("VrFilseReport").Rows.Count - 1
            DataHave = "Y"
            VrFilesNo.Text = (Convert.ToInt16(VrFilesNo.Text) + 1).ToString()

            Dim VrFilesCategory As DataTable = ViewState("VrFilesCategory")

            If VrFilesCategory Is Nothing Then
                Dim VrFilesdt As DataTable = New DataTable()

                VrFilesdt.Columns.Add(New DataColumn("序號", GetType(System.String)))
                VrFilesdt.Columns.Add(New DataColumn("實體檔案名稱", GetType(System.String)))
                VrFilesdt.Columns.Add(New DataColumn("環景照片名稱", GetType(System.String)))
                VrFilesdt.Columns.Add(New DataColumn("實體檔案路徑", GetType(System.String)))
                VrFilesdt.Columns.Add(New DataColumn("修改人員", GetType(System.String)))
                VrFilesdt.Columns.Add(New DataColumn("最後修改日期", GetType(System.String)))
                VrFilesdt.Columns.Add(New DataColumn("vrid", GetType(System.String)))

                ViewState("VrFilesCategory") = VrFilesdt
                VrFilesCategory = ViewState("VrFilesCategory")

            End If

            'Add new row
            VrFilesCategory.Rows.Add(VrFilesCategory.NewRow())
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("序號") = Convert.ToInt16(VrFilesNo.Text)
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("實體檔案名稱") = ds.Tables("VrFilseReport").Rows(i).Item("vrfilename").ToString
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("環景照片名稱") = ds.Tables("VrFilseReport").Rows(i).Item("vrfiledesc").ToString
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("實體檔案路徑") = ds.Tables("VrFilseReport").Rows(i).Item("vrfilepath").ToString
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("修改人員") = ds.Tables("VrFilseReport").Rows(i).Item("uploaduser").ToString
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("最後修改日期") = ds.Tables("VrFilseReport").Rows(i).Item("uploaddate").ToString
            VrFilesCategory.Rows(VrFilesCategory.Rows.Count - 1)("vrid") = ds.Tables("VrFilseReport").Rows(i).Item("id").ToString
            ViewState("VrFilesCategory") = VrFilesCategory

            VrGridView.DataSource = VrFilesCategory
            VrGridView.DataBind()
        Next

        '第四、關閉資料庫的連接與相關資源
        conn.Close()

        Select Case SubType
            Case "edit"
                VrGridView.Columns.Item(5).Visible = True
                VrGridView.Columns.Item(4).Visible = False
                VrFileUpLoadON()

            Case "view"
                Dim VrPathSrc As String = "iFraPage/VrFileDemo.aspx?Case=" + Trim(SWC02.Text) + "&VrPic="

                If DataHave = "Y" Then
                    VrViewArea.Text = "<iframe id='MyVrView' width='571px' height='405px' frameborder='0' runat='server' scrolling='no' src='" + VrPathSrc + "' title=''></iframe>"
                End If

                VrGridView.Columns.Item(5).Visible = False
                VrGridView.Columns.Item(4).Visible = True
                VrFileUpLoadOFF()
        End Select

    End Sub
    Sub VrFileUpLoadON()
        VrImgUpload.Visible = True
        VrImgDemo.Visible = False

        VrGridView.Columns.Item(6).Visible = False
    End Sub
    Sub VrFileUpLoadOFF()
        VrImgUpload.Visible = False
        VrImgDemo.Visible = True

        VrGridView.Columns.Item(6).Visible = False
    End Sub

    Sub VrFile2DB()    '環景照片存入DB

        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn = New SqlConnection()
        sqlcom = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("ilgforswcconnstring").ConnectionString
        conn.Open()
        sqlcom.Connection = conn

        '操作方式為刪除再新增，不使用update
        sqlcom.CommandText = "DELETE FROM [Vrfiles] WHERE [caseid] = '" + Trim(SWC02.Text) + "' and casetype='SWC' "
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()

        Dim VrFilesCategory As DataTable = ViewState("VrFilesCategory")

        If VrFilesCategory IsNot Nothing Then

            Dim i As Integer
            For i = 0 To (Int(VrFilesCategory.Rows.Count) - 1)
                sqlcom = New SqlCommand()
                sqlcom.Connection = conn
                '新增新的修正過的環景照片資料
                sqlcom.CommandText = "INSERT INTO [Vrfiles] ([casetype] ,[caseid] ,[vrfilepath] ,[vrfiledesc] ,[uploaduser] ,[uploaddate],[vrfilename] ) VALUES (@ILGlook000 ,@ILGlook001 ,@ILGlook006 ,@ILGlook003 ,@ILGlook004 ,@ILGlook005,@ILGlook002)"

                '參數設定從這邊開始
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook000", "SWC"))
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook001", SWC02.Text))
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook002", Trim(VrFilesCategory.Rows(i)("實體檔案名稱"))))
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook003", Trim(VrFilesCategory.Rows(i)("環景照片名稱"))))
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook006", Trim(VrFilesCategory.Rows(i)("實體檔案路徑"))))
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook004", Session("name")))
                sqlcom.Parameters.Add(New SqlParameter("@ILGlook005", Now.Date.ToString("yyyy-MM-dd")))

                '參數設定到這邊
                sqlcom.ExecuteNonQuery()
                sqlcom.Cancel()
                sqlcom.Dispose()
            Next i
        End If
        conn.Close()
        conn.Dispose()


    End Sub

    Private Sub VrFileUploadOk_Load(sender As Object, e As EventArgs) Handles VrFileUploadOk.Load

    End Sub

End Class
