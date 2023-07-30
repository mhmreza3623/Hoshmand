using Microsoft.AspNetCore.Http;

namespace Hoshmand.Core.Common;

public static class Extenstions
{
    public static byte[] ToByteArray(this IFormFile stream)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            stream.CopyTo(ms);

            return ms.ToArray();
        }
    }

}
