using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.IO;

namespace myStore.Pages.admin
{
    public class addFlowerModel : PageModel
    {
        [BindProperty]
        public flowerDetail flowerDetail { get; set; }

        public String errorMessage = "";
        public string successfulMessage = "";

        public void OnGet()
        {

        }

        public void OnPost(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                errorMessage = "Image file is required";
                return;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Generate a random file name
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;

                    // Save the image to the wwwroot/images directory with the unique file name
                    string imagePath = Path.Combine("wwwroot/images", uniqueFileName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        imageFile.CopyTo(stream);
                    }

                    string connectionString = "Data Source=LAPTOP-EDUC1OA9;Initial Catalog=flowerManagement;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "INSERT INTO flower " +
                            "(f_type, f_name, f_color, image, price) VALUES " +
                            "(@f_type, @f_name, @f_color, @image, @price);";

                        using (SqlCommand command = new SqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@f_type", flowerDetail.f_type);
                            command.Parameters.AddWithValue("@f_name", flowerDetail.f_name);
                            command.Parameters.AddWithValue("@f_color", flowerDetail.f_color);
                            command.Parameters.AddWithValue("@image", uniqueFileName); // Save the unique file name
                            command.Parameters.AddWithValue("@price", flowerDetail.price);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return;
                }

                flowerDetail.f_type = "";
                flowerDetail.f_name = "";
                flowerDetail.f_color = "";
                flowerDetail.image = "";
                flowerDetail.price = "";
                successfulMessage = "Your Registration was successful";

                Response.Redirect("/login/index");
            }
        }
    }
}
