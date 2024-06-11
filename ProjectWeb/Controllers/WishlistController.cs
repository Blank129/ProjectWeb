using PagedList;
using ProjectWeb.Models;
using ProjectWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ProjectWeb.Controllers
{
    public class WishlistController : Controller
    {
        
        // GET: Wishlist
        [Authorize]
        public ActionResult Index(int? page)
        {
            var pageSize = 10;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<Wishlist> items = db.Wishlist.Where(x => x.UserName == User.Identity.Name).OrderByDescending(x=>x.CreateDate);
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostWishList(int ProductId)
        {
            if (!Request.IsAuthenticated)
            {
                return Json(new { Success = false, Message = "Bạn chưa đăng nhập!" });
            }
            var checkItem = db.Wishlist.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);
            if (checkItem != null)
            {
                return Json(new { Success = false, Message = "Sản phẩm đã được yêu thích rồi!" });
            }
            var item = new Wishlist();
            item.ProductId = ProductId;
            item.UserName = User.Identity.Name;
            item.CreateDate = DateTime.Now;
            db.Wishlist.Add(item);
            db.SaveChanges();
            return Json(new {Success = true});
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostDelete(int ProductId)
        {
            var checkItem = db.Wishlist.FirstOrDefault(x => x.ProductId == ProductId && x.UserName == User.Identity.Name);
            if (checkItem != null)
            {
                db.Wishlist.Remove(checkItem);
                db.SaveChanges();
                return Json(new { Success = true, Message = "Xóa thành công" });
            }
            return Json(new { Success = false, Message = "Xóa thất bại" });
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}