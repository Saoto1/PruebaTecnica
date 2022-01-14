using SalonBelleza.AccesoADatos;
using SalonBelleza.EntidadesDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonBelleza.LogicaDeNegocio
{
    public class CitaBL
    {
        #region CRUD
        public async Task<int> CrearAsync(Cita pCita)
        {
            return await CitaDAL.CrearAsync(pCita);
        }

        public async Task<int> ModificarAsync(Cita pCita)
        {
            return await CitaDAL.ModificarAsync(pCita);
        }

        public async Task<int> EliminarAsync(Cita pCita)
        {
            return await CitaDAL.EliminarAsync(pCita);
        }

        public async Task<Cita> ObtenerPorIdAsync(Cita pCita)
        {
            return await CitaDAL.ObtenerPorIdAsync(pCita);
        }

        public async Task<List<Cita>> ObtenerTodosAsync()
        {
            return await CitaDAL.ObtenerTodosAsync();
        }

        public async Task<List<Cita>> BuscarAsync(Cita pCita)
        {
            return await CitaDAL.BuscarAsync(pCita);
        }

        #endregion

        public async Task<List<Cita>> BuscarIncluirUsuarioAsync(Cita pCita)
        {
            return await CitaDAL.BuscarIncluirUsuarioAsync(pCita);
        }

        public async Task<List<Cita>> BuscarIncluirClienteAsync(Cita pCita) 
        {
            return await CitaDAL.BuscarIncluirClienteAsync(pCita);
        }

        public async Task<List<Cita>> BuscarIncluirUsuarioClienteAsync(Cita pCita) 
        {
            return await CitaDAL.BuscarIncluirUsuarioClienteAsync(pCita);
        }
    }
}
