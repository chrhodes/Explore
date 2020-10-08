using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyStore.Models
{
    [MetadataType(typeof(ProductImage_MetaData))]
    public partial class ProductImage
    {
    }
    public class ProductImage_MetaData
    {
        [Display(Name = "File")]
        [StringLength(100)]
        //[Index(IsUnique = true)]
        public string FileName;
    }
}