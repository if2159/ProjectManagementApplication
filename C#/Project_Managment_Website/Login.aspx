<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Master.css" rel="stylesheet" />
</head>
<body>
<div class="container">
<form class="form-signin" id="form1" runat="server">
    
    
        
    <h2 class="form-signin-heading">Please sign in</h2>

    <asp:Label class="sr-only" ID="usernameLabel" runat="server" Text="Email: "></asp:Label>
    <asp:TextBox ID="emailField" placeholder="Email: " class="form-control" runat="server"></asp:TextBox>
       
        
    <asp:Label class="sr-only" ID="passwordLabel"  runat="server" Text="Password: "></asp:Label>
    <asp:TextBox ID="passwordField" runat="server" placeholder="Password: " class="form-control" type="password"></asp:TextBox>
    <asp:Label ID="errorLabel" runat="server" Text=""></asp:Label>
        
    <br />
        
    <asp:Button class="btn btn-lg btn-primary btn-block" ID="loginButton" runat="server" Text="Login" OnClick="loginButton_Click" />
        
        
    
</form>
</div>
</body>
</html>
