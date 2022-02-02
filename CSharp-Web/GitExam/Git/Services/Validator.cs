namespace Git.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Git.Models;
    using Git.Models.Repositories;
    using static Data.DataConstants;

    public class Validator : IValidator
    {


        public ICollection<string> ValidateUser(RegisterUserFormModel model)
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

        public ICollection<string> ValidateRepository(CreateRepositoryModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length < RepositoryMinName || model.Name.Length > RepositoryMaxName)
            {
                errors.Add($"Repostory name '{model.Name}' is not valid. It must be between {RepositoryMinName} and {RepositoryMaxName} charecters long.");
            }

            if (model.RepositoryType != RepositoryTypePrivate && model.RepositoryType != RepositoryTypePublic)
            {
                errors.Add($"Repository type can be either 'Public' or 'Private'");
            }

            return errors;
        }
    }
}
