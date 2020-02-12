using System;

namespace WingsOn.DTO
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public DateTime DateBirth { get; set; }
        public GenderTypeDTO Gender { get; set; }        
        public string Address { get; set; }
        public string Email { get; set; }
    }    
}
