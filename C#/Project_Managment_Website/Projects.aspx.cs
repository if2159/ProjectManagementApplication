using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Projects : System.Web.UI.Page
{
    /// <summary>
    /// This method is called on page load. It will validate a user has a valid session 
    /// and redirect them to the login page if it is not valid.
    /// </summary>B
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }
        startDateField.Text = DateTime.Now.ToShortDateString();
    }

    /// <summary>
    /// Retrieves the user's information from the cookies and validates the session.
    /// </summary>
    /// <returns>True if a valid session. False otherwise.</returns>
    private bool AuthenticateSession()
    {
        if (Request.Cookies["SessionID"] != null)
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


    protected void submitButton_Click(object sender, EventArgs e)
    {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

            con.Open();
            //Submit the hours for the employee
            String submitStatement =
                "INSERT INTO PROJECTS (NAME, BUDGET,CONTROLLING_DEPARTMENT,EID,CONTROLLING_TEAM,START_DATE,STATUS_TYPE)" +
                "VALUES (@NAME, @BUDGET,@CONTROLLING_DEPARTMENT,@EID,@CONTROLLING_TEAM,@START_DATE,@STATUS_TYPE)";
            SqlCommand cmd = new SqlCommand(submitStatement, con);
            SqlParameter nameParameter = new SqlParameter("@NAME", SqlDbType.VarChar, 70);
            SqlParameter budgetNumberParameter = new SqlParameter("@BUDGET", SqlDbType.VarChar, 20);
            SqlParameter controllingDeparmentNumberParameter = new SqlParameter("@CONTROLLING_DEPARTMENT", SqlDbType.VarChar, 50);
            SqlParameter eidNumberParameter = new SqlParameter("@EID", SqlDbType.VarChar, 60);
            SqlParameter controllingTeamNumberParameter = new SqlParameter("@CONTROLLING_TEAM", SqlDbType.VarChar, 50);
            SqlParameter startDateParameter = new SqlParameter("@START_DATE", SqlDbType.VarChar, 16);
            SqlParameter statusTypeParameter = new SqlParameter("@STATUS_TYPE", SqlDbType.VarChar, 90);

            nameParameter.Value = projectNameField.Text;
            budgetNumberParameter.Value = budgetField.Text;
            controllingDeparmentNumberParameter.Value = controllingDepartmentField.Text;
            eidNumberParameter.Value = employeeIDField.Text;
            controllingTeamNumberParameter.Value = teamsDropDownField.Text;
            startDateParameter.Value = startDateField.Text;
            statusTypeParameter.Value = statusTypeField.Text;


            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(budgetNumberParameter);
            cmd.Parameters.Add(controllingDeparmentNumberParameter);
            cmd.Parameters.Add(eidNumberParameter);
            cmd.Parameters.Add(controllingTeamNumberParameter);
            cmd.Parameters.Add(startDateParameter);
            cmd.Parameters.Add(statusTypeParameter);
            
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            finalLabel.Text = (nameParameter.Value + " has been created!");
            //outputLabel.Text += "Entry has been recorded.";


        }
    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    //Insert a -Select- item, used to determine if user tries to submit page without proper team selected
    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {
        controllingDepartmentField.Items.Insert(0, new ListItem("-Select-", String.Empty));
        controllingDepartmentField.SelectedIndex = 0; ;
    }

    protected void deparmentsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    //Insert a -Select- item, used to determine if user tries to submit page without proper team selected
    protected void SqlDataSouce2_DataBound(object sender, EventArgs e)
    {
        teamsDropDownField.Items.Insert(0, new ListItem("-Select-", String.Empty));
        teamsDropDownField.SelectedIndex = 0; ;
    }

    //for projectsDropDown
    protected void teamsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void statusTypeField_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void employeeIDField_TextChanged1(object sender, EventArgs e)
    {
        //employeeIDField.
    }
}