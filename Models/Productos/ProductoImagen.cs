using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerProyectos_BackEnd.Models
{
    [Table("ProductoImagen", Schema = "Productos")]
    public class ProductoImagen
    {
        [Key]
        public int idProducto { get; set; }
        [Key]
        public int idImagen { get; set; }
        public bool estado { get; set; }

        public Producto producto { get; set; }
        public Imagen imagen { get; set; }
    }
}
