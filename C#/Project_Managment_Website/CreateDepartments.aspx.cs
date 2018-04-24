using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Departments : System.Web.UI.Page
{
    private static String[] allowedRoles = { "ADMIN", "DEPARTMENT_LEAD" };
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

        departmentAlert.Visible = false;
        streetNumberAlert.Visible = false;
        streetNameAlert.Visible = false;
        cityAlert.Visible = false;
        stateProvinceAlert.Visible = false;
        zipcodePostcodeAlert.Visible = false;
        countryAlert.Visible = false;

        departmentAlertLabel.Text = "";
        streetNumberAlertLabel.Text = "";
        streetNameAlertLabel.Text = "";
        cityAlertLabel.Text = "";
        stateProvinceAlertLabel.Text = "";
        zipcodePostcodeAlertLabel.Text = "";
        countryAlertLabel.Text = "";
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
        departmentAlert.Visible = false;
        streetNumberAlert.Visible = false;
        streetNameAlert.Visible = false;
        cityAlert.Visible = false;
        stateProvinceAlert.Visible = false;
        zipcodePostcodeAlert.Visible = false;
        countryAlert.Visible = false;

        departmentAlertLabel.Text = "";
        streetNumberAlertLabel.Text = "";
        streetNameAlertLabel.Text = "";
        cityAlertLabel.Text = "";
        stateProvinceAlertLabel.Text = "";
        zipcodePostcodeAlertLabel.Text = "";
        countryAlertLabel.Text = "";

        int alert_count = 0;
        if (string.IsNullOrEmpty(departmentNameField.Text))
        {
            departmentAlertLabel.Text = "Please enter a department";
            departmentAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrEmpty(streetNumberField.Text))
        {
            streetNumberAlertLabel.Text = "Please enter a street number";
            streetNumberAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrEmpty(streetNameField.Text))
        {
            streetNameAlertLabel.Text = "Please enter a street";
            streetNameAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrEmpty(cityField.Text))
        {
            cityAlertLabel.Text = "Please enter a city";
            cityAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrEmpty(stateProvinceField.Text))
        {
            stateProvinceAlertLabel.Text = "Please enter a State or Province";
            stateProvinceAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrEmpty(zipcodeField.Text))
        {
            zipcodePostcodeAlertLabel.Text = "Please enter a zipcode";
            zipcodePostcodeAlert.Visible = true;
            alert_count++;
        }
        if (string.IsNullOrEmpty(countryField.Text))
        {
            countryAlertLabel.Text = "Please enter a country";
            countryAlert.Visible = true;
            alert_count++;
        }

        if (alert_count > 0)
        {
            return;
        }
        else
        {
            outputLabel.Text = "";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

                con.Open();
                //Submit the hours for the employee
                String submitStatement =
                    "INSERT INTO DEPARTMENTS (NAME, STREET_NUMBER, STREET_NAME, CITY, STATE_PROVINCE_REGION, ZIPCODE, COUNTRY) " +
                    "VALUES (@NAME, @STREET_NUMBER, @STREET_NAME, @CITY, @STATE_PROVINCE_REGION, @ZIPCODE, @COUNTRY)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter nameParameter = new SqlParameter("@NAME", SqlDbType.VarChar, 70);
                SqlParameter streetNumberParameter = new SqlParameter("@STREET_NUMBER", SqlDbType.VarChar, 20);
                SqlParameter streetNameParameter = new SqlParameter("@STREET_NAME", SqlDbType.VarChar, 50);
                SqlParameter cityParameter = new SqlParameter("@CITY", SqlDbType.VarChar, 60);
                SqlParameter stateParameter = new SqlParameter("@STATE_PROVINCE_REGION", SqlDbType.VarChar, 50);
                SqlParameter zipcodeParameter = new SqlParameter("@ZIPCODE", SqlDbType.VarChar, 16);
                SqlParameter countryParameter = new SqlParameter("@COUNTRY", SqlDbType.VarChar, 90);

                nameParameter.Value = departmentNameField.Text;
                streetNumberParameter.Value = streetNumberField.Text;
                streetNameParameter.Value = streetNameField.Text;
                cityParameter.Value = cityField.Text;
                stateParameter.Value = stateProvinceField.Text;
                zipcodeParameter.Value = zipcodeField.Text;
                countryParameter.Value = countryField.Text;


                cmd.Parameters.Add(nameParameter);
                cmd.Parameters.Add(streetNumberParameter);
                cmd.Parameters.Add(streetNameParameter);
                cmd.Parameters.Add(cityParameter);
                cmd.Parameters.Add(stateParameter);
                cmd.Parameters.Add(zipcodeParameter);
                cmd.Parameters.Add(countryParameter);

                cmd.Prepare();
                cmd.ExecuteNonQuery();
                outputLabel.Text += "Entry has been recorded.";


            }
        }
    }

    protected void countryField_TextChanged(object sender, EventArgs e)
    {

    }
}
