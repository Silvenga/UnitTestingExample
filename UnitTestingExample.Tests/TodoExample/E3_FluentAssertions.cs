namespace UnitTestingExample.Tests.TodoExample
{
    using System;

    using UnitTestingExample.TodoExample;

    using Xunit;

    // ReSharper disable once InconsistentNaming
    public class E3_FluentAssertions
    {
        // [ ] Values
        // [ ] Collections
        // [ ] Exceptions

        [Fact]
        public void When_given_a_new_title_then_change_title_of_todolist()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");

            // Act
            manager.ChangeTitle("new title", todoList);

            // Assert
            Assert.Equal("new title", todoList.Title);
        }

        [Fact]
        public void When_a_new_item_is_added_todo_list_should_contain_item_of_name()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");

            // Act
            manager.AddItem("some name", todoList);

            // Assert
            Assert.Contains(todoList.Items, item => item.Name == "some name");
        }

        [Fact]
        public void When_given_an_empty_title_then_throw_fluent()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");

            // Act
            var isThrown = false;
            try
            {
                manager.ChangeTitle(string.Empty, todoList);
            }
            catch (ArgumentException)
            {
                isThrown = true;
            }

            // Assert
            Assert.True(isThrown);
        }
    }
}