using System.ComponentModel.DataAnnotations;

namespace WingsAPI.Models
{
    public class AddressModel
    {
        [Required(ErrorMessage = "Please enter Address.")]
        public string Address { get; set; }
    }
}
