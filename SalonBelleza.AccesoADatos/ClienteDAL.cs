using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//***************
using Microsoft.EntityFrameworkCore;
using SalonBelleza.EntidadesDeNegocio;
namespace SalonBelleza.AccesoADatos
{
    public class ClienteDAL
    {
        public static async Task<int> CrearAsync(Clientes pCliente) 
        {
            int result = 0;
            using (var bdContexto = new DBContexto()) 
            {
                bdContexto.Add(pCliente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Clientes pCliente)
        {
            int result = 0;
            using (var dbContexto = new DBContexto())
            {
                var cliente = await dbContexto.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
                cliente.Nombre = pCliente.Nombre;
                cliente.Apellido = pCliente.Apellido;
                cliente.Numero = pCliente.Numero;
                cliente.Dui = pCliente.Dui;
                dbContexto.Update(cliente);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Clientes pCliente)
        {
            int result = 0;
            using (var bdContexto = new DBContexto())
            {
                var cliente = await bdContexto.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
                bdContexto.Cliente.Remove(cliente);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<Clientes> ObtenerPorIdAsync(Clientes pCliente)
        {
            var cliente = new Clientes();
            using (var bdContexto = new DBContexto())
            {
                cliente = await bdContexto.Cliente.FirstOrDefaultAsync(s => s.Id == pCliente.Id);
            }
            return cliente;
        }

        public static async Task<List<Clientes>> ObtenerTodosAsync()
        {
            var clientes = new List<Clientes>();
            using (var bdContexto = new DBContexto())
            {
                clientes = await bdContexto.Cliente.ToListAsync();
            }
            return clientes;
        }

        internal static IQueryable<Clientes> QuerySelect(IQueryable<Clientes> pQuery, Clientes pCliente)
        {
            if (pCliente.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pCliente.Id);
            if (!string.IsNullOrWhiteSpace(pCliente.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pCliente.Nombre));

            if (!string.IsNullOrWhiteSpace(pCliente.Dui))
                pQuery = pQuery.Where(s => s.Dui.Contains(pCliente.Dui));

            if (!string.IsNullOrWhiteSpace(pCliente.Apellido))
                pQuery = pQuery.Where(s => s.Apellido.Contains(pCliente.Apellido));

            if (pCliente.Numero > 0)
                pQuery = pQuery.Where(s => s.Numero == pCliente.Numero);

            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pCliente.Top_Aux > 0)
                pQuery = pQuery.Take(pCliente.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Clientes>> BuscarAsync(Clientes pCliente)
        {
            var clientes = new List<Clientes>();
            using (var bdContecto = new DBContexto())
            {
                var select = bdContecto.Cliente.AsQueryable();
                select = QuerySelect(select, pCliente);
                clientes = await select.ToListAsync();
            }
            return clientes;
        }

    }
}
