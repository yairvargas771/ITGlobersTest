using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Domain.Libreria
{
    public class Producto : Entity<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public Categoria Categoria { get; set; }
        public string Unidad { get; set; }

        public Producto() { }

        public static explicit operator Producto(ProductosDto productoDto)
        {
            if (productoDto.Categoria != null)
                productoDto.Categoria = null;

            return new Producto()
            {
                Id = productoDto.Id,
                Nombre = productoDto.Nombre,
                Descripcion = productoDto.Descripcion,
                Precio = productoDto.Precio,
                Cantidad = productoDto.Cantidad,
                Categoria = productoDto.Categoria
            };
        }
    }
}
