using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
