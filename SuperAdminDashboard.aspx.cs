using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class SuperAdminDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUserRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/superAdminNewFestivalApproval.aspx");

        }

        protected void btnFestivalRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SuperAdminUserApproval.aspx");

        }

        protected void btnTicketRequests_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminFestivalBookingDateSetUp.aspx");

        }

        protected void setPrice_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminTicketingFestivalManagement.aspx");

        }

        protected void user_update_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AdminTicketingUserManagement.aspx");

        }

        protected void book_ticket_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserTicketing.aspx");

        }
    }
}
