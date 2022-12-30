// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

using AuctionBackend.DomainLayer.BL.Interfaces;
using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.Startup;
using Ninject;
using System;
using System.Collections.Generic;

namespace AuctionBackend
{
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

            InsertCategory(categoryService);

            Console.ReadKey(true);
        }

        private static void InsertCategory(ICategoryService categoryService)
        {
            Category category = new Category
            {
                Name = "Category name"
            };
            
            Product product = new Product
            {
                Name = "Product name",
                Description = "Product description",
                Categories = new HashSet<Category> { category }
            };

            var validationResult = categoryService.Insert(category);

            Console.WriteLine(validationResult.IsValid);
        }
    }
}
