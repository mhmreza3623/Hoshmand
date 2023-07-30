namespace Hoshmand.Core.Dto.Response
{
    public class CompareFaceResponseDto : HoshmandIdCardResponseDto
    {
        public byte[] VideoLink { get; set; }
        public decimal VideoSimilarity { get; set; }
        public byte[] FaceImageLink { get; set; }
        public decimal ImageSimilarity { get; set; }
        public decimal IdCardSimilarity { get; set; }
    }
}
