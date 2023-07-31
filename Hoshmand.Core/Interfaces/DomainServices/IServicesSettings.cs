namespace Hoshmand.Core.Interfaces.DomainServices
{
    public interface IServiceSettings
    {
        string HoshmandOrderBaseAddress { get; }
        string HoshmandIdCardBaseAddress { get; set; }
    }
}
