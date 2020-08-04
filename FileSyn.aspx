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

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileSyn.aspx.vb" Inherits="FileSyn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="targetPCtext" runat="server"></asp:TextBox><asp:Button ID="startsyn" runat="server" Text="開始同步" />
        </div>
        <div>
            <asp:Button ID="Button1" runat="server" Text="Button" /><br />
            <asp:TextBox ID="TextBox1" runat="server" Height="323px" TextMode="MultiLine" Width="846px"></asp:TextBox>
        </div>
    </form>
</body>
</html>
