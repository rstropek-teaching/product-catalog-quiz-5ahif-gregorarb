using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository repository;

        public ProductsController(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Shows a list of all Products
        /// </summary>
        /// <returns>a view with a list of all products</returns>
        public IActionResult Index()
        {
            return View(repository.GetAll());
        }

        /// <summary>
        /// Shows a specific product in detail
        /// </summary>
        /// <param name="id">id of the product to be viewed in detail</param>
        /// <returns>details of the selected product</returns>
        public IActionResult Details(int id)
        {
            var product = repository.GetById(id);
            return View(product);
        }

        /// <summary>
        /// This method returns a view, where the user can then delete the contact
        /// Basically, it does the same as the Details
        /// </summary>
        /// <param name="id">id of the product to be deleted</param>
        /// <returns>A View with the product to be deleted</returns>
        public IActionResult Delete(int id)
        {
            var productToDelete = repository.GetById(id);
            if (productToDelete == null)
            {
                return NotFound();
            }
            return View(productToDelete);
        }

        /// <summary>
        /// Deletes the product from the List
        /// </summary>
        /// <param name="id">id of the product to be deleted</param>
        /// <returns>redirects back to the products-list</returns>
        public IActionResult DeleteConfirmed(int id)
        {
            repository.DeleteById(id);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Adds a new product to the list
        /// </summary>
        /// <param name="product">Product to be added</param>
        /// <returns>A view with the new product</returns>
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product != null && !string.IsNullOrEmpty(product.Name))
                {
                    repository.AddProduct(product);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        
        public IActionResult Search(string name)
        {
            return View(repository.SearchProduct(name));
        }
    }
}
