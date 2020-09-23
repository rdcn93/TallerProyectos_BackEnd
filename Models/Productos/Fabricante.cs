using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Fabricante", Schema = "Productos")]
    public class Fabricante
    {
        [Key]
        public int id { get; set; }

        public string nombre { get; set; }
        public string ruc { get; set; }
        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }

        //[ForeignKey("idFabricante")]
        public ICollection<Producto> productos { get; set; }
    }
}
