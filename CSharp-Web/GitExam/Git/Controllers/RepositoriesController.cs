namespace Git.Controllers
{
    using System.Linq;

    using Git.Data;
    using Git.Data.Models;
    using Git.Models.Repositories;
    using Git.Services;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants;


    public class RepositoriesController : Controller
    {
        public readonly ApplicationDbContext db;
        private readonly IValidator validator;

        public RepositoriesController(ApplicationDbContext db, IValidator validator)
        {
            this.db = db;
            this.validator = validator;
        }

        public HttpResponse All()
        {
            var repositories = this.db.Repository
                .Where(r => r.IsPublic)
                .Select(x => new RepositoryListingViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn.ToString("g"),
                    CommitsCount = x.Commits.Count()
                })
                .ToList();

            return View(repositories);
        }

        [Authorize]
        public HttpResponse Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateRepositoryModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var createRepository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == RepositoryTypePublic,
                OwnerId = this.User.Id
            };

            this.db.Repository.Add(createRepository);

            this.db.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
