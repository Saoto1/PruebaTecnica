using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//******************************
using Microsoft.EntityFrameworkCore;
using SalonBelleza.EntidadesDeNegocio;
namespace SalonBelleza.AccesoADatos
{
    public class RolDAL
    {
        public static async Task<int> CrearAsync(Rol pRol) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                dbContexto.Add(pRol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> ModificarAsync(Rol pRol) 
        {
            int result = 0;
            using (var dbContexto = new DBContexto()) 
            {
                var rol = await dbContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                rol.Nombre = pRol.Nombre;
                dbContexto.Update(rol);
                result = await dbContexto.SaveChangesAsync();
            }
            return result;
        }

        public static async Task<int> EliminarAsync(Rol pRol)
        {
            int result = 0;
            using (var bdContexto = new DBContexto())
            {
                var rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
                bdContexto.Rol.Remove(rol);
                result = await bdContexto.SaveChangesAsync();
            }
            return result;
        }
        
        public static async Task<Rol> ObtenerPorIdAsync(Rol pRol) 
        {
            var rol = new Rol();
            using (var bdContexto = new DBContexto()) 
            {
                rol = await bdContexto.Rol.FirstOrDefaultAsync(s => s.Id == pRol.Id);
            }
            return rol;
        }

        public static async Task<List<Rol>> ObtenerTodosAsync() 
        {
            var roles = new List<Rol>();
            using (var bdContexto = new DBContexto()) 
            {
                roles = await bdContexto.Rol.ToListAsync();
            }
            return roles;
        }
        
        internal static IQueryable<Rol> QuerySelect(IQueryable<Rol> pQuery, Rol pRol1)
        {
            if (pRol1.Id > 0)
                pQuery = pQuery.Where(s => s.Id == pRol1.Id);
            if (!string.IsNullOrWhiteSpace(pRol1.Nombre))
                pQuery = pQuery.Where(s => s.Nombre.Contains(pRol1.Nombre));
            pQuery = pQuery.OrderByDescending(s => s.Id).AsQueryable();
            if (pRol1.Top_Aux > 0)
                pQuery = pQuery.Take(pRol1.Top_Aux).AsQueryable();
            return pQuery;
        }

        public static async Task<List<Rol>> BuscarAsync (Rol pRol) 
        {
            var roles = new List<Rol>();
            using (var bdContecto = new DBContexto()) 
            {
                var select = bdContecto.Rol.AsQueryable();
                select = QuerySelect(select, pRol);
                roles = await select.ToListAsync();
            }
            return roles;
        }
        
    }   
}
