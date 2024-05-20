using System.Net.Http.Json;
using MauiFrontend.Services;

namespace MauiFrontend
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel viewModel;

        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.LoadPersonAsync();
        }
    }
}