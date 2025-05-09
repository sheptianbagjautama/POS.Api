﻿namespace POS.Api.DTOs
{
    public class CreateUserDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string>? Roles { get; set; }
    }
}
