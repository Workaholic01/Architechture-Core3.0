﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LVT.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60 , ErrorMessage ="Name can't be longer than 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Date of Birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Address is required")]
        [StringLength(100 , ErrorMessage ="Address can not be longer than 100 characters")]
        public string Address { get; set; }
    }
}
