<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BasPg001.aspx.cs" Inherits="SWCDOC_BasicInfo_BasPg001" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>臺北市水土保持申請書件管理平台</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="../images/logo-s.ico">
    <meta name="keywords" content="臺北市水土保持申請書件管理平台, 書件管理平台">
    <meta name="description" content="臺北市水土保持申請書件管理平台">
    <meta name="author" content="dobubu">
    <meta name="copyright" content="© 2017 臺北市水土保持申請書件管理平台">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1"/>
    <meta name="viewport" content="width=device-width">
    <link rel="stylesheet" type="text/css" href="~/css/reset.css" />
    <link rel="stylesheet" type="text/css" href="~/css/all.css?20200212" />
    <link rel="stylesheet" type="text/css" href="~/css/iris.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.0/jquery.min.js"></script>
    <script type="text/javascript">
        function checkAll(chk) {
            $('#<%= GVETUser.ClientID %> .myClass input[type=checkbox]').prop("checked", chk.checked);
        }
        function checkAll2(chk) {
            $('#<%= GridView1.ClientID %> .myClass input[type=checkbox]').prop("checked", chk.checked);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <div class="wrap-s">
        <div class="header-wrap-s">
            <div class="header header-s clearfix"><a href="../../SWCDOC/SWC001.aspx" class="logo-s"></a>
                <div class="header-menu-s">
                    <ul>
                        <li><a href="/sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                        <li>|</li>
                        <li><a href="https://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                        <asp:Panel ID="GoTslm" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                        <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="/SWCDOC/SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                        <asp:Panel ID="LogOutLink" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="../SWC000.aspx?ACT=LogOut" title="登出">登出</a></li></asp:Panel>
                    </ul>
                </div>
            </div>
                    
            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">孜然養樂多，您好</asp:Literal></span>
                </div>
            </div>
        </div>
        
        <div class="content-s">

            <div class="tab-frame">
            
             <input type="radio" checked name="tab" id="tab1" />
             <label for="tab1" id="select_1" style="display:none;">選擇審查公會技師</label>
             <input type="radio" name="tab" id="tab2" />
             <label for="tab2" id="select_2" style="display:none;">選擇檢查公會技師</label>
             

             <div class="tab">
                 <div class="newtitle"><img src="../../images/title/swcn-04.png" alt=""/></div><br/><br/>
                 <asp:GridView ID="GVETUser" runat="server" CssClass="ADDGridView AutoNewLine checkbox" AutoGenerateColumns="false" PagerStyle-CssClass="pgr"
                   EmptyDataText="沒有符合條件的技師資料">
                    <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CHKALL" runat="server" onclick="checkAll(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CHKSub" runat="server" CssClass="myClass" Checked='<%# Convert.ToBoolean(Eval("SELCHK")) %>' />
                            <asp:HiddenField ID="HDETID" runat="server" Value='<%# Server.HtmlEncode(Convert.ToString(Eval("ETID"))) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                                   
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="SWC000"/>
                        <asp:BoundField DataField="Phone" HeaderText="手機" SortExpression="SWC002"/>
                        <asp:BoundField DataField="GuildName" HeaderText="職業機構名稱" SortExpression="SWC004"/>
                        <asp:HyperLinkField DataNavigateUrlFields="FileA" DataTextField="FileAText" HeaderText="公會會員證書" Target="_blank" />
                        <asp:HyperLinkField DataNavigateUrlFields="FileB" DataTextField="FileBText" HeaderText="技師審查經歷" Target="_blank" />
                        <asp:BoundField DataField="EXP" HeaderText="有效日期" SortExpression="SWC013"/>
                        <asp:BoundField DataField="TCGECHK" HeaderText="審核通過" SortExpression="SWC013"/>  
                    </Columns>
                </asp:GridView>
              </div>


             <div class="tab">
               <div class="newtitle"><img src="../../images/title/swcn-10.png" alt=""/></div><br/><br/>
               <asp:GridView ID="GridView1" runat="server" CssClass="ADDGridView AutoNewLine checkbox" AutoGenerateColumns="false" PagerStyle-CssClass="pgr"
                   EmptyDataText="沒有符合條件的技師資料">
                    <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID="CHKALL" runat="server" onclick="checkAll2(this);" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="CHKSub" runat="server" CssClass="myClass" Checked='<%# Convert.ToBoolean(Eval("SELCHK")) %>' />
                            <asp:HiddenField ID="HDETID" runat="server" Value='<%# Server.HtmlEncode(Convert.ToString(Eval("ETID"))) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                                   
                        <asp:BoundField DataField="" HeaderText="" SortExpression=""></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="SWC000"/>
                        <asp:BoundField DataField="Phone" HeaderText="手機" SortExpression="SWC002"/>
                        <asp:BoundField DataField="GuildName" HeaderText="職業機構名稱" SortExpression="SWC004"/>
                        <asp:HyperLinkField DataNavigateUrlFields="FileA" DataTextField="FileAText" HeaderText="公會會員證書" Target="_blank" />
                        <asp:HyperLinkField DataNavigateUrlFields="FileB" DataTextField="FileBText" HeaderText="技師審查經歷" Target="_blank" />
                        <asp:BoundField DataField="EXP" HeaderText="有效日期" SortExpression="SWC013"/>
                        <asp:BoundField DataField="TCGECHK" HeaderText="審核通過" SortExpression="SWC013"/>  
                          
                    </Columns>
                </asp:GridView>
             </div>
 

            <div class="checklist-btn" style="text-align:center;">
                <asp:Button ID="SaveData" runat="server" Text="存檔" OnClientClick="return chkInput('');" OnClick="SaveData_Click" />
                <asp:TextBox ID="TextBox1" runat="server" style="display:none;"></asp:TextBox>
            </div>


		</div>	
	</div>
           <div class="footer">
                <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                   <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                   <span class="span2">建議使用IE11.0(含)以上，Chrome或Firefox版本瀏覽器 資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                   <span class="span2">客服電話：02-27929328 陳小姐 本系統由多維空間資訊有限公司開發維護</span><br/>
							<span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
           </div>
	</div>

    </form>

     <script src="../js/inner.js"></script>

</body>
    <script type="text/javascript">
        if (document.getElementById("TextBox1").value == '2') {
            document.getElementById("tab2").checked = true;
        }
        if (document.getElementById("TextBox1").value == '3') {
            select_1.style.display = "block";
            select_2.style.display = "block"; 
        }
    </script>
</html>



