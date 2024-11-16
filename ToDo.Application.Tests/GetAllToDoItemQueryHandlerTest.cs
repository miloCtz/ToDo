using FluentAssertions;
using Moq;
using ToDo.Application.ToDoItems.Queries.GetToDoItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Errors;

namespace ToDo.Application.Tests
{
    public class GetAllToDoItemQueryHandlerTest : TestHandlerBase
    {
        [Fact]
        public async Task Handle_Should_Success()
        {
            //Arrange
            var toDoItems = new List<ToDoItem> { ToDoItem.Create("Test") };

            _repositoryMock.Setup(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(toDoItems.AsEnumerable()));

            var command = new GetAllToDoItemQuery();
            var handler = new GetAllToDoItemQueryHandler(_repositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.ToDoItems.Should().HaveCount(toDoItems.Count);
            _repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Fail_ThrowsException()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var command = new GetAllToDoItemQuery();
            var handler = new GetAllToDoItemQueryHandler(_repositoryMock.Object);
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.GetType().Should().Be(typeof(Exception));
        }
    }
}
