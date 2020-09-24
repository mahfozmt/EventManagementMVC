using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_MVC_M8_project.Models;

namespace ASP_MVC_M8_project.Controllers
{
    [Authorize(Roles ="admin")]
    public class tblUsersController : Controller
    {
        private EventDbcontext db = new EventDbcontext();

        // GET: tblUsers
        public ActionResult Index()
        {
            var tblUsers = db.tblUsers.Include(t => t.Roles);
            return View(tblUsers.ToList());
        }

       
        public ActionResult Create()
        {
                     
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName");
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tblUserId,UserName,Password,RoleId")] tblUser tblUser)
        {

            var role = db.Roles.FirstOrDefault(x => x.RoleId == tblUser.RoleId);


            if (role.RoleName == "admin")
            {
                ViewBag.msg = "You cant registration as admin, Please chose another option.";
                ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", tblUser.RoleId);
                return View(tblUser);
            }
            else
            {
                if (ModelState.IsValid)
                {

                    db.tblUsers.Add(tblUser);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }

            }


            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", tblUser.RoleId);
            return View(tblUser);
        }

        // GET: tblUsers/Edit/5
        [Authorize(Roles ="admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", tblUser.RoleId);
            return View(tblUser);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblUserId,UserName,Password,RoleId")] tblUser tblUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", tblUser.RoleId);
            return View(tblUser);
        }

        // GET: tblUsers/Delete/5
        [Authorize(Roles = "admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUsers.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: tblUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tblUser = db.tblUsers.Find(id);
            db.tblUsers.Remove(tblUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
