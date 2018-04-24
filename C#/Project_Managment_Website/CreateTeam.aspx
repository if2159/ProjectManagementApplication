<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateTeam.aspx.cs" Inherits="CreateTeam" %>
<%@Import Namespace="System.Data" %>
<%@Import Namespace="System.Data.Common" %>
<%@Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Management - Create Teams</title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Master.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css"/>
</head>
<body>
   <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand"  href="#">
            <img class="hello" src="Images/LogoMakr_4taEgE.png" />
        </a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>

        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item active">
                    <a class="nav-link" href="Home.aspx">Home <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="SubmitHours.aspx">Submit Hours</a>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Reports
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <a class="dropdown-item" href="ProjectProductivityView.aspx">Project Report</a>
                        <a class="dropdown-item" href="TeamView.aspx">Team Report</a>
                    </div>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Create
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <a class="dropdown-item" href="CreateUser.aspx">Create Users</a>
                        <a class="dropdown-item" href="CreateTeam.aspx">Create Team</a>
                        <a class="dropdown-item" href="CreateUserRoles.aspx">Create Roles</a>
                        <a class="dropdown-item" href="CreateProjectStatuses.aspx">Create Statuses</a>
                        <a class="dropdown-item" href="CreateDepartments.aspx">Create Departments</a>
                        <a class="dropdown-item" href="CreateEmployee.aspx">Create Employees</a>
                        <a class="dropdown-item" href="CreateProjects.aspx">Create Projects</a>
                    </div>
                </li>
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">Update
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <a class="dropdown-item" href="UpdateUser.aspx">Update Users</a>
                        <a class="dropdown-item" href="UpdateTeam.aspx">Update Team</a>
                        <a class="dropdown-item" href="UpdateEmployees.aspx">Update Employees</a>
                        <a class="dropdown-item" href="UpdateProject.aspx">Update Projects</a>
                        <a class="dropdown-item" href="UpdatePassword.aspx">Update Password</a>
                    </div>
                </li>
            </ul>
        </div>
        <div class="form-inline my-2 my-lg-0 ">
            <a class="form-control mr-sm-2 " href="SignOut.aspx">Signout</a>

        </div>
    </nav>

    <div class="container">
    <form id="form1" runat="server">

        <h2 class="form-heading2">Create New Team</h2>

        <div>
            
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [PROJECT_ID], [NAME] FROM [PROJECTS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <div class="form-control">
            <asp:DropDownList class="dropdown-item" ID="projectDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="NAME" DataValueField="PROJECT_ID" OnSelectedIndexChanged="projectDropDown_SelectedIndexChanged" ondatabound ="SqlDataSouce1_DataBound">
        
            </asp:DropDownList>
            </div>
            <div class="alert alert-primary" runat="server" role="alert" id="projectIDAlert">
                    <asp:Label runat="server" id="projectIDAlertLabel"></asp:Label>
                </div>
        </div>

        <div>
        
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [DEPARTMENT_ID], [NAME] FROM [DEPARTMENTS]" OnSelecting="SqlDataSource2_Selecting"></asp:SqlDataSource>
        <div class="form-control">
            <asp:DropDownList class="dropdown-item" ID="departmentDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="NAME" DataValueField="DEPARTMENT_ID" OnSelectedIndexChanged="departmentDropDown_SelectedIndexChanged" ondatabound ="SqlDataSouce2_DataBound">
        </asp:DropDownList>
            </div>
            <div class="alert alert-primary" runat="server" role="alert" id="departmentNameAlert">
                    <asp:Label runat="server" id="departmentNameAlertLabel"></asp:Label>
                </div>
        </div>

        <div>
       
        <asp:TextBox class="form-control" ID="teamLeadID" placeholder="Team Lead (Employee ID): " runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="teamLeadID" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
       </asp:RegularExpressionValidator>
            <div class="alert alert-primary" runat="server" role="alert" id="teamLeadAlert">
                    <asp:Label runat="server" id="teamLeadAlertLabel"></asp:Label>
                </div>
        </div>

         <asp:Button class="btn btn-lg btn-primary btn-block" ID="Button1" runat="server" OnClick="Button1_Click" Text="Create Team" />
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>

    </form>
    </div>
</body>
</html>
