<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectProductivityView.aspx.cs" Inherits="ProjectProductivityView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Select custom date range:</h1>
        Project: <asp:DropDownList ID="projectDropDown" runat="server"></asp:DropDownList>
        <br />
        Start Date(MM/DD/YYYY):
        <asp:TextBox ID="startDateField" runat="server"></asp:TextBox>
        <br />
        End Date(MM/DD/YYYY):&nbsp;
        <asp:TextBox ID="endDateField" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="sumbitButton" runat="server" OnClick="sumbitButton_Click" Text="Load" />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Your team's combined work time is: " Visible="False"></asp:Label>
        <asp:Label ID="hoursWorkedByTeamDateLabel" runat="server" Text="0.0" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Your team's total cost is: " Visible="False"></asp:Label>
        <asp:Label ID="costOfTeamDateLabel" runat="server" Text="0.0" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label8" runat="server" Text="Your employees in order of hours worked: " Visible="False"></asp:Label>
        
        <br />
        <asp:Label ID="tableLabel" runat="server" Text=""></asp:Label>
        
    </form>
</body>
</html>
