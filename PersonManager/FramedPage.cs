using PersonManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonManager
{
    public class FramedPage : Page
    {
        public FramedPage(PersonViewModel personViewModel)
        {
            PersonViewModel = personViewModel;
        }

        public FramedPage(SubjectViewModel subjectViewModel)
        {
            SubjectViewModel = subjectViewModel;
        }

        public FramedPage(PersonSubjectViewModel personSubjectViewModel)
        {
            PersonSubjectViewModel = personSubjectViewModel;
        }

        public PersonViewModel PersonViewModel { get; }
        public SubjectViewModel SubjectViewModel { get; }
        public PersonSubjectViewModel PersonSubjectViewModel { get; }

        public Frame? Frame { get; set; }
    }
}
