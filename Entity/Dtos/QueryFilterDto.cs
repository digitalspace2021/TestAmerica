using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos
{
    public class QueryFilterDto : BasicQueryFilterDto
    {
        public decimal? Extra { get; set; }
        public int? ForeignKey { get; set; }
        public string NameForeignKey { get; set; }
    }
}
