﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Login.Models
{
    public sealed class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}