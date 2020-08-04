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

Imports System.Data.SqlClient
Imports System.IO
Imports System.Net
Imports System.Net.Mail

Partial Class FileSyn
    Inherits System.Web.UI.Page

    Sub MB(ByVal messagetext As String)
        Try
            Dim myMsg As New Literal()
            myMsg.Text = "<script>alert('" & messagetext & "')</script>"
            Page.Controls.Add(myMsg)
        Catch
        End Try
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '傳回傳遞給方法之虛擬路徑的完整實體路徑
        '傳遞給 MapPath 方法的路徑必須是應用程式的相對路徑，而不是絕對路徑。
        TextBox1.Text = TextBox1.Text + vbNewLine + "Server.MapPath : " + Server.MapPath("~")

        '抓取 ASP.NET 網頁程式，所在的目錄
        TextBox1.Text = TextBox1.Text + vbNewLine + "Server.MapPath : " + Server.MapPath(".")

        '取得 asp.net 應用程式在伺服器上虛擬應用程式根路徑
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.ApplicationPath : " + Request.ApplicationPath

        '取得目前要求的虛擬路徑
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.CurrentExecutionFilePath : " + Request.CurrentExecutionFilePath

        '取得目前要求的虛擬路徑，與 CurrentExecutionFilePath 屬性不同，FilePath 並不會反映伺服器端的傳輸。
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.FilePath : " + Request.FilePath

        '取得目前要求的虛擬路徑
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.Path : " + Request.Path

        '取得目前執行應用程式之根目錄的實體檔案系統路徑
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.PhysicalApplicationPath : " + Request.PhysicalApplicationPath

        '取得與要求的 URL 對應之實體檔案系統路徑
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.PhysicalPath : " + Request.PhysicalPath

        '取得與要求的 URL
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.URLAbsolutePath : " + Request.Url.AbsolutePath

        '取得與要求的 URL
        TextBox1.Text = TextBox1.Text + vbNewLine + "Request.URLAbsoluteUri : " + Request.Url.AbsoluteUri

        Dim urlparts() As String = Server.MapPath("~").Split("\")
        For i = 1 To urlparts.Length
            TextBox1.Text = TextBox1.Text + vbNewLine + urlparts(i - 1)
        Next

    End Sub
    Protected Sub startsyn_Click(sender As Object, e As EventArgs) Handles startsyn.Click
        '我是哪一台
        Dim requesttargetPC As String = ""
        Dim caseindex As String
        Dim sourceURL As String = ""
        Dim targetPATH As String = ""
        Dim sourceFILENAME As String = ""
        Dim errlog As String = ""
        '取得與要求的 URL
        Dim urlparts() As String = Request.Url.AbsoluteUri.Split("/")
        requesttargetPC = urlparts(urlparts.Length - 2).ToLower
        If targetPCtext.Text = "tcge" Or targetPCtext.Text = "swcdoc" Then
            requesttargetPC = targetPCtext.Text
        End If
        Select Case requesttargetPC
            Case "tcge"

            Case "swcdoc"

            Case Else
                MB("不是預設要同步的主機，離開同步功能")
                Exit Sub
        End Select
        '讀取要同步到我這一台的資料
        Dim connsyn As SqlConnection = New SqlConnection()
        Dim sqlcomsyn As SqlCommand = New SqlCommand()
        Dim dr As SqlDataReader
        connsyn.ConnectionString = ConfigurationManager.ConnectionStrings("TSLMSWCCONN").ConnectionString
        connsyn.Open()
        sqlcomsyn.Connection = connsyn
        sqlcomsyn.CommandText = "SELECT * FROM UploadFileSyn WHERE HAVEPROCESS='False' and TARGETPC ='" + requesttargetPC + "' order by 流水號"
        dr = sqlcomsyn.ExecuteReader()
        While (dr.Read())
            'swcdos8caseid = Trim(dr.Item(0).ToString)
            caseindex = Trim(dr.Item("流水號").ToString)
            sourceURL = Trim(dr.Item("SOURCEURL").ToString)
            targetPATH = Trim(dr.Item("TARGETPATH").ToString)
            sourceFILENAME = Trim(dr.Item("FILENAME").ToString)
            '如果SOURCEURL是空的，就是要刪的
            If sourceURL = "" Then
                '刪吧
                '檢查targetPC有沒有每一階層的資料夾，沒有的話要先建
                Dim fileExists As Boolean = My.Computer.FileSystem.FileExists(targetPATH)
                If fileExists Then
                    My.Computer.FileSystem.DeleteFile(targetPATH)
                End If
                '把這筆資料改成已同步完畢
                Dim conn As SqlConnection = New SqlConnection()
                Dim sqlcom As SqlCommand = New SqlCommand()
                conn.ConnectionString = ConfigurationManager.ConnectionStrings("TSLMSWCCONN").ConnectionString
                conn.Open()
                sqlcom.Connection = conn
                Dim processTIME As DateTime = Now
                sqlcom.CommandText = "UPDATE [UploadFileSyn] SET [HAVEPROCESS] = -1, [PROCESSTIME] = '" + processTIME.ToString("yyyy-MM-dd HH:mm:ss.000") + "' WHERE [流水號]=" + caseindex
                sqlcom.ExecuteNonQuery()
                sqlcom.Cancel()
                conn.Close()
                conn.Dispose()
            Else
                '先檢查targetPC有沒有這個檔案，有的話要先刪
                Dim fileExists As Boolean = My.Computer.FileSystem.FileExists(targetPATH)
                If fileExists Then
                    My.Computer.FileSystem.DeleteFile(targetPATH)
                End If
                '判斷各階層資料夾是不是都建好了
                Dim pathparts() As String = targetPATH.Split("\")
                Dim pathpart As String = pathparts(0)
                For i = 1 To urlparts.Length - 1   '注意這邊從1開始是為了跳過資料夾 D: E: 這個，(urlparts.Length - 1 )是因為最後一個是檔案不用比較
                    pathpart = pathpart + "\" + pathparts(i)
                    '檢查targetPC有沒有每一階層的資料夾，沒有的話要先建
                    Dim floderExists As Boolean = My.Computer.FileSystem.DirectoryExists(pathpart)
                    If Not floderExists Then
                        My.Computer.FileSystem.CreateDirectory(pathpart)
                    End If
                Next
                '可以開始同步檔案了
                '抓附件開始
                Dim myrequest As WebRequest
                Dim myresponse As HttpWebResponse
                Dim mystream As Stream
                '判斷55那邊有沒有檔案
                myrequest = WebRequest.Create(sourceURL)
                Try
                    myresponse = myrequest.GetResponse
                    '有檔案才會繼續，沒檔案就出錯去CATCH了
                    mystream = myresponse.GetResponseStream()
                    Dim localfileStream As Stream = File.Create(targetPATH)
                    mystream.CopyTo(localfileStream)
                    localfileStream.Close()
                    mystream.Close()
                    '把這筆資料改成已同步完畢
                    Dim conn As SqlConnection = New SqlConnection()
                    Dim sqlcom As SqlCommand = New SqlCommand()
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings("TSLMSWCCONN").ConnectionString
                    conn.Open()
                    sqlcom.Connection = conn
                    Dim processTIME As DateTime = Now
                    sqlcom.CommandText = "UPDATE [UploadFileSyn] SET [HAVEPROCESS] = -1, [PROCESSTIME] = '" + processTIME.ToString("yyyy-MM-dd HH:mm:ss.000") + "' WHERE [流水號]=" + caseindex
                    sqlcom.ExecuteNonQuery()
                    sqlcom.Cancel()
                    conn.Close()
                    conn.Dispose()
                Catch ex As Exception
                    errlog = errlog + ";\n\r" + sourceFILENAME + "未同步到 " + requesttargetPC + ";"
                End Try
            End If
        End While
        dr.Close()
        dr.Dispose()
        sqlcomsyn.Dispose()
        connsyn.Close()
        '看有沒有錯誤，有的話要通知
        If errlog <> "" Then
            '寄信通知
            Try
                '以下是舊的messagemail的程式，參考用
                '設定寄給誰(GV)
                Dim mail As MailMessage = New MailMessage("ge-tslm@mail.taipei.gov.tw", "tcge7@geovector.com.tw")
                mail.SubjectEncoding = Encoding.UTF8
                '設定主旨
                mail.Subject = "您好，" + Now.ToString("yyyy-MM-dd") + " 書件平台檔案同步通知信"
                mail.IsBodyHtml = True '是否允 HTML 格式
                mail.BodyEncoding = Encoding.UTF8
                '信件BODY
                mail.Body = "<br />" + errlog
                mail.Body = mail.Body + "<br /><br />" + "坡地管理資料庫系統自動通知郵件"
                '設定SMTP帳號密碼_北市府
                Dim smtp As SmtpClient = New SmtpClient(ConfigurationManager.AppSettings("taipeimailsmtpname"))
                smtp.Credentials = New NetworkCredential(ConfigurationManager.AppSettings("taipeimailusername"), ConfigurationManager.AppSettings("taipeimailuserpassword")) 'SMTP 帳號密碼
                ''多維測試
                'Dim smtp As SmtpClient = New SmtpClient("mail.geovector.com.tw")
                'smtp.Credentials = New NetworkCredential("geocheck@geovector.com.tw", "1234") 'SMTP 帳號密碼

                '開始寄信
                smtp.Send(mail)

                '回收元件
                mail.Dispose()

            Catch ex As Exception
                '錯誤信就先不要寄了
                'mailerr(checkarray(bb, 2), checkarray(bb, 0) + checkarray(bb, 1), checkarray(bb, 3), "水保、違規與輔導案件")
            End Try
        End If
		'關掉自己
        Response.Write("<script language=javascript> window.opener=null; window.open('','_self'); window.close();</script>")
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If Not IsPostBack Then
            startsyn_Click(startsyn, EventArgs.Empty)
        End If
    End Sub
End Class
