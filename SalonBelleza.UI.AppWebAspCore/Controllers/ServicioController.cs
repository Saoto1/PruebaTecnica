using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/********************************/
using SalonBelleza.EntidadesDeNegocio;
using SalonBelleza.LogicaDeNegocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace SalonBelleza.UI.AppWebAspCore.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ServicioController : Controller
    {

        ServicioBL ServicioBL = new ServicioBL();
        // GET: ServicioController
        public async Task<IActionResult> Index(Servicio pServicio = null)
        {
            if (pServicio == null)
                pServicio = new Servicio();
            if (pServicio.Top_Aux == 0)
                pServicio.Top_Aux = 10;
            else if (pServicio.Top_Aux == -1)
                pServicio.Top_Aux = 0;
            var servicios = await ServicioBL.BuscarAsync(pServicio);
            ViewBag.Top = pServicio.Top_Aux;
            return View(servicios);
        }

        // GET: ServicioController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var servicio = await ServicioBL.ObtenerPorIdAsync(new Servicio { Id = id });
            return View(servicio);
        }

        // GET: ServicioController/Create
        public ActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: ServicioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servicio pServicio)
        {
            try
            {
                int result = await ServicioBL.CrearAsync(pServicio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pServicio);
            }
        }

        // GET: ServicioController/Edit/5
        public async Task<IActionResult> Edit(Servicio pServicio)
        {
            var servicio = await ServicioBL.ObtenerPorIdAsync(pServicio);
            ViewBag.Error = "";
            return View(servicio);
        }

        // POST: ServicioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servicio pServicio)
        {
            try
            {
                int result = await ServicioBL.ModificarAsync(pServicio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pServicio);
            }
        }

        // GET: ServicioController/Delete/5
        public async Task<IActionResult> Delete(Servicio pServicio)
        {
            ViewBag.Error = "";
            var servicio = await ServicioBL.ObtenerPorIdAsync(pServicio);
            return View(servicio);
        }

        // POST: ServicioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Servicio pServicio)
        {
            try
            {
                int result = await ServicioBL.EliminarAsync(pServicio);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pServicio);
            }
        }
    }
}
