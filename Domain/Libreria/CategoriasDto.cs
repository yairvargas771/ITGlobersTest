using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Libreria
{
    public class CategoriasDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IList<Producto> Productos { get; set; }

        public static explicit operator CategoriasDto(Categoria categoria)
        {
            if (categoria.Productos != null)
                categoria.Productos = categoria.Productos
                    .Select(producto => { producto.Categoria = null; return producto; })
                    .ToList();

            return new CategoriasDto()
            {
                Id = categoria.Id,
                Productos = categoria.Productos,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
        }
    }
}
