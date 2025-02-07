using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Employee
{
    public class Delete : PageModel
    {

        public void OnGet()
        {
        }

        public void OnPost(int id){

            deleteEmployee(id);
            
            Response.Redirect("/Employee/Index");
        }


        private void deleteEmployee(int id){

                string connectionString = @"Server=LAPTOP-04S1TAI7\SQLEXPRESS;database=CrudDb;Trusted_Connection=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                    string sql = "DELETE FROM Employees WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection)){

                        command.Parameters.AddWithValue("id", id);

                        command.ExecuteNonQuery();
                    }

                }
        }
    }
}