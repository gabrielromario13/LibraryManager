using LibraryManager.Application.Commands.BookCommands;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using Moq;
using Shouldly;
using Xunit;

namespace LibraryManager.Tests.Application.Commands;

public class BookCommandHandlerTests
{
    private readonly Mock<IBookRepository> _mockRepo;

    public BookCommandHandlerTests()
    {
        _mockRepo = new Mock<IBookRepository>();
    }

    [Fact]
    public async Task Handle_ShouldCallAddWithCorrectBook()
    {
        // Arrange
        var handler = new InsertBookCommandHandler(_mockRepo.Object);
        var command = new InsertBookCommand {
            Title = "a dumbass perspective",
            Author = "romarinsta",
            Isbn = "adedsgsdf",
            PublishedYear = 2020,
            TotalCopies = 100
        };

        var expectedBook = command.ToEntity();
        expectedBook.SetId(123);

        _mockRepo
            .Setup(r => r.Add(It.IsAny<Book>()))
            .Callback<Book>(b => b.SetId(123))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        _mockRepo.Verify(r => r.Add(It.Is<Book>(b =>
            b.Title == expectedBook.Title &&
            b.Author == expectedBook.Author &&
            b.Isbn == expectedBook.Isbn &&
            b.PublishedYear == expectedBook.PublishedYear &&
            b.TotalCopies == expectedBook.TotalCopies)), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessWithBookId()
    {
        // Arrange
        var handler = new InsertBookCommandHandler(_mockRepo.Object);
        var command = new InsertBookCommand {
            Title = "a dumbass perspective",
            Author = "romarinsta",
            Isbn = "adedsgsdf",
            PublishedYear = 2020,
            TotalCopies = 100
        };

        var book = command.ToEntity();
        book.SetId(123);

        _mockRepo
            .Setup(r => r.Add(It.IsAny<Book>()))
            .Callback<Book>(b => b.SetId(book.Id))
            .Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldBe(123);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResultWithBookId()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var handler = new UpdateBookCommandHandler(mockRepo.Object);
        var command = new UpdateBookCommand { Id = 2, Title = "Some Book" };

        var book = CreateBook("Old Title");
        book.SetId(2);

        mockRepo.Setup(r => r.GetById(2)).ReturnsAsync(book);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldBe(2);
    }

    [Fact]
    public async Task Handle_ShouldUpdateBook_WhenBookExists()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var handler = new UpdateBookCommandHandler(mockRepo.Object);
        var command = new UpdateBookCommand
        {
            Id = 1,
            Title = "Updated Title",
            Author = "New Author",
            Isbn = "999-XYZ",
            PublishedYear = 2022,
            TotalCopies = 10,
            AvailableCopies = 7
        };

        var book = CreateBook("Original Title");
        book.SetId(1);

        mockRepo.Setup(r => r.GetById(1)).ReturnsAsync(book);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        mockRepo.Verify(r => r.Update(It.Is<Book>(b =>
            b.Id == 1 &&
            b.Title == command.Title &&
            b.Author == command.Author &&
            b.Isbn == command.Isbn &&
            b.PublishedYear == command.PublishedYear &&
            b.TotalCopies == command.TotalCopies &&
            b.AvailableCopies == command.AvailableCopies
        )), Times.Once);

        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeTrue();
        result.Data.ShouldBe(1);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenBookDoesNotExist()
    {
        // Arrange
        var mockRepo = new Mock<IBookRepository>();
        var handler = new UpdateBookCommandHandler(mockRepo.Object);
        var command = new UpdateBookCommand { Id = 1 };

        mockRepo.Setup(r => r.GetById(1)).ReturnsAsync((Book)null!);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.ShouldNotBeNull();
        result.IsSuccess.ShouldBeFalse();
        result.Message.ShouldContain("Livro não encontrado.");

        mockRepo.Verify(r => r.Update(It.IsAny<Book>()), Times.Never);
    }

    private static Book CreateBook(
        string title = "Default",
        string author = "Author",
        string isbn = "000-000",
        int year = 2020,
        short totalCopies = 5) =>
        new(title, author, isbn, year, totalCopies);
}