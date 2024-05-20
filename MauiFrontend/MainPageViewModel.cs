using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace MauiFrontend
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        public ObservableCollection<Person> PersonList { get; private set; } = new ObservableCollection<Person>();

        [ObservableProperty]
        private Person selectedListItem;

        public MainPageViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task LoadPersonAsync()
        {
            try
            {
                var people = await _httpClient.GetFromJsonAsync<Person[]>("Person");
                if (people != null)
                {
                    PersonList.Clear();
                    foreach (var person in people)
                    {
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