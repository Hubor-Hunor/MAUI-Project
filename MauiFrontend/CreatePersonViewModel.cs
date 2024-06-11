using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiFrontend
{
    public partial class CreatePersonViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string personName;

        [ObservableProperty]
        private int personAge;

        [ObservableProperty]
        private string personGender;

        [ObservableProperty]
        [JsonIgnore]
        private string personIdentity;

        [ObservableProperty]
        private string errorMessage;

        public CreatePersonViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [RelayCommand]
        public async Task CreatePersonAsync()
        {
            var newPerson = new PersonCreateRequest
            {
                PersonName = PersonName,
                PersonAge = PersonAge,
                PersonGender = PersonGender,
                PersonIdentity = "PersonIdentity"
            };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Person", newPerson);

                if (response.IsSuccessStatusCode)
                {
                    await Shell.Current.GoToAsync(".."); // Navigate back
                }
                else
                {
                    ErrorMessage = $"Error creating person: {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An unexpected error occurred: {ex.Message}";
            }
        }
    }
}