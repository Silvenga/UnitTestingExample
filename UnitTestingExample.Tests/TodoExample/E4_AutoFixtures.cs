namespace UnitTestingExample.Tests.TodoExample
{
    using System;
    using System.Collections.Generic;

    using FluentAssertions;

    using UnitTestingExample.TodoExample;

    using Xunit;

    // ReSharper disable once InconsistentNaming
    public class E4_AutoFixtures
    {
        // [ ] Creating Object Graphs
        // [ ] Creating forgoten values
        // [ ] Show data generated

        [Fact]
        public void When_an_item_is_add_todolist_should_reflect_auto()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);

            var todoList = new TodoList("A todo list")
            {
                Items = new List<TodoItem>
                {
                    new TodoItem
                    {
                        Name = "Item 1",
                        Id = Guid.NewGuid(),
                        IsCompleted = false
                    },
                    new TodoItem
                    {
                        Name = "Item 2",
                        Id = Guid.NewGuid(),
                        IsCompleted = false
                    },
                    new TodoItem
                    {
                        Name = "Item 3",
                        Id = Guid.NewGuid(),
                        IsCompleted = false
                    }
                }
            };

            // Act
            manager.AddItem("Item 4", todoList);

            // Assert
            todoList.Items.Should().ContainSingle(x => x.Name == "Item 4");
        }
    }
}