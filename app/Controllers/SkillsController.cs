using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementApp.Models;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace EmployeeManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {
        private readonly EmployeeSkillContext _context;

        public SkillsController(EmployeeSkillContext context)
        {
            _context = context;
        }

        // GET: api/Skills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Skill>>> GetSkill()
        {
            return await _context.Skill.ToListAsync();
        }

        // GET: api/Skills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Skill>> GetSkill(long id)
        {
            var skill = await _context.Skill.FindAsync(id);

            if (skill == null)
            {
                return NotFound();
            }

            return skill;
        }

        // PUT: api/Skills/{id}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSkill(long id, Skill skill)
        {
            // If id does not match uri id
            if (id != skill.Id)
            {
                return BadRequest();
            }

            _context.Entry(skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // If skill does not exist
                if (!SkillExists(id))
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

        // POST: api/Skills
        [HttpPost]
        public async Task<ActionResult<Skill>> PostSkill(Skill skill)
        {
            skill.DateCreated = DateTime.Now;
            _context.Skill.Add(skill);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSkill), new { id = skill.Id }, skill);
        }

        // DELETE: api/Skills/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSkill(long id)
        {
            var skill = await _context.Skill.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }

            _context.Skill.Remove(skill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/Skills/csv
        [HttpGet("csv")]
        public async Task<ActionResult> GetSkillCSV()
        {
            var skills = await _context.Skill.ToListAsync();
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            var stream = new MemoryStream();
            using (var writeFile = new StreamWriter(stream, leaveOpen: true))
            {
                var csv = new CsvWriter(writeFile, cc);
                csv.WriteRecords(skills);
            }
            return File(stream.ToArray(), "text/csv", $"export_{DateTime.UtcNow.Ticks}.csv");
        }


        private bool SkillExists(long id)
        {
            return _context.Skill.Any(e => e.Id == id);
        }
    }
}
