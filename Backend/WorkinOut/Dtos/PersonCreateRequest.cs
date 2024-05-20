using System.ComponentModel.DataAnnotations;
using WorkinOut.Models;

namespace WorkinOut.Dtos
{
    public class PersonCreateRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string PersonName { get; set; } = "";

        [Required]
        [Range(6, 99)]
        public int PersonAge { get; set; }

        [Required]
        public string PersonGender { get; set; } = "";

        public string? PersonIdentity { get; set; }


        public PersonCreateRequest() 
        {
            Console.WriteLine();
        }

        public PersonCreateRequest(string personName, int personAge, string personGender, string? personIdentity)
        {
            PersonName = personName;
            PersonAge = personAge;
            PersonGender = personGender;
            PersonIdentity = personIdentity;
            
        }
    }
}