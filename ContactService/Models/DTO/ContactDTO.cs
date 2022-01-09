using ContactService.Models.Enums;

namespace ContactService.Models.DTO
{
    public class ContactDTO
    {
        
        public EnumContactType InfoType { get; set; }
        public string InfoDetail { get; set; }
    }
}
