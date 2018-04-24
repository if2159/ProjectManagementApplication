using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePassword : System.Web.UI.Page
{
    String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }
    }


    protected void statusDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    protected void Button1_Click(object sender, EventArgs e)
    {
        String employeeID = LoginValidator.ValidateUserCredentials(oldPasswordField.Text, employeeEmailField.Text.ToLower());
        if (employeeID.Length > 0)
        { //Username/Password is valid
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "UPDATE USERS SET HASHED_PASSWORD = @HASHED_PASSWORD WHERE EMAIL = @EMAIL";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter EmployeeEmailParameter = new SqlParameter("@EMPLOYEE_ID", SqlDbType.VarChar, 70);
                SqlParameter NewPasswordParameter = new SqlParameter("@HASHED_PASSWORD", SqlDbType.VarChar, 66);

                EmployeeEmailParameter.Value = employeeEmailField.Text;
                NewPasswordParameter.Value = LoginValidator.HashPassword(newPasswordField.Text);


                cmd.Parameters.Add(EmployeeEmailParameter);
                cmd.Parameters.Add(NewPasswordParameter);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                FinalLabel.Text = ("User role has been updated!");
            }

        }
        else
        {//Username/password NOT valid.
            FinalLabel.Text = "Incorrect Email/Password Combination!";
        }


    }
}
