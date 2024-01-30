using PersonManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManager.Dal
{
    public interface IRepository
    {
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        void DeletePerson(Person person);
        Person GetPerson(int idPerson);
        IList<Person> GetPeople();

        void AddSubject(Subject subject);
        void UpdateSubject(Subject subject);
        void DeleteSubject(Subject subject);
        Subject GetSubject(int idSubject);
        IList<Subject> GetSubjects();

        IList<Person> GetPeopleForSubject(int subjectId);
        IList<Person> GetPeopleNotInSubject(int subjectId);
        void AddPersonToSubject(Person person, int subjectId);
        void DeletePersonFromSubject(Person person, int subjectId);
    }
}
