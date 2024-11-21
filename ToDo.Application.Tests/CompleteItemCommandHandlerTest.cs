using FluentAssertions;
using Moq;
using ToDo.Application.ToDoItems.Commands.CompleteTask;
using ToDo.Domain.Entities;
using ToDo.Domain.Errors;

namespace ToDo.Application.Tests
{
    public class CompleteItemCommandHandlerTest : TestHandlerBase
    {
        [Fact]
        public async Task Handle_Should_Success()
        {
            //Arrange
            var toDoItem = ToDoItem.Create("Test");

            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(toDoItem));

            var command = new CompleteToDoItemCommand(1);
            var handler = new CompleteToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            toDoItem.IsDone.Should().BeTrue();            
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Success_IsNotDone()
        {
            //Arrange
            var toDoItem = ToDoItem.Create("Test");
            toDoItem.Toggle();

            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(toDoItem));

            var command = new CompleteToDoItemCommand(1);
            var handler = new CompleteToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeTrue();
            toDoItem.IsDone.Should().BeFalse();            
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Fail_ReturnNotItemFound()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(default(ToDoItem)));

            var command = new CompleteToDoItemCommand(1);
            var handler = new CompleteToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Message.Should().Be(error.Message);
            _repositoryMock.Verify(x => x.Add(It.IsAny<ToDoItem>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_Should_Fail_ThrowsException()
        {
            //Arrange
            _repositoryMock.Setup(
                x => x.GetAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .Throws<Exception>();

            var command = new CompleteToDoItemCommand(1);
            var handler = new CompleteToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);
            var error = DomainErrors.ToDoList.NotFound(1);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.GetType().Should().Be(typeof(Exception));
            _repositoryMock.Verify(x => x.Add(It.IsAny<ToDoItem>()), Times.Never);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
