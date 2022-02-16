namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.ModelsViews.HomeViews;
    using System.Linq;

    public class HomeController : Controller
    {
        private readonly SMSDbContext db;

        public HomeController(SMSDbContext db)
        {
            this.db = db;
        }

        public HttpResponse Index()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.View("Index");
            }
            else
            {
                var user = db.Users.Find(User.Id);

                var products = this.db.Products
                    .Select(x => new HomeProductModel
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                    })
                    .ToList();

                var model = new HomeUserProductsModel
                {
                    Username = user.Username,
                    Products = products
                };

                return this.View("IndexLoggedIn", model);
            }
        }

    }
}