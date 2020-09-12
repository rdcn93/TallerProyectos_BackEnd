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
            _context.Producto.Add(producto);
            _context.SaveChanges();
        }

        public void UpdateProductoRecord(Producto producto)
        {
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
            var productos = _context.Producto.ToList();
            return productos;
        }
        #endregion

    }
}
