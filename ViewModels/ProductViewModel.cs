
using System.Collections.Generic;
using System.Web.Mvc;

namespace OnlineLibrary1.ViewModels
{
    public partial class ProductViewModel
    {
        public int ID { get; set; }
        public string Isbn { get; set; }
        public string Author{ get; set;}
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public SelectList CategoryList { get; set; } // caseta de selectie pentru categoria produsului
        public List<SelectList> ImageList { get; set; } //Lista de selectie pentru imaginile produsului
        public string [] ProductImages { get; set; }  // vectorul imaginilor produsului

    }
}