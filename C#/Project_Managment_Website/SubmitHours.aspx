<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitHours.aspx.cs" Inherits="SubmitHours" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="hoursLabel" runat="server" Text="Hours: "></asp:Label>
        <asp:TextBox ID="hoursField" runat="server"></asp:TextBox>
        <asp:Label ID="projectLabel" runat="server" Text="Project: "></asp:Label>
        <asp:DropDownList ID="projectDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="NAME" DataValueField="PROJECT_ID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [PROJECT_ID], [NAME] FROM [PROJECTS]"></asp:SqlDataSource>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <br />
        <asp:Label ID="outputLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
