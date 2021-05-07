using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoRuralComunitario.Models
{
    [Table("roles", Schema = "usuarios")]
    public class Rol
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("descripcion_rol")]
        public string TipoRol { get; set; }
        public enum Roles
        {
            none, SuperAdministrador, AdministradorMunicipal, Guia, Propietario, Cliente
        }
    }
}