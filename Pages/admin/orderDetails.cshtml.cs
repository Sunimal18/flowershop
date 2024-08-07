using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myStore.Pages.admin
{
    public class orderDetailsModel : PageModel
    {
        public List<orderDetails> listorder = new List<orderDetails>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM dbo.Booking";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                orderDetails orderDetails = new orderDetails();
                                orderDetails.b_id = reader.GetInt32(0);
                                orderDetails.date = reader.GetDateTime(1);
                                orderDetails.quantity = reader.GetInt32(2);
                                orderDetails.f_name = reader.GetString(3);
                                orderDetails.c_name = reader.GetString(4);

                                listorder.Add(orderDetails);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    public class orderDetails
    {
        public int b_id;
        public DateTime date;
        public int quantity;
        public string f_name;
        public string c_name;
    }
}
