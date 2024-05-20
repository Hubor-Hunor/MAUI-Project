using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorkinOut.Models
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string PersonName { get; set; } = "";

        [Required]
        [Range(6, 99)]
        public int PersonAge { get; set; }  // Change to int for age

        [Required]
        public string PersonGender { get; set; }

        // Foreign key

        // Navigation property
        public Account Account { get; set; }

        // Navigation property
        public ICollection<Workout> Workouts { get; set; }

        public string? PersonIdentity { get; set; }

        public Person()
        {
            Workouts = new List<Workout>();
            PersonId = Guid.NewGuid();
        }

        public enum Gender
        {
            Male, Female
        }
    }
}
