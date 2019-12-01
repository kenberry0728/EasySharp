using System;

namespace EasySharp.Threading
{
    public struct AlarmState : IEquatable<AlarmState>
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

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(AlarmState left, AlarmState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(AlarmState left, AlarmState right)
        {
            return !(left == right);
        }

        public bool Equals(AlarmState other)
        {
            return this.AlarmTime == other.AlarmTime;
        }
    }
}
