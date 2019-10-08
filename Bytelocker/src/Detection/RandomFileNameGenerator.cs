using System;

namespace ByteLocker.Detection
{
    class RandomFileNameGenerator
    {
        public static String GetRandomGUID()
        {
            return string.Format(@"{0}.exe", "{" + Guid.NewGuid() + "}");
        }
    }
}
