using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myStore.Pages.admin
{
    public class customerDetailsModel : PageModel
    {
        public List<customerDetail> listcustomer=new List<customerDetail>();  
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM dbo.customer";
                    using (SqlCommand command =new SqlCommand(sql,connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                customerDetail customerDetail = new customerDetail();
                                customerDetail.c_id = "" + reader.GetInt32(0);
                                customerDetail.c_name = reader.GetString(1);
                                customerDetail.address = reader.GetString(2);
                                customerDetail.tel = reader.GetString(3);
                                customerDetail.email = reader.GetString(4);
                                customerDetail.password=reader.GetString(5);

                                listcustomer.Add(customerDetail);
                            }
                        }
                       
                    }
                }
            }
            catch (Exception ex) 
            {
                 Console.WriteLine("Exception: " +ex.ToString());
            }
        }
    }
    public class customerDetail 
    {
        public string c_id;
        public string c_name;
        public string address;
        public string tel;
        public string email;
        public string password;
        public string created_at;

    }
}
