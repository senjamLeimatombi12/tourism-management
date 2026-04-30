 using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class UserTicketing : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindFestivalDropdown();
                // No default select; user chooses a festival.

                if (Session["UserID"] != null)
                {
                    txtName.Text = Session["Name"]?.ToString();
                    txtEmail.Text = Session["Email"]?.ToString();
                    txtPhone.Text = Session["Phone"]?.ToString();
                    txtState.Text = Session["State"]?.ToString();
                    txtCountry.Text = Session["Country"]?.ToString();
                    txtAddress.Text = Session["Address"]?.ToString();
                    txtPincode.Text = Session["Pincode"]?.ToString();
                    txtPassport.Text = Session["PassportNumber"]?.ToString();
                    txtGovtID.Text = Session["GovtID"]?.ToString();
                    ddlNationality.SelectedValue = Session["IDType"]?.ToString();
                    txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd"); // auto set today
                }
            }

            // Reinitialize client datepicker with the current hdnStartDate/hdnEndDate
            // (client-side script runs on each load and uses these)
        }

        private void BindFestivalDropdown()
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                string query = @"SELECT FestivalID, FestivalName 
                                 FROM Festivals
                                 WHERE ApprovedBySuperAdmin = 1 AND IsActive = 1
                                 ORDER BY FestivalName";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        ddlFestival.Items.Clear();
                        ddlFestival.Items.Add(new ListItem("-- Select Festival --", ""));
                        while (dr.Read())
                        {
                            ddlFestival.Items.Add(
                                new ListItem(dr["FestivalName"].ToString(),
                                             dr["FestivalID"].ToString()));
                        }
                    }
                }
            }
        }

        protected void ddlFestival_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnStartDate.Value = "";
            hdnEndDate.Value = "";
            lblCostPerTicket.Text = "";

            if (!int.TryParse(ddlFestival.SelectedValue, out int festivalId))
                return;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                // Get cost + booking window (if any)
                string sql = @"
                    SELECT f.TicketCost,
                           fb.StartDate,
                           fb.EndDate
                    FROM Festivals f
                    LEFT JOIN FestivalBookingDates fb ON f.FestivalID = fb.FestivalID
                    WHERE f.FestivalID = @FestivalID";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@FestivalID", festivalId);
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            if (r["TicketCost"] != DBNull.Value)
                                lblCostPerTicket.Text = Convert.ToDecimal(r["TicketCost"]).ToString("0.00");

                            if (r["StartDate"] != DBNull.Value)
                                hdnStartDate.Value = Convert.ToDateTime(r["StartDate"]).ToString("yyyy-MM-dd");
                            if (r["EndDate"] != DBNull.Value)
                                hdnEndDate.Value = Convert.ToDateTime(r["EndDate"]).ToString("yyyy-MM-dd");
                        }
                    }
                }
            }

            // Client-side will re-init the datepicker and prefill with start date if present.
            txtFestivalDate.Text = ""; // let client prefill
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            // ✅ Generate a unique Ticket Number
            string ticketNo = "T" + DateTime.Now.ToString("yyyyMMddHHmmssfff");

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string insertQuery = @"
            INSERT INTO Bookings 
                (UserID, FestivalID, Quantity, CostPerTicket, Start_Date_Of_Booking, End_Date_Of_Booking, 
                 FestivalName, BookingDate, TicketNo)
            VALUES 
                (@UserID, @FestivalID, 1, @CostPerTicket, @StartDate, @EndDate,
                 @FestivalName, @BookingDate, @TicketNo);
        ";

                SqlCommand cmd = new SqlCommand(insertQuery, con);
                // TODO: Replace 1 with actual logged-in user ID when authentication is implemented
                int userId = Convert.ToInt32(Session["UserID"]);
                cmd.Parameters.AddWithValue("@UserID", userId);

                cmd.Parameters.AddWithValue("@FestivalID", ddlFestival.SelectedValue);
                cmd.Parameters.AddWithValue("@CostPerTicket", Convert.ToDecimal(lblCostPerTicket.Text));
                cmd.Parameters.AddWithValue("@StartDate", string.IsNullOrEmpty(hdnStartDate.Value) ? (object)DBNull.Value : Convert.ToDateTime(hdnStartDate.Value));
                cmd.Parameters.AddWithValue("@EndDate", string.IsNullOrEmpty(hdnEndDate.Value) ? (object)DBNull.Value : Convert.ToDateTime(hdnEndDate.Value));
                cmd.Parameters.AddWithValue("@FestivalName", ddlFestival.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@BookingDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@TicketNo", ticketNo);
               // cmd.Parameters.AddWithValue("@SeatNo", "A1"); // can randomize later if you wish

                cmd.ExecuteNonQuery();
            }

            // ✅ Redirect to ShowPage, passing TicketNo in query string
            Response.Redirect("ShowPage.aspx?ticketNo=" + ticketNo);
        }



        private int AssignTicket(SqlConnection conn, int userId, int festivalId)
        {
            int ticketId = 0;
            using (SqlCommand cmd = new SqlCommand(
                "SELECT TOP 1 TicketID\r\nFROM ticket_template\r\nWHERE FestivalID = @FestivalID\r\nORDER BY TicketID ASC; ", conn))
            {
                cmd.Parameters.AddWithValue("@FestivalID", festivalId);
                object result = cmd.ExecuteScalar();
                if (result != null)
                    ticketId = Convert.ToInt32(result);
            }

            if (ticketId > 0)
            {
                

                using (SqlCommand cmd = new SqlCommand("UPDATE Ticket_Template SET FestivalID = @FestivalID WHERE TicketID = @TicketID", conn))
                {
                    cmd.Parameters.AddWithValue("@FestivalID", festivalId);   
                    cmd.Parameters.AddWithValue("@TicketID", ticketId);
                    cmd.ExecuteNonQuery();
                }
            }

            return ticketId;
        } } }

        public class BookingModel
        {
            public int FestivalID { get; set; }
            public string FestivalName { get; set; }
            public int TicketQuantity { get; set; }
            public decimal TicketCost { get; set; }
            public string SelectedDate { get; set; }   // yyyy-MM-dd from client
        }
    

