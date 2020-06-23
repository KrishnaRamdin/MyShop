using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> context;
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProducManager
        public ActionResult Index()
        {
            List<ProductCategory> productsCategories = context.Collection().ToList();
            return View(productsCategories);
        }
        public ActionResult Create()
        {
            ProductCategory productsCategories = new ProductCategory();
            return View(productsCategories);
        }
        [HttpPost]
        public ActionResult Create(ProductCategory productsCategories)
        {
            if (!ModelState.IsValid)
            {
                return View(productsCategories);
            }
            else
            {
                context.Insert(productsCategories);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            ProductCategory productsCategories = context.Find(Id);
            if (productsCategories == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productsCategories);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productsCategories, String Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productsCategories);
                }
                productCategoryToEdit.Category = productsCategories.Category;


                context.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(String Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(String Id)
        {
            ProductCategory productCategoryToDelete = context.Find(Id);
            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");

            }
        }
    }
}