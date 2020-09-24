using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ASP_MVC_M8_project.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; } 
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(50), Display(Name ="Father Name")]
        public string FatherName { get; set; }
        [Required, StringLength(300)]
        public string Address { get; set; }
        [Required, StringLength(20)]
        public string Mobile { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        public virtual List<ScheduleEvent> ScheduleEvents { get; set; }



    }
    public class Event_Type
    {
        [Key]
        public int EventTypeId { get; set; }
        [Required, StringLength(25)]
        public string EventType { get; set; }
        public string EventTypeImage { get; set; }
        public virtual List<ScheduleEvent> ScheduleEvents { get; set; }

    }
     public class ScheduleEvent
    {
        [Key]
        public int BookedEventId { get; set; }
        [ForeignKey("Event_Type") ]
        public int EventTypeId { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EntryDate { get; set; }

        public virtual Event_Type Event_Type { get; set; }
        public virtual Customer  Customer { get; set; }

    }
    public class tblUser
    {

        public int tblUserId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]

        public int RoleId { get; set; }
        public virtual Role Roles { get; set; }
    }
    public class Role
    {
        public Role()
        {
            tblUsers = new HashSet<tblUser>();
        }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<tblUser> tblUsers { get; set; }
    }
    public class EventDbcontext :DbContext
    {
        public EventDbcontext() : base("EventDbcontext")
        {

        }
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Customer> Customers  { get; set; }
        public DbSet<Event_Type>  Event_Types  { get; set; }
        public DbSet<ScheduleEvent> ScheduleEvents  { get; set; }

    }
   


}