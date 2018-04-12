<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateUserRoles.aspx.cs" Inherits="CreateUserRoles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>Create New User Role</h1>
        <p>
            <asp:Label ID="roleNameLabel" runat="server" Text="Role Description: "></asp:Label>
            <asp:TextBox ID="roleNameField" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
        </p>
        <p>
            <asp:Label ID="outputLabel" runat="server" Text=""></asp:Label>
        </p>
    
    </div>
    </form>
</body>
</html>
