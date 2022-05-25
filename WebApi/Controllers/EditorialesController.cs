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

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriasDto>> GetEditorialAsync(int id, bool eager = false)
        {
            var Editorial = await editorialService.GetEditorialAsync(id, eager);
            return Editorial == null ? NotFound() : Ok((CategoriasDto)Editorial);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetEditorialesAsync(bool eager = false)
        {
            return Ok((await editorialService.GetAllEditorialesAsync(eager)).Select(editorial => (CategoriasDto)editorial));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEditorialAsync(int id)
        {
            try
            {
                await editorialService.DeleteEditorialAsync(id);
                return Ok();
            }
            catch(ReferenceConstrainViolationException e)
            {
                return BadRequest(e.Message);
            }
            catch(EntityNotFoundException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateEditorialAsync(Categoria Editorial)
        {
            await editorialService.CreateEditorialAsync(Editorial);
            return CreatedAtAction("GetEditorial", new { id = Editorial.Id }, (CategoriasDto)Editorial);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEditorialAsync(Categoria Editorial)
        {
            await editorialService.UpdateEditorialAsync(Editorial);
            return Ok();
        }

        [HttpPut("{editorialId}/Agregar-Libro/{libroId}")]
        public async Task<ActionResult> AgregarLibro(int libroId, int editorialId)
        {
            try
            {
                await editorialService.AgregarLibroAsync(libroId, editorialId);
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

        [HttpPut("{editorialId}/Remover-Libro/{libroId}")]
        public async Task<ActionResult> RemoverLibro(int libroId, int editorialId)
        {
            try
            {
                await editorialService.RemoverLibroAsync(libroId, editorialId);
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
