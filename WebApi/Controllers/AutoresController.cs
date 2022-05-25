using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Categorias;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorService productoService;

        public AutoresController(IAutorService autorService)
        {
            this.productoService = autorService;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductosDto>> GetAutorAsync(int Id, bool eager = false)
        {
            var autor = await productoService.GetAutorAsync(Id, eager);
            return autor == null ? NotFound() : Ok((ProductosDto)autor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductosDto>>> GetAutoresAsync(bool eager = false)
        {
            return Ok((await productoService.GetAllAutoresAsync(eager)).Select(autor => (ProductosDto)autor));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutorAsync(int id)
        {
            try
            {
                await productoService.DeleteAutorAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAutorAsync(Producto autor)
        {
            await productoService.CreateAutorAsync(autor);
            return CreatedAtAction("GetAutor", new { id = autor.Id }, (ProductosDto)autor);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAutorAsync(Producto autor)
        {
            await productoService.UpdateAutorAsync(autor);
            return Ok();
        }

        [HttpPut("{autorId}/Agregar-Libro/{libroId}")]
        public async Task<ActionResult> AgregarLibro(int libroId, int autorId)
        {
            try
            {
                await productoService.AgregarLibroAsync(libroId, autorId);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityIsAlreadyRelatedToException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{autorId}/Remover-Libro/{libroId}")]
        public async Task<ActionResult> RemoverLibro(int libroId, int autorId)
        {
            try
            {
                await productoService.RemoverLibroAsync(libroId, autorId);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (EntityIsNotRelatedToException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
