using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerProyectos_BackEnd.Models;

namespace TallerProyectos_BackEnd.DataAccess
{
    public interface IDataAccessProvider
    {
        #region Usuario
        void AddUsuarioRecord(Usuario usuario);
        void UpdateUsuarioRecord(Usuario usuario);
        void DeleteUsuarioRecord(int id);
        Usuario GetUsuarioSingleRecord(int id);
        Usuario GetUsuarioByEmail(string email);
        bool ExisteUsuarioByEmail(string email);
        List<Usuario> GetUsuarioRecords();
        #endregion


        #region Producto
        void AddProductoRecord(Producto producto);
        void UpdateProductoRecord(Producto producto);
        void DeleteProductoRecord(int id);
        Producto GetProductoSingleRecord(int id);
        List<Producto> GetProductoRecords();
        #endregion
    }
}
