using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["role"] == null || Session["role"].ToString() == "")
                {
                    // Not logged in
                    User_Login.Visible = true;
                    Sign_up.Visible = true;
                    LinkButton3.Visible = false;
                    user.Visible = false;
                    LinkButton7.Visible = false;
                    Profile_Update.Visible = false;
                    Admin_Login.Visible = true;
                    Super_Admin_Login.Visible = true;
                }
                else if (Session["role"].ToString() == "user")
                {
                    User_Login.Visible = false;
                    Sign_up.Visible = false;
                    LinkButton3.Visible = true;
                    user.Visible = false;
                    Profile_Update.Visible = true;
                    LinkButton7.Text = "Hello " + Session["username"];
                    Admin_Login.Visible = false;
                    Super_Admin_Login.Visible = false;
                }
                else if (Session["role"].ToString() == "admin")
                {
                    User_Login.Visible = false;
                    Sign_up.Visible = false;
                    LinkButton3.Visible = true;
                    user.Visible = false;
                    Profile_Update.Visible = false;
                    LinkButton7.Text = "Hello Admin";
                    Admin_Login.Visible = false;   // 👈 looks odd, admin is already logged in, maybe you want `false` here?
                    Super_Admin_Login.Visible = false;
                }
                else if (Session["role"].ToString() == "superadmin")
                {
                    User_Login.Visible = false;
                    Sign_up.Visible = false;
                    LinkButton3.Visible = true;
                    user.Visible = false;
                    Profile_Update.Visible = false;
                    LinkButton7.Text = "Hello Super Admin";
                    Admin_Login.Visible = false;
                    Super_Admin_Login.Visible = false;
                }
            }
            catch (Exception ex)
            {
                // log ex.Message if needed
            }
        }




        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Linkbutton6_Click(object sender, EventArgs e)
        {
            Response.Redirect("Adminlogin.aspx");
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserSignUp.aspx");

        }

        protected void lnkUserSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("Signup.aspx");
        }

        protected void lnkAdminSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignupAdmin.aspx");
        }

        protected void lnkSuperAdminSignup_Click(object sender, EventArgs e)
        {
            Response.Redirect("SignupSuperAdmin.aspx");
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            Session["Name"] = "";
            Session["username"] = "";
            Session["full_name"] = "";
            Session["role"] = "";

            User_Login.Visible = true;
            Sign_up.Visible = true;
            LinkButton3.Visible = false;
            LinkButton7.Visible = false;
            Profile_Update.Visible = false;
            Admin_Login.Visible = true;


            Response.Redirect("homepage.aspx");
        }

        protected void LinkButton7_Click(object sender, EventArgs e)
        {

        }

        protected void Linkbutton7_Click(object sender, EventArgs e)
        {
            Response.Redirect("SuperAdmin_Login.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {

        }

        protected void LinkButton14_Click(object sender, EventArgs e)
        {
            Response.Redirect("profile_update.aspx");
        }
    }
}