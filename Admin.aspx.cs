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
    public partial class Admin : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e) { }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string email = txtDeleteEmail.Text.Trim();

          //  string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                // Delete from Customers
                SqlCommand deleteCmd = new SqlCommand("DELETE FROM Customers WHERE Email = @Email", conn);
                deleteCmd.Parameters.AddWithValue("@Email", email);
                deleteCmd.ExecuteNonQuery();

                // Set CanLogin = 0 in UserCredentials
                SqlCommand updateCmd = new SqlCommand("UPDATE UserCredentials SET CanLogin = 0 WHERE Email = @Email", conn);
                updateCmd.Parameters.AddWithValue("@Email", email);
                updateCmd.ExecuteNonQuery();

                conn.Close();
            }

            lblDeleteMessage.Text = "Customer deleted and login disabled.";
        }
    }
}