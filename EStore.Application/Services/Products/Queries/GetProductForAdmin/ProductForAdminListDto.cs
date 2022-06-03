namespace EStore.Application.Services.Products.Queries.GetProductForAdmin
{
    public class ProductForAdminListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Inventory { get; set; }
        public bool IsDisplay { get; set; }
    }

}
