﻿namespace MVCSmartClient01.Models
{
    public class LoginViewModel : HeaderViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}