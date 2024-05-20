using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WorkinOut.Models
{
    public class Account : IdentityUser
    {
        [Key]
        public Guid AccountId { get; set; }

        [StringLength(200)]

        public string FirstName { get; set; }

        [StringLength(200)]

        public string LastName { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual ICollection<Person>? Person { get; set; }
        [Required]
        public string? Role { get; set; }

        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public Account()
        {
            Person = new HashSet<Person>();
            AccountId = Guid.NewGuid();
            PasswordHash = "";
            PasswordSalt = "";
        }
    }
}
