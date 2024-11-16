using FluentAssertions;
using Moq;
using ToDo.Application.ToDoItems.Commands.CreateTask;
using ToDo.Domain.Entities;
using ToDo.Domain.Repositories;

namespace ToDo.Application.Tests
{
    public class CreateToDoItemCommandHandlerTest
    {
        private readonly Mock<IToDoItemRepository> _repositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateToDoItemCommandHandlerTest()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_Success_ReturnId()
        {
            //Arrange
            var command = new CreateToDoItemCommand("Title");
            var handler = new CreateToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

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
            var handler = new CreateToDoItemCommandHandler(_unitOfWorkMock.Object, _repositoryMock.Object);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            result.IsSuccessful.Should().BeFalse();
            result.Error.Should().NotBeNull();
            result.Error.Should().Be(exception);
        }
    }
}
