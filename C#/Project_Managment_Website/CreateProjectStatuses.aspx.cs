using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateProjectStatuses : System.Web.UI.Page
{
    private static String[] allowedRoles = { "ADMIN" };
    private static String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

    /// <summary>
    /// This method is called on page load. It will validate a user has a valid session 
    /// and redirect them to the login page if it is not valid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

        createStatusAlert.Visible = false;
        createStatusAlertLabel.Text = "";
    }

    private bool CheckRole()
    {
        // ArrayList myArrayList = new ArrayList();
        // myArrayList.AddRange(myStringArray);
        if (Request.Cookies["SessionID"] != null && Request.Cookies["UserID"] != null)
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

    /// <summary>
    /// Retrieves the user's information from the cookies and validates the session.
    /// </summary>
    /// <returns>True if a valid session. False otherwise.</returns>
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

    protected void submitButton_Click(object sender, EventArgs e)
    {
        createStatusAlert.Visible = false;

        if (string.IsNullOrWhiteSpace(roleNameField.Text))
        {
            createStatusAlert.Visible = true;
            createStatusAlertLabel.Text = "You did not enter a status";
            return;
        }
        else
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

                con.Open();
                //Submit the hours for the employee
                String submitStatement =
                    "INSERT INTO STATUSES (STATUS_DESCRIPTION) " +
                    "VALUES (@DESCRIPTION)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter descriptionParameter = new SqlParameter("@DESCRIPTION", SqlDbType.VarChar, 20);

                descriptionParameter.Value = roleNameField.Text.ToUpper();

                cmd.Parameters.Add(descriptionParameter);


                cmd.Prepare();
                cmd.ExecuteNonQuery();
                outputLabel.Text += "Entry has been recorded.";


            }
        }
    }
}