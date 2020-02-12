using System;
using System.ComponentModel.DataAnnotations;
using WingsAPI.Models;

namespace WingsAPI.Model
{
    public class PersonModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please add Name.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please add Date of birth.")]
        public DateTime DateBirth { get; set; }

        [Required(ErrorMessage = "Please add Gender.")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Please enter Email.")]
        public string Email { get; set; }
    }    
}
