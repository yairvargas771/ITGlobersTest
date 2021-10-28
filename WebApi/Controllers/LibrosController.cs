using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroService libroService;

        public LibrosController(ILibroService LibroService)
        {
            this.libroService = LibroService;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Libro>> GetLibroAsync(int id)
        {
            var Libro = await libroService.GetLibroAsync(id);
            return Libro == null ? NotFound() : Ok(Libro);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Libro>>> GetLibrosAsync()
        {
            return Ok(await libroService.GetAllLibrosAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteLibroAsync(int id)
        {
            await libroService.DeleteLibroAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateLibroAsync(Libro Libro, int? autorId, int? editorialId)
        {
            try
            {
                await libroService.CreateLibroAsync(Libro, autorId, editorialId);
                return CreatedAtAction("GetLibro", new { id = Libro.Id }, Libro);
            }
            catch (EntityAlreadyExistException e)
            {
                return BadRequest(e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateLibroAsync(Libro Libro)
        {
            await libroService.UpdateLibroAsync(Libro);
            return Ok();
        }
    }
}
