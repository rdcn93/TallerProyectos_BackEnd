using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TallerProyectos_BackEnd.Models
{
    [Table("Producto", Schema = "Productos")]
    public class Producto
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string nombre { get; set; }
        public string codigo { get; set; }
        [Required(ErrorMessage = "El campo Descripción es obligatorio")]
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        
        public int? idFabricante { get; set; }
        
        [NotMapped]
        [ForeignKey("idFabricante")]
        public virtual Fabricante fabricante { get; set; }

        public bool estado { get; set; }
        public DateTime fechaRegistro { get; set; }
        public DateTime fechaModificacion { get; set; }

        [NotMapped]
        public List<Categoria> categorias { get; set; }
        [NotMapped]
        public List<Imagen> imagenes { get; set; }

        public ICollection<ProductoCategoria> productoCategorias { get; set; }
        public ICollection<ProductoCatalogo> productoCatalogos { get; set; }
        public ICollection<ProductoImagen> productoImagenes { get; set; }

        public Producto()
        {
            categorias = new List<Categoria>();
            fabricante = new Fabricante();
            imagenes = new List<Imagen>();
        }

    }
}
