﻿using System.Collections.Generic;

namespace Models
{
    public class CreateBookResponse
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
    }
}
