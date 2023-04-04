using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    [Table("PEDIDO", Schema = "dbo")]
    public class Pedido
    {
        public string Id { get; set; }
        public string CodCli { get; set; }
        public DateTime Fecha { get; set; }
        public string CodVend { get; set; }
    }
}
