using System.Collections.Generic;

namespace SharedTrip.Models.Trips
{
    public class TripsAllVisualizeModel
    {
        public string Id { get; init; }

        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; init; }

        public int Seats { get; init; }

        public int TakenSeats { get; init; }
    }
}
