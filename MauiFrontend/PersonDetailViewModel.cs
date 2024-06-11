using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MauiFrontend
{
    public class PersonDetailViewModel : ObservableObject
    {
        private Person _selectedPerson;
        public ObservableCollection<Workout> WorkoutList { get; private set; } = new ObservableCollection<Workout>();

        private readonly HttpClient _httpClient;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => SetProperty(ref _selectedPerson, value);
        }

        public PersonDetailViewModel(Person selectedPerson, HttpClient httpClient)
        {
            SelectedPerson = selectedPerson;
            _httpClient = httpClient;
        }
        public async Task LoadWorkoutAsync()
        {
            try
            {
                var workout = await _httpClient.GetFromJsonAsync<Workout[]>($"person/{_selectedPerson.PersonId}");
                if (workout != null)
                {
                    WorkoutList.Clear();
                }
            }
            catch (Exception ex)
            {
                // Kezeld a hibát
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
    }
}
