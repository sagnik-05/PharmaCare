namespace PharmaAPI.DTO
{
    public class InventoryDTO
    {
        public int DrugId { get; set; }
        public string DrugName { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int Quantity { get; set; }
    }
}
