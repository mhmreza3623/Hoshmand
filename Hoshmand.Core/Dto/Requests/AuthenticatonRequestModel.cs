using Microsoft.AspNetCore.Http;

namespace Hoshmand.Core.Dto.Requests
{
    public class AuthenticatonRequestModel
    {
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public IFormFile IdCardFrontImage { get; set; }
        public IFormFile IdCardBehindImage { get; set; }
        public IFormFile SelfiImage { get; set; }
        public IFormFile liveVideo { get; set; }
    }
}
