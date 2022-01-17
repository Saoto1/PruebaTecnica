using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*********************************/
using PruebaTecnica.EntidadesDeNegocio;
using PruebaTecnica.LogicaDeNegocio;

namespace PruebaTecnica.UI.AppWebAspCore.Controllers
{
    public class LibrosController : Controller
    {


     
            LibrosBL librosBL = new LibrosBL();
            // GET: ClienteController
            public async Task<IActionResult> Index(Libros pLibros = null)
            {
                if (pLibros == null)
                pLibros = new Libros();
                if (pLibros.Top_Aux == 0)
                pLibros.Top_Aux = 10;
                else if (pLibros.Top_Aux == -1)
                pLibros.Top_Aux = 0;
                var libros = await librosBL.BuscarAsync(pLibros);
                ViewBag.Top = pLibros.Top_Aux;
                return View(libros);
            }

            // GET: ClienteController/Details/5
            public async Task<IActionResult> Details(int id)
            {
                var libros = await librosBL.ObtenerPorIdAsync(new Libros { Id = id });
                return View(libros);
            }

            // GET: ClienteController/Create
            public IActionResult Create()
            {
                ViewBag.Error = "";
                return View();
            }

            // POST: ClienteController/Create
            [HttpPost]

            public async Task<IActionResult> Create(Libros pLibros)
            {
                try
                {
                    int result = await librosBL.CrearAsync(pLibros);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(pLibros);
                }
            }

            // GET: ClienteController/Edit/5
            public async Task<IActionResult> Edit(Libros pLibros)
            {
                var libros = await librosBL.ObtenerPorIdAsync(pLibros);
                ViewBag.Error = "";
                return View(libros);
            }

            // POST: ClienteController/Edit/5
            [HttpPost]

            public async Task<IActionResult> Edit(int id, Libros pLibros)
            {
                try
                {
                    int result = await librosBL.ModificarAsync(pLibros);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(pLibros);
                }
            }

            // GET: ClienteController/Delete/5
            public async Task<IActionResult> Delete(Libros pLibros)
            {
                ViewBag.Error = "";
                var libros = await librosBL.ObtenerPorIdAsync(pLibros);
                return View(libros);
            }

            // POST: ClienteController/Delete/5
            [HttpPost]

            public async Task<IActionResult> Delete(int id, Libros pLibros)
            {
                try
                {
                    int result = await librosBL.EliminarAsync(pLibros);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View(pLibros);
                }
            }
        }

    
}
