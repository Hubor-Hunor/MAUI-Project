using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiFrontend
{
    public class Workout : ObservableObject
    {
        [Key]
        public Guid WorkoutId ;

        [Required]
        public DateTime WorkoutTime ;

        // Foreign key
        public Guid PersonId ;

        [JsonIgnore]
        public Person Person ;

        public string MuscleType ;  

        public string WorkoutTime_Weights ;  

        public string WorkoutTime_Cardio ;  

        public string WorkoutDifficulty ;  

        public string WorkoutDay ;  

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