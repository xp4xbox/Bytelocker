using System;

namespace ByteLocker.Detection
{
    internal class RandomFileNameGenerator
    {
        public static string GetRandomGUID()
        {
            return string.Format(@"{0}.exe", "{" + Guid.NewGuid() + "}");
        }
    }
}