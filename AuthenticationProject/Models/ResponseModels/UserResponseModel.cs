﻿namespace AuthenticationProject.Models.ResponseModels
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
