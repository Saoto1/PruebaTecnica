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
    public class DetalleCitaDAL
    {
        //Uy
        //comentario
             #region CRUD
            //metodo de creacion
            public static async Task<int> CrearAsync(DetalleCita pDetalleCita)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {

                    dbContexto.Add(pDetalleCita);
                    result = await dbContexto.SaveChangesAsync();
                }
                return result;
            }

            //metodo de modificar
            public static async Task<int> ModificarAsync(DetalleCita pDetalleCita)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {
                    var detallecita = await dbContexto.DetalleCita.FirstOrDefaultAsync(s => s.Id == pDetalleCita.Id);
                    detallecita.IdCita = pDetalleCita.IdCita;
                    detallecita.IdServicio = pDetalleCita.IdServicio;
                    detallecita.Precio = pDetalleCita.Precio;
                    detallecita.Duracion = pDetalleCita.Duracion;

                dbContexto.Update(detallecita);
                    result = await dbContexto.SaveChangesAsync();
                }
                return result;
            }

            //metodo de eliminacion
            public static async Task<int> EliminarAsync(DetalleCita pDetalleCita)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {
                    var servicio = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pDetalleCita.Id);
                    dbContexto.DetalleCita.Remove(pDetalleCita);
                    result = await dbContexto.SaveChangesAsync();
                }
                return result;
            }

            //busqueda por ID
            public static async Task<DetalleCita> ObtenerPorIdAsync(DetalleCita pDetalleCita)
            {
                var detallecita = new DetalleCita();
                using (var dbContexto = new DBContexto())
                {
                    detallecita = await dbContexto.DetalleCita.FirstOrDefaultAsync(s => s.Id == pDetalleCita.Id);
                }
                return detallecita;
            }

            //obtencion de todos
            public static async Task<List<DetalleCita>> ObtenerTodosAsync()
            {
                var detallecita = new List<DetalleCita>();
                using (var dbContexto = new DBContexto())
                {
                    detallecita = await dbContexto.DetalleCita.ToListAsync();
                }
                return detallecita;
            }

            //filtros personalizados usando un Iqueryable con expresiones lambday linQ(en proceso)
            internal static IQueryable<DetalleCita> QuerySelect(IQueryable<DetalleCita> pQuery, DetalleCita pDetalleCita) //los internal solo funcionan en su respectivo namespace 
            {
                if (pDetalleCita.Id > 0)
                    pQuery = pQuery.Where(s => s.Id == pDetalleCita.Id);
                if (pDetalleCita.IdCita > 0)
                    pQuery = pQuery.Where(s => s.IdCita == pDetalleCita.IdCita);
                if (pDetalleCita.IdServicio > 0)
                    pQuery = pQuery.Where(s => s.IdServicio == pDetalleCita.IdServicio);
                if (pDetalleCita.Precio> 0)
                    pQuery = pQuery.Where(s => s.Precio == pDetalleCita.Precio);

                pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
                if (pDetalleCita.Top_Aux > 0)
                    pQuery = pQuery.Take(pDetalleCita.Top_Aux).AsQueryable();

                return pQuery;
            }

            public static async Task<List<DetalleCita>> BuscarAsync(DetalleCita pDetalleCita)
            {
                var detallecita = new List<DetalleCita>();
                using (var dbContexto = new DBContexto()) //la palabra using encierra
                {
                    var select = dbContexto.DetalleCita.AsQueryable(); //esto es como un SELECT * FROM
                    select = QuerySelect(select, pDetalleCita);
                    detallecita = await select.ToListAsync();
                }
                return detallecita;
            }
        public static async Task<List<DetalleCita>> BuscarIncluirServicioAsync(DetalleCita pDetalleCita)
        {
            var detallecita = new List<DetalleCita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.DetalleCita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pDetalleCita).Include(s=> s.Servicio);
                detallecita = await select.ToListAsync();
            }
            return detallecita;
        }
        #endregion

        public static void CrearDetalles(DBContexto pContext,List<DetalleCita> pDetalles, Cita pCita) 
        {
            if (pDetalles != null && pDetalles.Count > 0)
            {
                foreach (var item in pDetalles)
                {
                    item.IdCita = pCita.Id;
                    pContext.Add(item);
                }
            }
        }

        public static async Task ActualizarDetalles(DBContexto pContext, List<DetalleCita> pDetalles, Cita pCita) 
        {
            if (pDetalles != null && pDetalles.Count() > 0)
            {
                foreach (var item in pDetalles)
                {
                    if (item.TipoAccion_Aux == (byte)DetalleCita.TipoAccion.NUEVO)
                    {
                        item.IdCita = pCita.Id;
                        pContext.Add(item);
                    }
                    else if (item.TipoAccion_Aux == (byte)DetalleCita.TipoAccion.MODIFICAR && item.Id > 0)
                    {
                        var detallecita = await pContext.DetalleCita.FirstOrDefaultAsync(s => s.Id == item.Id);
                        detallecita.Precio = item.Precio;
                        detallecita.Duracion = item.Duracion;
                        pContext.Update(detallecita);
                    }
                    else if (item.TipoAccion_Aux == (byte)DetalleCita.TipoAccion.ELIMINAR && item.Id > 0)
                    {
                        var detallecita = await pContext.DetalleCita.FirstOrDefaultAsync(s => s.Id == item.Id);
                        pContext.DetalleCita.Remove(detallecita);
                    }
                }
            }
        }


    }
}
