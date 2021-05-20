using System.Web.Mvc;

namespace TurismoRuralComunitario.Controllers
{
    public class ErroresController : Controller
    {
        // GET: Errores
        public ActionResult Error(int error = 0)
        {
            switch (error)
            {
                case 505:
                    ViewBag.StatusCode = error.ToString();
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Esto es muy vergonzoso, esperemos que no vuelva a pasar ..";
                    break;

                case 404:
                    ViewBag.StatusCode = error.ToString();
                    ViewBag.Title = "Parece que estás perdido en este espacio ";
                    ViewBag.Description = "Esta página que buscaba no existe";
                    break;

                default:
                    ViewBag.Title = "Error";
                    ViewBag.StatusCode = error.ToString();
                    ViewBag.Description = "Algo salio muy mal :( ..";
                    break;
            }
            return View();
        }
    }
}