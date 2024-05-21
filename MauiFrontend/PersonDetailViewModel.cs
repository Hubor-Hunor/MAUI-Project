using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiFrontend
{
    public class PersonDetailViewModel : ObservableObject
    {
        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get => _selectedPerson;
            set => SetProperty(ref _selectedPerson, value);
        }

        public PersonDetailViewModel(Person selectedPerson)
        {
            SelectedPerson = selectedPerson;
        }
    }
}
