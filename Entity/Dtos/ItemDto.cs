using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos
{
    public class ItemDto
    {
        public string NumPedido  { get; set; }
        public string CodProd { get; set; }
        public decimal Precio { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Subtotal { get; set; }

        public decimal TotalVentas { get; set; }
        public string Departamento { get; set; }
            
    }
}
