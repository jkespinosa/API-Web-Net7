using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleCode.Models.DTO
{
    public class UserUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string LastName { get; set; }
    }
}
