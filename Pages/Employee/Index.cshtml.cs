using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace CRUD.Pages.Employee
{
    public class Index : PageModel
    {
        public List<EmployeeInfo> EmployeeList { get; set; } = new List<EmployeeInfo>();
        public string SearchTerm { get; set; } = "";

        public void OnGet(string searchTerm)
        {
            SearchTerm = searchTerm;
            try
            {
                string connectionString = @"Server=LAPTOP-04S1TAI7\SQLEXPRESS;database=CrudDb;Trusted_Connection=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "SELECT * FROM Employees WHERE 1=1";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        sql += " AND (FirstName LIKE @Search OR LastName LIKE @Search OR Email LIKE @Search)";
                    }

                    sql += " ORDER BY id DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (!string.IsNullOrEmpty(searchTerm))
                        {
                            command.Parameters.AddWithValue("@Search", "%" + searchTerm + "%");
                        }

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                EmployeeInfo employeeInfo = new EmployeeInfo
                                {
                                    Id = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Address = reader.GetString(4),
                                    Phone_Number = reader.GetString(5),
                                    Date_Recorded = reader.GetDateTime(6).ToString("MM/dd/yyyy")
                                };

                                EmployeeList.Add(employeeInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error Occurred: " + ex.Message);
            }
        }
    }

    public class EmployeeInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone_Number { get; set; } = "";
        public string Date_Recorded { get; set; } = "";
    }
}
