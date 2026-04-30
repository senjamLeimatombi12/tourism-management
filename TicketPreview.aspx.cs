using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class TicketPreview : System.Web.UI.Page
    {
              
            string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

       

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadTicketPreview();
                }
            }

        private void LoadTicketPreview()
        {
            string ticketId = Request.QueryString["id"];
            if (string.IsNullOrEmpty(ticketId)) return;

            int ticketIdInt;
            if (!int.TryParse(ticketId, out ticketIdInt)) return;

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                // 1. Get FestivalID
                int festivalId = 0;
                using (SqlCommand cmdFestival = new SqlCommand(
                    "SELECT FestivalID FROM Bookings WHERE TicketID = @TicketID", con))
                {
                    cmdFestival.Parameters.Add("@TicketID", System.Data.SqlDbType.Int).Value = ticketIdInt;
                    object resultFestival = cmdFestival.ExecuteScalar();
                    if (resultFestival != null)
                        festivalId = Convert.ToInt32(resultFestival);
                }

                // 2. Get Template
                string templatePath = string.Empty;
                if (festivalId > 0)
                {
                    using (SqlCommand cmdTemplate = new SqlCommand(
                        "SELECT TOP 1 TicketPath FROM Ticket_Template WHERE FestivalID = @FestivalID ORDER BY TicketID DESC", con))
                    {
                        cmdTemplate.Parameters.Add("@FestivalID", System.Data.SqlDbType.Int).Value = festivalId;
                        object result = cmdTemplate.ExecuteScalar();
                        templatePath = result != null ? result.ToString() : string.Empty;
                    }
                }

                imgTicketTemplate.ImageUrl = !string.IsNullOrEmpty(templatePath) ?
                    ResolveUrl(templatePath) : "~/Images/default_ticket.png";

                // 3. Load Ticket Details
                using (SqlCommand cmd = new SqlCommand(@"
            SELECT u.Name, f.FestivalName, b.BookingDate, b.TicketID, f.Venue, b.TotalCost 
            FROM Bookings b
            JOIN Users u ON b.UserID = u.UserID
            JOIN Festivals f ON b.FestivalID = f.FestivalID
            WHERE b.TicketID = @TicketID", con))
                {
                    cmd.Parameters.Add("@TicketID", System.Data.SqlDbType.Int).Value = ticketIdInt;
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        lblName.Text = dr["Name"] == DBNull.Value ? "" : dr["Name"].ToString();
                        lblFestival.Text = dr["FestivalName"] == DBNull.Value ? "" : dr["FestivalName"].ToString();
                        lblDate.Text = dr["BookingDate"] == DBNull.Value ? "" :
                                           Convert.ToDateTime(dr["BookingDate"]).ToString("MMM dd, yyyy");
                        lblTicketID.Text = dr["TicketID"] == DBNull.Value ? "" : dr["TicketID"].ToString();
                        lblVenue.Text = dr["Venue"] == DBNull.Value ? "" : dr["Venue"].ToString();
                        lblCost.Text = dr["TotalCost"] == DBNull.Value ? "" : "₹" + dr["TotalCost"].ToString();
                    }
                    else
                    {
                        // No record → keep all blank
                        lblName.Text = lblFestival.Text = lblDate.Text = lblTicketID.Text = lblVenue.Text = lblCost.Text = "";
                    }

                   
                }
            }
        }

    }
}


