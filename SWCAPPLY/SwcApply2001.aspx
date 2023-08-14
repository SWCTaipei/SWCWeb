<%@ Page Title="建議核定" Language="C#" MasterPageFile="~/PriPage/MasterPageA.master" AutoEventWireup="true" CodeFile="SwcApply2001.aspx.cs" Inherits="SWCAPPLY_SwcApply2001" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            var vTry = document.getElementById("<%=TextBoxJSE.ClientID %>").value;
            if (vTry == "HH") {
                $(".red").hide();
            }
        });
        function UpLoadChk(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '150', '.pdf'); }
        }
        function UpLoadChk2(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '50', '.pdf'); }
        }
        function UpLoadChk3(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '50', '.dwg'); }
        }
        function UpLoadChk4(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '50', '.dwg.7z'); }
        }
        function UpLoadChk5(ObjId) {
            if (document.getElementById(ObjId).value == '') { alert('請選擇檔案'); return false; } else { return ChkFileSize(ObjId, '500', '.pdf'); }
        }
		function chkInput(ObjId){
			document.getElementById("<%=DataLock.ClientID %>").style.display="none";
			document.getElementById("<%=SaveCase.ClientID %>").style.display="none";
			document.getElementById("<%=GoHomePage.ClientID %>").style.display="none";
			
			return true;
		}
		function RB2(ObjId)
		{
			if(document.getElementById(ObjId).checked == true)
			{
				document.getElementById("<%=CBL.ClientID %>").checked = true;
				document.getElementById("<%=CBL1.ClientID %>").checked = true;
			}
		}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"/>

    <div class="content-s">
        <div class="txttitle">建議核定</div>
                
       <%-- <div class="flexrightbox">
            <ul><li>編輯</li>
                <li>送出</li>
                <li id="gotop"><img src="images/icon/top-20.png" title="top" /></li>
            </ul>
        </div>--%>

        <div>
            <table class="resend">
                <tr>
                    <th style="background:#ddf1ff">水保局編號</th>
                    <td style="background:#ddf1ff">
                        <asp:Label ID="LBSWC000" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="LBSWC002" runat="server" Text="Label"></asp:Label></td>
                </tr>
                 <tr>
                    <th style="background:#ddf1ff">書件名稱</th>
                    <td style="background:#ddf1ff">
                        <asp:Label ID="LBSWC005" runat="server" Text="Label"></asp:Label>
                        <asp:Label ID="LBSWC025" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:Label ID="LBSWC007" runat="server" Text="Label" Visible="false"></asp:Label></td>
                </tr>
                <tr>
                    <th>建議核定編號</th>
                    <td>
                        <asp:Label ID="LBSA01" runat="server" Text="Label"></asp:Label></td>
                </tr>
                <tr>
                    <th>檢視本收件日期<span style="color: red;font-family:cursive;">＊</span></th>
                    <td>
                        <asp:TextBox ID="TBA01" runat="server" Width="120px" autocomplete="off" Enabled="false"></asp:TextBox>
                    </td>
                </tr><%-- 2020-04-07 跟凱暉討論一下  把建議核定日期拿掉  到時候帶比單送出日期當建議核定日期--%>
                <tr>
                    <th>核定本<span style="color: red;font-family:cursive;">＊</span></th>
                    <td><asp:FileUpload ID="TXTSWC080_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC080_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC080_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC080" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC080_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC080_fileclean_Click" />
                        <br/><span class="red">※ 上傳格式限定為PDF，檔案大小請於500mb以內</span><br/>
                        <asp:Button ID="BTNLINK080" runat="server" CssClass="HLbtn" Visible="false" OnClick="BTNLINK_Click" />
                        <asp:HyperLink ID="Link080" runat="server" CssClass="word" Target="_blank"/>
                    </td>
                </tr>
                
                <tr>
                    <th>公開核定本</th>
                    <td>
                            <asp:CheckBox ID="CBSWC119" runat="server" Text="公開" /><br/>
                        <asp:Panel ID="Panel4" runat="server">
                            <asp:FileUpload ID="TXTSWC118_fileupload" runat="server" CssClass="wordtt" />
                            <asp:Button ID="TXTSWC118_fileuploadok" runat="server" Text="上傳檔案" OnClick="SWC118_fileuploadok_Click" />
                            <asp:Button ID="TXTSWC118_fileclean" runat="server" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC118_fileclean_Click" /><br/>
                            <span class="red">※ 上傳格式限定為PDF，檔案大小請於500mb以內</span><br/>
							<span class="red">※本欄位上傳檔案前，請確認是否確實移除相關個資內容！</span>
                        </asp:Panel>
                        <asp:TextBox ID="TXTSWC118" runat="server" CssClass="word sixteen" Visible="False" Width="20px"></asp:TextBox>
                        <asp:HyperLink ID="Link118" runat="server" CssClass="word" Target="_blank"></asp:HyperLink>
                    </td>
                </tr>

                <tr>
                    <th>水土保持設施配置圖<span style="color: red;font-family:cursive;">＊</span></th>
                    <td><asp:FileUpload ID="TXTSWC029CAD_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC029CAD_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC029CAD_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC029CAD" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC029CAD_fileclean" runat="server" CssClass="wordttb" Text="X" OnClick="TXTSWC029CAD_fileclean_Click" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" />
                        <br/><span class="red">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                        <asp:HyperLink ID="Link029CAD" runat="server" CssClass="word" Target="_blank"/>
                        <br/>
                        <asp:FileUpload ID="TXTSWC029_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC029_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC029_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC029" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC029_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC029_fileclean_Click" />
                        <br/><span class="red">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                        <asp:Button ID="BTNLINK029" runat="server" CssClass="HLbtn" Visible="false" OnClick="BTNLINK_Click"/>
                        <asp:HyperLink ID="Link029" runat="server" CssClass="word" Target="_blank"/>
                    </td>
                </tr>
                <tr>
                    <th>臨時性防災設施配置圖<span style="color: red;font-family:cursive;">＊</span></th>
                    <td><asp:FileUpload ID="TXTSWC030CAD_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC030CAD_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC030CAD_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC030CAD" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC030CAD_fileclean" runat="server" CssClass="wordttb" Text="X" OnClick="TXTSWC030CAD_fileclean_Click" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" />
                        <br/><span class="red">※ 上傳格式限定為CAD，檔案大小請於50mb以內</span><br/>
                        <asp:HyperLink ID="Link030CAD" runat="server" CssClass="word" Target="_blank"/>
                        <br/>
                        <asp:FileUpload ID="TXTSWC030_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC030_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC030_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC030" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC030_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC030_fileclean_Click" /><br/>
                        <span class="red">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                        <asp:Button ID="BTNLINK030" runat="server" CssClass="HLbtn" Visible="false" OnClick="BTNLINK_Click" />
                        <asp:HyperLink ID="Link030" runat="server" CssClass="word" Target="_blank" />
                    </td>
                </tr>
                <tr>
                    <th>審查單位查核表<span style="color: red;font-family:cursive;">＊</span></th>
                    <td><asp:FileUpload ID="TXTSWC110_fileupload" runat="server" CssClass="wordtt" />
                        <asp:Button ID="TXTSWC110_fileuploadok" runat="server" Text="上傳檔案" OnClick="TXTSWC110_fileuploadok_Click" />
                        <asp:TextBox ID="TXTSWC110" runat="server" CssClass="word" Width="20px" Visible="False"/>
                        <asp:Button ID="TXTSWC110_fileclean" runat="server" CssClass="wordttb" Text="X" OnClientClick="return confirm('刪除後無法復原，請再次確認是否要刪除!!!')" OnClick="TXTSWC110_fileclean_Click"/><br/>
                        <span class="red">※ 上傳格式限定為PDF，檔案大小請於50mb以內</span><br/>
                        <asp:Button ID="BTNLINK110" runat="server" CssClass="HLbtn" Visible="false" OnClick="BTNLINK_Click"/>
                        <asp:HyperLink ID="Link110" runat="server" CssClass="word" Target="_blank" />
                    </td>
                </tr>
                <tr>
                    <th>審查單位建議<span style="color: red;font-family:cursive;">＊</span></th>
                    <td>
                        <asp:Label id="LBRA" runat="server" Text="Label" Visible="false"></asp:Label>
                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="RadioButton1" Text="核定" />
                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="RadioButton1" onclick="RB2(this);" Text="不予核定" />&nbsp;&nbsp;
                        <asp:TextBox id="TBRB2" runat="server" style="width: 400px;" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th colspan="2" style="text-align:center; font-weight:bold; color:blue;">
                        <asp:CheckBox id="CBL" runat="server"></asp:CheckBox>請確認上方欄位資料是否正確</th>
                </tr>
            </table>
			
			
			<div class="detailsGrid2">
               <h2 class="closediv openh2">請審查單位加強檢視事項<img src="../images/btn-open.png"></h2>
                 <div class="detailsGrid_wrap">
                  <div id="hide22">
                  <span>1.計畫PDF檔請以軟體輸出。</span>
                  <span>2.CAD圖說轉PDF檔請以DWG To PDF.pc3格式輸出。</span>
                  <span>3.審查單位查核表及承辦技師自主檢查表為最新版本。</span>
                  <span>4.檢附檢附環境影響評估及都市設計及土地使用開發許可審議許可或免辦理說明。</span>
                  <span>5.CAD檔以TWD97座標系統製作，並確認座標及圖層無誤。</span>
                  <span>6.水保設施配置圖標註水土保持設施面積，並確認與本平台填登資料一致。</span>
                  <br><br>
                  <div style="text-align:center;"><asp:CheckBox id="CBL1" runat="server"></asp:CheckBox><b>已完成以上事項確認</b></div>
                  </div>
                 </div>
             </div>

        
         <br><br>
         
                <asp:Panel ID="ReviewResults" runat="server" Visible="false">
           <table class="review-out">
                <tr><td class="bgcolor" style="vertical-align:middle;">審查結果</td>
                    <td class="bgcolor2" style="line-height:40px;">
                        <asp:Label ID="LBRADIORS" runat="server" />
                        <asp:Label ID="LBResultsExplain" runat="server"/></td></tr></table>
                </asp:Panel>
         
		 <div>
		 
                <asp:Label ID="ReqCount" runat="server" Text="" style="display:none;" />
                <asp:Panel ID="SignList" runat="server"><br/><br/>

                <div><span style="background-color: #FFFF99; font-size: 16pt; font-weight: bold; margin-top:1em;">退補正歷程</span></div><br/>
            
                <asp:GridView ID="GVSignList" runat="server" DataSourceID="SqlDataSourceSign" CssClass="retirement" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TH001n" HeaderText="退文日期" SortExpression="SWC000" ItemStyle-Width="200px"/>
                        <asp:BoundField DataField="TH005n" HeaderText="改正期限" SortExpression="SWC002" ItemStyle-Width="190px"/>
                        <asp:BoundField DataField="THName" HeaderText="退文人員" SortExpression="SWC004" ItemStyle-Width="140px" Visible="false"/>
                        <asp:BoundField DataField="TH004" HeaderText="說明" SortExpression="SWC005"  ItemStyle-Width="350px"/>
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSourceSign" runat="server" ConnectionString="<%$ ConnectionStrings:SWCConnStr %>" SelectCommand="" OnSelected="SqlDataSourceSign_Selected" />
                </asp:Panel>
		 
		 </div>
		 
		 
         
            </div>      
        			<div class="btncenter">
                        <asp:Button ID="DataLock" runat="server" Text="確認送出" OnClientClick="return chkInput('DataLock');" OnClick="DataLock_Click" />
                        <asp:Button ID="SaveCase" runat="server" Text="暫時儲存" OnClientClick="return chkInput('');" OnClick="SaveCase_Click" />
                        <asp:Button ID="GoHomePage" runat="server" Text="返回瀏覽案件" OnClick="GoHomePage_Click" />
                        <asp:TextBox ID="TextBoxJSE" runat="server" Style="display: none;" />
                    </div>
        

    </div><!--content-s-->
    
     
     
     <div class="blueline"></div>
     <div class="greenline"></div>
	 
	 <script type="text/javascript">

        $(document).ready(function () {
            $('.detailsGrid_wrap').eq(0).show();
            $('.openh2, #Area04>h2, #Area05>h2, #Area01>h2, #Area02>h2, #Area03>h2').click(function (event) {
                $(this).next('div').slideToggle();
                if ($(this).children('img').attr('src') == '../images/btn-open.png') {
                    $(this).children('img').attr('src', '../images/btn-close.png');
                } else {
                    $(this).children('img').attr('src', '../images/btn-open.png');
                }
            });
        });
    </script>
      

</asp:Content>

