using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/*********************************/
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SalonBelleza.UI.AppWebAspCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ClienteController : Controller
    {
        ClienteBL clienteBL = new ClienteBL();
        // GET: ClienteController
        public async Task<IActionResult> Index(Clientes pCliente = null)
        {
            if (pCliente == null)
                pCliente = new Clientes();
            if (pCliente.Top_Aux == 0)
                pCliente.Top_Aux = 10;
            else if (pCliente.Top_Aux == -1)
                pCliente.Top_Aux = 0;
            var clientes = await clienteBL.BuscarAsync(pCliente);
            ViewBag.Top = pCliente.Top_Aux;
            return View(clientes);
        }

        // GET: ClienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await clienteBL.ObtenerPorIdAsync(new Clientes { Id = id });
            return View(cliente);
        }

        // GET: ClienteController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Clientes pCliente)
        {
            try
            {
                int result = await clienteBL.CrearAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(Clientes pCliente)
        {
            var cliente = await clienteBL.ObtenerPorIdAsync(pCliente);
            ViewBag.Error = "";
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Clientes pCliente)
        {
            try
            {
                int result = await clienteBL.ModificarAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(Clientes pCliente)
        {
            ViewBag.Error = "";
            var cliente = await clienteBL.ObtenerPorIdAsync(pCliente);
            return View(cliente);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Clientes pCliente)
        {
            try
            {
                int result = await clienteBL.EliminarAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }
    }
}
