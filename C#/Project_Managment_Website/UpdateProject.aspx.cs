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
    
    private static String[] allowedRoles = { "DEPARTMENT_LEAD", "ADMIN" };
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
        if (Request.Cookies["SessionID"] != null && Request.Cookies["UserID"] != null)
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
        Label3.Text = "";
        int statusID = autoSelectCurrentProject();
        if (int.Parse(projectsDropDown.SelectedValue) == 0)
        {
            Label3.Text = "Please select a project";
        }
        else
        {

            Label3.Text = "";
            //int statusID = int.Parse(projectsDropDown.SelectedValue);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "SELECT STATUS_DESCRIPTION FROM STATUSES WHERE STATUS_ID = @statusID";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter ProjectIDParameter = new SqlParameter("@statusID", SqlDbType.Int);
                ProjectIDParameter.Value = statusID;
                cmd.Parameters.Add(ProjectIDParameter);
                cmd.Prepare();

                string roleDesc;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    roleDesc = (string)rdr["STATUS_DESCRIPTION"];
                }

                statusDropDown.SelectedIndex =
                    statusDropDown.Items.IndexOf(statusDropDown.Items.FindByText(roleDesc));
            }
        }
        

    }

    protected int autoSelectCurrentProject()
    {
        if (int.Parse(projectsDropDown.SelectedValue) == 0)
        {
            return 0;
        }
  
        else
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "SELECT STATUS_TYPE FROM PROJECTS WHERE PROJECT_ID = @projectID";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter projectIDParameter = new SqlParameter("@projectID", SqlDbType.Int);
                projectIDParameter.Value = int.Parse(projectsDropDown.SelectedValue);
                cmd.Parameters.Add(projectIDParameter);
                cmd.Prepare();

                int statusID;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    if (!rdr.IsDBNull(rdr.GetOrdinal("STATUS_TYPE")))
                    {
                        statusID = (int)rdr["STATUS_TYPE"];
                    }
                    else
                    {
                        statusID = 0;
                    }
                    con.Close();
                    return statusID;
                }
            }
        }


    }
    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {
        projectsDropDown.Items.Insert(0, new ListItem("-Select-", "0"));
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
        statusDropDown.Items.Insert(0, new ListItem("-Select-", "0"));
        statusDropDown.SelectedIndex = 0; ;
    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }





    protected void Button1_Click(object sender, EventArgs e)
    {
        if (int.Parse(projectsDropDown.SelectedValue) == 0)
        {
            Label3.Text = "Please select a project";
        }
        else if(int.Parse(statusDropDown.SelectedValue) == 0)
        {
            Label3.Text = "Please select a status";
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
                Label3.Text = "Team Updated!";
            }
        }
    }
}
