<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>
<%@Import Namespace="System.Data" %>
<%@Import Namespace="System.Data.Common" %>
<%@Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="employeeIDLabel" runat="server" Text="Employee ID: "></asp:Label>
            <asp:TextBox ID="employeeIDField" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="roleLabel" runat="server" Text="Role"></asp:Label>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [ROLE_DESCRIPTION], [ROLE_ID] FROM [USER_ROLES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
            <asp:DropDownList ID="roleDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="ROLE_DESCRIPTION" DataValueField="ROLE_ID" OnSelectedIndexChanged="projectDropDown_SelectedIndexChanged" ondatabound ="SqlDataSouce1_DataBound"></asp:DropDownList>
        </div>

        <div>
            <asp:Label ID="emailLabel" runat="server" Text="Email: "></asp:Label>
            <asp:TextBox ID="emailField" runat="server" ></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="passwordLabel" runat="server" Text="Password: "></asp:Label>
            <asp:TextBox ID="passwordField" runat="server" type="password"></asp:TextBox>
        </div>

        <p>
            <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" />
        </p>

        <p>
            <asp:Label ID="finalLabel" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
