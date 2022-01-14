using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//referencias de ensamblado
using SalonBelleza.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace SalonBelleza.AccesoADatos
{
    public class ServicioDAL
    {


        #region CRUD
        //metodo de creacion
        public static async Task<int> CrearAsync(Servicio pServicio)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
              
                dbContexto.Add(pServicio);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //metodo de modificar
        public static async Task<int> ModificarAsync(Servicio pServicio)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var servicio = await dbContexto.Servicio.FirstOrDefaultAsync(s => s.Id == pServicio.Id);
                servicio.Nombre = pServicio.Nombre;
                servicio.Descripcion = pServicio.Descripcion;
                servicio.Precio = pServicio.Precio;
                servicio.Duracion = pServicio.Duracion;

                dbContexto.Update(servicio);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //metodo de eliminacion
        public static async Task<int> EliminarAsync(Servicio pServicio)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var servicio = await dbContexto.Servicio.FirstOrDefaultAsync(s => s.Id == pServicio.Id);
                dbContexto.Servicio.Remove(servicio);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        //busqueda por ID
        public static async Task<Servicio> ObtenerPorIdAsync(Servicio pServicio)
        {
            var servicio = new Servicio();
            using (var dbContexto = new DBContexto())
            {
                servicio = await dbContexto.Servicio.FirstOrDefaultAsync(s => s.Id == pServicio.Id);
            }
            return servicio;
        }

        //obtencion de todos
        public static async Task<List<Servicio>> ObtenerTodosAsync()
        {
            var servicio = new List<Servicio>();
            using (var dbContexto = new DBContexto())
            {
                servicio = await dbContexto.Servicio.ToListAsync();
            }
            return servicio;
        }

        //filtros personalizados usando un Iqueryable con expresiones lambday linQ(en proceso)
        internal static IQueryable<Servicio> QuerySelect(IQueryable<Servicio> pQuery, Servicio pServicio) //los internal solo funcionan en su respectivo namespace 
        {
            if (pServicio.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pServicio.Id);
            if (!string.IsNullOrWhiteSpace(pServicio.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pServicio.Nombre));
            if (!string.IsNullOrWhiteSpace(pServicio.Descripcion))
                pQuery = pQuery.Where(s => s.Descripcion.Contains(pServicio.Descripcion));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pServicio.Top_Aux > 0)
                pQuery = pQuery.Take(pServicio.Top_Aux).AsQueryable();

         return pQuery;
        }

        public static async Task<List<Servicio>> BuscarAsync(Servicio pServicio)
        {
            var servicio = new List<Servicio>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Servicio.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pServicio);
                servicio = await select.ToListAsync();
            }
            return servicio;
        }
        #endregion




    }
}
