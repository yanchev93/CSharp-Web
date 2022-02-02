namespace Git.Controllers
{
    using Git.Data;
    using Git.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;
    using static Data.DataConstants;

    public class CommitsController : Controller
    {
        public readonly ApplicationDbContext db;
        private readonly IValidator validator;

        public CommitsController(ApplicationDbContext db, IValidator validator)
        {
            this.db = db;
            this.validator = validator;
        }

        public HttpResponse Create()
        {
            return View();
        }

        public HttpResponse All()
        {
            if (User.IsAuthenticated == false)
            {
                return Redirect("/");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Delete(string id)
        {
            var commit = this.db.Commit
                .FirstOrDefault(x => x.Id == id);

            if (User.Id != commit.CreatorId || commit == null)
            {
                return BadRequest();
            }


            this.db.Commit.Remove(commit);
            this.db.SaveChanges();

            return Redirect("/Commits/All");
        }
    }
}
