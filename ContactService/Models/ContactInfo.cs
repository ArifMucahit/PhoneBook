using ContactService.Models.Enums;

namespace ContactService.Models
{
    public class ContactInfo
    {
        public EnumContactType InfoType { get; set; }
        public string InfoDetail { get; set; }
    }
}