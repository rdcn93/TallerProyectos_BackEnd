using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Cliente", Schema = "Seguridad")]
    public class Cliente
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string email { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string password { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string apePaterno { get; set; }
        public string apeMaterno { get; set; }
        public string nroDocumento { get; set; }
        public string telefono { get; set; }
        public string celular { get; set; }
        public string genero { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
