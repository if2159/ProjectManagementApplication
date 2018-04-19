<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateUser.aspx.cs" Inherits="UpdateUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMPLOYEE_ID] FROM [USERS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
            <asp:Label ID="Label1" runat="server" Text="Users Employee ID:"></asp:Label>
            <asp:TextBox ID="employeeIDField" runat="server" DataSourceID="SqlDataSource1" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
            </asp:TextBox>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="employeeIDField" errormessage="Please enter Employee ID!" />
            <br />


            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT ROLE_ID, ROLE_DESCRIPTION FROM USER_ROLES" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
            <asp:Label ID="Label2" runat="server" Text="Set role to: "></asp:Label>
            <asp:DropDownList ID="roleDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="ROLE_DESCRIPTION" DataValueField="ROLE_ID" OnSelectedIndexChanged="statusDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce2_DataBound">
            </asp:DropDownList>



            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />



            <br />
            <asp:Label ID="FinalLabel" runat="server" Text=""></asp:Label>

        </div>
    </form>
</body>
</html>

