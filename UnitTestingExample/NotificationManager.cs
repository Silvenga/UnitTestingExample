namespace UnitTestingExample
{
    using System;

    public interface INotificationManager
    {
        void NotifyOfCompletedTask(string name);
    }

    public class NotificationManager : INotificationManager
    {
        public void NotifyOfCompletedTask(string name)
        {
            throw new NotImplementedException();
        }
    }
}