using System.Collections.Generic;
using System.Linq;

namespace Domain.Libreria
{
    public class ProductosDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public Categoria Categoria { get; set; }
        public string Unidad { get; set; }

        public static explicit operator ProductosDto(Producto producto)
        {
            if (producto.Categoria != null)
                producto.Categoria = null;

            return new ProductosDto()
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Cantidad = producto.Cantidad,
                Categoria = producto.Categoria,
                Unidad = producto.Unidad
            };
        }
    }
}
