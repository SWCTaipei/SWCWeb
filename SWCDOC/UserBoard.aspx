<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserBoard.aspx.cs" Inherits="SWCDOC_UserBoard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>臺北市水土保持申請書件管理平台-留言版</title>

    <link rel="stylesheet" type="text/css" href="../css/reset.css" />
    <link rel="stylesheet" type="text/css" href="../css/all.css" />
    <link rel="stylesheet" type="text/css" href="../css/iris.css" />
    <link rel="stylesheet" type="text/css" href="../css/ad.css" />
    <script type="text/javascript">
        function chkInput() {
            if (document.getElementById('TXTTB003').value == '') {
                alert('請填寫留言主旨。'); return false;
            }
            if (document.getElementById("TXTTB004").value == '') {
                alert('請填寫留言內容。'); return false;
            }
        }
        function textcount(txtobj, labobj, txtcount) {
            var textboxtemp = document.getElementById(txtobj);
            var labeltemp = document.getElementById(labobj);

            if (window.event.keyCode == 13) {
                window.event.keyCode = null;
            }
            var ttval = textboxtemp.value.length;
            if (ttval > parseInt(txtcount)) {
                window.event.keyCode = null;
                textboxtemp.innerText = textboxtemp.value.substring(0, parseInt(txtcount));
                ttval = textboxtemp.value.length;
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
            else {
                labeltemp.innerText = "(" + ttval + "/" + txtcount + ")";
            }
        }
    </script>

    <script type="text/javascript">

            //test..
            function inFalseFilebox(obj, id) {

                $('#' + id).val(obj.value);

            }

            //第二個是點擊按鈕後的上傳檔案事件
            function fileUpload(id) {

                //取得該filebox中的檔案資料：
                var files = document.getElementById(id).files;
                //用JQ也可以寫成：
                // var files = $('#'+id)[0].files;
                //+
                //再來將剛剛取得的檔案資料放進FormData裡
                var fileData = new FormData();
                //files[0].name會回傳包含副檔名的檔案名稱
                //所以要做檔案類型的判斷也可以用file[0].name做
                fileData.append(files[0].name, files[0]);
                alert("ddd");
                //之後送ashx做處理 "~/PBMPage/FileUploadHandler.ashx"
                $.ajax({
                    url: "FileUploadHandler.ashx",
                    type: "post",
                    data: fileData,
                    contentType: false,
                    processData: false,
                    async: false,
                    success: function () {
                        //跳訊息提示
                        alert('上傳成功!');
                        //清掉假filebox中的內容
                        document.getElementById('false_fileBox').value = '';
                    }
                });
                alert("ccc");

            }
            function FileUploadSelFile(obj, id) {
                var file = obj.value;
                var strFileName = file.replace(/^.+?\\([^\\]+?)(\.[^\.\\]*?)?$/gi, "$1");  //正則表示式獲取檔名，不帶字尾
                var FileExt = file.replace(/.+\./, "");
                $('#' + id).val(strFileName + '.' + FileExt);
            }

    </script>

</head>

<body>
    <form id="form1" runat="server">

        <div class="wrap-s">
            <div class="header-wrap-s">
                <div class="header header-s clearfix">
                    <a href="SWC001.aspx" class="logo-s"></a>

                    <div class="header-menu-s">
                        <ul>
                            <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                            <li>|</li>
                            <li><a href="http://www.swc.taipei/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                            <asp:Panel ID="GoTslm" runat="server" Visible="false">
                                <li>|&nbsp&nbsp&nbsp&nbsp<a href="http://172.28.100.55/TSLM" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li>
                            </asp:Panel>
                            <asp:Panel ID="TitleLink00" runat="server" Visible="false">
                                <li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li>
                            </asp:Panel>
                            <asp:Panel ID="UserBoard00" runat="server" Visible="false">
                                <li>|&nbsp&nbsp&nbsp&nbsp<a href="UserBoardList.aspx" title="留言板">留言板</a></li>
                            </asp:Panel>
                            <asp:Panel ID="GOVMG" runat="server" Visible="false">
                                <li class="flip">|&nbsp&nbsp&nbsp<a href="#" title="系統管理">系統管理+</a>
                                    <ul class="openlist" style="display: none;">
                                        <li><a href="../SWCTCGOV/SWCGOV001.aspx">防災事件通知</a></li>
                                        <li><a href="../SWCTCGOV/SWCGOV011.aspx">公佈欄</a></li>
                                        <li><a href="../SWCDOC/UserBoardList.aspx">留言版</a></li>
										<li><a href="http://tgeo.swc.taipei/">T-GEO空間地理資訊平台</a></li>
                                    </ul>
                                </li>
                            </asp:Panel>
                            <li>|</li>
                            <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
                        </ul>
                    </div>
                </div>

                <div class="header-s-green">
                    <div class="header-s-green-nameWrap">
                        <span>
                            <asp:Literal ID="TextUserName" runat="server">大雷包，您好</asp:Literal></span>
                    </div>
                </div>
            </div>

            <div class="contentFooter">
                <div class="content-s content-s-inquire">
                    <div class="detailsMenu">
                        <img src="../images/title/message-02.png" />
                    </div>

                    <table class="MSbox" rules="all">
                        <tr>
                            <th>編號</th>
                            <td>
                                <asp:Label ID="LBTB001" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>留言主旨</th>
                            <td>
                                <asp:TextBox ID="TXTTB003" runat="server" MaxLength="50" autocomplete="off" CssClass="MStype" />
                            </td>
                        </tr>
                        <tr>
                            <th style="vertical-align: middle">留言內容</th>
                            <td>
                                <asp:TextBox ID="TXTTB004" runat="server" Width="100%" Height="100px" TextMode="MultiLine" MaxLength="255" onkeyup="textcount('TXTTB004','TXTTB004_count','255');" />
                                <asp:Label ID="TXTTB004_count" runat="server" Text="(0/255)" ForeColor="Red" /></td>
                        </tr>
                        <tr>
                            <th>附件上傳</th>
                            <td>
                                <asp:Panel ID="PanelPT05" runat="server" Visible="true">
                                    <input type="button" value="選擇檔案" onclick="<%=FULFilePT05.ClientID %>.click();" />
                                    <input type="text" class="edit filetxt" id="TextPgFilePT05" onclick="<%=FULFilePT05.ClientID %>.click();" />
                                    <asp:FileUpload ID="FULFilePT05" runat="server" Style="display: none;" onchange="FileUploadSelFile(this,'TextPgFilePT05');" />
                                    <asp:Button ID="BtnFilePT05_upload" runat="server" Text="確認上傳" OnClick="BtnFile_upload_Click" CssClass="allbtn" />
                                    <asp:TextBox ID="TBUpLoadFilePT05" runat="server" Visible="false" />
                                    <br />

                                </asp:Panel>
                                <asp:HyperLink ID="HLTBPT05" Text="" runat="server" CssClass="word" Target="_blank" Visible="false" />
                                <asp:Button ID="BtnFilePT05_del" runat="server" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')"
                                    OnClick="BtnFile_del_Click" Visible="true" />
                                <br />
                                <asp:Image ID="IMTBPT05" runat="server" CssClass="imgsize" />
                            </td>
                        </tr>
                    </table>

                    <div class="btn">
                        <asp:Button ID="DataLock" runat="server" Text="送出" OnClick="DataLock_Click" OnClientClick="chkInput();return false;" />
                    </div>
                </div>
            </div>

            <div class="footer">
               <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                     <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                      <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                      <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			          <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>
        </div>

        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js?202103230541"></script>
    </form>
</body>
</html>
