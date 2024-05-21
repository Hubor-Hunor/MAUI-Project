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
        string personName;

        [ObservableProperty]  
        int personAge;

        [ObservableProperty]  
        string personGender;

        [JsonIgnore]
        public string ImageURL { get; set; }

        [ObservableProperty]
        bool isDone;
    }

}
