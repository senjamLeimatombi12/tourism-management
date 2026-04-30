using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Tourism_Management
{
    public partial class AdminTicketingUserManagement : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUserGrid();
            }
        }

        // Load GridView with user data
        private void LoadUserGrid()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT RequestID, UserID, Name, Email, Phone, RequestDate, EditStatus FROM Users";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvUsers.DataSource = dt;
                gvUsers.DataBind();
            }
        }

        // Send user update request to Super Admin
        protected void btnSendUpdateRequest_Click(object sender, EventArgs e)
        {
            string userId = txtUserId.Text.Trim();
            string newName = txtNewName.Text.Trim();
            string newEmail = txtNewEmail.Text.Trim();
            string newPhone = txtNewPhone.Text.Trim();
            string message = txtMessage.Text.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"INSERT INTO UserUpdateRequestsByAdmin (UserID, NewName, NewEmail, NewPhone, Message, Status, RequestDate)
                                 VALUES (@UserID, @NewName, @NewEmail, @NewPhone, @Message, 'Pending', GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@NewName", newName);
                cmd.Parameters.AddWithValue("@NewEmail", newEmail);
                cmd.Parameters.AddWithValue("@NewPhone", newPhone);
                cmd.Parameters.AddWithValue("@Message", message);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // Optional: Clear fields
            txtUserId.Text = txtNewName.Text = txtNewEmail.Text = txtNewPhone.Text = txtMessage.Text = "";

            LoadUserGrid(); // Refresh grid
        }

        // GridView: Start editing
        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string editStatus = gvUsers.Rows[e.NewEditIndex].Cells[4].Text;

            if (editStatus == "Approved")
            {
                gvUsers.EditIndex = e.NewEditIndex;
                LoadUserGrid();
            }
            else
            {
                // Editing not allowed unless approved
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Edit not allowed until approved by Super Admin');", true);
            }
        }

        // GridView: Cancel editing
        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            LoadUserGrid();
        }

        // GridView: Update user data
        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvUsers.Rows[e.RowIndex];
            string userId = row.Cells[0].Text;

            string name = ((TextBox)row.Cells[1].Controls[0]).Text.Trim();
            string email = ((TextBox)row.Cells[2].Controls[0]).Text.Trim();
            string phone = ((TextBox)row.Cells[3].Controls[0]).Text.Trim();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"UPDATE Users SET Name = @Name, Email = @Email, Phone = @Phone, EditStatus = 'None'
                                 WHERE UserID = @UserID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Phone", phone);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            gvUsers.EditIndex = -1;
            LoadUserGrid();
        }

        protected void txtUserId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}