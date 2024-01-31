using PersonManager.Models;
using PersonManager.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PersonManager.Dal
{
    internal class SqlRepository : IRepository
    {
        private static readonly string cs = MainWindow.connStr;

        //
        //
        //
        //PEOPLE
        //
        //
        //
        public void AddPerson(Person person)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Person.FirstName), person.FirstName);
            cmd.Parameters.AddWithValue(nameof(Person.LastName), person.LastName);
            cmd.Parameters.AddWithValue(nameof(Person.Age), person.Age);
            cmd.Parameters.AddWithValue(nameof(Person.Email), person.Email);
            cmd.Parameters.Add(
                new SqlParameter(nameof(Person.Picture), System.Data.SqlDbType.Binary, person.Picture!.Length)
                {
                    Value = person.Picture
                });
            var id = new SqlParameter(nameof(Person.IDPerson), System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(id);
            cmd.ExecuteNonQuery();
            person.IDPerson = (int)id.Value;
        }

        public void DeletePerson(Person person)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
           
            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
            cmd.ExecuteNonQuery();

        }

        public IList<Person> GetPeople()
        {
            IList<Person> list = new List<Person>();

            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(ReadPerson(dr));
            }

            return list;
        }

        private Person ReadPerson(SqlDataReader dr) => new Person
        {
            IDPerson = (int)dr[nameof(Person.IDPerson)],   
            FirstName = dr[nameof(Person.FirstName)].ToString(),   
            LastName = dr[nameof(Person.LastName)].ToString(),   
            Age = (int)dr[nameof(Person.Age)],
            Email = dr[nameof(Person.Email)].ToString(),
            Picture = ImageUtils.ByteArrayFromReader(dr, nameof(Person.Picture))
        };

        public Person GetPerson(int idPerson)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), idPerson);
            
            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return ReadPerson(dr);
            }
            throw new ArgumentException("Wrong id, no such person");

        }

        public void UpdatePerson(Person person)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Person.FirstName), person.FirstName);
            cmd.Parameters.AddWithValue(nameof(Person.LastName), person.LastName);
            cmd.Parameters.AddWithValue(nameof(Person.Age), person.Age);
            cmd.Parameters.AddWithValue(nameof(Person.Email), person.Email);
            cmd.Parameters.Add(
                new SqlParameter(nameof(Person.Picture), System.Data.SqlDbType.Binary, person.Picture!.Length)
                {
                    Value = person.Picture
                });

            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
            cmd.ExecuteNonQuery();

        }

        //
        //
        //
        //SUBJECTS
        //
        //
        //

        public IList<Subject> GetSubjects()
        {
            IList<Subject> list = new List<Subject>();

            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(ReadSubject(dr));
            }

            return list;
        }

        private Subject ReadSubject(SqlDataReader dr) => new Subject
        {
            IDSubject = (int)dr[nameof(Subject.IDSubject)],
            SubjectName = dr[nameof(Subject.SubjectName)].ToString(),
            ECTS = (int)dr[nameof(Subject.ECTS)]
        };

        public void AddSubject(Subject subject)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Subject.SubjectName), subject.SubjectName);
            cmd.Parameters.AddWithValue(nameof(Subject.ECTS), subject.ECTS);
            var id = new SqlParameter(nameof(Subject.IDSubject), System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };
            cmd.Parameters.Add(id);
            cmd.ExecuteNonQuery();
            subject.IDSubject = (int)id.Value;
        }

        public void UpdateSubject(Subject subject)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Subject.SubjectName), subject.SubjectName);
            cmd.Parameters.AddWithValue(nameof(Subject.ECTS), subject.ECTS);
            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subject.IDSubject);
            cmd.ExecuteNonQuery();
        }

        public void DeleteSubject(Subject subject)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subject.IDSubject);
            cmd.ExecuteNonQuery();
        }

        public Subject GetSubject(int idSubject)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), idSubject);

            using SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                return ReadSubject(dr);
            }
            throw new ArgumentException("Wrong id, no such subject");
        }


        //
        //
        //
        //PERSONSUBJECTS
        //
        //
        //

        public IList<Person> GetPeopleForSubject(int subjectId)
        {
            IList<Person> list = new List<Person>();

            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subjectId);
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(ReadPerson(dr));
            }

            return list;
        }

        public IList<Person> GetPeopleNotInSubject(int subjectId)
        {
            IList<Person> list = new List<Person>();

            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subjectId);
            using SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                list.Add(ReadPerson(dr));
            }

            return list;
        }

        public void AddPersonToSubject(Person person, int subjectId)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subjectId);
            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
            cmd.ExecuteNonQuery();
        }

        public void DeletePersonFromSubject(Person person, int subjectId)
        {
            using SqlConnection con = new(cs);
            con.Open();
            using SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = MethodBase.GetCurrentMethod()?.Name;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue(nameof(Subject.IDSubject), subjectId);
            cmd.Parameters.AddWithValue(nameof(Person.IDPerson), person.IDPerson);
            cmd.ExecuteNonQuery();
        }
    }
}
