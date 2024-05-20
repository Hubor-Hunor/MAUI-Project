namespace MauiFrontend
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var services = new ServiceCollection();
            services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://localhost:5217/") });
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<MainPage>();

            var serviceProvider = services.BuildServiceProvider();
            var mainPage = serviceProvider.GetService<MainPage>();
            MainPage = mainPage;
        }
    }
}
