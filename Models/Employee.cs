﻿using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Employee
    {  
            public int?  Id { get; set; }

            [Required]
            public string Name { get; set; }

            public decimal Salary { get; set; }

            public string Department { get; set; }
        
    }
}
