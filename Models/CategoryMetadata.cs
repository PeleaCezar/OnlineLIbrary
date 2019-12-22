using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary1.Models
{
    [MetadataType(typeof(CategoryMetadata))]
    public partial class Category { }

    public class CategoryMetadata
    {
        [Required(ErrorMessage = "Numele categoriei nu poate fi lasat necompletat")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Numele categoriei trebuie sa contina minimum 3 si maximum 20 de caractere")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$",ErrorMessage ="Numele categoriei trebie sa inceapa cu o litera mare si sa contina doar litere si spatii")]
        [Display(Name = "Category name")]
        public string Name;
        [Display(Name = "Category description")]
        public string Description;
    }
}