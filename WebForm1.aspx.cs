using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        { 
        
        
        }
      
            protected void btnGenerate_Click(object sender, EventArgs e)
            {
                if (FileUpload1.HasFile)
                {
                    try
                    {
                        // 1. Fetch TicketID and Date from DB
                        string ticketId = "";
                        string ticketDate = "";

                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("SELECT TOP 1 TicketID, BookingDate FROM Bookings ORDER BY TicketID DESC", conn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.Read())
                            {
                                ticketId = dr["TicketID"].ToString();
                                ticketDate = Convert.ToDateTime(dr["BookingDate"]).ToString("yyyy-MM-dd");
                            }
                        }

                        // 2. Load uploaded image
                        using (Stream imgStream = FileUpload1.FileContent)
                        using (Bitmap bmp = new Bitmap(imgStream))
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            // 3. Draw text on image
                            Font font = new Font("Arial", 20, FontStyle.Bold);
                            Brush brush = Brushes.Black;

                            // Example positions (adjust as needed for your template)
                            g.DrawString("Ticket ID: " + ticketId, font, brush, new PointF(50, 50));
                            g.DrawString("Date: " + ticketDate, font, brush, new PointF(50, 100));

                            // 4. Save new image
                            string newFileName = "Ticket_" + ticketId + ".png";
                            string savePath = Server.MapPath("~/https://localhost:44332/Web.Debug.configUploadedTickets/" + newFileName);

                            Directory.CreateDirectory(Server.MapPath("~/UploadedTickets/"));
                            bmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);

                            // Show to user
                            imgPreview.ImageUrl = "~/UploadedTickets/" + newFileName;
                            lblMessage.Text = "Ticket generated successfully!";
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error: " + ex.Message;
                    }
                }
                else
                {
                    lblMessage.Text = "Please upload a ticket template image first.";
                }
            }
        }
    }






