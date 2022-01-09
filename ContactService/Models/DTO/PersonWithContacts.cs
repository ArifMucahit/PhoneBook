using System.Collections.Generic;

namespace ContactService.Models.DTO
{
    public class PersonWithContacts
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public List<ContactDTO> ContactInfos { get; set; }
    }
}
