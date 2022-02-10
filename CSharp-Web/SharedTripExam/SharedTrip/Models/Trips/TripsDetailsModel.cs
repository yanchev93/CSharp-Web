namespace SharedTrip.Models.Trips
{
    public class TripsDetailsModel
    {
        public string Id { get; init; }

        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; set; }

        public int Seats { get; init; }

        public string Description { get; init; }

        public string ImagePath { get; init; }
    }
}
