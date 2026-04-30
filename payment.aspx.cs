using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class payment : System.Web.UI.Page
    {
        public string key = "rzp_test_T7WxuV2yrd8PgL";
        public string orderId;
        public string orderIds;
        public string name; 
        public string product;
        public string email;
        public string contact;
        public string addressn;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                try
                {
                        
                    name = "Sanjeev";
                    product = "Shoes 01";
                    email = "senjamleima6@gmail.com";
                    contact = "8731003176";
                    addressn = "Delhi";
                    Session["product"] = product;
                    Session["totalprice"] = "100";
                    Dictionary<string, object> input = new Dictionary<string, object>();

                    int am = 100;
                    //string orderss = Session["OrderId"].ToString();
                    string orderss = System.DateTime.Now.Ticks.ToString();
                    orderIds = orderss;
                    //
                    input.Add("amount", am * 100);
                    //input.Add("amount", 100); // this amount should be same as transaction amount
                    input.Add("currency", "INR");
                    input.Add("receipt", orderss);
                    input.Add("payment_capture", 1);

                    string key = "rzp_test_T7WxuV2yrd8PgL";
                    string secret = "2BR9xLk6cNdH4Lq24QMn0rNC";

                    //Session["totalprice"] = "100";
                    RazorpayClient client = new RazorpayClient(key, secret);
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    Razorpay.Api.Order order = client.Order.Create(input);
                    orderId = order["id"].ToString();


                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}