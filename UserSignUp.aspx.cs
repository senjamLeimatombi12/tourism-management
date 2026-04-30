using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class UserSignUp : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;




            protected void Page_Load(object sender, EventArgs e)
            {
            }

            protected void Button1_Click(object sender, EventArgs e)
            {
                if (checkMemberExists())
                {
                    Response.Write("<script>alert('Username already exists. Try a different one.');</script>");
                }
                else
                {
                    signUpNewMember();
                }
            }

            // Check if username already exists
            bool checkMemberExists()
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(strcon))
                    {
                        con.Open();
                        string query = "SELECT COUNT(*) FROM Users WHERE username = @username";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@username", TextBox9.Text.Trim());

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    return false;
                }
            }

            // Register new member
            void signUpNewMember()
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();

                    try
                    {
                        // Insert into Users
                        string query = @"
                        INSERT INTO Users
                        (Name, DOB, Phone, Email, State, Country, Pincode, Address, PasswordHash, 
                         IDType, GovtID, PassportNumber, Nationality, Today_Date, username) 
                        VALUES 
                        (@Name, @dob, @Phone, @Email, @State, @Country, @Pincode, @Address, @Password, 
                         @IDType, @GovtID, @PassportNumber, @Nationality, GETDATE(), @username)";

                        SqlCommand cmd = new SqlCommand(query, con, transaction);

                        // Required fields
                        cmd.Parameters.AddWithValue("@Name", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", DateTime.Parse(TextBox3.Text.Trim()));
                        cmd.Parameters.AddWithValue("@Phone", TextBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", TextBox4.Text.Trim());
                        cmd.Parameters.AddWithValue("@State", TextBox5.Text.Trim());
                        cmd.Parameters.AddWithValue("@Country", TextBox6.Text.Trim());
                        cmd.Parameters.AddWithValue("@Pincode", TextBox7.Text.Trim());
                        cmd.Parameters.AddWithValue("@Address", TextBox8.Text.Trim());
                        cmd.Parameters.AddWithValue("@username", TextBox9.Text.Trim());

                        // Hash password
                        string hashedPassword = HashPassword(TextBox10.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", hashedPassword);

                        // Nationality + ID
                        string nationality = ddlNationality.SelectedValue;
                        string idType = ddlGovtIDType.SelectedValue;
                        string idNumber = txtGovtID.Text.Trim();

                        cmd.Parameters.AddWithValue("@IDType", idType);
                        cmd.Parameters.AddWithValue("@Nationality", nationality);

                        if (nationality == "Indian")
                        {
                            cmd.Parameters.AddWithValue("@GovtID", idNumber);
                            cmd.Parameters.AddWithValue("@PassportNumber", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@GovtID", DBNull.Value);
                            cmd.Parameters.AddWithValue("@PassportNumber", idNumber);
                        }

                        cmd.ExecuteNonQuery();

                        // Insert into UserCredentials
                        SqlCommand insertCredentials = new SqlCommand(
                            "INSERT INTO UserCredentials (Email, Password, CanLogin) VALUES (@Email, @Password, 1)",
                            con, transaction);

                        insertCredentials.Parameters.AddWithValue("@Email", TextBox4.Text.Trim());
                        insertCredentials.Parameters.AddWithValue("@Password", hashedPassword);
                        insertCredentials.ExecuteNonQuery();

                        // Commit if both succeed
                        transaction.Commit();

                        Response.Write("<script>alert('Sign Up Successful! Please log in.');</script>");
                        Response.Redirect("Login.aspx", false);
                        Context.ApplicationInstance.CompleteRequest(); // prevents ThreadAbortException
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }

            // Password hashing (SHA256)
            string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    foreach (byte b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
    } 
