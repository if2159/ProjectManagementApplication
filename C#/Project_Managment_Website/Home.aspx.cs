using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Home : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void top_left(object sender, EventArgs e)
    {
        Response.Redirect("CreateTeam.aspx");
    }
    protected void top_middle(object sender, EventArgs e)
    {
        Response.Redirect("EditEmployees.aspx");
    }

    protected void top_right(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }

    protected void bottom_left(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }
    protected void bottom_middle(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }

    protected void bottom_right(object sender, EventArgs e)
    {
        Response.Redirect("CreateUser.aspx");
    }

}