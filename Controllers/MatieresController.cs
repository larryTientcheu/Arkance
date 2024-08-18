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
        // GET: api/Matieres/5?professeurs=:=bool
        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id, [FromQuery] bool professeurs)
        {
            var matiere = await context.Matieres.FindAsync(id);

            if (matiere == null)
            {
                return NotFound();
            }
            if (professeurs)
            {
                var profParMat = await context.Matieres
                    .Where(m => m.Id == id)
                    .Include(p => p.Professeurs)
                    .AsNoTracking()
                    .ToListAsync();
                return Ok(profParMat);
            }

            return Ok(matiere);
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
            catch (DbUpdateConcurrencyException e)
            {
                if (!MatiereExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(e.InnerException?.Message);
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

            try
            {
                context.Matieres.Remove(matiere);
                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                return BadRequest(e.InnerException?.Message);
            }

            return NoContent();
        }

        private bool MatiereExists(int id)
        {
            return context.Matieres.Any(e => e.Id == id);
        }
    }
}
