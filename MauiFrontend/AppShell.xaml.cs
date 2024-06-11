namespace MauiFrontend
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("persondetails", typeof(PersonDetailPage));
            Routing.RegisterRoute("addnewperson", typeof(AddNewPersonPage));
        }
    }
}
