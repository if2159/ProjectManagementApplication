using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlTypes;

public partial class UpdateProject : System.Web.UI.Page
{
    
    private static String[] allowedRoles = { "DEPARTMENT_LEAD", "ADMIN", "TEAM_LEAD" };
    private static String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }
        else if (!CheckRole())
        {
            Response.Redirect("AccessForbidden.aspx");
        }
        
    }
    
    private bool AuthenticateSession()
    {
        if (Request.Cookies["SessionID"] != null)
        {
            String sessionID = Request.Cookies["SessionID"].Value.Split('=')[1];
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];
            return LoginValidator.ValidateSession(employeeID, sessionID);
        }
        else
        {
            return false;
        }
    }

    private bool CheckRole()
    {
        if (Request.Cookies["SessionID"] != null)
        {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];
            ArrayList roles = new ArrayList();
            roles.AddRange(allowedRoles);
            return LoginValidator.ValidatorUserRole(employeeID, roles);
        }
        else
        {
            return false;
        }
    }
     
    //projectsDropDown
    protected void projectsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {
        projectsDropDown.Items.Insert(0, new ListItem("-Select-", String.Empty));
        projectsDropDown.SelectedIndex = 0; ;
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }






    //for Status
    protected void statusDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SqlDataSouce2_DataBound(object sender, EventArgs e)
    {
        projectsDropDown.Items.Insert(0, new ListItem("-Select-", String.Empty));
        projectsDropDown.SelectedIndex = 0; ;
    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty((projectsDropDown.SelectedValue)))
        {
            Label3.Text = "Please selected a project";
        }
        else
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "UPDATE PROJECTS SET STATUS_TYPE = @statusType WHERE PROJECT_ID = @projectID";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter ProjectIDParameter = new SqlParameter("@projectID", SqlDbType.Int);
                SqlParameter statusTypeParameter = new SqlParameter("@statusType", SqlDbType.Int);



                ProjectIDParameter.Value = int.Parse(projectsDropDown.SelectedValue);
                statusTypeParameter.Value = int.Parse(statusDropDown.SelectedValue);

                cmd.Parameters.Add(ProjectIDParameter);
                cmd.Parameters.Add(statusTypeParameter);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Label3.Text = "Project Updated!";
            }
        }
    }
}
