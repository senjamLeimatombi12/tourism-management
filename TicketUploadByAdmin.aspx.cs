using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class TicketUploadByAdmin : System.Web.UI.Page
    {
        string conStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFestivals();
                LoadTickets();
            }
        }

        private void LoadFestivals()
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT FestivalID, FestivalName FROM Festivals";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                ddlFestival.DataSource = cmd.ExecuteReader();
                ddlFestival.DataTextField = "FestivalName";
                ddlFestival.DataValueField = "FestivalID";
                ddlFestival.DataBind();
            }
            ddlFestival.Items.Insert(0, new ListItem("--Select Festival--", "0"));
        }

        private bool FestivalOrTicketExists(int festivalId, string ticketId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = "SELECT COUNT(*) FROM Ticket_Template WHERE FestivalID = @FestivalID OR TicketID = @TicketID";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@FestivalID", festivalId);
                cmd.Parameters.AddWithValue("@TicketID", ticketId);
                con.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // ✅ Insert new ticket (manual entry + file upload optional)
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ddlFestival.SelectedValue != "0" && !string.IsNullOrWhiteSpace(txtTicketNumber.Text))
            {
                int festivalId = Convert.ToInt32(ddlFestival.SelectedValue);
                string festivalName = ddlFestival.SelectedItem.Text;
                string ticketId = txtTicketNumber.Text.Trim();

                if (FestivalOrTicketExists(festivalId, ticketId))
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Ticket already exists for this festival or Ticket ID is duplicate.";
                    return;
                }

                string filePath = null;
                if (FileUpload1.HasFile)
                {
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    filePath = "~/Ticket/" + fileName;
                    FileUpload1.SaveAs(Server.MapPath(filePath));
                }

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string insertQuery = @"INSERT INTO Ticket_Template (TicketID, FestivalID, FestivalName, TicketPath) 
                                           VALUES (@TicketID, @FestivalID, @FestivalName, @TicketPath)";
                    SqlCommand cmd = new SqlCommand(insertQuery, con);
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);
                    cmd.Parameters.AddWithValue("@FestivalID", festivalId);
                    cmd.Parameters.AddWithValue("@FestivalName", festivalName);
                    cmd.Parameters.AddWithValue("@TicketPath", (object)filePath ?? DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Ticket added successfully!";
                txtTicketNumber.Text = "";
                LoadTickets();
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please select a festival and enter a Ticket ID.";
            }
        }

        // ✅ Update existing ticket (replace festival name or file path)
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTicketNumber.Text))
            {
                string ticketId = txtTicketNumber.Text.Trim();
                string festivalName = ddlFestival.SelectedItem.Text;

                string filePath = null;
                if (FileUpload1.HasFile)
                {
                    string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                    filePath = "~/TicketFiles/" + fileName;
                    FileUpload1.SaveAs(Server.MapPath(filePath));
                }

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string updateQuery = @"UPDATE Ticket_Template 
                                           SET FestivalName = @FestivalName, TicketPath = @TicketPath 
                                           WHERE TicketID = @TicketID";
                    SqlCommand cmd = new SqlCommand(updateQuery, con);
                    cmd.Parameters.AddWithValue("@FestivalName", festivalName);
                    if (filePath != null)
                        cmd.Parameters.AddWithValue("@TicketPath", filePath);
                    else
                        cmd.Parameters.AddWithValue("@TicketPath", DBNull.Value);
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);

                    con.Open();
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Green;
                        lblMessage.Text = "Ticket updated successfully!";
                        LoadTickets();
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Ticket ID not found. Cannot update.";
                    }
                }
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Please enter a Ticket ID to update.";
            }
        }

        // ✅ GridView Edit button (load ticket into form)
        protected void gvTickets_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditFestival")
            {
                string ticketId = e.CommandArgument.ToString();
                txtTicketNumber.Text = ticketId;

                using (SqlConnection con = new SqlConnection(conStr))
                {
                    string query = "SELECT FestivalID, FestivalName FROM Ticket_Template WHERE TicketID = @TicketID";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        ddlFestival.SelectedValue = dr["FestivalID"].ToString();
                        txtFestivalName.Text = dr["FestivalName"].ToString();
                    }
                }
            }
        }

        private void LoadTickets() 
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                string query = @"SELECT TicketID, FestivalName, FestivalID, TicketPath 
                         FROM Ticket_Template";  // ✅ make sure all columns exist
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                gvTickets.DataSource = dr;
                gvTickets.DataBind();
            }
        }

    }
}
