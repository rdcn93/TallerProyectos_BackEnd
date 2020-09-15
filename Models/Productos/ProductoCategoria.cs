using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerProyectos_BackEnd.Models
{
    [Table("ProductoCategoria", Schema = "Productos")]
    public class ProductoCategoria
    {
        [Key]
        public int idProducto { get; set; }
        [Key]
        public int idCategoria { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }

        public Producto producto { get; set; }
        public Categoria categoria { get; set; }
    }
}
