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


public partial class UpdateTeam : System.Web.UI.Page
{

    private static String[] allowedRoles = { "DEPARTMENT_LEAD", "ADMIN" };
    private static String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

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

        teamAlert.Visible = false;
        projectAlert.Visible = false;
        teamLeadAlert.Visible = false;

        teamAlertLabel.Text = "";
        projectAlertLabel.Text = "";
        teamLeadAlertLabel.Text = "";

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

    private bool CheckRole()
    {
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
    

    //for TeamsDropDown, when they select the team, auto select the current values
    //of that record into the other dropdown as default, if no change is made, the update will use the
    //original value for that attribute
    protected void teamsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        int projectID = autoSelectCurrentProject();

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT NAME FROM PROJECTS WHERE PROJECT_ID = @projectID";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter ProjectIDParameter = new SqlParameter("@projectID", SqlDbType.Int);
            ProjectIDParameter.Value = projectID;
            cmd.Parameters.Add(ProjectIDParameter);
            cmd.Prepare();

            string projectName;
            if (projectID != 0)
            {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    projectName = (string)rdr["NAME"];
                }
            }
            else
            {
                projectName = "None";
            }


            
            projectsDropDown.SelectedIndex =
                projectsDropDown.Items.IndexOf(projectsDropDown.Items.FindByText(projectName));
        }
    }
    //Find the project number that team is working on
    protected int autoSelectCurrentProject()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT PROJECT_ID FROM TEAMS WHERE TEAM_ID = @TEAMID";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter teamIDParameter = new SqlParameter("@TEAMID", SqlDbType.Int);
            teamIDParameter.Value = int.Parse(teamsDropDown.SelectedValue);
            cmd.Parameters.Add(teamIDParameter);
            cmd.Prepare();


            int projectID;
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                rdr.Read();
                if (!rdr.IsDBNull(rdr.GetOrdinal("PROJECT_ID")))
                {
                    projectID = (int)rdr["PROJECT_ID"];
                }
                else
                {
                    projectID = 0;
                }
                con.Close();
                return projectID;
            }
        }
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    //Insert a -Select- item, used to determine if user tries to submit page without proper team selected
    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {
        teamsDropDown.Items.Insert(0, new ListItem("Teams: ", String.Empty));
        teamsDropDown.SelectedIndex = 0; ;
    }

    //for projectsDropDown
    protected void projectsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    //Create a none selection because projects attribute can be null
    protected void SqlDataSouce2_DataBound(object sender, EventArgs e)
    {
        projectsDropDown.Items.Insert(0, new ListItem("None", "0"));
        projectsDropDown.SelectedIndex = 0; ;
    }


    /// <summary>
    /// checks if inputted TEAMLEAD_ID is valid, and also a team to update is selected
    /// if teamLeadIDTextBox is left empty, assume there is no update to that attribute and 
    /// use the original value on update, and promote
    /// new employee to lead
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        teamAlert.Visible = false;
        projectAlert.Visible = false;
        teamLeadAlert.Visible = false;

        teamAlertLabel.Text = "";
        projectAlertLabel.Text = "";
        teamLeadAlertLabel.Text = "";

        if (checkIfValidEmployeeID() && !string.IsNullOrEmpty(teamsDropDown.SelectedValue))
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "UPDATE TEAMS SET PROJECT_ID = @ProjectID, TEAMLEAD_ID = @TeamLeadID WHERE TEAM_ID = @TeamID";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter ProjectIDParameter = new SqlParameter("@ProjectID", SqlDbType.Int);
                SqlParameter TeamIDParameter = new SqlParameter("@TeamID", SqlDbType.Int);
                SqlParameter TeamLeadIDParameter = new SqlParameter("@TeamLeadID", SqlDbType.Int);
                ///If field is left empty, use the original value
                if (string.IsNullOrEmpty(teamLeadIDTextBox.Text))
                {
                    initialValue(TeamLeadIDParameter);
                }
                ///else set the parameter to input value, this means there will be a new team lead
                ///and cause the previous team lead to demote back to developer and inputted employee to promote
                ///to a team lead
                else
                {
                    TeamLeadIDParameter.Value = int.Parse(teamLeadIDTextBox.Text);
                    //demotePreviousTeamLead();
                    assignTeamLead();
                }
                TeamIDParameter.Value = int.Parse(teamsDropDown.SelectedValue);
                assignValue(int.Parse(projectsDropDown.SelectedValue), ProjectIDParameter);                

                cmd.Parameters.Add(TeamLeadIDParameter);
                cmd.Parameters.Add(TeamIDParameter);
                cmd.Parameters.Add(ProjectIDParameter);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Label4.Text = "Team Updated!";
            }
        }

        else if (string.IsNullOrEmpty(teamsDropDown.SelectedValue))
        {
            teamAlertLabel.Text += "Select a team to update";
            teamAlert.Visible = true;
        }
        else
        {
            teamLeadAlertLabel.Text += "Invalid Employee ID";
            teamLeadAlert.Visible = true;
        }
    }

    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Checks the teamLeadIDTextBox for either an updated valid employee, if empty then no update, or invalid employee
    /// </summary>
    /// <returns true on valid employee or no update></returns>
    protected bool checkIfValidEmployeeID()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT U.EMPLOYEE_ID FROM USERS AS U WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Int);
            if (string.IsNullOrEmpty(teamLeadIDTextBox.Text))
            {
                return true;
            }
            eidParameter.Value = int.Parse(teamLeadIDTextBox.Text);
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
    /// <summary>
    /// Finds the original value of TEAMLEAD_ID from the project
    /// </summary>
    /// <returns value of TEAMLEAD_ID></returns>
    protected void initialValue(SqlParameter parameter)
    {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String queryStatement = "SELECT TEAMLEAD_ID FROM TEAMS WHERE TEAM_ID = @TEAMID";
                con.Open();
                SqlCommand cmd = new SqlCommand(queryStatement, con);
                SqlParameter teamIDParameter = new SqlParameter("@TEAMID", SqlDbType.Int);
                teamIDParameter.Value = int.Parse(teamsDropDown.SelectedValue);
                cmd.Parameters.Add(teamIDParameter);
                cmd.Prepare();
                int TeamLeadIDInitial;
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    rdr.Read();
                    TeamLeadIDInitial = (int)rdr["TEAMLEAD_ID"];
                    parameter.Value = TeamLeadIDInitial;
                    
                }
            }
        
    }

    protected void assignTeamLead()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "UPDATE USERS SET ROLE = 6 WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Int);
            eidParameter.Value = int.Parse(teamLeadIDTextBox.Text);
            cmd.Parameters.Add(eidParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }

    protected void demotePreviousTeamLead()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "UPDATE USERS SET ROLE = 1 WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Int);
            initialValue(eidParameter);
            cmd.Parameters.Add(eidParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}


