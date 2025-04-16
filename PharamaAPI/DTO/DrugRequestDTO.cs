namespace PharmaAPI.DTO
{
    public class DrugRequestDTO
    {
        public string DrugName { get; set; }
        public int Quantity { get; set; }
    }

    public class ApproveRequestDTO
    {
        public int RequestId { get; set; }
    }
}
