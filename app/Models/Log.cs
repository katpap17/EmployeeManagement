using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models
{
    public class Log
    {
        public long Id { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime LogDate { get; set; }

        public string action { get; set; }

        public Employee employee { get; set; }
        public Skill skill { get; set; }
    }
}
