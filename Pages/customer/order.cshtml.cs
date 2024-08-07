using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myStore.Pages.admin;
using System.Data.SqlClient;

namespace myStore.Pages.customer
{
    public class orderModel : PageModel
    {
        public orderDetail orderDetail = new orderDetail();
        public String errorMessage = "";
        public string successfullMessage = "";
        public void OnGet()
        {

        }

        public void OnPost()
        {
            orderDetail.c_name = Request.Form["name"];
            orderDetail.date = Request.Form["date"];
            orderDetail.quantity = Request.Form["quantity"];
            orderDetail.f_name = Request.Form["f_name"];

            if (orderDetail.c_name.Length == 0 || orderDetail.date.Length == 0 ||
                orderDetail.quantity.Length == 0 || orderDetail.f_name.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO booking" +
                        "(c_name,date, quantity, f_name)VALUES" +
                        "(@name,@date,@quantity,@f_name);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", orderDetail.c_name);
                        command.Parameters.AddWithValue("@date", orderDetail.date);
                        command.Parameters.AddWithValue("@quantity", orderDetail.quantity);
                        command.Parameters.AddWithValue("@f_name", orderDetail.f_name);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            orderDetail.c_name = "";
            orderDetail.date = "";
            orderDetail.quantity = "";
            orderDetail.f_name = "";
            
            successfullMessage = "Your Registration successfull";

            Response.Redirect("/customer/index");

        }
    }
    public class orderDetail
    {
        public string b_id;
        public string date;
        public string quantity;
        public string f_name;
        public string c_name;
        public string created_at;

    }
}