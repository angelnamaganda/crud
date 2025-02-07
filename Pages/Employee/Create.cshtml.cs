using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Employee
{
    public class Create : PageModel
    {

        [BindProperty, Required(ErrorMessage = "Required")]
       public string FirstName { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Required")]
       public string LastName{ get; set; } = "";

        [BindProperty, Required, EmailAddress]
       public string Email { get; set; } = "";

        [BindProperty, Required(ErrorMessage = "Required")]
       public string Address { get; set; } = "";

        [BindProperty, Required, Phone]
       public string Phone_Number { get; set; } = "";

        public void OnGet()
        {
        }
 
        public string ErrorMessage { get; private set; } = "";


        public void OnPost(){

            
            if (!ModelState.IsValid){
                return;
            }

            if (Phone_Number == null) Phone_Number = "";
            if (Address == null) Address = "";
            
            //create new employee

            try
            {
                string connectionString = @"Server=LAPTOP-04S1TAI7\SQLEXPRESS;database=CrudDb;Trusted_Connection=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString)){
                    connection.Open();

                   string sql = "INSERT INTO Employees (firstname, lastname, email, address, phone_number) VALUES " +
                                "(@firstname, @lastname, @email, @address, @phone_number)";


                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@firstname", FirstName);
                        command.Parameters.AddWithValue("@lastname", LastName);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@address", Address);
                        command.Parameters.AddWithValue("@phone_number", Phone_Number);

                        command.ExecuteNonQuery();
                    } 
                            
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message; 
                return;               
            }

            Response.Redirect("/Employee/Index");
        }
    }
}