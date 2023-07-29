using Microsoft.AspNetCore.Mvc;

namespace Hoshmand.Presentation.Models
{
    public class AuthenticatonRequestModel
    {
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
    }
}
