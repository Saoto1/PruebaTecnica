using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using PruebaTecnica.EntidadesDeNegocio;

namespace PruebaTecnica.AccesoADatos
{
  public   class LibrosDAL
    {

        public static async Task<int> CrearAsync(Libros pLibros)
        {
            int result = 0;
            using (var bdContexto = new DBContexto())
            {
                bdContexto.Add(pLibros);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Libros pLibros)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var libro = await dbContexto.Libros.FirstOrDefaultAsync(s => s.Id == pLibros.Id);
                libro.Titulo = pLibros.Titulo;
                libro.Autor = pLibros.Autor;
                libro.FechaPublicacion = pLibros.FechaPublicacion;
                libro.Genero = pLibros.Genero;
                libro.Estado = pLibros.Estado;
                libro.Precio = pLibros.Precio;
                libro.Otros = pLibros.Otros;
                dbContexto.Update(libro);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Libros pLibros)
        {
            int result = 0;
            using (var bdContexto = new DBContexto())
            {
                var libro = await bdContexto.Libros.FirstOrDefaultAsync(s => s.Id == pLibros.Id);
                bdContexto.Libros.Remove(libro);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Libros> ObtenerPorIdAsync(Libros pLibros)
        {
            var libro = new Libros();
            using (var bdContexto = new DBContexto())
            {
                libro = await bdContexto.Libros.FirstOrDefaultAsync(s => s.Id == pLibros.Id);
            }
            return libro;
        }


        //Metodo para obtener todos los Usuarios en la base de Datos.'
        public static async Task<List<Libros>> ObtenerTodosAsync()
        {
            var usuario = new List<Libros>(); //Variable que va a devolver nuestro metodo, en este caso una Lista.
            using (var dbContexto = new DBContexto())
            {
                usuario = await dbContexto.Libros.ToListAsync();
            }
            return usuario;
        }



        internal static IQueryable<Libros> QuerySelect(IQueryable<Libros> pQuery, Libros pLibros)
        {
            if (pLibros.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pLibros.Id);
            if (!string.IsNullOrWhiteSpace(pLibros.Titulo))
                pQuery = pQuery.Where(s => s.Titulo.Contains(pLibros.Titulo));

            if (!string.IsNullOrWhiteSpace(pLibros.Autor))
                pQuery = pQuery.Where(s => s.Autor.Contains(pLibros.Autor));

            if (pLibros.FechaPublicacion.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pLibros.FechaPublicacion.Year, pLibros.FechaPublicacion.Month, pLibros.FechaPublicacion.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaPublicacion >= fechaInicial && s.FechaPublicacion <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();


            if (!string.IsNullOrWhiteSpace(pLibros.Genero))
                pQuery = pQuery.Where(s => s.Genero.Contains(pLibros.Genero));

            if (pLibros.Estado > 0)
                pQuery = pQuery.Where(s => s.Estado == pLibros.Estado);

            if (pLibros.Precio > 0)
                pQuery = pQuery.Where(s => s.Precio == pLibros.Precio);

            if (!string.IsNullOrWhiteSpace(pLibros.Otros))
                pQuery = pQuery.Where(s => s.Otros.Contains(pLibros.Otros));

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pLibros.Top_Aux > 0)
                pQuery = pQuery.Take(pLibros.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Libros>> BuscarAsync(Libros pLibros)
        {
            var libros = new List<Libros>();
            using (var bdContecto = new DBContexto())
            {
                var select = bdContecto.Libros.AsQueryable();
                select = QuerySelect(select, pLibros);
                libros = await select.ToListAsync();
            }
            return libros;
        }


    }

}
