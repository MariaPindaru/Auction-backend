// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
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
            log4net.Config.XmlConfigurator.Configure();

            Injector.Inject();
            var kernel = Injector.Kernel;
            var categoryService = kernel.Get<ICategoryService>();
            var productService = kernel.Get<IProductService>();
            var a = kernel.Get<IAuctionService>();
            var u = kernel.Get<IUserService>();
            var us = kernel.Get<IUserScoreService>();
            var category = new Category { Name = "CCC" };
            var o = new User { Name = "AAA", Role = Role.Offerer };



            //var t = categoryService.Insert(category);
            //t = u.Insert(o);
            category = categoryService.GetByID(1);
            var user = u.GetByID(1);

            var prod = new Product
            {
                Name = "haha",
                Description = "mare haha",
                Offerer = user,
                Category = category,
            };
            //var aa = productService.Insert(prod);
            var p = productService.GetAll().ToList()[0];

            var auction = new Auction
            {
                Id = 1,
                Product = p,
                StartPrice = 10.6m,
                StartTime = DateTime.Now.AddDays(10),
                EndTime = DateTime.Now.AddDays(20),
                Currency = Currency.Euro,
                IsFinished = false,
            };
            var r = a.Insert(auction);

            r = a.Insert(auction);

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
