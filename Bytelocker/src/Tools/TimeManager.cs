using Bytelocker.Settings;
using Bytelocker.src.Tools;
using System;

namespace Bytelocker.Tools
{
    class TimeManager
    {
        private RegistryManager rm;
        private DateTime due_date;

        public void ReadFromRegistry()
        {
            this.rm = new RegistryManager();
            DateTime initial_date = DateTime.Parse(B64Manager.Base64ToString(rm.ReadStringValue(RegistryManager.SETTINGS_KEY_NAME, "t")));
            this.due_date = initial_date.AddHours(Bytelocker.TIME_TILL_REMOVAL_HOURS);
        }

        public double DetermineRemainingTimeInSeconds()
        {
            TimeSpan finalDate = due_date.Subtract(DateTime.Now);

            return Math.Round(finalDate.TotalSeconds);
        }
    }
}
