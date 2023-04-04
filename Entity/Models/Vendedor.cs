using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    [Table("VENDEDOR", Schema = "dbo")]
    public class Vendedor
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
    }
}
