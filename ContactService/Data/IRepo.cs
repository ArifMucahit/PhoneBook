using ContactService.Models;
using ContactService.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactService.Data
{
    public interface IRepo
    {
        Task<bool> CreatePerson(Person person);
        Task<bool> DeletePerson(Person person);
        Task<bool> CreateContact(PersonWithContact contact);
        Task<bool> RemoveContact(PersonWithContact contact);
        Task<List<PersonDTO>> GetPersons();
        Task<List<PersonWithContacts>> GetPersonWithContact();
    }
}
