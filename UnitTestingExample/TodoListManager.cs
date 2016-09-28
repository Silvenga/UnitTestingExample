namespace UnitTestingExample
{
    using System;
    using System.Linq;

    public class TodoListManager
    {
        private readonly INotificationManager _manager;

        public TodoListManager(INotificationManager manager)
        {
            _manager = manager;
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
            _manager.NotifyOfCompletedTask(result.Name);
        }
    }
}