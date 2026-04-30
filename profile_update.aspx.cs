using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



    namespace Tourism_Management
    {
        public partial class profile_update : System.Web.UI.Page
        {
            string connStr = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

            protected void Page_Load(object sender, EventArgs e)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("login.aspx"); // If not logged in, go to login
                    return;
                }

                if (!IsPostBack)
                {
                    SuccessMessage.Visible = false;
                    LoadUserData();
                }
            }

            private void LoadUserData()
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        string query = "SELECT * FROM Users WHERE UserID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);

                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                txtName.Text = reader["Name"].ToString();
                                txtUsername.Text = reader["Username"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                // Do not show hashed password, leave blank
                                txtAddress.Text = reader["Address"].ToString();
                                txtState.Text = reader["State"].ToString();
                                ddlCountry.SelectedValue = reader["Country"].ToString();
                                txtPincode.Text = reader["Pincode"].ToString();
                                txtPhone.Text = reader["Phone"].ToString();
                                txtDOB.Text = Convert.ToDateTime(reader["DOB"]).ToString("yyyy-MM-dd");

                                if (reader["Nationality"].ToString() == "Indian")
                                    rbIndian.Checked = true;
                                else
                                    rbForeigner.Checked = true;

                                ddlIDType.SelectedValue = reader["IDType"].ToString();
                                txtGovtID.Text = reader["GovtID"].ToString();
                                txtPassportNumber.Text = reader["PassportNumber"].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    SuccessMessage.Text = $"<div class='alert alert-danger'>Error loading profile: {ex.Message}</div>";
                    SuccessMessage.Visible = true;
                }
            }

            protected void btnSubmit_Click(object sender, EventArgs e)
            {
                if (Page.IsValid)
                {
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connStr))
                        {
                            conn.Open();
                            string query = @"UPDATE Users 
                                         SET Name=@Name, Username=@Username, Email=@Email, 
                                             PasswordHash=@PasswordHash, Phone=@Phone, Address=@Address, 
                                             State=@State, Country=@Country, Pincode=@Pincode, DOB=@DOB, 
                                             Nationality=@Nationality, IDType=@IDType, GovtID=@GovtID, 
                                             PassportNumber=@PassportNumber, Today_Date=@Today_Date, EditStatus=@EditStatus
                                         WHERE UserID=@UserID";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                                cmd.Parameters.AddWithValue("@Name", txtName.Text.Trim());
                                cmd.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());

                                // If password field is empty, keep old password
                                string password = txtPassword.Text.Trim();
                                if (!string.IsNullOrEmpty(password))
                                    cmd.Parameters.AddWithValue("@PasswordHash", HashPassword(password));
                                else
                                    cmd.Parameters.AddWithValue("@PasswordHash", GetOldPassword());

                                cmd.Parameters.AddWithValue("@Phone", txtPhone.Text.Trim());
                                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                                cmd.Parameters.AddWithValue("@State", txtState.Text.Trim());
                                cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedValue);
                                cmd.Parameters.AddWithValue("@Pincode", txtPincode.Text.Trim());
                                cmd.Parameters.AddWithValue("@DOB", DateTime.Parse(txtDOB.Text));
                                cmd.Parameters.AddWithValue("@Nationality", rbIndian.Checked ? "Indian" : "Foreigner");
                                cmd.Parameters.AddWithValue("@IDType", ddlIDType.SelectedValue);
                                cmd.Parameters.AddWithValue("@GovtID", txtGovtID.Text.Trim());
                                cmd.Parameters.AddWithValue("@PassportNumber", string.IsNullOrEmpty(txtPassportNumber.Text) ? (object)DBNull.Value : txtPassportNumber.Text.Trim());
                                cmd.Parameters.AddWithValue("@Today_Date", DateTime.Today);
                                cmd.Parameters.AddWithValue("@EditStatus", "Updated");

                                cmd.ExecuteNonQuery();
                            }
                        }

                        SuccessMessage.Text = "<div class='alert alert-success'>Profile updated successfully!</div>";
                        SuccessMessage.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        SuccessMessage.Text = $"<div class='alert alert-danger'>Error: {ex.Message}</div>";
                        SuccessMessage.Visible = true;
                    }
                }
            }

            private string GetOldPassword()
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    string query = "SELECT PasswordHash FROM Users WHERE UserID=@UserID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserID", Session["UserID"]);
                        return cmd.ExecuteScalar().ToString();
                    }
                }
            }

            /// <summary>
            /// Hash password using SHA256
            /// </summary>
            private string HashPassword(string password)
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    foreach (var b in bytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
    }
