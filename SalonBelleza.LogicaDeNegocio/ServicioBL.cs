using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//*-----------------------------
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.AccesoADatos;

namespace SalonBelleza.LogicaDeNegocio
{
    public class ServicioBL
    {
        public async Task<int> CrearAsync(Servicio pServicio)
        {
            return await ServicioDAL.CrearAsync(pServicio);
        }

        public async Task<int> ModificarAsync(Servicio pServicio)
        {
            return await ServicioDAL.ModificarAsync(pServicio);
        }

        public async Task<int> EliminarAsync(Servicio pServicio)
        {
            return await ServicioDAL.EliminarAsync(pServicio);
        }

        public async Task<Servicio> ObtenerPorIdAsync(Servicio pServicio)
        {
            return await ServicioDAL.ObtenerPorIdAsync(pServicio);
        }

        public async Task<List<Servicio>> ObtenerTodosAsync()
        {
            return await ServicioDAL.ObtenerTodosAsync();
        }

        public async Task<List<Servicio>> BuscarAsync(Servicio pServicio)
        {
            return await ServicioDAL.BuscarAsync(pServicio);
        }



    }
}
