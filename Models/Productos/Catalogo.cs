using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Catalogo", Schema = "Productos")]
    public class Catalogo
    {
        [Key]
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFin { get; set; }
        public string urlImagen { get; set; }
        public bool estado { get; set; }
        public DateTime fechaCreacion{ get; set; }
        public DateTime fechaModificacion { get; set; }

        public ICollection<ProductoCatalogo> productoCatalogos { get; set; }
    }
}
