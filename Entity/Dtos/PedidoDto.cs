using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos
{
    public class PedidoDto
    {
        public string NumPedido { get; set; }
        public string CodCli { get; set; }
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string CodVend { get; set; }
        public string Vendedor { get; set; }
        public decimal Comision { get; set; }

    }
}
