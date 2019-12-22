using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary1.ViewModels
{
    [MetadataType(typeof(ProductViewModelMeta))]
    public partial class ProductViewModel { }

    public class ProductViewModelMeta
    {
        [Required(ErrorMessage = "Numele produsului nu poate fi lăsat necompletat")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Numele produsului trebuie să aibă între 3 și 50 de caractere")]
        [RegularExpression(@"^[a-zA-Z0-9'-'\s]*$", ErrorMessage = " Introduceți doar litere și cifre în numele produsului")]
        public string Title;

        [Required(ErrorMessage = "Descrierea produsului nu poate fi lăsată necompletată")]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Descrierea produsului trebuie să conțină minimum 10 și maximum 200 de caractere")]
        [RegularExpression(@"^[,;a-zA-Z0-9'-'\s]*$", ErrorMessage = "Introduceți doar litere și cifre în descrierea produsului")]
        [DataType(DataType.MultilineText)]
        public string Description;

        [Required(ErrorMessage = "Prețul nu poate fi lăsat necompletat")]
        [Range(1.00, 10000, ErrorMessage = "Vă rugăm să introduceți o valoare între 1.00 și 10000.00 de lei")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        [RegularExpression("[0-9]+(\\.[0-9][0-9]?)?", ErrorMessage = "Prețul trebuie să fie un număr urmat de 2 zecimale")]
        public decimal Price;

        [Display(Name = "Categoria")]
        public int CategoryID;
    }

}