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
    public class ScheduleEventsController : Controller
    {
        private EventDbcontext db = new EventDbcontext();

        // GET: ScheduleEvents
        public ActionResult Index()
        {
            var scheduleEvents = db.ScheduleEvents.Include(s => s.Customer).Include(s => s.Event_Type);
            return View(scheduleEvents.ToList());
        }

        // GET: ScheduleEvents/Details/5


        // GET: ScheduleEvents/Create
        [Authorize(Roles = "admin")]

        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name");
            ViewBag.EventTypeId = new SelectList(db.Event_Types, "EventTypeId", "EventType");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookedEventId,EventTypeId,CustomerId,StartTime,EndTime,EntryDate")] ScheduleEvent scheduleEvent)
        {

            if (ModelState.IsValid)
            {
                ScheduleEvent scheduleEvent1 = new ScheduleEvent { EventTypeId = scheduleEvent.EventTypeId, CustomerId = scheduleEvent.CustomerId, StartTime = scheduleEvent.StartTime, EndTime = scheduleEvent.EndTime, EntryDate = DateTime.Now };
                db.ScheduleEvents.Add(scheduleEvent1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", scheduleEvent.CustomerId);
            ViewBag.EventTypeId = new SelectList(db.Event_Types, "EventTypeId", "EventType", scheduleEvent.EventTypeId);
            return View(scheduleEvent);
        }

        // GET: ScheduleEvents/Edit/5
        [Authorize(Roles = "admin")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleEvent scheduleEvent = db.ScheduleEvents.Find(id);
            if (scheduleEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", scheduleEvent.CustomerId);
            ViewBag.EventTypeId = new SelectList(db.Event_Types, "EventTypeId", "EventType", scheduleEvent.EventTypeId);
            return View(scheduleEvent);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookedEventId,EventTypeId,CustomerId,StartTime,EndTime,EntryDate")] ScheduleEvent scheduleEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", scheduleEvent.CustomerId);
            ViewBag.EventTypeId = new SelectList(db.Event_Types, "EventTypeId", "EventType", scheduleEvent.EventTypeId);
            return View(scheduleEvent);
        }



        // POST: ScheduleEvents/Delete/5
        [Authorize(Roles = "admin")]

        [HttpPost, ActionName("Delete")]
  
        public ActionResult DeleteConfirmed(int id)
        {
            ScheduleEvent scheduleEvent = db.ScheduleEvents.Find(id);
            db.ScheduleEvents.Remove(scheduleEvent);
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
