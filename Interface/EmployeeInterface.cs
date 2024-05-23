using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Interface
{
    public interface EmployeeInterface
    {
        List<Employee> GetDetails();
        string UpdateDetails(Employee employee); 
        string DeleteDetails(int EmployeeId);
        string CreateEmployee(Employee employee);

    }
}
