using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoRuralComunitario.Models
{
    [Table("municipio", Schema = "tours")]
    public class Municipio
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("nombre_municipio")]
        public string NombreMunicipio { get; set; }
    }

    public enum Municipios
    {
        none, Facatativa, Fusagasuga, Mosquera
    }
}