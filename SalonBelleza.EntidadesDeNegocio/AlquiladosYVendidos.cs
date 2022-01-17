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

        [Display(Name = "Clientes*")]
        [ForeignKey("Clientes")]
        [Required(ErrorMessage = "Cliente es obligatorio")]
        public int IdCliente{ get; set; }

        [Display(Name = "Libros*")]
        [ForeignKey("Libros")]
        [Required(ErrorMessage = "Titulo de libro es obligatorio")]
        public int IdLibro { get; set; }

        [Display(Name = "Estado*")]
        [Required(ErrorMessage = "Estado es obligatorio")]
        public byte Estado { get; set; }

   
        [Display(Name = "Fecha de alquiler o venta*")]
        public DateTime? Desde { get; set; }

      
        [Display(Name = "Fecha de devolucion(en caso de alquiler)")]
        public DateTime? Hasta { get; set; }

        public Clientes Clientes { get; set; }

        public Libros Libros { get; set; }


        [NotMapped]
        public int Top_Aux { get; set; }

        public enum Estado_Libros
        {
            VENDIDO = 1,
            ALQUILADOado = 2
         
        }

    }
}
