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
    public class FabricanteController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public FabricanteController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Fabricante> Get()
        {
            return _dataAccessProvider.GetFabricanteRecords();
        }

        [HttpPost]
        public IActionResult Create([FromBody] Fabricante cat)
        {
            if (ModelState.IsValid)
            {
                cat.estado = true;
                cat.fechaRegistro = DateTime.Now;

                _dataAccessProvider.AddFabricanteRecord(cat);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public Fabricante Details(int id)
        {
            return _dataAccessProvider.GetFabricanteSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Fabricante cat)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.UpdateFabricanteRecord(cat);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetFabricanteSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }
            _dataAccessProvider.DeleteFabricanteRecord(id);
            return Ok();
        }
    }
}
