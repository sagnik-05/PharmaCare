namespace PharmaAPI.DTO
{
    public class DrugDTO
    {
        public int DrugId { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class CreateDrugDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

    public class UpdateDrugDTO
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
