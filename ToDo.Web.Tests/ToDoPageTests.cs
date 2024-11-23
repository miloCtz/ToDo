using Bunit;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using ToDo.Application.ToDoItems.Commands.CreateItem;
using ToDo.Application.ToDoItems.Commands.DeleteItem;
using ToDo.Application.ToDoItems.Queries.GetItem;
using ToDo.Domain.Shared;
using ToDo.Web.Components.Pages;
using Unit = ToDo.Domain.Shared.Unit;

namespace ToDo.Web.Tests
{
    public class ToDoPageTests : TestContext
    {
        private readonly Mock<ISender> _senderMock;
        private readonly List<ToDoItemResponse> _items;

        public ToDoPageTests()
        {
            _senderMock = new Mock<ISender>();
            _items = new List<ToDoItemResponse>
            {
                new(1, "TitleTest1", false),
                new(2, "TitleTest2", true)
            };
        }

        [Fact]
        public void OnInitializedAsync_Should_Return_Items()
        {
            //Arrange
            var response = new ToDoItemAllResponse(_items);
            var result = Result.Success(response);

            _senderMock
                .Setup(x => x.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            Services.AddSingleton(_senderMock.Object);

            //Act
            var component = RenderComponent<Home>();
            var items = component.FindAll(AppCssSelectors.ToDoElements);
            var errorMessage = component.FindAll(AppCssSelectors.ErrorElement);

            //Act
            errorMessage.Should().BeEmpty();
            items.Count.Should().Be(_items.Count);
            items[0].TextContent.Should().Contain(_items[0].Title);
        }

        [Fact]
        public void OnInitializedAsync_Should_Return_Fail()
        {
            //Arrange
            var result = Result.Failure<ToDoItemAllResponse>(new Exception("Exception"));

            _senderMock
                .Setup(x => x.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            Services.AddSingleton(_senderMock.Object);

            //Act
            var component = RenderComponent<Home>();
            var items = component.FindAll(AppCssSelectors.ToDoElements);
            var errorMessage = component.Find(AppCssSelectors.ErrorElement);

            //Act
            items.Should().BeEmpty();
            errorMessage.Should().NotBeNull();
            errorMessage.TextContent.Should().Contain("Exception");
        }

        [Fact]
        public void SaveItemAsync_Should_Success()
        {
            //Arrange
            var response = new ToDoItemAllResponse(_items);
            var result = Result.Success(response);

            _senderMock
                .Setup(x => x.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var addResult = Result.Success(3);
            _senderMock
                .Setup(x => x.Send(It.IsAny<CreateToDoItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addResult);

            Services.AddSingleton(_senderMock.Object);

            //Act
            var component = RenderComponent<Home>();

            var input = component.Find(AppCssSelectors.AddInputElement);
            var button = component.Find(AppCssSelectors.AddButtonElement);
            input.Change("NewText");
            button.Click();
            var items = component.FindAll(AppCssSelectors.ToDoElements);
            var errorMessage = component.FindAll(AppCssSelectors.ErrorElement);

            //Act            
            errorMessage.Should().BeEmpty();
            items.Count.Should().Be(3);
            items[2].TextContent.Should().Contain("NewText");
        }

        [Fact]
        public void SaveItemAsync_Should_Return_Fail()
        {
            //Arrange
            var response = new ToDoItemAllResponse(_items);
            var result = Result.Success(response);

            _senderMock
                .Setup(x => x.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var addResult = Result.Failure<int>(new Domain.Shared.Error("ToDo", "InvalidTitle"));

            _senderMock
                .Setup(x => x.Send(It.IsAny<CreateToDoItemCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addResult);

            Services.AddSingleton(_senderMock.Object);

            //Act
            var component = RenderComponent<Home>();

            var input = component.Find(AppCssSelectors.AddInputElement);
            var button = component.Find(AppCssSelectors.AddButtonElement);
            input.Change("");
            button.Click();
            var items = component.FindAll(AppCssSelectors.ToDoElements);
            var errorMessage = component.Find(AppCssSelectors.ErrorElement);

            //Act            
            errorMessage.Should().NotBeNull();
            errorMessage.TextContent.Should().Contain("InvalidTitle");
            items.Count.Should().Be(2);
        }

        [Fact]
        public void DeleteItemAsycn_Should_Success()
        {
            //Arrange
            var response = new ToDoItemAllResponse(_items);
            var result = Result.Success(response);

            _senderMock
                .Setup(x => x.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var addResult = Result.Success(Unit.Value);

            _senderMock
                .Setup(x => x.Send(It.IsAny<DeleteTaskCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addResult);

            Services.AddSingleton(_senderMock.Object);

            //Act
            var component = RenderComponent<Home>();
            var deleteButton = component.Find(AppCssSelectors.DeleteButtonElement);
            deleteButton.Click();
            var items = component.FindAll(AppCssSelectors.ToDoElements);

            //Act            
            items.Should().NotBeEmpty();
            items.Count.Should().Be(1);
        }

        [Fact]
        public void DeleteItemAsycn_Should_Fail()
        {
            //Arrange
            var response = new ToDoItemAllResponse(_items);
            var result = Result.Success(response);

            _senderMock
                .Setup(x => x.Send(It.IsAny<GetAllToDoItemQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(result);

            var addResult = Result.Failure<Unit>(new Exception("Exception"));

            _senderMock
                .Setup(x => x.Send(It.IsAny<DeleteTaskCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(addResult);

            Services.AddSingleton(_senderMock.Object);

            //Act
            var component = RenderComponent<Home>();
            var deleteButton = component.Find(AppCssSelectors.DeleteButtonElement);
            deleteButton.Click();
            var items = component.FindAll(AppCssSelectors.ToDoElements);
            var errorMessage = component.Find(AppCssSelectors.ErrorElement);

            //Act            
            items.Should().NotBeEmpty();
            items.Count.Should().Be(2);
            errorMessage.Should().NotBeNull();
            errorMessage.TextContent.Should().Contain("Exception");
        }
    }
}
