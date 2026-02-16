using Entity.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entity.Models
{
    public class Camel : IIdentity
    {
        public Camel(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Color { get; set; }
        public int? HumpCount { get; set; }
        public DateTime? LastFed { get; set; }
    }
}
