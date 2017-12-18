namespace Springfield.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Springfield.DBModels;
    using Springfield.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Springfield.DBModels.MyContext>
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            _roleManager = new RoleManager<IdentityRole>(
                new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        protected override void Seed(Springfield.DBModels.MyContext context)
        {
            var exist = _roleManager.RoleExists("Manager");

            if (!exist)
            {
                var owner = new IdentityRole()
                {
                    Name = "Manager"
                };

                _roleManager.Create(owner);
            }

            exist = _roleManager.RoleExists("Seller");

            if (!exist)
            {
                var seller = new IdentityRole()
                {
                    Name = "Seller"
                };

                _roleManager.Create(seller);
            }

            var db = new DBModels.MyContext();

            if (db.Catalogue.Where(x=>x.Name == "汽车").FirstOrDefault() == null)
            {
                var newCatalog = new Catalog();
                {
                    newCatalog.Name = "汽车";
                }

                db.Catalogue.Add(newCatalog);
            }

            if (db.Catalogue.Where(x => x.Name == "家具").FirstOrDefault() == null)
            {
                var newCatalog = new Catalog();
                {
                    newCatalog.Name = "家具";
                }

                db.Catalogue.Add(newCatalog);
            }

            if (db.Catalogue.Where(x => x.Name == "家电").FirstOrDefault() == null)
            {
                var newCatalog = new Catalog();
                {
                    newCatalog.Name = "家电";
                }

                db.Catalogue.Add(newCatalog);
            }

            if (db.Catalogue.Where(x => x.Name == "娱乐").FirstOrDefault() == null)
            {
                var newCatalog = new Catalog();
                {
                    newCatalog.Name = "娱乐";
                }

                db.Catalogue.Add(newCatalog);
            }

            db.SaveChanges();


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
