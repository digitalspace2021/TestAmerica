using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos
{
    public class BasicQueryFilterDto
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Filter { get; set; }
        public string ColumnOrder { get; set; }
        public string DirectionOrder { get; set; }
    }
}
