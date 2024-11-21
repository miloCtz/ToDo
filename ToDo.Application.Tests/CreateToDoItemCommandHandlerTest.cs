using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using ToDo.Application.ToDoItems.Commands.CompleteTask;
using ToDo.Application.ToDoItems.Commands.CreateTask;
using ToDo.Domain.Entities;

namespace ToDo.Application.Tests
{
    public class CreateToDoItemCommandHandlerTest : TestHandlerBase
    {
        readonly Mock<ILogger<CreateToDoItemCommandHandler>> _loggerMock;
        readonly CreateToDoItemCommandHandler _handler;

        public CreateToDoItemCommandHandlerTest()
            : base()
        {
            _loggerMock = new Mock<ILogger<CreateToDoItemCommandHandler>>();
            _handler = new CreateToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Success_ReturnId()
        {
            //Arrange
            var command = new CreateToDoItemCommand("Title");
            var handler = new CreateToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object, _loggerMock.Object);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            _repositoryMock.Verify(x => x.Add(It.IsAny<ToDoItem>()), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Fail_ThrowsException()
        {
            //Arrange
            var exception = new Exception("Test");
            _repositoryMock.Setup(
                    x => x.Add(It.IsAny<ToDoItem>()))
                .Throws(exception);


            var command = new CreateToDoItemCommand("Title");            

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Should().Be(exception);
        }
    }
}
