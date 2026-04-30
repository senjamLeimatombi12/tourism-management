using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Tourism_Management
{
    public partial class superAdminNewfestivalApproval : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFestivalNameRequests();
                LoadFestivalCostRequests();
            }
        }

        private void LoadFestivalNameRequests()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestID, FestivalName, 'Admin' AS RequestedBy, Status FROM FestivalRequests WHERE RequestType = 'FestivalName'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvFestivalNameRequests.DataSource = dt;
                gvFestivalNameRequests.DataBind();
            }
        }

        private void LoadFestivalCostRequests()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT RequestID, FestivalName, RequestedCost, Message, Status, RequestDate FROM FestivalRequests WHERE RequestType = 'FestivalCost'";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvFestivalCostRequests.DataSource = dt;
                gvFestivalCostRequests.DataBind();
            }
        }

        protected void gvFestivalNameRequests_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int requestId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Approve")
            {
                ApproveFestivalNameRequest(requestId);
            }
            else if (e.CommandName == "Reject")
            {
                RejectFestivalNameRequest(requestId);
            }

            LoadFestivalNameRequests(); // Refresh grid
        }

        protected void gvFestivalCostRequests_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            int requestId = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Approve")
            {
                ApproveFestivalCostRequest(requestId);
            }
            else if (e.CommandName == "Reject")
            {
                RejectFestivalCostRequest(requestId);
            }

            LoadFestivalCostRequests(); // Refresh grid
        }

        private void ApproveFestivalNameRequest(int requestId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Get the festival name
                string getFestivalNameQuery = "SELECT FestivalName FROM FestivalRequests WHERE RequestID = @RequestID";
                SqlCommand getCmd = new SqlCommand(getFestivalNameQuery, con);
                getCmd.Parameters.AddWithValue("@RequestID", requestId);
                string festivalName = getCmd.ExecuteScalar()?.ToString();

                // Insert into Festivals table
                string insertQuery = "INSERT INTO Festivals (FestivalName, TicketCost, IsActive, ApprovedBySuperAdmin) VALUES (@FestivalName, 0.00, 1, 1)";
                SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                insertCmd.Parameters.AddWithValue("@FestivalName", festivalName);
                insertCmd.ExecuteNonQuery();

                // Update status
                string updateQuery = "UPDATE FestivalRequests SET Status = 'Approved' WHERE RequestID = @RequestID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@RequestID", requestId);
                updateCmd.ExecuteNonQuery();
            }
        }

        private void RejectFestivalNameRequest(int requestId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE FestivalRequests SET Status = 'Rejected' WHERE RequestID = @RequestID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@RequestID", requestId);
                con.Open();
                updateCmd.ExecuteNonQuery();
            }
        }

        private void ApproveFestivalCostRequest(int requestId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Get festival name and requested cost
                string getQuery = "SELECT FestivalName, RequestedCost FROM FestivalRequests WHERE RequestID = @RequestID";
                SqlCommand getCmd = new SqlCommand(getQuery, con);
                getCmd.Parameters.AddWithValue("@RequestID", requestId);
                SqlDataReader reader = getCmd.ExecuteReader();

                if (reader.Read())
                {
                    string festivalName = reader["FestivalName"].ToString();
                    decimal cost = Convert.ToDecimal(reader["RequestedCost"]);
                    reader.Close();

                    // Update cost in Festivals table
                    string updateFestival = "UPDATE Festivals SET TicketCost = @Cost WHERE FestivalName = @FestivalName";
                    SqlCommand updateCmd = new SqlCommand(updateFestival, con);
                    updateCmd.Parameters.AddWithValue("@Cost", cost);
                    updateCmd.Parameters.AddWithValue("@FestivalName", festivalName);
                    updateCmd.ExecuteNonQuery();

                    // Update request status
                    string updateStatus = "UPDATE FestivalRequests SET Status = 'Approved' WHERE RequestID = @RequestID";
                    SqlCommand statusCmd = new SqlCommand(updateStatus, con);
                    statusCmd.Parameters.AddWithValue("@RequestID", requestId);
                    statusCmd.ExecuteNonQuery();
                }
            }
        }

        private void RejectFestivalCostRequest(int requestId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string updateQuery = "UPDATE FestivalRequests SET Status = 'Rejected' WHERE RequestID = @RequestID";
                SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                updateCmd.Parameters.AddWithValue("@RequestID", requestId);
                con.Open();
                updateCmd.ExecuteNonQuery();
            }
        }
    }
}
