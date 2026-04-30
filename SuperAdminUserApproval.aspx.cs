using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class SuperAdminUserApproval : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUpdateRequests();
            }
        }

        private void LoadUpdateRequests()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM UserUpdateRequestsByAdmin WHERE Status = 'Pending' ORDER BY RequestDate DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvRequests.DataSource = dt;
                gvRequests.DataBind();
            }
        }

        protected void gvRequests_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Approve")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvRequests.Rows[index];
                string userId = row.Cells[0].Text;
                string newName = row.Cells[1].Text;
                string newEmail = row.Cells[2].Text;
                string newPhone = row.Cells[3].Text;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE Users SET Name = @Name, Email = @Email, Phone = @Phone, EditStatus = 'Approved' WHERE UserID = @UserID", conn);
                    cmd.Parameters.AddWithValue("@Name", newName);
                    cmd.Parameters.AddWithValue("@Email", newEmail);
                    cmd.Parameters.AddWithValue("@Phone", newPhone);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("UPDATE UserUpdateRequestsByAdmin SET Status = 'Approved' WHERE UserID = @UserID", conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    cmd.ExecuteNonQuery();
                }

                LoadUpdateRequests();
            }
        }
    }
}