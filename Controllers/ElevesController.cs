using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arkance.Models;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevesController : ControllerBase
    {
        private readonly ArkanceTestContext _context;

        public ElevesController(ArkanceTestContext context)
        {
            _context = context;
        }

        // GET: api/Eleves?sorted:=:bool
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Eleve>>> GetEleves([FromQuery] bool sorted)
        {
            if ( sorted)
            {
             var eleves = await _context.Eleves
                    .OrderBy(e => e.Nom)
                    .OrderBy(e => e.Prenom)
                    .ToListAsync();
               return Ok(eleves);
            }
            return await _context.Eleves.ToListAsync();
        }

        // GET: api/Eleves/5?notes=:bool
        [HttpGet("{id}")]
        public async Task<ActionResult> GetEleve(int id, [FromQuery] bool notes)
        {
            var eleve = await _context.Eleves.FindAsync(id);

            if (eleve == null)
            {
                return NotFound();
            }
            if (notes)
            {
                var eleveNotes = await _context.Eleves
                    .Where(e => e.Id == id)
                    .Include(e => e.Notes) 
                    .ThenInclude(m => m.Matiere)
                    .ThenInclude(p => p.Professeurs)
                    .ToListAsync();
                return Ok(eleveNotes);
            }

            return Ok(eleve);
        }

        // PUT: api/Eleves/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEleve(int id, Eleve eleve)
        {
            if (id != eleve.Id)
            {
                return BadRequest();
            }

            _context.Entry(eleve).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

        // POST: api/Eleves
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Eleve>> PostEleve(Eleve eleve)
        {
            _context.Eleves.Add(eleve);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEleve", new { id = eleve.Id }, eleve);
        }

        // DELETE: api/Eleves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEleve(int id)
        {
            var eleve = await _context.Eleves.FindAsync(id);
            if (eleve == null)
            {
                return NotFound();
            }

            _context.Eleves.Remove(eleve);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EleveExists(int id)
        {
            return _context.Eleves.Any(e => e.Id == id);
        }
    }
}
