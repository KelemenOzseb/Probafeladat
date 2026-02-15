using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entity.Dtos.Camel
{
    public class UpdateCamelDto
    {
        public string? Name { get; set; }
        public string? Color { get; set; }

        [Range(1, 2)]
        public int? HumpCount { get; set; }
        public DateTime? LastFed { get; set; }
    }
}
