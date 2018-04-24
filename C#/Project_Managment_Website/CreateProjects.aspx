<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateProjects.aspx.cs" Inherits="CreateProjects" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Management - Create Project</title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Master.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.1.1/css/bootstrap.min.css"/>
   
</head>
<body>
    <nav class="navbar navbar-expand-lg navbar-light bg-light" style="background-color: #4169e1;">
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
<div class="container">
<form id="form1" runat="server">
    
        <h2 class="form-signin-heading">Create Project</h2>
    
        <asp:Label class="sr-only" ID="Label1" runat="server" Text="Project Name: "></asp:Label>
        <asp:TextBox class="form-control" ID="projectNameField" placeholder="Project Name: " runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="projectNameField" errormessage="Please enter name!" />
        <br />

        <asp:Label class="sr-only" ID="Label2" runat="server" Text="Budget:      "></asp:Label>
        <asp:TextBox ID="budgetField" class="form-control" placeholder="Budget:" runat="server" ></asp:TextBox>
        <br/>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [DEPARTMENT_ID] FROM [DEPARTMENTS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label3" runat="server" Text="Controlling Department: "></asp:Label>    
        <div class="form-control">
        <asp:DropDownList class="dropdown-item" ID="controllingDepartmentField" runat="server" DataSourceID="SqlDataSource1" DataTextField="DEPARTMENT_ID" DataValueField="DEPARTMENT_ID" OnSelectedIndexChanged="deparmentsDropDown_SelectedIndexChanged" AutoPostBack="True" OnDataBound ="SqlDataSouce1_DataBound">
        </asp:DropDownList>
        </div>
        <br />

        
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMPLOYEE_ID] FROM [EMPLOYEES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label4" runat="server" Text="Employee ID: "></asp:Label>
        <asp:TextBox ID="employeeIDField" class="form-control" placeholder="Employee ID:" runat="server" DataSourceID="SqlDataSource3" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="employeeIDField_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce3_DataBound" OnTextChanged="employeeIDField_TextChanged1"></asp:TextBox>
        <br/>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [TEAM_ID] FROM [TEAMS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label5" runat="server" Text="Controlling Team: "></asp:Label>
        <div class="form-control">
        <asp:DropDownList class="dropdown-item"  ID="teamsDropDownField" runat="server" DataSourceID="SqlDataSource2" DataTextField="TEAM_ID" DataValueField="TEAM_ID" OnSelectedIndexChanged="teamsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce2_DataBound">
        </asp:DropDownList>
        </div>
        <br />

        <asp:Label class="sr-only" ID="Label6" runat="server" Text="Start Date:  "></asp:Label>
        <asp:TextBox class="form-control" placeholder="Start Date:" ID="startDateField" runat="server" ReadOnly="True"></asp:TextBox>
        <br />

        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [STATUS_ID],[STATUS_DESCRIPTION] FROM [STATUSES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label7" runat="server" Text="Status Type:  "></asp:Label>   
        <div class="form-control">
        <asp:DropDownList class="dropdown-item" ID="statusTypeField" placeholder="HJHJDHJSDFGHGFDSGH" runat="server" DataSourceID="SqlDataSource4" DataTextField="STATUS_DESCRIPTION" DataValueField="STATUS_ID" OnSelectedIndexChanged="statusTypeField_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce4_DataBound">
        </asp:DropDownList>
        <div></div>
        <br />
        

        <asp:Button class="btn btn-lg btn-primary btn-block" ID="submitButton" runat="server" OnClick="submitButton_Click" Text="Submit" />
        <br />
        
        <asp:Label class="alert" ID="finalLabel" runat="server" Text=""></asp:Label>
        
        <div>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator2" controltovalidate="budgetField" errormessage="Please enter Budget!" />
        </div>
        
        <div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="budgetField" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator3" controltovalidate="employeeIDField" errormessage="Please enter Employee ID!" />
        </div>

        <div>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="employeeIDField" runat="server" ErrorMessage="Only Numbers allowed" ValidationExpression="\d+">
            </asp:RegularExpressionValidator>
        </div>
</form>
</div>
</body>
</html>
      
    
