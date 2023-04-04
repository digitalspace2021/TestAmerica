using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos
{
    public class ClienteDto
    {
        public string CodCli { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int Cupo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Canal { get; set; }
        public string CodVend { get; set; }
        public string CodCiu { get; set; }
    }
}
