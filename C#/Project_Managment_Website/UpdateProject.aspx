<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateProject.aspx.cs" Inherits="UpdateProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Management - Update Project</title>
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
<form id="form1" runat="server">
    <div class="container">
        <h2 class="form-heading2">Changes to Project</h2>

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT PROJECT_ID, NAME FROM PROJECTS" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label1" runat="server" Text="Project to update:"></asp:Label>
        <div class="form-control">
        <asp:DropDownList class="dropdown-item" ID="projectsDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="NAME" DataValueField="PROJECT_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
        </asp:DropDownList>
        </div>
        <div class="alert alert-primary" runat="server" role="alert" id="projectAlert">
                    <asp:Label runat="server" id="projectAlertLabel"></asp:Label>
                </div>
        <br />
        

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT STATUS_DESCRIPTION, STATUS_ID FROM STATUSES" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label2" runat="server" Text="Set status update to: "></asp:Label>
        <div class="form-control">
        <asp:DropDownList class="dropdown-item" ID="statusDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="STATUS_DESCRIPTION" DataValueField="STATUS_ID" OnSelectedIndexChanged="statusDropDown_SelectedIndexChanged" AutoPostBack="true" ondatabound ="SqlDataSouce2_DataBound">
        </asp:DropDownList>
        </div>
        <div class="alert alert-primary" runat="server" role="alert" id="statusAlert">
                    <asp:Label runat="server" id="statusAlertLabel"></asp:Label>
                </div>
        <br />
        

        <asp:Button class="btn btn-lg btn-primary btn-block" ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        <br />
        

        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
    </div>
</form>
</body>
</html>
