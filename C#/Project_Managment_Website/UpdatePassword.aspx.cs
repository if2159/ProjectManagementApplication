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
    String eid;
    String temp;

    String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }
        employeeEmailField.Text = getEmail();

        newAlert.Visible = false;
        emailAlert.Visible = false;
        oldAlert.Visible = false;

        newAlertLabel.Text = "";
        oldAlertLabel.Text = "";
        emailAlertLabel.Text = "";

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
            eid = employeeID;
            return LoginValidator.ValidateSession(employeeID, sessionID);
        }
        else
        {
            return false;
        }
    }

    protected string getEmail()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {



            con.Open();

            String query = "SELECT EMAIL FROM USERS WHERE EMPLOYEE_ID = " + eid + ";";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Prepare();
            using (SqlDataReader rdr = cmd.ExecuteReader()) //get the employee's ID
            {

                while (rdr.Read())
                {


                    temp = rdr["EMAIL"].ToString();


                }

            }
            return temp;
        }
    }


protected void Button1_Click(object sender, EventArgs e)
    {
        newAlert.Visible = false;
        emailAlert.Visible = false;
        oldAlert.Visible = false;

        newAlertLabel.Text = "";
        oldAlertLabel.Text = "";
        emailAlertLabel.Text = "";

        int alert_count = 0;

        if (string.IsNullOrWhiteSpace(employeeEmailField.Text))
        {
            emailAlert.Visible = true;
            emailAlertLabel.Text = "Please enter in an Employee ID";
            alert_count++;
        }

        if (string.IsNullOrWhiteSpace(oldPasswordField.Text))
        {
            oldAlert.Visible = true;
            oldAlertLabel.Text = "Please enter in an Employee ID";
            alert_count++;
        }

        if (string.IsNullOrWhiteSpace(newPasswordField.Text))
        {
            newAlert.Visible = true;
            newAlertLabel.Text = "Please enter in an Employee ID";
            alert_count++;
        }

        if (alert_count > 0)
        {
            return;
        }

        String employeeID = LoginValidator.ValidateUserCredentials(oldPasswordField.Text, employeeEmailField.Text.ToLower());

        if (employeeID.Length > 0)
        { //Username/Password is valid
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "UPDATE USERS SET HASHED_PASSWORD = @HASHED_PASSWORD WHERE EMAIL = @EMAIL";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter EmployeeEmailParameter = new SqlParameter("@EMAIL", SqlDbType.VarChar, 70);
                SqlParameter NewPasswordParameter = new SqlParameter("@HASHED_PASSWORD", SqlDbType.VarChar, 66);

                EmployeeEmailParameter.Value = getEmail();
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
            newAlertLabel.Text = "Incorrect Email/Password Combination!";
            newAlert.Visible = true;
        }


    }
}
