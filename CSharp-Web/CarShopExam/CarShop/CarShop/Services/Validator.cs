namespace CarShop.Services
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using CarShop.ViewModels.Users;

    using static Data.DataConstants;
    using CarShop.ViewModels.Cars;
    using System;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateUser(UserRegisterModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UserMinUsername || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {DefaultMaxLength} charecters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{model.Email}' is not a valid e-mail address.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contains any whitespaces.");
            }

            if (model.Password.Length < UserMinPassword
                || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} charecters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and Confirm Password should be the same.");
            }

            return errors;
        }

        public ICollection<string> ValidateCar(CarAddModel model)
        {
            var errors = new List<string>();

            if (model.Year < CarMinYear || CarMaxYear < model.Year)
            {
                errors.Add($"The year can be only between '{CarMinYear}' & '{CarMaxYear}'");
            }

            if (model.Model.Length < CarMinModel || DefaultMaxLength < model.Model.Length)
            {
                errors.Add($"Model '{model.Model}' is not valid. It must be between {CarMinModel} and {DefaultMaxLength} charecters long.");
            }

            //bool validUrl = Uri.CheckSchemeName(model.PictureUrl);

            //if (!validUrl)
            //{
            //    errors.Add($"The provided URL is not valid. Please try a new one.");
            //}

            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumberRegEx))
            {
                errors.Add($"The provided plate number is not valid. A valid Plate number is (2 Capital English letters, followed by 4 digits, followed by 2 Capital English letters). No spaces in between.");
            }

            return errors;
        }
    }
}
