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
    public class LoginController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public LoginController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpPost]
        public IActionResult Login([FromBody]Usuario Usuario)
        {
            bool existeEmail = _dataAccessProvider.ExisteUsuarioByEmail(Usuario.email);

            if (!existeEmail)
                ModelState.AddModelError("email", "No existe usuario registrado con ese email");
            else
            {
                Usuario infoUsu = _dataAccessProvider.GetUsuarioByEmail(Usuario.email);

                if(GetSHA256(Usuario.password) == infoUsu.password)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            return BadRequest(ModelState);
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