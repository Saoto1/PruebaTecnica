using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***********************************************************
//Microsoft.EntityFrameworkCora es un using para utilizar la Clase DbContext, la cual nos ayuda para mapear las Entidades(Clases).
using Microsoft.EntityFrameworkCore;
using PruebaTecnica.EntidadesDeNegocio;

namespace PruebaTecnica.AccesoADatos
{
    public class DBContexto : DbContext
    {
        //Los DbSet sirven para Mapeasr las Entidades del proyecto.


        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<Libros> Libro { get; set; }
        public DbSet<AlquiladosYVendidos> AlquiladosYVendidosliente { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           

            //String de conexion a la base remota
            optionsBuilder.UseSqlServer(@"Data Source=198.38.83.200;Initial Catalog=eliqsv_salonbelleza2;Persist Security Info=True;User ID=eliqsv_andromeda;Password=fszlq6vgdxuhecrmoaiw");

        }




    }
}
