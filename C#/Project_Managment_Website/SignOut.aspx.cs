using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignOut : System.Web.UI.Page
{
    String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie signOut = new HttpCookie("SessionID");
        signOut.Expires = DateTime.Now.AddDays(-1);
        Response.Cookies.Add(signOut);
    }    
}
