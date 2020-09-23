using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TallerProyectos_BackEnd.Models
{
    [Table("ProductoCatalogo", Schema = "Productos")]
    public class ProductoCatalogo
    {
        [Key]
        public int idProducto { get; set; }
        [Key]
        public int idCatalogo { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }

        public Producto producto { get; set; }
        public Catalogo catalogo { get; set; }
    }
}
