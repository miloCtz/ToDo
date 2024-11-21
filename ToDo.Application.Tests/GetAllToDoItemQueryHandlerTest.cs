using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ToDo.Application.ToDoItems.Queries.GetToDoItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Errors;

namespace ToDo.Application.Tests
{
    public class GetAllToDoItemQueryHandlerTest : TestHandlerBase
    {
        readonly Mock<ILogger<GetAllToDoItemQueryHandler>> _loggerMock;
        readonly GetAllToDoItemQueryHandler _handler;

        public GetAllToDoItemQueryHandlerTest()
            : base()
        {
            _loggerMock = new Mock<ILogger<GetAllToDoItemQueryHandler>>();
            _handler = new GetAllToDoItemQueryHandler(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Success()
        {
            //Arrange
            var toDoItems = new List<ToDoItem> { ToDoItem.Create("Test") };

            _repositoryMock.Setup(
                x => x.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(toDoItems));

            var command = new GetAllToDoItemQuery();

            //Act
            var result = await _handler.Handle(command, default);

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
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.GetType().Should().Be(typeof(Exception));
        }
    }
}
