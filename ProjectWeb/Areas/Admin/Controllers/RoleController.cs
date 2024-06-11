using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using ProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ProjectWeb.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Admin/Role
        public ActionResult Index()
        {
            var items = db.Roles.ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(IdentityRole model)
        {
            if(ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var items= db.Roles.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                roleManager.Update(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //[HttpPost]
        //public async Task<ActionResult> Delete(string userId, string roleName)
        //{
        //    var result = await UserManager.RemoveFromRoleAsync(userId, roleName);
        //    if (result.Succeeded)
        //    {
        //        return Json(new { success = true });
        //    }

        //    return Json(new { success = false });
        //}
        [HttpPost]
        public ActionResult Delete(string userId)
        {
            var item = db.Roles.Find(userId);
            if (item != null)
            {
                db.Roles.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}