using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace MauiFrontend
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Person> PersonList { get; private set; } = new ObservableCollection<Person>();

        [ObservableProperty]
        Person selectedListItem;
    
        public MainPageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        [RelayCommand]
        public async Task NavigateToCreatePersonPageAsync()
        {
            await Shell.Current.GoToAsync("addnewperson");
        }
        [RelayCommand]
        public async Task ShowPersonDetailsAsync()
        {
            if (selectedListItem != null)
            {
                var param = new ShellNavigationQueryParameters
                {
                    {"Person",selectedListItem}
                };
                await Shell.Current.GoToAsync("persondetails", param);
            }

        }
        //[RelayCommand]
        //public async Task UpdatePersonAsync()
        //{
        //    await Shell.Current.GoToAsync("updateperson");
        //}
        [RelayCommand]  
        public async Task DeletePersonAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"person/{id}");

            if (response.IsSuccessStatusCode)
            {
                await LoadPersonAsync(); 
            }

        }
        public async Task LoadPersonAsync()
        {
           
            try
            {
                var people = await _httpClient.GetFromJsonAsync<Person[]>($"Person");
                if (people != null)
                {
                    PersonList.Clear();
                    foreach (var person in people)
                    {
                        Random random = new Random();
                        int num = random.Next(1, 53);
                        person.ImageURL = $"https://xsgames.co/randomusers/assets/avatars/pixel/{num}.jpg";
                        PersonList.Add(person);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
    }
}