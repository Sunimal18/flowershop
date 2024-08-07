using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using myStore.Pages.admin;
using System.Data.SqlClient;

namespace myStore.Pages.customer
{
    public class registerModel : PageModel
    {
        public customerDetail customerDetail=new customerDetail();
        public String errorMessage = "";
        public string successfullMessage = "";
        public void OnGet()
        {

        }

        public void OnPost() 
        {
            customerDetail.c_name = Request.Form["name"];
			customerDetail.address = Request.Form["address"];
			customerDetail.tel = Request.Form["tel"];
			customerDetail.email= Request.Form["email"];
            customerDetail.password= Request.Form["password"];

            if (customerDetail.c_name.Length == 0 || customerDetail.address.Length == 0 ||
				customerDetail.tel.Length == 0 || customerDetail.email.Length == 0 ||
				customerDetail.password.Length == 0) 
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
                    string sql = "INSERT INTO customer " +
                        "(c_name, address, tel, email, password) VALUES " +
                        "(@name,@address,@tel,@email,@password);";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name",customerDetail.c_name);
                        command.Parameters.AddWithValue("@address", customerDetail.address);
                        command.Parameters.AddWithValue("@tel", customerDetail.tel);
                        command.Parameters.AddWithValue("@email", customerDetail.email);
                        command.Parameters.AddWithValue("@password", customerDetail.password);

                        command.ExecuteNonQuery(); 
                    }
                }
            }
            catch (Exception ex) 
            {
                errorMessage=ex.Message;
                return;
            }
            
            customerDetail.c_name="";
			customerDetail.address = "";
			customerDetail.tel = "";
			customerDetail.email = "";
			customerDetail.password = "";
            successfullMessage = "Your Registration successfull";

            Response.Redirect("/login/index");

		}
	}
}
