using Microsoft.AspNetCore.Http;

namespace Hoshmand.Application.ApplicationServices.Hoshmand
{
    public class HoshmandPipelineRequest
    {
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public IFormFile IdCardBehindImage { get; set; }
        public IFormFile IdCardFrontImage { get; set; }
        public IFormFile SelfiImage { get; set; }
        public IFormFile LiveVideo { get; set; }
    }
}