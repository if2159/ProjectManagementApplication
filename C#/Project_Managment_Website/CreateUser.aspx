<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateUser.aspx.cs" Inherits="CreateUser" %>
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
        <a class="navbar-brand" href="#">Navbar</a>
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
        <form id="form1"  runat="server">
            <h2 class="form-heading2">Create New User</h2>
            <div>
                <!--<asp:Label ID="employeeIDLabel" runat="server" Text="Employee ID: "></asp:Label>-->
                <asp:TextBox ID="employeeIDField" placeholder="Employee ID: " class="form-control" runat="server"></asp:TextBox>
                <div class="alert alert-primary" runat="server" role="alert" id="employeeAlert">
                    <asp:Label runat="server" id="employeeAlertLabel"></asp:Label>
                </div>
            </div>

         

            

            <div>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [ROLE_DESCRIPTION], [ROLE_ID] FROM [USER_ROLES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
                <div class="form-control">
                <asp:DropDownList class="dropdown-item" ID="roleDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="ROLE_DESCRIPTION" DataValueField="ROLE_ID" OnSelectedIndexChanged="projectDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound"></asp:DropDownList>
                </div>
                    <div class="alert alert-primary" runat="server" role="alert" id="roleAlert">
                    <asp:Label runat="server" id="roleAlertLabel"></asp:Label>
                </div>
            </div>
                

            <div>
                <!--<asp:Label ID="emailLabel" runat="server" Text="Email: "></asp:Label>-->
                <asp:TextBox ID="emailField" placeholder="Email: " class="form-control" runat="server" ></asp:TextBox>
                <div class="alert alert-primary"  runat="server" role="alert" id="emailAlert">
                    <asp:Label runat="server" id="emailAlertLabel"></asp:Label>
                </div>
            </div>

            <div>
                <!--<asp:Label ID="passwordLabel" runat="server" Text="Password: "></asp:Label>-->
                <asp:TextBox ID="passwordField" class="form-control" placeholder="Password: " runat="server" type="password"></asp:TextBox>
                <div class="alert alert-primary" placeholder="Employee ID: " runat="server" role="alert" id="passwordAlert">
                    <asp:Label runat="server" id="passwordAlertLabel"></asp:Label>
                </div>
            </div>

            <p></p>
            <div>
                <asp:Button ID="submitButton" class="btn btn-lg btn-primary btn-block" runat="server" Text="Submit" OnClick="submitButton_Click" />
            </div>

            <p>
                <asp:Label ID="finalLabel" runat="server" Text=""></asp:Label>
            </p>
        </form>
    </div>
</body>
</html>
