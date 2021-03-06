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

/// <summary>
/// 2 drop downs, one textBox. Have a -Select- value for all dropDowns, value set to -1
/// checkIfValidEmployeeID() checks if the submitted employeeID is a record in EMPLOYEE
/// assignValue() assigns DBNULL or the value selected to the sqlParameter
/// assignTeamLead() promote the employee to teamLead
/// SqlDataSouce1_DataBound() adds the option of NULL which has a value 0, into the dropdown for projectID
/// </summary>

public partial class CreateTeam : System.Web.UI.Page
{

    private static String[] allowedRoles = {"ADMIN", "DEPARTMENT_LEAD" };
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

        projectIDAlert.Visible = false;
        departmentNameAlert.Visible = false;
        teamLeadAlert.Visible = false;

        projectIDAlertLabel.Text = "";
        departmentNameAlertLabel.Text = "";
        teamLeadAlertLabel.Text = "";
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


    protected void Button1_Click(object sender, EventArgs e)
    {
        String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
        int alert_counter = 0;
        projectIDAlert.Visible = false;
        departmentNameAlert.Visible = false;
        teamLeadAlert.Visible = false;

        projectIDAlertLabel.Text = "";
        departmentNameAlertLabel.Text = "";
        teamLeadAlertLabel.Text = "";

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            if (int.Parse(projectDropDown.SelectedValue) == -1)
            {
                projectIDAlert.Visible = true;
                projectIDAlertLabel.Text = "Please select a project if there is one";
                alert_counter++;
            }
            if(int.Parse(departmentDropDown.SelectedValue) == -1)
            {
                departmentNameAlertLabel.Text = "Please select a department";
                departmentNameAlert.Visible = true;
                alert_counter++;
            }

            if(alert_counter > 0)
            {
                return;
            }
            //DO some function call to make sure the entered employeeID is valid
            if(checkIfValidEmployeeID())
            {
                assignTeamLead();
                con.Open();
                String submitStatement =
                             "INSERT INTO TEAMS(PROJECT_ID, DEPARTMENT_ID, TEAMLEAD_ID)" +
                             "VALUES(@PROJECT_ID, @DEPARTMENT_ID, @TEAMLEAD_ID)";
                SqlCommand cmd = new SqlCommand(submitStatement, con);
                SqlParameter ProjectIDParameter = new SqlParameter("@PROJECT_ID", SqlDbType.Int);
                SqlParameter DepartmentIDParameter = new SqlParameter("@DEPARTMENT_ID", SqlDbType.Int);
                SqlParameter TeamLeadIDParameter = new SqlParameter("@TEAMLEAD_ID", SqlDbType.Int);

                assignValue(int.Parse(projectDropDown.SelectedValue), ProjectIDParameter);
                DepartmentIDParameter.Value = int.Parse(departmentDropDown.SelectedValue);
                TeamLeadIDParameter.Value = int.Parse(teamLeadID.Text);



                cmd.Parameters.Add(ProjectIDParameter);
                cmd.Parameters.Add(DepartmentIDParameter);
                cmd.Parameters.Add(TeamLeadIDParameter);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                Label4.Text = "Team has been created!";
                con.Close();



            }
            else
            {
                teamLeadAlertLabel.Text = "Invalid employee ID";
                teamLeadAlert.Visible = true;
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

    protected bool checkIfValidEmployeeID()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT U.EMPLOYEE_ID FROM USERS AS U WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Int);
            if (string.IsNullOrEmpty(teamLeadID.Text))
            {
                    return false;
            }
            eidParameter.Value = int.Parse(teamLeadID.Text);
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

    protected void assignTeamLead()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "UPDATE USERS SET ROLE = 6 WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Int);
            eidParameter.Value = int.Parse(teamLeadID.Text);
            cmd.Parameters.Add(eidParameter);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            con.Close();
        }

    }

    //for ProjectDropDown{
    protected void projectDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    //for Department Dropdown
    protected void departmentDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SqlDataSource2_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
    //for teamLead Dropdown
    protected void teamLeadDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void SqlDataSource3_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }

    protected void SqlDataSouce1_DataBound(object sender, EventArgs e) {
        projectDropDown.Items.Add(new ListItem("None", "0"));
        projectDropDown.Items.Insert(0, new ListItem("-Select-", "-1"));
        projectDropDown.SelectedIndex = 0; ;
        
    }

    protected void SqlDataSouce2_DataBound(object sender, EventArgs e)
    {
        //projectDropDown.Items.Add(new ListItem("-Select-", "0"));
        departmentDropDown.Items.Insert(0, new ListItem("-Select-", "-1"));
        departmentDropDown.SelectedIndex = 0; ;
    }







    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
}


