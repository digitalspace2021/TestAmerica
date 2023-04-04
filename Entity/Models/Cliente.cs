using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    [Table("CLIENTE", Schema = "dbo")]
    public class Cliente
    {
        public string Id { get; set; }
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
