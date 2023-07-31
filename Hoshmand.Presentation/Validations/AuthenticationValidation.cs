using FluentValidation;
using Hoshmand.Core.Dto.Requests;

namespace Hoshmand.Presentation.Validations
{
    public class AuthenticationValidation : AbstractValidator<AuthenticatonRequestModel>
    {
        public AuthenticationValidation()
        {
            RuleFor(q => q.Mobile).Must(x => x.StartsWith("09")).WithMessage("شماره موبایل با 09 شروع شود"); ;
            RuleFor(q => q.Mobile).Length(11).WithMessage("طول شماره موبایل اشتباه است"); ; 
            RuleFor(q => q.Mobile).NotNull().NotEmpty().WithMessage("شماره موبایل خالی است"); ;
         
            RuleFor(q => q.NationalCode).NotNull().NotEmpty().WithMessage("کد ملی خالی است"); ;
            RuleFor(q => q.NationalCode).Length(10).WithMessage("طول کد ملی اشتباه است"); ;


            RuleFor(q => q.IdCardFrontImage).NotEmpty().NotNull().WithMessage("تصویر روی کارت ملی خالی است"); ;
            RuleFor(q => q.IdCardBehindImage).NotEmpty().NotNull().WithMessage("تصویر پشت کارت ملی خالی  است"); ;
            RuleFor(q => q.liveVideo).NotEmpty().NotNull().WithMessage("ویدیوی خالی است"); ;
            RuleFor(q => q.SelfiImage).NotEmpty().NotNull().WithMessage("تصویر سلفی خالی است"); ;
        }
    }
}
