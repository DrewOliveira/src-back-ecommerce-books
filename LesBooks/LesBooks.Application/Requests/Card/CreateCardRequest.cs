﻿using LesBooks.Model.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LesBooks.Application.Requests
{
    public class CreateCardRequest
    {
        [Required] public string number { get; set; }
        [Required] public string name { get; set; }
        [Required] public string securityCode { get; set; }
        [Required] public bool pricipal { get; set; }
        [Required] public Method method { get; set; }
    }
}
