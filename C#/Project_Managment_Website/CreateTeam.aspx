<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateTeam.aspx.cs" Inherits="CreateTeam" %>
<%@Import Namespace="System.Data" %>
<%@Import Namespace="System.Data.Common" %>
<%@Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="CSS/bootstrap.css" rel="stylesheet" />
  <link href="CSS/Master.css" rel="stylesheet" />
</head>
<body>

    <div class="container">
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Project ID"></asp:Label>
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
        <asp:Label ID="Label2" runat="server" Text="Department Name"></asp:Label>
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
        <asp:Label ID="Label3" runat="server" Text="Team Lead"></asp:Label>        
        <asp:TextBox class="form-control" ID="teamLeadID" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
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
