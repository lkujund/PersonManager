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
    /// Interaction logic for ListPeopleOfSubjectPage.xaml
    /// </summary>
    public partial class ListPeopleOfSubjectPage : FramedPage
    {
        private readonly Subject? subject;
        public ListPeopleOfSubjectPage(PersonSubjectViewModel personSubjectViewModel,
            Subject? subject = null
            )
            : base(personSubjectViewModel)
        {
            InitializeComponent();
            lvPeople.ItemsSource = personSubjectViewModel.People;
            this.subject = subject;
            lbSubject.Content = $"People lecturing {subject.SubjectName}";
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame?.Navigate(new AddPersonToSubjectPage(PersonSubjectViewModel, subject)
            {
                Frame = Frame
            });
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvPeople.SelectedItem != null)
            {
                PersonSubjectViewModel.People.Remove((lvPeople.SelectedItem as Person)!);
            }
        }

    }
}
