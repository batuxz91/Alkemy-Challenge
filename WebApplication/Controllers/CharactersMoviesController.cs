using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CharactersMoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharactersMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CharactersMovies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaPersonaje>>> GetPeliculaPersonajes()
        {
            return await _context.PeliculaPersonajes.ToListAsync();
        }

        // GET: api/CharactersMovies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeliculaPersonaje>> GetPeliculaPersonaje(int id)
        {
            var peliculaPersonaje = await _context.PeliculaPersonajes.FindAsync(id);

            if (peliculaPersonaje == null)
            {
                return NotFound();
            }

            return peliculaPersonaje;
        }

        // PUT: api/CharactersMovies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeliculaPersonaje(int id, PeliculaPersonaje peliculaPersonaje)
        {
            if (id != peliculaPersonaje.PeliculaId)
            {
                return BadRequest();
            }

            _context.Entry(peliculaPersonaje).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeliculaPersonajeExists(id))
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

        // POST: api/CharactersMovies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PeliculaPersonaje>> PostPeliculaPersonaje(PeliculaPersonaje peliculaPersonaje)
        {
            _context.PeliculaPersonajes.Add(peliculaPersonaje);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PeliculaPersonajeExists(peliculaPersonaje.PeliculaId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPeliculaPersonaje", new { id = peliculaPersonaje.PeliculaId }, peliculaPersonaje);
        }

        // DELETE: api/CharactersMovies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePeliculaPersonaje(int id)
        {
            var peliculaPersonaje = await _context.PeliculaPersonajes.FindAsync(id);
            if (peliculaPersonaje == null)
            {
                return NotFound();
            }

            _context.PeliculaPersonajes.Remove(peliculaPersonaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PeliculaPersonajeExists(int id)
        {
            return _context.PeliculaPersonajes.Any(e => e.PeliculaId == id);
        }
    }
}
