namespace MauiFrontend;

public partial class AddNewPersonPage : ContentPage
{
    private readonly CreatePersonViewModel viewModel;

    public AddNewPersonPage(CreatePersonViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = this.viewModel;
    }
}