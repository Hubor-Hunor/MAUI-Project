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
        public async Task LoadPersonAsync()
        {
            //var token = await SecureStorage.GetAsync("auth_token");
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
                // Kezeld a hibát
                Console.WriteLine($"Hiba történt: {ex.Message}");
            }
        }
    }
}