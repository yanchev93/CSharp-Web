namespace SharedTrip.Controllers.Trip
{
    using System;
    using System.Globalization;
    using System.Linq;

    using MyWebServer.Controllers;
    using MyWebServer.Http;
    using SharedTrip.Data;
    using SharedTrip.Data.Models;
    using SharedTrip.Models.Trips;
    using SharedTrip.Services;

    public class TripsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IValidator validator;

        public TripsController(
            IValidator validator,
            ApplicationDbContext db)
        {
            this.validator = validator;
            this.db = db;
        }

        [Authorize]
        public HttpResponse All()
        {
            if (IsUserAuthorized() == false)
            {
                return this.Redirect("/Users/Register");
            }

            var allTrips = this.db.Trips
                .Select(t => new TripsAllVisualizeModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats - t.UserTrips.Count()
                })
                .ToList();

            return this.View(allTrips);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (IsUserAuthorized() == false)
            {
                return this.Redirect("/Users/Register");
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(TripsAddModel model)
        {
            if (IsUserAuthorized() == false)
            {
                return this.Redirect("/Users/Register");
            }

            var modelErrors = this.validator.ValidateAddTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var newTrip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.ParseExact(
                                                    model.DepartureTime,
                                                    "dd.MM.yyyy HH:mm",
                                                    CultureInfo.InvariantCulture),
                ImagePath = model.ImagePath,
                Seats = model.Seats,
                Description = model.Description,
            };

            this.db.Trips.Add(newTrip);
            this.db.SaveChanges();

            return this.Redirect("/Trips/All");
        }


        [Authorize]
        public HttpResponse Details(string tripId)
        {
            var currentTrip = this.db.Trips.FirstOrDefault(x => x.Id == tripId);

            if (currentTrip == null)
            {
                return this.Redirect("/Trips/All");
            }

            if (IsUserAuthorized() == false)
            {
                return this.Redirect("/Users/Register");
            }

            var details = new TripsDetailsModel
            {
                Id = currentTrip.Id,
                StartPoint = currentTrip.StartPoint,
                EndPoint = currentTrip.EndPoint,
                DepartureTime = currentTrip.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                Seats = currentTrip.Seats - currentTrip.UserTrips.Count(),
                Description = currentTrip.Description,
                ImagePath = currentTrip.ImagePath,
            };

            return this.View(details);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            if (IsUserAuthorized() == false)
            {
                return this.Redirect("/Users/Register");
            }

            Trip trip = this.db.Trips.Find(tripId);
            if (trip == null)
            {
                return this.Redirect("/Trips/All");
            }

            var isUserInTheTrip = this.db.Trips
                .Where(t => t.Id == tripId)
                .Any(u => u.UserTrips.Any(u => u.UserId == User.Id));

            if (isUserInTheTrip == true)
            {
                return this.Error($"You can't join the same trip more than once.");
            }

            var userTrip = new UserTrip
            {
                UserId = User.Id,
                TripId = tripId,
            };


            this.db.Trips.Find(tripId).UserTrips.Add(userTrip);
            this.db.SaveChanges();

            return this.Redirect("/Trips/All");
        }

        public bool IsUserAuthorized()
        {
            if (User.IsAuthenticated)
            {
                return true;
            }

            return false;
        }

    }
}
