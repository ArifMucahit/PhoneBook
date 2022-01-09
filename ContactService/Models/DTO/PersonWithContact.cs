namespace ContactService.Models.DTO
{
    public class PersonWithContact
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public ContactDTO ContactInfo { get; set; }
    }
}
