using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary1.Models
{
    [MetadataType(typeof(ProductMetadata))]
    public partial class Product { }

    public class ProductMetadata
    {
        [Required(ErrorMessage ="ISBN-ul Cartii nu poate fi lasat necompletat")]
        [StringLength(13,MinimumLength =10,ErrorMessage = "ISBN-ul trebuie sa contina minimum 10 si maximum 13 caractere")]
        [RegularExpression(@"^[0-9''-'\s]*$",ErrorMessage ="ISBN-ul trebuie sa contina doar cifre si spatii")]
        [Display(Name = "Book's ISBN")]
        public string Isbn;
        [Required(ErrorMessage ="Numerele autorului nu poate fi lasat necompletat")]
        [StringLength(50,MinimumLength = 3, ErrorMessage ="Numele autorului trebuie sa contina minimum")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$",ErrorMessage ="Numele autorului trebuie sa  inceapa cu o litera mare si sa contina doar litere si spatii ")]
        [Display(Name = "Book's Author")]
        public string Author;
        [Required(ErrorMessage ="Titlul cartii nu poate fi lasat necompletat")]
        [StringLength(200,MinimumLength =3, ErrorMessage ="Titlul cartii trebuie sa contina minimum 3 si maximum 200 de caractere")]
        [RegularExpression(@"^[,;a-zA-Z0-9''-'\s]*$", ErrorMessage = "Titlul cartii trebuie sa contina doar litere,cifree si spatii")]
        [Display(Name = "Book's Title")]
        public string Title;
        [DataType(DataType.MultilineText)]
        [Display(Name = "Book's Description")]
        public string Description;
        [Required(ErrorMessage ="Pretul cartii nu poate fi lasat necompletat")]
        [Range(0.10,10000,ErrorMessage ="Introduceti pretul cartii intre 0,10 si 10.0000")]
        [DataType(DataType.Currency)] [DisplayFormat(DataFormatString ="{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?",ErrorMessage ="Pretul trebuie sa fie un numar cu 2 zecimale")]
        [Display(Name = "Book's Price")]
        public decimal Price;
    }
}