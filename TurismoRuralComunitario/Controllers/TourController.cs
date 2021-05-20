using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Mvc;
using TurismoRuralComunitario.Helpers;
using TurismoRuralComunitario.Models;

namespace TurismoRuralComunitario.Controllers
{
    public class TourController : BaseController
    {
        private List<Tour> catalogoTours = new List<Tour>();
        private DatabaseContext db = new DatabaseContext();

        public async Task<ActionResult> Catalogo()
        {
            return View(await ListarCatalogo());
        }

        private async Task<List<Tour>> ListarCatalogo()
        {
            var usuario = await db.TablaUsuarios.FindAsync(ObtenerIdxClaim());

            if (usuario != null)
            {
                catalogoTours = usuario.MunicipioId == null ? await db.TablaTour.ToListAsync()
                    : await db.TablaTour.Where(x => x.MunicipioId == usuario.MunicipioId).ToListAsync();
            }
            else
            {
                catalogoTours = await db.TablaTour.ToListAsync();
            }

            foreach (var item in catalogoTours)
            {
                item.Detalles = JsonSerializer.Deserialize<DetallesDelTour>(item.DescripcionTour);
            }
            return catalogoTours;
        }


        public async Task<ActionResult> Detalle(int? id)
        {
            if (id == null)
            {
                SetMessage("No existe ese tour.");
                return Redirect("Catalogo");
            }
            Tour tour = await db.TablaTour.FindAsync(id);
            tour.Municipio = db.TablaMunicipios.Find(tour.MunicipioId).NombreMunicipio;
            if (tour == null)
            {
                SetMessage("No existe ese tour.");
                return Redirect("Catalogo");
            }
            tour.Detalles = JsonSerializer.Deserialize<DetallesDelTour>(tour.DescripcionTour);
            return View(tour);
        }


        public ActionResult Crear()
        {
            listaCantidadHabitaciones();
            return View();
        }

        private void listaCantidadHabitaciones()
        {
            List<int> habitaciones = new List<int>()
            {
                1,2,3,4,5,6
            };

            ViewBag.DDL_CantidadHabitaciones = new SelectList(habitaciones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(Tour tour)
        {
            if (ModelState.IsValid)
            {
                FolderIsExist(Constantes.PATH_IMAGENES_TOURS);
                if (tour.Imagen != null && tour.Imagen.ContentLength > 0)
                {
                    string extension = Path.GetExtension(tour.Imagen.FileName);
                    PathCompleto = ConcatenarPath(Constantes.PATH_IMAGENES_TOURS, tour.NombreSitio, extension);
                    string path = Server.MapPath(PathCompleto);
                    subirArchivo(tour.PathImagen, path, tour.Imagen);
                    tour.PathImagen = PathCompleto;
                }
                json = JsonSerializer.Serialize(tour.Detalles);
                tour.DescripcionTour = json;

                var municipio = await db.TablaUsuarios.FindAsync(ObtenerIdxClaim());

                tour.MunicipioId = municipio.MunicipioId != null ? municipio.MunicipioId : null;
                db.TablaTour.Add(tour);
                await db.SaveChangesAsync();
                return Redirect("Catalogo");
            }

            listaCantidadHabitaciones();
            return View(tour);
        }

        #region Editar producto
        public async Task<ActionResult> Editar(int? id)
        {
            if (id == null)
            {
                SetMessage("No existe ese tour.");
                return Redirect("Catalogo");
            }
            Tour tourEdit = await db.TablaTour.FindAsync(id);
            if (tourEdit == null)
            {
                SetMessage("No existe ese tour.");
                return Redirect("Catalogo");
            }
            tourEdit.Detalles = JsonSerializer.Deserialize<DetallesDelTour>(tourEdit.DescripcionTour);
            listaCantidadHabitaciones();
            return View(tourEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditarAsync(Tour newTour)
        {
            if (ModelState.IsValid)
            {
                if (newTour.Imagen != null && newTour.Imagen.ContentLength > 0)
                {
                    string extension = Path.GetExtension(newTour.Imagen.FileName);
                    PathCompleto = ConcatenarPath(Constantes.PATH_IMAGENES_TOURS, newTour.NombreSitio, extension);
                    string path = Server.MapPath(PathCompleto);
                    subirArchivo(newTour.PathImagen, path, newTour.Imagen);
                    newTour.PathImagen = PathCompleto;
                }
                json = JsonSerializer.Serialize(newTour.Detalles);
                newTour.DescripcionTour = json;
                db.Entry(newTour).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return Redirect("Catalogo");
            }
            return View(newTour);
        }
        #endregion

        public async Task<ActionResult> Eliminar(int id)
        {
            var tour = db.TablaTour.Find(id);
            if (!string.IsNullOrEmpty(tour.PathImagen))
            {
                EliminarArchivo(tour.PathImagen);
            }
            db.TablaTour.Remove(tour);
            await db.SaveChangesAsync();
            return RedirectToAction("Catalogo");
        }
    }
}