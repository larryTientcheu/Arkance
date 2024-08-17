using Arkance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatieresController(ArkanceTestContext context) : ControllerBase
    {

        // GET: api/Matieres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatieres()
        {
            return await context.Matieres.ToListAsync();
        }

        // TODO: Lister les professeurs par matière enseignée.
        //GET: api/Matieres/Professeurs
        [HttpGet("professeurs")]
        public async Task<ActionResult<IEnumerable<Professeurs_MatieresDto>>> GetProfParMAtieres()
        {
            var profMat = await context.Matieres
                .Include(m => m.Professeurs)
                .AsNoTracking()
                .ToListAsync();
            return Ok(profMat);
        }

        // GET: api/Matieres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            var matiere = await context.Matieres.FindAsync(id);

            if (matiere == null)
            {
                return NotFound();
            }

            return matiere;
        }

        // PUT: api/Matieres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatiere(int id, Matiere matiere)
        {
            if (id != matiere.Id)
            {
                return BadRequest();
            }

            context.Entry(matiere).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatiereExists(id))
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

        // POST: api/Matieres
        [HttpPost]
        public async Task<ActionResult<Matiere>> PostMatiere(Matiere matiere)
        {
            context.Matieres.Add(matiere);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetMatiere", new { id = matiere.Id }, matiere);
        }

        // DELETE: api/Matieres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatiere(int id)
        {
            var matiere = await context.Matieres.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }

            context.Matieres.Remove(matiere);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatiereExists(int id)
        {
            return context.Matieres.Any(e => e.Id == id);
        }
    }
}
