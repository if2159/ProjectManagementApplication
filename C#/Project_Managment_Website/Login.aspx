<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="usernameLabel" runat="server" Text="Email: "></asp:Label>
        <asp:TextBox ID="emailField" runat="server" ></asp:TextBox>
        <asp:Label ID="passwordLabel" runat="server" Text="Password: "></asp:Label>
    
        <asp:TextBox ID="passwordField" runat="server"></asp:TextBox>
        <asp:Label ID="errorLabel" runat="server" Text=""></asp:Label>
        <br />
        <asp:Button ID="loginButton" runat="server" style="height: 26px" Text="Login" OnClick="loginButton_Click" />
    
    </div>
    </form>
</body>
</html>
