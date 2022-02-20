namespace SMS.Controllers.Products
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.ModelViews.Products;

    public class ProductsController : Controller
    {
        private readonly SMSDbContext dbContext;

        public ProductsController(SMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Authorize]
        public HttpResponse Create()
        {
            return this.View();

        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(ProductCreateModel model)
        {
            var createProduct = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Cart = new Cart()
            };

            this.dbContext.Products.Add(createProduct);
            this.dbContext.SaveChanges();


            return this.Redirect("/");
        }
    }
}
