using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// Summary description for LoginValidator
/// </summary>
public class LoginValidator
{

    public static String ValidateUserCredentials(String password, String email) {
        String connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

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

    public static bool ValidateSession(String employeeID, String GUID) {
        String connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["PROJECT_MANAGMENTConnectionString"].ConnectionString;

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


}