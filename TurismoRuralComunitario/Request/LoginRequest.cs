using System.ComponentModel.DataAnnotations;

namespace TurismoRuralComunitario.Request
{
    public class LoginRequest
    {
        [Display(Name = "Correo Electrónico")]
        [Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Introduce una dirección de correo electrónico válida.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }

    public class ChangePasswordRequest
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [StringLength(64, ErrorMessage = "Debe tener una longitud mínima de {2} y una longitud máxima de {1}.", MinimumLength = 6)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        [Display(Name = "Confirmar contraseña")]
        public string ConfirmarPassword { get; set; }

    }
}