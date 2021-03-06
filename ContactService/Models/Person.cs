using System.Collections.Generic;

namespace ContactService.Models
{
    public class Person
    {
        public string UID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }


        public virtual List<ContactInfo> ContactInfos { get; set; }
    }
}
