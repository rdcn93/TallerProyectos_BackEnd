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
    public class ProductoController : ControllerBase
    {
        private readonly IDataAccessProvider _dataAccessProvider;

        public ProductoController(IDataAccessProvider dataAccessProvider)
        {
            _dataAccessProvider = dataAccessProvider;
        }

        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return _dataAccessProvider.GetProductoRecords();
        }

        [HttpPost]
        public IActionResult Registrar([FromBody]Producto producto)
        {
            if (ModelState.IsValid)
            {
                _dataAccessProvider.AddProductoRecord(producto);

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        public Producto Details(int id)
        {
            return _dataAccessProvider.GetProductoSingleRecord(id);
        }

        [HttpPut]
        public IActionResult Edit([FromBody]Producto Producto)
        {
            if (ModelState.IsValid)
            {
                Producto.fechaModificacion = DateTime.Now;

                _dataAccessProvider.UpdateProductoRecord(Producto);
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _dataAccessProvider.GetProductoSingleRecord(id);
            if (data == null)
            {
                return NotFound();
            }

            data.fechaModificacion = DateTime.Now;

            _dataAccessProvider.DeleteProductoRecord(id);
            return Ok();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IEnumerable<Producto> ProductosByCategoria(int id)
        {
            return _dataAccessProvider.ProductosByCategoria(id);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IEnumerable<Producto> ProductosByCatalogo(int id)
        {
            return _dataAccessProvider.ProductosByCatalogo(id);
        }
    }
}