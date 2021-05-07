using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoRuralComunitario.Models
{
    [Table("usuario", Schema = "usuarios")]
    public class Usuario
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("email")]
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Introduce una dirección de correo electrónico válida.")]
        public string Email { get; set; }
        [Column("password")]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Debe tener una longitud mínima de {2} y una longitud máxima de {1}.", MinimumLength = 6)]
        public string Password { get; set; }
        [Column("nombre_completo")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Debe tener una longitud mínima de {2} y una longitud máxima de {1}.", MinimumLength = 4)]
        [Display(Name = "Nombre Completo")]
        public string Nombre { get; set; }
        [Column("cedula")]
        [Required(ErrorMessage = "*")]
        [StringLength(18, ErrorMessage = "Debe tener una longitud mínima de {2} y una longitud máxima de {1}.", MinimumLength = 7)]
        [Display(Name = "Cédula")]
        public string Cedula { get; set; }
        [Column("rol_id")]
        [ScaffoldColumn(false)]
        public int RolId { get; set; }

        [Column("token")]
        [ScaffoldColumn(false)]
        public string Token { get; set; }
        [Column("vencimiento_token")]
        [ScaffoldColumn(false)]
        public DateTime? VencimientoToken { get; set; }
        [NotMapped]
        [ScaffoldColumn(false)]
        public string TipoRol { get; set; }

    }
}