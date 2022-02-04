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
            if (!this.UserHasAccess(carId))
            {
                return Unauthorized();
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
                        IssueId = i.Id,
                        Description = i.Description,
                        IsFixedButton = i.IsFixed,
                        IsFixed = i.IsFixed ? "Yes" : "Not Yet",
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
            if (!this.UserHasAccess(model.CarId))
            {
                return Unauthorized();
            }

            if (model.Description.Length < 5)
            {
                return this.Error("Description must be more than 5 characters long.");
            }

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
            var user = this.db.Users.Find(this.User.Id);
            if (!user.IsMechanic)
            {
                return Unauthorized();
            }

            var issue = this.db.Issues
                .FirstOrDefault(i => i.Id == issueId && i.CarId == carId);

            issue.IsFixed = true;

            this.db.SaveChanges();

            return this.Redirect($"/Issues/CarIssues?carId={carId}");
        }

        public HttpResponse Delete(string issueId, string carId)
        {
            //check if this car belongs to the logged user
            //check if the user is mechanic
            if (!this.UserHasAccess(carId))
            {
                return Unauthorized();
            }

            var issue = this.db.Issues.Find(issueId);

            this.db.Issues.Remove(issue);
            this.db.SaveChanges();

            return this.Redirect($"/Issues/CarIssues?carId={carId}");

        }

        public bool UserHasAccess(string carId)
        {
            var user = this.db.Users.Find(User.Id);

            if (!user.IsMechanic)
            {
                var userOwnsCar = this.db.Cars
                    .Any(c => c.Id == carId && c.OwnerId == this.User.Id);

                if (!userOwnsCar)
                {
                    return false;
                }

            }

            return true;
        }
    }
}
