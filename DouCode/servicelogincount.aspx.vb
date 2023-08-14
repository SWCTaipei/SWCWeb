Imports System
Imports System.Data
Imports System.Configuration
Imports System.Collections
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Drawing
Imports System.Data.SqlClient


Partial Public Class servicelogincount
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        '清除該頁輸出緩存，設置該頁無緩存  
        'Response.Buffer = True
        'Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0)
        'Response.Expires = 0
        'Response.CacheControl = "no-cache"
        'Response.AppendHeader("Pragma", "No-Cache")

        ' 7位數字的到訪人數   
        Dim ClientIP As String = Request.ServerVariables("REMOTE_ADDR")		
        Dim peoplecount As String = GetPeopleCount(ClientIP, Request.QueryString("webpagename"), "0", "0")
        ' 用於驗證的Session        
        Me.CreateCheckCodeImage(peoplecount)
    End Sub

    ' 讀取到訪人數計數器加一筆資料
    Public Function GetPeopleCount(ByVal logip As String, ByVal logpage As String, ByVal logx As String, ByVal logy As String) As String
        '資料庫開啟
        Dim servicelogconnstringsetting As ConnectionStringSettings
        Dim servicelogconn As SqlConnection
        Dim servicelogcommand As SqlCommand
        Dim servicelogreader As SqlDataReader
        Dim getcountstring As String
        Dim insertstring As String
        'Dim str_Number As String
        '先讀取目前總數並加1
        servicelogconnstringsetting = ConfigurationManager.ConnectionStrings("elandservicelogconnstring")
        servicelogconn = New SqlConnection(servicelogconnstringsetting.ConnectionString)
        servicelogconn.Open()
        getcountstring = "Select COUNT(流水號) as 資料筆數 FROM servicelog"
        servicelogcommand = New SqlCommand(getcountstring, servicelogconn)
        servicelogreader = servicelogcommand.ExecuteReader()
        While servicelogreader.Read
            getcountstring = (servicelogreader.Item(0) + 1).ToString()
        End While
        servicelogreader.Close()
        servicelogcommand.Dispose()
        Dim nowstring As String = Now.ToString("yyyy/MM/dd HH:mm:ss")
        If Session("dd") = "true" Then
            '地籍搜尋的，算是回應而以不要紀錄不要加一
        Else
            '寫入目前登入的人
            insertstring = "Insert Into servicelog (流水號, 登入ip, 登入時間, 登入網頁, 登入座標X, 登入座標Y) Values (" + Trim(getcountstring) + ", '" + logip + "', '" + nowstring + "', '" + logpage + "', '" + logx + "', '" + logy + "')"
            servicelogcommand = New SqlCommand(insertstring, servicelogconn)
            servicelogcommand.ExecuteReader()
        End If
        servicelogcommand.Dispose()
        servicelogconn.Close()
        Session("dd") = ""
        '讀取資料總筆數，轉換成七個字元的格式，前面補0
        If Len(getcountstring) < 7 Then
            For i = 1 To 7 - Len(getcountstring)
                getcountstring = "0" + getcountstring
            Next
        End If
        Return getcountstring
    End Function

    '產生驗證圖
    Private Sub CreateCheckCodeImage(ByVal checkCode As String)
        If checkCode Is Nothing OrElse checkCode.Trim() = [String].Empty Then
            Return
        End If
        '設圖片定長寬
        Dim image As New System.Drawing.Bitmap(CInt(Math.Ceiling((checkCode.Length * 13.5))), 22)
        Dim g As System.Drawing.Graphics = Graphics.FromImage(image)
        Try
            '生成隨機生成器          
            Dim random As New Random()
            '清空圖片背景色       
            g.Clear(Color.White)
            'For i As Integer = 0 To 24
            '    '畫圖片的背景噪音線          
            '    Dim x1 As Integer = random.[Next](image.Width)
            '    Dim x2 As Integer = random.[Next](image.Width)
            '    Dim y1 As Integer = random.[Next](image.Height)
            '    Dim y2 As Integer = random.[Next](image.Height)
            '    g.DrawLine(New Pen(Color.Silver), x1, y1, x2, y2)
            'Next
            Dim font As Font = New System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic))
            Dim brush As New System.Drawing.Drawing2D.LinearGradientBrush(New Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2F, True)
            g.DrawString(checkCode, font, brush, 10, 2)
            'For i As Integer = 0 To 99
            '    '畫圖片的前景噪音點     
            '    Dim x As Integer = random.[Next](image.Width)
            '    Dim y As Integer = random.[Next](image.Height)
            '    image.SetPixel(x, y, Color.FromArgb(random.[Next]()))
            'Next
            '畫圖片的邊框線         
            g.DrawRectangle(New Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1)
            Dim ms As New System.IO.MemoryStream()
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif)
            Response.ClearContent()
            Response.ContentType = "image/Gif"
            Response.BinaryWrite(ms.ToArray())
        Finally
            g.Dispose()
            image.Dispose()
        End Try
    End Sub
End Class