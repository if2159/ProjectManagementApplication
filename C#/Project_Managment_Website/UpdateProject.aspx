<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateProject.aspx.cs" Inherits="UpdateProject" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT PROJECT_ID, NAME FROM PROJECTS" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
              <asp:Label ID="Label1" runat="server" Text="Project to update:"></asp:Label>
              <asp:DropDownList ID="projectsDropDown" runat="server" DataSourceID="SqlDataSource1" DataTextField="NAME" DataValueField="PROJECT_ID" OnSelectedIndexChanged="projectsDropDown_SelectedIndexChanged" AutoPostBack="true" OnDataBound ="SqlDataSouce1_DataBound">
              </asp:DropDownList>
              
            <br />

              <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:PROJECT_MANAGMENTConnectionString %>" SelectCommand="SELECT STATUS_DESCRIPTION, STATUS_ID FROM STATUSES" OnSelecting="SqlDataSource1_Selecting"></asp:SqlDataSource>
              <asp:Label ID="Label2" runat="server" Text="Set status update to: "></asp:Label>
              <asp:DropDownList ID="statusDropDown" runat="server" DataSourceID="SqlDataSource2" DataTextField="STATUS_DESCRIPTION" DataValueField="STATUS_ID" OnSelectedIndexChanged="statusDropDown_SelectedIndexChanged" AutoPostBack="true" >
              </asp:DropDownList>



              <br />
              <br />
              <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />



              <br />
              <asp:Label ID="Label3" runat="server" Text=""></asp:Label>



        </div>
    </form>
</body>
</html>
