using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateEmployees : System.Web.UI.Page
{
    String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!AuthenticateSession())
        {
            Response.Redirect("Login.aspx");
        }
    }


    protected void statusDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    protected void Button1_Click(object sender, EventArgs e)
    {

        if (checkIfValidEmployeeID())
        {
            if (int.Parse(teamsDropDown.SelectedValue) == -1)
            {
                FinalLabel.Text = "Please select a Team";
            }
            else
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    String queryStatement = "UPDATE EMPLOYEES SET TEAM_ID = @TeamID WHERE EMPLOYEE_ID = @EMPLOYEE_ID";
                    con.Open();
                    SqlCommand cmd = new SqlCommand(queryStatement, con);
                    SqlParameter EmployeeIDParameter = new SqlParameter("@EMPLOYEE_ID", SqlDbType.Int);
                    SqlParameter TEAMIDParameter = new SqlParameter("@TeamID", SqlDbType.Int);
                    

                    EmployeeIDParameter.Value = int.Parse(employeeIDField.Text);
                    TEAMIDParameter.Value = int.Parse(teamsDropDown.SelectedValue);


                    cmd.Parameters.Add(EmployeeIDParameter);
                    cmd.Parameters.Add(TEAMIDParameter);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                    FinalLabel.Text = ("Team has been updated!");
                }
            }
        }
        else
        {
            FinalLabel.Text = "This Employee ID does not exist";
        }


    }
    protected void SqlDataSouce2_DataBound(object sender, EventArgs e)
    {

        teamsDropDown.Items.Insert(0, new ListItem("Team to update:", "-1"));
        teamsDropDown.SelectedIndex = 0; ;
    }
    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
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
}
