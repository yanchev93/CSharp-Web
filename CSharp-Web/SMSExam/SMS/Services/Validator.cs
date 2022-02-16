namespace SMS.Services
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using SMS.ModelsViews.Users;

    using static Data.DataConstants;

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

        //public ICollection<string> ValidateAddTrip(TripsAddModel model)
        //{
        //    var errors = new List<string>();

        //    if (model.Seats < TripMinSeats || TripMaxSeats < model.Seats)
        //    {
        //        errors.Add($"Seats can only be between {TripMinSeats} and {TripMaxSeats}.");
        //    }

        //    return errors;
        //}
    }
}
