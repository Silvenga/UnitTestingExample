namespace UnitTestingExample.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using Xunit;

    public class TodoListMangerFacts
    {
        #region Base Example

        [Fact]
        public void When_given_a_new_title_then_change_title_of_todolist_old()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");

            // Act
            manager.ChangeTitle("new title", todoList);

            // Assert
            Assert.Equal("new title", todoList.Title);
        }

        #endregion

        #region Fluent Validations

        [Fact]
        public void When_given_a_new_title_then_change_title_of_todolist_fluent()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");

            // Act
            manager.ChangeTitle("new title", todoList);

            // Assert
            todoList.Title.Should().Be("new title");
        }

        [Fact]
        public void When_given_an_empty_title_then_throw_fluent()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");

            // Act
            Action action = () => manager.ChangeTitle(string.Empty, todoList);

            // Assert
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void When_an_item_is_added_todolist_should_reflect()
        {
            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = new TodoList("current title");
            var itemName = "item name";

            // Act
            manager.AddItem(itemName, todoList);

            // Assert
            todoList.Items.Should().ContainSingle()
                     .Which.Name.Should().Be(itemName);
        }

        #endregion

        #region Autofixtures

        [Fact]
        public void When_an_item_is_add_todolist_should_reflect_auto()
        {
            var autoFixture = new Fixture();

            var manager = new TodoListManager(notificationManager: null, apiManager: null);
            var todoList = autoFixture.Build<TodoList>()
                                       .Without(x => x.Items)
                                       .With(x=>x.Title, "Hello")
                                       .Create();
            var itemName = autoFixture.Create<string>();

            // Act
            manager.AddItem(itemName, todoList);

            // Assert
            todoList.Items.Should().ContainSingle()
                     .Which.Name.Should().Be(itemName);
        }
            #endregion

        #region NSubstitute

        [Fact]
        public void When_downloading_remote_add_to_todo_list()
        {
            var autoFixture = new Fixture();
            var notificationManager = Substitute.For<INotificationManager>();
            var apiManager = Substitute.For<IRemoteApiManager>();

            var manager = new TodoListManager(notificationManager, apiManager);
            var todoList = autoFixture.Create<TodoList>();

            var remoteItems = autoFixture.CreateMany<TodoItem>().ToList(); // Autofixture can create lists of things too!
            apiManager.GetRemoveItems(Arg.Any<string>()) // The use of Arg can really help
                       .Returns(remoteItems); // Single API for intercepting returns

            // Act
            manager.DownloadRemoteItems(todoList);

            // Assert
            todoList.Items.Should().Contain(remoteItems);
        }

        [Fact]
        public void When_completing_an_item_send_notification()
        {
            var autoFixture = new Fixture();
            var notificationManager = Substitute.For<INotificationManager>();
            var apiManager = Substitute.For<IRemoteApiManager>();
            var todoList = autoFixture.Create<TodoList>();

            var name = autoFixture.Create<string>();

            var manager = new TodoListManager(notificationManager, apiManager);
            var id = manager.AddItem(name, todoList);

            // Act
            manager.CompleteItem(id, todoList);

            // Assert
            notificationManager.Received().NotifyOfCompletedTask(name);
        }

        [Fact]
        public async Task When_completing_an_item_send_notification_async()
        {
            var autoFixture = new Fixture();
            var notificationManager = Substitute.For<INotificationManager>();
            var apiManager = Substitute.For<IRemoteApiManager>();
            var todoList = autoFixture.Create<TodoList>();

            var name = autoFixture.Create<string>();

            var manager = new TodoListManager(notificationManager, apiManager);
            var id = manager.AddItem(name, todoList);

            // Act
            await manager.CompleteItemAsync(id, todoList);

            // Assert
            await notificationManager.Received().NotifyOfCompletedTaskAsync(name);
        }

        #endregion
    }
}