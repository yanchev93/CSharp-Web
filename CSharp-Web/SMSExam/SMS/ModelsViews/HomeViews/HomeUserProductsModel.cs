namespace SMS.ModelsViews.HomeViews
{
    using System.Collections.Generic;

    public class HomeUserProductsModel
    {
        public string Username { get; set; }

        public ICollection<HomeProductModel> Products { get; set; }
    }
}
