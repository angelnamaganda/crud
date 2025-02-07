using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace CRUD.Pages.Employee
{
    public class Edit : PageModel
    {
        [BindProperty]
       public int Id { get; set; }

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

        public string ErrorMessage { get; private set; } = "";

        public void OnGet(int id)
        {
            try
            {
                string connectionString = @"Server=LAPTOP-04S1TAI7\SQLEXPRESS;database=CrudDb;Trusted_Connection=True;TrustServerCertificate=True";
                
                using (SqlConnection connection = new SqlConnection(connectionString)) {
                    connection.Open();

                    string sql = "SELECT * FROM Employees WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader()){

                            if(reader.Read()){

                                Id = reader.GetInt32(0);
                                FirstName = reader.GetString(1);
                                LastName = reader.GetString(2);
                                Email = reader.GetString(3);
                                Address = reader.GetString(4);
                                Phone_Number = reader.GetString(5);
                            }
                            else{
                                Response.Redirect("/Employee/Index");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public void OnPost(){

            if (!ModelState.IsValid){
                return;
            }

            if (Address == null) Address = "";
            if (Phone_Number == null) Phone_Number = "";
         
            //update employee data
            try
            {
                string connectionString = @"Server=LAPTOP-04S1TAI7\SQLEXPRESS;database=CrudDb;Trusted_Connection=True;TrustServerCertificate=True";
                
                using (SqlConnection connection = new SqlConnection(connectionString)){
                connection.Open();

                    string sql = "UPDATE Employees SET firstname=@firstname, lastname=@lastname, email=@email," + 
                        "address=@address, phone_number=@phone_number WHERE id=@id";

                    using (SqlCommand command = new SqlCommand(sql, connection)){
                        command.Parameters.AddWithValue("@firstname", FirstName);
                        command.Parameters.AddWithValue("@lastname", LastName);
                        command.Parameters.AddWithValue("@email", Email);
                        command.Parameters.AddWithValue("@address", Address);
                        command.Parameters.AddWithValue("@phone_number", Phone_Number);
                        command.Parameters.AddWithValue("@id", Id);

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