using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.Dtos.Camel
{
    public class CreateCamelDto
    {
        public string Name { get; set; }
        public string? Color { get; set; }
        public int? HumpCount { get; set; }
        public DateTime? LastFed { get; set; }
    }
}
