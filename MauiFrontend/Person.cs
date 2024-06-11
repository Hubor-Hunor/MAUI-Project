using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiFrontend
{
    public partial class Person : ObservableObject
    {
        [ObservableProperty]
        Guid personId;

        [ObservableProperty] 
        string personName;

        [ObservableProperty]  
        int personAge;

        [ObservableProperty]  
        string personGender;

        [ObservableProperty]
        [JsonIgnore]
        Workout[] workouts;

        [JsonIgnore]
        public string ImageURL { get; set; }
        

        [ObservableProperty]
        bool isDone;
    }

}
