using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw7_mateen
{
    class Program
    {
        private static SqlConnection _connection;

        static void Main(string[] args)
        {
            string connectionString = ConfigurationSettings.AppSettings["connection_string"];
            string provider = ConfigurationSettings.AppSettings["provider"];
            try
            {
                Console.WriteLine(connectionString);
                _connection = new SqlConnection(connectionString);
                _connection.Open();
                Console.WriteLine(
                    "---------------------------------Employee Management System--------------------------");
                ShowMenu();
                Console.WriteLine(
                    "---------------------------------Developed by Mustafa Mateen------------------------");
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Choose: ");
            Console.WriteLine("1: View All Employees");
            Console.WriteLine("2: Delete an Employee");
            Console.WriteLine("3: Insert an Employee");
            Console.WriteLine("4: Quit");
            Console.Write("Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    ViewAllEmployees();
                    break;
                case 2:
                    DeleteAnEmployee();
                    break;
                case 3:
                    InsertAnEmployee();
                    break;
                case 4:
                    // early return for quit
                    return;
                default:
                    Console.WriteLine("Invalid choice try again");
                    break;
            }

            ShowMenu();
        }

        private static void InsertAnEmployee() {
            Console.Write("Enter Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Employee Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Employee Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Employee Salary: ");
            decimal salary = Convert.ToDecimal(Console.ReadLine());
            Console.Write("Enter Employee Department: ");
            string department = Console.ReadLine();
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = _connection;
            cmd.CommandText = "INSERT INTO employees(emp_id, emp_name, emp_email, emp_salary, emp_department) VALUES(@param1,@param2,@param3, @param4, @param5)";

            cmd.Parameters.AddWithValue("@param1", id);
            cmd.Parameters.AddWithValue("@param2", name);
            cmd.Parameters.AddWithValue("@param3", email);
            cmd.Parameters.AddWithValue("@param4", salary);
            cmd.Parameters.AddWithValue("@param5", department);

            cmd.ExecuteNonQuery(); 
            Console.WriteLine("Employee added");
            
            
        }
        

        private static void DeleteAnEmployee() {
            Console.Write("Enter Employee ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            using (var cmd = _connection.CreateCommand()) {
                cmd.CommandText = "DELETE FROM employees WHERE emp_id = @id";
                cmd.Parameters.AddWithValue("@id", id);  
                cmd.ExecuteNonQuery();
                Console.WriteLine("Employee deleted.");
            }
        }

        private static void ViewAllEmployees() {
            List<Employee> employees = GetAllEmployees();
            View.Print(employees);
        }

        
        private static List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            string oString = "SELECT * FROM employees";
            SqlCommand oCmd = new SqlCommand(oString, _connection);
            using (SqlDataReader oReader = oCmd.ExecuteReader()) {
                while (oReader.Read()) {
                    int id = Convert.ToInt32(oReader["emp_id"].ToString());
                    string name = oReader["emp_name"].ToString();
                    string email = oReader["emp_email"].ToString();
                    decimal salary = Convert.ToDecimal(oReader["emp_salary"].ToString());
                    string department = oReader["emp_department"].ToString();
                    employees.Add(new Employee(id, name, email, salary, department));
                }
            }

            return employees;
        }
    }
}