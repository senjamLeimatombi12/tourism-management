using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using QRCoder;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.WebControls;

namespace Tourism_Management
    {
        public partial class CartPage : System.Web.UI.Page
        {
            string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

      

            protected void Page_Load(object sender, EventArgs e)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }

            protected void btnDownloadLatest_Click(object sender, EventArgs e)
            {
                int userId = Convert.ToInt32(Session["UserID"]);

                // Fetch the latest booking for this user
                string query = @"
                SELECT TOP 1 
                    b.BookingID, 
                    b.TicketNo, 
                    u.Name AS TravelerName,
                    f.FestivalName AS Festival,
                    f.Venue,
                    b.BookingDate,
                    b.SeatNo
                FROM Bookings b
                INNER JOIN Users u ON b.UserID = u.UserID
                INNER JOIN Festivals f ON b.FestivalID = f.FestivalID
                WHERE b.UserID = @UserID
                ORDER BY b.BookingID DESC";

                using (SqlConnection conn = new SqlConnection(connStr))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            Response.Write("<script>alert('No booking found.');</script>");
                            return;
                        }

                        string traveler = reader["TravelerName"].ToString();
                        string festival = reader["Festival"].ToString();
                        string venue = reader["Venue"].ToString();
                        string date = Convert.ToDateTime(reader["BookingDate"]).ToString("dd-MMM-yyyy");
                        string seatNo = reader["SeatNo"].ToString();
                        string ticketNo = reader["TicketNo"].ToString();

                        // Generate QR code
                        byte[] qrBytes;
                        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                        {
                            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ticketNo, QRCodeGenerator.ECCLevel.Q);
                            using (QRCode qrCode = new QRCode(qrCodeData))
                            using (Bitmap qrBitmap = qrCode.GetGraphic(20))
                            using (MemoryStream msQr = new MemoryStream())
                            {
                                qrBitmap.Save(msQr, System.Drawing.Imaging.ImageFormat.Png);
                                qrBytes = msQr.ToArray();
                            }
                        }

                        // Generate PDF
                        using (MemoryStream ms = new MemoryStream())
                        {
                            PdfWriter writer = new PdfWriter(ms);
                            PdfDocument pdf = new PdfDocument(writer);
                            Document doc = new Document(pdf, PageSize.A4);
                            doc.SetMargins(50, 50, 50, 50);

                            doc.Add(new Paragraph("Tourism Ticket").SetFontSize(20).SetBold().SetTextAlignment(TextAlignment.CENTER));
                            doc.Add(new Paragraph(" "));

                            Div ticketDiv = new Div();
                            ticketDiv.Add(new Paragraph($"Traveler: {traveler}"));
                            ticketDiv.Add(new Paragraph($"Ticket No: {ticketNo}"));
                            ticketDiv.Add(new Paragraph($"Festival: {festival}"));
                            ticketDiv.Add(new Paragraph($"Venue: {venue}"));
                            ticketDiv.Add(new Paragraph($"Date: {date}"));
                            ticketDiv.Add(new Paragraph($"Seat No: {seatNo}"));

                            doc.Add(ticketDiv);

                            ImageData qrImageData = ImageDataFactory.Create(qrBytes);
                            iText.Layout.Element.Image qrImg = new iText.Layout.Element.Image(qrImageData)
                                .SetWidth(150)
                                .SetHeight(150)
                                .SetHorizontalAlignment(HorizontalAlignment.CENTER);
                            doc.Add(qrImg);

                            doc.Close();

                            Response.Clear();
                            Response.ContentType = "application/pdf";
                            Response.AddHeader("Content-Disposition", $"attachment;filename=Ticket_{ticketNo}.pdf");
                            Response.BinaryWrite(ms.ToArray());
                            Response.End();
                        }
                    }
                }
            }
        }
    }

           