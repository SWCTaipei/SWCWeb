<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWC00X.aspx.cs" Inherits="SWCDOC_SWC000" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="X-UA-Compatible" content="IE=9,IE=10,IE=11,IE=EDGE, chrome=1">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dorathy">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <meta name="viewport" content="width=device-width, user-scalable=no, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"?20201110/>
    <link rel="stylesheet" type="text/css" href="../css/ad.css"/>
    
	<!-- Google Tag Manager -->
	<script>(function(w,d,s,l,i){w[l]=w[l]||[];w[l].push({'gtm.start':
	new Date().getTime(),event:'gtm.js'});var f=d.getElementsByTagName(s)[0],
	j=d.createElement(s),dl=l!='dataLayer'?'&l='+l:'';j.async=true;j.src=
	'https://www.googletagmanager.com/gtm.js?id='+i+dl;f.parentNode.insertBefore(j,f);
	})(window,document,'script','dataLayer','GTM-MSP6KWH');</script>
	<!-- End Google Tag Manager -->
	
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/index.js"></script>
    <script type="text/javascript">
        function chkInput(jChkType) {
            var Today = new Date();
            if (Today.getFullYear() == '2020' && Today.getMonth() + 1 == '4' && Today.getDate() == '1' && Today.getHours() > 6) { alert('因松德辦公大樓預計於4月2日上午07:00至4月3日上午08:00斷電進行電器設備檢測維護，屆時本網站將暫停服務，預計於4月3日上午08:00恢復供電後即服務，不便之處敬請見諒！'); return false; }
            if (Today.getFullYear() == '2019' && Today.getMonth() + 1 == '11' && Today.getDate() == '16') { alert('因松德辦公大樓預計於4月2日上午07:00至4月3日上午08:00斷電進行電器設備檢測維護，屆時本網站將暫停服務，預計於4月3日上午08:00恢復供電後即服務，不便之處敬請見諒！'); return false; }
            if (Today.getFullYear() == '2020' && Today.getMonth() + 1 == '4' && Today.getDate() == '3' && Today.getHours() < 8) { alert('因松德辦公大樓預計於4月2日上午07:00至4月3日上午08:00斷電進行電器設備檢測維護，屆時本網站將暫停服務，預計於4月3日上午08:00恢復供電後即服務，不便之處敬請見諒！'); return false; }

            var x = document.getElementById("loginChange");
            var jUserType = (x.options[x.selectedIndex].text);

            var jInputID = document.getElementById('TXTID').value;
            var jInputPW = document.getElementById('TXTPW').value;

            switch (jUserType) {
                case "水土保持義務人":

                    if (jInputID == '')
                    {
                        alert('請輸入身份證字號');
                        return false;
                    }
                    if (jInputPW == '') {
                        alert('請輸入手機');
                        return false;
                    }
                    if (jInputPW.length != 10 || isNaN(jInputPW)) {
                        alert('手機格式不正確，請輸入10位數字號碼');
                        return false;
                    }
                    break;

                case "技師/各類委員":
                    document.getElementById('TXTID').value = jInputID.toUpperCase();

            }
        }
    </script>
</head>

<body>
	<!-- Google Tag Manager (noscript) -->
	<noscript><iframe src="https://www.googletagmanager.com/ns.html?id=GTM-MSP6KWH"
	height="0" width="0" style="display:none;visibility:hidden"></iframe></noscript>
	<!-- End Google Tag Manager (noscript) -->
	
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div>
    
    <div class="wrap-b">
      <div class="header1 header-b1"><a href="#" class="logo-b"></a></div>
        <div class="indexul">
          <ul>
            <li><a href="SWCTCGOV/SWCGOV011p.aspx" title="公佈欄" target="_blank">公佈欄</a></li>
            <li>|</li>
			<li><a href="SWCDOC/sample.aspx" title="申請文件範例" target="_blank">申請文件範例</a></li>
            <li class="DMnone">|</li>
			<li><a href="sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
            <li class="DMnone">|</li>
           <li class="flip"><a href="#" title="相關連結">相關連結+</a>
		       <ul class="openlist" style="display: none;">
		         <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統</a></li>
                 <li class="dashed"></li>
		         <li><a href="http://www.geo.gov.taipei/" title="臺北市政府工務局大地工程處" target="_blank">臺北市政府工務局大地工程處</a></li>
                 <li class="dashed"></li>
                 <li><a href="http://tgeo.swc.taipei/" target="_blank">T-GEO空間地理資訊平台</a></li>
		       </ul>
            </li>
          </ul>
        </div>
      </div>

        
        <div style="clear:both"></div>
        <div id="Marquee" title="" class ="news" style="display:none;">
            <asp:UpdatePanel ID="MarqueeUpdatePanel" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:TextBox ID="Marqueeclick" runat="server" AutoPostBack="True" style="display:none ;"></asp:TextBox>
                    <asp:Button ID="MarqueeButton" runat="server" Text="跑馬燈在這邊顯示" OnClientClick="MarqueeMessageDiv.style.display='';document.getElementById('Marqueeclick').value='ShowDtl';"  CssClass="news_buttom" BorderStyle="None" />
                    <asp:Timer ID="MarqueeTimer" runat="server" Interval="5000" OnTick="MarqueeTimer_Tick"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


      <div class="content-b">
        <div class="login"><span id="login1">身份</span>
            <asp:DropDownList ID="loginChange" runat="server" onchange="showChange(this.value);" /><br/><br/>
            <span id="login2">請輸入</span>
            <asp:TextBox ID="TXTID" runat="server" placeholder="身分證字號" MaxLength="10" /><br/><br/>
            <span id="login3">密碼</span>
            <asp:TextBox ID="TXTPW" runat="server" placeholder="手機" MaxLength="15" /><br/><br/><br/>
            <asp:HyperLink ID="NewUser" runat="server" Text="首次登入" />

            <asp:ImageButton ID="SwcLogin" runat="server" OnClientClick="return chkInput('');" OnClick="SwcLogin_Click" title="登入" ImageUrl="../images/btn/btn-login.png" CssClass="imgLogin"/>
        </div>
      </div>

            <div class="footer-b">
                <div class="footer-b-green"></div>
                    <div class="footer-bb-brown">
                        <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                    <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
                    </div>
                </div>
                
            </div>
        
       <div style="clear:both"></</div>

        <div id="MarqueeMessageDiv" class="model"  style="filter: Alpha(Ppacity=80); display:none;">

            <span  class="newstitleD">最新消息</span><br /><br /><br />
        <p>
            <asp:UpdatePanel ID="MarqueeMessageUpdatePanel" runat="server" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Label ID="MarqueeMessageLabel" runat="server" Text="跑馬燈在這邊顯示" CssClass="newsp"></asp:Label> <br/>
                    <asp:HyperLink ID="LinkFile" runat="server" Target="_blank" />
                    
                    <asp:Button ID="MarqueeMessageButton" runat="server" Text="X" CssClass="close-s" OnClientClick="MarqueeMessageDiv.style.display='none';document.getElementById('Marqueeclick').value='NoDtl';"/>
                    
                </ContentTemplate>
            </asp:UpdatePanel>
        </p>
        
            <script type="text/javascript">
                var x = document.getElementById("loginChange");
                var str = x.options[x.selectedIndex].text;   
                showChange(str);
                //MarqueeMessageDiv.style.display = 'none';
                document.getElementById('Marqueeclick').value = 'NoDtl';

                if (document.getElementById('MarqueeButton').value == '跑馬燈在這邊顯示') {}else{
                    document.getElementById("Marquee").style.display = '';
                    //document.getElementById("MarqueeMessageDiv").style.display = '';
                }
            </script>

            <asp:Literal ID="error_msg" runat="server"></asp:Literal>

        </div>
        <script src="../js/allhref.js"></script>
    </form>
    
</body>
</html>