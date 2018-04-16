<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateTeam.aspx.cs" Inherits="CreateTeam" %>
<%@Import Namespace="System.Data" %>
<%@Import Namespace="System.Data.Common" %>
<%@Import Namespace="System.Data.SqlClient" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="Label1" runat="server" Text="Project ID"></asp:Label>



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [PROJECT_ID], [NAME] FROM [PROJECTS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
        <asp:DropDownList ID="projectDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="NAME" DataValueField="PROJECT_ID" OnSelectedIndexChanged="projectDropDown_SelectedIndexChanged" ondatabound ="SqlDataSouce1_DataBound">
        </asp:DropDownList>
        </div>

        <asp:Label ID="Label2" runat="server" Text="Department Name"></asp:Label>

        <asp:DropDownList ID="departmentDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="NAME" DataValueField="DEPARTMENT_ID" OnSelectedIndexChanged="departmentDropDown_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [DEPARTMENT_ID], [NAME] FROM [DEPARTMENTS]" OnSelecting="SqlDataSource2_Selecting"></asp:SqlDataSource>
        <br />

        <asp:Label ID="Label3" runat="server" Text="Team Lead"></asp:Label>

        
        <asp:TextBox ID="teamLeadID" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>

        
        <br />
        <br />

        </div>
        

            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Create Team" />

        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>

        <br />
        <br />

    </form>
</body>
</html>
