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

        [HttpGet("{id}")]
        public async Task<ActionResult<Libro>> GetLibroAsync(int id, bool eager = false)
        {
            var Libro = await libroService.GetLibroAsync(id, eager);
            return Libro == null ? NotFound() : Ok((LibroDto)Libro);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LibroDto>>> GetLibrosAsync(bool eager = false)
        {
            return Ok((await libroService.GetAllLibrosAsync(eager)).Select(libro => (LibroDto)libro));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLibroAsync(int id)
        {
            try
            {
                await libroService.DeleteLibroAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateLibroAsync(Libro Libro, int? autorId, int? editorialId)
        {
            try
            {
                await libroService.CreateLibroAsync(Libro, autorId, editorialId);
                return CreatedAtAction("GetLibro", new { id = Libro.Id }, (LibroDto)Libro);
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
