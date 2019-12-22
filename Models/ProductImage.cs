 using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace OnlineLibrary1.Models
{
    public partial class ProductImage
    {
        public int ID { get; set; } // codul unic al cartii (cheie externa)
        [Index(IsUnique = true)]
        public string FileName { get; set; } //calea catre fisierul imagine a cartii

        public virtual ICollection<ProductImageMap> ProductImageMap { get; set; } //Asocierea de imagini a produsului
    }
}
