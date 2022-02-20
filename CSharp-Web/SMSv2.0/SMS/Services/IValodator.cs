namespace SMS.Services
{
    using System.Collections.Generic;

    using SMS.ModelViews.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterModel model);

        //ICollection<string> ValidateCar(CarAddModel model);
    }
}
