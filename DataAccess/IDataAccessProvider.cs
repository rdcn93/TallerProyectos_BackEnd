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

        #region Cliente
        void AddClienteRecord(Cliente usuario);
        void UpdateClienteRecord(Cliente usuario);
        void DeleteClienteRecord(int id);
        Cliente GetClienteSingleRecord(int id);
        Cliente GetClienteByEmail(string email);
        bool ExisteClienteByEmail(string email);
        bool ExisteClienteByUsuario(string usuario);
        List<Cliente> GetClienteRecords();
        #endregion


        #region Producto
        void AddProductoRecord(Producto producto);
        void UpdateProductoRecord(Producto producto);
        void DeleteProductoRecord(int id);
        Producto GetProductoSingleRecord(int id);
        List<Producto> GetProductoRecords();
        List<Producto> ProductosByCategoria(int id);
        List<Producto> ProductosByCatalogo(int id);
        List<Producto> ProductosByFabricante(int id);
        #endregion

        #region Categoria
        public void AddCategoriaRecord(Categoria cat);
        public void UpdateCategoriaRecord(Categoria cat);
        public void DeleteCategoriaRecord(int id);
        public Categoria GetCategoriaSingleRecord(int id);
        public List<Categoria> GetCategoriaRecords(bool todo = false);
        #endregion

        #region Catalogo
        public void AddCatalogo(Catalogo cat);

        public void UpdateCatalogoRecord(Catalogo cat);

        public void DeleteCatalogoRecord(int id);

        public Catalogo GetCatalogoSingleRecord(int id);

        public List<Catalogo> GetCatalogoRecords(bool todo = false);
        #endregion

        #region Fabricante
        public void AddFabricanteRecord(Fabricante cat);

        public void UpdateFabricanteRecord(Fabricante cat);

        public void DeleteFabricanteRecord(int id);

        public Fabricante GetFabricanteSingleRecord(int id);

        public List<Fabricante> GetFabricanteRecords(bool todo = false);
        #endregion
    }
}
