using System.ComponentModel.DataAnnotations;

namespace NSTask.Models.Entities
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
