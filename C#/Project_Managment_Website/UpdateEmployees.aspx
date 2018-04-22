<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateEmployees.aspx.cs" Inherits="UpdateEmployees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Master.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="container">
        
        <h2 class="form-heading2">Changes to Employee</h2>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMPLOYEE_ID] FROM [EMPLOYEES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label1" runat="server" Text="Users Employee ID:"></asp:Label>
        <asp:TextBox class="form-control" placeholder="Users Employee ID:" ID="employeeIDField" runat="server" DataSourceID="SqlDataSource1" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
        </asp:TextBox>
        <br />
        

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [TEAM_ID] FROM [TEAMS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label2" runat="server" Text="Team to update:"></asp:Label>
        <asp:DropDownList class="form-control" ID="teamsDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="TEAM_ID" DataValueField="TEAM_ID" OnDataBound="SqlDataSouce2_DataBound" AutoPostBack="true">
        </asp:DropDownList>
        <br />

       
        <asp:Button class="btn btn-lg btn-primary btn-block" ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        <br />
        

        <asp:Label ID="FinalLabel" runat="server" Text=""></asp:Label>
        <div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="employeeIDField" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>
        <div>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="employeeIDField" errormessage="Please enter Employee ID!" />
        </div>

    </div>
</form>
</body>
</html>
