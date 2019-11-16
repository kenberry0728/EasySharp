using System;

namespace EasySharp.Threading
{
    public struct AlarmState
    {
        public AlarmState(DateTime alarmTime)
        {
            this.AlarmTime = alarmTime;
        }

        public DateTime AlarmTime { get; }

        public override string ToString()
        {
            return this.AlarmTime.ToLongTimeString();
        }
    }
}
