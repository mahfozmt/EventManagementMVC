using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_MVC_M8_project.Models.ViewModel
{
    public class VM
    {

        public class Customer
        {

            public int Id { get; set; }
            public string Name { get; set; }
            public string FatherName { get; set; }
            public string Address { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }

        }
        public class Event_Type
        {
            public int EventTypeId { get; set; }
            public string EventType { get; set; }
            public HttpPostedFileBase EventTypeImage { get; set; }
            public string EventTypeImagePath { get; set; }

        }
        public class ScheduleEvent
        {

            public int BookedEventId { get; set; }
            public int EventTypeId { get; set; }
            public int CustomerId { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public DateTime EntryDate { get; set; }

        }
    }
}