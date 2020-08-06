﻿using AhmedTrading.Repository;
using JqueryDataTables.LoopsIT;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace AhmedTrading.Web.Controllers
{
    [Authorize(Roles = "admin, purchase")]
    public class PurchaseController : Controller
    {
        private readonly IUnitOfWork _db;

        public PurchaseController(IUnitOfWork db)
        {
            _db = db;
        }

        public IActionResult Purchase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(PurchaseViewModel model)
        {
            model.RegistrationId = _db.Registrations.GetRegID_ByUserName(User.Identity.Name);

            if (!ModelState.IsValid) UnprocessableEntity(ModelState);

            var response = await _db.Purchases.AddCustomAsync(model, _db).ConfigureAwait(false);

            if (!response.IsSuccess) return UnprocessableEntity(response);

            _db.Vendors.UpdatePaidDue(model.VendorId);
            await _db.SaveChangesAsync();

            return Ok(response);
        }


        public async Task<IActionResult> PurchaseReceipt(int? id)
        {
            if (id == null) return RedirectToAction("Purchase");

            var model = await _db.Purchases.PurchaseReceiptAsync(id.GetValueOrDefault(), _db).ConfigureAwait(false);

            if (model == null) return RedirectToAction("Purchase");
            return View(model);
        }

        //purchase Records
        public IActionResult PurchaseRecords()
        {
            return View();
        }

        //request from data-table(ajax)
        public IActionResult PurchaseRecordsData(DataRequest request)
        {
            var data = _db.Purchases.Records(request);
            return Json(data);
        }

        public IActionResult PurchaseReturn(int? id)
        {
            if (id == null) return RedirectToAction("PurchaseRecords");

            var model = _db.Purchases.FindReceipt(id.GetValueOrDefault(), _db);

            return View(model.Data);
        }

        //delete receipt if not payment
        public IActionResult DeleteReceipt(int id)
        {
            var model = _db.Purchases.ReceiptPaymentIsExist(id);
            return Json(model);
        }

        //delete receipt with payment
        public IActionResult ForceDeleteReceipt(int id)
        {
            var model = _db.Purchases.DeleteReceipt(id);
            return Json(model);
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