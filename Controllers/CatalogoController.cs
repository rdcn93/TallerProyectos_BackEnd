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
    public class CatalogoController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public CatalogoController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Catalogo> Get()
        {
            return _dataAccessProvider.GetCatalogoRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Catalogo cat)
        {
            if (ModelState.IsValid)
            {
                cat.estado = true;
                cat.fechaCreacion = DateTime.Now;

                _dataAccessProvider.AddCatalogo(cat);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public Catalogo Details(int id)
        {
            return _dataAccessProvider.GetCatalogoSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Catalogo cat)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateCatalogoRecord(cat);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetCatalogoSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteCatalogoRecord(id);
            return Ok();
        }
    }
}
