namespace Hoshmand.Core.Entities
{
    public class CheckCodeRequestEntity : BaseEntity
    {
        public int OrderRequestId { get; set; }
        public OrderRequestEntity OrderRequest { get; set; }

        public string MessageCodeInput { get; set; }


    }
}
