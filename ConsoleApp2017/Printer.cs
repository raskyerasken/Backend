using System;
using System.Collections.Generic;
using CustomerApp.Core.ApplicationService;
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using CustomerApp.Infrastructure.Static.Data.Repositories;

namespace ConsoleApp2017
{
    #region Comments

    /* -- UI -- 
        Console.WriteLine
        Console.Readline
        dkd
    */
    //-- Infrastructue --
    // EF - Static List - Text File

    // --- Test --
    // Unit test for Core

    /*--- CORE -- 
        Customer - Entity - Core.Entity
        Domain Service - Repository / UOW - Core
        Application Service - Service - Core
    */
    #endregion

    public class Printer: IPrinter
    {
        #region Service area

        readonly IProductService _productService;
        #endregion

        public Printer(IProductService productService)
        {
            _productService = productService;
            //Move to Infrastructure Layer later 
            InitData();

        }

        #region UI

        public void StartUI()
        {
            string[] menuItems = {
                "List All Products",
                "Add Product",
                "Delete Product",
                "Edit Product",
                "Exit"
            };

            var selection = ShowMenu(menuItems);

            while (selection != 5)
            {
                switch (selection)
                {
                    case 1:
                        var products = _productService.GetAllProducts();
                        ListProduct(products);
                        break;
                    case 2:
                        var productType = AskQuestion("Product Type: ");
                        var productSize = AskQuestion("Product Size: ");
                        var productPrice = AskQuestion("Product Price: ");
                        var product = _productService.NewProduct(productType, productSize, productPrice);
                        _productService.CreateProduct(product);
                        break;
                    case 3:
                        var idForDelete = PrintFindProductId();
                        _productService.DeleteProduct(idForDelete);
                        break;
                    case 4:
                        var idForEdit = PrintFindProductId();
                        var productToEdit = _productService.FindProductById(idForEdit);
                        Console.WriteLine("Updating " + productToEdit.ProductType + " " + productToEdit.ProductSize);
                        var newProductType = AskQuestion("Product Type: ");
                        var newProductSize = AskQuestion("Product Size: ");
                        var newProductPrice = AskQuestion("Product Price: ");
                        _productService.UpdateProduct(new Products()
                        {
                            Id = idForEdit,
                            ProductType = newProductType,
                            ProductSize = newProductSize,
                            ProductPrice = newProductPrice
                        });
                        break;
                    default:
                        break;
                }
                selection = ShowMenu(menuItems);
            }
            Console.WriteLine("Bye bye!");

            Console.ReadLine();
        }



        int PrintFindProductId()
        {
            Console.WriteLine("Insert Product Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Please insert a number");
            }
            return id;
        }

        string AskQuestion(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }

        void ListProduct(List<Products> products)
        {
            Console.WriteLine("\nList of Products");
            foreach (var product in products)
            {
                Console.WriteLine($"Id: {product.Id} Product Type: {product.ProductType} " +
                                $"{product.ProductSize} " +
                                $"Product Price: {product.ProductPrice}");
            }
            Console.WriteLine("\n");

        }

        /// <summary>
        /// Shows the menu.
        /// </summary>
        /// <returns>Menu Choice as int</returns>
        /// <param name="menuItems">Menu items.</param>
        int ShowMenu(string[] menuItems)
        {
            Console.WriteLine("Select What you want to do:\n");

            for (int i = 0; i < menuItems.Length; i++)
            {
                //Console.WriteLine((i + 1) + ":" + menuItems[i]);
                Console.WriteLine($"{(i + 1)}: {menuItems[i]}");
            }

            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                || selection < 1
                || selection > 5)
            {
                Console.WriteLine("Please select a number between 1-5");
            }

            return selection;
        }
        #endregion

        #region Infrastructure layer / Initialization Layer

        void InitData()
        {
            var product1 = new Products()
            {
                ProductType = "Bob",
                ProductSize = "Dylan",
                ProductPrice = "BongoStreet 202"
            };
            _productService.CreateProduct(product1);

            var product2 = new Products()
            {
                ProductType = "Lars",
                ProductSize = "Bilde",
                ProductPrice = "Ostestrasse 202"
            };
            _productService.CreateProduct(product2);
        }

        #endregion

    }
}
