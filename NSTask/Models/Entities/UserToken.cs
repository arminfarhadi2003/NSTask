using System.ComponentModel.DataAnnotations;

namespace NSTask.Models.Entities
{
    public class UserToken
    {
        [Key]
        public int Id { get; set; }
        public string TokenHash { get; set; }
        public DateTime TokenExp { get; set; }
        public Users User { get; set; }
    }
}
