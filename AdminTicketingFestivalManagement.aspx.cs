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
    public partial class AdminTicketingFestivalManagement : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFestivalDropdown();
                LoadFestivalRequestsGrid();
            }
        }

        protected void btnSubmitFestival_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFestivalName.Text))
            {
                // Optionally show error message
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO FestivalRequests (RequestType, FestivalName, Message, Status, RequestDate) " +
                               "VALUES ('FestivalName', @FestivalName, @Message, 'Awaiting', GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FestivalName", txtFestivalName.Text.Trim());
                cmd.Parameters.AddWithValue("@Message", txtFestivalMessage.Text.Trim());

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            txtFestivalName.Text = "";
            txtFestivalMessage.Text = "";
            LoadFestivalRequestsGrid();
        }

        protected void btnSubmitCost_Click(object sender, EventArgs e)
        {
            if (ddlFestivalList.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtFestivalCost.Text))
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "INSERT INTO FestivalRequests (RequestType, FestivalName, RequestedCost, Message, Status, RequestDate) " +
                               "VALUES ('FestivalCost', @FestivalName, @Cost, @Message, 'Awaiting', GETDATE())";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FestivalName", ddlFestivalList.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@Cost", Convert.ToDecimal(txtFestivalCost.Text.Trim()));
                cmd.Parameters.AddWithValue("@Message", txtCostMessage.Text.Trim());

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            txtFestivalCost.Text = "";
            txtCostMessage.Text = "";
            LoadFestivalRequestsGrid();
        }

        private void LoadFestivalDropdown()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT FestivalName FROM Festivals WHERE IsActive = 1 ORDER BY FestivalName";

                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                ddlFestivalList.Items.Clear();
                while (reader.Read())
                {
                    ddlFestivalList.Items.Add(reader["FestivalName"].ToString());
                }

                conn.Close();
            }
        }

        private void LoadFestivalRequestsGrid()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT RequestType, FestivalName, RequestedCost, Message, Status, RequestDate FROM FestivalRequests ORDER BY RequestDate DESC";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvFestivalRequests.DataSource = dt;
                gvFestivalRequests.DataBind();
            }
        }
    }
}