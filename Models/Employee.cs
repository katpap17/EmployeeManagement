using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        public long Id { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }

        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        public List<Skill> Skillset {get; set;}

        public Employee()
        {
            this.Skillset = new List<Skill>();
        }
    }
}
