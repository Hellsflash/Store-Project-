namespace Store.Migrations
{
    using Store.Data;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Store.Data.StoreDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Store.Data.StoreDbContext";
        }

        protected override void Seed(StoreDbContext context)
        {
            if (context.Products.Any())
            {
                return;
            }

            var user = context.Users.FirstOrDefault();

            if (user == null)
            {
                return;
            }
            context.Products.Add(new Product
            {
                Name = "Samusung TV",
                Categorie = "Electronics",
                Description = "Cool Smart TV",
                Price = 2999,
                ImageUrl = "http://s3.amazonaws.com/digitaltrends-uploads-prod/2013/02/Samsung-Smart-Tv.jpg",
                AuthorId = user.Id

            });

        }
    }
}
