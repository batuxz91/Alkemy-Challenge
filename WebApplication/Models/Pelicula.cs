using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Pelicula
    {
        public int Id { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        public string Titulo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; }

        [Range(1,5,ErrorMessage ="La calificación tiene que ser entre 1-5.")]
        public int Calificacion { get; set; }

        [Required]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }

        public ICollection<PeliculaPersonaje> PeliculaPersonaje { get; set; }
    }
}
