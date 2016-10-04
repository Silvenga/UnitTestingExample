namespace UnitTestingExample.TodoExample
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

#pragma warning disable 1998
        public async Task NotifyOfCompletedTaskAsync(string name)
#pragma warning restore 1998
        {
            throw new Exception("Can't be done locally!");
        }
    }
}