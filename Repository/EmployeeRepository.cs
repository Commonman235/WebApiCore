using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Repository
{
    public class EmployeeRepository :EmployeeInterface
    {
        private readonly IConfiguration _configuration;

        public EmployeeRepository(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        public  List<Employee> GetDetails()
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("AppCon").ToString());
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT  *  FROM EMPLOYEE", con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            List<Employee> list = new List<Employee>();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Employee employee = new Employee();
                    employee.Id = Convert.ToInt16(dt.Rows[i]["id"]);
                    employee.Name = dt.Rows[i]["name"].ToString();
                    employee.Salary = Convert.ToInt64(dt.Rows[i]["salary"]);
                    employee.Department = dt.Rows[i]["department"].ToString();
                    list.Add(employee);
                }


            }
            if (list.Count > 0)
            {
                return list;
            }
            else { return list; }

        }
         
        public string UpdateDetails(Employee employee)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("AppCon").ToString());
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand comm = new SqlCommand("update employee set name='" + employee.Name + "', Department ='" + employee.Department + "', salary =" + employee.Salary + "where id=" + employee.Id, con);
                comm.ExecuteNonQuery();
                con.Close();
                return  "Updated";
            }
            catch (Exception e)
            {
                con.Close();
                throw e;
            }


        }
 
        public string DeleteDetails(int EmployeeId)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("AppCon").ToString());
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand comm = new SqlCommand("Delete from  employee where id=" + EmployeeId, con);
                comm.ExecuteNonQuery();
                con.Close();
                return "Updated";
            }
            catch (Exception e)
            {
                con.Close();
                throw e;
            }

        }
 
        public string CreateEmployee(Employee employee)
        {

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("AppCon").ToString());
            try
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand comm = new SqlCommand("Sp_InsertEmployee", con);
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddWithValue("@name", employee.Name);
                comm.Parameters.AddWithValue("@salary", employee.Salary);
                comm.Parameters.AddWithValue("@dept", employee.Department);
                comm.ExecuteReader();
                con.Close();
                return  "New Employee Added";
            }
            catch (Exception e)
            {
                con.Close();
                throw e;
            }
        }
    }
}
