using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void loginButton_Click(object sender, EventArgs e) {
        String employeeID = LoginValidator.ValidateUserCredentials(passwordField.Text, emailField.Text.ToLower());
        if (employeeID.Length > 0) { //Username/Password is valid
            loginSuccess(employeeID);
        }
        else {//Username/password NOT valid.
            errorLabel.Text = "Incorrect Email/Password Combination!";
        }
    }

    private void loginSuccess(String employeeID) {
        Guid sessionID = createSessionCookie(employeeID);
        Response.Redirect("SubmitHours.aspx");
    }

    private Guid createSessionCookie(String employeeID)
    {
        Guid sessionID = Guid.NewGuid();
        writeSessionID(sessionID, employeeID);
        writeCookie(sessionID, employeeID);
        return sessionID;
    }

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

    private void writeSessionID(Guid sessionID, string employeeID)
    {
        String connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString)) {
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