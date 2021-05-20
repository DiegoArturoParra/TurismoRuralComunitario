using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TurismoRuralComunitario.Helpers;

namespace TurismoRuralComunitario.Controllers
{
    public class BaseController : Controller
    {
        public string json { get; set; }
        public string PathCompleto { get; set; }
        public string Confirmacion { get; set; }
        public Exception Error { get; set; }

        #region Subir archivos al servidor
        public void subirArchivo(string rutaExistente, string rutaActual, HttpPostedFileBase file)
        {
            try
            {
                if (System.IO.File.Exists(Server.MapPath(rutaExistente)))
                {
                    System.IO.File.Delete(Server.MapPath(rutaExistente));
                }
                file.SaveAs(rutaActual);
                this.Confirmacion = "archivo subido.";
            }
            catch (Exception ex)
            {
                this.Error = ex;
                throw;
            }
        }
        #endregion

        #region Eliminar archivos del servidor
        public void EliminarArchivo(string rutaExistente)
        {
            try
            {
                if (System.IO.File.Exists(Server.MapPath(rutaExistente)))
                {
                    System.IO.File.Delete(Server.MapPath(rutaExistente));
                }
                this.Confirmacion = "archivo eliminado.";
            }
            catch (Exception ex)
            {
                this.Error = ex;
                throw;
            }
        }
        #endregion

        #region Alerts para mensajes en formato bootstrap
        public void SetMessage(string message)
        {
            TempData[Constantes.MESSAGE] = message;
        }

        public void SetAlert(string alert)
        {
            TempData[Constantes.ALERT] = alert;
        }
        #endregion

        #region Creacion de carpeta si no existe
        public void FolderIsExist(string path)
        {
            bool folderExists = Directory.Exists(Server.MapPath(path));
            if (!folderExists)
                Directory.CreateDirectory(Server.MapPath(path));
        }

        #endregion

        #region guardar claims
        public void GuardarCookies(string email, string rol, int id)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, rol),
                new Claim(ClaimTypes.NameIdentifier, id.ToString())
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
        }
        #endregion

        #region Obtener ObtenerIdxClaim
        public int ObtenerIdxClaim()
        {
            var claimsIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            var id = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            return id != null ? int.Parse(id.Value) : default;
        }
        #endregion

        public string ConcatenarPath(string ruta,  string nombre, string extension)
        {
            return $"{ruta}{nombre}{DateTime.Now.Minute}{DateTime.Now.Second}{DateTime.Now.Millisecond}{extension}";
        }
    }
}