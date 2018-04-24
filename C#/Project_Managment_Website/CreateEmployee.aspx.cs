using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Employees : System.Web.UI.Page
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
    }

    /// <summary>
    /// Retrieves the user's information from the cookies and validates the session.
    /// </summary>
    /// <returns>True if a valid session. False otherwise.</returns>
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

    /// <summary>
    /// Called when the submit button is clicked. Will create a new Employee in the Database with the entered values
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Submit_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(fnameField.Text))
        {
            outputLabel.Text = "Please enter a first name";
        }
        else if (string.IsNullOrEmpty(lnameField.Text))
        {
            outputLabel.Text = "Please enter a last name";
        }
        else if (string.IsNullOrEmpty(eidField.Text))
        {
            outputLabel.Text = "Please enter a employee ID";
        }
        else if (string.IsNullOrEmpty(wageField.Text))
        {
            outputLabel.Text = "Please enter a wage";
        }

        else
        {
            String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

                con.Open();
                //Create a new Employee
                String submitStatement = "INSERT INTO EMPLOYEES (FIRST_NAME, MIDDLE_INIT, LAST_NAME, EMPLOYEE_ID, HOURLY_WAGE, TEAM_ID) VALUES (@FIRST_NAME, @MIDDLE_INIT, @LAST_NAME, @EMPLOYEE_ID, @HOURLY_WAGE, @TEAM_ID)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter fnameParameter = new SqlParameter("@FIRST_NAME", SqlDbType.VarChar, 35);
                SqlParameter minitParameter = new SqlParameter("@MIDDLE_INIT", SqlDbType.VarChar, 1);
                SqlParameter lnameParameter = new SqlParameter("@LAST_NAME", SqlDbType.VarChar, 35);
                SqlParameter eidParameter = new SqlParameter("@EMPLOYEE_ID", SqlDbType.Decimal);
                SqlParameter wageParameter = new SqlParameter("@HOURLY_WAGE", SqlDbType.Float);
                //SqlParameter manageParameter = new SqlParameter("@MANAGES", SqlDbType.Int);
                SqlParameter teamParameter = new SqlParameter("@TEAM_ID", SqlDbType.Int);

                fnameParameter.Value = fnameField.Text;
                assignValueString(mInitField.Text, minitParameter);
                //minitParameter.Value = mInitField.Text;
                lnameParameter.Value = lnameField.Text;
                eidParameter.Value = Decimal.Parse(eidField.Text);
                eidParameter.Scale = 0;
                eidParameter.Precision = 10;
                wageParameter.Value = float.Parse(wageField.Text);
                //assignValue(int.Parse(manageDropDown.SelectedValue), manageParameter);
                //teamParameter.Value = int.Parse(teamDropDown.SelectedValue);
                assignValue(int.Parse(teamDropDown.SelectedValue), teamParameter);
                

                cmd.Parameters.Add(fnameParameter);
                cmd.Parameters.Add(minitParameter);
                cmd.Parameters.Add(lnameParameter);
                cmd.Parameters.Add(eidParameter);
                cmd.Parameters.Add(wageParameter);
                //cmd.Parameters.Add(manageParameter);
                cmd.Parameters.Add(teamParameter);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                outputLabel.Text += "Entry has been recorded.";
            }
        }
    }

    protected void assignValue(int valueSelected, SqlParameter parameter)
    {
        if (valueSelected == 0)
        {
            parameter.Value = DBNull.Value;
        }
        else { parameter.Value = valueSelected; }
    }

    protected void assignValueString(string valueSelected, SqlParameter parameter)
    {
        if (string.IsNullOrEmpty(valueSelected))
        {
            parameter.Value = DBNull.Value;
        }
        else { parameter.Value = valueSelected; }
    }

    protected void SqlDataSource1_DataBound(object sender, EventArgs e)
    {
        teamDropDown.Items.Insert(0,new ListItem("None", "0"));
        teamDropDown.SelectedIndex = 0; ;

    }
}
