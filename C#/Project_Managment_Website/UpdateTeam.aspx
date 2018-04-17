<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateTeam.aspx.cs" Inherits="UpdateTeam" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT [TEAM_ID] FROM [TEAMS]" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
              <asp:Label ID="Label1" runat="server" Text="Team to update:"></asp:Label>
              <asp:DropDownList ID="teamsDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="TEAM_ID" DataValueField="TEAM_ID" OnSelectedIndexChanged="teamsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
              </asp:DropDownList>
              <br />
              <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT PROJECT_ID, NAME FROM PROJECTS" OnSelecting="SqlDataSource2_Selecting"></asp:SqlDataSource>
              <asp:Label ID="Label2" runat="server" Text="Project Name"></asp:Label>
              <asp:DropDownList ID="projectsDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="NAME" DataValueField="PROJECT_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce2_DataBound">
              </asp:DropDownList>
              <br />



        </div>
            <asp:Label ID="Label3" runat="server" Text= "If updating team lead enter here:"></asp:Label>
            <asp:TextBox ID="teamLeadIDTextBox" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
            <br />
            <br />

            <br />
        </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Update Team" />
        <br />
        <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        <br \ />

    </form>
</body>
</html>
