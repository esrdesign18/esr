using Aleph1.Logging;
using esr.DAL.Contracts;
using esr.Models;
using System.Collections.Generic;
using System.Linq;

namespace esr.DAL.Implementation
{
    internal class DAL : IDAL
    {
        private List<Person> persons = new List<Person>();

        [Logged(LogParameters = false)]
        public DAL()
        {
            persons.Add(new Person() { ID = 1, FirstName = "John", LastName = "Doe" });
            persons.Add(new Person() { ID = 2, FirstName = "Jane", LastName = "Doe" });
        }

        [Logged]
        public IQueryable<Person> GetPersons()
        {
            return persons.AsQueryable();
        }

        [Logged]
        public Person GetPersonByID(int ID)
        {
            return persons.FirstOrDefault(p => p.ID == ID);
        }

        [Logged]
        public Person InsertPerson(Person person)
        {
            persons.Add(person);
            return person;
        }
    }
}
