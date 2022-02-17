using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class PeliculaPersonaje
    {
        public int PeliculaId { get; set; }
        public int PersonajeId { get; set; }
        public Pelicula Pelicula { get; set; }
        public Personaje Personaje { get; set; }
    }
}
