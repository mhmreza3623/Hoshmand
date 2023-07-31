using FluentValidation;
using Hoshmand.Core.Dto.Requests;

namespace Hoshmand.Presentation.Validations
{
    public class AuthenticationValidation : AbstractValidator<AuthenticatonRequestModel>
    {
        public AuthenticationValidation()
        {
            RuleFor(q => q.Mobile).Must(x => x.StartsWith("09"));
            RuleFor(q => q.Mobile).Length(11); 
            RuleFor(q => q.Mobile).NotNull().NotEmpty();
         
            RuleFor(q => q.NationalCode).NotNull().NotEmpty();
            RuleFor(q => q.NationalCode).Length(11);


            RuleFor(q => q.IdCardFrontImage).NotEmpty().NotNull();
            RuleFor(q => q.IdCardBehindImage).NotEmpty().NotNull();
            RuleFor(q => q.liveVideo).NotEmpty().NotNull();
            RuleFor(q => q.SelfiImage).NotEmpty().NotNull();
        }
    }
}
