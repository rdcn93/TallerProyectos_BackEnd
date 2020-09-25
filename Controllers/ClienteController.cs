using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TallerProyectos_BackEnd.DataAccess;
using TallerProyectos_BackEnd.Models;
using System.Security.Cryptography;
using System.Text;

namespace TallerProyectos_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public ClienteController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return _dataAccessProvider.GetClienteRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Cliente Cliente)
        {
            bool existeEmail = _dataAccessProvider.ExisteClienteByEmail(Cliente.email);
            bool existeUsuario = _dataAccessProvider.ExisteClienteByUsuario(Cliente.usuario);
            if (existeEmail)
                ModelState.AddModelError("email", "Ya existe registrado un cliente con ese email");

            if (existeUsuario)
                ModelState.AddModelError("email", "Ya existe registrado un cliente con ese usuario");

            if (ModelState.IsValid)
            {
                //convertir contraseña a hash
                if (Cliente.password != null && Cliente.password != "")
                    Cliente.password = GetSHA256(Cliente.password);

                _dataAccessProvider.AddClienteRecord(Cliente);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public Cliente Details(int id)
        {
            return _dataAccessProvider.GetClienteSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Cliente Cliente)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateClienteRecord(Cliente);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetClienteSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteClienteRecord(id);
            return Ok();
        }

        #region UTIL
        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        #endregion
    }
}
