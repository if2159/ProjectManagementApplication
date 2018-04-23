<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateEmployee.aspx.cs" Inherits="CreateEmployee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Management - Create Employees</title>
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

    </nav>
    



    <form method="post" id="form1" runat="server">


        <div class="container" >
            <h1>Create Employee</h1>

            <asp:Label class="sr-only" ID="fnameLabel" runat="server" Text="First Name: "></asp:Label>
            <asp:TextBox class="form-control" placeholder ="First Name: " ID="fnameField" runat="server"></asp:TextBox>
            <br />
            <asp:Label class="sr-only" ID="mInitLabel" runat="server" Text="Middle Initial: "></asp:Label>
            <asp:TextBox class ="form-control" placeholder ="Middle Initial" ID="mInitField" runat="server"></asp:TextBox>
            <br />
            <asp:Label class ="sr-only" ID="lnameLabel" runat="server" Text="Last Name: "></asp:Label>
            <asp:TextBox class ="form-control" placeholder="Last Name:" ID="lnameField" runat="server"></asp:TextBox>
            <br />
            <asp:Label class="sr-only" ID="eidLabel" runat="server" Text="Employee ID: "></asp:Label>
            <asp:TextBox class="form-control" placeholder ="Employee ID: " ID="eidField" runat="server"></asp:TextBox>
            <br />
            <asp:Label class ="sr-only" ID="wageLabel" runat="server" Text="Hourly Wage: "></asp:Label>
            <asp:TextBox class ="form-control" placeholder ="Employee Wage:" ID="wageField" runat="server"></asp:TextBox>
            <br />
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [TEAM_ID] FROM [TEAMS]"></asp:SqlDataSource>
            

            <asp:Label ID="teamLabel" runat="server" Text="Team: "></asp:Label>

            <div class="form-control">
            <asp:DropDownList class="dropdown-item" ID="teamDropDown" runat="server" DataSourceID="SqlDataSource1" DataValueField="TEAM_ID" OnDataBound="SqlDataSource1_DataBound"></asp:DropDownList>
            </div>
            
            <br />
            <asp:Button class ="btn btn-lg btn-primary btn-block" ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" />
            <br />
            <asp:Label ID="outputLabel" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            

        </div>
    </form>
</body>
</html>
