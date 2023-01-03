// <copyright file="Program.cs" company="Transilvania University of Brasov">
// Maria Pindaru
// </copyright>

using AuctionBackend.DomainLayer.DomainModel;
using AuctionBackend.DomainLayer.ServiceLayer.Interfaces;
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

            //InsertCategory(categoryService);

            Console.ReadKey(true);
        }

        private static void InsertCategory(ICategoryService categoryService)
        {




            Category category = new Category
            {
                Name = "ChildCategory1",
            };

            Category category2 = new Category
            {
                Name = "ChildCategory2",
            };

            Category parentCategory = new Category
            {
                Name = "ParentCategory2",
                Children = new HashSet<Category> { category, category2 },
            };

            var r = categoryService.Insert(parentCategory);
            Console.WriteLine(r.IsValid);
            //Product product = new Product
            //{
            //    Name = "Product name",
            //    Description = "Product description",
            //};


            //var validationResult = categoryService.Insert(parentCategory);
            //validationResult = categoryService.Insert(category);

            //var l = categoryService.GetCategoriesWithChildren();
            ////parentCategory = categoryService.GetByID(16);
            //var category = categoryService.GetByID(17);

            //foreach (var e in l)
            //{
            //    e.Children.Add(category);
            //    var validationResult = categoryService.Update(e);
            //    validationResult = categoryService.Update(category);
            //    Console.WriteLine(validationResult.IsValid);
            //}

            //parentCategory.Children.Add(category);



        }
    }
}
