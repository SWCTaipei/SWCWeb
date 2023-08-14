Imports System.Data.SqlClient
Imports System.Data
Imports System.Net.Mail
Imports System.Net
Imports System.IO

Partial Class mailcheck
    Inherits System.Web.UI.Page
    '陣列第二維參數的定義
    '0=姓名
    '1=職稱
    '2=電子郵件
    '3=檢核說明(MAIL裡要放的內容)
    Dim checkswcswcarray(1, 5) As String    '水保申請案件的文字陣列
    Dim checkswcilgarray(1, 5) As String    '水保違規案件的文字陣列
    Dim checkswcgarray(1, 5) As String      '水保輔導案件案件的文字陣列
    Dim checkarray(1, 5) As String          '全部的文字陣列
    '北一區對應areauser(0),北二區對應areauser(1),,,,南一區對應areauser(9),南二區對應areauser(10),,,,以此類推
    Dim areauser(18) As String
    Dim areauser_ilg(18) As String
    Dim areauser_swcg(18) As String
    Dim managername As String = ""  '資料庫承辦也加入全部一啟發信的人
    'Dim managername1 As String = "許巽舜"  '資料庫承辦也加入全部一啟發信的人
    Dim managername1 As String = "章姿隆"  '資料庫承辦也加入全部一啟發信的人
    'Dim managername2 As String = "謝宜芸"  '資料庫承辦也加入全部一啟發信的人
    Dim managername2 As String = ""  '資料庫承辦也加入全部一啟發信的人
    'Dim managername3 As String = "黃凱暉"  '資料庫承辦也加入全部一啟發信的人
    Dim managername3 As String = ""  '資料庫承辦也加入全部一啟發信的人
    Dim maildebug As String = ""
    Dim smtpmailserver As String = ""
    Dim smtpmailsend As String
    Dim smtpmailusername As String = ""
    Dim smtpmailuserpassword As String = ""

    Sub getSMTPMailparament()
        '第一、連結使用人員資料庫
        Dim mailconnstringsetting As ConnectionStringSettings
        Dim mailconn As SqlConnection
        mailconnstringsetting = ConfigurationManager.ConnectionStrings("SWCConnStr")
        mailconn = New SqlConnection(mailconnstringsetting.ConnectionString)
        mailconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT * FROM [SysValue] ", mailconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "mailds")
        '如果沒有資料就離開
        If ds.Tables("mailds").Rows.Count = 0 Then
            Exit Sub
        End If
        '有資料開始判斷檢查並做紀錄
        For i = 0 To ds.Tables("mailds").Rows.Count - 1
            Select Case ds.Tables("mailds").Rows(i)("GVTypeDesc").ToString
                Case "smtpServer"
                    smtpmailserver = Trim(ds.Tables("mailds").Rows(i)("GVValue").ToString)
                Case "mailAccount"
                    smtpmailusername = Trim(ds.Tables("mailds").Rows(i)("GVValue").ToString)
                Case "mailPwd"
                    smtpmailuserpassword = Trim(ds.Tables("mailds").Rows(i)("GVValue").ToString)
                Case "sendMail"
                    smtpmailsend = Trim(ds.Tables("mailds").Rows(i)("GVValue").ToString)
            End Select
        Next
        '第四、關閉資料庫的連接與相關資源
        ds.Clear()
        ds.Dispose()
        da.Dispose()
        mailconn.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Label1.Text = Now.ToString() + " 處理案件檢核機制中，請勿關閉此視窗"
        'Label1.Text = Label1.Text + Now.Hour.ToString

        If Now.Hour.ToString("00") = "00" Then
            Session("mailchecknow") = ""
            Label1.Text = Now.ToString() + " 處理案件檢核機制中，請勿關閉此視窗"
            '先設定相關的參數
            arrayini()
            '抓取MAILSERVER要用的字串
            getSMTPMailparament()
            '檢查水保申請案件
            'checkswc() '原本的
            checkswc201612()  '新改的
            '檢查水保違規
            checkilg()
            '檢查水保輔導案件
            checkswcg()
            '發檢核成果信件
            '發MAIL給相關人員，以下是整合一封的
            Dim bb As Integer
            For bb = 0 To checkarray.GetLength(0) - 1
                If checkarray(bb, 3) <> "" Then
                    Try
                        'MB(checkswcgarray(bb, 2))
                        'MB(checkarray(bb, 0) + checkarray(bb, 1))
                        'MB(checkarray(bb, 3))
                        mailswcgcase(checkarray(bb, 2), checkarray(bb, 0) + checkarray(bb, 1), checkarray(bb, 3), "水保、違規與輔導案件")
                    Catch ex As Exception
                        
                        mailerr(checkarray(bb, 2), checkarray(bb, 0) + checkarray(bb, 1), checkarray(bb, 3), "水保、違規與輔導案件")
                    End Try
                    
                End If
            Next bb
        End If
    End Sub

    Sub arrayini()
        '先設定相關的參數
        '幾個人員
        '幾個人員做幾個存有問題案件狀況的參數

        '第一、連結使用人員資料庫
        Dim userconnstringsetting As ConnectionStringSettings
        Dim userconn As SqlConnection
        userconnstringsetting = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
        userconn = New SqlConnection(userconnstringsetting.ConnectionString)
        userconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT name as 姓名, jobtitle as 職稱, email as 電子郵件, Tcgearea01 as 區1, Tcgearea02 as 區2, Tcgearea03 as 區3, mbgroup02 as 權限 FROM [geouser] where department='審查管理科' and jobtitle<>'技工' and status='正常' ", userconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "user")
        ReDim checkswcswcarray(ds.Tables("user").Rows.Count - 1, 5)     '水保申請案件的文字陣列
        ReDim checkswcilgarray(ds.Tables("user").Rows.Count - 1, 5)     '水保違規案件的文字陣列
        ReDim checkswcgarray(ds.Tables("user").Rows.Count - 1, 5)       '水保輔導案件案件的文字陣列
        ReDim checkarray(ds.Tables("user").Rows.Count - 1, 5)           '整合成一封的全部的文字陣列
        Dim arealist() As String
        Dim arealist_ilg() As String
        Dim arealist_swcg() As String
        Dim i As Integer
        Dim al As Integer
        For i = 0 To ds.Tables("user").Rows.Count - 1
            checkswcswcarray(i, 0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
            checkswcilgarray(i, 0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
            checkswcgarray(i, 0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
            checkarray(i, 0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
            checkswcswcarray(i, 1) = Trim(ds.Tables("user").Rows(i)("職稱").ToString())
            checkswcilgarray(i, 1) = Trim(ds.Tables("user").Rows(i)("職稱").ToString())
            checkswcgarray(i, 1) = Trim(ds.Tables("user").Rows(i)("職稱").ToString())
            checkarray(i, 1) = Trim(ds.Tables("user").Rows(i)("職稱").ToString())
            checkswcswcarray(i, 2) = Trim(ds.Tables("user").Rows(i)("電子郵件").ToString())
            checkswcilgarray(i, 2) = Trim(ds.Tables("user").Rows(i)("電子郵件").ToString())
            checkswcgarray(i, 2) = Trim(ds.Tables("user").Rows(i)("電子郵件").ToString())
            checkarray(i, 2) = Trim(ds.Tables("user").Rows(i)("電子郵件").ToString())
            checkswcswcarray(i, 3) = ""
            checkswcilgarray(i, 3) = ""
            checkswcgarray(i, 3) = ""
            checkarray(i, 3) = ""
            checkswcswcarray(i, 4) = Trim(ds.Tables("user").Rows(i)("權限").ToString())
            checkswcilgarray(i, 4) = Trim(ds.Tables("user").Rows(i)("權限").ToString())
            checkswcgarray(i, 4) = Trim(ds.Tables("user").Rows(i)("權限").ToString())
            checkarray(i, 4) = Trim(ds.Tables("user").Rows(i)("權限").ToString())
            '處理區跟人的對應，寫入到areauser
            If Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "專門委員" And Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "科長" And Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "正工程司" And Trim(ds.Tables("user").Rows(i)("職稱").ToString()) <> "股長" Then
                arealist = Trim(ds.Tables("user").Rows(i)("區1").ToString()).Split(";")
                For al = 0 To arealist.Length - 1
                    Select Case arealist(al)
                        Case "北一區"
                            areauser(0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北二區"
                            areauser(1) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北三區"
                            areauser(2) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北四區"
                            areauser(3) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北五區"
                            areauser(4) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北六區"
                            areauser(5) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北七區"
                            areauser(6) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北八區"
                            areauser(7) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北九區"
                            areauser(8) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南一區"
                            areauser(9) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南二區"
                            areauser(10) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南三區"
                            areauser(11) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南四區"
                            areauser(12) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南五區"
                            areauser(13) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南六區"
                            areauser(14) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南七區"
                            areauser(15) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南八區"
                            areauser(16) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南九區"
                            areauser(17) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                    End Select
                Next al
				arealist_ilg = Trim(ds.Tables("user").Rows(i)("區2").ToString()).Split(";")
                For al = 0 To arealist_ilg.Length - 1
                    Select Case arealist_ilg(al)
                        Case "北一區"
                            areauser_ilg(0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北二區"
                            areauser_ilg(1) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北三區"
                            areauser_ilg(2) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北四區"
                            areauser_ilg(3) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北五區"
                            areauser_ilg(4) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北六區"
                            areauser_ilg(5) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北七區"
                            areauser_ilg(6) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北八區"
                            areauser_ilg(7) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北九區"
                            areauser_ilg(8) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南一區"
                            areauser_ilg(9) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南二區"
                            areauser_ilg(10) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南三區"
                            areauser_ilg(11) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南四區"
                            areauser_ilg(12) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南五區"
                            areauser_ilg(13) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南六區"
                            areauser_ilg(14) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南七區"
                            areauser_ilg(15) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南八區"
                            areauser_ilg(16) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南九區"
                            areauser_ilg(17) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                    End Select
                Next al
				arealist_swcg = Trim(ds.Tables("user").Rows(i)("區3").ToString()).Split(";")
                For al = 0 To arealist_swcg.Length - 1
                    Select Case arealist_swcg(al)
                        Case "北一區"
                            areauser_swcg(0) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北二區"
                            areauser_swcg(1) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北三區"
                            areauser_swcg(2) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北四區"
                            areauser_swcg(3) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北五區"
                            areauser_swcg(4) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北六區"
                            areauser_swcg(5) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北七區"
                            areauser_swcg(6) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北八區"
                            areauser_swcg(7) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "北九區"
                            areauser_swcg(8) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南一區"
                            areauser_swcg(9) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南二區"
                            areauser_swcg(10) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南三區"
                            areauser_swcg(11) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南四區"
                            areauser_swcg(12) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南五區"
                            areauser_swcg(13) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南六區"
                            areauser_swcg(14) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南七區"
                            areauser_swcg(15) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南八區"
                            areauser_swcg(16) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                        Case "南九區"
                            areauser_swcg(17) = Trim(ds.Tables("user").Rows(i)("姓名").ToString())
                    End Select
                Next al
            End If

        Next i
        'Label1.Text = ""
        'For i = 0 To 17
        '    Label1.Text = Label1.Text + "<br />" + i.ToString + areauser(i)
        'Next i
        '第四、關閉資料庫的連接與相關資源
        userconn.Close()
    End Sub

    Sub checkswc201612()
        Dim i As Integer
        For i = 0 To checkarray.GetLength(0) - 1
            checkarray(i, 3) = "<br /><br />" + Now.Date.ToString("yyyy-MM-dd") + "<br />"
            checkarray(i, 3) = checkarray(i, 3) + "==== 以下為水保申請案件檢核資訊 ====;<br />"
        Next i
        Dim ttt As Date
        Dim ttt1 As Date
        Dim ttt2 As Date
        '第一、連結使用人員資料庫
        Dim SWCConnStrsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        SWCConnStrsetting = ConfigurationManager.ConnectionStrings("TSLMSWCCONN")
        swcconn = New SqlConnection(SWCConnStrsetting.ConnectionString)
        swcconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT SWC00, SWC01, SWC02, SWC04, SWC05, SWC06, SWC07, SWC12, SWC13, SWC15, SWC21, SWC22, SWC24, SWC25, SWC31, SWC32, SWC33, SWC34, SWC36, SWC37, SWC38, SWC39, SWC41, SWC45, SWC51, SWC52, SWC53, SWC55, SWC56, SWC57, SWC58, SWC59, SWC77, SWC78, SWC82, SWC84, SWC85, SWC88, SWC89, SWC90, SWC104, SWC108, SWC109, SWC013TEL, SWC111,SWC125 FROM [SWCSWC] order by [SWC12] asc, [SWC02] desc", swcconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swc")
        '如果沒有資料就離開
        If ds.Tables("swc").Rows.Count = 0 Then
            Exit Sub
        End If
        '有資料開始判斷檢查並做紀錄
        Dim regincount As Integer

        Dim bb As Integer
        For i = 0 To ds.Tables("swc").Rows.Count - 1
            If Left(ds.Tables("swc").Rows(i)("SWC12").ToString, 1) = "北" Then
                regincount = 0
            Else
                regincount = 9
            End If
            'Label1.Text = Label1.Text + ";" + Mid(ds.Tables("ilg").Rows(i)("region").ToString, 2, 1)
            Select Case Mid(ds.Tables("swc").Rows(i)("SWC12").ToString, 2, 1)
                Case "一", "1"
                    regincount = regincount + 0
                Case "二", "2"
                    regincount = regincount + 1
                Case "三", "3"
                    regincount = regincount + 2
                Case "四", "4"
                    regincount = regincount + 3
                Case "五", "5"
                    regincount = regincount + 4
                Case "六", "6"
                    regincount = regincount + 5
                Case "七", "7"
                    regincount = regincount + 6
                Case "八", "8"
                    regincount = regincount + 7
                Case "九", "9"
                    regincount = regincount + 8
            End Select
            '先清調亮燈提示再來判斷有問題的
            updateSWClight("", "", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))

            ''測試中斷點
            'If Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) = "UA1510702004" Then
            '    Dim qq As String = "aaaaa"
            'End If

            Dim mailps As String = ""

            '欄位註記
            'SWC02行政審查案件編號，SWC04案件狀態，SWC05水土保持申請書件名稱，SWC07水土保持申請書件類別，SWC12轄區
            'SWC13水土保持義務人，SWC15聯絡人，SWC16連絡人手機
            'SWC31繳費期限，SWC32補正期限，SWC33繳納日期，SWC34受理日期，SWC37審查費核銷日期，SWC38核定日期，SWC39核定文號
            'SWC42保證金繳交日期，SWC43施工許可證核發日期，SWC51開工日期，SWC52預定完工日期，SWC53停工日期
            'SWC55完工申報日期，SWC57保證金退還日期，SWC58完工日期，SWC59完工證明書核發日期
            'SWC77已廢止已撤銷已失效不予受理不予核定，SWC78已廢止已撤銷已失效不予受理不予核定日期
            'SWC82開工期限,SWC84停工到期日，SWC88審查期限，SWC89暫停審查期限
            'SWC109        ，SWC111定稿本送達大地處日期


            '1.20180517若狀態為已核定，且未填寫開工日期，開工期限第二天起，檢核案件狀態是否為失效
            '201809修正訊息顯示文字
            'If (IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Or ds.Tables("swc").Rows(i)("SWC82").ToString <> "" And ds.Tables("swc").Rows(i)("SWC82").ToString <> "1912/1/1 上午 12:00:00" And ds.Tables("swc").Rows(i)("SWC82").ToString <> "1900/1/1 上午 12:00:00") Then

            If IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Then

            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Or ds.Tables("swc").Rows(i)("SWC82").ToString = "" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '有開工日期
                'For bb = 0 To checkswcswcarray.GetLength(0) - 1
                '    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                '    If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                '        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限未填寫，請修正開工期限!!;<br />"
                '        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限未填寫，請修正開工期限!!;<br />"
                '        updateSWClight("image/red.png", "開工期限未填寫，請修正開工期限!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                '    End If
                'Next
                'Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已至，請修正案件狀態為『失效』!!"

            Else
                '有開工期限。判斷開工期限第二天狀態改了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).AddDays(1)
                If Now.Date >= ttt Then
                    '第二天起
                    'If ds.Tables("swc").Rows(i)("SWC04").ToString <> "失效" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "廢止" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "撤銷" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "施工中" Then
                    If ds.Tables("swc").Rows(i)("SWC04").ToString = "已核定" Then
                        '(原本)不是失效、廢止、已完工、撤銷、施工中 
                        '已核定 
                        Dim checkerr01 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Then
                            '未填寫開工日期
                            checkerr01 = True
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Or ds.Tables("swc").Rows(i)("SWC51").ToString = "" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1900/1/1 上午 12:00:00") Then
                            '未填寫開工日期
                            checkerr01 = True
                        End If
                        If checkerr01 = True Then
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!;<br />"
                                End If
                            Next
                            updateSWClight("image/red.png", "開工期限已至，請修正案件狀態為『失效』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!"
                        End If
                    End If
                End If
            End If

            '2.20180517變更設計案件有填核定日期或核定文號，檢核原案非為退補件、受理中、不予受理、審查中、已核定、不予核定、施工中
            '02.20200807_變更設計有填【核定日期】或【核定文號】，檢核案件狀態非為「退補件」、「受理中」、「審查中」、「已核定」、「施工中」
            Dim needcheck As Boolean = False
            If Len(ds.Tables("swc").Rows(i)("SWC02").ToString) > 12 Then
                '有-1,-2這種才是變更設計案件
                If ds.Tables("swc").Rows(i)("SWC04").ToString = "施工中" Then
                    If (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Then
                        '核定日期是空的,那就看看有沒有核定文號
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC39")) Or ds.Tables("swc").Rows(i)("SWC39").ToString = "" And ds.Tables("swc").Rows(i)("SWC39").ToString = "北市工地審字第號") Then
                            '核定文號也是是空的
                        Else
                            needcheck = True
                        End If
                    Else
                        needcheck = True
                    End If
                End If
            End If
            If needcheck Then
                Dim changecount As Integer = Convert.ToInt32(Right(ds.Tables("swc").Rows(i)("SWC02").ToString, Len(ds.Tables("swc").Rows(i)("SWC02").ToString) - 13))
                Dim q As Integer
                For q = 1 To changecount
                    Dim nowcaseid As String = Left(ds.Tables("swc").Rows(i)("SWC02").ToString, 12)
                    Dim revcaseid As String = Left(ds.Tables("swc").Rows(i + q)("SWC02").ToString, 12)
                    Dim tta As String = ds.Tables("swc").Rows(i)("SWC02")
                    Dim ttb As String = Replace(ds.Tables("swc").Rows(i)("SWC38"), "/", "-")
                    If nowcaseid = revcaseid Then
                        'If ds.Tables("swc").Rows(i + q)("SWC04").ToString = "退補件" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "受理中" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "不予受理" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "審查中" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "已核定" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "不予核定" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "施工中" Then
                        If ds.Tables("swc").Rows(i + q)("SWC04").ToString = "退補件" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "受理中" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "審查中" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "已核定" Or ds.Tables("swc").Rows(i + q)("SWC04").ToString = "施工中" Then
                            '前案不是已變更 
                            '20190315加上SWC38核定日期   
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 變更設計已於 (" + ttb + ") 核定，請修正前案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 變更設計已於 (" + ttb + ") 核定，請修正前案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!;<br />"
                                End If
                            Next
                            updateSWClight("image/red.png", "變更設計已於 (" + ttb + ") 核定，請修正原案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 變更設計已於 (" + ttb + ") 核定，請修正原案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!;"
                        End If
                    End If
                Next
            End If
            needcheck = False

            '3.20180517若狀態為停工中，停工期限隔天起，檢核是否為失效
            '201809修正訊息顯示文字
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC84")) Then

            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC84")) Or ds.Tables("swc").Rows(i)("SWC84").ToString = "" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '沒有停工期限
            Else
                '有停工期限。判斷第二天狀態改了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).AddDays(1)
                If Now.Date >= ttt Then
                    '第二天起
                    'If ds.Tables("swc").Rows(i)("SWC04").ToString <> "失效" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "廢止" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "撤銷" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "施工中" Then
                    If ds.Tables("swc").Rows(i)("SWC04").ToString = "停工中" Then
                        '(原本)不是失效、廢止、已完工、撤銷、施工中 
                        '停工中
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                            'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 停工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 停工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!;<br />"
                            End If
                        Next
                        updateSWClight("image/red.png", "停工期限已至，請修正案件狀態為『失效』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 停工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!"

                    End If
                End If
            End If

            '4.20180517若狀態為施工中，預計完工期限隔天起，檢核是否為失效(完工期限已刪除，先改成預計完工期限判斷)
            '201809修正訊息顯示文字
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Then

            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Or ds.Tables("swc").Rows(i)("SWC52").ToString = "" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '沒有完工期限
            Else
                '有完工期限。判斷第二天狀態改了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).AddDays(1)
                If Now.Date >= ttt Then
                    '第二天起
                    If ((IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Or ds.Tables("swc").Rows(i)("SWC52").ToString = "" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1900/1/1 上午 12:00:00")) Then
                        '完工申報日期沒有填，而且完工日期也沒填，就要來判斷了
                        'If ds.Tables("swc").Rows(i)("SWC04").ToString <> "失效" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "廢止" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "撤銷" Then
                        If ds.Tables("swc").Rows(i)("SWC04").ToString = "施工中" Then
                            '(原本)不是失效、廢止、已完工、撤銷 
                            '施工中
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 完工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 完工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!;<br />"
                                End If
                            Next
                            updateSWClight("image/red.png", "完工期限已至，請修正案件狀態為『失效』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 完工期限已於" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).ToString("yyyy-MM-dd") + "到期，請修正案件狀態為『失效』!!"

                        End If
                    End If
                End If
            End If

            '5.20180517若狀態為退補件，A.補正期限5天前，B.當天，檢核補正期限有值且第二次補證期限無值，則進行提醒
            Dim checkerr05 As Boolean = False
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC32")) Then
                '補正期限沒填
            ElseIf ds.Tables("swc").Rows(i)("SWC32").ToString = "" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1900/1/1 上午 12:00:00" Then
                '補正期限沒填
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString = "退補件") Then
                    '退補件才要判斷
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC104")) Then
                        checkerr05 = True
                    ElseIf ds.Tables("swc").Rows(i)("SWC104").ToString = "" Or ds.Tables("swc").Rows(i)("SWC104").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC104").ToString = "1900/1/1 上午 12:00:00" Then
                        checkerr05 = True
                    End If
                    If checkerr05 = True Then
                        '需要判斷
                        ttt = DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).AddDays(-5)
                        If Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString) Then
                            'A.補正期限5天前，B.當天
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString) Then
                                '寄mail給技師
                                'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                                'End If
                                '寄MAIL給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                    '義務人的聯絡人信箱是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                    '義務人的聯絡人信箱是空的
                                Else
                                    'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                    mailps = ds.Tables("swc").Rows(i)("SWC15").ToString + " 您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;<br />"
                                    mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                    'End If
                                End If
                                '發簡訊給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                    '義務人的聯絡人電話是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                    '義務人的聯絡人電話是空的
                                Else
                                    'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                    'sogi = sogi.Replace("-", "")
                                    'If Left(sogi, 2) = "09" Then
                                    '    '發簡訊至聯絡人手機：SWC16
                                    '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                    '    Dim tSWC16 = Left(sogi, 10)
                                    '    Dim tPhoneNo As String = tSWC16
                                    '    Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + tSWC05 + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;"
                                    '    SendSMS(tPhoneNo, tSMSText)
                                    'Else
                                    '    '義務人的聯絡人手機號碼亂填，無法發送
                                    'End If
									'修改為複數義務人
									Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
									sogi.Replace("-", "")
									Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
									Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
									Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + tSWC05 + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;"
									SendSMS_Arr(tPhoneNoArr, tSMSText)
                                End If
                            End If
                        End If
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).ToString("yyyy-MM-dd") + "到期!!"
                    End If
                End If
            End If


            '6.20180517若狀態為退補件，第二次補證期限A.補正期限5天前，B.當天，進行提醒
            Dim checkerr06 As Boolean = False
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC104")) Then
                '第二次補正期限沒填
            ElseIf ds.Tables("swc").Rows(i)("SWC104").ToString = "" Or ds.Tables("swc").Rows(i)("SWC104").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC104").ToString = "1900/1/1 上午 12:00:00" Then
                '第二次補正期限沒填
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString = "退補件") Then
                    '退補件才要判斷
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).AddDays(-5)
                    If Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString) Then
                        'A.第二次補正期限5天前，B.當天
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                            'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />"
                                End If
                            End If
                        Next
                        updateSWClight("image/red.png", "尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                        If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString) Then
                            '寄mail給技師
                            'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                            mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;<br />"
                            mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                            'End If
                            '寄MAIL給義務人
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                '義務人的聯絡人信箱是空的
                            ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                '義務人的聯絡人信箱是空的
                            Else
                                'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                mailps = ds.Tables("swc").Rows(i)("SWC15").ToString + " 您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                'End If
                            End If
                            '發簡訊給義務人
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                '義務人的聯絡人電話是空的
                            ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                '義務人的聯絡人電話是空的
                            Else
                                'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                'sogi = sogi.Replace("-", "")
                                'If Left(sogi, 2) = "09" Then
                                '    '發簡訊至聯絡人手機：SWC16
                                '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                '    Dim tSWC16 = Left(sogi, 10)
                                '    Dim tPhoneNo As String = tSWC16
                                '    Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + tSWC05 + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;"
                                '    SendSMS(tPhoneNo, tSMSText)
                                'Else
                                '    '義務人的聯絡人手機號碼亂填，無法發送
                                'End If
								'修改為複數義務人
								Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
								sogi.Replace("-", "")
								Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
								Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
								Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + tSWC05 + "】尚未補件， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + ")內完成補正，逾期將不予受理!!;"
								SendSMS_Arr(tPhoneNoArr, tSMSText)
                            End If
                        End If
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC104").ToString).ToString("yyyy-MM-dd") + "到期!!"
                    End If
                End If
            End If

            '7.20180517若狀態為"受理中"，審查費繳納期限A.5天前，B.當天，檢核審查費繳納日期是否有值
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC31")) Then
                '審查費繳納期限沒填
            ElseIf ds.Tables("swc").Rows(i)("SWC31").ToString = "" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1900/1/1 上午 12:00:00" Then
                '審查費繳納期限沒填
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "受理中") Then
                    '受理中的才去判斷所以這邊要不是受理的跳過
                Else
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).AddDays(-5)
                    If Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString) Then
                        '審查費繳納期限A.5天前，B.當天
                        Dim chkerr07 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC33")) Then
                            '審查費繳納日期沒填
                            chkerr07 = True
                        ElseIf ds.Tables("swc").Rows(i)("SWC33").ToString = "" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1900/1/1 上午 12:00:00" Then
                            '審查費繳納日期沒填
                            chkerr07 = True
                        End If
                        If chkerr07 = True Then
                            '審查費繳納日期沒填 
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未繳納審查費，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未繳納審查費，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + "到期!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "尚未繳納審查費，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + "到期!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString) Then
                                '寄mail給技師
                                'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未繳納審查費， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + ")內繳納，逾期將不予受理!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                                'End If
                                '寄MAIL給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                    '義務人的聯絡人信箱是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                    '義務人的聯絡人信箱是空的
                                Else
                                    'if(mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                    mailps = ds.Tables("swc").Rows(i)("SWC15").ToString + " 您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未繳納審查費， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + ")內繳納，逾期將不予受理!!;<br />"
                                    mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                    'end If
                                End If
                                '發簡訊給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                    '義務人的聯絡人電話是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                    '義務人的聯絡人電話是空的
                                Else
                                    'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                    'sogi = sogi.Replace("-", "")
                                    'If Left(sogi, 2) = "09" Then
                                    '    '發簡訊至聯絡人手機：SWC16
                                    '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                    '    Dim tSWC16 = Left(sogi, 10)
                                    '    Dim tPhoneNo As String = tSWC16
                                    '    Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未繳納審查費， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + ")內繳納，逾期將不予受理!!"
                                    '    SendSMS(tPhoneNo, tSMSText)
                                    'Else
                                    '    '義務人的聯絡人手機號碼亂填，無法發送
                                    'End If
									'修改為複數義務人
									Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
									sogi.Replace("-", "")
									Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
									Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
									Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未繳納審查費， 請於期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + ")內繳納，逾期將不予受理!!"
									SendSMS_Arr(tPhoneNoArr, tSMSText)
                                End If
                            End If
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未繳納審查費，將於" + DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).ToString("yyyy-MM-dd") + "到期!!"
                        End If
                    End If
                End If
            End If

            '8.20180517若狀態為受理中(或審查中20161228不判斷受理中)，審查費繳納日期+7天起，檢核受理日期是否有值
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC33")) Then
                '審查費繳納日期沒填
            ElseIf ds.Tables("swc").Rows(i)("SWC33").ToString = "" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1900/1/1 上午 12:00:00" Then
                '審查費繳納日期沒填
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "受理中") Then
                    '受理中的才去判斷所以這邊要不是受理的跳過
                Else
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC33").ToString).AddDays(7)
                    If Now.Date >= ttt Then
                        '審查費繳納日期+7天起
                        Dim checkerr08 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC34")) Then
                            checkerr08 = True
                            '受理日期沒填
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC34")) Or ds.Tables("swc").Rows(i)("SWC34").ToString = "" Or ds.Tables("swc").Rows(i)("SWC34").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC34").ToString = "1900/1/1 上午 12:00:00") Then
                            checkerr08 = True
                            '受理日期沒填 
                        End If
                        If checkerr08 = True Then
                            '20190315加上審查費繳納日期SWC33
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC33").ToString).ToString("yyyy-MM-dd") + ") 繳交審查費，尚未委外，請確認辦理情形!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC33").ToString).ToString("yyyy-MM-dd") + ") 繳交審查費，尚未委外，請確認辦理情形!!;<br />"
                                End If
                            Next
                            updateSWClight("image/red.png", " 已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC33").ToString).ToString("yyyy-MM-dd") + ") 繳交審查費，尚未委外，請確認辦理情形!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ")  已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC33").ToString).ToString("yyyy-MM-dd") + ") 繳交審查費，尚未委外，請確認辦理情形!!"

                        End If
                    End If
                End If
            End If

            '9-1.20180517若狀態為審查中，審查期限A.5天前與當天，檢核"建議核定日期"是否有值
            '9-1.20180618若狀態為審查中，審查期限A.14天、5天前與當天，檢核"建議核定日期"是否有值
            'SWC88：審查期限(日期)
            'SWC109：建議核定日期(日期)
            '要多寄給陳世豪(2022-02-24)
            If ds.Tables("swc").Rows(i)("SWC04").ToString = "審查中" Then
                If IsDBNull(ds.Tables("swc").Rows(i)("SWC88")) Then
                    '沒有審查期限
                ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC88")) Or ds.Tables("swc").Rows(i)("SWC88").ToString = "" Or ds.Tables("swc").Rows(i)("SWC88").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC88").ToString = "1900/1/1 上午 12:00:00") Then
                    '沒有審查期限
                Else
                    '有審查期限。判斷審查期限A.5天前與當天
                    '20180618奉方科指示，要多加14天前也判斷
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC88")).AddDays(-5)
                    Dim sssttt As Date = DateValue(ds.Tables("swc").Rows(i)("SWC88")).AddDays(-14)
                    If Now.Date >= sssttt Or Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC88")) Then
                        'A.14天、5天前與當天，檢核"建議核定日期"是否有值
                        Dim checkerr0901 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC109")) Then
                            '沒有建議核定日期
                            checkerr0901 = True
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC109")) Or ds.Tables("swc").Rows(i)("SWC109").ToString = "" Or ds.Tables("swc").Rows(i)("SWC109").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC109").ToString = "1900/1/1 上午 12:00:00") Then
                            '沒有建議核定日期
                            checkerr0901 = True
                        End If
                        If checkerr0901 = True Then
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 0) = "陳世豪" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If Now.Date = sssttt Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC88")) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查期限將於" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "到期，請確認審查單位審查進度!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查期限將於" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "到期，請確認審查單位審查進度!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "審查期限將於" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "到期，請確認審查單位審查進度!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            'A.14天、5天與當天，才寄信
                            If Now.Date = sssttt Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC88")) Then
                                '寄信給審查公會，先抓審查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Then
                                    '審查公會信箱沒填
                                ElseIf ds.Tables("swc").Rows(i)("SWC22").ToString = "" Then
                                    '審查公會信箱沒填
                                Else
                                    '連線到137的帳號資料庫去抓審查公會的MAIL
                                    '第一、連結使用人員資料庫
                                    Dim geoinfoconnstr As ConnectionStringSettings
                                    Dim geoinfoconn As SqlConnection
                                    geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                                    geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                                    geoinfoconn.Open()
                                    '第二、執行SQL指令，取出資料
                                    Dim geoinfocommand As SqlCommand
                                    Dim geoinfodareader As SqlDataReader
                                    Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC22").ToString + "'"
                                    geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                                    geoinfodareader = geoinfocommand.ExecuteReader()
                                    While geoinfodareader.Read
                                        'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                        mailps = "提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未審查完成，請於審查期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + ")完成，逾期將依契約辦理!!;<br />"
                                        mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                                        'End If
                                    End While
                                    '關閉137的連線資料庫元件
                                    geoinfodareader.Close()
                                    geoinfocommand.Dispose()
                                    geoinfoconn.Close()
                                    geoinfoconn.Dispose()
                                End If
                            End If
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查期限將於" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "到期，請確認審查單位審查進度!!"
                        End If
                    End If
                End If
            End If
            '9-2.20180517若狀態為審查中，審查期限B.隔天與隔天+7n天，檢核"建議核定日期"是否有值
            '201809修改文字
            'SWC88：審查期限(日期)
            'SWC109：建議核定日期(日期)
            '要多寄給陳世豪(2022-02-24)
            If ds.Tables("swc").Rows(i)("SWC04").ToString = "審查中" Then
                If IsDBNull(ds.Tables("swc").Rows(i)("SWC88")) Then
                    '沒有審查期限
                ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC88")) Or ds.Tables("swc").Rows(i)("SWC88").ToString = "" Or ds.Tables("swc").Rows(i)("SWC88").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC88").ToString = "1900/1/1 上午 12:00:00") Then
                    '沒有審查期限
                Else
                    '有審查期限。判斷審查期限A.隔天與隔天+7n天
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC88"))
                    Dim aa As Integer = 0
                    If ttt <= Now.Date Then
                        '餘數是1代表差一天的每星期
                        'datediff 從DATE1到DATE2要幾天，有正負
                        aa = DateDiff(DateInterval.Day, ttt, Now.Date) Mod 7

                        Dim checkerr0901 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC109")) Then
                            '沒有建議核定日期
                            checkerr0901 = True
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC109")) Or ds.Tables("swc").Rows(i)("SWC109").ToString = "" Or ds.Tables("swc").Rows(i)("SWC109").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC109").ToString = "1900/1/1 上午 12:00:00") Then
                            '沒有建議核定日期
                            checkerr0901 = True
                        End If
                        If checkerr0901 = True Then
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 0) = "陳世豪" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If aa = 1 Then
                                        'A.隔天與隔天+7n天
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查期限" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "已逾期，請確認審查單位審查進度並依合約暫停輪值一次!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查期限" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "已逾期，請確認審查單位審查進度並依合約暫停輪值一次!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "審查期限" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "已逾期，請確認審查單位審查進度並依合約暫停輪值一次!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            If aa = 1 Then
                                'B.隔天與隔天+7n天
                                '寄信給審查公會，先抓審查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Then
                                    '審查公會信箱沒填
                                ElseIf ds.Tables("swc").Rows(i)("SWC22").ToString = "" Then
                                    '審查公會信箱沒填
                                Else
                                    '連線到137的帳號資料庫去抓審查公會的MAIL
                                    '第一、連結使用人員資料庫
                                    Dim geoinfoconnstr As ConnectionStringSettings
                                    Dim geoinfoconn As SqlConnection
                                    geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                                    geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                                    geoinfoconn.Open()
                                    '第二、執行SQL指令，取出資料
                                    Dim geoinfocommand As SqlCommand
                                    Dim geoinfodareader As SqlDataReader
                                    Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC22").ToString + "'"
                                    geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                                    geoinfodareader = geoinfocommand.ExecuteReader()
                                    While geoinfodareader.Read
                                        'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                        mailps = "提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】審查期限" + DateValue(ds.Tables("swc").Rows(i)("SWC88").ToString).ToString("yyyy-MM-dd") + "已逾且未完成審查，將依合約暫停後續審查輪值一次，請盡速於七日內完成本案審查，如仍逾期將續依契約辦理!!;<br />"
                                        mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                                        'End If
                                    End While
                                    '關閉137的連線資料庫元件
                                    geoinfodareader.Close()
                                    geoinfocommand.Dispose()
                                    geoinfoconn.Close()
                                    geoinfoconn.Dispose()
                                End If
                            End If
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查期限已逾，請確認審查單位審查進度並依合約暫停輪值一次!!"
                        End If
                    End If
                End If
            End If

            '10.2018.05.17若狀態為暫停審查，暫停審查期限A.五天前B.當日，提醒一下快到期了
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC89")) Then

            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC89")) Or ds.Tables("swc").Rows(i)("SWC89").ToString = "" Or ds.Tables("swc").Rows(i)("SWC89").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC89").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。

            Else
                '有暫停審查期限。判斷暫停審查期限A.五天前B.當日
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).AddDays(-5)
                If Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89")) Then
                    'A.五天前B.當日
                    If ds.Tables("swc").Rows(i)("SWC04").ToString = "暫停審查" Then
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                            'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89")) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 暫停審查期限將於(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")到期，請確認是否續行審查!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 暫停審查期限將於(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")到期，請確認是否續行審查!!;<br />"
                                End If
                            End If
                        Next
                        updateSWClight("image/red.png", "暫停審查期限將至，請確認是否續行審查!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                        If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89")) Then
                            '寄MAIL給技師
                            'If (mailonecheck.Checked = True Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89"))) And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                            mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】暫停審查期限將至，請於暫停審查期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")內提送審查單位續審!!;<br />"
                            mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                            'End If
                            '寄信給審查公會，先抓審查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Then
                                '審查公會信箱沒填
                            ElseIf ds.Tables("swc").Rows(i)("SWC22").ToString = "" Then
                                '審查公會信箱沒填
                            Else
                                '連線到137的帳號資料庫去抓審查公會的MAIL
                                '第一、連結使用人員資料庫
                                Dim geoinfoconnstr As ConnectionStringSettings
                                Dim geoinfoconn As SqlConnection
                                geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                                geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                                geoinfoconn.Open()
                                '第二、執行SQL指令，取出資料
                                Dim geoinfocommand As SqlCommand
                                Dim geoinfodareader As SqlDataReader
                                Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC22").ToString + "'"
                                geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                                geoinfodareader = geoinfocommand.ExecuteReader()
                                While geoinfodareader.Read
                                    'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                    mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】暫停審查期限將至，請於暫停審查期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")內提送審查單位續審!!;<br />"
                                    mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                                    'End If
                                End While
                                '關閉137的連線資料庫元件
                                geoinfodareader.Close()
                                geoinfocommand.Dispose()
                                geoinfoconn.Close()
                                geoinfoconn.Dispose()
                            End If
                            '寄MAIL給義務人
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                '義務人的聯絡人信箱是空的
                            ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                '義務人的聯絡人信箱是空的
                            Else
                                'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】暫停審查期限將至，請於暫停審查期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")內提送審查單位續審!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                'End If
                            End If
                            '發簡訊給義務人
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                '義務人的聯絡人電話是空的
                            ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                '義務人的聯絡人電話是空的
                            Else
                                'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                'sogi = sogi.Replace("-", "")
                                'If Left(sogi, 2) = "09" Then
                                '    '發簡訊至聯絡人手機：SWC16
                                '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                '    Dim tSWC16 = Left(sogi, 10)
                                '    Dim tPhoneNo As String = tSWC16
                                '    Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】暫停審查期限將至，請於暫停審查期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")內提送審查單位續審!!"
                                '    SendSMS(tPhoneNo, tSMSText)
                                'Else
                                '    '義務人的聯絡人手機號碼亂填，無法發送
                                'End If
								Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
								sogi.Replace("-", "")
								Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
								Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
								Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】暫停審查期限將至，請於暫停審查期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")內提送審查單位續審!!"
								SendSMS_Arr(tPhoneNoArr, tSMSText)
                            End If
                        End If
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 暫停審查期限將於(" + DateValue(ds.Tables("swc").Rows(i)("SWC89").ToString).ToString("yyyy-MM-dd") + ")到期，請確認是否續行審查!!"
                    End If
                End If
            End If

            '11.若狀態為審查中，審查紀錄紀錄表單中"暫存審查紀錄"表單建立3天後，開始提醒有表單尚未送出
            '201809修改文字
            '先寫，等倍瑩8那邊的書件把審查紀錄表在第一次新增進去INSERT的時候有存DTLA033(第一次暫存日期)再執行
            If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "審查中") Then
                '不是審查中不判斷
            Else
                Dim ckeckerr11 As Boolean = False
                Dim insertdate As Date = DateValue("1912-01-01")
                Dim DTLA000ID As String = ""
                '去審查紀錄表那邊判斷
                '連線到55的TCGESWC
                '第一、連結審查檢查紀錄表
                Dim tcgeswcconnstr As ConnectionStringSettings
                Dim tcgeswcconn As SqlConnection
                tcgeswcconnstr = ConfigurationManager.ConnectionStrings("SWCConnStr")
                tcgeswcconn = New SqlConnection(tcgeswcconnstr.ConnectionString)
                tcgeswcconn.Open()
                '第二、執行SQL指令，取出資料
                Dim tcgeswccommand As SqlCommand
                Dim tcgeswcdareader As SqlDataReader
                Dim tcgeswcselectstring As String = "SELECT DTLA000, DTLA033 FROM [SWCDTL01] where DATALOCK <> 'Y' and SWC000 = '" + ds.Tables("swc").Rows(i)("SWC00").ToString + "' order by DTLA033"
                tcgeswccommand = New SqlCommand(tcgeswcselectstring, tcgeswcconn)
                tcgeswcdareader = tcgeswccommand.ExecuteReader()
                While tcgeswcdareader.Read
                    If IsDBNull(tcgeswcdareader.Item("DTLA033")) Then
                        '沒有暫存審查紀錄表單的建立日期
                    ElseIf (IsDBNull(tcgeswcdareader.Item("DTLA033")) Or tcgeswcdareader.Item("DTLA033").ToString = "" Or tcgeswcdareader.Item("DTLA033").ToString = "1912/1/1 上午 12:00:00" Or tcgeswcdareader.Item("DTLA033").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有暫存審查紀錄表單的建立日期
                    Else
                        If insertdate < DateValue(tcgeswcdareader.Item("DTLA033").ToString()) Then
                            insertdate = DateValue(tcgeswcdareader.Item("DTLA033").ToString())
                            DTLA000ID = tcgeswcdareader.Item("DTLA000").ToString()
                            Exit While
                        End If
                    End If
                End While
                '關閉137的連線資料庫元件
                tcgeswcdareader.Close()
                tcgeswccommand.Dispose()
                tcgeswcconn.Close()
                tcgeswcconn.Dispose()
                If insertdate > DateValue("1912-01-01") Then
                    '有暫存審查紀錄表單的建立日期，3天後開始提醒有暫存表單未送出
                    ttt = insertdate.AddDays(3)
                    If Now.Date >= ttt Then
                        ckeckerr11 = True
                    End If
                End If
                If ckeckerr11 = True Then
                    '寄信給審查公會，先抓審查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Then
                        '審查公會信箱沒填
                    ElseIf ds.Tables("swc").Rows(i)("SWC22").ToString = "" Then
                        '審查公會信箱沒填
                    Else
                        '連線到137的帳號資料庫去抓審查公會的MAIL
                        '第一、連結使用人員資料庫
                        Dim geoinfoconnstr As ConnectionStringSettings
                        Dim geoinfoconn As SqlConnection
                        geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                        geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                        geoinfoconn.Open()
                        '第二、執行SQL指令，取出資料
                        Dim geoinfocommand As SqlCommand
                        Dim geoinfodareader As SqlDataReader
                        Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC22").ToString + "'"
                        geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                        geoinfodareader = geoinfocommand.ExecuteReader()
                        While geoinfodareader.Read
                            'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                            mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】有暫存審查表單(" + DTLA000ID + ") 未送出，請確認!!;<br />"
                            mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                            'End If
                        End While
                        '關閉137的連線資料庫元件
                        geoinfodareader.Close()
                        geoinfocommand.Dispose()
                        geoinfoconn.Close()
                        geoinfoconn.Dispose()
                    End If
                    '20180613 no light
                    'updateSWClight("image/red.png", "有暫存審查表單(" + DTLA000ID + ") 未送出，請確認!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 有暫存審查表單(" + DTLA000ID + ") 未送出，請確認!!"
                End If
            End If

            '12.若狀態為審查中，審查紀錄紀錄表單中"補正期限"A.5天前B.當天，提醒期限到期
            '201809修改審查公會文字
            If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "審查中") Then
                '不是審查中不判斷
            Else
                Dim ckeckerr12 As Boolean = False
                Dim fixdate As Date = DateValue("1912-01-01")
                '去審查紀錄表那邊判斷
                '連線到55的TCGESWC
                '第一、連結審查檢查紀錄表
                Dim tcgeswcconnstr As ConnectionStringSettings
                Dim tcgeswcconn As SqlConnection
                tcgeswcconnstr = ConfigurationManager.ConnectionStrings("SWCConnStr")
                tcgeswcconn = New SqlConnection(tcgeswcconnstr.ConnectionString)
                tcgeswcconn.Open()
                '第二、執行SQL指令，取出資料
                Dim tcgeswccommand As SqlCommand
                Dim tcgeswcdareader As SqlDataReader
                Dim tcgeswcselectstring As String = "SELECT A.DTLA003 FROM [SWCDTL01] A left join ShareFiles B on A.SWC000 = B.SWC000 and '00'+convert(nvarchar,A.DTLA006) = B.SFTYPE left join ShareFiles C on A.SWC000 = C.SWC000 and C.SFTYPE = '099' where A.DATALOCK = 'Y' and (ISNULL(B.SFName,'') != ''  or ISNULL(C.SFName,'') != '') and A.SWC000 = '" + ds.Tables("swc").Rows(i)("SWC00").ToString + "' order by A.DTLA003 DESC"
                tcgeswccommand = New SqlCommand(tcgeswcselectstring, tcgeswcconn)
                tcgeswcdareader = tcgeswccommand.ExecuteReader()
                While tcgeswcdareader.Read
                    If IsDBNull(tcgeswcdareader.Item("DTLA003")) Then
                        '沒有補正期限日期
                    ElseIf (IsDBNull(tcgeswcdareader.Item("DTLA003")) Or tcgeswcdareader.Item("DTLA003").ToString = "" Or tcgeswcdareader.Item("DTLA003").ToString = "1912/1/1 上午 12:00:00" Or tcgeswcdareader.Item("DTLA003").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有補正期限日期
                    Else
                        If fixdate < DateValue(tcgeswcdareader.Item("DTLA003").ToString()) Then
                            fixdate = DateValue(tcgeswcdareader.Item("DTLA003").ToString())
                            Exit While
                        End If
                    End If
                End While
                '關閉137的連線資料庫元件
                tcgeswcdareader.Close()
                tcgeswccommand.Dispose()
                tcgeswcconn.Close()
                tcgeswcconn.Dispose()
                If fixdate > DateValue("1912-01-01") Then
                    '有暫存審查紀錄表單的建立日期，3天後開始提醒有暫存表單未送出
                    ttt = fixdate.AddDays(-5)
                    If Now.Date >= ttt Then
                        '20180613 no light
                        'updateSWClight("image/red.png", "審查補正期限將至，請於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查補正期限將至，請於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正!!"
                        If Now.Date = ttt Or Now.Date = fixdate Then
                            ckeckerr12 = True
                        End If
                    End If
                End If
                If ckeckerr12 = True Then
                    '寄MAIL給技師
                    'If (mailonecheck.Checked = True Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89"))) And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                    mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】審查補正期限將至，請於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正!!;<br />"
                    mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                    'End If
                    '寄信給審查公會，先抓審查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Then
                        '審查公會信箱沒填
                    ElseIf ds.Tables("swc").Rows(i)("SWC22").ToString = "" Then
                        '審查公會信箱沒填
                    Else
                        '連線到137的帳號資料庫去抓審查公會的MAIL
                        '第一、連結使用人員資料庫
                        Dim geoinfoconnstr As ConnectionStringSettings
                        Dim geoinfoconn As SqlConnection
                        geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                        geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                        geoinfoconn.Open()
                        '第二、執行SQL指令，取出資料
                        Dim geoinfocommand As SqlCommand
                        Dim geoinfodareader As SqlDataReader
                        Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC22").ToString + "'"
                        geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                        geoinfodareader = geoinfocommand.ExecuteReader()
                        While geoinfodareader.Read
                            'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                            mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】審查補正期限將至，請確認技師是否於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正完成!!;<br />"
                            mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                            'End If
                        End While
                        '關閉137的連線資料庫元件
                        geoinfodareader.Close()
                        geoinfocommand.Dispose()
                        geoinfoconn.Close()
                        geoinfoconn.Dispose()
                    End If
                    '寄MAIL給義務人
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                        '義務人的聯絡人信箱是空的
                    ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                        '義務人的聯絡人信箱是空的
                    Else
                        'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                        mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】審查補正期限將至，請於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正!!;<br />"
                        mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                        'End If
                    End If
                    '發簡訊給義務人
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                        '義務人的聯絡人電話是空的
                    ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                        '義務人的聯絡人電話是空的
                    Else
                        'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                        'sogi = sogi.Replace("-", "")
                        'If Left(sogi, 2) = "09" Then
                        '    '發簡訊至聯絡人手機：SWC16
                        '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                        '    Dim tSWC16 = Left(sogi, 10)
                        '    Dim tPhoneNo As String = tSWC16
                        '    Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】審查補正期限將至，請於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正!!"
                        '    SendSMS(tPhoneNo, tSMSText)
                        'Else
                        '    '義務人的聯絡人手機號碼亂填，無法發送
                        'End If
						Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
						sogi.Replace("-", "")
						Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
						Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
						Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】審查補正期限將至，請於(" + fixdate.ToString("yyyy-MM-dd") + ") 前補正!!"
						SendSMS_Arr(tPhoneNoArr, tSMSText)
                    End If
                End If
            End If

            '21.201809若狀態為審查中，公會建議核定日期有值，7天後檢核"定稿本送達大地處日期"是否有值
            '21.20200909_停用此檢核
            'SWC38：核定日期(日期)
            'SWC109：建議核定日期(日期)
            'SWC111：定稿本送達大地處日期
            'If IsDBNull(ds.Tables("swc").Rows(i)("SWC109")) Then
            '        '沒有建議核定日期
            '    ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC109")) Or ds.Tables("swc").Rows(i)("SWC109").ToString = "" Or ds.Tables("swc").Rows(i)("SWC109").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC109").ToString = "1900/1/1 上午 12:00:00") Then
            '        '沒有建議核定日期
            '    Else
            '        If (ds.Tables("swc").Rows(i)("SWC04").ToString = "審查中") Then
            '            '狀態為審查中
            '            ttt = DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).AddDays(7)
            '            If Now.Date >= ttt Then
            '                '7天後開始檢核
            '                Dim checkerr21 As Boolean = False
            '                If IsDBNull(ds.Tables("swc").Rows(i)("SWC111")) Then
            '                    '沒有定稿本送達大地處日期
            '                    checkerr21 = True
            '                ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC111")) Or ds.Tables("swc").Rows(i)("SWC111").ToString = "" Or ds.Tables("swc").Rows(i)("SWC111").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC111").ToString = "1900/1/1 上午 12:00:00") Then
            '                    '沒有定稿本送達大地處日期
            '                    checkerr21 = True
            '                End If
            '                If checkerr21 = True Then
            '                    '沒有定稿本送達大地處日期
            '                    For bb = 0 To checkswcswcarray.GetLength(0) - 1
            '                        'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
            '                        If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
            '                            checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，公會已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認公會是否確實寄出定稿本!!;<br />"
            '                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，公會已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認公會是否確實寄出定稿本!!!!;<br />"
            '                        End If
            '                    Next
            '                    updateSWClight("image/red.png", "公會已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認公會是否確實寄出定稿本!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
            '                    '寄信給審查公會，先抓審查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
            '                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Then
            '                        '審查公會信箱沒填
            '                    ElseIf ds.Tables("swc").Rows(i)("SWC22").ToString = "" Then
            '                        '審查公會信箱沒填
            '                    Else
            '                        '連線到137的帳號資料庫去抓審查公會的MAIL
            '                        '第一、連結使用人員資料庫
            '                        Dim geoinfoconnstr As ConnectionStringSettings
            '                        Dim geoinfoconn As SqlConnection
            '                        geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
            '                        geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
            '                        geoinfoconn.Open()
            '                        '第二、執行SQL指令，取出資料
            '                        Dim geoinfocommand As SqlCommand
            '                        Dim geoinfodareader As SqlDataReader
            '                        Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC22").ToString + "'"
            '                        geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
            '                        geoinfodareader = geoinfocommand.ExecuteReader()
            '                        While geoinfodareader.Read
            '                            'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
            '                            mailps = "提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 由公會建議核定，惟大地處尚未收到定稿本文件，請確認是否確實寄出，如持續逾期將依契約辦理!!;<br />"
            '                            mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
            '                            'End If
            '                        End While
            '                        '關閉137的連線資料庫元件
            '                        geoinfodareader.Close()
            '                        geoinfocommand.Dispose()
            '                        geoinfoconn.Close()
            '                        geoinfoconn.Dispose()
            '                    End If
            '                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，公會已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認公會是否確實寄出定稿本!!"
            '                End If
            '            End If
            '        End If
            '    End If

            '13.20180517若狀態為審查中，公會建議核定日期有值，14天後檢核"核定日期"是否有值
            '13.201809若狀態為審查中，定稿本送達大地處日期有值，7天後檢核"核定日期"是否有值
            '13.20200909_若狀態為「審查中」，公會建議核定日期有值，7天後檢核「核定日期」是否有值
            '13.20201201_若狀態為「審查中」，「建議核定補正期限」有值，「5天前」及「當天」檢核提醒
            'SWC38：核定日期(日期)
            'SWC109：建議核定日期(日期)
            'SWC111：定稿本送達大地處日期
            'SWC125：建議核定補正期限
            Dim tSWC004 = ds.Tables("swc").Rows(i)("SWC04").ToString
            Dim tSWC038 = ds.Tables("swc").Rows(i)("SWC38").ToString
            Dim tSWC109 = ds.Tables("swc").Rows(i)("SWC109").ToString
            Dim tSWC125 = "" + ds.Tables("swc").Rows(i)("SWC125").ToString
            If tSWC004 = "審查中" Then
                If tSWC125 = "" Or tSWC125 = "1912/1/1 上午 12:00:00" Or tSWC125 = "1900/1/1 上午 12:00:00" Then
                Else
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC125").ToString).AddDays(-5)
                    ttt1 = DateValue(ds.Tables("swc").Rows(i)("SWC125").ToString)
                    If Now.Date >= ttt Or ttt1 = Now.Date Then
                        If tSWC004 = "" Then
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，建議核定補正期限將於(" + DateValue(ds.Tables("swc").Rows(i)("SWC125").ToString).ToString("yyyy-MM-dd") + ") 到期，請確認是否補件完成!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，建議核定補正期限將於(" + DateValue(ds.Tables("swc").Rows(i)("SWC125").ToString).ToString("yyyy-MM-dd") + ") 到期，請確認是否補件完成!!;<br />"
                                End If
                            Next
                            '轄區【水土保持計畫】，審查單位已建議核定審查期限已至，請確認是否已辦理核定
                            '轄區【水土保持計畫】，建議核定補正期限將於【建議核定補正期限】到期，請確認是否補件完成
                            updateSWClight("image/red.png", "建議核定補正期限已至，請確認是否補件完成!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，建議核定補正期限將於(" + DateValue(ds.Tables("swc").Rows(i)("SWC125").ToString).ToString("yyyy-MM-dd") + ") 到期，請確認是否補件完成!!"
                        End If
                    End If
                End If
            End If

            'If IsDBNull(ds.Tables("swc").Rows(i)("SWC111")) Then
            '    '沒有定稿本送達大地處日期
            'ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC111")) Or ds.Tables("swc").Rows(i)("SWC111").ToString = "" Or ds.Tables("swc").Rows(i)("SWC111").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC111").ToString = "1900/1/1 上午 12:00:00") Then
            '    '沒有定稿本送達大地處日期
            'Else
            '    If (ds.Tables("swc").Rows(i)("SWC04").ToString = "審查中") Then
            '        '狀態為審查中
            '        ttt = DateValue(ds.Tables("swc").Rows(i)("SWC111").ToString).AddDays(7)
            '        If Now.Date >= ttt Then
            '            '7天後開始檢核
            '            Dim checkerr13 As Boolean = False
            '            If IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Then
            '                '沒有核定日期
            '                checkerr13 = True
            '            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Then
            '                '沒有核定日期
            '                checkerr13 = True
            '            End If
            '            If checkerr13 = True Then
            '                '沒有核定日期
            '                For bb = 0 To checkswcswcarray.GetLength(0) - 1
            '                    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
            '                    If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
            '                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，定稿本已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC111").ToString).ToString("yyyy-MM-dd") + ") 送達大地處，請確認是否已辦理核定!!;<br />"
            '                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，定稿本已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC111").ToString).ToString("yyyy-MM-dd") + ") 送達大地處，請確認是否已辦理核定!!;<br />"
            '                    End If
            '                Next
            '                updateSWClight("image/red.png", "審查公會建議核定期限已至，請確認是否已辦理核定!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
            '                Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，定稿本已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC111").ToString).ToString("yyyy-MM-dd") + ") 送達大地處，請確認是否已辦理核定!!"

            '            End If
            '        End If
            '    End If
            'End If

            '14.20180517若狀態為已核定，開工期限A.5天前B.當天，檢核開工日期是否有值
            '開工期限算法，簡易水保為核定日期+一年，水保計畫為核定日期+3年，103/12/25前核定的，皆設定開工日期為107/12/25
            '簡易水保或水保計畫:SWC07
            '核定日期:SWC38
            '開工期限:SWC82
            '開工日期:SWC51
            Dim swctype As String = ds.Tables("swc").Rows(i)("SWC07").ToString ' "水土保持計畫 or 簡易水保" 
            If (ds.Tables("swc").Rows(i)("SWC04").ToString = "已核定") Then
                    '先確認是已核定，再來設定開工期限
                    '先確定核定日期，因為開工期限要從核定日期計算
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Then
                        '核定日期限沒填
                    ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Then
                        '核定日期沒填
                    Else
                        '核定日期有填，可以開始判斷開工期限填了沒
                        Dim startswclimatedate As Date '開工期限
                        If DateValue("2014-12-25") <= DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString()) Then
                            '103/12/25前
                            startswclimatedate = DateValue("2018-12-25")
                        Else
                            '103/12/25以後
                            If swctype = "水土保持計畫" Then
                                '水保計畫 +3年
                                startswclimatedate = DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString).AddYears(3)
                            Else
                                '簡易水保 +1年
                                startswclimatedate = DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString).AddYears(1)
                            End If
                        End If
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Then
                            '開工期限沒填
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Or ds.Tables("swc").Rows(i)("SWC82").ToString = "" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1900/1/1 上午 12:00:00") Then
                            '開工期限沒填
                        Else
                            '開工期限有填，把startswclimatedate用這個填入
                            startswclimatedate = DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString())
                        End If
                        ttt = startswclimatedate.AddDays(-5)
                        If Now.Date >= ttt Or Now.Date = startswclimatedate Then
                            'A.五天前B.當日
                            Dim checkerr014 As Boolean = False
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Then
                                '開工日期沒填
                                checkerr014 = True
                            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Or ds.Tables("swc").Rows(i)("SWC51").ToString = "" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1900/1/1 上午 12:00:00") Then
                                '開工日期沒填
                                checkerr014 = True
                            End If
                        If checkerr014 = True Then
                            '20190315加上開工期限SWC82
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If Now.Date = ttt Or Now.Date = startswclimatedate Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，請確認是否已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).ToString("yyyy-MM-dd") + ") 前申報開工，或是否已申請開工展延!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，請確認是否已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).ToString("yyyy-MM-dd") + ") 前申報開工，或是否已申請開工展延!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "尚未申報開工，請確認是否已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).ToString("yyyy-MM-dd") + ") 前申報開工，或是否已申請開工展延!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            If Now.Date = ttt Or Now.Date = startswclimatedate Then
                                '寄MAIL給技師
                                'If (mailonecheck.Checked = True Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89"))) And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報開工，請於(" + DateValue(startswclimatedate).ToString("yyyy-MM-dd") + ")前申報開工，或申請開工展延；如逾期仍未辦理，本水土保持申請書件將依法自動失效!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                                'End If
                                '寄MAIL給聯絡人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                    '聯絡人信箱是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                    '聯絡人信箱是空的
                                Else
                                    'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                    mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報開工，請於(" + DateValue(startswclimatedate).ToString("yyyy-MM-dd") + ")前申報開工，或申請開工展延；如逾期仍未辦理，本水土保持申請書件將依法自動失效!!;<br />"
                                    mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                    'End If
                                End If
                                '發簡訊給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                    '義務人的聯絡人電話是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                    '義務人的聯絡人電話是空的
                                Else
                                    'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                    'sogi = sogi.Replace("-", "")
                                    'If Left(sogi, 2) = "09" Then
                                    '    '發簡訊至聯絡人手機：SWC16
                                    '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                    '    Dim tSWC16 = Left(sogi, 10)
                                    '    Dim tPhoneNo As String = tSWC16
                                    '    Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報開工，請於(" + DateValue(startswclimatedate).ToString("yyyy-MM-dd") + ")前申報開工，或申請開工展延；如逾期仍未辦理，本水土保持申請書件將依法自動失效!!"
                                    '    SendSMS(tPhoneNo, tSMSText)
                                    'Else
                                    '    '義務人的聯絡人手機號碼亂填，無法發送
                                    'End If
									Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
									sogi.Replace("-", "")
									Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
									Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
									Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報開工，請於(" + DateValue(startswclimatedate).ToString("yyyy-MM-dd") + ")前申報開工，或申請開工展延；如逾期仍未辦理，本水土保持申請書件將依法自動失效!!"
									SendSMS_Arr(tPhoneNoArr, tSMSText)
                                End If
                            End If
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，請確認是否已申報開工，或是否已申請開工展延!!"
                        End If
                    End If
                    End If
                End If

            '15.從書件那邊檢核
            '16.從書件那邊檢核

            '17.20180517若狀態為停工中，停工期限A.5天前B.當天，檢核開工日期是否有值
            '201809修改文字
            '停工期限:SWC84
            '開工日期:SWC51
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC84")) Then
                    '沒有停工期限
                ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC84")) Or ds.Tables("swc").Rows(i)("SWC84").ToString = "" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1900/1/1 上午 12:00:00") Then
                    '沒有停工期限
                Else
                    '有停工期限
                    If (ds.Tables("swc").Rows(i)("SWC04").ToString = "停工中") Then
                        '有暫停審查期限。判斷暫停審查期限A.五天前B.當日
                        ttt = DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).AddDays(-5)
                        If Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC84")) Then
                            'A.五天前B.當日
                            Dim checkerr17 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Then
                            '沒有開工日期
                            checkerr17 = True
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Or ds.Tables("swc").Rows(i)("SWC51").ToString = "" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1900/1/1 上午 12:00:00") Then
                            '沒有開工日期
                            checkerr17 = True
                        End If

                        checkerr17 = True '2021-01-04 不論有無開工日期皆檢核
                        If checkerr17 = True Then
                                For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC84")) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報復工，請確認是否已申報復工，或停工期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).ToString("yyyy-MM-dd") + ")是否已申請展延!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報復工，請確認是否已申報復工，或停工期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).ToString("yyyy-MM-dd") + ")是否已申請展延!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "尚未申報復工，請確認是否已申報復工，或停工期限(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).ToString("yyyy-MM-dd") + ")是否已申請展延!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC84")) Then
                                '寄MAIL給技師
                                'If (mailonecheck.Checked = True Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89"))) And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                mailps = ds.Tables("swc").Rows(i)("SWC21").ToString + " 您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報復工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString()).ToString("yyyy-MM-dd") + ") 前申報復工或停工展延；如逾期仍未辦理，水土保持申請書件依法將自動失效!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC85").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                                'End If
                                '寄信給檢查公會，先抓檢查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC24")) Then
                                    '檢查公會信箱沒填
                                ElseIf ds.Tables("swc").Rows(i)("SWC24").ToString = "" Then
                                    '檢查公會信箱沒填
                                Else
                                    '連線到137的帳號資料庫去抓檢查公會的MAIL
                                    '第一、連結使用人員資料庫
                                    Dim geoinfoconnstr As ConnectionStringSettings
                                    Dim geoinfoconn As SqlConnection
                                    geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                                    geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                                    geoinfoconn.Open()
                                    '第二、執行SQL指令，取出資料
                                    Dim geoinfocommand As SqlCommand
                                    Dim geoinfodareader As SqlDataReader
                                    Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC24").ToString + "'"
                                    geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                                    geoinfodareader = geoinfocommand.ExecuteReader()
                                    While geoinfodareader.Read
                                        'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                        mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報復工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString()).ToString("yyyy-MM-dd") + ") 前申報復工或停工展延；如逾期仍未辦理，水土保持申請書件依法將自動失效!!;<br />"
                                        mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                                        'End If
                                    End While
                                    '關閉137的連線資料庫元件
                                    geoinfodareader.Close()
                                    geoinfocommand.Dispose()
                                    geoinfoconn.Close()
                                    geoinfoconn.Dispose()
                                End If
                                '寄MAIL給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                    '義務人的聯絡人信箱是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                    '義務人的聯絡人信箱是空的
                                Else
                                    'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                    mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報復工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString()).ToString("yyyy-MM-dd") + ") 前申報復工或停工展延；如逾期仍未辦理，水土保持申請書件依法將自動失效!!;<br />"
                                    mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                    'End If
                                End If
                                    '發簡訊給義務人
                                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                        '義務人的聯絡人電話是空的
                                    ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                        '義務人的聯絡人電話是空的
                                    Else
                                        'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                        'sogi = sogi.Replace("-", "")
                                        'If Left(sogi, 2) = "09" Then
                                        '    '發簡訊至聯絡人手機：SWC16
                                        '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                        '    Dim tSWC16 = Left(sogi, 10)
                                        '    Dim tPhoneNo As String = tSWC16
                                        '    Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報復工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString()).ToString("yyyy-MM-dd") + ") 前申報復工或停工展延；如逾期仍未辦理，水土保持申請書件依法將自動失效!!"
                                        '    SendSMS(tPhoneNo, tSMSText)
                                        'Else
                                        '    '義務人的聯絡人手機號碼亂填，無法發送
                                        'End If
										Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
										sogi.Replace("-", "")
										Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
										Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
										Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報復工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString()).ToString("yyyy-MM-dd") + ") 前申報復工或停工展延；如逾期仍未辦理，水土保持申請書件依法將自動失效!!"
										SendSMS_Arr(tPhoneNoArr, tSMSText)
                                    End If
                                End If
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報復工，請確認是否已申報復工，或是否已申請展延停工期限!!"

                            End If
                        End If
                    End If
                End If

            '18.20180517若狀態為施工中，核定完工日期A.5天前B.當天檢核提醒，檢核完工日期是否有值
            '核定完工日期:SWC52
            '完工日期:SWC58
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Then
                '沒有核定完工日期
            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Or ds.Tables("swc").Rows(i)("SWC52").ToString = "" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1900/1/1 上午 12:00:00") Then
                '沒有核定完工日期
            Else
                '有核定完工日期
                If (ds.Tables("swc").Rows(i)("SWC04").ToString = "施工中") Then
                    '有核定完工日期。判斷核定完工日期A.五天前B.當日
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).AddDays(-5)
                    If Now.Date >= ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC52")) Then
                        'A.五天前B.當日
                        Dim checkerr18 As Boolean = False
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC58")) Then
                            '沒有完工日期
                            checkerr18 = True
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC58")) Or ds.Tables("swc").Rows(i)("SWC58").ToString = "" Or ds.Tables("swc").Rows(i)("SWC58").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC58").ToString = "1900/1/1 上午 12:00:00") Then
                            '沒有完工日期
                            checkerr18 = True
                        End If
                        If checkerr18 = True Then
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC52")) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報完工，請確認是否已申報完工，或核定完工日期(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 是否已展延!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報完工，請確認是否已申報完工，或核定完工日期(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 是否已展延!!;<br />"
                                    End If
                                End If
                            Next
                            updateSWClight("image/red.png", "尚未申報完工，請確認是否已申報完工，或核定完工日期(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 是否已展延!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            If Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC52")) Then
                                '寄MAIL給技師，2019-10-31，改寄監造技師(原承辦技師)
                                'If (mailonecheck.Checked = True Or Now.Date = ttt Or Now.Date = DateValue(ds.Tables("swc").Rows(i)("SWC89"))) And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                mailps = ds.Tables("swc").Rows(i)("SWC45").ToString + " 您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報完工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 前申報完工或申請工期展延；如逾期仍未辦理，水土保持申請書件及施工許可證依法將自動失效!!;<br />"
                                mailone(ds.Tables("swc").Rows(i)("SWC90").ToString, ds.Tables("swc").Rows(i)("SWC21").ToString, mailps, "水保申請案件")
                                'End If
                                '寄信給檢查公會，先抓檢查公會欄位，再去帳號管理資料庫抓MAIL，然後在發信
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC24")) Then
                                    '檢查公會信箱沒填
                                ElseIf ds.Tables("swc").Rows(i)("SWC24").ToString = "" Then
                                    '檢查公會信箱沒填
                                Else
                                    '連線到137的帳號資料庫去抓檢查公會的MAIL
                                    '第一、連結使用人員資料庫
                                    Dim geoinfoconnstr As ConnectionStringSettings
                                    Dim geoinfoconn As SqlConnection
                                    geoinfoconnstr = ConfigurationManager.ConnectionStrings("GEOINFOCONN")
                                    geoinfoconn = New SqlConnection(geoinfoconnstr.ConnectionString)
                                    geoinfoconn.Open()
                                    '第二、執行SQL指令，取出資料
                                    Dim geoinfocommand As SqlCommand
                                    Dim geoinfodareader As SqlDataReader
                                    Dim geoinfoselectstring As String = "SELECT NAME, email FROM [geouser] where NAME = '" + ds.Tables("swc").Rows(i)("SWC24").ToString + "'"
                                    geoinfocommand = New SqlCommand(geoinfoselectstring, geoinfoconn)
                                    geoinfodareader = geoinfocommand.ExecuteReader()
                                    While geoinfodareader.Read
                                        'If mailonecheck.Checked = True Or Now.Date = ttt And ds.Tables("swc").Rows(i)("SWC85").ToString <> "" Then
                                        mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報完工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 前申報完工或申請工期展延；如逾期仍未辦理，水土保持申請書件及施工許可證依法將自動失效!!;<br />"
                                        mailone(geoinfodareader.Item("email").ToString(), geoinfodareader.Item("NAME").ToString(), mailps, "水保申請案件")
                                        'End If
                                    End While
                                    '關閉137的連線資料庫元件
                                    geoinfodareader.Close()
                                    geoinfocommand.Dispose()
                                    geoinfoconn.Close()
                                    geoinfoconn.Dispose()
                                End If
                                '寄MAIL給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                    '義務人的聯絡人信箱是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                    '義務人的聯絡人信箱是空的
                                Else
                                    'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                    mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報完工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 前申報完工或申請工期展延；如逾期仍未辦理，水土保持申請書件及施工許可證依法將自動失效!!;<br />"
                                    mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                    'End If
                                End If
                                '發簡訊給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                    '義務人的聯絡人電話是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                    '義務人的聯絡人電話是空的
                                Else
                                    'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                    'sogi = sogi.Replace("-", "")
                                    'If Left(sogi, 2) = "09" Then
                                    '    '發簡訊至聯絡人手機：SWC16
                                    '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                    '    Dim tSWC16 = Left(sogi, 10)
                                    '    Dim tPhoneNo As String = tSWC16
                                    '    Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報完工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 前申報完工或申請工期展延；如逾期仍未辦理，水土保持申請書件及施工許可證依法將自動失效!!"
                                    '    SendSMS(tPhoneNo, tSMSText)
                                    'Else
                                    '    '義務人的聯絡人手機號碼亂填，無法發送
                                    'End If
									Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
									sogi.Replace("-", "")
									Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
									Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
									Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】尚未申報完工，請於(" + DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString()).ToString("yyyy-MM-dd") + ") 前申報完工或申請工期展延；如逾期仍未辦理，水土保持申請書件及施工許可證依法將自動失效!!"
									SendSMS_Arr(tPhoneNoArr, tSMSText)
                                End If
                            End If
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報完工，請確認是否已申報完工，或核定完工日期是否已展延!!"
                        End If
                    End If
                End If
            End If

            '19-1.若狀態為施工中，完工檢查紀錄表單有送出，檢查結果為"已達完工標準"之表單，8天後檢核"完工日期"是否有值
            '201809若狀態為施工中，完工檢查紀錄表單有送出，檢查結果為"已達完工標準"之表單，8天後檢核"完工證明書核發日期"是否有值
            'SWC59完工證明書核發日期
            'SWC58完工日期
            If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "施工中") Then
                    '不是施工中不判斷
                Else
                    Dim ckeckerr191 As Boolean = False
                    Dim finishcheckdate As Date = DateValue("1912-01-01")
                    '去完工檢查紀錄表那邊判斷
                    '連線到55的TCGESWC
                    '第一、連結完工檢查紀錄表
                    Dim tcgeswcconnstr As ConnectionStringSettings
                    Dim tcgeswcconn As SqlConnection
                    tcgeswcconnstr = ConfigurationManager.ConnectionStrings("SWCConnStr")
                    tcgeswcconn = New SqlConnection(tcgeswcconnstr.ConnectionString)
                    tcgeswcconn.Open()
                    '第二、執行SQL指令，取出資料
                    Dim tcgeswccommand As SqlCommand
                    Dim tcgeswcdareader As SqlDataReader
                    Dim tcgeswcselectstring As String = "SELECT savedate FROM [SWCDTL06] where DTLF003 = '已達完工標準' and DATALOCK = 'Y' and SWC000 = '" + ds.Tables("swc").Rows(i)("SWC00").ToString + "'"
                    tcgeswccommand = New SqlCommand(tcgeswcselectstring, tcgeswcconn)
                    tcgeswcdareader = tcgeswccommand.ExecuteReader()
                    While tcgeswcdareader.Read
                        If IsDBNull(tcgeswcdareader.Item("savedate")) Then
                            '沒有完工檢查紀錄的送出日期
                        ElseIf (IsDBNull(tcgeswcdareader.Item("savedate")) Or tcgeswcdareader.Item("savedate").ToString = "" Or tcgeswcdareader.Item("savedate").ToString = "1912/1/1 上午 12:00:00" Or tcgeswcdareader.Item("savedate").ToString = "1900/1/1 上午 12:00:00") Then
                            '沒有完工檢查紀錄的送出日期
                        Else
                            If finishcheckdate < DateValue(tcgeswcdareader.Item("savedate").ToString()) Then
                                finishcheckdate = DateValue(tcgeswcdareader.Item("savedate").ToString())
                            End If
                        End If
                    End While
                    '關閉137的連線資料庫元件
                    tcgeswcdareader.Close()
                    tcgeswccommand.Dispose()
                    tcgeswcconn.Close()
                    tcgeswcconn.Dispose()
                If finishcheckdate > DateValue("1912-01-01") Then
                    '完工檢查表單有送出，開始檢查完工證明核發了沒有
                    ttt = finishcheckdate.AddDays(8)
                    If Now.Date >= ttt Then
                        If IsDBNull(ds.Tables("swc").Rows(i)("SWC59")) Then
                            '沒有完工證明書核發日期
                            ckeckerr191 = True
                        ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC59")) Or ds.Tables("swc").Rows(i)("SWC59").ToString = "" Or ds.Tables("swc").Rows(i)("SWC59").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC59").ToString = "1900/1/1 上午 12:00:00") Then
                            '沒有完工證明書核發日期
                            ckeckerr191 = True
                        End If
                    End If
                End If
                '20190315加上完工檢查送出日期
                If ckeckerr191 = True Then
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                        'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                        If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 已於 (" + finishcheckdate + ") 完工檢查，尚未核發完工證明書，請確認是否已核發!!;<br />"
                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 已於 (" + finishcheckdate + ") 完工檢查，尚未核發完工證明書，請確認是否已核發!!;<br />"
                        End If
                    Next
                        updateSWClight("image/red.png", "尚未核發完工證明書，請確認是否已核發!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 已於 (" + finishcheckdate + ") 完工檢查，尚未核發完工證明書，請確認是否已核發!!"
                End If
                End If

            '19-2.20180517若狀態為已完工，有填寫核定日期且核定日期為105/1/1以後，且水土保持書件類別是水保計畫，且無勾選"無保證金"，完工證明書核發日期有值，30天后檢核是否勾選保證金退還
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Then

            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Or DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString).Date < DateValue("2016-01-01").Date Then
                '核定日期在 2016/01/01 之前，不判斷。
                '沒有核定日期
            ElseIf ds.Tables("swc").Rows(i)("SWC07").ToString = "水土保持計畫" Then
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工") Or (ds.Tables("swc").Rows(i)("SWC41").ToString = "無保證金") Then

                Else
                    If IsDBNull(ds.Tables("swc").Rows(i)("SWC59")) Then
                        '沒有完工證明書核發日期
                    ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC59")) Or ds.Tables("swc").Rows(i)("SWC59").ToString = "" Or ds.Tables("swc").Rows(i)("SWC59").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC59").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有完工證明書核發日期
                    Else
                        '有完工證明書核發日期。判斷30天後，保證金退還填了沒
                        ttt = DateValue(ds.Tables("swc").Rows(i)("SWC59").ToString).AddDays(30)
                        If Now.Date >= ttt Then
                            '30天後
                            Dim checkerr192 As Boolean = False
                            If IsDBNull(ds.Tables("swc").Rows(i)("SWC56")) Then
                                '保證金還沒退
                                checkerr192 = True
                            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC56")) Or ds.Tables("swc").Rows(i)("SWC56").ToString = "") Then
                                '保證金還沒退 
                                checkerr192 = True
                            End If
                            '20190315加上完工證明書核發日期SWC59
                            If checkerr192 = True Then
                                For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC59").ToString).ToString("yyyy-MM-dd") + ") 同意完工，尚未退還保證金，請確認是否已完成退款!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + DateValue(ds.Tables("swc").Rows(i)("SWC59").ToString).ToString("yyyy-MM-dd") + ") 同意完工，尚未退還保證金，請確認是否已完成退款!!;<br />"
                                    End If
                                Next
                                updateSWClight("image/red.png", "已於 (" + DateValue(ds.Tables("swc").Rows(i)("SWC59").ToString).ToString("yyyy-MM-dd") + ") 同意完工，尚未退還保證金，請確認是否已完成退款!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                '寄MAIL給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC108")) Then
                                    '義務人的聯絡人信箱是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC108") = "" Then
                                    '義務人的聯絡人信箱是空的
                                Else
                                    'If (mailonecheck.Checked = True) And (ds.Tables("swc").Rows(i)("SWC85").ToString <> "") Then
                                    mailps = "您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】水土保持保證金申請退還，請盡速檢附相關文件至大地處辦理退款事宜!!;<br />"
                                    mailone(ds.Tables("swc").Rows(i)("SWC108").ToString, ds.Tables("swc").Rows(i)("SWC15").ToString, mailps, "水保申請案件")
                                    'End If
                                End If
                                '發簡訊給義務人
                                If IsDBNull(ds.Tables("swc").Rows(i)("SWC013TEL")) Then
                                    '義務人的聯絡人電話是空的
                                ElseIf ds.Tables("swc").Rows(i)("SWC013TEL") = "" Then
                                    '義務人的聯絡人電話是空的
                                Else
                                    'Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
                                    'sogi = sogi.Replace("-", "")
                                    'If Left(sogi, 2) = "09" Then
                                    '    '發簡訊至聯絡人手機：SWC16
                                    '    Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
                                    '    Dim tSWC16 = Left(sogi, 10)
                                    '    Dim tPhoneNo As String = tSWC16
                                    '    Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】水土保持保證金申請退還，請盡速檢附相關文件至大地處辦理退款事宜!!"
                                    '    SendSMS(tPhoneNo, tSMSText)
                                    'Else
                                    '    '義務人的聯絡人手機號碼亂填，無法發送
                                    'End If
									Dim sogi As String = ds.Tables("swc").Rows(i)("SWC013TEL").ToString()
									sogi.Replace("-", "")
									Dim tPhoneNoArr As String() = sogi.Split(new string(){";"}, StringSplitOptions.None)
									Dim tSWC05 = ds.Tables("swc").Rows(i)("SWC05").ToString + ""
									Dim tSMSText As String = "親愛的水土保持義務人您好，提醒您，【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】水土保持保證金申請退還，請盡速檢附相關文件至大地處辦理退款事宜!!"
									SendSMS_Arr(tPhoneNoArr, tSMSText)
                                End If
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未退還保證金，請確認是否已完成退款!!"
                            End If
                        End If
                    End If
                End If
            End If

            '20.20180517若狀態非為「不予受理」、「撤銷」、「不予核定」、「廢止」、「失效」，且有填審查費繳納日期，不予受理/撤銷/不予核定/廢止/失效日期30天後，檢核有無勾選"已核銷"或"無審查費"
            '沒有不予受理/撤銷/不予核定/廢止/失效日期 : SWC78
            If IsDBNull(ds.Tables("swc").Rows(i)("SWC33")) Then
                '沒有審查費繳納日期
            ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC33")) Or ds.Tables("swc").Rows(i)("SWC33").ToString = "" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1900/1/1 上午 12:00:00") Then
                '沒有審查費繳納日期
            Else
                If IsDBNull(ds.Tables("swc").Rows(i)("SWC78")) Then
                    '沒有不予受理/撤銷/不予核定/廢止/失效日期，且在105/1/1之前，就不判斷了
                ElseIf (IsDBNull(ds.Tables("swc").Rows(i)("SWC78")) Or ds.Tables("swc").Rows(i)("SWC78").ToString = "" Or ds.Tables("swc").Rows(i)("SWC78").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC78").ToString = "1900/1/1 上午 12:00:00" Or DateValue(ds.Tables("swc").Rows(i)("SWC78").ToString) < DateValue("2016-01-01")) Then
                    '沒有不予受理/撤銷/不予核定/廢止/失效日期，且在105/1/1之前，就不判斷了
                Else
                    If (ds.Tables("swc").Rows(i)("SWC04").ToString = "不予受理" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "撤銷" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "不予核定" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "廢止" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "失效") Then
                        '狀態是「不予受理」、「撤銷」、「不予核定」、「廢止」、「失效」
                    '狀態不是「不予受理」、「撤銷」、「不予核定」、「廢止」、「失效」
					Else
                        '有不予受理/撤銷/不予核定/廢止/失效日期。判斷30天後(若是12/15~12/31，從次年1/1開始檢核)，檢核審查費核銷勾了沒
                        If DateValue(ds.Tables("swc").Rows(i)("SWC78").ToString).Month = 12 And DateValue(ds.Tables("swc").Rows(i)("SWC78").ToString).Day >= 15 Then
                            Dim newyear As Integer = DateValue(ds.Tables("swc").Rows(i)("SWC78").ToString).AddYears(1).Year
                            ttt = DateValue(Trim(newyear.ToString()) + "-01-01")
                        Else
                            ttt = DateValue(ds.Tables("swc").Rows(i)("SWC78").ToString).AddDays(30)
                        End If
                        If Now.Date >= ttt Then
                            '30天後
                            'If ((IsDBNull(ds.Tables("swc").Rows(i)("SWC37")) Or ds.Tables("swc").Rows(i)("SWC37").ToString = "" Or ds.Tables("swc").Rows(i)("SWC37").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC37").ToString = "1900/1/1 上午 12:00:00")) And ds.Tables("swc").Rows(i)("SWC36").ToString <> "無審查費" Then
                            If ds.Tables("swc").Rows(i)("SWC36").ToString <> "已核銷" And ds.Tables("swc").Rows(i)("SWC36").ToString <> "無審查費" Then
                                '(原本的)檢核審查費核銷日期沒填 
                                '選單有選了就可以
                                For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                '新的只要尚未核銷就好
                                If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷，請確認已辦理核銷並勾選已核銷!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷，請確認已辦理核銷並勾選已核銷!!;<br />"
                                End If
                            Next
                                updateSWClight("image/red.png", "審查費尚未核銷，請確認已辦理核銷並勾選已核銷!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷，請確認已辦理核銷並勾選已核銷!!"
                        End If
                        End If
                    End If
                End If
            End If
			
			'95.20220420 (新增) 若狀態為「審查中」，「審查單位建議核定/不予核定日期」有值，檢核提醒直到案件狀態改為已核定
            '序號13的地方宣告過了
			'SWC109：建議核定日期(日期)
			'Dim tSWC004 = ds.Tables("swc").Rows(i)("SWC04").ToString
            'Dim tSWC109 = ds.Tables("swc").Rows(i)("SWC109").ToString
            If tSWC004 = "審查中" Then
                If tSWC109 = "" Or tSWC109 = "1912/1/1 上午 12:00:00" Or tSWC109 = "1900/1/1 上午 12:00:00" Then
                Else
                    For bb = 0 To checkswcswcarray.GetLength(0) - 1
                        'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                        If checkswcswcarray(bb, 4) = "系統管理員" Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認案件核定進度!!;<br />"
                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認案件核定進度。!!;<br />"
                        End If
                    Next
                    '轄區【水土保持計畫】，已於【審查單位建議核定/不予核定日期】建議核定，請確認案件核定進度
                    updateSWClight("image/red.png", "案件已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認案件核定進度!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC05").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") ，已於(" + DateValue(ds.Tables("swc").Rows(i)("SWC109").ToString).ToString("yyyy-MM-dd") + ") 建議核定，請確認案件核定進度!!"
                End If
            End If

        Next i
        Label1.Text = Label1.Text + "<br />" + "水保申請案件檢核完畢!!"
        Label1.Text = Label1.Text + "<br />" + "=============================================================<br /><br /><br />"

        '第四、關閉資料庫的連接與相關資源
        ds.Clear()
        ds.Dispose()
        da.Dispose()
        swcconn.Close()
        ''發MAIL給相關人員
        'For bb = 0 To checkswcswcarray.GetLength(0) - 1
        '    If checkswcswcarray(bb, 3) <> "" Then
        '        Try
        '            'MB(checkswcgarray(bb, 2))
        '            mailswcgcase(checkswcswcarray(bb, 2), checkswcswcarray(bb, 0) + checkswcswcarray(bb, 1), checkswcswcarray(bb, 3), "水保申請案件")
        '        Catch ex As Exception
        '            mailerr(checkswcswcarray(bb, 2), checkswcswcarray(bb, 0) + checkswcswcarray(bb, 1), checkswcswcarray(bb, 3), "水保申請案件")
        '        End Try

        '    End If
        'Next bb
        '
        '
        '重新回到倒案件資料的程式
        'Response.Redirect("http://localhost/TSLM/IMPEXCEL.aspx")
    End Sub
    
	Sub checkswc()
        Dim i As Integer
        For i = 0 To checkarray.GetLength(0) - 1
            checkarray(i, 3) = "<br /><br />" + Now.Date.ToString("yyyy-MM-dd") + "<br />"
            checkarray(i, 3) = checkarray(i, 3) + "==== 以下為水保申請案件檢核資訊 ====;<br />"
        Next i
        Dim ttt As Date
        '第一、連結使用人員資料庫
        Dim SWCConnStrsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        SWCConnStrsetting = ConfigurationManager.ConnectionStrings("SWCConnStr")
        swcconn = New SqlConnection(SWCConnStrsetting.ConnectionString)
        swcconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT SWC01, SWC02, SWC04, SWC06, SWC07, SWC12, SWC13, SWC22, SWC25, SWC31, SWC32, SWC33, SWC34, SWC36, SWC37, SWC38, SWC39, SWC41, SWC51, SWC52, SWC53, SWC55, SWC57, SWC58, SWC77, SWC78, SWC82, SWC84 FROM [SWCSWC] order by [SWC12] asc, [SWC02] desc", swcconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swc")
        '如果沒有資料就離開
        If ds.Tables("swc").Rows.Count = 0 Then
            Exit Sub
        End If
        '有資料開始判斷檢查並做紀錄
        Dim regincount As Integer

        Dim bb As Integer
        For i = 0 To ds.Tables("swc").Rows.Count - 1
            If Left(ds.Tables("swc").Rows(i)("SWC12").ToString, 1) = "北" Then
                regincount = 0
            Else
                regincount = 9
            End If
            'Label1.Text = Label1.Text + ";" + Mid(ds.Tables("ilg").Rows(i)("region").ToString, 2, 1)
            Select Case Mid(ds.Tables("swc").Rows(i)("SWC12").ToString, 2, 1)
                Case "一", "1"
                    regincount = regincount + 0
                Case "二", "2"
                    regincount = regincount + 1
                Case "三", "3"
                    regincount = regincount + 2
                Case "四", "4"
                    regincount = regincount + 3
                Case "五", "5"
                    regincount = regincount + 4
                Case "六", "6"
                    regincount = regincount + 5
                Case "七", "7"
                    regincount = regincount + 6
                Case "八", "8"
                    regincount = regincount + 7
                Case "九", "9"
                    regincount = regincount + 8
            End Select
            '先清調亮燈提示再來判斷有問題的
            updateSWClight("", "", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))

            '開工期限第二天起，檢核案件狀態是否為失效
            'If (IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Or ds.Tables("swc").Rows(i)("SWC82").ToString <> "" And ds.Tables("swc").Rows(i)("SWC82").ToString <> "1912/1/1 上午 12:00:00" And ds.Tables("swc").Rows(i)("SWC82").ToString <> "1900/1/1 上午 12:00:00") Then
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Or ds.Tables("swc").Rows(i)("SWC82").ToString = "" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '有開工日期
                'For bb = 0 To checkswcswcarray.GetLength(0) - 1
                '    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                '    If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                '        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限未填寫，請修正開工期限!!;<br />"
                '        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限未填寫，請修正開工期限!!;<br />"
                '        updateSWClight("image/red.png", "開工期限未填寫，請修正開工期限!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                '    End If
                'Next
                'Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已至，請修正案件狀態為『失效』!!"

            Else
                '有開工期限。判斷開工期限第二天狀態改了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).AddDays(1)
                If Now.Date >= ttt Then
                    '第二天起
                    'If ds.Tables("swc").Rows(i)("SWC04").ToString <> "失效" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "廢止" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "撤銷" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "施工中" Then
                    If ds.Tables("swc").Rows(i)("SWC04").ToString = "已核定" Then
                        '(原本)不是失效、廢止、已完工、撤銷、施工中 
                        '已核定 
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                            'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已至，請修正案件狀態為『失效』!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已至，請修正案件狀態為『失效』!!;<br />"
                                updateSWClight("image/red.png", "開工期限已至，請修正案件狀態為『失效』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            End If
                        Next
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 開工期限已至，請修正案件狀態為『失效』!!"

                    End If
                End If
            End If

            '變更設計案件有填核定日期或核定文號，檢核原案是否為已變更
            '比較複雜，晚點寫
            Dim needcheck As Boolean = False
            If Len(ds.Tables("swc").Rows(i)("SWC02").ToString) > 12 Then
                '有-1,-2這種才是變更設計案件
                If ds.Tables("swc").Rows(i)("SWC04").ToString = "施工中" Then
                    '施工中才判斷 
                    If (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Then
                        '核定日期是空的,那就看看有沒有核定文號
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC39")) Or ds.Tables("swc").Rows(i)("SWC39").ToString = "" And ds.Tables("swc").Rows(i)("SWC39").ToString = "北市工地審字第號") Then
                            '核定文號也是是空的
                        Else
                            needcheck = True
                        End If
                    Else
                        needcheck = True
                    End If
                End If
            End If
            If needcheck Then
                Dim changecount As Integer = Convert.ToInt32(Right(ds.Tables("swc").Rows(i)("SWC02").ToString, Len(ds.Tables("swc").Rows(i)("SWC02").ToString) - 13))
                Dim q As Integer
                For q = 1 To changecount
                    Dim nowcaseid As String = Left(ds.Tables("swc").Rows(i)("SWC02").ToString, 12)
                    Dim revcaseid As String = Left(ds.Tables("swc").Rows(i + q)("SWC02").ToString, 12)
                    If nowcaseid = revcaseid Then
                        If ds.Tables("swc").Rows(i + q)("SWC04").ToString <> "已變更" Then
                            '前案不是已變更 
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 變更設計已核定，請修正前案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 變更設計已核定，請修正前案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!;<br />"
                                    updateSWClight("image/red.png", "變更設計已核定，請修正前案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 變更設計已核定，請修正前案(" + Trim(ds.Tables("swc").Rows(i + q)("SWC02").ToString) + ")案件狀態為『已變更』!!;"
                        End If
                    End If
                Next
            End If
            needcheck = False

            '停工期限隔天起，檢核是否為失效
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC84")) Or ds.Tables("swc").Rows(i)("SWC84").ToString = "" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '沒有停工期限
            Else
                '有停工期限。判斷第二天狀態改了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).AddDays(1)
                If Now.Date >= ttt Then
                    '第二天起
                    'If ds.Tables("swc").Rows(i)("SWC04").ToString <> "失效" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "廢止" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "撤銷" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "施工中" Then
                    If ds.Tables("swc").Rows(i)("SWC04").ToString = "停工中" Then
                        '(原本)不是失效、廢止、已完工、撤銷、施工中 
                        '停工中
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                            'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 停工期限已至，請修正案件狀態為『失效』!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 停工期限已至，請修正案件狀態為『失效』!!;<br />"
                                updateSWClight("image/red.png", "停工期限已至，請修正案件狀態為『失效』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            End If
                        Next
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 停工期限已至，請修正案件狀態為『失效』!!"

                    End If
                End If
            End If

            '完工工期限隔天起，檢核是否為失效
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Or ds.Tables("swc").Rows(i)("SWC52").ToString = "" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '沒有完工期限
            Else
                '有完工期限。判斷第二天狀態改了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).AddDays(1)
                If Now.Date >= ttt Then
                    '第二天起
                    If ((IsDBNull(ds.Tables("swc").Rows(i)("SWC55")) Or ds.Tables("swc").Rows(i)("SWC55").ToString = "" Or ds.Tables("swc").Rows(i)("SWC55").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC55").ToString = "1900/1/1 上午 12:00:00")) And ((IsDBNull(ds.Tables("swc").Rows(i)("SWC58")) Or ds.Tables("swc").Rows(i)("SWC58").ToString = "" Or ds.Tables("swc").Rows(i)("SWC58").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC58").ToString = "1900/1/1 上午 12:00:00")) Then
                        '完工申報日期沒有填，而且完工日期也沒填，就要來判斷了
                        'If ds.Tables("swc").Rows(i)("SWC04").ToString <> "失效" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "廢止" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工" And ds.Tables("swc").Rows(i)("SWC04").ToString <> "撤銷" Then
                        If ds.Tables("swc").Rows(i)("SWC04").ToString = "施工中" Then
                            '(原本)不是失效、廢止、已完工、撤銷 
                            '施工中
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 完工期限已至，請修正案件狀態為『失效』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 完工期限已至，請修正案件狀態為『失效』!!;<br />"
                                    updateSWClight("image/red.png", "完工期限已至，請修正案件狀態為『失效』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 完工期限已至，請修正案件狀態為『失效』!!"

                        End If
                    End If
                End If
            End If

            '若是審查單位不是臺北市政府工務局大地工程處，補正期限三天前檢核審查費繳納期限三天前是否有值
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Or ds.Tables("swc").Rows(i)("SWC22").ToString = "臺北市政府工務局大地工程處") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '審查單位是臺北市政府工務局大地工程處
            Else
                '審查單位不是臺北市政府工務局大地工程處。判斷補正期限三天前，審查費繳納期限填了沒
                'If (ds.Tables("swc").Rows(i)("SWC04").ToString = "不予受理") Or (IsDBNull(ds.Tables("swc").Rows(i)("SWC32")) Or ds.Tables("swc").Rows(i)("SWC32").ToString = "" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1900/1/1 上午 12:00:00") Then
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "受理中") Or (IsDBNull(ds.Tables("swc").Rows(i)("SWC32")) Or ds.Tables("swc").Rows(i)("SWC32").ToString = "" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1900/1/1 上午 12:00:00") Then
                    '(原本)補正期限沒填，或是不予受理
                    '受理中的才去判斷所以這邊要不是受理的跳過

                Else
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).AddDays(-3)
                    If Now.Date >= ttt Then
                        '補正期限三天前
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC31")) Or ds.Tables("swc").Rows(i)("SWC31").ToString = "" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1900/1/1 上午 12:00:00") Then
                            '審查費繳納期限沒填 
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，請確認『審查費繳納期限』或修正案件狀態為『不予受理』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，請確認『審查費繳納期限』或修正案件狀態為『不予受理』!!;<br />"
                                    updateSWClight("image/red.png", "尚未補件，請確認『審查費繳納期限』或修正案件狀態為『不予受理』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，請確認『審查費繳納期限』或修正案件狀態為『不予受理』!!"

                        End If
                    End If
                End If
            End If

            '若是審查單位不是臺北市政府工務局大地工程處，審查費繳納期限三天前檢核審查費繳納日期是否有值
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Or ds.Tables("swc").Rows(i)("SWC22").ToString = "臺北市政府工務局大地工程處") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '審查單位是臺北市政府工務局大地工程處
            Else
                '審查單位不是臺北市政府工務局大地工程處。判斷審查費繳納期限三天前檢核審查費繳納日期是否有值
                'If (ds.Tables("swc").Rows(i)("SWC04").ToString = "不予受理") Or (IsDBNull(ds.Tables("swc").Rows(i)("SWC31")) Or ds.Tables("swc").Rows(i)("SWC31").ToString = "" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1900/1/1 上午 12:00:00") Then
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "受理中") Or (IsDBNull(ds.Tables("swc").Rows(i)("SWC31")) Or ds.Tables("swc").Rows(i)("SWC31").ToString = "" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC31").ToString = "1900/1/1 上午 12:00:00") Then
                    '(原本)審查費繳納期限沒填，或是不予受理
                    '受理中的才去判斷所以這邊要不是受理的跳過
                Else
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC31").ToString).AddDays(-3)
                    If Now.Date >= ttt Then
                        '審查費繳納期限三天前
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC33")) Or ds.Tables("swc").Rows(i)("SWC33").ToString = "" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC33").ToString = "1900/1/1 上午 12:00:00") Then
                            '審查費繳納日期沒填 
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未繳納，請確認『審查費繳納日期』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未繳納，請確認『審查費繳納日期』!!;<br />"
                                    updateSWClight("image/red.png", "審查費尚未繳納，請確認『審查費繳納日期』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未繳納，請確認『審查費繳納日期』!!"

                        End If
                    End If
                End If
            End If

            '若是審查單位是臺北市政府工務局大地工程處，補正期限三天前檢核送審日期是否有值貨狀態不予受理
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC22")) Or ds.Tables("swc").Rows(i)("SWC22").ToString <> "臺北市政府工務局大地工程處") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '審查單位是臺北市政府工務局大地工程處
            Else
                '審查單位是臺北市政府工務局大地工程處。判斷補正期限三天前檢核審查費繳納日期是否有值
                'If (ds.Tables("swc").Rows(i)("SWC04").ToString = "不予受理") Or (IsDBNull(ds.Tables("swc").Rows(i)("SWC32")) Or ds.Tables("swc").Rows(i)("SWC32").ToString = "" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1900/1/1 上午 12:00:00") Then
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "受理中") Or (IsDBNull(ds.Tables("swc").Rows(i)("SWC32")) Or ds.Tables("swc").Rows(i)("SWC32").ToString = "" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC32").ToString = "1900/1/1 上午 12:00:00") Then
                    '(原本的)補正期限沒填，或是不予受理
                    '受理中的才去判斷所以這邊要不是受理的跳過
                Else
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC32").ToString).AddDays(-3)
                    If Now.Date >= ttt Then
                        '補正期限三天前
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC34")) Or ds.Tables("swc").Rows(i)("SWC34").ToString = "" Or ds.Tables("swc").Rows(i)("SWC34").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC34").ToString = "1900/1/1 上午 12:00:00") Then
                            '送審日期沒填 
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，請確認『送審日期』或修正案件狀態為『不予受理』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，請確認『送審日期』或修正案件狀態為『不予受理』!!;<br />"
                                    updateSWClight("image/red.png", "尚未補件，請確認『送審日期』或修正案件狀態為『不予受理』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未補件，請確認『送審日期』或修正案件狀態為『不予受理』!!"

                        End If
                    End If
                End If
            End If

            '========= 以下，此段程式不可能執行到…請無視… =========
            '送審日期四個月後，檢核核定日期是否有值或不予核定日期是否有值
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC34")) Or ds.Tables("swc").Rows(i)("SWC34").ToString = "" Or ds.Tables("swc").Rows(i)("SWC34").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC34").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '沒有送審日期
            Else
                '有送審日期。判斷四個月後核定了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC34").ToString).AddMonths(4)
                If Now.Date >= ttt Then
                    '先看是不是失效、撤銷、不予核定、廢止
                    'If (ds.Tables("swc").Rows(i)("SWC04").ToString = "撤銷" Or ds.Tables("swc").Rows(i)("SWC77").ToString = "撤銷" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "不予受理" Or ds.Tables("swc").Rows(i)("SWC77").ToString = "不予受理" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "失效" Or ds.Tables("swc").Rows(i)("SWC77").ToString = "失效" Or ds.Tables("swc").Rows(i)("SWC04").ToString = "廢止" Or ds.Tables("swc").Rows(i)("SWC77").ToString = "廢止") Then
                    If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "審查中") Then
                        '(原本的)失效、撤銷、不予核定、廢止
                        '審查中的才去判斷所以這邊要不是審查的跳過
                    Else
                        If (ds.Tables("swc").Rows(i)("SWC04").ToString = "不予核定" Or ds.Tables("swc").Rows(i)("SWC77").ToString = "不予核定") Then
                            '不予核定，確認不予核定日期有沒有填
                            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC78")) Or ds.Tables("swc").Rows(i)("SWC78").ToString = "" Or ds.Tables("swc").Rows(i)("SWC78").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC78").ToString = "1900/1/1 上午 12:00:00") Then
                                '不予核定日期沒填 
                                For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查已逾四個月，請確認『核定日期』或『不予核定日期』!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查已逾四個月，請確認『核定日期』或『不予核定日期』!!;<br />"
                                        updateSWClight("image/red.png", "審查已逾四個月，請確認『核定日期』或『不予核定日期』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                    End If
                                Next
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查已逾四個月，請確認『核定日期』或『不予核定日期』!!"
                            End If
                        Else
                            '核定了嗎，核定日期填了沒
                            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Then
                                '送審日期沒填 
                                For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查已逾四個月，請確認『核定日期』或『不予核定日期』!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查已逾四個月，請確認『核定日期』或『不予核定日期』!!;<br />"
                                        updateSWClight("image/red.png", "審查已逾四個月，請確認『核定日期』或『不予核定日期』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                    End If
                                Next
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查已逾四個月，請確認『核定日期』或『不予核定日期』!!"
                            End If
                        End If
                    End If
                End If
            End If
            '========= 以上，此段程式不可能執行到…請無視… =========

            '核定日期30天後，檢核審查費核銷日期填了沒
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Or DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString).Date < DateValue("2016-01-01").Date Then
                '核定日期在 2016/01/01 之前，不判斷。
                '沒有核定日期
            Else
                '有核定日期。判斷30天後，檢核審查費核銷日期填了沒
                ttt = DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString).AddDays(30)
                If Now.Date >= ttt Then
                    '30天後
                    'If ((IsDBNull(ds.Tables("swc").Rows(i)("SWC37")) Or ds.Tables("swc").Rows(i)("SWC37").ToString = "" Or ds.Tables("swc").Rows(i)("SWC37").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC37").ToString = "1900/1/1 上午 12:00:00")) And ds.Tables("swc").Rows(i)("SWC36").ToString <> "無審查費" Then
                    If ds.Tables("swc").Rows(i)("SWC36").ToString <> "已核銷" And ds.Tables("swc").Rows(i)("SWC36").ToString <> "無審查費" Then
                        '(原本的)檢核審查費核銷日期沒填 
                        '選單有選了就可以
                        For bb = 0 To checkswcswcarray.GetLength(0) - 1
                            ''原本的要加上請填登日期
                            ''If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            'If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                            '    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷，請確認『審查費核銷日期』!!;<br />"
                            '    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷，請確認『審查費核銷日期』!!;<br />"
                            '    updateSWClight("image/red.png", "審查費尚未核銷，請確認『審查費核銷日期』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            'End If
                            '新的只要尚未核銷就好
                            If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷!!;<br />"
                                updateSWClight("image/red.png", "審查費尚未核銷!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                            End If
                        Next
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 審查費尚未核銷!!"

                    End If
                End If
            End If

            '開工期限10天前到開工期限當天，檢核開工日期填了沒
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC82")) Or ds.Tables("swc").Rows(i)("SWC82").ToString = "" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC82").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '開工期限
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "已核定") Or (Len(ds.Tables("swc").Rows(i)("SWC02").ToString) > 12) Then

                Else
                    '有開工期限。判斷10天內，開工日期填了沒。特例是103年12月25日以前核定的通通一律已107/12/25為開工期限
                    If DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString) < "2014-12-25" Then
                        ttt = DateValue("2018-12-25").AddDays(-10)
                    Else
                        ttt = DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).AddDays(-10)
                    End If

                    If Now.Date >= ttt And Now.Date <= DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).Date Then
                        '開工期限10天內
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Or ds.Tables("swc").Rows(i)("SWC51").ToString = "" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1900/1/1 上午 12:00:00") Then
                            '開工日期日期沒填 
                            Dim aaday As Integer
                            aaday = DateDiff(DateInterval.Day, Now.Date, DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).Date)
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!;<br />"
                                    updateSWClight("image/red.png", "尚未申報開工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!"

                        End If
                    ElseIf Now.Date > DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).Date Then
                        ''已過開工期限
                        'If (IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Or ds.Tables("swc").Rows(i)("SWC51").ToString = "" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1900/1/1 上午 12:00:00") Then
                        '    '開工日期日期沒填 
                        '    Dim aaday As Integer
                        '    aaday = DateDiff(DateInterval.Day, Now.Date, DateValue(ds.Tables("swc").Rows(i)("SWC82").ToString).Date)
                        '    For bb = 0 To checkswcswcarray.GetLength(0) - 1
                        '        'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                        '        If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                        '            checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，已過『開工日期』" + aaday.ToString() + "天，申報書應失其效力!!;<br />"
                        '            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，已過『開工日期』" + aaday.ToString() + "天，申報書應失其效力!!;<br />"
                        '            updateSWClight("image/red.png", "尚未申報開工，已過『開工日期』" + aaday.ToString() + "天，申報書應失其效力!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                        '        End If
                        '    Next
                        '    Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報開工，已過『開工日期』" + aaday.ToString() + "天，申報書應失其效力!!"

                        'End If
                    End If
                End If

            End If

            '停工期限10天前到停工期限當天，檢核開工日期填了沒
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC84")) Or ds.Tables("swc").Rows(i)("SWC84").ToString = "" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC84").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '停工期限
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "停工中") Then

                Else
                    '有停工期限。判斷10天內，開工日期填了沒
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).AddDays(-10)
                    If Now.Date >= ttt And Now.Date <= DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).Date Then
                        '開工期限10天內
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC51")) Or ds.Tables("swc").Rows(i)("SWC51").ToString = "" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC51").ToString = "1900/1/1 上午 12:00:00") Then
                            '開工日期日期沒填 
                            Dim aaday As Integer
                            aaday = DateDiff(DateInterval.Day, Now.Date, DateValue(ds.Tables("swc").Rows(i)("SWC84").ToString).Date)
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報復工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報復工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!;<br />"
                                    updateSWClight("image/red.png", "尚未申報復工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報復工，請確認『開工日期』或於" + aaday.ToString() + "天內申請展延，否則申報書失其效力!!"

                        End If

                    End If
                End If
            End If

            '若是水保計畫，完工申報日期30天後，檢核保證金退了沒
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC38")) Or ds.Tables("swc").Rows(i)("SWC38").ToString = "" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC38").ToString = "1900/1/1 上午 12:00:00") Or DateValue(ds.Tables("swc").Rows(i)("SWC38").ToString).Date < DateValue("2016-01-01").Date Then
                '核定日期在 2016/01/01 之前，不判斷。
                '沒有核定日期
            ElseIf ds.Tables("swc").Rows(i)("SWC07").ToString = "水土保持計畫" Then
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "已完工") Or (ds.Tables("swc").Rows(i)("SWC41").ToString = "無保證金") Then

                Else
                    If (IsDBNull(ds.Tables("swc").Rows(i)("SWC55")) Or ds.Tables("swc").Rows(i)("SWC55").ToString = "" Or ds.Tables("swc").Rows(i)("SWC55").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC55").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有完工申報日期
                    Else
                        '有完工申報日期。判斷30天後，保證金退還日期填了沒
                        ttt = DateValue(ds.Tables("swc").Rows(i)("SWC55").ToString).AddDays(30)
                        If Now.Date >= ttt Then
                            '30天後
                            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC57")) Or ds.Tables("swc").Rows(i)("SWC57").ToString = "" Or ds.Tables("swc").Rows(i)("SWC57").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC57").ToString = "1900/1/1 上午 12:00:00") Then
                                '檢核審查費核銷日期沒填 
                                For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                    'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                        checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未退還保證金，請確認『保證金退還日期』!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未退還保證金，請確認『保證金退還日期』!!;<br />"
                                        updateSWClight("image/red.png", "尚未退還保證金，請確認『保證金退還日期』!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                    End If
                                Next
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未退還保證金，請確認『保證金退還日期』!!"

                            End If
                        End If
                    End If
                End If
            End If

            '預定完工日期10天前，檢核完工日期填了沒
            If (IsDBNull(ds.Tables("swc").Rows(i)("SWC52")) Or ds.Tables("swc").Rows(i)("SWC52").ToString = "" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC52").ToString = "1900/1/1 上午 12:00:00") Then
                '水保日期在  ==2016/01/01==  之前，不判斷。
                '預定完工工期限
            Else
                If (ds.Tables("swc").Rows(i)("SWC04").ToString <> "施工中") Then

                Else
                    '有預定完工工期限。判斷10天前，檢核完工日期填了沒
                    ttt = DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).AddDays(-10)
                    If Now.Date >= ttt And Now.Date <= DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString) Then
                        '預定完工工期限10天前到預定完工期限當日
                        If (IsDBNull(ds.Tables("swc").Rows(i)("SWC55")) Or ds.Tables("swc").Rows(i)("SWC55").ToString = "" Or ds.Tables("swc").Rows(i)("SWC55").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("swc").Rows(i)("SWC55").ToString = "1900/1/1 上午 12:00:00") Then
                            '開工日期日期沒填 
                            Dim aaday As Integer
                            aaday = DateDiff(DateInterval.Day, Now.Date, DateValue(ds.Tables("swc").Rows(i)("SWC52").ToString).Date)
                            For bb = 0 To checkswcswcarray.GetLength(0) - 1
                                'If checkswcswcarray(bb, 0) = areauser(regincount) Or checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                If checkswcswcarray(bb, 1) = "科長" Or checkswcswcarray(bb, 1) = "正工程司" Or checkswcswcarray(bb, 1) = "股長" Or checkswcswcarray(bb, 0) = managername Or checkswcswcarray(bb, 0) = managername1 Or checkswcswcarray(bb, 0) = managername2 Or checkswcswcarray(bb, 0) = managername3 Or checkswcswcarray(bb, 0) = Trim(ds.Tables("swc").Rows(i)("SWC25").ToString) Then
                                    checkswcswcarray(bb, 3) = checkswcswcarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報完工，請確認『完工申報日期』或於" + aaday.ToString() + "天內申請工期展延，否則申報書失其效力!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報完工，請確認『完工申報日期』或於" + aaday.ToString() + "天內申請工期展延，否則申報書失其效力!!;<br />"
                                    updateSWClight("image/red.png", "尚未申報完工，請確認『完工申報日期』或於" + aaday.ToString() + "天內申請工期展延，否則申報書失其效力!!", Trim(ds.Tables("swc").Rows(i)("SWC02").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("swc").Rows(i)("SWC12").ToString + "【" + ds.Tables("swc").Rows(i)("SWC13").ToString + "】(" + Trim(ds.Tables("swc").Rows(i)("SWC02").ToString) + ") 尚未申報完工，請確認『完工申報日期』或於" + aaday.ToString() + "天內申請工期展延，否則申報書失其效力!!"

                        End If

                    End If
                End If
            End If
        Next i
        Label1.Text = Label1.Text + "<br />" + "水保申請案件檢核完畢!!"
        Label1.Text = Label1.Text + "<br />" + "=============================================================<br /><br /><br />"

        '第四、關閉資料庫的連接與相關資源
        ds.Clear()
        ds.Dispose()
        da.Dispose()
        swcconn.Close()
        ''發MAIL給相關人員
        'For bb = 0 To checkswcswcarray.GetLength(0) - 1
        '    If checkswcswcarray(bb, 3) <> "" Then
        '        Try
        '            'MB(checkswcgarray(bb, 2))
        '            mailswcgcase(checkswcswcarray(bb, 2), checkswcswcarray(bb, 0) + checkswcswcarray(bb, 1), checkswcswcarray(bb, 3), "水保申請案件")
        '        Catch ex As Exception
        '            mailerr(checkswcswcarray(bb, 2), checkswcswcarray(bb, 0) + checkswcswcarray(bb, 1), checkswcswcarray(bb, 3), "水保申請案件")
        '        End Try

        '    End If
        'Next bb
        '
        '
        '重新回到倒案件資料的程式
        'Response.Redirect("http://localhost/TSLM/IMPEXCEL.aspx")
    End Sub

    Sub checkilg()
        Dim i As Integer
        For i = 0 To checkarray.GetLength(0) - 1
            'checkarray(i, 3) = "<br /><br />" + Now.Date.ToString("yyyy-MM-dd") + "<br />"
            checkarray(i, 3) = checkarray(i, 3) + "<br /><br />==== 以下為違規案件檢核資訊 ====;<br />"
        Next i
        Dim ttt As Date
        '第一、連結使用人員資料庫
        Dim ilgconnstringsetting As ConnectionStringSettings
        Dim ilgconn As SqlConnection
        ilgconnstringsetting = ConfigurationManager.ConnectionStrings("TSLMSWCCONN")
        ilgconn = New SqlConnection(ilgconnstringsetting.ConnectionString)
        ilgconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT ILG001, ILG003, ILG005, ILG006, ILG024, ILG025, ILG038, ILG042, ILG043, ILG044, ILG063, ILG064, ILG070, ILG071, ILG082, ILG083, ILG084 FROM [ILGILG] order by [ILG024]", ilgconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "ilg")
        '如果沒有資料就離開
        If ds.Tables("ilg").Rows.Count = 0 Then
            Exit Sub
        End If
        '有資料開始判斷檢查並做紀錄
        Dim regincount As Integer
        'Dim i As Integer
        Dim bb As Integer
        For i = 0 To ds.Tables("ilg").Rows.Count - 1
            If Left(ds.Tables("ilg").Rows(i)("ILG024").ToString, 1) = "北" Then
                regincount = 0
            Else
                regincount = 9
            End If
            'Label1.Text = Label1.Text + ";" + Mid(ds.Tables("ilg").Rows(i)("region").ToString, 2, 1)
            Select Case Mid(ds.Tables("ilg").Rows(i)("ILG024").ToString, 2, 1)
                Case "一"
                    regincount = regincount + 0
                Case "二"
                    regincount = regincount + 1
                Case "三"
                    regincount = regincount + 2
                Case "四"
                    regincount = regincount + 3
                Case "五"
                    regincount = regincount + 4
                Case "六"
                    regincount = regincount + 5
                Case "七"
                    regincount = regincount + 6
                Case "八"
                    regincount = regincount + 7
                Case "九"
                    regincount = regincount + 8
            End Select
            '先清調亮燈提示再來判斷有問題的
            updateILGlight("", "", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))

            '檢查處分日期14天後，送達日期填了沒
            If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG003")) Or ds.Tables("ilg").Rows(i)("ILG003").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG003").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG003").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                '沒有處分日期
            Else
                '有處分日期。判斷14天後送達日期填了沒
                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString).AddDays(14)
                If Now.Date > ttt Then
                    '過了14天
                    If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG038")) Or ds.Tables("ilg").Rows(i)("ILG038").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG038").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG038").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有送達日期
                        For bb = 0 To checkswcilgarray.GetLength(0) - 1
                            'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                            If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】處分書逾2週尚未送達，請確認『送達日期』!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】處分書逾2週尚未送達，請確認『送達日期』!!;<br />"
                                updateILGlight("image/red.png", "處分書逾2週尚未送達，請確認『送達日期』!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                            End If
                        Next
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】處分書逾2週尚未送達，請確認『送達日期』!!"

                    End If
                End If
            End If

            '繳納期限三天前繳納情形更新了沒有
            If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG042")) Or ds.Tables("ilg").Rows(i)("ILG042").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG042").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG042").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                '沒有繳納期限
            Else
                '有繳納期限。判斷3天前繳納情況填了沒
                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG042").ToString).AddDays(-3)
                If Now.Date > ttt Then
                    '繳款期限前三天已經到了
                    If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG043")) Or ds.Tables("ilg").Rows(i)("ILG043").ToString = "") Then
                        '沒有繳納狀況
                        If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG044")) Or ds.Tables("ilg").Rows(i)("ILG044").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG044").ToString = "False" Or ds.Tables("ilg").Rows(i)("ILG044").ToString = "0") Then
                            '沒有撤銷
                            For bb = 0 To checkswcilgarray.GetLength(0) - 1
                                'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                    If Now.Month = 5 And Now.Day = 1 Then
                                        checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】罰鍰尚未繳納，請確認『繳納情形』!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】罰鍰尚未繳納，請確認『繳納情形』!!;<br />"
                                    End If
                                    '2019-11-06,燈照亮，但是一年只需要在5/1日通知1次。
                                    updateILGlight("image/red.png", "罰鍰尚未繳納，請確認『繳納情形』!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】罰鍰尚未繳納，請確認『繳納情形』!!"
                        End If
                    End If
                End If
            End If

            '檢查會勘記錄那邊的ILGlook
            Dim dalook As SqlDataAdapter = New SqlDataAdapter("SELECT ILGlook001, ILGlook002, ILGlook003, ILGlook004, ILGlook005 FROM [ILGlook] where ILGlook001='" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "'", ilgconn)
            Dim dslook As DataSet = New DataSet()
            dalook.Fill(dslook, "ilglook")
            '如果沒有資料就離開
            If dslook.Tables("ilglook").Rows.Count = 0 Then
                '沒資料不用判斷
            Else
                '有資料開始判斷檢查並做紀錄
                Dim ilook As Integer
                For ilook = 0 To dslook.Tables("ilglook").Rows.Count - 1
                    '檢查改正日期前14天，會勘日期填了沒
                    If IsDBNull(dslook.Tables("ilglook").Rows(ilook)("ILGlook002")) Or dslook.Tables("ilglook").Rows(ilook)("ILGlook002").ToString = "" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook002").ToString = "1912/1/1 上午 12:00:00" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook002").ToString = "1900/1/1 上午 12:00:00" Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                        '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                        '沒有改正日期
                    Else
                        '有改正日期。判斷14天前會勘日期填了沒
                        ttt = DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook002").ToString).AddDays(-14)
                        If Now.Date > ttt Then
                            '改正日期14天前
                            If ((IsDBNull(dslook.Tables("ilglook").Rows(ilook)("ILGlook003")) Or dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString = "" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString = "1912/1/1 上午 12:00:00" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString = "1900/1/1 上午 12:00:00")) And (IsDBNull(dslook.Tables("ilglook").Rows(ilook)("ILGlook005")) Or dslook.Tables("ilglook").Rows(ilook)("ILGlook005").ToString = "") Then
                                '沒有會勘日期
                                For bb = 0 To checkswcilgarray.GetLength(0) - 1
                                    'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                    If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                        checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】尚未改正完成，請確認『會勘日期』，並發開會通知單!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】尚未改正完成，請確認『會勘日期』，並發開會通知單!!;<br />"
                                        updateILGlight("image/red.png", "尚未改正完成，請確認『會勘日期』，並發開會通知單!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                                    End If
                                    '如果是14天前，還要多發給姿隆請她創號，就14天就可以，不要重複發
                                    If checkswcilgarray(bb, 0) = "章姿隆" Then
                                        ttt = DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook002").ToString).AddDays(-13)
                                        If Now.Date = ttt Then
                                            checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】尚未改正完成，請確認『會勘日期』，並請姿隆創號，以利發開會通知單!!;<br />"
                                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】尚未改正完成，請確認『會勘日期』，並請姿隆創號，以利發開會通知單!!;<br />"
                                            'updateILGlight("image/red.png", "尚未改正完成，請確認『會勘日期』，並發開會通知單!!", Trim(dslook.Tables("ilg").Rows(i)("ILG001").ToString))
                                        End If
                                    End If
                                Next
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】尚未改正完成，請確認『會勘日期』，並發開會通知單!!"
                            End If
                        End If
                    End If



                    '會勘日期隔日起，會勘紀錄發文了沒填了沒
                    If (IsDBNull(dslook.Tables("ilglook").Rows(ilook)("ILGlook003")) Or dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString = "" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString = "1912/1/1 上午 12:00:00" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                        '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                        '沒有會勘日期
                    Else
                        '有會勘日期。判斷第二天後會勘紀錄日期填了沒
                        ttt = DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).AddDays(0)
                        If Now.Date > ttt Then
                            '會勘日期第二天
                            If (IsDBNull(dslook.Tables("ilglook").Rows(ilook)("ILGlook004")) Or dslook.Tables("ilglook").Rows(ilook)("ILGlook004").ToString = "" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook004").ToString = "1912/1/1 上午 12:00:00" Or dslook.Tables("ilglook").Rows(ilook)("ILGlook004").ToString = "1900/1/1 上午 12:00:00") Then
                                '沒有會勘紀錄日期
                                For bb = 0 To checkswcilgarray.GetLength(0) - 1
                                    'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                    If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                        checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】已於 " + DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).ToString("yyyy-MM-dd") + " 會勘，請確認『會勘紀錄』發文日期!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】已於 " + DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).ToString("yyyy-MM-dd") + " 會勘，請確認『會勘紀錄』發文日期!!;<br />"
                                        updateILGlight("image/red.png", "已於 " + DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).ToString("yyyy-MM-dd") + " 會勘，請確認『會勘紀錄』發文日期!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                                    End If
                                    '如果是第二天，還要多發給姿隆請她創號，就第二天就可以，不要重複發
                                    If checkswcilgarray(bb, 0) = "章姿隆" Then
                                        ttt = DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).AddDays(1)
                                        If Now.Date = ttt Then
                                            checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】已於 " + DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).ToString("yyyy-MM-dd") + " 會勘，請姿隆創號，以利『會勘紀錄』發文!!;<br />"
                                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】已於 " + DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).ToString("yyyy-MM-dd") + " 會勘，請姿隆創號，以利『會勘紀錄』發文!!;<br />"
                                            'updateILGlight("image/red.png", "尚未改正完成，請確認『會勘日期』，並發開會通知單!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                                        End If
                                    End If
                                Next
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】已於 " + DateValue(dslook.Tables("ilglook").Rows(ilook)("ILGlook003").ToString).ToString("yyyy-MM-dd") + " 會勘，請確認『會勘紀錄』發文日期!!"
                            End If
                        End If
                    End If

                Next
            End If
            dslook.Clear()
            dslook.Dispose()
            dalook.Dispose()


            '檢查訴願日期15天以後答辯日期有填了嗎
            If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG063")) Or ds.Tables("ilg").Rows(i)("ILG063").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG063").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG063").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                '沒有訴願日期
            Else
                '有訴願日期。判斷14天後答辯日期填了沒
                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG063").ToString).AddDays(15)
                If Now.Date > ttt Then
                    '過了15天
                    If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG064")) Or ds.Tables("ilg").Rows(i)("ILG064").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG064").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG064").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有答辯日期
                        For bb = 0 To checkswcilgarray.GetLength(0) - 1
                            'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                            If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG063").ToString).AddDays(20)
                                checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】訴願答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, Now.Date, ttt) + 5).ToString + " 天內確認『答辯日期』!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】訴願答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, Now.Date, ttt) + 5).ToString + " 天內確認『答辯日期』!!;<br />"
                                updateILGlight("image/red.png", "訴願答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, Now.Date, ttt) + 5).ToString + " 天內確認『答辯日期』!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                            End If
                        Next
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】訴願答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, Now.Date, ttt) + 5).ToString + " 天內確認『答辯日期』!!"

                    End If
                End If
            End If

            '檢查訴願補充那邊的ILGlaw
            Dim dalaw As SqlDataAdapter = New SqlDataAdapter("SELECT ILGlaw001, ILGlaw002, ILGlaw003 FROM [ILGlaw] where ILGlaw001='" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "'", ilgconn)
            Dim dslaw As DataSet = New DataSet()
            dalaw.Fill(dslaw, "ilglaw")
            '如果沒有資料就離開
            If dslaw.Tables("ilglaw").Rows.Count = 0 Then
                '沒資料不用判斷
            Else
                '有資料開始判斷檢查並做紀錄
                Dim ilaw As Integer
                For ilaw = 0 To dslaw.Tables("ilglaw").Rows.Count - 1
                    '補充訴願日期15天後，補充答辯日期填了沒
                    If (IsDBNull(dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw002")) Or dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw002").ToString = "" Or dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw002").ToString = "1912/1/1 上午 12:00:00" Or dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw002").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                        '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                        '沒有補充訴願日期
                    Else
                        '有補充訴願日期。判斷15天後補充答辯日期填了沒
                        ttt = DateValue(dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw002").ToString).AddDays(15)
                        If Now.Date > ttt Then
                            '補充訴願日期15天後
                            If (IsDBNull(dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw003")) Or dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw003").ToString = "" Or dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw003").ToString = "1912/1/1 上午 12:00:00" Or dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw003").ToString = "1900/1/1 上午 12:00:00") Then
                                '沒有補充答辯日期
                                For bb = 0 To checkswcilgarray.GetLength(0) - 1
                                    'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                    If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                        ttt = DateValue(dslaw.Tables("ilglaw").Rows(ilaw)("ILGlaw002").ToString).AddDays(20)
                                        checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】訴願補充答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 5).ToString + " 天內確認『補充答辯日期』!!;<br />"
                                        checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】訴願補充答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 5).ToString + " 天內確認『補充答辯日期』!!;<br />"
                                        updateILGlight("image/red.png", "訴願補充答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 5).ToString + " 天內確認『補充答辯日期』!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                                    End If
                                Next
                                Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】訴願補充答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 5).ToString + " 天內確認『補充答辯日期』!!"
                            End If
                        End If
                    End If

                Next
            End If
            dslaw.Clear()
            dslaw.Dispose()
            dalaw.Dispose()


            '訴訟日期5天後有沒有填答辯日期
            If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG070")) Or ds.Tables("ilg").Rows(i)("ILG070").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG070").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG070").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                '沒有訴訟日期
            Else
                '有訴訟日期。判斷5天後答辯日期填了沒
                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG070").ToString).AddDays(5)
                If Now.Date > ttt Then
                    '過了5天
                    If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG071")) Or ds.Tables("ilg").Rows(i)("ILG071").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG071").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG071").ToString = "1900/1/1 上午 12:00:00") Then
                        '沒有答辯日期
                        For bb = 0 To checkswcilgarray.GetLength(0) - 1
                            'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                            If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG070").ToString).AddDays(20)
                                checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】行政訴訟答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 15).ToString + " 天內確認『答辯日期』!!;<br />"
                                checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】行政訴訟答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 15).ToString + " 天內確認『答辯日期』!!;<br />"
                                updateILGlight("image/red.png", "行政訴訟答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 15).ToString + " 天內確認『答辯日期』!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                            End If
                        Next
                        Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】行政訴訟答辯書尚未發文，請於 " + Convert.ToInt32(DateDiff(DateInterval.Day, ttt, Now.Date) + 15).ToString + " 天內確認『答辯日期』!!"

                    End If
                End If
            End If

            '代為履行繳納期限三天前繳納情形更新了沒有
            If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG082")) Or ds.Tables("ilg").Rows(i)("ILG082").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG082").ToString = "1912/1/1 上午 12:00:00" Or ds.Tables("ilg").Rows(i)("ILG082").ToString = "1900/1/1 上午 12:00:00") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2016/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) <> "AF") Or (DateValue(ds.Tables("ilg").Rows(i)("ILG003").ToString) <= DateValue("2011/1/1") And Left(ds.Tables("ilg").Rows(i)("ILG003").ToString, 2) = "AF") Then
                '處分日期在2016/01/01之前，不判斷。案件編號為AF開頭則是2011/01/01之前，不判斷
                '沒有繳納期限
            Else
                '有繳納期限。判斷3天前繳納情況填了沒
                ttt = DateValue(ds.Tables("ilg").Rows(i)("ILG082").ToString).AddDays(-3)
                If Now.Date > ttt Then
                    '繳款期限前三天已經到了
                    If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG083")) Or ds.Tables("ilg").Rows(i)("ILG083").ToString = "") Then
                        '沒有繳納狀況
                        If (IsDBNull(ds.Tables("ilg").Rows(i)("ILG084")) Or ds.Tables("ilg").Rows(i)("ILG084").ToString = "" Or ds.Tables("ilg").Rows(i)("ILG084").ToString = "False" Or ds.Tables("ilg").Rows(i)("ILG084").ToString = "0") Then
                            '沒有撤銷
                            For bb = 0 To checkswcilgarray.GetLength(0) - 1
                                'If checkswcilgarray(bb, 0) = areauser(regincount) Or checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                If checkswcilgarray(bb, 1) = "科長" Or checkswcilgarray(bb, 1) = "正工程司" Or checkswcilgarray(bb, 1) = "股長" Or checkswcilgarray(bb, 0) = managername Or checkswcilgarray(bb, 0) = managername1 Or checkswcilgarray(bb, 0) = managername2 Or checkswcilgarray(bb, 0) = "廖麗禎" Or checkswcilgarray(bb, 0) = Trim(ds.Tables("ilg").Rows(i)("ILG025").ToString) Then
                                    checkswcilgarray(bb, 3) = checkswcilgarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】代為履行費用尚未繳納，請確認『繳納情形』!!;<br />"
                                    checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】代為履行費用尚未繳納，請確認『繳納情形』!!;<br />"
                                    updateILGlight("image/red.png", "代為履行費用尚未繳納，請確認『繳納情形』!!", Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString))
                                End If
                            Next
                            Label1.Text = Label1.Text + "<br />" + ds.Tables("ilg").Rows(i)("ILG024").ToString + "_" + ds.Tables("ilg").Rows(i)("ILG006").ToString + "【" + Trim(ds.Tables("ilg").Rows(i)("ILG001").ToString) + "】代為履行費用尚未繳納，請確認『繳納情形』!!"
                        End If
                    End If
                End If
            End If


        Next i
        Label1.Text = Label1.Text + "<br />" + "違規案件檢核完畢!!"
        Label1.Text = Label1.Text + "<br />" + "=============================================================<br /><br /><br />"

        '第四、關閉資料庫的連接與相關資源
        ds.Clear()
        ds.Dispose()
        da.Dispose()
        ilgconn.Close()
        ''發MAIL給相關人員
        'For bb = 0 To checkswcilgarray.GetLength(0) - 1
        '    If checkswcilgarray(bb, 3) <> "" Then
        '        Try
        '            'MB(checkswcgarray(bb, 2))
        '            mailswcgcase(checkswcilgarray(bb, 2), checkswcilgarray(bb, 0) + checkswcilgarray(bb, 1), checkswcilgarray(bb, 3), "違規案件檢核")
        '        Catch ex As Exception
        '            mailerr(checkswcilgarray(bb, 2), checkswcilgarray(bb, 0) + checkswcilgarray(bb, 1), checkswcilgarray(bb, 3), "違規案件檢核")
        '        End Try

        '    End If
        'Next bb
        '
        '
        '重新回到倒案件資料的程式
        'Response.Redirect("http://localhost/TSLM/IMPEXCEL.aspx")
    End Sub

    Sub checkswcg()
        Dim i As Integer
        For i = 0 To checkarray.GetLength(0) - 1
            'checkarray(i, 3) = "<br /><br />" + Now.Date.ToString("yyyy-MM-dd") + "<br />"
            checkarray(i, 3) = checkarray(i, 3) + "<br /><br />==== 以下為水保輔導案件檢核資訊 ====;<br />"
        Next i
        '這邊因為使用者資料庫還未加入區別與承辦的連動，所以先用人工方式去寫一個區跟承辦的陣列，等使用者管理改好，這邊意改成去資料庫連動
        '北一區對應areauser(0),北二區對應areauser(1),,,,南一區對應areauser(9),南二區對應areauser(10),,,,以此類推
        'Dim areauser() As String = {"王宏仁", "", "陳世豪", "謝宜芸", "王琮元", "江秀雯", "黃以琳", "鄭賀譽", "王宏仁", "陳子裕", "", "謝旻希", "鄭仁傑", "潘彥華", "陳子裕", "謝旻希", "蕭耕華", "鄭仁傑"}
        'areauser(17) = {"王宏仁", "", "陳世豪", "謝宜芸", "王琮元", "江秀雯", "黃以琳", "鄭賀譽", "王宏仁", "陳子裕", "", "謝旻希", "鄭仁傑", "潘彥華", "陳子裕", "謝旻希", "蕭耕華", "鄭仁傑"}
        '先讀取輔導案件資料庫swcg
        '第一、連結使用人員資料庫
        Dim swcgcaseconnstringsetting As ConnectionStringSettings
        Dim swcgcaseconn As SqlConnection
        swcgcaseconnstringsetting = ConfigurationManager.ConnectionStrings("TSLMSWCCONN")
        swcgcaseconn = New SqlConnection(swcgcaseconnstringsetting.ConnectionString)
        swcgcaseconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT caseno, region, finishdate, status, resperson, tutordate FROM [swcgcase] order by region", swcgcaseconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swcgcase")
        '如果沒有資料就離開
        If ds.Tables("swcgcase").Rows.Count = 0 Then
            Exit Sub
        End If
        '有資料開始判斷檢查並做紀錄
        Dim regincount As Integer
        'Dim i As Integer
        Dim bb As Integer
        For i = 0 To ds.Tables("swcgcase").Rows.Count - 1
            If Left(ds.Tables("swcgcase").Rows(i)("region").ToString, 1) = "北" Then
                regincount = 0
            Else
                regincount = 9
            End If
            'Label1.Text = Label1.Text + ";" + Mid(ds.Tables("swcgcase").Rows(i)("region").ToString, 2, 1)
            Select Case Mid(ds.Tables("swcgcase").Rows(i)("region").ToString, 2, 1)
                Case "一"
                    regincount = regincount + 0
                Case "二"
                    regincount = regincount + 1
                Case "三"
                    regincount = regincount + 2
                Case "四"
                    regincount = regincount + 3
                Case "五"
                    regincount = regincount + 4
                Case "六"
                    regincount = regincount + 5
                Case "七"
                    regincount = regincount + 6
                Case "八"
                    regincount = regincount + 7
                Case "九"
                    regincount = regincount + 8
            End Select
            '先清調亮燈提示再來判斷有問題的
            updateswcgcaselight("", "", Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString))
            'Label1.Text = Label1.Text + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "_" + ds.Tables("swcgcase").Rows(i)("status").ToString() + "_" + ds.Tables("swcgcase").Rows(i)("finishdate").ToString + "<br />"
            '先判斷完工日期，沒填而且是施工中就要填，有填就看看是不是已經超過完工期限了
            Dim qFinDate As String = ds.Tables("swcgcase").Rows(i)("finishdate") + ""
            Dim qTutorDate As String = ds.Tables("swcgcase").Rows(i)("tutordate") + ""
			If Trim(qTutorDate) = "" Then
				qTutorDate = "2000/01/01"
			End If
			
            'If (IsDBNull(ds.Tables("swcgcase").Rows(i)("finishdate")) Or ds.Tables("swcgcase").Rows(i)("finishdate").ToString = "" Or ds.Tables("swcgcase").Rows(i)("finishdate").ToString = "1912/1/1 上午 12:00:00") Then
            If Trim(qFinDate) = "" Or Left(qFinDate, 4) = "1912" Then
				'沒有填完工日期
                If ((ds.Tables("swcgcase").Rows(i)("status").ToString = "施工中") Or (ds.Tables("swcgcase").Rows(i)("status").ToString = "已完工")) And (DateValue(qTutorDate) > DateValue("2016/01/01")) Then
                    For bb = 0 To checkswcgarray.GetLength(0) - 1
                        If checkswcgarray(bb, 0) = areauser_swcg(regincount) Or checkswcgarray(bb, 1) = "科長" Or checkswcgarray(bb, 1) = "正工程司" Or checkswcgarray(bb, 1) = "股長" Or checkswcgarray(bb, 0) = managername Or checkswcgarray(bb, 0) = managername1 Or checkswcgarray(bb, 0) = managername2 Then
                            checkswcgarray(bb, 3) = checkswcgarray(bb, 3) + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + "【" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "】完工期限未寫，請確認!!;<br />"
                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + "【" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "】完工期限未寫，請確認!!;<br />"
                            updateswcgcaselight("image/red.png", "完工期限未寫，請確認!!", Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString))
                        End If
                    Next
                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + " [" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "] 完工期限未寫，請確認!!"
                End If
            Else
                '有填完工日期，開始判斷
                Dim tFinDate As String = DateValue(ds.Tables("swcgcase").Rows(i)("finishdate").ToString).ToString("yyyy-MM-dd") '完工日期

                If DateValue(Now.ToString()) > DateValue(ds.Tables("swcgcase").Rows(i)("finishdate").ToString()) And ((ds.Tables("swcgcase").Rows(i)("status").ToString() = "施工中")) Then
                    For bb = 0 To checkswcgarray.GetLength(0) - 1
                        If checkswcgarray(bb, 0) = areauser_swcg(regincount) Or checkswcgarray(bb, 1) = "科長" Or checkswcgarray(bb, 1) = "正工程司" Or checkswcgarray(bb, 1) = "股長" Or checkswcgarray(bb, 0) = managername Or checkswcgarray(bb, 0) = managername1 Or checkswcgarray(bb, 0) = managername2 Then
                            checkswcgarray(bb, 3) = checkswcgarray(bb, 3) + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + "【" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "】已超過完工期限" + tFinDate + "，請確認案件狀態!!;<br />"
                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + "【" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "】已超過完工期限" + tFinDate + "，請確認案件狀態!!;<br />"
                            updateswcgcaselight("image/red.png", "已超過完工期限" + tFinDate + "，請確認案件狀態!!", Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString))
                        End If
                    Next
                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + " [" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "] 已超過完工期限" + tFinDate + "，請確認案件狀態!!"
                End If
            End If
			
            '接著判斷輔導日期有沒有填，如果超過五天還在受理中就是狀態要改
            If (IsDBNull(ds.Tables("swcgcase").Rows(i)("tutordate")) Or ds.Tables("swcgcase").Rows(i)("tutordate").ToString = "" Or ds.Tables("swcgcase").Rows(i)("tutordate").ToString = "1912/1/1 上午 12:00:00") Then
                '沒有填受理日期

            Else
                '有填受理日期，開始判斷
                Dim ddd As Date = Now
                ddd = ddd.AddDays(-5)
                If DateValue(ddd.ToString()) > DateValue(ds.Tables("swcgcase").Rows(i)("tutordate").ToString()) And ds.Tables("swcgcase").Rows(i)("status").ToString() = "受理中" Then
                    For bb = 0 To checkswcgarray.GetLength(0) - 1
                        If checkswcgarray(bb, 0) = areauser_swcg(regincount) Or checkswcgarray(bb, 1) = "科長" Or checkswcgarray(bb, 1) = "正工程司" Or checkswcgarray(bb, 1) = "股長" Or checkswcgarray(bb, 0) = managername Or checkswcgarray(bb, 0) = managername1 Or checkswcgarray(bb, 0) = managername2 Then
                            checkswcgarray(bb, 3) = checkswcgarray(bb, 3) + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + "【" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "】已超過受理期限五日，請確認案件狀態!!;<br />"
                            checkarray(bb, 3) = checkarray(bb, 3) + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + "【" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "】已超過受理期限五日，請確認案件狀態!!;<br />"
                            updateswcgcaselight("image/red.png", "已超過受理期限五日期限，請確認案件狀態!!", Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString))
                        End If
                    Next
                    Label1.Text = Label1.Text + "<br />" + ds.Tables("swcgcase").Rows(i)("region").ToString + "_" + ds.Tables("swcgcase").Rows(i)("resperson").ToString + " [" + Trim(ds.Tables("swcgcase").Rows(i)("caseno").ToString) + "] 已超過受理期限五日，請確認案件狀態!!"
                End If
            End If
        Next i
        Label1.Text = Label1.Text + "<br />" + "水保輔導案件檢核完畢!!"

        '第四、關閉資料庫的連接與相關資源
        swcgcaseconn.Close()
        ''發MAIL給相關人員
        'For bb = 0 To checkswcgarray.GetLength(0) - 1
        '    If checkswcgarray(bb, 3) <> "" Then
        '        Try
        '            'MB(checkswcgarray(bb, 2))
        '            mailswcgcase(checkswcgarray(bb, 2), checkswcgarray(bb, 0) + checkswcgarray(bb, 1), checkswcgarray(bb, 3), "水保輔導案檢核")
        '        Catch ex As Exception
        '            mailerr(checkswcgarray(bb, 2), checkswcgarray(bb, 0) + checkswcgarray(bb, 1), checkswcgarray(bb, 3), "水保輔導案檢核")
        '        End Try

        '    End If
        'Next bb
        '
        '
        '重新回到倒案件資料的程式
        'Response.Redirect("http://localhost/TSLM/IMPEXCEL.aspx")
    End Sub

    Sub MB(ByVal messagetext As String)
        Try
            Dim myMsg As New Literal()
            myMsg.Text = "<script>alert('" & messagetext & "')</script>"
            Page.Controls.Add(myMsg)
        Catch
        End Try
    End Sub

    Sub mailerr(ByVal mailtowho As String, ByVal mailname As String, ByVal mailps As String, ByVal whichcheck As String)
        Try

            '設定寄給多維，通知信箱無法使用
            Dim mail As MailMessage = New MailMessage("geocheck@geovector.com.tw", "gancy@geovector.com.tw")
            '設定寄給GV test
            'Dim mail As MailMessage = New MailMessage("ge-tslm@mail.taipei.gov.tw", ConfigurationManager.AppSettings("GVmail"))
            mail.Bcc.Add("dego@geovector.com.tw")
            mail.SubjectEncoding = Encoding.UTF8
            '設定主旨
            mail.Subject = "您好，" + Now.ToString("yyyy-MM-dd") + " " + whichcheck + "信箱無法使用通知信"
            mail.IsBodyHtml = True '是否允 HTML 格式
            mail.BodyEncoding = Encoding.UTF8
            '信件BODY
            mail.Body = "<br />" + " " + whichcheck + "信箱無法使用通知信"
            mail.Body = mail.Body + "<br />" + "===========error message not mail to==============="
            mail.Body = mail.Body + "<br />" + mailtowho
            mail.Body = mail.Body + "<br />" + mailname
            mail.Body = mail.Body + "<br />" + mailps
            mail.Body = mail.Body + "<br /><br />" + "臺北市政府工務局大地工程處系統管理員 敬上<br /><br /><br />＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞"
            mail.Body = mail.Body + "<br /><br />" + maildebug
            '設定SMTP帳號密碼
            Dim smtp As SmtpClient = New SmtpClient("mail.geovector.com.tw")
            smtp.Credentials = New NetworkCredential("geocheck@geovector.com.tw", "1234") 'SMTP 帳號密碼
            '開始寄信
            smtp.Send(mail)
            '回收元件
            mail.Dispose()

        Catch ex As Exception

        End Try

    End Sub

	
    Sub mailswcgcase(ByVal mailtowho As String, ByVal mailname As String, ByVal mailps As String, ByVal whichcheck As String)
        Dim x As New GBClass001
        maildebug = mailtowho + "," + mailname + "," + mailps + "<br />"
        Dim nocasecheck As String = "<br /><br />" + Now.Date.ToString("yyyy-MM-dd") + "<br />==== 以下為水保申請案件檢核資訊 ====;<br /><br /><br />==== 以下為違規案件檢核資訊 ====;<br /><br /><br />==== 以下為水保輔導案件檢核資訊 ====;<br />"
        If mailps = nocasecheck Then
            Exit Sub
        End If
        If (Session("mailchecknow") = "now" And mailunsend.Checked = False) Or (Session("mailchecknow") = "") Then
            Dim mailTo As String = mailtowho + ";"
            Dim mailSub As String = mailname + "您好，" + Now.ToString("yyyy-MM-dd") + " " + whichcheck + "檢核通知信"
            Dim mailBody As String = "<br />" + mailps
            '信件BODY
            mailBody += "<br /><br />" + "臺北市政府工務局大地工程處系統管理員 敬上<br /><br /><br />＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞"
            x.Mail_Send(x.str2array(mailTo, ";"), mailSub, mailBody)
        End If
    End Sub
	
	
	
    Sub mailswcgcase_1203per(ByVal mailtowho As String, ByVal mailname As String, ByVal mailps As String, ByVal whichcheck As String)
        maildebug = mailtowho + "," + mailname + "," + mailps + "<br />"
        'MB(mailtowho)
        'MB(mailname)
        'MB(mailps)
        'MB("ok")
        'Exit Sub
        Dim nocasecheck As String = "<br /><br />" + Now.Date.ToString("yyyy-MM-dd") + "<br />==== 以下為水保申請案件檢核資訊 ====;<br /><br /><br />==== 以下為違規案件檢核資訊 ====;<br /><br /><br />==== 以下為水保輔導案件檢核資訊 ====;<br />"
        If mailps = nocasecheck Then
            Exit Sub
        End If
        If (Session("mailchecknow") = "now" And mailunsend.Checked = False) Or (Session("mailchecknow") = "") Then
            '以下是舊的messagemail的程式，參考用
            '設定寄給誰
            Dim mail As MailMessage = New MailMessage(smtpmailsend, mailtowho)
            maildebug = maildebug + "設定寄給誰" + "<br />"
            '設定寄給GV test
            'Dim mail As MailMessage = New MailMessage("ge-tslm@mail.taipei.gov.tw", ConfigurationManager.AppSettings("GVmail"))
            mail.Bcc.Add(ConfigurationManager.AppSettings("GVmail"))
            maildebug = maildebug + "設定寄給GV" + "<br />"
            mail.SubjectEncoding = Encoding.UTF8
            maildebug = maildebug + "設定主旨編碼" + "<br />"
            '設定主旨
            mail.Subject = mailname + "您好，" + Now.ToString("yyyy-MM-dd") + " " + whichcheck + "檢核通知信"
            maildebug = maildebug + "設定主旨" + "<br />"
            mail.IsBodyHtml = True '是否允 HTML 格式
            mail.BodyEncoding = Encoding.UTF8
            maildebug = maildebug + "設定郵件編碼" + "<br />"
            '信件BODY
            mail.Body = "<br />" + mailps
            mail.Body = mail.Body + "<br /><br />" + "臺北市政府工務局大地工程處系統管理員 敬上<br /><br /><br />＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞"
            maildebug = maildebug + "完成郵件內容" + "<br />"
            '設定SMTP帳號密碼_北市府
            Dim smtp As SmtpClient = New SmtpClient(smtpmailserver)
            smtp.Credentials = New NetworkCredential(smtpmailusername, smtpmailuserpassword) 'SMTP 帳號密碼
            ''多維測試
            'Dim smtp As SmtpClient = New SmtpClient("mail.geovector.com.tw")
            'smtp.Credentials = New NetworkCredential("geocheck@geovector.com.tw", "1234") 'SMTP 帳號密碼
            maildebug = maildebug + "設定寄件主機資訊" + "<br />"
            '開始寄信
            smtp.Send(mail)
            maildebug = maildebug + "開始寄信" + "<br />"
            '回收元件
            mail.Dispose()
            maildebug = maildebug + "寄信完成" + "<br /><br />"
        End If

    End Sub

    Sub mailone(ByVal mailtowho As String, ByVal mailname As String, ByVal mailps As String, ByVal whichcheck As String)
        Dim x As New GBClass001

        '設定主旨
        Dim sMailSub As String = mailname + "您好，" + Now.ToString("yyyy-MM-dd") + " " + whichcheck + "檢核通知信"
        '信件BODY
        Dim sMailBody As String = "<br />" + mailps + "<br /><br />" + "臺北市政府工務局大地工程處系統管理員 敬上<br /><br /><br />＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞"

        Try
            If (Session("mailchecknow") = "now" And mailunsend.Checked = False) Or (Session("mailchecknow") = "") Then
                Dim sendMail As String() = {mailtowho}
                x.Mail_Send(sendMail, sMailSub, sMailBody)
            End If
        Catch ex As Exception
            '錯誤信就先不要寄了
            'mailerr(checkarray(bb, 2), checkarray(bb, 0) + checkarray(bb, 1), checkarray(bb, 3), "水保、違規與輔導案件")
        End Try
    End Sub

    Sub updateSWClight(ByVal light As String, ByVal lightps As String, ByVal caseno As String)
        '把資料寫入資料庫()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand() 
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("TSLMSWCCONN").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
		if lightps <> ""
			sqlcom.CommandText = "UPDATE [SWCSWC] SET [light] = @light ,[lightps] += @lightps WHERE [SWC02]='" + caseno + "'"
		else
			sqlcom.CommandText = "UPDATE [SWCSWC] SET [light] = @light ,[lightps] = @lightps WHERE [SWC02]='" + caseno + "'"
		end if
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@light", light))
        sqlcom.Parameters.Add(New SqlParameter("@lightps", lightps))
        '參數設定到這邊
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        conn.Close()
        conn.Dispose()
    End Sub

    Sub updateILGlight(ByVal light As String, ByVal lightps As String, ByVal caseno As String)
        '把資料寫入資料庫()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("TSLMSWCCONN").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "UPDATE [ILGILG] SET [light] = @light ,[lightps] = @lightps WHERE [ILG001]='" + caseno + "'"
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@light", light))
        sqlcom.Parameters.Add(New SqlParameter("@lightps", lightps))
        '參數設定到這邊
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        conn.Close()
        conn.Dispose()
    End Sub

    Sub updateswcgcaselight(ByVal light As String, ByVal lightps As String, ByVal caseno As String)
        '把資料寫入資料庫()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("TSLMSWCCONN").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "UPDATE [swcgcase] SET [light] = @light ,[lightps] = @lightps WHERE [caseno]='" + caseno + "'"
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@light", light))
        sqlcom.Parameters.Add(New SqlParameter("@lightps", lightps))
        '參數設定到這邊
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        conn.Close()
        conn.Dispose()
    End Sub

    Protected Sub startnow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles startnow.Click
        Session("mailchecknow") = "now"
        Label1.Text = Now.ToString() + " 處理案件檢核機制中，請勿關閉此視窗"
        'Label1.Text = Label1.Text + Now.Hour.ToString
        getSMTPMailparament()
        Label1.Text = Now.ToString() + " 處理案件檢核機制中，請勿關閉此視窗"
        '先設定相關的參數
        arrayini()
        '檢查水保申請案件
        checkswc201612()
        '檢查水保違規
        checkilg()
        '檢查水保輔導案件
        checkswcg()		
        '發檢核成果信件
        '發MAIL給相關人員，以下是整合一封的
        Dim bb As Integer
        For bb = 0 To checkarray.GetLength(0) - 1
            If checkarray(bb, 3) <> "" Then
                Try
                    'MB(checkswcgarray(bb, 2))
                    mailswcgcase(checkarray(bb, 2), checkarray(bb, 0) + checkarray(bb, 1), checkarray(bb, 3), "水保、違規與輔導案件")
                Catch ex As Exception
                    mailerr(checkarray(bb, 2), checkarray(bb, 0) + checkarray(bb, 1), checkarray(bb, 3), "水保、違規與輔導案件")
                End Try

            End If
        Next bb		
        Session("mailchecknow") = ""
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        '第一、連結使用人員資料庫
        Dim SWCConnStrsetting As ConnectionStringSettings
        Dim swcconn As SqlConnection
        SWCConnStrsetting = ConfigurationManager.ConnectionStrings("TSLMSWCCONN")
        swcconn = New SqlConnection(SWCConnStrsetting.ConnectionString)
        swcconn.Open()
        '第二、執行SQL指令，取出資料
        Dim da As SqlDataAdapter = New SqlDataAdapter("SELECT SWC02, SWC07 FROM [SWCSWC] ", swcconn)
        Dim ds As DataSet = New DataSet()
        da.Fill(ds, "swc")
        '如果沒有資料就離開
        If ds.Tables("swc").Rows.Count = 0 Then
            Exit Sub
        End If
        Dim i As Integer
        Label1.Text = "開始處理圖6-1 7-1 的資料庫更新" + "<br />"
        For i = 0 To ds.Tables("swc").Rows.Count - 1
            getuploadfilename6171(ds.Tables("swc").Rows(i)("SWC02").ToString, ds.Tables("swc").Rows(i)("SWC07").ToString)
        Next
        '第四、關閉資料庫的連接與相關資源
        ds.Clear()
        ds.Dispose()
        da.Dispose()
        swcconn.Close()
    End Sub

    Sub getuploadfilename6171(ByVal SWC02text As String, ByVal SWC07text As String)
        '先設定檔案的目錄
        Dim filename As String = SWC02text.Substring(0, 12)
        Dim FileYear As Integer
        Dim FileYearS As String
        Dim targetDirectory As String
        Dim targetDirectoryurl As String
        Dim newtargetDirectory As String
        Dim newtargetDirectoryurl As String
        Dim defaultpath() As String
        Dim defaulturl() As String
        Dim defaultpathitem As String
        Dim defaulturlitem As String
        Dim defaultpathitem67 As String
        Dim defaulturlitem67 As String
        Dim defaultpathitem67new As String
        Dim defaulturlitem67new As String
        Dim kk As Integer = 0
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
            If (SWC07text = "簡易水保") Then
                targetDirectory = ConfigurationManager.AppSettings("swcpspath") + FileYearS + "\水保申請案件\簡易水保"
                targetDirectoryurl = ConfigurationManager.AppSettings("swcpsurl") + FileYearS + "/水保申請案件/簡易水保"
            End If
            newtargetDirectory = targetDirectory
            newtargetDirectoryurl = targetDirectoryurl

            defaultpathitem67 = targetDirectory + "\" + filename
            defaulturlitem67 = targetDirectoryurl + "/" + filename
            defaultpathitem67new = targetDirectory + "\" + SWC02text
            defaulturlitem67new = targetDirectoryurl + "/" + SWC02text
            Dim subdirectoryEntries() As String = Directory.GetDirectories(targetDirectory)
            Dim subdirectory As String
            Dim tk As Integer
            For Each subdirectory In subdirectoryEntries
                Dim di As DirectoryInfo = New DirectoryInfo(subdirectory)
                If di.Name <> SWC02text Then

                    If di.Name.Length > 11 Then '簡易水保資料夾有檔名長度小於13者會出現錯誤，會導致無法讀取審查資料夾
                        If (filename = di.Name.Substring(0, 12)) Then
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
                    End If
                End If

            Next
        Catch
        End Try
        If Len(SWC02text) > 12 Then
            Dim aa As String
            aa = Right(SWC02text, 1)
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
        Dim text61 As String = ""
        Dim text71 As String = ""
        '新的直接放自己的資料夾的,_1,_2......
        defaultpathitem67 = targetDirectory + "\" + SWC02text
        defaulturlitem67 = targetDirectoryurl + "/" + SWC02text
        Dim b61 As Boolean = False
        Dim b71 As Boolean = False
        folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\審查\6-1\")
        If folderExists Then
            filelist6 = Directory.GetFiles(defaultpathitem67 + "\審查\6-1\")
            Try
                If filelist6(0) <> "" Then
                    text61 = My.Computer.FileSystem.GetFileInfo(filelist6(0)).FullName
                    b61 = True
                End If
            Catch ex As Exception
                text61 = ""
            End Try
        End If
        folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\審查\7-1\")
        If folderExists Then
            filelist7 = Directory.GetFiles(defaultpathitem67 + "\審查\7-1\")
            Try
                If filelist7(0) <> "" Then
                    text71 = My.Computer.FileSystem.GetFileInfo(filelist7(0)).FullName
                    b71 = True
                End If
            Catch ex As Exception
                text71 = ""
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
                    If filelist6(0) <> "" And My.Computer.FileSystem.GetFileInfo(filelist6(0)).Name <> "Thumbs.db" Then
                        text61 = My.Computer.FileSystem.GetFileInfo(filelist6(0)).FullName
                    End If
                Catch ex As Exception
                    text61 = ""
                End Try

            End If
        End If
        If b71 = False Then
            folderExists = My.Computer.FileSystem.DirectoryExists(defaultpathitem67 + "\" + checkfloder + "\7-1\")
            If folderExists Then
                filelist7 = Directory.GetFiles(defaultpathitem67 + "\" + checkfloder + "\7-1\")
                Try
                    If filelist7(0) <> "" And My.Computer.FileSystem.GetFileInfo(filelist7(0)).Name <> "Thumbs.db" Then
                        text71 = My.Computer.FileSystem.GetFileInfo(filelist7(0)).FullName
                    End If
                Catch ex As Exception
                    text71 = ""
                End Try
            End If
        End If
        update6171(text61, text71, SWC02text)
    End Sub

    Sub update6171(ByVal text61ps As String, ByVal text71ps As String, ByVal caseno As String)
        '把資料寫入資料庫()
        Dim conn As SqlConnection = New SqlConnection()
        Dim sqlcom As SqlCommand = New SqlCommand()
        conn.ConnectionString = ConfigurationManager.ConnectionStrings("SWCConnStr").ConnectionString
        conn.Open()
        sqlcom.Connection = conn
        sqlcom.CommandText = "UPDATE [SWCSWC] SET [SWC29] = @SWC29 ,[SWC30] = @SWC30 WHERE [SWC02]='" + caseno + "'"
        '參數設定從這邊開始
        sqlcom.Parameters.Add(New SqlParameter("@SWC29", text61ps))
        sqlcom.Parameters.Add(New SqlParameter("@SWC30", text71ps))
        '參數設定到這邊
        sqlcom.ExecuteNonQuery()
        sqlcom.Cancel()
        conn.Close()
        conn.Dispose()
        Label1.Text = Label1.Text + caseno + " => " + text61ps + " , " + text71ps + "<br />"
    End Sub

    Sub SendSMS(ByVal vbPhoneNo As String, ByVal vbText As String)
        Dim x As New GBClass001
        x.SendSMS(vbPhoneNo, vbText)
    End Sub
	
	Sub SendSMS_Arr(ByVal vbPhoneNoArr() As String, ByVal vbText As String)
        Dim x As New GBClass001
        x.SendSMS_Arr(vbPhoneNoArr, vbText)
    End Sub
	
    Public Function ConvertDateValue(ByVal fDate As String, ByVal fMode As String) As String
        '過濾null日期，或是指定日期轉變空白，預留mode參數，可變化使用
        'mode=1，2016/1/1，清為空白

        fDate = (fDate + "").Trim()

        If fDate <> "" And fMode = "1" Then
            If fDate = "1912/1/1 上午 12:00:00" Then
                fDate = ""
            End If
            If fDate = "1900/1/1 上午 12:00:00" Then
                fDate = ""
            End If
        End If
        Return fDate
    End Function

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'Dim tPhoneNo As String = "0928913441"
        'Dim tSMSText As String = "親愛的水土保持義務人您好， 提醒您，【臺北市中山區北安段一小段82-2地號(道路用地)自闢道路開發案劍南路道路工程】暫停審查期限將至，請於暫停審查期限(2018-06-30)內提送審查單位續審!!"
        'Dim tPhoneNo As String() = {"0901373880","0901373880"}
		'Dim tSMSText As String = "測試義務人排程簡訊"
		'SendSMS_Arr(tPhoneNo, tSMSText)
    End Sub

    Sub mailGVSMS(ByVal mailtowho As String, ByVal mailname As String, ByVal mailps As String, ByVal whichcheck As String)
        Try

            '以下是舊的messagemail的程式，參考用
            '設定寄給誰
            Dim mail As MailMessage = New MailMessage(smtpmailsend, mailtowho)
            '設定寄給GV test
            mail.Bcc.Add(ConfigurationManager.AppSettings("GVmail"))
                mail.SubjectEncoding = Encoding.UTF8
            '設定主旨
            mail.Subject = mailname + "您好，" + Now.ToString("yyyy-MM-dd") + " " + whichcheck
            mail.IsBodyHtml = True '是否允 HTML 格式
                mail.BodyEncoding = Encoding.UTF8
                '信件BODY
                mail.Body = "<br />" + mailps
                mail.Body = mail.Body + "<br /><br />" + "臺北市政府工務局大地工程處系統管理員 敬上<br /><br /><br />＜此封信為系統自動發送，請勿直接回信，若有任何問題請洽臺北市政府工務局大地工程處＞"
            '設定SMTP帳號密碼_北市府
            Dim smtp As SmtpClient = New SmtpClient(smtpmailserver)
            smtp.Credentials = New NetworkCredential(smtpmailusername, smtpmailuserpassword) 'SMTP 帳號密碼
			smtp.Port = 465
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
			Response.Write(ex)
        End Try
    End Sub
End Class
