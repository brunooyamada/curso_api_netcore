﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Security
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public string Iusser { get; set; }
        public int Seconds { get; set; }
    }
}
