<!--
    Soil and Water Conservation Platform Project is a web applicant tracking system which allows citizen can search, view and manage their SWC applicant case.
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
-->

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SwcPrivacy_01.aspx.cs" Inherits="PriPage_Privacy_01" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <script type="text/javascript">
        function chkPrivacy(jPrivacy) {
            if (jPrivacy.checked) {
                document.getElementById("GoSwcDoc").disabled = false;
            } else {
                document.getElementById("GoSwcDoc").disabled = true;
            }
        }
    </script>
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
                        <li><a href="http://tcgeswc.taipei.gov.tw/index_new.aspx" title="水土保持計畫查詢系統" target="_blank">水土保持計畫查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
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
            <p class="firstindent">以下的隱私權保護政策，適用於您在本平台以及其延伸服務網站（以tcgeswc.taipei.gov.tw網域為原則）活動時，所涉及的個人資料收集、運用與保護。但不適用於經由本平台搜尋連結之其他網站，當您在這些網站進行活動時，關於個人資料的保護，適用各該網站的隱私權保護政策。</p><br>
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


        <br><br><br><br><br>

        <p align="center" style="font-weight:bold;font-size:20px;">臺北市水土保持申請書件管理平台服務條款</p><br>
        <p>歡迎來到臺北市水土保持申請書件管理平台網站（以下簡稱本平台）！臺北市政府工務局大地工程處（以下簡稱本處）係依據本服務條款提供各項服務，當您註冊完成或開始使用本平台服務時，即表示您已閱讀、了解並同意接受本服務條款之所有內容。如果您不同意本服務條款的內容，或者您所屬的國家或地域排除本服務條款內容之全部或部分時，您應立即停止使用本服務。此外，當您使用本服務之特定功能時，可能會依據該特定功能之性質，而須遵守本服務所另行公告之服務條款或相關規定。此另行公告之服務條款或相關規定亦均併入屬於本服務條款之一部分。本處有權於任何時間修改或變更本服務條款之內容，並公告於本服務網站上，請您隨時注意該等修改或變更。若您於任何修改或變更後繼續使用本服務，則視為您已閱讀、了解並同意接受該等修改或變更。</p><br>
        <p>若您為未滿二十歲之未成年人，則應請您的父母或監護人閱讀、了解並同意本服務條款之所有內容及其後之修改變更，方得使用本服務。當您使用本服務時，即推定您的父母或監護人已閱讀、了解並同意接受本服務條款之所有內容及其後之修改變更。</p><br>
        <p class="title16blk">一般條款：</p>
        <p>本服務條款構成您與本處就您使用本服務之完整合意，取代您先前與本處間有關本服務所為之任何約定。本服務條款之解釋與適用，以及與本服務條款有關的爭議，除法律另有規定者外，均應依照中華民國法律予以處理，並以台灣台北地方法院為管轄法院。<br>
          未行使或執行本服務條款任何權利或規定，不構成前開權利或規定之棄權。若任何本服務條款規定，經有管轄權之法院認定無效，當事人仍同意法院應努力使當事人於前開規定所表達之真意生效，且本服務條款之其他規定仍應完全有效。 </p><br>
        <p class="title16blk">隱私權政策：</p>
        <p>您所提供的註冊資訊及其他於利用本網站服務時所提供之個人資料，本平台將依【<a href="personal_policy.aspx" target="_blank">隱私權政策</a>】進行蒐集、利用與保護。
</p><br>
        <p class="title16blk">會員的註冊義務及會員帳號、密碼安全：</p>
          <p>為了能使用本服務，您同意並承諾以下事項：</p><br>

          <p class="allindent2em_no1">1.　依本服務註冊流程之提示提供您本人正確及完整的資料，並維持、更新該資料，確保其為正確、最新及完整。</p>
          <p class="allindent2em_no1">2.　若您提供任何不完整、錯誤或不實的資料，本處有權暫停或終止您的帳號，並拒絕您使用本服務之全部或部分。</p><br>

        <p>完成本服務的註冊流程後，您將收到註冊通知。您同意並承諾以下事項：</p><br>
          <p class="allindent2em_no1">1.　此帳號係不可轉讓。</p>
          <p class="allindent2em_no1">2.　您有責任維持此帳號及密碼的機密安全。</p>
          <p class="allindent2em_no1">3.　當您的帳號遭到盜用或有其他任何安全問題發生時，您將立即通知本處。</p>
          <p class="allindent2em_no1">4.　您同意本平台依法律或契約有需要通知您時，得以電子文件向您於註冊時或後續更新之電子郵件帳號為通知，請您提供正確且經常使用的電子郵件帳號，以維護您的權益。</p><br>

        <p class="title16blk">會員使用規範與義務：</p>
        <p>除了遵守本服務條款之約定外，您同意遵守中華民國相關法規、本服務其他服務條款，並同意不從事以下行為：</p>
        <p>您若係中華民國以外之使用者，並同意遵守所屬國家或地域之相關法規。您同意並保證絕不利用本服務從事違法或損害他人權益之行為。</p>
          <p class="allindent2em_no1">1.　冒用他人名義使用本服務。</p>
          <p class="allindent2em_no1">2.　侵害他人名譽、隱私權、著作權、智慧財產權及其他權利。</p>
          <p class="allindent2em_no1">3.　上載、張貼或公布任何不實、誹謗、侮辱、具威脅性、攻擊性、違背公序良俗、引人犯罪或其他不法之文字、圖片或任何形式的檔案。</p>
          <p class="allindent2em_no1">4.　違反依法律或契約所應負之保密義務。</p>
          <p class="allindent2em_no1">5.　上載、張貼或公佈任何含有電腦木馬、病毒或任何對電腦軟、硬體產生中斷、破壞或限制功能之程式碼或資料。</p>
          <p class="allindent2em_no1">6.　從事販賣槍枝、毒品、禁藥、盜版軟體、其他違禁物或其他不法交易行為。</p>
          <p class="allindent2em_no1">7.　破壞及干擾本服務所提供之各項資料、活動或功能，或以任何方式侵入、試圖侵入、破壞本服務之任何系統，或藉由本服務為任何侵害或破壞行為。</p>
          <p class="allindent2em_no1">8.　未經同意收集他人電子郵件位址及其他個人資料者。</p>
          <p class="allindent2em_no1">9.　其他本處有正當理由認為不適當之行為。</p><br>

        <p>若您有違反前開事項之情事或之虞，本處有權於通知或未通知之情形下，隨時終止或限制您使用帳號（或其任何部分）或本服務之使用，並將本服務內任何相關「會員內容」加以移除並刪除。您同意若本服務之使用被終止或限制時，本處對您或任何第三人均不承擔責任。</p><br>
          <p class="title16blk">您對本服務之授權</p>
          <p>若您無合法權利得授權他人使用、修改、發行、散布、重製或公開展示某資料，並將前述權利轉授權第三人，請勿擅自將該資料上載、輸入或提供予本服務。您所上載、輸入或提供予本服務之任何資料，其權利仍為您或您的授權人所有；但任何資料一經您上載、輸入或提供予本服務時，即表示您已同意授權本處可以基於公益或為宣傳、推廣本服務之目的，進行使用、修改、發行、散布、重製或公開展示該等資料，並得在此範圍內將前述權利轉授權他人。您並保證本服務使用、修改、發行、散布、重製、公開展示或轉授權該資料，不致侵害任何第三人之相關權利，否則應負相關法律責任。</p><br>
          <p class="title16blk">系統中斷或故障</p>
          <p>本服務有時可能會出現中斷或故障等現象，或許將造成您使用上的不便、資料喪失、錯誤等情形。您於使用本服務時宜自行採取防護措施。本處對於您因使用（或無法使用）本服務而造成的損害，不負任何賠償責任。</p><br>
          <p class="title16blk">與第三方網站之連結及第三方提供之內容</p>
          <p>本服務可能會提供連結至其他機關單位(以下稱「第三方」)之網站。第三方之網站均由各該機關單位自行負責，不屬本服務控制及負責範圍之內。本處對任何檢索結果或外部連結，不擔保其有效性、正確性、合適性、及完整性。您也許會檢索或連結到一些不合適或不需要的網站，遇此情形時，本服務建議您不要瀏覽或儘速離開該等網站。您並同意本處無須為您連結至前述非屬於本服務之網站所生之任何損害，負損害賠償之責任。本服務隨時會與其他機關單位等第三方（以下稱「內容提供者」）合作，由其提供包括政令、新聞、訊息、活動等不同內容供本服務刊登，且本服務於刊登時均將註明內容來源。基於尊重內容提供者之智慧財產權，本服務對其所提供之內容並不做實質之審查或修改。對該等內容之正確真偽，您宜自行判斷之，本處對該等內容之正確真偽不負任何責任。您若認為某些內容屬不適宜、侵權或有所不實，請逕向該內容提供者反應意見。</p><br>
          <p class="title16blk">服務變更及通知</p>
          <p>您同意本處保留於任何時間點，不經通知隨時修改、暫時或永久停止繼續提供本服務（或其任一部分）的權利。如依法或其他相關規定須為通知時，本服務得以包括但不限於：張貼於本服務網頁、電子郵件，或其他現在或未來合理之方式通知您，包括本服務條款之變更。但若您違反本服務條款，以未經授權的方式存取本服務，或於註冊時所提供之資料不正確或未更新，您將不會收到前述通知。當您經由授權的方式存取本服務，您即同意本服務所為之任何及所有給您的通知，均視為送達。</p><br>
          <p class="title16blk">智慧財產權的保護：</p>
          <p>本平台使用之程式、軟體及網站內容，包括但不限於：資訊、資料、圖片、檔案、網站架構、網頁設計及會員內容等，均由本處或其他權利人依法擁有其智慧財產權，包括但不限於專利權、著作權與專有技術等。任何人不得逕自修改、重製、散布、發行、公開發表、進行還原工程或反向組譯。若您欲引用或轉載前述資料，除明確為法律所允許者外，均須依法事前取得本處或其他權利人之書面同意。如有違反，您應負相關法律責任。</p><br>
          <P class="title16blk">智慧財產權或著作權之侵害處理</p>
          <p>本處尊重他人之智慧財產權，同樣也要求本服務的使用者尊重他人之智慧財產權。本服務得對於可能屬侵權之使用者暫停或終止其帳戶。若您認為您的著作權或智慧財產權遭受侵害，請提供以下資訊予本服務： </p>
          <p class="allindent2em_no1">1.　您的正確資料與聯絡方式，並有異議情形時，同意將其資料提供給被檢舉人。</p>
          <p class="allindent2em_no1">2.　能合法代表著作權或智慧財產權利益之所有人之證明。</p>
          <p class="allindent2em_no1">3.　您所主張受侵害之著作或其他智慧財產權之描述，以及受侵害資料之描述。</p>
          <p class="allindent2em_no1">4.　您基於善意認為該利用未經著作權人其代理人或法律許可之聲明。</p>
          <p class="allindent2em_no1">5.　您在了解虛偽陳述之責任的前提下，對於上述載於您的通知上之資訊的正確性之聲明。</p><br>

        <p class="title16blk">對本服務的貢獻</p>
          <p>若您對於本服務提供任何想法、建議或提議（以下稱「貢獻」）並回饋給本處，您了解並同意： </p><br>
          <p class="allindent2em_no1">1.　該貢獻並非特定機密或專有資訊，且不侵害他人名譽、隱私權、著作權、智慧財產權及其他權利。</p>
          <p class="allindent2em_no1">2.　本處對該貢獻並不負有任何明示或默示的保密責任。</p>
          <p class="allindent2em_no1">3.　本處可能已有與該貢獻相似而正在考慮或發展中的想法或提案。</p>
          <p class="allindent2em_no1">4.　本處可以基於公益或為宣傳、推廣本服務之目的，以任何方式、在任何媒體上使用（或選擇不使用）該貢獻。</p>
          <p class="allindent2em_no1">5.　該貢獻將於本處不對您負任何責任的情形下，自動成為本處之財產。您於任何情形下皆無權利對本處主張任何形式的賠償或補償。</p>

        </p>
        <br>
        <br><span>  
                    <input type="checkbox" id="CheckBoxPrivacy" onclick="chkPrivacy(this);" />
                     本人已詳閱並同意「使用者規則」及「隱私權保護政策」。
                    <asp:Button ID="GoSwcDoc" runat="server" Text="進入系統" OnClick="GoSwcDoc_Click" Enabled="false" />
                </span><br><br><br><br></div>

           
             <div class="footer-b">
                <div class="footer-b-green"></div>
                    <div class="footer-bb-brown">
                        <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                           <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                            <span class="span2">建議使用IE8.0(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                           <span class="span2">客服電話：02-27593001#3718 許先生 本系統由多維空間資訊有限公司開發維護 TEL:(02)27929328</span></p>
                    </div>
                </div>
                
            </div>           
        
            


        
                

    </div>
    </form>
</body>
</html>
