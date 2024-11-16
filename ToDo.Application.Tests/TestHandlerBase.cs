using Moq;
using ToDo.Domain.Repositories;

namespace ToDo.Application.Tests
{
    public abstract class TestHandlerBase
    {
        protected readonly Mock<IToDoItemRepository> _repositoryMock;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;

        protected TestHandlerBase()
        {
            _repositoryMock = new();
            _unitOfWorkMock = new();
        }
    }
}
