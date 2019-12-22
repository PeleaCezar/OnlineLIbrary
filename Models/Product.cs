using System.Collections.Generic;

namespace OnlineLibrary1.Models
{
    public partial class Product
    {
        public int ID { get; set; }     //cheia primara
        public string Isbn { get; set; }      
        public string Author { get; set; }     
        public string Title { get; set; }
        public string Description { get; set; }       
        public decimal Price { get; set; }
        public int? CategoryID { get; set; }                                       // codul unic al categoriei cartii in baza de date - cheie externa
        public virtual Category Category { get; set; }                            // Categoria din care face parte cartea- proprietatea de navigare
        public virtual ICollection<ProductImageMap>ProductImageMap { get; set; }  // asocierea de imagini produslui

    }
}