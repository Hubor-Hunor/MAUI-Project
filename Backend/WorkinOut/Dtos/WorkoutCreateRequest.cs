using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using Newtonsoft.Json;
using WorkinOut.Models;

namespace WorkinOut.Dtos
{
    public class WorkoutCreateRequest
    {
        public WorkoutCreateRequest(string username, string workoutDay, string muscleType, string workoutTime_Weights, string workoutTime_Cardio, string workoutDifficulty)
        {
            Username = username;
            WorkoutDay = workoutDay;
            MuscleType = muscleType;
            WorkoutTime_Weights = workoutTime_Weights;
            WorkoutTime_Cardio = workoutTime_Cardio;
            WorkoutDifficulty = workoutDifficulty;
        }

        public string Username { get; set; }
        public string WorkoutDay { get; set; }
        [JsonIgnore]
        [ValidateNever]
        public string MuscleType { get; set; } 

        public string WorkoutTime_Weights { get; set; } 

        public string WorkoutTime_Cardio { get; set; } 

        public string WorkoutDifficulty { get; set; } 


    }
}
