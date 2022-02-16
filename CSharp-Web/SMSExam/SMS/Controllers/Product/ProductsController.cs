namespace SMS.Controllers.Product
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.ModelsViews.Carts;
    using SMS.ModelsViews.Products;
    using System;
    using System.Linq;

    public class ProductsController : Controller
    {
        private readonly SMSDbContext db;

        public ProductsController(SMSDbContext db)
        {
            this.db = db;
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(ProductsCreateModel model)
        {
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CartId = Guid.NewGuid().ToString(),
                Cart = new Cart { }
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();

            return this.Redirect("/");
        }

        //[HttpPost]
        //[Authorize]
        //public HttpResponse Add(ProductsIdModel product)
        //{
        //    var currentProduct = this.db.Products.Find(product.Id);
        //    var user = this.db.Users.Find(User.Id);

        //    if (user == null || product == null)
        //    {
        //        return this.Error("User or product can't be empty(null).");
        //    }

        //    var userCart = this.db.Carts.Where(x => x.Id == user.CartId).FirstOrDefault();

        //    userCart.Products.Add(currentProduct);
        //    this.db.SaveChanges();

        //    var model = user.Cart.Products
        //        .Select(x => new CartsDetailsModel
        //        {
        //            Name = x.Name,
        //            Price = x.Price,
        //        })
        //        .ToList();

        //    return this.Redirect("/Carts/Details");
        //}

        public HttpResponse Add()
        {
            return this.Redirect("/Carts/Details");
        }
    }
}
