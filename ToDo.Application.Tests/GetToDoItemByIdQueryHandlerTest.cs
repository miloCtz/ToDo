using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ToDo.Application.ToDoItems.Queries.GetItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Errors;

namespace ToDo.Application.Tests
{
    public class GetToDoItemByIdQueryHandlerTest : TestHandlerBase
    {
        readonly Mock<ILogger<GetToDoItemByIdQueryHandler>> _loggerMock;
        readonly GetToDoItemByIdQueryHandler _handler;


        public GetToDoItemByIdQueryHandlerTest()
            : base()
        {
            _loggerMock = new Mock<ILogger<GetToDoItemByIdQueryHandler>>();
            _handler = new GetToDoItemByIdQueryHandler(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Success()
        {
            //Arrange
            var toDoItem = ToDoItem.Create("Test");

            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(toDoItem));

            var command = new GetToDoItemByIdQuery(1);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Title.Should().Be(toDoItem.Title);
            _repositoryMock.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }


        [Fact]
        public async Task Handle_Should_Fail_ReturnItemNotFound()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(default(ToDoItem)));

            var command = new GetToDoItemByIdQuery(1);
            var handler = new GetToDoItemByIdQueryHandler(_repositoryMock.Object, _loggerMock.Object);
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Message.Should().Be(error.Message);
            _repositoryMock.Verify(x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Fail_ThrowsException()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var command = new GetToDoItemByIdQuery(1);
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Code.Should().Be(typeof(Exception).Name);
            _repositoryMock.Verify(x => x.Add(It.IsAny<ToDoItem>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
