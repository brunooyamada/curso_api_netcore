﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Dtos.User
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
