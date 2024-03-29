﻿using System.ComponentModel.DataAnnotations;

namespace VSMSignalRPart2Demo.Models
{
    public class Person
    {
        [Required]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}