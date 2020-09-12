using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Imagen", Schema = "Productos")]
    public class Imagen
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string nombre { get; set; }
        public string url { get; set; }
        public DateTime fechaRegistro { get; set; }

        public ICollection<ProductoImagen> productoImagenes { get; set; }
    }
}
