// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using Ninject;

    /// <summary>
    /// Main program.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        private static void Main(string[] args)
        {
            Injector.Inject();
            var kernel = Injector.Kernel;
            var categoryService = kernel.Get<ICategoryService>();
            var productService = kernel.Get<IProductService>();
            var a = kernel.Get<IAuctionService>();
            var u = kernel.Get<IUserService>();

            var category = new Category { Name = "CCC" };
            var o = new User { Name = "AAA", Role = Role.Offerer };

            //var t = categoryService.Insert(category);
            var t = u.Insert(o);
            category = categoryService.GetByID(1);
            var user = u.GetByID(1);
            var p = productService.GetByID(2);

            var auction = new Auction
            {
                Id = 1,
                Product = new Product
                {
                    Name = "haha",
                    Description = "mare haha",
                    Offerer = o,
                    Category = category,
                },
                StartPrice = 10.6m,
                StartTime = DateTime.Now.AddDays(10),
                EndTime = DateTime.Now.AddDays(20),
                Currency = Currency.Euro,
                IsFinished = false
            };
            var r = a.Insert(auction);

            //var product 

            //InsertCategory(categoryService, productService);
            Console.ReadKey(true);
        }

        private static void InsertCategory(ICategoryService categoryService, IProductService productService)
        {
            var category = categoryService.GetByID(1);

            var categoryP = new Category
            {
                Name = "nbbbb",
            };
            categoryP.Children.Add(category);

            var r = categoryService.Insert(categoryP);
            Console.WriteLine(r.IsValid);
        }
    }
}
