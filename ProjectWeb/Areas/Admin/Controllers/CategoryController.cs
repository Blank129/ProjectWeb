using Microsoft.Ajax.Utilities;
using ProjectWeb.Models;
using ProjectWeb.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var items = db.Categories;
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            //ModelState.IsValid là để check lỗi và trả về lỗi nếu có
            if (ModelState.IsValid)
            {
                // phải ghi 2 cái CreaetedDate và ModifiedDate vì nếu chỉ có 1 cái sẽ lỗi
                //Vì bên model ta khai báo 2 cái biến DateTime
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = ProjectWeb.Models.Common.Filter.FilterChar(model.Title);
                db.Categories.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
            
        }

        public ActionResult Edit(int id)
        {
            var item = db.Categories.Find(id);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            //ModelState.IsValid là để check có hợp lệ trong thag model của mình hay ko
            if (ModelState.IsValid)
            {
                db.Categories.Attach(model);
                model.ModifiedDate = DateTime.Now;
                model.Alias = ProjectWeb.Models.Common.Filter.FilterChar(model.Title);
                db.Entry(model).Property(x => x.Title).IsModified = true;
                db.Entry(model).Property(x => x.Description).IsModified = true;
                db.Entry(model).Property(x => x.Link).IsModified = true;
                db.Entry(model).Property(x => x.Alias).IsModified = true;
                db.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(model).Property(x => x.Position).IsModified = true;
                db.Entry(model).Property(x => x.ModifiedDate).IsModified = true;
                db.Entry(model).Property(x => x.Modifiedby).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);

        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Categories.Find(id);
            if(item != null)
            {
                //var DeleteItem = db.Categories.Attach(item);
                db.Categories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = true });
        }

        
    }
}