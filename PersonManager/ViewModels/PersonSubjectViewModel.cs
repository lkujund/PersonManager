using PersonManager.Dal;
using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.ViewModels
{
    public class PersonSubjectViewModel
    {
        public ObservableCollection<Person> People { get; }
        public ObservableCollection<Person> UnaddedPeople { get; }
        private readonly Subject? subject;
        public PersonSubjectViewModel(Subject? subject)
        {
            People = new ObservableCollection<Person>(
                RepositoryFactory.GetRepository().GetPeopleForSubject(subject.IDSubject)
                );
            UnaddedPeople = new ObservableCollection<Person>(
                RepositoryFactory.GetRepository().GetPeopleNotInSubject(subject.IDSubject)
                );

            People.CollectionChanged += People_CollectionChanged;
            this.subject = subject;
        }

        private void People_CollectionChanged(object? sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    RepositoryFactory.GetRepository().AddPersonToSubject(
                        People[e.NewStartingIndex], subject.IDSubject);
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    RepositoryFactory.GetRepository().DeletePersonFromSubject(
                        e.OldItems!.OfType<Person>().ToList()[0], subject.IDSubject);
                    break;
            }
        }
        public void UpdatePerson(Person person) => People[People.IndexOf(person)] = person;
    }
}
