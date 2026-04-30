using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Tourism_Management
{
    public partial class Signup : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

           // string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // 1. Add to Customers table
                    SqlCommand insertCustomer = new SqlCommand(
                        "INSERT INTO Customers (Name, Email) VALUES (@Name, @Email)", conn, transaction);
                    insertCustomer.Parameters.AddWithValue("@Name", name);
                    insertCustomer.Parameters.AddWithValue("@Email", email);
                    insertCustomer.ExecuteNonQuery();

                    // 2. Add to UserCredentials with CanLogin = 1
                    SqlCommand insertCredentials = new SqlCommand(
                        "INSERT INTO UserCredentials (Email, Password, CanLogin) VALUES (@Email, @Password, 1)", conn, transaction);
                    insertCredentials.Parameters.AddWithValue("@Email", email);
                    insertCredentials.Parameters.AddWithValue("@Password", password);
                    insertCredentials.ExecuteNonQuery();

                    transaction.Commit();
                    lblMessage.Text = "Signup successful. You can now login.";
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Error: " + ex.Message;
                }

                conn.Close();
            }
        }
    }
}