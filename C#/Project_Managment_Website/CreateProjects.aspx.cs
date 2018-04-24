using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateProjects : System.Web.UI.Page
{
    /// <summary>
    /// This method is called on page load. It will validate a user has a valid session 
    /// and redirect them to the login page if it is not valid.
    /// </summary>B
    /// <param name="sender"></param>
    /// <param name="e"></param>
    String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }
        startDateField.Text = DateTime.Now.ToShortDateString();

        projectAlert.Visible = false;
        budgetAlert.Visible = false;
        departmentAlert.Visible = false;
        employeeAlert.Visible = false;
        teamAlert.Visible = false;
        startAlert.Visible = false;
        statusAlert.Visible = false;

        projectAlertLabel.Text = "";
        budgetAlertLabel.Text = "";
        departmentAlertLabel.Text = "";
        employeeAlertLabel.Text = "";
        teamAlertLabel.Text = "";
        startAlertLabel.Text = "";
        statusAlertLabel.Text = "";

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


    protected void submitButton_Click(object sender, EventArgs e)
    {
        projectAlert.Visible = false;
        budgetAlert.Visible = false;
        departmentAlert.Visible = false;
        employeeAlert.Visible = false;
        teamAlert.Visible = false;
        startAlert.Visible = false;
        statusAlert.Visible = false;

        projectAlertLabel.Text = "";
        budgetAlertLabel.Text = "";
        departmentAlertLabel.Text = "";
        employeeAlertLabel.Text = "";
        teamAlertLabel.Text = "";
        startAlertLabel.Text = "";
        statusAlertLabel.Text = "";

        int alert_count = 0;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            if (int.Parse(controllingDepartmentField.SelectedValue) == -1)
            {
                departmentAlertLabel.Text = "Please select a Department";
                departmentAlert.Visible = true;
                alert_count++;
            }
            if (int.Parse(statusTypeField.SelectedValue) == -1)
            {
                statusAlertLabel.Text = "Please select a the status of Project";
                statusAlert.Visible = true;
                alert_count++;
            }
            if (int.Parse(teamsDropDownField.SelectedValue) == -1)
            {
                teamAlertLabel.Text = "Please select a Team";
                teamAlert.Visible = true;
                alert_count++;
            }

            if(alert_count > 0)
            {
                return;
            }

            if (checkIfValidEmployeeID())
            {
                String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

                con.Open();
                //Submit the hours for the employee
                String submitStatement =
                    "INSERT INTO PROJECTS (NAME, BUDGET,CONTROLLING_DEPARTMENT,EID,CONTROLLING_TEAM,START_DATE,STATUS_TYPE)" +
                    "VALUES (@NAME, @BUDGET,@CONTROLLING_DEPARTMENT,@EID,@CONTROLLING_TEAM,@START_DATE,@STATUS_TYPE)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter nameParameter = new SqlParameter("@NAME", SqlDbType.VarChar, 70);
                SqlParameter budgetNumberParameter = new SqlParameter("@BUDGET", SqlDbType.Int);
                SqlParameter controllingDeparmentNumberParameter =
                    new SqlParameter("@CONTROLLING_DEPARTMENT", SqlDbType.Int);
                SqlParameter eidNumberParameter = new SqlParameter("@EID", SqlDbType.Int);
                SqlParameter controllingTeamNumberParameter =
                    new SqlParameter("@CONTROLLING_TEAM", SqlDbType.Int);
                SqlParameter startDateParameter = new SqlParameter("@START_DATE", SqlDbType.DateTime);
                SqlParameter statusTypeParameter = new SqlParameter("@STATUS_TYPE", SqlDbType.Int);

                nameParameter.Value = projectNameField.Text;
                budgetNumberParameter.Value = int.Parse(budgetField.Text);
                controllingDeparmentNumberParameter.Value = int.Parse(controllingDepartmentField.Text);
                eidNumberParameter.Value = int.Parse(employeeIDField.Text);
                assignValue(int.Parse(teamsDropDownField.SelectedValue), controllingTeamNumberParameter);
                startDateParameter.Value = DateTime.Parse(startDateField.Text);
                statusTypeParameter.Value = int.Parse(statusTypeField.Text);


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
            else
            {
                employeeAlertLabel.Text = "The Employee Id does not exist";
                employeeAlert.Visible = true;
                alert_count++;
            }
        }
    }

    //protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}

    //Insert a -Select- item, used to determine if user tries to submit page without proper team selected
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {
        controllingDepartmentField.Items.Insert(0, new ListItem("Controlling Department:", "-1"));
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
        teamsDropDownField.Items.Add(new ListItem("None", "0"));
        teamsDropDownField.Items.Insert(0, new ListItem("Team:", "-1"));
        teamsDropDownField.SelectedIndex = 0; ;
    }

    //for projectsDropDown
    protected void teamsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void SqlDataSource3_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    protected void SqlDataSource4_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {
        
    }
    protected void SqlDataSouce4_DataBound(object sender, EventArgs e)
    {
        statusTypeField.Items.Insert(0, new ListItem("Status:", "-1"));
        statusTypeField.SelectedIndex = 0;
    }


    protected void statusTypeField_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void employeeIDField_TextChanged1(object sender, EventArgs e)
    {
        
    }
    protected bool checkIfValidEmployeeID()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT U.EMPLOYEE_ID FROM USERS AS U WHERE EMPLOYEE_ID = @eid";
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
    protected void assignValue(int valueSelected, SqlParameter parameter)
    {
        if (valueSelected == 0)
        {
            parameter.Value = DBNull.Value;
        }
        else { parameter.Value = valueSelected; }
    }
}
