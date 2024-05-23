
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using WebApi.Interface;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeInterface _employeeInterface;

        public EmployeeController( EmployeeInterface employeeInterface)
        { 
            _employeeInterface = employeeInterface;
        }




        [HttpGet]
        [Route("GetEmployee")]
        public ActionResult GetDetails()
        {
            try
            {
                return Ok(_employeeInterface.GetDetails());

            }
            catch (Exception e) { throw e; }

        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public IActionResult UpdateDetails(Employee employee)
        {
            try
            {
                return Ok(_employeeInterface.UpdateDetails(employee));

            }
            catch (Exception e) { throw e; }


        }

        [HttpDelete]
        [Route("DeleteEmployee")]
        public IActionResult DeleteDetails(int EmployeeId)
        {
            try
            {
                return Ok(_employeeInterface.DeleteDetails(EmployeeId));

            }
            catch (Exception e) { throw e; }


        }

        [HttpPost]
        [Route("NewEmployee")]
        public IActionResult CreateEmployee(Employee employee)
        {

            try
            {
                return Ok(_employeeInterface.CreateEmployee(employee));

            }
            catch (Exception e) { throw e; }

        }
    }
}
