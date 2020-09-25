using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            entity.estado = false;
            entity.fechaModificacion = DateTime.Now;

            _context.Usuario.Update(entity);

            //_context.Usuario.Remove(entity);
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

        #region Cliente
        public void AddClienteRecord(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            _context.SaveChanges();
        }

        public void UpdateClienteRecord(Cliente cliente)
        {
            var dbEntry = _context.Entry(cliente);

            dbEntry.Property(x => x.id).IsModified = false;
            dbEntry.Property(x => x.estado).IsModified = false;
            dbEntry.Property(x => x.fechaRegistro).IsModified = false;

            _context.Cliente.Update(cliente);
            _context.SaveChanges();
        }

        public void DeleteClienteRecord(int id)
        {
            var entity = _context.Cliente.FirstOrDefault(t => t.id == id);

            entity.estado = false;
            entity.fechaModificacion = DateTime.Now;

            _context.Cliente.Update(entity);

            //_context.Usuario.Remove(entity);
            _context.SaveChanges();
        }

        public Cliente GetClienteSingleRecord(int id)
        {
            return _context.Cliente.FirstOrDefault(t => t.id == id);
        }

        public Cliente GetClienteByEmail(string email)
        {
            return _context.Cliente.FirstOrDefault(t => t.email == email && t.estado == true);
        }

        public bool ExisteClienteByEmail(string email)
        {
            int cant = _context.Cliente.Where(x => x.email == email && x.estado == true).Count();

            return cant > 0 ? true : false;
        }

        public bool ExisteClienteByUsuario(string usuario)
        {
            int cant = _context.Cliente.Where(x => x.usuario == usuario && x.estado == true).Count();

            return cant > 0 ? true : false;
        }

        public List<Cliente> GetClienteRecords()
        {
            return _context.Cliente.ToList();
        }
        #endregion

        #region Producto
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

            #region Guardar categorias
            foreach (var pc in producto.categorias)
            {
                if (pc.id != 0)
                {
                    ProductoCategoria newProdCat = new ProductoCategoria();

                    newProdCat.idProducto = producto.id;
                    newProdCat.idCategoria = pc.id;
                    newProdCat.estado = true;
                    newProdCat.fechaRegistro = DateTime.Now;

                    _context.ProductoCategoria.Add(newProdCat);
                }
            }
            #endregion

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

            entity.estado = false;
            entity.fechaModificacion = DateTime.Now;

            _context.Producto.Update(entity);

            //_context.Producto.Remove(entity);
            _context.SaveChanges();
        }

        public Producto GetProductoSingleRecord(int id)
        {
            Producto prod = new Producto();

            //var das = _context.ProductoCategoria.Where(x => x.idProducto.Equals(id) && x.estado).ToList();
            prod = _context.Producto.Include(x => x.fabricante).FirstOrDefault(t => t.id == id);

            prod.categorias = (from ep in _context.Categoria
                              join e in _context.ProductoCategoria on ep.id equals e.idCategoria
                              where e.idProducto == id && e.estado
                              select new Categoria
                              {
                                  id = ep.id,
                                  nombre = ep.nombre
                              }).ToList();

            return prod;
            //return _context.Producto.Include(x => x.fabricante).FirstOrDefault(t => t.id == id);
        }

        public List<Producto> GetProductoRecords()
        {
            var productos = _context.Producto.Include(x => x.productoCategorias).ThenInclude(y => y.categoria).ToList();

            //var productos = _context.Producto.ToList();
            return productos;
        }

        public List<Producto> ProductosByCategoria(int id)
        {
            var productos = (from ep in _context.Producto
                               join e in _context.ProductoCategoria on ep.id equals e.idProducto
                               where e.idCategoria == id && e.estado
                               select ep).ToList();
            return productos;
        }
        
        public List<Producto> ProductosByCatalogo(int id)
        {
            var productos = (from ep in _context.Producto
                               join e in _context.ProductoCatalogo on ep.id equals e.idProducto
                               where e.idCatalogo == id && e.estado
                               select ep).ToList();
            return productos;
        }
        #endregion

        #region Categoria
        public void AddCategoriaRecord(Categoria cat)
        {
            _context.Categoria.Add(cat);
            _context.SaveChanges();
        }

        public void UpdateCategoriaRecord(Categoria cat)
        {
            var dbEntry = _context.Entry(cat);

            dbEntry.Property(x => x.id).IsModified = false;
            dbEntry.Property(x => x.estado).IsModified = false;
            dbEntry.Property(x => x.fechaRegistro).IsModified = false;

            _context.Categoria.Update(cat);
            _context.SaveChanges();
        }

        public void DeleteCategoriaRecord(int id)
        {
            var entity = _context.Categoria.FirstOrDefault(t => t.id == id);
            _context.Categoria.Remove(entity);
            _context.SaveChanges();
        }

        public Categoria GetCategoriaSingleRecord(int id)
        {
            return _context.Categoria.FirstOrDefault(t => t.id == id);
        }


        public List<Categoria> GetCategoriaRecords(bool todo = false)
        {
            if(todo)
                return _context.Categoria.ToList();
            else
                return (from ep in _context.Categoria
                        where ep.estado
                        select new Categoria
                        {
                            id = ep.id,
                            nombre = ep.nombre
                        }).ToList();
        }
        #endregion

        #region Fabricante
        public void AddFabricanteRecord(Fabricante cat)
        {
            _context.Fabricante.Add(cat);
            _context.SaveChanges();
        }

        public void UpdateFabricanteRecord(Fabricante cat)
        {
            var dbEntry = _context.Entry(cat);

            dbEntry.Property(x => x.id).IsModified = false;
            dbEntry.Property(x => x.estado).IsModified = false;
            dbEntry.Property(x => x.fechaRegistro).IsModified = false;

            _context.Fabricante.Update(cat);
            _context.SaveChanges();
        }

        public void DeleteFabricanteRecord(int id)
        {
            var entity = _context.Fabricante.FirstOrDefault(t => t.id == id);
            _context.Fabricante.Remove(entity);
            _context.SaveChanges();
        }

        public Fabricante GetFabricanteSingleRecord(int id)
        {
            return _context.Fabricante.FirstOrDefault(t => t.id == id);
        }


        public List<Fabricante> GetFabricanteRecords(bool todo = false)
        {
            if (todo)
                return _context.Fabricante.ToList();
            else
                return (from ep in _context.Fabricante
                        where ep.estado
                        select new Fabricante
                        {
                            id = ep.id,
                            nombre = ep.nombre
                        }).ToList();
        }
        #endregion

        #region Catalogo
        public void AddCatalogo(Catalogo cat)
        {
            _context.Catalogo.Add(cat);
            _context.SaveChanges();
        }

        public void UpdateCatalogoRecord(Catalogo cat)
        {
            var dbEntry = _context.Entry(cat);

            dbEntry.Property(x => x.id).IsModified = false;
            dbEntry.Property(x => x.estado).IsModified = false;
            dbEntry.Property(x => x.fechaCreacion).IsModified = false;

            _context.Catalogo.Update(cat);
            _context.SaveChanges();
        }

        public void DeleteCatalogoRecord(int id)
        {
            var entity = _context.Catalogo.FirstOrDefault(t => t.id == id);
            _context.Catalogo.Remove(entity);
            _context.SaveChanges();
        }

        public Catalogo GetCatalogoSingleRecord(int id)
        {
            return _context.Catalogo.FirstOrDefault(t => t.id == id);
        }

        public List<Catalogo> GetCatalogoRecords(bool todo = false)
        {
            if (todo)
                return _context.Catalogo.ToList();
            else
                return _context.Catalogo.Where(x => x.estado).ToList();
        }
        #endregion
    }
}
