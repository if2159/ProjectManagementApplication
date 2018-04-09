<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Departments.aspx.cs" Inherits="Departments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="Department Name: "></asp:Label>
        <asp:TextBox ID="departmentNameField" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Street Number:      "></asp:Label>
        <asp:TextBox ID="streetNumberField" runat="server" ></asp:TextBox>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Street Name: "></asp:Label>
        <asp:TextBox ID="streetNameField" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label4" runat="server" Text="City: "></asp:Label>
        <asp:TextBox ID="cityField" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label5" runat="server" Text="State/Province: "></asp:Label>
        <asp:TextBox ID="stateProvinceField" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label6" runat="server" Text="Zipcode/Postcode: "></asp:Label>
        <asp:TextBox ID="zipcodeField" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label7" runat="server" Text="Country: "></asp:Label>
        <asp:TextBox ID="countryField" runat="server"></asp:TextBox>
        <br />
        <asp:Button ID="submitButton" runat="server" OnClick="submitButton_Click" Text="Submit" />
    
        <br />
        <asp:Label ID="outputLabel" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
