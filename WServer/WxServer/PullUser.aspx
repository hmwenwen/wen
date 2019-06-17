<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PullUser.aspx.cs" Inherits="XJWZCatering.WServer.WxServer.PullUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>拉取用户信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label id="showtips" runat="server"></label>
            <asp:Button ID="btnPull" runat="server" Text="开始拉取" OnClick="btnPull_Click" />
        </div>
    </form>
</body>
</html>
