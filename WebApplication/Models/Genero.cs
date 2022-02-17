using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class Genero
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Imagen { get; set; }

        public virtual ICollection<Pelicula> Movies { get; set; }
    }
}
