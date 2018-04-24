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
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class CreateUser : System.Web.UI.Page
{
    private static String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

    //Beginning of Email validation
    bool invalid = false;
    public bool IsValidEmail(string strIn)
    {
        invalid = false;
        if (String.IsNullOrEmpty(strIn))
            return false;

        // Use IdnMapping class to convert Unicode domain names.
        try
        {
            strIn = Regex.Replace(strIn, @"(@)(.+)$", this.DomainMapper,
                                  RegexOptions.None, TimeSpan.FromMilliseconds(200));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }

        if (invalid)
            return false;

        // Return true if strIn is in valid email format.
        try
        {
            return Regex.IsMatch(strIn,
                  @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                  @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                  RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }
        catch (RegexMatchTimeoutException)
        {
            return false;
        }
    }
    private string DomainMapper(Match match)
    {
        // IdnMapping class with default property values.
        IdnMapping idn = new IdnMapping();

        string domainName = match.Groups[2].Value;
        try
        {
            domainName = idn.GetAscii(domainName);
        }
        catch (ArgumentException)
        {
            invalid = true;
        }
        return match.Groups[1].Value + domainName;
    }
    //End of Email validation

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }

        employeeAlert.Visible = false;
        roleAlert.Visible = false;
        emailAlert.Visible = false;
        passwordAlert.Visible = false;

        employeeAlertLabel.Text = "";
        roleAlertLabel.Text = "";
        emailAlertLabel.Text = "";
        passwordAlertLabel.Text = "";

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

    protected void submitButton_Click(object sender, EventArgs e)
    {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

        employeeAlert.Visible = false;
        roleAlert.Visible = false;
        emailAlert.Visible = false;
        passwordAlert.Visible = false;

        employeeAlertLabel.Text = "";
        roleAlertLabel.Text = "";
        emailAlertLabel.Text = "";
        passwordAlertLabel.Text = "";

        int alert_count = 0;
        int value;
        
        if(!int.TryParse(employeeIDField.Text, out value))
        {
            employeeAlert.Visible = true;
            employeeAlertLabel.Text = "An Employee ID only contains numbers";
            alert_count++;
        }
        if (employeeIDField.Text.Length != 10)
        {
            employeeAlertLabel.Text = "Employee ID is 10 integers long";
            employeeAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrWhiteSpace(employeeIDField.Text))
        {
            employeeAlert.Visible = true;
            employeeAlertLabel.Text = "Please enter in an Employee ID";
            alert_count++;
        }
        if (string.IsNullOrWhiteSpace(passwordField.Text))
        {
            passwordAlert.Visible = true;
            passwordAlertLabel.Text = "Please enter in a Password";
            alert_count++;
        }
        if (string.IsNullOrWhiteSpace(emailField.Text))
        {
            emailAlert.Visible = true;
            emailAlertLabel.Text = "Please enter in an email";
            alert_count++;
        }
        if (!IsValidEmail(emailField.Text))
        {
            emailAlert.Visible = true;
            emailAlertLabel.Text = "Email is not in the correct format";
            alert_count++;
        }
        if (IsEmailTaken())
        {
            emailAlert.Visible = true;
            emailAlertLabel.Text = "Email is taken";
            alert_count++;
        }
        if (int.Parse(roleDropDown.SelectedValue) == -1)
        {
            roleAlert.Visible = true;
            roleAlertLabel.Text = "Please choose a role";
            alert_count++;
        }

        if (alert_count > 0)
        {
            return;
        }

        if (checkIfEmployeeIDIsTaken())
        {
            employeeAlert.Visible = true;
            employeeAlertLabel.Text = "Employee ID is already in use";
            alert_count++;
            return;
        }

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            //DO some function call to make sure the entered employeeID is valid 

            if (checkIfValidEmployeeID())
            {
                String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

                con.Open();
                //Submit user
                String submitStatement =
                    "INSERT INTO USERS (EMPLOYEE_ID, ROLE, EMAIL, HASHED_PASSWORD)" +
                    "VALUES (@EMPLOYEE_ID, @ROLE, @EMAIL, @HASHED_PASSWORD)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter employeeIDParameter = new SqlParameter("@EMPLOYEE_ID", SqlDbType.Int);
                SqlParameter roleParameter = new SqlParameter("@ROLE", SqlDbType.Int);
                SqlParameter emailParameter = new SqlParameter("@EMAIL", SqlDbType.VarChar, 250);
                SqlParameter hashedPasswordParameter = new SqlParameter("@HASHED_PASSWORD", SqlDbType.VarChar, 66);

                employeeIDParameter.Value = int.Parse(employeeIDField.Text);
                roleParameter.Value = int.Parse(roleDropDown.Text);
                emailParameter.Value = emailField.Text;
                hashedPasswordParameter.Value = LoginValidator.HashPassword(passwordField.Text);

                cmd.Parameters.Add(employeeIDParameter);
                cmd.Parameters.Add(roleParameter);
                cmd.Parameters.Add(emailParameter);
                cmd.Parameters.Add(hashedPasswordParameter);

                try
                {
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    finalLabel.Text = ("User has been created!");
                }
                catch (Exception exception)
                {
                    finalLabel.Text = "There was an error during SqlCommand execution";
                }
            }
            else
            {
                employeeAlert.Visible = true;
                employeeAlertLabel.Text = "Invalid Employee ID";
                return;
            }
        }
        return;
    }


    protected bool checkIfValidEmployeeID()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT E.EMPLOYEE_ID FROM EMPLOYEES AS E WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Int);
            if (string.IsNullOrEmpty(employeeIDField.Text))
            {
                return false;
            }
            eidParameter.Value = int.Parse(employeeIDField.Text);
            cmd.Parameters.Add(eidParameter);
            cmd.Prepare();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
    }

    protected bool checkIfEmployeeIDIsTaken()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT U.EMPLOYEE_ID FROM USERS AS U WHERE EMPLOYEE_ID = @EMPLOYEE_ID";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter employeeIDParameter = new SqlParameter("@EMPLOYEE_ID", SqlDbType.Int);
            if (string.IsNullOrEmpty(employeeIDField.Text))
            {
                return false;
            }
            employeeIDParameter.Value = int.Parse(employeeIDField.Text);
            cmd.Parameters.Add(employeeIDParameter);
            cmd.Prepare();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
    }

    protected bool IsEmailTaken()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT U.EMAIL FROM USERS AS U WHERE EMAIL = @EMAIL";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter emailParameter = new SqlParameter("@EMAIL", SqlDbType.VarChar, 250);
            if (string.IsNullOrEmpty(emailField.Text))
            {
                return false;
            }
            emailParameter.Value = emailField.Text.ToString();
            cmd.Parameters.Add(emailParameter);
            cmd.Prepare();
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                if (rdr.HasRows)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    con.Close();
                    return false;
                }
            }
        }
    }

    protected void assignValue(int valueSelected, SqlParameter parameter)
    {
        if (valueSelected == 0)
        {
            parameter.Value = DBNull.Value;
        }
        else
        {
            parameter.Value = valueSelected;
        }
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    //for ProjectDropDown{
    protected void projectDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {
        roleDropDown.Items.Insert(0, new ListItem("Role: ", "-1"));
        roleDropDown.SelectedIndex = 0; ;
    }

}


