using PagedList;
using ProjectWeb.Models;
using ProjectWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page, string searchText)
        {
            IEnumerable<Order> items = db.Orders.OrderByDescending(x=>x.CreatedDate);
            if (!string.IsNullOrEmpty(searchText))
            {
                items = items.Where(x => x.CustomerName.Contains(searchText) || x.Code.Contains(searchText)).ToList();
            }
            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Detail(int id)
        {
            var item = db.Orders.Find(id);           
            return View(item);
        }
        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }
        [HttpPost]
        public ActionResult UpdateTT(int id, int status)
        {
            var items = db.Orders.Find(id);
            if(items != null)
            {
                db.Orders.Attach(items);
                items.TypePayment = status;
                db.Entry(items).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Fail", Success = false });
        }
    }
}