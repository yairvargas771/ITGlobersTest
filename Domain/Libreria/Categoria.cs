using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Domain.Libreria
{
    public class Categoria : Entity<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IList<Producto> Productos { get; set; }

        public Categoria() { }

        public static explicit operator Categoria(CategoriasDto categoriaslDto)
        {
            if (categoriaslDto.Productos != null)
                categoriaslDto.Productos = categoriaslDto.Productos
                    .Select(producto => { producto.Categoria = null; return producto; })
                    .ToList();

            return new Categoria()
            {
                Id = categoriaslDto.Id,
                Nombre = categoriaslDto.Nombre,
                Descripcion = categoriaslDto.Descripcion,
                Productos = categoriaslDto.Productos
            };
        }
    }
}