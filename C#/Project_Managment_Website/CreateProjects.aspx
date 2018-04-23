<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateProjects.aspx.cs" Inherits="CreateProjects" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/Master.css" rel="stylesheet" type="text/css" />
    <link href="CSS/bootstrap.css" rel="stylesheet" />
</head>
<body>
<div class="container">
<form id="form1" runat="server">
    
        <h2 class="form-signin-heading">New Project</h2>
    
        <asp:Label class="sr-only" ID="Label1" runat="server" Text="Project Name: "></asp:Label>
        <asp:TextBox class="form-control" ID="projectNameField" placeholder="Project Name: " runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator runat="server" id="RequiredFieldValidator1" controltovalidate="projectNameField" errormessage="Please enter name!" />
        <br />

        <asp:Label class="sr-only" ID="Label2" runat="server" Text="Budget:      "></asp:Label>
        <asp:TextBox ID="budgetField" class="form-control" placeholder="Budget:" runat="server" ></asp:TextBox>
        <br/>
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [DEPARTMENT_ID] FROM [DEPARTMENTS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label3" runat="server" Text="Controlling Department: "></asp:Label>    
        <asp:DropDownList class="form-control" ID="controllingDepartmentField" runat="server" DataSourceID="SqlDataSource1" DataTextField="DEPARTMENT_ID" DataValueField="DEPARTMENT_ID" OnSelectedIndexChanged="deparmentsDropDown_SelectedIndexChanged" AutoPostBack="True" OnDataBound ="SqlDataSouce1_DataBound">
        </asp:DropDownList> 
        <br />
        
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [EMPLOYEE_ID] FROM [EMPLOYEES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label4" runat="server" Text="Employee ID: "></asp:Label>
        <asp:TextBox ID="employeeIDField" class="form-control" placeholder="Employee ID:" runat="server" DataSourceID="SqlDataSource3" DataTextField="EMPLOYEE_ID" DataValueField="EMPLOYEE_ID" OnSelectedIndexChanged="employeeIDField_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce3_DataBound" OnTextChanged="employeeIDField_TextChanged1"></asp:TextBox>
        <br/>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [TEAM_ID] FROM [TEAMS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label5" runat="server" Text="Controlling Team: "></asp:Label>
        <asp:DropDownList class="form-control"  ID="teamsDropDownField" runat="server" DataSourceID="SqlDataSource2" DataTextField="TEAM_ID" DataValueField="TEAM_ID" OnSelectedIndexChanged="teamsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce2_DataBound">
            <asp:ListItem>Empty</asp:ListItem>
        </asp:DropDownList>
        <br />

        <asp:Label class="sr-only" ID="Label6" runat="server" Text="Start Date:  "></asp:Label>
        <asp:TextBox class="form-control" placeholder="Start Date:" ID="startDateField" runat="server" ReadOnly="True"></asp:TextBox>
        <br />

        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [STATUS_ID],[STATUS_DESCRIPTION] FROM [STATUSES]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:Label class="sr-only" ID="Label7" runat="server" Text="Status Type:  "></asp:Label>   
        <asp:DropDownList class="form-control" ID="statusTypeField" placeholder="HJHJDHJSDFGHGFDSGH" runat="server" DataSourceID="SqlDataSource4" DataTextField="STATUS_DESCRIPTION" DataValueField="STATUS_ID" OnSelectedIndexChanged="statusTypeField_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce4_DataBound">
        </asp:DropDownList>
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
      
    
