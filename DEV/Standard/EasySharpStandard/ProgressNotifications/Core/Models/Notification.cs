namespace EasySharp.ProgressNotifications.Core.Models
{
    public class Notification : INotification
    {
        public Notification()
        {
        }

        public Notification(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
