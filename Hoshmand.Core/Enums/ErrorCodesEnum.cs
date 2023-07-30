using System.ComponentModel;

namespace Hoshmand.Core.Enums;

internal enum ErrorCodesEnum
{

    [Description(" عدم همخوانی اطلاعات با کارت ملی ")] Error1 = 1001,
    [Description(" وجود چند چهره در تصویر کارت ملی")] Error2 = 1005,
    [Description(" عدم وجود چهره در تصویر کارت ملی")] Error3 = 1006,
    [Description(" عدم وضوح اطلاعات کارت ملی")] Error4 = 1021,
    [Description(" عدم وضوح اطلاعات پشت کارت ملی")] Error5 = 1022,
    [Description(" زاویه نامناسب کارت ملی")] Error6 = 1009,
    [Description(" عدم وجود کارت ملی در عکس")] Error7 = 1015,
    [Description(" پایین بودن کیفیت کارت ملی")] Error8 = 1014,
    [Description(" پیام خطای شاهکار")] Error9 = 1018,
    [Description(" عدم تطابق پیام ارسالی")] Error10 = 1019,
    [Description(" عدم تطابق اطلاعات کارت ملی")] Error11 = 1020,
    [Description(" عدم همخوانی کدملی")] Error12 = 1030,
    [Description(" پایین بودن کیفیت عکس")] Error13 = 1014,
    [Description(" فاصله بیش از حد شخص با دوربین")] Error14 = 1003,
    [Description(" عدم وضوح تصویر")] Error15 = 1004,
    [Description(" زاویه نامناسب تصویر")] Error16 = 1009,
    [Description(" عدم نگاه کردن مستقیم به دوربین")] Error17 = 1010,
    [Description(" عدم تطابق تصویر شخص با تصویر ثبت شده")] Error18 = 1011,
    [Description(" وجود چند چهره در تصویر")] Error19 = 1005,
    [Description(" عدم وجود چهره در تصویر")] Error20 = 1006,
    [Description(" عدم تطابق ویدیو شخص با تصویر ثبت شده")] Error21 = 1011,
    [Description(" وجود چند چهره در ویدیو")] Error22 = 1005,
    [Description(" عدم وجود چهره در ویدیو")] Error23 = 1005,
    [Description(" زنده نبودن ویدیو ارسالی")] Error24 = 1002,
}
