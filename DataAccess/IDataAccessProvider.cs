using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerProyectos_BackEnd.Models;

namespace TallerProyectos_BackEnd.DataAccess
{
    public interface IDataAccessProvider
    {
        void AddUsuarioRecord(Usuario usuario);
        void UpdateUsuarioRecord(Usuario usuario);
        void DeleteUsuarioRecord(int id);
        Usuario GetUsuarioSingleRecord(int id);
        List<Usuario> GetUsuarioRecords();
    }
}
