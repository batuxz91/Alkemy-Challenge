using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;
using System.Net.Http;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CharactersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CharactersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]

        public ActionResult GetCharacters(string name, int age, int movies)
        {
            using (Models.ApplicationDbContext db = _context)
            {
                if (Request.QueryString.Value.Length > 0)
                {
                    if (Request.QueryString.Value.Contains("name"))
                    {
                        var query = (from character in db.Personaje
                                     where character.Nombre == name
                                     let peliculas = (from pers2 in db.Personaje
                                                   from peli2 in db.Pelicula
                                                   from pelixpers2 in db.PeliculaPersonajes
                                                   where pers2.Id == pelixpers2.PersonajeId & peli2.Id == pelixpers2.PeliculaId & pers2.Id == character.Id
                                                   let genre = new { peli2.Genero.Id, peli2.Genero.Nombre, peli2.Genero.Imagen }
                                                   select new { peli2.Id, peli2.Imagen, peli2.Titulo, peli2.FechaCreacion, peli2.Calificacion, genre }).ToList()
                                     select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia, peliculas }).ToList();

                        if (query.Count == 0)
                            return NotFound();
                        else
                            return Ok(query);
                    }
                    else if (Request.QueryString.Value.Contains("age"))
                    {
                        var query = (from character in db.Personaje
                                     where character.Edad == age
                                     let peliculas = (from pers2 in db.Personaje
                                                   from peli2 in db.Pelicula
                                                   from pelixpers2 in db.PeliculaPersonajes
                                                   where pers2.Id == pelixpers2.PersonajeId & peli2.Id == pelixpers2.PeliculaId & pers2.Id == character.Id
                                                   let genre = new { peli2.Genero.Id, peli2.Genero.Nombre, peli2.Genero.Imagen }
                                                   select new { peli2.Id, peli2.Imagen, peli2.Titulo, peli2.FechaCreacion, peli2.Calificacion, genre }).ToList()
                                     select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia, peliculas }).ToList();

                        if (query.Count == 0)
                            return NotFound();
                        else
                            return Ok(query);
                    }
                    else if (Request.QueryString.Value.Contains("movies"))
                    {
                        var query = (from character in db.Personaje
                                     from peli in db.Pelicula
                                     from movieXcharacter in db.PeliculaPersonajes
                                     from gen in db.Genero
                                     where character.Id == movieXcharacter.PersonajeId & peli.Id == movieXcharacter.PeliculaId & gen.Id == peli.GeneroId & peli.Id == movies
                                     let genre = new { gen.Id, gen.Nombre, gen.Imagen }
                                     let peliculas = new { peli.Id, peli.Titulo, peli.Imagen, peli.FechaCreacion, peli.Calificacion, genre }
                                     select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia, peliculas }).ToList();

                        if (query.Count == 0)
                            return NotFound();
                        else
                            return Ok(query);
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                else
                {
                    var query = (from pers in db.Personaje
                                 select new { pers.Nombre, pers.Imagen }).ToList();
                    return Ok(query);
                }
            }
        }

        // GET: api/Characters/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharactersMovies(int id)
        {
            using (Models.ApplicationDbContext db = _context)
            {
                var chara = await _context.Personaje.FindAsync(id);

                if (chara == null)
                {
                    return NotFound();
                }

                var query = (from character in db.Personaje
                             where character.Id == id
                             let peliculas = (from pers2 in db.Personaje
                                           from peli2 in db.Pelicula
                                           from pelixpers2 in db.PeliculaPersonajes
                                           where pers2.Id == pelixpers2.PersonajeId & peli2.Id == pelixpers2.PeliculaId & pers2.Id == character.Id
                                           let genero = peli2.Genero.Nombre
                                           select new { peli2.Id, peli2.Imagen, peli2.Titulo, peli2.FechaCreacion, peli2.Calificacion, genero }).ToList()
                             select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia, peliculas }).ToList();
                return Ok(query);
            }

        }

        // PUT: api/Characters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCharacters(int id, Personaje characters)
        {
            if (id != characters.Id)
            {
                return BadRequest();
            }

            _context.Entry(characters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CharactersExists(id))
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

        // POST: api/Characters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Personaje>> PostCharacters(Personaje characters)
        {
            _context.Personaje.Add(characters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCharacters", new { id = characters.Id }, characters);
        }

        // DELETE: api/Characters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCharacters(int id)
        {
            var characters = await _context.Personaje.FindAsync(id);
            if (characters == null)
            {
                return NotFound();
            }

            _context.Personaje.Remove(characters);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CharactersExists(int id)
        {
            return _context.Personaje.Any(e => e.Id == id);
        }
    }
}
