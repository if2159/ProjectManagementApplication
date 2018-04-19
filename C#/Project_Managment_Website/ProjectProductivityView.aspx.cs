using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class ProjectProductivityView : System.Web.UI.Page
{
    private static String[] allowedRoles = { "ADMIN", "PROJECT_LEAD" };
    private static String connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;
    private static bool doOnce = true;

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
        else if (!CheckRole()) {
            Response.Redirect("AccessForbidden.aspx");
        }
        else {
            //tableLabel.Text += "TEST!";
           // tableLabel.Text = projectDropDown.SelectedIndex + "";
            if (projectDropDown.SelectedIndex < 0) {
                doOnce = false;
                fillDropDown();

            }

            // 
        }
    }

    private bool CheckRole()
    {
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


    private void fillDropDown() {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];

            con.Open();

            String submitStatement =
                "SELECT NAME, PROJECT_ID FROM PROJECTS AS P WHERE " +
                "P.EID = @EID ORDER BY [NAME]";
            SqlCommand cmd = new SqlCommand(submitStatement, con);

            SqlParameter eidParameter = new SqlParameter("@EID", SqlDbType.Decimal);
            eidParameter.Scale = 0;
            eidParameter.Precision = 10;
            eidParameter.Value = Decimal.Parse(employeeID);

            cmd.Parameters.Add(eidParameter);

            ArrayList pids = new ArrayList();
            ArrayList names = new ArrayList();
            cmd.Prepare();
            using (SqlDataReader rdr = cmd.ExecuteReader()) //get the employee's ID
            {
                while (rdr.Read()) {
                    names.Add(rdr.GetString(0));
                    pids.Add(rdr.GetInt32(1).ToString());
                }
            }
            for (int i = 0; i < pids.Count; i++) {
                projectDropDown.Items.Insert(i, new ListItem(names[i] + "", pids[i] + ""));
            }
        }
    }
    private void generateReport() {

        hoursWorkedByTeamDateLabel.Text = getHoursWorked();
        hoursWorkedByTeamDateLabel.Visible = true;
        Label6.Visible = true;
        costOfTeamDateLabel.Text = getCombinedCost();
        costOfTeamDateLabel.Visible = true;
        Label7.Visible = true;
       // createTable(getTeamProductivity());
    }

    private void createTable(ArrayList arrayList) {
        tableLabel.Text = "";
        /*<table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>*/
        String tableText = "<table style=\"width: 100 %; \">";
        tableText += "<tr>";
        tableText += "<td>1</td>";
        tableText += "<td>2</td>";
        tableText += "<td>3</td>";
        tableText += "</tr>";

        tableText += "</table>";
        tableLabel.Text = tableText;

    }

    private ArrayList getTeamProductivity() {
        return null;
    }

    private string getCombinedCost() {
        var totalCost = 0.0;
        if (verifyDates()) {

            using (SqlConnection con = new SqlConnection(connectionString)) {
            String employeeID = Request.Cookies["UserID"].Value.Split('=')[1];
            string projectID = projectDropDown.SelectedItem.Value;
            string startDateText = startDateField.Text;
            string endDateText = endDateField.Text;
                DateTime startDate = DateTime.ParseExact(startDateText, "MM/dd/yyyy", null);
                DateTime endDate = DateTime.ParseExact(endDateText, "MM/dd/yyyy", null);
                // Part to SqlDateTime then            
                System.Data.SqlTypes.SqlDateTime startdtSql =
                    System.Data.SqlTypes.SqlDateTime.Parse(startDate.ToString("yyyy/MM/dd"));
                System.Data.SqlTypes.SqlDateTime enddtSql =
                    System.Data.SqlTypes.SqlDateTime.Parse(endDate.ToString("yyyy/MM/dd"));

                con.Open();

                String query = "SELECT SUM(T.HOURS_LOGGED * E.HOURLY_WAGE) FROM TIMES AS T, EMPLOYEES AS E " +
                               "WHERE T.PROJECT_ID = @PID " +
                               "AND T.ENTRY_DATE >= @START_DATE " +
                               "AND T.ENTRY_DATE <= @END_DATE " +
                               "AND T.EID = E.EMPLOYEE_ID";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlParameter pidParameter = new SqlParameter("@PID", SqlDbType.Int);
                SqlParameter startDateParameter = new SqlParameter("@START_DATE", SqlDbType.DateTime);
                SqlParameter endDateParameter = new SqlParameter("@END_DATE", SqlDbType.DateTime);
                pidParameter.Value = projectID;
                startDateParameter.Value = startdtSql;
                endDateParameter.Value = enddtSql;
                cmd.Parameters.Add(pidParameter);
                cmd.Parameters.Add(startDateParameter);
                cmd.Parameters.Add(endDateParameter);

                cmd.Prepare();
                using (SqlDataReader rdr = cmd.ExecuteReader()) //get the employee's ID
                {

                    while (rdr.Read()) {

                        if (!rdr.IsDBNull(0)) {
                            totalCost = Convert.ToDouble(rdr[0]);
                        }
                        else {
                            totalCost = 0.0;
                        }
                    }
                }
            }
        }
        else {
            tableLabel.Text = "Incorrect Date Format.";
        }


        return String.Format("{0:0.0#}", totalCost);

    }

    private string getHoursWorked() {
       // tableLabel.Text = projectDropDown.SelectedItem.Value;
        string projectID = projectDropDown.SelectedItem.Value;
        string startDateText = startDateField.Text;
        string endDateText = endDateField.Text;
        double sumHours = 0.0;

        if (startDateText.Length > 0 && endDateText.Length > 0 && verifyDates())
        {
            DateTime startDate = DateTime.ParseExact(startDateText, "MM/dd/yyyy", null);
            DateTime endDate = DateTime.ParseExact(endDateText, "MM/dd/yyyy", null);
            // Part to SqlDateTime then            
            System.Data.SqlTypes.SqlDateTime startdtSql = System.Data.SqlTypes.SqlDateTime.Parse(startDate.ToString("yyyy/MM/dd"));
            System.Data.SqlTypes.SqlDateTime enddtSql = System.Data.SqlTypes.SqlDateTime.Parse(endDate.ToString("yyyy/MM/dd"));

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                String query = "SELECT HOURS_LOGGED FROM TIMES AS T " +
                               "WHERE T.PROJECT_ID = @PID " +
                               "AND T.ENTRY_DATE >= @START_DATE " +
                               "AND T.ENTRY_DATE <= @END_DATE";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlParameter pidParameter = new SqlParameter("@PID", SqlDbType.Int);
                SqlParameter startDateParameter = new SqlParameter("@START_DATE", SqlDbType.DateTime);
                SqlParameter endDateParameter = new SqlParameter("@END_DATE", SqlDbType.DateTime);
                pidParameter.Value = projectID;
                startDateParameter.Value = startdtSql;
                endDateParameter.Value = enddtSql;
                cmd.Parameters.Add(pidParameter);
                cmd.Parameters.Add(startDateParameter);
                cmd.Parameters.Add(endDateParameter);

                cmd.Prepare();

                using (SqlDataReader rdr = cmd.ExecuteReader()) //get the employee's ID
                {

                    while (rdr.Read())
                    {
                        sumHours += rdr.GetDouble(0);

                    }
                }


            }

        }
        else {
            tableLabel.Text = "Incorrect Date Format.";
        }

        return String.Format("{0:0.0#}", sumHours);

    }

    private bool verifyDates() {
        string startDateText = startDateField.Text;
        string endDateText = endDateField.Text;
        try {
            DateTime startDate = DateTime.ParseExact(startDateText, "MM/dd/yyyy", null);
            DateTime endDate = DateTime.ParseExact(endDateText, "MM/dd/yyyy", null);
            // Part to SqlDateTime then            
            System.Data.SqlTypes.SqlDateTime startdtSql =
                System.Data.SqlTypes.SqlDateTime.Parse(startDate.ToString("yyyy/MM/dd"));
            System.Data.SqlTypes.SqlDateTime enddtSql =
                System.Data.SqlTypes.SqlDateTime.Parse(endDate.ToString("yyyy/MM/dd"));
            return true;
        }
        catch {
            return false;
        }
    }


    protected void sumbitButton_Click(object sender, EventArgs e)
    {
        
        generateReport();


    }



}