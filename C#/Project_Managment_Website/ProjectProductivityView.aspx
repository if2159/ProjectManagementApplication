<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProjectProductivityView.aspx.cs" Inherits="ProjectProductivityView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="CSS/bootstrap.css" rel="stylesheet" />
    <link href="CSS/Master.css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="container">

    <h1>Select custom date range:</h1>

    <asp:DropDownList class="form-control" placeholder="Project:" ID="projectDropDown" runat="server"></asp:DropDownList>
    <br />
    
    
    <asp:TextBox class="form-control" placeholder="Start Date(MM/DD/YYYY):" ID="startDateField" runat="server"></asp:TextBox>
    <br />
    
    
    &nbsp;
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
