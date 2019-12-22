

namespace OnlineLibrary1.Models
{
    public class ProductImageMap
    {
        public int ID { get; set; } // Camp unic de identificare a inregistrari-- cheia primara
         public int ProductID { get; set; } // cheia externa a produselor
         public int ImageID { get; set; } // Cheia externa a fisierului de imagine

        public int ImageNumber { get; set; } // numarul asociat imaginii pentru pozitia de afisare

        public virtual Product Product { get; set; } // proprietatea de navigare a produselor
        public virtual ProductImage ProductImage { get; set; } // proprietatea de navigare pentru imaginile produselor
    }
}