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
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService categoriaService;

        public CategoriasController(ICategoriasService categoriaService)
        {
            this.categoriaService = categoriaService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoriasDto>> GetCategoriaAsync(int id, bool eager = false)
        {
            var categoria = await categoriaService.GetCategoriaAsync(id, eager);
            return categoria == null ? NotFound() : Ok((CategoriasDto)categoria);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetEditorialesAsync(bool eager = false)
        {
            return Ok((await categoriaService.GetAllCategoriasAsync(eager)).Select(editorial => (CategoriasDto)editorial));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategoriaAsync(int id)
        {
            try
            {
                await categoriaService.DeleteCategoriaAsync(id);
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
        public async Task<ActionResult> CreateCategoriaAsync(Categoria categoria)
        {
            await categoriaService.CreateCategoriaAsync(categoria);
            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, (CategoriasDto)categoria);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEditorialAsync(Categoria categoria)
        {
            await categoriaService.UpdateCategoriaAsync(categoria);
            return Ok();
        }

        [HttpPut("{categoriaId}/Agregar-Producto/{productoId}")]
        public async Task<ActionResult> AgregarProductoACategoria(int productoId, int categoriaId)
        {
            try
            {
                await categoriaService.AgregarProductoAsync(productoId, categoriaId);
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

        [HttpPut("{categoriaId}/Remover-Producto/{productoId}")]
        public async Task<ActionResult> RemoverProductoDeCategoria(int productoId, int categoriaID)
        {
            try
            {
                await categoriaService.RemoverProductoAsync(productoId, categoriaID);
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
