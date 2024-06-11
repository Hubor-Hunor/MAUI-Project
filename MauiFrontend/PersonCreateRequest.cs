using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiFrontend
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

        public string? PersonIdentity { get; set; } = "";
    }
}