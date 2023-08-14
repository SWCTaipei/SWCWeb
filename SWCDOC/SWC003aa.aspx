<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SWC003aa.aspx.cs" Inherits="SWCDOC_SWC003aa" %>

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
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome=1">
    <link rel="stylesheet" type="text/css" href="../css/reset.css"/>
    <link rel="stylesheet" type="text/css" href="../css/all.css"/>
    <link rel="stylesheet" type="text/css" href="../css/iris.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>


    <div class="wrap-s">
      <div class="header-wrap-s">
        <div class="header header-s clearfix"><a href="SWC001.aspx" class="logo-s"></a>
          <div class="header-menu-s">
            <ul>
                <li><a href="../sysFile/系統操作手冊.pdf" title="系統操作手冊" target="_blank">系統操作手冊</a></li>
                <li>|</li>
                <li><a href="http://swc.taipei/swcinfo/" title="臺北市山坡地保育利用資訊查詢系統" target="_blank">臺北市山坡地保育利用資訊查詢系統 </a></li>
                <asp:Panel ID="GoTslm" runat="server" Visible="true"><li>|&nbsp&nbsp&nbsp&nbsp<a href="http://tslm.swc.taipei/tslmwork/" title="坡地管理資料庫" target="_blank">坡地管理資料庫</a></li></asp:Panel>
                <asp:Panel ID="TitleLink00" runat="server" Visible="false"><li>|&nbsp&nbsp&nbsp&nbsp<a href="SWCBase001.aspx" title="帳號管理">帳號管理</a></li></asp:Panel>
                <li>|</li>
                <li><a href="SWC000.aspx?ACT=LogOut" title="登出">登出</a></li>
            </ul>
          </div>
        </div>
          
            <div class="header-s-green">
                <div class="header-s-green-nameWrap">
                    <span><asp:Literal ID="TextUserName" runat="server">香蕉抹茶巧克力奶昔，您好</asp:Literal></span>
                </div>
            </div>
        </div>
        <div class="contentFooter">
            <div class="content-s content-s-details">
                <div class="detailsMenu">
                    <img src="../images/title/title-details.png" alt=""/>                
                    <div class="detailsMenu-btn">
                        <asp:ImageButton ID="GoListPage" runat="server" OnClick="GoListPage_Click" title="返回總表" ImageUrl="../images/btn/btn-back.png" />
                        <asp:ImageButton ID="EditCase" runat="server" OnClick="EditCase_Click" title="編輯案件" ImageUrl="../images/btn/btn-edit.png" />
                        <asp:ImageButton ID="ReCaseVision" runat="server" Visible="false" title="變更設計" ImageUrl="../images/btn/btn-change.png" />
                    </div>
                </div>
                <div>
                    <asp:TextBox ID="sqllog" runat="server" Columns="3" Height="194px" TextMode="MultiLine" Width="521px"></asp:TextBox>
                </div>
                <div class="detailsGrid">
                    <h2 class="detailsBar_title_basic openh2">基本資料<img src="../images/btn/btn-close.png" alt=""/></h2>
                    <%--<h2 class="detailsBar_title_basic ">基本資料<img src="../images/btn/btn-close.png" alt="" class="open"/></h2>--%>
                    <div class="detailsGrid_wrap">
                        <table class="detailsGrid_skyblue">
                        <tr><td style="background: #d9d9d9;">案件編號</td>
                            <td class="detailsGrid_gray">
                                <asp:Label ID="LBSWC000" runat="server" /></td>
                            <td rowspan="24" class="innerBR">
                                變更設計：<br/>
                                <asp:GridView ID="GVSWCCHG" runat="server" CssClass="innerBR_file" AutoGenerateColumns="False"
                                    ShowHeader="false">
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="SWC002Link" DataTextField="SWC002" HeaderText="變更設計" Target="_blank"  />
                                    </Columns>
                                </asp:GridView>
                                <asp:Button ID="CopyCase" runat="server" Text="新增變更設計" Visible="false" CssClass="addchangbtn" OnClick="CopyCase_Click" /></td></tr>
                        <tr><td style="background: #d9d9d9;">水保局編號</td>
                            <td class="detailsGrid_gray">
                                <asp:Label ID="LBSWC002" runat="server"/></td></tr>
                        <tr><td style="background: #d9d9d9;">案件狀態</td>
                            <td class="detailsGrid_gray"><asp:Label ID="LBSWC004" runat="server"/></td></tr>
                        <tr><td>書件名稱</td>
                            <td><asp:Label ID="LBSWC005" runat="server"/></td></tr>
                        <tr><td>書件類別</td>
                            <td><asp:Label ID="LBSWC007" runat="server"/></td></tr>
                        <tr><td>地籍</td>
                            <td><asp:GridView ID="GVCadastral" runat="server" CssClass="startskyblue" AutoGenerateColumns="False" PagerStyle-CssClass="pgr"
                                    OnPageIndexChanged="GVCadastral_PageIndexChanged" OnPageIndexChanging="GVCadastral_PageIndexChanging" PageSize="5" AllowPaging="true">
                                    <Columns>
                                        <asp:BoundField DataField="序號" HeaderText="序號" />
                                        <asp:BoundField DataField="區" HeaderText="區" />
                                        <asp:BoundField DataField="段" HeaderText="段" />
                                        <asp:BoundField DataField="小段" HeaderText="小段" />
                                        <asp:BoundField DataField="地號" HeaderText="地號" />
                                        <asp:BoundField DataField="土地使用分區" HeaderText="使用分區" />
                                        <asp:BoundField DataField="土地可利用限度" HeaderText="可利用限度" />
                                        <asp:BoundField DataField="林地類別" HeaderText="林地類別" />
                                        <asp:BoundField DataField="地質敏感區" HeaderText="地質敏感區" />
                                    </Columns>
                                </asp:GridView></td></tr>
                        <tr><td>義務人</td>
                            <td><asp:Label ID="LBSWC013" runat="server"/></td></tr>
                        <tr><td>義務人身份證字號</td>
                            <td><asp:Label ID="TXTSWC013ID" runat="server" MaxLength="10" /></td></tr>
                        <tr><td>義務人手機</td>
                            <td><asp:Label ID="TXTSWC013TEL" runat="server" MaxLength="10" /></td></tr>
                        <tr><td>義務人地址</td>
                            <td><asp:Label ID="LBSWC014" runat="server"/></td></tr>
                        <tr><td>義務人及技師變更報備</td>
                            <td><asp:ImageButton ID="DTL_02_06_Link" runat="server" title="義務人及技師變更報備" ImageUrl="../images/btn/btn_ap01.png" OnClick="DTL_02_06_Link_Click" />
                        <asp:GridView ID="SWCOLA206" runat="server" CssClass="startskyblue" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA206_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="變更報備編號" />
                                <asp:BoundField DataField="OLA003" HeaderText="申請人" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA206" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA206_Click" />
                                        <asp:HiddenField ID="Lock206" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL206" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="ButtonDEL206_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView></td></tr>
                            
                        <tr><td>聯絡人</td>
                            <td><asp:Label ID="LBSWC015" runat="server"/></td></tr>
                        <tr><td>聯絡人手機</td>
                            <td><asp:Label ID="LBSWC016" runat="server"/></td></tr>
                        <tr><td>聯絡人E-mail</td>
                            <td><asp:Label ID="LBSWC108" runat="server"/></td></tr>
                        <tr><td>目的事業主管機關</td>
                            <td><asp:Label ID="LBSWC017" runat="server"/><asp:Label ID="LBSWC018" runat="server"/></td></tr>
                        <tr><td>計畫面積(公頃)</td>
                            <td><asp:Label ID="LBSWC023" runat="server"/>
                                <span style="margin-left:7px;">公頃</span></td></tr>
                        <tr><td style="background: #d9d9d9;">承辦技師</td>
                            <td class="detailsGrid_gray">
                                <asp:Label ID="LBSWC021" runat="server"/>
                                <asp:Label ID="LBSWC021ID" runat="server" Visible="false"/>
                            </td></tr>
                        <tr><td style="background: #d9d9d9;">承辦人員</td>
                            <td class="detailsGrid_gray"><asp:Label ID="LBSWC025" runat="server"/></td></tr>
                    <tr><td>開工申報</td>
                    <td><asp:ImageButton ID="DTL_02_04_Link" runat="server" title="開工申報" ImageUrl="../images/btn/btn_ap03.png" OnClick="DTL_02_04_Link_Click" />
                        <asp:GridView ID="SWCOLA204" runat="server" CssClass="startskyblue" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA204_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="開工申報編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="預定開工日期" />
                                <asp:BoundField DataField="OLA003" HeaderText="預定完工日期" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA204" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA204_Click" />
                                        <asp:HiddenField ID="Lock204" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL204" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>
                    <tr><td>工期展延</td>
                    <td><asp:ImageButton ID="DTL_02_07_Link" runat="server" title="工期展延" ImageUrl="../images/btn/btn_ap06.png" OnClick="DTL_02_07_Link_Click" />
                        <asp:GridView ID="SWCOLA207" runat="server" CssClass="startskyblue" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA207_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="工期展延編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="開工日期" />
                                <asp:BoundField DataField="OLA003" HeaderText="原核定完工日期" />
                                <asp:BoundField DataField="OLA004" HeaderText="目的事業主管機關核定(展延)工期" HeaderStyle-Width="150" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" HeaderStyle-Width="90" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA207" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA207_Click" />
                                        <asp:HiddenField ID="Lock207" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL207" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="ButtonDEL207_Click"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>
                    <tr><td>設施安全自主檢查表</td>
                    <td><asp:ImageButton ID="DTL_02_01_Link" runat="server" title="設施安全自主檢查表" ImageUrl="../images/btn/btn_ap09.png" OnClick="DTL_02_01_Link_Click" />
                        <asp:GridView ID="SWCOLA201" runat="server" CssClass="startskyblue" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA201_RowDataBound" >
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="安全自主檢查表編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="檢查日期" />
                                <asp:BoundField DataField="OLA003" HeaderText="社區(設施)地址" />
                                <asp:BoundField DataField="OLA004" HeaderText="義務人(聯絡人)" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA201" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA201_Click" />
                                        <asp:HiddenField ID="Lock201" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL201" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="ButtonDEL201_Click" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>
                        <tr><td>座標</td>
                            <td>X：<asp:Label ID="LBSWC027" runat="server"/>　　
                                Y：<asp:Label ID="LBSWC028" runat="server"/></td></tr>
                        <tr><td>計畫申請書</td>
                            <td><asp:GridView ID="SWCFILES001" runat="server" CssClass="startskyblue plan" AutoGenerateColumns="False"
                                >
                                <Columns>
                                    <asp:BoundField DataField="File001000" HeaderText="序號" />
                                    <asp:HyperLinkField DataNavigateUrlFields="File001004" DataTextField="File001003" HeaderText="計畫申請書" Target="_blank" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Button runat="server" CommandName="delfile001" Text="刪除" OnClientClick="return confirm('確認刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView></td></tr>
                    <tr><td>水保設施核定項目</td>
                        <td>
                                <asp:GridView ID="SDIList" runat="server" CssClass="segtable AutoNewLine" AutoGenerateColumns="False" Height="50">
                                    <Columns>
                                        <asp:BoundField DataField="SDIFD003" HeaderText="水土保持設施類別" HeaderStyle-Width="20%" />
                                        <asp:BoundField DataField="SDIFD004" HeaderText="設施名稱<br>（位置或編號）" HeaderStyle-Width="17%" HtmlEncode="false" />
                                        <asp:BoundField DataField="SDIFD005" HeaderText="設施型式" HeaderStyle-Width="12%" />
                                        <asp:BoundField DataField="SDIFD006D" HeaderText="原核定計畫之數量" HeaderStyle-Width="13%" />
                                        <asp:BoundField DataField="SDIFD008" HeaderText="檢核項目"/>
                                        <asp:BoundField DataField="SDIFD012D" HeaderText="原核定計畫之尺寸" HeaderStyle-Width="16%" />
                                    </Columns>
                                </asp:GridView></td></tr>
                        </table>
                </div>

                    
                    
            <asp:Panel ID="Area06" runat="server">
                <h2 class="detailsBar_title_accept openh2" style="background:#bce2e8">檔案交換區<img src="../images/btn/btn-open.png"" alt="" class="open"/></h2>
                
                <div class="detailsGrid_wrap close">
                <table class="filechange" style="border:1px solid #000000;"><tbody>
                <tr><td><div style="text-align:left; font-size:14pt;">修正本上傳區</div>
                    <table class="filebase"><tbody>
                    <tr><th>序號</th>
                        <th style="width:480px;">修正本上傳</th>
                        <th>連結</th>
                        <th style="width:13%">上傳人</th>
                        <th style="width:15%">上傳日期</th>
                        </tr>
                    <tr><td>第一次修正本</td>
                                <td><asp:FileUpload ID="SFFile01_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile01" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile01" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile01" runat="server" Width="70px" Visible="False" /></td>
                        <td style="color:blue; text-decoration:underline;">
                            <asp:HyperLink ID="SFLINK01" runat="server" Target="_blank"/></td>
                        <td><asp:Label ID="LBUPLOADU01" runat="server"/></td>
                        <td><asp:Label ID="LBUPLOADD01" runat="server"/></td>
                    </tr>
                            <tr>
                                <td>第二次修正本</td>
                                <td><asp:FileUpload ID="SFFile02_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile02" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile02" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile02" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue;text-decoration:underline;">
                                    <asp:HyperLink ID="SFLINK02" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU02" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD02" runat="server"/></td>
                            </tr>
                            <tr><td>第三次修正本</td>
                                <td><asp:FileUpload ID="SFFile03_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile03" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile03" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile03" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue">
                                    <asp:HyperLink ID="SFLINK03" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU03" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD03" runat="server"/></td>
                            </tr>
                            <tr><td>第四次修正本</td>
                                <td><asp:FileUpload ID="SFFile06_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile06" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile06" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile06" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue">
                                    <asp:HyperLink ID="SFLINK06" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU06" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD06" runat="server"/></td>
                            </tr>
                            <tr><td>第五次修正本</td>
                                <td><asp:FileUpload ID="SFFile07_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile07" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile07" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile07" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue">
                                    <asp:HyperLink ID="SFLINK07" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU07" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD07" runat="server"/></td>
                            </tr>
                        </tbody></table>
                        <span style="color:red; margin-top:0.5em; display:inline-block;">※ 上傳格式限定為PDF，檔案大小請於150mb以內</span>
                    </td>
                </tr>

                 <tr>
                    <td>
                        <div style="text-align:left; font-size:14pt; margin-top:1em;">相關檔案交換區</div>
                        <table class="filebase">
                            <tbody><tr>
                                <th>序號</th>
                                <th style="width:480px;">相關檔案上傳</th>
                                <th>連結</th>
                                <th style="width:13%">上傳人</th>
                                <th style="width:15%">上傳日期</th>
                            </tr>
                            <tr><td>01</td>
                                <td><asp:FileUpload ID="SFFile04_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile04" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile04" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile04" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue; text-decoration:underline;">
                                    <asp:HyperLink ID="SFLINK04" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU04" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD04" runat="server"/></td>
                            </tr>
                            <tr><td>02</td>
                                <td><asp:FileUpload ID="SFFile05_FileUpload" runat="server" style="height:25px;" />
                                    <asp:Button ID="Btn_UPSFile05" runat="server" Text="上傳檔案" style="height:25px;" OnClick="Btn_UPSFile_Click" />
                                    <asp:Button ID="Btn_DelSFile05" runat="server" Text="x" ToolTip="刪除上傳檔案" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" style="height:25px;" OnClick="Btn_DelSFile_Click" />
                                    <asp:TextBox ID="TXTSFile05" runat="server" Width="70px" Visible="False" /></td>
                                <td style="color:blue; text-decoration:underline;">
                                    <asp:HyperLink ID="SFLINK05" runat="server" Target="_blank"/></td>
                                <td><asp:Label ID="LBUPLOADU05" runat="server"/></td>
                                <td><asp:Label ID="LBUPLOADD05" runat="server"/></td>
                            </tr>
                        </tbody></table>
                        <span style="color:red;margin-top:0.5em; display:inline-block;">※ 上傳格式限定為PDF、JPG、PNG，檔案大小請於50mb以內</span>
                    </td>
                </tr>
                 
            </tbody></table>
                </div>
            </asp:Panel>


            <asp:Panel ID="Area02" runat="server">
                <h2 class="detailsBar_title_accept openh2">受理<img src="../images/btn/btn-open.png" alt=""/></h2>
                <%--<h2 class="detailsBar_title_accept">受理<img src="../images/btn/btn-open.png" alt="" class="open"/></h2>--%>
                <div class="detailsGrid_wrap close">
                    <table class="detailsGrid_purple">
                    <tr><td>補正期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC032" runat="server"/></td></tr>
                    <tr><td>退補件說明</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC103" runat="server"/></td></tr>
                    <tr><td>第二次補正期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC104" runat="server"/></td></tr>
                    <tr><td>第二次退補件說明</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC105" runat="server"/></td></tr>
                    <tr><td>審查費金額</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC035" runat="server" Visible="false"/>
                                            <asp:GridView ID="GVPay01" runat="server" AutoGenerateColumns="False" CssClass="constructionPurple" DataSourceID="SqlDataSource01">
                                                <Columns>
                                                    <asp:BoundField DataField="FD004" HeaderText="繳費單號" />
                                                    <asp:BoundField DataField="FD002" HeaderText="繳款期限" />
                                                    <asp:BoundField DataField="FD003" HeaderText="應納金額" />
                                                    <asp:BoundField DataField="CPI004" HeaderText="繳費日期" />
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" runat="server" CommandArgument='<%#Container.DataItemIndex %>' OnClick="btnPrint01_Click" Text="列印繳費單" />
                                                            <asp:Label ID="LBCSMSG" runat="server" Text='<%# Bind("FD006") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HDKID" runat="server" Value='<%# Bind("FD001") %>' />
                                                            <asp:HiddenField ID="HDACT" runat="server" Value='<%# Bind("FD005") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource01" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
           SelectCommand=" select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CPI004 from tslm2.dbo.CasePaymentInfo where CaseType = '審查費' and CPI006='已列印' order by id; "></asp:SqlDataSource>

                        &nbsp;<a href="https://smis.swcb.gov.tw/MainSys/WinInfo/WinCalCharge.aspx" target="_blank">試算連結</a>

                        </td></tr>
                    <tr><td>審查費繳納期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC031" runat="server"/></td></tr>
                    <tr><td>審查費繳納日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC033" runat="server"/></td></tr>
                    </table>
                </div>
            </asp:Panel>

            <asp:Panel ID="Area03" runat="server">
                <h2 class="detailsBar_title_review openh2">審查<img src="../images/btn/btn-open.png" alt="" /></h2>
                <%--<h2 class="detailsBar_title_review openh2">審查<img src="../images/btn/btn-open.png" alt="" class="open" /></h2>--%>
                <div class="detailsGrid_wrap close">
                    <table class="detailsGrid_green">
                    <tr><td style="background: #d9d9d9;">受理日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC034" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">審查單位</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC022" runat="server"/>
                            <asp:Label ID="LBSWC022ID" runat="server" Visible="false"/></td></tr>
                    <tr><td>審查小組</td>
                        <td>召集人：
                            <asp:Label ID="LBSA01" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSAOID" runat="server" style="height:25px;" Visible="false" /><br/><br/>
                            委　員：
                            <asp:Label ID="LBSA02" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA03" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA04" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA05" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA06" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA07" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA08" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA09" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSA10" runat="server" style="height:25px;" /><br/><br/>
                            審查會議日期：
                            <asp:Label ID="LBSWC113" runat="server" width="120px" autocomplete="off"></asp:Label>
                        </td>
                    </tr>
                    <tr><td>審查委員</td>
                        <td><asp:Label ID="LBSWC087" runat="server"/></td></tr>
                  
                <tr><td>審查紀錄<br><%--<span>(鎖審查單位填)</span>--%></td>
                    <td><asp:GridView ID="SWCDTL01" runat="server" CssClass="greenPause" AutoGenerateColumns="False"
                            >
                            <Columns>
                                <asp:BoundField DataField="DTLA001" HeaderText="審查表單編號" />
                                <asp:BoundField DataField="DTLA002" HeaderText="審查日期" />
                                <asp:BoundField DataField="DTLA003" HeaderText="補正期限" />
                                <asp:BoundField DataField="DTLA004" HeaderText="主旨" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL01" runat="server" CommandArgument='<%# Eval("DTLA001") %>' OnClick="ButtonDTL01_Click" Text="詳情"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td></tr>
                <tr><td style="background: #d9d9d9;">審查期限</td>
                    <td class="detailsGrid_gray">
                        <asp:Label ID="LBSWC088" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">暫停審查期限</td>
                    <td class="detailsGrid_gray">
                        <asp:Label ID="LBSWC089" runat="server"/></td></tr>
                <tr><td>暫停審查</td>
                    <td><asp:ImageButton ID="DTL_02_02_Link" runat="server" title="暫停審查" ImageUrl="../images/btn/btn_ap02.png" OnClick="DTL_02_02_Link_Click" />
                        <asp:GridView ID="SWCOLA202" runat="server" CssClass="greenPause" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA202_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="暫停審查編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="暫停期限" />
                                <asp:BoundField DataField="OLA003" HeaderText="暫停期限" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA202" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA202_Click"/>
                                        <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL202" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>
                <tr><td>核定本</td>
                    <td><asp:HyperLink ID="Link080" runat="server" CssClass="word" Target="_blank"/></td></tr>
                <tr><td>水土保持設施配置圖<br/><span>(圖6-1)</span></td>
                    <td><asp:HyperLink ID="Link029CAD" runat="server" CssClass="word" Target="_blank"/><br/>
                        <asp:HyperLink ID="Link029" runat="server" CssClass="word" Target="_blank"/></td></tr>
                <tr><td>臨時性防災設施配置圖<br/><span>(圖7-1)</span></td>
                    <td><asp:HyperLink ID="Link030" runat="server" CssClass="word" Target="_blank"/></td></tr>
                <tr><td>公會建議核定日期</td>
                    <td><asp:Label ID="LBSWC109" runat="server"/></td></tr>
                <tr><td>審查單位查核表</td>
                    <td><asp:HyperLink ID="Link110" runat="server" CssClass="word" Target="_blank" />
                    </td></tr>
                <tr><td style="background: #d9d9d9;">核定日期</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC038" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">核定文號</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC039" runat="server"/></td></tr>
                <tr><td style="background: #d9d9d9;">保證金金額</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC040" runat="server" Visible="false"/>

                                            <asp:GridView ID="GVPay02" runat="server" AutoGenerateColumns="False"  CssClass="greenPause" DataSourceID="SqlDataSource02">
                                                <Columns>
                                                    <asp:BoundField DataField="FD004" HeaderText="繳費單號" />
                                                    <asp:BoundField DataField="FD002" HeaderText="繳款期限" />
                                                    <asp:BoundField DataField="FD003" HeaderText="應納金額" />
                                                    <asp:BoundField DataField="CPI004" HeaderText="繳費日期" />
                                                    <asp:TemplateField ShowHeader="False">
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnPrint" runat="server" CommandArgument='<%#Container.DataItemIndex %>' OnClick="btnPrint_Click" Text="列印繳費單" />
                                                            <asp:Label ID="LBCSMSG" runat="server" Text='<%# Bind("FD006") %>' Visible="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="HDKID" runat="server" Value='<%# Bind("FD001") %>' />
                                                            <asp:HiddenField ID="HDACT" runat="server" Value='<%# Bind("FD005") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
       <asp:SqlDataSource ID="SqlDataSource02" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>"
           SelectCommand=" select BillID as FD001,CONVERT(varchar(100), CPI002, 23) as FD002,CPI003 as FD003, BillID as FD004,CaseID3 as FD005,CPI006 as FD006,CPI007,CPI004 from tslm2.dbo.CasePaymentInfo where CaseType = '保證金' and CPI006='已列印' order by id; "></asp:SqlDataSource>

                    
                    &nbsp;<a href="https://tcgeswc.taipei.gov.tw/swcmoneyinfo.aspx#a" target="_blank">試算連結</a>
                    </td></tr>
                <tr><td style="background: #d9d9d9;">審查費核銷</td>
                    <td class="detailsGrid_gray"><span id="LBSWC036"></span>

                    </td></tr>
                <tr><td style="background: #d9d9d9;">審查費核銷</td>
                    <td class="detailsGrid_gray"><asp:Label ID="LBSWC036" runat="server"/></td></tr>
                </table>
            </div>
               
            </asp:Panel>     
            <asp:Panel ID="Area04" runat="server">
                <h2 class="detailsBar_title_construction">施工<img src="../images/btn/btn-open.png" alt=""/></h2>
                <%--<h2 class="detailsBar_title_construction">施工<img src="../images/btn/btn-open.png" alt="" class="open"/></h2>--%>
                <div class="detailsGrid_wrap close">
                    <table class="detailsGrid_purple2">
                    <tr><td style="background: #d9d9d9;">開工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC082" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">開工展延次數</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC083" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">保證金繳納</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC041" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">目的事業主管機關核定完工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC112" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">施工許可證核發日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC043" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">施工許可證核發文號</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC044" runat="server"/></td></tr>
                    <tr><td>開工日期</td>
                        <td><asp:Label ID="LBSWC051" runat="server"/></td></tr>
                    <tr><td>開工/復工展延</td>
                    <td><asp:ImageButton ID="DTL_02_03_Link" runat="server" title="開工/復工展延" ImageUrl="../images/btn/btn_ap04.png" OnClick="DTL_02_03_Link_Click" />
                        <asp:GridView ID="SWCOLA203" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA203_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="展延編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="原定開工/復工期限" />
                                <asp:BoundField DataField="OLA003" HeaderText="預定展延期限" />
                                <asp:BoundField DataField="OLA004" HeaderText="展延理由" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA203" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA203_Click" />
                                            <asp:HiddenField ID="Lock203" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL203" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="ButtonDEL203_Click"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>
                    <tr><td style="background: #d9d9d9;">核定完工日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC052" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">監造技師</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC045" runat="server"/>
                            <asp:Label ID="LBSWC045ID" runat="server" Visible="false"/>
                        </td></tr>
                    <tr><td style="background: #d9d9d9;">監造技師手機</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC047" runat="server"/></td></tr>
                    <tr><td>施工廠商</td>
                        <td><asp:Label ID="LBSWC048" runat="server"/></td></tr>
                    <tr><td>工地負責人</td>
                        <td><asp:Label ID="LBSWC049" runat="server"/></td></tr>
                    <tr><td>工地負責人手機</td>
                        <td><asp:Label ID="LBSWC050" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">檢查單位</td>
                        <td class="detailsGrid_gray">
                            <asp:Label ID="LBSWC024" runat="server"/>
                            <asp:Label ID="LBSWC024ID" runat="server" Visible="false"/></td></tr>
                    <tr><td>檢查委員</td>
                        <td>委員：
                            <asp:Label ID="LBSB01" runat="server" style="height:25px;" />
                            <asp:Label ID="LBSB02" runat="server" style="height:25px;" /><br/><br/>
                            施工檢查日期：
                            <asp:Label ID="LBSWC114" runat="server" width="120px" autocomplete="off"></asp:Label><br/><br/>
                            完工檢查日期：
                            <asp:Label ID="LBSWC115" runat="server" width="120px" autocomplete="off"></asp:Label>
                        </td>
                    </tr>
                    <tr><td>施工中監督檢查紀錄<%--<span>(鎖檢查單位(施工檢查紀錄表)、機關人員(施工抽查紀錄表)填)</span>--%></td>
                        <td><asp:GridView ID="SWCDTL0302" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            >
                            <Columns>
                                <asp:BoundField DataField="DTLC001" HeaderText="施工監督表編號" />
                                <asp:BoundField DataField="DTLC002" HeaderText="檢查日期" />
                                <asp:BoundField DataField="DTLC003" HeaderText="檢查類型" />
                                <asp:BoundField DataField="DTLC004" HeaderText="檢查公會" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL0302" runat="server" CommandArgument='<%# Eval("DTLC001") %>' OnClick="ButtonDTL0302_Click" Text="詳情"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td></tr>
                <tr><td>颱風豪雨設施自主檢查表<%--<span>(鎖監造技師填)</span>--%></td>
                    <td><asp:GridView ID="SWCDTL04" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            >
                            <Columns>
                                <asp:BoundField DataField="DTLD001" HeaderText="自主檢查表編號" />
                                <asp:BoundField DataField="DTLD002" HeaderText="檢查日期" />
                                <asp:BoundField DataField="DTLD003" HeaderText="防災標的" />
                                <asp:BoundField DataField="DTLD004" HeaderText="自主檢查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL04" runat="server" CommandArgument='<%# Eval("DTLD001") %>' OnClick="ButtonDTL04_Click" Text="詳情"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td></tr>

                    <tr><td>設施調整報備</td>
                    <td><asp:ImageButton ID="DTL_02_05_Link" runat="server" title="設施調整報備" ImageUrl="../images/btn/btn_ap05.png" OnClick="DTL_02_05_Link_Click"/>
                        <asp:GridView ID="SWCOLA205" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA2_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="設施調整報備編號" />
                                <asp:BoundField DataField="OLA003" HeaderText="送出日期" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA205" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA205_Click" />
                                            <asp:HiddenField ID="HDLock2" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnDel2" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>

                <tr><td>監造紀錄表<%--<span>(鎖監造技師填)</span>--%></td>
                    <td><asp:GridView ID="SWCDTL05" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            >
                            <Columns>
                                <asp:BoundField DataField="DTLE001" HeaderText="監造紀錄表編號" />
                                <asp:BoundField DataField="DTLE002" HeaderText="檢查日期" />
                                <asp:BoundField DataField="DTLE003" HeaderText="累積進度百分比" />
                                <asp:BoundField DataField="DTLE004" HeaderText="監造結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL05" runat="server" CommandArgument='<%# Eval("DTLE001") %>' OnClick="ButtonDTL05_Click" Text="詳情"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td></tr>
                    
                    <tr><td style="background: #d9d9d9;">空拍影像套疊</td>
                        <td class="detailsGrid_gray">
                            <asp:HyperLink ID="OutLink1" runat="server" target="_blank" NavigateUrl="https://data.geodac.tw/TCGEO/" Text="https://data.geodac.tw/TCGEO/" /></td></tr>
                    <tr><td style="background: #d9d9d9;">核備圖說變更</td>
                        <td class="detailsGrid_gray">
                            <asp:HyperLink ID="Link106" runat="server" target="_blank" /></td></tr>
                    <tr><td>停工申請</td>
                    <td><asp:ImageButton ID="DTL_02_08_Link" runat="server" title="停工申請" ImageUrl="../images/btn/btn_ap07.png" OnClick="DTL_02_08_Link_Click" />
                        <asp:GridView ID="SWCOLA208" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA208_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="停工申請編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="開工日期" />
                                <asp:BoundField DataField="OLA003" HeaderText="預計停工期限(起)" />
                                <asp:BoundField DataField="OLA004" HeaderText="預計停工期限(迄)" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA208" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA208_Click" />
                                            <asp:HiddenField ID="Lock208" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>


                                    <asp:TemplateField Visible="true">
                                        <ItemTemplate>
                                            <asp:Button ID="ButtonDEL208" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Lock202" runat="server" Value='<%# Bind("OLA005") %>'  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>


                            </Columns>
                        </asp:GridView>

                    </td></tr>
                    <tr><td style="background: #d9d9d9;">停工日期</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC053" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">停工期限</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC084" runat="server"/></td></tr>
                    <tr><td style="background: #d9d9d9;">停工展延次數</td>
                        <td class="detailsGrid_gray"><asp:Label ID="LBSWC092" runat="server"/></td></tr>
                <tr><td>完工檢查紀錄<%--<span>(鎖檢查單位填)</span>--%></td>
                    <td><asp:GridView ID="SWCDTL06" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            >
                            <Columns>
                                <asp:BoundField DataField="DTLF001" HeaderText="完工檢查表編號" />
                                <asp:BoundField DataField="DTLF002" HeaderText="檢查日期" />
                                <asp:BoundField DataField="DTLF003" HeaderText="達完工標準" />
                                <asp:BoundField DataField="DTLF004" HeaderText="檢查公會" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL06" runat="server" CommandArgument='<%# Eval("DTLF001") %>' OnClick="ButtonDTL06_Click" Text="詳情"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td></tr>
                    <tr><td>完工申報書</td>
                    <td><asp:ImageButton ID="DTL_02_09_Link" runat="server" title="完工申報書" ImageUrl="../images/btn/btn_ap08.png" OnClick="DTL_02_09_Link_Click" />
                        <asp:GridView ID="SWCOLA209" runat="server" CssClass="constructionPurple" AutoGenerateColumns="False"
                            OnRowDataBound="SWCOLA209_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="OLA001" HeaderText="完工申報編號" />
                                <asp:BoundField DataField="OLA002" HeaderText="申報日期" />
                                <asp:BoundField DataField="OLA003" HeaderText="申請人" />
                                <asp:BoundField DataField="OLA006" HeaderText="審查結果" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonOLA209" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="詳情" OnClick="ButtonOLA209_Click" />
                                        <asp:HiddenField ID="Lock209" runat="server" Value='<%# Bind("OLA005") %>'  />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="true">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDEL209" runat="server" CommandArgument='<%# Eval("OLA001") %>' Text="刪除" OnClientClick="return confirm('資料刪除後無法復原，確認是否刪除這筆資料?')" OnClick="ButtonDEL209_Click"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView></td></tr>
                <tr><td>申報完工日期</td>
                    <td><asp:Label ID="TXTSWC058" runat="server" /></td></tr>
                <tr><td>竣工圖說</td>
                    <td><asp:HyperLink ID="Link101CAD" runat="server" CssClass="word" Target="_blank"/><br/>
                        <asp:HyperLink ID="Link101" runat="server" CssClass="word" Target="_blank"/></td></tr>
                <tr><td style="background: #d9d9d9;">完工證明書核發日期</td>
                    <td class="detailsGrid_gray">
                        <asp:Label ID="LBSWC059" runat="server"/></td></tr>
                <tr><td>水保設施</td>
                    <td><asp:Label ID="LBSWC067068" runat="server"/>
                        <asp:Label ID="LBSWC069" runat="server"/>
                        <asp:Label ID="LBSWC070" runat="server"/><br/>
                        <asp:Label ID="LBSWC071072" runat="server"/>
                        <asp:Label ID="LBSWC073074" runat="server"/><br/>
                        <asp:Label ID="LBSWC075" runat="server"/></td></tr>
                  
                <tr><td style="background: #d9d9d9;">保證金退還</td>
                    <td class="detailsGrid_gray">
                        <asp:Label ID="LBSWC056" runat="server"/></td></tr>
                </table>
                </div>
                    
            </asp:Panel>
            <asp:Panel ID="Area05" runat="server">
                <h2 class="detailsBar_title_finish">完工後水土保持設施檢查<img src="../images/btn/btn-open.png" alt=""/></h2>
                <div class="detailsGrid_wrap close">
                    <table class="detailsGrid_orange">
                    <tr><td>維護管理人</td>
                        <td><asp:Label ID="LBSWC093" runat="server"/>
                            <asp:Label ID="LBSWC107" runat="server"/>
                            <asp:Label ID="LBSWC107ID" runat="server" Visible="false"/></td></tr>
                    <tr><td>維護管理人手機</td>
                        <td><asp:Label ID="LBSWC095" runat="server"/></td></tr>
                    <tr><td>維護管理人地址</td>
                        <td><asp:Label ID="LBSWC094" runat="server"/></td></tr>
                    <tr><td>基地概況</td>
                    <td class="detailsGrid_orange_ctBR">
                        <asp:Label ID="LBSWC061062" runat="server"/>
                        <asp:Label ID="LBSWC063064" runat="server"/>
                        <asp:Label ID="LBSWC065066" runat="server"/>
                    </td></tr>
                    <tr>
                    <td>設施維護檢查表</td>
                    <td><asp:GridView ID="SWCDTL07" runat="server" CssClass="detailsGrid_orange_innerTable Completedorange" AutoGenerateColumns="False"
                            >
                            <Columns>
                                <asp:BoundField DataField="DTLG001" HeaderText="設施維護表編號" />
                                <asp:BoundField DataField="DTLG002" HeaderText="檢查日期" />
                                <asp:BoundField DataField="DTLG003" HeaderText="檢查情形" />
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonDTL07" runat="server" CommandArgument='<%# Eval("DTLG001") %>' OnClick="ButtonDTL07_Click" Text="詳情"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                    </td>
                  </tr>
                </table>
              </div>
            </asp:Panel>



            </div>
          </div>
            
           <%-- <div class="apply">
            	<div class="apply-menu">
                  <input type="image" src="../images/btn/btn_ap01.png"  alt="義務人及技師變更報備" name="" id="" onclick="" />
                  <input type="image" src="../images/btn/btn_ap02.png"  alt="暫停審查" name="" id="" onclick="" />


                    <asp:ImageButton ID="DTL_02_09_Link" runat="server" OnClick="DTL_02_09_Link_Click" title="自主檢查表" ImageUrl="../images/btn/btn_ap09.png" />




                  <input type="image" src="../images/btn/btn_ap03.png"  alt="開工申報" name="" id="" onclick="" />
                  <input type="image" src="../images/btn/btn_ap04.png"  alt="開工/復工展延" name="" id="" onclick="" />
                  <input type="image" src="../images/btn/btn_ap05.png"  alt="設施調整報備" name="" id="" onclick="" />
                  <input type="image" src="../images/btn/btn_ap06.png"  alt="工期展延" name="" id="" onclick="" />
                  <input type="image" src="../images/btn/btn_ap07.png"  alt="停工申請" name="" id="" onclick="" />
                  <input type="image" src="../images/btn/btn_ap08.png"  alt="完工申報" name="" id="" onclick="" />
                </div>
            </div>--%>
            

<%--            <div class="footer-s">
                <div class="footer-s-green"></div>
                <div class="footer-b-brown">
                    <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                       <span class="span2">110臺北市信義區松德路300號3樓 　(02)27593001   臺北市民當家熱線1999</span><br/>
                       <span class="span2">資料更新：<asp:Label ID="ToDay" runat="server" Text=""/>　來訪人數：<asp:Label ID="Visitor" runat="server" Text=""/> </span></p>
                </div>
            </div>--%>

            <div class="footer">
               <p><span class="span1">臺北市政府工務局大地工程處</span><br/>
                    <span class="span2">110臺北市信義區松德路300號3樓 　服務專線(02)27591109   臺北市民當家熱線1999</span><br/>
                    <span class="span2">建議使用IE11(含)以上，Chrome或Firefox版本瀏覽器　<b>資料更新：</b><asp:Label ID="ToDay" runat="server" Text=""/>　<b>來訪人數：</b><asp:Label ID="Visitor" runat="server" Text=""/> </span><br/>
                    <span class="span2"><b>客服電話：</b>02-27929328 陳小姐　<b>信箱：</b>tcge7@geovector.com.tw　本系統由多維空間資訊有限公司開發維護 TEL：(02)27929328</span><br/>
			        <span class="span2">※為維護系統服務品質，本平台訂於每周三凌晨AM 4:00-6:30 進行系統維護更新，更新期間偶有瞬斷情形，敬請使用者避開該時段使用。謝謝！</span></p>
            </div>

        </div>
    </div>















        <script src="../js/jquery-3.1.1.min.js"></script>
        <script src="../js/inner.js"></script>
        <script src="../js/allhref.js"></script>
    </div>
    </form>
</body>
</html>
