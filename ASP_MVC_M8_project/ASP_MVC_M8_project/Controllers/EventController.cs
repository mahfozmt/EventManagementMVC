using ASP_MVC_M8_project.Models;
using ASP_MVC_M8_project.Models.ViewModel;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_M8_project.Controllers
{
    public class EventController : Controller
    {

        public EventDbcontext db = new EventDbcontext();
        public ActionResult Index()
        {
            return View(db.Event_Types.ToList());
        }


        // GET: Event/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        public ActionResult Create(VM.Event_Type eVm)
        {
            if (ModelState.IsValid)
            {
                if (eVm.EventTypeImage != null)
                {
                    string newfilename = Guid.NewGuid().ToString() + Path.GetExtension(eVm.EventTypeImage.FileName);
                    string newpath = Path.Combine("Images", "EvType", newfilename);
                    string filetoFolder = Path.Combine(Server.MapPath("~/" + newpath));
                    eVm.EventTypeImage.SaveAs(filetoFolder);
                    Event_Type event_Type = new Event_Type { EventType = eVm.EventType, EventTypeImage = newpath };
                    db.Event_Types.Add(event_Type);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }

            }
            return View(eVm);

        }

        // GET
        [Authorize(Roles = "admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entry = db.Event_Types.FirstOrDefault(x => x.EventTypeId == id);
            string path = entry.EventTypeImage;
            ViewBag.path = path;
            VM.Event_Type event_Type = new VM.Event_Type { EventTypeId = entry.EventTypeId, EventType = entry.EventType, EventTypeImagePath = entry.EventTypeImage };
            return View(event_Type);
        }

        // POST
        [HttpPost]
        public ActionResult Edit(VM.Event_Type evm)
        {
            if (ModelState.IsValid)
            {
                if (evm.EventTypeImage != null)
                {
                    string newfilename = Guid.NewGuid().ToString() + Path.GetExtension(evm.EventTypeImage.FileName);
                    string newpath = Path.Combine("Images", "EvType", newfilename);
                    string filetoFolder = Path.Combine(Server.MapPath("~/" + newpath));
                    evm.EventTypeImage.SaveAs(filetoFolder);

                    string FileName = evm.EventTypeImagePath;
                    string img = Path.Combine(Server.MapPath("~/" + FileName));
                    FileInfo file = new FileInfo(img);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    Event_Type event_Type = new Event_Type { EventTypeId = evm.EventTypeId, EventType = evm.EventType, EventTypeImage = newpath };

                    db.Entry(event_Type).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    Event_Type event_Type = new Event_Type { EventTypeId = evm.EventTypeId, EventType = evm.EventType, EventTypeImage = evm.EventTypeImagePath };

                    db.Entry(event_Type).State = System.Data.Entity.EntityState.Modified;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            return View(evm);


        }



        //  Event/Delete/5
        [Authorize(Roles = "admin")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event_Type event_Type = db.Event_Types.Find(id);
            if (event_Type == null)
            {
                return HttpNotFound();
            }
            string FileName = event_Type.EventTypeImage;
            string img = Path.Combine(Server.MapPath("~/" + FileName));
            FileInfo file = new FileInfo(img);
            if (file.Exists)
            {
                file.Delete();
            }

            db.Event_Types.Remove(event_Type);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
