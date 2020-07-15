﻿using AhmedTrading.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Threading.Tasks;

namespace AhmedTrading.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _db;

        public ProductController(IUnitOfWork db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductViewModel model)
        {
            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label");

            if (!ModelState.IsValid) return View(model);

            var isExist = await _db.Products.IsExistAsync(model.ProductName).ConfigureAwait(false);

            if (!isExist)
            {
                _db.Products.AddCustom(model);
                await _db.SaveChangesAsync();
                return RedirectToAction("AddProduct");
            }

            ModelState.AddModelError("ProductName", "Product Name Already Exist");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest(HttpStatusCode.BadRequest);

            var model = await _db.Products.FindByIdAsync(id.GetValueOrDefault()).ConfigureAwait(false);

            if (model == null) return NotFound();

            ViewBag.ProductBrand = new SelectList(_db.ProductBrands.ddl(), "value", "label", model.ProductBrandId);

            return PartialView("_Edit", model);
        }

        //POST
        [HttpPost]
        public async Task<IActionResult> Edit(ProductUpdateModel model)
        {
            var exist = await _db.Products.IsExistAsync(model.ProductName, model.ProductId).ConfigureAwait(false);
            if (exist) ModelState.AddModelError("ProductName", "Product Name already exist!");

            if (!ModelState.IsValid) return PartialView("_Edit", model);

            _db.Products.CustomUpdate(model);

            var task = await _db.SaveChangesAsync();
            if (task != 0) return Json(model);

            ModelState.AddModelError("", "Unable to update");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return PartialView("_Edit", model);
        }

        public int DeleteProduct(int id)
        {
            if (!_db.Products.RemoveCustom(id)) return -1;
            return _db.SaveChanges();
        }

        public async Task<IActionResult> GetProductByBrand(int brandId = 0)
        {
            var productList = await _db.Products.FindByBrandAsync(brandId);
            return Json(productList);
        }

        public async Task<IActionResult> FindProductsByName(string name)
        {
            var data = await _db.Products.FindByNameAsync(name).ConfigureAwait(false);
            return Json(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}