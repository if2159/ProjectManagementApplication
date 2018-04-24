using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


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
                outputLabel.Text = "Entry has been recorded.";
            
            
        }
        checkForAlerts();
    }

    private void checkForAlerts() {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

            con.Open();
            //Submit the hours for the employee
            String submitStatement =
                "SELECT PROJECT_LEAD_EMAIL, PROJECT_NAME, ENTRY_ID FROM ALERTS WHERE EMAIL_SENT='FALSE'";

            SqlCommand cmd = new SqlCommand(submitStatement, con);


            cmd.Prepare();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if (rdr.HasRows)
                    {
                        sendEmail(rdr.GetString(0), rdr.GetString(1));
                        emailSent(rdr.GetInt32(2));


                    }
                }
            }
            con.Close();


        }
    }

    private void emailSent(int v)
    {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

            con.Open();
            //Submit the hours for the employee
            String submitStatement =
                "UPDATE ALERTS SET EMAIL_SENT='1' WHERE ENTRY_ID = '" + v + "'";

            SqlCommand cmd = new SqlCommand(submitStatement, con);


            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();


        }
    }

    private void sendEmail(string email, String projectName)
    {
        const String FROM = "ian@ianfennen.com";
        const String FROMNAME = "From Fennen";

        // Replace recipient@example.com with a "To" address. If your account 
        // is still in the sandbox, this address must be verified.
        const String TO = "ian@ianfennen.com";

        // Replace smtp_username with your Amazon SES SMTP user name.
        const String SMTP_USERNAME = "AKIAID4GRYFNPII2DNYQ";

        // Replace smtp_password with your Amazon SES SMTP user name.
        const String SMTP_PASSWORD = "AmarPFd3+fKjIpJj/bnYjC8ajrmL9mOeZAApt0C6Ox1a";

        // (Optional) the name of a configuration set to use for this message.
        // If you comment out this line, you also need to remove or comment out
        // the "X-SES-CONFIGURATION-SET" header below.
        //            const String CONFIGSET = "ConfigSet";

        // If you're using Amazon SES in a region other than US West (Oregon), 
        // replace email-smtp.us-west-2.amazonaws.com with the Amazon SES SMTP  
        // endpoint in the appropriate Region.
        const String HOST = "email-smtp.us-west-2.amazonaws.com";

        // The port you will connect to on the Amazon SES SMTP endpoint. We
        // are choosing port 587 because we will use STARTTLS to encrypt
        // the connection.
        const int PORT = 587;

        // The subject line of the email
        const String SUBJECT =
            "Project Budget Alert";

        // The body of the email
        String BODY =    "Project " + projectName + " has triggered a budget alert.";

        // Create and build a new MailMessage object
        MailMessage message = new MailMessage();
        message.IsBodyHtml = true;
        message.From = new MailAddress(FROM, FROMNAME);
        message.To.Add(new MailAddress(TO));
        message.Subject = SUBJECT;
        message.Body = BODY;
        // Comment or delete the next line if you are not using a configuration set
        //           message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

        // Create and configure a new SmtpClient
        SmtpClient client =
            new SmtpClient(HOST, PORT);
        // Pass SMTP credentials
        client.Credentials = new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
        // Enable SSL encryption
        client.EnableSsl = true;

        // Send the email. 
        try
        {
            Console.WriteLine("Attempting to send email...");
            client.Send(message);
            Console.WriteLine("Email sent!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("The email was not sent.");
            Console.WriteLine("Error message: " + ex.Message);
        }
    }
}