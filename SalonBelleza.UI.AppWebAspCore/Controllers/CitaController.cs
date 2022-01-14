using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.LogicaDeNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SalonBelleza.UI.AppWebAspCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class CitaController : Controller
    {
        CitaBL citaBL = new CitaBL();
        ClienteBL clienteBL = new ClienteBL();
        UsuarioBL usuarioBL = new UsuarioBL();
        ServicioBL servicioBL = new ServicioBL();
        DetalleCitaBL detalleCitaBL = new DetalleCitaBL();
        // GET: CitaController
        public async Task<IActionResult> Index(Cita pCita = null)
        {
            if (pCita == null)
                pCita = new Cita();
            if (pCita.Top_Aux == 0)
                pCita.Top_Aux = 10;
            else if (pCita.Top_Aux == -1)
                pCita.Top_Aux = 0;
            var taskBuscarCliente = citaBL.BuscarIncluirClienteAsync(pCita);
            var taskObtenerTodosClientes = clienteBL.ObtenerTodosAsync();
            var taskBuscarUsuario = citaBL.BuscarIncluirUsuarioAsync(pCita);
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var taskBuscarUsuarioCliente = citaBL.BuscarIncluirUsuarioClienteAsync(pCita);
            var citas = await taskBuscarUsuarioCliente;
            ViewBag.Top = pCita.Top_Aux;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            return View(citas);
        }

        // GET: CitaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cita = await citaBL.ObtenerPorIdAsync(new Cita { Id = id });
            var taskCliente = clienteBL.ObtenerPorIdAsync(new Clientes { Id = cita.IdCliente });
            var taskUsuario= usuarioBL.ObtenerPorIdAsync(new Usuario { Id = cita.IdUsuario });
            var taskDetalles = detalleCitaBL.BuscarIncluirServicioAsync(new DetalleCita { IdCita = cita.Id });
            cita.Cliente = await taskCliente;
            cita.Usuario = await taskUsuario;
            cita.DetalleCita = await taskDetalles;
            return View(cita);
        }

        // GET: CitaController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = await clienteBL.ObtenerTodosAsync();
            ViewBag.Usuarios = await usuarioBL.ObtenerTodosAsync();
            ViewBag.servicios = await servicioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: CitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cita pCita)
        {
            try
            {
               // int result = 0;
                int result = await citaBL.CrearAsync(pCita);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Clientes = await clienteBL.ObtenerTodosAsync();
                ViewBag.Usuarios = await usuarioBL.ObtenerTodosAsync();
                ViewBag.servicios = await servicioBL.ObtenerTodosAsync();
                return View(pCita); //si ocurrio un error se redirecciona a la vista create con el parametro
            }
        }

        // GET: CitaController/Edit/5
        public async Task<IActionResult> Edit(Cita pCita)
        {
            var taskObtenerPorId = citaBL.ObtenerPorIdAsync(pCita);
            var taskObtenerTodosClientes = clienteBL.ObtenerTodosAsync();
            var taskObtenerTodosUsuarios = usuarioBL.ObtenerTodosAsync();
            var cita = await taskObtenerPorId;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Usuarios = await taskObtenerTodosUsuarios;
            ViewBag.Error = "";
            return View(cita);
        }

        // POST: CitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cita pCita)
        {
            try
            {
                int result = await citaBL.ModificarAsync(pCita);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Clientes = await clienteBL.ObtenerTodosAsync();
                ViewBag.Usuarios = await usuarioBL.ObtenerTodosAsync();
                return View(pCita);
            }
        }

        // GET: CitaController/Delete/5
        public async Task<IActionResult> Delete(Cita pCita)
        {
            ViewBag.Error = "";
            var cita = await citaBL.ObtenerPorIdAsync(pCita);
            cita.Cliente = await clienteBL.ObtenerPorIdAsync(new Clientes { Id = cita.IdCliente });
            cita.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = cita.IdUsuario });
            return View(cita);
        }

        // POST: CitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Cita pCita)
        {
            try
            {
                int result = await citaBL.EliminarAsync(pCita);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var cita = await citaBL.ObtenerPorIdAsync(pCita);
                if (cita == null)
                    cita = new Cita();
                if (cita.Id > 0)
                {
                    cita.Cliente = await clienteBL.ObtenerPorIdAsync(new Clientes { Id = cita.IdCliente });
                    cita.Usuario = await usuarioBL.ObtenerPorIdAsync(new Usuario { Id = cita.IdUsuario });
                }
                return View(pCita);
            }
        }

        public IActionResult ManejarTablas()
        {
            return View();
        }
    }
}
