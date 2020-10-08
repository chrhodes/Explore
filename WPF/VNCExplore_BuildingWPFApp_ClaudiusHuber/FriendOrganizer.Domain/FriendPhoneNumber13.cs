using System.ComponentModel.DataAnnotations;

namespace FriendOrganizer.Domain
{
    public class FriendPhoneNumber13
    {
        public int Id { get; set; }

        [Phone]
        [Required]
        public string Number { get; set; }

        public int FriendId { get; set; }

        public Friend13 Friend { get; set; }
    }
}
