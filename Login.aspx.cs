using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Tourism_Management
{
    public partial class Login : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {



        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();

                    // Hash the entered password before checking
                    string hashedPassword = HashPassword(txtPassword.Text.Trim());

                    string query = @"SELECT CanLogin FROM UserCredentials WHERE Email = @Email AND Password = @Password";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    object result = cmd.ExecuteScalar();

                    if (result != null && Convert.ToInt32(result) == 1)
                    {
                        // Fetch full user details from Users table
                        string getUserQuery = "SELECT TOP 1 * FROM Users WHERE Email = @Email";
                        using (SqlCommand cmdUser = new SqlCommand(getUserQuery, con))
                        {
                            cmdUser.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                            using (SqlDataReader dr = cmdUser.ExecuteReader())
                            {
                                if (dr.Read())
                                {
                                    Session["UserID"] = dr["UserID"];
                                    Session["Name"] = dr["Name"].ToString();
                                    Session["Email"] = dr["Email"].ToString();
                                    Session["Phone"] = dr["Phone"].ToString();
                                    Session["State"] = dr["State"].ToString();
                                    Session["Country"] = dr["Country"].ToString();
                                    Session["Address"] = dr["Address"].ToString();
                                    Session["Pincode"] = dr["Pincode"].ToString();
                                    Session["PassportNumber"] = dr["PassportNumber"].ToString();
                                    Session["GovtID"] = dr["GovtID"].ToString();
                                    Session["IDType"] = dr["IDType"].ToString();
                                    Session["Today_Date"] = dr["Today_Date"].ToString();

                                    Session["role"] = "user";
                                    
                                }
                            }
                        }
                        Response.Write("<script>alert('Login Successful');</script>");
                        Response.Redirect("UserTicketing.aspx");
                    }                  
                    else
                    {
                        Response.Write("<script>alert('Login Disabled or Invalid Credentials');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }

        // Reuse your SHA256 hashing function from SignUp
        private string HashPassword(string password)
        {
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }




    }
}


