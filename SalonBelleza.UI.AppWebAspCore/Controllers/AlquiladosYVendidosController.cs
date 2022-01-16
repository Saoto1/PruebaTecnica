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
    public class AlquiladosYVendidosController:Controller
    {

        LibrosBL librosBL = new LibrosBL();
        ClientesBL clientesBL = new ClientesBL();
        AlquiladosYVendidosBL alquiladosYVBL = new AlquiladosYVendidosBL();   
        // GET: CitaController
        public async Task<IActionResult> Index(AlquiladosYVendidos pAlquiladosYVendidos = null)
        {
            if (pAlquiladosYVendidos == null)
                pAlquiladosYVendidos = new AlquiladosYVendidos();
            if (pAlquiladosYVendidos.Top_Aux == 0)
                pAlquiladosYVendidos.Top_Aux = 10;
            else if (pAlquiladosYVendidos.Top_Aux == -1)
                pAlquiladosYVendidos.Top_Aux = 0;
            var taskBuscarCliente = alquiladosYVBL.BuscarIncluirClientesAsync(pAlquiladosYVendidos);
            var taskObtenerTodosClientes = clientesBL.ObtenerTodosAsync();
            var taskBuscarUsuario = alquiladosYVBL.BuscarIncluirLibrosAsync(pAlquiladosYVendidos);
            var taskObtenerTodosLibros = librosBL.ObtenerTodosAsync();
            var taskBuscarUsuarioCliente = alquiladosYVBL.BuscarIncluirClienteLibroAsync(pAlquiladosYVendidos);
            var citas = await taskBuscarUsuarioCliente;
            ViewBag.Top = pAlquiladosYVendidos.Top_Aux;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Libros = await taskObtenerTodosLibros;

            return View(alquiladosYVBL);
        }

        // GET: CitaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var AlquiladoYV = await alquiladosYVBL.ObtenerPorIdAsync(new AlquiladosYVendidos { Id = id });
            var taskCliente = clientesBL.ObtenerPorIdAsync(new Clientes { Id = AlquiladoYV.IdCliente });
            var taskLibros = librosBL.ObtenerPorIdAsync(new Libros { Id = AlquiladoYV.IdLibro });
            //var taskDetalles = detalleCitaBL.BuscarIncluirServicioAsync(new DetalleCita { IdCita = cita.Id });
            AlquiladoYV.Clientes = await taskCliente;
            AlquiladoYV.Libros = await taskLibros;
            //cita.DetalleCita = await taskDetalles;
            return View(AlquiladoYV);
        }

        // GET: CitaController/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = await clientesBL.ObtenerTodosAsync();
            ViewBag.Libros = await librosBL.ObtenerTodosAsync();
            //ViewBag.servicios = await servicioBL.ObtenerTodosAsync();
            ViewBag.Error = "";
            return View();
        }

        // POST: CitaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            try
            {
                // int result = 0;
                int result = await alquiladosYVBL.CrearAsync(pAlquiladosYVendidos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Clientes = await clientesBL.ObtenerTodosAsync();
                ViewBag.Libros = await librosBL.ObtenerTodosAsync();
                //ViewBag.servicios = await servicioBL.ObtenerTodosAsync();
                return View(pAlquiladosYVendidos); //si ocurrio un error se redirecciona a la vista create con el parametro
            }
        }

        // GET: CitaController/Edit/5
        public async Task<IActionResult> Edit(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            var taskObtenerPorId = alquiladosYVBL.ObtenerPorIdAsync(pAlquiladosYVendidos);
            var taskObtenerTodosClientes = clientesBL.ObtenerTodosAsync();
            var taskObtenerTodosLibros = librosBL.ObtenerTodosAsync();
            var AlquiladoYV = await taskObtenerPorId;
            ViewBag.Clientes = await taskObtenerTodosClientes;
            ViewBag.Libros = await taskObtenerTodosLibros;
            ViewBag.Error = "";
            return View(AlquiladoYV);
        }

        // POST: CitaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AlquiladosYVendidos pAlquiladosYVendidos)
        {
            try
            {
                int result = await alquiladosYVBL.ModificarAsync(pAlquiladosYVendidos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Clientes = await clientesBL.ObtenerTodosAsync();
                ViewBag.Libros = await librosBL.ObtenerTodosAsync();
                return View(pAlquiladosYVendidos);
            }
        }

        // GET: CitaController/Delete/5
        public async Task<IActionResult> Delete(AlquiladosYVendidos pAlquiladosYVendidos)
        {
            ViewBag.Error = "";
            var AlquiladoYV = await alquiladosYVBL.ObtenerPorIdAsync(pAlquiladosYVendidos);
            AlquiladoYV.Clientes = await clientesBL.ObtenerPorIdAsync(new Clientes { Id = AlquiladoYV.IdCliente });
            AlquiladoYV.Libros = await librosBL.ObtenerPorIdAsync(new Libros { Id = AlquiladoYV.IdLibro });
            return View(AlquiladoYV);
        }

        // POST: CitaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, AlquiladosYVendidos pAlquiladosYVendidos)
        {
            try
            {
                int result = await alquiladosYVBL.EliminarAsync(pAlquiladosYVendidos);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                var AlquiladoYV = await alquiladosYVBL.ObtenerPorIdAsync(pAlquiladosYVendidos);
                if (AlquiladoYV == null)
                    AlquiladoYV = new AlquiladosYVendidos();
                if (AlquiladoYV.Id > 0)
                {
                    AlquiladoYV.Clientes = await clientesBL.ObtenerPorIdAsync(new Clientes { Id = AlquiladoYV.IdCliente });
                    AlquiladoYV.Libros = await librosBL.ObtenerPorIdAsync(new Libros { Id = AlquiladoYV.IdLibro });
                }
                return View(pAlquiladosYVendidos);
            }
        }

        public IActionResult ManejarTablas()
        {
            return View();
        }

    }
}
