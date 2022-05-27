using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductosService productoService;

        public ProductosController(IProductosService productoService)
        {
            this.productoService = productoService;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductosDto>> GetProductoAsync(int Id, bool eager = false)
        {
            var autor = await productoService.GetProductoAsync(Id, eager);
            return autor == null ? NotFound() : Ok((ProductosDto)autor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductosDto>>> GetProductosAsync(bool eager = false)
        {
            return Ok((await productoService.GetAllProductosAsync(eager)).Select(producto => (ProductosDto)producto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutorAsync(int id)
        {
            try
            {
                await productoService.DeleteProductoAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateProductoAsync(Producto producto)
        {
            await productoService.CreateProductoAsync(producto);
            return CreatedAtAction("GetProducto", new { id = producto.Id }, (ProductosDto)producto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductoAsync(Producto autor)
        {
            await productoService.UpdateProductoAsync(autor);
            return Ok();
        }

        //[HttpPut("{autorId}/Agregar-Libro/{libroId}")]
        //public async Task<ActionResult> AgregarLibro(int libroId, int autorId)
        //{
        //    try
        //    {
        //        await productoService.AgregarLibroAsync(libroId, autorId);
        //        return Ok();
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //    catch (EntityIsAlreadyRelatedToException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        //[HttpPut("{autorId}/Remover-Libro/{libroId}")]
        //public async Task<ActionResult> RemoverLibro(int libroId, int autorId)
        //{
        //    try
        //    {
        //        await productoService.RemoverLibroAsync(libroId, autorId);
        //        return Ok();
        //    }
        //    catch (EntityNotFoundException e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //    catch (EntityIsNotRelatedToException e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
