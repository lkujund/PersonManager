using PersonManager.Dal;
using PersonManager.Models;
using PersonManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PersonManager
{
    /// <summary>
    /// Interaction logic for AddPersonToSubjectPage.xaml
    /// </summary>
    public partial class AddPersonToSubjectPage : FramedPage
    {
        private readonly Subject? subject;
        public AddPersonToSubjectPage(PersonSubjectViewModel personSubjectViewModel,
                   Subject? subject = null
                   )
                   : base(personSubjectViewModel)
        {
            InitializeComponent();
            this.subject = subject;
            lvPeople.ItemsSource = personSubjectViewModel.UnaddedPeople;
            lbSubject.Content = $"Add a person to {subject.SubjectName}";
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (lvPeople.SelectedItem != null)
            {
                RepositoryFactory.GetRepository().AddPersonToSubject(lvPeople.SelectedItem as Person, subject.IDSubject);
                Frame?.Navigate(new ListPeopleOfSubjectPage(new PersonSubjectViewModel(subject), subject)
                {
                    Frame = Frame
                });
            
            }
        }
    }
}
