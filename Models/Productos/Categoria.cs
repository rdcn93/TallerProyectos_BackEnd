using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Categoria", Schema = "Productos")]
    public class Categoria
    {
        [Key]
        public int id { get; set; }
        //[Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string nombre { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }

        [NotMapped]
        public ICollection<ProductoCategoria> productoCategorias { get; set; }
    }
}
