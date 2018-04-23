using System;
using System.Data;
using System.Data.SqlClient;

public partial class SubmitHours : System.Web.UI.Page
{
    /// <summary>
    /// This method is called on page load. It will validate a user has a valid session 
    /// and redirect them to the login page if it is not valid.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession()) {
            Response.Redirect("Login.aspx");
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

    /// <summary>
    /// Called when the Submit button is clicked. Will submit the hours to the Database for the logged in user.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void submitHoursButton_Click(object sender, EventArgs e)
    {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString)) {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];
            
                con.Open();
                //Submit the hours for the employee
                String submitStatement =
                    "INSERT INTO TIMES (EID, HOURS_LOGGED, PROJECT_ID, ENTRY_DATE) VALUES (@EID, @HOURS_LOGGED, @PROJECT_ID, CURRENT_TIMESTAMP)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter eidParameter = new SqlParameter("@EID", SqlDbType.Decimal);
                SqlParameter hoursParameter = new SqlParameter("@HOURS_LOGGED", SqlDbType.Float);
                SqlParameter projectIdParameter = new SqlParameter("@PROJECT_ID", SqlDbType.Int);
                eidParameter.Value = Decimal.Parse(employeeID);
                eidParameter.Scale = 0;
                eidParameter.Precision = 10;
                hoursParameter.Value = float.Parse(hoursField.Text);
                projectIdParameter.Value = int.Parse(projectDropDown.SelectedValue);

                cmd.Parameters.Add(eidParameter);
                cmd.Parameters.Add(hoursParameter);
                cmd.Parameters.Add(projectIdParameter);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                outputLabel.Text += "Entry has been recorded.";
            
            
        }
    }
}