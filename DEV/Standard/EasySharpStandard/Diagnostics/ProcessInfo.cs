using EasySharp.Reflection;
using System;
using System.Diagnostics;

namespace EasySharp.Diagnostics
{
    public struct ProcessInfo : IEquatable<ProcessInfo>
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Checked")]
        public ProcessInfo(Process process)
        {
            process.ThrowArgumentExceptionIfNull(nameof(process));

            this.ProcessName = process.ProcessName;
            this.Id = process.Id;
            this.MainWindowTitle = process.MainWindowTitle;
        }

        public string ProcessName { get; }

        public int Id { get; }

        public string MainWindowTitle { get; }

        public override string ToString()
        {
            return this.ToFormattedString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(ProcessInfo left, ProcessInfo right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ProcessInfo left, ProcessInfo right)
        {
            return !(left == right);
        }

        public bool Equals(ProcessInfo other)
        {
            return this.Id == other.Id
                && this.MainWindowTitle == other.MainWindowTitle
                && this.ProcessName == other.ProcessName;
        }
    }
}
