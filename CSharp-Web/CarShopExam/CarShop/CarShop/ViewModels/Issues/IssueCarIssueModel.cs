namespace CarShop.ViewModels.Issues
{
    using CarShop.Data.Models;
    using System.Collections.Generic;

    public class IssueCarIssueModel
    {
        public string CarId { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public IEnumerable<IssuesCarModel> Issues { get; init; }

    }
}
