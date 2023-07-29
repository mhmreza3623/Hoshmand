namespace Hoshmand.Core.Entities
{
    public class IdCardRequestEntity : BaseEntity
    {
        public int OrderRequestId { get; set; }
        public OrderRequestEntity OrderRequest { get; set; }

        public byte[] ImageId1 { get; set; }
        public byte[] ImageId2 { get; set; }

    }
}
