<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamView.aspx.cs" Inherits="TeamView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <br />
            <asp:Label ID="Label4" runat="server" Text="View Productivity of Each Team Member" BorderStyle="None" Font-Bold="True" Font-Size="20pt" ForeColor="Black" Height="41px" Width="826px"></asp:Label>
            <br />
            <br />
            <br />
            <br />

        <asp:Label ID="Label1" runat="server" Text="Team to view"></asp:Label>
        <asp:DropDownList ID="teamsLeadingDropDown" runat="server" DataTextField="TEAM_ID" DataValueField="TEAM_ID" OnSelectedIndexChanged="teamsDropDown_SelectedIndexChanged" OnLoad ="onload_teamsLeadingDropDown" ondatabound ="SqlDataSouce1_DataBound" AutoPostBack ="true">
        </asp:DropDownList>
           
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           
            <asp:Label ID="Label2" runat="server" Text="Projects created in the last"></asp:Label>
            <asp:DropDownList ID="dateSelectedDropDown" runat="server" OnSelectedIndexChanged="dateSelectedDropDown_SelectedIndexChanged">
            </asp:DropDownList>
            <br />
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit" />
            <br />

            <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
            </asp:GridView>
            <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
            <br />
            <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
        </div>
    </form>
</body>
</html>
