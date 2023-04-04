using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Models
{
    [Table("DEPARTAMENTO", Schema = "dbo")]
    public class Departamento
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
    }
}
