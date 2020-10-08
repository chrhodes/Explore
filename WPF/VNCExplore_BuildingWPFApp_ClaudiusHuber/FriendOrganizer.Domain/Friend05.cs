﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendOrganizer.Domain
{
    //[Table("Friend")]
    public class Friend05 : IFriend
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        // Added in Module12

        public int? FavoriteLanguageId { get; set; }

        public ProgrammingLanguage12 FavoriteLanguage { get; set; }
    }
}
