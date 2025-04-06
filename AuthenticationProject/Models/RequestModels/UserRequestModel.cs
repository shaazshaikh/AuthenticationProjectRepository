﻿using System.ComponentModel.DataAnnotations;

namespace AuthenticationProject.Models.RequestModels
{
    public class UserRequestModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
