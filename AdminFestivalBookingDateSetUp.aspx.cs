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
    
        public partial class AdminFestivalBookingDateSetUp : System.Web.UI.Page
        {
            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadFestivals();
                }
            }

            private void LoadFestivals()
            {
                using (SqlConnection con = new SqlConnection(cs))
                {
                    string query = "SELECT FestivalID, FestivalName FROM Festivals WHERE ApprovedBySuperAdmin = 1 AND IsActive = 1";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    DropDownList1.DataSource = rdr;
                    DropDownList1.DataTextField = "FestivalName";
                    DropDownList1.DataValueField = "FestivalID";
                    DropDownList1.DataBind();

                    DropDownList1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Select Festival --", ""));
                }
            }

        protected void StartDate_SelectionChanged(object sender, EventArgs e)
        {
            // Store the selected date in the textbox in yyyy-MM-dd format
            txtStartDate.Text = StartDate.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void EndDate_SelectionChanged(object sender, EventArgs e)
        {
            txtEndDate.Text = EndDate.SelectedDate.ToString("yyyy-MM-dd");
        }

        protected void Button1_Click(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(DropDownList1.SelectedValue))
                {
                    Response.Write("<script>alert('Please select a festival');</script>");
                    return;
                }

                DateTime startDate, endDate;
                if (!DateTime.TryParse(txtStartDate.Text, out startDate))
                {
                    Response.Write("<script>alert('Invalid start date');</script>");
                    return;
                }
                if (!DateTime.TryParse(txtEndDate.Text, out endDate))
                {
                    Response.Write("<script>alert('Invalid end date');</script>");
                    return;
                }
                if (endDate < startDate)
                {
                    Response.Write("<script>alert('End Date cannot be earlier than Start Date');</script>");
                    return;
                }

                bool allowContinuous = CheckBox1.Checked;
                bool bookingEnabled = CheckBox2.Checked;

                using (SqlConnection con = new SqlConnection(cs))
                {
                   
                string query = @"
                    IF EXISTS(SELECT 1 FROM FestivalBookingDates WHERE FestivalID = @FestivalID)
                    UPDATE FestivalBookingDates  -- ✅ correct table
                    SET StartDate = @StartDate,
                        EndDate = @EndDate,
                        AllowMultipleDays = @AllowMultipleDays,
                        BookingEnabled = @BookingEnabled
                    WHERE FestivalID = @FestivalID
                    ELSE
                    INSERT INTO FestivalBookingDates(FestivalID, StartDate, EndDate, AllowMultipleDays, BookingEnabled)
                    VALUES(@FestivalID, @StartDate, @EndDate, @AllowMultipleDays, @BookingEnabled)";

                                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@FestivalID", DropDownList1.SelectedValue);
                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);
                    cmd.Parameters.AddWithValue("@AllowMultipleDays", allowContinuous);
                    cmd.Parameters.AddWithValue("@BookingEnabled", bookingEnabled);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                Response.Write("<script>alert('Booking period saved successfully');</script>");
            }
        }
    }
