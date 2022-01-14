using SalonBelleza.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SalonBelleza.AccesoADatos
{
    public class CitaDAL
    {
        #region CRUD
        public static async Task<int> CrearAsync(Cita pCita) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                {
                    try
                    {
                        pCita.FechaRegistrada = DateTime.Now; //Esta fecha se tomara al momento de crearse la cita
                        dbContexto.Add(pCita);
                      //  DetalleCitaDAL.CrearDetalles(dbContexto, pCita.DetalleCita, pCita);
                        result = await dbContexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }
                    catch (Exception ex) 
                    {
                        await transaccion.RollbackAsync();
                        throw ex;
                    }
                }

            }
            return result;
        }

        public static async Task<int> ModificarAsync(Cita pCita) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                
                result = await dbContexto.SaveChangesAsync();
                using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var cita = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pCita.Id);
                        cita.IdUsuario = pCita.IdUsuario;
                        cita.IdCliente = pCita.IdCliente;
                        cita.FechaCita = pCita.FechaCita;
                        cita.Total = pCita.Total;
                        cita.Estado = pCita.Estado;
                        dbContexto.Update(cita);
                        await DetalleCitaDAL.ActualizarDetalles(dbContexto, pCita.DetalleCita, pCita);
                        result = await dbContexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaccion.RollbackAsync();
                        throw ex;
                    }
                }
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Cita pCita)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var cita = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pCita.Id);
                        var detalles= await dbContexto.DetalleCita.Where(s => s.Id == pCita.Id).ToListAsync();
                        if (detalles != null) 
                        {
                            detalles.ForEach(s => s.TipoAccion_Aux = (byte)DetalleCita.TipoAccion.ELIMINAR);
                            await DetalleCitaDAL.ActualizarDetalles(dbContexto, detalles, pCita);
                        }
                        dbContexto.Cita.Remove(cita);
                        result = await dbContexto.SaveChangesAsync();
                        await transaccion.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaccion.RollbackAsync();
                        throw ex;
                    }
                }

            }
            return result;
        }

        public static async Task<Cita> ObtenerPorIdAsync(Cita pCita)
        {
            var cita = new Cita();
            using (var dbContexto = new DBContexto())
            {
                cita = await dbContexto.Cita.FirstOrDefaultAsync(s => s.Id == pCita.Id);
            }
            return cita;
        }

        public static async Task<List<Cita>> ObtenerTodosAsync()
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto())
            {
                cita = await dbContexto.Cita.ToListAsync();
            }
            return cita;
        }

        internal static IQueryable<Cita> QuerySelect(IQueryable<Cita> pQuery, Cita pCita) //los internal solo funcionan en su respectivo namespace 
        {
            if (pCita.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pCita.Id);
            if (pCita.IdUsuario > 0)
                pQuery = pQuery.Where(s => s.IdUsuario == pCita.IdUsuario);
            if (pCita.IdCliente > 0)
                pQuery = pQuery.Where(s => s.IdCliente == pCita.IdCliente);
            if (pCita.FechaRegistrada.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pCita.FechaRegistrada.Year, pCita.FechaRegistrada.Month, pCita.FechaRegistrada.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaRegistrada >= fechaInicial && s.FechaRegistrada <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCita.FechaCita.Year > 1000)
            {
                DateTime fechaInicial = new DateTime(pCita.FechaCita.Year, pCita.FechaCita.Month, pCita.FechaCita.Day, 0, 0, 0);
                DateTime fechaFinal = fechaInicial.AddDays(1).AddMilliseconds(-1);
                pQuery = pQuery.Where(s => s.FechaCita >= fechaInicial && s.FechaCita <= fechaFinal);
            }
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCita.Total > 0)
                pQuery = pQuery.Where(s => s.Total == pCita.Total);
            if (pCita.Estado > 0)
                pQuery = pQuery.Where(s => s.Estado == pCita.Estado);
            if (pCita.Top_Aux > 0)
                pQuery = pQuery.Take(pCita.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Cita>> BuscarAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita);
                cita = await select.ToListAsync();
            }
            return cita;
        }
        #endregion

        public static async Task<List<Cita>> BuscarIncluirUsuarioAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita).Include(s => s.Usuario).AsQueryable();
                cita = await select.ToListAsync();
            }
            return cita;
        }

        public static async Task<List<Cita>> BuscarIncluirClienteAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita).Include(s => s.Cliente).AsQueryable();
                cita = await select.ToListAsync();
            }
            return cita;
        }

        public static async Task<List<Cita>> BuscarIncluirUsuarioClienteAsync(Cita pCita)
        {
            var cita = new List<Cita>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pCita).Include(s => s.Cliente).AsQueryable();
                // Esta linea es para agregar otro incluide si agrega otro mas solo duplicar esta linea y cambiar a la
                // clase que se desea incluir en la consulta
                select = select.Include(s=> s.Usuario).AsQueryable();
                cita = await select.ToListAsync(); 
            }
            return cita;
        }
    }
}
