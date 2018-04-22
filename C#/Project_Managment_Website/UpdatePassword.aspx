<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="UpdatePassword" %>

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
            <h2 class="form-heading2">Change Password</h2>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMAIL] FROM [USERS]"></asp:SqlDataSource>
            <asp:Label class="sr-only" ID="Label1" runat="server" Text="User email:"></asp:Label>
            <asp:TextBox class="form-control" ID="employeeEmailField" placeholder="User email:" runat="server" DataSourceID="SqlDataSource1" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
            </asp:TextBox>
            <br />


            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [HASHED_PASSWORD] FROM [USERS]"></asp:SqlDataSource>
            <asp:Label class="sr-only" ID="Label2" runat="server" Text="Old Password: "></asp:Label>
            <asp:TextBox class="form-control" ID="oldPasswordField" placeholder="Old Password:" runat="server" type="password" DataSourceID="SqlDataSource2" DataTextField="ROLE_DESCRIPTION" DataValueField="ROLE_ID" AutoPostBack="true">
            </asp:TextBox>
            <br />
         
            
            <asp:Label class="sr-only" ID="Label3" runat="server" Text="New Password: "></asp:Label>
            <asp:TextBox class="form-control"  ID="newPasswordField" placeholder="New Password:" runat="server" type="password"></asp:TextBox>
            <br />
            

            <asp:Button ID="Button1" class="btn btn-lg btn-primary btn-block" runat="server" OnClick="Button1_Click" Text="Submit" />
            <br />
            
            <div>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="employeeEmailField" errormessage="Please enter email" />
            </div>
            
            <div>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="newPasswordField" errormessage="Please enter new Password!" />
            </div>
            
            <div>
                <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="oldPasswordField" errormessage="Please enter old Password!!" />
            </div>

            <asp:Label ID="FinalLabel" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
