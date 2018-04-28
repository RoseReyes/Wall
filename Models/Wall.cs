using System;
using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class Messages
    {
            [Required]
            public string user_id { get; set; }

            [Required] 
            [MaxLength(255)] 
            public string message { get; set; }
    }
}