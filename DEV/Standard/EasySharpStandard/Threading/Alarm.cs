using EasySharp.Observer;
using EasySharp.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EasySharp
{
    public class Alarm : StateObserverBase<AlarmState>
    {
        private readonly Queue<DateTime> remainingSettingTimes;
        private readonly Timer timer;
        private readonly int minimumPeriod;
        private readonly IDateTime dateTime;

        public Alarm(params DateTime[] settingTimes): this(100, settingTimes)
        {
        }

        public Alarm(int minimumTimerPeriod, params DateTime[] settingTimes)
            : this(new DateTimeWrapper(), minimumTimerPeriod, settingTimes)
        {
        }

        public Alarm(IDateTime dateTime, int minimumTimerPeriod, params DateTime[] settingTimes)
        {
            this.dateTime = dateTime;
            this.remainingSettingTimes = new Queue<DateTime>(settingTimes.OrderBy(dt => dt));
            this.minimumPeriod = minimumTimerPeriod;

            NotifyTimeIfAny();
            this.timer = new Timer(Tick, null, 0, GetNextPeriod());
        }

        public IReadOnlyCollection<DateTime> RemainingSettingTimes => remainingSettingTimes;

        private void Tick(object state)
        {
            NotifyTimeIfAny();

            if (remainingSettingTimes.Any())
            {
                this.timer.Change(0, this.GetNextPeriod());
            }
            else
            {
                this.timer.Dispose();
            }
        }

        private void NotifyTimeIfAny()
        {
            var now = this.dateTime.Now;
            var pastTimes = new List<DateTime>();
            while (DateTime.Compare(remainingSettingTimes.Peek(), now) < 0)
            {
                pastTimes.Add(remainingSettingTimes.Dequeue());
            }

            foreach (var pastTime in pastTimes)
            {
                var arg = this.SetCurrentState(new AlarmState(pastTime));
                this.OnStateChange(this, arg);
            }
        }

        private int GetNextPeriod()
        {
            return Math.Max(
                this.minimumPeriod,
                (int)((this.remainingSettingTimes.Peek() - this.dateTime.Now).TotalMilliseconds / 2.0));
        }
    }
}
