namespace UnitTestingExample
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class TodoListManager
    {
        private readonly INotificationManager _notificationManager;
        private readonly IRemoteApiManager _apiManager;

        public TodoListManager(INotificationManager notificationManager, IRemoteApiManager apiManager)
        {
            _notificationManager = notificationManager;
            _apiManager = apiManager;
        }

        public void ChangeTitle(string title, TodoList todoList)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }
            if (todoList == null)
            {
                throw new ArgumentNullException("todoList");
            }

            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title must have value.");
            }

            todoList.Title = title;
        }

        public Guid AddItem(string name, TodoList todoList)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            if (todoList == null)
            {
                throw new ArgumentNullException("todoList");
            }

            var item = new TodoItem
            {
                Id = Guid.NewGuid(),
                IsCompleted = false,
                Name = name
            };
            todoList.Items.Add(item);

            return item.Id;
        }

        public void RemoveItem(Guid guid, TodoList todoList)
        {
            if (todoList == null)
            {
                throw new ArgumentNullException("todoList");
            }

            var result = todoList.Items
                .SingleOrDefault(x => x.Id == guid);
            if (result == null)
            {
                throw new ArgumentException("Item does not exist in list.");
            }

            todoList.Items.Remove(result);
        }

        public void CompleteItem(Guid guid, TodoList todoList)
        {
            if (todoList == null)
            {
                throw new ArgumentNullException("todoList");
            }

            var result = todoList.Items.SingleOrDefault(x => x.Id == guid);
            if (result == null)
            {
                throw new ArgumentException("Item does not exist in list.");
            }

            if (result.IsCompleted)
            {
                throw new ArgumentException("Item is already completed.");
            }

            result.IsCompleted = true;
            _notificationManager.NotifyOfCompletedTask(result.Name);
        }

        public async Task CompleteItemAsync(Guid guid, TodoList todoList)
        {
            if (todoList == null)
            {
                throw new ArgumentNullException("todoList");
            }

            var result = todoList.Items.SingleOrDefault(x => x.Id == guid);
            if (result == null)
            {
                throw new ArgumentException("Item does not exist in list.");
            }

            if (result.IsCompleted)
            {
                throw new ArgumentException("Item is already completed.");
            }

            result.IsCompleted = true;
            await _notificationManager.NotifyOfCompletedTaskAsync(result.Name);
        }

        public void DownloadRemoteItems(TodoList todoList)
        {
            if (todoList == null)
            {
                throw new ArgumentNullException("todoList");
            }

            var items = _apiManager.GetRemoveItems(todoList.Title);
            foreach (var item in items)
            {
                todoList.Items.Add(item);
            }
        }
    }
}