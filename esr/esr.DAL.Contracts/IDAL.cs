using esr.Models;
using System.Linq;

namespace esr.DAL.Contracts
{
    public interface IDAL
    {
        IQueryable<Person> GetPersons();
        Person GetPersonByID(int ID);
        Person InsertPerson(Person person);
    }
}
