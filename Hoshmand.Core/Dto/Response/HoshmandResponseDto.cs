namespace Hoshmand.Core.Dto.Response
{
    public class HoshmandResponseDto : HoshmandOrderResponseDto
    {
        public string IdNum { get; set; }
        public string Phone { get; set; }
        public bool ShahkarResult { get; set; }
        public string MessageCodeOutput { get; set; }
        public string RejectCode { get; set; }
        public string RejectMessage { get; set; }
    }
}
