using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Arkance.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesseursController : ControllerBase
    {
        private readonly ArkanceTestContext _context;

        public ProfesseursController(ArkanceTestContext context)
        {
            _context = context;
        }

        // GET: api/Professeurs
        //GET: api/Professeurs?matieres=:matieresId
            // Liste Professeurs par Matieres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professeur>>> GetProfesseurs([FromQuery] int matieres = 0)
        {    
            return await _context.Professeurs.ToListAsync();  
        }

        // GET: api/Professeurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professeur>> GetProfesseur(int id)
        {
            var professeur = await _context.Professeurs.FindAsync(id);

            if (professeur == null)
            {
                return NotFound();
            }

            return professeur;
        }

        // PUT: api/Professeurs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesseur(int id, Professeur professeur)
        {
            if (id != professeur.Id)
            {
                return BadRequest();
            }

            _context.Entry(professeur).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesseurExists(id))
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

        // POST: api/Professeurs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Professeur>> PostProfesseur(Professeur professeur)
        {
            _context.Professeurs.Add(professeur);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesseur", new { id = professeur.Id }, professeur);
        }

        // DELETE: api/Professeurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesseur(int id)
        {
            var professeur = await _context.Professeurs.FindAsync(id);
            if (professeur == null)
            {
                return NotFound();
            }

            _context.Professeurs.Remove(professeur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesseurExists(int id)
        {
            return _context.Professeurs.Any(e => e.Id == id);
        }

    }
}
