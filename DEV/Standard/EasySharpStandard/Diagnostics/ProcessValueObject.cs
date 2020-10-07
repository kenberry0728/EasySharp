using System.Diagnostics;

namespace EasySharp.Diagnostics
{
    public class ProcessValueObject : ValueObjectBase<Process>
    {
        public ProcessValueObject(Process value) 
            : base(value)
        {
            this.ExitedEvent = new EventContainer(
                arg => value.Exited += arg,
                arg => value.Exited -= arg);
        }

        public IEventContainer ExitedEvent { get; }
    }
}
