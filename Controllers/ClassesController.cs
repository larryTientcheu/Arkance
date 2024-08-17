using Arkance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController(ArkanceTestContext context) : ControllerBase
    {

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Classe>>> GetClasses()
        {
            return await context.Classes.ToListAsync();
        }

        // TODO: Lister les élèves par classe.
        // GET: api/Classes/5?eleves=:bool
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClasse(int id, [FromQuery] bool eleves)
        {
            var classe = await context.Classes.FindAsync(id);

            if (classe == null)
            {
                return NotFound();
            }
            if (eleves)
            {
                var eleveParClass = await context.Classes
                    .Where(c => c.Id == id)
                    .Include(c => c.Eleves)
                    .Include(c => c.Professeur)
                    .ToListAsync();

                return Ok(eleveParClass);
            }

            return Ok(classe);
        }


        // PUT: api/Classes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClasse(int id, Classe classe)
        {
            context.Entry(classe).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClasseExists(id))
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

        // POST: api/Classes
        [HttpPost]
        public async Task<ActionResult<Classe>> PostClasse(Classe classe)
        {
            context.Classes.Add(classe);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetClasse", new { id = classe.Id }, classe);
        }

        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClasse(int id)
        {
            var classe = await context.Classes.FindAsync(id);
            if (classe == null)
            {
                return NotFound();
            }

            context.Classes.Remove(classe);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClasseExists(int id)
        {
            return context.Classes.Any(e => e.Id == id);
        }
    }
}
