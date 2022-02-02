namespace Git.Services
{
    using Git.Models;
    using Git.Models.Repositories;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateRepository(CreateRepositoryModel model);
    }
}
