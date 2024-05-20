using WorkinOut.Dtos;
using WorkinOut.Models;

namespace WorkinOut.Data.IRepository
{
    public interface IPersonRepository
    {
        Person Create(PersonCreateRequest person);
        void Delete(Guid id);
        void DeleteFirst(Person person);
        IEnumerable<Person> Read();
        Person ReadFromId(Guid id);
        void Update(Person person);
    }
}