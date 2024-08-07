using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace myStore.Pages.login
{
    public class adminModel : PageModel
    {
        public adminDetail adminDetail = new adminDetail();
        public String errorMessage = "";
        public string successfullMessage = "";

        public List<adminDetail> listadmin = new List<adminDetail>();
        public string enterEmail;
        public string enterPassword;


        public void OnPost()
        {
            enterEmail = Request.Form["username"];
            enterPassword = Request.Form["password"];
            try
            {
                string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                string sql = "SELECT admin_id FROM dbo.admin where user_name = @enterEmail AND password = @enterPassword";
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
                                Response.Redirect("/admin/index");
                                // Optionally provide a success message

                                // You can set a session or cookie to store user information.
                            }
                            else
                            {
                                errorMessage = "Invalid credentials";
                                Response.Redirect("/login/admin");
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
    public class adminDetail
    {
        public string admin_id;
        public string password;
    }
}
