using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnica.EntidadesDeNegocio
{
  public  class AlquiladosYVendidos
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Clientes")]
        [Required(ErrorMessage = "IdCliente es obligatorio")]
        public int IdCliente{ get; set; }

        [ForeignKey("Libros")]
        [Required(ErrorMessage = "IdLibro es obligatorio")]
        public int IdLibro { get; set; }


        [Required(ErrorMessage = "El total es obligatorio")]
        public byte Estado { get; set; }

        [Required(ErrorMessage = "El total es obligatorio")]
        [Display(Name = "Fecha De Entrega")]
        public DateTime Desde { get; set; }

        [Required(ErrorMessage = "El total es obligatorio")]
        [Display(Name = "Fecha De Devolucion")]
        public DateTime Hasta { get; set; }

        public Clientes Clientes { get; set; }

        public Libros Libros { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }

        public enum Estado_Libros
        {
            Disponible = 1,
            Alquilado= 2,
            Vendido=3
        }

    }
}
