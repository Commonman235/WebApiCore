 
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using WebApi.Models; 

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

         
        [HttpGet]
        [Route("GetEmployee")]
        public ActionResult GetDetails()
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
                return Ok(list);
            }
            else
            {

                return BadRequest("No Data Found");
            }

        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateDetails(Employee employee)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("AppCon").ToString());
            try
            {
                
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand comm = new SqlCommand("update employee set name='"+ employee.Name+ "', Department ='" + employee.Department+"', salary =" + employee.Salary + "where id=" + employee.Id, con);
                comm.ExecuteNonQuery();
                con.Close();
                return Ok("Updated");
            }
            catch (Exception e)
            {
                con.Close();
                throw e;
            }


        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteDetails(int EmployeeId)
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
                return Ok("Updated");
            }
            catch (Exception e)
            {
                con.Close();
                throw e;
            }
         
        }

        [HttpPost]
        [Route("NewEmployee")]
        public IActionResult CreateEmployee(Employee employee)
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
                return Ok("New Employee Added");
            }
            catch (Exception e)
            {
                con.Close();
                throw e;
            }
        }
    }
}
