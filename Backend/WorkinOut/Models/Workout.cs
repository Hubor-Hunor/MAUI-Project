using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WorkinOut.Models
{
    public class Workout
    {
        [Key] 
        public Guid WorkoutId { get; set; }

        [Required] 
        public DateTime WorkoutTime { get; set; }

        // Foreign key
        public Guid PersonId { get; set; }

        [JsonIgnore]
        [ValidateNever]
        public Person Person { get; set; }

        public string MuscleType { get; set; } = "0";

        public string WorkoutTime_Weights { get; set; } = "0";

        public string WorkoutTime_Cardio { get; set; } = "0";

        public string WorkoutDifficulty { get; set; } = "0";

        public string WorkoutDay { get; set; } = "0";

        public Workout()
        {
            WorkoutId = Guid.NewGuid();
        }

        public enum MuscleTypes
        {
            Chest,
            Legs,
            Biceps,
            Triceps,
            Shoulders,
            Back
        }

        public enum WorkoutDifficulties
        {
            Easy,
            Medium,
            Hard,
            Extreme
        }
    }
}