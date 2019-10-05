using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteLocker.Detection
{
    class RandomFileNameGenerator
    {
        public static String getRandomGUID()
        {
            return string.Format(@"{0}.exe", "{" + Guid.NewGuid() + "}");
        }
    }
}
