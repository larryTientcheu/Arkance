using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arkance.Models;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevesController(ArkanceTestContext context) : ControllerBase
    {

        //TODO: Lister tous les élèves (trié dans l’ordre alphabétique par nom puis prénom).
        // GET: api/Eleves?sorted:=:bool
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Eleve>>> GetEleves([FromQuery] bool sorted)
        {
            if ( sorted)
            {
             var eleves = await context.Eleves
                    .OrderBy(e => e.Nom)
                    .OrderBy(e => e.Prenom)
                    .ToListAsync();
               return Ok(eleves);
            }
            return await context.Eleves.ToListAsync();
        }

        //TODO: Lister les notes d’un élève.
        // GET: api/Eleves/5?notes=:bool
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEleve(int id, [FromQuery] bool notes)
        {
            var eleve = await context.Eleves.FindAsync(id);

            if (eleve == null)
            {
                return NotFound();
            }
            if (notes)
            {
                var eleveNotes = await context.Eleves
                    .Where(e => e.Id == id)
                    .Include(e => e.Notes) 
                    .ThenInclude(m => m.Matiere)
                    .ThenInclude( p => p.Professeurs)
                    .ToListAsync();
                return Ok(eleveNotes);
            }

            return Ok(eleve);
        }

        // PUT: api/Eleves/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEleve(int id, Eleve eleve)
        {
          

            context.Entry(eleve).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EleveExists(id))
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

        // TODO: Ajouter un élève.
        // POST: api/Eleves
        [HttpPost]
        public async Task<ActionResult<Eleve>> PostEleve(Eleve eleve)
        {
            context.Eleves.Add(eleve);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetEleve", new { id = eleve.Id }, eleve);
        }

        // DELETE: api/Eleves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEleve(int id)
        {
            var eleve = await context.Eleves.FindAsync(id);
            if (eleve == null)
            {
                return NotFound();
            }

            context.Eleves.Remove(eleve);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool EleveExists(int id)
        {
            return context.Eleves.Any(e => e.Id == id);
        }
    }
}
