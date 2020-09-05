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

        public List<Usuario> GetUsuarioRecords()
        {
            return _context.Usuario.ToList();
        }
    }
}
