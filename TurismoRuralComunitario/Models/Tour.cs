using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Web;
using TurismoRuralComunitario.Helpers;

namespace TurismoRuralComunitario.Models
{
    [Table("tour", Schema = "tours")]
    public class Tour
    {

        public Tour()
        {
            this.Detalles = new DetallesDelTour();
        }
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("municipio_id")]
        public int MunicipioId { get; set; }

        [Column("nombre_del_sitio")]
        [Display(Name = "Nombre del tour")]
        [Required(ErrorMessage = "*")]
        public string NombreSitio { get; set; }

        [Column("precio")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Precio del tour")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public double Precio { get; set; }

        [Column("path_imagen")]
        [Display(Name = "Imagen")]
        public string PathImagen { get; set; }

        [Column("descripcion_tour")]
        public string DescripcionTour { get; set; }

        [NotMapped]
        [ValidImageFile(ErrorMessage = "Es incorrecto el formato.")]
        public HttpPostedFileBase Imagen { get; set; }

        [NotMapped]
        public DetallesDelTour Detalles { get; set; }
        public class ValidImageFileAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (value == null)
                    return true;

                string[] _validExtensions = { "JPG", "JPEG", "BMP", "GIF", "PNG" };

                var file = (HttpPostedFileBase)value;
                var ext = Path.GetExtension(file.FileName).ToUpper().Replace(".", "");
                return _validExtensions.Contains(ext) && file.ContentType.Contains("image");
            }
        }
    }

    public class DetallesDelTour
    {
        [Display(Name = "Registro nacional de turismo")]
        [Required(ErrorMessage = "*")]
        public string RegistroNacionalDeTurismo { get; set; }
        [Display(Name = "Total de habitaciones")]
        [Required(ErrorMessage = "*")]
        public int TotalHabitaciones { get; set; }
        [Display(Name = "Caracteristicas del tour")]
        [Required(ErrorMessage = "*")]
        public string Caracteristicas { get; set; }
    }
}