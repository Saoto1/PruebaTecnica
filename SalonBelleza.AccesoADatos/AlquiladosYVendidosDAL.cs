using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PruebaTecnica.EntidadesDeNegocio;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.AccesoADatos
{
   public class AlquiladosYVendidosDAL
    {

            #region CRUD
            public static async Task<int> CrearAsync(AlquiladosYVendidos pAlquiladosYVendidos)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {
                    using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                    {
                        try
                        {
                         
                            dbContexto.Add(pAlquiladosYVendidos);
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

            public static async Task<int> ModificarAsync(AlquiladosYVendidos pAlquiladosYVendidos)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {

                    result = await dbContexto.SaveChangesAsync();
                    using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var vendidoyA = await dbContexto.AlquiladosYVendidosliente.FirstOrDefaultAsync(s => s.Id == pAlquiladosYVendidos.Id);
                            vendidoyA.IdCliente = pAlquiladosYVendidos.IdCliente;
                        vendidoyA.IdLibro = pAlquiladosYVendidos.IdLibro;
                        vendidoyA.Estado = pAlquiladosYVendidos.Estado;
                        vendidoyA.Desde = pAlquiladosYVendidos.Desde;
                        vendidoyA.Hasta = pAlquiladosYVendidos.Hasta;                   
                        dbContexto.Update(vendidoyA);                          
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

            public static async Task<int> EliminarAsync(AlquiladosYVendidos pAlquiladosYVendidos)
            {
                int result = 0;
                using (var dbContexto = new DBContexto())
                {
                    using (var transaccion = await dbContexto.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            var vendidoyA = await dbContexto.AlquiladosYVendidosliente.FirstOrDefaultAsync(s => s.Id == pAlquiladosYVendidos.Id);                                                 
                            dbContexto.AlquiladosYVendidosliente.Remove(vendidoyA);
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

            public static async Task<AlquiladosYVendidos> ObtenerPorIdAsync(AlquiladosYVendidos pAlquiladosYVendidos)
            {
                var vendidoyA = new AlquiladosYVendidos();
                using (var dbContexto = new DBContexto())
                {
                vendidoyA = await dbContexto.AlquiladosYVendidosliente.FirstOrDefaultAsync(s => s.Id == pAlquiladosYVendidos.Id);
                }
                return vendidoyA;
            }

            public static async Task<List<AlquiladosYVendidos>> ObtenerTodosAsync()
            {
                var vendidoyA = new List<AlquiladosYVendidos>();
                using (var dbContexto = new DBContexto())
                {
                vendidoyA = await dbContexto.AlquiladosYVendidosliente.ToListAsync();
                }
                return vendidoyA;
            }

        internal static IQueryable<AlquiladosYVendidos> QuerySelect(IQueryable<AlquiladosYVendidos> pQuery, AlquiladosYVendidos pAlquiladosYVendidos) //los internal solo funcionan en su respectivo namespace 
        {
            if (pAlquiladosYVendidos.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pAlquiladosYVendidos.Id);
            if (pAlquiladosYVendidos.IdCliente > 0)
                pQuery = pQuery.Where(s => s.IdCliente == pAlquiladosYVendidos.IdCliente);
            if (pAlquiladosYVendidos.IdLibro > 0)
                pQuery = pQuery.Where(s => s.IdLibro == pAlquiladosYVendidos.IdLibro);
            
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();          
            if (pAlquiladosYVendidos.Estado > 0)
                pQuery = pQuery.Where(s => s.Estado == pAlquiladosYVendidos.Estado);
            if (pAlquiladosYVendidos.Top_Aux > 0)
                pQuery = pQuery.Take(pAlquiladosYVendidos.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<AlquiladosYVendidos>> BuscarAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            var vendidoyA = new List<AlquiladosYVendidos>();
            using (var dbContexto = new DBContexto()) //la palabra using encierra
            {
                var select = dbContexto.AlquiladosYVendidosliente.AsQueryable(); //esto es como un SELECT * FROM
                select = QuerySelect(select, pAlquiladosYVendidos);
                vendidoyA  = await select.ToListAsync();
            }
            return vendidoyA;
        }
        #endregion

        //    public static async Task<List<Cita>> BuscarIncluirUsuarioAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        //    {
        //        var cita = new List<Cita>();
        //        using (var dbContexto = new DBContexto()) //la palabra using encierra
        //        {
        //            var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
        //            select = QuerySelect(select, pCita).Include(s => s.Usuario).AsQueryable();
        //            cita = await select.ToListAsync();
        //        }
        //        return cita;
        //    }

        //    public static async Task<List<Cita>> BuscarIncluirClienteAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        //    {
        //        var cita = new List<Cita>();
        //        using (var dbContexto = new DBContexto()) //la palabra using encierra
        //        {
        //            var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
        //            select = QuerySelect(select, pCita).Include(s => s.Cliente).AsQueryable();
        //            cita = await select.ToListAsync();
        //        }
        //        return cita;
        //    }

        //    public static async Task<List<Cita>> BuscarIncluirUsuarioClienteAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        //    {
        //        var cita = new List<Cita>();
        //        using (var dbContexto = new DBContexto()) //la palabra using encierra
        //        {
        //            var select = dbContexto.Cita.AsQueryable(); //esto es como un SELECT * FROM
        //            select = QuerySelect(select, pCita).Include(s => s.Cliente).AsQueryable();
        //            // Esta linea es para agregar otro incluide si agrega otro mas solo duplicar esta linea y cambiar a la
        //            // clase que se desea incluir en la consulta
        //            select = select.Include(s => s.Usuario).AsQueryable();
        //            cita = await select.ToListAsync();
        //        }
        //        return cita;
    }
}


    

