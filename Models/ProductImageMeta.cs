using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary1.Models
{
    [MetadataType(typeof(ProductImageMeta))]
    public partial class ProductImage { }
    public class ProductImageMeta
    {
        [Display(Name = "Fisiere imagine")] [StringLength(100)]
        public string FileName;
    }
}