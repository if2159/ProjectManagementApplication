<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamView.aspx.cs" Inherits="TeamView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Management - Team</title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Master.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css"/>
</head>
<body>
   <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" id="color1" href="#">PM</a>
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

        <br />
        <asp:Label ID="Label4" runat="server" Text="View Productivity of Each Team Member" BorderStyle="None" Font-Bold="True" Font-Size="20pt" ForeColor="Black" Height="41px" Width="826px"></asp:Label>
        <br />
        <br />
        <br />
        <br />

        <asp:Label class="sr-only" ID="Label1" runat="server" Text="Team to view"></asp:Label>
        <div class="form-control">
        <asp:DropDownList class="dropdown-item" ID="teamsLeadingDropDown" runat="server" DataTextField="TEAM_ID" DataValueField="TEAM_ID" OnSelectedIndexChanged="teamsDropDown_SelectedIndexChanged" OnLoad ="onload_teamsLeadingDropDown" ondatabound ="SqlDataSouce1_DataBound" AutoPostBack ="true">
        </asp:DropDownList>
        </div>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           
    
        <asp:Label class="sr-only" ID="Label2" runat="server" Text="Projects created in the last:"></asp:Label>
        <div class="form-control">
        <asp:DropDownList class="dropdown-item" ID="dateSelectedDropDown" runat="server" OnSelectedIndexChanged="dateSelectedDropDown_SelectedIndexChanged">
        </asp:DropDownList>
        </div>
        <br />
        <br />
        <br />

        <asp:Button class="btn btn-lg btn-primary btn-block" ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
        <br />
        <asp:Label class="text-info" ID="Label6" runat="server" Text=""></asp:Label>
        <br />
        <asp:Label class="text-info" ID="Label5" runat="server" Text=""></asp:Label>
        <br />

        <br />
        <asp:Label class="text-info" ID="Label3" runat="server" Text=""></asp:Label>

        <table class="table">
          <thead class="thead-light" id="teamTable" runat="server">
            <tr>
              <th scope="col">Hours Worked</th>
              <th scope="col">First</th>
              <th scope="col">Last</th>
            </tr>
          </thead>
          <tbody>  
            <div id="divOnPage" runat="server"></div>
          </tbody>
        </table>

    </div>
</form>
</body>
</html>
