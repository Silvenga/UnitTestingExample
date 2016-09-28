namespace UnitTestingExample
{
    using System.Collections.Generic;

    public class TodoList
    {
        public TodoList(string title)
        {
            Title = title;
            Items = new List<TodoItem>();
        }

        public string Title { get; set; }

        public IList<TodoItem> Items { get; set; }
    }
}