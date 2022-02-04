namespace CarShop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.Services;
    using CarShop.ViewModels.Cars;
    using MyWebServer.Controllers;
    using MyWebServer.Http;

    using static Data.DataConstants;

    public class CarsController : Controller
    {
        public readonly IValidator validator;
        public readonly ApplicationDbContext db;

        public CarsController(
            IValidator validator,
            ApplicationDbContext db)
        {
            this.validator = validator;
            this.db = db;
        }

        [Authorize]
        public HttpResponse All()
        {
            var user = this.db.Users.Find(User.Id);
            var carsQuery = this.db.Cars.AsQueryable();

            if (user.IsMechanic)
            {
                carsQuery = carsQuery
                    .Where(c => c.Issues.Any(i => i.IsFixed == false));
            }
            else
            {
                carsQuery = carsQuery.Where(u => u.OwnerId == User.Id);
            }

            var listCars = carsQuery
                .Select(c => new CarsVisualModel
                {
                    Id = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    PictureUrl = c.PictureUrl,
                    PlateNumber = c.PlateNumber,
                    RemainingIssues = c.Issues.Where(x => x.IsFixed == false).Count(),
                    FixedIssues = c.Issues.Where(x => x.IsFixed).Count()
                })
                .ToList();

            return this.View(listCars);
        }

        [Authorize]
        public HttpResponse Add()
        {
            var user = this.db.Users.Find(User.Id);

            if (user.IsMechanic)
            {
                return this.Redirect("/Cars/All");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(CarAddModel model)
        {
            var user = this.db.Users.Find(User.Id);

            if (user.IsMechanic)
            {
                return this.Redirect("/Cars/All");
            }

            var modelErrors = this.validator.ValidateCar(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = User.Id
            };

            this.db.Cars.Add(car);

            this.db.SaveChanges();

            return this.Redirect("/Cars/All");
        }
    }
}
