using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace Tourism_Management
{
    public partial class UserDetailUpdateTicketingRequestForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSubmitRequest_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string date = txtDate.Text.Trim(); 
            string message = txtMessage.Text.Trim();
            string filePath = "";

            if (fileID.HasFile)
            {
                string fileName = Path.GetFileName(fileID.PostedFile.FileName);
                filePath = "~/Uploads_documents_IDs/" + fileName;

                string physicalPath = Server.MapPath(filePath);

                // Ensure folder exists
                string directory = Path.GetDirectoryName(physicalPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                fileID.SaveAs(physicalPath);

            }

            string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
               
                    string query = @"UPDATE Users
                     SET Name = @Name,
                         Email = @Email,
                         Phone = @Phone,
                         IDDocumentPath = @FilePath,
                         Message = @Message,
                         RequestID = NEWID()
                     WHERE UserID = @UserID";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@FilePath", filePath);
                    cmd.Parameters.AddWithValue("@Message", message);
                    cmd.Parameters.AddWithValue("@UserID", Convert.ToInt32(Session["UserID"])); // ← Add this line

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
            }

               
            

            lblStatus.Text = "Update request submitted successfully!";
            ClearForm();
        }

        private void ClearForm()
        {
            txtName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtMessage.Text = "";
            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }
}
    