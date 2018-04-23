<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectProductivityView.aspx.cs" Inherits="ProjectProductivityView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Management - Projects</title>
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
<form id="form1" runat="server">
    <div class="container">

    <h1>Select custom date range:</h1>

    <div class="form-control">
    <asp:DropDownList class="dropdown-item" ID="projectDropDown" runat="server"></asp:DropDownList>
    </div>
    <br />
    
    
    <asp:TextBox class="form-control" placeholder="Start Date(MM/DD/YYYY):" ID="startDateField" runat="server"></asp:TextBox>
    <br />
    
    
    <asp:TextBox class="form-control" placeholder="End Date(MM/DD/YYYY):" ID="endDateField" runat="server"></asp:TextBox>
    <br />
    

    <asp:Button class="btn btn-lg btn-primary btn-block" ID="sumbitButton" runat="server" OnClick="sumbitButton_Click" Text="Load" />
    <br />
    
    
    <asp:Label class="text-info" ID="Label6" runat="server" Text="Your team's combined work time is: " Visible="False"></asp:Label>
    
    
    <asp:Label class="text-info" ID="hoursWorkedByTeamDateLabel" runat="server" Text="0.0" Visible="False"></asp:Label>
    <br />
    
    
    <asp:Label class="text-info" ID="Label7" runat="server" Text="Your team's total cost is: " Visible="False"></asp:Label>
    
    
    <asp:Label class="text-info" ID="costOfTeamDateLabel" runat="server" Text="0.0" Visible="False"></asp:Label>
    <br />
    
    
    <asp:Label class="text-info" ID="Label8" runat="server" Text="Your employees in order of hours worked: " Visible="False"></asp:Label>
    <br />
    
    
    <asp:Label ID="tableLabel" runat="server" Text=""></asp:Label>
    </div>
</form>
</body>
</html>
