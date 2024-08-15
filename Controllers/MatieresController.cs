using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arkance.Models;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatieresController : ControllerBase
    {
        private readonly ArkanceTestContext _context;

        public MatieresController(ArkanceTestContext context)
        {
            _context = context;
        }

        // GET: api/Matieres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matiere>>> GetMatieres()
        {
            return await _context.Matieres.ToListAsync();
        }

        [HttpGet("professeurs")]
        public async Task<ActionResult<IEnumerable<Professeurs_MatieresDto>>> GetProfParMAtieres()
        {
            var profMat = await _context.Matieres
                .Include(m => m.Professeurs)            
                .AsNoTracking()
                .ToListAsync();
            return Ok(profMat);
        }

        // GET: api/Matieres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matiere>> GetMatiere(int id)
        {
            var matiere = await _context.Matieres.FindAsync(id);

            if (matiere == null)
            {
                return NotFound();
            }

            return matiere;
        }

        // PUT: api/Matieres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatiere(int id, Matiere matiere)
        {
            if (id != matiere.Id)
            {
                return BadRequest();
            }

            _context.Entry(matiere).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matiere>> PostMatiere(Matiere matiere)
        {
            _context.Matieres.Add(matiere);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatiere", new { id = matiere.Id }, matiere);
        }

        // DELETE: api/Matieres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatiere(int id)
        {
            var matiere = await _context.Matieres.FindAsync(id);
            if (matiere == null)
            {
                return NotFound();
            }

            _context.Matieres.Remove(matiere);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatiereExists(int id)
        {
            return _context.Matieres.Any(e => e.Id == id);
        }
    }
}
