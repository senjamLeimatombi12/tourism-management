using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class adminlogin : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
          


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open(); // No need to check the state if using SqlConnection inside `using`.

                    string query = "SELECT * FROM admin_table WHERE username = @username AND password = @password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", TextBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", TextBox2.Text.Trim());

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    Session["username"] = dr.GetValue(0).ToString();
                                    Session["full_name"] = dr.GetValue(2).ToString();
                                    Session["role"] = "admin";
                                    Response.Write("<script>alert('Welcome " + dr["username"].ToString() + "');</script>");
                                    Response.Redirect("~/AdminDashboard.aspx");

                                }
                            }
                            else
                            {
                                Response.Write("<script>alert('Invalid credentials');</script>");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('Error: " + ex.Message.Replace("'", "\\'") + "');</script>");
            }
        }
    }
}