namespace Hoshmand.Core.Dto.Response
{
    public class HoshmandIdCardResponseDto : HoshmandResponseDto
    {
        public object IdCardLink { get; set; }
        public object IdCardLink2 { get; set; }
        public string IdCardBirthDate { get; set; }
        public string IdCardNationalCode { get; set; }
        public string IdCardSerialNum { get; set; }
    }
}
