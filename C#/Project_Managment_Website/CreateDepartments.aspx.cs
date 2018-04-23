using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Departments : System.Web.UI.Page {
    private static String[] allowedRoles = { "ADMIN", "DEPARTMENT_LEAD"};
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
        } else if (!CheckRole()) {
            Response.Redirect("AccessForbidden.aspx");
        }
    }

    private bool CheckRole() {
        // ArrayList myArrayList = new ArrayList();
        // myArrayList.AddRange(myStringArray);
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