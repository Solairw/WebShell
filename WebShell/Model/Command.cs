using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebShell.Model
{
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

    }
}
