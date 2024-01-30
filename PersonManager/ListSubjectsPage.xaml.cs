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
    /// Interaction logic for ListSubjectsPage.xaml
    /// </summary>
    public partial class ListSubjectsPage : FramedPage
    {
        public ListSubjectsPage(SubjectViewModel subjectViewModel) :
            base(subjectViewModel)
        {
            InitializeComponent();
            lvSubjects.ItemsSource = subjectViewModel.Subjects;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame?.Navigate(new EditSubjectPage(SubjectViewModel)
            {
                Frame = Frame
            });
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvSubjects.SelectedItem != null)
            {
                Frame?.Navigate(new EditSubjectPage(
                    SubjectViewModel,
                    lvSubjects.SelectedItem as Subject
                    )
                {
                    Frame = Frame
                });
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lvSubjects.SelectedItem != null)
            {
                SubjectViewModel.Subjects.Remove((lvSubjects.SelectedItem as Subject)!);
            }
        }

        private void BtnViewSubjectPeople_Click(object sender, RoutedEventArgs e)
        {
            if (lvSubjects.SelectedItem != null)
            {
                Frame?.Navigate(new ListPeopleOfSubjectPage(
                    new PersonSubjectViewModel(lvSubjects.SelectedItem as Subject),
                    lvSubjects.SelectedItem as Subject
                    )
                {
                    Frame = Frame
                });
            }
        }
    }
}
