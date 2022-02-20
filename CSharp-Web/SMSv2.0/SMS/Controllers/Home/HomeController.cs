namespace SMS.Controllers
{
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (!User.IsAuthenticated)
            {
                return this.View("Index");
            }
            else
            {
                return this.View("IndexLoggedIn");
            }
        }
    }
}