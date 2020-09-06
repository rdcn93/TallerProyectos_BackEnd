using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerProyectos_BackEnd.DataAccess;
using TallerProyectos_BackEnd.Models;
using System.Security.Cryptography;
using System.Text;

namespace TallerProyectos_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public UsuarioController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return _dataAccessProvider.GetUsuarioRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody]Usuario Usuario)
        {
            bool existeEmail = _dataAccessProvider.ExisteUsuarioByEmail(Usuario.email);

            if(existeEmail)
                ModelState.AddModelError("email", "Ya existe registrado un usuario con ese email");

            if (ModelState.IsValid)
            {
                //convertir contraseña a hash
                if(Usuario.password != null && Usuario.password != "")
                    Usuario.password = GetSHA256(Usuario.password);

                _dataAccessProvider.AddUsuarioRecord(Usuario);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public Usuario Details(int id)
        {
            return _dataAccessProvider.GetUsuarioSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody]Usuario Usuario)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateUsuarioRecord(Usuario);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetUsuarioSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteUsuarioRecord(id);
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