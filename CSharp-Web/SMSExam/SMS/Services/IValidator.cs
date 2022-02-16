namespace SMS.Services
{
    using SMS.ModelsViews.Users;
    using System.Collections.Generic;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterModel model);

        //public ICollection<string> ValidateAddTrip(TripsAddModel model);

    }
}
