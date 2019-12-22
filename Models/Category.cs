using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary1.Models
{
    public partial class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }      
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}