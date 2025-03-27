namespace PharmaAPI.DTO
{
    public class InventoryDTO
    {
        public int DrugId { get; set; }  // ✅ Include DrugId for DB reference
        public string DrugName { get; set; } // ✅ Allow adding drugs by name
        public int Quantity { get; set; }
    }
}
