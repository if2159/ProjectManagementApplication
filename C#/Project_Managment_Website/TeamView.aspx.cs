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
using System.Drawing;

/// <summary>
/// Made to view hours worked
/// </summary>
public partial class TeamView : System.Web.UI.Page
{
    
    private static String[] allowedRoles = { "ADMIN", "TEAM_LEAD" };
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
    }

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


    /// <summary>
    /// Data is displayed through a select statement whose contents are loaded into a datatable
    /// </summary>
    protected void displayData()
    {        
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
        if (string.IsNullOrEmpty(teamsLeadingDropDown.SelectedValue))
        {
            Label3.Text = "Please select a team to view";
        }
        else if (string.IsNullOrEmpty(dateSelectedDropDown.SelectedValue))
        {
            Label3.Text = "Please select a date for project creation";
        }



        else
        {
            Label3.Text = "";            
            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                con1.Open();
                string submitStatement = "select PROJECTS.NAME AS 'PROJECT_NAME', SUM(TIMES.HOURS_LOGGED) AS 'HOURS_WORKED', EMPLOYEES.FIRST_NAME, EMPLOYEES.LAST_NAME " +
                                         "FROM TIMES, EMPLOYEES, TEAMS, PROJECTS " +
                                         "WHERE TIMES.EID = EMPLOYEES.EMPLOYEE_ID AND EMPLOYEES.TEAM_ID = @teamID AND TEAMS.TEAM_ID = EMPLOYEES.TEAM_ID AND PROJECTS.PROJECT_ID = TEAMS.PROJECT_ID AND PROJECTS.START_DATE BETWEEN @startDate AND @endDate " +
                                         "GROUP BY TIMES.EID, EMPLOYEES.FIRST_NAME, EMPLOYEES.LAST_NAME, EMPLOYEES.TEAM_ID, TEAMS.TEAMLEAD_ID, PROJECTS.NAME";
                SqlCommand cmd = new SqlCommand(submitStatement, con1);

                DateTime today = DateTime.Today;
                DateTime projectStart = DateTime.Today.AddDays(int.Parse(dateSelectedDropDown.SelectedValue));

                SqlParameter startDateParameter = new SqlParameter("@startDate", SqlDbType.DateTime);
                SqlParameter endDateParameter = new SqlParameter("@endDate", SqlDbType.DateTime);
                SqlParameter teamIDParameter = new SqlParameter("@teamID", SqlDbType.Int);

                teamIDParameter.Value = int.Parse(teamsLeadingDropDown.SelectedValue);
                startDateParameter.Value = projectStart;
                endDateParameter.Value = today;
                cmd.Parameters.Add(startDateParameter);
                cmd.Parameters.Add(endDateParameter);
                cmd.Parameters.Add(teamIDParameter);
                cmd.Prepare();

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        if (hoursWorkedByTeamOnProject(int.Parse(teamsLeadingDropDown.SelectedValue)) == 0)
                        {

                        }
                        else
                        {
                            Label5.Text = "Team's combined work time on project: " + hoursWorkedByTeamOnProject(int.Parse(teamsLeadingDropDown.SelectedValue)).ToString();
                        }
                        
                    }
                }
                cmd.ExecuteNonQuery();

                con1.Close();

                DataTable viewTable = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(viewTable);
                GridView1.DataSource = viewTable;
                GridView1.DataBind();

            }
        }

    }

    /// <summary>
    /// Find the hours worked on a project by a team as a whole
    /// </summary>
    /// <param name="teamID"></param>
    /// <returns></returns>
    protected int hoursWorkedByTeamOnProject(int teamID)
    {
        int hoursWorkedByTeam = 0;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "select sum(TIMES.HOURS_LOGGED) AS TOTALHOURS " +
                                    "from TIMES, EMPLOYEES " +
                                    "where TIMES.EID = EMPLOYEES.EMPLOYEE_ID AND EMPLOYEES.TEAM_ID = @teamID";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter teamIDParameter = new SqlParameter("@teamID", SqlDbType.Int);
            teamIDParameter.Value = teamID;
            cmd.Parameters.Add(teamIDParameter);
            cmd.Prepare();

            
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("TOTALHOURS")))
                    {
                        hoursWorkedByTeam = Convert.ToInt32(rdr["TOTALHOURS"]);
                    }
                    else
                    {

                    }
                }
            }
        }
        return hoursWorkedByTeam;
    }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void teamsDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// Load into the drop down the teams are the team_lead is in charge of
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void onload_teamsLeadingDropDown(object sender, EventArgs e)
    {
        String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT TEAM_ID FROM TEAMS WHERE TEAMLEAD_ID = @teamLeadID";
            con.Open();


            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter teamLeadIDParameter = new SqlParameter("@teamLeadID", SqlDbType.Int);
            teamLeadIDParameter.Value = int.Parse(employeeID);
            cmd.Parameters.Add(teamLeadIDParameter);
            cmd.Prepare();



            int teamID;
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal("TEAM_ID")))
                    {
                        teamID = (int)rdr["TEAM_ID"];

                        if (!Page.IsPostBack)
                        {
                            teamsLeadingDropDown.Items.Insert(0, new ListItem(teamID.ToString(), teamID.ToString()));
                        }
                    }
                    else
                    {

                    }
                }
            
            }
        }
        if (!Page.IsPostBack)
        {
            teamsLeadingDropDown.Items.Insert(0, new ListItem("-Select-", String.Empty));
            teamsLeadingDropDown.SelectedIndex = 0; ;
        }

        //Add items to date of project creation, basically datetime is calculated based on today
        //and we can view projects created within last 30 days, 90 days, all time
        if (!Page.IsPostBack)
        {
            
            dateSelectedDropDown.Items.Insert(0, new ListItem("Last 30 Days", "-30"));
            dateSelectedDropDown.Items.Insert(0, new ListItem("Last 90 Days", "-90"));
            dateSelectedDropDown.Items.Insert(0, new ListItem("All", "-999"));
            dateSelectedDropDown.Items.Insert(0, new ListItem("-Select-", ""));
            dateSelectedDropDown.SelectedIndex = 0; ;
        }

    }

    protected void SqlDataSouce1_DataBound(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Label5.Text = "";
        displayData();
    }

    protected void dateSelectedDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}