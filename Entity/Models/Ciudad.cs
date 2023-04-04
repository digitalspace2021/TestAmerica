using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    [Table("CIUDAD", Schema = "dbo")]
    public class Ciudad
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string CodDep { get; set; }
    }
}
