using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendOrganizer.Domain
{
    //[Table("Friend")]
    public class Friend13 : IFriend
    {
        public Friend13()
        {
            PhoneNumbers = new Collection<FriendPhoneNumber13>();
        }
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        public int? FavoriteLanguageId { get; set; }

        public ProgrammingLanguage12 FavoriteLanguage { get; set; }

        public ICollection<FriendPhoneNumber13> PhoneNumbers { get; set; }
    }
}
