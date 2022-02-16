namespace SMS.Controllers.Cart
{

    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.ModelsViews.Carts;
    using SMS.ModelsViews.Products;
    using System.Linq;

    public class CartsController : Controller
    {
        private readonly SMSDbContext db;

        public CartsController(SMSDbContext db)
        {
            this.db = db;
        }

        public HttpResponse AddProduct(string productId)
        {
            var user = this.db.Users.Find(User.Id);

            var product = this.db.Products.Find(productId);
            var cart = this.db.Carts.Where(c => c.Id == user.CartId).FirstOrDefault();

            if (user == null || cart == null || product == null)
            {
                return this.Error("Ibasi mecha!");
            }

            cart.Products.Add(product);
            this.db.SaveChanges();

            return this.Redirect("/Products/Add");
        }

        [Authorize]
        public HttpResponse Details()
        {
            var user = this.db.Users.Find(User.Id);
            var model = this.db.Carts.Where(x => x.Id == user.CartId).FirstOrDefault();

            return this.View(model);
        }

        [Authorize]
        public HttpResponse Buy()
        {
            var user = this.db.Users.Find(User.Id);
            var userCart = this.db.Carts.Where(x => x.Id == user.CartId).FirstOrDefault();

            if (user == null || userCart == null)
            {
                return this.Error("You should be logged in first and your cart should have something!");
            }

            userCart.Products.Clear();
            this.db.SaveChanges();

            return this.Redirect("/");
        }
    }
}
