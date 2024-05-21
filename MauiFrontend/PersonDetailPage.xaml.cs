
namespace MauiFrontend;
[QueryProperty(nameof(Person), "Person")]
public partial class PersonDetailPage : ContentPage
{
    Person person;
    public Person Person
    {
        get => person;
        set
        {
            person = value;
            OnPropertyChanged();
        }
    }
    public PersonDetailPage()
    {
        InitializeComponent();
        BindingContext = this;
    }
}