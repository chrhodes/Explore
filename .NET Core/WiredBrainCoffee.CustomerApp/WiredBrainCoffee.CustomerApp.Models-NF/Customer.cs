using System.ComponentModel.DataAnnotations;

namespace WiredBrainCoffee.CustomerApp.Models
{
  public class Customer
  {
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string FirstName { get; set; }

    [StringLength(50)]
    public string LastName { get; set; }

    [StringLength(9)]
    public string FavoriteColor { get; set; }
  }
}
