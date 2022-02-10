namespace SharedTrip.Services
{
    using System.Collections.Generic;
    using SharedTrip.Models.Users;
    using SharedTrip.Models.Trips;

    public interface IValidator
    {
        ICollection<string> ValidateUser(UserRegisterModel model);

        public ICollection<string> ValidateAddTrip(TripsAddModel model);

    }
}
