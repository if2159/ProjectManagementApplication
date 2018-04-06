using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SubmitHours : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession()) {
            Response.Redirect("Login.aspx");
        }
    }

    private bool AuthenticateSession()
    {
        if (Request.Cookies["SessionID"] != null){
            String sessionID = Request.Cookies["SessionID"].Value.Split('=')[1];
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];
            return LoginValidator.ValidateSession(employeeID, sessionID);
        } else {
            return false;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString)) {
            String employeeID = LoginValidator.ValidateUserCredentials(passwordField.Text, emailField.Text.ToLower());
            if (employeeID.Length > 0) {
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
            else {
                outputLabel.Text += "INVALID USERNAME PASSWORD COMBINATION!";
            }
        }
    }
}