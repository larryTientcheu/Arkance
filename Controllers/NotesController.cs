﻿using Arkance.Interface;
using Arkance.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arkance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController(ArkanceTestContext context) : ControllerBase
    {

        // GET: api/Notes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Note>>> GetNotes()
        {
            return await context.Notes.ToListAsync();
        }

        // GET: api/Notes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> GetNote(int id)
        {
            var note = await context.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return note;
        }

        // TODO: Modifier une note d’un élève
        // PUT: api/Notes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNote(int id, Note note)
        {
            context.Entry(note).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!NoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, ex.InnerException?.Message); ;
                }
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }

            return NoContent();


        }

        //TODO: Ajouter une note d’un élève.
        // POST: api/Notes
        [HttpPost]
        public async Task<ActionResult<Note>> PostNote(Note note)
        {
            try
            {
                var helper = new Helpers();
                if (note.Appreciation is null)
                    note.Appreciation = helper.SetAppreciations(note.Valeur.Value);               
                context.Notes.Add(note);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetNote", new { id = note.Id }, note);
            }
            catch (DbUpdateException)
            {
                return BadRequest("The note value must be between 0 and 20");
            }


        }

        // DELETE: api/Notes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            try
            {
                var note = await context.Notes.FindAsync(id);
                if (note == null)
                {
                    return NotFound();
                }

                context.Notes.Remove(note);
                await context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException?.Message);
            }
        }

        private bool NoteExists(int id)
        {
            return context.Notes.Any(e => e.Id == id);
        }


    }
}
