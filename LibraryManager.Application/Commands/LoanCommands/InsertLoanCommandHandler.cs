using System.Linq.Expressions;
using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Enums;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class InsertLoanCommandHandler(
    ILoanRepository repository,
    IBookRepository bookRepository)
    : IRequestHandler<InsertLoanCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        InsertLoanCommand request,
        CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetSingle(GetExpression(request.BookId));
        if (book is null)
            return ResultViewModel<int>.Error("Livro não encontrado.");
        
        if (book.Status != BookStatus.Available)
            return ResultViewModel<int>.Error("Livro indisponível.");
        
        book.Loaned();
        await bookRepository.Update(book);
        
        var loan = request.ToEntity();
        await repository.Add(loan);
        
        return ResultViewModel<int>.Success(loan.Id);
    }

    private static Expression<Func<Book, bool>> GetExpression(int bookId)
    {
        return book =>
            book.Id == bookId &&
            book.AvailableCopies >= 1;
    }
}