using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;


namespace myStore.Pages
{
    public class IndexModel : PageModel
    {
        public List<flowerDetail> listflowers = new List<flowerDetail>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql1 = "SELECT * FROM dbo.flower";
                    using (SqlCommand command = new SqlCommand(sql1, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                flowerDetail flowerDetail1 = new flowerDetail();
                                flowerDetail1.f_id = reader.GetInt32(0); // Use index or column name
                                flowerDetail1.f_type = reader.GetString(1);
                                flowerDetail1.f_name = reader.GetString(2);
                                flowerDetail1.f_color = reader.GetString(3);
                                flowerDetail1.image = reader.GetString(4);
                                flowerDetail1.price = reader.GetString(5); // Use the correct data type and GetDouble

                                listflowers.Add(flowerDetail1);
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

    public class flowerDetail
    {
        public int f_id;
        public string f_type;
        public string f_name;
        public string f_color;
        public string image;
        public String price;
    }
}


