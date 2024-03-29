﻿using LesBooks.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LesBooks.Application.Requests
{
    public class CreateAdressRequest
    {
        public string identification { get; set; }
        [Required]
        public int id_client { get; set; }   
        [Required]
        public string street { get; set; }
        [Required] public string number { get; set; }
        public string obs { get; set; }
        [Required] public string zipCode { get; set; }
        [Required] public string neighborhood { get; set; }
        [Required] public string city { get; set; }
        [Required] public string state { get; set; }
        [Required] public string country { get; set; }
        [Required]
        public TypeAdress typeAdress { get; set; }
        [Required] public TypeResidence typeResidence { get; set; }
        [Required] public TypeStreet typeStreet { get; set; }
    }
}
