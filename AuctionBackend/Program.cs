// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

namespace AuctionBackend
{
    using System;
    using AuctionBackend.DomainLayer.DomainModel;
    using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
    using AuctionBackend.Startup;
    using Ninject;

    /// <summary>
    /// Main program.
    /// </summary>
    internal class Program
    {
        private static void Main(string[] args)
        {
            Injector.Inject();
            var kernel = Injector.Kernel;
            var categoryService = kernel.Get<ICategoryService>();
            var productService = kernel.Get<IProductService>();

            InsertCategory(categoryService, productService);
            Console.ReadKey(true);
        }

        private static void InsertCategory(ICategoryService categoryService, IProductService productService)
        {
            var category = categoryService.GetByID(1);

            var categoryP = new Category
            {
                Name = "cp",
            };
            categoryP.Children.Add(category);

            var r = categoryService.Insert(categoryP);
            Console.WriteLine(r.IsValid);
        }
    }
}
