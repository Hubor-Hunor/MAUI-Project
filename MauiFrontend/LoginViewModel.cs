using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiFrontend
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private string errorMessage;

        public LoginViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        [RelayCommand]
        public async Task LoginAsync()
        {
            ErrorMessage = string.Empty;

            var loginModel = new { UserName, Password };

            try
            {
                var response = await _httpClient.PostAsJsonAsync("Auth/login", loginModel);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        var result = JsonSerializer.Deserialize<LoginResponse>(responseContent);

                        if (result != null)
                        {
                            // Store the JWT token (e.g., in SecureStorage)
                            await SecureStorage.SetAsync("auth_token", result.Token);

                            // Navigate to the main page
                            await Shell.Current.GoToAsync("//MainPage");
                        }
                    }
                    catch (JsonException ex)
                    {
                        ErrorMessage = $"Failed to deserialize the response: {ex.Message}";
                    }
                }
                else
                {
                    ErrorMessage = $"Login failed: {response.ReasonPhrase}";
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error content: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred: {ex.Message}";
            }
        }
    }
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }
    }
}
