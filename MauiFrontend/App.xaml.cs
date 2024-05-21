using Microsoft.Extensions.DependencyInjection;

namespace MauiFrontend
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Set the MainPage to AppShell, which enables Shell navigation
            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }
    }
}
