using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Usuario", Schema = "Seguridad")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo Correo es obligatorio")]
        public string email { get; set; }
        [Required(ErrorMessage = "El campo Password es obligatorio")]
        public string password { get; set; }
        public string nombre { get; set; }
        public string apePaterno { get; set; }
        public string apeMaterno { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }
    }
}
