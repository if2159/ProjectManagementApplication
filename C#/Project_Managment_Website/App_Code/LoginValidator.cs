using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

/// <summary>
/// Summary description for LoginValidator
/// </summary>
public class LoginValidator
{

    private static String connectionString = System.Configuration.ConfigurationManager
        .ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;


    /// <summary>
    /// Loads the hashed password for supplied email.
    /// Then compares the hashes to validate that a user has the correct password/email combination.
    /// </summary>
    /// <param name="password">The password that the user supplies. This is hashed then compared to the stored hash.</param>
    /// <param name="email">The user's provided email. This is used to find the user's stored hash in the Database.</param>
    /// <returns>True if hashes match. False otherwise; e.g. user does not exist, passwords do not match.</returns>
    public static String ValidateUserCredentials(String password, String email) {
        

        using (SqlConnection con = new SqlConnection(connectionString)) {
            String queryStatement = "SELECT EMPLOYEE_ID, HASHED_PASSWORD FROM dbo.USERS WHERE EMAIL = @email";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter emailParameter = new SqlParameter("@email", SqlDbType.VarChar, 250);
            emailParameter.Value = email;
            cmd.Parameters.Add(emailParameter);
            cmd.Prepare();
            decimal employeeID = -1;
            String correctPassword = "";
            using (SqlDataReader rdr = cmd.ExecuteReader()) //get the employee's ID
            {
                if (!rdr.HasRows) {
                    return "";
                }

                while (rdr.Read()) {
                    employeeID = rdr.GetSqlDecimal(0).Value;
                    correctPassword = rdr.GetString(1);
                }
            }

            return (ValidatePassword(password, correctPassword))? employeeID+"": "";
        }

    }

    public const int SaltByteSize = 24;
    public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash 
    public const int Pbkdf2Iterations = 1000;
    public const int IterationIndex = 0;
    public const int SaltIndex = 1;
    public const int Pbkdf2Index = 2;

    /// <summary>
    /// Hashes and returns the supplied string. This includes the number of iterations, salt, and hash.
    /// </summary>
    /// <param name="password">The string to hash.</param>
    /// <returns>A string includes the number of iterations, salt, and hash for the provided password.</returns>
    public static string HashPassword(string password)
    {
        var cryptoProvider = new RNGCryptoServiceProvider();
        byte[] salt = new byte[SaltByteSize];
        cryptoProvider.GetBytes(salt);

        var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
        return Pbkdf2Iterations + ":" +
               Convert.ToBase64String(salt) + ":" +
               Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Compares a plain text password with the correct hash.
    /// </summary>
    /// <param name="password">Plain text password.</param>
    /// <param name="correctHash">Correct hash from the database.</param>
    /// <returns>True if the hashes match. False otherwise.</returns>
    public static bool ValidatePassword(string password, string correctHash)
    {
        char[] delimiter = { ':' };
        var split = correctHash.Split(delimiter);
        var iterations = Int32.Parse(split[IterationIndex]);
        var salt = Convert.FromBase64String(split[SaltIndex]);
        var hash = Convert.FromBase64String(split[Pbkdf2Index]);

        var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
        return SlowEquals(hash, testHash);
    }

    private static bool SlowEquals(byte[] a, byte[] b)
    {
        var diff = (uint)a.Length ^ (uint)b.Length;
        for (int i = 0; i < a.Length && i < b.Length; i++)
        {
            diff |= (uint)(a[i] ^ b[i]);
        }
        return diff == 0;
    }

    private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
    {
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
        pbkdf2.IterationCount = iterations;
        return pbkdf2.GetBytes(outputBytes);
    }

    /// <summary>
    /// Validates if a user has a valid stored session token.
    /// </summary>
    /// <param name="employeeID">The logged in user.</param>
    /// <param name="GUID">The token supplied by the user's browser.</param>
    /// <returns>True if the token is stored in the database for this user. False otherwise.</returns>
    public static bool ValidateSession(String employeeID, String GUID) {
        
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT SESSION_ID FROM dbo.SESSIONS WHERE EMPLOYEE_ID = @eid";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Decimal);
            eidParameter.Precision = 10;
            eidParameter.Scale = 0;
            eidParameter.Value = Decimal.Parse(employeeID);
            cmd.Parameters.Add(eidParameter);
            cmd.Prepare();
            ArrayList sessionIds = new ArrayList();
            using (SqlDataReader rdr = cmd.ExecuteReader()) //get the employee's ID
            {
                if (!rdr.HasRows)
                {
                    return false;
                }

                while (rdr.Read())
                {
                    sessionIds.Add(rdr.GetString(0));
                    
                }
            }

            if (sessionIds.Contains(GUID)) {
                return true;
            }

            return false;
        }
    }
    /// <summary>
    /// This method will verify that the employeeID has one of the roles in the requiredRoles List.
    /// </summary>
    /// <param name="employeeID">The user to get the role of.</param>
    /// <param name="requiredRoles">The roles that are allowed to access this page.</param>
    /// <returns>True if the user's role is in requiredRoles otherwise false.</returns>
    public static bool ValidatorUserRole(String employeeID, ArrayList requiredRoles) {

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            String queryStatement = "SELECT ROLE_DESCRIPTION FROM dbo.USER_ROLES AS UR, dbo.USERS AS U WHERE U.EMPLOYEE_ID = @eid AND U.ROLE = UR.ROLE_ID";
            con.Open();
            SqlCommand cmd = new SqlCommand(queryStatement, con);
            SqlParameter eidParameter = new SqlParameter("@eid", SqlDbType.Decimal);
            eidParameter.Precision = 10;
            eidParameter.Scale = 0;
            eidParameter.Value = Decimal.Parse(employeeID);
            cmd.Parameters.Add(eidParameter);
            cmd.Prepare();
            String usersRole = "Guest";
            using (SqlDataReader rdr = cmd.ExecuteReader()) 
            {
                if (!rdr.HasRows)
                {
                    return false;
                }

                while (rdr.Read())
                {
                    usersRole = rdr.GetString(0);

                }
            }

            if (requiredRoles.Contains(usersRole))
            {
                return true;
            }

            return false;
        }
    }


}