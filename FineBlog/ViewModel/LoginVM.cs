﻿using Microsoft.Build.Framework;

namespace FineBlog.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
