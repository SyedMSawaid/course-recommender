﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class NewStudentDto
    {
        public string StudentId { get; set; }
        public string Contact { get; set; }
        public string Address { get; set; }
    }
}