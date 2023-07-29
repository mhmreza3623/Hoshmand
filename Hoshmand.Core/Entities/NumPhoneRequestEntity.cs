namespace Hoshmand.Core.Entities
{
    public class NumPhoneRequestEntity : BaseEntity
    {
        public int OrderRequestId { get; set; }
        public OrderRequestEntity OrderRequest { get; set; }

        public string Number { get; set; }
        public string Phone { get; set; }

    }
}
