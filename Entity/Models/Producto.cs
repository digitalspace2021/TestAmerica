using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    [Table("PRODUCTO", Schema = "dbo")]
    public class Producto
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Familia { get; set; }
        public decimal Precio { get; set; }
    }
}
