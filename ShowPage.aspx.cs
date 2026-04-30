using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using QRCoder;

namespace Tourism_Management
{
    public partial class ShowPage : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ticketNo = Request.QueryString["ticketNo"];
                if (string.IsNullOrEmpty(ticketNo))
                {
                    lblFestivalHeading.Text = "No Ticket Selected.";
                    return;
                }

                LoadBooking(ticketNo);
            }
        }

        private void LoadBooking(string ticketNo)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                string query = @"
            SELECT 
                b.TicketNo,
                f.FestivalName,
                f.Venue,
                b.BookingDate,
                b.SeatNo,
                u.Name AS TravelerName
            FROM Bookings b
            INNER JOIN Users u ON b.UserId = u.UserId
            INNER JOIN Festivals f ON b.FestivalId = f.FestivalId
            WHERE b.TicketNo = @TicketNo";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@TicketNo", ticketNo);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // ✅ Display core booking info
                            lblFestivalHeading.Text = rdr["FestivalName"].ToString();
                            lblTravelerName.Text = rdr["TravelerName"].ToString();
                            lblFestival.Text = rdr["FestivalName"].ToString();
                            lblVenue.Text = rdr["Venue"].ToString();

                            DateTime bookingDate = Convert.ToDateTime(rdr["BookingDate"]);
                            lblDate.Text = bookingDate.ToString("yyyy-MM-dd");

                            lblTicketNo.Text = rdr["TicketNo"].ToString();

                            string seatNo = rdr["SeatNo"] == DBNull.Value ? null : rdr["SeatNo"].ToString();

                            // ✅ Check if SeatNo is missing — then generate and update
                            if (string.IsNullOrEmpty(seatNo))
                            {
                                rdr.Close(); // close reader before updating

                                seatNo = GenerateNextSeatNumber(con);
                                UpdateSeatNumber(ticketNo, seatNo, con);
                            }

                            lblSeatNo.Text = seatNo;

                            // ✅ Generate QR code
                            GenerateQrCode(ticketNo);
                        }
                        else
                        {
                            lblFestivalHeading.Text = "Ticket not found.";
                        }
                    }
                }
            }
        }


        private string GenerateNextSeatNumber(SqlConnection con)
        {
            string query = "SELECT MAX(SeatNo) FROM Bookings";
            SqlCommand cmd = new SqlCommand(query, con);
            object result = cmd.ExecuteScalar();
            string lastSeat = result?.ToString();

            if (string.IsNullOrEmpty(lastSeat))
                return "A1"; // first seat

            char row = lastSeat[0];
            int number = int.Parse(lastSeat.Substring(1));

            number++;
            if (number > 50)
            {
                number = 1;
                row = (char)(row + 1); // next row
            }

            return $"{row}{number}";
        }

        private void UpdateSeatNumber(string ticketNo, string seatNo, SqlConnection con)
        {
            string updateQuery = "UPDATE Bookings SET SeatNo = @SeatNo WHERE TicketNo = @TicketNo";
            using (SqlCommand updateCmd = new SqlCommand(updateQuery, con))
            {
                updateCmd.Parameters.AddWithValue("@SeatNo", seatNo);
                updateCmd.Parameters.AddWithValue("@TicketNo", ticketNo);
                updateCmd.ExecuteNonQuery();
            }
        }

        private void GenerateQrCode(string ticketNo)
        {
            string imagesFolder = Server.MapPath("~/Images/");
            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            string qrPath = Path.Combine(imagesFolder, $"{ticketNo}.png");

            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(ticketNo, QRCodeGenerator.ECCLevel.Q))
            using (QRCode qrCode = new QRCode(qrCodeData))
            using (Bitmap qrImage = qrCode.GetGraphic(20))
            {
                qrImage.Save(qrPath, ImageFormat.Png);
            }

            imgQrCode.ImageUrl = "~/Images/" + ticketNo + ".png";
        }
    }
}
