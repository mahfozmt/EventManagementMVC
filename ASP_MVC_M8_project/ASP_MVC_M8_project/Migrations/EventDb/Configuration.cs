namespace ASP_MVC_M8_project.Migrations.EventDb
{
    using ASP_MVC_M8_project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ASP_MVC_M8_project.Models.EventDbcontext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\EventDb";
        }

        protected override void Seed(ASP_MVC_M8_project.Models.EventDbcontext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            context.Roles.AddOrUpdate(x => x.RoleId, new Role() { RoleId = 1, RoleName = "admin" }, new Role() { RoleId = 2, RoleName = "guest" }, new Role() { RoleId = 3, RoleName = "user" });

            context.tblUsers.AddOrUpdate(x => x.tblUserId, new tblUser() { tblUserId = 1, UserName = "admin", Password = "admin", RoleId = 1 });
        }
    }
}
