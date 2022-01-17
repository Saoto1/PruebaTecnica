using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.EntidadesDeNegocio
{
   public class Libros
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El Titulo es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "El Autor es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Autor { get; set; }


        [Required(ErrorMessage = "La Fecha de publicacion es obligatoria")]   
        //[Display(Name = "Fecha de publicacion")]
        public DateTime FechaPublicacion { get; set; }


        [Required(ErrorMessage = "El Genero es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        [Display(Name = "Genero")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "El Estado es obligatorio")]
        public byte Estado { get; set; }

        [Required(ErrorMessage = "El Precio es obligatorio")]
        public decimal Precio { get; set; }

   
        public string Otros { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public List<AlquiladosYVendidos> Alquilados { get; set; }

        public enum Estado_Libros
        {
            Disponible = 1,
            Alquilado = 2,
            Vendido = 3
        }


    }
}
