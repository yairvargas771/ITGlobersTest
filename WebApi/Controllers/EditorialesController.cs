using Application.Exceptions;
using Application.Libreria.Specifications;
using Domain.Libreria;
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
    public class EditorialesController : ControllerBase
    {
        private readonly IEditorialService editorialService;

        public EditorialesController(IEditorialService EditorialService)
        {
            this.editorialService = EditorialService;
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Editorial>> GetEditorialAsync(int id)
        {
            var Editorial = await editorialService.GetEditorialAsync(id);
            return Editorial == null ? NotFound() : Ok(Editorial);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editorial>>> GetEditorialesAsync()
        {
            return Ok(await editorialService.GetAllEditorialesAsync());
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteEditorialAsync(int id)
        {
            await editorialService.DeleteEditorialAsync(id);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateEditorialAsync(Editorial Editorial)
        {
            await editorialService.CreateEditorialAsync(Editorial);
            return CreatedAtAction("GetEditorial", new { id = Editorial.Id }, Editorial);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEditorialAsync(Editorial Editorial)
        {
            await editorialService.UpdateEditorialAsync(Editorial);
            return Ok();
        }

        [HttpPut("Agregar-Libro")]
        public async Task<ActionResult> AgregarLibro(int isbn, int editorialId)
        {
            try
            {
                await editorialService.AgregarLibroAsync(isbn, editorialId);
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
        public async Task<ActionResult> RemoverLibro(int isbn, int editorialId)
        {
            try
            {
                await editorialService.RemoverLibroAsync(isbn, editorialId);
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
