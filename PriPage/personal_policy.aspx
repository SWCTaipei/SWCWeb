<%@ Page Language="C#" AutoEventWireup="true" CodeFile="personal_policy.aspx.cs" Inherits="PriPage_BlankTemplate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/textstyle.css"/>
    
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/index.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="../SWCDOC/SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="https://swc.taipei/swcinfo/" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="https://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>

            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
                </div>
            </div>
        </div>
        
        <div class="content-s">

 
            <p align="center" style="font-weight:bold;font-size:20px;">臺北市水土保持申請書件管理平台網站個人資料安全保護政策</p><br>
            <p class="title16blk">隱私權暨資訊安全保護政策</p>
          	<p class="firstindent">親愛的使用者，臺北市政府水土保持申請書件管理平台網站(以下簡稱本平台)非常重視並尊重使用者的隱私權。為了幫助您瞭解本平台如何收集、應用及保護您的個人資訊，請詳細閱讀以下本平台隱私權暨資訊安全保護政策，本政策將幫助您了解，在您使用本平台以及其延伸服務網站所提供的服務時，我們收集、運用及保護使用者個人資料的政策與原則。</p><br>
           	<p>若您為未滿二十歲之未成年人，則應請您的父母或監護人閱讀、了解以下說明。</p><br>
            <p class="title16blk">適用範圍</p>
            <p class="firstindent">以下的隱私權保護政策，適用於您在本平台以及其延伸服務網站（以swc.taipei網域為原則）活動時，所涉及的個人資料收集、運用與保護。但不適用於經由本平台搜尋連結之其他網站，當您在這些網站進行活動時，關於個人資料的保護，適用各該網站的隱私權保護政策。</p><br>
            <p class="title16blk">個人資料收集方式</p>
              <p class="firstindent">我們會透過以下幾種管道收集使用者個人資料： </p><br>
				<p class="allindent2em_no1">1.　註冊會員：當您於本平台註冊成為具承辦/監造技師身分之會員時，我們必須請您提供更多資訊，包含姓名、電子郵件、身分證字號、通訊地址、技師證書編號、執業機構、及相關證明文件等。</p>
                <p class="allindent2em_no1">2.　申請案件：當您於本平台提出水土保持申請書件立案申請時，我們必須請您提供申請人資料(包含姓名、身分證字號、手機號碼、通訊地址)、聯絡人資料(包含姓名、手機號碼)及相關水土保持申請書件等。</p>
                <p class="allindent2em_no1">3.　一般瀏覽：本平台會保留使用者在上網瀏覽或查詢時，伺服器自行產生的相關紀錄(LOG)，包括連線設備IP位址、使用時間、使用的瀏覽器、瀏覽及點選資料紀錄等。本平台會對個別連線者的瀏覽器予以標示，歸納使用者瀏覽器在本平台內部所瀏覽的網頁，除非您願意告知您的個人資料，否則本平台不會，也無法將此項紀錄和您的個人資料進行對應。</p>
                <p class="allindent2em_no1">4.　訂閱電子報：您希望透過電子信箱取得各項資訊服務時，必須正確填入電子信箱、性別等基本資料確保可收到所需資訊。我們因作業需求，會保留您填入的資料，並提供取消訂閱或變更之服務。</p>
                <p class="allindent2em_no1">5.　其他：除了您主動於網站登錄提供個人資料外，您可能在本平台的討論區等單元，主動提供個人資料如電子郵件，姓名等。這種形式的資料提供，不在本平台隱私權保護政策的範圍以內。此外，如果您寫信與臺北市政府聯繫或是透過其他管道向我們反應意見，我們也會保存此項通訊及處理紀錄。</p><br>

            <p class="title16blk">Cookies的運用與政策</p>
              <p class="firstindent">為提供個人化的服務，我們有時候會使用Cookies技術來儲存並在某些時候追蹤使用者的資料。Cookie是從網站傳送到瀏覽器，並保存在使用者電腦硬碟中的簡短資料。使用者可以在瀏覽器中選擇修改其對Cookies的接受程度，包括接受所有Cookies、設定Cookies時得到通知、拒絕所有Cookies等。如果您選擇拒絕所有的Cookies，可能無法使用部份個人化服務或是參與部份的活動。</p><br>
			<p class="firstindent">   一般而言，我們會依據以下目的及情況，在使用者瀏覽器中寫入並讀取Cookies︰</p>

              <p class="allindent2em_no1">1.　為提供更好、更個人化的服務：方便您參與個人化的互動活動。cookies在您註冊或登入時建立，並在您登出時修改。
              <p class="allindent2em_no1">2.　 為統計瀏覽人數及分析瀏覽模式：以了解網頁瀏覽的情況，做為改善服務的參考。
              <p class="allindent2em_no1">3.　追蹤點選宣導廣告或參加行銷活動情形：在發送電子報或網站主辦的行銷活動時，有時會寫入Cookies資料以追蹤整個活動過程中，使用者的參與程度及相關數據。</p><br>


		<p class="title16blk">個人資料的運用方式</p>
        
			<p class="firstindent">本平台不會出售、出租或任意交換任何您的個人資料給其他團體或個人。只有在以下狀況，本平台會在本政策原則之下，與第三者共用您的個人資料。</p>

			<p class="allindent2em_no1">1.　公務使用：本平台基於增進公共利益所必要，得將所收集之水土保持申請書件相關資料，作為辦理審查、施工中監督檢查及已完工設施檢查使用；該資料僅供辦理水土保持法及其子法規之用，將不會將該等資料用做其他用途。
			<p class="allindent2em_no1">2.　統計與分析：本平台根據使用者註冊、問卷調查、行銷活動或伺服器日誌文件，對使用者的人數、興趣和行為進行內部或委託學術研究。此研究是根據全體使用者的資料進行統計分析與整理，而我們所有的公開資訊或分析報告，將僅對全體使用者行為總和進行分析及公布，並不會提供特定對象個別資料之分析報告。</p><br>


		<p class="title16blk">資料的分享與公開方式</p>
			<p class="firstindent">本平台不會任意出售、交換、或出租任何您的個人資料給其他團體或個人。只有在以下狀況，本平台會在本政策原則之下，與第三者共用您的個人資料。</p>

			<p class="allindent2em_no1">1.　為了提供您其他服務或優惠權益：需要與提供該服務或優惠之第三者共用您的資料時，本平台會在活動時提供充分說明，並且在資料收集之前通知您，您可以自由選擇是否接受這項特定服務或優惠。
			<p class="allindent2em_no1">2.　為進行服務品質加值、評估及研究：為提供使用者各精確及優質之服務，本平台得按照資料保密協議，將使用者資料與第三人資料比較對照。此外，為了向未來的合作伙伴、廣告的單位及其他第三方介紹我們的服務及其他的合法目的，本平台得在不具名的情形之下，揭露使用者之統計資料。
			<p class="allindent2em_no1">3.　其他：本網站有義務保護其使用者隱私及個人資料，非經您本人同意不會自行修改、刪除或提供任何（或部份）使用者個人資料及檔案。除非經過您事先同意或符合以下情況；</p>

                <p class="Indentation">一、中華民國司法檢調單位透過合法程序進行調閱。</p>
                  
                <p class="Indentation">二、違反網站相關規章且已造成脅迫性。</p>
                  
                <p class="Indentation">三、基於主動衍伸政府網站服務效益之考量。</p>
                  
                <p class="Indentation">四、保護其他使用者之合法使用權益</p><br>



		<p class="title16blk">資訊安全保護措施</p>
			<p class="firstindent">由於網際網路資料的傳輸不能保證百分之百的安全，儘管本平台努力保護使用者的個人資料安全，在部分情況下會使用通行標準的SSL保全系統，保障資料傳送的安全性。由於資料傳輸過程牽涉您上網環境保全之良窳，我們並無法確信或保證使用者傳送或接收本平台資料的安全，使用者須注意並承擔網路資料傳輸之風險。</p> 
			<p class="firstindent">  使用者基於個人意願在網站透露的各種個人資料，例如在討論區公布個人資料如電子郵件、姓名等，可能會被他人收集和使用，如果您擔心這些困擾，您可以不用輸入這些資料。如果您在網路上公布可被他人讀取的個人資料時，可能會收到其他團體主動提供的廣告或垃圾郵件。請您諒解此部份所造成的後果均非本平台所能控制範圍，我們也無法負擔任何責任。</p><br>
            <p class="title16blk">隱私權暨資訊安全保護政策修訂</p>
            <p class="firstindent">本平台會不定時修訂本項政策，以符合最新之隱私權保護規範。當我們在使用個人資料的規定做較大幅度修改時，我們會在網頁上張貼告示，通知您相關事項。</p><br>
            <p class="title16blk">隱私權暨資訊安全保護政策諮詢</p>
            如果您對於我們的隱私權保護政策或是有個人資料收集、運用、更新等問題，請於上班時間聯絡：02-27593001轉3726。</p>








        </div>
    
            <div class="footer-b">
                <div class="footer-b-green"></div>
                    <div class="footer-bb-brown">
                        <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">本系統建置維護廠商：多維空間資訊有限公司 TEL：02-27929328</span></p>
                    </div>
                </div>
                
            </div>
    </div>
    </form>
</body>
</html>
