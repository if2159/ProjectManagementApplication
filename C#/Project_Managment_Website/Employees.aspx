<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="Employees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Employee</title>
</head>

    <body>
        <h1>Create Employee</h1>
        <form method="post" id="form1" runat="server">
        <div>
            <asp:Label ID="fnameLabel" runat="server" Text="First Name: "></asp:Label>
            <asp:TextBox ID="fnameField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="mInitLabel" runat="server" Text="Middle Initial: "></asp:Label>
            <asp:TextBox ID="mInitField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="lnameLabel" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox ID="lnameField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="eidLabel" runat="server" Text="Employee ID: "></asp:Label>
            <asp:TextBox ID="eidField" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="wageLabel" runat="server" Text="Hourly Wage: "></asp:Label>
            <asp:TextBox ID="wageField" runat="server"></asp:TextBox>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [TEAM_ID] FROM [TEAMS]"></asp:SqlDataSource>
            
            <asp:Label ID="teamLabel" runat="server" Text="Team: "></asp:Label>
            <asp:DropDownList ID="teamDropDown" runat="server" DataSourceID="SqlDataSource1" DataValueField="TEAM_ID" OnDataBound ="SqlDataSource1_DataBound"></asp:DropDownList>
            <br />
            <asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
            <br />
            <asp:Label ID="outputLabel" runat="server" Text=""></asp:Label>
        </div>
        </form>
    </body>
</html>
