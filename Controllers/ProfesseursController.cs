﻿using Arkance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesseursController(ArkanceTestContext context) : ControllerBase
    {

        // GET: api/Professeurs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Professeur>>> GetProfesseurs()
        {
            return await context.Professeurs.ToListAsync();
        }

        // GET: api/Professeurs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Professeur>> GetProfesseur(int id)
        {
            var professeur = await context.Professeurs.FindAsync(id);

            if (professeur == null)
            {
                return NotFound();
            }

            return professeur;
        }

        // PUT: api/Professeurs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesseur(int id, Professeur professeur)
        {
            if (id != professeur.Id)
            {
                return BadRequest();
            }

            context.Entry(professeur).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
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
        [HttpPost]
        public async Task<ActionResult<Professeur>> PostProfesseur(Professeur professeur)
        {
            context.Professeurs.Add(professeur);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetProfesseur", new { id = professeur.Id }, professeur);
        }

        // DELETE: api/Professeurs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesseur(int id)
        {
            var professeur = await context.Professeurs.FindAsync(id);
            if (professeur == null)
            {
                return NotFound();
            }

            context.Professeurs.Remove(professeur);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesseurExists(int id)
        {
            return context.Professeurs.Any(e => e.Id == id);
        }

    }
}
