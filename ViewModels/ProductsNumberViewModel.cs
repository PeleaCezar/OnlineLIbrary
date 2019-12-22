using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OnlineLibrary1.Models;
using PagedList;
namespace OnlineLibrary1.ViewModels
{  //Clasa care simuleaza categoriile din lista derulanta
    public class ProductsNumberViewModel
    {
        //Definim View-Modelul
        public IPagedList<Product>Products { get; set; }                            // modelul domeniului pentru entitatea produselor
        public string Search { get; set; }                                         //proprietatea ce stocheaza textul cautat
        public IEnumerable<CategoryWithNumber> CategoryWithNumbers { get; set; }  // proprietatea ceea ce contine numele categoriilor si numarul de produse 
        public string CatName { get; set; }                                      //proprietatea ce contine filtrarea dupa o categorie
        public IEnumerable<SelectListItem> CategoryFilterItems                  // proprietatea  de alegere din lista a elementului de filtrare -read only
        {
            get
            {
                var allCategories = CategoryWithNumbers.Select(cat => new SelectListItem
                {
                    Value = cat.CategoryName,
                    Text = cat.CategoryNameWithNumbers
                });
                return allCategories;
            }
        }
        //Implementarea proprietatilor de sortare
         public string SortBy { get; set; }                     // proprietatea ce contine criteriul de sortare
         public Dictionary<string,string> Sorts { get; set; }  //  proprietatea ce contine lista criteriilor de sortare

    }
    public class CategoryWithNumber
    {
        public int ProductNumbers { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameWithNumbers
        {
            get { return CategoryName + "(" + ProductNumbers.ToString() + ")"; }
        }


    }
}