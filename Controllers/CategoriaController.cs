using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TallerProyectos_BackEnd.DataAccess;
using TallerProyectos_BackEnd.Models;

namespace TallerProyectos_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CategoriaController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Categoria> Get()
        {
            return _dataAccessProvider.GetCategoriaRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Categoria cat)
        {
            if (ModelState.IsValid)
            {
                cat.estado = true;
                cat.fechaRegistro = DateTime.Now;

                _dataAccessProvider.AddCategoriaRecord(cat);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public Categoria Details(int id)
        {
            return _dataAccessProvider.GetCategoriaSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Categoria cat)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCategoriaRecord(cat);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetCategoriaSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCategoriaRecord(id);
            return Ok();
        }
    }
}
