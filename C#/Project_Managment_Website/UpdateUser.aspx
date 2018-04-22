<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateUser.aspx.cs" Inherits="UpdateUser" %>

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

            <h2 class="form-heading2">Change User Role</h2>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMPLOYEE_ID] FROM [USERS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
            <asp:Label class="sr-only" ID="Label1" runat="server" Text="Users Employee ID:"></asp:Label>
            <asp:TextBox class="form-control" ID="employeeIDField" placeholder="Users Employee ID:" runat="server" DataSourceID="SqlDataSource1" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
            </asp:TextBox>
            <br />
            

            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT ROLE_ID, ROLE_DESCRIPTION FROM USER_ROLES" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
            <asp:Label ID="Label2" class="sr-only" runat="server" Text="Set role to: "></asp:Label>
            <asp:DropDownList class="form-control" ID="roleDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="ROLE_DESCRIPTION" DataValueField="ROLE_ID" OnSelectedIndexChanged="statusDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce2_DataBound">
            </asp:DropDownList>
            <br />
            

            <asp:Button class="btn btn-lg btn-primary btn-block" ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
            <br />
            

            <asp:Label ID="FinalLabel" runat="server" Text=""></asp:Label>
            
            <div>
                <asp:RequiredFieldValidator runat="server" class="alert" id="RequiredFieldValidator1" controltovalidate="employeeIDField" errormessage="Please enter Employee ID!" />
            </div>
            
            <div>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="employeeIDField" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
                </asp:RegularExpressionValidator>
            </div>

        </div>
    </form>
</body>
</html>

