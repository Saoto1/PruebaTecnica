using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PruebaTecnica.EntidadesDeNegocio;
using PruebaTecnica.AccesoADatos;

namespace PruebaTecnica.LogicaDeNegocio
{
  
    public class ClientesBL
    {
        public async Task<int> CrearAsync(Clientes pCliente) 
        {
            return await ClientesDAL.CrearAsync(pCliente);
        }

        public async Task<int> ModificarAsync(Clientes pCliente) 
        {
            return await ClientesDAL.ModificarAsync(pCliente);
        }
        public async Task<int> EliminarAsync(Clientes pCliente) 
        {
            return await ClientesDAL.EliminarAsync(pCliente);
        }
        public async Task<Clientes> ObtenerPorIdAsync(Clientes pCliente) 
        {
            return await ClientesDAL.ObtenerPorIdAsync(pCliente);
        }
       
        public async Task<List<Clientes>> BuscarAsync(Clientes pCliente1) 
        {
            return await ClientesDAL.BuscarAsync(pCliente1);
        }
    }
}
