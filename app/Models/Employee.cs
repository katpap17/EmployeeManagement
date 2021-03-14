using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        // Properties 
        public long Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public virtual ICollection<Skill> Skills {get; set;}

        // Constructor
        public Employee()
        {
            Skills = new HashSet<Skill>();
        }
    }
}
