using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TurismoRuralComunitario.Models;

namespace TurismoRuralComunitario.Helpers
{
    public static class Constantes
    {
        #region Variables TEMPDATA
        public static string MESSAGE = "mensaje";
        public static string ALERT_SUCCESS = "alert alert-success";
        public static string ALERT_ERROR = "alert alert-danger";
        public static string ALERT = "alerta";
        public static string ERROR = "Error";
        public const string ADMIN = "SuperAdministrador";
        public const string ADMINMUNICIPAL = "AdministradorMunicipal";
        public const string GUIA = "Guia";
        public const string PROPIETARIO = "Propietario";
        public const string CLIENTE = "Cliente";
        #endregion

        #region Variables APPLICACION
        public static string CORREOWEB = "vasborsas@gmail.com";
        public static string PASSWORD = "vasbor12345";
        public static string PATH_IMAGENES_TOURS = "~/Recursos/imgTours/";
        public static string PRODUCTO_ID = "productoID";
        #endregion
    }
}