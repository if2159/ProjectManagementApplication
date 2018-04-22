using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

public partial class Login : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// This method is called whenever the login button is clicked. 
    /// It validates the user's password and email combination.
    /// </summary>
    /// <param name="sender"> Auto generated param</param>
    /// <param name="e">Auto generated param</param>
    protected void loginButton_Click(object sender, EventArgs e)
    {
        String employeeID = LoginValidator.ValidateUserCredentials(passwordField.Text, emailField.Text.ToLower());
        if (employeeID.Length > 0)
        { //Username/Password is valid
            loginSuccess(employeeID);
        }
        else
        {//Username/password NOT valid.
            errorLabel.Text = "Incorrect Email/Password Combination!";
        }
    }

    /// <summary>
    /// Is called whenever a user is successfully validated.
    /// Redirects the user to the SubmiteHours page.
    /// </summary>
    /// <param name="employeeID">The employee ID used to create the session tokens.</param>
    private void loginSuccess(String employeeID)
    {
        Guid sessionID = createSessionToken(employeeID);
        Response.Redirect("UpdateProject.aspx");
    }

    /// <summary>
    /// Creates the user session Token then rights it to the user's browser and the the Database.
    /// Generates the token using a GUID.
    /// </summary>
    /// <param name="employeeID">The employee ID to use for the session token.</param>
    /// <returns>The GUID Created for this session.</returns>
    private Guid createSessionToken(String employeeID)
    {
        Guid sessionID = Guid.NewGuid();
        writeSessionID(sessionID, employeeID);
        writeCookie(sessionID, employeeID);
        return sessionID;
    }

    /// <summary>
    /// This method writes the user's session token to their browser.
    /// Cookie values:
    /// Username=employeeID
    /// SessionID=token
    /// </summary>
    /// <param name="sessionID">The GUID that identifies this users session.</param>
    /// <param name="employeeID">The user this session is for.</param>
    private void writeCookie(Guid sessionID, string employeeID)
    {
        HttpCookie userCookie = new HttpCookie("UserID");
        userCookie["Username"] = employeeID;
        HttpCookie sessionCookie = new HttpCookie("SessionID");
        sessionCookie["SessionID"] = sessionID.ToString();
        sessionCookie.Expires = DateTime.Now.AddDays(1d);
        Response.Cookies.Add(sessionCookie);
        Response.Cookies.Add(userCookie);
    }

    /// <summary>
    /// This method writes the supplied session ID to the Database SESSIONS Table
    /// </summary>
    /// <param name="sessionID">The identifying session token.</param>
    /// <param name="employeeID">The employee that the session is for.</param>
    private void writeSessionID(Guid sessionID, string employeeID)
    {
        String connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "INSERT INTO SESSIONS (EMPLOYEE_ID, SESSION_ID, ENTRY_DATE, EXPIRATION_DATE) " +
                                    "VALUES(@EID, @SID, CURRENT_TIMESTAMP, DATEADD(minute, 10, SYSDATETIME()))";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@EID", SqlDbType.Decimal);
            eidParameter.Scale = 0;
            eidParameter.Precision = 10;
            eidParameter.Value = Decimal.Parse(employeeID);
            SqlParameter sisParameter = new SqlParameter("@SID", SqlDbType.VarChar, 36);
            sisParameter.Value = sessionID.ToString();

            cmd.Parameters.Add(eidParameter);
            cmd.Parameters.Add(sisParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
