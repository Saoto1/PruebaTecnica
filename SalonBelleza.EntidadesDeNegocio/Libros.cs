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

        [Required(ErrorMessage = "El DUI obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Titulo { get; set; }


        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Autor { get; set; }


        [Required(ErrorMessage = "El apellido del cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public DateTime FechaPublicacion { get; set; }


        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public int Genero { get; set; }

        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [Display(Name = "Teléfono")]
        public int Estado { get; set; }


    }
}
