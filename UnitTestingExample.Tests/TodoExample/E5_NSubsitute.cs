namespace UnitTestingExample.Tests.TodoExample
{
    using System.Linq;
    using System.Threading.Tasks;

    using FluentAssertions;

    using NSubstitute;

    using Ploeh.AutoFixture;

    using UnitTestingExample.TodoExample;

    using Xunit;

    // ReSharper disable once InconsistentNaming
    public class E5_NSubstitute
    {
        // [ ] Why?
        // [ ] Creating Mocks
        // [ ] Diff from Autofixtures?

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
    }
}