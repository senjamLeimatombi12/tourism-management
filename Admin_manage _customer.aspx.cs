using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class Admin_manage__customer : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrid();
            }
        }

        void LoadGrid(string filter = "")
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = "SELECT * FROM admin_access_member";
                if (!string.IsNullOrEmpty(filter))
                {
                    query += " WHERE Name LIKE @search";
                }

                SqlDataAdapter da = new SqlDataAdapter(query, con);
                if (!string.IsNullOrEmpty(filter))
                    da.SelectCommand.Parameters.AddWithValue("@search", "%" + filter + "%");

                DataTable dt = new DataTable();
                da.Fill(dt);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                try
                {
                    con.Open(); // ❗ Always open the connection before using it

                    // Check if ID already exists
                    SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM admin_access_member WHERE Id = @Id", con);
                    cmd1.Parameters.AddWithValue("@Id", int.Parse(txtID.Text));
                    int count = (int)cmd1.ExecuteScalar();

                    if (count > 0)
                    {
                        // ID already exists — show alert
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Try with different ID for different place or festivals.');", true);
                        return;
                    }

                    // Insert new record
                    SqlCommand cmd = new SqlCommand("INSERT INTO admin_access_member (Id, Name) VALUES (@Id, @Name)", con);
                    cmd.Parameters.AddWithValue("@Id", int.Parse(txtID.Text));
                    cmd.Parameters.AddWithValue("@Name", txtName.Text);
                    cmd.ExecuteNonQuery();

                    LoadGrid(); // Reload data
                    ClearForm(); // Clear input fields
                }
                catch (Exception ex)
                {
                    // Show real error in alert
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error: {ex.Message.Replace("'", "\\'")}');", true);
                }

                finally
                {
                    if (con.State == ConnectionState.Open)
                        con.Close();
                }
            }
        }



        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("UPDATE admin_access_member SET Name=@Name WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(txtID.Text));
                cmd.Parameters.AddWithValue("@Name", txtName.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadGrid();
                ClearForm(); 
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM admin_access_member WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", int.Parse(txtID.Text));
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadGrid();
                ClearForm(); 
            }
        }

        protected void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadGrid(txtSearch.Text);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            txtID.Text = row.Cells[0].Text;
            txtName.Text = row.Cells[1].Text;
        }

        protected void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        void ClearForm()
        {
            txtID.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSearch.Text = string.Empty;

            // If you use DropDownLists, CheckBoxes, etc., reset them like this:
            // myDropDown.SelectedIndex = -1;
            // myCheckBox.Checked = false;
        }

    }
}