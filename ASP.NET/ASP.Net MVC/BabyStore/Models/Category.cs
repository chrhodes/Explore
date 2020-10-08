using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Models
{
    public partial class Category
    {
        public int ID { get; set; }

        //[Required]
        //[StringLength(50, MinimumLength = 3)]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        //[Required(ErrorMessage = "The name cannot be blank")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "Please enter a name between 3 and 50 characters")]
        //[RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$", ErrorMessage = "Please enter a name beginning with a Capital letter and made up of letters and spaces only")]
        //[Display(Name = "Category")]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}