using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Tourism_Management
{
    public partial class book_ticket : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {


                string selectedPlace = ddlPlaces.SelectedValue;
                int entryFee = int.Parse(txtEntryFee.Text);
                int persons = int.Parse(txtPersons.Text);

                int totalCost = entryFee * persons;
                txtTotalCost.Text = totalCost.ToString();

                using (SqlConnection con = new SqlConnection(strcon))
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }

                    string query = "INSERT INTO TouristVisits(Place, EntryFee, Persons, TotalCost) Values(@Place, @EntryFee, @Persons, @TotalCost)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Place", selectedPlace);
                        cmd.Parameters.AddWithValue("@EntryFee", entryFee);
                        cmd.Parameters.AddWithValue("@Persons", persons);
                        cmd.Parameters.AddWithValue("@TotalCost", totalCost);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                txtTotalCost.Text = "Error: " + ex.Message;
            }
        }

        protected void txtTotalCost_TextChanged(object sender, EventArgs e)
        {

        }
    }
}