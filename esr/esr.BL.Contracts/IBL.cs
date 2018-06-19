using esr.Models;
using System.Linq;

namespace esr.BL.Contracts
{
    public interface IBL
    {
        IQueryable<Person> GetPersons();
        Person GetPersonByID(int ID);
        Person GetPersonByName(string firstName);
        Person InsertPerson(Person person);
    }
}
