using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
using Infrastructure.Data;
using Infrastructure.Data.Libreria;
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
        private readonly IAutorService autorService;

        public AutoresController(IAutorService autorService)
        {
            this.autorService = autorService;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AutorDto>> GetAutorAsync(int Id, bool eager = false)
        {
            var autor = await autorService.GetAutorAsync(Id, eager);
            return autor == null ? NotFound() : Ok((AutorDto)autor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AutorDto>>> GetAutoresAsync(bool eager = false)
        {
            return Ok((await autorService.GetAllAutoresAsync(eager)).Select(autor => (AutorDto)autor));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAutorAsync(int id)
        {
            try
            {
                await autorService.DeleteAutorAsync(id);
                return Ok();
            }
            catch (EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateAutorAsync(Autor autor)
        {
            await autorService.CreateAutorAsync(autor);
            return CreatedAtAction("GetAutor", new { id = autor.Id }, (AutorDto)autor);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAutorAsync(Autor autor)
        {
            await autorService.UpdateAutorAsync(autor);
            return Ok();
        }

        [HttpPut("{autorId}/Agregar-Libro/{libroId}")]
        public async Task<ActionResult> AgregarLibro(int libroId, int autorId)
        {
            try
            {
                await autorService.AgregarLibroAsync(libroId, autorId);
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
                await autorService.RemoverLibroAsync(libroId, autorId);
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
