using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerProyectos_BackEnd.Models;

namespace TallerProyectos_BackEnd.DataAccess
{
    public class DataAccessProvider : IDataAccessProvider
    {
        private readonly PostgreSqlContext _context;

        public DataAccessProvider(PostgreSqlContext context)
        {
            _context = context;
        }

        #region Usuario
        public void AddUsuarioRecord(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            _context.SaveChanges();
        }

        public void UpdateUsuarioRecord(Usuario usuario)
        {
            var dbEntry = _context.Entry(usuario);

            dbEntry.Property(x => x.id).IsModified = false;
            dbEntry.Property(x => x.estado).IsModified = false;
            dbEntry.Property(x => x.fechaRegistro).IsModified = false;

            _context.Usuario.Update(usuario);
            _context.SaveChanges();
        }

        public void DeleteUsuarioRecord(int id)
        {
            var entity = _context.Usuario.FirstOrDefault(t => t.id == id);
            _context.Usuario.Remove(entity);
            _context.SaveChanges();
        }

        public Usuario GetUsuarioSingleRecord(int id)
        {
            return _context.Usuario.FirstOrDefault(t => t.id == id);
        }

        public Usuario GetUsuarioByEmail(string email)
        {
            return _context.Usuario.FirstOrDefault(t => t.email == email && t.estado == true);
        }

        public bool ExisteUsuarioByEmail(string email)
        {
            int cant = _context.Usuario.Where(x => x.email == email && x.estado == true).Count();

            return cant > 0 ? true : false;
        }

        public List<Usuario> GetUsuarioRecords()
        {
            return _context.Usuario.ToList();
        }
        #endregion

        #region Usuario
        public void AddProductoRecord(Producto producto)
        {
            var dbEntry = _context.Entry(producto);

            //dbEntry.Property(x => x.fabricante).IsModified = false;
            //dbEntry.Property(x => x.productoCategorias).IsModified = false;
            //dbEntry.Property(x => x.productoImagenes).IsModified = false;
            //dbEntry.Property(x => x.imagenes).IsModified = false;
            //dbEntry.Property(x => x.categorias).IsModified = false;

            _context.Producto.Add(producto);
            _context.SaveChanges();

            foreach (var pc in producto.categorias)
            {
                if(pc.id != 0)
                {
                    ProductoCategoria newProdCat = new ProductoCategoria();

                    newProdCat.idProducto = producto.id;
                    newProdCat.idCategoria = pc.id;
                    newProdCat.estado = true;
                    newProdCat.fechaRegistro = DateTime.Now;

                    _context.ProductoCategoria.Add(newProdCat);
                }
            }

            _context.SaveChanges();
        }

        public void UpdateProductoRecord(Producto producto)
        {
            var dbEntry = _context.Entry(producto);

            dbEntry.Property(x => x.id).IsModified = false;
            dbEntry.Property(x => x.estado).IsModified = false;
            dbEntry.Property(x => x.fechaRegistro).IsModified = false;

            List<ProductoCategoria> listProdCat = new List<ProductoCategoria>();
            listProdCat = _context.ProductoCategoria.Where(x => x.idProducto.Equals(producto.id)).ToList();

            foreach (var pc in producto.categorias)
            {
                bool existe = listProdCat.Where(x => x.idCategoria.Equals(pc.id)).Count() > 0 ? true : false;

                if (pc.id != 0)
                {
                    ProductoCategoria newProdCat = new ProductoCategoria();
                    var dbEntryProdCat = _context.Entry(newProdCat);

                    newProdCat.idProducto = producto.id;
                    newProdCat.idCategoria = pc.id;                    
                    newProdCat.fechaRegistro = DateTime.Now;

                    if (!existe)
                    {
                        _context.ProductoCategoria.Add(newProdCat);
                    }
                    else
                    {
                        dbEntryProdCat.Property(x => x.idProducto).IsModified = false;
                        dbEntryProdCat.Property(x => x.idCategoria).IsModified = false;
                        dbEntryProdCat.Property(x => x.estado).IsModified = false;
                        dbEntryProdCat.Property(x => x.fechaRegistro).IsModified = false;

                        newProdCat.fechaModificacion = DateTime.Now;

                        newProdCat.estado = true;

                        _context.ProductoCategoria.Update(newProdCat);
                    }
                }
            }

            _context.Producto.Update(producto);
            _context.SaveChanges();

        }

        public void DeleteProductoRecord(int id)
        {
            var entity = _context.Producto.FirstOrDefault(t => t.id == id);
            _context.Producto.Remove(entity);
            _context.SaveChanges();
        }

        public Producto GetProductoSingleRecord(int id)
        {
            return _context.Producto.FirstOrDefault(t => t.id == id);
        }

        public List<Producto> GetProductoRecords()
        {
            var productos = _context.Producto.Include(x => x.productoCategorias).ThenInclude(y => y.categoria).ToList();


            //var productos = _context.Producto.ToList();
            return productos;
        }
        #endregion

    }
}
