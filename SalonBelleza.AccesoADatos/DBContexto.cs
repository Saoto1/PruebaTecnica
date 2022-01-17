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
        public DbSet<Libros> Libros { get; set; }
        public DbSet<AlquiladosYVendidos> AlquiladosYVendidos { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            //String de conexion a la base remota
            optionsBuilder.UseSqlServer(@"workstation id=Libreria.mssql.somee.com;packet size=4096;user id=SaulValdez_SQLLogin_1;pwd=w3f5h3biaa;data source=Libreria.mssql.somee.com;persist security info=False;initial catalog=Libreria");

            //string de conexion base de datos local
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-O5GJIMR\MSSQLSERVER01;Initial Catalog=Libreria;Integrated Security=True;");
        }




    }
}
