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

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Autor>> GetAutorAsync(int id)
        {
            var autor = await autorService.GetAutorAsync(id);
            return autor == null ? NotFound() : Ok(autor);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutoresAsync()
        {
            return Ok(await autorService.GetAllAutoresAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAutorAsync(int id)
        {
            await autorService.DeleteAutorAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAutorAsync(Autor autor)
        {
            await autorService.CreateAutorAsync(autor);
            return CreatedAtAction("GetAutor", new { id = autor.Id }, autor);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAutorAsync(Autor autor)
        {
            await autorService.UpdateAutorAsync(autor);
            return Ok();
        }

        [HttpPut("Agregar-Libro")]
        public async Task<ActionResult> AgregarLibro(int isbn, int autorId)
        {
            try
            {
                await autorService.AgregarLibroAsync(isbn, autorId);
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

        [HttpPut("Remover-Libro")]
        public async Task<ActionResult> RemoverLibro(int isbn, int autorId)
        {
            try
            {
                await autorService.RemoverLibroAsync(isbn, autorId);
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
