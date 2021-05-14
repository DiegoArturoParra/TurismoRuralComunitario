using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Web.Mvc;
using TurismoRuralComunitario.Helpers;
using TurismoRuralComunitario.Models;

namespace TurismoRuralComunitario.Controllers
{
    public class TourController : BaseController
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Catalogo()
        {
           
            return View();
        }

        public ActionResult CrearTour()
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
        public ActionResult CrearTour(Tour tour)
        {
            if (ModelState.IsValid)
            {
                FolderIsExist(Constantes.PATH_IMAGENES_TOURS);
                if (tour.Imagen != null && tour.Imagen.ContentLength > 0)
                {
                    string extension = Path.GetExtension(tour.Imagen.FileName);
                    PathCompleto = Constantes.PATH_IMAGENES_TOURS + tour.NombreSitio + extension;
                    string path = Server.MapPath(PathCompleto);
                    subirArchivo(tour.PathImagen, path, tour.Imagen);
                    tour.PathImagen = PathCompleto;
                }
                json = JsonSerializer.Serialize(tour.Detalles);
                tour.DescripcionTour = json;
                db.TablaTour.Add(tour);
                db.SaveChanges();
                return Redirect("Catalogo");
            }
            listaCantidadHabitaciones();
            return View(tour);
        }
    }
}