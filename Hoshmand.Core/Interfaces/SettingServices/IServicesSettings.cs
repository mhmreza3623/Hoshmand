namespace Hoshmand.Core.Interfaces.SettingServices
{
    public interface IServiceSettings
    {
        string HoshmandOrderBaseAddress { get; }
        string HoshmandIdCardBaseAddress { get; set; }
    }
}
