namespace UnitTestingExample
{
    using System;
    using System.Threading.Tasks;

    public interface INotificationManager
    {
        void NotifyOfCompletedTask(string name);
        Task NotifyOfCompletedTaskAsync(string name);
    }

    public class NotificationManager : INotificationManager
    {
        public void NotifyOfCompletedTask(string name)
        {
            throw new Exception("Can't be done locally!");
        }

        public async Task NotifyOfCompletedTaskAsync(string name)
        {
            throw new Exception("Can't be done locally!");
        }
    }
}