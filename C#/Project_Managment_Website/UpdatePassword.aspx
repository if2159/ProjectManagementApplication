<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="UpdatePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMAIL] FROM [USERS]"></asp:SqlDataSource>
            <asp:Label ID="Label1" runat="server" Text="User email:"></asp:Label>
            <asp:TextBox ID="employeeEmailField" runat="server" DataSourceID="SqlDataSource1" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="employeeEmailField" errormessage="Please enter Employee ID!" />
            <br />


            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [HASHED_PASSWORD] FROM [USERS]"></asp:SqlDataSource>
            <asp:Label ID="Label2" runat="server" Text="Old Password: "></asp:Label>
            <asp:TextBox ID="oldPasswordField" runat="server" type="password" DataSourceID="SqlDataSource2" DataTextField="ROLE_DESCRIPTION" DataValueField="ROLE_ID" AutoPostBack="true">
            </asp:TextBox><asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="oldPasswordField" errormessage="Please enter Employee ID!" />
            <br />
            <br/>
            
            <asp:Label ID="Label3" runat="server" Text="New Password: "></asp:Label>
            <asp:TextBox ID="newPasswordField" runat="server" type="password"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="newPasswordField" errormessage="Please enter Employee ID!" />
            <br />
            <br />

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />



            <br />
            <asp:Label ID="FinalLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
