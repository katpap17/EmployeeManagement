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
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeSkillContext _context;

        public EmployeesController(EmployeeSkillContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Getemployee()
        {
            return await _context.employee.ToListAsync();
        }

        // GET: api/Employees/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(long id)
        {
            var employee = await _context.employee
                .Where(emp => emp.Id == id) // find employee by id
                .FirstOrDefaultAsync();  

            // if employee does not exist
            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
            employee.HireDate = DateTime.Now;
            // Add employee
            _context.employee.Add(employee);
           
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            var employee = await _context.employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(long id)
        {
            return _context.employee.Any(e => e.Id == id);
        }

        // POST: api/Employees/5/addskills
        [HttpPost("{id}/Skills")]
        public async Task<IActionResult> PostEmployeeSkills(long id, Skill skill)
        {
            // Create employee-skill association
            EmployeeSkill empskill = new EmployeeSkill();
            empskill.employeeid = id;
            empskill.skillid = skill.Id;
            empskill.LastChange = DateTime.Now;
            _context.employeeSkill.Add(empskill);

            // Create log
            Log log = new Log();
            log.employeeid = id;
            log.skillid = skill.Id;
            log.LogDate = DateTime.Now;
            log.action = "add";
            _context.log.Add(log);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("PostEmployeeSkills", new { id = empskill.id }, empskill);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return NoContent();

        }

        // Get: api/Employees/5/getskills
        [HttpGet("{id}/Skills")]
        public async Task<ActionResult<IEnumerable<Skill>>> GetEmployeeSkills(long id)
        {

            var employeeskill = await _context.employeeSkill
                .Where(empsk => empsk.employeeid == id) // find skills by employee id
                .ToListAsync();

            List<Skill> skillList = new List<Skill>();
            foreach (EmployeeSkill empskill in employeeskill)
            {
                var skill = await _context.Skill.FindAsync(empskill.skillid);
                skillList.Add(skill);
            }
            // if employee does not exist
            if (skillList == null)
            {
                return NotFound();
            }

            return skillList;

        }
        // Delete: api/Employees/5/deleteskills
        [HttpDelete("{id}/Skills")]
        public async Task<IActionResult> DeleteEmployeeSkills(long id, Skill skill)
        {
            // Find employee by id
            var employeeskill = await _context.employeeSkill
                .Where(empsk => empsk.employeeid == id && empsk.skillid == skill.Id)
                .FirstOrDefaultAsync();

            if (employeeskill == null)
            {
                return NotFound();
            }

            // Remove employee
            _context.employeeSkill.Remove(employeeskill);

            // Create log
            Log log = new Log();
            log.employeeid = id;
            log.skillid = skill.Id;
            log.LogDate = DateTime.Now;
            log.action = "remove";
            _context.log.Add(log);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
