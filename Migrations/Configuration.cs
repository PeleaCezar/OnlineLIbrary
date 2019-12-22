namespace OnlineLibrary1.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    

    internal sealed class Configuration : DbMigrationsConfiguration<OnlineLibrary1.Models.ApplicationDbContext>
    {
        /*public Configuration()
         {
             AutomaticMigrationsEnabled = false;
         } */

        protected override void Seed(OnlineLibrary1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var categories = new List<Category>
            {
                new Category {Name ="Programare",Description = "Carti de programarea a calculatoarelor si IT"},
                new Category { Name = "Beletristica", Description = "Carti de literatura romana si universala "},
                new Category {Name = "Statistica", Description = "Carti de statistica si analiza datelor"},
                new Category {Name = "Psihologie",Description ="Psihologie,consiliere si dezvoltare personala"}
             };
            categories.ForEach(cats => context.Categories.AddOrUpdate(it => it.Name, cats));
            context.SaveChanges();

            var products = new List<Product>
           {
                new Product {Isbn = "0000000000000", Author = "Georgescu Mihai",Title = "Programare in  limbajul PHP", Description ="", Price = 140.33M,CategoryID =categories.Single(cat => cat.Name == "Programare").ID },
                new Product {Isbn = "0000000000001", Author = "Ionut Popescu",Title = "Dezvoltarea aplicatiilor pentru echipamente mobile", Description ="", Price = 40.38M,CategoryID =categories.Single(cat => cat.Name == "Programare").ID },
                new Product {Isbn = "0000000000002", Author = "Dan Ionescu",Title = "Tehnici de securizare aplicatiilor web", Description ="", Price = 120.10M,CategoryID =categories.Single(cat => cat.Name == "Programare").ID },
                new Product {Isbn = "0000000000003", Author = "Andrei Sebastian",Title = "Oameni si roboti ", Description ="", Price = 37.78M,CategoryID =categories.Single(cat => cat.Name == "Beletristica").ID },
                new Product {Isbn = "0000000000004", Author = "Anca Dan",Title = "Arta manipularii", Description ="", Price = 127.58M,CategoryID =categories.Single(cat => cat.Name == "Beletristica").ID },
                new Product {Isbn = "0000000000005", Author = "Ion Vasile",Title = "Declinul Civilizatie", Description ="", Price = 240.50M,CategoryID =categories.Single(cat => cat.Name == "Beletristica").ID },
                new Product {Isbn = "0000000000006", Author = "Alexandru Popa",Title = "Testarea modelelor SEM", Description ="", Price = 147.20M,CategoryID =categories.Single(cat => cat.Name == "Statistica").ID },
                new Product {Isbn = "0000000000007", Author = "Andra Grigore",Title = "Modelarea ierarhica a fenomenelor sociale", Description ="", Price = 130.00M,CategoryID =categories.Single(cat => cat.Name == "Statistica").ID },
                new Product {Isbn = "0000000000008", Author = "Marian Andronie",Title = "Distributii folosite in interfata statistica", Description ="", Price = 163.20M,CategoryID =categories.Single(cat => cat.Name == "Statistica").ID },
                new Product {Isbn = "0000000000009", Author = "Ion Vasile",Title = "Comportamentul in societatea moderna", Description ="", Price = 37.30M,CategoryID =categories.Single(cat => cat.Name == "Psihologie").ID },
                new Product {Isbn = "0000000000010", Author = "Ion Daniel",Title = "Gandire rapida gandire lenta", Description ="", Price = 79.10M,CategoryID =categories.Single(cat => cat.Name == "Psihologie").ID },
                new Product {Isbn = "0000000000011", Author = "Andrei Tona",Title = "Adevarul despre necinste", Description ="", Price = 88.90M,CategoryID =categories.Single(cat => cat.Name == "Psihologie").ID },
                new Product {Isbn = "0000000000012", Author = "Andrei Ion",Title = "Adevarul despre psihologie", Description ="", Price = 83.90M,CategoryID =categories.Single(cat => cat.Name == "Psihologie").ID },
            };
            products.ForEach(prods => context.Products.AddOrUpdate(pr => pr.Title, prods));
            context.SaveChanges();
        }
    }
}
