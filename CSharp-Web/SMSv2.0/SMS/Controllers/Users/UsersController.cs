namespace SMS.Controllers.Users
{
    using System.Linq;

    using MyWebServer.Http;
    using MyWebServer.Controllers;


    using SMS.Services;
    using SMS.Data;
    using SMS.Data.Models;
    using SMS.ModelViews.Users;

    using static Data.DataConstants;
    using System;

    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly SMSDbContext db;

        public UsersController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            SMSDbContext db)
        {
            this.validator = validator;
            this.passwordHasher = passwordHasher;
            this.db = db;
        }


        public HttpResponse Index()
        {
            return this.View();
        }

        public HttpResponse Login()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(UserLoginModel model)
        {
            var hashedPassword = passwordHasher.HashPassword(model.Password);

            var userId = this.db
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/");
        }

        public HttpResponse Register()
        {
            if (User.IsAuthenticated)
            {
                return Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(UserRegisterModel model)
        {
            var modelErrors = this.validator.ValidateUser(model);

            if (this.db.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' already exists.");
            }

            if (this.db.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with this email already exists. Please provide a new email");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email,
            };

            var cart = new Cart
            {
                UserId = user.Id,
                User = user
            };

            this.db.Users.Add(user);
            this.db.Carts.Add(cart);

            this.db.SaveChanges();

            return Redirect("/Users/Login");
        }


        public HttpResponse Logout()
        {
            if (User.IsAuthenticated)
            {
                this.SignOut();

                return Redirect("/");
            }

            return this.Redirect("/Users/Register");
        }
    }
}
