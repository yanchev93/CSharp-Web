namespace CarShop.Controllers
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Issues;
    using Microsoft.EntityFrameworkCore;
    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using System.Linq;

    public class IssuesController : Controller
    {
        private readonly ApplicationDbContext db;

        public IssuesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var user = this.db.Users.Find(User.Id);
            if (!user.IsMechanic)
            {
                var userOwnsCar = this.db
                    .Cars
                    .Any(c => c.Id == carId && c.OwnerId == this.User.Id);

                if (!userOwnsCar)
                {
                    return Unauthorized();
                }
            }

            var carIssues = this.db
                .Cars
                .Where(i => i.Id == carId)
                .Select(c => new IssueCarIssueModel
                {
                    CarId = carId,
                    Model = c.Model,
                    Year = c.Year,
                    Issues = c.Issues.Select(i => new IssuesCarModel
                    {
                        Description = i.Description,
                        IsFixed = i.IsFixed ? "Yes" : "Not Yet",
                        IssueId = i.Id,
                    }).ToList()
                }).FirstOrDefault();

            return this.View(carIssues);
        }

        [Authorize]
        public HttpResponse Add(string carId)
        {
            return this.View(new IssueAddModel { CarId = carId });
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(IssueDescriptionModel model)
        {
            var issue = new Issue
            {
                CarId = model.CarId,
                Description = model.Description,
                IsFixed = false
            };

            var car = this.db.Cars
                .Where(c => c.Id == model.CarId)
                .FirstOrDefault();
            car.Issues.Add(issue);

            this.db.SaveChanges();

            return this.Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            var user = this.db.Users.Find(User.Id);
            if (!user.IsMechanic)
            {
                return this.Redirect("/Cars/All");
            }

            var issue = this.db.Issues
                .FirstOrDefault(i => i.Id == issueId && i.CarId == carId);

            issue.IsFixed = true;

            this.db.SaveChanges();

            return this.View();
        }
    }
}
