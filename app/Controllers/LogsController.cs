using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly EmployeeSkillContext _context;

        public LogsController(EmployeeSkillContext context)
        {
            _context = context;
        }

        // GET: api/Logs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Log>>> Getlog()
        {
            return await _context.log
                .ToListAsync();

        }

        
        private bool LogExists(long id)
        {
            return _context.log.Any(e => e.Id == id);
        }
    }
}
