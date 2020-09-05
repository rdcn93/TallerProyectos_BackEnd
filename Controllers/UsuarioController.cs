using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerProyectos_BackEnd.DataAccess;
using TallerProyectos_BackEnd.Models;

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
            if (ModelState.IsValid)
            {
                //Guid obj = Guid.NewGuid();
                //Usuario.id = obj.ToString();
                _dataAccessProvider.AddUsuarioRecord(Usuario);
                return Ok();
            }
            return BadRequest();
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
    }
}