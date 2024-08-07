using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace myStore.Pages.login
{
    public class IndexModel : PageModel
    {
        public customerDetail customerDetail = new customerDetail();
        public String errorMessage = "";
        public string successfullMessage = "";

        public List<customerDetail> listcustomer = new List<customerDetail>();
        public string enterEmail;
        public string enterPassword;


        public void OnPost()
        {
            enterEmail = Request.Form["username"];
            enterPassword = Request.Form["password"];
            try
            {
                string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                string sql = "SELECT c_id FROM dbo.customer where email = @enterEmail AND password = @enterPassword";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@enterEmail", enterEmail);
                        command.Parameters.AddWithValue("@enterPassword", enterPassword);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Successful login
                                errorMessage = "Success credentials";
                                successfullMessage = "Login successful";
                                Response.Redirect("/customer/index");
                                // Optionally provide a success message

                                // You can set a session or cookie to store user information.
                            }
                            else
                            {
                                errorMessage = "Invalid credentials";
                                Response.Redirect("/login/index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "An error occurred while processing your request";
                // Handle the exception as needed
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



