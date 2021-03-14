using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models
{
    public class EmployeeSkill
    {
        public long id { get; set; }
        public long employeeid { get; set; }
        public long skillid { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastChange { get; set; }

    }
}
