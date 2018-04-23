<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateProjectStatuses.aspx.cs" Inherits="CreateProjectStatuses" %>

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
    
            <h1>Create New Project Status</h1>
            <p>
                <asp:Label ID="roleNameLabel" runat="server" Text="Status Description: "></asp:Label>
                <asp:TextBox ID="roleNameField" class="form-control" runat="server"></asp:TextBox>
                <div class="alert alert-primary" runat="server" role="alert" id="createStatusAlert">
                        <asp:Label runat="server" id="createStatusAlertLabel"></asp:Label>
                    </div>
            </p>
            <p>
                <asp:Button ID="submitButton" class="btn btn-lg btn-primary btn-block" runat="server" Text="Submit" OnClick="submitButton_Click" />
            </p>
            <p>
                <asp:Label ID="outputLabel" runat="server" Text=""></asp:Label>
            </p>
    
        </div>
    </form>
</div>
</body>
</html>
