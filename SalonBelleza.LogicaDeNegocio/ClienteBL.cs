using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.AccesoADatos;

namespace SalonBelleza.LogicaDeNegocio
{
    //Soy un cliente.
    //Soy el segundo cliente
    public class ClienteBL
    {
        public async Task<int> CrearAsync(Clientes pCliente) 
        {
            return await ClienteDAL.CrearAsync(pCliente);
        }

        public async Task<int> ModificarAsync(Clientes pCliente) 
        {
            return await ClienteDAL.ModificarAsync(pCliente);
        }
        public async Task<int> EliminarAsync(Clientes pCliente) 
        {
            return await ClienteDAL.EliminarAsync(pCliente);
        }
        public async Task<Clientes> ObtenerPorIdAsync(Clientes pCliente) 
        {
            return await ClienteDAL.ObtenerPorIdAsync(pCliente);
        }
        public async Task<List<Clientes>> ObtenerTodosAsync() 
        {
            return await ClienteDAL.ObtenerTodosAsync();
        }
        public async Task<List<Clientes>> BuscarAsync(Clientes pCliente1) 
        {
            return await ClienteDAL.BuscarAsync(pCliente1);
        }
    }
}
