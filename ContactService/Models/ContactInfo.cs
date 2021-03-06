using ContactService.Models.Enums;

namespace ContactService.Models
{
    public class ContactInfo
    {
        public int ContactID { get; set; }
        public EnumContactType InfoType { get; set; }
        public string InfoDetail { get; set; }
        public string PersonUID { get; set; }
    }
}