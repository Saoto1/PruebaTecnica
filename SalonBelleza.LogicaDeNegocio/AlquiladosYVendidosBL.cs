using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PruebaTecnica.EntidadesDeNegocio;
using PruebaTecnica.AccesoADatos;

namespace PruebaTecnica.LogicaDeNegocio
{
   public class AlquiladosYVendidosBL
    {

        public async Task<int> CrearAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            return await AlquiladosYVendidosDAL.CrearAsync(pAlquiladosYVendidos);
        }

        public async Task<int> ModificarAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            return await AlquiladosYVendidosDAL.ModificarAsync(pAlquiladosYVendidos);
        }
        public async Task<int> EliminarAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            return await AlquiladosYVendidosDAL.EliminarAsync(pAlquiladosYVendidos);
        }
        public async Task<AlquiladosYVendidos> ObtenerPorIdAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            return await AlquiladosYVendidosDAL.ObtenerPorIdAsync(pAlquiladosYVendidos);
        }

        public async Task<List<AlquiladosYVendidos>> BuscarAsync(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            return await AlquiladosYVendidosDAL.BuscarAsync(pAlquiladosYVendidos);
        }

    }
}
