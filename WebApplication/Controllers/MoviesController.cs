using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Movies
        [HttpGet]
        public ActionResult Get(string name, int genre, string order)
        {
            using (Models.ApplicationDbContext db = _context)
            {
                if (Request.QueryString.Value.Length > 0)
                {
                    if (Request.QueryString.Value.Contains("name"))
                    {
                        var query = (from movie in db.Pelicula
                                     from gen in db.Genero
                                     where gen.Id == movie.GeneroId & movie.Titulo == name
                                     let genero = new { gen.Id, gen.Nombre, gen.Imagen }
                                     let personajes = (from movie1 in db.Pelicula
                                                       from character in db.Personaje
                                                       from characterXmovie in db.PeliculaPersonajes
                                                       where movie1.Id == characterXmovie.PeliculaId & character.Id == characterXmovie.PersonajeId & movie1.Id == movie.Id
                                                       select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia}).ToList()
                                     select new { movie.Id, movie.Imagen, movie.Titulo, movie.FechaCreacion, movie.Calificacion, genero, personajes }).ToList();
                        
                        if (query.Count == 0)
                            return NotFound();
                        else
                            return Ok(query);
                    }
                    else if (Request.QueryString.Value.Contains("genre"))
                    {
                        var query = (from movie in db.Pelicula
                                     from gen in db.Genero
                                     where gen.Id == movie.GeneroId & gen.Id == genre
                                     let genero = new { gen.Id, gen.Nombre, gen.Imagen }
                                     let personajes = (from movie1 in db.Pelicula
                                                       from character in db.Personaje
                                                       from characterXmovie in db.PeliculaPersonajes
                                                       where movie1.Id == characterXmovie.PeliculaId & character.Id == characterXmovie.PersonajeId & movie1.Id == movie.Id
                                                       select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia }).ToList()
                                     select new { movie.Id, movie.Imagen, movie.Titulo, movie.FechaCreacion, movie.Calificacion, genero, personajes }).ToList();

                        if (query.Count == 0)
                            return NotFound();
                        else
                            return Ok(query);
                    }
                    else if (Request.QueryString.Value.Contains("order"))
                    {
                        if (order == "ASC")
                        {
                            var query = (from movie in db.Pelicula
                                         from gen in db.Genero
                                         where gen.Id == movie.GeneroId
                                         let genero = new { gen.Id, gen.Nombre, gen.Imagen }
                                         orderby movie.FechaCreacion ascending
                                         let personajes = (from movie1 in db.Pelicula
                                                           from character in db.Personaje
                                                           from characterXmovie in db.PeliculaPersonajes
                                                           where movie1.Id == characterXmovie.PeliculaId & character.Id == characterXmovie.PersonajeId & movie1.Id == movie.Id
                                                           select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia }).ToList()
                                         select new { movie.Id, movie.Imagen, movie.Titulo, movie.FechaCreacion, movie.Calificacion, genero, personajes }).ToList();

                            if (query.Count == 0)
                                return NotFound();
                            else
                                return Ok(query);
                        }
                        else if ((order == "DESC"))
                        {
                            var query = (from movie in db.Pelicula
                                         from gen in db.Genero
                                         where gen.Id == movie.GeneroId
                                         let genero = new { gen.Id, gen.Nombre, gen.Imagen }
                                         orderby movie.FechaCreacion descending
                                         let personajes = (from movie1 in db.Pelicula
                                                           from character in db.Personaje
                                                           from characterXmovie in db.PeliculaPersonajes
                                                           where movie1.Id == characterXmovie.PeliculaId & character.Id == characterXmovie.PersonajeId & movie1.Id == movie.Id
                                                           select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia }).ToList()
                                         select new { movie.Id, movie.Imagen, movie.Titulo, movie.FechaCreacion, movie.Calificacion, genero, personajes }).ToList();

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
                        return BadRequest();
                    }

                }
                else
                {
                    var query = (from peli in db.Pelicula
                                 select new { peli.Imagen, peli.Titulo, peli.FechaCreacion}).ToList();
                    return Ok(query);
                }
            }

        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pelicula>> GetMovies(int id)
        {
            using (Models.ApplicationDbContext db = _context)
            {
                var pelicula = await _context.Pelicula.FindAsync(id);

                if (pelicula == null)
                {
                    return NotFound();
                }

                var query = (from movie in db.Pelicula
                             from gen in db.Genero
                             where gen.Id == movie.GeneroId & movie.Id == id
                             let genre = new { gen.Id, gen.Nombre, gen.Imagen }
                             let personajes = (from movie1 in db.Pelicula
                                               from character in db.Personaje
                                               from characterXmovie in db.PeliculaPersonajes
                                               where movie1.Id == characterXmovie.PeliculaId & character.Id == characterXmovie.PersonajeId & movie1.Id == movie.Id
                                               select new { character.Id, character.Nombre, character.Imagen, character.Edad, character.Peso, character.Historia }).ToList()
                             select new { movie.Id, movie.Imagen, movie.Titulo, movie.FechaCreacion, movie.Calificacion, genre, personajes }).ToList();
                return Ok(query);
            }
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovies(int id, Pelicula movies)
        {
            if (id != movies.Id)
            {
                return BadRequest();
            }

            _context.Entry(movies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pelicula>> PostMovies(Pelicula movies)
        {
            _context.Pelicula.Add(movies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovies", new { id = movies.Id }, movies);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovies(int id)
        {
            var movies = await _context.Pelicula.FindAsync(id);
            if (movies == null)
            {
                return NotFound();
            }

            _context.Pelicula.Remove(movies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoviesExists(int id)
        {
            return _context.Pelicula.Any(e => e.Id == id);
        }
    }
}
