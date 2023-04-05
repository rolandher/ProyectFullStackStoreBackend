﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdmonStoreDomain.Entities.Commands
{
    public class UpdateUser
    {
        [Required(ErrorMessage = "Fire is required")]
        public string Fire_Id { get; set; }
        [Required(ErrorMessage = "Names is required")]
        public string Names { get; set; }
        [Required(ErrorMessage = "Surnames is required")]
        public string SurNames { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }
    }
}
