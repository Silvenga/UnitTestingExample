namespace UnitTestingExample
{
    using System;
    using System.Collections.Generic;

    public interface IRemoteApiManager
    {
        IEnumerable<TodoItem> GetRemoveItems(string title);
    }

    public class RemoteApiManager : IRemoteApiManager
    {
        public IEnumerable<TodoItem> GetRemoveItems(string title)
        {
            throw new Exception("Can't run locally!");
        }
    }
}