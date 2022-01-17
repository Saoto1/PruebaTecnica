using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.EntidadesDeNegocio
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Dui*")]
        [Required(ErrorMessage = "El DUI obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Dui { get; set; }

        [Display(Name = "Nombre*")]
        [Required(ErrorMessage = "El nombre del cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Nombre { get; set; }

        [Display(Name = "Apellido*")]
        [Required(ErrorMessage = "El apellido del cliente es obligatorio")]
        [StringLength(50, ErrorMessage = "Maximo 50 caracteres")]
        public string Apellido { get; set; }


        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [Display(Name = "Teléfono*")]
        public int Numero { get; set; }

        [NotMapped]
        public int Top_Aux { get; set; }

        public List<AlquiladosYVendidos> alquiladosYVendidos { get; set; }

        //public List<Cita> Cita { get; set; }
    }
}
