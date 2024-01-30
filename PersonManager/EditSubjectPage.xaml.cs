using PersonManager.Models;
using PersonManager.Utils;
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
    /// Interaction logic for EditSubjectPage.xaml
    /// </summary>
    public partial class EditSubjectPage : FramedPage
    {
        private readonly Subject? subject;
        public EditSubjectPage(SubjectViewModel subjectViewModel,
            Subject? subject = null
            )
            :base(subjectViewModel)
        {
            InitializeComponent();
            this.subject = subject ?? new Subject();
            DataContext = subject;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Frame.NavigationService.GoBack();
        }

        private void BtnCommit_Click(object sender, RoutedEventArgs e)
        {
            if (FormValid())
            {
                subject!.SubjectName = tbSubjectName.Text.Trim();
                subject!.ECTS = int.Parse(tbEcts.Text.Trim());

                Frame?.NavigationService.GoBack();

                if (subject.IDSubject == 0)
                {
                    SubjectViewModel.Subjects.Add(subject);
                }
                else
                {
                    SubjectViewModel.UpdateSubject(subject);
                }
            }
        }

        private bool FormValid()
        {
            bool ok = true;
            grid.Children.OfType<TextBox>().ToList().ForEach(e =>
            {
                e.Background = Brushes.White;

                if (string.IsNullOrEmpty(e.Text.Trim())
                    || "Int".Equals(e.Tag) && !int.TryParse(e.Text.Trim(), out int r)
                    )
                {
                    ok = false;
                    e.Background = Brushes.LightCoral;
                }
            }
                );

            return ok;
        }
    }
}
