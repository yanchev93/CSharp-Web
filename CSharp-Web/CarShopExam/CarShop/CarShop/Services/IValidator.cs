namespace CarShop.Services
{
    using System.Collections.Generic;
    using CarShop.ViewModels.Cars;
    using CarShop.ViewModels.Users;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterModel model);
        ICollection<string> ValidateCar(CarAddModel model);
    }
}
