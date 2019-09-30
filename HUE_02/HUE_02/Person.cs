using System;
using System.ComponentModel.DataAnnotations;

namespace HUE_02
{
    public class Person
    {

        [Required]
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
