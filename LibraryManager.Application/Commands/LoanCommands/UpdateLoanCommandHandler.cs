using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.LoanCommands;

public class UpdateLoanCommandHandler(
    ILoanRepository loanRepository,
    IBookRepository bookRepository)
    : IRequestHandler<UpdateLoanCommand, ResultViewModel<int>>
{
    public async Task<ResultViewModel<int>> Handle(
        UpdateLoanCommand request,
        CancellationToken cancellationToken)
    {
        var loan = await loanRepository.GetById(request.Id);
        if (loan is null)
            return ResultViewModel<int>.Error("Empréstimo não encontrado.");

        var book = (await bookRepository.GetById(loan.BookId))!;
        book.Available();
        await bookRepository.Update(book);
        
        loan.SetReturnDate();
        await loanRepository.Update(loan);

        return ResultViewModel<int>.Success(loan.Id);
    }
}