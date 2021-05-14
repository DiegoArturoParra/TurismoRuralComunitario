using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using TurismoRuralComunitario.Helpers;
using TurismoRuralComunitario.Models;
using TurismoRuralComunitario.Request;

namespace TurismoRuralComunitario.Controllers
{
    public class CuentaController : BaseController
    {
        private DatabaseContext db = new DatabaseContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "Email,Password")] LoginRequest usuario)
        {
            if (ModelState.IsValid)
            {
                var existe = db.TablaUsuarios.Where(x => x.Password.Equals(usuario.Password)
                && x.Email.Equals(usuario.Email)).FirstOrDefault();

                if (existe != null)
                {
                    string rol = db.TablaRoles.Where(x => x.Id == existe.RolId).Select(x => x.TipoRol).FirstOrDefault();
                    GuardarCookies(existe.Email, rol);
                    if (existe.RolId == (int)Rol.Roles.AdministradorMunicipal)
                    {
                        // redirigir al controlador y vista que desea
                        // return RedirectToAction("Administrador", "Home");
                    }
                    else if (existe.RolId == (int)Rol.Roles.SuperAdministrador)
                    {
                        return RedirectToAction("Administrador", "Home");
                    }
                    else if (existe.RolId == (int)Rol.Roles.Cliente)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (existe.RolId == (int)Rol.Roles.Propietario)
                    {
                        //return RedirectToAction("Administrador", "Home");
                    }
                    else if (existe.RolId == (int)Rol.Roles.Guia)
                    {
                        //return RedirectToAction("Administrador", "Home");
                    }

                }
                else
                {
                    SetAlert(Constantes.ALERT_ERROR);
                    SetMessage("Contraseña y/o email invalido.");
                    return RedirectToAction("Login");
                }

            }
            return View(usuario);
        }

        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrarse([Bind(Include = "Email,Password,Nombre,Cedula")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                var existe = ExisteCorreo(usuario.Email);
                var existecedula = ExisteCedula(usuario.Cedula);

                if (existe != null || existecedula)
                {
                    if (existecedula)
                    {
                        SetAlert(Constantes.ALERT_ERROR);
                        SetMessage("Cédula ya registrada..");
                    }
                    else
                    {
                        SetAlert(Constantes.ALERT_ERROR);
                        SetMessage("Email ya registrado.");
                    }
                    return RedirectToAction("Registrarse");
                }

                usuario.RolId = (int)Rol.Roles.Cliente;
                db.TablaUsuarios.Add(usuario);
                db.SaveChanges();
                SetAlert(Constantes.ALERT_SUCCESS);
                SetMessage("Registro satisfactoriamente.");
                return RedirectToAction("Login");
            }

            return View(usuario);
        }



        public ActionResult RecuperarPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult RecuperarPassword([Bind(Include = "Email")] string Email)
        {
            if (ModelState.IsValid)
            {
                var persona = ExisteCorreo(Email);
                if (persona != null)
                {
                    persona.Token = Helper.Encriptar(JsonConvert.SerializeObject(persona));
                    persona.VencimientoToken = DateTime.Now.AddHours(4);
                    Helper.enviarCorreo(persona.Email, persona.Token, "Recuperación Contraseña");
                    try
                    {
                        db.Entry(persona).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    SetAlert(Constantes.ALERT_SUCCESS);
                    SetMessage("Revise su bandeja de su correo eléctronico.");
                    return RedirectToAction("Login");
                }
                else
                {
                    SetAlert(Constantes.ALERT_ERROR);
                    SetMessage("No hay ningún usuario con ese email.");
                    return RedirectToAction("RecuperarContraseña");
                }
            }
            return View();
        }


        public ActionResult CambiarPassword(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var existe = BuscarUsuarioByToken(token);

                if (existe != null && existe.VencimientoToken > DateTime.Now)
                {
                    TempData["token"] = token;
                    return View();
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ChangePassword(ChangePasswordRequest cambio)
        {
            if (ModelState.IsValid)
            {
                if (TempData["token"] != null)
                {
                    var usuario = BuscarUsuarioByToken(TempData["token"].ToString());
                    if (usuario != null)
                    {
                        usuario.Token = null;
                        usuario.VencimientoToken = null;
                        usuario.Password = cambio.Password;
                        try
                        {
                            db.Entry(usuario).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        SetAlert(Constantes.ALERT_SUCCESS);
                        SetMessage("Su contraseña se ha cambiado satisfactoriamente..");
                        return RedirectToAction("Login");
                    }
                }
            }
            return View("CambiarPassword");
        }


        [HttpPost]
        public ActionResult CerrarSesion()
        {
            if (User.Identity.IsAuthenticated)
            {
                Session.Clear();
                Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                Response.Cookies.Clear();
                HttpCookie c = new HttpCookie("Login");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
                Session.Abandon();
            }
            Session.Abandon();
            return RedirectToAction("Login");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        private Usuario ExisteCorreo(string correo)
        {
            return db.TablaUsuarios.Where(x => x.Email.Equals(correo)).FirstOrDefault();
        }
        private bool ExisteCedula(string cedula)
        {
            return db.TablaUsuarios.Any(x => x.Cedula.Equals(cedula));
        }

        private Usuario BuscarUsuarioByToken(string token)
        {
            return db.TablaUsuarios.Where(x => x.Token.Equals(token)).FirstOrDefault();
        }



        private void GuardarCookies(string email, string rol)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, rol),
            };
            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var context = Request.GetOwinContext();
            var authManager = context.Authentication;
            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
        }
    }
}
