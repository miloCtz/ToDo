using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ToDo.Application.ToDoItems.Commands.UpdateItem;
using ToDo.Domain.Entities;
using ToDo.Domain.Errors;

namespace ToDo.Application.Tests
{
    public class UpdateTaskCommandHanlderTest : TestHandlerBase
    {
        readonly Mock<ILogger<UpdateTaskCommandHanlder>> _loggerMock;
        readonly UpdateTaskCommandHanlder _handler;

        public UpdateTaskCommandHanlderTest()
            : base()
        {
            _loggerMock = new Mock<ILogger<UpdateTaskCommandHanlder>>();
            _handler = new UpdateTaskCommandHanlder(_repositoryMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Success()
        {
            //Arrange
            var toDoItem = ToDoItem.Create("Test");

            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(toDoItem));

            var command = new UpdateTaskCommand(1, "New Title");

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeTrue();
            toDoItem.Title.Should().Be("New Title");
            _repositoryMock.Verify(x => x.Add(It.IsAny<ToDoItem>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Fail_ReturnItemNotFound()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(default(ToDoItem)));

            var command = new UpdateTaskCommand(1, "New Title");
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Message.Should().Be(error.Message);
            _repositoryMock.Verify(x => x.Delete(It.IsAny<ToDoItem>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Fail_ThrowsException()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var command = new UpdateTaskCommand(1, "New Title");
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
